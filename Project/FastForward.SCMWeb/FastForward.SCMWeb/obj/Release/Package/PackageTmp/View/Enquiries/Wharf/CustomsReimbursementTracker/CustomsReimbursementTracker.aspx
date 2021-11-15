<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CustomsReimbursementTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Imports.Customs_Reimbursement_Tracker.CustomsReimbursementTracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script type="text/javascript">
        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
            }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
       
        function pageLoad() {
            //consol.log('Page Load');
            //maintainScrollPosition();
        }
        function ConfirmClearForm() {
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmByPass() {
            var selectedvalueclear = confirm("Do you want to bypass this bond to customs reimbursement ?");
            if (selectedvalueclear) {
                document.getElementById('<%=hidbypass.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hidbypass.ClientID %>').value = "No";
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
                alert("Maximum characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
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

        .panel {
            margin-bottom: 0px;
            margin-top: 0px;
            padding-bottom: 0px;
            padding-top: 0px;
        }

        .panel-heading {
            height: 21px;
        }
    </style>
    <style>
         .gventrydetails th{
            border-left: 2px solid ;
            border-left-color: darkgray;
      
        }
        .gventrydetails th:nth-child(1) {
             border-left: none ;
        }
        .gventrydetails th:nth-child(2) {
             border-left: none ;
        }
        .gventrydetails th:nth-child(10) {
             border-left: none ;
        }
        .gventrydetails th:nth-child(14) {
             border-left: none ;
        }
        .gventrydetails th:nth-child(16) {
             border-left: none ;
        }

        .gvaodout th{
            border-left: 2px solid ;
            border-left-color: darkgray;
      
        }
        .gvaodout th:nth-child(1) {
             border-left: none ;
        }
        .gvaodout th:nth-child(6) {
             border-left: none ;
        }

        .gvaodin th{
            border-left: 2px solid ;
            border-left-color: darkgray;
      
        }
        .gvaodin th:nth-child(1) {
             border-left: none ;
        }
        .gvaodin th:nth-child(6) {
             border-left: none ;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanelMain">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="hidbypass" runat="server" />


            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-7  buttonrow">
                        <%-- <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">

                            <strong>Info!</strong>
                            <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>

                        </div>--%>
                    </div>

                    <div class="col-sm-5  buttonRow" style="height: 25px;">

                        <div class="col-sm-8 paddingRight0">
                            <asp:LinkButton ID="lbtnsave" runat="server" Visible="false" CssClass="floatRight" OnClientClick="ConfirmSave();">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Settle
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight15">
                            <asp:LinkButton ID="lbuttonclear" Visible="false" CausesValidation="false" runat="server" OnClientClick="ConfirmClearForm();">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear 
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <div style="margin-top: -3px;">

                                <asp:LinkButton ID="lbtnclear" CausesValidation="false" runat="server" OnClientClick="ConfirmClearForm();" OnClick="lbtnclear_Click">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">

                            <div class="panel-heading pannelheading ">
                                <strong>Customs Reimbursement Tracker</strong>
                            </div>

                            <div class="panel panel-body padding0">

                                <div class="col-sm-4 padding0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading pannelheading ">
                                            <strong>Search By Date</strong>
                                        </div>
                                        <div class="panel panel-body padding0" id="panelbodydiv">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-11 paddingRight0">
                                                        <div class="row">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-2 labelText1 ">
                                                                    From
                                                                </div>
                                                                <div class="col-sm-6 paddingRight0">
                                                                    <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                                        PopupButtonID="lbtnassmentdate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                    <asp:LinkButton ID="lbtnassmentdate" TabIndex="1" CausesValidation="false" runat="server">
                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-2 labelText1 ">
                                                                    To
                                                                </div>
                                                                <div class="col-sm-6 paddingRight0">
                                                                    <asp:TextBox ID="dtpToDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpToDate"
                                                                        PopupButtonID="lbtntodate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                    <asp:LinkButton ID="lbtntodate" TabIndex="2" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-2 labelText1 ">
                                                                    Status
                                                                </div>
                                                                <div class="col-sm-6 paddingRight0">
                                                                    <asp:DropDownList ID="ddlstus" runat="server" CssClass="form-control" TabIndex="3" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">All</asp:ListItem>
                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <div class="buttonRow">
                                                            <asp:LinkButton ID="lbtnperiod" runat="server" TabIndex="4" OnClick="lbtnperiod_Click">
                                                                <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-8 padding0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading pannelheading ">
                                            <strong>Search By Details</strong>
                                        </div>
                                        <div class="panel panel-body" id="panelbodydivd">
                                            <div class="col-sm-11 padding0">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0 labelText1">
                                                            Entry #
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtentryno" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtentryno_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" TabIndex="5" OnClick="LinkButton1_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 labelText1">
                                                            AST #
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtastno" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtastno_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="LinkButton2" runat="server" TabIndex="6" OnClick="LinkButton2_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 labelText1">
                                                            STL #
                                                        </div>

                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtstlno" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtstlno_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" TabIndex="7" OnClick="LinkButton3_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0 labelText1">
                                                            Cusdec Entry No
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtcustomref" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtcustomref_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="LinkButton4" runat="server" TabIndex="8" OnClick="LinkButton4_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 labelText1">
                                                            Ass.Notice #
                                                        </div>

                                                        <div class="col-sm-7 paddingRight5">
                                                            <asp:TextBox ID="txtassesnoticno" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtassesnoticno_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="LinkButton5" runat="server" TabIndex="9" OnClick="LinkButton5_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1 padding0">
                                                <div class="buttonRow">
                                                    <asp:LinkButton ID="LinkButton9" runat="server" TabIndex="10" OnClick="LinkButton9_Click">
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
                </div>
                <div class="row">
                    <div class="col-sm-12">


                        <div class="panel panel-default">

                            <div class="panel-heading pannelheading ">
                                <strong>Cusdec Entry Details</strong>
                            </div>

                            <div class="panel panel-body padding0">

                                <div class="row">
                                    <div class="col-sm-12">

                                     <div class="dvScroll" style="height: 230px; overflow-y: scroll;" onscroll="setScrollPosition(this.scrollTop);">
                                        <%--<div class="panelscollbar">--%>
                                            <asp:HiddenField ID="hfScrollPosition"  Value="0" runat="server" />
                                            <asp:GridView ID="gventrydetails" AutoGenerateColumns="False" runat="server" 
                                                
                                                CssClass="table table-hover table-striped gventrydetails" 
                                                GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." 
                                                OnSelectedIndexChanged="gventrydetails_SelectedIndexChanged" OnRowDataBound="gventrydetails_RowDataBound">

                                                <Columns>

                                                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" >

                                                    <HeaderStyle Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>

                                                    <asp:TemplateField HeaderText="Entry #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblentryno" runat="server" Text='<%# Bind("cuh_doc_no") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="75px" />
                                                        <HeaderStyle Width="75px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldate1" runat="server" Text='<%# Bind("cuh_dt", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Custom Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcustomref" runat="server" Text='<%# Bind("cuh_cusdec_entry_no") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="75px" />
                                                        <HeaderStyle Width="75px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AST #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblastno" runat="server" Text='<%# Bind("cuh_ast_no") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle Width="100px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblastdate" runat="server" Text='<%# Bind("cuh_ast_dt", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ass.Notice #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblassnono" runat="server" Text='<%# Bind("cuh_ast_noties_no") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="75px" />
                                                        <HeaderStyle Width="75px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblassnoticedate" runat="server" Text='<%# Bind("Ass_Note_Date", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label Text="Ass. Amt" ID="lblAssAmt" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="lblassamt" runat="server" Text='<%# Bind("Assessment_Amount","{0:N2}") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="70px"/>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" Width="70px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTemp2" runat="server" Text='' Width="5px"></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle Width="5px" />
                                                        <HeaderStyle Width="5px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="STL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstlno" runat="server" Text='<%# Bind("cuh_stl_no") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle Width="100px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstldate" runat="server" Text='<%# Bind("cuh_stl_dt", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label Text="STL Amt" runat="server" ID="lblSetAmt" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsettleamt" runat="server" Text='<%# Bind("Settle_Amount","{0:N2}") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="70px"/>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" Width="70px"/>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTemp1" runat="server" Text='' Width="5px"></asp:Label>
                                                        </ItemTemplate>
                                                           <ItemStyle Width="5px" />
                                                        <HeaderStyle Width="5px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AST Stus" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblassstus" runat="server" Text='<%# Bind("cuh_ast_stus") %>' Width="1px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ignored" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblignored" runat="server" Text='<%# Bind("cuh_ast_ignore") %>' Width="1px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bypass Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbypassstus" runat="server" Text='<%# Bind("BypassStatus") %>' Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnbypass" Width="20px" runat="server" CausesValidation="false" OnClientClick="ConfirmByPass();" OnClick="lbtnbypass_Click">
                                                                                                <span aria-hidden="true" class="glyphicon glyphicon-share-alt"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridHeaderAlignCenter" Width="20px" />
                                                        <HeaderStyle CssClass="gridHeaderAlignCenter" Width="20px"/>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>

                                        <%--</div>--%>


                                    </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">

                        <div class="panel panel-body padding0">
                             <div class="col-sm-2" style="padding-left: 1px; padding-right: 1px;">
                                <div class="panel panel-default">
                                    <div class="panel-heading paddingtopbottom0">
                                        <strong>Cusdec Entry Details</strong>
                                    </div>
                                    <div class="panel-body" style="height: 150px;">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="grdentrydetails" CssClass="table table-hover table-striped tblTest" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Item">
                                                      <ItemTemplate>
                                                            <asp:Label ID="lbITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbITRI_QTY" runat="server" Text='<%# Bind("ITRI_QTY") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Bal Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbITRI_BQTY" runat="server" Text='<%# Bind("ITRI_BQTY") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 </Columns>
                                                </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 padding0">

                                <div class="panel panel-default">

                                    <div class="panel-heading pannelheading ">
                                        <strong>Outward Entry Details</strong>
                                    </div>

                                    <div class="panel panel-body padding0">

                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="" style="height:150px; overflow-y:auto;">

                                                    <asp:GridView ID="gvaodout" AutoGenerateColumns="false" runat="server" CssClass="gvaodout table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                        <Columns>
                                                                 <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtngrnsearch" CausesValidation="false" runat="server"  OnClick="lbtngrnsearch_Click">   
                                                        <span class="glyphicon glyphicon-ban-circle" aria-hidden="true"></span>                                                                   
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaodoutno" runat="server" Text='<%# Bind("aod_no") %>' Width="170px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Location">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloutloc" ToolTip='<%# Bind("FROM_LOC_DESC") %>' runat="server" Text='<%# Bind("from_location") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloutdate" runat="server" Text='<%# Bind("aod_date", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Other Loc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="to_location" runat="server" ToolTip='<%# Bind("TO_LOC_DESC") %>' Text='<%# Bind("to_location") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                              <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbitem" runat="server"  Text='<%# Bind("Item") %>' Width="60px" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloutqty" runat="server" Text='<%# Bind("qty") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTemp" runat="server"  Text='' Width="10px"></asp:Label>
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

                            <div class="col-sm-5 padding0">

                                <div class="panel panel-default">

                                    <div class="panel-heading pannelheading ">
                                        <strong>Inward Entry Details</strong>
                                    </div>

                                    <div class="panel panel-body padding0">

                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="" style="height:150px; overflow-y:auto;">

                                                    <asp:GridView ID="gvaodin" AutoGenerateColumns="false" runat="server" CssClass="gvaodin table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaodinno" runat="server" Text='<%# Bind("aod_no") %>' Width="170px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Location">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinloc" ToolTip='<%# Bind("FROM_LOC_DESC") %>' runat="server" Text='<%# Bind("from_location") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblindate" runat="server" Text='<%# Bind("aod_date", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Other Loc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinotherloc" runat="server" ToolTip='<%# Bind("TO_LOC_DESC") %>' Text='<%# Bind("to_location") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinqty" runat="server" Text='<%# Bind("qty") %>' Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Oth Doc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblothaodinno" runat="server" Text='<%# Bind("OTH_DOC") %>' Width="170px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTemp" runat="server"  Text='' Width="10px"></asp:Label>
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

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

    
    <asp:UpdatePanel runat="server" ID="pnlUpdate">
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
                <div class="panel-heading" style="height:25px;">
                    
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1"><asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    </div>
                </div>
                <div class="panel panel-body">
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
                                    <div id="resultgrd" class=" POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
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

<%-- serch pop with date time --%>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upPnl1">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Panel1" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height350 width700">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="height:26px;">

                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnClosePop" OnClick="lbtnClosePop_Click" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
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
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFDate"
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
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSerByDate" runat="server" OnClick="lbtnSerByDate_Click">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlDateSearch" runat="server" class="form-control">
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
                                            <asp:TextBox ID="txtDateSearch" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtDateSearch_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnDateSearch" runat="server" OnClick="lbtnDateSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtDateSearch" />
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
                                    <div class="col-sm-12" style="height:250px;">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="dgvDateSearch" CausesValidation="false" runat="server" GridLines="None"
                                                    CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager"
                                                    AllowPaging="True" OnSelectedIndexChanged="dgvDateSearch_SelectedIndexChanged" OnPageIndexChanging="dgvDateSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="upPnl1">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait1" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait1" runat="server"
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
     <script>
         if (typeof jQuery == 'undefined') {
             alert('jQuery is not loaded');
         }
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
