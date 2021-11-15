<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PasswordSecurityPolicy.aspx.cs" Inherits="FastForward.SCMWeb.PasswordSecurityPolicy" %>

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

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div visible="false" class="alert alert-success" role="alert" runat="server" id="DivAsk">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Well done!</strong>
                        <asp:Label ID="lblAsk" runat="server" Text="Sucessfully updated !!!"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtColse" runat="server" CausesValidation="false" OnClick="lbtColse_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel panel-default marginLeftRight5">
                <div class="panel-body">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Security Policy
                        </div>
                        <div class="panel-body">
                            <div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Maximum password age
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtmaxpwag" Width="70px" runat="server" TabIndex="1" class="form-control" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                        &nbsp;Days
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Minimum password age
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtminpwag" runat="server" Width="70px" TabIndex="2" class="form-control" onkeydown="return jsDecimals(event);" />
                                        &nbsp;Days
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Enforce password history
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtenfpwhis" Width="70px" runat="server" TabIndex="3" class="form-control" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                        &nbsp;Passwords remembered
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Minimum password length
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtminpwlen" runat="server" Width="70px" TabIndex="4" class="form-control" onkeydown="return jsDecimals(event);" />
                                        &nbsp;Characters
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Lock user after failed log in
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtlockfail" Width="70px" runat="server" TabIndex="5" class="form-control" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                        &nbsp;Attempts
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Number of consecutive identical characters allowed
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtchar" runat="server" Width="70px" TabIndex="6" class="form-control" onkeydown="return jsDecimals(event);" />
                                        &nbsp;Consecutive identical characters
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%;">
                                        Password must not match user name
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:CheckBox ID="chkpwusename" runat="server" TabIndex="7" Text="Disabled" AutoPostBack="True" OnCheckedChanged="chkpwusename_CheckedChanged" />
                                    </div>
                                </div>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%; height: 57px;">
                                        Passwords must meet complexity requirements
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:CheckBox ID="chkpwcomplex" runat="server" TabIndex="8" Text="Disabled" AutoPostBack="True" OnCheckedChanged="chkpwcomplex_CheckedChanged" />
                                    </div>
                                </div>

                                <%--    <div style="clear: both;">
                        <div style="float: left; width: 32%; height: 57px;">
                        </div>
                        <div style="float: left; width: 50%;">
                            <asp:CheckBox ID="chkworddict" runat="server" TabIndex="9" Text="Disabled" AutoPostBack="True" Visible="False" OnCheckedChanged="chkworddict_CheckedChanged" />
                        </div>
                    </div>--%>

                                <div style="clear: both;">
                                    <div style="float: left; width: 32%; height: 57px;">
                                        <asp:LinkButton ID="lbtnadd" runat="server" TabIndex="10" OnClick="btnadd_Click1" CssClass="glyphicon glyphicon-floppy-saved" />
                                    </div>
                                    <div style="float: left; width: 50%;">
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
