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
    public partial class uc_ItemSerialView : System.Web.UI.UserControl
    {
        public bool IsVisible
        {
            get { return (bool)ViewState["isVisible"]; }
            set { ViewState["isVisible"] = value; }
        }

        public string ITEM_CODE {
            get { return (string)ViewState["item_code"]; }
            set { ViewState["item_code"] = value; }
        }

        public string ITEM_STATUS
        {
            get { return (string)ViewState[ "item_status"]; }
            set { ViewState["item_status"] = value; }
        }

        public string COMPANY
        {
            get { return (string)ViewState["company"]; }
            set { ViewState["company"] = value; }
        }
        public string LOC
        {
            get { return (string)ViewState["loc"]; }
            set { ViewState["loc"] = value; }
        }

        public string CHANNEL
        {
            get { return (string)ViewState[" channel"]; }
            set { ViewState[" channel"] = value; }
        }
        public string SUB_CHANNEL
        {
            get { return (string)ViewState["sub_channel"]; }
            set { ViewState["sub_channel"] = value; }
        }
        public string AREA
        {
            get { return  (string)ViewState["area"]; }
            set { ViewState["area"] = value; }
        }
        public string ZONE
        {
            get { return (string)ViewState[ "zone"]; }
            set { ViewState["zone"] = value; }
        }
        public string REAGION
        {
            get { return  (string)ViewState["region"]; }
            set { ViewState["region"] = value; }
        }
        public string TYPE
        {
            get { return  (string)ViewState["type"]; }
            set { ViewState["type"] = value; }
        }


        BasePage _basePage = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { IsVisible = false;
            ITEM_CODE = string.Empty;
            ITEM_STATUS = string.Empty;
            COMPANY = string.Empty;
            CHANNEL = string.Empty;
            SUB_CHANNEL = string.Empty;
            AREA = string.Empty;
            ZONE = string.Empty;
            REAGION = string.Empty;
            LOC = string.Empty;
            TYPE = string.Empty;
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
                case CommonUIDefiniton.SearchUserControlType.Item_Documents:
                    {
                        paramsText.Append(ITEM_CODE + seperator + ITEM_STATUS + seperator + COMPANY + seperator + CHANNEL + seperator + SUB_CHANNEL + seperator + AREA + seperator + REAGION + seperator + ZONE + seperator + LOC + seperator + TYPE + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(ITEM_CODE + seperator + ITEM_STATUS + seperator + COMPANY + seperator + CHANNEL + seperator + SUB_CHANNEL + seperator + AREA + seperator + REAGION + seperator + ZONE + seperator + LOC + seperator + TYPE + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        public void Display()
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            if (IsVisible)
            {
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Documents);
                DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetItemDocSearchData(ucc.SearchParams);
                GridViewDocuments.DataSource = dataSource;
                GridViewDocuments.DataBind();

                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable dataSource1 = _basePage.CHNLSVC.CommonSearch.GetItemSerialSearchData(ucc.SearchParams);
                GridViewSerials.DataSource = dataSource1;
                GridViewSerials.DataBind();
            }
        }
        }
}