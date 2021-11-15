using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.UserControls
{
    public partial class ucItemSerialView : UserControl
    {
        Base _basePage;
        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        //public bool IsVisible
        //{
        //    get { return (bool)ViewState["isVisible"]; }
        //    set { ViewState["isVisible"] = value; }
        //}

        private string iTEM_CODE;

        public string ITEM_CODE
        {
            get { return iTEM_CODE; }
            set { iTEM_CODE = value; }
        }
        //public string ITEM_CODE
        //{
        //    get { return (string)ViewState["item_code"]; }
        //    set { ViewState["item_code"] = value; }
        //}
        private string iTEM_STATUS;

        public string ITEM_STATUS
        {
            get { return iTEM_STATUS; }
            set { iTEM_STATUS = value; }
        }
        //public string ITEM_STATUS
        //{
        //    get { return (string)ViewState["item_status"]; }
        //    set { ViewState["item_status"] = value; }
        //}
        private string cOMPANY;

        public string COMPANY
        {
            get { return cOMPANY; }
            set { cOMPANY = value; }
        }
        //public string COMPANY
        //{
        //    get { return (string)ViewState["company"]; }
        //    set { ViewState["company"] = value; }
        //}
        private string lOC;

        public string LOC
        {
            get { return lOC; }
            set { lOC = value; }
        }
        //public string LOC
        //{
        //    get { return (string)ViewState["loc"]; }
        //    set { ViewState["loc"] = value; }
        //}
        private string cHANNEL;

        public string CHANNEL
        {
            get { return cHANNEL; }
            set { cHANNEL = value; }
        }
        //public string CHANNEL
        //{
        //    get { return (string)ViewState[" channel"]; }
        //    set { ViewState[" channel"] = value; }
        //}
        private string sUB_CHANNEL;

        public string SUB_CHANNEL
        {
            get { return sUB_CHANNEL; }
            set { sUB_CHANNEL = value; }
        }
        //public string SUB_CHANNEL
        //{
        //    get { return (string)ViewState["sub_channel"]; }
        //    set { ViewState["sub_channel"] = value; }
        //}
        private string aREA;

        public string AREA
        {
            get { return aREA; }
            set { aREA = value; }
        }
        //public string AREA
        //{
        //    get { return (string)ViewState["area"]; }
        //    set { ViewState["area"] = value; }
        //}
        private string zONE;

        public string ZONE
        {
            get { return zONE; }
            set { zONE = value; }
        }
        //public string ZONE
        //{
        //    get { return (string)ViewState["zone"]; }
        //    set { ViewState["zone"] = value; }
        //}
        private string rEAGION;

        public string REAGION
        {
            get { return rEAGION; }
            set { rEAGION = value; }
        }
        //public string REAGION
        //{
        //    get { return (string)ViewState["region"]; }
        //    set { ViewState["region"] = value; }
        //}
        private string tYPE;

        public string TYPE
        {
            get { return tYPE; }
            set { tYPE = value; }
        }

        private bool showCost;

        public bool ShowCost
        {
            get { return showCost; }
            set { showCost = value; }
        }
        //public string TYPE
        //{
        //    get { return (string)ViewState["type"]; }
        //    set { ViewState["type"] = value; }
        //}


        public ucItemSerialView()
        {
            _basePage = new Base();
            InitializeComponent();

            IsVisible = false;
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
            ShowCost = false;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new Base();
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
            //_basePage = new Base();
            ////Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //if (IsVisible)
            //{
            //    ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Documents);
            //    DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetItemDocSearchData(ucc.SearchParams);
            //    GridViewDocuments.DataSource = dataSource;
            //    GridViewDocuments.DataBind();

            //    ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
            //    DataTable dataSource1 = _basePage.CHNLSVC.CommonSearch.GetItemSerialSearchData(ucc.SearchParams);
            //    GridViewSerials.DataSource = dataSource1;
            //    GridViewSerials.DataBind();
            //}

            ////-----------------------------------------------------------------------------------------------
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            if (IsVisible)
            {
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Documents);
                //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetItemDocSearchData(_CommonSearch.SearchParams);
                //GridViewDocuments.DataSource = null;
                //GridViewDocuments.AutoGenerateColumns = false;
                //GridViewDocuments.DataSource = _result;
                //GridViewDocuments.DataBind();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result2 = _basePage.CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);

                GridViewSerials.DataSource = null;
                GridViewSerials.AutoGenerateColumns = false;
                GridViewSerials.DataSource = _result2;
                serials = true;

                if (ShowCost)
                {
                    GridViewDocuments.Columns["Unit_Cost"].Visible = true;
                    GridViewSerials.Columns["_cost"].Visible = true;
                    // DivTotal.Visible = true;
                }
                else
                {
                    GridViewDocuments.Columns["Unit_Cost"].Visible = false;
                    GridViewSerials.Columns["_cost"].Visible = false;
                }


                //change row color
                foreach (DataGridViewRow gvr in GridViewSerials.Rows)
                {
                    int available = Convert.ToInt32(gvr.Cells["_available"].Value);
                    if (available == -1)
                    {
                        gvr.DefaultCellStyle.BackColor = Color.Red;
                        gvr.Cells["_avail"].Value = "Yes";
                        //gvr.Cells["_avail"].Style.ForeColor = Color.Red;
                    }
                    else {
                        gvr.Cells["_avail"].Value = "No";
                        //gvr.Cells["_avail"].Style.ForeColor = Color.Green;
                    }
                }
       
            }
        }
        bool serials = false;
        bool doc = false;
        bool tempIsuue = false;

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            if (!serials) {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result2 = _basePage.CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);

                GridViewSerials.DataSource = null;
                GridViewSerials.AutoGenerateColumns = false;
                GridViewSerials.DataSource = _result2;
                serials = true;

                if (ShowCost)
                {
                    GridViewDocuments.Columns["Unit_Cost"].Visible = true;
                    GridViewSerials.Columns["_cost"].Visible = true;
                    // DivTotal.Visible = true;
                }
                else
                {
                    GridViewDocuments.Columns["Unit_Cost"].Visible = false;
                    GridViewSerials.Columns["_cost"].Visible = false;
                }


                //change row color
                foreach (DataGridViewRow gvr in GridViewSerials.Rows)
                {
                    int available = Convert.ToInt32(gvr.Cells["_available"].Value);
                    if (available == -1)
                    {
                        gvr.DefaultCellStyle.BackColor = Color.Red;
                        gvr.Cells["_avail"].Value = "Yes";
                        //gvr.Cells["_avail"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        gvr.Cells["_avail"].Value = "No";
                        //gvr.Cells["_avail"].Style.ForeColor = Color.Green;
                    }
                }
                serials = true;
            }
            if (!doc) {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Documents);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetItemDocSearchData(_CommonSearch.SearchParams);
                GridViewDocuments.DataSource = null;
                GridViewDocuments.AutoGenerateColumns = false;
                GridViewDocuments.DataSource = _result;
                doc = true;
            }
  
                List<Service_TempIssue> _tempIssueItms = new List<Service_TempIssue>();
                _tempIssueItms = _basePage.CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, "", -999, ITEM_CODE, BaseCls.GlbUserDefProf, "TMPI");
                grvTempIssue.AutoGenerateColumns = false;
                grvTempIssue.DataSource = _tempIssueItms;
                tempIsuue = true;
            
        }

        

    }
}
