<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="StockTransferOutwardEntry.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.StockTransferOutwardEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>
<%--<%@ Register Src="~/UserControls/ucTransportMethode.ascx" TagPrefix="uc1" TagName="ucTransportMethode" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <link href="../Css/bootstrap.css" rel="stylesheet" />
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>--%>

    <script type="text/javascript">
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
                position: 'top-center',
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

    <script type="text/javascript">
        function ConfirmPrint() {
            var selectedvalueOrdPlace = confirm("Do you want to print Doument ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnprint.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnprint.ClientID %>').value = "No";
            }
        };
        function ConfirmSendToPDA() {
            var selectedvaldelitm = confirm("Are you sure you want to send ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                return true;
            } else {
                return false;
            }
        };

        function SaveConfirma() {
            var selectedvalue = confirm("Do you want to save?");
            if (selectedvalue) {
                document.getElementById('<%=hdfSave.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSave.ClientID %>').value = "No";
            }
        };
        function DelSerialConfirm() {
            var selectedvalue = confirm("Do you want to delete this serial?");
            if (selectedvalue) {
                document.getElementById('<%=hdfDelSerila.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelSerila.ClientID %>').value = "No";
            }
        };

        function DelItemConfirm() {
            var selectedvalue = confirm("Do you want to delete this item?");
            if (selectedvalue) {
                document.getElementById('<%=hdfItemdel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfItemdel.ClientID %>').value = "No";
            }
        };

        function COnfirmClear() {
            var selectedvalue = confirm("Do you want to clear the screen?");
            if (selectedvalue) {
                window.location.reload();
            }
            else {
                return false;
            }
        };

        function showTooltip(obj) {
            if (obj.options[obj.selectedIndex].title == "") {
                obj.title = obj.options[obj.selectedIndex].text;
                obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
                for (i = 0; i < obj.options.length; i++) {
                    obj.options[i].title = obj.options[i].text;
                }
            }
            else
                obj.title = obj.options[obj.selectedIndex].text;
        };


        function balanceConfimation(value) {
            var result = confirm(value);
            if (result) {
                document.getElementById('<%= autoSelect.ClientID %>').click();
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
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
    <script>
        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
    </script>
    <style>
        .panel-body{
            margin-bottom:1px;
            padding-bottom:0px;
        }
        .table{
            padding-bottom:0px !important;
        }
    </style>
    <style>
        .panel {
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }
    </style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upGvItems">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="mainPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="mainPnl">
        <ContentTemplate>
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-body paddingtopbottom0">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="upsavebtns" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfSave" runat="server" />
                            <asp:HiddenField ID="hdfDelSerila" runat="server" />
                            <asp:HiddenField ID="hdfItemdel" runat="server" />
                            <asp:HiddenField ID="txtpdasend" runat="server" />
                            <asp:HiddenField ID="hdnprint" runat="server" />
                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                            <div class="row">
                                <div class="col-sm-12 height30">
                                    <div class="col-sm-7 labelText1  buttonrow padding0">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="col-sm-1 ">
                                                    <asp:CheckBox runat="server" ID="chkpda" AutoPostBack="true" Enabled="false" OnCheckedChanged="chkpda_CheckedChanged" />
                                                </div>
                                                <div class="col-sm-8 paddingLeft3 ">
                                                    <strong>
                                                        <asp:Label Text="Send to PDA" runat="server" /></strong>
                                                </div>
                                            </div>


                                            <div class="col-sm-6">
                                                <asp:Label Text="lblBackDateInfor" ID="lblBackDateInfor" Visible="false" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 buttonRow">
                                        <%--  <div class="col-sm-2 paddingRight0">
                                </div>--%>
                                        <div class="col-sm-2 paddingRight0">
                                            <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirma()" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnPrint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="lprintcour" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lprintcour_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Courier
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="lbtnprintserial" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnprintserial_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Serial
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="lbtnprintexpiry" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnprintexpiry_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Expiry
                                            </asp:LinkButton>
                                        </div>

                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm();" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading padding0" style="height: 25px;">
                                            <div class="col-sm-7 buttonRow padding0 ">
                                                <strong><b>Stock Transfer- Outward Entry </b></strong>
                                            </div>
                                            <div class="col-sm-2 buttonRow padding0 ">
                                                <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                </div>
                                            </div>

                                            <div class="col-sm-2 labelText1 padding0 ">
                                                <div class="col-sm-1 padding03 paddingtop0">
                                                    <asp:CheckBox Text="" ID="chkMakeAdj" AutoPostBack="true" runat="server" OnCheckedChanged="chkMakeAdj_CheckedChanged" />
                                                </div>
                                                <div class="col-sm-10 paddingLeft3">
                                                    <asp:Label Text="Assemble Code" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-1 labelText1 padding0 ">
                                                <div class="col-sm-2 padding0 paddingtop0">
                                                    <asp:CheckBox Text="" ID="chkDirectOut" AutoPostBack="true" runat="server" OnCheckedChanged="chkDirectOut_CheckedChanged" />
                                                </div>
                                                <div class="col-sm-10 paddingLeft3">
                                                    <asp:Label Text="Direct Out" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-7 padding0">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 labelText1 padding0">
                                                                    From
                                                                </div>
                                                                <div class="col-sm-7 padding03">
                                                                    <asp:TextBox runat="server" ID="txtFrom" CausesValidation="false" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 padding03">
                                                                    <asp:LinkButton ID="txtFroml" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFrom"
                                                                        PopupButtonID="txtFroml" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 labelText1 padding0">
                                                                    To
                                                                </div>
                                                                <div class="col-sm-7 padding03">
                                                                    <asp:TextBox runat="server" Enabled="false" ID="txtTo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 padding03">
                                                                    <asp:LinkButton ID="txtTol" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                                        PopupButtonID="txtTol" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-5 padding0">
                                                            <div class="col-sm-1 labelText1 padding0">
                                                                Type
                                                            </div>
                                                            <div class="col-sm-10 paddingRight5" style="padding-left: 15px;">
                                                                <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 padding03">
                                                            <div class="col-sm-5 labelText1 padding03">
                                                                Search By
                                                            </div>
                                                            <div class="col-sm-7 padding0">
                                                                <asp:DropDownList ID="ddlSerTp" OnSelectedIndexChanged="ddlSerTp_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="ReqNo" Text="Request" />
                                                                    <asp:ListItem Value="RefNo" Text="Reference" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4" style="padding-left: 0px; padding-right: 5px;">
                                                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-sm-4 padding0">
                                                            <div class="col-sm-4  labelText1" style="padding-left: 2px; padding-right: 0px;">
                                                                Req. By
                                                            </div>
                                                            <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                <asp:TextBox ID="txtReqBy" ReadOnly="true" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-sm-1 padding3">
                                                                <asp:LinkButton ID="btnSearchUsr" runat="server" CausesValidation="false" OnClick="btnSearchUsr_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 padding0">
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Req. Loc
                                                            </div>
                                                            <div class="col-sm-6" style="padding-left: 2px; padding-right: 0px;">
                                                                <asp:TextBox ID="txtReqLoc" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtReqLoc_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 padding3">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="btnSReqLoc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Button Text="Search" ID="btnDocSearch" runat="server" OnClick="btnDocSearch_Click" />
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 padding0 labelText1">
                                                                Document #
                                                            </div>
                                                            <div class="col-sm-7 padding0">
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDocNo" AutoPostBack="true" OnTextChanged="txtDocNo_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                <asp:LinkButton ID="lbtnSeDocNo" runat="server" CausesValidation="false" OnClick="lbtnSeDocNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 paddingLeft0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading padding0 height16">

                                                <div class="row  ">
                                                    <div class="col-sm-12  ">
                                                        <div class="col-sm-4 paddingLeft5">
                                                            <strong>Pending Request </strong>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="col-sm-1">
                                                                <asp:CheckBox ID="chkAODoutserials" AutoPostBack="true" OnCheckedChanged="chkAODoutserials_CheckedChanged" runat="server" Width="5px"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-7 paddingLeft3 ">
                                                                <strong>
                                                                    <asp:Label Text="Auto Scan Non-Serialized Items" runat="server" /></strong>
                                                            </div>
                                                            <div class="col-sm-3 padding0 ">
                                                                <div class="col-sm-2 padding0 ">
                                                                    <asp:CheckBox AutoPostBack="true" ID="chkPendingDoc" OnCheckedChanged="chkPendingDoc_CheckedChanged" Text="" runat="server" />
                                                                </div>
                                                                <div class="col-sm-10 padding0 ">
                                                                    <asp:Label Text="PDA Completed" runat="server" ID="lblAllPendin" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body padding0">
                                                <div class="row height70">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body padding0">
                                                                <div class="dvScroll" onscroll="setScrollPosition(this.scrollTop);" style="height: 100px; overflow: scroll">
                                                                    <asp:GridView ID="gvPending" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="7" OnPageIndexChanging="gvPending_PageIndexChanging" OnRowCommand="gvPending_RowCommand" PagerStyle-CssClass="cssPager">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox Text=" " ID="pen_Select" runat="server" Enabled="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Request No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton Text='<%# Bind("itr_req_no") %>' ID="Btnitr_req_no" runat="server" OnClick="Btnitr_req_no_Click" Width="120px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_dt" runat="server" Text='<%# Bind("itr_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Entry Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_tp" runat="server" Text='<%# Bind("itr_tp") %>' Width="70px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Ref. No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_ref" runat="server" Text='<%# Bind("itr_ref") %>' Width="120px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req. Com">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_com" runat="server" Text='<%# Bind("itr_com") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req. Loc">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_rec_to" runat="server" Text='<%# Bind("itr_rec_to") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="GRNA Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_gran_nstus" runat="server" Text='<%# Bind("itr_gran_nstus") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Base DIN">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_anal1" runat="server" Text='<%# Bind("itr_anal1") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="App. Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_gran_app_stus" runat="server" Text='<%# Bind("itr_gran_app_stus") %>' Width="65px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Job No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_job_no" runat="server" Text='<%# Bind("itr_job_no") %>' Width="130px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Job Line" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_job_line" runat="server" Text='<%# Bind("itr_job_line") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SubType">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_sub_tp" runat="server" Text='<%# Bind("itr_sub_tp") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Vehicle">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitr_vehi_no" runat="server" Text='<%# Bind("itr_vehi_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Vehicle" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTMP_Tuh_fin_stus" runat="server" Text='<%# Bind("Itr_pda_comp") %>'></asp:Label>
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
                                    <div class="col-sm-6 paddingRight0 paddingLeft0">
                                        <div class="col-sm-12 paddingRight0 paddingLeft0">
                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                <div class="col-sm-12 labelText1">
                                                    Document Type 
                                                </div>
                                                <div class="col-sm-10 paddingRight5 paddingLeft0">
                                                    <asp:DropDownList runat="server" ID="ddlManType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlManType_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                </div>
                                            </div>
                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                <div class="row">
                                                    <div class="col-sm-12 labelText1">
                                                        <div class="col-sm-1 ">
                                                            <asp:CheckBox Text="" ID="chkManualRef" AutoPostBack="true" runat="server" OnCheckedChanged="chkManualRef_CheckedChanged" />
                                                        </div>
                                                        <div class="col-sm-10 paddingLeft3">
                                                            <asp:Label Text="By Manual Ref." runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-10 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtManualRef" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtManualRef_TextChanged" />
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                <div class="col-sm-12 labelText1">
                                                    Direct Type
                                                </div>
                                                <div class="col-sm-10 paddingRight5 paddingLeft0">
                                                    <asp:DropDownList runat="server" ID="cmbDirType" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                </div>
                                            </div>
                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                <div class="col-sm-12 labelText1">
                                                    Direct Scan
                                                </div>
                                                <div class="col-sm-10 paddingRight5 paddingLeft0">
                                                    <asp:DropDownList runat="server" ID="cmbDirectScan" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbDirectScan_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 height5"></div>
                                        <div class="col-sm-12 padding0">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-12 labelText1">
                                                                    Date
                                                                </div>
                                                                <div class="col-sm-10 padding0">
                                                                    <asp:TextBox runat="server" Enabled="false" ID="txtDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 padding3">
                                                                    <asp:LinkButton ID="txtDatel" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                                                                        PopupButtonID="txtDatel" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-12 labelText1">
                                                                    Selected Request No
                                                                </div>
                                                                <div class="col-sm-12 padding03">
                                                                    <asp:TextBox ID="txtRequest" runat="server" ReadOnly="true" CssClass="form-control" />
                                                                    <asp:DropDownList runat="server" ID="ddlRecCompany" CssClass="form-control" OnDataBound="ddlRecCompany_DataBound" onchange="showTooltip(this)" Visible="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-12 labelText1">
                                                                    Rec. Loc
                                                                </div>
                                                                <div class="col-sm-10 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtDispatchRequried" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDispatchRequried_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSearch_RecLocation" runat="server" CausesValidation="false" OnClick="btnSearch_RecLocation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                                <asp:Label ID="lblseq" Visible="false" runat="server"></asp:Label>
                                                                <asp:Button Text="Send to scan" ID="Button3" runat="server" OnClick="btnSentScan_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-6 labelText1 padding0">
                                                                <div class="col-sm-1 ">
                                                                    <asp:RadioButton GroupName="boq" Enabled="true" Text="" ID="chkProd" AutoPostBack="true" runat="server" OnCheckedChanged="chkProd_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    Production #
                                                                </div>
                                                                <div class="col-sm-1 ">
                                                                    <asp:RadioButton GroupName="boq" Enabled="true" Text="" ID="chkboq" AutoPostBack="true" runat="server" OnCheckedChanged="chkboq_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    BOQ #
                                                                </div>
                                                                <div class="col-sm-1 ">
                                                                    <asp:RadioButton Text="" GroupName="boq" ID="chkbatch" AutoPostBack="true" runat="server" OnCheckedChanged="chkbatch_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    Batch #
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0 labelText1">
                                                                <div class="col-sm-8 paddingRight5 ">
                                                                    <asp:TextBox ID="txtBoq" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBoq_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnProCode" runat="server" CausesValidation="false" OnClick="lbtnProCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 height5"></div>
                                        <div class="col-sm-12 padding0">
                                            <%--<div class="col-sm-3">
                                                <asp:LinkButton ID="lbtnTransportMth" runat="server" CausesValidation="false" OnClick="lbtnTransportMth_Click">Transport Method
                                                         <span class="glyphicon" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>--%>

                                            <div class="col-sm-3">
                                                 <asp:LinkButton ID="btnColapse" Visible="true" Text="Transport Method" runat="server">Transport Method
                                                          <span class="glyphicon" aria-hidden="true"></span>
                                                 </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-9 paddingRight0 paddingLeft0">
                                                <div class="col-sm-2 padding0">
                                                    Remark
                                                </div>
                                                <div class="col-sm-10 padding0" style="padding-right: 32px">
                                                    <asp:TextBox ID="txtnewRemarks" MaxLength="100" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <asp:Panel runat="server" Visible="false">
                                                <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-2 padding0">
                                                        Remark
                                                    </div>
                                                    <div class="col-sm-10 padding0" style="padding-right: 32px">
                                                        <asp:TextBox ID="txtRemarks" MaxLength="100" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingRight0 paddingLeft0" visible="false">
                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1 paddingLeft5 paddingRight0">
                                                            Transport Method
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                            <asp:DropDownList ID="ddlDeliver" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDeliver_SelectedIndexChanged">
                                                                <%-- <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-4 abelText1 paddingLeft5 paddingRight0">
                                                            <asp:Label ID="lblVehicle" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                            <asp:TextBox ID="txtVehicle" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <%-- Dilshan on 13/03/2018 --%>
                                    <%-- Transport Method --%>
                    <div class="row">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <asp:CollapsiblePanelExtender runat="server" ID="cpe" TargetControlID="pnlCollaps" CollapseControlID="btnColapse"
                                        ExpandControlID="btnColapse" Collapsed="true" CollapsedSize="0" ExpandedSize="200" ExpandedText="(Collapse...)" CollapsedText="(Expand...)">
                                    </asp:CollapsiblePanelExtender>
                                    <asp:Panel ID="pnlCollaps" runat="server">
                                        <div class="panel panel-default">
                                            <div class="panel panel-heading">
                                                Transport Method
                                            </div>
                                            <div class="panel panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-7 padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-3 padding0 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                Transport Method
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-12 labelText1" style="padding-left: 3px; padding-right: 3px;">
                                                                                    <asp:DropDownList ID="ddlTransportMe" AutoPostBack="true" OnSelectedIndexChanged="ddlTransportMe_SelectedIndexChanged" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3 padding0 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                Service By
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-12 labelText1" style="padding-left: 3px; padding-right: 3px;">
                                                                                    <asp:DropDownList AutoPostBack="true" ID="ddlServiceBy" OnSelectedIndexChanged="ddlServiceBy_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                                                                        <%--<asp:ListItem Value="0" Text="--Select--" />--%>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div runat="server" id="divSubLvl" class="col-sm-2 padding0 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <asp:Label ID="lblSubLvl" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-10 padding0 labelText1">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtSubLvl_TextChanged" ID="txtSubLvl" CssClass="form-control" runat="server" />
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                                <div class="col-sm-2 labelText1" style="padding-left: 3px; padding-right: 3px;">
                                                                                    <asp:LinkButton ID="lbtnSeVehicle" CausesValidation="false" runat="server" OnClick="lbtnSeVehicle_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                Remarks
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 labelText1" style="padding-right:10px; padding-left:10px;">
                                                                                <asp:TextBox ID="txtRemarksTrans" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 " runat="server" id="divNxtLvlD1">
                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1">
                                                                    <asp:Label ID="lblNxtLvlD1" Text="" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-9 padding0" style="margin-top:-1px;">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtNxtLvlD1" AutoPostBack="true" OnTextChanged="txtNxtLvlD1_TextChanged" CssClass="form-control" runat="server" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-sm-3 padding3">
                                                                        <asp:LinkButton ID="lbtnSeDriver" CausesValidation="false" runat="server" OnClick="lbtnSeDriver_Click">
                                                                               <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 padding0" runat="server" id="divNxtLvlD2">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 padding0 labelText1">
                                                                        <asp:Label ID="lblNxtLvlD2" Text="" runat="server" CssClass="labelText1" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                   <div class="col-sm-9 padding0" style="margin-top:-1px;">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtNxtLvlD2" AutoPostBack="true" OnTextChanged="txtNxtLvlD2_TextChanged" CssClass="form-control" runat="server" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-sm-3 padding3">
                                                                        <asp:LinkButton ID="lbtnSeHelper" CausesValidation="false" runat="server" OnClick="lbtnSeHelper_Click">
                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <div class="col-sm-9 paddingRight0">
                                                                <div class="row">
                                                                    <div class="col-sm-12 labelText1 text-center padding0">
                                                                        Nof packing
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 " style="margin-top:-1px;">
                                                                        <asp:TextBox ID="txtNoOfPacking" CssClass="txtNoOfPacking form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 padding0 labelText1">
                                                                <div class="buttonRow">
                                                                    <div style="margin-top: 10px;">
                                                                        <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="false" CssClass="floatleft"
                                                                            OnClick="lbtnAdd_Click">
                                                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top:2px;">
                                                    <div class="col-sm-12">
                                                        <div style="height: 125px; overflow-x: hidden; overflow-y: auto;">
                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="dgvTrns" CssClass="table table-hover table-striped" runat="server"
                                                                        GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Tra. Method">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTraMe" Text='<%# Bind("Itrn_trns_pty_tp") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Service By">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSerBy" Text='<%# Bind("Itrn_trns_pty_cd") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHedSubLvl" Text='Ref #' runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmSubLvl" Text='<%# Bind("Itrn_ref_no") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemarks" Text='<%# Bind("Itrn_rmk") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHedNxtLvlD1" Text='Driver/Hand Over' runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmNxtLvlD1" Text='<%# Bind("Itrn_anal1") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHedNxtLvlD2" Text='Helper' runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmNxtLvlD2" Text='<%# Bind("Itrn_anal2") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRowNo" Text='<%# Bind("_grdRowNo") %>' Visible="false" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="margin-top: -3PX; width: 10px;">
                                                                                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="false"
                                                                                            OnClientClick="return ConfirmDelete()" OnClick="lbtnDel_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                                    <%-- ********************* --%>

                                    <asp:Panel runat="server" ID="pnloutscan">
                                        <div class="col-sm-12 padding0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading padding0 height16">
                                                    Item Details
                                                </div>
                                                <div class="panel-body padding0">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <uc1:ucOutScan runat="server" ID="ucOutScan" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-sm-12 padding0">
                                        <div class="panel panel-default">
                                            <div class="panel-body padding0">
                                                <div class="row">
                                                    <div class="panel-body paddingtopbottom0">
                                                        <div class="col-sm-12">

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <ul id="myTab2" class="nav nav-tabs">
                                                                        <li class="active"><a href="#Item" data-toggle="tab">Item</a></li>
                                                                        <li><a href="#Serial" data-toggle="tab">Serial</a></li>
                                                                         <div class="col-sm-1">
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:CheckBox Text="Get Similar Items" ID="chkChangeSimilarItem" AutoPostBack="true" runat="server" Style="float: right;" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:CheckBox Text="Change Status" Visible="false" ID="chkChangeStatus" AutoPostBack="true" OnCheckedChanged="chkChangeStatus_CheckedChanged" runat="server" Style="float: right; padding-right: 35px" />

                                                                        </div>
                                                                       
                                                                        <div class="col-sm-3 labelText1 padding0">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <strong>Document Qty :</strong>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <asp:Label Text="" ID="lblDocQty" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <strong>Serial Pick Qty :</strong>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <asp:Label Text="" ID="lblDocSerPickQty" runat="server" Style="text-align:left !important" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div id="myTabContent" class="tab-content">
                                                                        <div class="tab-pane fade in active" id="Item">
                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                            <asp:UpdatePanel ID="upGvItems" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="row ">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12 ">
                                                                                                   <%-- <div class="panel-body panelscollbar height100">--%>
                                                                                                    <div class="panel-body">
                                                                                                        <div class="dvScroll" onscroll="setScrollPosition(this.scrollTop);" style="height: 135px; overflow: scroll">
                                                                                                            <asp:GridView ID="gvItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="btnItm_AddSerial" runat="server" CausesValidation="false" OnClick="btnItm_AddSerial_Click">
                                                                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="btnItm_Remove" runat="server" CausesValidation="false" OnClientClick="return DelItemConfirm()" OnClick="btnItm_Remove_Click">
                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Item">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_itm_cd1" runat="server" Text='<%# Bind("itri_itm_cd") %>' Visible="false"></asp:Label>
                                                                                                                            <asp:LinkButton ID="lblitri_itm_cd" Text='<%# Bind("itri_itm_cd") %>' runat="server" Width="70px" OnClick="lblitri_itm_cd_Click" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="150px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblmi_longdesc" runat="server" Text='<%# Bind("mi_longdesc") %>' Width="150px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="150px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblmi_model" runat="server" Text='<%# Bind("mi_model") %>' Width="50px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="P.No">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblmi_part_no" runat="server" Text='<%# Bind("mi_part_no") %>' Width="50px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>' Width="30px" Visible="false"></asp:Label>
                                                                                                                            <asp:LinkButton ID="btnMis_desc" Text='<%# Bind("Mis_desc") %>' runat="server" Width="70px" OnClick="Btnitri_itm_stus_Click" />

                                                                                                                            <asp:LinkButton ID="Btnitri_itm_stus" Text='<%# Bind("itri_itm_stus") %>' runat="server" Width="70px" OnClick="Btnitri_itm_stus_Click" Visible="false" />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>

                                                                                                                    <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Request">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_note" runat="server" Text='<%# Bind("itri_note") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Job No">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_job_no" runat="server" Text='<%# Bind("itri_job_no") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Job Line No" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_job_line" runat="server" Text='<%# Bind("itri_job_line") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Main Item Code">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblITRI_MITM_CD" runat="server"
                                                                                                                                Visible='<%# !Eval("ITRI_MITM_CD").ToString().Equals("0")%>'
                                                                                                                                Text='<%# Bind("ITRI_MITM_CD") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="150px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="App. Qty">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Balance. Qty">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_bqty" runat="server" Text='<%# Bind("itri_bqty","{0:#,##0.####}") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Pick Qty">
                                                                                                                        <EditItemTemplate>
                                                                                                                            <asp:TextBox ID="txtlblitri_qty" onkeypress="return isNumberKey(event)" runat="server" Text='<%# Bind("itri_qty","{0:N2}") %>'></asp:TextBox>
                                                                                                                        </EditItemTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField ShowHeader="False">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="lbtngrdDOItemstEdit" CausesValidation="false" runat="server" OnClick="lbtngrdDOItemstEdit_Click">
                                                                                                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                        <EditItemTemplate>
                                                                                                                            <asp:LinkButton ID="lbtngrdDOItemstUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdDOItemstUpdate_Click">
                                                                                                                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </EditItemTemplate>
                                                                                                                        <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="res" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblitri_res_qty" runat="server" Text='<%# Bind("itri_res_qty") %>'></asp:Label>
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
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <div class="tab-pane fade" id="Serial">
                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 ">
                                                                                            <div style="height: 100px; overflow-y: auto; overflow-x: hidden;">
                                                                                                <div class="col-sm-12 ">
                                                                                                    <div class="dvScroll" onscroll="setScrollPosition(this.scrollTop);" style="height: 135px; overflow: scroll">
                                                                                                        <asp:GridView ID="gvSerial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="btnSer_Remove" runat="server" CausesValidation="false" OnClientClick="return DelSerialConfirm()" OnClick="btnSer_Remove_Click">
                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Item">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_itm_cd" runat="server" Text='<%# Bind("tus_itm_cd") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="150px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Model">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_itm_model" runat="server" Text='<%# Bind("tus_itm_model") %>' Width="150px"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Status">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>' Visible="true"></asp:Label>
                                                                                                                        <asp:LinkButton ID="btntus_itm_stus" Text='<%# Bind("tus_itm_stus") %>' runat="server" OnClick="btntus_itm_stus_Click" Visible="false" />
                                                                                                                        <asp:LinkButton ID="btnTus_itm_stus_Desc" Text='<%# Bind("Tus_itm_stus_Desc") %>' runat="server" OnClick="btntus_itm_stus_Click" Visible="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="150px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_qty" runat="server" Text='<%# Bind("tus_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="150px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Serial 1">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_ser_1" runat="server" Text='<%# Bind("tus_ser_1") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="150px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Serial 2">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_ser_2" runat="server" Text='<%# Bind("tus_ser_2") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Serial 3">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_ser_3" runat="server" Text='<%# Bind("tus_ser_3") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Bin">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_bin" runat="server" Text='<%# Bind("tus_bin") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_ser_id" runat="server" Text='<%# Bind("tus_ser_id") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Request" Visible="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_base_doc_no" runat="server" Text='<%# Bind("tus_base_doc_no") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="BaseLineNo" Visible="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbltus_base_itm_line" runat="server" Text='<%# Bind("tus_base_itm_line") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
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
                            <asp:Button ID="autoSelect" Text="AutoSelect" runat="server" OnClick="autoSelect_Click" Visible="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSearchcommon" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSearch" runat="server" Enabled="True" TargetControlID="btnSearchcommon"
                PopupControlID="pnlSearchCommon" CancelControlID="btnsearchComClose" PopupDragHandleControlID="divSearchChdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearchCommon" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-default height400 width700">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divSearchChdr" style="height: 28px">
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnsearchComClose" runat="server" OnClick="btnsearchComClose_Click1" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body paddingtopbottom0">
                            <div class="col-sm-12" id="Div10" runat="server">
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
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
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
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerialPick" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPickSerial" runat="server" Enabled="True" TargetControlID="btnSerialPick"
                PopupControlID="pnlPickSerial" CancelControlID="btnPSPClose" PopupDragHandleControlID="divPSPHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPickSerial" Style="display: none;">
                <div runat="server" id="Div4" class="panel panel-default height350 width850">
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="divPSPHdr">

                            <div class="col-sm-11">
                                <strong>Advanced Serial Search</strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnPSPClose" runat="server" OnClick="btnPSPClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body paddingtopbottom0">
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
                                    <div class="col-sm-10" id="Div6" runat="server">
                                        <div class="col-sm-2 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyA" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-2 labelText1">
                                            <asp:Label ID="lblPickSerText" Text="Search by word" runat="server" />
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordA" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordA_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchA" runat="server" OnClick="lbtnSearchA_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbywordA" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2" runat="server">
                                        <asp:Button ID="btnAdvanceAddItem" runat="server" CssClass="btn btn-primary btn-xs" Text="Add" OnClick="btnAdvanceAddItem_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Panel runat="server" Visible="false">
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" ID="txtPopupQty" Text="0.00" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblPopupQty" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblScanQty" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblApprovedQty" runat="server" />
                                        </div>
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="height: 275px;">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAdSearch" AutoGenerateColumns="False" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                                    runat="server"
                                                    GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" AllowPaging="True" PageSize="10"
                                                    OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="selectchk" OnCheckedChanged="selectchk_CheckedChanged" AutoPostBack="true"
                                                                    Checked='<%#Convert.ToBoolean(Eval("Tus_isSelect")) %>'
                                                                    runat="server" Width="10px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_2" runat="server" Text='<%# Bind("Tus_ser_2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Warranty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_warr_no" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BIN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_bin" runat="server" Text='<%# Bind("Tus_bin") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Desc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_desc" runat="server" Text='<%# Bind("Tus_itm_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_doc_dt" runat="server" Text='<%# Bind("tus_doc_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_exist_supp" runat="server" Text='<%# Bind("tus_exist_supp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_unit_cost" runat="server" Text='<%# Bind("tus_unit_cost") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="cssPager" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnchnitemstsatui" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpChangeItemStatus" runat="server" Enabled="True" TargetControlID="btnchnitemstsatui"
                PopupControlID="pnlChangeItemStatus" CancelControlID="btnclseitmsrtu" PopupDragHandleControlID="divStuschane" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlChangeItemStatus" DefaultButton="lbtnSearch" Width="300px" Height="250px">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading" id="divStuschane" style="height: 28px">
                        <div class="col-sm-9">
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="btnclseitmsrtu" runat="server" Style="float: right" OnClick="btnclseitmsrtu_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="gvItemStatus" AutoGenerateColumns="false" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvItemStatus_RowCommand">
                            <Columns>
                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINL_ITM_STUS" runat="server" Text='<%# Bind("INL_ITM_STUS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPPDA" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="CustomerPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="CustomerPanel">
                <div runat="server" id="Div2" class="panel panel-default height150 width525">
                    <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btncClose" runat="server" CausesValidation="false" OnClick="btncClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Document No
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdocname" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Loading Bay
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" TabIndex="2" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5">
                                                </div>

                                                <div class="col-sm-7">
                                                    <asp:Button ID="btnsend" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Send" OnClientClick="ConfirmSendToPDA();" OnClick="btnsend_Click" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
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


    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div1" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
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
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label9" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnSbu" runat="server" Text="Ok" CausesValidation="false" class="btn btn-primary" OnClick="btnSbu_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel ID="up2" runat="server">
        <ContentTemplate>
            <div>
                <asp:Button ID="btn11" runat="server" Text="Button" Style="display: none;" />
                <asp:ModalPopupExtender ID="popupTransport" runat="server" Enabled="True" TargetControlID="btn11"
                    PopupControlID="pnlPop" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlPop" Style="margin-top: -100px;">
        <div runat="server" class="panel panel-default height45 width700 ">
            <div class="col-sm-12 padding0">
                <div class="panel panel-default">
                    <div class="panel-heading height30">
                        <div class="col-sm-11">
                            <strong><b>Transport Method</b></strong>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="lbtnCls" runat="server" OnClick="lbtnCls_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel panel-body">
                        <%--<uc1:ucTransportMethode runat="server" ID="ucTransportMethode" />--%>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-default height400 width850">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div7" runat="server">
                                <div class="row">
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-6 padding0">
                                            <div class="col-sm-4 labelText1">
                                                From
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFDate"
                                                    PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 padding0">
                                            <div class="col-sm-4 labelText1">
                                                To
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTDate"
                                                    PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>

                                        </div>
                                        <div class="col-sm-1 padding3">
                                            <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-8 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server" OnClick="lbtnSearchD_Click">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="6" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait3" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait3" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>

                                </asp:UpdateProgress>
                            </div>
                        </div>

                    </div>

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnPrintPnl" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popUpPrint" runat="server" Enabled="True" TargetControlID="btnPrintPnl"
                PopupControlID="pnlPrint" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress9" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel8">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait235" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait235" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlPrint" runat="server" align="center">
        <div runat="server" id="Div8" class="panel panel-info height120 width250">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label12" runat="server"></asp:Label>
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
                                <asp:Button ID="lbtnPrintOk" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="lbtnPrintOk_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="linkPrintNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="linkPrintNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn16" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popuplocQty" runat="server" Enabled="True" TargetControlID="btn16"
                PopupControlID="pnllocQty" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel10">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnllocQty">
                <div runat="server" id="Div9" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 1100px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Location Quantity</strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnlocQty" runat="server" OnClick="lbtnlocQty_Click">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 400px; overflow-y: auto;">
                                                                        <asp:GridView ID="dgvlocQty" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False"
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Item #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblitemCode" Text='<%# Bind("itemCode") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblitemStatus" Text='<%# Bind("itemStatus") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Pick Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_by" Text='<%# Bind("Pick_qty") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="In Hand">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("inHand") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Free">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("free") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Reserved">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("reserved") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Resevation Qty" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_cd" Text='<%# Bind("Inl_resQty") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Error">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("errorMsg") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
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
