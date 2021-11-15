<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DGCAccountSettlements.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Imports.DGC_Account_Settlements.DGCAccountSettlements" %>

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
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
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
        .ammountAlign
        {
            text-align:right;
           padding-right:36px;
        }
        .headerAlign{
            text-align:left;
           
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
            <asp:HiddenField ID="txtcancel" runat="server" />


            <div class="panel panel-default marginLeftRight5">

                <div class="row">
                    <div class="col-sm-2  buttonRow"> 
                      <div class="col-sm-6">Dealer Entries</div>
                        <div class="col-sm-2">  <asp:CheckBox ID="chkdealer" AutoPostBack="true" OnCheckedChanged="chkdealer_CheckedChanged" runat="server" /></div>
                                   
                    </div>
                     <div class="col-sm-2  buttonRow"> 
                      <div class="col-sm-4">Excel</div>
                        <div class="col-sm-2">  <asp:CheckBox ID="chkexcel" AutoPostBack="true"  runat="server" /></div>
                                   
                    </div>
                    <div class="col-sm-4  buttonrow">
                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">

                            <strong>Info!</strong>
                            <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            <asp:Label ID="lblH1" runat="server" Text=""></asp:Label>

                        </div>
                    </div>

                    <div class="col-sm-4  buttonRow">

                         <div class="col-sm-3">
                            <asp:LinkButton ID="lbtnprint" runat="server" CssClass="floatRight" OnClick="lbtnprint_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Print
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="lbtnsave" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="lbtnsave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Settle
                            </asp:LinkButton>
                        </div>

                         <div class="col-sm-3 ">
                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClientClick="ConfirmCancelReq();" OnClick="LinkButton1_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel 
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 ">
                            <asp:LinkButton ID="lbuttonclear" CausesValidation="false" runat="server" OnClientClick="ConfirmClearForm();" OnClick="lbuttonclear_Click">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear 
                            </asp:LinkButton>
                        </div>

                       

                    </div>

                </div>
                 <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                  <div class="panel-heading"><strong>  DGC Account Settlements</strong> </div>
                <div class="row">

                    <div class="panel-body">

                       

                        <div class="col-sm-6">

                            <div class="panel panel-default">
                               
                                <div class="panel-heading pannelheading ">
   
                                            DGC A/C Information
 
                                


       
                                </div>
                              

                                <div class="panel-body" id="panelbodydivd">

                                    <div class="panelscollbar">

                                        <asp:GridView ID="gvDGCAcc" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                            <Columns>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chlselectacc" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Acc No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblaccno" runat="server" Text='<%# Bind("mda_acc_no") %>' Width="1px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Facility Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfacilityamt" runat="server" Text='<%# Bind("mda_fac_amt","{0:n2}") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                               <asp:label ID="UtilityAmount" runat="server" />
                                                           </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblutilamt" runat="server" Text='<%# Bind("mda_used_amt","{0:n2}") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                               <asp:label ID="Balance" runat="server" />
                                                           </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbalancedgc" runat="server" Text='<%# Bind("Balance","{0:n2}") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>

                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="col-sm-1"></div>
                         <div class="col-sm-5">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Find Settlement
                                </div>

                                <div class="panel-body" id="panelbodydiv">

                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:RadioButton runat="server" GroupName="selectprint" ID="radprint" Checked="true"/>
                                            </div>
                                            <div class="col-sm-4 labelText1">
                                                Settlement #
                                            </div>

                                            <div class="col-sm-6 paddingRight5">
                                                <asp:TextBox ID="txtsettlemntno" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnsettlemnt" runat="server" TabIndex="1" OnClick="lbtnsettlemnt_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Date
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                        PopupButtonID="lbtnassmentdate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div class="col-sm-1" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnassmentdate" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                     <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:RadioButton runat="server" GroupName="selectprint" ID="datprintexel"/>
                                            </div>
                                            <div class="col-sm-4 labelText1">
                                                From Date
                                            </div>

                                            <div class="col-sm-6 paddingRight5">
                                                <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                             <div class="col-sm-1 paddingLeft0">
                            <asp:LinkButton ID="lbtnSDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                            </asp:LinkButton>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfdate"
                                PopupButtonID="lbtnSDate" Format="dd/MMM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                                        </div>

                                    </div>
                                     <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                To Date
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txttdate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                              <div class="col-sm-1 paddingLeft0">
                            <asp:LinkButton ID="lbtnEDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                            </asp:LinkButton>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttdate"
                                PopupButtonID="lbtnEDate" Format="dd/MMM/yyyy">
                            </asp:CalendarExtender>
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
                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-6">

                            <div class="panel panel-default" >

                                <div class="panel-heading pannelheading ">
                                    Pending Assessment Requests 
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12" >

                                            <div class="panelscoll" style="height:150px">

                                                <asp:GridView ID="gvpendingreq" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="seqno" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseqno" runat="server" Text='<%# Bind("isth_seq_no") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Assessment Req #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassreqno1" runat="server" Text='<%# Bind("isth_doc_no") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate1" runat="server" Text='<%# Bind("isth_dt", "{0:dd/MMM/yyyy}") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                           <HeaderTemplate>
                                                               <asp:label ID="lblCurrCostCode" runat="server" />
                                                           </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblamount1" runat="server" Text='<%# Bind("isth_stl_amt","{0:n2}") %>' Width="100px" CssClass="ammountAlign"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkselect" />
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

                        <div class="col-sm-1" style="margin-top:50px">

                            <asp:LinkButton ID="lbtnaddentry" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnaddentry_Click">
                            <span class="glyphicon glyphicon-arrow-right fontsize20 paddingRight15" aria-hidden="true"></span> 
                            </asp:LinkButton>

                        </div>

                      

                          <div class="col-sm-5">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Assessment Requests
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll1">

                                                <asp:GridView ID="gvrequestheader" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="gvrequestheader_SelectedIndexChanged">

                                                    <Columns>

                                                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                        <asp:TemplateField HeaderText="lineno" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllineno" runat="server" Text='<%# Bind("isdt_line") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="seqno" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseqno2" runat="server" Text='<%# Bind("isth_seq_no") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Assessment Req #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassreqno2" runat="server" Text='<%# Bind("isth_doc_no") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate2" runat="server" Text='<%# Bind("isth_dt", "{0:dd/MMM/yyyy}") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                               <asp:label ID="Total" runat="server" />
                                                           </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltot" runat="server" Text='<%# Bind("isth_stl_amt","{0:n2}") %>' Width="100px" CssClass="ammountAlign"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                         <SelectedRowStyle BackColor="Silver" />
                                                </asp:GridView>

                                            </div>


                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm-12 height10">
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="row">

                                    <div class="col-sm-5 labelText1">
                                        Settle Amount<asp:label ID="Currcom1" runat="server" />
                                    </div>

                                    <div class="col-sm-7 paddingRight5">
                                        <asp:TextBox ID="txtsettleamount" BorderStyle="None" ReadOnly="true" Style="text-align: right" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-6">
                                <div class="row">

                                    <div class="col-sm-4 labelText1">
                                        Balance<asp:label ID="Currcom2" runat="server" />
                                    </div>

                                    <div class="col-sm-8 paddingRight5">
                                        <asp:TextBox ID="txfinalbalance" runat="server" Style="text-align: right" BorderStyle="None" ReadOnly="true" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>


                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-6">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Request Assessment Details 
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll1">

                                                <asp:GridView ID="gvreqdetails" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="gvreqdetails_SelectedIndexChanged">

                                                    <Columns>

                                                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                        <asp:TemplateField HeaderText="Assessment #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassreqno3" runat="server" Text='<%# Bind("istd_assess_no") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bond No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbondno" runat="server" Text='<%# Bind("istd_entry_no") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Duty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblduty" runat="server" Text='<%# Bind("istd_cost_ele") %>' Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <%--<asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassreqno3" runat="server" Text='<%# Bind("istd_assess_no") %>' Width="100px" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField>
                                                             <HeaderTemplate>
                                                               <asp:label ID="Amount2" runat="server" />
                                                           </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblamountfinal" runat="server" Text='<%# Bind("istd_cost_stl_amt","{0:n2}") %>' Width="100px" CssClass="ammountAlign"></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                            <HeaderStyle CssClass="headerAlign" />
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

                        <div class="col-sm-1"></div>

                        <div class="col-sm-5">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    AOD Receipt Details
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll" style="height:125px">

                                                <asp:GridView ID="gvaod" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                         

                                                        <asp:TemplateField HeaderText="AOD #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaodno" runat="server" Text='<%# Bind("AOD_NO") %>' Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AOD Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaoddate" runat="server" Text='<%# Bind("AOD_DATE", "{0:dd/MMM/yyyy}") %>' Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AOD Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaodtype" runat="server" Text='<%# Bind("AOD_TYPE") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfrom" runat="server" ToolTip='<%# Bind("FromlocDes") %>' Text='<%# Bind("FROM_LOCATION") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="To Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblto" runat="server" ToolTip='<%# Bind("TolocDes") %>' Text='<%# Bind("TO_LOCATION") %>' Width="80px"></asp:Label>
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
    </asp:Panel>


</asp:Content>
