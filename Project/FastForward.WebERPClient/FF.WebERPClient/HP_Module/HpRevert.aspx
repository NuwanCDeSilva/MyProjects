<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HpRevert.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HpRevert" EnableEventValidation="false" %>
<%@ Register src="../UserControls/uc_HpAccountSummary.ascx" tagname="uc_HpAccountSummary" tagprefix="AS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript"  >

    function clickButton(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {
            if (evt.keyCode == 113) {
                bt.click();
                return false;
            }

        }
    }

    function ToUpper(ctrl) {
        var t = ctrl.value;
        ctrl.value = t.toUpperCase();
    }

    function ToLower(ctrl) {
        var t = ctrl.value;
        ctrl.value = t.toLowerCase();
    }

    function CheckProfitCenter(pcenter) {

        var _pvalue = document.getElementById(pcenter);
        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.CheckProfitCenter(_pvalue.value, onProfitCheckPass, onProfitCheckFail, pcenter);
    }

    function onProfitCheckPass(result, destCtrl) {
        var _pvalue = document.getElementById(destCtrl);
    
        if (result == null) {
            alert('Invalid profit center');
           
            _pvalue.value = '';
            return;
        }
        window.location = 'HpRevert.aspx?pc=' + result.Mpc_cd;
      }
    function onProfitCheckFail(error, destCtrl) {

        alert('Invalid profit center');
        var _pvalue = document.getElementById(destCtrl);
        var _phide = document.getElementById('hdnProfitCenter');
     _pvalue.value = '';
     _phide.value = '';
    }

    function CheckPopUserQty() {
        var hdnSystemQty = document.getElementById('<%=hdnSystemQty.ClientID%>');
        var txtPopQty = document.getElementById('<%=txtPopQty.ClientID%>');
        var btnPopUpSer = document.getElementById('<%=btnPopUpSer.ClientID%>');

        if (txtPopQty.value == '') { btnPopUpSer.click(); return };

        if (parseFloat(txtPopQty.value) > parseFloat(hdnSystemQty.value)) {
            txtPopQty.value = hdnSystemQty.value;
            alert('Select qty and the delivered qty is mismatch');
            btnPopUpSer.click();
            return;
        }
        SelectAuto();
        btnPopUpSer.click();
    }

        function SelectAuto() {
            var text = document.getElementById('<%= txtPopQty.ClientID %>');
            var btnPopUpSer = document.getElementById('<%=btnPopUpSer.ClientID%>');
            var val = text.value;
            var len;
            var myform = document.forms[0];
            if (val != null && val != "") {
                len = val;
            }
            else {
                len = 0;
                btnPopUpSer.click();
                return;
            }

            var Elen = myform.elements.length;
            var counter = 0;

            for (var i = 0; i < Elen; i++) {
                if (myform.elements[i].checked) {
                    myform.elements[i].checked = false;
                }
            }

            for (var i = 0; i < Elen; i++) {
                if (myform.elements[i].type == 'checkbox') {
                    if (myform.elements[i].checked) {
                        myform.elements[i].checked = false;
                    }
                    else {
                        myform.elements[i].checked = true;
                    }

                    counter++;
                    if (counter == len) {
                        btnPopUpSer.click();
                        return;
                    }
                }
            }
            btnPopUpSer.click();
       }
</script>

<asp:UpdatePanel runat="server" ID="pnlUdtRevert">
<ContentTemplate >
<div style=" float:left; width:100%; color:Black;" >

    <%--Button Area--%>
    <div class="PanelHeader invheadersize">
        <asp:Button ID="btnProcess" runat="server" Text="Process"  CssClass="Button invbtn"  OnClick="Process" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Button invbtn" OnClick="Clear" />
        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button invbtn" OnClick="Close" />
    </div>

    <%--Top Criteriai--%>
    <div class="hprvt1" >
        <div class="div1pcelt">&nbsp;</div>
        <div class="div5pcelt">Date </div>
        <div class="div10pcelt"><asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="70%" Enabled="false"></asp:TextBox>&nbsp;<asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"  ImageAlign="Middle" Visible="false" /></div>

        <div class="div2pcelt">&nbsp;</div>
        <div class="div8pcelt">Profit Center </div>
        <div class="div15pcelt"> <asp:TextBox ID="txtProfitCenter" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>  &nbsp;<asp:ImageButton ID="ImgBtnProfitCenter" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="ImgBtnPC_Click" /></div>

        <div class="div2pcelt" >&nbsp;</div>
        <div class="div8pcelt" >Account No</div>
        <div class="div15pcelt"> <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>  &nbsp;<asp:ImageButton ID="ImgBtnAccountNo" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="ImgAccountSearch_Click" /></div>
        <div class="div1pcelt" >&nbsp;</div>
        <div class="div10pcelt" ><asp:Label ID="lblAccountNo" runat="server" ></asp:Label> </div>
        
        <div class="div1pcelt" >&nbsp;</div>
        <div class="div10pcelt" >Create Date</div>
        <div class="div10pcelt" ><asp:Label ID="lblAccountDate" runat="server" ></asp:Label> </div>
    </div>

    <div class="hprvt1" >
        <div class="hprvt2" >
            <%--Customer Detail--%>
            <div class="hprvt1" >
                <div class="PanelHeader invcollapsovrid" > Customer Detail </div> 
                <div class="hprvt1"> 
                        <div class="div1pcelt"> &nbsp; </div>
                        <div class="div10pcelt"> Code </div><div class="div2pcelt">:</div>
                        <div class="hprvt3"><asp:Label ID="lblACode" runat="server" ></asp:Label> </div>

                        <div class="div10pcelt"> &nbsp; </div>
                        <div class="div10pcelt"> Name </div><div class="div2pcelt">:</div>
                        <div class="hprvt4"><asp:Label ID="lblAName" runat="server" ></asp:Label> </div>
                </div>

                <div class="hprvt5"> 
                        <div class="div1pcelt"> &nbsp; </div>
                        <div class="div10pcelt"> Address </div><div class="div2pcelt">:</div>
                        <div class="hprvt6"><asp:Label ID="lblAAddress1" runat="server" ></asp:Label> </div>
                </div>

             </div>
            <%--Trade Detail--%>
            <div class="hprvt1" >
                <div class="PanelHeader invcollapsovrid" > Trade Detail </div>
                <div class="hprvt7"> 
                    <asp:Panel ID="pnlTrade" runat="server" ScrollBars="Auto" Height="100px" >
                        <asp:GridView runat="server" ID="gvATradeItem" AutoGenerateColumns="False"  
                            CssClass="GridView" RowStyle-Wrap ="false"
                            CellPadding="3" ForeColor="#333333" GridLines="Both" 
                            DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no,Sad_itm_stus" 
                            ShowHeaderWhenEmpty="True" OnRowDataBound="AccountItem_OnRowBind"  OnSelectedIndexChanged="BindSelectedAccItemetail" >  
                            <EmptyDataTemplate > <div class="hprvt8"> No data found </div> </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" /> 
                            <Columns> 
                                <asp:BoundField DataField='Sad_itm_cd' HeaderText='Item' HeaderStyle-Width ="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Mi_longdesc' HeaderText='Description' HeaderStyle-Width ="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Mi_model' HeaderText='Model' HeaderStyle-Width ="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Sad_qty' HeaderText='Qty' HeaderStyle-Width ="70px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField='Sad_unit_rt' HeaderText='U. Price'  HeaderStyle-Width ="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField Visible="false" > <ItemTemplate> <asp:HiddenField runat="server" ID="hdnlineNo"   Value='<%# DataBinder.Eval(Container.DataItem, "Sad_itm_line") %>' />    <asp:HiddenField runat="server" ID="hdnIsForwardSale"  Value='<%# DataBinder.Eval(Container.DataItem, "Mi_act") %>' /> <asp:HiddenField runat="server" ID="hdnInvoiceNo"                                              Value='<%# DataBinder.Eval(Container.DataItem, "sad_inv_no") %>' />   </ItemTemplate>    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                 </div>
            </div>
        </div>
        <div class="hprvt9" ></div>
        <%--Account Summary--%>
        <div class="hprvt10" >
        <asp:CheckBox ID="CheckBoxView" runat="server" Text="View" AutoPostBack="True" 
                oncheckedchanged="CheckBoxView_CheckedChanged" />
            <AS:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server"  Visible="false"/>
        </div>
    </div>
    <%--Selected Item Detail--%>
    <div class="hprvt1" >
         <div class="PanelHeader invcollapsovrid" > Returning Detail </div>
         <div class="hprvt7"> 
            <asp:Panel ID="pnlReturn" runat="server" ScrollBars="Auto" Height="150px" >
                        <asp:GridView runat="server" ID="gvAReturnItem" RowStyle-Wrap ="false"  Width="100%"  AutoGenerateColumns="False"  CssClass="GridView"  ShowHeaderWhenEmpty="true"
                            CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="Tus_ser_id,Tus_itm_cd,Tus_ser_1,Tus_ser_2,Tus_ser_3,Tus_warr_no,Tus_itm_stus,Tus_base_doc_no,Tus_base_itm_line,Tus_batch_line,Tus_ser_line" OnRowDeleting="SelectedItem_OnDelete" > 
                            <EmptyDataTemplate > <div class="hprvt8"> No data found </div> </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <Columns>
                                <asp:TemplateField  HeaderText=""  ItemStyle-Width="18px"  >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnPickItemDelete" runat ="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png" Width="12px" Height="12px"  CommandName="Delete" />
                                        <asp:HiddenField ID="hdnPopSerialID" runat ="server" Value=' <%# DataBinder.Eval(Container.DataItem, "Tus_ser_id") %>' />
                                        <asp:HiddenField ID="hdnPopItem" runat ="server" Value= ' <%# DataBinder.Eval(Container.DataItem, "Tus_itm_cd") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField='Tus_doc_no' HeaderText='DO No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_itm_cd' HeaderText='Item'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_itm_desc' HeaderText='Description'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_itm_model' HeaderText='Model'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_itm_stus' HeaderText='Status'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_qty' HeaderText='Qty'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_ser_1' HeaderText='Serial No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_warr_no' HeaderText='Warranty No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='tus_unit_price' HeaderText='U.Price'  ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                            </Columns>
                        </asp:GridView>
             </asp:Panel>
         </div>
    </div>
    <%--General Details--%>
    <div class="hprvt1" >
        <div class="PanelHeader invcollapsovrid" > General Detail </div> 
        <div class="hprvt7">
            <div class="div1pcelt"> &nbsp; </div>
            <div class="div10pcelt"> Reverted By </div><div class="div2pcelt">:</div>
            <div class="div25pcelt"><asp:TextBox runat="server" ID="txtRevertedBy" CssClass="TextBox" ></asp:TextBox> </div>
        </div>
        <div class="hprvt7">
            <div class="div1pcelt"> &nbsp; </div>
            <div class="div10pcelt"> Remarks </div><div class="div2pcelt">:</div>
            <div class="div81pcelt"><asp:TextBox runat="server" ID="txtRemarks" CssClass="TextBox" Width="100%" TextMode="MultiLine" Rows=2 ></asp:TextBox></div> 
        </div>
    </div>
</div>
<%-- Modal Popup Extenders for hire sales serial --%>
<div>
        <asp:ModalPopupExtender ID="MPESerial" RepositionMode="RepositionOnWindowScroll" BehaviorID="Modal2"
        TargetControlID="btnPopUpSer" runat="server" ClientIDMode="Static" PopupControlID="pnlSerialPopUp"
        BackgroundCssClass="modalBackground" CancelControlID="imgbtnserClose" PopupDragHandleControlID="divpopserHeader">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlSerialPopUp" runat="server" Height="350px" Width="500px" CssClass="ModalWindow">
            <%-- PopUp Handler for drag and control --%>
            <div class="popUpHeader" id="divpopserHeader" runat="server">
                <div class="div80pcelt" >   Select Deliverd Serial</div>
                <div class="invunkwn57">
                <asp:ImageButton ID="imgbtnserClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
            </div>
            <%-- PopUp Message Area --%>
            <div class="hprvt11">
                        <asp:Label ID="Label1" runat="server" Text="" Width="100%"></asp:Label>
            </div>
            <div class="hprvt12" id ="divPopUpNonSeral" runat="server">
                Qty &nbsp; <asp:TextBox runat ="server" ID="txtPopQty" CssClass="TextBox" Width="25%" ></asp:TextBox> &nbsp; <asp:Button runat="server" ID="btnPopUpPickQty" CssClass="Button" Text="Pick" OnClientClick="CheckPopUserQty();return false;"  />
            </div>
            <div class="hprvt13">
                     <asp:Button ID="btnPopSerConfirm" runat="server" Text="Confirm" Width="75px" CssClass="Button" OnClick="ConfirmPopUpSerial_Click" />
            </div>
            <asp:Panel runat="server" ID="pnlSerMain" Width="100%" ScrollBars="Auto">
                <asp:GridView runat="server" ID="gvPopSerial" AutoGenerateColumns="false" 
                CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both"  DataKeyNames="Tus_ser_id,Tus_itm_cd,Tus_ser_1,Tus_ser_2,Tus_ser_3,Tus_warr_no,Tus_itm_stus,Tus_base_doc_no,Tus_base_itm_line,Tus_batch_line,Tus_ser_line" >
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:TemplateField  HeaderText=""  ItemStyle-Width="18px"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPopSerSelect" runat ="server"  Checked=' <%# MyFunction(Convert.ToString(DataBinder.Eval(Container.DataItem, "tus_serial_id"))) %>'  />
                                <asp:HiddenField ID="hdnPopSerialID" runat ="server" Value=' <%# DataBinder.Eval(Container.DataItem, "Tus_ser_id") %>' />
                                <asp:HiddenField ID="hdnPopItem" runat ="server" Value= ' <%# DataBinder.Eval(Container.DataItem, "Tus_itm_cd") %>'  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField='Tus_doc_no' HeaderText='DO No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        <asp:BoundField DataField='Tus_ser_1' HeaderText='Serial No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        <asp:BoundField DataField='Tus_warr_no' HeaderText='Warranty No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        <asp:BoundField DataField='Tus_itm_stus' HeaderText='Status'  ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                    </Columns>
                </asp:GridView>
            </asp:Panel>

        </asp:Panel>
    </div>
<%-- Modal Popup Extenders for multiple account/Date --%>
<div>
    <%--Modal pop-up --%>
    <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel" ClientIDMode="Static"  PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup"  BackgroundCssClass="modalBackground" PopupDragHandleControlID="divpopHeader"> </asp:ModalPopupExtender>
    <div class="invunkwn5">
    <asp:Panel ID="Panel_popUp" runat="server" Width="500px" >
        <%-- PopUp Handler for drag and control --%>
        <div class="popUpHeader" id="divpopHeader" runat="server">
        <div class="div80pcelt" runat="server" id="divPopCaption"> Select Account </div>
        <div class="invunkwn57"> <asp:ImageButton ID="btnPopupCancel" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div> 
        </div>
        <asp:Panel ID="PanelPopup_grv" runat="server" ScrollBars="Both" >
            <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT" 
            onselectedindexchanged="grvMpdalPopUp_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectText="select" ShowSelectButton="True" />
                <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" />
            </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>
    </div>

    <%-- Doc Date --%>
    <asp:CalendarExtender ID="CEDocDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDate"   PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MM/yyyy">   </asp:CalendarExtender>
</div>
<%-- Control Area --%>
<div style="display: none;">
        
        <asp:Button ID="btnPopUpSer" runat="server" ClientIDMode="Static"  />
        <asp:Button ID="btnAccount" runat="server" OnClick="btn_validateACC_Click" />
        <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
        <asp:HiddenField ID="hdnSystemQty" Value ="" runat="server" ClientIDMode="Static" />   

       
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
