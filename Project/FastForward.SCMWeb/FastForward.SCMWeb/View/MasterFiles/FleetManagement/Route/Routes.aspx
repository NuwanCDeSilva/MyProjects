<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="Routes.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Fleet_Management.Route.Routes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

        function ConfirmClearForm() {
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalsave = confirm("Do you want to save ?");
            if (selectedvalsave) {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "No";
            }
        };

        function UpdateDistance() {
            var selectedvalsave = confirm("Do you want to update the distance?");
            if (selectedvalsave) {
                document.getElementById('<%=updateDistance.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=updateDistance.ClientID %>').value = "No";
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

        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        };

    </script>

    <script type="text/javascript">
        ////$(function () {
        //document.getElementById("myText")
        //var tabName = document.getElementById("[id*=TabName]") != "" ? document.getElementById("[id*=TabName]") : "RoleCreation";
        //    $('#Tabs a[href="#' + tabName + '"]').tab('show');
        //    $("#Tabs a").click(function () {
        //        $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
        //    });
        //});
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlSave">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait33" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait33" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upAddItemsRS">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait35" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait35" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5 height525">
        <div class="panel-body">

            <div class="panel panel-default height515">



                <asp:HiddenField ID="txtconfirmclear" runat="server" />
                <asp:HiddenField ID="txtsaveconfirm" runat="server" />
                <asp:HiddenField ID="updateDistance" runat="server" />
                <asp:HiddenField ID="TabName" runat="server" />

                <ul id="myTab" class="nav nav-tabs">

                    <li >
                        <a href="#RoleCreation" data-toggle="tab">Route Schedule</a>
                    </li>

                    <li class="active">
                        <a href="#GrantPrivileges" data-toggle="tab">Route Definition</a>
                    </li>

                </ul>

                <div id="myTabContent" class="tab-content">

                    <div class="tab-pane fade" id="RoleCreation">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                            <ContentTemplate>

                                <div class="col-sm-4 buttonRow PDButtons">

                                    <div class="col-sm-3 paddingRight0">
                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:UpdatePanel runat="server" ID="pnlSave">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="lbtnsave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Save
                                                </asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="lbtnclear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnclear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:20px"></span>Clear
                                        </asp:LinkButton>

                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="LinkButton5" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="LinkButton5_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true" style="font-size:20px"></span>Excel Upload
                                        </asp:LinkButton>

                                    </div>

                                </div>

                                <div class="col-sm-12">
                                    <div class="panel panel-default height400">

                                        <div class="panel-heading">
                                            Route Schedule
                                        </div>

                                        <div class="panel-body">

                                            <div>

                                                <div class="row">

                                                    <div class="col-sm-12">

                                                        <div class="col-sm-4">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Route Code
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtroutcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtroutcode_TextChanged"></asp:TextBox>
                                                                </div>

                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnroutecod" runat="server" TabIndex="1" OnClick="lbtnroutecod_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>


                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Description
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtroutdesc" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Active
                                                                </div>

                                                                <div class="col-sm-2 paddingRight5">
                                                                    <asp:CheckBox ID="chkactive" Checked="true" TabIndex="4" runat="server" />
                                                                </div>

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

                                                    <div class="col-sm-5">

                                                        <div class="col-sm-12">
                                                            <div class="row">

                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-3 labelText1">
                                                                            Date
                                                                        </div>

                                                                        <div class="col-sm-3 paddingRight5">
                                                                            <asp:TextBox ID="txtselecteddate" runat="server" TabIndex="2" ReadOnly="false" Enabled="true" CssClass="form-control"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtselecteddate"
                                                                                PopupButtonID="lbtnfromdate" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft3" id="caldv">
                                                                            <asp:LinkButton ID="lbtnfromdate" TabIndex="1" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1">
                                                                    <%--<asp:Calendar ID="PrettyCalendar"
                                                                        runat="server" OnSelectionChanged="PrettyCalendar_SelectionChanged" >
                                                                        <TodayDayStyle ForeColor="Blue" BackColor="#9999ff"></TodayDayStyle>
                                                                        <DayStyle Font-Bold="True"
                                                                            HorizontalAlign="Left"
                                                                            Height="38px"
                                                                            BorderWidth="1px"
                                                                            BorderStyle="Solid"
                                                                            BorderColor="Black"
                                                                            Width="100px"
                                                                            VerticalAlign="Top"
                                                                            BackColor="White"></DayStyle>
                                                                        <NextPrevStyle ForeColor="Black" />
                                                                        <DayHeaderStyle Font-Size="Large" Font-Bold="True" BorderWidth="1px" ForeColor="Black" BorderStyle="Solid" BorderColor="Black" Width="100px" BackColor="White"></DayHeaderStyle>
                                                                        <TitleStyle Font-Size="Large" Font-Bold="True" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" BackColor="White"></TitleStyle>
                                                                        <WeekendDayStyle BackColor="#ffccff"></WeekendDayStyle>
                                                                    </asp:Calendar>--%>
                                                                </div>


                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-1">

                                                        <div class="col-sm-12">

                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1">
                                                                    <asp:UpdatePanel runat="server" ID="upAddItemsRS">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="lbtnadditems" CausesValidation="false" TabIndex="5" CssClass="floatRight" runat="server" OnClick="lbtnadditems_Click">
                                                        <span class="glyphicon glyphicon-plus fontsize20" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-6">

                                                        <div class="panel panel-default">
                                                            <div class="panel-heading">
                                                                :: Details ::
                                                            </div>

                                                            <div class="panel-body">

                                                                <div class="row">
                                                                    <div class="col-sm-12">

                                                                        <div class="panelscoll275">

                                                                            <asp:GridView ID="gvschdule" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="Route Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblroutecode" runat="server" Text='<%# Bind("frsh_cd") %>' Width="150px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbldescsch" runat="server" Text='<%# Bind("frsh_shed_desc") %>' Width="150px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblfrsh_shed_dt" runat="server" Text='<%# Bind("frsh_shed_dt", "{0:dd/MMM/yyyy}") %>' Width="150px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Active">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkschact" runat="server" Checked='<%#Convert.ToBoolean(Eval("frsh_act")) %>' />
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

                    <div class="tab-pane fade in active" id="GrantPrivileges">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                            <ContentTemplate>

                                <div class="col-sm-4 buttonRow PDButtons">

                                    <div class="col-sm-3 paddingRight0">
                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="LinkButton1_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Save 
                                        </asp:LinkButton>

                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="LinkButton2_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:20px"></span>Clear 
                                        </asp:LinkButton>

                                    </div>

                                     <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="LinkButton6" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="LinkButton6_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true" style="font-size:20px"></span>Excel Upload
                                        </asp:LinkButton>

                                    </div>

                                </div>

                                <div class="col-sm-12">

                                    <div class="panel panel-default">

                                        <div class="panel-heading" style="font-weight: bold">
                                            Route Creation
                                        </div>

                                        <div class="panel-body">

                                            <div>

                                                <div class="row">

                                                    <div class="col-sm-3">

                                                        <div class="row">

                                                            <div class="col-sm-3 labelText1">
                                                                Route Code
                                                            </div>

                                                            <div class="col-sm-8 paddingRight5">
                                                                <asp:TextBox ID="txtroutecodecreate" TabIndex="1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtroutecodecreate_TextChanged"></asp:TextBox>
                                                            </div>

                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnrcd" runat="server" TabIndex="2" OnClick="lbtnrcd_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="col-sm-3">

                                                        <div class="row">

                                                            <div class="col-sm-3 labelText1">
                                                                Description
                                                            </div>

                                                            <div class="col-sm-9 paddingRight5">
                                                                <asp:TextBox ID="txtroudesc" TabIndex="3" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="col-sm-3">

                                                        <div class="row">

                                                            <div class="col-sm-3 labelText1">
                                                                Route Type
                                                            </div>

                                                            <div class="col-sm-9 paddingRight5">
                                                                <asp:DropDownList ID="ddlrouttype" AutoPostBack="true" TabIndex="4" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Value="F">Flat</asp:ListItem>
                                                                    <asp:ListItem Value="H">Hill</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="col-sm-3">

                                                        <div class="row">

                                                            <div class="col-sm-3 labelText1">
                                                                Active
                                                            </div>

                                                            <div class="col-sm-9 paddingRight5">
                                                                <asp:CheckBox ID="chkrouteact" TabIndex="5" Checked="true" runat="server" />
                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                    </div>

                                </div>

                                <div class="col-sm-12">

                                    <div class="panel panel-default">

                                        <div class="panel-heading" style="font-weight: bold">
                                            Assign Warehouse
                                        </div>

                                        <div class="panel-body">

                                            <div class="col-sm-7">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading" style="font-weight: bold">
                                                        Data Entry
                                                    </div>

                                                    <div class="panel-body">

                                                        <div>
                                                            <div class="row">

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Route Code
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtrcode" Enabled="false" TabIndex="6" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtrcode_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnrucd" Visible="false" runat="server" TabIndex="7" OnClick="lbtnrucd_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            W/H Complex Company
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtwhcomp" runat="server" TabIndex="8" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtwhcomp_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnwhcom" runat="server" TabIndex="9" OnClick="lbtnwhcom_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            W/H Complex Code
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtwhcomcode_TextChanged" ID="txtwhcomcode" TabIndex="10" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Active
                                                                        </div>

                                                                        <div class="col-sm-9 paddingRight5">
                                                                            <asp:CheckBox ID="chkactivewh" TabIndex="11" runat="server" Checked="true" />
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

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Related W/H Company
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtrelwhcom_TextChanged" ID="txtrelwhcom" TabIndex="12" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnrelwcom" runat="server" TabIndex="13" OnClick="lbtnrelwcom_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            W/H Company Code
                                                                        </div>

                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtwhcompcode_TextChanged" ID="txtwhcompcode" runat="server" TabIndex="14" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Route Distance (KM)
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtdistance" runat="server" TabIndex="16" CssClass="validateDecimal form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            <asp:LinkButton ID="lbtnaddthose" CausesValidation="false" TabIndex="17" CssClass="floatRight" runat="server" OnClick="lbtnaddthose_Click">
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

                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-5">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading" style="font-weight: bold">
                                                        Details
                                                    </div>

                                                    <div class="panel-body">

                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll" runat="server" style="overflow-x: hidden;">

                                                                    <asp:GridView ID="gvdetailsWh" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Route">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblruuut" runat="server" Text='<%# Bind("frw_cd") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="W/H Complex Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblwhcom" runat="server" Text='<%# Bind("frw_wh_com") %>' Width="60px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="W/H Complex Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblwhcomcode" runat="server" Text='<%# Bind("frw_wh_cd") %>' Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Related W/H Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrelwhcom" runat="server" Text='<%# Bind("frw_com_cd") %>' Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Related W/H Company Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrelwhcomcode" runat="server" Text='<%# Bind("frw_loc_cd") %>' Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField>
                                                                                  <HeaderTemplate>
                                                                                      <asp:Label ID="lblWhDistHdr" Text="Distance (KM)" runat="server" />
                                                                                  </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblwaredistance" runat="server" Text='<%# (Convert.ToDecimal(Eval("frw_route_distance"))).ToString("N2") %>' Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                   <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkactivegrid_CheckedChanged" ID="chkactivegrid" runat="server" Checked='<%#Convert.ToBoolean(Eval("frw_act")) %>' />
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
                                
                                <div class="col-sm-12">

                                    <div class="panel panel-default">

                                        <div class="panel-heading" style="font-weight: bold">
                                            Assign Show Rooms
                                        </div>

                                        <div class="panel-body">

                                            <div class="col-sm-7">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading" style="font-weight: bold">
                                                        Data Entry
                                                    </div>

                                                    <div class="panel-body">

                                                        <div>
                                                            <div class="row">

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Route Code
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtlocrutcd" Enabled="false" runat="server" TabIndex="18" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtlocrutcd_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnlocrcode" Visible="false" runat="server" TabIndex="19" OnClick="lbtnlocrcode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Company
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtcompany" AutoPostBack="true" OnTextChanged="txtcompany_TextChanged" runat="server" TabIndex="20" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="LinkButton4" runat="server" TabIndex="21" OnClick="LinkButton4_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Location
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtloc" AutoPostBack="true" OnTextChanged="txtloc_TextChanged" TabIndex="22" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                         <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="LinkButton3" runat="server" TabIndex="23" OnClick="LinkButton3_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Active
                                                                        </div>

                                                                        <div class="col-sm-9 paddingRight5">
                                                                            <asp:CheckBox ID="chkshowroom" TabIndex="24" runat="server" Checked="true" />
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

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Distance (KM)
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtdisware" runat="server" TabIndex="25"  CssClass="validateDecimal form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            <asp:LinkButton ID="LinkButton7" CausesValidation="false" TabIndex="26" CssClass="floatRight" runat="server" OnClick="LinkButton7_Click">
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

                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-5">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading" style="font-weight: bold">
                                                        Details
                                                    </div>

                                                    <div class="panel-body">

                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll">

                                                                    <asp:GridView ID="gvshowroooms" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Route">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrouteahowroom" runat="server" Text='<%# Bind("frs_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Line" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblline" runat="server" Text='<%# Bind("frs_line") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcompany" runat="server" Text='<%# Bind("frs_com_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblloccc" runat="server" Text='<%# Bind("frs_loc_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField >
                                                                                <HeaderTemplate>
                                                                                      <asp:Label ID="lblShDistHdr" Text="Distance (KM)" runat="server" />
                                                                                  </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldistanceee" runat="server" Text='<%#  (Convert.ToDecimal( Eval("frs_distance"))).ToString("N2") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkactshowroom_CheckedChanged" ID="chkactshowroom" runat="server" Checked='<%#Convert.ToBoolean(Eval("frs_act")) %>' />
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

                                

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" Enabled="True" TargetControlID="Button3"
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
   
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                PopupControlID="pnlpopupExcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

 
    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary ">

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">
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

                            <div class="col-sm-6">
                                <div class="row">

                                    <div class="col-sm-4 labelText1">
                                        File Name
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="fileupexcelupload1" runat="server" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        <asp:RadioButtonList ID="rbHDR" runat="server" RepeatDirection="Horizontal" Enabled="false" Visible="false">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                    <div class="col-sm-8">
                                        <asp:LinkButton ID="lbtnuploadexcel" runat="server" OnClick="lbtnuploadexcel_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true">Upload</span>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>

                            <div class="col-sm-12">
                                <asp:Label runat="server" ID="lblwarning" CssClass="form-control" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </div>

                            <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>

                               <asp:UpdatePanel runat="server">
        <ContentTemplate>
                            <asp:Panel runat="server" ID="pnlpopupExcelss" Visible="false">

                                <div class="col-sm-12">
                                    <asp:Label runat="server" ID="lblsuccess" CssClass="form-control" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>

                                <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>

                                <div class="col-sm-12">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading">
                                                        Details
                                                    </div>

                                                    <div class="panel-body">

                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll">

                                                                    <asp:GridView ID="gvroutes" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Route">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblexrt" runat="server" Text='<%# Bind("frh_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Route Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblexrtdesc" runat="server" Text='<%# Bind("frh_desc") %>' Width="150px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Route Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblexrttyp" runat="server" Text='<%# Bind("frh_cat") %>' Width="150px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Active">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox OnCheckedChanged="chkschactexcel_CheckedChanged" AutoPostBack="true" ID="chkschactexcel" runat="server" Checked='<%#Convert.ToBoolean(Eval("frh_act")) %>' />
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
                        <div class="col-sm-12 height5">
                        </div>
                    </div>

                                 <div class="col-sm-12">

                                       <asp:LinkButton ID="LinkButton9" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="LinkButton9_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Save
                                        </asp:LinkButton>

                                     </div>

                            </asp:Panel>

             </ContentTemplate>
    </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlexcel" CancelControlID="btnClose_excel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlexcel">
                <div runat="server" id="dv" class="panel panel-default height45 width700 ">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose_excel" runat="server" >
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <strong>Excel Upload</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height10">
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlupload" Visible="true">
                                        <div class="col-sm-12" id="Div6" runat="server">
                                            <div class="col-sm-12 paddingRight5">



                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="ddlexcelTyp" runat="server" Width="200px">
                                                            <asp:ListItem Text="Select type" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Warehouse" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Location" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4 labelText1">
                                                        <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                                    </div>
                                                    <div class="col-sm-2 paddingRight5">
                                                        <asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" Visible="false" />
                                                        <asp:Button ID="btnupload" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height30">
                                                    <asp:Label ID="lblErrorMessage" runat="server" Text="Please select the routing type!!!" Visible="false" ForeColor="Red"  /> 
                                                </div>
                                            </div>



                                        </div>
                                    </asp:Panel>

                                    <%--<div class="row">--%>
                                    <%-- <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAsyncUpload"
                EventName="Click" />
            <asp:PostBackTrigger ControlID="btnupload" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload2" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="Panel1" CancelControlID="btnClose_excel2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="Panel1">
                <div runat="server" class="panel panel-default height45 width700 ">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose_excel2" runat="server" >
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <strong>Excel Upload</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height10">
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Panel runat="server" ID="Panel2" Visible="true">
                                        <div class="col-sm-12" id="Div3" runat="server">
                                            <div class="col-sm-8 paddingRight5">



                                                <div class="row">
                                                    <div class="col-sm-7 labelText1">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    </div>
                                                    <div class="col-sm-2 paddingRight5">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnAsyncUpload2" runat="server" Text="Async_Upload" Visible="false" />
                                                        <asp:Button ID="btnupload2" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnUpload2_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAsyncUpload2"
                EventName="Click" />
            <asp:PostBackTrigger ControlID="btnupload2" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div5" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg4" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="lbtnYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="lbtnYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="lbtNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="lbtNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>  

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupUpdateRate" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlUpdateRate" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlUpdateRate" runat="server" align="center">
        <div runat="server" id="Div4" class="panel panel-info height120 width250">
            <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label9" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>  
    <script>
        Sys.Application.add_load(func);
        function func() {
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
     }
    </script>
</asp:Content>
