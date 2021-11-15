<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="AgentDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.AgentDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

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

        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmPlaceOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "No";
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

        function Enable() {
            return;
        }

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
            padding-top: 4%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server" DisplayAfter="10">
        <ProgressTemplate>

            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-8  buttonrow">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-11  buttonrow ">
                                <strong>Well done!</strong>
                                <asp:Label ID="lblok" runat="server"></asp:Label>
                            </div>

                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">
                            <div class="col-sm-11  buttonrow ">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblalert" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                            <div class="col-sm-11  buttonrow ">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblinfo" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-sm-4  buttonRow crnbuttonrowmargin">

                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                            <div class="col-sm-11">
                                <strong>Info!</strong>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-4 paddingRight15">
                            <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save/Process
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingLeft15" style="margin-left: -20px">
                            <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" CssClass="floatleft" OnClientClick="ConfirmPlaceOrder();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="LinkButton1_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    <strong>Agent Definition</strong>
                                </div>

                                <div class="panel-body">


                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Code
                                            </div>

                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtagcode" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnTextChanged="txtagcode_TextChanged" MaxLength="20" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>


                                            <div class="col-sm-1 labelText1">
                                                Active
                                            </div>

                                            <div class="col-sm-1 paddingRight5 labelText1">
                                                <asp:CheckBox ID="chkactive" runat="server" TabIndex="3" />
                                            </div>


                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>


                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Name
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" TabIndex="8" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>


                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Address
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtadd1" runat="server" CssClass="form-control" TabIndex="10" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtadd2" runat="server" CssClass="form-control" TabIndex="12" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                BR #
                                            </div>

                                            <div class="col-sm-2 paddingRight5">
                                                <asp:TextBox ID="txtbeno" runat="server" CssClass="form-control" TabIndex="6" MaxLength="14" AutoPostBack="true" OnTextChanged="txtbeno_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 labelText1">
                                                Country
                                            </div>

                                            <div class="col-sm-2 paddingRight5">
                                                <asp:TextBox ID="txtcountry" runat="server" CssClass="form-control" TabIndex="9" AutoPostBack="true" OnTextChanged="txtcountry_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                                <asp:LinkButton ID="lbtncountry" runat="server" OnClick="lbtncountry_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-3 labelText1 padding0">
                                                <asp:Label ID="lblbountry" runat="server" ForeColor="Black"></asp:Label>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                                Mobile
                                            </div>

                                            <div class="col-sm-2 paddingRight5">
                                                <asp:TextBox ID="txtmob" MaxLength="10" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true" OnTextChanged="txtmob_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 labelText1">
                                                Fax
                                            </div>

                                            <div class="col-sm-2 paddingRight5">
                                                <asp:TextBox ID="txtfax" MaxLength="10" runat="server" CssClass="form-control" TabIndex="5" AutoPostBack="true" OnTextChanged="txtfax_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 labelText1">
                                                Telephone
                                            </div>

                                            <div class="col-sm-2 paddingRight5">
                                                <asp:TextBox ID="txttel" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2" AutoPostBack="true" OnTextChanged="txttel_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Email
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" TabIndex="7" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Ref #
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control" TabIndex="11" MaxLength="15" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
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
        <div runat="server" id="test" class="panel panel-default width700">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default ">
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
