<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="MaterialRequisitionNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.MaterialRequisitionNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

        <script>
            function setScrollPosition(scrollValue) {

                $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
    </script>
    <script type="text/javascript">

        function ConfirmClearForm() {
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalsave = confirm("Do you want to save ?");
            if (selectedvalsave) {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "No";
            }
        };

        function ConfirmDelete() {
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };

        function ConfirmApproveRequest() {
            var selectedvaldelitm = confirm("Do you want to approve ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtapprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancelReq() {
            var selectedvaldelitm = confirm("Do you want to cancel ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtcancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancel.ClientID %>').value = "No";
            }
        };

        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
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

        function Enable() {
            return;
        }

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
        }

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
        }

        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum characters allowed are " + maxLength + ".Extra characters have been removed !!!");
                textBox.value = textBox.value.substr(0, maxLength);
            }
        };

    </script>

    <style type="text/css">
        body {
            overflow-x: hidden;
        }
       
    </style>

    <%-- <style type="text/css">
        body {
            overflow-y: hidden;
        }
    </style>--%>

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
        .divWaiting2{
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
    <style>
        .buttonRow {
            height:30px;
        }
        .panel-body{
            padding-top:0px;
            padding-bottom:0px;
            padding-left:0px;
            padding-right:0px;
        }
        .panel{
            margin-bottom:2px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="blkcancelpnl">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanelMain">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />
            <asp:HiddenField ID="txtapprove" runat="server" />
            <asp:HiddenField ID="txtcancel" runat="server" />
            <asp:HiddenField ID="hfScrollPosition"  Value="0" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-7  buttonrow">
                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">

                            <strong>Info!</strong>
                            <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>

                        </div>
                    </div>

                    <div class="col-sm-4  buttonRow">

                        <div class="col-sm-3 padding0">
                            <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save/Process
                            </asp:LinkButton>
                        </div>

                        <div id="hidediv" runat="server">
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="btnApproved" CausesValidation="false" CssClass="floatRight" runat="server" OnClientClick="ConfirmApproveRequest();" OnClick="btnApproved_Click">
                                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true" style="font-size:20px"></span> Approve
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="btnCancel" CausesValidation="false" CssClass="floatRight" runat="server" OnClientClick="ConfirmCancelReq();" OnClick="btnCancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size:20px"></span> Cancel
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                    </div>

                    <div class="col-sm-1  buttonRow">

                        <div class="col-sm-12">

                            <div class="col-sm-2 paddingRight0">
                                <div class="dropdown">
                                    <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    </a>
                                    <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div id="blkcancelhide" runat="server">
                                                    <div class="col-sm-12 paddingRight0 MRNINTRBlkCancelbtn">

                                                        <asp:LinkButton ID="btnBulkCancel" CausesValidation="false" runat="server" OnClick="LinkButton3_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size:20px"></span>Bulk Cancel
                                                        </asp:LinkButton>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 paddingRight0">
                                                    <asp:LinkButton ID="btnPrint" CausesValidation="false" runat="server" OnClick="btnPrint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true" style="font-size:20px"></span>Print 
                                                    </asp:LinkButton>
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
                    </div>
                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-10">

                            <div class="panel panel-default stockinwarpendinggrid " id="dvContentsOrder">

                                <div class="panel-heading pannelheading ">

                                    <div class="row">
                                        <div class="col-sm-2">
                                            <strong>
                                                <asp:Label ID="lbltitle" runat="server"></asp:Label></strong>
                                        </div>

                                        <div id="hidden" runat="server">
                                            <div class="col-sm-1">
                                                <asp:CheckBox ID="chkReqType" runat="server" AutoPostBack="true" OnCheckedChanged="chkReqType_CheckedChanged" />
                                            </div>
                                            <div class="col-sm-2" style="margin-left: -70px">
                                                <asp:Label Text="Request from W/H" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="panel-body" id="panelbodydiv">
                                    <div class="row">
                                        <div class="col-sm-12 labelText1">
                                            <div class="col-sm-2">
                                                <div class="row">

                                                    <div class="col-sm-4 labelText1">
                                                        Date
                                                    </div>

                                                    <div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtRequestDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="txtRequestDate"
                                                                PopupButtonID="lbtnimgselectdate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                            <asp:LinkButton ID="lbtnimgselectdate" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-2">
                                                <div class="row">

                                                    <div class="col-sm-4 labelText1">
                                                        Required Date
                                                    </div>

                                                    <div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtRequriedDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calexreq" runat="server" TargetControlID="txtRequriedDate"
                                                                PopupButtonID="lbtnimgselectreqdate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div id="caldv2" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                            <asp:LinkButton ID="lbtnimgselectreqdate" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-3">

                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        Sub Type
                                                    </div>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList ID="ddlRequestSubType" runat="server" TabIndex="3" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlRequestSubType_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-3">

                                                <div class="row">

                                                    <div class="col-sm-2 labelText1">
                                                        Dispatch Company
                                                    </div>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlCompany" runat="server" TabIndex="4" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-2 paddingleft0">
                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        Dispatch Location
                                                    </div>

                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtDispatchRequried" runat="server" AutoPostBack="true" OnTextChanged="txtDispatchRequried_TextChanged" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnreqnosearch" runat="server" TabIndex="6" OnClick="lbtnreqnosearch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">

                            <div class="panel panel-default  " id="dvContentsOrder1e">

                                <div class="panel-heading pannelheading ">
                                    Request Search
                                </div>

                                <div class="panel-body" id="panelbodydiv1e">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-11 paddingRight5">
                                                <asp:TextBox ID="txtRequest" placeholder="Document Number" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtRequest_TextChanged1"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton1" runat="server" TabIndex="7" OnClick="LinkButton1_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row height5"></div>
                                        <div class="row">

                                            <div class="col-sm-11 paddingRight5">
                                                <asp:TextBox ID="txtBoq" placeholder="BOQ Number" OnTextChanged="txtBoq_TextChanged" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnProCode" runat="server" OnClick="lbtnProCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row" style="height:5px;">

                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>

                    </div>


                </div>

                <div id="hiddendiv" runat="server">

                    <div class="row">

                        <div class="panel-body">

                            <div class="col-sm-2">

                                <div id="showhide1" runat="server" visible="false">

                                    <div class="panel panel-default stockinwarpendinggrid " id="dvContentsOrdere">

                                        <div class="panel-heading pannelheading ">
                                            Document Detail
                                        </div>

                                        <div class="panel-body" id="panelbodydive">

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        <asp:Label ID="lbldoctype" runat="server" Text="Doc."></asp:Label>
                                                    </div>

                                                    <div class="col-sm-8 paddingRight5">
                                                        <asp:TextBox ID="txtInvoice" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" TabIndex="8" OnClick="LinkButton2_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                </div>

                                <div id="showhide2" runat="server" visible="true">

                                    <div class="alert alert-info mrnalertdiv" role="alert">
                                        <a href="#" class="alert-link">
                                            <asp:Label ID="lblCaption" runat="server"></asp:Label>
                                        </a>
                                    </div>

                                </div>

                            </div>

                            <div class="col-sm-10">

                                <div class="panel panel-info" id="dvContentsOrder1">

                                    <div class="panel-heading pannelheading ">
                                        Customer Detail
                                    </div>

                                    <div class="panel-body" id="panelbodydiv1 list-group-item list-group-item-info">

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-4">

                                                    <div class="row">

                                                        <div class="col-sm-3 labelText1">
                                                            <strong>Code </strong>
                                                        </div>
                                                        <div class="col-sm-9 stockinwardlabels">
                                                            <asp:Label ID="lblCusCode" CssClass="labelText1" runat="server"></asp:Label>
                                                        </div>

                                                    </div>

                                                </div>

                                                <div class="col-sm-4">

                                                    <div class="row">

                                                        <div class="col-sm-3 labelText1">
                                                            <strong>Name </strong>
                                                        </div>
                                                        <div class="col-sm-9 stockinwardlabels">
                                                            <asp:Label ID="lblCusName" CssClass="labelText1" runat="server"></asp:Label>
                                                        </div>

                                                    </div>

                                                </div>

                                                <div class="col-sm-4">

                                                    <div class="row">

                                                        <div class="col-sm-3 labelText1">
                                                            <strong>Address </strong>
                                                        </div>
                                                        <div class="col-sm-9 stockinwardlabels">
                                                            <asp:Label ID="lblCusAddress" CssClass="labelText1" runat="server"></asp:Label>
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

                <div id="showhidegrd" runat="server" visible="false">

                    <div class="row">

                        <div class="panel-body">

                            <div class="col-sm-12">

                                <div class="panel panel-default " id="dvContentsOrder1dd">

                                    <div class="row">

                                        <div class="col-sm-1 panel-heading pannelheading">
                                            Invoice Details
                                        </div>

                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnaddinvitems" CausesValidation="false" TabIndex="14" CssClass="floatRight" runat="server" OnClick="lbtnaddinvitems_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                        <div id="subdv" style="margin-top: 3px">
                                            Quantity
                                        </div>

                                        <div class="col-sm-1" style="margin-top: -16px; margin-left: 148px">
                                            <asp:TextBox runat="server" ID="txt_cashGiven" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="panel-body" id="panelbodydivdd1">

                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="panelscollheight50">

                                                    <asp:GridView ID="gvInvoice" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                        <Columns>

                                                            <asp:TemplateField HeaderText="No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblno" runat="server" Text='<%# Bind("sad_itm_line") %>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitem" runat="server" Text='<%# Bind("SAD_ITM_CD") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="150px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("SAD_ITM_STUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("SAD_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="3%" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllineno" runat="server" Text='<%# Bind("SAD_ITM_LINE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>

                                                        <SelectedRowStyle BackColor="Silver" />

                                                    </asp:GridView>

                                                </div>


                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>



                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading">
                                    Order Items
                                </div>

                                <div class="panel-body">

                                    <div id="hidedataentrydv" runat="server">

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <asp:Panel runat="server" ID="itempnl" DefaultButton="defbtn">

                                            <div class="col-sm-2">

                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        Item
                                                        <asp:Button ID="defbtn" runat="server" OnClick="defbtn_Click" BorderStyle="None" BackColor="White" />
                                                    </div>
                                                    <div class="col-sm-8 paddingRight5">
                                                        <asp:TextBox ID="txtItem" OnTextChanged="txtItem_TextChanged"  runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnmodelfind" runat="server" TabIndex="9" OnClick="lbtnmodelfind_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </asp:Panel>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    Status
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="cmbStatus" runat="server" TabIndex="10" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbStatus_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Reservation
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtReservation" AutoPostBack="true" OnTextChanged="txtReservation_TextChanged" runat="server" TabIndex="11" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    Qty
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtQty" runat="server" onkeydown="return jsDecimals(event);" TabIndex="12" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Aval.Qty
                                                </div>
                                                <div class="col-sm-8 stockinwardlabels">
                                                    <asp:Label ID="lblAvalQty" CssClass="labelText1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Free Qty
                                                </div>
                                                <div class="col-sm-8 stockinwardlabels">
                                                    <asp:Label ID="lblFreeQty" CssClass="labelText1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="col-sm-10">
                                            <div class="row">
                                                <div class="col-sm-1 labelText1">
                                                    Batch #
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:TextBox ID="txtBatch" runat="server" OnTextChanged="txtBatch_TextChanged" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>

                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnBatch" runat="server" TabIndex="9" OnClick="lbtnBatch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1 labelText1">
                                                    Remarks
                                                </div>

                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtItmRemark" runat="server" MaxLength="200" TabIndex="13" Height="25px" CssClass="form-control" TextMode="MultiLine" Style="resize: none" onKeyUp="javascript:Check(this, 200);" onChange="javascript:Check(this, 200);"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:LinkButton ID="btnAddItem" CausesValidation="false" TabIndex="14" CssClass="floatRight" runat="server" OnClick="btnAddItem_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="panel-body">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">

                                                        <div class="panel-heading panelHeadingInfoBar">

                                                            <div class="col-sm-4">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Description-
                                                                    </div>

                                                                    <div class="col-sm-8 prnlbldescription">
                                                                        <asp:Label ID="lblItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-2 padding0">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Model-
                                                                    </div>
                                                                    <div class="col-sm-8" style="margin-top: 3px">
                                                                        <asp:Label ID="lblItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-2">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Brand-
                                                                    </div>
                                                                    <div class="col-sm-8" style="margin-top: 3px">
                                                                        <asp:Label ID="lblItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="col-sm-1 padding0">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1 padding0">
                                                                        UOM -
                                                                    </div>
                                                                    <div class="col-sm-8" style="margin-top: 3px">
                                                                        <asp:Label ID="lblItmUom" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="col-sm-3">

                                                                <div class="row">

                                                                    <div class="col-sm-5 labelText1">
                                                                        Serial Status-
                                                                    </div>
                                                                    <div class="col-sm-7" style="margin-top: 3px">
                                                                        <asp:Label ID="lblItemSubStatus" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="dvScroll" style="height: 120px; overflow-y: scroll;" onscroll="setScrollPosition(this.scrollTop);">

                                        <asp:GridView ID="gvItem" AutoGenerateColumns="false" runat="server" OnRowUpdating="gvItem_RowUpdating" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                            <Columns>

                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtngrdInvoiceDetailsEdit" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lbtngrdInvoiceDetailsUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdInvoiceDetailsUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        &nbsp;<asp:LinkButton ID="lbtngrdInvoiceDetailsCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtngrdInvoiceDetailsCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnHdrDeleteAll" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete selected items ?');" OnClick="lbtnHdrDeleteAll_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                         </asp:LinkButton>
                                                        </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtngrdInvoiceDetailsDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtngrdInvoiceDetailsDalete_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="1%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  Visible="true">
                                                     <HeaderTemplate>
                                                         <asp:CheckBox Text="" ID="chkSelectAllGvItem" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAllGvItem_CheckedChanged" />
                                                     </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox Checked='<%#Convert.ToBoolean(Eval("Tmp_itm_select")) %>'  Text="" ID="chkSelectGvItem" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectGvItem_CheckedChanged" />
                                                    </ItemTemplate>
                                                     <ItemStyle CssClass="gridHeaderAlignRight" Width="1%" />
                                                     <HeaderStyle CssClass="gridHeaderAlignRight" Width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItri_line_no_tmp" runat="server" Text='<%# Bind("Itri_line_no_tmp") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnoitm" runat="server" Text='<%# Bind("Itri_line_no") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitem2" runat="server" Text='<%# Bind("Itri_itm_cd") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesc2" runat="server" Text='<%# Bind("Mi_longdesc") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbrand2" runat="server" Text='<%# Bind("Mi_brand") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmodel2" runat="server" Text='<%# Bind("Mi_Model") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmi_itm_uom" runat="server" Text='<%# Bind("mi_itm_uom") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus2" runat="server" Text='<%# Bind("Itri_mitm_stus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtnstusdesc" runat="server" Text='<%# Bind("Itri_mitm_stus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="lblqty2" runat="server" Text='<%# Bind("Itri_qty") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="secondlblqty2" runat="server" Text='<%# Bind("Itri_qty", "{0:N4}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="textAlignRight" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateField>

                                           <asp:TemplateField HeaderText="App Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblappQty" runat="server" Text='<%# Bind("Itri_app_qty", "{0:N4}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="textAlignRight" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reservation">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="lblres2" runat="server" Text='<%# Bind("Itri_res_no") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="secondlblres2" runat="server" Text='<%# Bind("Itri_res_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Remarks">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="lblremarks2" runat="server" Text='<%# Bind("Itri_note") %>' Enabled='<%# Eval("Tmp_Itri_app_rem_show").ToString().Equals("0")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="secondremarks2" runat="server" Text='<%# Bind("Itri_note") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="App Remarks">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAppRem" MaxLength="200" Visible='<%# !Eval("Tmp_Itri_app_rem_show").ToString().Equals("0")%>' Enabled='<%# !Eval("Tmp_Itri_app_rem_show").ToString().Equals("0")%>' runat="server" Text='<%# Bind("Tmp_Itri_app_rem") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAppRem" Visible='<%# !Eval("Tmp_Itri_app_rem_show").ToString().Equals("0")%>' runat="server" Text='<%# Bind("Tmp_Itri_app_rem") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label Text="" ID="lblHdrLocBal" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTmp_loc_bal" runat="server" Text='<%# Bind("Tmp_loc_bal","{0:N4}") %>' Visible='<%# !Eval("Tmp_loc_bal_show").ToString().Equals("0")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label Text="" ID="lblHdrDisLocBal" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTmp_dis_loc_bal" runat="server" Text='<%# Bind("Tmp_dis_loc_bal","{0:N4}") %>' Visible='<%# !Eval("Tmp_loc_bal_show").ToString().Equals("0")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Main Item" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmainitem2" runat="server" Text='<%# Bind("itri_mitm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Kit Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTmp_kit_cd" runat="server" Text='<%# Bind("Tmp_kit_cd") %>' Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tmp_base_doc_bal" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTmp_base_doc_bal" runat="server" Text='<%# Bind("Tmp_base_doc_bal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Itri_base_req_no" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItri_base_req_no" runat="server" Text='<%# Bind("Itri_base_req_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:HiddenField ID="hdfLoc" runat="server" />
                                    <asp:HiddenField ID="hdfDisLoc" runat="server" />
                                </div>

                            </div>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">
                        <div class="col-sm-12">

                            <div class="panel panel-default" id="dvContentsOrder22">

                                <div class="panel-heading pannelheading">
                                    
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-8">
                                                <strong>Identification</strong>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-2 text-right labelText1" style="padding-right:3px;">
                                                    Kit Item 
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <asp:TextBox CssClass="form-control" runat="server" AutoPostBack="true" id="txtKitCode" OnTextChanged="txtKitCode_TextChanged"/>
                                                </div>
                                                <div class="col-sm-1 labelText1" style="padding-left: 3px; padding-right: 0px; margin-top:-3px;">
                                                    <asp:LinkButton ID="lbtnClearKitCode" CausesValidation="false" runat="server" OnClick="lbtnClearKitCode_Click">
                                                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-5 labelText1" style="padding-left:3px; padding-right:0px;">
                                                    <asp:LinkButton ID="lbtnKitBrekUp" CausesValidation="false" runat="server" OnClick="lbtnKitBrekUp_Click">
                                                                            <span class="glyphicon " aria-hidden="true"></span><strong>KIT Breakup</strong>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body panelscollSmall " id="panelbodydi34v">

                                    <div class="col-sm-2">
                                        <div class="row labelText1">

                                            <div class="col-sm-3 labelText1">
                                                Requested By
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtRequestBy" runat="server" TabIndex="15" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="row labelText1">

                                            <div class="col-sm-3 labelText1">
                                                Approved By
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtApprovedBy" runat="server" TabIndex="16" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="row labelText1">

                                            <div class="col-sm-3 labelText1">
                                                Collector's NIC
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtNIC" runat="server" TabIndex="17" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="row labelText1">

                                            <div class="col-sm-3 labelText1">
                                                Collector's Name
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtCollecterName" runat="server" TabIndex="18" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="row labelText1">

                                            <div class="col-sm-3 labelText1">
                                                Remarks
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TabIndex="19" CssClass="form-control" onKeyUp="javascript:Check(this, 200);" onChange="javascript:Check(this, 200);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row labelText1">
                                            <asp:Panel runat="server" ID="pnlAppRemark">
                                                <div class="col-sm-3 labelText1">
                                                    Approve Remarks
                                                </div>

                                                <div class="col-sm-9 paddingRight5">
                                                    <asp:TextBox ID="txtAppRemark" MaxLength="200" runat="server" TabIndex="20" CssClass="form-control" onKeyUp="javascript:Check(this, 200);" onChange="javascript:Check(this, 200);"></asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                        <div class="col-sm-12">
                            <div class="row" runat="server" id="divMrnBalance">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 padding0 " style="width:23%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 paddingRight0" >
                                                        <strong>Approved Bank Guaranty</strong>
                                                    </div>
                                                    <div class="col-sm-6 labelText1" style="padding-right:3px; ">
                                                        <asp:TextBox Enabled="false" CssClass="form-control text-right" runat="server" ID="txtBGValue" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 labelText1">
                                                        <strong>Insured Value</strong>
                                                    </div>
                                                    <div class="col-sm-6 labelText1" style="padding-right:3px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control text-right" runat="server" ID="txtStockVal" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Showroom Stock Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control text-right" runat="server" ID="txtShStkVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:10%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Approved MRN Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtAppMrnVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:9%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Free Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtFreeVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Current MRN Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtCurrMrnVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:8%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Main Item Qty.</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtTotMrnCom" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Component Item Quantity</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtTotQtyMain" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Total Retail Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtSalesValue" />
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




    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">


                    <asp:UpdatePanel ID="blkcancelpnl" runat="server">
                        <ContentTemplate>


                            <div class="row">

                                <div class="col-sm-12">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                From
                                            </div>

                                            <div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="dtpFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpFrom"
                                                        PopupButtonID="lbtnimgselectdatenew" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldvs" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnimgselectdatenew" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4">
                                                Location
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtLoc" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtLoc_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 mrnbulkcancelsearchbtn">
                                                <asp:LinkButton ID="btnUserLocation" runat="server" OnClick="btnUserLocation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-5">
                                        <div class="row">

                                            <div class="col-sm-3">
                                                Reason
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlSearchReason" OnSelectedIndexChanged="ddlSearchReason_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-1">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:CheckBox runat="server" ID="chkAll" Text="All" />
                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                To
                                            </div>

                                            <div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="dtpTo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="dtpTo"
                                                        PopupButtonID="lbtnimgselectdatenewto" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldvss" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnimgselectdatenewto" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="row">

                                            <div class="col-sm-3">
                                                Status
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="cmbSearchStatus" OnSelectedIndexChanged="cmbSearchStatus_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>PENDING</asp:ListItem>
                                                    <asp:ListItem>APPROVED</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="row">

                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnGetInvoices" runat="server" OnClick="btnGetInvoices_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
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

                            <div class="col-sm-12">

                                <div class="row">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll2">

                                                <asp:GridView ID="dgvPromo" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="col_p_Get" runat="server" Width="30px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocationcancel" runat="server" Text='<%# Bind("ITR_LOC") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblreqnoblk" runat="server" Text='<%# Bind("ITR_REQ_NO") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldateblk" runat="server" Text='<%# Bind("ITR_DT", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Request For">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblreqforblk" runat="server" Text='<%# Bind("MSTP_DESC") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatusblk" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                            </div>


                                        </div>

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

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="lbtndconfirm" CausesValidation="false" runat="server" CssClass="floatleft" OnClick="lbtndconfirm_Click">
                                                        <span class="glyphicon glyphicon-align-justify fontsize20" aria-hidden="true"></span>Select All
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="lbtndreset" CausesValidation="false" CssClass="floatleft" runat="server" OnClick="lbtndreset_Click">
                                                        <span class="glyphicon glyphicon-list fontsize20" aria-hidden="true"></span>Unselect
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="lbtndclear" CausesValidation="false" runat="server" CssClass="floatleft" OnClientClick="ConfirmCancelReq();" OnClick="lbtndclear_Click">
                                                        <span class="glyphicon glyphicon-remove fontsize20" aria-hidden="true"></span>Process Cancel
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="lbtndcancel" CausesValidation="false" CssClass="floatleft" runat="server" OnClick="lbtndcancel_Click">
                                                        <span class="glyphicon glyphicon-refresh fontsize20" aria-hidden="true"></span>Clear
                                                </asp:LinkButton>
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
    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel7">

        <ProgressTemplate>
            <div class="divWaiting2">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div2" class="panel panel-default  width700" style="height:360px;">

                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" TabIndex="200" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" TabIndex="201" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" TabIndex="202" runat="server" class="form-control">
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
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" TabIndex="203" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
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
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
       <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlSearch">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait100" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait100" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlSearch">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-primary width800 height375">

                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>

                                            <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                                <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- pnl Old Part remove --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popKitBup" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlKitBup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlKitPop">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait101" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait101" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlKitBup">
        <asp:UpdatePanel runat="server" ID="upPnlKitPop">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 235px; width: 260px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-12 paddingLeft0">
                            <div class="col-sm-10 text-left">
                                <%--<strong>KIT Codes Breakup</strong>--%>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnKitBupClose" runat="server" OnClick="lbtnKitBupClose_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div style="height: 200px; overflow-y: scroll;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="dgvKitBup" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnSelectKit" runat="server" OnClick="lbtnSelectKit_Click">
                                                    <span class="glyphicon glyphicon-arrow-down"  aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%"/>
                                                    <HeaderStyle Width="10%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="KIT Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMIKC_ITM_CODE_MAIN" runat="server" Text='<%# Bind("MIKC_ITM_CODE_MAIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="90%"/>
                                                    <HeaderStyle Width="90%"/>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Add pop show for validation --%>
      <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel26">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lbsslWait101" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imssgWait101" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div3" class="panel panel-info height120 width250">
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
                                <asp:Button ID="btnConfOk" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnConfOk_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnConfNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnConfNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
