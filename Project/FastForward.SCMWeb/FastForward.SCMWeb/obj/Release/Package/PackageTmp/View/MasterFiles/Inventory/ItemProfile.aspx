<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ItemProfile.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.ItemProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ConfDel() {
            var selectedvalueOrd = confirm("Are you sure do you want to remove ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
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

        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdKit.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        };

        function SellectTab() {
            // $("#TabCompanyRool option:selected").val();
            //  $("#tab").tabs("select", "#AssignItem");
            jQuery(".tab-content div").removeClass("active");
            jQuery("#AssignItem").addClass("active");


        }
        function PressAssighteam() {

            //  $("#tab").tabs("select", "#AssignItem");
            jQuery(".tab-content div").removeClass("active");
            jQuery("#Logistics").addClass("active");
        }
        function Presslogistics() {
            jQuery(".tab-content div").removeClass("active");
            jQuery("#WarrantyPeriod").addClass("active");
        }

        //$('#BodyContent_txttrimRight').live('keydown', function (e) {
        //    var keyCode = e.keyCode || e.which;

        //    if (keyCode == 9) {
        //        e.preventDefault();
        //        // call custom function here
        //        //alert("click 0000");
        //        console.log("here");
        //    }
        //});

        <%--        $(document).ready(function () {
            $('#<%= txttrimRight.ClientID%>').keydown(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 9) {
                    alert("click 0000");
                    return false;
                }
            });
        });--%>
        //function ValidateInput(input) {
        //    var string = input;
        //    var re = new RegExp("^[a-zA-Z0-9 -]*$");
        //    console.log(re.test(string));
        //    if (re.test(string)) {
        //        return false;
        //    } 
        //}
       

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



        function checkAll() {

            var checkBoxSelector = '#<%=grdMultiplestatus.ClientID%> input[id*="chkMultiplestatus"]:checkbox';

            if ($('#BodyContent_ChkStatusall').is(':checked')) {
                $(checkBoxSelector).attr('checked', true);
                console.log($(checkBoxSelector).attr('checked', true));
            }
            else {
                $(checkBoxSelector).attr('checked', false);
            }
        }

        function DateValidFrom(sender, args) {

            var fromDate = Date.parse(document.getElementById('<%=txtpcfrom.ClientID%>').value);
            var sysDate = Date.parse(document.getElementById('<%=hdfCurrDate.ClientID%>').value);
            // alert(sysDate);
            // alert(sysDate);
            if (sysDate > fromDate) {
                document.getElementById('<%=txtpcfrom.ClientID%>').value = document.getElementById('<%=hdfCurrDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date !');
            }
        }
        function DateValidTo(sender, args) {

            var fromDate = Date.parse(document.getElementById('<%=txtpcto.ClientID%>').value);
            var sysDate = Date.parse(document.getElementById('<%=hdfCurrDate.ClientID%>').value);
            // alert(sysDate);
            // alert(sysDate);
            if (sysDate > fromDate) {
                document.getElementById('<%=txtpcto.ClientID%>').value = document.getElementById('<%=hdfCurrDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date !');
            }
        }
        function DateValidEffect(sender, args) {

            var fromDate = Date.parse(document.getElementById('<%=txtEffectiveDate.ClientID%>').value);
            var sysDate = Date.parse(document.getElementById('<%=hdfCurrDate.ClientID%>').value);
            // alert(sysDate);
            // alert(sysDate);
            if (sysDate > fromDate) {
                document.getElementById('<%=txtEffectiveDate.ClientID%>').value = document.getElementById('<%=hdfCurrDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date !');
            }
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
            padding-top: 20%;
        }
    </style>
    <style>
        .margin_set {
            padding-top: 1px;
            padding-bottom: 1px;
            margin-top: 0px;
            margin-bottom: 1px;
        }

        .grdKitStyle th {
            border-left: 2px solid;
            border-left-color: darkgray;
        }

            .grdKitStyle th:nth-child(1) {
                border-left: none;
            }

            .grdKitStyle th:nth-child(2) {
                border-left: none;
            }
    </style>
    <script>
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="costBtn">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait501" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait501" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel16">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait133" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait133" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait12" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait12" runat="server"
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
    <asp:HiddenField ID="hdfCurrDate" Value="" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-8  buttonrow">
                        </div>
                        <div class="col-sm-4  buttonRow">

                            <div class="col-sm-6">
                                <asp:Panel runat="server" ID="pnlsavechk">
                                    <div class="col-sm-2">
                                        <asp:CheckBox ID="chkSave" AutoPostBack="true" runat="server" OnCheckedChanged="chkSave_CheckedChanged" />
                                    </div>
                                    <div class="col-sm-10 labelText1">
                                        Save as new
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="btnAddNew" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="btnAddNew_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="col-sm-2 labelText1">
                                                Code 
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control itemcode" OnTextChanged="txtItem_TextChanged" TabIndex="100"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnSrchCode" runat="server" CausesValidation="false" OnClick="lbtnSrchCode_Click" TabIndex="101">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <div class="col-sm-2 labelText1">
                                                Brand 
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtBrand" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBrand_TextChanged" TabIndex="102"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnSrchBrand" runat="server" CausesValidation="false" OnClick="lbtnSrchBrand_Click" TabIndex="103">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 paddingLeft0">
                                            <div class="col-sm-2 labelText1">
                                                Short Desc. 
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txtSdes" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSdes_TextChanged" TabIndex="104"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="row">
                                                <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                    Part # 
                                                </div>
                                                <div class="col-sm-5 paddingLeft0 ">
                                                    <asp:TextBox runat="server" ID="txtPartNo" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 labelText2 ">
                                                    Active
                                                </div>

                                                <div class="col-sm-3  ">
                                                    <asp:DropDownList ID="ddlActive" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="col-sm-2 labelText1">
                                                Model 
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtModel" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtModel_TextChanged" TabIndex="106"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnSrchModel" runat="server" CausesValidation="false" OnClick="lbtnSrchModel_Click" TabIndex="107">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-2  paddingLeft0 paddingRight0">
                                            <div class="col-sm-2 labelText1">
                                                UOM 
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtUOM" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUOM_TextChanged" TabIndex="108"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnsrhBaseUOM" runat="server" CausesValidation="false" OnClick="lbtnsrhBaseUOM_Click" TabIndex="109">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 paddingLeft0">
                                            <div class="col-sm-2 labelText1">
                                                Long Desc.
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txtLdes" TextMode="MultiLine" AutoPostBack="true" CausesValidation="false" CssClass="form-control" TabIndex="105" OnTextChanged="txtLdes_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1  paddingLeft0 paddingRight0">
                                                    Packing Material Code
                                                </div>
                                                <div class="col-sm-7 padding03">
                                                    <asp:TextBox runat="server" OnTextChanged="txtPackCode_TextChanged" ID="txtPackCode" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 padding03">
                                                <asp:LinkButton ID="lbtnSePackCd" runat="server" CausesValidation="false" OnClick="lbtnSePackCd_Click" TabIndex="109">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
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
            <div class="row">
                <div class="col-sm-12 ">
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active"><a href="#BasicItemDetails" data-toggle="tab">Main Details</a></li>
                            <li tabindex="130" onkeypress="PressAssighteam()"><a href="#AssignItem" data-toggle="tab">Company Rules</a></li>
                            <li tabindex="131" onkeypress="Presslogistics()"><a href="#Logistics" data-toggle="tab">Logistics</a></li>
                            <li tabindex="132"><a href="#WarrantyPeriod" data-toggle="tab">Warranty and after Sales</a></li>
                            <li><a href="#itemCost" data-toggle="tab">Item Cost </a></li>
                            <%--<li><a href="#SKU" data-toggle="tab">Image & specification</a></li>--%>
                        </ul>

                    </div>
                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>
                    <div class="col-sm-12 ">
                        <div class="tab-content">
                            <div class="tab-pane active" id="BasicItemDetails">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Item Catogories
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1 paddingLeft5">
                                                                    Main Category
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtMainCat" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtMainCat_TextChanged" TabIndex="110"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_mainCat" runat="server" OnClick="lbtnSrch_mainCat_Click" TabIndex="111">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Sub  
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Level 1 
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtCat1" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCat1_TextChanged" TabIndex="112"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat1" runat="server" OnClick="lbtnSrch_cat1_Click" TabIndex="113">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Level 2 
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtCat2" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCat2_TextChanged" TabIndex="114"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat2" runat="server" OnClick="lbtnSrch_cat2_Click" TabIndex="115">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Level 3 
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtCat3" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCat3_TextChanged" TabIndex="116"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat3" runat="server" OnClick="lbtnSrch_cat3_Click" TabIndex="117">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Level 4 
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtCat4" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCat4_TextChanged" TabIndex="118"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat4" runat="server" OnClick="lbtnSrch_cat4_Click" TabIndex="119">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 labelText1 paddingLeft5">
                                                                    Size
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtSize" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                    <asp:DropDownList ID="ddluomwidth" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-3 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">Default</div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5 labelText1 paddingLeft5">
                                                                    Item Type
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlItemType" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" TabIndex="120">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5 labelText1 paddingLeft5">
                                                                    Maintain Inventory 
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <%--<asp:CheckBox ID="chkStcokMain" Checked="true" runat="server" />--%>
                                                                    <asp:DropDownList ID="ddlStcokMain" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" TabIndex="121">
                                                                        <%--<asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5 labelText1 paddingLeft5">
                                                                    Country of Original
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtCountry" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtCountry_TextChanged" TabIndex="122"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrchCounty" runat="server" OnClick="lbtnSrchCounty_Click" TabIndex="123">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel runat="server" ID="pnlcapacit" Visible="false">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5 labelText1 paddingLeft5">
                                                                        Capacity
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtCapacity" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Color -
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    External
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 height22">
                                                                    <asp:CheckBox ID="chkColorExt" Visible="false" runat="server" />
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtColorExt" AutoPostBack="false" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtColorExt_TextChanged" TabIndex="124"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnshcextcolor" runat="server" OnClick="lbtnshcextcolor_Click" TabIndex="125">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingLeft5">
                                                                    Internal
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 height22">
                                                                    <asp:CheckBox ID="chkColorInt" Visible="false" runat="server" />
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtColorInt" Text="N/A" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtColorInt_TextChanged" TabIndex="126"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnsrhintcolor" runat="server" OnClick="lbtnsrhintcolor_Click" TabIndex="127">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5 labelText1 paddingLeft5">
                                                                    Chargerble
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlPayType" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                        <%--  <asp:ListItem Text="CHA"></asp:ListItem>
                                                                        <asp:ListItem Text="FOC"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="col-sm-5 labelText1 paddingLeft5">
                                                                    Track Expired Date
                                                                </div>
                                                                <div class="col-sm-2 paddingRight5 paddingLeft0">
                                                                    <asp:CheckBox ID="chkIsExpired" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel runat="server" ID="pnlStatus" Visible="false">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5 labelText1 paddingLeft5">
                                                                        Item Status
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlStatus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">Parameters</div>
                                                    <div class="panel-body">
                                                        <div class="row">

                                                            <div class="col-sm-12">
                                                                <div class="col-sm-6 labelText1 paddingLeft5">
                                                                    Serialized
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlSerilize" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                        <%--<asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <asp:Panel runat="server" ID="pnltrim" Visible="false">
                                                            <div class="row">
                                                                <div class="col-sm-1 labelText1">
                                                                    Trim
                                                                </div>
                                                                <div class="col-sm-2 paddingRight0 height22">
                                                                    <asp:CheckBox ID="chkTrim" runat="server" />
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                                    Serial 
                                                                </div>
                                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                                    Left Trim
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txttrimLeft" CausesValidation="false" runat="server" class="form-control" TabIndex="128"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                                </div>
                                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                                    Right Trim
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txttrimRight" CausesValidation="false" runat="server" class="form-control" TabIndex="129" Onkeypress="SellectTab()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                                </div>
                                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                                    Prefix
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtPrefix" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <asp:Panel runat="server" ID="pnlwarranty" Visible="false">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 labelText1 paddingLeft5">
                                                                        Warranty
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlWarranty" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                            <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-6 labelText1 paddingLeft5">
                                                                    Second Serial ID
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlChassis" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlChassis_SelectedIndexChanged">
                                                                        <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnbooktype" Visible="false" runat="server" OnClick="lbtnbooktype_Click">
                                                           add prefix
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-6 labelText1 paddingLeft5">
                                                                    Accessory Scan
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlScanSub" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-6 labelText1 paddingLeft5">
                                                                    Sub Item 
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlIsSubItem" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-6 labelText1 paddingLeft5">
                                                                    Purchase by
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtPurComp" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtPurComp_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrhPurCom" runat="server" OnClick="lbtnSrhPurCom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel runat="server" ID="pnlHs" Visible="false">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1 paddingLeft5">
                                                                        HS Code
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="ttxHsCode" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1 paddingLeft5">
                                                                        Book Type 
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlbooktype" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlbooktype_SelectedIndexChanged">
                                                                            <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </asp:Panel>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">Other</div>
                                                    <div class="panel-body panelscollbar height170">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAlowHP" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Is allow HP
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAlowInsu" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Is allow Insurance
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAlowVehReg" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Is allow Vehicle Reg
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAlowVehInsu" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Is allow Vehicle Insurance
                                                                    </div>
                                                                </div>
                                                            </div>
                                                           
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAdditem" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Amend Item Description when billing
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Panel runat="server" ID="pnlSditable" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2 paddingRight0 height22">
                                                                            <asp:CheckBox ID="chkAlterSer" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-10 labelText1 paddingLeft5">
                                                                            Editable Alternative Serial
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <%--<div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkProRegis" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Product Registration Required
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <asp:Panel runat="server" ID="pnlsr" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2 paddingRight0 height22">
                                                                            <asp:CheckBox ID="chkSerReq" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-10 labelText1 paddingLeft5">
                                                                            Serials Required by Customs
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2 paddingRight0 height22">
                                                                            <asp:CheckBox ID="chkDiscont" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-10 labelText1 paddingLeft5">
                                                                            Discontinue Item
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAppCond" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Apply Item Conditions
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkcust" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Validate Customer in Redeem
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<div class="row">
                                                                <div class="col-sm-12">

                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkIsRegister" Visible="true" runat="server" />
                                                                        <asp:DropDownList ID="ddlIsRegister" Visible="false" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                            
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Comapny Insurance Protection
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <%--<div class="row">
                                                                <div class="col-sm-12">


                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkProInsurance" Visible="true" runat="server" />
                                                                        <asp:DropDownList ID="ddlProInsurance" CausesValidation="false" Visible="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                       
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Customer Insurance Mandatory
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div class="row">
                                                                <div class="col-sm-12">

                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkCounterfoil" Visible="true" runat="server" />

                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Counterfoil
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                                     <div class="row">
                                                                <div class="col-sm-12">

                                                                    <div class="col-sm-2 paddingRight0 height22">
                                                                        <asp:CheckBox ID="chkAllowMinus" Visible="true" runat="server" />

                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft5">
                                                                        Allow minus figures
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Item Reorder
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Company
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:TextBox runat="server" ID="txtCompany" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCompany_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnSearchreCom" runat="server" CausesValidation="false" OnClick="lbtnSearchreCom_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Safety  stock
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:TextBox runat="server" ID="txtsaftystock" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Re-Order point
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:TextBox runat="server" ID="txtRoLevel" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Order quntity
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:TextBox runat="server" ID="txtRoQty" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            ABC Classification
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:DropDownList ID="ddlClassification" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Text="A"></asp:ListItem>
                                                                                <asp:ListItem Text="B"></asp:ListItem>
                                                                                <asp:ListItem Text="C"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnAddReorder" runat="server" CausesValidation="false" OnClick="lbtnAddReorder_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="panel-body panelscollbar height120">
                                                                    <asp:GridView ID="grdReorder" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Icr_com_code" runat="server" Text='<%# Bind("Icr_com_code") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Re-order point">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Icr_re_order_lvl" runat="server" Text='<%# Bind("Icr_re_order_lvl") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="order quntity">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Icr_re_order_qty" runat="server" Text='<%# Bind("Icr_re_order_qty") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="safety  stock">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Icr_safety_qty" runat="server" Text='<%# Bind("Icr_safety_qty") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Classification">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="colClassi" runat="server" Text='<%# Bind("Icr_class") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtndelete" runat="server" OnClick="lbtndelete_Click" Width="50px"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="AssignItem">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Company parameter

                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-5 paddingLeft0">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-6 labelText1 ">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtItemCom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtItemCom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchitemCom" runat="server" OnClick="lbtnSearchitemCom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnMultipleCom" runat="server" OnClick="lbtnMultipleCom_Click">
                                                         Multiple
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-6 labelText1 ">
                                                                        Description
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtitemDes" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-6 labelText1 ">
                                                                        FOC Allow
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlFoc" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Allow"></asp:ListItem>
                                                                            <asp:ListItem Text="Not Allow"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <asp:Panel runat="server" ID="pnpagency" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Agency Type
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlAgecType" runat="server" class="form-control">
                                                                                <asp:ListItem Text="N/A"></asp:ListItem>
                                                                                <asp:ListItem Text="Sole"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                            </asp:Panel>

                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-6 labelText1 ">
                                                                        prohibited Sales Type
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlhpSalesAccept" AppendDataBoundItems="true" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                           <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-6 labelText1 ">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlitemStatus" runat="server" class="form-control">
                                                                            <asp:ListItem Text="YES"></asp:ListItem>
                                                                            <asp:ListItem Text="NO"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnsearchComItemAdd" runat="server" OnClick="lbtnsearchComItemAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel-body panelscollbar height100">
                                                                <asp:GridView ID="grdComItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ci_com" runat="server" Text='<%# Bind("MCI_COM") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ci_des" runat="server" Text='<%# Bind("Mci_comDes") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FOC" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ci_foc" runat="server" Text='<%# Bind("MCI_ISFOC") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FOC">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Mci_isfoc_status" runat="server" Text='<%# Bind("Mci_isfoc_status") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="P.S.Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ci_Msi_restric_inv_tp" runat="server" Text='<%# Bind("Msi_restric_inv_tp") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ci_status" runat="server" Text='<%# Bind("MCI_ACT") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Mci_act_status" runat="server" Text='<%# Bind("Mci_act_status") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Redeem at
                                                    </div>
                                                    <div class="panel-body">

                                                        <div class="col-sm-5">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtReCom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtReCom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchRedeem" runat="server" OnClick="lbtnSearchRedeem_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbtnMultipleCom_Click">
                                                         Multiple
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Description
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtReDes" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlReStatus" runat="server" class="form-control">
                                                                            <asp:ListItem Text="YES"></asp:ListItem>
                                                                            <asp:ListItem Text="NO"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnsearchRedeemCom" runat="server" OnClick="lbtnsearchRedeemCom_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel-body panelscollbar height100">
                                                                <asp:GridView ID="grdRedeemCom" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="red_Com" runat="server" Text='<%# Bind("Red_com_code") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="red_des" runat="server" Text='<%# Bind("Red_com_des") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="red_status" runat="server" Text='<%# Bind("Red_active") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Red_active_status" runat="server" Text='<%# Bind("Red_active_status") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height10">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlCustomeritem" Visible="false">
                                            <div class="row">
                                                <div class="col-sm-6 paddingLeft0">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading paddingtopbottom0">
                                                            Customer  Items

                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-sm-5 paddingLeft0">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Company
                                                                        </div>
                                                                        <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtCuscom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtCuscom_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnSearchCusitem" runat="server" OnClick="lbtnSearchCusitem_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Customer
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtCust" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtCust_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnCusSearch" runat="server" OnClick="lbtnCusSearch_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Name
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtCustName" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Active
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlCustStatus" runat="server" class="form-control">
                                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnsearchCustomer" runat="server" OnClick="lbtnsearchCustomer_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="panel-body panelscollbar height100">
                                                                    <asp:GridView ID="grdCustomer" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="cui_com" runat="server" Text='<%# Bind("mbii_com") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Customer">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="cui_cust" runat="server" Text='<%# Bind("mbii_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="cui_Name" runat="server" Text='<%# Bind("mbii_CustName") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="cui_status" runat="server" Text='<%# Bind("mbii_act") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MBII_ACT_status" runat="server" Text='<%# Bind("MBII_ACT_status") %>' Width="100px"></asp:Label>
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
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-sm-12 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Tax Claim
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Company
                                                                        </div>
                                                                        <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtclaimcom" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtclaimcom_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnsrhTaxCom" runat="server" OnClick="lbtnsrhTaxCom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lbtnMultipleCom_Click">
                                                         Multiple
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Tax Category
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlclaimcate" runat="server" class="form-control">
                                                                                <asp:ListItem Text="Profit"></asp:ListItem>
                                                                                <asp:ListItem Text="Cost"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Tax Rate
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txttaxRate" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Claimable %
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtclaim" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Active
                                                                        </div>
                                                                        <div class="col-sm-2 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlTaxClAc" runat="server" class="form-control">
                                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnAddTaxClaim" runat="server" OnClick="lbtnAddTaxClaim_Click">
                                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="panel-body panelscollbar height100">
                                                                    <asp:GridView ID="grdTaxClaim" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="tc_com" runat="server" Text='<%# Bind("Mic_com") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Tax Category">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="tc_cate" runat="server" Text='<%# Bind("Mic_tax_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Tax Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="tc_rate" runat="server" Text='<%# Bind("Mic_tax_rt") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Claimable">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_claim" runat="server" Text='<%# Bind("Mic_claim") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ACT_status" runat="server" Text='<%# Bind("mic_stus") %>' Width="100px"></asp:Label>
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
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                    Tax Structure
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox ID="txtTaxStucture" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtTaxStucture_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnsrcTax" runat="server" OnClick="lbtnsrcTax_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtntxD" runat="server" Text="View Details" OnClick="lbtntxD_Click">
                                                           
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%--<div class="tab-pane" id="SKU">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5 paddingLeft0">
                                                <div class="row">
                                                    <div class="col-sm-12 paddingLeft0">
                                                        <div class="col-sm-4 labelText1 ">
                                                            Image Path
                                                        </div>
                                                        <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtImagePath" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>--%>
                            <div class="tab-pane" id="Logistics">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxyLogistics" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Item Logistics
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4">

                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Weight
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtwuom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtwuom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSrhWeightUOM" runat="server" OnClick="lbtnSrhWeightUOM_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Gross
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtgross" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Net
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtnet" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>



                                                        </div>
                                                        <div class="col-sm-5">

                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Diamentions
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtduom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtduom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnsrhdimenuom" runat="server" OnClick="lbtnsrhdimenuom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Height
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txthight" AutoPostBack="true" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txthight_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Width
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtwidth" AutoPostBack="true" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtwidth_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Depth
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtbreath" AutoPostBack="true" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtbreath_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    Warehouse Cost base by
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Volume
                                                                        </div>
                                                                        <div class="col-sm-2 labelText1 ">
                                                                            <asp:RadioButton ID="rdvolume" GroupName="ware" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Weight
                                                                        </div>
                                                                        <div class="col-sm-1 labelText1 ">
                                                                            <asp:RadioButton ID="rdweight" GroupName="ware" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12 ">
                                                                            <div class="col-sm-5 labelText1 ">
                                                                                Capacity
                                                                            </div>
                                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                                <asp:TextBox ID="txtwarehousvolum" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 labelText1 ">
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Container Utilization
                                                    </div>
                                                    <div class="panel-body margin_set">

                                                        <div class="col-sm-5">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Cont  Type
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlContType" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        No of Items
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtContUnits" CausesValidation="false" runat="server" class="diWMClick valIntValue form-control"></asp:TextBox>
                                                                    </div>
                                                                    
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlContainerAct" runat="server" class="form-control">
                                                                       <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddCont" runat="server" OnClick="lbtnAddCont_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel-body margin_set" style="height: 94px; overflow: scroll;">
                                                                <asp:GridView ID="grdCont" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Cont Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="cont_type" runat="server" Text='<%# Bind("Ic_container_type_code") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No of Items">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="cont_des" runat="server" Text='<%# Bind("Ic_no_of_unit_per_one_item") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIc_act" Text='<%# Eval("Ic_act").ToString().Equals("1")?"YES":"NO" %>'  runat="server"></asp:Label>
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
                                        <div class="row">
                                            <div class="col-sm-5 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        MRN Company

                                                    </div>
                                                    <div class="panel-body margin_set">

                                                        <div class="col-sm-5 paddingLeft0">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtMrnCom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtMrnCom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchmrn" runat="server" OnClick="lbtnSearchmrn_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Description
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtMrndes" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlmrnStatus" runat="server" class="form-control">
                                                                            <asp:ListItem Text="YES"></asp:ListItem>
                                                                            <asp:ListItem Text="NO"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchgvMRN" runat="server" OnClick="lbtnSearchgvMRN_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>



                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel-body margin_set">
                                                                <asp:GridView ID="grdMRN" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="mrn_com" runat="server" Text='<%# Bind("Imc_com") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="mrn_des" runat="server" Text='<%# Bind("Imc_comdes") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="mrn_status" runat="server" Text='<%# Bind("Imc_active") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Imc_active_status" runat="server" Text='<%# Bind("Imc_active_status") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-7 ">

                                                <div class="row">
                                                    <div class="col-sm-12 paddingLeft0">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading paddingtopbottom0">
                                                                Assemble costing

                                                            </div>
                                                            <div class="panel-body margin_set">
                                                                <div class="row">
                                                                    <div class="col-sm-5">
                                                                        <div class="row">
                                                                            <div class="col-sm-12 paddingLeft5">
                                                                                <div class="col-sm-4 labelText1">
                                                                                    Finish Good
                                                                                </div>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox runat="server" ID="txtFgood" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtFgood_TextChanged"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                    <asp:LinkButton ID="lbtnSearchFG" runat="server" CausesValidation="false" OnClick="lbtnSearchFG_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 paddingLeft5">
                                                                                <div class="col-sm-4 labelText1">
                                                                                    Cost Element

                                                                                </div>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox runat="server" ID="txtCostElement" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 paddingLeft5">
                                                                                <div class="col-sm-4 labelText1">
                                                                                    Amount
                                                                                </div>
                                                                                <div class="col-sm-5">
                                                                                    <asp:TextBox runat="server" ID="txtAmount" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                    <asp:LinkButton ID="lbtnAddstatus" runat="server" CausesValidation="false" OnClick="lbtnAddstatus_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <div class="panel-body margin_set" style="height: 65px; overflow: scroll;">
                                                                            <asp:GridView ID="grdStatus" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Finish Good">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="col_Ifc_fg_item_code" runat="server" Text='<%# Bind("Ifc_fg_item_code") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Cost Element">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="col_Ifc_cost_type" runat="server" Text='<%# Bind("Ifc_cost_type") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="colfgAmount" runat="server" Text='<%# Bind("Ifc_cost_amount") %>' Width="100px"></asp:Label>
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
                                        <div class="row">
                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Supplier  Items

                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-5 paddingLeft0">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtSupCom" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtSupCom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnsrhSupCom" runat="server" OnClick="lbtnsrhSupCom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Supplier
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtSupp" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtSupp_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchSupp" runat="server" OnClick="lbtnSearchSupp_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Supplier Name
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtSupName" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlsupStatus" runat="server" class="form-control">
                                                                            <asp:ListItem Text="YES"></asp:ListItem>
                                                                            <asp:ListItem Text="NO"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    
                                                                </div>

                                                            </div>

                                                            <%--dilshan--%>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Sup. Period
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlSupPrd" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Sup. Remarks
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtSupRem" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchgvSupplier" runat="server" OnClick="lbtnSearchgvSupplier_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <%-- end --%>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel-body panelscollbar height150">
                                                                <asp:GridView ID="grdSupplier" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="supCom" runat="server" Text='<%# Bind("MBII_COM") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Supplier">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sup_Code" runat="server" Text='<%# Bind("MBII_CD") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sup_Name" runat="server" Text='<%# Bind("mbii_custname") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sup_status" runat="server" Text='<%# Bind("MBII_ACT") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="MBII_ACT_status" runat="server" Text='<%# Bind("MBII_ACT_status") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sup. Period">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="MBII_WARR_PERI" runat="server" Text='<%# Bind("MBII_WARR_PERI") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sup. Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="MBII_WARR_RMK" runat="server" Text='<%# Bind("MBII_WARR_RMK") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        KIT Setup
                                                    </div>
                                                    <div class="panel-body margin_set">
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            Item Code
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtkitItem" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtkitItem_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnSearchKit" runat="server" OnClick="lbtnSearchKit_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            Category
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlkitCate" runat="server" class="form-control">
                                                                                <asp:ListItem Text="SKU"></asp:ListItem>
                                                                                <asp:ListItem Text="KIT"></asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            Type
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlKitItemType" runat="server" class="form-control">
                                                                                <asp:ListItem Text="M"></asp:ListItem>
                                                                                <asp:ListItem Text="A"></asp:ListItem>
                                                                                <asp:ListItem Text="C"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            Cost Method
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList AutoPostBack="true" ID="ddlCostMeth" OnSelectedIndexChanged="ddlCostMeth_SelectedIndexChanged" runat="server" class="form-control">
                                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                                <asp:ListItem Text="Percentage" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Amount" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            <asp:Label Text="Cost" runat="server" ID="lblCost" />
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtkitcost" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            Scan
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlScan" runat="server" class="form-control">
                                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            No of Units
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtunits" OnTextChanged="txtunits_TextChanged" AutoPostBack="true"  CausesValidation="false" runat="server" class="diWMClick validateDecimal form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-5 labelText1 ">
                                                                            Active
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlKitActive" runat="server" class="form-control">
                                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnAddKit" runat="server" OnClick="lbtnAddKit_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-8 paddingLeft0">
                                                                <div class="panel-body margin_set">
                                                                    <asp:GridView ID="grdKit" CssClass="grdKitStyle table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Kitcheck" AutoPostBack="true" runat="server" onclick="CheckBoxCheck(this);" OnCheckedChanged="chk_Kitcheck_Click" Checked="false" Width="100%" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10px" />
                                                                                <HeaderStyle Width="10px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kit_item" runat="server" Text='<%# Bind("Micp_comp_itm_cd") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="100px" />
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Category">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kit_cat" runat="server" Text='<%# Bind("Micp_cate") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px" />
                                                                                <HeaderStyle Width="50px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kit_type" runat="server" Text='<%# Bind("Micp_itm_tp") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                                <HeaderStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CostMeth" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmicp_is_percentage" runat="server" Text='<%# Bind("micp_is_percentage") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                                <HeaderStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cost">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kit_cost" runat="server" Text='<%# Bind("Micp_cost_percentage","{0:N2}") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cost Met." Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCostDesc" runat="server" Text='<%# Eval("micp_is_percentage").ToString().Equals("1")?"PER":"AMT" %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="40px" />
                                                                                <HeaderStyle Width="40px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Scan">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kit_scan" runat="server" Text='<%# Bind("Micp_must_scan") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                                <HeaderStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kit_units" runat="server" Text='<%# Bind("Micp_qty") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                                <HeaderStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Micp_act_status" runat="server" Text='<%# Bind("Micp_act_status") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                                <HeaderStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="kitactive" runat="server" Text='<%# Bind("Micp_act") %>' Width="100%"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                                <HeaderStyle Width="30px" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="WarrantyPeriod">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxyWarrantyPeriod" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanelWarrantyPeriod" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-5 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Item Status Wise Warranty
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-9 paddingLeft0">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Item Status
                                                                    </div>
                                                                    <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlwStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlwStatus_SelectedIndexChanged" CausesValidation="false" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnMultiplestatus" runat="server" Text="Multiple Status" OnClick="lbtnMultiplestatus_Click">
                                                            
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        UOM
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtWaraUOM" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtWaraUOM_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnsrhwarauom" runat="server" OnClick="lbtnsrhwarauom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Period
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlwPeriod" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Remarks
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtWarRem" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Sup. Period
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlwarsPrd" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Sup. Remarks
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtwarsRem" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Effective Date
                                                                    </div>
                                                                    <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtEffectiveDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnEffectiveDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender  OnClientDateSelectionChanged="DateValidEffect" ID="CalendarExtender3" runat="server" TargetControlID="txtEffectiveDate"
                                                                            PopupButtonID="lbtnEffectiveDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnAddWarranty" runat="server" CausesValidation="false" OnClick="lbtnAddWarranty_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnviewwarranty" runat="server" CausesValidation="false" OnClick="lbtnviewwarranty_Click">
                                                           View warranty
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Main Warranty UOM
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtMainWaraUOM" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtMainWaraUOM_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbMainWaraUOM" runat="server" OnClick="lbtnsrhMainwarauom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height10">
                                                                </div>
                                                            </div>
                                                            <asp:DropDownList ID="ddlwdur" Visible="false" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlwarsdur" Visible="false" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 buttonRow">
                                                            <div class="col-sm-12  paddingRight0 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-sm-7 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Company/PC/Channel
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        Define by
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlPC_Ch_Com" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPC_Ch_Com_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Company" Value="COM"></asp:ListItem>
                                                                            <asp:ListItem Text="Channel" Value="CHNL"></asp:ListItem>
                                                                            <asp:ListItem Text="PC" Value="PC"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtwarrantycompany" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtwarrantycompany_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <asp:LinkButton ID="lbtnwarrantycompany" runat="server" OnClick="lbtnwarrantycompany_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">

                                                                        <asp:Label ID="lblPC_Ch_Com" runat="server" Text="Company"></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtpcCom" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtpcCom_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnSearchpcWara" Visible="false" runat="server" OnClick="lbtnSearchpcWara_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnsrhchannel" Visible="false" runat="server" OnClick="lbtnsrhchannel_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnSearchpc" Visible="false" runat="server" OnClick="lbtnSearchpc_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnChacom" runat="server" OnClick="lbtnChacom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <asp:Panel runat="server" ID="pnlPc" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-3 labelText1 ">
                                                                            PC
                                                                        </div>
                                                                        <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtpc" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtpc_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                            </asp:Panel>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        Item Status
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlPcstatus" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        Period
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlpcPrd" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        Remarks
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtpcRem" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        From
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtpcfrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnpcfrom" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtpcfrom"
                                                                            OnClientDateSelectionChanged="DateValidFrom"
                                                                            PopupButtonID="lbtnpcfrom" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtpcto" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnpcto" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtpcto"
                                                                            OnClientDateSelectionChanged="DateValidTo"
                                                                            PopupButtonID="lbtnpcto" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddPcwarranty" runat="server" OnClick="lbtnAddPcwarranty_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3  paddingRight0 paddingLeft0 buttonRow">
                                                                        <asp:LinkButton ID="lbtnExceluploadcompWar" runat="server" OnClick="lbtnExceluploadcompWar_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="panel-body panelscollbar height150">
                                                                <asp:GridView ID="grdPcwarranty" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <div style="margin-top: -3px;">
                                                                                <asp:LinkButton ID="lbtnRemovePcWarra" OnClientClick="return ConfDel();" runat="server" OnClick="lbtnRemovePcWarra_Click">
                                                                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                      <%--  <ItemStyle Width="10px" />
                                                                        <HeaderStyle Width="10px" />--%>
                                                                    </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="pcw_com" runat="server" Text='<%# Bind("Pc_com") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PC/Channel/Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="pcw_pc" runat="server" Text='<%# Bind("Pc_code") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="pcw_status" runat="server" Text='<%# Bind("Pc_item_stus") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Pc_item_st_des" runat="server" Text='<%# Bind("Pc_item_st_des") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Period">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="pcw_prd" runat="server" Text='<%# Bind("Pc_wara_period") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="From">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="pcw_from" runat="server" Text='<%# Bind("Pc_valid_from", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="To">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="pcw_to" runat="server" Text='<%# Bind("Pc_valid_to", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
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
                                        <asp:Panel runat="server" ID="pnlchannel" Visible="false">
                                            <div class="row">

                                                <div class="col-sm-6 paddingLeft0">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading paddingtopbottom0">
                                                            Channel  Wise Warranty
                                                        </div>
                                                        <div class="panel-body">

                                                            <div class="col-sm-5">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Company
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtChaCom" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtChaCom_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Channel
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtChan" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtChan_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Item Status
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlchanStatus" runat="server" class="form-control">
                                                                                <asp:ListItem Text="Profit"></asp:ListItem>
                                                                                <asp:ListItem Text="Cost"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1 ">
                                                                            Period
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlChanPrd" runat="server" class="form-control">
                                                                                <asp:ListItem Text="Profit"></asp:ListItem>
                                                                                <asp:ListItem Text="Cost"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnAddcannelWara" runat="server" OnClick="lbtnAddcannelWara_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="panel-body panelscollbar height150">
                                                                    <asp:GridView ID="grdcannelWara" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="cwcom" runat="server" Text='<%# Bind("Cw_com") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Channel">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="chaw_chan" runat="server" Text='<%# Bind("Cw_channel") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="chaw_status" runat="server" Text='<%# Bind("Cw_item_status") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Cw_item_st_des" runat="server" Text='<%# Bind("Cw_item_st_des") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Period">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="chaw_prd" runat="server" Text='<%# Bind("Cw_warranty_prd") %>' Width="100px"></asp:Label>
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
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-sm-8 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        Service Schedule
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Item Status
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlserSts" runat="server" class="form-control">
                                                                                <asp:ListItem Text="Profit"></asp:ListItem>
                                                                                <asp:ListItem Text="Cost"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Term
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlserTerm" runat="server" class="form-control">
                                                                                <asp:ListItem Text="1"></asp:ListItem>
                                                                                <asp:ListItem Text="2"></asp:ListItem>
                                                                                <asp:ListItem Text="3"></asp:ListItem>
                                                                                <asp:ListItem Text="4"></asp:ListItem>
                                                                                <asp:ListItem Text="5"></asp:ListItem>
                                                                                <asp:ListItem Text="5"></asp:ListItem>
                                                                                <asp:ListItem Text="6"></asp:ListItem>
                                                                                <asp:ListItem Text="7"></asp:ListItem>
                                                                                <asp:ListItem Text="8"></asp:ListItem>
                                                                                <asp:ListItem Text="9"></asp:ListItem>
                                                                                <asp:ListItem Text="10"></asp:ListItem>
                                                                                <asp:ListItem Text="11"></asp:ListItem>
                                                                                <asp:ListItem Text="12"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Period From
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtserfrom" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="diWMClick validateDecimal form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Period To
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtserto" onkeypress="return isNumberKey(event)" CausesValidation="false" runat="server" class="diWMClick validateDecimal form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            UOM
                                                                        </div>
                                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtseruom" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtseruom_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnsrhserUOM" runat="server" OnClick="lbtnsrhserUOM_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Distance / Duration From
                                                                        </div>
                                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtserdfrom" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <%--                                                                            <asp:LinkButton ID="lbtnserdfrom" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtserdfrom"
                                                                                PopupButtonID="lbtnserdfrom" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>--%>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Distance / Duration To
                                                                        </div>
                                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtserdto" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <%-- <asp:LinkButton ID="lbtnserdto" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtserdto"
                                                                                PopupButtonID="lbtnserdto" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>--%>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            UOM
                                                                        </div>
                                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtserduom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtserduom_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnsrhseraUOM" runat="server" OnClick="lbtnsrhseraUOM_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 paddingLeft0">
                                                                        <div class="col-sm-6 labelText1 ">
                                                                            Is Free
                                                                        </div>
                                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlserisfree" runat="server" class="form-control">
                                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnAddservice" runat="server" OnClick="lbtnAddservice_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="panel-body panelscollbar height150">
                                                                    <asp:GridView ID="grdservice" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_itemsts" runat="server" Text='<%# Bind("Msp_itm_stus") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Msp_status_de" runat="server" Text='<%# Bind("Msp_status_de") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Term">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_term" runat="server" Text='<%# Bind("Msp_term") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="From">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_from" runat="server" Text='<%# Bind("Msp_pd_from") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="To">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_to" runat="server" Text='<%# Bind("Msp_pd_to") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_uom" runat="server" Text='<%# Bind("Msp_pd_uom") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Distance/Duration  From">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_distancefrom" runat="server" Text='<%# Bind("Msp_pdalt_from") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Distance Duration To">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_distanceto" runat="server" Text='<%# Bind("Msp_pdalt_to") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_distanceuom" runat="server" Text='<%# Bind("Msp_pdalt_uom") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Is Free" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ser_isfree" runat="server" Text='<%# Bind("Msp_isfree") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Is Free">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Msp_isfree_status" runat="server" Text='<%# Bind("Msp_isfree_status") %>' Width="100px"></asp:Label>
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
                                            <div class="col-sm-4 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-sm-10">
                                                                        <div class="col-sm-2 paddingRight0 height22">
                                                                            <asp:CheckBox ID="chkwarrprint" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-10 labelText1 paddingLeft5">
                                                                            Is Print Warranty card
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-10">
                                                                        <div class="col-sm-2 paddingRight0 height22">
                                                                            <asp:CheckBox ID="chkFreeSer" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-10 labelText1 paddingLeft5">
                                                                            Free Service Included
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12">

                                                                        <div class="col-sm-5 labelText1 paddingLeft5">
                                                                            product warranty by 
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0 height22">
                                                                            <asp:CheckBox ID="chkMaintSupp" Visible="false" runat="server" />
                                                                            <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                                <asp:DropDownList ID="ddlMaintSupp" runat="server" class="form-control">
                                                                                    <asp:ListItem Text="Company" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                                                </asp:DropDownList>
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
                            </div>
                            <div class="tab-pane" id="itemCost">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3">
                                                                    Item Cost
                                                                    </div>
                                                                <div class="col-sm-5">
                                                                   
                                                                    </div>
                                                                <div class="col-sm-3">
                                                                    <asp:UpdatePanel runat="server" ID="costBtn">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="lbtnGetCostData" OnClick="lbtnGetCostData_Click" CausesValidation="false" runat="server" CssClass="floatRight" >
                                                                        <span class="glyphicon" aria-hidden="true"></span>View Cost Data
                                                                    </asp:LinkButton>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                    <div class="panel-body">
                                                        
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="panel-body panelscollbar" style="height:280px;">
                                                                    <asp:GridView ID="grdItemCost" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_com_code" runat="server" Text='<%# Bind("Icr_com_code") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_itm_code" runat="server" Text='<%# Bind("Icr_itm_code") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_itm_sts" runat="server" Text='<%# Bind("Icr_itm_sts") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_Status_Des" runat="server" Text='<%# Bind("Icr_Status_Des") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Minimum Cost">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_min_cost" runat="server" Text='<%# Bind("Icr_min_cost","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Highest Cost">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_max_cost" runat="server" Text='<%# Bind("Icr_max_cost","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Latest Cost">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Icr_curr_cost" runat="server" Text='<%# Bind("Icr_curr_cost","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button8" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Multiplestatus" runat="server" Enabled="True" TargetControlID="Button8"
                PopupControlID="pnlMultipleStatus" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMultipleStatus" DefaultButton="lbtnSearch">
                <div runat="server" id="Div13" class="panel panel-default height300 width700 panelscollbar">
                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton8" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div14" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body ">
                                            <asp:CheckBox ID="ChkStatusall" runat="server" OnCheckedChanged="ChkStatusall_CheckedChanged" Text=" Select All" AutoPostBack="true"/>
                                            <%-- <asp:CheckBoxList runat="server" ID="chkMultiplestatus"
                                                RepeatColumns="5"
                                                RepeatDirection="Vertical"
                                                RepeatLayout="Table" Width="500"
                                                TextAlign="Right"
                                                ForeColor="#333"
                                                Font-Bold="false">
                                            </asp:CheckBoxList>--%>

                                            <asp:GridView ID="grdMultiplestatus" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SELECT">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMultiplestatus" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Icr_com_code" runat="server" Text='<%# Bind("MIS_CD") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Icr_itm_code" runat="server" Text='<%# Bind("MIS_DESC") %>' Width="100px"></asp:Label>
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
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="itemwarrantypopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlItemWarranty" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlItemWarranty">
                <div runat="server" id="Div1" class="panel panel-default height300 width950">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div2" runat="server">
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
                                        <div class="panel-body panelscollbar height300">
                                            <asp:GridView ID="grdWarranty" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div style="margin-top: -3px;">
                                                                <asp:LinkButton ID="lbtnRemoveWarra" OnClientClick="return ConfDel();" runat="server" OnClick="lbtnRemoveWarra_Click">
                                                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                        <HeaderStyle Width="10px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="iw_itemsts" runat="server" Text='<%# Bind("Mwp_itm_stus") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Mwp_item_st_des" runat="server" Text='<%# Bind("Mwp_item_st_des") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="iw_seffDate" runat="server" Text='<%# Bind("Mwp_effect_dt", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="warauom" runat="server" Text='<%# Bind("mwp_warr_tp") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Period">
                                                        <ItemTemplate>
                                                            <asp:Label ID="iw_prd" runat="server" Text='<%# Bind("Mwp_val") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="iw_rem" runat="server" Text='<%# Bind("Mwp_rmk") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Suppler Period">
                                                        <ItemTemplate>
                                                            <asp:Label ID="iw_sprd" runat="server" Text='<%# Bind("Mwp_sup_warranty_prd") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Suppler Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="iw_srem" runat="server" Text='<%# Bind("Mwp_sup_wara_rem") %>' Width="100px"></asp:Label>
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
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="taxDetailspopup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnltaxdetails" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnltaxdetails">
                <div runat="server" id="Div11" class="panel panel-default height300 width950">
                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div12" runat="server">
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
                                        <div class="panel-body panelscollbar height300">
                                            <asp:GridView ID="grdTax" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="colmCode" runat="server" Text='<%# Bind("ITS_COM") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="colmDes" runat="server" Visible="false" Text='<%# Bind("ITS_STUS") %>' Width="80px"></asp:Label>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Its_stus_Des") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="colTaxtype" runat="server" Text='<%# Bind("ITS_TAX_CD") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="coltaxrate" runat="server" Text='<%# Bind("ITS_TAX_RATE") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="colmStataus" Enabled="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("ITS_ACT")) %>' Width="5px" />
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
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MultipleCom" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlMultiple" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMultiple" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-default height200 width700 panelscollbar">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton4" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div6" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body ">

                                            <asp:CheckBoxList runat="server" ID="chklstbox"
                                                RepeatColumns="5"
                                                RepeatDirection="Vertical"
                                                RepeatLayout="Table" Width="500"
                                                TextAlign="Right"
                                                ForeColor="#333"
                                                Font-Bold="false">
                                            </asp:CheckBoxList>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
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

    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="prefixpopup" runat="server" Enabled="True" TargetControlID="Button6"
                PopupControlID="pnlprefix" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlprefix">

                <div runat="server" id="Div5" class="panel panel-default height200 width700">

                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton5" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-sm-12 paddingLeft5">
                                                <div class="col-sm-5 labelText1">
                                                    Page Count
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" ID="txtpagecount" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12 paddingLeft5">
                                                <div class="col-sm-5 labelText1">
                                                    Multiple prefix allow
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlprefixallow" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlprefixallow_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>




                                </div>
                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlmultipleprefix" Visible="false">
                                        <div class="col-sm-5">
                                            <div class="row">
                                                <div class="col-sm-12 paddingLeft5">
                                                    <div class="col-sm-5 labelText1">
                                                        Company
                                                    </div>
                                                    <div class="col-sm-6 ">
                                                        <asp:DropDownList ID="ddlItemcompany" CausesValidation="false" runat="server" CssClass="form-control">
                                                            <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 paddingLeft5">
                                                    <div class="col-sm-5 labelText1">
                                                        Prefix
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox runat="server" ID="txtbookprefix" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-12 paddingLeft5">
                                                    <div class="col-sm-5 labelText1">
                                                        Descriptions
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox runat="server" ID="txtpreDescription" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>

                                            </div>


                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 paddingLeft5">
                                                    <div class="col-sm-5 labelText1">
                                                        Active
                                                    </div>
                                                    <div class="col-sm-6 ">
                                                        <asp:DropDownList ID="ddlprfix" CausesValidation="false" runat="server" CssClass="form-control">
                                                            <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:LinkButton ID="lbtnAddPrefix" runat="server" CausesValidation="false" OnClick="lbtnAddPrefix_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="panel-body panelscollbar height120">
                                                <asp:GridView ID="grdprefix" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Prifix">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_mi_prefix" runat="server" Text='<%# Bind("MI_PREFIX") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_mi_desc" runat="server" Text='<%# Bind("MI_DESC") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_mi_act" runat="server" Text='<%# Bind("MI_ACT") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_MI_ACTIVE_STATUS" runat="server" Text='<%# Bind("MI_ACTIVE_STATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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



    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlexcel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>


        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="Div7" class="panel panel-default height45 width700 ">


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose3" runat="server" OnClick="btnClose3_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblsuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-12" id="Div8" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />

                                </div>

                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>


                            </div>


                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="exceluploadCompanywarranty" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlexcelComWar" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>


        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcelComWar">
        <div runat="server" id="Div9" class="panel panel-default height45 width700 ">


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose2" runat="server" OnClick="btnClose2_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:Label ID="lblalert2" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-12" id="Div10" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload2" runat="server" />

                                </div>

                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload2" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnUpload2_Click" />
                                </div>


                            </div>


                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted2" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label6" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess2" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess2_Click" />
                                    </div>
                                </div>
                            </div>


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
            $('.validateDecimal').keypress(function (evt) {
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
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            });
            $('.diWMClick').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            /*Validate int value and length*/
            $('.valIntValue').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 5) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 5);
                    alert('Maximum 5 characters are allowed ');
                    return false;
                }
            });
            $('#BodyContent_txtPackCode,#BodyContent_txtPartNo,#BodyContent_txtItem,#BodyContent_txtBrand,#BodyContent_txtUOM,#BodyContent_txtMainCat,#BodyContent_txtCat1,#BodyContent_txtCat2,#BodyContent_txtCat3,#BodyContent_txtCat4,#BodyContent_txtColorExt,#BodyContent_txtColorInt,#BodyContent_txttrimLeft,#BodyContent_txttrimRight,#BodyContent_txtPrefix,#BodyContent_txtCompany,#BodyContent_txtclaimcom,#BodyContent_txtTaxStucture,#BodyContent_txtReCom,#BodyContent_txtMrnCom,#BodyContent_txtMrndes,#BodyContent_txtSupCom,#BodyContent_txtSupRem,#BodyContent_txtCostElement,#BodyContent_txtWarRem,#BodyContent_txtwarsRem,#BodyContent_txtwarrantycompany,#BodyContent_txtpcCom,#BodyContent_txtpcRem,#BodyContent_txtserdfrom,#BodyContent_txtserdto,#BodyContent_txtItemCom').on('keypress', function (event) {
                var regex = new RegExp("^[~!@$%^&*]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_txtModel').on('keypress', function (event) {
                var regex = new RegExp("^[~!@$%^&*]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_txtSdes,#BodyContent_txtLdes').on('keypress', function (event) {
                var regex = new RegExp("^[~!@$%^&*]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }

    </script>
</asp:Content>
