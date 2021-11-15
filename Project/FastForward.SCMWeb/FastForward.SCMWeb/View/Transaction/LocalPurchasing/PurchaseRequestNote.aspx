<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PurchaseRequestNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Local_Purchasing.PurchaseRequestNote" %>
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

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtapprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtcancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancel.ClientID %>').value = "No";
            }
        };


    function Check(textBox, maxLength) {
          if (textBox.value.length > maxLength) {
               alert("Maximum characters allowed are " + maxLength);
               textBox.value = textBox.value.substr(0, maxLength);
          }
     };

     function Enable() {
            return;
        }
    
    function ConfirmDelete() {
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };


        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
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

    <style type="text/css">
        .spaced input[type="radio"] {
            margin-right: 5px;
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

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
   
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
            <asp:HiddenField ID="txtapprove" runat="server" />
            <asp:HiddenField ID="txtcancel" runat="server" />
              <asp:HiddenField ID="txtconfirmdelete" runat="server" />

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
                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright" >
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

                    <div class="col-sm-4  buttonRow">

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="lbtnsave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                          <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="LinkButton1_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtnapprove" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmApprove();" OnClick="lbtnapprove_Click">
                            <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel();" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint();" OnClick="lbtnprint_Click">
                            <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                            </asp:LinkButton>
                        </div>


                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Request Variation
                                </div>

                                <div class="panel-body">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Date
                                            </div>

                                            <div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRequestDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="txtRequestDate"
                                                        PopupButtonID="lbtnimgselectdate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnimgselectdate" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Required Date
                                            </div>

                                            <div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRequriedDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calexreq" runat="server" TargetControlID="txtRequriedDate"
                                                        PopupButtonID="lbtnimgselectreqdate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldv2" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnimgselectreqdate" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Request #
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtreqno" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnreqnosearch" runat="server" TabIndex="3" OnClick="lbtnreqnosearch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Ref #
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtrefno" runat="server" TabIndex="4" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                            </div>
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

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Remarks
                                            </div>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TabIndex="5" CssClass="form-control" Width="1211px" TextMode="MultiLine" style="resize:none"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading">
                                    Order Items
                                </div>

                                <div class="panel-body panelscollPRN">

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Item
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtitem" runat="server" AutoPostBack="true" OnTextChanged="txtitem_TextChanged"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnmodelfind" runat="server" TabIndex="6" OnClick="lbtnmodelfind_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Status
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="7" AutoPostBack="true" Enabled="true" CssClass="form-control"></asp:DropDownList>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Qty
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtqty" AutoPostBack="true" OnTextChanged="txtqty_TextChanged" runat="server" TabIndex="8" CssClass="form-control" onkeydown="return jsDecimals(event);" MaxLength="8"  Style="text-align: right"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Remarks
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtitmremarks" runat="server" TabIndex="9" CssClass="form-control" Width="530px" TextMode="MultiLine" style="resize:none" onKeyUp="javascript:Check(this, 200);" onChange="javascript:Check(this, 200);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-9">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:LinkButton ID="lbtnadditems" CausesValidation="false" TabIndex="10" CssClass="floatRight" runat="server" OnClick="lbtnadditems_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="panel-body">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">

                                                    <div class="panel-heading panelHeadingInfoBar">

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Description-
                                                                </div>

                                                                <div class="col-sm-8 prnlbldescription">
                                                                    <asp:Label ID="lbldesc" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                   
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                   
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                   
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Model-
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    <asp:Label ID="lblmodel" runat="server" ForeColor="#A513D0" ></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Brand-
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    <asp:Label ID="lblbrand" runat="server" ForeColor="#A513D0" ></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Part #-
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    <asp:Label ID="lblpart" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:GridView ID="grdorderdetails" AutoGenerateColumns="false" TabIndex="11" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="grdorderdetails_RowDataBound"  OnRowDeleting="OnRowDeleting" OnRowEditing="grdorderdetails_RowEditing" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                      

                                        <Columns>
                                            <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="150" ReadOnly="true" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="300" ReadOnly="true" />
                                            <asp:BoundField DataField="Brand" HeaderText="Brand" ItemStyle-Width="150" ReadOnly="true" />
                                            <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-Width="150" ReadOnly="true" />
                                            <asp:BoundField DataField="ItemStatus" HeaderText="Status" ItemStyle-Width="150" ReadOnly="true"/>
                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-Width="150"/>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="150"/>

                                            <asp:BoundField DataField="Line" HeaderText="Line" ItemStyle-Width="150" Visible="false"/>
                                            <asp:BoundField DataField="B.Qty" HeaderText="B.Qty" ItemStyle-Width="150" Visible="false"/>
                                            <asp:BoundField DataField="Po.Qty" HeaderText="Po.Qty" ItemStyle-Width="150" Visible="false"/>
                                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="150" Visible="false"/>
                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <div id="editbtndiv">
                                                        <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </ItemTemplate>
                                                <EditItemTemplate>

                                                    <div id="dvupdatebtn">
                                                    <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server" OnClick="OnUpdate">
                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    </div>

                                                      <div id="dvcancelbtn" class="prncalcelbutton">
                                                     <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" runat="server" OnClick="OnCancel">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                          </div>

                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <div id="delbtndiv">
                                                        <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
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

        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvResultUser_PageIndexChanging" OnSelectedIndexChanged="dvResultUser_SelectedIndexChanged">
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
