<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="LocationMaster.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Organization.LocationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

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
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
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


        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function showErrorToast() {
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
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

    <script type="text/javascript">
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Insert Data?");


            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "0";
            }


        };

        function ConfirmationMessage() {
            var selectedvalue = confirm("Do you want to save?");


            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "0";
            }


        };


        function ClearMessage() {
            var selectedvalue = confirm(" Do you want to clear data?");


            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "0";
            }


        };



    </script>

    <script type="text/javascript">
        function filterDigits(eventInstance) {
            eventInstance = eventInstance || window.event;
            key = eventInstance.keyCode || eventInstance.which;
            if ((47 < key) && (key < 58) || key == 8) {
                return true;
            } else {
                if (eventInstance.preventDefault)
                    eventInstance.preventDefault();
                eventInstance.returnValue = false;
                return false;
            } //if
        } //filterDigits
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
    <style>
        .panel {
            margin-bottom: 1PX;
            margin-top: 0PX;
        }

        .panel-body {
            padding-bottom: 1PX;
            padding-top: 5PX;
        }

        .row {
            /*margin-left: -15px;*/
            margin-right: -8px;
        }

        .toast-item-wrapper {
            width: 295px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <div class="col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <strong><b>Location Master</b></strong>
                        </div>
                        <div class="panel panel-body" style="height: 367px;">
                            <div class="row buttonRow">
                                <asp:HiddenField ID="hdfSaveTp" Value="0" runat="server" />
                                <div class="col-sm-9">
                                </div>
                                <div class="col-sm-3 ">
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="LinkButton11" runat="server" CausesValidation="false" CssClass="floatRight"
                                            OnClientClick="return ConfirmationMessage()" OnClick="LinkButton11_Click">
                                  <span class="glyphicon glyphicon-saved fontsize20" aria-hidden="true"></span>Save                                
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClientClick="return ClearMessage()" OnClick="LinkButton6_Click">
                                  <span class="glyphicon glyphicon-refresh fontsize20"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">
                                  <span class="glyphicon glyphicon-upload fontsize20"></span>Upload
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <%--<div class="panel panel-default" style="padding-left: 0px; padding-right: 0px">
                                <div class="panel panel-body" style="padding-left: 1px; padding-right: 1px">--%>
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 11px; padding-right: 0px;">
                                            <div class="panel panel-default" style="padding-left: 0px; padding-right: 0px">
                                                <div class="panel panel-body" style="padding-left: 1px; padding-right: 1px">
                                                    <div class="col-sm-3" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading paddingtopbottom0">
                                                                Company Details
                                                            </div>
                                                            <div class="panel-body" style="height: 150px; overflow-y: auto; overflow-x: hidden">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Company
                                                                            </div>

                                                                            <div class="col-sm-3 padding0">
                                                                                <asp:TextBox ID="TextBox1" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"
                                                                                    Width="100px" runat="server" Style="text-transform: uppercase"
                                                                                    CssClass="TextBox1 form-control"></asp:TextBox></td>
                                                                            </div>


                                                                            <div class="col-sm-1"></div>

                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                                <div class="col-sm-4"></div>

                                                                                <div class="col-sm-8 padding0">
                                                                                    <asp:Label ID="Labelcompanyd" runat="server" Text=""></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Operation 
                                                                            </div>

                                                                            <div class="col-sm-3 padding0">
                                                                                <asp:TextBox ID="TextBox2" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged" Style="text-transform: uppercase" Width="100px" runat="server" CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1"></div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                                <div class="col-sm-4"></div>

                                                                                <div class="col-sm-8 padding0">
                                                                                    <asp:Label ID="lblOperation" runat="server" Text=""></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Channel
                                                                            </div>

                                                                            <div class="col-sm-3 padding0">
                                                                                <asp:TextBox AutoPostBack="true" OnTextChanged="TextBox3_TextChanged" ID="TextBox3" Style="text-transform: uppercase" Width="100px" runat="server" CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1"></div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4"></div>
                                                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <div class="col-sm-8 padding0">
                                                                                            <asp:Label ID="Labelchanel" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Sub Channel
                                                                            </div>

                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                <ContentTemplate>

                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:TextBox AutoPostBack="true" OnTextChanged="TextBox4_TextChanged" ID="TextBox4" Style="text-transform: uppercase" Width="100px" runat="server" CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                                    </div>

                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="col-sm-1"></div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>

                                                                                <asp:HiddenField ID="HiddenField1" Value="" runat="server" />
                                                                                <asp:HiddenField ID="hdfCurrDate" Value="" runat="server" />
                                                                                <asp:Label ID="Labelcurrent" runat="server" Visible="false" Text="Label"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>


                                                                        <div class="row">
                                                                            <div class="col-sm-12">

                                                                                <div class="col-sm-4"></div>

                                                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <div class="col-sm-8 padding0">
                                                                                            <asp:Label ID="Labelsubchanel" runat="server"></asp:Label>
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
                                                    <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading paddingtopbottom0">
                                                                Location Details
                                                            </div>
                                                            <div class="panel-body" style="height: 150px;">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-2 paddingLeft0 labelText1">
                                                                                    Location Code
                                                                                </div>
                                                                                <div class="col-sm-2 padding0">
                                                                                    <asp:TextBox ID="TextBox5" runat="server" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="TextBox5_TextChanged"
                                                                                        CssClass="TextBox5 form-control "> </asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1" style="padding-left: 3px;">
                                                                                    <asp:LinkButton ID="lbtnSeLocation" runat="server" OnClick="lbtnSeLocation_Click1">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="LinkButtonsearch" Visible="false" runat="server" OnClick="LinkButtonsearch_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <div class="col-sm-1 labelText1">
                                                                                    Ref
                                                                                </div>
                                                                                <div class="col-sm-2 padding0">
                                                                                    <asp:TextBox ID="TextBox6" Style="text-transform: uppercase" Width="100px" runat="server" CssClass="TextBox6 form-control  locCodeReq" OnTextChanged="TextBox6_TextChanged"></asp:TextBox>

                                                                                </div>
                                                                                <div class="col-sm-1 labelText1">
                                                                                    Status
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                                                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                    Loc.Name
                                                                                </div>
                                                                                <div class="col-sm-10 paddingLeft0">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox ID="TextBox7" Style="text-transform: uppercase" runat="server"
                                                                                                CssClass="TextBox7 form-control  locCodeReq" OnTextChanged="TextBox7_TextChanged"></asp:TextBox>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                    Type
                                                                                </div>
                                                                                <div class="col-sm-3 paddingLeft0">
                                                                                    <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true"
                                                                                        CssClass="form-control">
                                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-2 labelText1">
                                                                                    Categery
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="True"
                                                                                        CssClass="form-control">
                                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                    Grade
                                                                                </div>
                                                                                <div class="col-sm-3 paddingLeft0">
                                                                                    <asp:DropDownList ID="DropDownList10" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-2 labelText1">
                                                                                    Suspended
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                    Def Profit center
                                                                                </div>
                                                                                <div class="col-sm-3 paddingLeft0">
                                                                                    <asp:TextBox ID="TextBox8" Style="text-transform: uppercase"
                                                                                        runat="server" CssClass="form-control  locCodeReq" OnTextChanged="TextBox8_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2 labelText1">
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-12 paddingLeft0">
                                                                                <div class="col-sm-1 height22" style="margin-top: 2px;">
                                                                                    <asp:CheckBox ID="CheckBox1" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                                                </div>
                                                                                <div class="col-sm-3 paddingLeft0 labelText1" style="margin-left: -18px;">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Maintain Sub Location"></asp:Label>
                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                </div>
                                                                                <div class="col-sm-2 padding0" style="margin-left: -7px;">
                                                                                    Main Loc Code 
                                                                                </div>
                                                                                <div class="col-sm-2  padding0" style="margin-left: -3px;">
                                                                                    <asp:TextBox ID="TextBox9" Style="text-transform: uppercase" AutoPostBack="true" runat="server" CssClass="form-control  locCodeReq" OnTextChanged="TextBox9_TextChanged"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1" style="padding-left: 3px;">
                                                                                    <asp:LinkButton ID="lbtnSeMainLoc" runat="server" OnClick="lbtnSeMainLoc_Click">
                                                                                        <span class="glyphicon glyphicon-search"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <%-- <div class="row">
                                                                            <div class="col-sm-12">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel panel-heading" style="padding-bottom:1px;padding-top:1px; height:15px;">
                                                                                <div style="margin-top:-3px;margin-left:-7px;">
                                                                                    Category
                                                                                </div>
                                                                                </div>
                                                                                <div class="panel panel-body">
                                                                                    <div class=" col-sm-12 padding0">
                                                                                        <div class="col-sm-4 padding0">
                                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                                Main
                                                                                            </div>
                                                                                            <div class="col-sm-7 paddingLeft0">
                                                                                                <asp:DropDownList ID="ddlMainCat" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                           
                                                                                        </div>
                                                                                        <div class="col-sm-4 padding0">
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                Channel
                                                                                            </div>
                                                                                             <div class="col-sm-7 paddingLeft0">
                                                                                                <asp:TextBox AutoPostBack="true" ID="txtChannelCat" Style="text-transform: uppercase" OnTextChanged="txtChannelCat_TextChanged"
                                                                                                    runat="server" CssClass="form-control  locCodeReq txtMainCategory">
                                                                                                </asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0" style="margin-left: -12px;">
                                                                                                <asp:LinkButton ID="lbtnSeChannel" runat="server" OnClick="lbtnSeChannel_Click">
                                                                                                <span class="glyphicon glyphicon-search"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4 padding0">
                                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                                Other
                                                                                            </div>
                                                                                            <div class="col-sm-7 paddingLeft1">
                                                                                                <asp:TextBox ID="txtOtherCat" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtOtherCat_TextChanged"
                                                                                                    runat="server" CssClass="form-control  locCodeReq txtOtherCat">
                                                                                                </asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1 padding0" style="margin-left: -12px;">
                                                                                                <asp:LinkButton ID="lbtnSeOther" runat="server" OnClick="lbtnSeOther_Click">
                                                                                                <span class="glyphicon glyphicon-search"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            </div>
                                                                        </div>--%>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <div class="row">
                                                                    <div class="col-sm-12">

                                                                        <%-- <div class="row">
                                                            <div class="col-sm-2 paddingRight0">
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-3">
                                                                    </div>
                                                                </div>

                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                                <ContentTemplate>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>



                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>


                                                        <div class="row">


                                                            <div class="col-sm-1">
                                                            </div>

                                                            <div class="col-sm-2">
                                                            </div>

                                                            <div class="col-sm-1">
                                                            </div>

                                                            <div class="col-sm-2">
                                                            </div>

                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>

                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>


                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-3"></div>
                                                            <div class="col-sm-2">
                                                            </div>






                                                        </div>


                                                        <div class="row">

                                                            <div class="col-sm-5">


                                                                <div class="row">
                                                                    <div class="col-sm-12">


                                                                        <div class="col-sm-1">
                                                                        </div>
                                                                        <div class="col-sm-7">
                                                                        </div>


                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 height10">
                                                                    </div>
                                                                </div>


                                                                <div class="row">


                                                                    <div class="col-sm-5">
                                                                    </div>
                                                                    <div class="col-sm-2"></div>

                                                                    <div class="col-sm-2">
                                                                    </div>

                                                                    <asp:CheckBox ID="CheckBox_checking" Checked="true" Visible="false" runat="server" />
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 ">
                                                                    </div>
                                                                </div>



                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-1"></div>


                                                        </div>--%>

                                                                        <div class="row">
                                                                        </div>


                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading paddingtopbottom0">
                                                                Insurance Details
                                                            </div>
                                                            <div class="panel-body" style="height: 150px;">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1 paddingRight0">
                                                                                <!--   Records -->
                                                                                Allow no of RCC
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox ID="TextBox10" Width="100px" runat="server" CssClass="TextBox10 form-control  locCodeReq" MaxLength="4"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Is Insured 
                                                                            </div>
                                                                            <div class="col-sm-2">

                                                                                <asp:DropDownList ID="DropDownList5" AutoPostBack="true" runat="server"
                                                                                    CssClass="ddlInsured form-control" Width="100px" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="yes" Value="1"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Value
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox ID="TextBox11" Text="" Width="100px" runat="server" CssClass="InsValue form-control  locCodeReq"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <div class="row">

                                                                            <div class="col-sm-4 labelText1">
                                                                                Bank Gurranty
                                                                            </div>

                                                                            <div class="col-sm-2">

                                                                                <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control" Width="100px"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="yes" Value="1"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>


                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Value
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox ID="TextBox12" Width="100px" runat="server" CssClass="TextBox12 form-control  locCodeReq"></asp:TextBox>

                                                                            </div>
                                                                        </div>



                                                                        <div class="row" style="display: none">
                                                                            <div class="col-sm-4">
                                                                                Direct Operated
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">

                                                                            <div class="col-sm-4">
                                                                                Online
                                                                            </div>

                                                                            <div class="col-sm-2">

                                                                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" Width="100px">
                                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="yes" Value="1"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>





                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>
                                                        <!--start row 03 -->




                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 11px; padding-right: 0px;">
                                            <div class="panel panel-default" style="padding-left: 0px; padding-right: 0px">
                                                <div class="panel panel-body" style="padding-left: 1px; padding-right: 1px">

                                                    <div class="col-sm-8" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading paddingtopbottom0">
                                                                Location Address
                                                            </div>
                                                            <div class="panel-body" style="height: 150px;">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-sm-1 labelText1">
                                                                                Address 1
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:TextBox ID="TextBox14" Style="text-transform: uppercase" Width="400px" runat="server" CssClass="form-control  locCodeReq" AutoPostBack="true" OnTextChanged="TextBox14_TextChanged"></asp:TextBox>

                                                                            </div>
                                                                            <div class="col-sm-4"></div>


                                                                            <div class="col-sm-1 labelText1">
                                                                                Country
                                                                            </div>
                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="col-sm-2">

                                                                                        <asp:TextBox ID="TextBox16" Style="text-transform: uppercase" Width="100px" runat="server"
                                                                                            OnTextChanged="TextBox16_TextChanged" AutoPostBack="true"
                                                                                            CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                                        <asp:Label ID="Labelcountry" runat="server" Visible="false" Text=""></asp:Label>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>

                                                                            </div>

                                                                        </div>


                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>






                                                                        <div class="row">

                                                                            <div class="col-sm-1 labelText1">
                                                                                Address 2
                                                                            </div>
                                                                            <div class="col-sm-3">

                                                                                <asp:TextBox ID="TextBox17" Style="text-transform: uppercase" Width="400px" runat="server" CssClass="form-control  locCodeReq" AutoPostBack="true" OnTextChanged="TextBox17_TextChanged"></asp:TextBox>

                                                                            </div>
                                                                            <div class="col-sm-4"></div>

                                                                            <div class="col-sm-1 labelText1">
                                                                                Province
                                                                            </div>

                                                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="col-sm-2">

                                                                                        <asp:TextBox ID="TextBox19" Style="text-transform: uppercase" Width="100px" runat="server"
                                                                                            AutoPostBack="true" OnTextChanged="TextBox19_TextChanged"
                                                                                            CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                                        <asp:Label ID="Labelprovince" runat="server" Visible="false" Text=""></asp:Label>

                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="col-sm-1">
                                                                            </div>
                                                                            <div class="col-sm-1">

                                                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>

                                                                            </div>


                                                                        </div>


                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>

                                                                        <!-- -->
                                                                        <div class="row">

                                                                            <div class="col-sm-1 labelText1">Email</div>


                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox ID="TextBox18" Width="400px" runat="server" CssClass="TextBox18 form-control  locCodeReq"></asp:TextBox>



                                                                            </div>

                                                                            <div class="col-sm-5">
                                                                            </div>


                                                                            <div class="col-sm-1 labelText1">
                                                                                District
                                                                            </div>
                                                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                                <ContentTemplate>

                                                                                    <div class="col-sm-2">


                                                                                        <asp:TextBox ID="TextBox22" Style="text-transform: uppercase" Width="100px" runat="server"
                                                                                            AutoPostBack="true" OnTextChanged="TextBox22_TextChanged"
                                                                                            CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                                        <asp:Label ID="LabelDistrict" runat="server" Visible="false" Text=""></asp:Label>

                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="col-sm-1">

                                                                                <asp:LinkButton ID="LinkButtondistrict" runat="server" OnClick="LinkButtondistrict_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>

                                                                            </div>





                                                                        </div>
                                                                        <!-- -->









                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-1 labelText1">
                                                                                Tel
                                                                            </div>

                                                                            <div class="col-sm-2">

                                                                                <asp:TextBox ID="TextBox20" Width="100px" runat="server" CssClass=" TextBox20 form-control  locCodeReq"></asp:TextBox>
                                                                                <%-- <asp:TextBox ID="TextBox20" Width="100px" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control"></asp:TextBox>--%>
                                                                            </div>

                                                                            <div class="col-sm-2 labelText1" style="padding-left: 38px;">
                                                                                Fax
                                                                            </div>
                                                                            <div class="col-sm-3">

                                                                                <asp:TextBox ID="TextBox21" Width="134px" runat="server" CssClass="TextBox21 form-control  locCodeReq" MaxLength="10"></asp:TextBox>

                                                                            </div>

                                                                            <!--district -->
                                                                            <div class="col-sm-1 labelText1">Town</div>
                                                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:TextBox ID="TextBoxtown" Width="100px" runat="server"
                                                                                            OnTextChanged="TextBoxtown_TextChanged" AutoPostBack="true"
                                                                                            CssClass="form-control  locCodeReq"></asp:TextBox>
                                                                                        <asp:Label ID="Labeltown" runat="server" Text="" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButtontown" runat="server" OnClick="LinkButtontown_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>

                                                                            </div>



                                                                            <!--district -->

                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-1">
                                                                                Contact Person
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:TextBox ID="TextBox23" Style="text-transform: uppercase" runat="server" AutoPostBack="true" CssClass="TextBox23 form-control  locCodeReq" OnTextChanged="txtID_TextChanged"></asp:TextBox>

                                                                            </div>

                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="LinkButton12" runat="server" OnClick="ImgbtnUID_Click">
                                                                          <span class="glyphicon glyphicon-search"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            
                                                                            <!--Town -->

                                                                            <!--Town -->
                                                                        </div>
                                                                        <div class="row">
                                                                              <div class="col-sm-1 labelText1">Name</div>
                                                                            <div class="col-sm-7">
                                                                                <asp:TextBox ID="TextBox24" Width="400px" runat="server" CssClass="TextBox24 form-control  locCodeReq" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 labelText1">Mobile</div>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox ID="TextBox13" Width="100px" runat="server" CssClass="TextBox13 form-control  locCodeReq" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <!-- party type -->

                                                                        <!--Email -->


                                                                        <!-- -->




                                                                    </div>





                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 1px; padding-right: 1px;">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading paddingtopbottom0">
                                                                Others
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Commenced On
                                                                            </div>
                                                                            <div class="col-sm-2"></div>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox ID="TextBox26" runat="server" CssClass="form-control  locCodeReq" Width="100px"></asp:TextBox>

                                                                            </div>
                                                                            <div class="col-sm-1"></div>
                                                                            <div class="col-sm-1">

                                                                                <asp:LinkButton ID="dochandoverbtn" runat="server" CausesValidation="false">
                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtenderhandover" runat="server" TargetControlID="TextBox26" Animated="true"
                                                                                    PopupButtonID="dochandoverbtn" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>

                                                                            </div>

                                                                            <div class="col-sm-1">
                                                                                <asp:Label ID="commDtSet" runat="server" ForeColor="Red" Text="Not Setup" Visible="false" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-5 labelText1">
                                                                                Fixed Asset Location Code
                                                                            </div>
                                                                            <div class="col-sm-1"></div>
                                                                            <div class="col-sm-4">

                                                                                <asp:TextBox ID="TextBox25" Width="100px" Style="text-transform: uppercase" runat="server" CssClass="form-control  locCodeReq"></asp:TextBox>

                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 "></div>

                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1 paddingRight0">Warehouse Company</div>
                                                                            <div class="col-sm-2"></div>
                                                                            <div class="col-sm-4">
                                                                                <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control" Width="100px" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-12 "></div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">Warehouse Code</div>
                                                                            <div class="col-sm-2"></div>
                                                                            <div class="col-sm-6">
                                                                                <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control" Width="100px" AppendDataBoundItems="true">
                                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>


                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                                <div class="col-sm-6 padding0">
                                                                                    <div class="col-sm-1 padding0">
                                                                                        <asp:CheckBox ID="CheckBox3" Checked="true" Enabled="true" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-8 padding0">
                                                                                        <asp:Label ID="LabelType" runat="server" Text="Is Serial Maintain"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6 ">
                                                                                    <div class="col-sm-1 padding0">
                                                                                        <asp:CheckBox ID="CheckBoxpda" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-4" style="margin-left: -12px;">
                                                                                        <asp:Label ID="Labelpda" runat="server" Text="PDA"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-6 padding0">
                                                                                    <div class="col-sm-1 padding0">
                                                                                        <asp:CheckBox ID="chkMaintaionBin" runat="server" Checked="false" />
                                                                                    </div>
                                                                                    <div class="col-sm-8 padding0">
                                                                                        <asp:Label ID="lblBinLoc" runat="server" Text="Is maintain Bin Location"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6 ">
                                                                                    <div class="col-sm-1 padding0">
                                                                                        <asp:CheckBox ID="chkAutoIn" runat="server" Checked="false" />
                                                                                    </div>
                                                                                    <div class="col-sm-8 padding0">
                                                                                        <asp:Label ID="Label1" runat="server" Text="Is Auto In"></asp:Label>
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
                                    <%-- </div>
                            </div>--%>
                                </div>
                            </div>
                            <div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div>
        <div class="">
            <div class="">

                <div class="row">
                    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                    <div class="col-sm-12 padding0">
                        <div class="col-sm-7">
                        </div>
                        <div class="col-sm-5 buttonRow">
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-3">
                                </div>

                                <div class="col-sm-1"></div>
                                <div class="col-sm-3">
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <div class="row">
                    <div class="col-sm-12 ">
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12 padding0">


                        <!--end div 03 -->

                        <div class="col-sm-6 padding0">
                            <div class="">
                                <div class="">
                                </div>
                            </div>
                        </div>
                        <!-- end row 09 -->



                        <!--start row 03 -->





                    </div>


                    <div class="row">
                        <div class="col-sm-12">
                        </div>




                        <!-- -->



                        <!-- -->

                    </div>





                </div>


                <!--create panel -->
                <!--      <div class="panel panel-default">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-12 padding0">
                        <div class="col-sm-2">
                            Party Details
                        </div>
                    </div>
                </div>

            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="col-sm-12">



                            <asp:GridView ID="gvSubSerial" runat="server" GridLines="None" Style="border-collapse: collapse" CssClass="table table-hover table-striped" AutoGenerateColumns="False">

                                <Columns>
                                    <asp:BoundField HeaderText="Item" DataField="irsms_itm_cd" />
                                    <asp:BoundField HeaderText="Description" DataField="irsms_warr_no" />
                                    <asp:BoundField HeaderText="Model" DataField="irsms_mfc" />
                                    
                                    <asp:BoundField HeaderText="Status" DataField="irsms_itm_stus" />
                                    <asp:BoundField HeaderText="Qty" DataField="irsms_qty" />
                                    <asp:BoundField HeaderText="Serial" DataField="irsms_sub_ser" />
                                    <asp:BoundField HeaderText="Waranty" DataField="irsms_warr_rem" />

                                </Columns>
                                <EmptyDataTemplate>

                                    <table border="0" style="border-collapse: collapse" class="table table-hover table-striped" rules="all>
                                        <tr style="background-color: lightgrey;">
                                            <th scope="col">Item
                                            </th>
                                            <th scope="col">Description
                                            </th>
                                            <th scope="col">Model
                                            </th>
                                            
                                            <th scope="col">Status
                                            </th>
                                            <th scope="col">Qty
                                            </th>
                                            <th scope="col">Serial
                                            </th>
                                            <th scope="col">Waranty
                                            </th>

                                        </tr>
                                        <tr>
                                            <td colspan="7">No Results found.
                                            </td>
                                        </tr>

                                    </table>
                                </EmptyDataTemplate>


                            </asp:GridView>







                        </div>
                    </div>
                </div>

            </div>

        </div> -->


                <!-- -->





            </div>

            <!-- extra-->
        </div>

    </div>



    <!-- user popups-->
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




    <!-- -->

    <!-- userlocation -->

    <div runat="server" style="width: 427px">
        <asp:Button ID="Button4" runat="server" Text="" Style="display: none;" />
    </div>
    <asp:ModalPopupExtender ID="UserPopoupLocation" runat="server" Enabled="True" TargetControlID="Button4"
        PopupControlID="testPanelLoc" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="testPanelLoc" DefaultButton="ImgSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="LabelLoc" runat="server" Text="Label" Visible="false"></asp:Label>


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
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
                        <asp:Panel runat="server" DefaultButton="LinkButton3">
                            <div class="col-sm-12">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>

                                <div class="col-sm-3 paddingRight5">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList9" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <%--onkeydown="return (event.keyCode!=13);"--%>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="TextBox15" placeholder="Search by word" CausesValidation="false" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="ImgSearchLoc_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </asp:Panel>
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
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dvResult_Loc" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                        EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                        CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvResult_Loc_PageIndexChanging" OnSelectedIndexChanged="dvResult_Loc_SelectedIndexChanged">
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



            </div>
        </div>
    </asp:Panel>


    <!-- -->
    <div style="display: none">
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>

    </div>

    <div style="display: none">
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </div>
    <asp:CheckBox ID="CheckBox_checking" Checked="true" Visible="false" runat="server" />

    <div style="display: none">
        <asp:GridView ID="GridView3" runat="server"></asp:GridView>
    </div>


    <%-- Fazan  --%>
    <!-- -->
    <div class="row">
        <div class="col-sm-12  buttonrow">
            <div id="errorDiv" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                <strong>Warning!</strong>
                <asp:Label ID="lblWarn" runat="server"></asp:Label>
            </div>
            <div id="successDiv" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                <div class="alert alert-success">
                    <strong>Success!</strong>
                    <asp:Label ID="Label9" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <!-- -->

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
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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

                                                            <asp:Label ID="lblerror" runat="server" Text="Please select the correct upload file path !" ForeColor="Red" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblProcess" Text="Excel file upload completed. Do you want to process ?" runat="server" Visible="false"></asp:Label>
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
    <%-- Dulaj 2018-Oct-11 --%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button13" runat="server" Text="btnConfex" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtenderOutstanding" runat="server" Enabled="True" TargetControlID="Button13"
                PopupControlID="pnlConfirmationoutstanding" PopupDragHandleControlID="divCOnfOutstading" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlConfirmationoutstanding" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div14" class="panel panel-primary">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfOutstading">
                            Confirmation
                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="btnConfClose_ClickExcel">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="Label11" Text="Contact person is not available. Do you want to continue." runat="server" />
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:HiddenField ID="HiddenField6" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button14" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConOutfYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button15" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNoOut_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>



    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script>
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        function DateValid(sender, args) {

            var fromDate = Date.parse(document.getElementById('<%=TextBox26.ClientID%>').value);
            var sysDate = Date.parse(document.getElementById('<%=hdfCurrDate.ClientID%>').value);
            // alert(sysDate);
            // alert(sysDate);
            if (sysDate > fromDate) {
                document.getElementById('<%=TextBox26.ClientID%>').value = document.getElementById('<%=hdfCurrDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date !');
            }
        }
        Sys.Application.add_load(func);
        function func() {

            $('.TextBox18').focusout(function () {
                var str = $(this).val();
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                if (!emailReg.test(str)) {
                    showStickyWarningToast('Please enter a valid email address !!!');
                    $(this).val('');
                }
            });
            $('.TextBox20 , .TextBox21 ,.TextBox13').keypress(function (evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var str = jQuery(this).val();
                //alert(charCode);
                if ((charCode == 8)) {
                    return true;
                }
                if ((charCode == 9)) {
                    return true;
                }
                if (str.length < 15) {
                    if ((charCode < 58 && charCode > 47)) {
                        return true;
                    }
                    if ((charCode == 43)) {
                        // var no = str.value;
                        var result = "+" + str;
                        console.log(result);
                        //  alert(result);
                        if (str.charAt(0) != "+") {
                            $(this).val(result)
                            $(this).value = result;
                            return false;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 15);
                    alert('Maximum 15 characters are allowed ');
                    return false;
                }
            });
            $('#BodyContent_TextBox1,#BodyContent_TextBox2,#BodyContent_TextBox3,#BodyContent_TextBox4,#BodyContent_TextBox5,#BodyContent_TextBox6,#BodyContent_TextBox16,#BodyContent_TextBox19,#BodyContent_TextBox22,#BodyContent_TextBoxtown,#BodyContent_TextBox13,#BodyContent_TextBox20,#BodyContent_TextBox23,#BodyContent_TextBox10').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*()=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_TextBox7,#BodyContent_TextBox14,#BodyContent_TextBox17').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
        $('.TextBox20 , .TextBox21 ,.TextBox13, .TextBox12 , .InsValue, .TextBox23 ,.TextBox10').mousedown(function (e) {
            if (e.button == 2) {
                alert('This functionality is disabled !');
                return false;
            } else {
                return true;
            }
        });

        $('.TextBox23').keypress(function (evt) {
            evt = (evt) ? evt : window.event;
            var ch = (evt.which) ? evt.which : evt.keyCode;
            // console.log(ch); 
            if ((ch == 8) || (ch == 9) || (ch == 32) || (ch == 37) || (ch == 39) || (ch == 46) || (ch < 91 && ch > 64) || (ch < 122 && ch > 96)) {
                return true;
            }
            else {
                return false;
            }
        });


        $('.TextBox10 ,.InsValue, .TextBox12').keypress(function (evt) {
            // var ch = (evt.which) ? evt.which : evt.keyCode;
            var ch = evt.which;
            var str = $(this).val();
            console.log(ch);
            if (ch == 46) {
                if (str.indexOf(".") == -1) {
                    return true;
                } else {
                    return false;
                }
            }
            else if ((ch == 8) || (ch == 9) || (ch == 37) || (ch == 39) || (ch == 46) || (ch == 0)) {
                return true;
            }
            else if (ch > 47 && ch < 58) {
                return true;
            }
            else {
                return false;
            }
        })

        $('.locCodeReq').keypress(function (evt) {
            var str = $('.TextBox5').val();
            if (str == "") {
                showStickyWarningToast('Please enter a location code frist !');
                return false;
            } else {
                console.log('Test');
            }
        });
    </script>

</asp:Content>
