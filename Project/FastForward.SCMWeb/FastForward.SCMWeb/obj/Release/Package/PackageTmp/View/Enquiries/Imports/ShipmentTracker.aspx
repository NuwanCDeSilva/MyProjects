<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ShipmentTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Imports.ShipmentTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>

      <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

     <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />


    <script type="text/javascript">
        function pageLoad(sender, args) {
            jQuery(".DateextBox1").datetimepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".ActualEtatxt").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".clearancetxt").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        }
       
        
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
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
                sticky: true,
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


        function DateValidationETA(sender, args) {
            var fromDate = document.getElementById('<%=TextBoxfrom.ClientID%>').value;
            var toDate = document.getElementById('<%=TextBoxto.ClientID%>').value;
            if (toDate < fromDate) {
                document.getElementById('<%=TextBoxto.ClientID%>').value = document.getElementById('<%=hdfCurrentDate.ClientID%>').value;
                fromDate = "";
                toDate = "";
                showStickyWarningToast('Please select a valid date range !');

            }
        }

        function DateValid(sender, args) {
            var fromDate = document.getElementById('<%=TextBox7.ClientID%>').value;
            var toDate = document.getElementById('<%=TextBox8.ClientID%>').value;
            if (toDate < fromDate) {
                document.getElementById('<%=TextBox8.ClientID%>').value = document.getElementById('<%=hdfCurrentDate.ClientID%>').value;
                fromDate = "";
                toDate = "";
                showStickyWarningToast('Please select a valid date range !');

            }
        }


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
        }
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
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
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
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
    <script type="text/javascript">
        //variable that will store the id of the last clicked row
        var previousRow;

        function ChangeRowColor(row) {
            //If last clicked row and the current clicked row are same
            if (previousRow == row)
                return;//do nothing
                //If there is row clicked earlier
            else if (previousRow != null)
                //change the color of the previous row back to white
                document.getElementById(previousRow).style.backgroundColor = "#ffffff";

            //change the color of the current row to light yellow

            document.getElementById(row).style.backgroundColor = "#ffffda";
            //assign the current row id to the previous row id 
            //for next row to be clicked
            previousRow = row;
        }
    </script>

   <script type="text/javascript">
         function calculateError(sender, args) {
        <%--     __doPostBack(document.getElementById('<%=Button1.ClientID%>'), '');--%>
        
             document.all('Button1').click()
             
         }
    </script>

    <style>
        .panel {
            margin-bottom: 1px;
             margin-top: 1px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel14">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
     <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-8  buttonrow">
                </div>
                <div class="col-sm-4  buttonRow">

                    <div class="col-sm-9">
                    </div>

                    <div class="col-sm-3">
                        <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-heading paddingtopbottom0">
                    <strong>Shipment Tracker</strong>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                    <asp:LinkButton ID="lnkDummy1" runat="server"></asp:LinkButton>
                    <asp:HiddenField ID="hdfCurrentDate" Value="" runat="server" />
                    <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>

                    <asp:ModalPopupExtender ID="multiplepopup" BehaviorID="mpee" runat="server"
                        PopupControlID="Panel3" TargetControlID="lnkDummy1" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>

                    <asp:Label ID="Labelmycompany" runat="server" Text="" Visible="false"></asp:Label>


                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <!-- new menu -->
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-5  paddingRight0">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    Company
                                                                </div>
                                                                <div class="col-sm-4 padding0">
                                                                    <asp:TextBox ID="TextBoxcmpny" ValidationGroup="g" runat="server" placeholder="Company"
                                                                         CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1" style="padding-left:3px; padding-right:0px;">

                                                                    <asp:LinkButton ID="LinkButtoncompanySearch" runat="server" OnClick="LinkButton1_Click"> 
                                                                        <span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                                                    <asp:Label ID="Labeldescription" Visible="false" runat="server" Text=""></asp:Label>


                                                                </div>
                                                                <%-- <div class="col-sm-1">
                                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click"> <span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                                    <asp:Label ID="Label5" Visible="false" runat="server" Text=""></asp:Label>
                                                </div>--%>
                                                                <div class="col-sm-1" style="padding-left:3px; padding-right:0px;">
                                                                    <asp:CheckBox ID="CheckBoxal" runat="server" AutoPostBack="true" Checked="false" ValidationGroup="g" 
                                                                        OnCheckedChanged="CheckBoxal_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-6 padding0">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1 text-right">B/L#</div>

                                                                <div class="col-sm-7 padding0">

                                                                    <asp:TextBox ID="TextBoxBL" runat="server" CssClass="form-control"></asp:TextBox>

                                                                </div>


                                                                <div class="col-sm-1" style="padding-left:3px; padding-right:0px;">

                                                                    <asp:LinkButton ID="LinkButtonBLLoad" runat="server" OnClick="LinkButtonBLLoad_Click">
                                                       <span class="glyphicon glyphicon-search "></span>
                                                                    </asp:LinkButton>

                                                                </div>

                                                                <%--   <div class="col-sm-1" style="display: none">
                                                            <asp:CheckBox ID="CheckBoxall2" runat="server" Enabled="false" />
                                                        </div>--%>

                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="LinkButtonsearch2" runat="server" OnClick="LinkButtonsearch2_Click">
                                                                <span class="glyphicon glyphicon-search fontsize20"></span>

                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <!-- new menu -->
                                        </div>

                                        <!-- new menu-->
                                        <div class="col-sm-4">

                                            <div class="row">
                                                <!--  change new menu 12 -->
                                                <div class="col-sm-12 padding0">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="col-sm-1 labelText1">
                                                                ETA
                                                            </div>

                                                            <div class="col-sm-1 labelText1">
                                                                From
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:TextBox ID="TextBoxfrom" Format="dd/MMM/yyyy" onkeypress="filterDigits(event)" Width="80px" 
                                                                    Enabled="true" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>


                                                            </div>
                                                            <div class="col-sm-1"></div>

                                                            <div class="col-sm-1" style="margin-left: 15px;">

                                                                <asp:LinkButton ID="btFrom" runat="server" CausesValidation="false"> 
                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>

                                                                <asp:CalendarExtender ID="CalendarExtenderfrom" runat="server" TargetControlID="TextBoxfrom" Animated="true"
                                                                    PopupButtonID="btFrom" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>



                                                            </div>
                                                            <div class="col-sm-1">
                                                                To
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:TextBox ID="TextBoxto" Width="80px" Enabled="true" onkeypress="filterDigits(event)" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>

                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-1 " style="margin-left: 15px;">

                                                                <asp:LinkButton ID="LinkButtonto" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>

                                                                <asp:CalendarExtender ID="CalendarExtenderto" runat="server" TargetControlID="TextBoxto" Animated="true"
                                                                    PopupButtonID="LinkButtonto" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="DateValidationETA">
                                                                </asp:CalendarExtender>

                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="LinkButtonSearch" runat="server" CausesValidation="false" OnClick="LinkButtonSearch_Click"> <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span></asp:LinkButton>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <!-- End new menu-->
                                        <div class="col-sm-4 ">

                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row ">
                                                        <div class="col-sm-12 labelText1 padding0">

                                                            <div class="col-sm-1 labelText1">Clearance</div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-1 labelText1">From</div>
                                                            <div class="col-sm-1 ">

                                                                <asp:TextBox ID="TextBox7" runat="server" onkeypress="filterDigits(event)" Enabled="true" CausesValidation="false" CssClass="form-control" Width="80px"></asp:TextBox>
                                                                <asp:Label ID="Label18" runat="server" Width="10px"></asp:Label>
                                                            </div>

                                                            <div class="col-sm-1" style="margin-left: 45px">

                                                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>

                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox7" Animated="true"
                                                                    PopupButtonID="LinkButton3" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>




                                                            </div>
                                                            <div class="col-sm-1 labelText1">To</div>
                                                            <div class="col-sm-1">
                                                                <asp:TextBox ID="TextBox8" onkeypress="filterDigits(event)" runat="server" Enabled="true" CausesValidation="false" CssClass="form-control" Width="80px"></asp:TextBox>

                                                            </div>
                                                            <div class="col-sm-1" style="margin-left: 45px;">

                                                                <asp:LinkButton ID="LinkButtoncleardate" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>

                                                                <asp:CalendarExtender ID="CalendarExtenderclearance" runat="server" TargetControlID="TextBox8" Animated="true"
                                                                    PopupButtonID="LinkButtoncleardate" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="DateValid">
                                                                </asp:CalendarExtender>


                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="Searchbtn" runat="server" OnClick="Searchbtn_Click"> <span class="glyphicon glyphicon-search fontsize20 " aria-hidden="true"></span></asp:LinkButton>
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
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-9">
                                                <strong>B/L Details</strong>
                                            </div>
                                            
                                            <div class="col-sm-3">
                                                 <asp:LinkButton ID="lbtnPopUpSearch" CausesValidation="false" OnClick="lbtnPopUpSearch_Click" runat="server"> 
                                                    <span class="glyphicon" aria-hidden="true"></span>Search By Item Criteria 

                                                 </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="panel-body">

                                    <div class="row">
                                            <div class="col-sm-12" style="height:215px;">
                                                <div>
                                                    <!--remove panel -->
                                                    <asp:GridView ID="gvBL" PagerStyle-CssClass="dgvBl cssPager" ShowHeaderWhenEmpty="True" runat="server" GridLines="None" 
                                                        ValidationGroup="g" AllowPaging="True" PageSize="5" CssClass="table table-hover table-striped" 
                                                        AutoGenerateColumns="False" OnRowCommand="gvBL_RowCommand" OnPageIndexChanging="gvBL_PageIndexChanging" 
                                                        OnSelectedIndexChanged="gvBL_SelectedIndexChanged" OnDataBound="gvBL_DataBound" OnRowDataBound="gvBL_RowDataBound">
                                                        <Columns>
                                                            <%-- <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                                <ItemStyle Width="10px"></ItemStyle>
                                                            </asp:ButtonField>--%>
                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />
                                                            <%--<asp:BoundField DataField="ib_bl_no" HeaderText="B/L#" ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="B/L#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ib_bl_no" runat="server" Text='<%# Bind("ib_bl_no") %>' Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ib_bl_dt" HeaderText="Date" DataFormatString="{0:d}" ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ib_bl_dt" runat="server" Text='<%# Bind("ib_bl_dt", "{0:dd/MMM/yyyy}") %>' Width="70px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ib_doc_no" HeaderText="Doc #" Visible="true" ItemStyle-Width="400px" />--%>
                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ib_doc_no" runat="server" Text='<%# Bind("ib_doc_no") %>' Width="100px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="IB_SUPP_CD" HeaderText="Supplier"  ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="IB_SUPP_CD" runat="server" ToolTip='<%# Bind("supp_name") %>' Text='<%# Bind("IB_SUPP_CD") %>' Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ib_agent_cd"  HeaderText="Agent" ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="Agent">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ib_agent_cd" runat="server" ToolTip='<%# Bind("Agent_name") %>' Text='<%# Bind("ib_agent_cd") %>' Width="50px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ib_stus" HeaderText="Status" ItemStyle-Width="70px" Visible="false" />
                                                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="70px" />
                                                            <asp:BoundField DataField="IB_ETD" HeaderText="ETD" DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-Width="70px" />
                                                            <asp:BoundField DataField="ib_eta" HeaderText="ETA" DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-Width="70px" />

                                                            <asp:TemplateField HeaderText="Doc. han. Date">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dochandovertxt" Width="105px" Enabled="true" CausesValidation="false" runat="server" Class="form-control DateextBox1" ToolTip='<%#Eval("ib_doc_hnd_dt") %>' Text='<%# Bind("ib_doc_hnd_dt", "{0:dd/MMM/yyyy H:mm}") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ATA" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ActualEtatxt" Width="75px" Enabled="true" CausesValidation="false"
                                                                        runat="server" CssClass="ActualEtatxt form-control" Text='<%# Bind("ib_act_eta", "{0:dd/MMM/yyyy}") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Exp.clearence">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="clearancetxt" Width="75px" Enabled="true" CausesValidation="false"
                                                                        runat="server" CssClass="clearancetxt form-control" Text='<%# Bind("ib_doc_clear_dt", "{0:dd/MMM/yyyy}") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="To Bond">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblToBond"  Text='<%# Bind("IB_REF_NO") %>'  runat="server" />
                                                                    </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="IB_REF_NO" HeaderText="To Bond"  />--%>

                                                            <asp:TemplateField HeaderText="Entry Type" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox6" Width="60px" runat="server" CssClass="form-control" ToolTip='<%# Bind("ib_cusdec_entryno") %>' Text='<%# Bind("ib_cusdec_entryno") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="St. Le. Time" Visible="true">
                                                                <ItemTemplate >
                                                                    <asp:Label ID="lblStTime" Text='<%# Bind("Ib_standard_lead") %>' Width="40px" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Act. Le. Time" Visible="true">
                                                                <ItemTemplate >
                                                                    <asp:TextBox ID="lblActTime" Text='<%# Bind("ib_act_lead")%>'  
                                                                        CssClass="lblActTime form-control text-right" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Er. Le. Time" Visible="true">
                                                                <ItemTemplate >
                                                                    <asp:Label ID="lblErTime" 
                                                                        Visible='<%# !Eval("ib_act_eta").ToString().Equals("") && (Convert.ToDecimal(Eval("Ib_error_lead"))>0)%>' 
                                                                        Text='<%# Bind("Ib_error_lead") %>' 
                                                                        Width="40px" runat="server" />
                                                                </ItemTemplate>
                                                                 <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Location" Visible="true">
                                                                <ItemTemplate >
                                                                        <asp:TextBox ID="txtLoc" Text='<%# Bind("IB_LOC_CD") %>' ToolTip='<%# Bind("ml_loc_desc") %>' AutoPostBack="true" Style="text-transform: uppercase" 
                                                                            OnTextChanged="txtLoc_TextChanged" Width="60px" CssClass="txtLoc form-control" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton4" CommandName="AddToCart" Width="10px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Add to Cart" runat="server">
                                                                        <span class="glyphicon glyphicon-save"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButtonUpdate" CommandName="UpdateCusdec" Width="10px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Update" runat="server">
                                                                        <span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>



                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                </div>
                            </div>
                            <!-- --->
                        </div>
                    </div>
                    <!-- panel 3-->
                    <div class="row">
                        <div class="col-sm-8" style="padding-right:1px;">

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <strong>Item Details</strong>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div style="height: 109px; overflow: auto;">
                                        <asp:GridView ID="dvGrn" ShowHeader = "true" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." runat="server" 
                                            GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="False" AllowPaging="True" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvGrn_PageIndexChanging" OnSelectedIndexChanged="dvGrn_SelectedIndexChanged">

                                            <Columns>
                                                <asp:ButtonField Text="Select" HeaderText="" CommandName="Select" />
                                                <asp:TemplateField HeaderText="Items">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_UnitPrice" runat="server" Text='<%# Bind("ibi_itm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mi_shortdesc" runat="server" Text='<%# Bind("mi_shortdesc") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MI_MODEL" runat="server" Text='<%# Bind("MI_MODEL") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mi_part_no" runat="server" Text='<%# Bind("mi_part_no") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ibi_qty" runat="server" Style="text-align: right" Text='<%# Bind("ibi_qty","{0:n}") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label  runat="server" Width="10px" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Financing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IBI_FIN_NO" runat="server" Text='<%# Bind("IBI_FIN_NO") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ibi_pi_no" runat="server" Text='<%# Bind("ibi_pi_no") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Entry Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ibi_pi_no_1" runat="server" Text='<%# Bind("ibi_req_qty","{0:N}") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                      <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Type" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="ibi_tp" runat="server" Text='<%# Bind("IBI_TP") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LineNo" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ibi_line" runat="server" Text='<%# Bind("ibi_line") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <%--    <asp:BoundField DataField="ibi_itm_cd" HeaderText="Items" />
                                            <asp:BoundField HeaderText="Description" DataField="mi_shortdesc" />
                                            <asp:BoundField HeaderText="Model" DataField="MI_MODEL" />
                                            <asp:BoundField HeaderText="Part#" DataField="mi_part_no" />
                                            <asp:BoundField HeaderText="Quantity" DataField="ibi_qty" DataFormatString="{0:N}">
                                                <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderAlignRight" />
                                                <ItemStyle HorizontalAlign="Right" CssClass="gridHeaderAlignRight" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Order Financing" DataField="IBI_FIN_NO" ItemStyle-Width="120px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" CssClass="gridHeaderAlignRight" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ibi_pi_no" HeaderText="PI">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" CssClass="gridHeaderAlignRight" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ibi_line" HeaderText="LineNo" Visible="true" />--%>
                                            </Columns>



                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3" style="padding-left:1px;padding-right:1px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <strong>Container Summary</strong>

                                </div>
                                <div class="panel-body ">
                                    <div style="height: 109px; overflow-y: auto; overflow-x:hidden;">
                                        <asp:GridView ID="GRNCount" runat="server" GridLines="None" EmptyDataText="No data found..." 
                                            ShowHeaderWhenEmpty="true" ShowHeader = "true"
                                            CssClass="GRNCount table table-hover table-striped" AutoGenerateColumns="False">

                                            <Columns>
                                                <%-- <asp:BoundField HeaderText="Container Type" DataField="ibc_tp" ItemStyle-Width="100px" />
                                            <asp:BoundField HeaderText="Description" DataField="ibc_desc" ItemStyle-Width="250px" />--%>
                                                <%--<asp:BoundField DataField="Container Count" HeaderText="Number of Containers" ItemStyle-Width="150px" />--%>
                                                <asp:TemplateField HeaderText="Cont. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Container_Count" runat="server" Text='<%# Bind("ibc_tp") %>'></asp:Label>
                                                    </ItemTemplate>


                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ibc_desc" runat="server" Style="text-align: center" Text='<%# Bind("ibc_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Containers">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Container_Count" runat="server" Text='<%# Bind("Container_Count") %>' ></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-1" style="padding-left:1px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Progress
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12 labelText1">
                                            <asp:CheckBox AutoPostBack="true" ID="CheckBox1" runat="server" Text="  PI" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 labelText1">
                                            <asp:CheckBox AutoPostBack="true" ID="CheckBox2" runat="server" Text="  B/L" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 labelText1">

                                            <asp:CheckBox AutoPostBack="true" ID="CheckBox4" runat="server" Text="  Cusdec" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 labelText1">
                                            <asp:CheckBox AutoPostBack="true" ID="CheckBox5" runat="server" Text="  Costing" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 labelText1">
                                            <asp:CheckBox AutoPostBack="true" ID="CheckBox3" runat="server" Text="  GRN" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="Lable_doc_no" runat="server" Visible="false" Text="Lable_doc_no"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>


                        <!--end bls-->


                        <div>
                            <asp:Label ID="LabelBLs" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="Labeltest" runat="server" Text="" Visible="false"></asp:Label>
                        </div>



                    </div>



                </div>
                <div class="panel-footer">
                    <div class="row">
                        <div class="col-sm-12 paddingtopbottom0">
                            <div class="col-sm-1 height16 Lwidth" style="background-color: LightGreen; border-style: groove">
                            </div>

                            <div class="col-sm-3">
                                – No shipment delay / Early shipment 
                            </div>
                            <div class="col-sm-1 height16 Lwidth" style="background-color: LightCoral; border-style: groove">
                            </div>

                            <div class="col-sm-2">
                                – Shipment delay  
                            </div>
                            <div class="col-sm-1 height16 Lwidth" style="background-color: white; border-style: groove">
                            </div>

                            <div class="col-sm-4">
                                – ATA Pending  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Usersppp" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="Button5">
                <div runat="server" id="Div5" class="panel panel-default height400 width700">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyDate" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-8 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordDate" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultDate" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" AutoGenerateColumns="False" OnSelectedIndexChanged="grdResultDate_SelectedIndexChanged" OnPageIndexChanging="grdResultDate_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">

                                                            <ItemStyle Width="10px" />
                                                        </asp:ButtonField>

                                                        <asp:BoundField DataField="BL NO" HeaderText="BL NO" />
                                                        <asp:BoundField DataField="SYS REF" HeaderText="SYS REF" />
                                                        <asp:BoundField DataField="BL DATE" HeaderText="BL DATE" DataFormatString="{0:d}" />
                                                        <asp:BoundField DataField="CUSDEC REF" HeaderText="CUSDEC REF" />


                                                    </Columns>
                                                    <PagerStyle CssClass="cssPager" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel15">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>

                                </asp:UpdateProgress>
                            </div>
                        </div>

                    </div>

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!--end panel 3 -->
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <div runat="server" style="width: 427px">
                <asp:Button ID="Button3" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- hiddn form-->
    <div>
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
                                    <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>

                                        <%--onkeydown="return (event.keyCode!=13);"--%>


                                        <div class="col-sm-4 paddingRight5">
                                            <asp:Panel runat="server" ID="Panel5" DefaultButton="ImgSearch">
                                                <asp:TextBox ID="txtSearchbyword" placeholder="Search by word" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </asp:Panel>
                                        </div>


                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="ImgSearch" runat="server" OnClick="ImgSearch_Click">
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

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="dvResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="dvResult_SelectedIndexChanged" OnPageIndexChanging="dvResult_PageIndexChanging" OnSelectedIndexChanging="dvResult_SelectedIndexChanging">
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
    </div>
    <!-- -->

    <!-- hidden select _select -->

    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Style="display: none">
                <div runat="server" id="Div2" class="panel panel-primary">
                    <div class="panel-default height100 width700">


                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-10 ">
                                    <strong>GRN Details</strong>

                                </div>


                                <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>

                            </div>
                        </div>


                        <div class="panel-body">
                            <div class="col-sm-12">

                                <asp:GridView ID="gvMultipleItem" runat="server" CellSpacing="0" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="False">

                                    <Columns>
                                        <asp:ButtonField Text="Select" Visible="false" CommandName="Select" ItemStyle-Width="10" />
                                        <asp:BoundField DataField="ith_loc" HeaderText="Item Location" />
                                        <asp:BoundField DataField="ith_doc_no" HeaderText="Doc No" />
                                        <asp:BoundField DataField="ith_doc_date" HeaderText="Doc Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="itb_itm_cd" HeaderText="Item Code" />
                                        <asp:BoundField DataField="mis_desc" HeaderText="Status" />
                                        <asp:BoundField DataField="itb_qty" DataFormatString="{0:n}" HeaderText="Quantity" />

                                    </Columns>


                                </asp:GridView>


                            </div>
                        </div>

                    </div>


                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>







    <!--end select -->

    <!-- start company -->

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <div runat="server" style="width: 427px">
                <asp:Button ID="Buttoncompany" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="ModalPopupExtenderCompany" runat="server" Enabled="True" TargetControlID="Buttoncompany"
                PopupControlID="testPanelcompany" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- hiddn form-->
    <div>
        <asp:Panel runat="server" ID="testPanelcompany" DefaultButton="ImgSearch">
            <div runat="server" id="Div3" class="panel panel-primary Mheight">

                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>


                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:LinkButton ID="LinkButton5" runat="server">
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
                            <div class="col-sm-12">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>

                                <div class="col-sm-3 paddingRight5">
                                    <asp:DropDownList ID="DropDownListcomp" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word 
                                </div>

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <%--onkeydown="return (event.keyCode!=13);"--%>

                                        <asp:Panel runat="server" ID="Panel7" DefaultButton="LinkButtoncompany">
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="TextBoxcm" placeholder="Search by word" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="LinkButtoncompany" runat="server" OnClick="LinkButtoncompany_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true" ></span>
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

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="GridView2" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="GridView2_PageIndexChanging" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                    <ItemStyle Width="10px" />
                                                </asp:ButtonField>
                                            </Columns>
                                            <HeaderStyle Width="10px" />
                                            <PagerStyle CssClass="cssPager" />
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>



                </div>
            </div>
        </asp:Panel>
    </div>



    <!-- -->




    <!--start bl nos -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div runat="server" style="width: 427px">
                <asp:Button ID="ButtonBL" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="UserBL" runat="server" Enabled="True" TargetControlID="ButtonBL"
                PopupControlID="testPanelBL" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- hiddn form-->

    <asp:Panel runat="server" ID="testPanelBL" DefaultButton="ImgSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

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
                                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2 labelText1">
                                Search by word 
                            </div>

                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <%--onkeydown="return (event.keyCode!=13);"--%>

                                    <asp:Panel runat="server" ID="Panel4" DefaultButton="LinkButtonBLNOS">
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="TextBox1" placeholder="Search by word" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="LinkButtonBLNOS" runat="server" OnClick="LinkButtonBLNOS_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true" ></span>
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

                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="BLLoad" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AutoGenerateColumns="False" OnPageIndexChanging="BLLoad_PageIndexChanging" OnSelectedIndexChanged="BLLoad_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                <ItemStyle Width="10px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="BL NO" HeaderText="BL#" />
                                            <asp:BoundField DataField="Doc No" HeaderText="Doc No" />
                                            <asp:BoundField DataField="Ref No" HeaderText="Ref No" />
                                            <asp:BoundField DataField="Entry No" HeaderText="Entry No" />
                                        </Columns>
                                        <HeaderStyle Width="10px" />
                                        <PagerStyle CssClass="cssPager" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </asp:Panel>



    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="serchpopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="Div4" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton6" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div7" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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
                                    <asp:TextBox ID="txtSearchbyword2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
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
    
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnDocument" runat="server" Text="" Style="display: none;" />
                    <asp:ModalPopupExtender ID="popUpSearchModel" runat="server" Enabled="True" TargetControlID="btnDocument" 
                        PopupControlID="panelDocument" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
        
  <%--  <div id="divDocument" class="row" >
        <div class="col-sm-12">--%>
            <asp:Panel runat="server" ID="panelDocument">
                <div class="panel panel-default">
                    <div class="panel panel-heading">
                        <div class="row">
                            <div class="col-sm-8">
                                <strong>Search Item Details</strong>
                            </div>
                            <div class="col-sm-3">
                                <span class="" style="margin-left: 400px" aria-hidden="true"></span>
                            </div>

                            <div class="col-sm-1 padding0">
                                <asp:LinkButton ID="lbtPopDocClose" runat="server" OnClick="lbtPopDocClose_Click">
                                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Close
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="panel panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                            <div class="col-sm-2">
                                <div class="row buttonRow">
                                    
                                </div>
                            </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblErr" ForeColor="Red" Visible="false" Text="" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                <div class="row">
                                    <asp:Label runat="server" ID="lblTestLabel" Visible="False">Test Label</asp:Label>
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Item Code
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox runat="server" ID="txtItemCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lBtnItemCode" CausesValidation="false" runat="server" OnClick="lBtnItemCode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <asp:Label runat="server" ID="lblItem" ReadOnly="true" CausesValidation="false"  CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Model
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox runat="server" ID="txtModel" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtModel_TextChanged" CssClass="form-control"></asp:TextBox>
                                        </div>
                                       
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lBtnModel" CausesValidation="false" runat="server" OnClick="lBtnModel_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                         <div class="col-sm-5 paddingRight0">
                                            <asp:Label runat="server" ID="lblModel" ReadOnly="true" CausesValidation="false"  CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Brand
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox runat="server" ID="txtBrand" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtBrand_TextChanged" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lBtnBrand" CausesValidation="false" runat="server" OnClick="lBtnBrand_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <asp:Label runat="server" ID="lblBrand" ReadOnly="true" CausesValidation="false"  CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Category 1
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox runat="server" ID="txtMainCategory" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtMainCategory_TextChanged" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lBtnMainCategory" CausesValidation="false" runat="server" OnClick="lBtnMainCategory_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <asp:Label runat="server" ID="lblMainCategory" ReadOnly="true" CausesValidation="false"  CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Category 2
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox runat="server" ID="txtSubCategory" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtSubCategory_TextChanged" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lBtnSubCategory" CausesValidation="false" runat="server" OnClick="lBtnSubCategory_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <asp:Label runat="server" ID="lblSubCategory" ReadOnly="true" CausesValidation="false"  CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Category 3
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox runat="server" ID="txtItemRange" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtItemRange_TextChanged" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lBtnItemRange" CausesValidation="false" runat="server" OnClick="lBtnItemRange_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <asp:Label runat="server" ID="lblItemRange" ReadOnly="true" CausesValidation="false"  CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-2">
                                <div class="row buttonRow">
                                    <div class="col-sm-12 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearchByCat" runat="server" OnClick="lbtnSearchByCat_Click">
                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>Search
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row buttonRow">
                                    <div class="col-sm-12 paddingLeft0">
                                        <asp:LinkButton ID="lbtnClearItemData" CausesValidation="false" runat="server" OnClick="lbtnClearItemData_Click">
                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"  ></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
       <%-- </div>
    </div>--%>
     <%-- Added By Dulaj 2018-Dec-06 --%>
       <asp:UpdatePanel ID="Up2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtenderUpdateCusdec" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopupUpdateCusdec" PopupDragHandleControlID="PopupHeader" Drag="true" 
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="pnlpopupUpdateCusdec" >
        <div runat="server" id="Div9" class="panel panel-default height300 width600">
            <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                      <asp:LinkButton ID="LinkButton11" runat="server" OnClick="lbtCusDecClose_Click">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true" >Close</span>
                    </asp:LinkButton>             
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                    <div class="col-sm-12" id="Div10" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">                            
                        </div>
                          <div class="row">
                              
                            <div class="col-sm-12" id="Div17" runat="server">                               
                                <div class="col-sm-2 labelText1">
                                    To Bond
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="TextBoxCusDocNo" CausesValidation="false"  class="form-control"  runat="server" ReadOnly="true"></asp:TextBox>
                                </div>                               
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div11" runat="server">                               
                                <div class="col-sm-2 labelText1">
                                    Cleared By
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    
                                    <asp:TextBox ID="TextBoxCleardBy" CausesValidation="false"  class="form-control" runat="server"  AutoPostBack="true" OnTextChanged="txtcleardBy_TextChanged"></asp:TextBox>
                              
                                        </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="LinkButton8" runat="server" OnClick="lbtnSearchCleard_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div13" runat="server">                               
                                <div class="col-sm-2 labelText1">
                                    Model
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="TextBoxCusdecModal" CausesValidation="false"  class="form-control" AutoPostBack="true"  OnTextChanged="txtmodal_TextChanged" runat="server"></asp:TextBox>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
                               
                              
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="lbtnSearchmodal_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                              
                              
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div14" runat="server">                               
                                <div class="col-sm-2 labelText1">
                                    CIF Val
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="TextBoxCIF" CausesValidation="false" class="form-control"  runat="server"   OnTextChanged="txtCif_TextChanged" onkeyDown="checkTextAreaMaxLength(this,event,'12');return jsDecimals(event);"></asp:TextBox>
                                </div>                               
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-12" id="Div15" runat="server">                               
                                <div class="col-sm-2 labelText1">
                                   Entry No
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="TextBoxEntryNo" CausesValidation="false" MaxLength="100" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtentryNo_TextChanged"></asp:TextBox>
                                </div>                               
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-12" id="Div16" runat="server">                               
                                <div class="col-sm-2 labelText1">
                                    Remark
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="TextBoxRemark" CausesValidation="false" TextMode="MultiLine" Columns="50" Rows="2" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtrmk_TextChanged" onkeyDown="checkTextAreaMaxLength(this,event,'100');"></asp:TextBox>
                                </div>    
                                 <div class="col-sm-2 height5 floatRight">
                                <asp:Button ID="Button7" runat="server" Text="Update"  OnClick="lbtnupdateCusdec_Click" />
                            </div>                           
                            </div>
                              
                        </div>
                        <div class="row">
                          
                        </div>                                         
                    </div>
                         </ContentTemplate></asp:UpdatePanel>    
                </div>
            </div>
        </div>
    </asp:Panel>
    
    
     <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ItemPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="Panel1" CancelControlID="lbtnHidePop" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="Panel1">
        <div runat="server" id="Div8" class="panel panel-primary Mheight">

            <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnHidePop" runat="server">
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
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="ddlSeByKey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Panel runat="server" DefaultButton="lbtnSearchPopUp">
                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>

                                <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtSearch" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSearchPopUp" runat="server" OnClick="lbtnSearchPopUp_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvSearch" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" 
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvSearch_PageIndexChanging" OnSelectedIndexChanged="dgvSearch_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
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
   
    

  

    

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script>
        Sys.Application.add_load(initSomething);
        function initSomething() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.lblActTime').keypress(function (evt) {
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
           <%-- $('#<%=gvBL.ClientID%>').bind(function () {
                console.log(jQuery('.lblActTime').val());
                if (jQuery('.lblActTime').val() == "0") {
                    jQuery('.lblActTime').val("");

                }
            });--%>
        }
        jQuery(document).ready(function () {
            console.log(jQuery('.lblActTime').val());
            $('#<%=gvBL.ClientID%>').load(function () {
                if (jQuery('.lblActTime').val() == "0") {
                    jQuery('.lblActTime').val("");

                }
            });
        })
    </script>

    <!-- -->
</asp:Content>

