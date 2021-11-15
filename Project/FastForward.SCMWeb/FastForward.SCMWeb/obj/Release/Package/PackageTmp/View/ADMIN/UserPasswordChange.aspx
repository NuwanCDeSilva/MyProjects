<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="UserPasswordChange.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.UserPasswordChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function ConfirmMessage() {
            var selectedvalue = confirm("Are you sure to change password?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
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

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

<asp:HiddenField ID="txtconformmessageValue" runat="server" />
<asp:HiddenField ID="txtconfirmclear" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">

                <div class="col-sm-12">
                <div visible="false" class="alert alert-success" role="alert" runat="server" id="DivAsk">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Well done!</strong>
                        <asp:Label ID="lblAsk" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtColse" runat="server" CausesValidation="false" OnClick="lbtColse_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>

                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="Div1">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Alert!</strong>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="LinkButton2_Click" CssClass="floatright">
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
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" OnClick="LinkButton3_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                </div>

            </div>

            <div class="panel panel-default marginLeftRight5">

                <div class="col-sm-12">

                
                    <div class="col-sm-4">
                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" Visible="false" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-save" aria-hidden="true"></span>AddNew/Update
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:LinkButton ID="lbtnSubmit" CausesValidation="false" TabIndex="5" runat="server" CssClass="floatRight" OnClientClick="ConfirmMessage();" OnClick="btnSubmit_Click">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Submit
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:LinkButton ID="lbtnClear" runat="server" CausesValidation="false" TabIndex="6" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:20px"></span>Clear
                        </asp:LinkButton>
                    </div>
               

                    </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>

                    <div class="panel panel-default">

                        <div class="panel-heading">
                            Change Password
                        </div>

                        <div class="panel-body">
                            <div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 12%;">
                                        Username
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtNewUserName" Width="175px" runat="server" TabIndex="1" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div style="clear: both;">
                                    <div style="float: left; width: 12%;">
                                        Current Password
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtCurrentPassword" runat="server" Width="175px" TabIndex="2" TextMode="Password" class="form-control" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div style="clear: both;">
                                    <div style="float: left; width: 12%;">
                                        New Password
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtNewPassword" Width="174px" runat="server" TabIndex="3" TextMode="Password" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div style="clear: both;">
                                    <div style="float: left; width: 12%;">
                                        Confirm Password
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" Width="175px" TabIndex="4" TextMode="Password" class="form-control" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
