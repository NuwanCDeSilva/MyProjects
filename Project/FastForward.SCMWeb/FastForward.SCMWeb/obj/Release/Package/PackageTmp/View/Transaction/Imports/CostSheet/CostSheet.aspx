<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CostSheet.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Imports.CostSheet.CostSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="../../Js/jquery-1.7.2.min.js"></script>--%>
    <script type="text/javascript">
        function jsDecimals(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (key == 109 || key == 173) {

                    } else {
                        if (!jsIsUserFriendlyChar(key, "Decimals")) {
                            return false;
                        }
                    }

                } else {
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
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtConfirmSave.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtConfirmSave.ClientID %>').value = "No";
            }
        };

        function ConfirmUpdate() {
            var selectedvalueOrd = confirm("Do you want to update ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtConfirmUpdate.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtConfirmUpdate.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrd = confirm("Do you want to cancel ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtConfirmCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtConfirmCancel.ClientID %>').value = "No";
            }
        };

        function ConfirmApply() {
            var selectedvalueOrd = confirm("Do you want to apply ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtConfirmApply.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtConfirmApply.ClientID %>').value = "No";
            }
        };
        function ConfirmCusdecEntryDetails() {
            var selectedvalueOrd = confirm("Do you want to save Cusdec entry data ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirncusdecentry.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirncusdecentry.ClientID %>').value = "No";
            }
        };

        function ConfirmReset() {
            var selectedvalueOrd = confirm("Do you want to reset ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtConfirmReset.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtConfirmReset.ClientID %>').value = "No";
            }
        };

        function ConfirmDelOther() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtConfDelOther.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtConfDelOther.ClientID %>').value = "No";
            }
        };

        function confDeleChar() {
            var resuilt = confirm("Do you want to delete this item?");
            if (resuilt) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfApprove() {
            var selectedvalueOrd = confirm("Do you want to approve ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfFinalize() {
            var selectedvalueOrd = confirm("Do you want to Finalize ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }

    </script>

    <style>
        .gridHeaderAlignRight {
            text-align: right;
        }
    </style>

    <script type="text/javascript">

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

    <%--    <script>
        Sys.Application.add_load(initSomething);
        function initSomething() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.GRNCount').ready(function () {
                var gridHeader = $('#<%=GRNCount.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                   $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                   var v = 0;
                   $('#<%=GRNCount.ClientID%> tbody tr td').each(function (i) {
                    // Here Set Width of each th from gridview to new table(clone table) th 
                    if (v < $(this).width()) {
                        v = $(this).width();
                    }
                    console.log((v).toString());
                    $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                });
                $('.GHead').append(gridHeader);
                $('.GHead').css('position', 'inherit');
                $('.GHead').css('width', '99%');
                $('.GHead').css('top', $('#<%=GRNCount.ClientID%>').offset().top);
                jQuery('#BodyContent_GRNCount tbody').children('tr').eq(1).remove();

               });
        }
    </script>--%>
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
        <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel33">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlApprove">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlSearchMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait38" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait38" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlItemsDetails">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitee" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitee" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5 paddingbottom0">
        <div class="panel-body paddingtopbottom0">
            <asp:UpdatePanel runat="server" ID="UpdatePanel33">
                <ContentTemplate>
                    <asp:HiddenField ID="txtConfirmSave" runat="server" />
                    <asp:HiddenField ID="txtConfirmUpdate" runat="server" />
                    <asp:HiddenField ID="txtConfirmCancel" runat="server" />
                    <asp:HiddenField ID="txtconfirmclear" runat="server" />
                    <asp:HiddenField ID="txtConfirmReset" runat="server" />
                    <asp:HiddenField ID="txtConfirmApply" runat="server" />
                    <asp:HiddenField ID="txtConfDelOther" runat="server" />
                    <asp:HiddenField ID="txtconfirncusdecentry" runat="server" />

                    <div class="row">
                        <div class="col-sm-7">
                            <div id="divWarning" visible="false" class="alert alert-danger alert-dismissible" role="alert" runat="server">
                                <div class="col-sm-11 buttonrow">
                                    <strong>Alert!</strong>
                                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1  buttonrow">
                                    <div class="col-sm-3  buttonrow">
                                        <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lblMsgClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <div class="col-sm-11 buttonrow">
                                    <strong>Success!</strong>
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1  buttonrow">
                                    <div class="col-sm-3  buttonrow">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lblMsgClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <div class="col-sm-11 buttonrow">
                                    <strong>Alert!</strong>
                                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1  buttonrow">
                                    <div class="col-sm-3  buttonrow">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lblMsgClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-5 buttonRow ">
                            <div class="col-sm-2 paddingRight0" runat="server" id="divSave">
                                <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="btnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Finalize
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-2 paddingRight0" runat="server" id="div4">
                                <asp:LinkButton ID="btnReset" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmReset()" OnClick="btnReset_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Reset
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0" runat="server" id="div1">
                                <asp:LinkButton ID="btnViewItemDetials" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnViewItemDetials_Click">
                            <span class="glyphicon glyphicon-list" aria-hidden="true"></span>Item Details
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0" runat="server" id="div3">
                                <asp:LinkButton ID="btnAddCostDetails" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnAddCostDetails_Click">
                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Cost Details
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-2 paddingRight0">
                                <asp:LinkButton ID="lblClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm()" OnClick="lblClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0 height22">
                                    <div class="row">
                                        <div class="col-sm-10">
                                            <b>Cost Sheet</b>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RadioButtonList ID="dbtnActualPost" Enabled="true" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="dbtnActualPost_SelectedIndexChanged">
                                                <asp:ListItem Text="Actual" Value="Actual" />
                                                <asp:ListItem Text="Post" Value="Post" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <asp:Panel runat="server" DefaultButton="btnSearch">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 padding0">
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-3 padding0 labelText1">
                                                            Doc #
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtBlNumer" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 paddingLeft0">
                                                            <asp:LinkButton ID="btnDocNumber" runat="server" OnClick="btnDocNumber_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            Bond
                                                        </div>
                                                        <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtBond" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" Visible="false">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                            Status
                                                        </div>
                                                        <div class="col-sm-8  padding0">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-5 labelText1 paddingRight0">
                                                            Admin team
                                                        </div>
                                                        <div class="col-sm-7 paddingLeft0">
                                                            <asp:TextBox ID="txtAdminteam" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                            Actual Clear Date
                                                        </div>
                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtActualClearDate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 padding0">
                                                            <asp:LinkButton ID="btbtxtActualClearDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtActualClearDate" Animated="true"
                                                                PopupButtonID="btbtxtActualClearDate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:CheckBox ID="chkAll" Text="" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" ToolTip="All" />

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 labelText1 paddingRight0">
                                                            Container #
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtContainerNo" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1  padding3">
                                                            <asp:LinkButton ID="lbtnSeContainer" runat="server" CausesValidation="false" OnClick="lbtnSeContainer_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <div class="buttonRow" style="height: 20px;">
                                                        <asp:UpdatePanel runat="server" ID="pnlSearchMain">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="btnSearch" runat="server" CausesValidation="false" OnClick="btnSearch_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"> Search</span>
                                                                </asp:LinkButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-6 padding0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            From
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtFrom" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnFrom" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrom" Animated="true"
                                                                PopupButtonID="btnFrom" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            To
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <%--<asp:TextBox ID="txtTo" Enabled="false" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtTo" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnto" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo" Animated="true"
                                                                PopupButtonID="btnto" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                            Caring Type
                                                        </div>
                                                        <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                            <asp:DropDownList ID="ddlCaringType" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="Select" Value="0" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                            Mode Of shipment
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <asp:DropDownList ID="ddlModeofShipment" runat="server" class="form-control">
                                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Air" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="Sea" Value="S"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                            LC #
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 padding0">
                                                            <asp:TextBox ID="txtbndreff" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 padding3">
                                                            <asp:LinkButton ID="lbtnSeLcNo" runat="server" CausesValidation="false" OnClick="lbtnSeLcNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-12 labelText1 paddingLeft0">
                                                        <asp:CheckBox runat="server" ID="bypasscusdec" Text="By Pass" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                        </div>
                                    </asp:Panel>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12" style="height: 145px;">
                                                            <asp:Panel runat="server" ScrollBars="Horizontal" Height="145px">
                                                                <asp:GridView ID="dgvCostSheetHeader" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" PagerStyle-CssClass="cssPager"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="dgvCostSheetHeader_PageIndexChanging" OnRowCommand="dgvCostSheetHeader_RowCommand" OnRowDataBound="dgvCostSheetHeader_RowDataBound" OnSelectedIndexChanged="dgvCostSheetHeader_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Select" OnClick="lbtnedititem_Click" CommandArgument="<%# Container.DataItemIndex %>" runat="server">
                                                                                <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seq" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIch_seq_no" runat="server" Text='<%# Bind("Ich_seq_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText='Entry Type' DataField="ich_tp" Visible="false" />
                                                                        <asp:BoundField HeaderText='Bond #' DataField="ich_ref_no" />
                                                                        <asp:TemplateField HeaderText="Doc #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblich_doc_no" runat="server" Text='<%# Bind("ich_doc_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText='B/L Date' DataFormatString="{0:dd/MMM/yyyy}" DataField="ich_pre_dt" HtmlEncode="false" Visible="false" />
                                                                        <asp:BoundField HeaderText='LC #' DataField="IB_BL_REF_NO" HtmlEncode="false" />
                                                                        <asp:BoundField HeaderText='Order Plan Ref' DataField="Cuh_rmk" HtmlEncode="false" Visible="false" />

                                                                        <asp:TemplateField HeaderText="Order Plan Ref">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Cuh_rmk" runat="server" ToolTip='<%# Bind("Cuh_rmk") %>' Text='<%# Bind("Cuh_rmk")==null? "" : Bind("Cuh_rmk") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField HeaderText='Clearance Date' DataFormatString="{0:dd/MMM/yyyy}" DataField="IB_DOC_CLEAR_DT" HtmlEncode="false" />
                                                                        <asp:TemplateField HeaderText="Location">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ib_loc_cd" runat="server" Text='<%# Bind("ib_loc_cd") %>' ToolTip='<%# Bind("LocDescription") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--   <asp:BoundField HeaderText='Location' DataField="ib_loc_cd" HtmlEncode="false" />--%>
                                                                        <asp:BoundField HeaderText='Shipping Mode' DataField="IB_TOS" HtmlEncode="false" Visible="false" />

                                                                        <asp:TemplateField HeaderText="Container Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Ib_carry_tp" runat="server" ToolTip='<%# Bind("containerdes") %>' Text='<%# Bind("Ib_carry_tp") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField HeaderText='Caring Type' DataField="Ib_carry_tp" HtmlEncode="false" Visible="false" />
                                                                        <asp:BoundField HeaderText='Is GRN' DataField="Ich_anal_5" HtmlEncode="false" />
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblICH_STUS" runat="server" Text='<%# Eval("ICH_STUS").ToString().Equals("P")?"Pending":Eval("ICH_STUS").ToString().Equals("A")?"Complete":Eval("ICH_STUS").ToString().Equals("F")?"Finalize":"" %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Supplier
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtSupplier" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtSupplierDesc" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        SI #
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtBLNumber" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        B/L Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtBlDate" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        B/L Ref
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtBLRef" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        ETA
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtETA" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Trade Term
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtTradeTerm" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Entry Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtEntryDate" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Entry #
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtEntryNum" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Actual B/L Clear Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtActualBLClearDate" ReadOnly="true" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        CusEntry
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtCustDecEntry" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-9">
                                                    <div class="col-sm-3 padding0">
                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                            Ex. Rate
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtExRate" onkeydown="return jsDecimals(event);" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <div class="col-sm-8 labelText1 padding0">
                                                            dem/days
                                                        </div>
                                                        <div class="col-sm-4 padding0">
                                                            <asp:TextBox ID="txtDemurrageDays" onkeydown="return jsDecimals(event);" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                            Rate(USD)
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtsgexrate" onkeydown="return jsDecimals(event);" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtsgexrate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:Button ID="btnCalculate" Text="Calculate" runat="server" Width="100%" OnClientClick="return confirm('Do you want to calcualte?')" OnClick="btnCalculate_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        File No
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtFileNo" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnfileno" runat="server" OnClick="lbtnfileno_Click">
                                                            <span class="glyphicon glyphicon-save"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        CusEntry Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtcusdecentrydate" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtcusdecentrydate_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtcusdecentrydate" Animated="true"
                                                            PopupButtonID="LinkButton4" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <asp:Button Text="Update Cusdec" ID="btnupdatecusdec" OnClientClick="ConfirmCusdecEntryDetails()" Width="100%" runat="server" OnClick="btnupdatecusdec_Click" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Buying Rate
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtbuyingrt" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%--      <div class="col-sm-4">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Costing Rate
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtcostingrate" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>--%>
                                                  <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Freight Rate(USD) 
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtFreightRate" onkeydown="return jsDecimals(event);" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                               
                                            </div>
                                            <div class="row">
                                              
                                                 <div class="col-sm-1">
                                                    <div class="col-sm-2 labelText1 paddingLeft0">
                                                        Remarks
                                                    </div>
                                                     </div>
                                                    <div class="col-sm-10 paddingRight5">
                                                        <asp:TextBox ID="txtRemarks" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" TextMode="multiline" Columns="40" Rows="2" MaxLength='160'  onkeyDown="checkTextAreaMaxLength(this,event,'160');" ></asp:TextBox>
                                                    </div>
                                                                                               
                                            </div>
                                               <div class="row">
                                                     <div class="col-sm-10">
                                              <div class="col-sm-2 padding0">
                                                    <asp:Button Text="Update Rates" ID="btnupdaterates" Width="100%" runat="server" OnClick="btnupdaterates_Click" />
                                                </div>
                                                         </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-11">
                                                            Duty
                                                        </div>
                                                        <div class="col-sm-2 padding0">
                                                            <asp:Button Text="Apply" ID="btnApply" OnClientClick="ConfirmApply()" Width="100%" runat="server" OnClick="btnApply_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body" style="height: auto">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-12">
                                                                <asp:GridView ID="dgvDuty" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Seq Num" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIch_seq_no" runat="server" Text='<%# Bind("Icet_seq_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BL Cost">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_ele_cd" runat="server" Text='<%# Bind("icet_ele_cd") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Icet_ele_cd_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIcet_stus" runat="server" Text='<%# Bind("Icet_stus") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pre">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_pre_rt" runat="server" Text='<%# Bind("icet_pre_rt","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Actual">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtIcet_actl_rt" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" Text='<%#Eval("icet_actl_rt","{0:N2}")%>' MaxLength="12"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_actl_rt" runat="server" Text='<%#Eval("icet_actl_rt","{0:N2}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Post">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txticet_finl_rt" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>' MaxLength="12"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_finl_rt" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 paddingLeft0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            Cost Details
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-12">
                                                                <asp:GridView ID="dgvCostDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowCancelingEdit="dgvCostDetails_RowCancelingEdit" OnRowEditing="dgvCostDetails_RowEditing" OnRowUpdating="dgvCostDetails_RowUpdating">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seq Num" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIcet_seq_no" runat="server" Text='<%# Bind("Icet_seq_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="is Edit" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMCAE_IS_EDIT" runat="server" Text='<%# Bind("MCAE_IS_EDIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BL Cost">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_ele_cd" runat="server" Text='<%# Bind("icet_ele_cd") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Icet_ele_cd_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pre">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_pre_rt" runat="server" Text='<%# Eval("icet_pre_rt","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="lblRateHdr" runat="server" Text=''></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRateVal" runat="server" Text=''></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Actual">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txticet_actl_rt" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" Text='<%# Eval("icet_actl_rt","{0:N2}") %>' MaxLength="14"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_actl_rt" runat="server" Text='<%# Eval("icet_actl_rt","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Post">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txticet_finl_rt" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>' MaxLength="14"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblicet_finl_rt" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div id="editbtndiv2" style="width: 1px">
                                                                                    <asp:LinkButton ID="lbtnedititem2" CausesValidation="false" CommandName="Edit" runat="server" OnClick="lbtnedititem2_Click">
                                                                                         <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:LinkButton ID="btndgvCostDetailsUpdate" CausesValidation="false" runat="server" OnClick="btndgvCostDetailsUpdate_Click">
                                                                                     <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="btndgvCostDetailsCancel" CausesValidation="false" runat="server" OnClick="btndgvCostDetailsCancel_Click">
                                                                                     <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 floatRight">
                                                            <div class="col-sm-4">
                                                                <div class="col-sm-6 padding0">
                                                                    Tot. Pre
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtTotPre" BackColor="WHITESMOKE" Style="text-align: right" class="form-control" ReadOnly="true" runat="server" MaxLength="12" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="col-sm-6 padding0">
                                                                    Tot. Act.
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtTotAct" BackColor="WHITESMOKE" Style="text-align: right" class="form-control" ReadOnly="true" runat="server" MaxLength="12" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="col-sm-6 padding0">
                                                                    Tot. Post
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtTotPost" BackColor="WHITESMOKE" Style="text-align: right" class="form-control" ReadOnly="true" runat="server" MaxLength="12" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            Other Cost Details
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-5 padding0">
                                                                    Code
                                                                </div>
                                                                <div class="col-sm-1 padding0">
                                                                </div>
                                                                <div class="col-sm-6 paddingLeft0">
                                                                    Description
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">

                                                                <div class="col-sm-4 paddingLeft0">
                                                                    Actual
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0">
                                                                    Post
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-5 padding0">
                                                                    <asp:TextBox ID="txtCode" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCode_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 padding0">
                                                                    <asp:LinkButton ID="btnOtherEleSearch" runat="server" OnClick="btnOtherEleSearch_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-6 paddingLeft0">
                                                                    <asp:TextBox ID="txtDescription" BackColor="WHITESMOKE" class="form-control" ReadOnly="true" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">

                                                                <div class="col-sm-4 paddingLeft0">
                                                                    <asp:TextBox ID="txtActual" runat="server" Style="text-align: right" onkeydown="return jsDecimals(event);" class="form-control" MaxLength="12" />
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0">
                                                                    <asp:TextBox ID="txtPost" runat="server" Style="text-align: right" onkeydown="return jsDecimals(event);" class="form-control" MaxLength="12" />
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0">
                                                                    <asp:LinkButton ID="btnAddOtherCost" runat="server" OnClick="btnAddOtherCost_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="dgvOtherCostDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowCancelingEdit="dgvOtherCostDetails_RowCancelingEdit" OnRowEditing="dgvOtherCostDetails_RowEditing" OnRowUpdating="dgvOtherCostDetails_RowUpdating" OnRowDeleting="dgvOtherCostDetails_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Seq Num" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIch_seq_no" runat="server" Text='<%# Bind("Icet_seq_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="BL Cost">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblicet_ele_cd" runat="server" Text='<%# Bind("icet_ele_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Icet_ele_cd_desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIcet_stus" runat="server" Text='<%# Bind("Icet_stus") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pre" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblicet_pre_rt" runat="server" Text='<%# Bind("icet_pre_rt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Actual">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtIcet_actl_rt" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" Text='<%#Eval("icet_actl_rt","{0:N2}")%>' MaxLength="12"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblicet_actl_rt" runat="server" Text='<%#Eval("icet_actl_rt","{0:N2}")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Post">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txticet_finl_rt" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>' MaxLength="12"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblicet_finl_rt" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div id="editbtndiv2" style="width: 1px">
                                                                                        <asp:LinkButton ID="btndgvOthCstEdit" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                         <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:LinkButton ID="btndgvOthCstUpdate" CausesValidation="false" runat="server" OnClick="btndgvOthCstUpdate_Click">
                                                                                     <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="btndgvOthCstCancel" CausesValidation="false" runat="server" OnClick="btndgvOthCstCancel_Click">
                                                                                     <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div id="editbtndiv2" style="width: 1px">
                                                                                        <asp:LinkButton ID="btndgvOthCstDelete" CausesValidation="false" CommandName="Delete" OnClientClick="return ConfirmDelOther()" runat="server">
                                                                                         <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-6 padding0">
                                                                        W/O PAL
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtwopal" BackColor="WHITESMOKE" Style="text-align: right" class="form-control" ReadOnly="true" runat="server" MaxLength="12" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-6 padding0">
                                                                        Tot. Act.
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtTotActOther" BackColor="WHITESMOKE" Style="text-align: right" class="form-control" ReadOnly="true" runat="server" MaxLength="12" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-6 padding0">
                                                                        Tot. Post
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtTotPostOther" BackColor="WHITESMOKE" Style="text-align: right" class="form-control" ReadOnly="true" runat="server" MaxLength="12" />
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
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="mpUserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                        PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch" Style="display: none;">
                        <div runat="server" id="test" class="panel panel-primary Mheight">
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
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="mpItemDetails" runat="server" Enabled="True" TargetControlID="Button1"
                        PopupControlID="pnlItemDetails" CancelControlID="btnCloseItemdet" PopupDragHandleControlID="divHeaItemC" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="server" ID="pnlItemDetails" DefaultButton="lbtnSearch" Style="top: 1px !important">
                        <div runat="server" id="Div2" class="panel panel-primary" style="width: 750px; height: 450px; padding: 0px;">
                            <div class="panel panel-default">
                                <div id="divHeaItemC" class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-9">
                                            Items
                                        </div>

                                        <div class="col-sm-2">
                                            <asp:RadioButtonList ID="dbtnActualPost2" Enabled="true" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="dbtnActualPost2_SelectedIndexChanged">
                                                <asp:ListItem Text="Actual" Value="Actual" />
                                                <asp:ListItem Text="Post" Value="Post" />
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="btnCloseItemdet" runat="server">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12 height150">
                                            <asp:UpdatePanel runat="server" ID="pnlItemsDetails">
                                                <ContentTemplate>
                                                    <asp:GridView ID="dgvitems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="7" OnPageIndexChanging="dgvitems_PageIndexChanging" OnRowDataBound="dgvitems_RowDataBound" OnSelectedIndexChanged="dgvitems_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDgvItemSelect" OnClick="btnDgvItemSelect_Click" CausesValidation="false" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" runat="server">
                                                                        <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ici_seq_no" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_seq_no" runat="server" Text='<%# Bind("ici_seq_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ici_line" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_line" runat="server" Text='<%# Bind("ici_line") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ici_ref_line" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_ref_line" runat="server" Text='<%# Bind("ici_ref_line") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_itm_cd" runat="server" Text='<%# Bind("ici_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText='Status' DataField="ici_itm_stus" Visible="false" />
                                                            <asp:BoundField HeaderText='Status' DataField="Ici_anal_5" />
                                                            <asp:BoundField HeaderText='Financial Doc' DataField="Ref_FinNumber" />
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_qty" runat="server" Text='<%# Bind("ici_qty","{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_unit_rt" runat="server" Text='<%# Bind("ici_unit_rt","{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblici_unit_amt" runat="server" Text='<%# Bind("ici_unit_amt","{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-heading" id="divHeader3">
                                    <div class="row">
                                        <div class="col-sm-11">
                                            Item Cost
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12 GridScroll">
                                        <asp:GridView ID="dgvItemCosts" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="15">
                                            <Columns>
                                                <asp:BoundField HeaderText='Item code' DataField="ice_itm_cd" Visible="false" />
                                                <asp:BoundField HeaderText='Item Status' DataField="ice_itm_stus" Visible="false" />
                                                <asp:BoundField HeaderText='Ele. Cat' DataField="ice_ele_cat" Visible="false" />
                                                <asp:BoundField HeaderText='Ele. Type' DataField="ice_ele_tp" Visible="false" />
                                                <asp:BoundField HeaderText='Ele. Code' DataField="ice_ele_cd" />
                                                <asp:BoundField HeaderText='Ele. Code Desc.' DataField="Ice_ele_cd_Desc" />
                                                <asp:BoundField HeaderText='Ele. Rate' DataField="ice_ele_rt" Visible="false" />
                                                <asp:BoundField HeaderText='Ele. Amount' DataField="ice_ele_amnt" Visible="false" />
                                                <asp:BoundField HeaderText='Pre Rate' DataField="ice_pre_rt" Visible="false" />
                                                <asp:TemplateField HeaderText="Tobond">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblice_pre_amnt" runat="server" Text='<%# Bind("Ice_pre_rt","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText='Actual Rate' DataField="ice_actl_rt" Visible="false" />
                                                <asp:TemplateField HeaderText="Rebond">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblice_actl_amnt" runat="server" Text='<%# Bind("ice_actl_rt","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText='Final Rate' DataField="ice_finl_rt" Visible="false" />
                                                <asp:TemplateField HeaderText="Exbond">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblice_finl_amnt" runat="server" Text='<%# Bind("ice_finl_rt","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 height5"></div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-12 padding0">
                                            <div class="col-sm-6 padding0">
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <%--<div class="col-sm-6 padding0" style="text-align: right;">
                                                    Total Pre Amount
                                                </div>--%>
                                                <%--<div class="col-sm-6 padding0">--%>
                                                <asp:TextBox ID="txtTotalPreAmount" Style="text-align: right" CssClass="form-control" ReadOnly="true" runat="server" />
                                                <%--</div>--%>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <%-- <div class="col-sm-6 padding0" style="text-align: right;">
                                                    Total Actual Amount
                                                </div>--%>
                                                <%--<div class="col-sm-6 padding0">--%>
                                                <asp:TextBox ID="txtTotalActualAmount" Style="text-align: right" CssClass="form-control" ReadOnly="true" runat="server" />
                                                <%--</div>--%>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <%--<div class="col-sm-6 padding0" style="text-align: right;">
                                                    Total Post Amount
                                                </div>--%>
                                                <%--<div class="col-sm-6 padding0">--%>
                                                <asp:TextBox ID="txtTotalFinalAmount" Style="text-align: right" CssClass="form-control" ReadOnly="true" runat="server" />
                                                <%--</div>--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 height5"></div>
                                        <div class="col-sm-12 padding0">

                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 padding0" style="text-align: right;">
                                                    Doc #
                                                </div>
                                                <div class="col-sm-6 paddingRight0">
                                                    <asp:TextBox ID="txtPrvDocNo" Style="text-align: left" CssClass="form-control" ReadOnly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3 padding0">
                                                <div class="col-sm-6 padding0" style="text-align: right;">
                                                    Document Date
                                                </div>
                                                <div class="col-sm-6 paddingRight0">
                                                    <asp:TextBox ID="txtPrvDocDt" Style="text-align: right" CssClass="form-control" ReadOnly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-6 padding0" style="text-align: right;">
                                                    Previous Values
                                                </div>
                                                <div class="col-sm-6 paddingRight0">
                                                    <asp:TextBox ID="txtPreviousValues" Style="text-align: right" CssClass="form-control" ReadOnly="true" runat="server" />
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
                    <asp:Button ID="btnaddcpst" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="mpAddCostItems" runat="server" Enabled="True" TargetControlID="btnaddcpst"
                        PopupControlID="pnlAddCostItems" PopupDragHandleControlID="Div6" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="server" ID="pnlAddCostItems" DefaultButton="lbtnSearch">
                        <div runat="server" id="Div6" class="panel panel-primary" style="width: 800px; height: 600px; padding: 0px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-11">
                                            Cost Item Details
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="btnCloseCostItem" runat="server" OnClick="btnCloseCostItem_Click">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body panelscollbar height200">
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <asp:GridView ID="dgvOtherChargeSelect" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectOtherCharge" Text="Select" runat="server" OnClick="btnSelectOtherCharge_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq Num" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIch_seq_no" runat="server" Text='<%# Bind("Icet_seq_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BL Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblicet_ele_cd" runat="server" Text='<%# Bind("icet_ele_cd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIcet_ele_cd_desc" runat="server" Text='<%# Bind("Icet_ele_cd_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIcet_stus" runat="server" Text='<%# Bind("Icet_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pre" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblicet_pre_rt" runat="server" Text='<%# Bind("icet_pre_rt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblicet_actl_rt" runat="server" Text='<%#Eval("icet_actl_rt","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Post">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblicet_finl_rt" runat="server" Text='<%# Eval("icet_finl_rt","{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Line" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIcet_line" runat="server" Text='<%# Eval("Icet_line") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>

                                <div class="panel-heading">
                                    <div class="row">
                                        Add details
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12 padding0">
                                        <div class="col-sm-3 padding0">
                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                Doc No
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                <asp:TextBox ID="txticer_doc_no" CausesValidation="false" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                <asp:Label Text="" ID="lblSelectedSeq" runat="server" Visible="false" />
                                                <asp:Label Text="" ID="LblSelectedLine" runat="server" Visible="false" />
                                            </div>
                                            <div class="col-sm-2 paddingLeft0">
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding0">
                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                Invoice No 
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                <asp:TextBox ID="txticer_ref_no" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding0">
                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                Date
                                            </div>
                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                <asp:TextBox ID="txticer_ref_dt" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnicer_ref_dt" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txticer_ref_dt" Animated="true"
                                                    PopupButtonID="btnicer_ref_dt" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding0">
                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                Amount
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                <asp:TextBox ID="txticer_ref_amt" onkeydown="return jsDecimals(event);" Style="text-align: right" MaxLength="12" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 paddingLeft0">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingLeft0">
                                        <div class="col-sm-4 paddingLeft0">
                                            <div class="col-sm-2 labelText1 paddingLeft0">
                                                Remark
                                            </div>
                                            <div class="col-sm-10 " style="padding-left: 8px; padding-right: 0px;">
                                                <asp:TextBox ID="txticer_ref_rmk" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="col-sm-4 labelText1 padding0">
                                                S: Provider
                                            </div>
                                            <div class="col-sm-6 labelText1 padding0">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtSerProv" OnTextChanged="txtSerProv_TextChanged" />
                                            </div>
                                            <div class="col-sm-1 labelText1 padding3">
                                                <asp:LinkButton ID="lbtnSeSerProv" CausesValidation="false" runat="server" OnClick="lbtnSeSerProv_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                Container
                                            </div>
                                            <div class="col-sm-8 " style="padding-left: 8px; padding-right: 0px;">
                                                <asp:DropDownList ID="dpcontainer" CausesValidation="false" runat="server" class="form-control" Visible="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:Button Text="Add" ID="btnAddITems" runat="server" Width="100%" OnClick="btnAddITems_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-heading">
                                </div>
                                <div class="panel-body  panelscollbar height100">
                                    <div class="col-sm-12 padding0">
                                        <asp:GridView ID="dgvCostBrackup" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteChar" Text="Select" runat="server" OnClientClick="return confDeleChar()" OnClick="btnDeleteChar_Click">
                                                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICER_SEQ_NO" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ICER_SEQ_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICER_ELE_LINE" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LBLICER_ELE_LINE" runat="server" Text='<%# Bind("ICER_ELE_LINE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICER_LINE" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblICER_LINE" runat="server" Text='<%# Bind("ICER_LINE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblICER_DOC_NO" runat="server" Text='<%# Bind("ICER_DOC_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ICER_REF_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("ICER_REF_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblICER_REF_AMT" Style="text-align: right" runat="server" Text='<%# Bind("ICER_REF_AMT","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("ICER_REF_RMK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Provider">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("ICER_SER_PROVIDER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Veh:Reg">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtvehreg" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnelesub" Text="Select" runat="server" OnClick="btnelesub_Click">
                                                            <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                </div>
                                <div class="panel-body  panelscollbar height100">
                                    <div class="col-sm-12 padding0">
                                        <asp:GridView ID="grdelesubcost" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbliced_ref" runat="server" Text='<%# Bind("container") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbliced_cd" runat="server" Text='<%# Bind("iced_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_sub_amt" runat="server" Text='<%# Bind("iced_amt") %>' OnTextChanged="txt_sub_amt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Veh:Reg" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="iced_veh_reg" runat="server" Text='<%# Bind("iced_veh_reg") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICER_LINE" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbliced_det_line" runat="server" Text='<%# Bind("iced_det_line") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICER_LINE" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbliced_ele_line" runat="server" Text='<%# Bind("iced_ele_line") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICER_LINE" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbiced_ref_line" runat="server" Text='<%# Bind("iced_ref_line") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6"></div>
                                    <div class="col-md-2">
                                        <asp:Button Text="Save" ID="btnCostdetSave" OnClientClick="ConfirmSave()" runat="server" Width="100%" OnClick="btnCostdetSave_Click" />
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="pnlApprove">
                                        <ContentTemplate>
                                            <div class="col-md-2">
                                                <asp:Button Text="Approve" ID="btnApprove" OnClientClick="return ConfApprove();" runat="server" Width="100%" OnClick="btnApprove_Click" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button Text="Finalize" ID="btnfinalize" OnClientClick="return ConfFinalize();" runat="server" Width="100%" OnClick="btnfinalize_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSea">
        <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnSerClose" runat="server" OnClick="lbtnSerClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row height16">
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
                                        <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSea" runat="server" OnClick="lbtnSea_Click">
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
</asp:Content>


