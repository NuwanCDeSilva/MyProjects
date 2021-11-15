<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CusdecBOI.aspx.cs" EnableEventValidation="false" Inherits="FastForward.SCMWeb.View.Transaction.Wharf.CusdecBOI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#dvScroll").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
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

        function scrollTop() {
            window.document.body.scrollTop = 0;
            window.document.documentElement.scrollTop = 0;
        };

        function ConfirmClearForm() {
            var res = confirm("Do you want to clear?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function Cancel() {
            var selectedvalueOrd = confirm("Do you want to Cancel ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                console.log("here");
                return false;
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

        function reloadPage() {
            window.location.reload();
        }

        function CheckAllBOIReq(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdBOIRequests.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="panel panel-default marginLeftRight5 marginBottom0">
        <div class="panel-body">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <%--<ContentTemplate>--%>
            <asp:HiddenField ID="hdfClear" runat="server" />
            <asp:HiddenField ID="hdfNewRecord" runat="server" />

            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-6  buttonrow">
                    </div>
                    <div class="col-sm-6  buttonRow">
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnconproc" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnconproc_Click">
                                        <span class="glyphicon glyphicon-blackboard" aria-hidden="true" ></span>Pro/Code
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnboiprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnboiprint_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true" ></span>Print
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtncusdeccancel" runat="server" OnClientClick="return Cancel();" OnClick="lbtncusdeccancel_Click" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" ></span>Cancel
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12 marginBottom0">
                    <div class="panel panel-default marginBottom0">
                        <div class="panel-heading">
                            <asp:Label ID="lblHeaderName" runat="server" Text="To-bond Entry"> </asp:Label>
                            <asp:Panel ID="pnlTobondNo" runat="server" Visible="false">
                                - [
                                <asp:Label ID="lblTobondNo" ForeColor="Blue" runat="server" Text="B20154789"></asp:Label>
                                ]
                            </asp:Panel>
                        </div>
                        <div class="panel-body marginBottom0">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label ID="lblBLNo" runat="server" Text="Customer Code"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingRight5">
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label ID="lblDocNo" runat="server" Text="To-bond Entry"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3 paddingRight0">
                                                            Date
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-4 paddingRight0">
                                                            <div class="col-sm-10 padding0">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtBLno" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBLno_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingRight0 paddingLeft5">
                                                                <asp:LinkButton ID="btnBLSearch" runat="server" CausesValidation="false" OnClick="btnBLSearch_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingRight5">
                                                            <asp:LinkButton ID="btnBOIRequest" runat="server" CausesValidation="false" OnClick="btnBOIRequest_Click" ToolTip="BOI Request(s)">
                                                                <span class="glyphicon glyphicon-th-list" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <div class="col-sm-10 padding0">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtRefno" CausesValidation="false" CssClass="form-control" OnTextChanged="txtRefno_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingRight0 paddingLeft5">
                                                                <asp:LinkButton ID="btnRefSearch" runat="server" CausesValidation="false" OnClick="btnRefSearch_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 paddingRight0">
                                                            <div class="col-sm-9 padding0">
                                                                <asp:TextBox ID="txtDocDate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3 paddingRight0 paddingLeft5">
                                                                <asp:LinkButton ID="btnDocDate" runat="server" CausesValidation="false">
                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDocDate"
                                                                    PopupButtonID="btnDocDate" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-9">
                                                            Description
                                                        </div>
                                                        <div class="col-sm-3">
                                                            Invoice No
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" AutoPostBack="true" ID="txtDesc" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox runat="server" AutoPostBack="true" ID="txtReference" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-7 paddingLeft0">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    Procedure Code
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    Declaration
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="col-sm-9 padding0">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtProceCode" CausesValidation="false" CssClass="form-control" OnTextChanged="txtProceCode_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft5 paddingLeft0">
                                                                        <asp:LinkButton ID="btnProceSearch" runat="server" CausesValidation="false" OnClick="btnProceSearch_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="col-sm-4 padding0">
                                                                        <asp:TextBox runat="server" ID="txtDec1" CausesValidation="false" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <asp:TextBox runat="server" ID="txtDec2" CausesValidation="false" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <asp:TextBox runat="server" ID="txtDec3" CausesValidation="false" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    Cusdec Entry Date
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    Cusdec Entry No
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="col-sm-9 padding0">
                                                                        <asp:TextBox ID="txtDocRecDate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft5">
                                                                        <asp:LinkButton ID="btnDocRecDate" runat="server" CausesValidation="false">
                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDocRecDate"
                                                                            PopupButtonID="btnDocRecDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" ID="txtCusdecEntryNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-3 padding0">
                                                                        TIN
                                                                    </div>
                                                                    <div class="col-sm-9 padding0">
                                                                        Consignee
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-3 padding0">
                                                                        <asp:TextBox ID="txtConsigneeTin" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-9 padding0">
                                                                        <div class="col-sm-3 padding0">
                                                                            <asp:TextBox ID="txtConsigneeCode" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-9 padding0">
                                                                            <asp:TextBox ID="txtConsigneeName" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <asp:TextBox ID="txtConsigneeAddress" runat="server" TextMode="MultiLine" CssClass="form-control" />
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
                            <div class="row marginBottom0">
                                <div class="panel-body marginBottom0">
                                    <div class="col-sm-12 marginBottom0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <ul id="myTab" class="nav nav-tabs">
                                                    <li class="active">
                                                        <a href="#CustHdr" data-toggle="tab"><b>Cusdec Header</b></a>
                                                    </li>
                                                    <li>
                                                        <a href="#CustItems" data-toggle="tab"><b>Cusdec Details</b></a>
                                                    </li>
                                                    <li>
                                                        <a href="#Reservations" data-toggle="tab"><b>Entry Requests</b></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div id="myTabContent2" class="tab-content">
                                                    <div class="tab-pane fade in active" id="CustHdr">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row marginBottom0">
                                                                    <div class="col-sm-12 marginBottom0">
                                                                        <div class="panel panel-default marginBottom0">
                                                                            <div class="panel-body marginBottom0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                Exporter
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                <div class="col-sm-4 paddingRight0">
                                                                                                    TIN
                                                                                                </div>
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox runat="server" ID="txtExporterTin" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox runat="server" ID="txtExporterCode" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-10 padding0">
                                                                                                <asp:TextBox runat="server" ID="txtExporterName" CssClass="form-control" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <asp:TextBox runat="server" ID="txtExporterAddress" CssClass="form-control" TextMode="MultiLine" />
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                Declarant / Representative
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                <div class="col-sm-4 paddingRight0">
                                                                                                    TIN
                                                                                                </div>
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox runat="server" ID="txtDeclarantTin" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox runat="server" ID="txtDeclarantCode" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-10 padding0">
                                                                                                <asp:TextBox runat="server" ID="txtDeclarantName" CssClass="form-control" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <asp:TextBox runat="server" ID="txtDeclarantAddress" CssClass="form-control" TextMode="MultiLine" />
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-3 padding0">
                                                                                                Vessel/Flight
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                Place of Loading
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Office of Entry / In
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Office of Entry / Exit
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Location of Goods
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <asp:TextBox ID="txtVessleNo" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <asp:TextBox ID="txtPlaceOfLoading" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:DropDownList ID="ddlOfficeOfEntryIn" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:DropDownList ID="ddlOfficeOfEntry" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:DropDownList AppendDataBoundItems="true" ID="ddlLocOfGoods" runat="server" CssClass="form-control">
                                                                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                                                    <asp:ListItem Value="1" Text="OTHER"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                Voyage No / Date
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                Mode of Trans. at Border
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <asp:TextBox ID="txtVoyageNo" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <div class="col-sm-9 padding0">
                                                                                                    <asp:TextBox ID="txtVoyageDate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-3 paddingLeft5">
                                                                                                    <asp:LinkButton ID="btnVoyageDate" runat="server" CausesValidation="false">
		                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtVoyageDate"
                                                                                                        PopupButtonID="btnVoyageDate" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <asp:TextBox ID="txtModeOfTransAtBorder" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                Container FCL
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                Carrying Type
                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                <asp:TextBox ID="txtContainerFCL" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                <asp:TextBox ID="txtFCL" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <asp:GridView ID="dgvContainers" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Container Type">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblIbc_tp" runat="server" Text='<%# Bind("Ibc_tp") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Container No">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblIbc_desc" runat="server" Text='<%# Bind("Ibc_desc") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblIbc_act" runat="server" Text='<%# Bind("Ibc_act") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="btnContaiDelete" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="return ConfirmDelConta()">
                                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-7 paddingRight0">
                                                                                        <div class="col-sm-12 padding0 height20">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    File No
                                                                                                </div>

                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Lists
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Items
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Total Packages
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                </div>
                                                                                                <div class="col-sm-5 padding0">
                                                                                                    Declarant's Seq. No
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtFileNo" runat="server" CssClass="form-control" />
                                                                                                </div>

                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtLists" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtItems" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtTotalPackages" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:DropDownList ID="ddlTotalPkgUOM" class="form-control" runat="server" AppendDataBoundItems="true">
                                                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                                <div class="col-sm-5 padding0">
                                                                                                    <asp:TextBox ID="txtDecSeqNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                Person Responsible for Financial Settlement
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                Identification of Warehouse & Period
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <asp:TextBox ID="txtFinSettlement" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <asp:TextBox ID="txtWarehouseID" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-3 padding0">
                                                                                                City of Last Consi. / First Dest.
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                Trading Country
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                Marks & Numbers
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Natu. of Transt.
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtLastConsiName" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-1 padding0">
                                                                                                <asp:TextBox ID="txtLastConsi" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtTradingCountryName" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-1 padding0">
                                                                                                <asp:TextBox ID="txtTradingCountry" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <asp:TextBox ID="txtMarksNumbers" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtNatuOfTranst" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    Country of Export
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    Country of Destination
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    Country of Origin
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox ID="txtCrtyExport" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtCrtyExportCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox ID="txtCrtyDesti" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtCrtyDestiCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox ID="txtCrtyOrigin" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtCrtyOriginCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-3 padding0">
                                                                                                Delivery Terms
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <b>Currency</b>
                                                                                                </div>
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <b>Invoice Amount</b>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <b>Exchange Rate</b>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                Value Details
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <asp:TextBox ID="txtDelTerms" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox ID="txtInvoiceAmt" runat="server" CssClass="form-control" Style="text-align: right" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtCurrencyOwn" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox ID="txtExRate" AutoPostBack="true" runat="server" CausesValidation="false" OnTextChanged="txtExRate_TextChanged" Font-Bold="true" ForeColor="Red" CssClass="form-control" Style="text-align: right" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0">
                                                                                                <asp:TextBox ID="txtValueDetails" runat="server" CssClass="form-control" Style="text-align: right" ReadOnly="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    Financing Ref. No
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    Terms of Payment
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Bank Code
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    Bank Name
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Branch
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtFinRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtTermsOfPayment" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtTermsOfPaymentCustom" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtBankCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Total Gross Mass (Kg)
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Total Net Mass (Kg)
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Main HS Code
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                A.C. Number
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Insurance Details
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Freight Details
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtTotGrossMass" runat="server" CssClass="form-control" />
                                                                                                <%--<asp:MaskedEditExtender ID="meTotGrossMass" runat="server"  TargetControlID ="txtTotGrossMass" MaskType="Number" Mask="999,999,999.9999" />--%>
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtTotNetMass" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtMainHS" runat="server" CssClass="form-control" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtAcNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>

                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtInsuranceInfor" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtFreightInfor" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                <div class="col-sm-12 padding0">
                                                                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                                                    <asp:UpdatePanel runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:GridView ID="dgvCostItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowEditing="dgvCostItems_RowEditing" OnRowUpdating="dgvCostItems_RowUpdating" OnRowCancelingEdit="dgvCostItems_RowCancelingEdit">
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField HeaderText='Category' DataField="cus_ele_cat" Visible="false" />
                                                                                                                    <asp:BoundField HeaderText='Type' DataField="Cus_ele_tp" Visible="false" />
                                                                                                                    <asp:TemplateField HeaderText="Element Code" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblCus_ele_cd" runat="server" Text='<%# Bind("Cus_ele_cd") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Element Code">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblCus_ele_cd_name" runat="server" Text='<%# Bind("Cus_ele_cd_name") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                                                        <EditItemTemplate>
                                                                                                                            <asp:TextBox ID="txtCus_amt" Style="text-align: right" onkeydown="return jsDecimals(event);" runat="server" Text='<%# Bind("Cus_amt","{0:#,0.00}") %>' MaxLength="20"></asp:TextBox>
                                                                                                                        </EditItemTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblCus_amt" runat="server" Text='<%# Bind("Cus_amt","{0:#,0.00}") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <div id="editbtndiv2" style="width: 1px">
                                                                                                                                <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                                                            <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </div>
                                                                                                                        </ItemTemplate>
                                                                                                                        <EditItemTemplate>
                                                                                                                            <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server" OnClick="OnUpdateCost">
                                                                                                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                            <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" runat="server" OnClick="OnCancelCost">
                                                                                                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </EditItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblCus_act" runat="server" Text='<%# Bind("Cus_act") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-1 padding0">
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="panel panel-default">
                                                                                                    <div class="panel-heading" id="divSearchHdr">
                                                                                                        Customs House Agent
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel-body">
                                                                                                    <div class="col-sm-12 padding0">
                                                                                                        <div class="col-sm-5 padding0">
                                                                                                            Authorized by
                                                                                                        </div>
                                                                                                        <div class="col-sm-7 padding0">
                                                                                                            <asp:TextBox ID="txtAuthorized" Text="A. SIRIWARDANA - DGM" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-12 padding0">
                                                                                                        <div class="col-sm-5 padding0">
                                                                                                            Submitted by
                                                                                                        </div>
                                                                                                        <div class="col-sm-7 padding0">
                                                                                                            <asp:TextBox ID="txtSubmitted" Text="S. DESHABANDU" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-12 padding0">
                                                                                                        <div class="col-sm-5 padding0">
                                                                                                            ID Number
                                                                                                        </div>
                                                                                                        <div class="col-sm-7 padding0">
                                                                                                            <asp:TextBox ID="txtIDNo" Text="497 - 08" runat="server" CssClass="form-control" ReadOnly="true" />
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
                                                    <div class="tab-pane fade" id="CustItems">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row marginBottom0">
                                                                    <div class="col-sm-12 marginBottom0">
                                                                        <div class="panel panel-default marginBottom0">
                                                                            <div class="panel-body">
                                                                                <div class="col-sm-12 padding0 marginBottom0">
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Marks & No of Packages
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Number & Kind
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    HS Code
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    Country of origin
                                                                                                    <asp:CheckBox ID="chkIgnoreCountry" Text="Force" runat="server" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Procedure Code
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtMarksAndNos" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtNoAndKind" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtHSCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtCtryOriginItem" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtProceCode1" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtProceCode2" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-1 padding0">
                                                                                                #
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                Item Code
                                                                                            </div>
                                                                                            <div class="col-sm-9 padding0">
                                                                                                Item Description
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-1 padding0">
                                                                                                <asp:TextBox ID="txtLineNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                            <div class="col-sm-9 padding0">
                                                                                                <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Gross Mass (Kg)
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Net Mass (Kg)
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Capacity
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-8 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    Previous Document / BL / AWB No
                                                                                                </div>
                                                                                                <div class="col-sm-2 padding0">
                                                                                                    Quota
                                                                                                </div>
                                                                                                <div class="col-sm-2 padding0">
                                                                                                    Preference
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtGrossMassItem" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtNetMassItem" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-8 padding0">
                                                                                                <div class="col-sm-8 padding0">
                                                                                                    <asp:TextBox ID="txtPreviousDocument" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-2 padding0">
                                                                                                    <asp:TextBox ID="txtQuota" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-2 padding0">
                                                                                                    <asp:TextBox ID="txtPreference" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-12 padding0">
                                                                                                        UOM & Qty 1
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-12 padding0">
                                                                                                        UOM & Qty 2
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-12 padding0">
                                                                                                        UOM & Qty 3
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    License No
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    Adjustment
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-4 padding0">
                                                                                                        <asp:TextBox ID="txtUOM1" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 padding0">
                                                                                                        <asp:TextBox ID="txtQty1" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-4 padding0">
                                                                                                        <asp:TextBox ID="txtUOM2" runat="server" CssClass="form-control" />
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 padding0">
                                                                                                        <asp:TextBox ID="txtQty2" runat="server" CssClass="form-control" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-4 padding0">
                                                                                                        <asp:TextBox ID="txtUOM3" runat="server" CssClass="form-control" />
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 padding0">
                                                                                                        <asp:TextBox ID="txtQty3" runat="server" CssClass="form-control" />
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtLicenceNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtAdjustment" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Value 1
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Item Price (FOB / CIF)
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    Value (NCY)
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Computer Charges
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    Unit Price
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtValue1" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtItemPrice" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <asp:TextBox ID="txtValueNCY" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtComChgAmount" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtrealunitprice" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtrealunitprice_TextChanged" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:TextBox ID="txtitemsqty" runat="server" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:Button ID="btnUpdateItem" Text="Update" runat="server" CausesValidation="false" OnClick="btnUpdateItem_Click" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-sm-12 padding0">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:Button ID="btnUpdateHS" Text="Update HS & Mass" runat="server" CausesValidation="false" OnClick="btnUpdateHS_Click" Visible="false" />
                                                                                                </div>
                                                                                                <div class="col-sm-9 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <div class="col-sm-5">
                                                                                                    <asp:CheckBox ID="chkCurrentDuty" Text="With Current Duty" runat="server" Visible="false" Enabled="false" />
                                                                                                </div>
                                                                                                <div class="col-sm-5">
                                                                                                    <asp:CheckBox ID="chkHSHistory" Text="HS History" runat="server" Checked="false" OnCheckedChanged="chkHSHistory_CheckedChanged" AutoPostBack="true" />
                                                                                                </div>
                                                                                                <div class="col-sm-2 padding0">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-12 padding0">
                                                                                            <asp:Panel ID="pnlDutyElements" runat="server">
                                                                                                <asp:GridView ID="dgvDutyElements" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="Type">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblDutyType" runat="server" Text='<%# Bind("Mhc_cost_ele") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="MP">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblDutyMP" runat="server" Text='<%# Bind("Mhc_mp") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Rate/Qty">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblDutyRate" runat="server" Text='<%# Bind("Tax_rate") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Tax Base/Unit Rate">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblTaxBase" runat="server" Text='<%# Bind("Tax_base","{0:#,0}")  %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Amount">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblTaxAmt" runat="server" Text='<%# Bind("Tax_amount","{0:#,0}") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlHSHistory" runat="server" Visible="false">
                                                                                                <asp:GridView ID="dgvHSHistory" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="HS Code">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblHSCode" runat="server" Text='<%# Bind("CUI_HS_CD") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Item Code">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("CUI_ITM_CD") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Document No">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblDocNo" runat="server" Text='<%# Bind("CUH_DOC_NO") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Document Date">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblDocDate" runat="server" Text='<%# Bind("CUH_DT") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <SelectedRowStyle BackColor="#ccffcc" />
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 padding0 marginBottom0">
                                                                                    <div class="panel panel-default padding0 marginBottom0">
                                                                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                                                        <div id="dvScroll" class="panel-body panelscollbar height200" onscroll="setScrollPosition(this.scrollTop);">
                                                                                            <asp:GridView ID="dgvItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                                OnRowDataBound="dgvItems_RowDataBound" OnSelectedIndexChanged="dgvItems_SelectedIndexChanged" EmptyDataText="No data found..." AutoGenerateColumns="False" AllowSorting="true" OnSorting="dgvItems_SortClick">
                                                                                                <Columns>
                                                                                                    <%--<asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <div style="margin-top: -3px">
                                                                                                                <asp:LinkButton ID="btnAddHS" runat="server" CausesValidation="false" OnClick="btnAddHS_Click">                         
                                                                                                                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>--%>
                                                                                                    <%--<asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText=""/>--%>
                                                                                                    <asp:TemplateField HeaderText="#" SortExpression="Cui_line">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_line" runat="server" Text='<%# Bind("Cui_line") %>' Visible="true"></asp:Label>
                                                                                                            <asp:LinkButton ID="btnCui_line" runat="server" OnClick="btnSelectItem_Click" Text='<%# Bind("Cui_line") %>' Visible="false"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Item Code" SortExpression="Cui_itm_cd">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_itm_cd" runat="server" Text='<%# Bind("Cui_itm_cd") %>' ToolTip='<%# Bind("Cui_itm_cd") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Model" SortExpression="Cui_model">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_model" runat="server" Text='<%# Bind("Cui_model") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="HS Code" SortExpression="Cui_hs_cd">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_hs_cd" runat="server" Text='<%# Bind("Cui_hs_cd") %>' Visible="false"></asp:Label>
                                                                                                            <asp:LinkButton ID="btnAddHS" runat="server" OnClick="btnAddHS_Click" Text='<%# Bind("Cui_hs_cd") %>' Visible="true"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Description" SortExpression="Cui_itm_desc">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_itm_desc" runat="server" Text='<%# Eval("Cui_itm_desc") %>' ToolTip='<%# Bind("Cui_itm_desc") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Tobond No">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="cui_oth_doc_no" runat="server" Text='<%# Eval("cui_oth_doc_no") %>' ToolTip='<%# Bind("cui_oth_doc_no") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Tobond Line">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="cui_oth_doc_line" runat="server" Text='<%# Eval("EntryLine") %>' ToolTip='<%# Bind("EntryLine") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Qty">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_qty" runat="server" Text='<%# Bind("Cui_qty","{0:#,0.00}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Unit Rate">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_unit_rt" runat="server" Text='<%# Bind("Cui_unit_rt","{0:#,0.00}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Value 1">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_unit_amt" runat="server" Text='<%# Bind("Cui_unit_amt","{0:#,0.00}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCui_orgin_cnty" runat="server" Text='<%# Bind("Cui_orgin_cnty") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <SelectedRowStyle BackColor="#ccffcc" />
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

                                                    <div class="tab-pane fade" id="Reservations">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-5 padding0">
                                                                    <div class="col-sm-12 padding0 GridScroll">
                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                            <asp:GridView ID="grdreservations" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <asp:TemplateField></asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Request Doc">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbresnp" runat="server" Text='<%# Bind("Cui_anal_2") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
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
        <%--</ContentTemplate>--%>
        <%--</asp:UpdatePanel>--%>
    </div>

    <%--Common Search >>--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpUserPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopup" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch" Style="display: none;">
                <div runat="server" id="test" class="panel panel-default height400 width700">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divHeader">
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
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2 labelText1">
                                        Search by word
                                    </div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
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
                                    <asp:GridView ID="grdResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                        <Columns>
                                            <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<< Common Search--%>

    <%--Customer Requests Search >>--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpBOIRequests" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlReqModal" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlReqModal" DefaultButton="lbtnSearch" Style="display: none;">
                <div runat="server" id="Div1" class="panel panel-default height400 width700">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divHeader">
                            <div class="row height20">
                                <div class="col-sm-6 paddingRight5">
                                    <asp:LinkButton ID="btnReqAdd" runat="server" OnClick="btnReqAdd_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true">Add</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-6 paddingLeft5">
                                    <asp:LinkButton class="pull-right" ID="btnReqClose" runat="server" OnClick="btnReqClose_Click">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
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
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panelscollbar" style="height: 250px">
                                        <asp:GridView ID="grdBOIRequests" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="itr_req_no" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllBOIReq(this)"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkReqNo" runat="server" Checked="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="itr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="itr_dt" runat="server" Text='<%# Bind("itr_dt", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tobond No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="itr_job_no" runat="server" Text='<%# Bind("itr_job_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SI No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="itr_anal2" runat="server" Text='<%# Bind("itr_anal2") %>'></asp:Label>
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popubprocsetup" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlprocsetup" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlprocsetup" DefaultButton="lbtnSearch" Style="display: none;">
                <div runat="server" id="Div3" class="panel panel-default height400 width700">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row height20">
                                <div class="col-sm-6 paddingRight5">
                                    <asp:LinkButton ID="lbtnprocsave" runat="server" OnClick="lbtnprocsave_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true">Save</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-6 paddingLeft5">
                                    <asp:LinkButton class="pull-right" ID="LinkButton3" runat="server" OnClick="btnReqClose_Click">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="col-sm-3 padding0">
                                        Supplier 
                                    </div>
                                    <div class="col-sm-6 padding0">
                                        <asp:TextBox runat="server"  ID="txtsupcode" CausesValidation="false" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 paddingLeft5 paddingLeft0">
                                        <asp:LinkButton ID="lbtnsupsearch" runat="server" CausesValidation="false" OnClick="lbtnsupsearch_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-1 paddingLeft5 paddingLeft0">
                                        <asp:LinkButton ID="lbtnaddprocdata" runat="server" CausesValidation="false" OnClick="lbtnaddprocdata_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-3 padding0">
                                        Proc/Code 
                                    </div>
                                    <div class="col-sm-6 padding0">
                                        <asp:TextBox runat="server"  ID="txtproccodenew" CausesValidation="false" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 paddingLeft5 paddingLeft0">
                                        <asp:LinkButton ID="lbtnproccodenew" runat="server" CausesValidation="false" OnClick="lbtnproccodenew_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-1 paddingLeft5 paddingLeft0">
                                        <asp:LinkButton ID="lbtnaddnewproc" runat="server" CausesValidation="false" OnClick="lbtnaddnewproc_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                </div>
                                <div class="col-sm-6 padding0">
                                    <asp:GridView ID="grdproccodes" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Proc/Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="mph_proc_cd" runat="server" Text='<%# Bind("mph_proc_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprocdelete" CausesValidation="false"  runat="server" OnClick="btnprocdelete_Click" >
                                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnaddcostele" CausesValidation="false" runat="server" OnClick="btnaddcostele_Click">
                                                                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-sm-2">
                                </div>
                                  <div class="col-sm-6 padding0">
                                    <asp:GridView ID="grdeleproc" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Proc/Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="procd" runat="server" Text='<%# Bind("procd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duty">
                                                <ItemTemplate>
                                                    <asp:Label ID="duty" runat="server" Text='<%# Bind("duty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="MP">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="mp" runat="server"  Checked='<%# subProgram((DataBinder.Eval(Container.DataItem, "mp") + "")) %>'></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Act">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="act" runat="server" Checked='<%# subProgram((DataBinder.Eval(Container.DataItem, "act") + "")) %>'></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<< Common Search--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="serPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="Panel1" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="Panel1" DefaultButton="lbtnSearchNew" Style="display: none;">
                <div runat="server" id="Div2" class="panel panel-default height350 width850">
                    <asp:Label ID="lblSerValue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divHdr">
                            <asp:LinkButton ID="lbtnClose" runat="server" OnClick="lbtnClose_Click">
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
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-1 labelText1">
                                                From
                                            </div>
                                            <div class="col-sm-8 padding0">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-5 paddingRight0">
                                                        <asp:TextBox ID="txtSeFromDate" runat="server" class="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnDWfrom" runat="server" CausesValidation="false">
                                                              <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="cldDWfrom" runat="server" TargetControlID="txtSeFromDate"
                                                            PopupButtonID="lbtnDWfrom" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-1 labelText1">
                                                To
                                            </div>
                                            <div class="col-sm-8 padding0">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-5 paddingRight0">
                                                        <asp:TextBox ID="txtSerToDate" runat="server" class="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft3">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false">
                                                              <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSerToDate"
                                                            PopupButtonID="LinkButton2" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnSeDateRange" runat="server" OnClick="lbtnSeDateRange_Click" CausesValidation="false">
                                                              <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Search by key
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:DropDownList ID="ddlSearchby" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Search by word
                                            </div>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox ID="txtSearchbywordNew" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordNew_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSearchNew" runat="server" OnClick="lbtnSearchNew_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                <div class="col-sm-12" style="height: 250px;">
                                    <asp:GridView ID="dgvSearch" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager"
                                        OnSelectedIndexChanged="dgvSearch_SelectedIndexChanged" OnPageIndexChanging="dgvSearch_PageIndexChanging">
                                        <Columns>
                                            <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
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
        <div runat="server" id="Div23" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy21" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
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
                                <asp:Button ID="btnalertYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnalertYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnalertNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnalertNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
</asp:Content>
