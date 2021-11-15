<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PriceEnquiry.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Sales.PriceEnquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <script type="text/javascript">


        function closeDialog() {
            $(this).showStickySuccessToast("close");
        }
        function showStickySuccessToast(value) {
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
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
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
        function pageLoad(sender, args) {
            $("#<%=txtFromDt.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtToDt.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtAsAtDt.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
        function Tab() {
            document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "1";
        };

    </script>

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
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-8  buttonrow">
                </div>
                <div class="col-sm-4  buttonRow">

                    <div class="col-sm-6">
                    </div>
                    <div class="col-sm-3 paddingRight0">
                    </div>

                    <div class="col-sm-3">
                        <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="panel panel-default marginLeftRight5 paddingbottom0">

        <div class="panel-heading paddingtopbottom0">
            <strong>Price Enquiry</strong>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12 height10">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="panel panel-default">
                            <div class="panel-heading paddingtopbottom0">
                                Pricing Parameter
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <div class="col-sm-6 labelText1">
                                            Profit Center
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-3 labelText1">
                                            Book
                                        </div>

                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-3 labelText1">
                                            Level
                                        </div>

                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-5 labelText1">
                                            Circular
                                        </div>

                                    </div>
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <div class="col-sm-4 labelText1">
                                            Promotion
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtPc" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPc_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnPc" runat="server" CausesValidation="false" OnClick="lbtnPc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtPriceBook" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPriceBook_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Book_Click" runat="server" CausesValidation="false" OnClick="lbtnSearch_Book_Click_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtLevel" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtLevel_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Level" runat="server" CausesValidation="false" OnClick="lbtnSearch_Level_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtCircular" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCircular_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Circular" runat="server" CausesValidation="false" OnClick="lbtnSearch_Circular_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtPromotion" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPromotion_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Promotion" runat="server" CausesValidation="false" OnClick="lbtnSearch_Promotion_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="panel panel-default">
                            <div class="panel-heading paddingtopbottom0">
                                Item Parameter
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <div class="col-sm-6 labelText1">
                                            Item Code
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-10 labelText1">
                                            Main Category
                                        </div>

                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-3 labelText1">
                                            Category
                                        </div>

                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-12 labelText1">
                                            Sub Category
                                        </div>

                                    </div>
                                    <%-- <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <div class="col-sm-4 labelText1">
                                            Status
                                        </div>

                                    </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Item" runat="server" CausesValidation="false" OnClick="lbtnSearch_Item_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtCate1" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate1_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Cat1" runat="server" CausesValidation="false" OnClick="lbtnSearch_Cat1_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtCate2" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate2_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Cat2" runat="server" CausesValidation="false" OnClick="lbtnSearch_Cat2_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">

                                        <div class="col-sm-10 paddingRight0 ">
                                            <asp:TextBox runat="server" ID="txtCate3" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate3_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Cat3" runat="server" CausesValidation="false" OnClick="lbtnSearch_Cat3_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <asp:Panel runat="server" Visible="false">
                                            <div class="col-sm-9 paddingRight0 ">
                                                <asp:DropDownList ID="ddlStatus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="GOOD"></asp:ListItem>
                                                    <asp:ListItem Text="GOOD-LP"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:CheckBox runat="server" ID="chkAllStatus" Visible="false" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 paddingtopbottom0">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">

                                    <div class="col-sm-3">
                                        <div class="col-sm-3 labelText1 paddingRight0 paddingLeft0">
                                            <strong>Description :</strong>
                                        </div>
                                        <div class="col-sm-9 paddingRight0 labelText1">
                                            <strong>
                                                <asp:Label ID="lbtnItemDescription" runat="server" CssClass="Color1 fontWeight900"></asp:Label></strong>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="col-sm-4 labelText1 ">
                                            <strong>Model : </strong>
                                        </div>
                                        <div class="col-sm-8 paddingRight0 paddingLeft0 labelText1">

                                            <asp:Label ID="lbtnItemModel" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="col-sm-4 labelText1">
                                            <strong>Brand : </strong>
                                        </div>
                                        <div class="col-sm-8 paddingRight0 paddingLeft0 labelText1">

                                            <asp:Label ID="lbtnBrand" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="col-sm-4 labelText1">
                                            <strong>Serial Status : </strong>
                                        </div>
                                        <div class="col-sm-7 paddingRight0 paddingLeft0 labelText1">

                                            <asp:Label ID="lblItemSubStatus" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="col-sm-8 labelText1">
                                            <strong>Imported VAT Rt. : </strong>
                                        </div>
                                        <div class="col-sm-4 paddingRight0 paddingLeft0 labelText1">

                                            <asp:Label ID="lblVatRate" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 paddingtopbottom0">
                        <div class="panel panel-default">
                            <div class="panel-heading paddingtopbottom0">
                                Additional Parameter
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5 paddingLeft0">
                                        <div class="col-sm-4 labelText1 ">
                                            Type
                                        </div>
                                        <div class="col-sm-7 paddingRight0 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtType" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtType_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Type" runat="server" CausesValidation="false" OnClick="lbtnSearch_Type_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-7 paddingLeft0">
                                        <div class="col-sm-5 labelText1 ">
                                            Customer
                                        </div>
                                        <div class="col-sm-6 paddingRight0 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtCustomer" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearch_Customer" runat="server" CausesValidation="false" OnClick="lbtnSearch_Customer_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-9 paddingLeft0">
                        <div class="panel panel-default">
                            <div class="panel-heading paddingtopbottom0">
                                Advance Parameter
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12 ">
                                        <div class="col-sm-1  width25 labelText1">
                                            <asp:RadioButton runat="server" AutoPostBack="true" ID="rdoDateRange" GroupName="1" />
                                        </div>
                                        <div class="col-sm-1 labelText1 ">
                                            Range From
                                        </div>
                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtFromDt" AutoPostBack="true" CausesValidation="false" OnTextChanged="txtFromDt_TextChanged" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                            <asp:LinkButton ID="lbtnFoDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDt"
                                                PopupButtonID="lbtnFoDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1  labelText1 ">
                                            Up to
                                        </div>
                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtToDt" AutoPostBack="true" OnTextChanged="txtToDt_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 Lwidth">
                                            <asp:LinkButton ID="lbtnToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDt"
                                                PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1 width25 labelText1">
                                            <asp:RadioButton runat="server" GroupName="1" ID="rdoAsAt" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-1  labelText1 ">
                                            As At
                                        </div>
                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtAsAtDt" AutoPostBack="true" OnTextChanged="txtAsAtDt_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 Lwidth">
                                            <asp:LinkButton ID="lbtnAsAtDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAsAtDt"
                                                PopupButtonID="lbtnAsAtDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1 width25 labelText1">
                                            <asp:CheckBox runat="server" ID="chkWithHistory" AutoPostBack="true" OnCheckedChanged="chkWithHistory_CheckedChanged" />
                                        </div>
                                        <div class="col-sm-1 labelText1 ">
                                            With History
                                        </div>
                                        <div class="col-sm-1 width25 labelText1">
                                            <asp:CheckBox runat="server" ID="chkScheme" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-1 labelText1 ">
                                            Scheme
                                        </div>
                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtSchemeCD" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSchemeCD_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 Lwidth">
                                            <asp:LinkButton ID="lbtnSearch_Scheme" CausesValidation="false" runat="server" OnClick="lbtnSearch_Scheme_Click">
                                                                        <span class="glyphicon glyphicon-search " aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnGetDetail" CausesValidation="false" runat="server" OnClick="lbtnGetDetail_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="bs-example">
                            <ul class="nav nav-tabs" id="myTab">
                                <li class="active"><a href="#PriceDetail" data-toggle="tab">Price Detail</a></li>
                                <li onclick="document.getElementById('<%= lbtnSerPrice.ClientID %>').click();"><a href="#SerializedPriceDetail" data-toggle="tab">Serialized Price Detail</a></li>
                                <li onclick="document.getElementById('<%= lbtnSDetails.ClientID %>').click();"><a href="#SchemeDetail" data-toggle="tab">Scheme Detail</a></li>
                                <li onclick="document.getElementById('<%= lbtnPro.ClientID %>').click();"><a href="#PromoDiscount" data-toggle="tab">Promo. Discount</a></li>
                                <li onclick="document.getElementById('<%= lbtnPay.ClientID %>').click();"><a href="#PaymentType" data-toggle="tab">Payment Type</a></li>
                                <div class="col-sm-2 width450">
                                </div>
                                <div class="col-sm-2 width25 labelText1">
                                    <asp:CheckBox ID="chkWithInv" runat="server" AutoPostBack="true" OnCheckedChanged="chkWithInv_CheckedChanged" />

                                </div>
                                <div class="col-sm-1 labelText1 ">
                                    Discount Calculator
                                </div>
                                <div class="col-sm-2 labelText1 width25">
                                    <asp:CheckBox ID="chkDiscountCal" runat="server" AutoPostBack="true" OnCheckedChanged="chkDiscountCal_CheckedChanged" />
                                </div>
                                <div class="col-sm-1 labelText1 ">
                                    With Inventory
                                </div>
                            </ul>

                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="col-sm-12 ">
                            <div class="tab-content">
                                <div class="tab-pane active" id="PriceDetail">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>

                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="panel-body  height250">
                                                    <asp:GridView ID="grdPriceDetail" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdPriceDetail_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnview" CausesValidation="false" runat="server" OnClick="lbtnview_Click">
                                                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnpc" CausesValidation="false" ToolTip="Profit Center" runat="server" OnClick="lbtnpc_Click" Width="10px">
                                                                        <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnstatus" CausesValidation="false" ToolTip="Item Status" runat="server" OnClick="lbtnstatus_Click">
                                                                        <span class="glyphicon glyphicon-wrench" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Circular">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="pr_circluer" CausesValidation="false" Text='<%# Bind("SAPD_CIRCULAR_NO") %>' runat="server" OnClick="pr_circluer_Click">
                                                                       
                                                                    </asp:LinkButton>
                                                                    <%-- <asp:Label ID="pr_circluer" runat="server" Text='<%# Bind("SAPD_CIRCULAR_NO") %>'></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_type" runat="server" Text='<%# Bind("SARPT_CD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Book">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_book" runat="server" Text='<%# Bind("sapd_pb_tp_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Level">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_level" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Promotion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_promotioncd" runat="server" Text='<%# Bind("sapd_promo_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_item" runat="server" Text='<%# Bind("sapd_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_status" runat="server" Text='<%# Bind("SAPD_PRICE_STUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price (VAT Ex.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_vatExPrice" Style="text-align: right" runat="server" Text='<%# Bind("SAPD_ITM_PRICE","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price (Max. VAT In.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_vatInPrice" Style="text-align: right" runat="server" Text='<%# Bind("Sapd_with_tax","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active For">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_activeFor" runat="server" Text='<%# Bind("SAPD_CUSTOMER_CD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_validFrom" runat="server" Text='<%# Bind("sapd_from_date" ,"{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_validTo" runat="server" Text='<%# Bind("sapd_to_date","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_qtyFrom" runat="server" Style="text-align: right" Text='<%# Bind("sapd_qty_from","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_qtyTo" runat="server" Style="text-align: right" Text='<%# Bind("sapd_qty_to","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Times">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_Times" runat="server" Text='<%# Bind("SAPD_NO_OF_TIMES") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Used">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_used" runat="server" Text='<%# Bind("SAPD_NO_OF_USE_TIMES","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pb. Seq" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_pbseq" runat="server" Text='<%# Bind("SAPD_PB_SEQ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LineSeq" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_pblineseq" runat="server" Text='<%# Bind("SAPD_SEQ_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="type seq" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_typeseq" runat="server" Text='<%# Bind("sapd_price_type") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Com" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_iscom" runat="server" Text='<%# Bind("SARPT_IS_COM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_createdate" runat="server" Text='<%# Bind("sapd_cre_when","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VAT Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_vatcode" runat="server" Text='<%# Bind("MICT_TAXRATE_CD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Max. VAT Rate %">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_vatrate" runat="server" Text='<%# Bind("MICT_TAX_RATE","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane " id="SerializedPriceDetail">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnSerPrice" CausesValidation="false" runat="server" OnClick="lbtnSerPrice_Click">
                                                                       
                                            </asp:LinkButton>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="panel-body  height250">
                                                    <asp:GridView ID="grdPriceSerial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdPriceSerial_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnview2" CausesValidation="false" runat="server" OnClick="lbtnview2_Click">
                                                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Circular">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_circular" runat="server" Text='<%# Bind("sars_circular_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_type" runat="server" Text='<%# Bind("SARPT_CD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Book">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_book" runat="server" Text='<%# Bind("sars_pbook") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Level">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_level" runat="server" Text='<%# Bind("sars_price_lvl") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="pr_item" runat="server" Text='<%# Bind("sars_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_itemstatus" Visible="false" runat="server" Text='<%# Bind("Sapl_itm_stuts") %>'></asp:Label>
                                                                    <asp:Label ID="prs_itm_stuts_Des" runat="server" Text='<%# Bind("Sapl_itm_stuts_Des") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_serial" runat="server" Text='<%# Bind("sars_ser_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price (VAT Ex.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_pricewovat" runat="server" Text='<%# Bind("sars_itm_price","{0:N}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price (Max. VAT In.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_wvat" runat="server" Text='<%# Bind("Sapd_with_tax","{0:N}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active For">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_customer" runat="server" Text='<%# Bind("sars_customer_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_fromdate" runat="server" Text='<%# Bind("sars_val_frm","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_todate" runat="server" Text='<%# Bind("sars_val_to","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pb. Seq" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_pbseq" runat="server" Text='<%# Bind("sars_pb_seq") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="type seq" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_prtype" runat="server" Text='<%# Bind("sars_price_type") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Com" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_iscom" runat="server" Text='<%# Bind("SARPT_IS_COM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_createdate" runat="server" Text='<%# Bind("sars_cre_when","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VAT Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_vatcode" runat="server" Text='<%# Bind("MICT_TAXRATE_CD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Max. VAT Rate %">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_vatrate" runat="server" Text='<%# Bind("MICT_TAX_RATE","{0:N}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Cancel">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="prs_Active" runat="server" Text='<%# Bind("sars_is_cancel") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane " id="SchemeDetail">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnSDetails" CausesValidation="false" runat="server" OnClick="lbtnSerPrice_Click">
                                                                       
                                            </asp:LinkButton>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="panel-body  height250">
                                                    <asp:GridView ID="grdScheme" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdScheme_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_code" runat="server" Text='<%# Bind("hpc_sch_cd") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Book">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_book" runat="server" Text='<%# Bind("hpc_pb") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Level">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_level" runat="server" Text='<%# Bind("hpc_pb_lvl") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_commCategory" runat="server" Text='<%# Bind("hpc_comm_cat") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="From Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_fromDate" runat="server" Text='<%# Bind("hpc_from_dt","{0:d}") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_toDate" runat="server" Text='<%# Bind("hpc_to_dt","{0:d}") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inst. Commitment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_instComm" runat="server" Text='<%# Bind("hpc_inst_comm") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Promotion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_promotion" runat="server" Text='<%# Bind("hpc_pro") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_category" runat="server" Text='<%# Bind("hpc_cat") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brand">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_brand" runat="server" Text='<%# Bind("hpc_brd") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="sc_item" runat="server" Text='<%# Bind("hpc_itm") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane " id="PromoDiscount">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnPro" CausesValidation="false" runat="server" OnClick="lbtnSerPrice_Click">
                                                                       
                                            </asp:LinkButton>
                                            <div class="row">
                                                <asp:Panel runat="server" Visible="false">
                                                    <div class="col-sm-1 width25 labelText1">
                                                        <asp:CheckBox runat="server" ID="chkIsPromotionBase" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-sm-4 labelText1 ">
                                                        Promotion Allocated Discount
                                                    </div>
                                                </asp:Panel>
                                                <div class="col-sm-1 width25 labelText1">
                                                    <asp:CheckBox runat="server" ID="chkCancelDis" AutoPostBack="true" />
                                                </div>
                                                <div class="col-sm-4 labelText1 ">
                                                    Cancel Discount
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 ">
                                                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                        <div class="panel-body  height250">
                                                            <asp:GridView ID="grdPromotionDiscount" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdPromotionDiscount_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Dis. Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Spdd_mod_by" runat="server" Text='<%# Bind("Spdd_mod_by") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="coL_Spdi_mod_by" runat="server" Text='<%# Bind("Spdi_mod_by") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sale Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="coL_spdd_sale_tp" runat="server" Text='<%# Bind("spdd_sale_tp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Valid From">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_from_dt" runat="server" Text='<%# Bind("spdd_from_dt","{0:d}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Valid To">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_to_dt" runat="server" Text='<%# Bind("spdd_to_dt","{0:d}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dis.Rate  %">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="coL_spdd_disc_rt" runat="server" Text='<%# Bind("spdd_disc_rt","{0:n}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dis.Value">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_disc_val" runat="server" Text='<%# Bind("spdd_disc_val","{0:n}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pay Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_pay_tp" runat="server" Text='<%# Bind("spdd_pay_tp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bank">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_bank" runat="server" Text='<%# Bind("spdd_bank") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CC Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_cc_tp" runat="server" Text='<%# Bind("spdd_cc_tp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Allow CC Promo">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Spdd_cre_by" runat="server" Text='<%# Bind("Spdd_cre_by") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Time From">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_from_time" runat="server" Text='<%# Bind("spdd_from_time") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Time To">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_to_time" runat="server" Text='<%# Bind("spdd_to_time") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Day">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_day" runat="server" Text='<%# Bind("spdd_day") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Inv. Value From">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_from_val" runat="server" Text='<%# Bind("spdd_from_val") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Inv. Value To">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_to_val" runat="server" Text='<%# Bind("spdd_to_val") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty From">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_from_qty" runat="server" Text='<%# Bind("spdd_from_qty","{0:n}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty To">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_to_qty" runat="server" Text='<%# Bind("spdd_to_qty","{0:n}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seq #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_spdd_seq" runat="server" Text='<%# Bind("spdd_seq") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Promo Pd">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Spdd_cc_pd" runat="server" Text='<%# Bind("Spdd_cc_pd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane " id="PaymentType">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnPay" CausesValidation="false" runat="server" OnClick="lbtnSerPrice_Click">
                                                                       
                                            </asp:LinkButton>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="panel-body  height250">
                                                    <asp:GridView ID="grdPayType" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdPayType_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Company">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_com" runat="server" Text='<%# Bind("stp_com") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pty_tp" runat="server" Text='<%# Bind("stp_pty_tp") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pty_cd" runat="server" Text='<%# Bind("stp_pty_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trans. Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_txn_tp" runat="server" Text='<%# Bind("stp_txn_tp") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_from_dt" runat="server" Text='<%# Bind("stp_from_dt","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_to_dt" runat="server" Text='<%# Bind("stp_to_dt","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pay Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pay_tp" runat="server" Text='<%# Bind("stp_pay_tp") %>' W></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price Book">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pb" runat="server" Text='<%# Bind("stp_pb") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price Level">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pb_lvl" runat="server" Text='<%# Bind("stp_pb_lvl") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brand">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_brd" runat="server" Text='<%# Bind("stp_brd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Main Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_main_cat" runat="server" Text='<%# Bind("stp_main_cat") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_stp_cat" runat="server" Text='<%# Bind("stp_cat") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_itm" runat="server" Text='<%# Bind("stp_itm") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_ser" runat="server" Text='<%# Bind("stp_ser") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Promotion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pro" runat="server" Text='<%# Bind("stp_pro") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bank">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_bank" runat="server" Text='<%# Bind("stp_bank") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Period">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_pd" runat="server" Text='<%# Bind("stp_pd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bank Chr. Rate %">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_bank_chg_rt" runat="server" Text='<%# Bind("stp_bank_chg_rt","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bank Charge">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_bank_chg_val" runat="server" Text='<%# Bind("stp_bank_chg_val","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Satus">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="stp_act" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height10">
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupBalance" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopupBalance" CancelControlID="lbtnBClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupBalance" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-default height400 width450">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnBClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div2" runat="server">
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
                            <div class="col-sm-12">
                                <div class="panel-body ">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdBalance" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bal_Status" runat="server" Text='<%# Bind("inl_itm_stus") %>' Width="1px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bal_Desc" runat="server" Text='<%# Bind("mis_desc") %>' Width="210px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Free Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bal_qty" runat="server" Text='<%# Bind("inl_free_qty","{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
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
        </div>
    </asp:Panel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupCombine" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlpopupCombine" CancelControlID="lbtnCombineClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupCombine">
        <div runat="server" id="Div3" class="panel panel-default height400 width700">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnCombineClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        Promotion Item Detail
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div5" runat="server">
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
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdCombine" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="com_item" runat="server" Text='<%# Bind("Sapc_itm_cd") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="com_description" runat="server" Text='<%# Bind("Mi_longdesc") %>' Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="com_model" runat="server" Text='<%# Bind("Mi_model") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="com_qty" runat="server" Text='<%# Bind("SAPC_QTY","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="com_qty" runat="server" Text='<%# Bind("Sapc_price","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="com_serial" runat="server" Text='<%# Bind("sapc_sub_ser") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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

    <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupStatus" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopupStatus" CancelControlID="lbtnstatus" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupStatus">
        <div runat="server" id="s" class="panel panel-default height400 width450">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnstatus" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-9">
                        Status wise Tax
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div7" runat="server">
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
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdStatus" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_MICT_STUS" runat="server" Text='<%# Bind("MICT_STUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_MICT_TAXRATE_CD" runat="server" Text='<%# Bind("MICT_TAXRATE_CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax Rate %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_MICT_TAX_RATE" runat="server" Text='<%# Bind("MICT_TAX_RATE","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="With Tax Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_SAPD_WITH_TAX" runat="server" Text='<%# Bind("SAPD_WITH_TAX","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Warranty & Period">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_sapl_warr_period" runat="server" Text='<%# Bind("sapl_warr_period") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:TemplateField>
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

    <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Popuppc" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlpopuppc" CancelControlID="lbtnpcpop" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopuppc">
        <div runat="server" id="pc" class="panel panel-default height400 width700">
            <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnpcpop" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        Allocated Profit Centers
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div8" runat="server">
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
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdPc" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped"
                                            PageSize="15" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdPc_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_mpc_cd" runat="server" Text='<%# Bind("mpc_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_mpc_desc" runat="server" Text='<%# Bind("mpc_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

    <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDiscountCalculator" runat="server" Enabled="True" TargetControlID="Button6"
                PopupControlID="pnlpopupDiscountCalculator" CancelControlID="lbtnpopupDiscountCalculator" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy20" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupDiscountCalculator">
                <div runat="server" id="Div6" class="panel panel-default height200 width450">
                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">

                            <asp:LinkButton ID="lbtnpopupDiscountCalculator" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-8">
                                Discount Calculator
                            </div>
                            <div class="col-sm-2">
                                <asp:Label runat="server" ID="lblcr" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div9" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-12 ">
                                            <div class="col-sm-1  width25 labelText1">
                                                <asp:RadioButton runat="server" AutoPostBack="true" ID="radNormal" GroupName="2" OnCheckedChanged="radNormal_CheckedChanged" />
                                            </div>
                                            <div class="col-sm-4 labelText1 ">
                                                Normal Invoice
                                            </div>
                                            <div class="col-sm-1  width25 labelText1">
                                                <asp:RadioButton runat="server" AutoPostBack="true" ID="radTaxInvoice" GroupName="2" OnCheckedChanged="radTaxInvoice_CheckedChanged" />
                                            </div>
                                            <div class="col-sm-4 labelText1 ">
                                                Tax Invoice
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        Rate
                                    </div>
                                    <div class="col-sm-6">
                                        Qty
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" ID="txtDisRate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label runat="server" ID="lblDisVal" CausesValidation="false"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" ID="txtQty" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height10">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 width250">
                                        <asp:Button ID="btnprocess" Width="300px" class="btn btn-success btn-xs" runat="server" Text="Value" OnClick="btnprocess_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div10" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg4" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="Button8" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button1_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="Button9" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button2_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

</asp:Content>
