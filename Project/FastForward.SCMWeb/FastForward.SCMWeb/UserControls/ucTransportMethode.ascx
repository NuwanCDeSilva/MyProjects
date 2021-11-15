<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTransportMethode.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucTransportMethode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />--%>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />
<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />
<script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
<script type="text/javascript">
    function ConfirmDelete() {
        var selectedvaldelitm = confirm("Do you want to remove ?");
        if (selectedvaldelitm) {
            return true;
        } else {
            return false;
        }
    };
    function ConfirmClearForm() {
        var selectedvalueOrd = confirm("Do you want to clear all details ?");
        if (selectedvalueOrd) {
            return true;
        } else {
            return false;
        }
    };
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
        if (jQuery('.toast-item-wrapper') != null) {
            jQuery('.toast-item-wrapper').remove();
        }
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
    </script>
<style>
    #BodyContent_ucTransportMethode_pnlSearch {
    position: absolute;
    z-index: 11001;
    left: 0.5px !important;
    top: 0  !important;
}
</style>
<div class="row">
    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-10">
                    <asp:HiddenField id="hdfDelete" runat="server" Value="0"/>
                </div>
                <div class="col-sm-2 buttonRow">
                    <asp:LinkButton ID="lbtnClearData" runat="server"  OnClick="lbtnClearData_Click"
                        OnClientClick="return ConfirmClearForm()" CausesValidation="false" CssClass="floatRight">
                         <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-2 padding0 labelText1">
                    Transport Method
                </div>
                <div class="col-sm-3 padding0">
                    <asp:DropDownList ID="ddlTransportMe" AutoPostBack="true" OnSelectedIndexChanged="ddlTransportMe_SelectedIndexChanged" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                        
                    </asp:DropDownList>
                </div>
                <div class="col-sm-1">

                </div>
                <div class="col-sm-2 paddingright0 labelText1  ">
                    Service By
                </div>
                <div class="col-sm-3 padding0">
                    <asp:DropDownList AutoPostBack="true" ID="ddlServiceBy" OnSelectedIndexChanged="ddlServiceBy_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0" Text="--Select--" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-2 padding0 labelText1">
                    <asp:Label ID="lblSubLvl" runat="server" />
                </div>
                <div class="col-sm-3 padding0">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtSubLvl_TextChanged1" ID="txtSubLvl" CssClass="form-control" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-1 padding3">
                        <asp:LinkButton ID="lbtnSeVehicle" CausesValidation="false" runat="server" OnClick="lbtnSeVehicle_Click">
                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                <div class="col-sm-2 paddingright0 labelText1">
                    <asp:Label Text="Remarks" runat="server" />
                </div>
                <div class="col-sm-4 padding0">
                   <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pannelNxtLvl">
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-2 padding0 labelText1">
                        <asp:Label ID="lblNxtLvlD1" Text="" runat="server" />
                    </div>
                    <div class="col-sm-3 padding0">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtNxtLvlD1" AutoPostBack="true" OnTextChanged="txtNxtLvlD1_TextChanged" CssClass="form-control" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-1 padding3">
                        <asp:LinkButton ID="lbtnSeDriver" CausesValidation="false" runat="server" OnClick="lbtnSeDriver_Click">
                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-2">
                        <asp:Label ID="lblNxtLvlD2" Text="" runat="server" CssClass="labelText1" />
                    </div>
                    <div class="col-sm-3 padding0">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                 <asp:TextBox ID="txtNxtLvlD2" AutoPostBack="true" OnTextChanged="txtNxtLvlD2_TextChanged" CssClass="form-control" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-1 padding3">
                        <asp:LinkButton ID="lbtnSeHelper" CausesValidation="false" runat="server" OnClick="lbtnSeHelper_Click">
                              <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-2 padding0 labelText1">
                    <asp:Label Text="No of packing" runat="server" />
                </div>
                <div class="col-sm-2 padding0">
                    <asp:TextBox ID="txtNoOfPacking" CssClass="txtNoOfPacking form-control" runat="server" />
                </div>
                <div class="col-sm-1" style="padding-left:3px; padding-right:0px;">
                    <div class="buttonRow">
                        <div>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="false" CssClass="floatRight" 
                                    OnClick="lbtnAdd_Click">
                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div  class="col-sm-12" >
            <div style="height:200px; overflow-x:hidden; overflow-y:auto;">
               <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                <asp:GridView ID="dgvTrns" CssClass="table table-hover table-striped" runat="server"
                    GridLines="None" PagerStyle-CssClass="cssPager"
                    EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                    <EditRowStyle BackColor="MidnightBlue" />
                    <Columns>
                                               
                        <asp:TemplateField HeaderText="Tra. Method">
                            <ItemTemplate>
                                <asp:Label id="lblTraMe" Text='<%# Bind("Itrn_trns_pty_tp") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service By">
                            <ItemTemplate>
                                <asp:Label id="lblSerBy" Text='<%# Bind("Itrn_trns_pty_cd") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                             <HeaderTemplate>
                                <asp:Label id="lblHedSubLvl" Text='Ref #' runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label id="lblItmSubLvl" Text='<%# Bind("Itrn_ref_no") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label id="lblRemarks" Text='<%# Bind("Itrn_rmk") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField>
                             <HeaderTemplate>
                                <asp:Label id="lblHedNxtLvlD1" Text='Driver/Hand Over' runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label id="lblItmNxtLvlD1" Text='<%# Bind("Itrn_anal1") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                             <HeaderTemplate>
                                <asp:Label id="lblHedNxtLvlD2" Text='Helper' runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label id="lblItmNxtLvlD2" Text='<%# Bind("Itrn_anal2") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label id="lblRowNo" Text='<%# Bind("_grdRowNo") %>' Visible="false" runat="server" />
                            </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="margin-top: -3PX; width: 10px;">
                                    <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="false"
                                        OnClientClick="return ConfirmDelete()" OnClick="lbtnDel_Click">
                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <PagerStyle CssClass="cssPager"></PagerStyle>
                </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            </div>
        </div>
    


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
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlSerch">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlSerch">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 350px; width: 700px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row height16">
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="col-sm-4 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-6 paddingRight5">
                                                <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-6">
                                    <div class="col-sm-4 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSerByKey_TextChanged"></asp:TextBox>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
<script>
    Sys.Application.add_load(fun);
    function fun() {
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        //else {
        //    alert('jQuery is  loaded');
        //}

            $('.txtNoOfPacking').keypress(function (evt) {
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
                    $(this).value = str.slice(0, -1);
                    alert('Maximum 2 characters are allowed ');
                    return false;
                }
            });	


        $('.txtNoOfPacking').mousedown(function (e) {
            if (e.button == 2) {
                alert('This functionality is disabled !');
                return false;
            } else {
                return true;
            }
        });
    }
</script>
