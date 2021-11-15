<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="OrderPlan.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Order_Plan.OrderPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function ConfirmDeleteContainer() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
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

        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmPlaceOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "No";
            }
        };

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtapprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtcancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancel.ClientID %>').value = "No";
            }
        };

        function ConfirmDeleteAll() {
            var selectedvalueOrdPlace = confirm("Do you want to delete selected items ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtdelselected.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtdelselected.ClientID %>').value = "No";
            }
        };

        function ConfirmDeleteItem() {
            var selectedvalueOrdPlace = confirm("Do you want to delete selected item ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtdeleteItem.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtdeleteItem.ClientID %>').value = "No";
            }
        };

        function hideColumns() {
            // Hide unwanted columns before printing. BodyContent_grdorderdetails is the html generated id of the grid
            var gridrows = $("#BodyContent_grdorderdetails tbody tr");

            for (var i = 0; i < gridrows.length; i++) {
                gridrows[i].cells[0].style.display = "none";
                gridrows[i].cells[11].style.display = "none";
                gridrows[i].cells[15].style.display = "none";
                gridrows[i].cells[16].style.display = "none";
            }
        };

        function showColumns() {
            // show hidden columns after printing. BodyContent_grdorderdetails is the html generated id of the grid
            var gridrows = $("#BodyContent_grdorderdetails tbody tr");

            for (var i = 0; i < gridrows.length; i++) {
                gridrows[i].cells[0].style.display = "inline";
                gridrows[i].cells[11].style.display = "inline";
                gridrows[i].cells[15].style.display = "inline";
                gridrows[i].cells[16].style.display = "inline";
            }
        };

        function PrintDiv() {
            var n = document.getElementById('<%= txtordno.ClientID %>').value;
            if (n.length < 1) {
                alert('Please load order details !!!');
                return false;
            }

            hideColumns();

            //Hide unwanted areas of the page by placing areas in divs
            //document.getElementById("hidediv").style.display = 'none';
            //document.getElementById("hidedivpanelheader").style.display = 'none';
            //document.getElementById("dvContentsOrder").style.display = 'none';

            //document.getElementById("pnldvopitems").style.borderColor = "transparent";
            //document.getElementById("darkpnl").style.borderColor = "transparent";   
            //document.getElementById("pnldvopitemsSub").style.borderColor = "transparent";



            // pnlop mean the panel which contain the print area
            var printContent = document.getElementById('<%= pnlop.ClientID %>');
            var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');

            //getting header values
            var ordernumber = document.getElementById('<%= txtordno.ClientID %>').value;
            var refno = document.getElementById('<%= txtmanualref.ClientID %>').value;
            var supcode = document.getElementById('<%= txtsupplier.ClientID %>').value;
            var supname = document.getElementById('<%= lblsupplier.ClientID %>').innerHTML;
            var date = document.getElementById('<%= txtdate.ClientID %>').value;
            var remarks = document.getElementById('<%= txtdescription.ClientID %>').value;
            var tradetirm = document.getElementById('<%= ddltradeterm.ClientID %>').value;
            var tratetirmtext = document.getElementById('<%= lblcif.ClientID %>').innerHTML;

            var DropdownList1 = document.getElementById('<%= ddlmodeofshipment.ClientID %>');
            var shipmode = DropdownList1.options[DropdownList1.selectedIndex].text;

            var DropdownList2 = document.getElementById('<%= ddlportoforigin.ClientID %>');
            var port = DropdownList2.options[DropdownList2.selectedIndex].text;

            var eta = document.getElementById("<%= txteta.ClientID %>").value;
            var eta1 = document.getElementById("<%= lblleadtime.ClientID %>").innerHTML;
            var eta2 = document.getElementById("<%= lblleaddays.ClientID %>").innerHTML;
            var eta3 = document.getElementById("<%= lbldays.ClientID %>").innerHTML;
            var qty = document.getElementById("<%= lbltotordqty.ClientID %>").innerHTML;
            var currency = document.getElementById("<%= lblcurrency.ClientID %>").innerHTML;
            var value = document.getElementById("<%= lbltotordval.ClientID %>").innerHTML;


            //applying boostrap theme & contents to the report header
            printWindow.document.write('<link  href="../../../../Css/bootstrap.min.css" rel="stylesheet" />');
            // printWindow.document.write('<img src="../../../../images/banners/lg-abans.png" />');
            printWindow.document.write("</br>");
            printWindow.document.write("</br>");
            printWindow.document.write("<u>Order Plan # - " + ordernumber + "</u>");
            printWindow.document.write("</br>");
            printWindow.document.write("</br>");
            printWindow.document.write("Ref # - " + refno);
            printWindow.document.write("</br>");
            printWindow.document.write("Supplier - " + supcode + " ~ (" + supname + ")");
            printWindow.document.write("</br>");
            printWindow.document.write("Date - " + date);
            printWindow.document.write("</br>");
            printWindow.document.write("Remarks - " + remarks);
            printWindow.document.write("</br>");
            printWindow.document.write("Trade Term - " + tradetirm + " ~ (" + tratetirmtext + ")");
            printWindow.document.write("</br>");
            printWindow.document.write("Mode of Shipment - " + shipmode);
            printWindow.document.write("</br>");
            printWindow.document.write("Port of Origin - " + port);
            printWindow.document.write("</br>");
            printWindow.document.write("ETA - " + eta + " ~ (" + eta1 + "  " + eta2 + "  " + eta3 + ")");
            printWindow.document.write("</br>");
            printWindow.document.write("</br>");
            printWindow.document.write("<b>Total Order Qty - " + qty + "</b>");
            printWindow.document.write("</br>");
            printWindow.document.write("<b>Total Order Value - " + currency + " " + value + "</b>");
            printWindow.document.write("</br>");
            printWindow.document.write("</br>");

            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();

            //show hidden div after printing
            //document.getElementById("hidediv").style.display = 'inline';
            //document.getElementById("hidedivpanelheader").style.display = 'inline';
            //document.getElementById("dvContentsOrder").style.display = 'inline';

            //document.getElementById("pnldvopitems").style.borderColor = "#ddd";
            //document.getElementById("darkpnl").style.borderColor = "#ddd";
            //document.getElementById("pnldvopitemsSub").style.borderColor = "#ddd";

            showColumns();
        }



        <%--$(function () {
            $(document).keyup(function (e) {
                var key = (e.keyCode ? e.keyCode : e.charCode);
                switch (key) {
                    case 112:
                        navigateUrl($('a[id$=lnk1]'));
                        break;
                    case 113:
                        doevent();
                        break;
                    case 114:
                        navigateUrl($('a[id$=lnk3]'));
                        break;
                    default:;
                }
            });

            function navigateUrl(jObj) {
                window.location.href = $(jObj).attr("href");
            };

            function doevent() {
                document.getElementById('<%= Button1.ClientID %>').click();
            };
        });--%>

    </script>
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

    <script src="../../../../Js/jquery.min.js"></script>

    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
            <asp:HiddenField ID="txtapprove" runat="server" />
            <asp:HiddenField ID="txtcancel" runat="server" />
            <asp:HiddenField ID="txtdeleteItem" runat="server" />
            <asp:HiddenField ID="txtdelselected" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-8  buttonrow">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-11  buttonrow ">
                                <strong>Well done!</strong>
                                <asp:Label ID="lblok" runat="server"></asp:Label>
                            </div>

                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivokclose_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">
                            <div class="col-sm-11  buttonrow ">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblalert" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndicalertclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                            <div class="col-sm-11  buttonrow ">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblinfo" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivinfoclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-sm-4  buttonRow">

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="btnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm()" OnClick="lbtnClear_Click1">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-4">
                            <asp:LinkButton ID="lbtnprintord" CausesValidation="false" runat="server" OnClientClick="PrintDiv();">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true" style="font-size:20px"></span>Print Order
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-1 paddingRight0">
                            <div class="dropdown">
                                <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                </a>
                                <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnkititem" CausesValidation="false" runat="server" OnClick="lbtnkititem_Click">
                                                        <span class="glyphicon glyphicon-briefcase" aria-hidden="true" style="font-size:20px"></span>KIT Item
                                                </asp:LinkButton>
                                            </div>

                                            <%--              <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnmailord" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-envelope" aria-hidden="true" style="font-size:20px"></span>Mail Order
                                                </asp:LinkButton>
                                            </div>--%>

                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>

                                            <%--                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnpi" CausesValidation="false" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-list-alt" aria-hidden="true" style="font-size:20px"></span>Generate PI
                                                </asp:LinkButton>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>--%>

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ConfirmCancel();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size:20px"></span>Cancel
                                                </asp:LinkButton>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnapprove" CausesValidation="false" runat="server" OnClientClick="ConfirmApprove();" OnClick="lbtnapprove_Click">
                                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true" style="font-size:20px"></span>Approve
                                                </asp:LinkButton>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>

                                            <%--<div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-save" aria-hidden="true" style="font-size:20px"></span>Save
                                                </asp:LinkButton>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>--%>

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnupload" CausesValidation="false" runat="server" OnClick="lbtnupload_Click">
                                                        <span class="glyphicon glyphicon-upload" aria-hidden="true" style="font-size:20px"></span>Upload Excel
                                                </asp:LinkButton>
                                            </div>

                                            <%--                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>

                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnexportexcel" CausesValidation="false" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-list-alt" aria-hidden="true" style="font-size:20px"></span>Export Excel
                                                </asp:LinkButton>--%>
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

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default" id="dvContentsOrder">

                                <div class="panel panel-heading" style="height:30px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-9 padding0">
                                             <strong>Order Plan</strong>
                                        </div>
                                        <div class="col-sm-3 padding0">
                                             <asp:Button Text="Excel Upload" ID="btnExcelDataUpload" OnClick="btnExcelDataUpload_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body" id="panelbodydiv">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Ref #
                                            </div>
                                            <div class="col-sm-7 oprefnotxtbox">
                                                <asp:TextBox ID="txtmanualref" runat="server" TabIndex="1" CssClass="form-control" MaxLength="20" OnTextChanged="txtmanualref_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbtnsearchordref_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-1 ">

                                                <asp:CheckBox ID="Chkitem" runat="server" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Supplier
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtsupplier" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnTextChanged="txtsupplier_TextChanged"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblsupplier"></asp:Label>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnsupplier" runat="server" OnClick="lbtnsupplier_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Order #
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtordno" runat="server" TabIndex="3" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtordno_TextChanged"></asp:TextBox>
                                                <asp:CheckBox ID="chkteplate" runat="server" AutoPostBack="true" OnCheckedChanged="chkteplate_CheckedChanged" />
                                                <asp:Label runat="server" ID="Label2" Text="Copy From Previous Order"></asp:Label>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnsearchord" runat="server" OnClick="lbtnsearchord_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Date
                                            </div>

                                            <div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtdate" runat="server" TabIndex="4" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                                        PopupButtonID="lbtnimgselectdate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -4px">
                                                    <asp:LinkButton ID="lbtnimgselectdate" CausesValidation="false" runat="server" Visible="false">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>


                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Remarks
                                            </div>
                                            <div class="col-sm-6 padding0 opremarksnotxtbox">
                                                <asp:TextBox ID="txtdescription" runat="server" TabIndex="5" CssClass="form-control" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Mode of Shipment
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlmodeofshipment" runat="server" TabIndex="6" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmodeofshipment_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label runat="server" ID="lblmodeship"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Port of Origin
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:DropDownList ID="ddlportoforigin" runat="server" TabIndex="7" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlportoforigin_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label runat="server" ID="lblportoforigin"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-4 paddingLeft0">
                                        <div class="panel panel-default marginBottom0">
                                            <div class="panel-heading paddingtopbottom0">Container Details</div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1 padding0">
                                                            Type
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5 padding0">
                                                            <asp:DropDownList ID="ddlContainersType" AutoPostBack="true" OnSelectedIndexChanged="ddlContainersType_SelectedIndexChanged" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-sm-2 labelText1 padding0">
                                                            unit
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:TextBox ID="txtContainerNo" CausesValidation="false" runat="server"
                                                                class="diWMClick validateInt form-control" MaxLength="15"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight5">
                                                            <asp:LinkButton ID="btnContainerAdd" runat="server" OnClick="btnContainerAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:15px"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="dgvContainers" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Container Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIbc_tp" runat="server" Text='<%# Bind("Ibc_tp") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIbc_desc" runat="server" Text='<%# Bind("Ibc_unit") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIbc_act" runat="server" Text='<%# Bind("Ibc_act") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnContaiDelete" OnClick="btnContaiDelete_Click" CausesValidation="false" runat="server" OnClientClick="return ConfirmDeleteContainer();">
                                                                 <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Trade Term
                                            </div>
                                            <div class="col-sm-8 tradetermddl">
                                                <asp:DropDownList ID="ddltradeterm" runat="server" TabIndex="8" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltradeterm_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label runat="server" ID="lblcif"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                ETA
                                            </div>
                                            <div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txteta" runat="server" CssClass="form-control" TabIndex="9" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:Label runat="server" ID="lblleadtime" Text="Lead time in "></asp:Label>
                                                    <asp:Label runat="server" ID="lblleaddays"></asp:Label>
                                                    <asp:Label runat="server" ID="lbldays" Text="Days"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Status
                                            </div>
                                            <div class="col-sm-8 tradetermddl" style="color: red">
                                                <asp:Label runat="server" ID="lblstatus"></asp:Label>
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
                            <div class="panel panel-default " id="pnldvopitems">


                                <div class="panel-heading pannelheading" id="pnldvopitemsSub">

                                    <div id="hidedivpanelheader">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-11">
                                                    Order Items
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:LinkButton ID="lbtnPDelete" runat="server" CausesValidation="false" OnClientClick="ConfirmDeleteAll();" OnClick="lbtnPDelete_Click">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Delete Items
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>



                                <div class="panel-body ">

                                    <div id="hidediv">
                                        <div class="row">
                                            <div class="col-sm-3 ordvaliditydiv">

                                                <div class="row">

                                                    <div class="col-sm-2 labelText1 ordvaliditysubdiv">
                                                        For :
                                                    </div>

                                                    <div class="col-sm-2 labelText1 ordvaliditysubdiv">
                                                        Year
                                                    </div>

                                                    <div class="col-sm-4 ddlyearcontrol">
                                                        <asp:TextBox runat="server" CssClass="form-control" TabIndex="10" onkeydown="return jsDecimals(event);" ID="txtddlYear"></asp:TextBox>
                                                        <%--<asp:DropDownList ID="ddlYear" runat="server" Width="73px" TabIndex="10" AutoPostBack="true" CssClass="form-control">
                                                </asp:DropDownList>--%>
                                                    </div>

                                                    <div class="col-sm-1 labelText1 ordvaliditysubdiv">
                                                        Month
                                                    </div>

                                                    <div class="col-sm-3 ddlmonthcss">
                                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="127px" TabIndex="11" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Price Type 
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:DropDownList ID="ddlTag" runat="server" class="form-control">
                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="General" Value="N" />
                                                            <asp:ListItem Text="Special Project" Value="S" />
                                                        </asp:DropDownList>
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

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    Item
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtitem" runat="server" TabIndex="12" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtitem_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnitmfind" runat="server" OnClick="lbtnitmfind_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    Model
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtmodel" runat="server" TabIndex="13" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnmodelfind" runat="server" OnClick="lbtnmodelfind_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Item Type
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlitemtype" runat="server" TabIndex="14" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">
                                                <div id="msgColor" runat="server">
                                                    <div class="col-sm-3 labelText1">
                                                        Colour
                                                    </div>
                                                    <%-- <div class="col-sm-9">
                                                    <asp:TextBox ID="txtcolour" runat="server" TabIndex="15" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                </div>--%>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList ID="ddlMultiColor" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    Qty
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtqty" runat="server" TabIndex="16" CssClass="form-control" onkeydown="return jsDecimals(event);" MaxLength="12" Style="text-align: right" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Unit Rate
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtunitrate" runat="server" TabIndex="17" CssClass="form-control" onkeydown="return jsDecimals(event);" MaxLength="12" Style="text-align: right" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnadditems" CausesValidation="false" TabIndex="18" CssClass="floatRight" runat="server" OnClick="lbtnadditems_Click">
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
                                            <div class="col-sm-1">
                                                <div class="labelText1">
                                                    Container Type
                                                </div>
                                            </div>
                                              <div class="col-sm-1">
                                                  <asp:DropDownList ID="DropDownListContainer" AutoPostBack="true" OnSelectedIndexChanged="DropDownListContainer_SelectedIndexChanged" runat="server" class="form-control">
                                        </asp:DropDownList>
                                              </div>
                                              <div class="col-sm-1">
                                                  <div class="labelText1">
                                                      Container Qty
                                                  </div>
                                            </div>
                                              <div class="col-sm-1">
                                                  <asp:TextBox ID="TextBoxContainerQty" runat="server"  CssClass="form-control" onkeydown="return jsDecimals(event);" MaxLength="12" Style="text-align: right" ></asp:TextBox>                                                                                                       
                                            </div>
                                        </div>
                                        <div class="col-sm-12 height5">

                                            <div class="col-sm-7">

                                                <div class="row">

                                                    <div class="col-sm-2 labelText1">
                                                        <strong>Description</strong>
                                                    </div>
                                                    <div class="col-sm-10">
                                                        <asp:Label ID="lblitmdescription" runat="server" CssClass="col-sm-3 labelText1" Font-Bold="true" ForeColor="#A513D0" Width="250px" MaxLength="200"></asp:Label>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-3">

                                                <div class="row">

                                                    <div class="col-sm-4 labelText1">
                                                        <strong>Brand</strong>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:Label ID="lblbrandit" runat="server" CssClass="col-sm-3 labelText1" Font-Bold="true" ForeColor="#A513D0" MaxLength="20"></asp:Label>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-2">

                                                <div class="row">

                                                    <div class="col-sm-4 labelText1">
                                                        <strong>UOM </strong>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:Label ID="lbluomit" runat="server" Font-Bold="true" CssClass="col-sm-3 labelText1" ForeColor="#A513D0" MaxLength="10"></asp:Label>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                </div>

                                                <div class="col-sm-9">
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                    </div>
                                    <asp:UpdatePanel>
                                        <asp:Panel ID="pnlop" runat="server">
                                            <div id="divgridprint">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body panelscollbar height120">
                                                            <asp:GridView ID="grdorderdetails" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" OnRowDeleting="OnRowDeleting" OnRowDataBound="grdorderdetails_RowDataBound" OnRowEditing="grdorderdetails_RowEditing" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                <Columns>

                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="200" ReadOnly="true" />
                                                                    <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <asp:BoundField DataField="Brand" HeaderText="Brand" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <asp:BoundField DataField="Colour" HeaderText="Colour" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <%--<asp:BoundField DataField="PartNo" HeaderText="PartNo" ItemStyle-Width="100" ReadOnly="true" />--%>
                                                                    <asp:BoundField DataField="UOM" HeaderText="UOM" ReadOnly="true" HeaderStyle-CssClass="GridColumnHide" ItemStyle-CssClass="GridColumnHide" />
                                                                    <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-Width="100" />
                                                                    <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-Width="100" />
                                                                    <asp:BoundField DataField="Ord Qty" HeaderText="Ord Qty" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Unit Rate" HeaderText="Unit Rate" DataFormatString="{0:0.#####}" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Value" HeaderText="Item Value" ItemStyle-Width="100" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />                                                                    
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:Label Text="Kit Code" runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label Visible='<%# !Eval("KitItemCode").ToString().Equals("0")%>' ID="lblKitItemCode" Width="100px" Text='<%# Bind("KitItemCode") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="Kit Item Code" HeaderText="Kit Code" ItemStyle-Width="100" ReadOnly="true" />--%>
                                                                    <asp:BoundField DataField="Ioi Line" HeaderText="Ioi Line" ItemStyle-Width="25" ReadOnly="true" HeaderStyle-CssClass="GridColumnHide" ItemStyle-CssClass="GridColumnHide" />
                                                                    <asp:BoundField DataField="TagName" HeaderText="Price Type" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <asp:BoundField DataField="Tag" HeaderText="Tag" ItemStyle-Width="25" ReadOnly="true" HeaderStyle-CssClass="GridColumnHide" ItemStyle-CssClass="GridColumnHide" />
                                                                    <asp:TemplateField HeaderText="Project Name">
                                                                        <EditItemTemplate>

                                                                            <asp:TextBox ID="txtpName" MaxLength="20" Enabled="true" CausesValidation="false"
                                                                                runat="server" CssClass="txtserexdate form-control" Width="120px" Text='<%# Bind("ProName") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProNamenew" runat="server" Text='<%# Bind("ProName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CBM">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotCBM" Width="100px" Text='<%# Bind("IOI_ITM_TOT_CBM","{0:N2}") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Trade Agreement" HeaderText="Trade Agreement" ItemStyle-Width="100" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                                    <%--<asp:BoundField DataField="ContainerType" HeaderText="ContainerType" ItemStyle-Width="100" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="ContainerType">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ContainerTypelbl" Width="100px" Text='<%# Bind("ContainerType") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Container Qty" HeaderText="Container Qty" DataFormatString="{0:0.#####}" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="PartNo" HeaderText="PartNo" ItemStyle-Width="100" ReadOnly="true" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>

                                                                            <div id="editbtndiv">
                                                                                <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>

                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>

                                                                            <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server" OnClick="OnUpdate">
                                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>
                                                                            </asp:LinkButton>


                                                                            <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" OnClick="OnCancel" runat="server">
                                                        <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span>
                                                                            </asp:LinkButton>

                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>

                                                                            <div id="delbtndiv">
                                                                                <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" OnClientClick="ConfirmDeleteItem();" runat="server">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProName" runat="server" Text='<%# Bind("ProName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </asp:UpdatePanel>
                                </div>



                            </div>
                        </div>
                    </div>


                </div>

                <div class="row">
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading panelHeadingInfoBar">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-9">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Currency :
                                            </div>
                                            <div class="col-sm-3 labelText1">
                                                <asp:Label ID="lblcurrency" runat="server" ForeColor="#A513D0"></asp:Label>
                                            </div>
                                            <div class="col-sm-2 labelText1">
                                                Rate :
                                            </div>
                                            <div class="col-sm-2 labelText1">
                                                <asp:Label ID="lbtnrate" runat="server" ForeColor="#A513D0"></asp:Label>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1 paddingRight0 paddingLeft0">
                                                Total Order Qty :
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbltotordqty" runat="server" CssClass="col-sm-3 labelText1" ForeColor="#A513D0" Style="text-align: right"></asp:Label>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1 paddingRight0 paddingLeft0">
                                                Total Order Value In :
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:Label ID="lbltotordval" runat="server" CssClass="col-sm-3 labelText1" ForeColor="#A513D0" Style="text-align: right"></asp:Label>
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
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvResultUser_PageIndexChanging" OnSelectedIndexChanged="dvResultUser_SelectedIndexChanged">
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

      <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupContainer" runat="server" Enabled="True" TargetControlID="Button6"
                PopupControlID="pnlContainer" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

      <asp:Panel runat="server" ID="pnlContainer">
        <div runat="server" id="Div7" class="panel panel-primary">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnf">
                            Confirmation
                    <asp:LinkButton ID="btnConfClose" runat="server" OnClick="btnConfClose_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="lblConfText" Text="Container details are not available.Do you want to continue?" runat="server" />
                                    <asp:HiddenField ID="hdfConfItem" runat="server" />
                                    <asp:HiddenField ID="hdfConfStatus" runat="server" />
                                    <asp:HiddenField ID="hdfConf" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfYes" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConfYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfNo" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnkit" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpkit" runat="server" Enabled="True" TargetControlID="btnkit"
                PopupControlID="pnlpopupkit" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="pnlpopupkitgrd">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupkit">
                <div runat="server" id="DivKit" class="panel panel-primary Mheight">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton1" runat="server">
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

                                    <div id="pnlscroll" class="panelscoll" style="height: 310px">
                                        <asp:GridView ID="grdkititems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">


                                            <EmptyDataTemplate>
                                                <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                    <tbody>
                                                        <tr>
                                                            <th scope="col">Code
                                                            </th>
                                                            <th scope="col">Description
                                                            </th>
                                                            <th scope="col">Category 1
                                                            </th>
                                                            <th scope="col">Category 2
                                                            </th>
                                                            <th scope="col">Brand
                                                            </th>
                                                            <th scope="col">Model
                                                            </th>
                                                            <th scope="col">Part No
                                                            </th>
                                                            <th scope="col">Colour
                                                            </th>
                                                            <th scope="col">Item Type
                                                            </th>
                                                            <th scope="col">UOM
                                                            </th>
                                                            <th scope="col">Status
                                                            </th>
                                                            <th scope="col">Cost
                                                            </th>
                                                            <th scope="col">Qty
                                                            </th>
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
                                                <asp:BoundField DataField="MI_CD" HeaderText="Code" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_SHORTDESC" HeaderText="Description" ItemStyle-Width="150" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_CATE_1" HeaderText="Category 1" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_CATE_2" HeaderText="Category 2" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_BRAND" HeaderText="Brand" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_MODEL" HeaderText="Model" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_PART_NO" HeaderText="Part No" ItemStyle-Width="150" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_COLOR_INT" HeaderText="Colour" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_ITM_TP" HeaderText="Item Type" ItemStyle-Width="150" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_ITM_UOM" HeaderText="UOM" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_ITM_STUS" HeaderText="Status" ItemStyle-Width="100" ReadOnly="true" />
                                                <asp:BoundField DataField="MI_ITMTOT_COST" HeaderText="Cost" ItemStyle-Width="100" ReadOnly="true" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-Width="100" ReadOnly="true" ItemStyle-HorizontalAlign="Right" />
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

    <asp:UpdatePanel ID="UpdatePanelOP" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button22" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button22"
                PopupControlID="PanelOP" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="PanelOP" runat="server" align="center">
        <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div class="panel panel-default" style="width: 300px;">
                    <div class="panel-heading">
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
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="Button4" runat="server" Text="Yes" CausesValidation="false" class="btn btn-primary" OnClick="Button4_Click" />
                            </div>
                            <div class="col-sm-4 ">
                                <asp:Button ID="Button5" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="Button5_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                PopupControlID="pnlpopupExcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton2" runat="server">
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

                            <div class="col-sm-6">
                                <div class="row">

                                    <div class="col-sm-4 labelText1">
                                        File Name
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        <asp:RadioButtonList ID="rbHDR" runat="server" RepeatDirection="Horizontal" Enabled="false" Visible="false">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                    <div class="col-sm-8">
                                        <asp:LinkButton ID="lbtnuploadexcel" runat="server" OnClick="lbtnuploadexcel_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true">Upload</span>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>

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
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div2" class="panel panel-default height400 width850">

                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div3" runat="server">
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
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblSbuMsg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblSbuMsg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lMssg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
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
                                            <asp:Button ID="lbtnUploadExcelFile" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                                OnClick="lbtnUploadExcelFile_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Button ID="btn15" runat="server" Text="Button" Style="display: none;" />
                <asp:ModalPopupExtender ID="popupErro" runat="server" Enabled="True" TargetControlID="btn15"
                    PopupControlID="pnlExcelErro" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
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
                                                                <asp:LinkButton ID="lbtnExcClose" runat="server" OnClick="lbtnExcClose_Click">
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
                                                                        <div style="height: 400px; overflow-y: auto;">
                                                                            <asp:GridView ID="dgvError" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False"
                                                                                PagerStyle-CssClass="cssPager">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Excel Line">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblLine" Text='<%# Bind("Line","{0:#00}") %>' runat="server" Width="100%" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="60px" />
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Err Data">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTmp_err_text" Text='<%# Bind("Tmp_err_text") %>' runat="server" Width="100%" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="50px" />
                                                                                        <HeaderStyle Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Error">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTmp_err" Text='<%# Bind("Tmp_err") %>' runat="server" Width="100%" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="100px" />
                                                                                        <HeaderStyle Width="100px" />
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
    <%-- pnl save order plan excel --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popOpExcSave" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlOpExcSave" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upOpExcSave">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaidt10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaidt10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlOpExcSave">
        <asp:UpdatePanel runat="server" ID="upOpExcSave">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 1px;">
                    <div class="panel panel-default" style="height: 40px; width: 500px;">
                        <div class="panel-heading" style="height: 40px;">
                            <div class="col-sm-8">
                                <strong>Generate order plans</strong>
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Generate" ID="btnGenOrdPlans" OnClick="btnGenOrdPlans_Click" runat="server" />
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Cancel" ID="btnCancelProcess" OnClick="btnCancelProcess_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Pnl document popup --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popDocNoShow" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDocNoShow" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlDocNoShow">
        <asp:UpdatePanel runat="server" ID="upDocNoShow">
            <ContentTemplate>
                <div runat="server" id="Div6" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 280px; width: 225px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10 padding0">
                                <strong>Document Numbers</strong>
                            </div>
                            <div class="col-sm-2 text-right paddingLeft0">
                                <asp:LinkButton ID="lbtnOldPartRemClose" runat="server" >
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body" style="padding-bottom:0px; padding-top:0px;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="height: 250px; overflow-y: auto;">
                                        <asp:GridView ID="dgvDocNo" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False"
                                            PagerStyle-CssClass="cssPager">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Order #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIO_OP_NO" Text='<%# Bind("IO_OP_NO") %>' runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                    <HeaderStyle Width="50px" />
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
    </asp:Panel>
    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('.validateDecimal').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                // console.log(ch);
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
            $('.diWMClick').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            $('.validateInt').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0) || (charCode == 13)) {
                    return true;
                }
                else if (str.length < 10) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 10);
                    //alert(charCode);
                    alert('Maximum 4 characters are allowed ');
                    return false;
                }
            });
        }
    </script>


</asp:Content>
