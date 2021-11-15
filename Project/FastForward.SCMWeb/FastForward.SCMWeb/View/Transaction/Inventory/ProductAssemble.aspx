<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ProductAssemble.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.ProductAssemble" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ConfSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfPrint() {
            var selectedvalueOrd = confirm("Do you want to print ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfOk() {
            var selectedvalueOrd = confirm("Are you sure ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfCancel() {
            var selectedvalueOrd = confirm("Do you want to cancel ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
    </script>
    <script>
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
            padding-top: 1px;
            padding-bottom: 0px;
            margin-bottom: 0px;
        }

        .panel-heading {
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="row">
        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upMain">
            <ProgressTemplate>
                <div class="divWaiting">
                    <asp:Label ID="lblWait" runat="server"
                        Text="Please wait... " />
                    <asp:Image ID="imgWait" runat="server"
                        ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="upMain" runat="server">
            <ContentTemplate>
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="row buttonRow">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3 padding0">
                                        
                                    </div>
                                    <div class="col-sm-2 padding0">
                                        <asp:LinkButton ID="lbtnPrint" CausesValidation="false" runat="server" OnClick="lbtnPrint_Click" Visible="false"
                                            OnClientClick="return ConfPrint();" CssClass="floatRight"> 
                                <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 padding0">
                                        <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" OnClick="lbtnSave_Click"
                                            OnClientClick="return ConfSave();" CssClass="floatRight"> 
                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 padding0">
                                        <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" OnClick="lbtnClear_Click"
                                            OnClientClick="return ConfClear();" CssClass="floatRight"> 
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                    </div>
                                    
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-heading">
                                    <strong><b>Product Assemble</b></strong>
                                </div>
                                <div class="panel panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-11 padding0">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3 padding0">
                                                                            <div class="col-sm-4 labelText1 padding0">
                                                                                Finish Good
                                                                            </div>
                                                                            <div class="col-sm-6  padding0">
                                                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtProduct_TextChanged" ID="txtProduct" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1  paddingLeft3">
                                                                                <asp:LinkButton ID="lbtnSeProduct" OnClick="lbtnSeProduct_Click" CausesValidation="false" runat="server"> 
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span> </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <div class="col-sm-2 paddingLeft0 labelText1">
                                                                                Status
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <asp:TextBox runat="server" ID="txtStatus" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtStatus_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft3">
                                                                                <asp:LinkButton ID="lbtnSeStatus" OnClick="lbtnSeStatus_Click" CausesValidation="false" runat="server"> 
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span> </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-3 paddingRight0 labelText1">
                                                                                Units To Produce
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <asp:TextBox onpaste="return false" runat="server" ID="txtNoOfUnit" AutoPostBack="true" OnTextChanged="lbtnMainSearch_Click" CausesValidation="false" CssClass="txtNoOfUnit form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="col-sm-4 padding0 labelText1">
                                                                                Assembly Job # 
                                                                            </div>
                                                                            <div class="col-sm-8 padding0">
                                                                                <asp:TextBox runat="server"  Style="text-transform: uppercase" AutoPostBack="false" OnTextChanged="txtAssJobNo_TextChanged" ID="txtAssJobNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3 padding0">
                                                                            <div class="col-sm-4 labelText1 padding0">
                                                                                Source Location
                                                                            </div>
                                                                            <div class="col-sm-6  padding0">
                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtLocation_TextChanged" ID="txtLocation" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1  paddingLeft3">
                                                                                <asp:LinkButton ID="lbtnSeLocation" OnClick="lbtnSeLocation_Click" CausesValidation="false" runat="server"> 
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <div class="col-sm-2 labelText1 padding0">
                                                                                FG To Store At
                                                                            </div>
                                                                            <div class="col-sm-2  padding0">
                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtLocationStoreAt_TextChanged" ID="txtLocationStoreAt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1  paddingLeft3">
                                                                                <asp:LinkButton ID="lbtnSeFGLocation" OnClick="lbtnSeFGLocation_Click" CausesValidation="false" runat="server"> 
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-3 paddingRight0 labelText1">
                                                                                Date
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                    onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                                                                    PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft3">
                                                                                <asp:LinkButton ID="lbtnDate" CausesValidation="false" runat="server">
                                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <div class="col-sm-4 padding0 labelText1">
                                                                                Manual Ref #
                                                                            </div>
                                                                            <div class="col-sm-8 padding0">
                                                                                <asp:TextBox runat="server" ID="txtManualRef" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <div class="row buttonRow">
                                                                    <asp:LinkButton ID="lbtnMainSearch" CausesValidation="false" runat="server" OnClick="lbtnMainSearch_Click"> 
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>Search </asp:LinkButton>
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
                                            <div class="col-sm-7 padding0">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <b>Component Stock Details</b>
                                                            </div>
                                                            <div class="panel panel-body">
                                                                <div style="height: 120px; overflow-x: hidden; overflow-y: auto;">
                                                                    <asp:GridView ID="dgvBillOfQty" CssClass="table table-hover table-striped"
                                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnSeBillOfQty" OnClick="lbtnSeBillOfQty_Click" CausesValidation="false" runat="server" Width="6px"> 
                                                                                    <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmCode" Text='<%# Bind("Micp_comp_itm_cd") %>' ToolTip='<%# Bind("ItemDesc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemModel" Text='<%# Bind("ItemModel") %>' ToolTip='<%# Bind("ItemModelDesc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <%-- <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" Text='<%# Bind("ItemDesc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Serialized">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIsSerialized" Text='<%#Convert.ToBoolean(Eval("ItemIsSerialized"))?"Yes":"No"%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPerQty" Text='<%# Bind("Micp_qty","{0:N2}") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Required Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQty" Text='<%# Bind("ProductAssemblyQty","{0:N2}") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                   <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label  Text='' Width="5px" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemUom" Text='<%# Bind("ItemUom") %>' runat="server"></asp:Label>
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
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <b>Assemble Products</b>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div style="height: 70px; overflow-x: hidden; overflow-y: auto;">
                                                            <asp:GridView ID="dgvFinishGoodDetails" CssClass="table table-hover table-striped"
                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <div class="margin-top-3">
                                                                                 <asp:LinkButton ID="lbtnSelFinishGood" Width="8px" OnClick="lbtnSelFinishGood_Click" Enabled="true" CausesValidation="false" runat="server"> 
                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="NO" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_temp_itm_line" Width="20px"  Text='<%# Bind("Tus_temp_itm_line") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_cd" Text='<%# Bind("Tus_itm_cd") %>' ToolTip='<%# Bind("Tus_itm_desc") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_model" Text='<%# Bind("Tus_itm_model") %>' ToolTip='<%# Bind("Tus_itm_model_desc") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Component Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_brand" Text='<%# Bind("Tus_itm_brand") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_stus_Desc" Text='<%# Bind("Tus_itm_stus_Desc") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial #">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblTus_ser_1" Text='<%# Bind("Tus_ser_1") %>' runat="server"></asp:Label>--%>
                                                                            <asp:TextBox OnTextChanged="txtTus_ser_1_TextChanged" ReadOnly='<%#Convert.ToBoolean(Eval("SerialAvailable")) %>' AutoPostBack="true"  
                                                                                runat="server" Text='<%# Bind("Tus_ser_1") %>' ID="txtTus_ser_1" Width="150px" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pick">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_isSelect" Text='<%#Convert.ToBoolean(Eval("Tus_isSelect")) ? "Yes" : "No"%>'  runat="server"></asp:Label>
                                                                            <%--<asp:Label ID="lblTus_isSelect" Text='<%# Eval("Tus_isSelect").ToString().Equals("1") ? "No" : "No"%>'  runat="server"></asp:Label>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_stus" Text='<%# Bind("Tus_itm_stus")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_id" Text='<%# Bind("Tus_ser_id")%>' runat="server"></asp:Label>
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
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <div class="row">
                                                             <div class="col-sm-12">
                                                            <div class="col-sm-3"> 
                                                                <b>Component Item List</b>
                                                            </div>
                                                            <div class="col-sm-5"> 
                                                                
                                                            </div>
                                                            <div class="col-sm-1 padding0"> 
                                                               Serial #
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0"> 
                                                               <asp:TextBox runat="server" AutoPostBack="true" ID="txtSubSerial" onblur="doPostBack(this)"  
                                                                   OnTextChanged="txtSubSerial_TextChanged"
                                                                    CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div style="height: 150px; overflow-x: hidden; overflow-y: auto;">
                                                            <asp:GridView ID="dgvItems" CssClass="table table-hover table-striped"
                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="NO" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_temp_itm_line" Width="20px"  Text='<%# Bind("Tus_itm_line") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_cd" Text='<%# Bind("Tus_itm_cd")%>' ToolTip='<%# Bind("Tus_itm_desc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_model" Text='<%# Bind("Tus_itm_model")%>' ToolTip='<%# Bind("Tus_itm_model_desc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FG Serial #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFgSerial" Text='<%# Bind("MainItemSerialNo")%>'  runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Serial #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSerialNo" Text='<%# Bind("Tus_ser_1")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UOM">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemUom" Text='<%# Bind("Tus_itm_brand") %>' runat="server"></asp:Label>
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
                                                <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                               <%-- <div class="col-sm-5 labelText1">
                                                    <asp:Label Text="FG Code :" runat="server" />
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label Text="" ID="lblFgCode" runat="server" />
                                                </div>--%>
                                            </div>
                                            <div class="col-sm-3">
                                                <%--<div class="col-sm-5 labelText1">
                                                    <asp:Label Text="FG Serial :" runat="server" />
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label Text="" ID="lblFgSerial" runat="server" />
                                                </div>--%>
                                            </div>
                                            <div class="col-sm-3">
                                               <%-- <div class="col-sm-6 padding0 labelText1">
                                                    <asp:Label Text="FG Warranty :" runat="server" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label Text="" ID="lblFgWarranty" runat="server" />
                                                </div>--%>
                                            </div>
                                            <div class="col-sm-3 buttonRow">
                                                <div class="col-sm-6">
                                                    <asp:LinkButton ID="lbtnOk" OnClick="lbtnOk_Click" OnClientClick="return ConfOk()" runat="server" Visible="true">
                                                            <span class="glyphicon glyphicon-ok-circle"></span>Ok
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <asp:LinkButton ID="lbtnCancel" OnClick="lbtnCancel_Click" OnClientClick="return ConfCancel()" runat="server" Visible="true">
                                                            <span class="glyphicon glyphicon-remove-circle"></span>Cancel
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                            </div>
                                            <div class="col-sm-5  padding0">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <b>Stock in hand</b>
                                                            </div>
                                                            <div class="panel panel-body">
                                                                <div>
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-sm-2 paddingRight0 labelText1">
                                                                                Item Code :
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1 padding0">
                                                                                <asp:Label Text="" ID="lblItemCode" runat="server" />
                                                                            </div>
                                                                           <%-- <div class="col-sm-2 paddingRight0 labelText1">
                                                                                MFC :
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1 padding0">
                                                                                <asp:Label Text="" ID="lblMfc" runat="server" />
                                                                            </div>--%>
                                                                        </div>
                                                                        <div class="row">
                                                                        </div>
                                                                        <asp:Panel runat="server" Visible="false">
                                                                            <div class="row">
                                                                                <div class="col-sm-3 paddingRight0 labelText1">
                                                                                    Status
                                                                                </div>
                                                                                <div class="col-sm-1 labelText1">
                                                                                    :
                                                                                </div>
                                                                                <div class="col-sm-4 labelText1 paddingRight0">
                                                                                    <asp:Label Text="" ID="lblStatus" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <div class="col-sm-6 padding0 labelText1">
                                                                                    Qty In Hand :
                                                                                </div>
                                                                                <div class="col-sm-6 labelText1 paddingLeft0 text-left">
                                                                                    <asp:Label Text="" ID="lblQtyInHand" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <div class="col-sm-6 padding0 labelText1">
                                                                                    Reserved Qty :
                                                                                </div>
                                                                                <div class="col-sm-6 labelText1 paddingLeft0 text-left">
                                                                                    <asp:Label Text="" ID="lblResQty" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <div class="col-sm-5 padding0 labelText1">
                                                                                    Free Qty :
                                                                                </div>
                                                                                <div class="col-sm-6 labelText1 paddingLeft0 text-left">
                                                                                    <asp:Label Text="" ID="lblFreeQty" runat="server" />
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
                                                            <div class="panel panel-heading">
                                                                <b>Finish Good Scan Details</b>
                                                            </div>
                                                            <div class="panel panel-body">
                                                                <%--<div style="height: 200px; overflow-x: hidden; overflow-y: auto;">--%>
                                                                <div style="height: 140px; overflow-x: hidden; overflow-y: auto;">
                                                                    <asp:GridView ID="dgvScanHdr" CssClass="table table-hover table-striped"
                                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <div class="margin-top-3">
                                                                                        <asp:LinkButton ID="lbtnSeScanHdr" Width="3px" OnClick="lbtnSeScanHdr_Click"  CausesValidation="false" runat="server"> 
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_cd" Text='<%# Bind("Tus_itm_cd")%>' ToolTip='<%# Bind("Tus_itm_desc")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_model" Text='<%# Bind("Tus_itm_model")%>' ToolTip='<%# Bind("Tus_itm_model_desc")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_stus_Desc" Text='<%# Bind("Tus_itm_stus_Desc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_ser_1" Text='<%# Bind("Tus_ser_1") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_stus" Text='<%# Bind("Tus_itm_stus")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>

                                                            <div class="panel panel-heading">
                                                                <b>Assemble Product Scan Details</b>
                                                            </div>
                                                            <div class="panel panel-body">
                                                                <%--<div style="height: 200px; overflow-x: hidden; overflow-y: auto;">--%>
                                                                <div style="height: 180px; overflow-x: hidden; overflow-y: auto;">
                                                                    <asp:GridView ID="dgvScanItm" CssClass="table table-hover table-striped"
                                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_cd" Text='<%# Bind("Tus_itm_cd")%>' ToolTip='<%# Bind("Tus_itm_desc")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_model" Text='<%# Bind("Tus_itm_model")%>' ToolTip='<%# Bind("Tus_itm_model_desc")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_stus_Desc" Text='<%# Bind("Tus_itm_stus_Desc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_ser_1" Text='<%# Bind("Tus_ser_1") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTus_itm_stus" Text='<%# Bind("Tus_itm_stus")%>' runat="server"></asp:Label>
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
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Panel runat="server" Visible="false">
                                        <div class="col-sm-12">
                                            <div class="col-sm-7" style="padding-left: 1px; padding-right: 1px;">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <b>Main Details</b>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div class="col-sm-5">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    Company
                                                                </div>
                                                                <div class="col-sm-7 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtCompany" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCompany_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                    <asp:LinkButton ID="lbtnSeCompany" OnClick="lbtnSeCompany_Click" CausesValidation="false" runat="server"> 
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span> </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    
                                                                </div>
                                                                <div class="col-sm-7 paddingRight0">
                                                                    
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                    
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    FG Stored Bin
                                                                </div>
                                                                <div class="col-sm-7 paddingRight0">
                                                                    <asp:TextBox runat="server" AutoPostBack="true"  Style="text-transform: uppercase" ID="txtLocBin" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    <asp:TextBox runat="server" AutoPostBack="true"   Style="text-transform: uppercase" ID="txtLocStorAtBin" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                    <asp:LinkButton ID="lbtnSeBin" OnClick="lbtnSeBin_Click" CausesValidation="false" runat="server"> 
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span> </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    
                                                                </div>
                                                                <div class="col-sm-7 paddingRight0">
                                                                    
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    
                                                                </div>
                                                                <div class="col-sm-7 paddingRight0">
                                                                    
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                
                                                                <div class="col-sm-1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5" style="padding-left: 1px; padding-right: 1px;">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <b>Finish good</b>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div class="col-sm-12">
                                                            <asp:Panel runat="server" DefaultButton="lbtnMainSearch">
                                                                <div class="col-sm-7 paddingLeft0">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0">
                                                                            
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft3">
                                                                            
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        
                                                                     </div>
                                                                     <div class="row">
                                                                         <div class="col-sm-4 labelText1">
                                                                             
                                                                         </div>
                                                                         <div class="col-sm-5 paddingRight0">
                                                                             
                                                                         </div>
                                                                         <div class="col-sm-1 paddingLeft3">
                                                                         </div>
                                                                     </div>
                                                                 </div>
                                                                <div class="col-sm-1">
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <br />
                                                                    
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                            </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight" style="width: 700px;">
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-2 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
    <script>
        Sys.Application.add_load(func);
        function func() {

            $('.txtNoOfUnit').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 2) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 2);
                    alert('Maximum 2 characters are allowed ');
                    return false;
                }
            });


            $('.txtNoOfUnit').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
        }
    </script>
</asp:Content>
