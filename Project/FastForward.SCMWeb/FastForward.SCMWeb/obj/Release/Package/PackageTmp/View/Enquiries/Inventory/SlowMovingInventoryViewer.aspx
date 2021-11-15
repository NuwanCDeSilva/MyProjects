<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SlowMovingInventoryViewer.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.SlowMovingInventoryViewer" %>

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

        function ExcelDownloadConfirm() {
            var selectedvalue = confirm("Do you want to download the balance breakup ?");
            if (selectedvalue) {
                document.getElementById('<%=hfexceldownload.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hfexceldownload.ClientID %>').value = "No";
            }
        };

        function Enable() {
            return;
        }

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

        document.documentElement.style.overflowX = 'hidden';

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="mainpnl">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="mainpnl" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
            <asp:HiddenField ID="hfexceldownload" runat="server" />
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
                                    <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright">
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
                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright">
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
                                    <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-sm-4  buttonRow crnbuttonrowmargin">

                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                            <div class="col-sm-11">
                                <strong>Info!</strong>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-4 paddingRight15">
                            <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save/Process
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingLeft15" style="margin-left: -20px">
                            <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="LinkButton1_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                    </div>

                </div>


                <div class="row paddingtopbottom0">

                    <div class="panel-body paddingtopbottom0">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    <strong>Slow Moving Inventory Viewer</strong>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row paddingtopbottom0">

                                    <div class="panel-body paddingtopbottom0">


                                        <div class="col-sm-3">

                                            <div class="panel panel-default">

                                                <div class="panel-heading pannelheading paddingtopbottom0">
                                                    <strong>Sales For the Period</strong>
                                                </div>

                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-6">
                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    From
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="txtfrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                                            PopupButtonID="lbtnfrom" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                    <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                                        <asp:LinkButton ID="lbtnfrom" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-6">
                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    To
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="txtto" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                                            PopupButtonID="lbtnto" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                    <div id="caldv2" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                                        <asp:LinkButton ID="lbtnto" TabIndex="3" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-9">

                                            <div class="panel panel-default">

                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Item
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtitem" runat="server" TabIndex="6" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtitem_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnitemsearch" CausesValidation="false" runat="server" OnClick="lbtnitemsearch_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div id="chkitem" class="col-sm-3 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chkitemall" Text="All" AutoPostBack="true" OnCheckedChanged="chkitemall_CheckedChanged" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Model
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtmodel" runat="server" TabIndex="6" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtmodel_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnmodel" CausesValidation="false" runat="server" OnClick="lbtnmodel_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div id="chkmodel" class="col-sm-3 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chkmodelall" Text="All" AutoPostBack="true" OnCheckedChanged="chkmodelall_CheckedChanged1" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="col-sm-3">
                                                            <div class="row">

                                                                <div class="col-sm-2 labelText1">
                                                                    Brand
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtbrand" runat="server" TabIndex="4" AutoPostBack="true" OnTextChanged="txtbrand_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lBtnBrand" CausesValidation="false" runat="server" OnClick="lBtnBrand_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div id="caldvw" class="col-sm-3 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chkbrand" Text="All" AutoPostBack="true" OnCheckedChanged="chkbrand_CheckedChanged" />
                                                                        <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>



                                                        <div class="col-sm-3">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Category
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtcat" runat="server" TabIndex="6" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtcat_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtncategory" CausesValidation="false" runat="server" OnClick="lbtncategory_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div id="caldvw34" class="col-sm-3 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chkcat" Text="All" AutoPostBack="true" OnCheckedChanged="chkcat_CheckedChanged" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-8">

                                            <div class="panel panel-default">
                                                <div class="panel-heading pannelheading paddingtopbottom0">
                                                    <strong>Limits of Sold Quantity</strong>
                                                </div>

                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-4">
                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1" style="width:61px">
                                                                    Qty Limit
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-6" style="width:123px">
                                                                        <asp:TextBox Width="72.25px" ID="txtqtylimit" runat="server" onkeydown="return jsDecimals(event);" TabIndex="7" AutoPostBack="true" OnTextChanged="txtqtylimit_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                    <div id="cal3dvw" class="col-sm-2 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chkqty" Text="All" AutoPostBack="true" OnCheckedChanged="chkqty_CheckedChanged" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    No of Items
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox Width="72.25px" ID="txtnoofitems" onkeydown="return jsDecimals(event);" runat="server" TabIndex="8" AutoPostBack="true" OnTextChanged="txtnoofitems_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                    <div id="cal33dvw" class="col-sm-2 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chknumitm" Text="All" AutoPostBack="true" OnCheckedChanged="chknumitm_CheckedChanged" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Classification
                                                                </div>

                                                                <div>
                                                                    <div class="col-sm-5">
                                                                        <%--    <asp:TextBox ID="txtclassific" runat="server" TabIndex="5" AutoPostBack="true" OnTextChanged="txtclassific_TextChanged" CssClass="form-control"></asp:TextBox>--%>
                                                                        <asp:DropDownList ID="txtclassific" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Text="A"></asp:ListItem>
                                                                            <asp:ListItem Text="B"></asp:ListItem>
                                                                            <asp:ListItem Text="C"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div id="caldvw4" class="col-sm-3 paddingLeft0">
                                                                        <asp:CheckBox runat="server" ID="chkclasific" Text="All" AutoPostBack="true" OnCheckedChanged="chkclasific_CheckedChanged" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-1">

                                            <asp:LinkButton ID="lbtnsearchrec" runat="server" TabIndex="10" OnClick="lbtnsearchrec_Click">
                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                            </asp:LinkButton>

                                        </div>

                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading pannelheading paddingtopbottom0">
                                                    Details
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <%--<div class="panelscoll150">--%>
                                                                <div class="dvScroll" onscroll="setScrollPosition(this.scrollTop);" style="height: 135px; overflow: scroll">
                                                                    <asp:GridView ID="gvalldetails" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnRowDataBound="gvalldetails_RowDataBound" OnSelectedIndexChanged="gvalldetails_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnageview" OnClick="lbtnageview_Click" runat="server"><span class="glyphicon glyphicon-book">Age</span></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitm" runat="server" Text='<%# Bind("iti_itm_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("mi_shortdesc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Brand">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbrand" runat="server" Text='<%# Bind("mi_brand") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Main Category">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("mi_cate_1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="ABC Classification">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblabc" runat="server" Text='<%# Bind("clasification") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty Sold">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("sold_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Inhand Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblinhand" runat="server" Text='<%# Bind("inhand_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText=">90">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lb90val" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText=">120">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lb120val" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText=">170">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lb170val" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText=">270">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lb270val" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText=">365">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lb365val" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Avg">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbavgval" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CL1" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div class="oprefnotxtboxDUP">
                                                                                        <asp:Label ID="lblcl1" runat="server" Text='<%# Bind("rags_slot_l1","{0:n}") %>' Width="170px" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CL2" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div class="oprefnotxtboxDUP">
                                                                                        <asp:Label ID="lblcl2" runat="server" Text='<%# Bind("rags_slot_l2","{0:n}") %>' Width="180px" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CL3" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div class="oprefnotxtboxDUP">
                                                                                        <asp:Label ID="lblcl3" runat="server" Text='<%# Bind("rags_slot_l3","{0:n}") %>' Width="180px" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CL4" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div class="oprefnotxtboxDUP">
                                                                                        <asp:Label ID="lblcl4" runat="server" Text='<%# Bind("rags_slot_l4","{0:n}") %>' Width="180px" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CL5" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div class="oprefnotxtboxDUP">
                                                                                        <asp:Label ID="lblcl5" runat="server" Text='<%# Bind("rags_slot_g1","{0:n}") %>' Width="180px" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Avg Inventory Days" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblavginv" runat="server" Text='<%# Bind("avg_inv_days") %>' Width="130px" Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;% of Total Inventory Value" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <asp:Label ID="lblinvvaluepercen" runat="server" Text='<%# Bind("percentage_inv_val") %>' Width="160px" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                        </Columns>

                                                                        <SelectedRowStyle BackColor="Silver" />

                                                                    </asp:GridView>
                                                                </div>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--                                        <div class="col-sm-2">
                                            <h1 style="font-size:small;font-weight:bolder">Aging Breakup</h1>
                                            <div class="row">
                                                <div class="col-sm-3">  <asp:Label ID="lb90" runat="server" Text=" >90" ></asp:Label></div>
                                                <div class="col-sm-3">    <asp:Label ID="lb90val" runat="server"   ></asp:Label></div>
                                            </div>
                                            <div class="row">
                                                 <div class="col-sm-3"> <asp:Label ID="lb120" runat="server" Text=">120" ></asp:Label></div>
                                                <div class="col-sm-3">    <asp:Label ID="lb120val" runat="server"  ></asp:Label></div>
                                            </div>
                                            <div class="row">
                                                    <div class="col-sm-3">     <asp:Label ID="lb170" runat="server" Text=">170" ></asp:Label></div>
                                                <div class="col-sm-3">  <asp:Label ID="lb170val" runat="server"  ></asp:Label></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">       <asp:Label ID="lb270" runat="server" Text=">270" ></asp:Label></div>
                                                <div class="col-sm-3">    <asp:Label ID="lb270val" runat="server"  ></asp:Label></div>
                                            </div>
                                            <div class="row">
                                                  <div class="col-sm-3">         <asp:Label ID="lb365" runat="server" Text=">365" ></asp:Label></div>
                                                <div class="col-sm-3">      <asp:Label ID="lb365val" runat="server"  ></asp:Label></div>
                                            </div>
                                            <div class="row">
                                                  <div class="col-sm-3">         <asp:Label ID="lbavg" runat="server" Text="Avg" ></asp:Label></div>
                                                <div class="col-sm-3">      <asp:Label ID="lbavgval" runat="server"  ></asp:Label></div>
                                            </div>
                                        </div>--%>

                                        <div class="col-sm-12">

                                            <div class="panel panel-default">

                                                <div class="panel-heading pannelheading paddingtopbottom0">
                                                    <p style="font-weight: bolder">Balance Breakup</p>
                                                    <asp:RadioButtonList ID="RBordby" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBordby_SelectedIndexChanged">
                                                        <asp:ListItem Text="Order by Document Date &nbsp;&nbsp;" Value="3" />
                                                        <asp:ListItem Text="Order by Expiry Date &nbsp;&nbsp;" Value="1" />
                                                        <asp:ListItem Text="Order by Expiry Date & Location" Value="2" />
                                                    </asp:RadioButtonList>
                                                    <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click" OnClientClick="ExcelDownloadConfirm();">
                                                        <span class="glyphicon glyphicon-download-alt" aria-hidden="true">Excel Download</span>
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-12">

                                                            <div class="panelscoll1">

                                                                <asp:GridView ID="gvorderbydetails" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Location">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblloc" runat="server" Text='<%# Bind("inb_loc") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Doc No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldoc" runat="server" Text='<%# Bind("inb_doc_no") %>' Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Batch No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblbatch" runat="server" Text='<%# Bind("inb_batch_no") %>' Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Bind("inb_doc_dt", "{0:dd/MMM/yyyy}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Expiry Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblexpirydate" runat="server" Text='<%# Bind("exp_date", "{0:dd/MMM/yyyy}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qty">
                                                                            <ItemTemplate>
                                                                                <div>
                                                                                    <asp:Label ID="lblqtyorderby" runat="server" Text='<%# Bind("inb_qty","{0:n}") %>' Width="100px"></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Right" />
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

                                    <asp:Label ID="Label1" runat="server" Text="" Visible="False"></asp:Label>
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
