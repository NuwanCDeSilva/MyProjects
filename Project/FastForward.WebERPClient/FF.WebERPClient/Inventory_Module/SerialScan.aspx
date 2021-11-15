<%@ Page Title="Serial Scaning" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SerialScan.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.SerialScan"
    EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">


        function pageLoad() {
            var infochngPositionBin = $find('<%= DDEBin.ClientID %>')._dropPopupPopupBehavior;
            infochngPositionBin.set_positioningMode(2);

            var infochngPositionStatus = $find('<%= DDEStatus.ClientID %>')._dropPopupPopupBehavior;
            infochngPositionStatus.set_positioningMode(2);

            var infochngPositionSStatus = $find('<%= DDESStatus.ClientID %>')._dropPopupPopupBehavior;
            infochngPositionSStatus.set_positioningMode(2);


            //$find('<%= DDEBin.ClientID %>')._dropWrapperHoverBehavior_onhover();
            //$find('<%= DDEBin.ClientID %>').unhover = VisibleMe;

            //$find('<%= DDEStatus.ClientID %>')._dropWrapperHoverBehavior_onhover();
            //$find('<%= DDEStatus.ClientID %>').unhover = VisibleMe;

            //$find('<%= DDESStatus.ClientID %>')._dropWrapperHoverBehavior_onhover();
            //$find('<%= DDESStatus.ClientID %>').unhover = VisibleMe;
        }

        function VisibleMe() {
            //$find('<%= DDEBin.ClientID %>')._dropWrapperHoverBehavior_onhover();
            //$find('<%= DDEStatus.ClientID %>')._dropWrapperHoverBehavior_onhover();
            //$find('<%= DDESStatus.ClientID %>')._dropWrapperHoverBehavior_onhover();
        }




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


        function TableRowClicks(rowIndex, destCtrl) {
            //Get the selected cell value using selected rowIndex.
            var tabRowId = "tab" + rowIndex;
            var selectedRow = document.getElementById(tabRowId);
            var Cells = selectedRow.getElementsByTagName("td");
            var selectedValue = Cells[0].innerText;


            //Get the result object and set the value.
            var resultObject = document.getElementById(destCtrl);
            resultObject.value = selectedValue;
            resultObject.focus();

        }


        function GetItemDescription(src, dest) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetItemDescription(ctrl.value, onDescriptionPass, onDescriptionFail, dest);
            }
        }
        //SucceededCallback method.
        function onDescriptionPass(result, destCtrl) {
            var objs = new Array();
            objs = destCtrl.split("|");
            var _desc = document.getElementById(objs[0]);
            var _item = document.getElementById(objs[1]);
            if (result == "") {
                _desc.value = "";
                _item.value = "";
                alert('Invalid Item Code');
            }
            else _desc.value = result;


        }
        //FailedCallback method.
        function onDescriptionFail(error, destCtrl) {
            var objs = new Array();
            objs = destCtrl.split("|");
            var _desc = document.getElementById(objs[0]);
            var _item = document.getElementById(objs[1]);

        }


        //---------------------------------------------------------------------------------------------------------Get Item Description
        function IsItemSerialized_1(src, objTarget) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemSerialized_1(ctrl.value, onSer1Pass, onSer1Fail, objTarget);
            }
        }
        //SucceededCallback method.
        function onSer1Pass(result, objTarget) {
            var objs = new Array();
            objs = objTarget.split("|");
            var _Qty = document.getElementById(objs[0]);
            var _Serial = document.getElementById(objs[1]);
            if (_Qty.id == "txtQty") {
                var hidn = document.getElementById("hdnIsSerialized_1");
                hidn.value = result;
            }
            if (result == true) {
                _Qty.setAttribute("value", "1");
                _Qty.setAttribute("disabled", true);
                _Serial.value = "";
                _Serial.disabled = false;
            }
            else {
                _Qty.value = "";
                _Qty.disabled = false;
                _Serial.value = "N/A";
                _Serial.disabled = true;
            }
        }

        //FailedCallback method.
        function onSer1Fail(error, objQtys, objSerials) {
            var _Qty = document.getElementById(objQty);
            var _Serial = document.getElementById(objSerial);
            var hidn = document.getElementById("hdnIsSerialized_1");
            _Qty.value = "";
            _Qty.Enabled = false;
            _Serial.value = "";
            _Serial.Enabled = false;
            hidn.value = "";
        }


        //---------------------------------------------------------------------------------------------------------Get Item Description
        function IsItemSerialized_2(src, dest) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemSerialized_2(ctrl.value, onSer2Pass, onSer2Fail, dest);
            }
        }
        //SucceededCallback method.
        function onSer2Pass(result, destCtrl) {
            var hidn = document.getElementById(destCtrl);
            hidn.value = result;
            var _serial2 = document.getElementById("txtSerial2");
            if (result == true) {
                _serial2.disabled = false;
                _serial2.value = "";
            }
            else {
                _serial2.disabled = true;
                _serial2.value = "N/A";
            }

        }
        //FailedCallback method.
        function onSer2Fail(error, destCtrl) {
            var hidn = document.getElementById(destCtrl);
            hidn.value = "";
            var _serial2 = document.getElementById("txtSerial2");
            _serial2.disabled = false;
            _serial2.value = "";
        }



        //---------------------------------------------------------------------------------------------------------Get Item Description
        function IsItemSerialized_3(src, dest) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemSerialized_3(ctrl.value, onSer3Pass, onSer3Fail, dest);
            }
        }
        //SucceededCallback method.
        function onSer3Pass(result, destCtrl) {
            var hidn = document.getElementById(destCtrl);
            hidn.value = result;
            var _serial3 = document.getElementById("txtSerial3");
            if (result == true) {
                _serial3.disabled = false;
                _serial3.value = "";
            }
            else {
                _serial3.disabled = true;
                _serial3.value = "N/A";
            }
        }
        //FailedCallback method.
        function onSer3Fail(error, destCtrl) {
            var hidn = document.getElementById(destCtrl);
            hidn.value = "false";
            var _serial3 = document.getElementById("txtSerial3");
            _serial3.disabled = false;
            _serial3.value = "";
        }


        //---------------------------------------------------------------------------------------------------------Get Item Description
        function IsItemHaveSubSerial(src, dest) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemHaveSubSerial(ctrl.value, onSubItemPass, onSubItemFail, dest);
            }
        }
        //SucceededCallback method.
        function onSubItemPass(result, destCtrl) {
            var divResults = document.getElementById(destCtrl);
            divResults.value = result;
            var _image3 = document.getElementById("Image3");
            //var _extender = document.getElementById("CPESubSerial");

            if (result == true) {
                $find('CPESubSerial')._doOpen();
                _image3.disabled = false;
            }
            else {
                $find('CPESubSerial')._doClose();
                _image3.disabled = true;
            }
            IsHaveSubSerial();

        }
        //FailedCallback method.
        function onSubItemFail(error, destCtrl) {
            var divResults = document.getElementById(destCtrl);
            divResults.value = "false";
        }


        //Developed by Prabhath Wijetunge - on 15 03 2012
        //Allow only numaric and decimal values
        function numbersonly(e, decimal) {
            var key;
            var keychar;

            if (window.event) {
                key = window.event.keyCode;
            }
            else if (e) {
                key = e.which;
            }
            else {
                return true;
            }
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            else if (decimal && (keychar == ".")) {
                return true;
            }
            else
                return false;
        }


        //---------------------------------------------------------------------------------------------------------Get Item Description
        function IsUOMDecimalAllow(src, qty) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsUOMDecimalAllow(ctrl.value, onUOMPass, onUOMFail, qty);
            }
        }

        function onUOMPass(result, qty) {
            var objCtrl = document.getElementById(qty);
            objCtrl.setAttribute("onKeyPress", "return numbersonly(event," + result + ")");

        }

        function onUOMFail(error, qty) {
            var objCtrl = document.getElementById(qty);
            objCtrl.setAttribute("onKeyPress", "return numbersonly(event,false)");
        }

        function IsHaveSubSerial() {
            var _isHaveSSerial = document.getElementById("HdnIsHaveSubItem");
            // call server side method
            PageMethods.IsHaveSubSerial(_isHaveSSerial.value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updMain" runat="server">
        <ContentTemplate>
         <div style=" float:left; width:100%;">
             <div style=" float:left; width:100%;">
             <div style=" float:left; width:12%;">Document Type : </div>
             <div style=" float:left; width:12%;"><asp:TextBox ID="txtDocType" runat="server" CssClass="TextBox"  BorderStyle="None" Text='<%# GlbSerialScanDocumentType %>' ReadOnly="true"></asp:TextBox> </div>
           
        </div>
            <%-- Collaps Header - Request --%>
            <div style="height: 16px; background-color: Navy; color: White; width: 98%; float: left;"
                id="divReqBorder">
                Request Details
            </div>
            <%-- Collaps Image - Request --%>
            <div style="float: left;">
                <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" />
            </div>
            <%-- Collaps control - Request --%>
            <asp:CollapsiblePanelExtender ID="CPERequest" runat="server" TargetControlID="pnlRequest"
                CollapsedSize="0" ExpandedSize="100" Collapsed="True" ExpandControlID="Image1"
                CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
            </asp:CollapsiblePanelExtender>
            <%-- Collaps Area - Request --%>
            <div style="width: 100%; float: left; color: Navy; padding-top: 2px; padding-bottom: 2px;">
                <asp:Panel runat="server" ID="pnlRequest" Width="100%" ScrollBars="Auto">
                    <asp:GridView ID="gvRequest" runat="server" AutoGenerateColumns="false" OnRowDataBound="ScanRequestItemRowDataBound">
                        <Columns>
                            <asp:BoundField DataField="TUI_USRSEQ_NO" HeaderText="User Seq No" />
                            <asp:BoundField DataField="TUI_REQ_ITM_CD" HeaderText="Item" />
                            <asp:BoundField DataField="TUI_REQ_ITM_STUS" HeaderText="Status" />
                            <asp:BoundField DataField="TUI_REQ_ITM_QTY" HeaderText="Qty" />
                            <asp:BoundField DataField="TUI_PIC_ITM_CD" HeaderText="Pick Item" />
                            <asp:BoundField DataField="TUI_PIC_ITM_STUS" HeaderText="Pick Status" />
                            <asp:BoundField DataField="TUI_PIC_ITM_QTY" HeaderText="Pick Qty" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <%-- Collaps Header - Serial Scan (Main) --%>
            <div style="height: 16px; background-color: Navy; color: White; width: 98%; float: left;"
                id="divScanBorder">
                Serial Scan
            </div>
            <%-- Collaps Image - Serial Scan (Main) --%>
            <div style="float: left;">
                <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" />
            </div>
            <%-- Collaps control - Serial Scan (Main) --%>
            <asp:CollapsiblePanelExtender ID="CPEScanSerial" runat="server" TargetControlID="pnlSerialScan"
                CollapsedSize="0" ExpandedSize="500" Collapsed="True" ExpandControlID="Image2"
                CollapseControlID="Image2" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                ExpandDirection="Vertical" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
            </asp:CollapsiblePanelExtender>
            <%-- Collaps area - Serial Scan (Main) --%>
            <div style="width: 100%; height: 60px; float: left; color: Navy; padding-top: 2px;
                padding-bottom: 2px;">
                <asp:Panel runat="server" ID="pnlSerialScan">
                    <%--1nd Row--%>
                    <div style="width: 100%; float: left; color: Navy; padding-top: 2px;">
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 6%; float: left;">
                            Bin
                        </div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 8%; float: left;">
                            <asp:TextBox ID="txtBin" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div style="width: 15%; float: left;">
                            &nbsp;</div>
                        <div style="width: 8%; float: left;">
                            Item</div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 15%; float: left;">
                            <asp:TextBox ID="txtItem" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></div>
                        <div style="width: 5%; float: left; color: #A62D00;">
                            &nbsp;
                            <asp:ImageButton ID="imgBtnItem" runat="server" ImageUrl="~/Images/16x16_Lsearch.ico" ImageAlign="Middle" OnClick="imgBtnItem_Click" /></div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 30%; float: left;">
                            <asp:TextBox ID="txtItemDesc" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <%--2nd Row--%>
                    <div style="width: 100%; float: left; color: Navy; padding-top: 2px;">
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 6%; float: left;">
                            Status</div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 8%; float: left;">
                            <asp:TextBox ID="txtStatus" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div style="width: 15%; float: left;">
                            &nbsp;</div>
                        <div style="width: 8%; float: left;">
                            Serial 1</div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 20%; float: left;">
                            <asp:TextBox ID="txtSerial1" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></div>
                        <div style="width: 3%; float: left; color: #A62D00;">
                            &nbsp;
                            <asp:ImageButton ID="imgBtnSerial" runat="server" ImageUrl="~/Images/16x16_Lsearch.ico"
                                ImageAlign="Middle" /></div>
                        <div style="width: 3%; float: left;">
                            &nbsp;</div>
                        <div style="width: 6%; float: left;">
                            Serial 2</div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 20%; float: left;">
                            <asp:TextBox ID="txtSerial2" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></div>
                    </div>
                    <%--3nd Row--%>
                    <div style="width: 100%; float: left; color: Navy; padding-top: 2px;">
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 6%; float: left;">
                            Serial 3</div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 20%; float: left;">
                            <asp:TextBox ID="txtSerial3" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></div>
                        <div style="width: 3%; float: left;">
                            &nbsp;</div>
                        <div style="width: 8%; float: left;">
                            MFC</div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 15%; float: left;">
                            <asp:TextBox ID="txtMFC" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"
                                Text="N/A"></asp:TextBox></div>
                        <div style="width: 14%; float: left;">
                            &nbsp;</div>
                        <div style="width: 3%; float: left;">
                            Qty
                        </div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 5%; float: left;">
                            <asp:TextBox ID="txtQty" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"
                                onKeyPress="return numbersonly(event,false)"></asp:TextBox></div>
                        <div style="width: 1%; float: left;">
                            &nbsp;</div>
                        <div style="width: 5%; float: left;">
                            <asp:Button ID="btnAddMainItem" runat="server" CssClass="Button" Text="Add Item"
                                OnClick="SaveToTemporary" /></div>
                    </div>
                    <%-- Serial Details Grid (Main) --%>
                    <div style="width: 100%; float: left;">
                        <asp:Panel ID="pnlMain" runat="server" ScrollBars="Auto" Height="150px">
                            <asp:GridView runat="server" ID="gvMainSerial" ClientIDMode="Static" BackColor="White"
                                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="false"
                                OnRowDataBound="ScanSerialRowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="TUS_COM" HeaderText="Company" Visible="false" />
                                    <asp:BoundField DataField="TUS_LOC" HeaderText="Location" Visible="false" />
                                    <asp:BoundField DataField="TUS_BIN" HeaderText="Bin" />
                                    <asp:BoundField DataField="TUS_ITM_CD" HeaderText="Item" />
                                    <asp:BoundField DataField="TUS_ITM_STUS" HeaderText="Status" />
                                    <asp:BoundField DataField="TUS_QTY" HeaderText="Qty" />
                                    <asp:BoundField DataField="TUS_SER_1" HeaderText="Serial 1" />
                                    <asp:BoundField DataField="TUS_SER_2" HeaderText="Serial 2" />
                                    <asp:BoundField DataField="TUS_SER_3" HeaderText="Serial 3" />
                                    <asp:BoundField DataField="TUS_SER_4" HeaderText="Serial 4" Visible="false" />
                                    <asp:BoundField DataField="TUS_USRSEQ_NO" HeaderText="User Seq No" Visible="false" />
                                    <asp:BoundField DataField="TUS_DOC_NO" HeaderText="Doc No" Visible="false" />
                                    <asp:BoundField DataField="TUS_SEQ_NO" HeaderText="Seq No" Visible="false" />
                                    <asp:BoundField DataField="TUS_ITM_LINE" HeaderText="Line No" Visible="false" />
                                    <asp:BoundField DataField="TUS_BATCH_LINE" HeaderText="Bacth Line" Visible="false" />
                                    <asp:BoundField DataField="TUS_SER_LINE" HeaderText="Serial Line" Visible="false" />
                                    <asp:BoundField DataField="TUS_DOC_DT" HeaderText="Doc Date" Visible="false" />
                                    <asp:BoundField DataField="TUS_SER_ID" HeaderText="Serial ID" Visible="false" />
                                    <asp:BoundField DataField="TUS_WARR_NO" HeaderText="Warranty No" />
                                    <asp:BoundField DataField="TUS_WARR_PERIOD" HeaderText="Warranty Period" Visible="false" />
                                    <asp:BoundField DataField="TUS_ORIG_GRNCOM" HeaderText="Original Grn Company" Visible="false" />
                                    <asp:BoundField DataField="TUS_ORIG_GRNNO" HeaderText="Original Grn No" Visible="false" />
                                    <asp:BoundField DataField="TUS_ORIG_GRNDT" HeaderText="Original Grn Date" Visible="false" />
                                    <asp:BoundField DataField="TUS_ORIG_SUPP" HeaderText="Original Supplier" Visible="false" />
                                    <asp:BoundField DataField="TUS_EXIST_GRNCOM" HeaderText="Exist Grn Company" Visible="false" />
                                    <asp:BoundField DataField="TUS_EXIST_GRNNO" HeaderText="Exist Grn No" Visible="false" />
                                    <asp:BoundField DataField="TUS_EXIST_GRNDT" HeaderText="Exist Grn Date" Visible="false" />
                                    <asp:BoundField DataField="TUS_EXIST_SUPP" HeaderText="Exist Supplier" Visible="false" />
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                    <%--Small Void--%>
                    <div style="width: 100%; float: left; padding-bottom: 2px;">
                    </div>
                    <%-- Collaps Header - Serial Scan (Sub) --%>
                    <div style="height: 16px; background-color: Navy; color: White; width: 98%; float: left;"
                        id="divSubSerialBorder">
                        Sub Serial Details
                    </div>
                    <%-- Collaps Image - Serial Scan (Sub) --%>
                    <div style="float: left;">
                        <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right"
                            ClientIDMode="Static" Enabled="false" />
                    </div>
                    <%-- Collaps control - Serial Scan (Sub) --%>
                    <asp:CollapsiblePanelExtender ID="CPESubSerial" runat="server" TargetControlID="pnlSubSerials"
                        CollapsedSize="0" ExpandedSize="150" Collapsed="True" ExpandControlID="Image3"
                        CollapseControlID="Image3" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image3" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg" ClientIDMode="Static">
                    </asp:CollapsiblePanelExtender>
                    <%-- Collaps area - Serial Scan (Sub) --%>
                    <div>
                        <asp:Panel runat="server" ID="pnlSubSerials" ScrollBars="Auto" Width="100%" BackColor="GhostWhite">
                            <%--1nd Row--%>
                            <div style="width: 100%; float: left; color: Navy; padding-top: 2px;">
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 6%; float: left;">
                                    Sub Item</div>
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 15%; float: left;">
                                    <asp:TextBox ID="txtSItem" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></div>
                                <div style="width: 2%; float: left;">
                                    <asp:ImageButton ID="imgBtnSItem" runat="server" ImageUrl="~/Images/16x16_Lsearch.ico"
                                        ImageAlign="Middle" OnClick="imgBtnSItem_Click" /></div>
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 70%; float: left;">
                                    <asp:TextBox ID="txtSubItemDesc" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <%--2nd Row--%>
                            <div style="width: 100%; float: left; color: Navy; padding-top: 2px;">
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 6%; float: left;">
                                    Status</div>
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 8%; float: left;">
                                    <asp:TextBox ID="txtSStatus" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div style="width: 20%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 8%; float: left;">
                                    Sub Serial</div>
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 18%; float: left;">
                                    <asp:TextBox ID="txtSSerial" CssClass="TextBox" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox></div>
                                <div style="width: 4%; float: left; color: #A62D00;">
                                    &nbsp;
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/16x16_Lsearch.ico"
                                        ImageAlign="Middle" /></div>
                                <div style="width: 10%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 4%; float: left;">
                                    Qty
                                </div>
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 5%; float: left;">
                                    <asp:TextBox ID="txtSQty" CssClass="TextBox" runat="server" Width="100%" onKeyPress="return numbersonly(event,false)"></asp:TextBox></div>
                                <div style="width: 1%; float: left;">
                                    &nbsp;</div>
                                <div style="width: 5%; float: left;">
                                    &nbsp;<asp:Button ID="btnSubItem" runat="server" Text="Add Item" OnClick="UpdateReptPickSerialsSub" />
                                </div>
                            </div>
                            <%-- Serial Details Grid (Sub) --%>
                            <div style="width: 100%; float: left;">
                                <asp:Panel ID="pnlSub" runat="server" ScrollBars="Auto" Height="150px">
                                    <asp:GridView runat="server" ID="gvSSerial" ClientIDMode="Static" RowStyle-VerticalAlign="Bottom"
                                        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" AutoGenerateColumns="False" AllowSorting="True" OnRowDataBound="ScanSubSerialRowDataBound">
                                        <%-- <Columns>
                                            <asp:BoundField DataField="TPSS_USRSEQ_NO" HeaderText="User Seq No" />
                                            <asp:BoundField DataField="TPSS_M_ITM_CD" HeaderText="Main Item Code" />
                                            <asp:BoundField DataField="TPSS_M_SER" HeaderText="Main Serial 1" />
                                            <asp:BoundField DataField="TPSS_WARR_NO" HeaderText="Warranty No" />
                                            <asp:BoundField DataField="TPSS_ITM_CD" HeaderText="Item" />
                                            <asp:BoundField DataField="TPSS_ITM_STUS" HeaderText="Status" />
                                            <asp:BoundField DataField="TPSS_SUB_SER" HeaderText="Serial No" />
                                            <asp:BoundField DataField="TPSS_MFC" HeaderText="MFC No" Visible="false" />
                                            <asp:BoundField DataField="TPSS_TP" HeaderText="Doc Type" />
                                            <asp:BoundField DataField="TPSS_WARR_PERIOD" HeaderText="Warranty Period" />
                                            <asp:BoundField DataField="TPSS_WARR_REM" HeaderText="Warranty Remarks" />
                                        </Columns>--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="User Seq No">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_USRSEQ_NO" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_USRSEQ_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_M_ITM_CD" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_M_ITM_CD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Serial 1">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_M_SER" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_M_SER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warranty No">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_WARR_NO" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_WARR_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_ITM_CD" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_ITM_CD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_ITM_STUS" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_ITM_STUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_SUB_SER" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_SUB_SER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MFC No">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_MFC" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_MFC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_TP" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_TP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warranty Period">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_WARR_PERIOD" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_WARR_PERIOD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warranty Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="TPSS_WARR_REM" runat="server" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container.DataItem, "TPSS_WARR_REM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                        <RowStyle BackColor="White" ForeColor="#003399" VerticalAlign="Bottom" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <%--Bin DropDown Area--%>
            <asp:DropDownExtender ID="DDEBin" runat="server" TargetControlID="txtBin" DropDownControlID="pnlBin"
                DropArrowBackColor="Black">
                <Animations> 
                    <OnShow> 
                        <Sequence>                     
                            <HideAction Visible="true" />                                           
                            <FadeIn  Duration="0" Fps="0" />           
                        </Sequence> 
                    </OnShow> 
                    <OnHide> 
                        <Sequence>                         
                            <FadeOut Duration=".5" Fps="10" /> 
                            <HideAction Visible="false" /> 
                            <StyleAction Attribute="display" Value="none"/> 
                        </Sequence> 
                    </OnHide> 
                </Animations>
            </asp:DropDownExtender>
            <asp:Panel ID="pnlBin" runat="server" ScrollBars="Auto">
                <asp:GridView runat="server" ID="dgvBin" AutoGenerateColumns="false" OnRowDataBound="BinRowDataBound"
                    OnSelectedIndexChanged="BinSelectedIndexChanged" CellPadding="4" ForeColor="#333333">
                    <Columns>
                        <asp:BoundField DataField="Ibl_bin_cd" HeaderText="Code" SortExpression="Ibl_bin_cd" />
                        <asp:BoundField DataField="Ibl_bin_des" HeaderText="Description" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:Panel>
            <%--Status DropDown Area--%>
            <asp:DropDownExtender ID="DDEStatus" runat="server" TargetControlID="txtStatus" DropDownControlID="pnlStatus"
                DropArrowBackColor="Black">
                <Animations> 
                    <OnShow> 
                        <Sequence>                     
                            <HideAction Visible="true" />                                           
                            <FadeIn  Duration="0" Fps="0" />           
                        </Sequence> 
                    </OnShow> 
                    <OnHide> 
                        <Sequence>                         
                            <FadeOut Duration=".5" Fps="10" /> 
                            <HideAction Visible="false" /> 
                            <StyleAction Attribute="display" Value="none"/> 
                        </Sequence> 
                    </OnHide> 
                </Animations>
            </asp:DropDownExtender>
            <asp:Panel ID="pnlStatus" runat="server" Height="151px" ScrollBars="Auto">
                <asp:GridView runat="server" ID="dgvStatus" AutoGenerateColumns="false" OnRowDataBound="StatusRowDataBound"
                    OnSelectedIndexChanged="StatusSelectedIndexChanged" AllowPaging="True" CellPadding="4"
                    ForeColor="#333333">
                    <Columns>
                        <asp:BoundField DataField="Mic_cd" HeaderText="Code" SortExpression="Mic_cd" />
                        <asp:BoundField DataField="Mis_desc" HeaderText="Description" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:Panel>
            <%--Sub Status DropDown Area--%>
            <asp:DropDownExtender ID="DDESStatus" runat="server" TargetControlID="txtSStatus"
                DropDownControlID="pnlSStatus" DropArrowBackColor="Black">
                <Animations> 
                    <OnShow> 
                        <Sequence>                     
                            <HideAction Visible="true" />                                           
                            <FadeIn  Duration="0" Fps="0" />           
                        </Sequence> 
                    </OnShow> 
                    <OnHide> 
                        <Sequence>                         
                            <FadeOut Duration=".5" Fps="10" /> 
                            <HideAction Visible="false" /> 
                            <StyleAction Attribute="display" Value="none"/> 
                        </Sequence> 
                    </OnHide> 
                </Animations>
            </asp:DropDownExtender>
            <asp:Panel ID="pnlSStatus" runat="server" Height="151px" ScrollBars="Auto">
                <asp:GridView runat="server" ID="dgvSStatus" AutoGenerateColumns="false" OnRowDataBound="SStatusRowDataBound"
                    OnSelectedIndexChanged="SStatusSelectedIndexChanged" AllowPaging="True" CellPadding="4"
                    ForeColor="#333333">
                    <Columns>
                        <asp:BoundField DataField="Mic_cd" HeaderText="Code" SortExpression="Mic_cd" />
                        <asp:BoundField DataField="Mis_desc" HeaderText="Description" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:Panel>
            <%-- Hidden Filed Area--%>
            <div>
                <asp:HiddenField ID="hdnResultControl" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnIsSerialized_1" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnIsSerialized_2" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnIsSerialized_3" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnIsHaveSubItem" runat="server" Value="" ClientIDMode="Static" />
                <%-- Out Side User Parameters --%>
                <asp:HiddenField ID="hdnDirection" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDocumentType" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDocumentNo" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnUserSeqNo" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSeqNo" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnGrnSupplier" runat="server" Value="" ClientIDMode="Static" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
