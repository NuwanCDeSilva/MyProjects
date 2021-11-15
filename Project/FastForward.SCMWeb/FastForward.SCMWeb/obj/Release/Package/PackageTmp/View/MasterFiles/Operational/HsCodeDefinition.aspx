<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="HsCodeDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.HsCodeDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };
        function ConfirmPlaceOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdfConfirmSave.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfConfirmSave.ClientID %>').value = "No";
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
    <style>
        .gridHeaderAlignRight {
            text-align: right;
        }

        .gridHeaderAlignLeft {
            text-align: left;
        }

        .gridHeaderAlignCenter {
            text-align: center;
        }
    </style>
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

        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function ValidWPrice(evt, textBox) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // alert(charCode);
            if (Number(textBox.value) < 1000000) {
                if ((charCode == 46) || (charCode == 8) || (charCode == 110) || (charCode == 39) || (charCode == 37) || (charCode == 13)) {
                    return true;
                }
                else if ((charCode < 106 && charCode > 95)) {
                    return true;
                }
                else {
                    return false;
                }
            } else {
                // var va = 
                textBox.value = textBox.value.slice(0, -1);
                //alert(textBox.value);
                alert('Maximum weight price allowed is 999,999.00 !');
                return false;
            }
        }
    </script>
    <style>
        .panel {
            margin-bottom: 2px;
        }
    </style>
   
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
    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdfConfirmSave" runat="server" Value="0" />
             <asp:HiddenField ID="hdfClearData" runat="server" Value="0" />
            <div class="row">
                <%--Main Div--%>
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-body" style="padding-bottom:1px;padding-top:1px;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row buttonRow" id="HederBtn">
                                        <div class="col-sm-12 col-md-12">
                                            <div class="col-md-10"></div>
                                            <div class="col-md-1 paddingRight0">
                                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" OnClientClick="ConfirmPlaceOrder();" CssClass="floatRight" OnClick="lbtnSave_Click"> 
                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save </asp:LinkButton>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click"> 
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12" style="padding-left:11px; padding-right:11px;padding-bottom:0px;padding-top:0px;">
                                    <div class="panel panel-default" >
                                        <div class="panel panel-heading" style="padding-top: 1px; padding-bottom: 1px;">
                                            <strong><b> HS Code Definition </b></strong>
                                        </div 
                                        <div class="panel panel-body">
                                            <%--Div 1--%>
                                            <div class="row">
                                                <%--<div class="col-md-12">
                                                    <%--<div class="col-md-12">--%>
                                                <div class="col-md-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-6 padding0">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 labelText1">
                                                                                    HS Code
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtHsCode" CssClass="form-control" OnTextChanged="txtHsCode_TextChanged"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                    <asp:LinkButton ID="lbtnSeHsCode" CausesValidation="false" runat="server" OnClick="lbtnSeHsCode_Click">
                                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <div class="col-md-5">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 labelText1">
                                                                                    HS Description
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox runat="server" ID="txtHsDes" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 labelText1">
                                                                                    Entry Type
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlEntryType" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEntryType_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-6" style="vertical-align:middle;">
                                                                                    <div style="padding-top:2px;">
                                                                                    <asp:Label Text="" ID="lblEntryTy" runat="server" />
                                                                                        </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6 padding0">
                                                                        <div class="row">

                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 labelText1">
                                                                                    From Country
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <asp:TextBox runat="server" Text="DEF" AutoPostBack="true" ID="txtFromCountry" CssClass="form-control" OnTextChanged="txtFromCountry_TextChanged"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-7">
                                                                                    <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                        <asp:LinkButton ID="lbtnSeFromCountry" CausesValidation="false" runat="server" OnClick="lbtnSeFromCountry_Click">
                                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-md-11 padding0" style="padding-top:2px;">
                                                                                        <asp:Label runat="server" ID="lblFromCountry" CssClass="labelText1"></asp:Label>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 labelText1">
                                                                                    To Country
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <asp:TextBox runat="server" ID="txtToCountry" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                                </div>

                                                                                <div class="col-md-7">
                                                                                    <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                    </div>
                                                                                     <div class="col-md-11 padding0" style="padding-top:2px;">
                                                                                    <asp:Label runat="server" ID="lblToCountry" CssClass="labelText1"></asp:Label>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 labelText1">
                                                                                    <%--Status--%>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                  <%--  <asp:DropDownList runat="server" Visible="false" ID="ddlStatus" CssClass="form-control" AppendDataBoundItems="True">
                                                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Inactive</asp:ListItem>
                                                                                    </asp:DropDownList>--%>
                                                                                </div>

                                                                                <div class="col-md-5">
                                                                                    <asp:Label runat="server" ID="Label1" CssClass="labelText1"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                           

                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--</div>--%>
                                                <%--</div>--%>
                                            </div>
                                            <%--Div 2--%>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-8" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading" style="padding-top: 1px; padding-bottom: 1px;">
                                                                <strong><b>Add/Edit Duty</b></strong>
                                                            </div>
                                                            <div class="panel-body" style="height: 186px;padding-top:2px;">
                                                                <div class="panel panel-default" style="padding-left:0px; padding-right:0px;padding-bottom:0px;padding-top:0px;margin-left: -5px;">
                                                                    <div class="panel panel-heading" style="padding-left:0px; padding-right:0px;padding-bottom:0px;padding-top:0px; height:21px;margin-bottom:0px;">
                                                                        <table>
                                                                            <tr style="">
                                                                                <th scope="col" style="width:90px;padding-left:5px;padding-top:2px;">Type</th>
                                                                                <th scope="col" style="width:235px;padding-left:3px;padding-top:2px;">Description</th>
                                                                                <th scope="col" style="width:76px;padding-top:2px;text-align:left;">Rate</th>
                                                                               <th scope="col" style="width:143px;padding-top:2px;text-align:left;">Unit Price(
                                                                                    <asp:Label ID="lblUCur" Text="" runat="server" /> 
                                                                                        )</th>
                                                                                <th scope="col" style="width:140px;padding-top:2px;text-align:left;">Weight Price(
                                                                                    <asp:Label ID="lblWCurr" Text="" runat="server" /> )
                                                                                </th>
                                                                                <th scope="col" style="width:82px;padding-left:0px;text-align:left;">MP (Status)</th>
                                                                                <th scope="col" style="width:80px;padding-left:0px;text-align:left;">Act./Inact.</th>
                                                                                 <th scope="col" style="width:140px;padding-top:2px;text-align:left;">CCE
                                                                                    <asp:Label ID="lbtcc" Text="" runat="server" /> 
                                                                                </th>
                                                                                 <th scope="col" style="width:140px;padding-top:2px;text-align:left;">CCE CLM
                                                                                    <asp:Label ID="lblccclm" Text="" runat="server" /> 
                                                                                </th>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div style="max-height: 150px; overflow: auto;">
                                                                    <asp:GridView ID="dgvAddDuty" ShowHeader="False" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="3" OnRowDataBound="dgvAddDuty_RowDataBound">
                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                        <EmptyDataTemplate>
                                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                                <tbody>
                                                                                    <tr>
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
                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAdType" runat="server" Text='<%# Bind("Code") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAdDescription" runat="server" Text='<%# Bind("Description") %>' Width="160px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAdRate" Text='<%# Bind("Rate", "{0:N2}") %>' 
                                                                                          CssClass="txtRate form-control text-right" runat="server" Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Unit Price">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAdUnitPrice" Text='<%# Bind("UnitPrice", "{0:N2}") %>' 
                                                                                        CssClass="txtUnitPrice form-control text-right" runat="server" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Weight Price">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAdWeightPrice" Text='<%# Bind("WeightPrice", "{0:N2}") %>'   CssClass="txtWeightPrice form-control text-right" runat="server" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="">
                                                                                 <ItemTemplate>
                                                                                    <asp:Label ID="Label2" Visible="true" runat="server"  Width="10px"></asp:Label>
                                                                                    <asp:Label ID="lblAdMp" Text='<%# Bind("MP") %>' Visible="false" runat="server"  Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MP">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox Width="50px" class="height20" ID="ddlAdMp" runat="server">
                                                                                    </asp:CheckBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle/>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                 <ItemTemplate>
                                                                                    <asp:Label ID="lblDutyActive" Text='<%# Bind("mhc_act") %>' Visible="false" runat="server"  Width="10px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active/Inactive">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox Width="50px" class=" height20" ID="chkDutyActive" runat="server">
                                                                                    </asp:CheckBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle />
                                                                            </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="CCE">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtcc" Text='<%# Bind("mhc_cce") %>'   CssClass="txtWeightPrice form-control text-right" runat="server" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CCE CLM">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtccclm" Text='<%# Bind("mhc_cce_clm") %>'   CssClass="txtWeightPrice form-control text-right" runat="server" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                    </asp:GridView>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading" style="padding-top: 1px; padding-bottom: 0px;">
                                                                <strong><b>Country Wise Break Up</b></strong>
                                                            </div>
                                                            <div class="panel panel-body" style="height:184px;padding-top:2px;">
                                                                <div class="panel panel-default" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; margin-left: -5px;">
                                                                    <div class="panel panel-heading" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; height: 21px; margin-bottom: 0px;">
                                                                        <table>
                                                                            <tr style="">
                                                                                <th scope="col" style="width: 46px; padding-left: 5px; padding-top: 2px;"></th>
                                                                                <th scope="col" style="width: 145px; padding-left: 5px; padding-top: 2px;">From Country</th>
                                                                                <th scope="col" style="width: 150px; padding-left: 3px; padding-top: 2px;">To Country</th>
                                                                                <th scope="col" style="width: 150px;padding-left: 5px; padding-top: 2px; text-align: left;">Entry Type</th>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div style="max-height: 186px; overflow: auto;">
                                                                    <asp:GridView ShowHeader="false" ID="dgvCountryBU" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3">
                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                        <EmptyDataTemplate>
                                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>No records found.
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="">
                                                                                        <asp:LinkButton ID="lbtnEditDuty" runat="server" CausesValidation="false"
                                                                                            OnClick="lbtnEditDuty_Click">
                                                                                       Select
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="From Country">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCoFromCountry" Text='<%# Bind("MHC_FRM_CNTY") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="To Country">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCoToCountry" Text='<%# Bind("MHC_TO_CNTY") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Entry Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCoEntryTp" Text='<%# Bind("MHC_TP") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--Div 3--%>
                                            <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-8" style="padding-left: 1px; padding-right: 1px;">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" style="padding-top: 1px; padding-bottom: 1px;">
                                                    <strong><b>Duty Claimable</b></strong>
                                                </div>
                                                <div class="panel-body" style="height: 176px;padding-top:2px;">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-1 labelText1">
                                                                Company
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox AutoPostBack="true" Style="text-transform: uppercase" runat="server" ID="txtCompnay" CssClass="form-control" OnTextChanged="txtCompnay_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1" style="padding-left: 3px;">
                                                                <asp:LinkButton ID="lbtnCompany" CausesValidation="false" runat="server" OnClick="lbtnCompany_Click">
                                                                       <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-8">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="panel panel-default" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; margin-left: -5px;">
                                                            <div class="panel panel-heading" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; height: 21px; margin-bottom: 0px;">
                                                                <table>
                                                                    <tr style="">
                                                                        <th scope="col" style="width: 90px; padding-left: 5px; padding-top: 2px;">Type</th>
                                                                        <th scope="col" style="width: 235px; padding-left: 3px; padding-top: 2px;">Description</th>
                                                                        <th scope="col" style="width: 340px; padding-top: 2px; text-align: right;">Claim Percentage(%)</th>
                                                                        <th scope="col" style="width: 110px; padding-left: 35px; text-align: left;">Act./Inact.</th>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="height: 125px; overflow: auto;">
                                                        <asp:GridView ShowHeader="false" ID="dgvDutyClaimable" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3" OnRowDataBound="dgvDutyClaimable_RowDataBound">
                                                            <EditRowStyle BackColor="MidnightBlue" />
                                                            <EmptyDataTemplate>
                                                                <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                    <tbody>
                                                                        <tr>
                                                                       
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
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDuType" runat="server" Text='<%# Bind("Code") %>' Width="60px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                    <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDuDescription" runat="server" Text='<%# Bind("Description") %>' Width="218px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                    <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl" runat="server" Width="158px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Claim Percentage(%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox  CssClass="txtPercentage form-control text-right"  ID="txtDuClaim" Text='<%# Bind("ClaimPrecentage", "{0:N2}") %>'
                                                                             runat="server"  Width="100px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                    <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="Label4" Visible="true" runat="server" Width="25px"></asp:Label>
                                                                        <asp:Label ID="lblDuMp" Visible="false" runat="server" Text='<%# Bind("MP") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Active">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ddlDuMp" runat="server" class="height20" Width="152px">
                                                                        </asp:CheckBox>
                                                                    </ItemTemplate>
                                                                   <ItemStyle width="70px"/>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 1px; padding-right: 1px;">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" style="padding-top: 1px; padding-bottom: 1px;">
                                                    <strong><b>Claim Company Break Up</b></strong>
                                                </div>
                                                <div class="panel panel-body" style="padding-top:2px;">
                                                <div class="panel panel-default" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; margin-left: -5px;">
                                                    <div class="panel panel-heading" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; height: 21px; margin-bottom: 0px;">
                                                        <table>
                                                            <tr style="">
                                                                <th scope="col" style="width: 46px; padding-left: 5px; padding-top: 2px;"></th>
                                                                <th scope="col" style="width: 83px; padding-left: 5px; padding-top: 2px;">Company</th>
                                                                <th scope="col" style="width: 150px; padding-left: 3px; padding-top: 2px;">Description</th>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div style="height: 140px;overflow:auto;">
                                                    <asp:GridView ShowHeader="false" ID="dgvClaimBU" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3">
                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                        <EmptyDataTemplate>
                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                <tbody>
                                                              
                                                                    <tr>
                                                                        <td>No records found.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div style="">
                                                                    <asp:LinkButton ID="lbtnEditClaim" runat="server" CausesValidation="false"
                                                                        OnClick="lbtnEditClaim_Click">
                                                                            Select
                                                                    </asp:LinkButton>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Company">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComCd" Text='<%# Bind("MHCL_COM") %>' runat="server" Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComDesc" Text='<%# Bind("MC_DESC") %>' runat="server" Width="200px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Entry Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClEntryTp" runat="server" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
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
        </ContentTemplate>
    </asp:UpdatePanel>




    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height:350px;">
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

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
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
                        <div class="col-sm-12 height5">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" 
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

     <script>
         if (typeof jQuery == 'undefined') {
             alert('jQuery is not loaded');
         }
         Sys.Application.add_load(fun);
         function fun() {
             $('.txtPercentage').keypress(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode == 46) {
                     if (str.indexOf(".") != -1) {
                         return false;
                     }
                 }
                 if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 else {
                     return true;
                 }
             })
             $('.txtPercentage').keyup(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 //alert(str);
                 if (charCode != 9) {
                     if (Number(str) > 100) {
                         if (str.indexOf(".") == -1) {
                             str = str.slice(0, -1);
                         } else {
                             str = str.slice(0, -4);
                         }
                         $(this).val(str);
                         alert('Percentage is invalid !');
                     }
                 }
             })
             $('.txtRate').keypress(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode == 46) {
                     if (str.indexOf(".") != -1) {
                         return false;
                     }
                 }
                 if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 else {
                     return true;
                 }
             })
             $('.txtRate').keyup(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode != 9) {
                     if (Number(str) > 100) {
                         if (str.indexOf(".") == -1) {
                             str = str.slice(0, -1);
                         } else {
                             str = str.slice(0, -4);
                         }
                         $(this).val(str);
                         alert('Maximum allowed rate is 100 !');
                     }
                 }
             })
             $('.txtUnitPrice').keypress(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode == 46) {
                     if (str.indexOf(".") != -1) {
                         return false;
                     }
                 }
                 if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 else {
                     return true;
                 }
             })

             $('.txtUnitPrice').keyup(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode != 9) {
                     if (Number(str) > 999999.99) {
                         if (str.indexOf(".") == -1) {
                             str = str.slice(0, -1);
                         } else {
                             str = str.slice(0, -4);
                         }
                         $(this).val(str);
                         alert('Maximum allowed unit price is 999,999.00 !');
                     }
                 }
             })
             $('.txtWeightPrice').keypress(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode == 46) {
                     if (str.indexOf(".") != -1) {
                         return false;
                     }
                 }
                 if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 else {
                     return true;
                 }
             })

             $('.txtWeightPrice').keyup(function (evt) {
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 var str = $(this).val();
                 if (charCode != 9) {
                     if (Number(str) > 999999.99) {
                         if (str.indexOf(".") == -1) {
                             str = str.slice(0, -1);
                         } else {
                             str = str.slice(0, -4);
                         }
                         $(this).val(str);
                         alert('Maximum weight price allowed is 999,999.00 !');
                     }
                 }
             })
         }
    </script>
    <%--Test--%>

</asp:Content>
