<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="InventoryTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.InventoryTracker" %>


<%@ Register Src="~/UserControls/ucItemSerialView1.ascx" TagPrefix="uc1" TagName="ucItemSerialView1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function CheckAllItem(Checkbox) {
            // alert('S');
            var GridVwHeaderChckbox = document.getElementById("<%=grvAllLocation.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function CheckAllLocation() {
            var grdItems = document.getElementById("<%=grvAllLocation.ClientID %>");

            for (i = 1; i < grdItems.rows.length; i++) {
                grdItems.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = true;
            }
        }

        function UncheckAllLocation() {
            var grdItems = document.getElementById("<%=grvAllLocation.ClientID %>");

            for (i = 1; i < grdItems.rows.length; i++) {
                grdItems.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
            }
        }

        function makeAlert() {
            alert(document.getElementById("<%= hdfTabIndex.ClientID %>").value);
        }

        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };



        function SetHdfTabIndex(tabId) {
            document.getElementById('<%=hdfClearData.ClientID %>').value = tabId;
        };

        function LocationChange() {
            if (document.getElementById('').value) {
                document.getElementById('txtval2').value = document.getElementById('txtval1').value * 2 / 3;
            }
        }
    </script>
    <script type="text/javascript">
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };
        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
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
        function showNoticeToast() {
            $().toastmessage('showNoticeToast', "Notice  Dialog which is fading away ...");
        }
        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showWarningToast() {
            $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
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

        function showErrorToast() {
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
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

    <script>
        function ConfirmAdvancedSearch() {
            var selectedvalue = confirm("Do you want to search item details in advance ?");
            if (selectedvalue) {
                document.getElementById('<%=hdfAdvancedSearch.ClientID %>').value = "1";
                return true;
            } else {
                document.getElementById('<%=hdfAdvancedSearch.ClientID %>').value = "0";
                return true;
            }
        }

        function ConfirmLocation() {
            var selectedvalue = confirm("Do you want to search all items in all locations?(It might take a long time) ");
            if (selectedvalue) {
                document.getElementById('<%=hdfAllLocation.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfAllLocation.ClientID %>').value = "0";
            }
        };
        function ConfirmChanel() {
            var selectedvalue = confirm("Do you want to search all items in the selected channel?(It might take a long time) ");
            if (selectedvalue) {
                document.getElementById('<%=hdfChannel.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfChannel.ClientID %>').value = "0";
            }
        };


    </script>
    <style>
        .panel {
            padding-bottom: 0px;
            padding-top: 0px;
            margin-top: 0px;
            margin-bottom: 1px;
        }
    </style>
    <style type="text/css">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel2">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:HiddenField ID="hdfUserLevel" runat="server" />
    <asp:HiddenField ID="hdfSelectLocation" runat="server" />
    <%--<asp:HiddenField ID="hdfTabIndex" runat="server" Value="InventoryTracker" />--%>
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <asp:HiddenField ID="hdfSubTabIndex" runat="server" />
    <asp:HiddenField ID="hdfAlertInfo" runat="server" Value="0" />
    <asp:HiddenField ID="hdfClearData" runat="server" Value="0" />
    <asp:HiddenField ID="hdfAdvancedSearch" runat="server" Value="0" />
    <asp:HiddenField ID="hdfAllLocation" runat="server" Value="0" />
    <asp:HiddenField ID="hdfChannel" runat="server" Value="0" />

    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <div class="panel panel-default">
                <asp:Panel DefaultButton="lbtnView" runat="server">
                    <div class="row">
                        <div class="col-sm-8  buttonrow">
                        </div>
                        <div class="col-sm-4 buttonRow">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="" OnClick="lbtnView_Click"> <span class="glyphicon glyphicon-search" aria-hidden="true"></span>View </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click"> <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="panel panel-default marginLeftRight5">
                    <div class="panel-body">
                        <div id="tabArea">
                            <ul id="myTab" class="nav nav-tabs">
                                <li class="active"><a href="#InventoryTracker" data-toggle="tab">Inventory Tracker</a></li>
                                <%--<li><a href="#ScannedSerials" data-toggle="tab">Scanned Serials</a></li>--%>
                                <li onclick="document.getElementById('<%= lbtnProDetails.ClientID %>').click();"><a href="#ProductDetails" data-toggle="tab">Product Details</a></li>
                                <%--<li><a href="#ExcessStockEnquiry" data-toggle="tab">Excess Stock Enquiry</a></li>--%>
                                <li onclick="document.getElementById('<%= lbtnDocWiseBal.ClientID %>').click();"><a href="#DocumentWiseBal" data-toggle="tab">Balances by Purchase Document</a></li>
                                <li onclick="document.getElementById('<%= lbtnDocserialdet.ClientID %>').click();"><a href="#Documentwithserial" data-toggle="tab">Entry Request Details</a></li>
                                <li onclick="document.getElementById('<%= lbtnDocserialdet.ClientID %>').click();"><a href="#ItemAllocation" data-toggle="tab">Item Allocation Details</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane pade in active" id="InventoryTracker">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div id="Message" style="padding-top: 3px;">
                                                <asp:Button ID="btnLoads" runat="server" OnClick="btnLoad_Click" Visible="False" />
                                                <div class="row">

                                                    <div class="col-sm-3">

                                                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divSuccess" style="width: 500px">
                                                            <div class="col-sm-11  buttonrow ">
                                                                <strong>
                                                                    <asp:Label ID="lblAlertSuccess" runat="server" CssClass="labelText1"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-sm-1  buttonrow">
                                                                <div class="col-sm-3  buttonrow">
                                                                    <asp:LinkButton ID="lBtnAlertSuccess" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lBtnAlertSuccess_Click">
                                                                    <span class="glyphicon glyphicon-ok" aria-hidden="true" ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divDanger" style="width: 500px; height: 40px">
                                                            <div class="col-sm-11  buttonrow ">
                                                                <strong>
                                                                    <asp:Label ID="lblAlertDanger" runat="server" CssClass="labelText1"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-sm-1  buttonrow">
                                                                <div class="col-sm-3  buttonrow">
                                                                    <asp:LinkButton ID="lBtnAlertDanger" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lBtnAlertDanger_Click">
                                                            <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="divInfo" style="width: 500px">
                                                            <div class="col-sm-11  buttonrow ">
                                                                <strong>
                                                                    <asp:Label ID="lblAlertInfo" runat="server" CssClass="labelText1"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-sm-1  buttonrow">
                                                                <div class="col-sm-3  buttonrow">
                                                                    <asp:LinkButton ID="lBtnAlertInfoOk" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lBtnAlertInfoOk_Click">
                                                            <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-3  buttonrow">
                                                                    <asp:LinkButton ID="lBtnAlertInfoCancel" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lBtnAlertInfoCancel_Click">
                                                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div>
                                                <div class="row">
                                                    <div class="col-sm-12" style="padding-bottom: 1px; padding-top: 1px;">
                                                        <div class="col-sm-9" style="padding-left: 3px; padding-right: 3px;">
                                                            <div class="panel panel-default" style="height: 175px; padding-bottom: 1px; padding-top: 1px;">
                                                                <div class="panel-heading">
                                                                    Item Details
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-5">

                                                                            <div class="row">
                                                                                <asp:Label runat="server" ID="lblTestLabel" Visible="False">Test Label</asp:Label>
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Item
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtItemCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnItemCode" CausesValidation="false" runat="server" OnClick="lBtnItemCode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>

                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Cate1
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtMainCategory" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnMainCategory" CausesValidation="false" runat="server" OnClick="lBtnMainCategory_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>


                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Model
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtModel" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnModel" CausesValidation="false" runat="server" OnClick="lBtnModel_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>

                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Cate2
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtSubCategory" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnSubCategory" CausesValidation="false" runat="server" OnClick="lBtnSubCategory_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Brand
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtBrand" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnBrand" CausesValidation="false" runat="server" OnClick="lBtnBrand_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>

                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Cate3
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtItemRange" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnItemRange" CausesValidation="false" runat="server" OnClick="lBtnItemRange_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Status
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:DropDownList ID="ddlItemStatus" runat="server" class="form-control"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <%-- <asp:LinkButton ID="lBtnItemStatus" CausesValidation="false" runat="server">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>--%>
                                                                                    </div>

                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Cate4
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtCat4" CausesValidation="false" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnSeCat4" CausesValidation="false" runat="server" OnClick="lbtnSeCat4_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <%--<div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Cate 1
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtMainCategory" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnMainCategory" CausesValidation="false" runat="server" OnClick="lBtnMainCategory_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Cate 2
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtSubCategory" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnSubCategory" CausesValidation="false" runat="server" OnClick="lBtnSubCategory_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Cate 3
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtItemRange" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lBtnItemRange" CausesValidation="false" runat="server" OnClick="lBtnItemRange_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Cate 4
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtCat4" CausesValidation="false" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnSeCat4" CausesValidation="false" runat="server" OnClick="lbtnSeCat4_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Cate 5
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtCat5" CausesValidation="false" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnSeCat5" CausesValidation="false" runat="server" OnClick="lbtnSeCat5_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>--%>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-1 height22 paddingRight0">
                                                                                        <asp:CheckBox ID="chkShowCostValue" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkShowCostValue_CheckedChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-5 paddingLeft1">
                                                                                        <asp:Label runat="server" ID="lblShowCostValue" CssClass="labelText1">Show Cost Value</asp:Label>
                                                                                    </div>
                                                                                    <%--<div class="col-sm-2 paddingLeft0"></div>--%>

                                                                                    <div class="col-sm-1 labelText1">
                                                                                        Cate5
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtCat5" CausesValidation="false" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnSeCat5" CausesValidation="false" runat="server" OnClick="lbtnSeCat5_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-7">
                                                                            <div class="col-sm-5">
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Company
                                                                                        </div>


                                                                                        <div class="col-sm-5 paddingRight5">
                                                                                            <asp:TextBox runat="server" ID="txtCompany" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Enabled="true"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-2 paddingLeft0">
                                                                                            <asp:LinkButton ID="lBtnCompany" Enabled="true" CausesValidation="false" runat="server" OnClick="lBtnCompany_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Location
                                                                                        </div>

                                                                                        <div class="col-sm-5 paddingRight5">
                                                                                            <asp:TextBox runat="server" ID="txtLocation" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lBtnLocation" CausesValidation="false" runat="server" OnClick="lBtnLocation_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>

                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:CheckBox ID="chkAllLocation" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAllLocation_CheckedChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            All
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-2  height22">
                                                                                            <asp:CheckBox ID="chkChannel" runat="server" OnCheckedChanged="chkChannel_CheckedChanged" AutoPostBack="True" />
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                                            Channel
                                                                                        </div>

                                                                                        <div class="col-sm-5" style="padding-right: 5px">

                                                                                            <asp:TextBox runat="server" ID="txtChannel" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>

                                                                                        </div>

                                                                                        <div class="col-sm-2" style="padding-left: 1px;">
                                                                                            <asp:LinkButton ID="lBtnChannel" CausesValidation="false" runat="server" OnClick="lBtnChannel_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>


                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Location
                                                                                        </div>


                                                                                        <div class="col-sm-5 paddingRight5">
                                                                                            <asp:TextBox runat="server" ID="txtChannelLocation" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Enabled="true"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-2 paddingLeft0">
                                                                                            <asp:LinkButton ID="lBtnChanLocation" Enabled="true" CausesValidation="false" runat="server" OnClick="lBtnChanLocation_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                                <%-- By Dilshan  --%>
                                                                                 <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Bin
                                                                                        </div>


                                                                                        <div class="col-sm-5 paddingRight5">
                                                                                            <asp:TextBox runat="server" ID="txtBinCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Enabled="true"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-2 paddingLeft0">
                                                                                            <asp:LinkButton ID="lBtnBinCode" Enabled="true" CausesValidation="false" runat="server" OnClick="lBtnBinCode_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>



                                                                            <div class="col-sm-7">
                                                                                <div class="panel panel-default">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div id="panel_chanel" runat="server">
                                                                                                <div class="panel-body" style="height: 120px; overflow: auto;">

                                                                                                    <asp:GridView ID="grvAllLocation" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager" EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="4">
                                                                                                        <EmptyDataTemplate>
                                                                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <th scope="col">Loc
                                                                                                                        </th>
                                                                                                                        <th scope="col">Description
                                                                                                                        </th>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>No records found.
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                            </table>
                                                                                                        </EmptyDataTemplate>

                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                <HeaderTemplate>
                                                                                                                    <asp:CheckBox ID="chkboxSelectAll" onclick="CheckAllItem(this);" runat="server" Checked="true" AutoPostBack="true" />
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkLocationRow" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="10px" />
                                                                                                                <ItemStyle Width="10px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="Ml_loc_cd" HeaderText="Loc" SortExpression="CompanyCode" />
                                                                                                            <asp:BoundField DataField="Ml_loc_desc" HeaderText="Description" SortExpression="RoleId"></asp:BoundField>
                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                <HeaderTemplate>
                                                                                                                    <asp:LinkButton ID="lbtnLocationClear" CausesValidation="false" runat="server" OnClick="lbtnLocationClear_Click">
                                                                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"  ></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </HeaderTemplate>
                                                                                                                <HeaderStyle Width="10px" />
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <PagerStyle CssClass="cssPager" />
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



                                                        <div class="col-sm-3" style="padding-left: 3px; padding-right: 3px; height: 175px; overflow-y: auto; overflow-x: hidden;">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default" style="height: 256px;">
                                                                        <div class="panel-heading">
                                                                            <div class="row" style="height: 17px;">
                                                                                <div class="col-sm-4 paddingRight0">
                                                                                    Advanced Search
                                                                                </div>
                                                                                <div class="col-sm-1 height20 paddingLeft0">
                                                                                    <asp:CheckBox ID="chkAdvancedSearch" AutoPostBack="true" Enabled="true" runat="server" OnCheckedChanged="chkAdvancedSearch_CheckedChanged" />
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                        <div class="" style="padding-top: 1px; padding-bottom: 1px; padding-left: 1px; padding-right: 1px;">
                                                                            <asp:Panel ID="panel_advanceSearch" runat="server">
                                                                                <div>
                                                                                    <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                                                                                </div>

                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div id="bottomGrid" class="panel-body" style="padding-bottom: 1px; padding-top: 1px;">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-body">
                                                                            <asp:Label runat="server" ID="lblStock" CssClass="labelText1" ForeColor="#CC0000">
                                                                        :: Only received stocks are displayed. [ GIT not considered ] ::</asp:Label>
                                                                            <div id="grdItemDiv" class="row">
                                                                                <div class="col-sm-12" style="height: 250px;">
                                                                                    <asp:GridView ID="dgvItemDetails" CssClass="table table-hover table-striped bound" runat="server"
                                                                                        GridLines="None" PagerStyle-CssClass="cssPager" EmptyDataText="No data found..."
                                                                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                                                                                        AllowSorting="true"
                                                                                        OnRowDataBound="dgvItemDetails_RowDataBound"
                                                                                        OnPageIndexChanging="dgvItemDetails_PageIndexChanging" OnRowCreated="dgvItemDetails_RowCreated"
                                                                                        OnSelectedIndexChanged="dgvItemDetails_SelectedIndexChanged" OnSorting="dgvItemDetails_Sorting">
                                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                                        <EmptyDataTemplate>
                                                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <th scope="col">Com</th>
                                                                                                        <th scope="col">Loc</th>
                                                                                                        <th scope="col">Item</th>
                                                                                                        <th scope="col">Description</th>
                                                                                                        <th scope="col">Model</th>
                                                                                                        <th scope="col">Status</th>
                                                                                                        <th scope="col">In Hand</th>
                                                                                                        <th scope="col">Free</th>
                                                                                                        <th scope="col">Reserved</th>
                                                                                                        <th scope="col">Cost</th>
                                                                                                        <th scope="col">Buffer</th>
                                                                                                        <th scope="col">UOM</th>
                                                                                                        <th scope="col">Img Select</th>
                                                                                                        <th scope="col">Res Details</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>No records found.
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                    </tr>
                                                                                            </table>
                                                                                        </EmptyDataTemplate>
                                                                                        <Columns>
                                                                                            <%--<asp:BoundField DataField="" HeaderText="Expire Date">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                                        </asp:BoundField>--%>
                                                                                            <%--<asp:BoundField DataField="COMPANY" HeaderText="Com" SortExpression="COMPANY">
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                                            </asp:BoundField>--%>
                                                                                            <asp:TemplateField HeaderText="Com" SortExpression="COMPANY">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCOMPANY" runat="server" Text='<%#Bind("COMPANY") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                                            </asp:TemplateField>
                                                                                            <%--<asp:BoundField DataField="LOC" HeaderText="Loc" SortExpression="LOC">--%>
                                                                                            <asp:TemplateField HeaderText="Loc" SortExpression="LOC">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblLocation" runat="server" ToolTip='<%#Bind("LOC_DESC") %>' Text='<%#Bind("LOC") %>' Width="80px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                                            </asp:TemplateField>
                                                                                            <%--  </asp:BoundField>--%>
                                                                                            <asp:TemplateField HeaderText="Item" SortExpression="ITEM_CODE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblITEM_CODE" runat="server" Text='<%#Bind("ITEM_CODE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                            </asp:TemplateField>

                                                                                            <%--<asp:BoundField DataField="ITEM_CODE" HeaderText="Item" SortExpression="ITEM_CODE">
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="150px"  />
                                                                                             </asp:BoundField>--%>
                                                                                            <%--<asp:BoundField DataField="ITEM_DESC" HeaderText="Description " SortExpression="ITEM_DESC">--%>
                                                                                            <asp:TemplateField HeaderText="Description" SortExpression="ITEM_DESC">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItmDesc" runat="server" ToolTip='<%#Bind("ITEM_DESC") %>' Text='<%#Eval("ITEM_DESC").ToString().Length > 35? (Eval("ITEM_DESC") as string).Substring(0,35) + " ..." : Eval("ITEM_DESC") %>' Width="250px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                                            </asp:TemplateField>
                                                                                            <%--</asp:BoundField>--%>

                                                                                            <%--<asp:BoundField DataField="MODEL" HeaderText="Model" SortExpression="MODEL">
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                            </asp:BoundField>
                                                                                            --%>
                                                                                            <asp:TemplateField HeaderText="Model" SortExpression="MODEL">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblMODEL_CODE" runat="server" Text='<%#Eval("MODEL") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                            </asp:TemplateField>
                                                                                            <%--<asp:BoundField DataField="ITEM_STATUS" HeaderText="Status" SortExpression="ITEM_STATUS">
                                                                                                <HeaderStyle Width="75px" />
                                                                                                <ItemStyle Width="75px" />
                                                                                            </asp:BoundField>--%>
                                                                                            <asp:TemplateField HeaderText="Status" SortExpression="ITEM_STATUS">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblITEM_STATUS" runat="server" Text='<%#Eval("ITEM_STATUS") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" Width="75px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:BoundField DataField="QTY" HeaderText="In Hand" SortExpression="QTY">
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="FREE_QTY" HeaderText="Free" SortExpression="FREE_QTY">
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <%--<asp:BoundField DataField="RES_QTY" HeaderText="Reserved" SortExpression="RES_QTY">
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundField>--%>
                                                                                            <asp:TemplateField HeaderText="Reserved" SortExpression="RES_QTY">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblRES_QTY" runat="server" Text='<%#Eval("RES_QTY") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Cost" SortExpression="COST_VAL">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCostVal" runat="server" Text='<%#Bind("COST_VAL", "{0:N2}") %>' Width="80px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="BUFFER_LEVEL" HeaderText="Buffer" SortExpression="BUFFER_LEVEL">
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField SortExpression="FREE_QTY">
                                                                                                 <HeaderTemplate>
                                                                                                     <div style="color:#7E1974">
                                                                                                         <asp:Label Visible="true" ID="lblHdrPkgQty" runat="server"  Text='Pkg Qty'></asp:Label>
                                                                                                     </div>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Visible="true" ID="lblPkgQty" runat="server" Text=''></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM">
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div id="editbtndiv2" style="width: 1px; margin-top: -3px;">
                                                                                                        <asp:LinkButton ID="btnGridImgSelect" CausesValidation="false" CommandName="Select" OnClick="btnGridImgSelect_Click" CommandArgument="<%# Container.DataItemIndex %>" runat="server" ToolTip="Item Details">
                                                                                                <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" Width="40px" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="gridVerticalrAlignMiddle" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div id="resbtndiv" style="width: 1px; margin-top: -3px;">
                                                                                                        <asp:LinkButton ID="btnGridItmRes" CausesValidation="false" OnClick="btnItmRes_Click" runat="server" ToolTip="Item Reservation Details">
                                                                                                <%--OnClick="btnGridImgSelect_Click" --%>
                                                                                                            <span class="glyphicon glyphicon-expand" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" Width="40px" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="gridVerticalrAlignMiddle" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div id="resbtndiv" style="width: 1px; margin-top: -3px;">
                                                                                                        <asp:LinkButton ID="btnItmBond" CausesValidation="false" OnClick="btnItmBond_Click" runat="server" ToolTip="Bond Details">
                                                                                                <%--OnClick="btnGridImgSelect_Click" --%>
                                                                                                            <span class="glyphicon glyphicon-expand" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" Width="40px" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="gridVerticalrAlignMiddle" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div id="divPkgQty" style="width: 1px; margin-top: -3px;">
                                                                                                        <asp:LinkButton ID="lbtnPkgQtyShow" Visible="false" CausesValidation="false" OnClick="lbtnPkgQtyShow_Click" runat="server" ToolTip="Package Quantity Details">
                                                                                                            <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" id="divTotal">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-9 labelText1"></div>
                                                                                    <div class="col-md-3 paddingRight0">
                                                                                        <div class="col-md-1 labelText1"></div>
                                                                                        <div class="col-md-5 labelText1">
                                                                                            <asp:Label ID="lblGrandTotal" Visible="False" runat="server" CssClass="labelText1">Total Quantity </asp:Label>
                                                                                        </div>
                                                                                        <div class="col-md-6">
                                                                                            <asp:TextBox runat="server" ID="txtTotalQty" Visible="False" CausesValidation="false" AutoPostBack="true" CssClass="form-control text-right" Enabled="False"></asp:TextBox>
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
                                <%--<div class="tab-pane" id="ScannedSerials">
                                <asp:Button Text="Product Tab" ID="btnTemp" runat="server" OnClick="btnTemp_Click" />
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="ProductDetails">
                                <asp:Button Text="Scanned Tab" ID="Button2" runat="server" />
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="ExcessStockEnquiry">
                                <asp:Button Text="text" ID="Button4" runat="server" />
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>--%>

                                <div class="tab-pane " id="ProductDetails">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="pnlTabPrdctDetails" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnProDetails" CausesValidation="false" runat="server" AutoPostBack="true" OnClick="lbtnProDetails_Click"></asp:LinkButton>
                                            <div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="col-sm-12" style="padding-bottom: 1px; padding-top: 1px; padding-left: 3px; padding-right: 3px;">
                                                    <div class="panel panel-default" style="height: 452px; padding-bottom: 1px; padding-top: 1px;">
                                                        <div class="panel-heading">
                                                            Product Search Details
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Item Code
                                                                    </div>
                                                                    <div class="col-sm-5 paddingRight5">
                                                                        <asp:TextBox runat="server" ID="txtPDitemcode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtPDitemcode_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPDitemcode" CausesValidation="false" runat="server" OnClick="lbtnPDitemcode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4 paddingRight5">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">Product Detail Information</div>
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-12">
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Item Code
                                                                                    </div>
                                                                                    <div class="col-sm-5 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtItemCodePDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Item Type
                                                                                    </div>
                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtItemTypePDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Description
                                                                                    </div>
                                                                                    <div class="col-sm-9 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtDescriptionPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
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
                                                                                    <div class="col-sm-9 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtBrandPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
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
                                                                                    <div class="col-sm-3 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtModelPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                    </div>

                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Ext. Color
                                                                                    </div>
                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtExtClrPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <%-- <div class="col-sm-3 labelText1">
                                                                                        Category
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtCategoryPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>--%>

                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Packing Code
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtPackingCodePDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                    </div>

                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Int. Color
                                                                                    </div>
                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtIntClrPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Tax Rate
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtTaxRatePDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                    </div>

                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Has HP insurance
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                                        <asp:CheckBox ID="chkHasHPinsPDI" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        UOM
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight5">
                                                                                        <asp:TextBox runat="server" ID="txtUOMPDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="col-sm-2 paddingRight5">
                                                                                    </div>

                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Status - Active
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                                        <asp:CheckBox ID="chkStusActivePDI" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Category 01
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        <asp:TextBox runat="server" ID="txtCate1PDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-6 labelText1">
                                                                                        <asp:Label ID="lblCate1PDI" runat="server" ReadOnly="true" Visible="true" ForeColor="#2E64FE"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <%-- <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>--%>
                                                                                <%-- add red label --%>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                    </div>
                                                                                    <div class="col-sm-8 labelText1">
                                                                                        <asp:Label ID="lblCate1AgePDI" runat="server" ReadOnly="true" Visible="true" ForeColor="#CC0000"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <%-- <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>--%>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Category 02
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        <asp:TextBox runat="server" ID="txtCate2PDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="col-sm-6 labelText1">
                                                                                        <asp:Label ID="lblCate2PDI" runat="server" ReadOnly="true" Visible="true" ForeColor="#2E64FE"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Category 03
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        <asp:TextBox runat="server" ID="txtCate3PDI" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-6 labelText1">
                                                                                        <asp:Label ID="lblCate3PDI" runat="server" ReadOnly="true" Visible="true" ForeColor="#2E64FE"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Part #
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        <asp:TextBox runat="server" ID="txtPartNoPd" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-6 labelText1">
                                                                                        <asp:Label ID="Label1" runat="server" ReadOnly="true" Visible="true" ForeColor="#2E64FE"></asp:Label>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>

                                                                                <asp:Panel runat="server" Visible="false">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Packing Code
                                                                                        </div>
                                                                                        <div class="col-sm-3 paddingRight5">
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                                        <asp:CheckBox ID="chkSerializedPDI" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Serialized
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                                        <asp:CheckBox ID="chkHasWarrtPDI" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Has Warranty
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                                        <asp:CheckBox ID="chkHasInsPDI" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Has Insurance
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-8 paddingLeft0">
                                                                    <div class="panel panel-default" style="height: 400px; padding-bottom: 1px; padding-top: 1px;">
                                                                        <div class="panel-body">
                                                                            <div id="prdArea">
                                                                                <ul id="prTab" class="nav nav-tabs">
                                                                                    <li class="active"><a href="#WarrentyDetailsGL" data-toggle="tab">Warranty Details [General]</a></li>
                                                                                    <li><a href="#WarrentyDetailsSP" data-toggle="tab">Warranty Details [Special]</a></li>
                                                                                    <li><a href="#StatusWiseTaxDet" data-toggle="tab">Status wise Tax Details</a></li>
                                                                                    <li><a href="#ServiceShedule" data-toggle="tab">Service Schedule</a></li>
                                                                                    <li><a href="#ComponentItem" data-toggle="tab">Component Item</a></li>
                                                                                    <li><a href="#SimmilarItem" data-toggle="tab">Similar Item</a></li>
                                                                                    <li><a href="#TaxClaimable" data-toggle="tab">Tax Claimable</a></li>
                                                                                </ul>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="tab-content">
                                                                                    <div class="tab-pane pade in active" id="WarrentyDetailsGL">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlWarrentyDetailsGL" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body paddingLeft0 paddingRight0">
                                                                                                        <div class="panel-body" style="height: 342px; overflow: auto;">
                                                                                                            <asp:GridView ID="grdWarrentyDetailsGL" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Status_WDGL" runat="server" Text='<%# Bind("MWP_ITM_STUS") %>' Width="70px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblStatus" runat="server" Width="70px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Warranty Period">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Warrenty_Period_WDGL" runat="server" Text='<%# Bind("MWP_VAL") %>' Width="50px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label runat="server" Text='' Width="10px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Warranty Type">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Unit_WDGL" runat="server" Text='<%# Bind("MSU_DESC") %>' Width="50px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Warranty Remark">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_WarrentyRemark_WDGL" runat="server" ToolTip='<%# Bind("MWP_RMK") %>' Text='<%# Bind("tmpRmk") %>' Width="300px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <div class="tab-pane" id="WarrentyDetailsSP">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlWarrentyDetailsSP" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="panel-body panelscollbar height370">
                                                                                                            <asp:GridView ID="grdWarrentyDetailsSP" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Status_WDSP" runat="server" Text='<%# Bind("SPW_ITM_STUS") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblStatus" runat="server" Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Period">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Period_WDSP" runat="server" Text='<%# Bind("SPW_WARA_PD") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label runat="server" Text='' Width="10px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Remark">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Remark_WDSP" runat="server" Text='<%# Bind("SPW_WARA_RMK") %>' Width="200px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Valid From">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_ValidFrom_WDSP" runat="server" Text='<%# Bind("SPW_VALID_FROM","{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Valid To">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_ValidTo_WDSP" runat="server" Text='<%# Bind("SPW_VALID_TO","{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <div class="tab-pane" id="StatusWiseTaxDet">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlStatusWiseTaxDet" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-4">
                                                                                                                <div class="col-sm-3 paddingLeft5 labelText1">
                                                                                                                    Company
                                                                                                                </div>
                                                                                                                <div class="col-sm-8 paddingRight5">
                                                                                                                    <asp:TextBox runat="server" ID="txtCompanySWTD" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtCompanySWTD_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-sm-1 paddingLeft0">
                                                                                                                    <asp:LinkButton ID="lbtnCompanySWTD" CausesValidation="false" runat="server" OnClick="lbtnCompanySWTD_Click">
                                                                                                                     <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <div class="panel-body panelscollbar height370">
                                                                                                            <asp:GridView ID="grdStatusWiseTaxDet" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Status_SWTD" runat="server" Text='<%# Bind("MICT_STUS") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblStatus" runat="server" Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Tax Code">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_TaxCode_SWTD" runat="server" Text='<%# Bind("MICT_TAX_CD") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Tax Rate (%)">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_TaxRate_SWTD" runat="server" Text='<%# Bind("MICT_TAX_RATE","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label runat="server" Text='' Width="20px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>

                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <div class="tab-pane" id="ServiceShedule">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlServiceShedule" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="panel-body panelscollbar height370">
                                                                                                            <asp:GridView ID="grdServiceShedule" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Status_SS" runat="server" Text='<%# Bind("MSP_ITM_STUS") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblStatus" runat="server" Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Term">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Term_SS" runat="server" Text='<%# Bind("MSP_TERM") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Period From">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_PeriodFrom_SS" runat="server" Text='<%# Bind("MSP_PD_FROM") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Period To">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_PeriodTo_SS" runat="server" Text='<%# Bind("MSP_PD_TO") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Period UOM">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_PeriodUOM_SS" runat="server" Text='<%# Bind("MSP_PD_UOM") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Distance From">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_DistanceFrom_SS" runat="server" Text='<%# Bind("MSP_PDALT_FROM","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Distance To">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_DistanceTo_SS" runat="server" Text='<%# Bind("MSP_PDALT_TO","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Distance UOM">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_DistanceUOM_SS" runat="server" Text='<%# Bind("MSP_PDALT_UOM") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Is Free">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_IsFree_SS" runat="server" Text='<%# Bind("Is_Free") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <div class="tab-pane" id="ComponentItem">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlComponentItem" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="panel-body" style="height: 350px; overflow-x: auto;">
                                                                                                            <asp:GridView ID="grdComponentItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Component Item">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_MainItem_CI" runat="server" Text='<%# Bind("MICP_COMP_ITM_CD") %>' Width="120px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_MainItem_CI" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="230px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Brand">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Component_CI" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Type">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Type_CI" runat="server" Text='<%# Bind("MI_ITM_TP") %>' Width="50px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="No of units">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("micp_qty","{0:N2}") %>' Width="80px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label runat="server" Text='' Width="10px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <asp:Label Text="Cost" ID="lblCostTp" runat="server" />
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Cost_CI" runat="server" Text='<%# Bind("MICP_ISPRICE","{0:N2}") %>' Width="80px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label runat="server" Text='' Width="10px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <div class="tab-pane" id="SimmilarItem">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlSimmilarItem" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="panel-body panelscollbar height370">
                                                                                                            <asp:GridView ID="grdSimmilarItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Similar Item">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_SimilarItem_SI" runat="server" Text='<%# Bind("MISI_SIM_ITM_CD") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Description_SI" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Model_SI" runat="server" Text='<%# Bind("MI_MODEL") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>

                                                                                                                    <asp:TemplateField HeaderText="Brand">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Brand_SI" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>

                                                                                    <div class="tab-pane" id="TaxClaimable">
                                                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
                                                                                        <asp:UpdatePanel ID="pnlTaxClaimable" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="panel-body panelscollbar height370">
                                                                                                            <asp:GridView ID="grdTaxClaimable" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Company">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_TaxCom_TC" runat="server" Text='<%# Bind("Mic_com") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Tax Code">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_TaxCode_TC" runat="server" Text='<%# Bind("MIC_TAX_CD") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Tax Rate">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_TaxRate_TC" runat="server" Text='<%# Bind("MIC_TAX_RT") %>' Width="100px" style="text-align: center;"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Tax Percentage">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_TaxPercentage_TC" runat="server" Text='<%# Bind("MIC_CLAIM") %>' Width="100px" style="text-align: center;"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Active">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lbl_Status_TC" runat="server" Text='<%# Bind("MIC_STUS") %>' Width="100px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
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

                                <div class="tab-pane " id="DocumentWiseBal">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnDocWiseBal" CausesValidation="false" runat="server" AutoPostBack="true" OnClick="lbtnProDetails_Click"></asp:LinkButton>
                                            <div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="col-sm-12" style="padding-bottom: 1px; padding-top: 1px; padding-left: 3px; padding-right: 3px;">
                                                    <div class="panel panel-default" style="height: 475px; padding-bottom: 1px; padding-top: 1px;">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <div class="col-sm-4 labelText1">
                                                                        PO/BL #
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5">
                                                                        <asp:TextBox runat="server" ID="txtPOorBLNumber" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPOBLNumber" CausesValidation="false" runat="server" OnClick="lbtnPOBLNumber_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="col-sm-3 paddingRight0 labelText1">
                                                                        GRN #
                                                                    </div>
                                                                    <div class="col-sm-7 paddingLeft0 paddingRight5">
                                                                        <asp:TextBox runat="server" ID="txtDocGRNumber" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnDocGrnNumber" CausesValidation="false" runat="server" OnClick="lbtnDocGrnNumber_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-2">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Entry #
                                                                    </div>
                                                                    <div class="col-sm-7 paddingLeft0 paddingRight5">
                                                                        <asp:TextBox runat="server" ID="txtEntryNumber" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnEntryNumber" CausesValidation="false" runat="server" OnClick="lbtnEntryNumber_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-2 labelText1 width100">
                                                                        Period From
                                                                    </div>
                                                                    <div class="col-sm-3 padding0">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtDWfrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:LinkButton ID="lbtnDWfrom" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="cldDWfrom" runat="server" TargetControlID="txtDWfrom"
                                                                            PopupButtonID="lbtnDWfrom" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                    <div class="col-sm-1 labelText1">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-3 padding0">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtDWto" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:LinkButton ID="lbtnDWto" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDWto"
                                                                            PopupButtonID="lbtnDWto" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>

                                                            <div class="panel panel-default">
                                                                <div class="panel-body">
                                                                    <div class="panel-body panelscollbar height400">
                                                                        <asp:GridView ID="grdDocWiseBal" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Location">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_MainItem_CI" runat="server" Text='<%# Bind("INB_LOC") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PO/BL #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Component_CI" runat="server" Text='<%# Bind("inb_base_doc_no") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Entry #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Type_CI" runat="server" Text='<%# Bind("inb_job_no") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Entry Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Cost_CI" runat="server" Text='<%# Bind("inb_base_itmcd") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="GRN Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Cost_CI" runat="server" Text='<%# Bind("inb_itm_cd") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Model">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Cost_CI" runat="server" Text='<%# Bind("mi_model") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("mis_desc") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Inhand Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Cost_CI" runat="server" Text='<%# Bind("INB_FREE_QTY") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Balance Qty.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Cost_CI" runat="server" Text='<%# Bind("INB_RES_QTY") %>' Width="100px"></asp:Label>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane " id="Documentwithserial">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnDocserialdet" CausesValidation="false" runat="server" AutoPostBack="true" OnClick="lbtnProDetails_Click"></asp:LinkButton>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-12" style="padding-bottom: 1px; padding-top: 1px; padding-left: 3px; padding-right: 3px;">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="row col-sm-3">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    Item Code
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtitemcode2" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnitencode2" CausesValidation="false" runat="server" OnClick="lbtnitencode2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    Model
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtmodel2" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnmodelsearch2" CausesValidation="false" runat="server" OnClick="lbtnmodelsearch2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    Brand
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtbrand2" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnbrandsearch2" CausesValidation="false" runat="server" OnClick="lbtnbrandsearch2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row col-sm-3">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    Cat 1
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtcat12" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtncat12search" CausesValidation="false" runat="server" OnClick="lbtncat12search_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    Cat 2
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtcat22" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtncat22search" CausesValidation="false" runat="server" OnClick="lbtncat22search_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    Cat 3
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtcat32" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtncat32search" CausesValidation="false" runat="server" OnClick="lbtncat32search_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row col-sm-3">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    BL #
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtblno2" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnblsearch2" CausesValidation="false" runat="server" OnClick="lbtnblsearch2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    To Bond #
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txttobond2" CausesValidation="false" AutoPostBack="False" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtntobondsearch2" CausesValidation="false" runat="server" OnClick="lbtntobondsearch2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row col-sm-3">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    From Date
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtbldate2" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnbldate2" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="blclfrom" runat="server" TargetControlID="txtbldate2"
                                                                        PopupButtonID="lbtnbldate2" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1">
                                                                    To Date
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox runat="server" ID="txtbltodate2" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtntobldate" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtbltodate2"
                                                                        PopupButtonID="lbtntobldate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnsearchblall" runat="server" CausesValidation="false" OnClick="btnsearchblall_Click">
                                                                                             <span class="glyphicon glyphicon-search" style="font-size:x-large" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="panel-body panelscollbar height400" style="height: 175px">
                                                        <asp:GridView ID="grdbldata" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ibi_itm_cd" runat="server" Text='<%# Bind("ibi_itm_cd") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ibi_model" runat="server" Text='<%# Bind("ibi_model") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ibi_itm_stus" runat="server" Text='<%# Bind("ibi_itm_stus") %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ib_bl_dt" runat="server" Text='<%# Bind("ib_bl_dt","{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ib_doc_no" runat="server" Text='<%# Bind("ib_doc_no") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BL No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ib_bl_no" runat="server" Text='<%# Bind("ib_bl_no") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bond No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ib_ref_no" runat="server" Text='<%# Bind("ib_ref_no") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Entry No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="CusEntryno" runat="server" Text='<%# Bind("CusEntryno") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Entry Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="CusEntrydate" runat="server" Text='<%# Bind("CusEntrydate","{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BL Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ibi_qty" runat="server" Text='<%# Bind("ibi_qty") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BL Req">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ibi_req_qty" runat="server" Text='<%# Bind("ibi_req_qty") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" BL Bal">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Entrybal" runat="server" Text='<%# Bind("Entrybal") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GRN Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="GrnQty" runat="server" Text='<%# Bind("GrnQty") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GRN Free">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="GrnfreeQty" runat="server" Text='<%# Bind("GrnfreeQty") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GRN Res">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="GrnResQty" runat="server" Text='<%# Bind("GrnResQty") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnselectitm" runat="server" OnClick="btnselectitm_Click" Width="20px"><span class="glyphicon glyphicon-arrow-down"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="panel-body panelscollbar height400" style="height: 150px">
                                                        <asp:GridView ID="grdreqentry" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Req No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="itr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="itr_dt" runat="server" Text='<%# Bind("itr_dt","{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="itri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="itri_qty" runat="server" Text='<%# Bind("itri_qty") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane " id="ItemAllocation">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <div>

                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="col-sm-12" style="padding-bottom: 1px; padding-top: 1px; padding-left: 3px; padding-right: 3px;">
                                                    <div class="panel panel-default" style="height: 475px; padding-bottom: 1px; padding-top: 1px;">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-2 labelText1">
                                                                            Item 
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtItem3" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem3_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                            <asp:LinkButton ID="lbtnDocNo" runat="server" CausesValidation="false" OnClick="lbtnItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Channel 
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtchannelAlloc" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtchannelAlloc_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" OnClick="lbtnchannel_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                  <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Location 
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtLocAlloc" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtLocAlloc_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" OnClick="lbtnloc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel-body panelscollbar height300">
                                                                        <asp:GridView ID="grdStockAllo" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None"
                                                                            CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Channel " ControlStyle-CssClass="marginleft">
                                                                                    <ItemTemplate>

                                                                                        <asp:Label ID="col_channel" runat="server" Text='<%# Bind("Isa_chnl") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Location">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_loc" runat="server" Text='<%# Bind("Isa_loc") %>' ></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Allocated Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_isa_dt" runat="server" Text='<%# Bind("isa_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Doc #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_reqDate" runat="server" Text='<%# Bind("isa_doc_no") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_Item" runat="server" Text='<%# Bind("isa_itm_cd") %>' ></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Alloc Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAqty" runat="server" Text='<%# Bind("isa_aloc_qty","{0:n2}") %>'  CssClass="txtalignright2"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Balance">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_Bqty" runat="server" Text='<%# Bind("isa_aloc_bqty","{0:n2}") %>' CssClass="txtalignright"></asp:Label>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ItemPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="testPanel" DefaultButton="ImageSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
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
                    <div class="row" id="itmDetSrh" runat="server">
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

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" placeholder="Search by word" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="ImageSearch" runat="server" OnClick="ImageSearch_Click">
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
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResultItem" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager"
                                        OnPageIndexChanging="dgvResultItem_PageIndexChanging" OnSelectedIndexChanged="dgvResultItem_SelectedIndexChanged">
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
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDilogResult" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="panelDivDilogResult" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-sm-12 col-lg-12">
            <asp:Panel runat="server" ID="panelDivDilogResult">
                <div class="row">
                    <div class="col-sm-12 col-lg-12 panel panel-body">
                        <div class="row">
                            <div class="col-sm-12 col-lg-12 fontsize18">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblDilogResult" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-lg-12 col-sm-12">
                                <div class="row buttonRow">
                                    <div class="col-sm-3 col-md-3">
                                    </div>
                                    <div class="col-sm-3 col-md-3" style="padding-left: 3px; padding-right: 3px;">
                                        <asp:LinkButton ID="lBtnDilogResultYes" runat="server" OnClick="lBtnDilogResultYes_Click">
                             <span class="glyphicon glyphicon-ok"  aria-hidden="true"> Yes</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3 col-md-3" style="padding-left: 3px; padding-right: 3px;">
                                        <asp:LinkButton ID="lBtnDilogResultNo" runat="server" OnClick="lBtnDilogResultNo_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true"> No</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <asp:UpdatePanel ID="updatePanelDocument" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnDocument" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDocument" runat="server" Enabled="True" TargetControlID="btnDocument"
                PopupControlID="panelDocument" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divDocument" class="row">
        <div class="col-sm-12 col-lg-12">
            <asp:Panel runat="server" ID="panelDocument">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <strong>Item Details</strong>
                            <asp:LinkButton ID="lbtPopDocClose" runat="server" OnClick="lbtPopDocClose_OnClick" Style="float: right;">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                        <div class="panel panel-body" style="padding-top: 0px;">
                            <uc1:ucItemSerialView1 runat="server" ID="ucItemSerialView1" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ItmResPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="ItmResPopupID" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divItemRes" class="row">
        <div class="col-sm-12 col-lg-12">
            <asp:Panel runat="server" ID="ItmResPopupID">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <strong>Item Reservation Details</strong>
                        <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <div class="panel-body">


                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <strong>Item: </strong>
                                                <asp:Label ID="lblItemRes" runat="server" ForeColor="#7E1974" Text="" AutoPostBack="True"></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                                <strong>Model: </strong>
                                                <asp:Label ID="lblModelRes" runat="server" ForeColor="#7E1974" Text="" AutoPostBack="True"></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                                <strong>Res Qty: </strong>
                                                <asp:Label ID="lblQtyRes" runat="server" ForeColor="#7E1974" Text="" AutoPostBack="True"></asp:Label>
                                            </div>
                                        </div>
                                        <asp:GridView ID="gdvResDet" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                            CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager"
                                            OnPageIndexChanging="gdvResDet_PageIndexChanging">
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoupBond" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopupBond" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupBond">
        <div runat="server" id="Div1" class="panel panel-default height400 width950">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        Bond Details
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
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResultBond" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped"
                                            PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True">
                                            <Columns>
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
     <%-- pnl Model types Show --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popModelTp" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlModelTp" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upModelTp">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait110" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait110" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlModelTp">
        <asp:UpdatePanel runat="server" ID="upModelTp">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 200px;">
                        <div class="panel-heading" style="height: 25px; ">
                            <div class="col-sm-12">
                            <div class="col-sm-10 padding0">
                                <strong>Model Details</strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnModelPopClose" runat="server" OnClick="lbtnModelPopClose_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div style="height:170px; overflow-y:scroll;">
                                <asp:GridView ID="dgvModelDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="" Width="100px" />
                                            <HeaderStyle CssClass="" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="" Width="100px" />
                                            <HeaderStyle CssClass="" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pkg Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPkgTp" runat="server" Text='<%# Bind("PkgTp") %>'></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle CssClass="" Width="100px" />
                                            <HeaderStyle CssClass="" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" Width="100%" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                        </asp:TemplateField>
                                    </Columns>
                                        </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    

    <%--    <script>
        $('#myTab a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
            //alert($(this).attr('href'));
            document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
        });
        $(document).ready(function () {
            var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
            $('#myTab a[href="' + tab + '"]').tab('show');
        });
    </script>--%>
    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('#myTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                //  alert($(this).attr('href'));
                document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
                // alert(tab);
                $('#myTab a[href="' + tab + '"]').tab('show');
            });
        }

    </script>
    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('#prTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                //  alert($(this).attr('href'));
                document.getElementById('<%=hdfSubTabIndex.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfSubTabIndex.ClientID%>').value;
                // alert(tab);
                $('#prTab a[href="' + tab + '"]').tab('show');
            });
        }

    </script>
</asp:Content>
