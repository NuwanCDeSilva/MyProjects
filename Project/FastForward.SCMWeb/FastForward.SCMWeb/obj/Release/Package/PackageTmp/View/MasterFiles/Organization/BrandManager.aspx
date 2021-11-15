<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BrandManager.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Organization.BrandManager" %>

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
            var selectedvalueOrd = confirm("Are you sure you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClose() {
            var selectedvalueOrd = confirm("Are you sure you want to close ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
         
        function ConfDel() {
            var selectedvalueOrd = confirm("Are you sure you want to delete and save ?");
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
      
        $("#txtEDate").change(function () {
            alert("ddfsa");
            var startDate = document.getElementById("txtSDate").value;
            var endDate = document.getElementById("txtEDate").value;
            alert("ddfsa");
            if ((Date.parse(startDate) >= Date.parse(endDate))) {
                alert("End date should be greater than Start date");
                document.getElementById("txtEDate").value = "";
            }
        });
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
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
  

  <div class="col-sm-12"  style="height: 40px; margin-top: 5px;">
  <div class="panel panel-default">
 <div class="row">

        <div class="col-sm-12">
            <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upMain">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Label ID="lblWait13" runat="server"
                            Text="Please wait... " />
                        <asp:Image ID="imgWait13" runat="server"
                            ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="upMain">
                <ContentTemplate>

                        <div class="col-sm-12 ">
                            <div class="row "  >
                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                            <asp:HiddenField ID="hdfSaveTp" Value="0" runat="server" />
                         


                                 <div class="col-sm-6">
                                     </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-5">
                                     </div>
                                   <div class="col-sm-7 paddingRight0 text-left" style="height: 40px; margin-top: 10px; margin-bottom: -8px;">
                                    <div class="buttonRow" style="height: 30px; margin-top: -12px; ">
                                        <div class="col-sm-4  text-center" style="width:110px;">
                                            <asp:LinkButton OnClick="lbtnSave_Click" ID="lbtnSave" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                CssClass=""> 
                                            <span class="glyphicon glyphicon-save" aria-hidden="true"></span>Save</asp:LinkButton>
                                        </div>
                                         <div class="col-sm-4   text-center" style="width:110px;">
                                            <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                OnClientClick="return ConfClear();" CssClass=""> 
                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-4 padding0 text-center" style="width:110px;">
                                            <asp:LinkButton ID="lbtnUploadFile" OnClick="lbtnUploadFile_Click" CausesValidation="false" runat="server" CssClass=""> 
                                            <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Upload</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                            <div class="row">
                            <div class="col-sm-12">
                             <div class="panel panel-default">
                                <div class="panel panel-default">
                                <div class="panel-heading paddingtop10 paddingtopbottom3">
                                    <div class="row">
                                        <div class="col-sm-7">
                                            <strong>Brand Managers-Setup</strong>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                    

                        <div class="col-sm-4 paddingLeft0 paddngup20 paddingRight0">
                            <div class="panel panel-default">
                                <div class="panel panel-body padding0" style="height: 46px;">
                                    <div class="row" style="margin-top: 2px;">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 paddingRight0 labelText1">
                                                Brand Managers
                                            </div>
                                            <div class="col-sm-5 padding0">
                                                <asp:TextBox runat="server" AutoPostBack="true" Style="text-transform: uppercase" ID="txtManager" OnTextChanged="txtManager_TextChanged" CssClass="form-control" />
                                            </div>
                                            <div class="col-sm-3 padding3">
                                                <asp:LinkButton ID="lbtnSeManager" CausesValidation="false" runat="server" Onclick="lbtnSeManager_Click1">
                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 2px;">
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-10">
                                            <asp:Label ID="lblCompany" ForeColor="DarkBlue" Text="" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="col-sm-8 paddingRight0 paddingLeft0">
                            <div class="panel panel-default">
                                <div class="row" style="margin-top: 5px; margin-left: 10px; height: 41px;">

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Start  Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox runat="server" ID="txtSDate" OnTextChanged="txtSDate_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSDate" runat="server"   CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSDate"
                                                            PopupButtonID="lbtnSDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        End  Date
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                        <asp:TextBox runat="server" ID="txtEDate"  OnTextChanged="txtEDate_TextChanged" AutoPostBack="true"  CausesValidation="false" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnEDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEDate"
                                                            PopupButtonID="lbtnEDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="row"> 
                                            <div class="col-sm-3 labelText1 paddingLeft30 paddingRight15 fontsize12">
                                                                        View
                                              </div>
                                             <div class="col-sm-9 padding3">
                                               <asp:LinkButton ID="lbtnview"  runat="server" OnClick="lbtnview_Click" >
                                              <span class="glyphicon glyphicon-search" style="font-size:large"  aria-hidden="true"></span>
                                              </asp:LinkButton>
                                                 </div>
                                        </div>  
                                             </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-12 paddingRight0 paddingLeft0">
                            <div class="panel panel-default">
                                <div class="panel panel-body paddingRight0">
                                    <div class="row height10 paddingbottom5">
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3 ">
                                                <div class="col-sm-3 padding0 labelText1">
                                                    Define By
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control" runat="server" ID="DropDownList1">
                                                        <asp:ListItem Value="0" Text="--Select--" />
                                                        <asp:ListItem Value="COM" Text="Company" />
                                                        <asp:ListItem Value="CHNL" Text="Channel" />
                                                        <asp:ListItem Value="SCHNL" Text="Sub Channel" />
                                                        <asp:ListItem Value="AREA" Text="Area" />
                                                        <asp:ListItem Value="REGION" Text="Region" />
                                                        <asp:ListItem Value="ZONE" Text="Zone" />
                                                        <asp:ListItem Value="PC" Text="Profit Center" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-1 paddingRight0">
                                                <div class="col-sm-5 padding0 labelText1">
                                                    Code
                                                </div>
                                                <div class="col-sm-7 padding0">
                                                    <asp:TextBox AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtDefCode_TextChanged1" ID="txtDefCode" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                </asp:LinkButton>
                                           </div>
                                                 <div class="col-sm-4 labelText1 padding3">
                                                    <asp:Label Text="" ForeColor="DarkBlue" ID="Label1" runat="server" />
                                                </div>
                                           <div class="col-sm-3 labelText1">
                                              <div class="col-sm-1">
                                                 <asp:CheckBox Checked="true"  Text="" ID="chkActTarg" runat="server" />
                                                 </div>
                                                <div class="col-sm-10 padding3">
                                             <asp:Label Text="Active" runat="server" />
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
                                                    <div class="panel panel-body padding0">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-9 paddingLeft20">
                                                                    <div class="col-sm-2 labelText1 paddingLeft30">
                                                                        Brand
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding30">
                                                                        Category 1
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding30">
                                                                         Category 2
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding30">
                                                                         Category 3
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding30">
                                                                        Category 4
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding30">
                                                                        Category 5
                                                                    </div>

                                                                </div>
                                                               
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-9 paddingLeft10">
                                                                    <div class="col-sm-2 paddingRight15">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtBrand" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtBrand_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeBrand" runat="server"  OnClick="lbtnSeBrand_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingRight15">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat1" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat1_TextChanged"  CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSrch_Cat1" runat="server" OnClick="lbtnSrch_Cat1_Click" >
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingRight15">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat2" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat2_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSrch_Cat2" runat="server" OnClick="lbtnSrch_Cat2_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingRight15">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat3" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat3_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSrch_Cat3" runat="server" OnClick="lbtnSrch_Cat3_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingRight15">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat4" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat4_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSrch_Cat4" runat="server" OnClick="lbtnSrch_Cat4_Click" >
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingRight15">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat5" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat5_TextChanged"  CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSrch_Cat5" runat="server" OnClick="lbtnSrch_Cat5_Click" >
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 paddingRight15">
                                                                    
                                                                    <div class="col-sm-1 padding3">
                                                                        <div style="margin-top: -3px;">
                                                                            <asp:LinkButton ID="lbtnAddTarItem" runat="server" OnClick="lbtnAddTarItem_Click" >
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
                                                                    <asp:TemplateField HeaderText="Manager">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblman_cd" Text='<%# Bind("Mba_emp_id") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Define By">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmbaDefBy" Text='<%# Bind("Mba_pty_tp") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Define Cd">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmbaDefCd" Text='<%# Bind("Mba_pty_cd") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmba_brd" Text='<%# Bind("Mba_brnd") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category 1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmba_cat1" Text='<%# Bind("Mba_ca1") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category 2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmba_cat2" Text='<%# Bind("Mba_ca2") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category 3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmba_cat3" Text='<%# Bind("Mba_ca3") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category 4">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmba_cat4" Text='<%# Bind("Mba_ca4") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category 5">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmba_cat5" Text='<%# Bind("Mba_ca5") %>' runat="server" Width="100%" />
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
                                 </div>
                                 </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>



                        
                                 </div>
                                 </div>
                               
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true"  runat="server" OnTextChanged="lbtnSearch_Click" ></asp:TextBox>
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
                                        <asp:Button ID="lbtnUploadExcel" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
<%-- Pnl Process  --%>
    <asp:UpdatePanel ID="upProcess" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn11" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcProc" runat="server" Enabled="True" TargetControlID="btn11"
                PopupControlID="pnlExcelProcces" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel13">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelProcces">
                <div runat="server" class="panel panel-default height45 width700 ">
                    <div class="panel panel-default">
                        <div class="panel-heading height30">

                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelProccesError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelProccesSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelProccesInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnProcClose" runat="server" OnClick="lbtnProcClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <asp:Panel runat="server" ID="Panel3">
                                        <div class="col-sm-12 ">
                                            <div id="" class="alert alert-info alert-success" role="alert">
                                                <div class="col-sm-1 padding0">
                                                    <strong>Alert!</strong>
                                                </div>
                                                <div class="col-sm-10 padding0">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblProcess" Text="Excel file upload completed. Do you want to process ?" runat="server"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="lbtnExcelProcess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="lbtnExcelProcess_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
    </asp:UpdatePanel>
      <script>
          $(document).ready(function () {
              // console.log('redy doc');
              maintainScrollPosition();
          });
          </script>
</asp:Content>





