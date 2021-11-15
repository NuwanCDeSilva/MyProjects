using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text;
using System.Data;

namespace FF.AbansTours.UserControls
{
    public partial class uc_CompanySearch : System.Web.UI.UserControl
    {
        BasePage _basePage = null;

        #region properties
        
        public string Company
        {
            get { return TextBoxCompany.Text; } 
            set {  TextBoxCompany.Text=value; } 
        }

        public string CompanyDes
        {
            get { return TextBoxCompanyDes.Text; }
            set { TextBoxCompanyDes.Text = value; }
        }

        public string Channel
        {
            get { return TextBoxChannel.Text; }
            set { TextBoxChannel.Text = value; } 
        }

        public string SubChannel
        {
            get { return TextBoxSubChannel.Text; }
            set { TextBoxSubChannel.Text = value; } 
        }

        public string Area
        {
            get { return TextBoxArea.Text; }
            set { TextBoxArea.Text = value; } 
        }

        public string Region
        {
            get { return TextBoxRegion.Text; }
            set { TextBoxRegion.Text = value; } 
        }

        public string Zone
        {
            get { return TextBoxZone.Text; }
            set { TextBoxZone.Text = value; } 
        }

        public string ProfitCenter
        {
            get { return TextBoxLocation.Text; }
            set { TextBoxLocation.Text = value; } 
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _basePage = new BasePage();
                BindJavaScript();
            }
        }

        private void BindJavaScript()
        {
            TextBoxCompany.Attributes.Add("onblur", "GetCompanyData(" + 35 + ",'" + TextBoxCompany.ClientID + "','" + TextBoxCompanyDes.ClientID + "')");
            TextBoxChannel.Attributes.Add("onblur", "GetCompanyData(" + 36 + ",'" + TextBoxChannel.ClientID + "','" + TextBoxChannelDes.ClientID + "')");
            TextBoxSubChannel.Attributes.Add("onblur", "GetCompanyData(" + 37 + ",'" + TextBoxSubChannel.ClientID + "','" + TextBoxSubChannelDes.ClientID + "')");
            TextBoxArea.Attributes.Add("onblur", "GetCompanyData(" + 38 + ",'" + TextBoxArea.ClientID + "','" + TextBoxAreaDes.ClientID + "')");
            TextBoxRegion.Attributes.Add("onblur", "GetCompanyData(" + 39 + ",'" + TextBoxRegion.ClientID + "','" + TextBoxRegionDes.ClientID + "')");
            TextBoxZone.Attributes.Add("onblur", "GetCompanyData(" + 40 + ",'" + TextBoxZone.ClientID + "','" + TextBoxZoneDes.ClientID + "')");
            TextBoxLocation.Attributes.Add("onblur", "GetCompanyData(" + 41 + ",'" + TextBoxLocation.ClientID + "','" + TextBoxLocationDes.ClientID + "')");

            TextBoxCompany.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 35 + ",'" + TextBoxCompany.ClientID + "','" + TextBoxCompanyDes.ClientID + "')");
            TextBoxChannel.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 36 + ",'" + TextBoxChannel.ClientID + "','" + TextBoxChannelDes.ClientID + "')");
            TextBoxSubChannel.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 37 + ",'" + TextBoxSubChannel.ClientID + "','" + TextBoxSubChannelDes.ClientID + "')");
            TextBoxArea.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 38 + ",'" + TextBoxArea.ClientID + "','" + TextBoxAreaDes.ClientID + "')");
            TextBoxRegion.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 39 + ",'" + TextBoxRegion.ClientID + "','" + TextBoxRegionDes.ClientID + "')");
            TextBoxZone.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 40 + ",'" + TextBoxZone.ClientID + "','" + TextBoxZoneDes.ClientID + "')");
            TextBoxLocation.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 41 + ",'" + TextBoxLocation.ClientID + "','" + TextBoxLocationDes.ClientID + "')");

            TextBoxCompany.Attributes.Add("onKeyup", "return clickButton(event,'" + imgComSearch.ClientID + "')");
            TextBoxChannel.Attributes.Add("onKeyup", "return clickButton(event,'" + imgChaSearch.ClientID + "')");
            TextBoxSubChannel.Attributes.Add("onKeyup", "return clickButton(event,'" + imgSubChaSearch.ClientID + "')");
            TextBoxArea.Attributes.Add("onKeyup", "return clickButton(event,'" + imgAreaSearch.ClientID + "')");
            TextBoxRegion.Attributes.Add("onKeyup", "return clickButton(event,'" + imgRegionSearch.ClientID + "')");
            TextBoxZone.Attributes.Add("onKeyup", "return clickButton(event,'" + imgZoneSearch.ClientID + "')");
            TextBoxLocation.Attributes.Add("onKeyup", "return clickButton(event,'" + imgLocSearch.ClientID + "')");

        }

        protected void imgItemSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc =(uc_CommonSearch) pp.Master.FindControl("uc_CommonSearchMaster");
 
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxCompany.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgChaSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxChannel.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgSubChaSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxSubChannel.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgAreaSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxArea.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgRegionSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxRegion.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgZoneSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxZone.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgProCeSearch_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxLocation.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new BasePage();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator +"" + seperator + "" + seperator + "" + seperator + "" + seperator +"" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString()+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator +"" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region: 
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + "" + seperator +"" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + TextBoxZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

    }
}