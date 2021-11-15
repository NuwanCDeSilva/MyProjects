<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ProfitCenterSearch.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_ProfitCenterSearch" %>
<%@ Register Src="~/UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="CC" %>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    //-------------------------------------GetItemData()------------------------------------------------------//
    function GetCompanyData(EnumId, TargetControlId, TargetLabelId) {
        var itemCode = document.getElementById(TargetControlId).value;

        itemCode = itemCode.toUpperCase();
        if (itemCode != "") {

            FF.AbansTours.LocalWebServices.CommonSearchWebServive.Get_pc_HIRC_SearchDesc(EnumId, itemCode, generateSuccess(TargetLabelId, TargetControlId), generateFalier(TargetLabelId));
        }
        //        else {
        //            
        //            ClearItemFields(TargetControlId);
        //        }
    }


    function GetCompanyDataTextChange(EnumId, TargetControlId, TargetLabelId) {
        var itemCode = document.getElementById(TargetControlId).value;

        itemCode = itemCode.toUpperCase();
        if (itemCode != "") {

            FF.AbansTours.LocalWebServices.CommonSearchWebServive.Get_pc_HIRC_SearchDesc(EnumId, itemCode, generateSuccessTextChange(TargetLabelId, TargetControlId), generateFalierTextChange(TargetLabelId));
        }
        else {

            ClearItemFields(TargetControlId);
        }
    }


    function generateSuccess(var1, var2) {
        return function (res) {
            if (res != null)
                document.getElementById(var1).value = res;
            //            else {
            //                ClearItemFields(var2);
            //            }
        }
    }

    function generateSuccessTextChange(var1, var2) {
        return function (res) {
            if (res != null)
                document.getElementById(var1).value = res;
            else {
                ClearItemFields(var2);
            }
        }
    }
    function generateFalierTextChange(TargetLabelId) {
        ClearItemFields(TargetLabelId);
    }


    function generateFalier(TargetLabelId) {
        ClearItemFields(TargetLabelId);
    }

    function ClearItemFields(TargetLabelId) {
        if (TargetLabelId == '<%=TextBoxCompany.ClientID %>') {
            document.getElementById('<%=TextBoxCompany.ClientID %>').value = "";
            document.getElementById('<%=TextBoxCompanyDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxChannel.ClientID %>').value = "";
            document.getElementById('<%=TextBoxChannelDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxSubChannel.ClientID %>').value = "";
            document.getElementById('<%=TextBoxSubChannelDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxArea.ClientID %>').value = "";
            document.getElementById('<%=TextBoxAreaDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegion.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegionDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZone.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZoneDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
        if (TargetLabelId == '<%=TextBoxChannel.ClientID %>') {
            document.getElementById('<%=TextBoxChannel.ClientID %>').value = "";
            document.getElementById('<%=TextBoxChannelDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxSubChannel.ClientID %>').value = "";
            document.getElementById('<%=TextBoxSubChannelDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxArea.ClientID %>').value = "";
            document.getElementById('<%=TextBoxAreaDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegion.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegionDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZone.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZoneDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
        if (TargetLabelId == '<%=TextBoxSubChannel.ClientID %>') {
            document.getElementById('<%=TextBoxSubChannel.ClientID %>').value = "";
            document.getElementById('<%=TextBoxSubChannelDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxArea.ClientID %>').value = "";
            document.getElementById('<%=TextBoxAreaDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegion.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegionDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZone.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZoneDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
        if (TargetLabelId == '<%=TextBoxArea.ClientID %>') {
            document.getElementById('<%=TextBoxArea.ClientID %>').value = "";
            document.getElementById('<%=TextBoxAreaDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegion.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegionDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZone.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZoneDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
        if (TargetLabelId == '<%=TextBoxRegion.ClientID %>') {
            document.getElementById('<%=TextBoxRegion.ClientID %>').value = "";
            document.getElementById('<%=TextBoxRegionDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZone.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZoneDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
        if (TargetLabelId == '<%=TextBoxZone.ClientID %>') {
            document.getElementById('<%=TextBoxZone.ClientID %>').value = "";
            document.getElementById('<%=TextBoxZoneDes.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
        if (TargetLabelId == '<%=TextBoxLocation.ClientID %>') {
            document.getElementById('<%=TextBoxLocation.ClientID %>').value = "";
            document.getElementById('<%=TextBoxLocationDes.ClientID %>').value = "";
        }
    }
       
</script>
<style>
    .Label
    {
        border: none;
        background-color: white;
    }
</style>
<asp:UpdatePanel ID="UpdatePanelContent" runat="server">
    <ContentTemplate>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Company</div>
                <div style="float: left; width: 70%;">
                  <asp:TextBox ID="TextBoxCompany" runat="server" CssClass="TextBox TextBoxUpper" 
                        Width="80px"></asp:TextBox>
                    
                    
                        <asp:ImageButton ID="imgComSearch" runat="server" 
                        ImageUrl="~/Images/icon_search.png" OnClick="imgComSearch_Click" />
                   
                    
                        <asp:TextBox ID="TextBoxCompanyDes" CssClass="Label" Enabled="false" runat="server"
                            ForeColor="Blue" Width="240px"></asp:TextBox>
                  
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Channel</div>
                <div style="float: left; width: 70%;">
                    <asp:TextBox ID="TextBoxChannel" runat="server" CssClass="TextBox TextBoxUpper" 
                        Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="imgChaSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgChaSearch_Click" />
                    <asp:TextBox ID="TextBoxChannelDes" CssClass="Label" Enabled="false" runat="server"
                        ForeColor="Blue" Width="240px"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Sub Channel</div>
                <div style="float: left; width: 70%;">
                    <asp:TextBox ID="TextBoxSubChannel" runat="server" 
                        CssClass="TextBox TextBoxUpper" Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="imgSubChaSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgSubChaSearch_Click" />
                    <asp:TextBox ID="TextBoxSubChannelDes" CssClass="Label" Enabled="false" runat="server"
                        ForeColor="Blue" Width="240px"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Area</div>
                <div style="float: left; width: 70%;">
                    <asp:TextBox ID="TextBoxArea" runat="server" CssClass="TextBox TextBoxUpper" 
                        Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="imgAreaSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgAreaSearch_Click" />
                    <asp:TextBox ID="TextBoxAreaDes" runat="server" CssClass="Label" Enabled="false"
                        ForeColor="Blue" Width="240px"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Region</div>
                <div style="float: left; width: 70%;">
                    <asp:TextBox ID="TextBoxRegion" runat="server" CssClass="TextBox TextBoxUpper" 
                        Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="imgRegionSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgRegionSearch_Click" />
                    <asp:TextBox ID="TextBoxRegionDes" runat="server" CssClass="Label" Enabled="false"
                        ForeColor="Blue" Width="240px"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Zone</div>
                <div style="float: left; width: 70%;">
                    <asp:TextBox ID="TextBoxZone" runat="server" CssClass="TextBox TextBoxUpper" 
                        Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="imgZoneSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgZoneSearch_Click" />
                    <asp:TextBox ID="TextBoxZoneDes" runat="server" CssClass="Label" Enabled="false"
                        ForeColor="Blue" Width="240px"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 20%; text-align: right;">
                    Profit Center</div>
                <div style="float: left; width: 70%;">
                    <asp:TextBox ID="TextBoxLocation" runat="server" 
                        CssClass="TextBox TextBoxUpper" Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="imgLocSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgLocSearch_Click" />
                    <asp:TextBox ID="TextBoxLocationDes" runat="server" CssClass="Label" Enabled="false"
                        ForeColor="Blue" Width="240px"></asp:TextBox>
                </div>
                <%--<CC:uc_CommonSearch ID="ucSearch" runat="server" ClientIDMode="Static" />--%>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
