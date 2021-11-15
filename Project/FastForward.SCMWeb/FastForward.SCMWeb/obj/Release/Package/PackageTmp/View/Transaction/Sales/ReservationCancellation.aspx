<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ReservationCancellation.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Sales.ReservationCancellation" %>

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

        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdnClear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnClear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnSave.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnSave.ClientID %>').value = "No";
            }
        };

        function ConfirmUpdate() {
            var selectedvalueOrdPlace = confirm("Do you want to Update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "No";
            }
        };

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to Approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "No";
            }
        };
        function Enable() {
            return;
        }
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdReservation.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
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
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.divTar').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function scrollTop() {
            window.document.body.scrollTop = 0;
            window.document.documentElement.scrollTop = 0;
        };
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
            padding-bottom: 0px;
            padding-top: 0px;
            margin-bottom: 0px;
            margin-top: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="mainPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="mainPnl">
        <ContentTemplate>
            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
            <asp:HiddenField ID="hdnSave" runat="server" />
            <asp:HiddenField ID="hdnUpdate" runat="server" />
            <asp:HiddenField ID="hdnApprove" runat="server" />
            <asp:HiddenField ID="hdnCancel" runat="server" />
            <asp:HiddenField ID="hdnClear" runat="server" />
            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7  buttonrow">
                            <div id="divWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <strong>Warning!</strong>
                                <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnlWarning" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <strong>Success!</strong>
                                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnSuccess" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnAlert" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-4  buttonRow">
                            <div class="col-sm-3 paddingRight0">
                                <%--<asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>--%>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <%--<asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>--%>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnCancelAll" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancelAll_Click">
                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Cancel All
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancel_Click">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-1  buttonRow paddingLeft0">
                            <div class="col-sm-12 paddingLeft5">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClear()" OnClick="lbtnClear_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">Reservation Cancellation</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-11 padding0">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 padding0 labelText1 ">
                                                                    From 
                                                                </div>
                                                                <div class="col-sm-8 paddingRight0">
                                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                        Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 padding3">
                                                                    <asp:LinkButton ID="lbtnFromDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate"
                                                                        PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 labelText1">
                                                                    To
                                                                </div>
                                                                <div class="col-sm-8 paddingRight0">
                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 padding3">
                                                                    <asp:LinkButton ID="lbtnToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                                        PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-4 labelText1">
                                                                Request Reason
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <asp:DropDownList ID="ddlRequestReason" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRequestReason_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-5 labelText1">
                                                                Reservation Type
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <asp:DropDownList ID="ddlReservationType" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-3 labelText1">
                                                                Customer
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtCustomer" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnCustomer" runat="server" OnClick="lbtnCustomer_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-9">
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-3 labelText1">
                                                                Reservation
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtreservati" OnTextChanged="txtReservationNo_TextChanged" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtReservationNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                                <div class="col-sm-6">
                                                    <asp:LinkButton ID="lbtnSearchRequest" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnSearchRequest_Click">
                                                            <span class="glyphicon glyphicon-search search4" aria-hidden="true"></span>
                                                    </asp:LinkButton>
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
                                <div class="panel-heading height22">Reservation Details</div>
                                <div class="row">
                                    <div class="col-sm-12 ">
                                        <div class="panel-body padding0">
                                            <div class="divTar" style="overflow-y: scroll; height: 125px;" id="divTar" onscroll="setScrollPosition(this.scrollTop);">
                                                <asp:GridView ID="grdReservation" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <div style="margin-top: -2px;">
                                                                    <asp:LinkButton ID="lbtnAddRes" runat="server" CausesValidation="false" Width="10px" OnClick="lbtnAddRes_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--<asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10%" />--%>
                                                        <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkReservation" runat="server" AutoPostBack="true" onclick="CheckBoxCheck(this);" GroupName="Reservatio" OnCheckedChanged="chkReservation_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="SeqNo" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseqNo" runat="server" Text='<%# Bind("IRS_SEQ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reservation #" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblResNo" runat="server" Text='<%# Bind("IRS_RES_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="IRS_RES_NO" HeaderText="Reservation #" ReadOnly="true" />
                                                        <asp:BoundField DataField="IRS_RES_DT" HeaderText="Date" ReadOnly="true" DataFormatString="{0:dd/MMM/yyyy}" />
                                                        <asp:BoundField DataField="IRS_CUST_CD" HeaderText="Customer Code" ReadOnly="true" />
                                                        <asp:BoundField DataField="MBE_NAME" HeaderText="Customer" ReadOnly="true" />
                                                        <asp:BoundField DataField="RRS_DESC" HeaderText="Reservation Type" ReadOnly="true" />
                                                    </Columns>
                                                </asp:GridView>
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
                                <div class="panel-heading height22">
                                    <div class="col-sm-12">
                                        <div class="col-sm-9">
                                            Item Details
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="col-sm-5">
                                                Reservation #
                                            </div>
                                            <div class="col-sm-7" style="margin-top: -5px;">
                                                <asp:TextBox runat="server" id="txtReservNo" CssClass="form-control" Enabled="false"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body padding0">
                                            <div class="" style="overflow-y: scroll; height: 105px;">
                                                <asp:GridView ID="grdItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnSelectedIndexChanged="grdItem_SelectedIndexChanged">
                                                    <Columns>
                                                        <%--<asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10%" />--%>
                                                        <asp:TemplateField HeaderText="" Visible="true" ControlStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <div style="margin-top: -2px;">
                                                                    <asp:LinkButton ID="lbtnAddItem" runat="server" OnClick="lbtnAddItem_Click">
                                                            <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10px" />
                                                            <ItemStyle Width="10px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SeqNo" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseqNo" runat="server" Width="100%" Text='<%# Bind("IRL_SEQ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--                                                <asp:BoundField DataField="IRD_ITM_CD" HeaderText="Item Code" ReadOnly="true" />--%>
                                                        <asp:TemplateField HeaderText="Item Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRD_ITM_CD" Width="100%" runat="server" Text='<%# Bind("IRL_ITM_CD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItmDes" Width="100%" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="130px" />
                                                            <ItemStyle Width="130px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="MI_MODEL" HeaderText="Model" ReadOnly="true" />--%>
                                                        <asp:TemplateField HeaderText="Model" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMI_MODEL" Width="100%" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMIS_DESC" Width="100%" runat="server" Text='<%# Bind("IRL_STUS_DESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                            <ItemStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="MIS_DESC" HeaderText="Status" ReadOnly="true" />--%>
                                                        <asp:TemplateField HeaderText="Reserve QTY" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRD_RES_QTY" Width="100%" runat="server" Text='<%# Bind("IRL_RES_QTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="IRD_RES_QTY" HeaderText="Reserve QTY" ReadOnly="true" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />--%>
                                                        <asp:TemplateField HeaderText="Balance QTY" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRD_RES_BQTY" Width="100%" runat="server" Text='<%# Bind("IRL_RES_BQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="IRD_RES_BQTY" HeaderText="Balance QTY" ReadOnly="true" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />--%>
                                                        <asp:TemplateField HeaderText="Cancel QTY" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRD_RES_CQTY" Width="100%" runat="server" Text='<%# Bind("IRL_RES_IQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTemp" Width="100%" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="10px" />
                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="10px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="IRD_RES_CQTY" HeaderText="Cancel QTY" ReadOnly="true" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />--%>
                                                        <asp:TemplateField HeaderText="Request #" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRD_RESREQ_NO" Width="100%" runat="server" Text='<%# Bind("IRL_REQ_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="100px" />
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="IRD_RESREQ_NO" HeaderText="Request #" ReadOnly="true" />--%>
                                                    </Columns>
                                                </asp:GridView>
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
                                <div class="panel-heading height22">Item Log Details</div>
                                <div class="row" style="margin-top: 2px; margin-bottom: 2px;">
                                    <div class="panel-body padding0">
                                        <div class="col-sm-12">
                                            <div class="col-sm-11 padding0">
                                                <%-- <div class="col-sm-4 paddingRight0 labelText1">
                                                    Company 
                                                </div>
                                                <div class="col-sm-8 padding0 ">
                                                    <asp:TextBox ID="txtCom" Enabled="false" runat="server" CssClass="form-control" />--%>
                                                <asp:Label ID="lblSeq" Text="" runat="server" Visible="false" />
                                            </div>
                                            <%--</div>
                                            <div class="col-sm-2 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    Location 
                                                </div>
                                                <div class="col-sm-6 padding0 ">
                                                    <asp:TextBox ID="txtLoc" Enabled="false" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                             <div class="col-sm-2 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    Item Code 
                                                </div>
                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox ID="txtItm" Enabled="false" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                             <div class="col-sm-2 padding0">
                                                <div class="col-sm-5 paddingRight0 labelText1">
                                                     Reserve QTY 
                                                </div>
                                                <div class="col-sm-5 padding0 ">
                                                    <asp:TextBox ID="txtRes" Enabled="false" runat="server" CssClass="form-control text-right" />
                                                </div>
                                            </div>
                                             <div class="col-sm-2 padding0">
                                                <div class="col-sm-5 paddingRight0 labelText1">
                                                   Balance QTY
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <asp:TextBox ID="txtBalQty" Enabled="false"  runat="server" CssClass="form-control text-right" />
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <div class="col-sm-5 paddingRight0 labelText1">
                                                    Cancel QTY
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <asp:TextBox ID="txtCancelQty" class="form-control text-right" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 padding0"  style="margin-top:-3px;">
                                                    <asp:LinkButton ID="lbtnAddQty" runat="server" OnClick="lbtnAddQty_Click" >
                                                            <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" style="font-size:20px;"></span>
                                                                    </asp:LinkButton>
                                                </div>--%>
                                            <div class="col-sm-1 padding0">
                                                <asp:LinkButton ID="lbtnUpdtQty" runat="server" OnClick="lbtnUpdtQty_Click">
                                                            <span class="glyphicon glyphicon-saved" aria-hidden="true">Update</span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="panel-body padding0">
                                    <div class="col-sm-12 GridScroll120">
                                        <asp:GridView ID="grdItemLog" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <%-- <asp:BoundField DataField="IRL_ORIG_COM" HeaderText="Company" ReadOnly="true" />
                                                    <asp:BoundField DataField="IRL_ORIG_LOC" HeaderText="Location" ReadOnly="true" />
                                                    <asp:BoundField DataField="IRL_ITM_CD" HeaderText="Item Code" ReadOnly="true" />
                                                    <asp:BoundField DataField="IRL_RES_QTY" HeaderText="Reserve QTY" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="IRL_RES_BQTY" HeaderText="Balance QTY" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                   <%-- <asp:BoundField DataField="IRL_RES_IQTY" HeaderText="Cancel QTY" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="Cancel QTY" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label id="IRL_RES_IQTY"  runat="server"  Text='<%# (Convert.ToDecimal(Eval("IRL_RES_IQTY"))).ToString("N2") %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Company" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_CURT_COM" Width="100%" runat="server" Text='<%# Bind("IRL_CURT_COM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_CURT_LOC" Width="100%" runat="server" Text='<%# Bind("IRL_CURT_LOC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_ITM_CD" Width="100%" runat="server" Text='<%# Bind("IRL_ITM_CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle Width="80px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reserve QTY" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_RES_QTY" Width="100%" runat="server" Text='<%# Bind("IRL_RES_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance QTY" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_RES_BQTY" Width="100%" runat="server" Text='<%# Bind("IRL_RES_BQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cancel QTY" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:Label id="IRL_RES_IQTY"  runat="server"  Text='<%# (Convert.ToDecimal(Eval("IRL_RES_IQTY"))).ToString("N2") %>'/>--%>
                                                        <asp:TextBox ID="txtIrlResIQty" runat="server" AutoPostBack="true" Width="60px" BorderColor="White" Text='<%# (Convert.ToDecimal(Eval("IRL_RES_IQTY"))).ToString("N2") %>' onkeydown="return jsDecimals(event);" OnTextChanged="txtIrlResIQty_TextChanged" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cancel QTY" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_RES_IQTY" runat="server" Text='' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cur Doc No" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_CURT_DOC_NO" runat="server" Text='<%# Bind("IRL_CURT_DOC_NO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WIP" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IRL_RES_WP" runat="server" Text='<%# Bind("IRL_RES_WP") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="irl_line" Width="100%" runat="server" Text='<%# Bind("irl_line") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle Width="80px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <%-- Style="display: none"--%>
    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerial" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSerial" runat="server" Enabled="True" TargetControlID="btnSerial"
                PopupControlID="pnlSerial" CancelControlID="lbtnSerialClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlSerial" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-primary Mheight3">
                    <asp:Label ID="lblSerialvalue" runat="server" Text="" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnSerialClose" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-7 paddingRight0">
                                    <div runat="server" id="Div2" class="panel panel-primary Mheight">
                                        <%--<asp:Label ID="lblvalue1" runat="server" Text="Label" Visible="false"></asp:Label>--%>
                                        <div class="panel panel-default">
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
                                                            <asp:DropDownList ID="ddlSearchbykey1" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            Search by word
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:TextBox ID="txtSearchbyword1" CausesValidation="true" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnSearch1" runat="server" OnClick="lbtnSearch_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                                        <asp:GridView ID="grdResult1" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                                                            <Columns>
                                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="100" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grdSerial" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped"
                                                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>
                                                            <asp:BoundField DataField="ITRS_SER_1" HeaderText="Serial 1" ReadOnly="true" />
                                                            <asp:BoundField DataField="ITRS_SER_2" HeaderText="Serial 2" ReadOnly="true" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnSerialDalete" runat="server" CausesValidation="false" OnClick="lbtnSerialDalete_Click">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnStock" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpopStock" runat="server" Enabled="True" TargetControlID="btnStock"
                PopupControlID="pnlStock" CancelControlID="lbtnStock" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlStock" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-primary Mheight">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnStock" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
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
                                    <asp:GridView ID="grdStock" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged"
                                        AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" AutoGenerateColumns="false"
                                        CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                                        <Columns>
                                            <asp:BoundField DataField="INL_COM" HeaderText="COM" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_LOC" HeaderText="LOC" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_ITM_CD" HeaderText="ITEM" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_ITM_STUS" HeaderText="STATUS" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_QTY" HeaderText="QTY" ReadOnly="true" />
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
    <script>
        Sys.Application.add_load(func);
        function func() {
            $(document).ready(function () {
                // console.log('redy doc');
                maintainScrollPosition();
            });
        }
    </script>
</asp:Content>
