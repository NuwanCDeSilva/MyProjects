<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="InterCompanyInWardEntry.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.InterCompanyInWardEntry" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

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

        function ConfirmApprove() {
            var selectedvaldelitm = confirm("Do you want to approve ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtapproveconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapproveconfirm.ClientID %>').value = "No";
            }
        };

        function ConfirmReject() {
            var selectedvaldelitm = confirm("Do you want to reject ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtrejectconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtrejectconfirm.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvaldelitm = confirm("Do you want to cancel ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtcancenconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancenconfirm.ClientID %>').value = "No";
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

        function conDelSerials() {
            var result = confirm("Do you want to delete this serial?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfSend() {
            var result = confirm("Are you sure do you want to send ?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfClear() {
            var result = confirm("Do you want to clear data");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
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
        function ConfirmPrint() {
            var selectedvalueOrdPlace = confirm("Do you want to print Doument ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnprint.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnprint.ClientID %>').value = "No";
            }
        };
        function Enable() {
            return;
        }

    </script>
    <script>
        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
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
        .panel {
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnprint" runat="server" />
            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />
            <asp:HiddenField ID="txtapproveconfirm" runat="server" />
            <asp:HiddenField ID="txtcancenconfirm" runat="server" />
            <asp:HiddenField ID="txtrejectconfirm" runat="server" />
            <asp:HiddenField ID="txtpdasend" runat="server" />
            <asp:HiddenField ID="hfScrollPosition"  Value="0" runat="server" />
            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-6">

                        <div class="col-sm-2">
                            <strong>Send to PDA</strong>
                        </div>

                        <div class="col-sm-1">
                            <asp:CheckBox runat="server" AutoPostBack="true" ID="chkpda" OnCheckedChanged="chkpda_CheckedChanged" Enabled="false" />
                        </div>
                        <div class="col-sm-9">
                            <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                <div class="col-sm-11">
                                    <strong>Info!</strong>
                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                    <asp:Label ID="lblH1" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 buttonRow">


                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnPrint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                            <asp:LinkButton ID="lbtnprintserial" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnprintserial_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Serial
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="LinkButton1_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                            <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click1">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save/Process
                            </asp:LinkButton>

                        </div>
                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-1 paddingRight15" style="visibility: hidden">
                            <div class="dropdown">
                                <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                </a>
                                <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                    <div class="row">
                                        <div class="col-sm-12 inwardentrymakercheckerbuttons">

                                            <div class="col-sm-12 paddingRight0 AODReceiptAppBtn">
                                                <asp:LinkButton ID="lbtnapprove" CausesValidation="false" runat="server" OnClientClick="ConfirmApprove();" OnClick="lbtnapprove_Click">
                                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true" style="font-size:20px"></span>Approve
                                                </asp:LinkButton>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnreject" CausesValidation="false" runat="server" OnClientClick="ConfirmReject();" OnClick="lbtnreject_Click" Visible="false">
                                                        <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true" style="font-size:20px"></span>Reject
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ConfirmCancel();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size:20px"></span>Cancel
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

                <div class="row">

                    <div class="panel-body" style="padding-bottom:1px; padding-top:1px;">

                        <div class="col-sm-12">

                            <div class="panel panel-default" id="dvContentsOrder">

                                <div class="panel-heading pannelheading " style="padding-bottom:1px; padding-top:1px;">
                                   <strong> <b>Stock Transfer- Inward </b></strong>
                                </div>

                                <div class="panel-body" id="panelbodydiv" style="padding-bottom:2px; padding-top:5px;">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3 padding0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-3 labelText1">
                                                        From
                                                    </div>
                                                    <div class="col-sm-7 paddingRight0">
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFrom"
                                                            PopupButtonID="lbtnfromdate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                    <div class="col-sm-1 paddingLeft3" id="caldv">
                                                        <asp:LinkButton ID="lbtnfromdate" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-3 labelText1">
                                                        TO
                                                    </div>
                                                    <div class="col-sm-7 paddingRight0">
                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                            PopupButtonID="lbtntodate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                    <div class="col-sm-1 paddingLeft3" id="caldv">
                                                        <asp:LinkButton ID="lbtntodate" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <div class="col-sm-2 labelText1">
                                                    Type
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="ddlType" runat="server" TabIndex="3" AutoPostBack="true" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-2 padding0 labelText1">
                                                    Outward No
                                                </div>

                                                <div class="col-sm-2 labelText1 padding0">
                                                    <div id="tempchk" class="col-sm-1">
                                                        <asp:CheckBox runat="server" ID="chktemp" Text="" />
                                                    </div>
                                                    <div class="col-sm-6 paddingLeft3 paddingRight0">
                                                        <asp:Label Text="Temp" runat="server" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-7 paddingRight5">
                                                    <asp:TextBox ID="txtAODNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnsearchrec" runat="server" TabIndex="4" OnClick="lbtnsearchrec_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <div class="col-sm-4 labelText1">
                                                Location
                                            </div>

                                            <div class="col-sm-6 paddingRight5">
                                                <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnlocsearch" runat="server" TabIndex="5" OnClick="lbtnlocsearch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-1 padding0">
                                                <div class="col-sm-12 buttonRow" >
                                                    <asp:LinkButton ID="lbtnbtnDocSearch" runat="server" TabIndex="6" CausesValidation="false" OnClick="lbtnbtnDocSearch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>Search
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

                <div class="row">

                    <div class="panel-body" style="padding-bottom:1px; padding-top:1px;">

                        <div class="col-sm-9" style="padding-right:1px;">

                            <div class="panel panel-default " id="dvContentsOrder1">

                                <div class="panel-heading " style="padding-bottom:1px; padding-top:1px;">

                                    <div class="row  ">
                                        <div class="col-sm-12  ">
                                            <div class="col-sm-2 paddingLeft5">
                                                <strong>Pending Entry Details </strong>
                                            </div>
                                            <div class="col-sm-3 paddingLeft0 paddingRight0 labelText1">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-1 padding0">
                                                            <asp:CheckBox ID="chkAODoutserials" AutoPostBack="true" OnCheckedChanged="chkAODoutserials_CheckedChanged" runat="server" Width="1px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-10 padding3">
                                                            Get AOD Out Serials
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 labelText1 padding0">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-1 padding0 ">
                                                        <asp:CheckBox AutoPostBack="true" ID="chkPendingDoc" OnCheckedChanged="chkPendingDoc_CheckedChanged" Text="" runat="server" />
                                                    </div>
                                                    <div class="col-sm-10 padding3 ">
                                                        <asp:Label Text="PDA Completed" runat="server" ID="lblAllPendin" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 labelText1 padding0">
                                                <div class="col-sm-12 labelText1 padding0">
                                                    <div class="col-sm-6 padding0">
                                                        <asp:LinkButton ID="lbtnAllToPda" runat="server" TabIndex="6" CausesValidation="false" OnClick="lbtnAllToPda_Click">
                                                        Send All To PDA
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:LinkButton ID="lbtnPdaPartial" runat="server" TabIndex="6" CausesValidation="false" OnClick="lbtnPdaPartial_Click">
                                                        PDA partially send
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body" id="panelbodydiv1" style="padding-bottom:1px; padding-top:1px;">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="dvScroll" style="overflow-y:scroll; height:135px;" onscroll="setScrollPosition(this.scrollTop);">

                                                <asp:GridView ID="gvPending" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="gvPending_RowDataBound" OnSelectedIndexChanged="gvPending_SelectedIndexChanged" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltypepending" runat="server" Text='<%# Bind("ith_doc_tp") %>' Width="100%"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px" />
                                                            <HeaderStyle Width="30px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Outward No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbloutwrdnopending" runat="server" Text='<%# Bind("ith_doc_no") %>' Width="100%"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldatepending" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="100%"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                            <HeaderStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrefnopending" runat="server" Text='<%# Bind("ith_manual_ref") %>' Width="100%"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Issued Company">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissecompending" runat="server" Width="100%" Text='<%# Bind("ith_com") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                            <HeaderStyle Width="70px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Issued Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissuelocpending" runat="server" Width="100%" Text='<%# Bind("ith_loc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                            <HeaderStyle Width="70px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Other Doc No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblotherdocpendng" runat="server" Width="100%" Text='<%# Bind("ith_oth_docno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" />
                                                            <HeaderStyle Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supplier" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsupplerpending" runat="server" Text='<%# Bind("ith_bus_entity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sub Doc" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsubdocpending" runat="server" Text='<%# Bind("ith_sub_docno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created By">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcreatedbypending" Width="100%" runat="server" Text='<%# Bind("SE_USR_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Job No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljobnopending" Width="100%" runat="server" Text='<%# Bind("ITH_JOB_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIth_entry_no" runat="server" Text='<%# Bind("Ith_entry_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vehicle" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTMP_Tuh_fin_stus" runat="server" Text='<%# Bind("TMP_Tuh_fin_stus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="wh_com" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwh_com" runat="server" Text='<%# Bind("wh_com") %>'></asp:Label>
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

                        <div class="col-sm-3" style="padding-left:1px;">

                            <div class="panel panel-default " id="dvContentsOrder1a">

                                <div class="panel-heading pannelheading " style="height:27px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-8 padding0">
                                            Selected Entry Details
                                        </div>
                                        <div class="col-sm-4 padding0">
                                            <asp:LinkButton Visible="true" ID="lbtnSendToScan" OnClick="btnSendToScan_Click" CausesValidation="false" runat="server">
                                                <span class="glyphicon" aria-hidden="true"  ></span>Send to scan
                                            </asp:LinkButton>
<%--                                            <asp:Button Text="Send to scan" ID="btnSendToScan" runat="server" OnClick=""  />--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body" id="panelbodydiv1a" style="height:140px;">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-4 paddingLeft0 labelText1">
                                                Issued Company :
                                            </div>
                                            <div class="col-sm-8 stockinwardlabels">
                                                <asp:Label ID="lblIssedCompany" CssClass="labelText1" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 ">
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-4 paddingLeft0 labelText1">
                                                Issued Location :
                                            </div>
                                            <div class="col-sm-8 stockinwardlabels">
                                                <asp:Label ID="lblIssuedLocation" CssClass="labelText1" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 ">
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                            </div>
                                            <div class="col-sm-8 stockinwardlabels">
                                                <asp:Label ID="lblIssueLocDesc" CssClass="labelText1" ForeColor="Green" Font-Bold="true" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 ">
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-4 paddingLeft0 labelText1">
                                                Outward No :
                                            </div>
                                            <div class="col-sm-8 stockinwardlabels paddingRight0">
                                                <asp:Label ID="lblIssuedDocNo" CssClass="labelText1" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="col-sm-4 paddingLeft0 labelText1">
                                                Outward Qty :
                                            </div>
                                            <div class="col-sm-8 stockinwardlabels paddingRight0">
                                                <asp:Label ID="lblOutQty" CssClass="labelText1" Text="" runat="server"></asp:Label>
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

                            <div class="panel panel-default " id="dvContentsOrder1ss">

                                <div class="panel-heading pannelheading ">
                                </div>

                                <div class="panel-body" id="panelbodydiv1ss" style="padding-bottom:1px; padding-top:5px;">

                                    <div class="col-sm-2">
                                        <div class="row">

                                            <div class="col-sm-3 paddingRight0 labelText1">
                                                Date
                                            </div>

                                            <div>
                                                <div class="col-sm-6 paddingRight0">
                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                                        PopupButtonID="lbtndate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldvd" class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtndate" TabIndex="7" CausesValidation="false" runat="server" Visible="false">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Vehicle No
                                            </div>

                                            <div class="col-sm-8 padding0">
                                                <asp:TextBox ID="txtVehicle" runat="server" TabIndex="8" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Remarks
                                            </div>

                                            <div class="col-sm-10 paddingRight5">
                                                <asp:TextBox ID="txtRemarks" runat="server" TabIndex="9" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="row">

                                            <div class="col-sm-3 padding0 labelText1">
                                                Reference #
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtManRefNo" MaxLength="99"  runat="server" TabIndex="9" CssClass="form-control"></asp:TextBox>
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

                <div class="row">

                    <div class="panel-body" style="padding-bottom:1px; padding-top:5px;">

                        <div class="col-sm-12">

                            <div class="panel panel-default " id="dvContentsOrder1sds">

                                <div class="panel panel-heading"  style="padding-bottom:1px; padding-top:1px; height:20px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <strong>Item Details</strong>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbtnSerVar" ToolTip="Sub serial verify..." CausesValidation="false" runat="server" OnClick="lbtnSerVar_Click">
                                                         <span class="glyphicon " aria-hidden="true" style="font-size:15px"></span> Serial Verification
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="col-sm-6">
                                                <asp:Label Text="Document Qty :" ID="lblDocQty" runat="server" />
                                            </div>
                                             <div class="col-sm-6">
                                                 <asp:Label Text="Serial Pick Qty :" ID="lblDocScanQty" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body" id="panelbodydivd1ss" style="padding-left:1px; padding-right:1px; padding-top:1px; padding-bottom:1px;">

                                    <div class="col-sm-12">

                                        <div class="row">

                                            <div class="col-sm-12">

                                                <ul id="myTab" class="nav nav-tabs">

                                                    <li class="active">
                                                        <a href="#Item" data-toggle="tab">Item</a>
                                                    </li>
                                                    <li>
                                                        <a href="#Serial" data-toggle="tab">Serial</a>
                                                    </li>
                                                    <div class="col-sm-7 labelText1 padding0">
                                                        </div>
                                                    
                                                                    <div class="col-sm-3 labelText1 padding0">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <strong>Document Qty :</strong>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <asp:Label Text="" ID="lblDocQty1" runat="server" />
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

                                        <div id="myTabContent" class="tab-content">

                                            <div class="tab-pane fade in active" id="Item">
                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row ">
                                                            <div class="col-sm-12 ">

                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>

                                                                <div class="" style="height:120px; overflow-y:scroll">

                                                                    <asp:GridView ID="gvItem" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitem" runat="server" Text='<%# Bind("Tus_itm_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("Tus_itm_desc") %>' Width="300px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="200px" />
                                                                                <HeaderStyle Width="200px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("Tus_itm_model") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStsDes" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Tus_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="80px" CssClass="gridHeaderAlignRight"/>
                                                                                <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight"/>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                   <div style="margin-top:-3px;">
                                                                                        <asp:LinkButton ID="btnAddSerials" ToolTip="Add Serials....." Width="20px" CausesValidation="false" runat="server" OnClick="btnAddSerials_Click" Visible="false">
                                                                                        <span class="glyphicon glyphicon-paperclip" aria-hidden="true" style="font-size:15px"></span> 
                                                                                    </asp:LinkButton>
                                                                                   </div>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="20px" />
                                                                                <HeaderStyle Width="20px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="tab-pane fade" id="Serial">
                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">

                                                            <div class="col-sm-12 ">
                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>

                                                                <div class="" style="height:120px; overflow-y:scroll">

                                                                    <asp:GridView ID="gvSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnViewSerial" Width="20px" runat="server" CausesValidation="false"
                                                                                        OnClick="lbtnViewSerial_Click">
                                                                                                    <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20px" />
                                                                                <HeaderStyle Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton Width="20px" ID="lbtndeleteserial" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtndeleteserial_Click">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="20px" />
                                                                                <HeaderStyle Width="20px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Bin">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbinser" runat="server" Text='<%# Bind("Tus_bin") %>' Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitemser" runat="server" Text='<%# Bind("Tus_itm_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodelser" runat="server" Text='<%# Bind("Tus_itm_model") %>' Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatusserDesc" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="60px" />
                                                                                <HeaderStyle Width="60px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatusser" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="60px" />
                                                                                <HeaderStyle Width="60px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqtyser" runat="server" Width="60px" Text='<%# Bind("Tus_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="60px" CssClass="gridHeaderAlignRight"/>
                                                                                <HeaderStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTemp" runat="server" Width="10px" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="10px" />
                                                                                <HeaderStyle Width="10px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 1">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblser1" runat="server" Text='<%# Bind("Tus_ser_1") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 2">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblser2" runat="server" Text='<%# Bind("Tus_ser_2") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 3">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblser3" runat="server" Text='<%# Bind("Tus_ser_3") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblserid" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbluserseqno" Width="80px" runat="server" Text='<%# Bind("Tus_usrseq_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
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

                        <div class="col-sm-12">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                            <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span> View item details (With serials)
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
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
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped"
                                             PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
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
    </asp:Panel>



    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width850">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body" style="padding-bottom:1px; padding-top:1px;">
                            <div class="col-sm-12" id="Div2" runat="server">
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
                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
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

                                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait2" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait2" runat="server"
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
                <div runat="server" id="Div4" class="panel panel-default height150 width525">
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
                                                    Document No :
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
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" AutoPostBack="true" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlloadingbay_SelectedIndexChanged"></asp:DropDownList>
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


    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerialPick" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPickSerial" runat="server" Enabled="True" TargetControlID="btnSerialPick"
                PopupControlID="pnlPickSerial" PopupDragHandleControlID="divPSPHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPickSerial" Style="display: none">
                <div runat="server" id="Div14" class="panel panel-default height400 width850">
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="divPSPHdr">
                            
                            <div class="col-sm-11">
                                <strong>Serial Picker</strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnPSPClose" runat="server" OnClick="btnPSPClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div15" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-8" id="Div16" runat="server">
                                                <div class="col-sm-4 padding0">
                                                    <div class="col-sm-5 padding0">
                                                        Serial 1
                                                    </div>
                                                    <div class="col-sm-7 padding0">
                                                        <asp:TextBox ID="txtSerial1Add" CausesValidation="false" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <div class="col-sm-5">
                                                        Serial 2
                                                    </div>
                                                    <div class="col-sm-7 padding0">
                                                        <asp:TextBox ID="txtSerial2Add" CausesValidation="false" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <asp:LinkButton ID="btnAdvanceAddItemNew" runat="server" OnClick="btnAdvanceAddItemNew_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <asp:Button ID="btnConfirm" Text="Confirm" runat="server" OnClick="btnConfirm_Click" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label ID="lblPopupItemCode" Text="" runat="server" Visible="false" />
                                    <asp:Label ID="lblInvoiceLine" Text="" runat="server" Visible="false" />
                                    <asp:Label ID="lblItemStatusSer" Text="" runat="server" Visible="false" />
                                    <asp:Label ID="lblItemStatus" Text="" runat="server" Visible="false" />
                                    <asp:Label ID="lblQty" Text="" runat="server" Visible="false" />
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtPopupQty" Text="0.00" CssClass="form-control" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblPopupQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblScanQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblApprovedQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-1" style="float: right">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panelscoll POPupResultspanelscroll" style="height: 300px;">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdAdSearch" AutoGenerateColumns="False" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                                        runat="server"
                                                        GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager"
                                                        OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged">
                                                        <Columns>
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
                                                            <asp:TemplateField HeaderText=" ">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelPickSer" runat="server" OnClientClick="return conDelSerials()" OnClick="btnDelPickSer_Click">
                                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnldel" DefaultButton="lbtnSearch">
        <div class="height350 width700">

            <div runat="server" id="Div3" class="panel panel-primary ">

                <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>

                <div class="panel panel-default">
                    <div class="panel-heading height30">

                        <div class="col-sm-11">
                            <strong>Item Details (with Serials)</strong>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="LinkButton13" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">

                        <asp:UpdatePanel ID="blkcancelpnlss" runat="server">
                            <ContentTemplate>

                                <div class="col-sm-12">

                                    <div class="row">

                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="" style="height: 277px; overflow-x: auto; overflow-y: hidden">

                                                    <asp:GridView ID="gventryserials" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitempopup" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmodelpopup" runat="server" Text='<%# Bind("tus_itm_model") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstatuspoup" runat="server" Text='<%# Bind("tus_itm_desc") %>' Width="200px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstuspopupDes" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstuspopup" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Serial 1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblser1popup" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Serial 2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblser2popup" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Serial 3">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblser3popup" runat="server" Text='<%# Bind("tus_ser_3") %>' Width="100px"></asp:Label>
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

    </asp:Panel>

    <asp:UpdatePanel ID="updatePanelDocument" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnDocument" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDocument" runat="server" Enabled="True" TargetControlID="btnDocument"
                PopupControlID="panelDocument" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divDocument" class="row">
        <div class="col-sm-12">
            <asp:Panel runat="server" ID="panelDocument">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <strong>Sub Serial Item Details</strong>
                            <asp:LinkButton ID="lbtPopDocClose" runat="server" OnClick="lbtPopDocClose_Click" Style="float: right;">
                             <span class="glyphicon glyphicon-remove" style="margin-left:400px" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                        <div class="panel panel-body" style="padding-top: 0px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvSubSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped"
                                        GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitempopup" runat="server" Text='<%# Bind("irsms_itm_cd") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmodelpopup" runat="server" Text='<%# Bind("irsms_sub_ser") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div5" class="panel panel-info height120 width250">
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

    <%-- Serial Varification --%>
    <asp:UpdatePanel ID="upSerVar" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnVar" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSerVar" runat="server" Enabled="True" TargetControlID="btnVar"
                PopupControlID="pnlSerVar" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSerVar">
        <div class="row" style="width: 900px;">
            <div class="panel panel-default">
                <div class="panel panel-heading height30">
                    <div class="col-sm-12 padding0">
                        <div class="col-sm-10">
                            <strong>Serial Verification</strong>
                        </div>
                        <div class="col-sm-1 padding0">
                            <asp:LinkButton ID="lbtnSerVarClear" runat="server" OnClick="lbtnSerVarClear_Click" OnClientClick="return ConfClear()">
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true">Clear</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="lbtnSerVarClose" runat="server" OnClick="lbtnSerVarClose_Click" Style="float: right;">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="panel panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="" Visible="false" ForeColor="Red" Font-Bold="true" ID="lblSerVarError" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-3 labelText1">
                                Main Item Serial 1
                            </div>
                            <asp:Panel runat="server" DefaultButton="lbtnSubSerLoad">
                                <div class="col-sm-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <%--  <asp:TextBox runat="server" AutoPostBack="true" onblur="doPostBack(this)" ID="txtMainItmSer1" CssClass="form-control"
                                                OnTextChanged="txtMainItmSer1_TextChanged"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" ID="txtMainItmSer1" CssClass="form-control"></asp:TextBox>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-4">
                                    <asp:LinkButton ID="lbtnSubSerLoad" CausesValidation="false" Style="display: none;" runat="server" OnClick="txtMainItmSer1_TextChanged">
                                    </asp:LinkButton>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel panel-body" style="height: 175px; overflow: auto;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvSubSerPick" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                EmptyDataText="No data found..." ShowHeaderWhenEmpty="true"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItmCode" runat="server" Text='<%# Bind("Irsms_itm_cd") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Serial">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubSerial" runat="server" Text='<%# Bind("Irsms_sub_ser") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPickSubSer" Checked='<%#Convert.ToBoolean(Eval("SubSerIsAvailable")) %>' runat="server" Enabled="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10px" />
                                                        <ItemStyle Width="10px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="cssPager"></PagerStyle>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <asp:Panel runat="server">
                                <div class="col-sm-4 labelText1">
                                    Sub Serial 
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox AutoPostBack="true" onblur="doPostBack(this)" OnTextChanged="txtSubSerial_TextChanged" runat="server" ID="txtSubSerial" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button ID="btnI" runat="server" OnClick="txtSubSerial_TextChanged" Text="Submit" Style="display: none;" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-body" style="height: 175px; overflow: auto;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvPopSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Bin">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbinser" runat="server" Text='<%# Bind("Tus_bin") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemser" runat="server" Text='<%# Bind("Tus_itm_cd") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Model">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmodelser" runat="server" Text='<%# Bind("Tus_itm_model") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStsDes" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatusser" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblqtyser" runat="server" Text='<%# Bind("Tus_qty","{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                        <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Serial 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblser1" runat="server" Text='<%# Bind("Tus_ser_1") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltemp" runat="server" Text='' Width="5px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Serial 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblser2" runat="server" Text='<%# Bind("Tus_ser_2") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Serial 3" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblser3" runat="server" Text='<%# Bind("Tus_ser_3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserid" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluserseqno" runat="server" Text='<%# Bind("Tus_usrseq_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Warranty #" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWar" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPickSer" Checked='<%#Convert.ToBoolean(Eval("Tus_ser_ver")) %>' runat="server" Enabled="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10px" />
                                                        <ItemStyle Width="10px" />
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

    <%--All Send to pda --%>
     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSendToPda" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlSendToPda" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel8">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait23" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait23" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="pnlSendToPda" runat="server" align="center">
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div>
                    <div class="panel panel-default">
                    <div class="panel panel-heading text-left height22">
                        <strong>Send Documents To PDA</strong>
                    </div>
                        <div class="panel panel-body" >
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-5">
                                        <div class="row" style="margin-top:3px;">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 labelText1">
                                                    Loading Bay
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlSendAllLoadingBay" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="2" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        </div>
                                    <div class="col-sm-3 padding0">
                                        <div class="row buttonRow">
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 padding0">
                                                    <asp:LinkButton ID="lbtnSendToPDA" runat="server" OnClientClick="return ConfSend();" OnClick="lbtnSendToPDA_Click">
                                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Send
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-6 padding0" style="width:55px;">
                                                    <asp:LinkButton ID="lbtnSPDaClose" runat="server" OnClick="lbtnSPDaClose_Click" Style="float: right;">
                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Close
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="height: 350px; overflow-y: scroll;">

                                        <asp:GridView ID="dgvPopPendingDoc" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped"
                                            GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkAllDocNo" Width="100%" AutoPostBack="true" OnCheckedChanged="chkAllDocNo_CheckedChanged"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkDocNo" Width="100%"></asp:CheckBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                    <HeaderStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltypepending" runat="server" Text='<%# Bind("ith_doc_tp") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                    <HeaderStyle Width="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Outward No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloutwrdnopending" runat="server" Text='<%# Bind("ith_doc_no") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="130px" />
                                                    <HeaderStyle Width="130px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldatepending" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefnopending" runat="server" Text='<%# Bind("ith_manual_ref") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="130px" />
                                                    <HeaderStyle Width="130px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issued Company">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblissecompending" runat="server" Width="100%" Text='<%# Bind("ith_com") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issued Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblissuelocpending" runat="server" Width="100%" Text='<%# Bind("ith_loc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Other Doc No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblotherdocpendng" runat="server" Width="100%" Text='<%# Bind("ith_oth_docno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Supplier" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsupplerpending" runat="server" Text='<%# Bind("ith_bus_entity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sub Doc" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsubdocpending" runat="server" Text='<%# Bind("ith_sub_docno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Created By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcreatedbypending" Width="100%" runat="server" Text='<%# Bind("SE_USR_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="130px" />
                                                    <HeaderStyle Width="130px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Job No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbljobnopending" Width="100%" runat="server" Text='<%# Bind("ITH_JOB_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIth_entry_no" runat="server" Text='<%# Bind("Ith_entry_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="Silver" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

     <%--All Send to pda partially --%>
     <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSendToPdaPart" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlSendToPdaPartial" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:Panel ID="pnlSendToPdaPartial" runat="server" align="center">
        <div class="col-sm-12" style="width:850px;">
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
            <ContentTemplate>
                <div class="col-sm-12">
                    <div class="panel panel-default">
                    <div class="panel panel-heading text-left height22">
                        <strong>Send Documents To PDA Partially</strong>
                    </div>
                        <div class="panel panel-body" >
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-8 padding0">
                                        <div class="row" style="margin-top:3px;">
                                            <div class="col-sm-6 padding0">
                                                <div class="col-sm-4 labelText1 ">
                                                    Document No
                                                </div>
                                                <div class="col-sm-8 padding0 labelText1">
                                                    <asp:Label ID="lblDocNoPart" Text="" Style="text-align:left" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6 paddingRight0">
                                                <div class="col-sm-4 labelText1">
                                                    Loading Bay
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlLoadBayPart" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="2" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 padding0">
                                        <div class="row buttonRow">
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 padding0">
                                                    <asp:LinkButton ID="lbtnParSend" runat="server" OnClientClick="return ConfSend();" OnClick="lbtnParSend_Click">
                                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Send
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-6 padding0" style="width:55px;">
                                                    <asp:LinkButton ID="lbtnCanPart" runat="server" OnClick="lbtnCanPart_Click" Style="float: right;">
                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Close
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="height: 350px; overflow-y: scroll;">
                                        <asp:GridView ID="dgvAodPartIn" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" 
                                            GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ROE No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTMP_ROW_NO" runat="server" Text='<%# Bind("TMP_ROW_NO") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTui_req_itm_cd" runat="server" Text='<%# Bind("Tui_req_itm_cd") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItmDesc" runat="server" Text=''  Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID ="lblItmModel" runat="server" Text='' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStusDesc" runat="server" Text='' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTui_req_itm_stus" runat="server" Text='<%# Bind("Tui_req_itm_stus") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ava Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTMP_ITM_APP_QTY" runat="server" Width="100%" Text='<%# Bind("TMP_ITM_APP_QTY","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                    <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bal Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTMP_ITM_BAL_QTY" runat="server" Width="100%" Text='<%# Bind("TMP_ITM_BAL_QTY","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                    <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pick Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTMP_ITM_PICK_QTY" runat="server" Width="100%" Text='<%# Bind("TMP_ITM_PICK_QTY","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtTMP_ITM_PICK_QTY" onkeypress="return isNumberKey(event)" Width="100%" runat="server" Text='<%# Bind("TMP_ITM_PICK_QTY","{0:N2}") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                    <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnPartQtyEdit" CausesValidation="false" runat="server" OnClick="lbtnPartQtyEdit_Click">
                                                             <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lbtnPartQtyUpdate" runat="server" CausesValidation="false" CommandName="Update" 
                                                            OnClick="lbtnPartQtyUpdate_Click">
                                                              <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="1%" />
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
    </asp:Panel>

    <%-- pop up partial save --%>
    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupPartialSave" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlPartialSave" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel20">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait423" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait423" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlPartialSave" runat="server" align="center">
        <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12 labelText1">
                                <asp:Label ID="lblPartalSave" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label12" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label13" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="lbtnSavePop" runat="server" Text="Yes" CausesValidation="false" class="btn btn-primary" OnClick="lbtnSavePop_Click" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="lbtnCancelPop" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="lbtnCancelPop_Click" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
