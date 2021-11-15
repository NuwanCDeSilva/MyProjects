using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.AbansTours.UserControls;

namespace FF.AbansTours
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        BasePage _basePage = null;
        int Option = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    if (!IsPostBack)
                    {
                        lblUser.Text = Session["UserID"].ToString();
                        lblCompany.Text = Session["UserCompanyCode"].ToString();
                        lblPC.Text = Session["UserDefProf"].ToString();
                        lblLoc.Text = Session["UserDefLoca"].ToString();
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    //string gotoURL = "login.aspx";
                    //Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                    Response.Redirect("~/login.aspx");
                }
            }
            catch (Exception)
            {
                throw;
            }
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

        protected void lblLoc_Click(object sender, EventArgs e)
        {
            _basePage = new BasePage();
            uc_CommonSearchMaster.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetUserLocationSearchData(uc_CommonSearchMaster.SearchParams, null, null);
            uc_CommonSearchMaster.BindUCtrlDDLData(dataSource);
            uc_CommonSearchMaster.BindUCtrlGridData(dataSource);
            uc_CommonSearchMaster.ReturnResultControl = lblLoc.ClientID;
            uc_CommonSearchMaster.UCModalPopupExtender.Show();
        }

        protected void lblPC_Click(object sender, EventArgs e)
        {
            _basePage = new BasePage();
            uc_CommonSearchMaster.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(uc_CommonSearchMaster.SearchParams, null, null);
            uc_CommonSearchMaster.BindUCtrlDDLData(dataSource);
            uc_CommonSearchMaster.BindUCtrlGridData(dataSource);
            uc_CommonSearchMaster.ReturnResultControl = lblPC.ClientID;
            uc_CommonSearchMaster.UCModalPopupExtender.Show();
        }

        protected void lblUser_Click(object sender, EventArgs e)
        {
            ClearSesstions();
            Response.Redirect("~/login.aspx");
        }

        protected void lblLogOut_Click(object sender, EventArgs e)
        {
            _basePage = new BasePage();
            int result = _basePage.CHNLSVC.Security.ExitLoginSession(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["SessionID"].ToString());
            ClearSesstions();
            Response.Redirect("~/login.aspx");
        }

        private void ClearSesstions()
        {
            Session.RemoveAll();
            Session.Clear();
        }
    }
}
