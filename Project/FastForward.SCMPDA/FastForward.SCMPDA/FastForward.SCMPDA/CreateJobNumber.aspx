<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="CreateJobNumber.aspx.cs" Inherits="FastForward.SCMPDA.CreateJobNumber" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

        function HideLabelAuto() {
            var seconds = 3;
            setTimeout(function () {
                document.getElementById("<%=divokjob.ClientID %>").style.display = "none";
            }, seconds * 1000);
            };

            function scrollTop() {
                $('body').animate({ scrollTop: 0 }, 500);
            };
            function confirmationMsg() {
                var selectedvalue = confirm("Do you want to process Document?");
                if (selectedvalue) {
                    return true;
                } else {
                    return false;
                }
            };
    </script>

    <style type="text/css">
        .uppercase {
            text-transform: uppercase;
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
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="processaod">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/Css/Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div1" class="panel panel-info height120 width250">
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
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="Button8" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnPrint_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="Button9" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button17" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PoConfBox" runat="server" Enabled="True" TargetControlID="Button17"
                PopupControlID="ppConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel45">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitloc" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitloc" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="ppConfBox" runat="server" align="center">
        <div runat="server" id="Div23" class="panel panel-info height120 width250">
            <asp:Label ID="Label17" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy24" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                <ContentTemplate>
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <span>Alert</span>
                        </div>
                        <div class="panel-body">
                              <div class="row">
                                <div class="col-sm-12">
                                    <strong>
                                        <asp:Label ID="lblerror" Text="" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <strong>
                                        <asp:Label ID="lbldataMsg" Text="" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="Button18" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnOkGrn_Click" />
                                </div>
                                <div class="col-sm-2 ">
                                    <asp:Button ID="Button19" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnNoGrn_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

            <asp:Panel ID="defpnl" runat="server" DefaultButton="btnok">
                 <div class="col_sm-12" runat="server" id="dvcreatejobnumber">

                    <%--<div class="row">
                        <div class="col-sm-12">

                            <div visible="false" class="alert alert-success" role="alert" runat="server" id="divokjob">

                                <div class="col-sm-12">
                                    <asp:Label ID="lblokjob" runat="server"></asp:Label>
                                </div>

                            </div>
                         
                            <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">

                                <div class="col-sm-12">
                                    <asp:Label ID="lblalert" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnalert" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnalert_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>

                            </div>

                            <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtninfo" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtninfo_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                         
                        </div>
                    </div>--%>

                    <div class="panel panel-default mainpnlmargin">
                        <div class="panel-heading defaultpanelheader">
                            Create Job Number

                            <asp:Button ID="btnback" runat="server" TabIndex="17" CssClass="btn-info form-control remove-margin" Text="Back" OnClick="btnback_Click" />
                                                
                            <asp:Button ID="btnfinish" runat="server" TabIndex="18" CssClass="btn-info form-control remove-margin" Text="Finish" OnClick="btnfinish_Click" />
                            <asp:UpdatePanel ID="processaod" runat="server">
                                <ContentTemplate>
                                   <asp:Button ID="btnProcess" runat="server" TabIndex="18" CssClass="btn-info form-control" Text="Process"  OnClientClick="return confirmationMsg();" OnClick="btnProcess_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="printdoc" runat="server">
                                <ContentTemplate>
                                  <asp:Button ID="btnPrint" runat="server" TabIndex="18" CssClass="btn-info form-control" Text="Print"  OnClientClick="return confirmationPrintMsg();" OnClick="btnPrint_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="panel-body scn-jobs-view">
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12  set-height">
                                        <div class="row doc-vew-dv">

                                            <div class="labelText1 label-chk-all">
                                                <span>
                                                    Job Number
                                                </span>
                                            </div>

                                            <div class="label-chk-rgh labelText1 ">
                                                <asp:Label runat="server" ID="lbljobno" CssClass="ControlText" ForeColor="#A513D0"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Dulaj 2018/Oct-15 --%>
                                          <div class="col-sm-12  set-height">
                                        <div class="row doc-vew-dv"  runat="server" id="qrDiv">

                                            <div class="labelText1 label-chk-all">
                                                <span>
                                                    QR
                                                </span>
                                            </div>
                                             <asp:CheckBox runat="server" ID="CheckBoxQR" AutoPostBack="true" />
                                           
                                        </div>
                                    </div>
                                                      <div class="col-sm-12  set-height">
                                        <div class="row doc-vew-dv" runat="server" id="qrDivDrop">

                                            <div class="labelText1 label-chk-all">
                                                <span>
                                                    Company
                                                </span>
                                            </div>
                                             <asp:DropDownList ID="DropDownListQRCom" CausesValidation="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Hero" Value="1" ></asp:ListItem>
                                                       <asp:ListItem Text="TVS" Value="2" ></asp:ListItem>
                                                   </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--  --%>
                                    <div class="col-md-12  set-height">
                                        <div class="row scn-bth">
                                            <span class="badge">Item Scan Qty : 
                                                <asp:Label ID="itmScnQty" runat="server" CssClass="lblItmScnQty">0</asp:Label>
                                             </span>-
                                            <span class="badge">
                                                Item Doc Qty : 
                                                <asp:Label ID="itmDocQty" runat="server" CssClass="lblDocScnQty">0</asp:Label>
                                            </span>-
                                            <span class="badge">
                                                Bin Scan Qty : 
                                                <asp:Label ID="binScnQty" runat="server" CssClass="lblBinScnQty">0</asp:Label>
                                            </span>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-12  set-height">
                                        <div class="row">
                                            <div class="set-in-row">

                                                 <asp:Panel ID="itmCdPnl" runat="server" DefaultButton="btItmCdClk">
                                                    
                                                     <div class="labelText1  lbltxt-set">
                                                    Item Code 
                                                </div>

                                            <%--    <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>

                                            
                                                <asp:TextBox ID="txtitemcode" runat="server" AutoPostBack="false" TabIndex="1" CssClass="form-control txt-wdth-set ControlText uppercase" OnTextChanged="txtitemcode_TextChanged" ></asp:TextBox><%--OnTextChanged="txtitemcode_TextChanged"--%>
                                            
                                                     
                                                     <asp:Button ID="btItmCdClk" runat="server" CssClass="btn-info hide form-control" Text="OK" OnClick="btItmCdClk_Click" />
                                                </asp:Panel>

                                                <%--</div>

                                            <div class="">--%>
                                                <asp:CheckBox ID="chkallitems" TabIndex="2" runat="server" Text="All" CssClass="optioncontrols" AutoPostBack="true" OnCheckedChanged="chkallitems_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>

                                    <%--      <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                    <div class="col-sm-12  set-height" style="display:none" >
                                        <div class="row">
                                            <div class="set-in-row">
                                                <div class="labelText1  lbltxt-set">
                                                    Bin Code
                                                </div>
                                                    <asp:DropDownList ID="ddlbincode" runat="server" TabIndex="3" AutoPostBack="true" CssClass="form-control ControlText" OnSelectedIndexChanged="ddlbincode_SelectedIndexChanged"></asp:DropDownList>
                                            
                                                    <asp:CheckBox ID="chkallbin" TabIndex="4" runat="server" Text="All" CssClass="optioncontrols" AutoPostBack="true" OnCheckedChanged="chkallbin_CheckedChanged" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-12  set-height">
                                        <div class="row">
                                            <div class="set-in-row">
                                                <div class="labelText1  lbltxt-set">
                                                    Bin Code
                                                </div>
                                                        <asp:TextBox ID="txtBinCode" runat="server" AutoPostBack="false" TabIndex="1" CssClass="form-control txt-wdth-set ControlText" OnTextChanged="txtBinCode_TextChanged" ></asp:TextBox><%--OnTextChanged="txtitemcode_TextChanged"--%>
                                                    
                                                 <asp:CheckBox ID="chkallbinAll" TabIndex="4" runat="server" Text="All" CssClass="optioncontrols" AutoPostBack="true" OnCheckedChanged="chkallbin_CheckedChanged" />
                                            </div>

                                        </div>
                                    </div>
                                    <%--                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                    <div class="col-sm-12  set-height">
                                        <div class="row">
                                            <div class="set-in-row">
                                                <div class="labelText1  lbltxt-set">
                                                    Item Status
                                                </div>
                                                <asp:DropDownList ID="ddlitmstatus" runat="server" TabIndex="5" AutoPostBack="true" CssClass="form-control ControlText drp-width-set" OnSelectedIndexChanged="ddlitmstatus_SelectedIndexChanged"></asp:DropDownList>
                                            
                                                <asp:CheckBox ID="chkallstatus" TabIndex="6" runat="server" Text="All" CssClass="optioncontrols" AutoPostBack="true" OnCheckedChanged="chkallstatus_CheckedChanged" />
                                            </div>

                                        </div>
                                    </div>

                                    <%--       <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                    <div id="serialdv" runat="server">

                                        <%--<div class="col-sm-12">
                                            <div class="row">
                                                <div class="set-in-row-only">
                                                    <div class="labelText1">
                                                        Bulk Serial
                                                    </div>
                                                    <asp:CheckBox ID="chkblkserial" TabIndex="7" runat="server" AutoPostBack="true" OnCheckedChanged="chkblkserial_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>--%>

                                        <%--    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>

                                        <div class="col-sm-12  set-height">
                                            <div class="row">
                                                <div class="set-in-row">
                                                    <div class="labelText1  lbltxt-set">
                                                        Serial #
                                                    </div>
                                                    <asp:TextBox ID="txtserialnumber1" runat="server" TabIndex="8" CssClass="form-control txt-wdth-set ControlText" OnTextChanged="txtSerial1_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>

                                        <div id="serial2dv" runat="server" class="col-sm-12  set-height">

                                            <%-- <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>

                                            <div class="col-sm-12">
                                                <div class="row">


                                                    <div class="set-in-row">
                                                    <div class="labelText1  lbltxt-set">
                                                        Serial #2
                                                    </div>
                                                  <asp:TextBox ID="txtserialnumber2" runat="server" TabIndex="9" CssClass="form-control txt-wdth-set ControlText"></asp:TextBox>

                                                </div>
                                                </div>
                                            </div>

                                        </div>

                                        <%--<div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>

                                        <div id="serial3dv" runat="server" class="col-sm-12  set-height">

                                            <%-- <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="set-in-row">
                                                    <div class="labelText1  lbltxt-set">
                                                        Serial #2
                                                    </div>
                                                  <asp:TextBox ID="txtserialnum3" runat="server" TabIndex="9" CssClass="form-control txt-wdth-set ControlText"></asp:TextBox>

                                                </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                    <div id="nonserdv" runat="server">

                                        <%--<div class="col-sm-12">
                                            <div class="row">
                                                 <div class="set-in-row-only">
                                                    <div class="labelText1">
                                                        Bulk Items
                                                     </div>
                                                    <asp:CheckBox ID="chkbulkitems" TabIndex="11" runat="server" AutoPostBack="true" OnCheckedChanged="chkbulkitems_CheckedChanged" />
                                                    
                                                </div>
                                            </div>
                                        </div>--%>

                                        <%--<div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>

                                        <div class="col-sm-12  set-height">
                                            <div class="row">
                                                 <div class="set-in-row-only">
                                                    <div class="labelText1  lbltxt-set">
                                                        Barcode
                                                    </div>
                                                    <asp:TextBox ID="txtbarcode" runat="server" TabIndex="12" CssClass="form-control txt-wdth-set ControlText"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div><br/>

                                        <%--<div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>
                                    </div>

                                    <%--<div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>

                                    <div class="col-sm-12  set-height">
	                                    <div class="row">
		                                    <div class="set-in-row">
			                                    <div class="labelText1  lbltxt-set">
				                                    Qty
			                                    </div>
			                                    <asp:TextBox ID="txtqty" runat="server" TabIndex="13" CssClass="form-control txt-wdth-set ControlText" onkeydown="return jsDecimals(event);"></asp:TextBox>
		                                    </div>
	                                    </div>
                                    </div><br/>
                                    <div id="entrynodv" runat="server" class="col-sm-12  set-height">
                                        <div class="col-sm-12">
	                                        <div class="row">

		                                        <div class="set-in-row">
		                                        <div class="labelText1  lbltxt-set">
			                                        Entry #
		                                        </div>
	                                          <asp:TextBox ID="txtEntryNo" runat="server" TabIndex="13" CssClass="form-control txt-wdth-set ControlText"></asp:TextBox>

	                                        </div>
	                                        </div>
                                        </div>
                                    </div>
                                    <%--     <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div visible="false" class="alert alert-success" role="alert" runat="server" id="divokjob">

                                                <div class="col-sm-12">
                                                    <asp:Label ID="lblokjob" runat="server"></asp:Label>
                                                </div>

                                            </div>

                                            <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">

                                                <div class="col-sm-12">
                                                    <asp:Label ID="lblalert" runat="server"></asp:Label>
                                                    <asp:LinkButton ID="lbtnalert" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnalert_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>

                                            <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                                    <asp:LinkButton ID="lbtninfo" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtninfo_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-12 set-height-sp">
                                        <div class="row">
                                            <div class="set-in-row-only">
                                            <div class="labelText1 with-set-def">
                                                Last Scanned Serial / Barcode
                                            </div>
                                                <asp:Label ID="lstscnserial" runat="server" CssClass="labelText1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <%--    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-12 set-height">
                                        <div class="row scn-bth">
                                            <span class="badge">Total Scan Qty : 
                                                <asp:Label ID="lblscqty" runat="server" CssClass="labelText1"></asp:Label>
                                             </span>
                                            <span class="badge">
                                                Total Doc Qty : 
                                                <asp:Label ID="lbldocqty" runat="server" CssClass="labelText1"></asp:Label>
                                            </span>
                                        </div>
                                            
                                        </div>
                                    <div class="col-sm-12 set-height">
                                        <div class="row">

                                            <div class="row-button">
                                                <asp:Button ID="btnok" runat="server" TabIndex="14" CssClass="btn-info form-control remove-margin" Text="OK" OnClick="btnok_Click" />
                                            
                                                <asp:Button ID="btkreset" runat="server" TabIndex="15" CssClass="btn-info form-control remove-margin" Text="Reset" OnClick="btkreset_Click" />
                                            
                                                <asp:Button ID="btncheck" runat="server" TabIndex="16" CssClass="btn-info form-control remove-margin" Text="Check" OnClick="btncheck_Click" />

                                                <asp:Button ID="btnitmcheck" runat="server" TabIndex="16" CssClass="btn-info form-control remove-margin" Text="Items" OnClick="btnitmcheck_Click" />
                                                
                                            </div>

                                           

                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddin-top" runat="server" id="itemlistdiv">
                                        <div class="row">
                                            <div class="col-sm-12 panelscoll itm-pnl">
                                            <asp:GridView ID="grdjobitems" runat="server"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                CssClass="table table-hover table-striped labelText1"
                                                GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No items found...">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Cd" ControlStyle-CssClass="code-width">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitm" runat="server" Text='<%# Bind("tui_req_itm_cd") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req Qty" ControlStyle-CssClass="reqqty-width">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("tui_req_itm_qty") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Scan Qty" ControlStyle-CssClass="scnqty-width">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblseq" runat="server" Text='<%# Bind("tui_pic_itm_qty") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ControlStyle-CssClass="stus-width">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstus" runat="server" Text='<%# Bind("tui_req_itm_stus") %>' Width="75px"></asp:Label>
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
