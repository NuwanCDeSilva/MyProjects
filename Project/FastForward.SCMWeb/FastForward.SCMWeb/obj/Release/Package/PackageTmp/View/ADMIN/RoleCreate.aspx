<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="RoleCreate.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.RoleCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>
<%@ Register Src="~/UserControls/ucProfitCenterSearch.ascx" TagPrefix="uc1" TagName="ucProfitCenterSearch" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<%--    <script src="../../Js/jquery-1.4.2.js"></script>
    <script src="../../Js/jquery-ui-1.8.1.js"></script>--%>

    <style type="text/css">
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

        function ConfirmMessage() {
            var selectedvalue = confirm("Do you want to save?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
            }
        };

        function ConfirmMessage2() {
            var selectedvalue = confirm("Do you want to save?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue2.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue2.ClientID %>').value = "No";
            }
        };

        function ConfirmMessageUpdateRoleDetails() {
            var selectedvalueRole = confirm("Do you want to update role details?");
            if (selectedvalueRole) {
                document.getElementById('<%=confirmrolesave.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=confirmrolesave.ClientID %>').value = "No";
            }
        };

        function ConfirmMessageNewRoleDetails() {
            var selectedvalueNewRole = confirm("Are you sure to create?");
            if (selectedvalueNewRole) {
                document.getElementById('<%=confirmnewrole.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=confirmnewrole.ClientID %>').value = "No";
            }
        };

        function ConfirmDeletion() {
            var selectedvalueNewRole = confirm("Are you sure you want to delete ?");
            if (selectedvalueNewRole) {
                document.getElementById('<%=txtconfirmdeletion.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdeletion.ClientID %>').value = "No";
            }
        };

        var popup;
        function SelectName(url, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            popup = window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            popup.focus();
        };

        function doClick(buttonName, e) {
            var key;

            if (window.event)
                key = window.event.keyCode;
            else
                key = e.which;

            if (key == 13) {
                var btn = document.getElementById(buttonName);
                if (btn != null) {
                    btn.click();
                    event.keyCode = 0
                }
            }
        };

        function ConfirmClearForm() {
            var selectedvalueRole = confirm("Do you want to clear all details ?");
            if (selectedvalueRole) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmClearGrid() {
            var selectedvalueRole = confirm("Do you want to clear all details ?");
            if (selectedvalueRole) {
                document.getElementById('<%=txtconfirmcleargrids.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmcleargrids.ClientID %>').value = "No";
            }
        };

    </script>



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

<%--    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="../../Js/jquery-1.7.2.min.js"></script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="panel panel-default marginLeftRight5" style="height:515px">
        <div class="panel-body">
             <div class="panel panel-default" style="height:495px" >

                 <asp:HiddenField ID="txtconformmessageValue" runat="server" />
                 <asp:HiddenField ID="txtconformmessageValue2" runat="server" />
                 <asp:HiddenField ID="confirmrolesave" runat="server" />
                 <asp:HiddenField ID="confirmnewrole" runat="server" />
                 <asp:HiddenField ID="txtconfirmclear" runat="server" />
                 <asp:HiddenField ID="txtconfirmcleargrids" runat="server" />
                 <asp:HiddenField ID="txtconfirmdeletion" runat="server" />

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

<%--    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel4">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <input type="text" id="txtName" name="Name" readonly="readonly" style="display: none" />

                 <div class="col-sm-6  buttonRow"  style="position:absolute; right:0px; width:400px">
                    <div class="col-sm-5">
                    </div>
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" Visible="false" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-save" aria-hidden="true"></span>AddNew/Update
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-2">
                        <asp:LinkButton ID="lbtnSubmit" CausesValidation="false" Visible="false" TabIndex="5" runat="server" CssClass="floatRight" >
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Submit
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-2">
                        <asp:LinkButton ID="lbtnClear" runat="server" CausesValidation="false" TabIndex="6" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="btnClear_Click" >
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                        </asp:LinkButton>
                    </div>
                </div>


            <br />
            <br />

        <ul id="myTab" class="nav nav-tabs">

                        <li class="active">
                            <a href="#RoleCreation" data-toggle="tab">Role Creation</a>
                        </li>

                        <li onclick="document.getElementById('<%= lbltree.ClientID %>').click();">
                            <a href="#GrantPrivileges" data-toggle="tab">Grant Privileges</a>
                        </li>

                        <li>
                            <a href="#ViewRoleUsers" data-toggle="tab">View Role Users</a>
                        </li>

                        <li>
                            <a href="#GrantRoleOptions" data-toggle="tab">Grant Role Options</a>
                        </li>

                        <li>
                            <a href="#AssignLocations" data-toggle="tab">Assign Locations</a>
                        </li>

                        <li>
                            <a href="#AssignPC" data-toggle="tab">Assign PC</a>
                        </li>

                    </ul>

        <div id="myTabContent" class="tab-content">

                        <div class="tab-pane fade in active" id="RoleCreation">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button17" runat="server" Text="" BackColor="White" BorderColor="White" BorderStyle="None" OnClick="Button17_Click" Width="1px" Height="1px" />

                        <div class="row">

                            <div class="col-sm-3">

                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="divscrole" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblscrole" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" OnClick="LinkButton3_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divfailrole" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblfailrole" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="false" OnClick="LinkButton9_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                           </div>

                            <div class="col-sm-3">

                                

                            </div>

                            <div class="col-sm-3">

                               

                            </div>
                            <div class="col-sm-3" style="margin-left:-18px">

                                 <asp:LinkButton ID="lbtnNewRole" runat="server" Visible="false" CausesValidation="false" TabIndex="7" CssClass="floatRight" OnClientClick="ConfirmMessageNewRoleDetails();" OnClick="btnNewRole_Click">
                                        <span class="glyphicon glyphicon-save" aria-hidden="true" style="font-size:20px"></span>AddNew
                                </asp:LinkButton>

                                <asp:LinkButton ID="lbtnRoleSave" CausesValidation="false" TabIndex="8" runat="server" OnClick="btnRoleSave_Click" CssClass="floatRight" OnClientClick="ConfirmMessageUpdateRoleDetails();">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>Update
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

                        <div class="col-sm-12" style="margin-left:-15px">

                        <div class="panel panel-default"style=" margin-left:8px; height:318px;">

                            <div class="panel-heading">
                                Role Details
                            </div>

                            <div class="panel-body">

                                <div>

                                    <div class="row">

                                        <div class="col-sm-12">

                                            <div class="col-sm-6">

                                                <div class="panel panel-default" style="margin-left: 8px;">
                                                    <div class="panel-heading">
                                                    </div>
                                                    <div class="panel-body">



                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Company
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" AutoPostBack="true"  TabIndex="1" class="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div> 

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                   Role ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                      <asp:TextBox ID="txtRoleID" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchRoleNew" runat="server" TabIndex="2"  OnClick="btnSearchRoleNew_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Role Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtRoleName" runat="server" TabIndex="3" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Create New
                                                </div>
                                                <div class="col-sm-2 paddingRight0 height22">
                                                    <asp:CheckBox ID="chkNewRole" runat="server" AutoPostBack="true" TabIndex="4" OnCheckedChanged="chkNewRole_CheckedChanged"/>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Role Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtRoleDesc" runat="server" TabIndex="5" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Active
                                                </div>
                                                <div class="col-sm-2 paddingRight0 height22">
                                                    <asp:CheckBox ID="chkIsActiveRole" runat="server" AutoPostBack="true"  TabIndex="6" />
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

                                            <div class="col-sm-6">
                                            <div class="panel panel-default" >
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:230px";>
                                                    
                                                    <asp:GridView ID="grvRoleVeiwDet" runat="server" CssClass="table table-hover table-striped">

                                                        <EmptyDataTemplate>
                                                            <table class="table table-hover table-striped"  border="1" style="border-collapse: collapse;" rules="all">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="col">
                                                                            Company
                                                                        </th>
                                                                        <th scope="col">
                                                                            Role Id
                                                                        </th>
                                                                        <th scope="col">
                                                                            Description
                                                                        </th>
                                                                        <th scope="col">
                                                                            Created By
                                                                        </th>
                                                                        <th scope="col">
                                                                            Modified By
                                                                        </th>
                                                                        <th scope="col">
                                                                            Modified Date
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >No records found.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        
                                                        <Columns>
                                                            <asp:BoundField DataField="CompanyCode" HeaderText="Company" SortExpression="CompanyCode" />
                                                            <asp:BoundField DataField="RoleId" HeaderText="Role Id" SortExpression="RoleId"></asp:BoundField>
                                                            <asp:BoundField DataField="RoleName" HeaderText="Role Name" SortExpression="RoleName" Visible="false" />
                                                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                                            <asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="IsActive" Visible="false" />
                                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" Visible="false" />
                                                            <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" SortExpression="ModifiedBy" />
                                                            <asp:BoundField DataField="ModifyedDate" HeaderText="Modified Date" SortExpression="ModifyedDate" DataFormatString="{0:dd-MMM-yyyy}" />
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

                        <div class="tab-pane fade" id="GrantPrivileges">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="row">

                            <div class="col-sm-3">

                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="divscgrant" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblscgrant" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton11" runat="server" CausesValidation="false" OnClick="LinkButton11_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divfailgrant" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblfailgrant" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton12" runat="server" CausesValidation="false" OnClick="LinkButton12_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                           </div>

                            <div class="col-sm-3">

                            </div>

                            <div class="col-sm-3">

                               

                            </div>
                              <div class="col-sm-3" style="margin-left:-18px">
                                <asp:LinkButton ID="lbltree" CausesValidation="false"  OnClick="lbltree_Click" runat="server" CssClass="floatRight">
                                        <%--<span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>--%>
                                </asp:LinkButton>

                                 <asp:LinkButton ID="lbtnGrant" CausesValidation="false" TabIndex="10" OnClientClick="ConfirmMessage();" OnClick="btnGrant_Click" runat="server" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>Grant
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

                        <div class="col-sm-12" style="margin-left:-15px">

                        <div class="panel panel-default"style="margin-left:8px; height:340px; ">

                            <div class="panel-heading">
                                Role Details
                            </div>

                            <div class="panel-body">

                               <div>

                                    <div class="row">

                                        <div class="col-sm-12">

                                         <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:250px";>
                                                <asp:TreeView ID="treeView1" runat="server" ShowCheckBoxes="All" ShowLines="true" Height="100px" Width="100px" ExpandDepth="0" EnableClientScript="false" ></asp:TreeView>
                                   
                                                    </div>
                                                </div>
                                             </div>

                                         <div class="col-sm-6">

                                         <div class="panel panel-default"style=" height:273px;">
                                             <div class="panel-heading">
                                             </div>
                                             <div class="panel-body">

                                             <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                Company
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlCompayGrant" class="form-control" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlCompayGrant_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>

                                             <div class="row">
                                                 <div class="col-sm-12 height5">
                                                 </div>
                                             </div>

                                             <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                   Role ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                      <asp:TextBox ID="txtRoleIDGrant" runat="server"  ReadOnly="true" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchRoleGrant" runat="server"  OnClick="btnSearchRoleGrant_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                             <div class="row">
                                                 <div class="col-sm-12 height5">
                                                 </div>
                                             </div>

                                             <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                     Role Name
                                                 </div>
                                                  <div class="col-sm-9">
                                                     <asp:TextBox ID="txtRoleNameGrant" runat="server" class="form-control"></asp:TextBox>
                                                     <br />
                                                 </div>
                                             </div>

                                             <div class="row">
                                                 <div class="col-sm-12 height5">
                                                 </div>
                                             </div>

                                             <div class="row">
                                                 <div class="col-sm-3 labelText1">
                                                     Role Description
                                                 </div>
                                                 <div class="col-sm-9">
                                                     <asp:TextBox ID="txtRoleDescGrant" runat="server" class="form-control"></asp:TextBox>
                                                     <br />
                                                 </div>
                                             </div>

                                             <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                             <div class="row">
                                                 <div class="col-sm-3 labelText1">
                                                     Active
                                                 </div>
                                                 <div class="col-sm-2 paddingRight0 height22">
                                                     <asp:CheckBox ID="chkActRoleGrant" runat="server" AutoPostBack="true" Enabled="false"/>
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
                                     
                              </div>
                        </div>

                        </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div>

                        <div class="tab-pane fade" id="ViewRoleUsers">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>

                        <div class="row">

                            <div class="col-sm-3">

                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="dvscvwroleusers" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblscvwroleusr" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" OnClick="LinkButton4_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="dvfailroleusr" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblfailvwroleusers" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="false" OnClick="LinkButton5_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                           </div>

                            <div class="col-sm-3">

                                

                            </div>

                            <div class="col-sm-3">

                               

                            </div>
                            <div class="col-sm-3" style="margin-left:-18px">

                               <asp:LinkButton ID="lbtnClearViewGrid" runat="server" CausesValidation="false" OnClientClick="ConfirmClearGrid();" CssClass="floatRight" OnClick="btnClearViewGrid_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"  style="font-size:20px"></span>Clear
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

                        <div class="col-sm-12" style="margin-left:-15px">

                        <div class="panel panel-default"style="margin-left:8px; height:330px;">

                            <div class="panel-heading">
                                Users/Menus for Role
                            </div>

                            <div class="panel-body">
                               <div>
                                    <div class="row">

                                        <div class="col-sm-12">

                                    <div class="col-sm-4">
                   <div class="panel panel-default">
                            <div class="panel-heading">
                                
                            </div>

                            <div class="panel-body">
                               <div>


                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Company
                                </div>
                                 <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlComView" runat="server" AutoPostBack="true" Width="100%" class="form-control" OnSelectedIndexChanged="ddlComView_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Role ID
                                </div>
                                <div class="col-sm-8 paddingRight5">
                                     <asp:TextBox ID="txtRoleIDview" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="lbtnRoleIDView" runat="server"  OnClick="btnRoleIDView_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                              <div class="col-sm-3 labelText1">
                                    Role Description
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtViewRoleDesc" runat="server" class="form-control"></asp:TextBox>
                                    <br />

                                        <asp:LinkButton ID="lImgBtnViewBD" runat="server" OnClick="ImgBtnViewBD_Click">Search
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>
                                                    </asp:LinkButton>
                                </div>
                            </div>
                               </div>
                            </div>
                   </div>
                   </div>

                                    <div class="col-sm-4">
                                            <div class="panel panel-default" >
                                                <div class="panel-heading">
                                                    Users For Role
                                                </div>
                                                <div class="panel-body panelscoll" style="height:210px";>
                                       
                                    <asp:GridView ID="grvUserRole" runat="server" CssClass="table table-hover table-striped">

                                        <EmptyDataTemplate>
                                            <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                <tbody>
                                                    <tr>
                                                        <th scope="col">Company
                                                        </th>
                                                        <th scope="col">User Id
                                                        </th>
                                                        <th scope="col">User Name
                                                        </th>
                                                        <th scope="col">Description
                                                        </th>
                                                        <th scope="col">Mobile
                                                        </th>
                                                        <th scope="col">Phone
                                                        </th>
                                                        <th scope="col">Domain ID
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>No records found.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                            </table>
                                        </EmptyDataTemplate>

                                        <Columns>
                                            <asp:BoundField DataField="ROWNUM" HeaderText="" SortExpression="ROWNUM" />
                                            <asp:BoundField DataField="serm_com_cd" HeaderText="Company" SortExpression="serm_com_cd" />
                                            <asp:BoundField DataField="serm_role_id" HeaderText="Role ID" SortExpression="serm_role_id" Visible="false" />
                                            <asp:BoundField DataField="serm_usr_id" HeaderText="User ID" SortExpression="serm_usr_id" />
                                            <asp:BoundField DataField="se_usr_name" HeaderText="User Name" SortExpression="se_usr_name" />
                                            <asp:BoundField DataField="se_usr_desc" HeaderText="Description" SortExpression="se_usr_desc" />
                                            <asp:BoundField DataField="se_email" HeaderText="E Mail" SortExpression="se_email" Visible="false" />
                                            <asp:BoundField DataField="se_mob" HeaderText="Mobile" SortExpression="se_mob" />
                                            <asp:BoundField DataField="se_phone" HeaderText="Phone" SortExpression="se_phone" />
                                            <asp:BoundField DataField="se_domain_id" HeaderText="Domain ID" SortExpression="se_domain_id" />
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                                </div>
                                        </div>

                                    <div class="col-sm-4">
                                            <div class="panel panel-default" margin-left:100px">
                                                <div class="panel-heading">
                                                    Menus For Role
                                                </div>
                                                <div class="panel-body panelscoll" style="height:210px";>
                                 
                                       
                                    <asp:GridView ID="grvViewRoleMenus" runat="server" CssClass="table table-hover table-striped">

                                        <EmptyDataTemplate>
                                            <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                <tbody>
                                                    <tr>
                                                        <th scope="col">Company
                                                        </th>
                                                        <th scope="col">Role Id
                                                        </th>
                                                        <th scope="col">Role Name
                                                        </th>
                                                        <th scope="col">Menu Name
                                                        </th>
                                                        <th scope="col">Menu Description
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>No records found.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                            </table>
                                        </EmptyDataTemplate>

                                        <Columns>
                                            <asp:BoundField DataField="ROWNUM" HeaderText="" SortExpression="ROWNUM" />
                                            <asp:BoundField DataField="COMPANY" HeaderText="Company" SortExpression="COMPANY" />
                                            <asp:BoundField DataField="ROLE_ID" HeaderText="Role ID" SortExpression="ROLE_ID" />
                                            <asp:BoundField DataField="ROLE_NAME" HeaderText="Role Name" SortExpression="ROLE_NAME" />
                                            <asp:BoundField DataField="MENU_NAME" HeaderText="Menu Name" SortExpression="MENU_NAME" />
                                            <asp:BoundField DataField="MENU_DESC" HeaderText="Menu Description" SortExpression="MENU_DESC" />
                                            <asp:BoundField DataField="MENU_ID" HeaderText="Menu ID" SortExpression="MENU_ID" Visible="false" />
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

                        <div class="tab-pane fade" id="GrantRoleOptions">
                             <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                         

                        <div class="row">

                            <div class="col-sm-3">

                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="dvscgrantroleopt" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblscgrantroleopt" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" OnClick="LinkButton6_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="dvfailgrntroleopt" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblfailgrntroleopt" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" OnClick="LinkButton7_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                           </div>

                            <div class="col-sm-3">

                                

                            </div>

                            <div class="col-sm-3">

                               

                            </div>
                            <div class="col-sm-3" style="margin-left:-18px">

                                    <asp:LinkButton ID="lbtnSaveRoleOpt" CausesValidation="false" TabIndex="5" runat="server" OnClientClick="ConfirmMessage2();" OnClick="btnSaveRoleOpt_Click"  CssClass="floatRight">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>Save
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

                        <div class="col-sm-12" style="margin-left:-15px">

                        <div class="panel panel-default"style="margin-left:8px; height:325px; ">

                            <div class="panel-heading">
                                Role Details/Options
                            </div>

                            <div class="panel-body">
                               <div>
                                    <div class="row">

                                        <div class="col-sm-12">

                                         <div class="col-sm-6">

                         <div class="panel panel-default"style=" margin-left:8px; height:250px;">
                            <div class="panel-heading">
                               
                            </div>
                            <div class="panel-body">


                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Company
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlCompanyRoleOpt" class="form-control" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlCompanyRoleOpt_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Role ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtRoleID_opt" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                     <asp:LinkButton ID="lbtnSearchRoleIDopt" runat="server" ImageUrl="~/images/icons/searchicon.png" OnClick="btnSearchRoleIDopt_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Role Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtRoleName_opt" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Role Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtRoleDesc_opt" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="row">
                                                 <div class="col-sm-3 labelText1">
                                                     Active
                                                 </div>
                                                 <div class="col-sm-2 paddingRight0 height22">
                                                     <asp:CheckBox ID="chkActRole_opt" runat="server" AutoPostBack="true"/>
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

                                            <div style="clear: both;">
                                                <div style="float: left; width: 49%;">
                                                    <asp:Button ID="btnGetOptForRole" runat="server" Font-Size="11px" Text="View Role Options" OnClick="btnGetOptForRole_Click" CssClass="btn btn-default" />
                                                </div>
                                            </div>
                                        </div>


                                                    </div>
                                            </div>

                                         <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:230px";>

                                                <asp:GridView ID="grvGrpOpt" runat="server" CssClass="table table-hover table-striped">
                                                    
                                                    <EmptyDataTemplate>
                                                        <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                            <tbody>
                                                                <tr>
                                                                    <th scope="col">Option ID
                                                                    </th>
                                                                    <th scope="col">Title
                                                                    </th>
                                                                    <th scope="col">Description
                                                                    </th>
                                                                    <th scope="col">Created By
                                                                    </th>
                                                                    <th scope="col">Created Date
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>No records found.
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                        </table>
                                                    </EmptyDataTemplate>

                                                    <Columns>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"  />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRow" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="ssp_optid" HeaderText="Option ID" SortExpression="ssp_optid" ItemStyle-Width="80" />
                                                        <asp:BoundField DataField="ssp_title" HeaderText="Title" SortExpression="ssp_title" />
                                                        <asp:BoundField DataField="ssp_desc" HeaderText="Description" SortExpression="ssp_desc" />
                                                        <asp:BoundField DataField="ssp_cre_by" HeaderText="Created By" SortExpression="ssp_cre_by" ItemStyle-Width="125" />
                                                        <asp:BoundField DataField="ssp_cre_dt" HeaderText="Created Date" SortExpression="ssp_cre_dt" DataFormatString="{0:dd-MMM-yyyy}" />
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

                        <div class="tab-pane fade" id="AssignLocations">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>


                        <div class="row">

                            <div class="col-sm-3">

                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="dvscassloc" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblscassloc" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="false" OnClick="LinkButton8_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="dvfaileassloc" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblassfailloc" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton10" runat="server" CausesValidation="false" OnClick="LinkButton10_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                           </div>

                            <div class="col-sm-3">

                                

                            </div>

                            <div class="col-sm-3">

                               

                            </div>
                            <div class="col-sm-3" style="margin-left:-18px">

                                 

                            </div>
                        </div>

                        <div class="col-sm-12" style="margin-left:-15px">
                        <div class="panel panel-default"style=" margin-left:15px; height:385px;">

                            <div class="panel-heading">
                                Assign Location/Channel
                            </div>

                            <div class="panel-body">
                               <div>


                                   <div class="row">
                                       <div class="col-md-8">



                                <div class="row">

                                    <div class="col-sm-6">

                                        <div class="col-sm-3 labelText1">
                                            Company
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlCompanyLoc" class="form-control" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlCompanyLoc_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="col-sm-6">

                                        <div class="col-sm-3 labelText1">
                                            Role ID
                                        </div>
                                        <div class="col-sm-8 paddingRight4">
                                            <asp:TextBox ID="txtRoleIDLoc" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="btnRoleLoc" runat="server" OnClick="btnRoleLoc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                   <div class="col-sm-12">

                                   <asp:Menu
                                       ID="Menu1"
                                       Width="168px"
                                       runat="server"
                                       Orientation="Horizontal"
                                       StaticEnableDefaultPopOutImage="False"
                                       OnMenuItemClick="Menu1_MenuItemClick"  StaticSelectedStyle-BackColor="#cccccc" Font-Bold="True" Font-Size="14px" >
                                       <Items>
                                           <asp:MenuItem  
                                               Text="Location" Value="0">
                                           </asp:MenuItem>
                                           <asp:MenuItem 
                                               Text="Channel" Value="1"></asp:MenuItem>
                                       </Items>
                                   </asp:Menu>

                                   <asp:MultiView
                                       ID="MultiView1"
                                       runat="server"
                                       ActiveViewIndex="0">

                                   <asp:View ID="Tab1" runat="server">
                                           
                                           <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                           <asp:UpdatePanel ID="sub1a" runat="server">
                                               <ContentTemplate>

                                                  

                                       <div class="col-sm-12" style="margin-left:-30px">

                                       <div class="col-sm-4" style="height:245px;">
                                           <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                                       </div>

                                       <div class="col-sm-1" style="margin-left:-57px" >
                                            <asp:LinkButton ID="lbtnAddLocs" runat="server" OnClick="btnAddLocs_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                       </div>

                                       <div class="col-sm-3">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body height230">

                                                    <div class="height300 panelscoll2">
                                                        <asp:GridView ID="grvLocs" runat="server" CssClass="table table-hover table-striped">

                                                            <EmptyDataTemplate>
                                                                <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th scope="col">Code
                                                                            </th>
                                                                            <th scope="col">Description
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>No records found.
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                </table>
                                                            </EmptyDataTemplate>

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRow" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="LOCATION" HeaderText="Code" ItemStyle-Width="80" SortExpression="LOCATION" />
                                                                <asp:BoundField DataField="LOC_DESCRIPTION" HeaderText="Description" SortExpression="LOC_DESCRIPTION" />
                                                                <asp:BoundField DataField="SEL_COM_CD" HeaderText="Company" SortExpression="SEL_COM_CD" Visible="false" />
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>

                                                </div>
                                            </div>
                                       </div>

                                       <div class="col-sm-3">

                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body height230">

                                                    <div class="height300 panelscoll2">
                                                        <asp:GridView ID="gvRoleLoc" runat="server" CssClass="table table-hover table-striped">

                                                            <EmptyDataTemplate>
                                                                <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th scope="col">Role ID
                                                                            </th>
                                                                            <th scope="col">Company Code
                                                                            </th>
                                                                            <th scope="col">Location
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>No records found.
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                </table>
                                                            </EmptyDataTemplate>

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRow" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="SSRL_ROLEID" HeaderText="Role ID" ItemStyle-Width="80" SortExpression="SSRL_ROLEID" />
                                                                <asp:BoundField DataField="SSRL_COM" HeaderText="Company Code" SortExpression="SSRL_COM" />
                                                                <asp:BoundField DataField="SSRL_LOC" HeaderText="Location" SortExpression="SSRL_LOC" />
                                                                <asp:BoundField DataField="SEL_DEF_LOCCD" HeaderText="Is Default" SortExpression="SEL_DEF_LOCCD" Visible="false" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>


                                                </div>

                                       </div>

                                       <div class="col-sm-1 rolecreationaddbtnassign">

                                                <asp:LinkButton ID="lbtnAddLoc" runat="server" OnClick="lbtnAddLoc_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Add New
                                                   </asp:LinkButton>

                                                </div>

                                       <div class="col-sm-1 rolecreationdelbuttonassign">

                                                <asp:LinkButton ID="lbtnDelRoleLoc" runat="server" OnClientClick="ConfirmDeletion();" OnClick="lbtnDelRoleLoc_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"  style="font-size:20px"></span>Delete
                                                   </asp:LinkButton>
</div>

                                       </div>

                                               </ContentTemplate>
                                           </asp:UpdatePanel>
                                            
                                       </asp:View>

                                   <asp:View ID="Tab2" runat="server">
                                           
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                           <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                               <ContentTemplate>

                                               
                                      <div class="col-sm-12" style="margin-left:-15px">

                                       <div class="col-sm-4">
                                            <div class="panel panel-default" style="margin-left:-14px">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:225px";>

                                    
                                           <asp:GridView ID="grvParty" runat="server" CssClass="table table-hover table-striped">

                                               <EmptyDataTemplate>
                                                   <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                       <tbody>
                                                           <tr>
                                                               <th scope="col">Code
                                                               </th>
                                                               <th scope="col">Description
                                                               </th>
                                                           </tr>
                                                           <tr>
                                                               <td>No records found.
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                           </tr>
                                                   </table>
                                               </EmptyDataTemplate>
                                               
                                                <Columns>
                                                   <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" />
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:CheckBox ID="chkRow" runat="server"  />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>

                                                   <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-Width="80" SortExpression="Code" />
                                                   <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                               </Columns>

                                           </asp:GridView>

                                                    </div>
                                                </div>
                                           </div>

                                       <div class="col-sm-1">
                                           <asp:LinkButton ID="lbtnAddPartys" runat="server" OnClick="btnAddPartys_Click" CssClass="floatRight" >
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                       </div>

                                       <div class="col-sm-5">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:225px";>
                                     
                                           <asp:GridView ID="grvRoleChnl" runat="server" CssClass="table table-hover table-striped">
                                               
                                               <EmptyDataTemplate>
                                                   <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                       <tbody>
                                                           <tr>
                                                               <th scope="col">Role ID
                                                               </th>
                                                               <th scope="col">Company Code
                                                               </th>
                                                               <th scope="col">Channel
                                                               </th>
                                                           </tr>
                                                           <tr>
                                                               <td>No records found.
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                           </tr>
                                                   </table>
                                               </EmptyDataTemplate>

                                               <Columns>
                                                    <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"/>
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:CheckBox ID="chkRow" runat="server"  />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>

                                                   <asp:BoundField DataField="SSRLC_ROLEID" HeaderText="Role ID" ItemStyle-Width="80" SortExpression="SSRLC_ROLEID" />
                                                   <asp:BoundField DataField="SSRLC_COM" HeaderText="Company Code" SortExpression="SSRLC_COM" />
                                                   <asp:BoundField DataField="SSRLC_CHNNL" HeaderText="Channel" SortExpression="SSRLC_CHNNL" />
                                                   <asp:BoundField DataField="SEL_DEF_LOCCD" HeaderText="Is Default" SortExpression="SEL_DEF_LOCCD" Visible="false" />
                                               </Columns>
                                           </asp:GridView>
                                                    </div>
                                                </div>
                                             </div>

                                       <div class="col-sm-1">
                                           <div class="col-md-1" style="margin-left:140px">
                                               <asp:LinkButton ID="lbtnAddLocChnl" runat="server" OnClick="lbtnAddLocChnl_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"  style="font-size:20px" ></span>Add New
                                           </asp:LinkButton>
                                           </div>
                                           </div>

                                       <div class="col-sm-1">
                                           <div class="col-md-1">
                                               <asp:LinkButton ID="lbtnDelLocChnl" runat="server" OnClientClick="ConfirmDeletion();" OnClick="lbtnDelLocChnl_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"  style="font-size:20px"></span>Delete
                                           </asp:LinkButton>
                                           </div>

                                       </div>

                                </div>

                                               </ContentTemplate>
                                           </asp:UpdatePanel>

                                       </asp:View>

                                   </asp:MultiView>

                                   </div>

                               </div>
                            </div>

                         </div>
                       </div>

                   </ContentTemplate>
                </asp:UpdatePanel>
                            </div>

                        <div class="tab-pane fade" id="AssignPC">

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                  
                        <div class="row">

                            <div class="col-sm-3">

                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="dvsucpc" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblsucpc" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton13" runat="server" CausesValidation="false" OnClick="LinkButton13_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="dvfailpc" style="width:500px">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblfailpc" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton14" runat="server" CausesValidation="false" OnClick="LinkButton14_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                           </div>

                            <div class="col-sm-3">

                                

                            </div>

                            <div class="col-sm-3">

                               

                            </div>
                            <div class="col-sm-3" style="margin-left:-18px">

                            </div>
                        </div>

                         <div class="col-sm-12" style="margin-left:-15px">
                         <div class="panel panel-default"style=" margin-left:15px; height:382px;">
                            <div class="panel-heading">
                                Assign (PC) Profit Center/Channel
                            </div>
                            <div class="panel-body">
                               <div>


                                   <div class="row">

                                       <div class="col-sm-5">
                                                <div class="col-sm-3 labelText1">
                                                    Company
                                                </div>
                                                <div class="col-sm-7">
                                                     <asp:DropDownList ID="ddlCompanyPC" runat="server" class="form-control" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlCompanyPC_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                       </div>

                                       <div class="col-sm-5">
                                            <div class="col-sm-3 labelText1">
                                                    Role ID
                                                </div>
                                                <div class="col-sm-6 paddingRight5">
                                                     <asp:TextBox ID="txtRoleIDPC" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                     <asp:LinkButton ID="imgbutton4" runat="server"  OnClick="imgbutton4_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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

                                    <div class="col-sm-12">

                                   <div id="pc">
                                       <asp:Menu
                                           ID="Menu2"
                                           Width="168px"
                                           runat="server"
                                           Orientation="Horizontal"
                                           StaticEnableDefaultPopOutImage="False"
                                           OnMenuItemClick="Menu2_MenuItemClick" StaticSelectedStyle-BackColor="#cccccc" Font-Bold="True" Font-Size="14px">
                                           <Items>
                                               <asp:MenuItem 
                                                   Text="Profit Center" Value="0"></asp:MenuItem>
                                               <asp:MenuItem 
                                                   Text="Location" Value="1"></asp:MenuItem>
                                           </Items>
                                       </asp:Menu>
                                   </div>

                                   <asp:MultiView
                                       ID="MultiView2"
                                       runat="server"
                                       ActiveViewIndex="0">

                                       <asp:View ID="View1" runat="server">
                                          
                                           <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                           <asp:UpdatePanel ID="sub23" runat="server">
                                               <ContentTemplate>

                                         <div class="col-sm-12" style="margin-left:-30px">

                                         <div class="col-sm-4">
                                           <uc1:ucProfitCenterSearch runat="server" ID="ucProfitCenterSearch" />
                                       </div>

                                         <div class="col-sm-1" style="margin-left:-64px">
                                            <asp:LinkButton ID="btnAddPC" runat="server" OnClick="btnAddPC_Click" CssClass="floatRight" >
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                       </div>

                                         <div class="col-sm-3">
                                            <div class="panel panel-default height250" style=" margin-left:-15px;">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll height230">
                                    
                                            <asp:GridView ID="grvPCs" runat="server" CssClass="table table-hover table-striped">

                                                <EmptyDataTemplate>
                                                    <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                        <tbody>
                                                            <tr>
                                                                <th scope="col">Code
                                                                </th>
                                                                <th scope="col">Description
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>No records found.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                    </table>
                                                </EmptyDataTemplate>

                                                <Columns>
                                                    <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"/>
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:CheckBox ID="chkRow" runat="server"  />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>

                                                    <asp:BoundField DataField="PROFIT_CENTER" HeaderText="Code" SortExpression="PROFIT_CENTER" ItemStyle-Width="80" />
                                                    <asp:BoundField DataField="PC_DESCRIPTION" HeaderText="Description" SortExpression="PC_DESCRIPTION" />
                                                    <asp:BoundField DataField="SEL_COM_CD" HeaderText="Company" SortExpression="SEL_COM_CD" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                                    </div>
                                                </div>
                                             </div>

                                         <div class="col-sm-3" style="margin-left:-18px">
                                            <div class="panel panel-default" style=" height:254px;">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:225px";>

                                     
                                           <asp:GridView ID="grvRolePC" runat="server" CssClass="table table-hover table-striped">

                                               <EmptyDataTemplate>
                                                   <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                       <tbody>
                                                           <tr>
                                                               <th scope="col">Role ID
                                                               </th>
                                                               <th scope="col">Company Code
                                                               </th>
                                                                <th scope="col">Profit Center
                                                               </th>
                                                           </tr>
                                                           <tr>
                                                               <td>No records found.
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                           </tr>
                                                   </table>
                                               </EmptyDataTemplate>

                                               <Columns>
                                                   <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"/>
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:CheckBox ID="chkRow" runat="server"  />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>

                                                   <asp:BoundField DataField="SSRP_ROLEID" HeaderText="Role ID" SortExpression="SSRP_ROLEID" ItemStyle-Width="80" />
                                                   <asp:BoundField DataField="SSRP_COM" HeaderText="Company Code" SortExpression="SSRP_COM" />
                                                   <asp:BoundField DataField="SSRP_PC" HeaderText="Profit Center" SortExpression="SSRP_PC" />
                                                   <asp:BoundField DataField="SEL_DEF_LOCCD" HeaderText="Is Default" SortExpression="SEL_DEF_LOCCD" Visible="false" />
                                               </Columns>
                                           </asp:GridView>
                                            </div>
                                                </div>
                                            </div>

                                         <div class="col-sm-1" style="margin-top:-55px">

                                                 <asp:LinkButton ID="btnAddRolePC" runat="server" OnClick="btnAddRolePC_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"  style="font-size:20px"></span>Add New
                                                 </asp:LinkButton>
                                             </div>

                                         <div class="col-sm-1 rolecreationdelbuttonassignPC">

                                                 <asp:LinkButton ID="btnDelRolePc" runat="server" OnClientClick="ConfirmDeletion();" OnClick="btnDelRolePc_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"  style="font-size:20px"></span>Delete
                                                 </asp:LinkButton>

                                             </div>

                                         </div>

                                               </ContentTemplate>
                                           </asp:UpdatePanel>
                                            
                                       </asp:View>

                                       <asp:View ID="View2" runat="server">
                                           
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                           <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                               <ContentTemplate>

                                                 <div class="col-sm-12" style="margin-left:-30px">

                                                 <div class="col-sm-4">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:225px";>
                                      
                                             <asp:GridView ID="grvParty2" runat="server" CssClass="table table-hover table-striped">
                                                 
                                                  <EmptyDataTemplate>
                                                   <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                       <tbody>
                                                           <tr>
                                                               <th scope="col">Code
                                                               </th>
                                                               <th scope="col">Description
                                                               </th>
                                                           </tr>
                                                           <tr>
                                                               <td>No records found.
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                           </tr>
                                                   </table>
                                               </EmptyDataTemplate>

                                                 <Columns>
                                                      <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"/>
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:CheckBox ID="chkRow" runat="server"  />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>

                                                     <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" ItemStyle-Width="80" />
                                                     <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                                 </Columns>
                                             </asp:GridView>
                                                    </div>
                                                    </div>
                                           </div>

                                                 <div class="col-sm-1">
                                           <asp:LinkButton ID="btnAddPartys2" runat="server" OnClick="btnAddPartys2_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                       </div>

                                                 <div class="col-sm-5">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll" style="height:225px";>

                                           <asp:GridView ID="grvRolePCChnl" runat="server" CssClass="table table-hover table-striped">
                                               
                                               <EmptyDataTemplate>
                                                   <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                       <tbody>
                                                           <tr>
                                                               <th scope="col">Role ID
                                                               </th>
                                                               <th scope="col">Company Code
                                                               </th>
                                                               <th scope="col">Channel
                                                               </th>
                                                           </tr>
                                                           <tr>
                                                               <td>No records found.
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                           </tr>
                                                   </table>
                                               </EmptyDataTemplate>

                                               <Columns>
                                                   <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"/>
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:CheckBox ID="chkRow" runat="server"  />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>

                                                   <asp:BoundField DataField="SSRPC_ROLEID" HeaderText="Role ID" SortExpression="SSRPC_ROLEID" ItemStyle-Width="80" />
                                                   <asp:BoundField DataField="SSRPC_COM" HeaderText="Company Code" SortExpression="SSRPC_COM" />
                                                   <asp:BoundField DataField="SSRPC_CHNNL" HeaderText="Channel" SortExpression="SSRPC_CHNNL" />
                                                   <asp:BoundField DataField="SEL_DEF_LOCCD" HeaderText="Is Default" SortExpression="SEL_DEF_LOCCD" Visible="false" />
                                               </Columns>
                                           </asp:GridView>
                                                     </div>
                                                 </div>
                                       </div>

                                                 <div class="col-sm-1">
                                                 
                                                             <asp:LinkButton ID="btnAddPCchanel" runat="server" OnClick="btnAddPCchanel_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"  style="font-size:20px"></span>Add New
                                                             </asp:LinkButton>
                                                 </div>

                                                 <div class="col-sm-1">
                                               <asp:LinkButton ID="btnDelPCChnl" runat="server" OnClientClick="ConfirmDeletion();" OnClick="btnDelPCChnl_Click" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"  style="font-size:20px"></span>Delete
                                           </asp:LinkButton>
                                           </div>

                                                </div>

                                   </div>

                                               </ContentTemplate>
                                           </asp:UpdatePanel>

                                       </asp:View>

                                   </asp:MultiView>

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
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

       <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="ImageSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="True">
                                            <asp:ListItem>ID</asp:ListItem>
                                            <asp:ListItem>DESCRIPTION</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>


                            <div class="col-sm-4 paddingRight5">
                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged" ></asp:TextBox>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="ImageSearch" runat="server" OnClick="ImageSearch_Click" >
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
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dvResultUser" CausesValidation="false" runat="server"  AllowPaging="True"  GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvResultUser_PageIndexChanging" OnSelectedIndexChanged="dvResultUser_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                        runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Label ID="lblWait2" runat="server"
                                    Text="Please wait... " />
                                <asp:Image ID="imgWait2" runat="server"
                                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                            </div>
                        </ProgressTemplate>

                    </asp:UpdateProgress>
                </div>
            </div>
        </div>
    </asp:Panel>
            </ContentTemplate>
           </asp:UpdatePanel>
    
</asp:Content>
