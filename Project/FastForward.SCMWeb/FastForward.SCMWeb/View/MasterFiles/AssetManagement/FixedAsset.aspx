<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="FixedAsset.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.AssetManagement.FixedAsset" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 panel panel-default">
            <div class="panel-body">
                <div class="row buttonRow" id="HederBtn">
                    <div class="col-sm-12 col-md-12">
                        <div class="col-md-6">
                            <h1 style="font-size: small; margin-top: 2px">Fixed Asset</h1>
                        </div>

                        <div class="col-md-1 ">
                            <%--   <asp:LinkButton ID="btnDownloadfile"  runat="server" CssClass="floatRight" OnClick="btnDownloadfile_Click"> 
                                <span class="glyphicon glyphicon-download" aria-hidden="true"></span>Download </asp:LinkButton>--%>
                            <%-- <asp:Button ID="btnDownloadfile" Text="Download" OnClick="btnDownloadfile_Click" runat="server" />--%>
                        </div>

                        <div class="col-md-1 paddingRight0">
                            <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnView_Click"> 
                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>Send request </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click"> 
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Cancel request </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click1"> 
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Cancel request </asp:LinkButton>
                        </div>





                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="col-md-1">
                            Transaction type
                        </div>
                        <div class="col-md-1">
                            <asp:DropDownList runat="server" ID="ddlReuestType" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            Action
                        </div>
                        <div class="col-md-1">
                            <asp:DropDownList runat="server" ID="ddlAction" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            Send Location
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="sendlocation" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtsendlocation" CausesValidation="false" runat="server" OnClick="lbtnSeDocType_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-1">
                            Ref.No
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="refno" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <%--<asp:LinkButton ID="lbtnrefno" CausesValidation="false" runat="server" OnClick="lbtnrefno_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                            </asp:LinkButton>--%>
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="lbtnrefno_Click">
                                    <span class="glyphicon glyphicon-search"></span>
                            </asp:LinkButton>

                            <%--                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
                                    <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button5"
                                        PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                                    </asp:ModalPopupExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>

        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>

            <div runat="server" style="width: 427px">
                <asp:Button ID="Button3" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

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

                <asp:Panel runat="server" DefaultButton="ImgSearch">
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
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%--onkeydown="return (event.keyCode!=13);"--%>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:TextBox ID="txtSearchbyword" placeholder="Search by word" CausesValidation="false" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-2 paddingLeft0">
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="dvResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dvResult_PageIndexChanging" OnSelectedIndexChanged="dvResult_SelectedIndexChanged">
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
                </asp:Panel>


            </div>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-sm-12 col-md-12 panel panel-default ">
            <div class="panel-heading ">
                <strong>Request Item Details</strong>
            </div>

        </div>
    </div>
</asp:Content>
