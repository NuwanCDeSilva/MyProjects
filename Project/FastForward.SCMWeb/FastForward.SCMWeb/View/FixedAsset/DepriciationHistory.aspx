<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DepriciationHistory.aspx.cs" Inherits="FastForward.SCMWeb.View.Fixed_Asset.DepriciationHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 1 !important;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="mainPnl">
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
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-body">
                    <div class="col-sm-12 buttonRow">
                        <div class="col-sm-10"></div>
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="btnSearch" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnSearch_Click">
                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>Search
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default marginLeftRight5">
                    <div class=" panel-heading"></div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upsavebtns" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-2 paddingLeft0 labelText1">
                                            Location
                                        </div>
                                        <div class="col-md-4 padding0">
                                            <asp:TextBox ID="Location" runat="server" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="Location_TextChanged"
                                                CssClass="TextBox5 form-control "> </asp:TextBox>
                                        </div>
                                        <div class="col-md-1" style="padding-left: 3px;">
                                            <asp:LinkButton ID="lbtnSeLocation" runat="server" OnClick="lbtnSeLocation_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                            </asp:LinkButton>
                                        </div>

                                        <div class="col-md-5 padding0">
                                            <div class="col-sm-1 padding0">
                                                <asp:CheckBox ID="alllocation" Checked="true" Enabled="true" runat="server" />
                                            </div>
                                            <div class="col-md-8 padding0">
                                                <asp:Label ID="LabelType" runat="server" Text="All"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                            <div class="col-md-6">
                                                <div class="col-sm-7 padding0">
                                                    <div class="col-sm-6 padding0">
                                                        <div class="col-sm-3 labelText1 padding0">
                                                            From
                                                        </div>
                                                        <div class="col-sm-7 padding03">
                                                            <asp:TextBox runat="server" ID="fromdate" CausesValidation="false" CssClass="form-control" Enabled="false" OnTextChanged="fromdate_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding03">
                                                            <asp:LinkButton ID="txtFroml" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="fromdate"
                                                                PopupButtonID="txtFroml" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <div class="col-sm-3 labelText1 padding0">
                                                            To
                                                        </div>
                                                        <div class="col-sm-7 padding03">
                                                            <asp:TextBox runat="server" Enabled="false" ID="txtTo" CausesValidation="false" CssClass="form-control" OnTextChanged="txtTo_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding03">
                                                            <asp:LinkButton ID="txtTol" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                                PopupButtonID="txtTol" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-sm-5">

                                                    <div class="col-md-5 padding0">
                                                        <div class="col-sm-3 padding0">
                                                            <asp:CheckBox ID="alltimeduration" Checked="true" Enabled="true" runat="server" />
                                                        </div>
                                                        <div class="col-md-8 padding0">
                                                            <asp:Label ID="Label1" runat="server" Text="All"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-2 paddingLeft0 labelText1">
                                            Item
                                        </div>
                                        <div class="col-md-4 padding0">
                                            <asp:TextBox ID="itemiddeta" runat="server" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="itemid_TextChanged"
                                                CssClass="TextBox5 form-control "> </asp:TextBox>
                                        </div>
                                        <div class="col-md-1" style="padding-left: 3px;">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-sm-2 paddingLeft0 labelText1">
                                            Available Location
                                        </div>
                                        <div class="col-md-2 padding0">
                                            <asp:TextBox ID="availablelocation" runat="server" Style="text-transform: uppercase" AutoPostBack="true"
                                                CssClass="TextBox5 form-control " disabled="disabled"> </asp:TextBox>
                                        </div>
                                    </div>


                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-2 paddingLeft0 labelText1">
                                            Serial
                                        </div>
                                        <div class="col-md-4 padding0">
                                            <asp:TextBox ID="serialdata" runat="server" Style="text-transform: uppercase" AutoPostBack="true"
                                                CssClass="TextBox5 form-control "> </asp:TextBox>
                                        </div>
                                        <%--                                        <div class="col-md-1" style="padding-left: 3px;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                            </asp:LinkButton>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                           Category 01
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtMainCat" CssClass="TextBox5 form-control " CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtMainCat_TextChanged" TabIndex="110"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSrch_mainCat" runat="server" OnClick="lbtnSrch_mainCat_Click" TabIndex="111">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">                                        
                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                            Category 02
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtCat1" CssClass="TextBox5 form-control " CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCat1_TextChanged" TabIndex="112"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSrch_cat1" runat="server" OnClick="lbtnSrch_cat1_Click" TabIndex="113">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="panel-body  height250">
                                    <asp:GridView ID="grdPriceDetail" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdPriceDetail_PageIndexChanging">
                                        <EmptyDataTemplate>
                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                <tbody>
                                                    <tr>
                                                        <th scope="col">Location</th>
                                                        <th scope="col">Item</th>
                                                        <th scope="col">Serial</th>
                                                        <th scope="col">Current Doc No</th>
                                                        <th scope="col">Current Doc Date</th>
                                                        <th scope="col">Status</th>
                                                        <th scope="col">Current Unit Cost</th>
                                                        <th scope="col">New Status</th>
                                                        <th scope="col">New Unit Cost</th>
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
                                            <asp:TemplateField HeaderText="Channel">
                                                <ItemTemplate>
                                                    <asp:Label ID="channel" runat="server" Text='<%# Bind("ml_cate_2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_type" runat="server" Text='<%# Bind("idi_loc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_book" runat="server" Text='<%# Bind("idi_itm_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_level" runat="server" Text='<%# Bind("idi_ser_1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AssetName">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_book" runat="server" Text='<%# Bind("mi_shortdesc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Doc No">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_promotioncd" runat="server" Text='<%# Bind("idi_cur_doc_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Doc Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_item" runat="server" Text='<%#Bind("idi_cur_doc_dt","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_status" runat="server" Text='<%# Bind("idi_cur_itm_stus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Unit Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_vatExPrice" runat="server" Text='<%# Bind("idi_cur_unit_cost","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_status" runat="server" Text='<%# Bind("idi_new_itm_stus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Unit Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="pr_vatExPrice" runat="server" Text='<%# Bind("idi_new_unit_cost","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" />
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

    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>

            <div runat="server" style="width: 427px">
                <asp:Button ID="Button3" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="ImgSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>


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

                <asp:Panel runat="server" DefaultButton="ImgSearch">
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
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%--onkeydown="return (event.keyCode!=13);"--%>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:TextBox ID="txtSearchbyword" placeholder="Search by word" CausesValidation="false" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-2 paddingLeft0">
                                            <asp:LinkButton ID="ImgSearch" runat="server" OnClick="ImgSearch_Click">
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="dvResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvResult_PageIndexChanging" OnSelectedIndexChanged="dvResult_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                            </Columns>
                                            <HeaderStyle Width="10px" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </div>

        </div>
    </asp:Panel>

</asp:Content>

