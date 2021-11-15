using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.WebERPClient.UserControls;
using System.Text;
using System.Data;


namespace FF.WebERPClient
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        BasePage _basePage = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            _basePage = new BasePage();
            if (!Page.IsPostBack)
            {
                SetControlDefaultValues(_basePage.GlbUserDefLoca, _basePage.GlbUserDefProf);
            }

            if (string.IsNullOrEmpty(_basePage.GlbUserName) && _basePage.GlbisLoging == false)
            {
                Response.Redirect("~/ErrorPage.aspx");

            }

        }

        public void SetControlDefaultValues(string _defaultLocation, string _defaultProfitCenter)
        {
            divLocations.Visible = false;
            divProfitCenters.Visible = false;

            if (!string.IsNullOrEmpty(_defaultLocation))
            {
                divLocations.Visible = true;
                txtMasterUserLocation.Text = _defaultLocation;
            }

            if (!string.IsNullOrEmpty(_defaultProfitCenter))
            {
                divProfitCenters.Visible = true;
                txtMasterProfitCenters.Text = _defaultProfitCenter;
            }

        }

        protected void imgbtnSearchLocation_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();

            uc_CommonSearchMaster.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetUserLocationSearchData(uc_CommonSearchMaster.SearchParams, null, null);

            uc_CommonSearchMaster.BindUCtrlDDLData(dataSource);
            uc_CommonSearchMaster.BindUCtrlGridData(dataSource);
            uc_CommonSearchMaster.ReturnResultControl = txtMasterUserLocation.ClientID;
            uc_CommonSearchMaster.UCModalPopupExtender.Show();
        }

        protected void imgbtnSearchProfitCenter_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();

            uc_CommonSearchMaster.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(uc_CommonSearchMaster.SearchParams, null, null);

            uc_CommonSearchMaster.BindUCtrlDDLData(dataSource);
            uc_CommonSearchMaster.BindUCtrlGridData(dataSource);
            uc_CommonSearchMaster.ReturnResultControl = txtMasterProfitCenters.ClientID;
            uc_CommonSearchMaster.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + _basePage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + _basePage.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

    }
}
