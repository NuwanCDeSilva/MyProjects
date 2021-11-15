<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CostProfitabilityAssistant.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Imports.Cost_and_Profitability_Assistant" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
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
    <style>
     td.data_style_text
    {
        text-align: center;
    }
     td.data_style_text2
    {
        text-align: left;
    }
     td.data_style_decimal
    {
        text-align: right;
    }    
    th.column_style_right
    {
        border-right: 1px solid #D3D3D3;
        text-align: center;
    } 
     th.column_style_text
    {
        text-align: center;
    } 
     td.column_right
    {
        border-right: 1px solid #D3D3D3;
        text-align: center;
    } 
      td.column_right_x
    {
       /*text-align: center;*/
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

    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />

    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="panel panel-default marginRight5">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                        
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div>
                                            <div class="col-sm-2 labelText1">
                                                Order from
                                            </div>
                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                <asp:DropDownList ID="orderFrom" CausesValidation="false" runat="server" CssClass="form-control" AutoPostBack="True" onselectedindexchanged="orderFrom_DataBound">
                                                    <asp:ListItem Text="Imports" Value="I" />
                                                     <asp:ListItem Text="Local" Value="L" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-1 ">
                                            </div>
                                         </div>
                                        <div>
                                            <div class="col-sm-2 labelText1">
                                                Model
                                            </div>
                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="textModel" AutoPostBack="true" CausesValidation="false" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 ">
                                                <asp:LinkButton ID="lbtnModel" runat="server" CausesValidation="false" OnClick="lbtnModel_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div>
                                            <div class="col-sm-2 labelText1">
                                                Item
                                            </div>
                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 ">
                                                <asp:LinkButton ID="lbtnItem" runat="server" CausesValidation="false" OnClick="lbtnItem_Click" >
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <div>
                                            <div class="col-sm-2 labelText1">
                                                SI/PO #
                                            </div>
                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="textBLNo" AutoPostBack="true" CausesValidation="false" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 ">
                                                <asp:LinkButton ID="lbtnBLNo" runat="server" CausesValidation="false" OnClick="lbtnBLNo_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <asp:Label ID="lblDtvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            <asp:CheckBox ID="chkBLDate" Text="" runat="server" AutoPostBack="true" ToolTip="SI/PO Date" OnCheckedChanged="chkBLDate_CheckChanged" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                           SI/PO From
                                        </div>
                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtBLFrom" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div  class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnBLFrom" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="BLFrom" runat="server" TargetControlID="txtBLFrom" Animated="true"
                                                                            PopupButtonID="lbtnBLFrom" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                           To
                                        </div>
                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtBLTo" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div  class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnBLTo" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="BLTo" runat="server" TargetControlID="txtBLTo" Animated="true"
                                                                            PopupButtonID="lbtnBLTo" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2" id="costDate" runat="server">
                            <div class="panel panel-default " >
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            <asp:CheckBox ID="chkCostDate" Text="" runat="server" AutoPostBack="true" ToolTip="Costing Date" OnCheckedChanged="chkCostingDate_CheckChanged"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                           Costing From
                                        </div>
                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtCostFrom" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div  class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnCostFrom" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CostFrom" runat="server" TargetControlID="txtCostFrom" Animated="true"
                                                                            PopupButtonID="lbtnCostFrom" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                           To
                                        </div>
                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtCostTo" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div  class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnCostTo" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CostTo" runat="server" TargetControlID="txtCostTo" Animated="true"
                                                                            PopupButtonID="lbtnCostTo" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            <asp:CheckBox ID="chkGRNDate" Text="" runat="server" AutoPostBack="true" ToolTip="GRN Date" OnCheckedChanged="chkGRNDate_CheckChanged" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                           GRN From
                                        </div>
                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtGRNFrom" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div  class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnGRNFrom" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="GRNFrom" runat="server" TargetControlID="txtGRNFrom" Animated="true"
                                                                            PopupButtonID="lbtnGRNFrom" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                           To
                                        </div>
                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtGRNTo" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div  class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnGRNTo" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="GRNTo" runat="server" TargetControlID="txtGRNTo" Animated="true"
                                                                            PopupButtonID="lbtnGRNTo" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="row">
                                <div class="col-sm-5">
                                    <asp:LinkButton ID="lbtnSearchAll" runat="server" OnClick="lbtnSearchall_Click">
                                        <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-5">
                                    <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                           </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body panelscollbar height120" id="ImportGridPanel" runat="server">
                                <asp:GridView ID="ImportGrid" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False"  OnRowCreated="ImportGrid_OnRowDataBound" OnSelectedIndexChanged="ImportGrid_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SI #" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridBLNo" runat="server" Text='<%# Bind("BL_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry #" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridEntryNo" runat="server" Text='<%# Bind("ENTRY_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridItem" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridModel" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Costing" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridCost" runat="server" Text='<%# (Convert.ToDecimal(Eval("COSTING"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Forcast" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImpGridFrCst" runat="server" Text='<%# (Convert.ToDecimal(Eval("FORCAST"))).ToString("N2") %>' OnClick="ImpGridFrCst_Click"></asp:LinkButton>
                                                <asp:TextBox runat="server" ID="txtfrcst" Visible="false" CssClass="form-control" OnTextChanged="txtfrcst_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buying" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridBuy" runat="server" Text='<%# (Convert.ToDecimal(Eval("BUYING"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay Mode" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridPayM" runat="server" Text='<%# Bind("PAYMODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Currency" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridcurr" runat="server" Text='<%# Bind("CURR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FOB" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridFOB" runat="server" Text='<%# (Convert.ToDecimal(Eval("FOB"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duty Free" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridDF" runat="server" Text='<%# (Convert.ToDecimal(Eval("DF"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DF+PAL($)" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridDFPal" runat="server" Text='<%# (Convert.ToDecimal(Eval("DF_PAL_D"))).ToString("N2") %>'> </asp:Label>
                                                    </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DF+PAL(Rs.)" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridDFPalD" runat="server" Text='<%# (Convert.ToDecimal(Eval("DF_PAL_RS"))).ToString("N2") %>'></asp:Label>
                                                    </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duty Paid" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridDP" runat="server" Text='<%# (Convert.ToDecimal(Eval("DP"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Costing Date" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridCostDt" runat="server" Text='<%# Bind("COSTING_DT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank bill paid cost" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text">
                                            <ItemTemplate>
                                                <asp:Label ID="ImpGridBBPC" runat="server" Text='<%# Eval("PAYMODE").ToString()=="FOC"?(Convert.ToDecimal(Eval("BANK BILL PAID COST"))).ToString("N2"):"" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRN"  HeaderStyle-CssClass="column_style_text" ItemStyle-CssClass="data_style_text">
                                            <ItemTemplate>
                                                 <asp:Button ID="ImpGridGRNBtn" Text="GRN" runat="server"  CommandName="LoadGRN" OnClick="lbtnGRN_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body panelscollbar height120" id="LocalGridPanel" runat="server">
                                <asp:GridView ID="LocalGrid" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowCreated="LocalGrid_OnRowDataBound" OnSelectedIndexChanged="LocalGrid_SelectedIndexChanged" >
                                    <%--<HeaderStyle BorderWidth="0.5"  />--%>
                                    <%--<HeaderStyle BorderWidth="1"/>--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="PO #" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            
                                            <ItemTemplate >
                                                <asp:Label ID="LclGridPONo" runat="server" Text='<%# Bind("POH_DOC_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="LclGridDate" runat="server" Text='<%# Bind("DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="LclGridItem" runat="server" Text='<%# Bind("POD_ITM_CD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                            <ItemTemplate>
                                                <asp:Label ID="LclGridModel" runat="server" Text='<%# Bind("MI_MODEL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Unit Price" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="LclGridUCost" runat="server" Text='<%# (Convert.ToDecimal(Eval("POD_UNIT_PRICE"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRN Cost" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="LclGridACost" runat="server" Text='<%# (Convert.ToDecimal(Eval("POD_ACT_UNIT_PRICE"))).ToString("N2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRN" HeaderStyle-CssClass="column_style_text" ItemStyle-CssClass="data_style_text">
                                            <ItemTemplate>
                                                <asp:Button ID="LclGridGRNBtn" Text="GRN" runat="server"  CommandName="LoadGRN" OnClick="lbtnGRN_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height20">
                        </div>
                    </div>
                    <div class="panel panel-default marginLeftRight5">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-2 fontWeight900">
                                            Item:
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lbItem" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 ">
                                    <div class="row">
                                        <div class="col-sm-4 fontWeight900">
                                            Description
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lbDes" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-2 fontWeight900">
                                            Brand:
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lbBrand" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-4 fontWeight900">
                                            VAT Rate:
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lbVAT" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="panel panel-default marginRight5">
                                <div class="panel-heading">
                                    Previous
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12 ">
                                                <div class="bs-example">
                                                    <ul class="nav nav-tabs" id="PreValue">
                                                        <li class="active"><a href="#OD" data-toggle="tab">Order Details</a></li>
                                                        <li><a href="#PP" data-toggle="tab">Default Price Book Details</a></li>
                                                    </ul>
                                                </div>
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="OD">
                                                        <div class="panel-body panelscollbar height120">
                                                            <asp:GridView ID="GrdPreOrdDet" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="SI/PO #" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PreOrdDetBLNo" runat="server" Text='<%# Bind("ITH_OTH_DOCNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PreOrdDetDate" runat="server" Text='<%# Bind("DATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cost" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PreOrdDetCost" runat="server" Text='<%# (Convert.ToDecimal(Eval("ITB_UNIT_COST"))).ToString("N2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FOB" HeaderStyle-CssClass="column_style_right"  ItemStyle-CssClass="data_style_text">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PreOrdDetFOB" runat="server" Text='<%# Bind("FOB") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="tab-pane " id="PP">
                                                        <div class="panel-body panelscollbar height120">
                                                            <asp:GridView ID="GrdPrePrice" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Price Book" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PrePriceBook" runat="server" Text='<%# Bind("sapd_pb_tp_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price Level" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PrePriceLevel" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="From Date" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PrePriceFrmDt" runat="server" Text='<%# Bind("DATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PrePricePrice" runat="server" Text='<%# (Convert.ToDecimal(Eval("sapd_itm_price"))).ToString("N2") %>'></asp:Label>
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
                        <div class="col-sm-8">
                            <div class="panel panel-default marginRight5">
                                <div class="panel-heading">
                                    Cost-GP-View
                                </div>
                                <div class="panel-body">
                                    
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1">
                                                    Item
                                                </div>
                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" ></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="lbtnGPItem" runat="server" CausesValidation="false" OnClick="lbtnGPItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1 ">
                                                    <asp:LinkButton ID="lbtnGPItmUp" runat="server" CausesValidation="false" OnClick="lbtnGPItmUp_Click" ToolTip="Item Excel">
                                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1">
                                                    Main Cat
                                                </div>
                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPCat1" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPCat1" runat="server" CausesValidation="false" OnClick="lbtnGPCat1_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1">
                                                    Cat 3
                                                </div>
                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPCat3" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPCat3" runat="server" CausesValidation="false" OnClick="lbtnGPCat3_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1">
                                                    Serial
                                                </div>
                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPSerial" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtGPSerial_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="lbtnGPSerial" runat="server" CausesValidation="false" OnClick="lbtnGPSerial_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="lbtnGPSerUp" runat="server" CausesValidation="false" OnClick="lbtnItem_Click" ToolTip="Serial Excel">
                                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1">
                                                    Cat 2
                                                </div>
                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPCat2" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPCat2" runat="server" CausesValidation="false" OnClick="lbtnGPCat2_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1">
                                                    Brand
                                                </div>
                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPBrand" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPBrand" runat="server" CausesValidation="false"  OnClick="lbtnGPBrand_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="font-weight:bold">
                                            <div class="col-sm-3">
                                                Detials With
                                            </div>
                                            <div class="col-sm-3">
                                                Document Type
                                            </div>
                                            <div class="col-sm-4"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:CheckBox ID="chkGPFOC" Text="" runat="server" AutoPostBack="true" />
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGPFOC" runat="server" Text="FOC Items"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:RadioButton ID="chkGPOrgDoc" Text="" runat="server" AutoPostBack="true" GroupName="chkgrn"/>
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGPOrgDoc" runat="server" Text="Original Purchase(GRN)"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-5 labelText1">
                                                    Price Book
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPPriBk" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPPriBk" runat="server" CausesValidation="false" OnClick="lbtnGPPriBk_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:CheckBox ID="chkGPCurAllPri" Text="" runat="server" AutoPostBack="true" Visible="false"/>
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGpCurAllPri" runat="server" Text="Current All Prices" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:CheckBox ID="chkGPSpcl" Text="" runat="server" AutoPostBack="true" />
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGPSpcl" runat="server" Text="Special"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:RadioButton ID="chkGPAllDoc" Text="" runat="server" Checked="true" AutoPostBack="true" GroupName="chkgrn"/>
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGPSAllDoc" runat="server" Text="All"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-5 labelText1">
                                                    Price Level
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPPriLvl" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPPriLvl" runat="server" CausesValidation="false" OnClick="lbtnGPPriLvl_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-2"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:CheckBox ID="chkGPNrml" Text="" runat="server" AutoPostBack="true" />
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGPNrml" runat="server" Text="Normal"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-6 labelText1">
                                                    No of Records
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtRecCount" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-5 labelText1">
                                                    Promo Code
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPPromoCd" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPPromoCd" runat="server" CausesValidation="false" OnClick="lbtnGPPromoCd_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-2"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <div class="col-sm-1">
                                                    <asp:CheckBox ID="chkGPAllItm" Text="" runat="server" AutoPostBack="true" />
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblGPAllItm" runat="server" Text="All"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-6 labelText1">
                                                    Status
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtStatus" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="col-sm-3 labelText1">
                                                    Circular
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtGPCircular" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnGPCircular" runat="server" CausesValidation="false" OnClick="lbtnGPCircular_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                                    <asp:Button ID="btnPreviewGP" class="btn btn-success btn-xs" runat="server" Text="Preview" OnClick="btnPreviewGP_Click" />
                                            </div>
                                        </div>
                                    </div>

                                   <%-- <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>--%>
                                    <%--<div class="row">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="col-sm-4">
                                                <div class="col-sm-8 labelText1">
                                                    Expect MU %
                                                </div>
                                                <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="AllExpctMUPer" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="ExpctMUPer_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-8 labelText1">
                                                    Expect GP %
                                                </div>
                                                <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                    <asp:TextBox runat="server" ID="AllExpctGPPer" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="ExpctGPPer_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btnApplyToAll" class="btn btn-success btn-xs" runat="server" Text="Calculate All" OnClick="btnCalAll_Click"/>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btnClearPerAll" class="btn btn-success btn-xs" runat="server" Text="Clear All" OnClick="btnClearPerAll_Click"/>
                                            </div>
                                        </div>
                                        
                                    </div>--%>
                                    <%--<div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel-body panelscollbar height120">
                                                <asp:GridView ID="GrdGPCal" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cicular No" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalCirNo" runat="server" Text='<%# Bind("sapd_circular_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price Book" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalPBook" runat="server" Text='<%# Bind("sapd_pb_tp_cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price Level" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalPLevel" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalPrice" runat="server" Text='<%# ((decimal)Eval("sapd_itm_price")).ToString("N2") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cur Markup%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalCurPer" runat="server" Text='<%# ((decimal)Eval("Markup_Per")).ToString("N2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GP%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalGPPer" runat="server" Text='<%# ((decimal)Eval("GP_Per")).ToString("N2") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Expect MU%" HeaderStyle-CssClass="column_style_right">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="GrdGPCalExpctMUPer" runat="server" AutoPostBack="true" OnTextChanged="GrdGPCalExpctPer_TextChanged" Width="75px" Height="40%" BorderColor="White"/>
                                                                <%--<asp:Label ID="GrdGPCalExpctMUPer" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Expect MU%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ExpctMUPerLbl" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Expect GP%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="GrdGPCalExpctGPPer" runat="server" AutoPostBack="true" OnTextChanged="GrdGPCalExpctPer_TextChanged" Width="75px" Height="40%" BorderColor="White"/>
                                                                <%--<asp:Label ID="GrdGPCalExpctGPPer" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Expect GP%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ExpctGPPerLbl" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="New Price for MU" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalNewMU" runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="New Price for GP" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_decimal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GrdGPCalNewGP" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose2" runat="server" AutoPostBack="true" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" >Close</span>
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
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />

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

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-default height400 width850">

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
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div7" runat="server">
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
                                            <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click" >
                                            <span class="glyphicon glyphicon-search" aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging" > 
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <%--<asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait3" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait3" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>

                                </asp:UpdateProgress>--%>
                            </div>
                        </div>

                    </div>

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpCostGPView" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnCostGPView"  PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground" CancelControlID="lbtnclosegpvw">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

     <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="updtPnlcostGPView">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitNew5" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitNew5" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="updtPnlcostGPView" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnCostGPView" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width1000">

                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnclosegpvw" runat="server" AutoPostBack="true"  OnClick="lbtnclosegpvw_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                                Cost GP Preview
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div2" runat="server">
                                <div class="col-sm-12 height5">
                                </div>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="col-sm-5 labelText1">
                                                Price Book
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtGPPnlPB"  CausesValidation="false" CssClass="form-control" AutoPostBack="true"  OnTextChanged="txtGPPnlPB_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:LinkButton ID="LbtnGPPnlPB" runat="server" CausesValidation="false" OnClick="LbtnGPPnlPB_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="col-sm-5 labelText1">
                                                Promo Code
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtGPPnlPromoCd"  CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtGPPnlPromoCd_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:LinkButton ID="lbtnGPPnlPromoCd" runat="server" CausesValidation="false" OnClick="lbtnGPPnlPromoCd_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="col-sm-7 labelText1">
                                                Expect MU%
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtGPPnlExMU"  CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtGPPnlExMU_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="col-sm-1">
                                            <asp:Button ID="btnexportexel" class="btn btn-success btn-xs" runat="server" Text="Export" OnClick="btnexportexel_Click"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="col-sm-5 labelText1">
                                                Price Level
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtGPPnlPL"  CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtGPPnlPL_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:LinkButton ID="LbtnGPPnlPL" runat="server" CausesValidation="false" OnClick="LbtnGPPnlPL_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="col-sm-5 labelText1">
                                                Circular
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtGPPnlCir"  CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtGPPnlCir_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:LinkButton ID="lbtnGPPnlCir" runat="server" CausesValidation="false" OnClick="lbtnGPPnlCir_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="col-sm-7 labelText1">
                                                Expect GP%
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtGPPnlExGP"  CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtGPPnlExGP_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnGPPnlProc" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnGPPnlProc_Click"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 height5">
                                </div>
                                <div class="col-sm-12" style="overflow-y:scroll; height:300px">
                                    <asp:GridView ID="grdCostGPView" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="15" OnRowCreated="grdCostGPView_RowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGItm" runat="server" Text='<%# Eval("ITEM").ToString()=="" ? Eval("ITEM1").ToString() : Eval("ITEM").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SerialDF" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2" >
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGSer" runat="server" Text='<%# Eval("SER").ToString()=="" ? Eval("SER").ToString() : Eval("SER").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SerialDP" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2" >
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGSerDp" runat="server" Text='<%# Eval("SER1").ToString()=="" ? Eval("SER1").ToString() : Eval("SER1").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DF Status" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGStsDF" runat="server" Text='<%# Eval("STATUS").ToString()=="" ? "" : Eval("STATUS").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DP Status" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGStsDP" runat="server" Text='<%# Eval("STATUS1").ToString()=="" ? "" : Eval("STATUS1").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DF" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGLtstDF" runat="server" Text='<%# Eval("DF_COST").ToString()==""?"":(Convert.ToDecimal(Eval("DF_COST"))).ToString("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DP" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGLtstDP" runat="server" Text='<%#  Eval("DP_COST").ToString()==""?"":(Convert.ToDecimal(Eval("DP_COST"))).ToString("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DF" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGPicDocDF" runat="server" Text='<%# Eval("DF_DOC").ToString()=="" ? "" : Eval("DF_DOC").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGPicDocDFDt" runat="server" Text='<%# Eval("DF_DT").ToString()=="" ? "" : Eval("DF_DT").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DP" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGPicDocDP" runat="server" Text='<%# Eval("DP_DOC").ToString()=="" ? "" : Eval("DP_DOC").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGPicDocDPDt" runat="server" Text='<%# Eval("DP_DT").ToString()=="" ? "" : Eval("DP_DT").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Price Book" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="gdCGPB" runat="server" Text='<%# Eval("PRI_BK").ToString()=="" ? "" : Eval("PRI_BK").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price Level" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCDPL" runat="server" Text='<%# Eval("PRI_LVL").ToString()=="" ? "" : Eval("PRI_LVL").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGCurPri" runat="server" Text='<%#  Eval("CUR_PRI").ToString()==""?"":(Convert.ToDecimal(Eval("CUR_PRI"))).ToString("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MU%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="grdCGMUPer" runat="server" AutoPostBack="true" Width="75px" Height="40%" BorderColor="White" Text='<%#  Eval("MK_PER").ToString()==""?"":(Convert.ToDecimal(Eval("MK_PER"))).ToString("N2") %>' OnTextChanged="grdCGMUPer_TextChanged" ReadOnly="true"/>
                                                    <%--<asp:Label ID="grdCGMUPer" runat="server" Text=''></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GP%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="grdCGGPPer" runat="server" AutoPostBack="true" Width="75px" Height="40%" BorderColor="White" Text='<%#  Eval("GP_PER").ToString()==""?"":(Convert.ToDecimal(Eval("GP_PER"))).ToString("N2") %>' OnTextChanged="grdCGMUPer_TextChanged" ReadOnly="true"/>
                                                    <%--<asp:Label ID="grdCGGPPer" runat="server" Text=''></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VAT%" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="grdCGVATPer" runat="server" AutoPostBack="true" Width="75px" Height="40%" BorderColor="White" OnTextChanged="grdCGVATPer_TextChanged"/>
                                                    <%--<asp:Label ID="grdCGVATPer" runat="server" Text=''></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New MU" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGMU" runat="server" Text='<%#  Eval("CUR_NWMU").ToString()==""?"":(Convert.ToDecimal(Eval("CUR_PRI"))).ToString("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New GP" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGGP" runat="server" Text='<%#  Eval("CUR_NWGP").ToString()==""?"":(Convert.ToDecimal(Eval("CUR_NWGP"))).ToString("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Price" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGPri" runat="server" Text=''></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Base Item" HeaderStyle-CssClass="column_style_right" ItemStyle-CssClass="data_style_text2">
                                                <ItemTemplate>
                                                    <asp:Label ID="grdCGBItm" runat="server" Text='<%# Eval("BITEM").ToString()=="" ? Eval("BITEM1").ToString() : Eval("BITEM").ToString() %>'></asp:Label>
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

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="exUplodPnlBtn" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="exUplodPnlBtn"
                PopupControlID="pnlexcel" CancelControlID="btnClose_excel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlexcel">
                <div runat="server" id="dv" class="panel panel-default height250 width700 ">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose_excel" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <strong>Excel Upload</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height10">
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlupload" Visible="true">
                                        <div class="col-sm-12" id="Div6" runat="server">
                                            <div class="col-sm-8 paddingRight5">



                                                <div class="row">
                                                    <div class="col-sm-7 labelText1">
                                                        <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                                    </div>
                                                    <div class="col-sm-2 paddingRight5">
                                                        <asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" Visible="false" />
                                                        <asp:Button ID="btnupload" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnupload_Click"/>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height20">
                                                </div>
                                            </div>



                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAsyncUpload"
                EventName="Click" />
            <asp:PostBackTrigger ControlID="btnupload" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
