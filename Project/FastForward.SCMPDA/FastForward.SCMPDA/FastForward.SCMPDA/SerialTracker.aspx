<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PDAWeb.Master" CodeBehind="SerialTracker.aspx.cs" Inherits="FastForward.SCMPDA.SerialTracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function scrollTop() {
            $('body').animate({ scrollTop: 0 }, 500);
        };
        function HideLabelAuto() {
            var seconds = 3;
            setTimeout(function () {
                document.getElementById("<%=divalert.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
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
        #BodyContent_detailsdiv{
            background-color:#FF7474;
        }
        .tothed{
            background-color:#989898;
        }
        .spcol{
            background-color:#E5C3DD;
        }
        .lbltxt-set {
    float: left;
    padding-bottom: 3px;
    width: 75px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="defpnl" runat="server" DefaultButton="btnCheck">
                <div class="col-sm-12">
                    <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">
                        <div class="col-sm-12">
                            <asp:Label ID="lblalert" runat="server"></asp:Label>
                            <asp:LinkButton ID="lbtnalert" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnalert_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body scn-jobs-view">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12 set-height">
                                    <div class="row">
                                        <div class="set-in-row">
                                            <div class="labelText1 lbltxt-set">Serial #1</div>
                                            <asp:TextBox ID="txtserialnumber1" runat="server" TabIndex="8" CssClass="form-control txt-wdth-set ControlText"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div id="serial2dv" runat="server">

                                    <div class="col-sm-12 set-height">
                                        <div class="row">
                                            <div class="set-in-row">
                                                <div class="labelText1 lbltxt-set">Serial #2</div>
                                                <asp:TextBox ID="txtserialnumber2" runat="server" TabIndex="9" CssClass="form-control txt-wdth-set ControlText"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-sm-12  set-height">
                                <div class="row">
                                    <div class="row-button">
                                        <asp:Button ID="btnback" runat="server" TabIndex="14" CssClass="btn-info form-control remove-margin" Text="Back" OnClick="btnback_Click" />
                                        <asp:Button ID="btnCheck" runat="server" TabIndex="14" CssClass="btn-info form-control remove-margin" Text="Check" OnClick="btnCheck_Click" />
                                    </div>


                                </div>
                            </div>
                             <div id="detailsdiv" visible="false" runat="server">
                                  <asp:Label ID="invalSer" runat="server">-</asp:Label>
                             </div>
                            <div id="serdetailsdiv" visible="false" runat="server">
                                <div class="col-sm-12 paddin-top" runat="server" id="itemlistdiv">
                                    <div class="col-sm-12">
                                        <div  class="col-sm-12 set-height tothed" >
                                            <asp:Label ID="lblTle" runat="server">Serial Details</asp:Label>
                                        </div>
                                        <div class="col-sm-12 set-height">
                                            <div class="row spcol">
                                                <div class="set-in-row">
                                                    <div class="labelText1 lbltxt-set">Item Code :</div>
                                                    <asp:Label ID="lblItemCd" runat="server">-</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 set-height">
                                            <div class="row">
                                                <div class="set-in-row">
                                                    <div class="labelText1 lbltxt-set">Location :</div>
                                                        <asp:Label ID="lblLocation" runat="server">-</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 set-height">
                                            <div class="row spcol">
                                                <div class="set-in-row">
                                                    <div class="labelText1 lbltxt-set">Desc :</div>
                                                        <asp:Label ID="lblDesc" runat="server">-</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 set-height">
                                            <div class="row">
                                                <div class="set-in-row">
                                                    <div class="labelText1 lbltxt-set">Serial #1 :</div>
                                                        <asp:Label ID="lblSer1" runat="server">-</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 set-height">
                                            <div class="row spcol">
                                                <div class="set-in-row ">
                                                    <div class="labelText1 lbltxt-set">Serial #2 :</div>
                                                        <asp:Label ID="lblSer2" runat="server">-</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 set-height">
                                            <div class="row">
                                                <div class="set-in-row">
                                                    <div class="labelText1 lbltxt-set">Scan Status :</div>
                                                        <asp:Label ID="lblScnStus" runat="server">-</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
