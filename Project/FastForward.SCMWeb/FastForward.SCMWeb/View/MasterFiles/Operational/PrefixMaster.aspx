<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PrefixMaster.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.PrefixMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <script type="text/javascript">
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.divTar').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }

        function ConfSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfClear() {
            var selectedvalueOrd = confirm("Do you want to clear data ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfDel() {
            var selectedvalueOrd = confirm("Are you sure you want to delete ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfClose() {
            var selectedvalueOrd = confirm("Are you sure do you want to close ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

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
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
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
                sticky: false,
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

    

    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="col-sm-12 paddingLeft0">
                <div class="panel panel-default">
                    <div class="panel-body">
                        
     

                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="bs-example">
                                    <ul class="nav nav-tabs" id="myTab">
                                        <li class="active"><a href="#InvoiceType" data-toggle="tab">Invoice Type Creation</a></li>
                                        <li><a href="#Prefix" data-toggle="tab">Invoice prefix Define</a></li>
                                        <li><a href="#Sun" data-toggle="tab">Sun Accounts </a></li>
                                    </ul>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="tab-content">

                                        <div class="tab-pane active" id="InvoiceType">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>

                                                 
                                                    <div class="col-sm-12 ">
                                                        <div class="col-sm-6 ">
                                                        </div>
                                                           
                                                        <div class="col-sm-6">
                                                            <div class="col-sm-9">
                                                            </div>
                                                            <div class="row col-sm-2 ">
                                                                <div class="text-center" style="width: 70px;">
                                                                    <asp:LinkButton OnClick="lbtnSave_Click" ID="lbtnSave" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                                        CssClass=""> 
                                                                 <span class="glyphicon glyphicon-save" aria-hidden="true" style="font-size:x-large"></span>Add</asp:LinkButton>
                                                                </div>
                                                            </div>

                                                            <div class="row col-sm-1 ">
                                                                <div class="text-center" style="width: 70px;">
                                                                    <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                                        OnClientClick="return ConfClear();" CssClass=""> 
                                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:x-large"></span>Clear </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height3">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 ">
                                                        <div class="tab-content">


                                                            <div class="row ">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-12 paddingLeft20">
                                                                        <div class="col-sm-2 labelText1 paddingLeft30">
                                                                            Type Code
                                                                        </div>
                                                                        <div class="col-sm-3 labelText1 padding30">
                                                                            Type Description
                                                                        </div>
                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                            Type Category
                                                                        </div>
                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                           Main Type
                                                                        </div>
                                                                        <div class="col-sm-1 labelText1 padding30">
                                                                            Active
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-12 paddingLeft10">
                                                                        <div class="col-sm-2 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:TextBox ID="txtsrtp" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtsrtp_TextChanged" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                            <div class="col-sm-1 padding3">
                                                                                <asp:LinkButton ID="lbtnSesrtp" runat="server" OnClick="lbtnSesrtp_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:TextBox runat="server" ID="txtdes" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>
                                                                        <div class="col-sm-2 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:DropDownList AutoPostBack="true" CssClass="form-control" runat="server" ID="DropDownList2">
                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                    <asp:ListItem Value="D" Text="D" />
                                                                                    <asp:ListItem Value="S" Text="S" />
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                            <div class="col-sm-2 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:DropDownList AutoPostBack="true" CssClass="form-control" runat="server" ID="DropDownList3">
                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                    <asp:ListItem Value="CREDIT" Text="CREDIT" />
                                                                                    <asp:ListItem Value="DEBIT" Text="DEBIT" />
                                                                                    <asp:ListItem Value="CASH" Text="CASH" />
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-1 labelText1">
                                                                            <div class="col-sm-1">
                                                                                <asp:CheckBox Checked="true" Text="" ID="chkActTarg" runat="server" />
                                                                            </div>

                                                                        </div>
                                                                    </div>




                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div class="col-sm-12 ">
                                                        <div class="col-sm-4 ">
                                                        </div>
                                                         <div class="col-sm-4 ">
                                                    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                                                                 runat="server" AssociatedUpdatePanelID="UpdatePanel1">                 

                                                                        <ProgressTemplate>
                                                                            <div class="divWaiting">
                                                                            <asp:Label ID="lblWait1" runat="server"
                                                                            Text="Please wait... " />
                                                                            <asp:Image ID="imgWait1" runat="server"
                                                                            ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                            </div>
                                                                        </ProgressTemplate>

                                                            </asp:UpdateProgress>
                                                               </div>
                                                         <div class="col-sm-4 ">
                                                        </div>
                                                           </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="tab-pane " id="Sun">

                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>


                                                    <div class="col-sm-12 ">
                                                        <div class="col-sm-6 ">
                                                        </div>
                                                       
                                                        <div class="col-sm-6 ">
                                                            <div class="col-sm-9 ">
                                                            </div>
                                                            <div class="row col-sm-2">
                                                                <div class="text-center" style="width: 70px;">
                                                                <asp:LinkButton OnClick="LinkButton3_Click" ID="LinkButton3" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                                    CssClass=""> 
                                                                                   <span class="glyphicon glyphicon-save" aria-hidden="true" style="font-size:x-large"></span>Save</asp:LinkButton>
                                                            </div>
                                                                </div>

                                                            <div class="row col-sm-1">   
                                                                <div class="text-center" style="width: 70px;">
                                                                <asp:LinkButton ID="LinkButton4" OnClick="LinkButton4_Click" CausesValidation="false" runat="server"
                                                                    OnClientClick="return ConfClear();" CssClass=""> 
                                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:x-large"></span>Clear </asp:LinkButton>
                                                            </div>
                                                                </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height3">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 ">
                                                        <div class="tab-content">

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body padding0">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-9 paddingLeft20">
                                                                                        <div class="col-sm-2 labelText1 paddingLeft30">
                                                                                            Prefix
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                                            Profit Center
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                                            Type
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                                            Account Type
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                                            Account Code
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                                            Pay Type
                                                                                        </div>

                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-9 paddingLeft10">
                                                                                        <div class="col-sm-2 paddingRight15">
                                                                                            <div class="col-sm-11 padding03">
                                                                                                <asp:TextBox ID="txtpfx" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtpfx_TextChanged" CssClass="form-control" runat="server" />
                                                                                            </div>
                                                                                            <div class="col-sm-1 padding3">
                                                                                                <asp:LinkButton ID="lbtnSrch_prfx" runat="server" OnClick="lbtnSrch_prfx_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight15">
                                                                                            <div class="col-sm-11 padding03">
                                                                                                <asp:TextBox ID="txtpc" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtpc_TextChanged" CssClass="form-control" runat="server" />
                                                                                            </div>
                                                                                            <div class="col-sm-1 padding3">
                                                                                                <asp:LinkButton ID="lbtnSepc" runat="server" OnClick="lbtnSepc_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight15">
                                                                                            <div class="col-sm-11 padding03">
                                                                                                <asp:DropDownList AutoPostBack="true" CssClass="form-control" runat="server" ID="DropDownListACC">
                                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                                    <asp:ListItem Value="VAT" Text="VAT" />
                                                                                                    <asp:ListItem Value="SA" Text="SA" />
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight15">
                                                                                            <div class="col-sm-11 padding03">
                                                                                                <asp:DropDownList AutoPostBack="true" CssClass="form-control" runat="server" ID="DropDownListSUB">
                                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                                    <asp:ListItem Value="CR" Text="CREDIT" />
                                                                                                    <asp:ListItem Value="DR" Text="DEBIT" />
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight15">
                                                                                            <div class="col-sm-11 padding03">
                                                                                                <asp:TextBox runat="server" ID="Accnt" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight15">
                                                                                            <div class="col-sm-11 padding03">
                                                                                                <asp:DropDownList AutoPostBack="true" CssClass="form-control" runat="server" ID="DropDownList1">
                                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                                    <asp:ListItem Value="CASH" Text="CASH" />
                                                                                                    <asp:ListItem Value="CRED" Text="CREDIT" />
                                                                                                    <asp:ListItem Value="TOTO" Text="TOTO" />
                                                                                                    <asp:ListItem Value="DUTY" Text="DUTY" />
                                                                                                    <asp:ListItem Value="UNITY" Text="UNITY" />
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight15">

                                                                                        <div class="col-sm-1 padding3">
                                                                                            <div style="margin-top: -3px;">
                                                                                                <asp:LinkButton ID="lbtnAddTarItem" runat="server" OnClick="lbtnAddTarItem_Click">
                                                                            <span class="glyphicon glyphicon-arrow-down" style="font-size:20px;"  aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="margin-bottom: 3px;">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-heading">
                                                                            <strong>Target Details</strong>
                                                                        </div>
                                                                        <div class="panel panel-body padding0">
                                                                            <div class="divTar" id="divTar" onscroll="setScrollPosition(this.scrollTop);" style="height: 350px; overflow-y: auto;">
                                                                                <asp:GridView ID="dgvTarget" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                                    ShowHeaderWhenEmpty="True"
                                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false"
                                                                                    PagerStyle-CssClass="cssPager">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Prefix">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsls_typ" Text='<%# Bind("ledg_sales_tp") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Profit Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblprf_cr" Text='<%# Bind("ledg_pc") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsub_typ" Text='<%# Bind("ledg_sub_tp") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Account Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblacc_typ" Text='<%# Bind("ledg_acc_tp") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Account Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblacc_cd" Text='<%# Bind("ledg_acc_cd") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="150px" />
                                                                                            <HeaderStyle Width="150px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Pay Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblledg_dec" Text='<%# Bind("ledg_desc") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <div style="margin-top: -3px;">
                                                                                                    <asp:LinkButton ID="lbtnTarDelete" OnClientClick="return ConfDel();" Width="100%" runat="server" OnClick="lbtnTarDelete_Click">
                                                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10px" />
                                                                                            <HeaderStyle Width="10px" />
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
                                                      <div class="col-sm-12 ">
                                                        <div class="col-sm-4 ">
                                                        </div>
                                                         <div class="col-sm-4 ">
                                                    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
                                                                 runat="server" AssociatedUpdatePanelID="UpdatePanel1">                 

                                                                        <ProgressTemplate>
                                                                            <div class="divWaiting">
                                                                            <asp:Label ID="lblWait2" runat="server"
                                                                            Text="Please wait... " />
                                                                            <asp:Image ID="imgWait2" runat="server"
                                                                            ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                            </div>
                                                                        </ProgressTemplate>

                                                            </asp:UpdateProgress>
                                                               </div>
                                                         <div class="col-sm-4 ">
                                                        </div>
                                                           </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>

                                        <div class="tab-pane " id="Prefix">

                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>


                                                    <div class="col-sm-12 ">
                                                        <div class="col-sm-6 ">
                                                        </div>
                                                       
                                                        <div class="col-sm-6 ">
                                                            <div class="col-sm-9 ">
                                                            </div>
                                                            <div class="row col-sm-2">  
                                                                <div class="text-center" style="width: 70px;">
                                                                <asp:LinkButton OnClick="LinkButton1_Click" ID="LinkButton1" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                                    CssClass=""> 
                                                                                   <span class="glyphicon glyphicon-save" aria-hidden="true" style="font-size:x-large"></span>Save</asp:LinkButton>
                                                                    </div>
                                                            </div>

                                                            <div class="row col-sm-1">  
                                                                <div class="text-center" style="width: 70px;">
                                                                <asp:LinkButton ID="cleardefine" OnClick="cleardefine_Click" CausesValidation="false" runat="server"
                                                                    OnClientClick="return ConfClear();" CssClass=""> 
                                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:x-large"></span>Clear </asp:LinkButton>
                                                           </div>
                                                                     </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height3">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 ">
                                                        <div class="tab-content">


                                                            <div class="row ">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-12 paddingLeft20">
                                                                        <div class="col-sm-2 labelText1 paddingLeft30">
                                                                            Profit Center
                                                                        </div>
                                                                        <div class="col-sm-2 labelText1 padding30">
                                                                            Prefix
                                                                        </div>
                                                                        <div class="col-sm-3 labelText1 padding30">
                                                                            Prefix No
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-12 paddingLeft10">
                                                                        <div class="col-sm-2 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:TextBox ID="textpc1" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="textpc1_TextChanged" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                            <div class="col-sm-1 padding3">
                                                                                <asp:LinkButton ID="lbtnpc1" runat="server" OnClick="lbtnpc1_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:TextBox ID="textprfx1" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="textprfx1_TextChanged" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                            <div class="col-sm-1 padding3">
                                                                                <asp:LinkButton ID="lbtnprfx1" runat="server" OnClick="lbtnprfx1_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3 paddingRight15">
                                                                            <div class="col-sm-11 padding03">
                                                                                <asp:TextBox runat="server" ID="textprfxno" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                          <div class="col-sm-12 ">
                                                        <div class="col-sm-4 ">
                                                        </div>
                                                         <div class="col-sm-4 ">
                                                    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
                                                                 runat="server" AssociatedUpdatePanelID="UpdatePanel1">                 

                                                                        <ProgressTemplate>
                                                                            <div class="divWaiting">
                                                                            <asp:Label ID="lblWait3" runat="server"
                                                                            Text="Please wait... " />
                                                                            <asp:Image ID="imgWait3" runat="server"
                                                                            ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                            </div>
                                                                        </ProgressTemplate>

                                                            </asp:UpdateProgress>
                                                               </div>
                                                         <div class="col-sm-4 ">
                                                        </div>
                                                           </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>


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
    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>

    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
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
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="lbtnSearch_Click"></asp:TextBox>
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
    </asp:Panel>

    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('#myTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                //  alert($(this).attr('href'));
                document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
              });

              $(document).ready(function () {
                  var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
                // alert(tab);
                $('#myTab a[href="' + tab + '"]').tab('show');
            });
        }
    </script>

</asp:Content>
