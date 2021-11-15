<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucProfitCenterSearch.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucProfitCenterSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />

<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>Profit Center</b>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12  buttonrow">
                        <div id="errorDiv" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                            <strong>Warning!</strong>
                            <asp:Label ID="lblWarn" runat="server"></asp:Label>
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
                        <asp:TextBox runat="server" ID="txtCompany" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCompany_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnCompany" CausesValidation="false" runat="server" OnClick="imgbtnCompany_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtCompanyDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtChannel" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtChannel_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnChannel" CausesValidation="false" runat="server" OnClick="imgbtnChannel_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtChannelDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtSubChannel" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtSubChannel_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnSubChannel" CausesValidation="false" runat="server" OnClick="imgbtnSubChannel_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtSubChannelDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtArea" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtArea_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnArea" CausesValidation="false" runat="server" OnClick="imgbtnArea_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtAreaDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtRegion" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtRegion_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnRegion" CausesValidation="false" runat="server" OnClick="imgbtnRegion_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtRegionDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtZone" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtZone_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnZone" CausesValidation="false" runat="server" OnClick="imgbtnZone_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtZoneDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 labelText1">
                        ProfitCenter
                    </div>
                    <div class="col-sm-3 paddingRight5">
                        <asp:TextBox runat="server" ID="txtProfitCenter" Style="text-transform: uppercase" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtProfitCenter_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 paddingLeft0">
                        <asp:LinkButton ID="imgbtnProfitCenter" CausesValidation="false" runat="server" OnClick="imgbtnProfitCenter_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-4 paddingRight0">
                        <asp:TextBox ID="txtProfitCenterDes" AutoPostBack="false" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
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

                        <asp:GridView ID="dvResult" CausesValidation="false" runat="server" OnSelectedIndexChanged="dvResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="dvResult_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
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
