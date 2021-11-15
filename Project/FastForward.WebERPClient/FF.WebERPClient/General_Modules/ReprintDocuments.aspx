<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ReprintDocuments.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ReprintDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TabContainer ID="tbcRequest" runat="server" ActiveTabIndex="0" Height="450px">
        <asp:TabPanel ID="tbpInventory" runat="server" HeaderText="Reprint Document Request Details">
            <ContentTemplate>
                <asp:UpdatePanel ID="pnlButtons" runat="server">
                    <ContentTemplate>
                        <div style="float: none;" id="divButton">
                            <asp:Panel ID="pnlButton" runat="server" Direction="RightToLeft">
                                <asp:Button Text="Close" ID="btnClose" runat="server" CssClass="Button" OnClick="btnClose_Click" />
                                &nbsp;
                                <asp:Button Text="Cancel" ID="btnCancel" runat="server" CssClass="Button" OnClick="btnCancel_Click" />
                                &nbsp;
                                <asp:Button Text="Print" ID="btnPrint" runat="server" CssClass="Button" OnClick="btnPrint_Click"
                                    Enabled="false" />
                                &nbsp;
                                <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click" />
                                &nbsp;
                            </asp:Panel>
                        </div>
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 7%">
                                From Date</div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 15%">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Enabled="false" Width="93px"
                                    AutoPostBack="false"></asp:TextBox>
                                <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgFromDate"
                                    TargetControlID="txtFromDate" Format="dd/MMM/yyyy">
                                </cc1:CalendarExtender>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 7%; height: 15px;">
                                To Date</div>
                            <div style="float: left; width: 1%; height: 15px;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 15%">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Enabled="false" Width="93px"
                                    AutoPostBack="false"></asp:TextBox>
                                <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgToDate"
                                    TargetControlID="txtToDate" Format="dd/MMM/yyyy">
                                </cc1:CalendarExtender>
                            </div>
                            <div style="float: left; width: 3%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 15%">
                                <asp:Button Text="View" ID="btnView" runat="server" CssClass="Button" OnClick="btnView_Click" />
                            </div>
                        </div>
                        <div style="float: left; height: 8px; width: 100%;">
                        </div>
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%">
                                <%--            <asp:UpdatePanel ID="pnlRep" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                                <div style="float: left; width: 18%">
                                    <asp:Panel ID="pnlCheckBoxes" runat="server" GroupingText="." Font-Size="11px" Width="285px"
                                        Font-Names="Tahoma">
                                        <div class="CollapsiblePanelHeader" style="width: 100%">
                                            Document Types
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 98%;">
                                                <div>
                                                    <asp:RadioButton ID="opt1" runat="server" Text="Cash Invoice" GroupName="Rpt" OnCheckedChanged="opt1_OnCheckedChanged"
                                                        AutoPostBack="true" Checked="true" /></div>
                                                <div>
                                                    <asp:RadioButton ID="opt2" runat="server" Text="Cash Sale Reversal" GroupName="Rpt"
                                                        OnCheckedChanged="opt2_OnCheckedChanged" AutoPostBack="true" /></div>
                                                <div>
                                                    <asp:RadioButton ID="opt3" runat="server" Text="Credit Invoice" GroupName="Rpt" 
                                                    OnCheckedChanged="opt3_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt4" runat="server" Text="Credit Sale Reversal" GroupName="Rpt" 
                                                    OnCheckedChanged="opt4_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt5" runat="server" Text="Diriya Cash Memo" GroupName="Rpt" 
                                                    OnCheckedChanged="opt5_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt6" runat="server" Text="HP Invoive" GroupName="Rpt" 
                                                    OnCheckedChanged="opt6_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt7" runat="server" Text="HP Reversal" GroupName="Rpt" 
                                                    OnCheckedChanged="opt7_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt8" runat="server" Text="Credit Sale Settlement" GroupName="Rpt" /></div>
                                                <div>
                                                    <asp:RadioButton ID="opt9" runat="server" Text="Outward Document Note" GroupName="Rpt" 
                                                    OnCheckedChanged="opt9_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt10" runat="server" Text="Inward Document Note" GroupName="Rpt" 
                                                    OnCheckedChanged="opt10_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt11" runat="server" Text="Advance Receipt" GroupName="Rpt" 
                                                    OnCheckedChanged="opt11_OnCheckedChanged" AutoPostBack="true"/></div>
                                                <div>
                                                    <asp:RadioButton ID="opt12" runat="server" Text="HP Receipt" GroupName="Rpt" 
                                                    OnCheckedChanged="opt12_OnCheckedChanged" AutoPostBack="true"/></div>
                                                                      <div>
                                                    <asp:RadioButton ID="opt13" runat="server" Text="HP Agreement" GroupName="Rpt" 
                                                    OnCheckedChanged="opt13_OnCheckedChanged" AutoPostBack="true"/></div>
                                               
                                            </div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <%--            </ContentTemplate>
            </asp:UpdatePanel>--%>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 68%;">
                                <div class="commonPageCss" style="float: left; width: 100%">
                                    <asp:Panel ID="pnlHeader" runat="server" GroupingText="." Font-Size="11px" Font-Names="Tahoma">
                                        <div class="commonPageCss" style="float: left; width: 100%">
                                            <div class="CollapsiblePanelHeader" style="width: 100%">
                                                Document Details
                                            </div>
                                            <asp:Panel ID="pnlPendingDoc" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                                BorderWidth="1px" Font-Bold="true" Height="115px" Width="98%">
                                                <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                    DataKeyNames="docno,docdate" OnRowDataBound="OnPendingRequestBind" OnSelectedIndexChanged="BindSelectedDocNo"
                                                    CssClass="GridView" Width="100%" CellPadding="4" EmptyDataText="No data found"
                                                    ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField='docno' HeaderText='Document Number' HeaderStyle-Width="170px"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField='docdate' HeaderText='Document Date' HeaderStyle-Width="75px"
                                                            DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%">
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 18%">
                                        Document Number</div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 18">
                                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="TextBox" Enabled="false" Width="120px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 3%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 15%">
                                        Document Date</div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 18%">
                                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="TextBox" Enabled="false" Width="93px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%">
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 18%">
                                        Reason</div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 70%">
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="TextBox" Width="450px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%">
                                    <div class="commonPageCss" style="float: left; width: 100%">
                                        <asp:Panel ID="Panel1" runat="server" GroupingText="." Font-Size="11px" Font-Names="Tahoma">
                                            <div class="commonPageCss" style="float: left; width: 100%">
                                                <div class="CollapsiblePanelHeader" style="width: 100%">
                                                    Requested Document Details
                                                </div>
                                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                                    BorderWidth="1px" Font-Bold="true" Height="115px" Width="98%">
                                                    <asp:GridView ID="gvReqDocs" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                        DataKeyNames="DRP_DOC_NO,DRP_DOC_DT,DRP_TP,DRP_REQ_DT,DRP_STUS,DRP_PRINTED" OnRowDataBound="OnRequestedBind"
                                                        OnSelectedIndexChanged="BindSelectedReqDocNo" CssClass="GridView" Width="100%"
                                                        CellPadding="4" EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField='DRP_DOC_NO' HeaderText='Document Number' HeaderStyle-Width="120px"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='DRP_DOC_DT' HeaderText='Document Date' HeaderStyle-Width="75px"
                                                                DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='DRP_TP' HeaderText='Document Type' HeaderStyle-Width="100px"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='DRP_REQ_DT' HeaderText='Requested Date' HeaderStyle-Width="75px"
                                                                DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='DRP_STUS' HeaderText='Status' HeaderStyle-Width="70px"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='DRP_PRINTED' HeaderText='Printed' HeaderStyle-Width="80px"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div style="float: left; height: 8px; width: 100%;">
                                    </div>
                                    <div style="float: left; width: 100%">
                                        <div style="float: left; width: 1%">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 22%">
                                            Requested Document Number</div>
                                        <div style="float: left; width: 1%">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 18">
                                            <asp:TextBox ID="txtReqDocNo" runat="server" CssClass="TextBox" Enabled="false" Width="120px"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 3%">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 22%">
                                            Requested Document Date</div>
                                        <div style="float: left; width: 1%">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 18%">
                                            <asp:TextBox ID="txtReqDocDate" runat="server" CssClass="TextBox" Enabled="false"
                                                Width="93px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tbpApprove" runat="server" HeaderText="Reprint Document Approve Details">
            <ContentTemplate>
                <div style="float: none;" id="div1">
                    <asp:Panel ID="Panel3" runat="server" Direction="RightToLeft">
                        <asp:Button Text="Close" ID="btnCloseApp" runat="server" CssClass="Button" OnClick="btnCloseApp_Click" />
                        &nbsp;
                        <asp:Button Text="Reject" ID="btnReject" runat="server" CssClass="Button" OnClick="btnReject_Click"
                            Enabled="false" />
                        &nbsp;
                        <asp:Button Text="Approve" ID="btnApprove" runat="server" CssClass="Button" OnClick="btnApprove_Click"
                            Enabled="false" />
                        &nbsp;
                    </asp:Panel>
                </div>
                <div class="commonPageCss" style="float: left; width: 100%">
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 7%">
                        From Date</div>
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 15%">
                        <asp:TextBox ID="txtFromDateApp" runat="server" CssClass="TextBox" Enabled="false"
                            Width="93px" AutoPostBack="false"></asp:TextBox>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgFromDate"
                            TargetControlID="txtFromDateApp" Format="dd/MMM/yyyy">
                        </cc1:CalendarExtender>
                    </div>
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 7%; height: 15px;">
                        To Date</div>
                    <div style="float: left; width: 1%; height: 15px;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 15%">
                        <asp:TextBox ID="txtToDateApp" runat="server" CssClass="TextBox" Enabled="false"
                            Width="93px" AutoPostBack="false"></asp:TextBox>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="imgToDate"
                            TargetControlID="txtToDateApp" Format="dd/MMM/yyyy">
                        </cc1:CalendarExtender>
                    </div>
                    <div style="float: left; width: 3%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 15%">
                        <asp:Button Text="View" ID="btnViewApp" runat="server" CssClass="Button" OnClick="btnViewApp_Click" />
                    </div>
                </div>
                <div class="commonPageCss" style="float: left; width: 100%">
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 60%">
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 25%">
                                <asp:RadioButton ID="optALL" runat="server" Text="All" GroupName="App" AutoPostBack="true"
                                    Checked="true" OnCheckedChanged="optAll_OnCheckedChanged" /></div>
                            <div style="float: left; width: 25%">
                                <asp:RadioButton ID="optPending" runat="server" Text="Pending" GroupName="App" AutoPostBack="true"
                                    OnCheckedChanged="optPending_OnCheckedChanged" /></div>
                            <div style="float: left; width: 25%">
                                <asp:RadioButton ID="optApp" runat="server" Text="Approved" GroupName="App" AutoPostBack="true"
                                    OnCheckedChanged="optApp_OnCheckedChanged" /></div>
                            <div style="float: left; width: 25%">
                                <asp:RadioButton ID="optRej" runat="server" Text="Rejected" GroupName="App" AutoPostBack="true"
                                    OnCheckedChanged="optRej_OnCheckedChanged" /></div>
                        </div>
                    </div>
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                </div>
                <div style="float: left; height: 8px; width: 100%;">
                </div>
                <div class="commonPageCss" style="float: left; width: 100%">
                    <div class="CollapsiblePanelHeader" style="width: 100%">
                        Requested Document Details
                    </div>
                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                        BorderWidth="1px" Font-Bold="true" Height="330px" Width="98%">
                        <asp:GridView ID="gvAllDocs" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            DataKeyNames="drp_loc,DRP_DOC_NO,DRP_DOC_DT,DRP_TP,DRP_REQ_DT,DRP_STUS,DRP_PRINTED"
                            OnRowDataBound="OnAllBind" OnSelectedIndexChanged="BindAllReqDocNo" CssClass="GridView"
                            Width="100%" CellPadding="4" EmptyDataText="No data found" ForeColor="#333333"
                            ShowHeaderWhenEmpty="True">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField='DRP_LOC' HeaderText='Location' HeaderStyle-Width="120px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='DRP_DOC_NO' HeaderText='Document Number' HeaderStyle-Width="120px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='DRP_DOC_DT' HeaderText='Document Date' HeaderStyle-Width="75px"
                                    DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='DRP_TP' HeaderText='Document Type' HeaderStyle-Width="100px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='DRP_REQ_DT' HeaderText='Requested Date' HeaderStyle-Width="75px"
                                    DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='DRP_STUS' HeaderText='Status' HeaderStyle-Width="70px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='DRP_PRINTED' HeaderText='Printed' HeaderStyle-Width="80px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div style="float: left; height: 8px; width: 100%;">
                </div>
                <div style="float: left; width: 100%">
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 22%">
                        Document Number</div>
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 18">
                        <asp:TextBox ID="txtDocNoApp" runat="server" CssClass="TextBox" Enabled="false" Width="93px"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 3%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 22%">
                        Document Date</div>
                    <div style="float: left; width: 1%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 18%">
                        <asp:TextBox ID="txtDocDateApp" runat="server" CssClass="TextBox" Enabled="false"
                            Width="93px"></asp:TextBox>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
