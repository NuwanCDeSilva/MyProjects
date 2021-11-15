using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace FF.AbansTours.UserControls
{
	public partial class uc_ProfitCenterSearch : System.Web.UI.UserControl
	{
        BasePage _basePage = null;
        #region properties

        public string Company
        {
            get { return TextBoxCompany.Text; }
            set { TextBoxCompany.Text = value; }
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
                TextBoxCompany.Text = _basePage.GlbUserComCode;
                TextBoxCompanyDes.Text = _basePage.GlbUserComDesc;
                TextBoxLocation.Text = _basePage.GlbUserDefProf;
                
                BindJavaScript();
            }
		}
        private void BindJavaScript()
        {
            TextBoxCompany.Attributes.Add("onblur", "GetCompanyData(" + 69 + ",'" + TextBoxCompany.ClientID + "','" + TextBoxCompanyDes.ClientID + "')");
            TextBoxChannel.Attributes.Add("onblur", "GetCompanyData(" + 70 + ",'" + TextBoxChannel.ClientID + "','" + TextBoxChannelDes.ClientID + "')");
            TextBoxSubChannel.Attributes.Add("onblur", "GetCompanyData(" + 71 + ",'" + TextBoxSubChannel.ClientID + "','" + TextBoxSubChannelDes.ClientID + "')");
            TextBoxArea.Attributes.Add("onblur", "GetCompanyData(" + 72 + ",'" + TextBoxArea.ClientID + "','" + TextBoxAreaDes.ClientID + "')");
            TextBoxRegion.Attributes.Add("onblur", "GetCompanyData(" + 73 + ",'" + TextBoxRegion.ClientID + "','" + TextBoxRegionDes.ClientID + "')");
            TextBoxZone.Attributes.Add("onblur", "GetCompanyData(" + 74 + ",'" + TextBoxZone.ClientID + "','" + TextBoxZoneDes.ClientID + "')");
            TextBoxLocation.Attributes.Add("onblur", "GetCompanyData(" + 75 + ",'" + TextBoxLocation.ClientID + "','" + TextBoxLocationDes.ClientID + "')");

            TextBoxCompany.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 69 + ",'" + TextBoxCompany.ClientID + "','" + TextBoxCompanyDes.ClientID + "')");
            TextBoxChannel.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 70 + ",'" + TextBoxChannel.ClientID + "','" + TextBoxChannelDes.ClientID + "')");
            TextBoxSubChannel.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 71 + ",'" + TextBoxSubChannel.ClientID + "','" + TextBoxSubChannelDes.ClientID + "')");
            TextBoxArea.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 72 + ",'" + TextBoxArea.ClientID + "','" + TextBoxAreaDes.ClientID + "')");
            TextBoxRegion.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 73 + ",'" + TextBoxRegion.ClientID + "','" + TextBoxRegionDes.ClientID + "')");
            TextBoxZone.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 74 + ",'" + TextBoxZone.ClientID + "','" + TextBoxZoneDes.ClientID + "')");
            TextBoxLocation.Attributes.Add("onchange", "GetCompanyDataTextChange(" + 75 + ",'" + TextBoxLocation.ClientID + "','" + TextBoxLocationDes.ClientID + "')");
        }
        public void Descript_Visible(bool trueOrFalse)
        {            
            TextBoxAreaDes.Visible = trueOrFalse;
            TextBoxChannelDes.Visible = trueOrFalse;
            TextBoxCompanyDes.Visible = trueOrFalse;
            TextBoxLocationDes.Visible = trueOrFalse;
            TextBoxRegionDes.Visible = trueOrFalse;
            TextBoxSubChannelDes.Visible = trueOrFalse;
            TextBoxZoneDes.Visible = trueOrFalse;
        
        }
        public void Clear()
        { 
            Company="";
            CompanyDes="";
            Channel="";
            SubChannel="";
            Area="";
            Region="";
            Zone="";
            ProfitCenter = "";
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new BasePage();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + TextBoxZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void imgComSearch_Click(object sender, ImageClickEventArgs e)//company
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxCompany.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgChaSearch_Click(object sender, ImageClickEventArgs e)//channel
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxChannel.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgSubChaSearch_Click(object sender, ImageClickEventArgs e)//sub channel
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxSubChannel.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgAreaSearch_Click(object sender, ImageClickEventArgs e)//area
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxArea.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgRegionSearch_Click(object sender, ImageClickEventArgs e)//region
        {

            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxRegion.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgZoneSearch_Click(object sender, ImageClickEventArgs e)//zone
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxZone.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgLocSearch_Click(object sender, ImageClickEventArgs e)//pc
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxLocation.ClientID;
            ucc.UCModalPopupExtender.Show();
        }
	}
}