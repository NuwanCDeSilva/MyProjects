<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SerialTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.SerialTracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>

    <!-- <%@ Register Src="~/UserControls/SerSearch.ascx"  TagPrefix="uc2" TagName="SerSearch" %>   -->
    <!-- <script src="../../../Js/UserValidation.js"></script> -->
    <!-- <link href="../../../Css/style.css" rel="stylesheet" />-->
   
    <!---->
    <script type="text/javascript">

        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
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
    <!---->

   <!-- check -->
    <!-- -->

    <script type="text/javascript">
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            //  var j = document.getElementById('<%=lblItem.ClientID%>').value;

            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "0";
            }


        };

        function confirms() {
            var cnfm = confirm("hghghghghg");
        }

        function NoserialFound() {

            var serial = alert("there is noserial ");
            //   var s = $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
        }

        function PrintConfirm() {
            var selectedvalue = confirm("Are you sure you want to print?");
            //  var j = document.getElementById('<%=lblItem.ClientID%>').value;

            if (selectedvalue) {
                document.getElementById('<%=txtPrintconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtPrintconformmessageValue.ClientID %>').value = "0";
            }


        };

    </script>

    <style>
        .panel-body {
    padding: 2px;
    }

        .panel {
            margin-bottom: 2px;
        }

        .panel-group {
            margin-bottom: 5px;
        }

        .panel-heading {
            height: 18px;
            padding-top: 0px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">



    
    <div>

        <!-- <asp:Button ID="Button1" runat="server" Text="Button 1"  OnClick="Button1_Click3"  OnClientClick="return ShowModalPopup()"/> -->

        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lnkDummy1" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lnkDummy2" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lnkDummy3" runat="server"></asp:LinkButton>
        <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
        <asp:HiddenField ID="txtPrintconformmessageValue" runat="server" />
      <%--  <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
            PopupControlID="pnlPopup1" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>--%>

        <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>

        <asp:ModalPopupExtender ID="multiplepopup" BehaviorID="mpee" runat="server"
            PopupControlID="Panel3" TargetControlID="lnkDummy1" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>


        <asp:ModalPopupExtender ID="multiplepopup1" BehaviorID="mpeee1" runat="server"
            PopupControlID="PanelSerial" TargetControlID="lnkDummy2" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>

        <asp:ModalPopupExtender ID="panelpop4" BehaviorID="mpeeee1" runat="server"
            PopupControlID="Panelenterpopup" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>

        <!-- -->

        <div visible="false" class="alert alert-warning" role="alert" runat="server" id="divscro" style="width: 500px">
            <div class="col-sm-11">
                <strong>Alert!</strong>
                <asp:Label ID="lblWarning" runat="server"></asp:Label>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btnWarning" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                </asp:LinkButton>
            </div>
        </div>

        <!-- -->

        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-4 paddingRight0">
                            <div class="col-sm-1 padding0">
                                Serial
                           <%-- <table>
                                <tr>
                                    <td>
                                        <b>Serial</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label60" runat="server"></asp:Label>
                                    </td>
                       
                                    <td>--%>
                            </div>

                            <div class="col-sm-2 padding0">
                                <asp:DropDownList ID="cmbSerialType" runat="server" Height="22px" CssClass="form-control">
                                    <asp:ListItem>Select--</asp:ListItem>
                                    <asp:ListItem>Serial 1</asp:ListItem>
                                    <asp:ListItem>Serial 2</asp:ListItem>
                                    <asp:ListItem>Serial 3</asp:ListItem>
                                    <asp:ListItem>Serial 4</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2 paddingRight0">
                                Serial #
                            </div>
                            <div class="col-sm-3 padding0">
                                <asp:Panel runat="server" ID="testPanel" DefaultButton="btnShowPopup">
                                    <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" KeyDown="OnKeyDownHandler" OnTextChanged="txtSerialNo_TextChanged"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                <asp:LinkButton ID="btnShowPopup" runat="server" OnClick="LinkButton1_Click" OnCommand="btnShowPopup_Command">
                                <span class="glyphicon glyphicon-search " aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                     <%--       <div class="col-sm-2 paddingRight0">
                                Serial #
                        </div>
                            <div class="col-sm-3 padding0">
                                <asp:Panel runat="server" ID="Panel1" DefaultButton="btnShowPopup">
                                    <asp:TextBox ID="txtSerialNoAdv" runat="server" CssClass="form-control" KeyDown="OnKeyDownHandler"></asp:TextBox>
                                </asp:Panel>
                            </div>--%>
                            <div class="col-sm-2" style="padding-left: 3px; padding-right: 0px;">
                                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lBtnAdvSerialSearch_Click" OnCommand="btnShowPopup_Command">
                                <span aria-hidden="true">Advanced Serial Search</span>
                                </asp:LinkButton>
                            </div>


                        </div>

                        <div class="col-sm-2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Item" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label54" runat="server" Width="10px" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label52" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" Width="10px" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblItem" runat="server" ForeColor="Purple"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        
                        <div class="col-sm-1" style="padding-right: 30px;">
                        </div>

                        <div class="col-sm-6">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-5">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label55" runat="server" Width="2px" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Advance Search" BackColor="white" ForeColor="black" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label56" runat="server" Width="5px" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label61" runat="server" Width="32px" Font-Bold="true"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Character Case" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>

                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="cmbCaseType" runat="server" CssClass="form-control">
                                            <asp:ListItem>Normal</asp:ListItem>
                                            <asp:ListItem>Upper</asp:ListItem>
                                            <asp:ListItem>Lower</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-3 padding0">
                                        <asp:CheckBox ID="chkWholeWord" runat="server" Text="Match Whole Word" Checked="True" />
                                    </div>

                                     <div class="col-sm-1">
                                         <asp:LinkButton ID="lBtnPrint" runat="server" OnClientClick="return PrintConfirm()" OnClick="lBtnPrint_Click">
                                             <span class="glyphicon glyphicon-print fontsize20" aria-hidden="true"></span> Print
                                         </asp:LinkButton>
                                    </div>
                                    
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="Clear" runat="server" OnClientClick="return ClearConfirm()" OnClick="LinkButton2_Click">
                        <span class="glyphicon glyphicon-refresh fontsize20" aria-hidden="true"></span> Clear
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>

                        </div>

                        <!--
                        <div class="col-sm-4">

                            <asp:TextBox ID="txtSerialNo1" runat="server" Width="100px" placeholder="Enter Serial"></asp:TextBox>





                            <asp:LinkButton ID="btnShowPopup1" runat="server" >
                        <span class="glyphicon glyphicon-search " aria-hidden="true"></span>
                            </asp:LinkButton>




                            <asp:Label ID="Label1_test" runat="server" Text="Item" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblItem1" runat="server" ForeColor="Purple"></asp:Label>




                        </div>




                        <div class="col-sm-5">

                            <asp:Label ID="Label2_test" runat="server" Text="Advance Search" BackColor="Purple" ForeColor="White"></asp:Label>
                            <asp:Label ID="Label3_test" runat="server" Text="Character Case" Font-Bold="true"></asp:Label>

                            <asp:DropDownList ID="cmbCaseType_test" runat="server">
                                <asp:ListItem>Normal</asp:ListItem>
                                <asp:ListItem>Upper</asp:ListItem>
                                <asp:ListItem>Lower</asp:ListItem>
                            </asp:DropDownList>

                            <asp:CheckBox ID="chkWholeWord_test" runat="server" Text="Match Whole World" Checked="True" />


                        </div>
                        <div>
                            <asp:LinkButton ID="Clear_test" runat="server" OnClientClick="return ClearConfirm()" OnClick="LinkButton2_Click">
                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                        -->
                    </div>
                </div>
            </div>


        </div>




        <div class="panel panel-default">
            <div class="panel-body">

                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Currently Stored at
                            </div>


                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12 labelText1">
                                        <div class="col-sm-4 labelText1">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server"><b>Location</b></asp:Label></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server"><b>:</b></asp:Label></td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblCurLocation" runat="server"  ForeColor="Purple"></asp:Label></td>
                                                </tr>
                                            </table>

                                        </div>
                                        <!--  <div class="col-sm-2"></div>  -->
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-2 labelText1">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server"><b>Company</b></asp:Label>
                                                    </td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server"><b>:</b></asp:Label></td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblCurCompany" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 labelText1">
                                        <div class="col-sm-2 labelText1">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server"><b>Recieved on</b></asp:Label>
                                                    </td>
                                                    <td style="width: 10px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label24" runat="server"><b>:</b></asp:Label>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblCurReceivedDate" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                        <div class="col-sm-2 labelText1">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label25" runat="server" Text="Bin" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label26" runat="server" Text=":" Font-Bold="true"></asp:Label></td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblCurBin" runat="server" Text="" ForeColor="Purple"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-2"></div>

                                        <div class="col-sm-2 labelText1">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label27" runat="server" Text="Item Status" Font-Bold="true"></asp:Label></td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label28" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblCurItemStatus" runat="server" Text="" ForeColor="Purple"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>

                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-sm-12 labelText1">
                                        <div class="col-sm-4 labelText1">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label29" runat="server" Text="Description" Font-Bold="true"></asp:Label></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label30" runat="server" Text=":" Font-Bold="true"></asp:Label></td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblItemDescription" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-2 "></div>
                                        <div class="col-sm-2 labelText1">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label31" runat="server" Text="Model" Font-Bold="true"></asp:Label></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td style="width: 10px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label36" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblItemModel" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="col-sm-2 labelText1">
                                            <asp:Label ID="Label32" runat="server" Text="Brand" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label34" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label39" runat="server" Width="1px" Text="" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblItemBrand" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                        </div>


                                    </div>
                                </div>

                                <!--        <div class="row">
                                    <div class="col-sm-12 labelText1">
                                        <div class="col-sm-2">

                                        </div>
                                    </div>
                                </div> -->

                                <!--enter waranty here -->

                                <div class="row">
                                    <div class="col-sm-12 labelText1">



                                        <div class="col-sm-4 labelText1">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" Text="Warranty No" Font-Bold="true"></asp:Label></td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label22" runat="server" Text=":" Font-Bold="true"></asp:Label></td>

                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="" ForeColor="Purple"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-2 labelText1">
                                            <asp:Label ID="Label16" runat="server" Text="Period" Font-Bold="true"></asp:Label>
                                             <asp:Label ID="Label51" runat="server" Width="33px" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label50" runat="server" Width="1px" Text=":" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label40" runat="server" Width="1px" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label17" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                        </div>


                                    </div>
                                </div>

                                <!--enter waranty here -->



                            </div>

                        </div>
                    </div>
                </div>






            </div>
        </div>



        <!--new div-->



        <!-- -->



        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default">
                    <div class="panel panel-body">
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label60" runat="server" Text="GRN No:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblgrnno" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label66" runat="server" Text="GRN Date:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblgrndate" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label68" runat="server" Text="Supplier:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblsupp" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label70" runat="server" Text="Sys BL No:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblsysbl" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label72" runat="server" Text="BL No:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblbl" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label74" runat="server" Text="BL Date:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblbldate" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label76" runat="server" Text="LC No &nbsp;&nbsp;&nbsp;:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lbllcno" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-2 labelText1">
                            <asp:Label ID="Label67" runat="server" Text="Supplier Invoice # &nbsp;&nbsp;&nbsp;:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblSupInvno" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label71" runat="server" Text="Supplier Invoice Date &nbsp;&nbsp;&nbsp;:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblSupInvDt" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-3 labelText1">
                        </div>
                        <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label69" runat="server" Text="Serial 1 :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblSerial1" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                        <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label75" runat="server" Text="Serial 2 :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblSerial2" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                          <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label73" runat="server" Text="Cusdec Entry Date" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblcusdecEntryDate" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                          <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label78" runat="server" Text="Entry" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblEntryNo" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                          <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label80" runat="server" Text="CusDec Entry" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblMvEntry" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                         <div class="col-sm-3 labelText1">
                            <asp:Label ID="Label82" runat="server" Text="Financing Ref. No" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblFinancingRef" runat="server" Font-Bold="true" ForeColor="Purple"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <!-- before updated nov 6-->

        <!--      </div>
             </div>   -->

        <!-- another panel -->
        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">

                <div class="panel-heading" role="tab" id="headingTwo">


                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" style="color: #000000">

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-2">Details </div>
                            </div>
                        </div>
                    </a>

                </div>

                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">


                                <!--         <div class="col-sm-2">
                             <div class="panel panel-default">
                             <div class="panel-body">
                              <asp:ImageButton ID="ImageButton2" runat="server" Visible="False" />
                                 </div></div>
                         </div>  -->
                                <div class="col-sm-4 padding0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Physical Details
                                    

                                        </div>



                                        <div class="panel-body">
                                            <!-- newly updated nov 13 -->
                                            <div class="row">
                                                <div class="col-sm-12 labelText1">
                                                    <div class="col-sm-6 labelText1">

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div></div>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" Text="Ext, Color" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label41" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmColor" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>


                                                    </div>
                                                    <div class="col-sm-2 labelText1"></div>



                                                    <div class="col-sm-4 labelText1">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label19" runat="server" Text="UOM" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 10px;"></td>
                                                                <td style="width: 10px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label42" runat="server" Text=":" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmUOM" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>



                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 labelText1">
                                                    <div class="col-sm-6 labelText1">

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" Text="W x H x L" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 1px"></td>
                                                                <td>
                                                                    <asp:Label ID="Label43" runat="server" Text=":" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 1px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmDimension" runat="server" Text="" ForeColor="Purple"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-2"></div>
                                                    <div class="col-sm-4 labelText1">
                                                        <table>
                                                            <tr>

                                                                <td>
                                                                    <asp:Label ID="Label21" runat="server" Text="Weight" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label44" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 6px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmWeight" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- newly updated nov 13 -->




                                        </div>
                                    </div>
                                </div>


                                <div class="col-sm-7">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Additional Details
                                    

                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-sm-12 labelText1">
                                                    <div class="col-sm-3 labelText1">

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text="Main Category" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label45" runat="server" Text=":" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmMainCat" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-3 labelText1">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label33" runat="server" Text="Hs Code" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px"></td>
                                                                <td>
                                                                    <asp:Label ID="Label47" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmHSCode" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>


                                                    </div>

                                                    <div class="col-sm-6 labelText1">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label35" runat="server" Text="Serials Availability" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 30px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label48" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmSerSts1" runat="server" Text="Serial 1" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label57" runat="server" Width="3px"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmSerSts2" runat="server" Text="Serial 2" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label58" runat="server" Width="3px"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmSerSts3" runat="server" Text="Serial 3" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label59" runat="server" Width="3px"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmSerSts4" runat="server" Text="Serial 4" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>



                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 labelText1">
                                                    <div class="col-sm-4 labelText1">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label37" runat="server" Text="HP Availability" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label46" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>

                                                                <td>
                                                                    <asp:Label ID="lblItmHPAvailability" runat="server" Text="" ForeColor="Purple"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-2 labelText1"></div>
                                                    <div class="col-sm-6 labelText1">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label38" runat="server" Text="Insurance Availability" Font-Bold="true"></asp:Label></td>
                                                                <td style="width: 10px;"></td>
                                                                <td>
                                                                    <asp:Label ID="Label49" runat="server" Text=":" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td>
                                                                    <asp:Label ID="lblItmInsuAvailability" runat="server" Text="" ForeColor="Purple"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>

                                            </div>




                                        </div>
                                    </div>
                                    <!-- panel ends-->

                                </div>



                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <!-- -->

        <div class="panel panel-default">

            <div class="panel-heading" style="height: 22px;">
                <div class="row">
                    <div class="col-sm-12">
                        <div style="margin-top: -3px;">
                            <div class="col-sm-2">
                            <asp:LinkButton ID="LinkButtonsubserial" runat="server" OnClick="LinkButtonsubserial_Click">
                                            <span class="glyphicon glyphicon-save"> Sub Serial </span>
                                        </asp:LinkButton>
                                </div>
                            <div class="col-sm-2">
                            <asp:LinkButton ID="compDet" runat="server" OnClick="lbtnComp_Click">
                                <span class="glyphicon glyphicon-search"> Component </span>
                            </asp:LinkButton>

                        </div>
                       </div>
                    </div>
                </div>

            </div>

            <div class="panel-body" style="display: none">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="col-sm-12">


                            
                            <!-- grid -->


                            




                        </div>
                    </div>
                </div>

            </div>

        </div>

        <!-- new Update -->
        <asp:Panel runat="server" ID="costPannel">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <asp:GridView ID="GridView2" runat="server" GridLines="None" Style="border-collapse: collapse" CssClass="table table-hover table-striped" AutoGenerateColumns="False">

                                    <Columns>
                                        <asp:BoundField HeaderText="Storage Cost" DataField="Total sales" />
                                        <asp:BoundField HeaderText="Storage Cost" DataField="Total sales" />

                                    </Columns>
                                    <EmptyDataTemplate>

                                        <table border="0" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                            <tr style="background-color: lightgrey;">
                                                <th scope="col">Storage Cost
                                                </th>
                                                <th scope="col">Storage Cost
                                                </th>


                                            </tr>
                                            <tr>

                                                <td colspan="7">No customers found.
                                                </td>
                                            </tr>

                                        </table>
                                    </EmptyDataTemplate>



                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12" style="height: 150px;">
                            <div class="col-sm-12">
                                <table border="0" style="border-collapse: collapse" class="table table-hover table-striped">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label62" runat="server" Text=""></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="Label63" runat="server" Text=""><b>Current Cost
                                                <asp:Label Text="" ID="lblCurrCostCode" runat="server" />
                                            </b></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label64" runat="server" Text=""><b>Original Cost
                                                <asp:Label Text="" ID="lblOrgCostCode" runat="server" /></b></asp:Label></td>
                                    </tr>
                                    <tr>

                                        <td>
                                            <b>Product Cost</b> </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Storage Cost</b> </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Handling Cost</b> </td>
                                        <td>
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>Total</b></td>
                                        <td>
                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>Total No of Days</b></td>
                                        <td>
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- -->
        </asp:Panel>
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            Movement
                        </div>
                        <div class="col-sm-2"></div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-4">
                            Sale With Returns
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-5" style="padding-left: 2px; padding-right: 1px;">
                            <div class="panel panel-default">
                                <div class="panel-body panelscollbar height120" style="overflow-x: hidden;">


                                    <asp:GridView ID="gvMovement" runat="server" GridLines="None" Style="border-collapse: collapse"  
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records found. " 
                                        CssClass="table table-hover table-striped bound" AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvMovement_PageIndexChanging" AllowSorting="true" OnSorting="gvMovement_Sorting"  AllowPaging="True" PageSize="6">

                                        <Columns>
                                            <asp:BoundField DataField="ITS_COM" HeaderText="Com" SortExpression="ITS_COM" />
                                            <asp:BoundField DataField="ITS_DOC_NO" HeaderText="Doc No" SortExpression="ITS_DOC_NO" />
                                            <asp:BoundField DataField="ITS_DOC_DT" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}" SortExpression="ITS_DOC_DT" />
                                            <%--<asp:BoundField DataField="ITH_LOC" HeaderText="Tra. Loc" />--%>
                                            <asp:TemplateField HeaderText="Tra. Loc" SortExpression="ITH_LOC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocDesc" runat="server" ToolTip='<%#Bind("ITH_LOC") %>' Text='<%#Eval("ITH_LOC").ToString().Length > 5? (Eval("ITH_LOC") as string).Substring(0,5) : Eval("ITH_LOC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="ITH_OTH_LOC" HeaderText="Oth. Loc" SortExpression="ITH_CRE_WHEN" />--%>
                                             <asp:TemplateField HeaderText="Oth. Loc" SortExpression="ITH_OTH_LOC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocOthDesc" runat="server" ToolTip='<%#Bind("ITH_OTH_LOC") %>' Text='<%#Eval("ITH_OTH_LOC").ToString().Length > 5? (Eval("ITH_OTH_LOC") as string).Substring(0,5) : Eval("ITH_OTH_LOC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ITH_CRE_WHEN" HeaderText="Create Date" DataFormatString="{0:d}"  SortExpression="ITH_CRE_WHEN" />
                                            <asp:boundfield datafield="ITS_DOC_DT" headertext="Tra. Date" dataformatstring="{0:dd/MMM/yyyy hh:mm tt}" headerstyle-forecolor="Purple" />
                                            <%--<asp:BoundField DataField="" HeaderText="Unit Cost" DataFormatString="{0:N2}"><HeaderStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:BoundField>--%>
                                             <asp:TemplateField >
                                                <HeaderTemplate>
                                                    <asp:Label Text="" ID="lblCostCd" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitPrice" Text='<%# Bind("its_unit_cost","{0:N2}") %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-7" style="padding-left: 2px; padding-right: 1px;">
                            <div class="panel panel-default">

                                <div class="panel-body panelscollbar height120">

                                    <!-- sale table -->
                                    <asp:GridView ID="gvSale" runat="server" GridLines="None" Style="border-collapse: collapse" 
                                         ShowHeaderWhenEmpty="True" EmptyDataText="No Records found. "
                                        CssClass="table table-hover table-striped" AutoGenerateColumns="False">

                                        <Columns>
                                            <asp:BoundField HeaderText="Company" DataField="SAH_COM" />
                                            <asp:BoundField HeaderText="Date" DataField="SAH_DT" DataFormatString="{0:dd/MMM/yyyy}" />
                                            <asp:BoundField HeaderText="Invoice" DataField="SAH_INV_NO" />
                                            <asp:BoundField HeaderText="Customer" DataField="SAH_CUS_NAME" />
                                            <asp:BoundField HeaderText="Profit Center" DataField="SAH_PC" />
                                            <asp:BoundField HeaderText="Item" DataField="SAD_ITM_CD" />

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label Text="" ID="lblCostCd" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitPrice" Text='<%# Bind("SAD_TOT_AMT","{0:N2}") %>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Price" DataField="SAD_TOT_AMT" DataFormatString="{0:N2}">
                                            </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="Waranty Period" DataField="sad_warr_period">
                                                 <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                </asp:BoundField>
                                            <asp:BoundField DataField="sad_warr_remarks" HeaderText="Warranty Remarks" />

                                                             <asp:BoundField HeaderText="Pri. Book" DataField="SAD_PBOOK" />
                                            <asp:BoundField HeaderText="Pri. Level" DataField="SAD_PB_LVL" />

                                        </Columns>
                                    </asp:GridView>
                                    <!-- -->
                                    <!--   <asp:GridView ID="gvSale1" runat="server" GridLines="None" CellSpacing="0" style="border-collapse:collapse" CssClass="table table-hover table-striped"   AutoGenerateColumns="False">
                                         <Columns>
                                    <asp:BoundField HeaderText="Company" DataField="SAH_COM" />
            <asp:BoundField HeaderText="Date" DataField="SAH_DT"  DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Invoice" DataField="SAH_INV_NO"/>
            <asp:BoundField HeaderText="Customer" DataField="SAH_CUS_NAME"/>
            <asp:BoundField HeaderText="Profit Center" DataField="SAH_PC"/>
            <asp:BoundField HeaderText="Item" DataField="SAD_ITM_CD"/>
            <asp:BoundField HeaderText="Price" DataField="SAD_TOT_AMT"/>
           <asp:BoundField HeaderText="Waranty" DataField="sad_warr_period"/>
            
                                    <asp:BoundField DataField="sad_warr_remarks" HeaderText="Warranty Remarks" />
                                                 
                                </Columns>
                                
                                <EmptyDataTemplate>
            <table  border="0"  style="border-collapse:collapse" class="table table-hover table-striped" rules="all">
                <tr>
                    <th scope="col">
                        Company
                    </th>
                    <th scope="col">
                       Date
                    </th>
                    <th scope="col">
                        Invoice
                    </th>
                    <th scope="col">
                    Customer
                        </th>
                    <th scope="col">
                        Profit Center
                    </th>
                    <th scope="col">
                        Item
                    </th>
                    <th scope="col">
                        Price
                    </th>
                    <th scope="col">
                       Waranty
                    </th>
                    <th scope="col">
                       Waranty Remarks
                    </th>
                    
                </tr>
                <tr>
                    <td colspan = "9" >
                      No customers found.
                    </td>
                </tr>
                
            </table>
        </EmptyDataTemplate>

                                   </asp:GridView> -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- div tag -->
                </div>
            </div>
        </div>
        <!-- change table -->
        <!-- added new table-->
        <!-- -->
        <!-- -->
        <!-- -->
        <div>
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">


                <div runat="server" id="Div1" class="panel panel-primary">

                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label><div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-12">

                                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">


                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                        <p style="font-size: 15px">Do You Want To Clear the Page?</p>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12"></div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">

                                        <asp:Button class="btn btn-primary btn-sm" Width="100px" ID="Button7" runat="server" Text="Ok" OnClick="Button7_Click" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button class="btn btn-primary btn-sm" Width="100px" ID="Button8" runat="server" Text="Cancel" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- -->
            </asp:Panel>

        </div>



        <!---nov 11 update -->




        <!-- -->
        <div>
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Style="display: none">
                <div runat="server" id="Div2" class="panel panel-primary">
                    <div class="panel-default">


                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-12">

                                    <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12">

                                <asp:GridView ID="gvMultipleItem" runat="server" CellSpacing="0" Style="border-collapse: collapse" CssClass="table table-hover table-striped" AutoGenerateColumns="False" OnSelectedIndexChanged="gvMultipleItem_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="#C2D69B" />
                                    <Columns>
                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                        <asp:BoundField DataField="ins_ser_1" HeaderText="Serial" />
                                        <asp:BoundField DataField="ins_itm_cd" HeaderText="Item" />
                                        <asp:BoundField DataField="MI_LONGDESC" HeaderText="Description" />
                                        <asp:BoundField DataField="MI_MODEL" HeaderText="Model" />
                                        <asp:BoundField DataField="MI_BRAND" HeaderText="Brand" />
                                        <asp:BoundField DataField="ins_ser_1" HeaderText="Serial1"/>
                                        <asp:BoundField DataField="ins_ser_2" HeaderText="Serial2"/>
                                    </Columns>
                                    <EmptyDataTemplate>

                                        <table border="1" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                            <tr style="background-color: lightgrey;">
                                                <th scope="col">Serial </th>
                                                <th scope="col">Item </th>
                                                <th scope="col">Description </th>
                                                <th scope="col">Model </th>
                                                <th scope="col">Brand </th>
                                            </tr>
                                            <tr>
                                                <td colspan="6">No customers found. </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <!-- -->
        <div>
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label><asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <!--no serial -->
        <div>
            <asp:Panel ID="PanelSerial" runat="server" CssClass="modalPopup" Style="display: none">


                <div runat="server" id="Div3" class="panel panel-primary">

                    <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label><div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-12">

                                    <asp:LinkButton ID="LinkButton3" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">


                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-10">
                                        <p style="font-size: 15px;">There is no such serial available</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- -->
            </asp:Panel>
        </div>
        <!-- -->
        <!--ENTER SERIAL -->
        <div>
            <asp:Panel ID="Panelenterpopup" runat="server" CssClass="modalPopup" Style="display: none">


                <div runat="server" id="Div4" class="panel panel-primary">

                    <asp:Label ID="Label13" runat="server" Text="Label" Visible="false"></asp:Label><div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-12">

                                    <asp:LinkButton ID="LinkButton4" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">


                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-10">
                                        <p style="font-size: 15px">Please select the serial number</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- -->
            </asp:Panel>
        </div>
        <!-- -->
        <!--grid not needed -->
        <div style="display: none">

            <asp:GridView ID="GridView1" runat="server">
                 <AlternatingRowStyle BackColor="#C2D69B" />
                                    <Columns>
                                        
                                        <asp:BoundField DataField="irsm_warr_no" HeaderText="Serial" />
                                        <asp:BoundField DataField="irsm_warr_period" HeaderText="Item" />


                                    </Columns>


                                              <EmptyDataTemplate>

                                        <table border="1" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                            <tr style="background-color: lightgrey;">
                            <th scope="col">Serial </th>
                            <th scope="col">Item </th>
                        </tr>
                        <tr>
                            <td colspan="2">No customers found. </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <!--grid not needed -->
    </div>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

        

            <asp:Panel runat="server" ID="pnlpopup">
        <div runat="server" id="test" class="panel panel-default width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label><div class="panel panel-default">
                <div class="panel-heading">
                    
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-10">
                                <strong><b>Sub Serial Advanced Search</b></strong>
                            </div>
                            <div class="col-sm-2">
                        <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                
                    <!-- -->
                            <div class="row">
                    <div class="col-sm-12">

                        <div class="col-sm-12">         
                    
                            <asp:GridView ID="gvSubSerial" runat="server" GridLines="None" Style="border-collapse: collapse" CssClass="table table-hover table-striped" AutoGenerateColumns="False">

                                <Columns>
                                    <asp:BoundField HeaderText="Item" DataField="irsms_itm_cd" />
                                    <asp:BoundField HeaderText="Description" DataField="irsms_warr_no" />
                                    <asp:BoundField HeaderText="Model" DataField="irsms_mfc" />
                                    
                                
                                    <asp:BoundField HeaderText="Status" DataField="mis_desc" />
                                    <asp:BoundField HeaderText="Qty" DataField="irsms_qty" DataFormatString="{0:n}" />
                                    <asp:BoundField HeaderText="Serial" DataField="irsms_sub_ser" />
                                    <asp:BoundField HeaderText="Waranty" DataField="irsms_warr_rem" />

                                </Columns>
                                <EmptyDataTemplate>

                                                <table border="0" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                        <tr style="background-color: lightgrey;">
                                                        <th scope="col">Item </th>
                                                        <th scope="col">Description </th>
                                                        <th scope="col">Model </th>
                                                        <th scope="col">Status </th>
                                                        <th scope="col">Qty </th>
                                                        <th scope="col">Serial </th>
                                                        <th scope="col">Waranty </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7">No records found. </td>
                                                    </tr>

                                    </table>

                                </EmptyDataTemplate>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <!-- -->
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ItmResPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="ItmResPopupID" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

   <div id="divItemRes" class="row">
        <div class="col-sm-12 col-lg-12">
    <asp:Panel runat="server" ID="ItmResPopupID">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <strong>Item Component Details</strong>
                    <asp:LinkButton ID="LinkButton5" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                </div>
                <div class="panel-body">
                    
                    
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    
                                    <asp:GridView ID="gdvResDet" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnRowDataBound="gdvResDet_RowDataBound" OnPageIndexChanging="gdvResDet_PageIndexChanging">
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Adv Serial Popup --> 
   <%--   <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <asp:Button ID="Button11" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="taxDetailspopup" runat="server" Enabled="True" TargetControlID="Button11"
                PopupControlID="taxpnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divItemRes" class="row">
        <div class="col-sm-12 col-lg-12">
            <asp:Panel runat="server" ID="taxpnlpopup">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <strong>Item Component Details</strong>
                        <asp:LinkButton ID="LinkButton8" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <div>
                                       
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" OnTextChanged="txtSearchbyword_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="panel-body">


                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="grdSerial" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                            CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnRowDataBound="grdSerial_RowDataBound" OnPageIndexChanging="grdSerial_SelectedIndexChanged">
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
        </div>
    </asp:Panel>
</div>
    </div>--%>


    <!-- Advanced Search Panel Popup -->
   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup2" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopup2" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlpopup2">



                 <div runat="server" id="Div6" class="panel panel-primary Mheight">
            <asp:Label ID="Label67" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                    </asp:LinkButton>
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
                                Search by Serial #
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdSerialData" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnRowDataBound="grdSerial_RowDataBound" OnSelectedIndexChanged="grdSerial_SelectedIndexChanged">

                                            <Columns>
                                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitm" runat="server" Text='<%# Bind("INS_SER_ID") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Serial #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("INS_SER_1") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>

                                            <SelectedRowStyle BackColor="Silver" />

                                        </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <!-- Advanced Search Panel Popup  END-->

   <%-- <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button11" runat="server" Text="Button" Style="display: none;" />
           <asp:ModalPopupExtender ID="taxDetailspopup" runat="server" Enabled="True" TargetControlID="Button11"
                PopupControlID="pnltaxdetails" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

             <asp:Panel runat="server" ID="pnltaxdetails">
                 <div runat="server" id="Div11" class="panel panel-default height300 width950">
                    <asp:Label ID="Label65" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton7" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>

                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div12" runat="server">
                                <div class="row">
                                    <div class="col-sm-3">

                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" OnTextChanged="txtSearchbyword_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                   
                         

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body panelscollbar height300">
                                           
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                            
                                             <asp:GridView ID="grdSerial" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                               <asp:CommandField ShowSelectButton="true" ButtonType="Link" />
                                                    <asp:TemplateField HeaderText="Company" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="colmCode" runat="server"   Text='<%# Bind("INS_SER_ID") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="colmDes" runat="server" Visible="true" Text='<%# Bind("INS_SER_1") %>' Width="80px"></asp:Label>
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
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
                </div>

               
            </asp:Panel> --%>


    <%-- <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="taxDetailspopup" runat="server" Enabled="True" TargetControlID="Button12"
                PopupControlID="pnlpopup3" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
   
    <asp:Panel runat="server" ID="pnlpopup3" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height400 width700">
                    <asp:Label ID="Label65" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="btnClose_Click">
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height8">
                                        Please use this option only, if you are not known full serial #
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
                                    <div class="col-sm-12" id="search" runat="server">
                                        
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                        
                                        <%--        <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>--%>
                                          
                                        <div class="col-sm-2 labelText1">
                                            Search by Serial No
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by Serial No" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                       
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
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
                                    <div class="col-sm-12">
                                       <%-- <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>--%>
                                       
                                          <%--      <asp:GridView ID="grdSerial" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" >
                                                    <Columns>
                                                        <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10"  runat="server" />

                                                    </Columns>
                                                </asp:GridView>--%>
                                    
                                                <asp:GridView ID="grdSerial" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="10" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdSerial_SelectedIndexChanged" OnPageIndexChanging="grdSerial_PageIndexChanging">
                                                    <Columns>
                                                       <%-- <asp:CommandField ShowSelectButton="true" ButtonType="Link" />--%>
                                                        <%--<asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10"  />--%>
                                                       
                                                         <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="lBtnSelect" runat="server" AutoPostBack="true" Text="Select" OnClick="lBtnSelect_Click" runat="server"
                                                                        CommandArgument='<%# Eval("ITS_SER_1").ToString()%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="Company" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="colSerialID" runat="server" Text='<%# Bind("ITS_SER_ID") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="colSerialNo" runat="server" Visible="true" Text='<%# Bind("ITS_SER_1") %>' Width="80px"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="colItem" runat="server" Visible="true" Text='<%# Bind("its_itm_cd") %>' Width="80px"></asp:Label>

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

       


    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
</asp:Content>


    