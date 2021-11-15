<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLoactionSearch.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucLoactionSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />

<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />
<style>
    .panel{
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
    }
</style>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default">
            <asp:Panel runat="server" ID="pnlHeading">
            <div class="panel-heading">
                <b><asp:Label ID="lblFormName" Text="Location Details" runat="server" Visible="true"></asp:Label></b>
            </div>
                </asp:Panel>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12  buttonrow">
                        <div id="errorDiv" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert" style="padding-right:0px;">
                            <div class="col-sm-10 padding0">
                                <strong>Warning!</strong>
                                <asp:Label ID="lblWarn" runat="server"></asp:Label>
                               
                            </div>
                            <div class="col-sm-1">
                                 <asp:LinkButton ID="lBtnAlertDanger" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lBtnAlertDanger_Click">
                                  <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div id="successDiv" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                            <div class="alert alert-success">
                                <strong>Success!</strong>
                                <asp:Label ID="Label9" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <div class="col-sm-3 labelText1">
                        Company
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxCompany" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TextBoxCompany_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="ImgBtnAccountNo" CausesValidation="false" runat="server" OnClick="ImgBtnAccountNo_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxCompanyDes" AutoPostBack="true" ReadOnly="true" runat="server" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">

                    <div class="col-sm-3 labelText1">
                        Channel
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxChannel" CausesValidation="false" AutoPostBack="true" Style="text-transform: uppercase" CssClass="form-control" OnTextChanged="TextBoxChannel_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgChaSearch" CausesValidation="false" runat="server" OnClick="imgChaSearch_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxChannelDes" AutoPostBack="true" ReadOnly="true" runat="server" CssClass="form-control" OnTextChanged="TextBoxChannelDes_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 labelText1">
                        Sub Channel
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxSubChannel" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TextBoxSubChannel_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgSubChaSearch" CausesValidation="false" runat="server" OnClick="imgSubChaSearch_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxSubChannelDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 labelText1">
                        Area
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxArea" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TextBoxArea_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgAreaSearch" CausesValidation="false" runat="server" OnClick="imgAreaSearch_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxAreaDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 labelText1">
                        Region
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxRegion" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TextBoxRegion_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgRegionSearch" CausesValidation="false" runat="server" OnClick="imgRegionSearch_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxRegionDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 labelText1">
                        Zone
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxZone" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TextBoxZone_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgZoneSearch" CausesValidation="false" runat="server" OnClick="imgZoneSearch_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxZoneDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 labelText1">
                        <asp:Label ID="lblLocation" Text="Location" runat="server" />
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="TextBoxLocation" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="TextBoxLocation_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgProCeSearch" CausesValidation="false" runat="server" OnClick="imgProCeSearch_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="TextBoxLocationDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>







<div runat="server" style="width: 427px">
    <asp:Button ID="Button3" runat="server" Text="" Style="display: none;" />
</div>
<asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
    PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>



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


                        <%--onkeydown="return (event.keyCode!=13);"--%>
                        <div class="col-sm-4 paddingRight5">
                            <asp:TextBox ID="txtSearchbyword" placeholder="Search by word" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                        </div>

                        <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="ImgSearch" runat="server" OnClick="ImgSearch_Click">
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

                        <asp:GridView ID="dvResult" CausesValidation="false" runat="server" OnSelectedIndexChanged="dvResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="dvResult_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager">
                            <Columns>
                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                            </Columns>
                            <HeaderStyle Width="10px" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>




<%-- Style="display: none"--%>
