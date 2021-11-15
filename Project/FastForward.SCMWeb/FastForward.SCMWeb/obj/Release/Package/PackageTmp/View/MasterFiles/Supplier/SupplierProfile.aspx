<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SupplierProfile.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.SupplierProfile" %>

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

        function ConfirmSaveSupplier() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtsavesupplier.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtsavesupplier.ClientID %>').value = "No";
            }
        };

        function ConfirmDeleteAllCurrency() {
            var selectedvalueOrdPlace = confirm("Do you want to delete selected currency/currencies ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtdelallcurr.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtdelallcurr.ClientID %>').value = "No";
            }
        };

        function ConfirmDeleteAllPOrts() {
            var selectedvalueOrdPlace = confirm("Do you want to delete selected port/ports ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtdelport.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtdelport.ClientID %>').value = "No";
            }
        };

        function ConfirmDeleteAllItems() {
            var selectedvalueOrdPlace = confirm("Do you want to delete selected item/items ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtdelitem.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtdelitem.ClientID %>').value = "No";
            }
        };

    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">

        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });

        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsavesupplier" runat="server" />
             <asp:HiddenField ID="txtdelallcurr" runat="server" />
            <asp:HiddenField ID="txtdelport" runat="server" />
            <asp:HiddenField ID="txtdelitem" runat="server" />

            <div class="panel panel-default marginLeftRight5 mainpnl ">
                <div class="panel-body">

                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-6  buttonrow paddingLeft0">

                                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                                            <div class="col-sm-11  buttonrow ">
                                                <strong>Well done!</strong>
                                                <asp:Label ID="lblok" runat="server"></asp:Label>
                                            </div>

                                            <div class="col-sm-1  buttonrow">
                                                <div class="col-sm-3  buttonrow">
                                                    <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivokclose_Click">
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
                                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndicalertclose_Click">
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
                                                    <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivinfoclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">

                                        <div class="col-sm-6  buttonRow">
                                            <div class="col-sm-2"></div>

                                            <div class="col-sm-2 paddingRight0">
                                            </div>

                                            <div class="col-sm-2 paddingRight0">
                                            </div>

                                            <div class="col-sm-2 paddingRight0">
                                            </div>

                                            <div class="col-sm-2 paddingRight0">
                                                <asp:LinkButton ID="lbtnApproval" TabIndex="44" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSaveSupplier();" OnClick="lbtnApproval_Click">
                                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="lbtnClear" TabIndex="45"  CausesValidation="false"  runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnClear_Click">
                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-5">
                                    <div class="panel panel-default supsubgridmargin supdetailpnlheight">

                                        <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    Supplier Profile
                                                </div>
                                            </div>
                                        </div>

                                        <div class="panel-body supplierleftgrids">

                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Supplier Code
                                                        </div>
                                                        <div class="col-sm-7 paddingRight3 paddingLeft0">
                                                             <asp:TextBox ID="txtsupcode" runat="server" TabIndex="1" CssClass="form-control" AutoPostBack="true" MaxLength="20" OnTextChanged="txtsupcode_TextChanged"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingRight3 paddingLeft0">
                                                         <asp:LinkButton ID="lbtnsupsearch" runat="server" TabIndex="2" OnClick="lbtnsupsearch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        </div>

                                                    </div>

                                                    <div class="col-sm-4 paddingLeft0 paddingRight1">
                                                             
                                                        </div>

                                                        <div class="col-sm-8 paddingLeft0 paddingRight1 suppliercodetxt">
                                                             <asp:TextBox ID="txtsupname" runat="server" TabIndex="3" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Active
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                             <asp:CheckBox ID="chkactive" runat="server" TabIndex="4" AutoPostBack="true"/>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            TIN #
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                            <asp:TextBox runat="server"  ID="txttin" MaxLength="20" TabIndex="5" CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Tax Rg. #
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txttaxrg" MaxLength="60" TabIndex="6" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Address
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtadd1" MaxLength="200" TabIndex="7" CssClass="form-control" TextMode="MultiLine" style="resize:none"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtadd2" MaxLength="200" TabIndex="8" CssClass="form-control" style="resize:none" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Contact Persons
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtcontactperson" MaxLength="50" TabIndex="9" CssClass="form-control" style="resize:none" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Telephone
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txttel" MaxLength="15" onkeydown="return jsDecimals(event);" TabIndex="10" CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Fax
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                             <asp:TextBox runat="server" ID="txtfax" MaxLength="15" onkeydown="return jsDecimals(event);" TabIndex="11" CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            E-mail
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                             <asp:TextBox runat="server" ID="txtemail" MaxLength="50" TabIndex="12"  CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="col-sm-6">

                                                    <div class="row">

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Web Site
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                             <asp:TextBox runat="server" ID="txtweb" MaxLength="20" TabIndex="13" CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">

                                                        <div class="col-sm-4 labelText1">
                                                            Country of Origin
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 paddingRight1">
                                                             <asp:TextBox ID="txtcountry" runat="server" CssClass="form-control" AutoPostBack="true" MaxLength="15"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtncountry" TabIndex="14" runat="server" CausesValidation="false" OnClick="lbtncountry_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-4 paddingLeft0 paddingRight1">
                                                             <asp:TextBox ID="txtcountry2" runat="server"  CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Deling Currency
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0 paddingRight1">
                                                                <asp:TextBox ID="txtdelcurr" runat="server"  CssClass="form-control" AutoPostBack="true" MaxLength="20"></asp:TextBox>
                                                            </div>

                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtndelcurr" runat="server" TabIndex="15" CausesValidation="false" OnClick="lbtndelcurr_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                            <div class="col-sm-4 paddingLeft0 paddingRight1">
                                                                <asp:TextBox ID="txtdelcurr2" runat="server"  CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                           Credit Period
                                                        </div>
                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                             <asp:TextBox runat="server" ID="txtcreditperiod" TabIndex="16" onkeydown="return jsDecimals(event);" CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3 paddingRight12 paddingLeft5">
                                                             <asp:Label ID="lbldays" runat="server" Text="Days"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                           GL Acc.Code
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                             <asp:TextBox runat="server" ID="txtglacc" MaxLength="20" TabIndex="17" CssClass="form-control" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                          Supplier Type
                                                        </div>
                                                        <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                             <asp:DropDownList runat="server" ID="ddlsuptype" TabIndex="18" AutoPostBack="true" CssClass="form-control" ></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                           Tax Category
                                                        </div>
                                                        <div class="col-sm-7 paddingRight3 paddingLeft0">
                                                             <asp:TextBox runat="server" ID="txttaxcat" MaxLength="20" CssClass="form-control" ></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtntaxcat" runat="server" TabIndex="19" CausesValidation="false" OnClick="lbtntaxcat_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    <div class="row">

                                                        <div class="panel panel-default suppliersptaxpnl">

                                                            <div class="panel-heading">
                                                                <div class="row">
                                                                    <div class="col-sm-7">
                                                                        Special Tax
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <asp:CheckBox ID="chksptax" AutoPostBack="true" TabIndex="20" runat="server" OnCheckedChanged="chksptax_CheckedChanged"  />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="panel-body">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Code / Rate
                                                                    </div>
                                                                    <div class="col-sm-4 paddingLeft0 paddingRight1">
                                                                        <asp:TextBox ID="txttaxcode" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                                        <asp:LinkButton ID="lbtncoderate" TabIndex="21" runat="server" CausesValidation="false" OnClick="lbtncoderate_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                    <div class="col-sm-3 paddingLeft0 paddingRight1">
                                                                        <asp:TextBox ID="txttaxrate" runat="server" CssClass="form-control" onkeydown="return jsDecimals(event);" MaxLength="20"></asp:TextBox>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-sm-12 height5">
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-4 labelText1">
                                                                        
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                                        <asp:TextBox runat="server" ID="txttaxratecd" MaxLength="20" onkeydown="return jsDecimals(event);" TabIndex="22" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-sm-12 height5">
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-4 labelText1">
                                                                        Div Rate
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight3 paddingLeft0">
                                                                        <asp:TextBox runat="server" ID="txttaxdivrate" MaxLength="20" onkeydown="return jsDecimals(event);" TabIndex="22" CssClass="form-control"></asp:TextBox>
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

                                <div class="col-sm-7">

                                    <div class="panel panel-default supsubgridmargin">
                                        <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    Supplier Ports
                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="lbtndelports" runat="server" TabIndex="28" CausesValidation="false" OnClientClick="ConfirmDeleteAllPOrts();" OnClick="lbtndelports_Click">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>Delete Ports
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="panel-body">

                                            <div class="row">

                                                <div class="col-sm-1 labelText1">
                                                    Port Code
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtportcode" runat="server"  CssClass="form-control" MaxLength="20" AutoPostBack="true" OnTextChanged="txtportcode_TextChanged"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                    <asp:LinkButton ID="lbtnportcode" runat="server" TabIndex="29" CausesValidation="false" OnClick="lbtnportcode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                                  <div class="col-sm-1 labelText1">
                                                    Port Name
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtportname" runat="server" TabIndex="30" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                </div>

                                                 <div class="col-sm-1 labelText1">
                                                   Lead time CMB
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtleadtime" runat="server" TabIndex="31" onkeydown="return jsDecimals(event);" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-sm-1 labelText1">
                                                    
                                                </div>
                                                <div class="col-sm-2">
                                                    
                                                </div>

                                                <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                    
                                                </div>

                                                  <div class="col-sm-2 labelText1">
                                                   
                                                </div>
                                                <div class="col-sm-2">
                                                    
                                                </div>

                                                 <div class="col-sm-2 labelText1">
                                                   
                                                </div>
                                                <div class="col-sm-2">
                                                       <asp:LinkButton ID="lbtnaddports" CausesValidation="false" TabIndex="32" CssClass="floatRight" runat="server" OnClick="lbtnaddports_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 ">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body panelscoll height5">

                                                            <asp:GridView ID="grdports" AutoGenerateColumns="false" TabIndex="33" runat="server" OnRowDataBound="grdports_RowDataBound" OnRowDeleting="grdports_RowDeleting" OnRowEditing="grdports_RowEditing" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                <Columns>

                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:BoundField DataField="mspr_frm_port" HeaderText="Port Code" ItemStyle-Width="100" ReadOnly="true" />
                                                                                    <asp:BoundField DataField="mp_name" HeaderText="Port Name" ItemStyle-Width="150" ReadOnly="true"  />
                                                                                    <asp:BoundField DataField="mspr_lead_time" HeaderText="Lead Time (Days)" ItemStyle-Width="150"   />

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <div id="editbtndiv">
                                                                                                <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:15px"></span>Edit
                                                                                                </asp:LinkButton>
                                                                                            </div>

                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>

                                                                                            <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server" OnClick="OnUpdate">
                                                                                            <span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:15px"></span>Update
                                                                                            </asp:LinkButton>


                                                                                            <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" OnClick="OnCancel" runat="server">
                                                                                            <span class="glyphicon glyphicon-remove-circle" aria-hidden="true" style="font-size:15px"></span>Cancel
                                                                                            </asp:LinkButton>

                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <div id="delbtndiv">
                                                                                                <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server">
                                                                                                      <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span>Delete
                                                                                                </asp:LinkButton>
                                                                                            </div>

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

                                        <div class="col-sm-12">

                                            <div class="panel panel-default itmfstsearchpnlhieght">
                                                <div class="panel-heading">Item Fast Search & Assign </div>
                                                <div class="panel-body">

                                                    <div class="row">

                                                        <div class="col-sm-1 labelText1">
                                                            Main Cat.
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtmaincat" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtnmaincat" runat="server" TabIndex="34" CausesValidation="false" OnClick="lbtnmaincat_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-1 labelText1">
                                                            Sub Cat.
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtsubcat" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtnsubcat" runat="server" TabIndex="35" CausesValidation="false" OnClick="lbtnsubcat_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-1 labelText1">
                                                            Item Range
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtitmrange" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtnitmrange" runat="server" TabIndex="36" CausesValidation="false" OnClick="lbtnitmrange_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
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

                                                        <div class="col-sm-1 labelText1">
                                                            Brand
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtbrand" runat="server"  CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtnbrand" runat="server" TabIndex="37" CausesValidation="false" OnClick="lbtnbrand_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-1 labelText1">
                                                            Item Code
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtitemcode" runat="server"  CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtnitemcode" runat="server" TabIndex="38" CausesValidation="false" OnClick="lbtnitemcode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-1 labelText1">
                                                           Model No
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtmodelno" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight1">
                                                            <asp:LinkButton ID="lbtnmodel" runat="server" TabIndex="39" CausesValidation="false" OnClick="lbtnmodel_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
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

                                                        <div class="col-sm-1 labelText1">
                                                            <asp:LinkButton ID="lbtnfindall" runat="server" TabIndex="40" CausesValidation="false" OnClick="lbtnfindall_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>Search
                                                            </asp:LinkButton>
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
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    
                                                     <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">

                                                        <div class="col-sm-5">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel panel-default">

                                                                        <div class="panel-heading">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                   Search Items
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="panel-body panelscoll height5 itemsgridheight">

                                                                            <asp:GridView ID="grdselecteditems" AutoGenerateColumns="false" TabIndex="41" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                <Columns>

                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:BoundField DataField="CODE" HeaderText="Item Code" ItemStyle-Width="100"  />
                                                                                    <asp:BoundField DataField="DESCRIPT" HeaderText="Description" ItemStyle-Width="150"  />

                                                                                </Columns>
                                                                            </asp:GridView>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-sm-1 paddingLeft5">
                                                            <asp:LinkButton ID="lbtnadditem" runat="server" TabIndex="42" CssClass="floatRight" CausesValidation="false" OnClick="lbtnadditem_Click">
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel panel-default">

                                                                        <div class="panel-heading">
                                                                            <div class="row">
                                                                                <div class="col-sm-4">
                                                                                   Supplier Items
                                                                                </div>

                                                                                <div class="col-sm-2">
                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" TabIndex="28" CausesValidation="false" OnClientClick="ConfirmDeleteAllItems();" OnClick="LinkButton1_Click">
                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>Delete Items
                                                                                    </asp:LinkButton>
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                        <div class="panel-body panelscoll height5 itemsgridheight">

                                                                            <asp:GridView ID="grdallitems" AutoGenerateColumns="false" TabIndex="43" runat="server" CssClass="table table-hover table-striped" GridLines="None" OnRowDataBound="grdallitems_RowDataBound" OnRowDeleting="grdallitems_RowDeleting" OnRowEditing="grdallitems_RowEditing" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                <Columns>

                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:BoundField DataField="mbii_itm_cd" HeaderText="Item Code" ItemStyle-Width="100" ReadOnly="true"  />
                                                                                    <asp:BoundField DataField="mi_shortdesc" HeaderText="Description" ItemStyle-Width="150" ReadOnly="true"/>

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <div id="delbtndiv">
                                                                                                <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span>Delete
                                                                                                </asp:LinkButton>
                                                                                            </div>

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

                                    </div>


                                </div>
                            </div>
                            
                            <div class="row">

                                <div class="col-sm-5">
                                    <div class="panel panel-default suppliercurgrdpossition">

                                        <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-7">
                                                    Supplier Currency
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="lbtndelallcurr" runat="server" TabIndex="23" CausesValidation="false" OnClientClick="ConfirmDeleteAllCurrency();" OnClick="lbtndelallcurr_Click">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>Delete Currencies
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-sm-12">

                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Currency
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtcurrency" TabIndex="24" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtcurrency_TextChanged"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnfindcurr" CausesValidation="false" TabIndex="25" CssClass="floatRight" OnClick="lbtnfindcurr_Click" runat="server">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-6 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtcurrencyname" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnaddcurr" CausesValidation="false" TabIndex="26" CssClass="floatRight" runat="server" OnClick="lbtnaddcurr_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <br />
                                                        <br />

                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class="panel-body  panelscoll height5 pnlscrollsupplier ">
                                                                    <div class="row">
                                                                        <div class="col-sm-12 ">

                                                                            <asp:GridView ID="grdsupcurrency" AutoGenerateColumns="false" TabIndex="27" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="grdsupcurrency_RowDataBound" OnRowDeleting="grdsupcurrency_RowDeleting" OnSelectedIndexChanged="grdsupcurrency_SelectedIndexChanged" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                <Columns>

                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-Width="100"  />
                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150"  />

                                                                                    <asp:ButtonField Text="Select this as Default Currency" CommandName="Select" ItemStyle-Width="150" />

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <div id="delbtndiv">
                                                                                                <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span>Delete
                                                                                                </asp:LinkButton>
                                                                                            </div>

                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                </Columns>
                                                                                 <SelectedRowStyle BackColor="Silver" />
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

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>


        </ContentTemplate>

    </asp:UpdatePanel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopupSupplier" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
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
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click" CausesValidation="false">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                    <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" >
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
