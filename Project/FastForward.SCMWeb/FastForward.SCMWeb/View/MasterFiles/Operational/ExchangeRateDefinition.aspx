<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ExchangeRateDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.ExchangeRateDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <script>
        function ClearConfirm() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };
        function SaveConfirm() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfSaveData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfSaveData.ClientID %>').value = "0";
            }
        };
        function ConfirmDelete() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=hdfDelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelete.ClientID %>').value = "No";
            }
        };
        function CheckAllCompany(Checkbox) {
            // alert('S');
            var GridVwHeaderChckbox = document.getElementById("<%=dgvCompany.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function OneConfirm() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvaldelitm = confirm("Both bank selling rate and buying rate is 1.\nDo you want to add ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=hdfRateOne.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfRateOne.ClientID %>').value = "0";
            }
        }
        function DateValid(sender, args) {
            var fromDate = Date.parse(document.getElementById('<%=txtFromDate.ClientID%>').value);
            var toDate = Date.parse(document.getElementById('<%=txtToDate.ClientID%>').value);
            if (toDate < fromDate) {
                document.getElementById('<%=txtToDate.ClientID%>').value = document.getElementById('<%=hdfCurrentDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date range !');
            }
        }



        function clickButon() {
            if (confirm('Are you sure ?')) {
                jQuery.ajax({
                    type: "POST",
                    url: "ExchangeRateDefinition.aspx/textmessage?mType=Sales",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    },
                    success: function (result) {
                        alert("We returned: " + result);
                    }
                });
            }
        }

    </script>

    <script>
        function ConfirmUsdZero() {
            if (confirm('You have set exchange rate between USD and LKR as 1\nAre you sure want to process ?')) {
                jQuery.ajax({
                    type: "POST",
                    url: "ExchangeRateDefinition.aspx/SendMail?mType=Sales",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    cache: false,
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    },
                    success: function (result) {
                        alert("We returned: " + result);
                    }
                });
                //$.ajax({
                //    type: "POST",
                //    url: "ExchangeRateDefinition.aspx/SendMail?mType=Sales",
                //    data: "{}",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    async: true,
                //    cache: false,
                //    success: function (data) {
                //        alert("We returned: " + result);
                //    }

                //});

            }
        }
    </script>
    <style>
         .hdCompanyCh {
           /*position:absolute;*/
        }
          .hdCompanyCode{
          /*position:absolute;*/
        }
        .hdCompanyDesc {
            /*position:absolute;*/
        }
        .panel {
            margin-bottom: 1px;
        }

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
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
            <div class="row">
                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                <asp:HiddenField ID="hdfCurrentDate" runat="server" Value="0" />
                <asp:HiddenField ID="hdfRateOne" runat="server" Value="0" />
                <asp:HiddenField ID="hdfDelete" runat="server" Value="0" />
                <asp:HiddenField ID="hdfSaveData" runat="server" Value="0" />
                <asp:HiddenField ID="hdfShowCom" runat="server" Value="0" />
                <asp:HiddenField ID="hdfShowLoc" runat="server" Value="0" />
                <asp:HiddenField ID="hdfShowDelBtn" runat="server" Value="0" />
                <asp:HiddenField ID="hdfClearData" runat="server" Value="0" />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="buttonRow">
                                            <div class="col-sm-10">
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="col-sm-4 padding0">
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight"
                                                        OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click"> 
                                                        <span class="glyphicon glyphicon-save" aria-hidden="true"></span>Save 
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click"> 
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default">
                                            <div class="panel panel-heading">
                                                <strong><b>Exchange Rate Definition</b></strong>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading" style="height: 25px; padding-top: 2px;">
                                                            <strong><b>Rate Details</b></strong>
                                                            <%--<strong><b>Exchange Rate Details</b></strong>--%>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px; padding-bottom: 1px; padding-top: 1px;">
                                                                    <div class="panel panel-default">
                                                                        <%-- <div class="panel panel-heading" style="height:20px; padding-top:1px; margin-bottom:1px;">
                                                                <strong><b>Company </b></strong>
                                                            </div>--%>
                                                                        <div class="panel panel-body padding0" style="height: 122px;">
                                                                            <div class="" style="max-height: 115px; overflow: auto;">
                                                                                <asp:GridView ID="dgvCompany" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                    EmptyDataText="No data found..." ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="false" PageSize="3">
                                                                                    <EditRowStyle BackColor="MidnightBlue" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox CssClass="chkboxSelectAllCom" ID="chkboxSelectAllCom" onclick="CheckAllCompany(this);" runat="server" AutoPostBack="False" />
                                                                                            </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkCompanyCode" runat="server" AutoPostBack="True" />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="10px" CssClass="hdCompanyCh"/>
                                                                                            <ItemStyle Width="10px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Company">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SEC_COM_CD") %>' Width="50px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="hdCompanyCode"/>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblComDesc" runat="server" Text='<%# Bind("MasterComp.mc_desc") %>' ></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px; padding-bottom: 1px; padding-top: 1px;">
                                                                    <div class="panel panel-default">
                                                                        <%-- <div class="panel panel-heading" style="height:20px; padding-top:1px; margin-bottom:1px;">
                                                                <strong><b>Company </b></strong>
                                                            </div>--%>
                                                                        <div class="panel panel-body" style="height: 122px;">
                                                                            <div class="row">
                                                                                <div class="col-sm-8 paddingRight0">
                                                                                    <asp:Panel ID="PanelPrfit" Visible="true" runat="server">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-5 labelText1">
                                                                                                Profit Center 
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="padding-right: 0px;">
                                                                                                <asp:TextBox ID="txtProfitCenter" Style="text-transform: uppercase" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtProfitCenter_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                <asp:LinkButton ID="lbtnSeProfiCenter" CausesValidation="false" runat="server" OnClick="lbtnSeProfiCenter_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    </asp:Panel>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-5 labelText1">
                                                                                                From Currency
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="padding-right: 0px;">
                                                                                                <asp:TextBox ID="txtFromCurr" CssClass="form-control" runat="server" OnTextChanged="txtFromCurr_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                <asp:LinkButton ID="lbtnSeFromCurr" CausesValidation="false" runat="server" OnClick="lbtnSeFromCurr_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-5 labelText1">
                                                                                                To Currency
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="padding-right: 0px;">
                                                                                                <asp:TextBox ID="txtToCurr" CssClass="form-control" runat="server" OnTextChanged="txtToCurr_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                <asp:LinkButton ID="lbtnSeToCurr" CausesValidation="false" runat="server" OnClick="lbtnSeToCurr_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-5 labelText1">
                                                                                                From Date
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="padding-right: 0px;">
                                                                                                <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                <asp:LinkButton ID="lbtnFromDate" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                                                                                    PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                                                </asp:CalendarExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-5 labelText1">
                                                                                                To Date
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="padding-right: 0px;">
                                                                                                <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                <asp:LinkButton ID="lbtnToDate" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                                                                    PopupButtonID="lbtnToDate"
                                                                                                    OnClientDateSelectionChanged="DateValid" Format="dd/MMM/yyyy">
                                                                                                </asp:CalendarExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <div class=" buttonRow">
                                                                                        <div class="col-sm-12">
                                                                                            <br />
                                                                                            <br />
                                                                                            <br />
                                                                                            <br />
                                                                                            <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server"
                                                                                                CssClass="floatRight" OnClientClick="" OnClick="lbtnView_Click"> 
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>View History
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row ">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-12" style="padding-left: 1px; padding-right: 1px; padding-bottom: 1px; padding-top: 1px;">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                                            <div class="col-sm-9" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px;">
                                                                                <div class="row buttonRow" style="height: 24px;">
                                                                                    <div class="col-sm-4 paddingRight0">
                                                                                        <div class="col-sm-5 labelText1" style="padding-left: 0px; padding-right: 0px;">
                                                                                            Bank Selling Rate
                                                                                        </div>
                                                                                        <div class="col-sm-7">
                                                                                            <asp:TextBox ID="txtBankSellRate" CssClass="txtBankSellRate form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight0">
                                                                                        <div class="col-sm-5 labelText1" style="padding-left: 0px; padding-right: 0px;">
                                                                                            Bank Buying Rate
                                                                                        </div>
                                                                                        <div class="col-sm-7">
                                                                                            <asp:TextBox ID="txtBankBuyRate" CssClass="txtBankBuyRate form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="col-sm-5 labelText1" style="padding-left: 0px; padding-right: 0px;">
                                                                                            Custom Rate
                                                                                        </div>
                                                                                        <div class="col-sm-7">
                                                                                            <asp:TextBox ID="txtCustmRate" CssClass="txtCustmRate form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-1" style="padding-right: 0px; padding-left: 0px;">
                                                                                <div class="col-sm-12">
                                                                                    <div class="row buttonRow" style="margin-top: -3px; height: 24px;">
                                                                                        <asp:LinkButton ID="lbtnAddRate" CausesValidation="false" runat="server" OnClick="lbtnAddRate_Click">
                                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2" style="padding-right: 0px; padding-left: 0px; margin-top: 3px;">
                                                                                <div class="col-sm-2" style="padding-left: 0px; padding-right: 0px;">
                                                                                    <asp:CheckBox runat="server" ID="chkUs" />
                                                                                </div>
                                                                                <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                                                                                    <asp:Label Text="Generate from USD also" runat="server"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row ">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default" style="height: 270px;">
                                                                <div class="col-sm-12" style="padding-left: 1px; padding-right: 1px; padding-bottom: 1px; padding-top: 1px;">
                                                                    <div class="panel panel-default" style="height: 265px;">
                                                                        <div class="" style="max-height: 260px; overflow: auto;">
                                                                            <asp:GridView ID="dgvRate"  CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager" OnRowDataBound="dgvRate_RowDataBound"
                                                                                EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" PageSize="3">
                                                                                <EditRowStyle BackColor="MidnightBlue" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <div style="margin-top: -3PX;">
                                                                                                <asp:LinkButton ID="lbtnDelRate" Visible='<%# hdfShowDelBtn.Value=="1" %>' runat="server" CausesValidation="false" Width="0px"
                                                                                                    OnClientClick="ConfirmDelete();" OnClick="lbtnDelRate_Click">
                                                                                                 <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                </asp:LinkButton>

                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Bind("mer_id") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField> 
                                                                                    <asp:TemplateField HeaderText="">
                                                                                         <HeaderTemplate>
                                                                                            <asp:Label ID="lblComDess"  Visible='<%# hdfShowCom.Value=="1" %>' runat="server" Text='Company'></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblCom" Visible='<%# hdfShowCom.Value=="1" %>' Width="60px" ToolTip='<%# Bind("Mer_com_desc") %>' runat="server" Text='<%# Bind("mer_com") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Base Currency">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblBaseCurr" Visible="true" Width="100px" runat="server" ToolTip='<%# Bind("Mer_from_cur_desc") %>' Text='<%# Bind("mer_cur") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="To Currency">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblToCurr" Visible="true" Width="100px" runat="server" ToolTip='<%# Bind("Mer_to_cur_desc") %>' Text='<%# Bind("mer_to_cur") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="From Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblFromDate" Visible="true" Width="100px" runat="server" Text='<%# Bind("mer_vad_from","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="To Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblToDate" Visible="true" Width="100px" runat="server" Text='<%# Bind("mer_vad_to","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Selling Rate">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSellingRate" Visible="true" Width="100px" runat="server" Text='<%# Bind("mer_bnksel_rt","{0:N4}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Buying Rate">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblBuyingRate" Visible="true" Width="100px" runat="server" Text='<%# Bind("mer_bnkbuy_rt","{0:N4}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Custom Rate">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblCustomRate" Visible="true" Width="100px" runat="server" Text='<%# Bind("mer_cussel_rt","{0:N4}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbllCustomRate" Width="10px" Visible="true" runat="server" Text=''></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField>
                                                                                         <HeaderTemplate>
                                                                                            <asp:Label ID="lblLocHed"  Visible='<%# hdfShowLoc.Value=="1" %>' runat="server" Text='Profit Center'></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProfCenter" runat="server"  Visible='<%# hdfShowLoc.Value=="1" %>' Text='<%# Bind("Mer_pc") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                   
                                                                                     <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStatus" Visible="true" runat="server" Text='<%# Convert.ToBoolean(Eval("mer_act"))?"Active":"Inactive" %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbllCustomRate1" Width="10px" Visible="true" runat="server" Text=''></asp:Label>
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
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight" style="width: 900px;">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
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
                        <div class="col-sm-12">
                            <div class="col-sm-2 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
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
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
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
    </asp:Panel>
    <script>
        Sys.Application.add_load(func);
        function func() {
            jQuery('#BodyContent_dgvCompany tr td').on('click', function () {

                if (jQuery('#BodyContent_dgvCompany tr td input:checked').length == jQuery('#BodyContent_dgvCompany tr td input').length) {
                    jQuery('.chkboxSelectAllCom input').attr('checked', true);
                } else {
                    jQuery('.chkboxSelectAllCom input').attr('checked', false);
                }
            });

            $('.txtBankSellRate').keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                //   alert(charCode);
                var str = $(this).val();
                if (charCode == 46) {
                    if (str.indexOf(".") != -1) {
                        return false;
                    }
                }
                if (charCode != 46 && charCode != 37 && charCode != 39 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            });
            $('.txtBankBuyRate').keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                //   alert(charCode);
                var str = $(this).val();
                if (charCode == 46) {
                    if (str.indexOf(".") != -1) {
                        return false;
                    }
                }
                if (charCode != 46 && charCode != 37 && charCode != 39 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            });
            $('.txtCustmRate').keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                //   alert(charCode);
                var str = $(this).val();
                if (charCode == 46) {
                    if (str.indexOf(".") != -1) {
                        return false;
                    }
                }
                if (charCode != 46 && charCode != 37 && charCode != 39 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            });
        }
    </script>
</asp:Content>
