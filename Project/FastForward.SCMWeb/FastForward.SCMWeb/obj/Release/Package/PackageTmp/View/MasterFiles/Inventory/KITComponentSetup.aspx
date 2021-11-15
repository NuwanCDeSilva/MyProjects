<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="KITComponentSetup.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.KITComponentSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

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
        function inactive()
        {
            alert("Already Inactive");
        }
    </script>
    <script>
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

        function filterDigits(eventInstance) {
            eventInstance = eventInstance || window.event;
            key = eventInstance.keyCode || eventInstance.which;
            if ((key < 58) && (key > 47) || key == 45 || key == 8) {
                return true;
            }

            else {
                if (eventInstance.preventDefault)
                    eventInstance.preventDefault();
                eventInstance.returnValue = false;
                return false;

            } //if
        } //filterDigits


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };


        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function SaveConfirm() {

            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {

            var selectedvalue = confirm("Do you want to inactive item?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };       
        function UpdateConfirm() {

            var selectedvalue = confirm("Do you want to update data?");
            if (selectedvalue) {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ApproveConfirm() {

            var selectedvalue = confirm("Do you want to approve data?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="row">
            <div class="col-sm-12 buttonrow">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="col-sm-12 buttonRow paddingRight5" id="divTopCheck" runat="server">
                            <div class="col-sm-7 buttonRow padding0">
                            </div>
                            <div class="col-sm-5 buttonRow padding0">

                                <div class="col-sm-6">
                                </div>

                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="btnSave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnPrint" CausesValidation="false" runat="server" OnClick="lbtnPrint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnclear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row" id="divMainRow">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel-body paddingbottom0">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-heading pannelheading  paddingtop0">
                                    KIT ComponentSetup
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="col-sm-3 labelText1">
                                                Kit Code
                                            </div>
                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtkititemcode" Style="text-transform: uppercase" runat="server" TabIndex="7" OnTextChanged="txtkititemcode_TextChanged"
                                                    CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 paddingLeft0">
                                                <asp:LinkButton ID="lbtnkititem" Visible="true" CausesValidation="false" runat="server" OnClick="lbtnkititem_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-8 labelText1 " style="color: red">
                                            <asp:Label runat="server" ID="lblkitDes"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-9">
                                            <div class="panel panel-default " id="y5">
                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                    <div class="row">
                                                        <div class="col-sm-8 paddingRight0">
                                                            Kit Item
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-2">
                                                            Item                                                    
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1">
                                                            No of unit                                                        
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            Unit Cost(Rs)
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            Order Seq #
                                                        </div>
                                                        <div class="col-sm-1">
                                                            Type         
                                                        </div>
                                                        <div class="col-sm-1">
                                                            Status          
                                                        </div>
                                                        <div class="col-sm-1">
                                                            Ch.M.Item  
                                                        </div>
                                                        <div class="col-sm-1">
                                                            Scan   
                                                        </div>
                                                        <div class="col-sm-1">
                                                            Scan Seq 
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-3">
                                                            <div class="col-sm-10 paddingLeft0">
                                                                <asp:TextBox runat="server" ID="txtitm" AutoPostBack="true" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btnSearch_Item" CausesValidation="false" runat="server" OnClick="btnSearch_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        
                                                        <div class="col-sm-1 padding0">
                                                            <asp:TextBox runat="server" ID="txtnounit" onkeydown="return jsDecimals(event);" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:TextBox runat="server" ID="txtcost" onkeydown="return jsDecimals(event);" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:TextBox runat="server" ID="txtseq" onkeydown="return jsDecimals(event);" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:DropDownList ID="ddltype" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="MAIN" Value="M"></asp:ListItem>
                                                                <asp:ListItem Text="COM" Value="C"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:DropDownList ID="ddlstatus" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="INACTIVE" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:DropDownList ID="ddlmain" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:DropDownList ID="ddlscan" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:TextBox runat="server" ID="txtscanseq" onkeypress="filterDigits(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:LinkButton ID="lbtnAddItem" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">

                                                            <div class="row">
                                                                <div class="col-sm-12">


                                                                    <div class="col-sm-5 paddingLeft0">
                                                                        <div class="row">
                                                                            <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                Description:
                                                                            </div>
                                                                            <div class="col-sm-8 paddingRight0" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="row">
                                                                            <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                Model:
                                                                            </div>
                                                                            <div class="col-sm-9" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="row">
                                                                            <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                Brand:
                                                                            </div>
                                                                            <div class="col-sm-9" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>



                                                                </div>
                                                            </div>


                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <div class="col-sm-12 ">
                                                            <div class="row">
                                                                <div class="col-sm-12 height20">
                                                                </div>
                                                            </div>
                                                            <div class="panelscoll2">
                                                                <asp:GridView ID="grdKitcomp" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>

                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false"  runat="server" OnClick="lbtnkitDelete_Click" OnClientClick="DeleteConfirm()">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="2px" />
                                                                        </asp:TemplateField>

                                                                           <asp:TemplateField HeaderText="Edit">
                                                                            <ItemTemplate>
                                                                                <%--<asp:LinkButton ID="lbtnEdit" CausesValidation="false" CommandName="Edit" runat="server" OnClick="lbtnkitEdit_Click">--%>
                                                                                    <asp:Button ID="Button8" runat="server" Text="Edit" OnClick="lbtnkitEdit_Click"/>
                                                                            <%--<span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:15px"></span>--%> 
                                                                                <%--</asp:LinkButton>--%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="2px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_itm_code_main" runat="server" Text='<%# Bind("mikc_itm_code_component") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_desc_component" runat="server" Text='<%# Bind("mikc_desc_component") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No of unit">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_no_of_unit" runat="server" CssClass="floatRight paddingRight15" Text='<%# Bind("mikc_no_of_unit" ) %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit Cost(Rs)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_cost" runat="server" CssClass="floatRight paddingRight5" Text='<%# Bind("mikc_cost","{0:n2}") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Order Seq #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_seq_no" runat="server"  CssClass="floatRight paddingRight5" Text='<%# Bind("mikc_seq_no") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_tp" runat="server" CssClass="paddingLeft5" Text='<%# Bind("MIKC_ITM_TYPE","{0:n2}") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_active"   runat="server" Text='<%# Bind("ACTIVEDES") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Chn.M.Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_chg_main_serial"  CssClass="paddingLeft5"  runat="server" Text='<%# Bind("CHANGITEM") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Scan">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_isscan"  CssClass="floatRight paddingRight5" runat="server" Text='<%# Bind("ISSCAN") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Scan Seq">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_scan_seq" CssClass="floatRight paddingRight5" runat="server" Text='<%# Bind("mikc_scan_seq") %>'></asp:Label>
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
                                        <div class="col-sm-3">
                                            <div class="panel panel-default " id="y5">
                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                    <div class="row">
                                                        <div class="col-sm-8 paddingRight0">
                                                            Finish Good Item
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-4">Item code</div>
                                                        <div class="col-sm-5 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtfitem" AutoPostBack="true" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control" OnTextChanged="txtfitem_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClick="btnSearch2_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-4  ">
                                                            Chargerble
                                                        </div>
                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                            <asp:DropDownList ID="ddlPayType" CausesValidation="false" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="CHA" Value="CHA"></asp:ListItem>
                                                                <asp:ListItem Text="FOC" Value="FOC"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-4  ">
                                                            Cost
                                                        </div>
                                                        <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtfcost" onkeypress="filterDigits(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-4  ">
                                                        </div>

                                                        <asp:LinkButton ID="lbtnaddfin" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnaddfin_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                        </asp:LinkButton>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 ">
                                                            <div class="row">
                                                                <div class="col-sm-12 height20">
                                                                </div>
                                                            </div>
                                                            <div class="panelscoll2">
                                                                <asp:GridView ID="grdfitem" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>

                                                                        <asp:TemplateField HeaderText="Remove">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClick="lbtnFItemDelete_Click" OnClientClick="DeleteConfirm()">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="2px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblifc_fg_item_code" runat="server" Text='<%# Bind("ifc_fg_item_code") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Chargerble">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblifc_cost_type" runat="server" Text='<%# Bind("ifc_cost_type") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Cost">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmikc_no_of_unit" runat="server" Text='<%# Bind("ifc_cost_amount") %>'></asp:Label>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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

</asp:Content>
