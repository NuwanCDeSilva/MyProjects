using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.TempObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Services
{
    public partial class ServiceWorkingProcess : BasePage
    {
        private Service_Chanal_parameter _scvParam = null;
        MST_ITM_CAT_COMP oCateComp
        {
            get { if (Session["oCateComp"] != null) { return (MST_ITM_CAT_COMP)Session["oCateComp"]; } else { return new MST_ITM_CAT_COMP(); } }
            set { Session["oCateComp"] = value; }
        }
        bool _isDecimalAllow
        {
            get { if (Session["_isDecimalAllow"] != null) { return (bool)Session["_isDecimalAllow"]; } else { return false; } }
            set { Session["_isDecimalAllow"] = value; }
        }
        bool isWarehouseSearch
        {
            get { if (Session["isWarehouseSearch"] != null) { return (bool)Session["isWarehouseSearch"]; } else { return false; } }
            set { Session["isWarehouseSearch"] = value; }
        }
        List<Service_OldPartRemove> MailList
        {
            get { if (Session["MailList"] != null) { return (List<Service_OldPartRemove>)Session["MailList"]; } else { return new List<Service_OldPartRemove>(); } }
            set { Session["MailList"] = value; }
        }
        List<Service_job_Det> oItms
        {
            get { if (Session["oItms"] != null) { return (List<Service_job_Det>)Session["oItms"]; } else { return new List<Service_job_Det>(); } }
            set { Session["oItms"] = value; }
        }
        Service_JOB_HDR oHeader
        {
            get { if (Session["oHeader"] != null) { return (Service_JOB_HDR)Session["oHeader"]; } else { return new Service_JOB_HDR(); } }
            set { Session["oHeader"] = value; }
        }
        Service_job_Det oItem
        {
            get { if (Session["oItem"] != null) { return (Service_job_Det)Session["oItem"]; } else { return new Service_job_Det(); } }
            set { Session["oItem"] = value; }
        }
        bool AddOldItemAuto
        {
            get { if (Session["AddOldItemAuto"] != null) { return (bool)Session["AddOldItemAuto"]; } else { return false; } }
            set { Session["AddOldItemAuto"] = value; }
        }
        Int32 isActionTaken
        {
            get { if (Session["isActionTaken"] != null) { return (Int32)Session["isActionTaken"]; } else { return 0; } }
            set { Session["isActionTaken"] = value; }
        }
        bool IsHavingGitItems
        {
            get { if (Session["IsHavingGitItems"] != null) { return (bool)Session["IsHavingGitItems"]; } else { return false; } }
            set { Session["IsHavingGitItems"] = value; }
        }
        bool popOldPartRemShow
        {
            get { if (Session["popOldPartRemShow"] != null) { return (bool)Session["popOldPartRemShow"]; } else { return false; } }
            set { Session["popOldPartRemShow"] = value; }
        }
        bool IsOldItemAdded
        {
            get { if (Session["IsOldItemAdded"] != null) { return (bool)Session["IsOldItemAdded"]; } else { return false; } }
            set { Session["IsOldItemAdded"] = value; }
        }
        string _pnlSelectTag
        {
            get { if (Session["_pnlSelectTag"] != null) { return (string)Session["_pnlSelectTag"]; } else { return ""; } }
            set { Session["_pnlSelectTag"] = value; }
        }
        string _serial
        {
            get { if (Session["_serial"] != null) { return (string)Session["_serial"]; } else { return ""; } }
            set { Session["_serial"] = value; }
        }
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        string _toolTip
        {
            get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
            set { Session["_toolTip"] = value; }
        }
        bool _ava
        {
            get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
            set { Session["_ava"] = value; }
        }
        string _para = "";
        List<MasterItemStatus> _statusList
        {
            get { if (Session["_statusList"] != null) { return (List<MasterItemStatus>)Session["_statusList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_statusList"] = value; }
        }
        List<InventoryRequestItem> _invReqItemList
        {
            get { if (Session["_invReqItemList"] != null) { return (List<InventoryRequestItem>)Session["_invReqItemList"]; } else { return new List<InventoryRequestItem>(); } }
            set { Session["_invReqItemList"] = value; }
        }
        MasterItem _itemdetail
        {
            get { if (Session["_itemdetail"] != null) { return (MasterItem)Session["_itemdetail"]; } else { return new MasterItem(); } }
            set { Session["_itemdetail"] = value; }
        }
        string _selectedJobNo
        {
            get { if (Session["_selectedJobNo"] != null) { return (string)Session["_selectedJobNo"]; } else { return ""; } }
            set { Session["_selectedJobNo"] = value; }
        }
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        bool _popJobCloseShow
        {
            get { if (Session["_popJobCloseShow"] != null) { return (bool)Session["_popJobCloseShow"]; } else { return false; } }
            set { Session["_popJobCloseShow"] = value; }
        }
        bool _popReqShow
        {
            get { if (Session["_popReqShow"] != null) { return (bool)Session["_popReqShow"]; } else { return false; } }
            set { Session["_popReqShow"] = value; }
        }
        Int32 _selectedJobLine
        {
            get { if (Session["_selectedJobLine"] != null) { return (Int32)Session["_selectedJobLine"]; } else { return 0; } }
            set { Session["_selectedJobLine"] = value; }
        }
        bool _serPopShowDt
        {
            get { if (Session["_serPopShowDt"] != null) { return (bool)Session["_serPopShowDt"]; } else { return false; } }
            set { Session["_serPopShowDt"] = value; }
        }
        List<TmpServiceWorkingProcess> _jobItmList
        {
            get
            {
                if (Session["_jobItmList"] != null)
                {
                    return (List<TmpServiceWorkingProcess>)Session["_jobItmList"];
                }
                else
                {
                    return new List<TmpServiceWorkingProcess>();
                }
            }
            set { Session["_jobItmList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Base _tmpBase = new Base();
                _tmpBase.LoadLocationDetail();
                Service_Chanal_parameter _Parameters = null;
                _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                if (_Parameters != null)
                {
                    if (_Parameters.SP_ISNEEDWIP != 1)
                    {
                        popLocationError.Show();
                    }
                }
                else
                {
                    popLocationError.Show();
                }
                ClearData();
            }
            else
            {
                if (_serPopShow)
                {
                    PopupSearch.Show();
                }
                else
                {
                    PopupSearch.Hide();
                }
                if (_serPopShowDt)
                {
                    PopupSearchDT.Show();
                }
                else
                {
                    PopupSearch.Hide();
                }
                if (_popReqShow)
                {
                    popRequest.Show();
                }
                else
                {
                    popRequest.Hide();
                }
                if (_popJobCloseShow)
                {
                    popJobClose.Show();
                }
                else
                {
                    popJobClose.Hide();
                }
                if (popOldPartRemShow)
                {
                    popOldPartRem.Show();
                }
                else
                {
                    popOldPartRem.Hide();
                }
            }
        }

        private void ClearData()
        {
            isWarehouseSearch = false;
            oCateComp = new MST_ITM_CAT_COMP();
            _isDecimalAllow = false;
            oItem = new Service_job_Det();
            AddOldItemAuto = false;
            popOldPartRemShow = false;
            loadCloseTypes();
            IsHavingGitItems = false;
            IsOldItemAdded = false;
            _serData = new DataTable();
            _serType = "";
            _pnlSelectTag ="";
            _popJobCloseShow = false;
            _serPopShow = false;
            _serPopShowDt = false;
            _popReqShow = false;
            _para = "";
            _jobItmList = new List<TmpServiceWorkingProcess>();
            _invReqItemList = new List<InventoryRequestItem>();
            MailList = new List<Service_OldPartRemove>();
            dgvJobItem.DataSource = new int[] { };
            dgvJobItem.DataBind();
            _statusList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            _statusList = _statusList.OrderBy(c => c.Mis_desc).ToList();
            while (ddlMrnItmStus.Items.Count >1)
            {
                ddlMrnItmStus.Items.RemoveAt(1);
            }
            ddlMrnItmStus.DataSource = _statusList;
            ddlMrnItmStus.DataTextField = "Mis_desc";
            ddlMrnItmStus.DataValueField = "Mis_cd";
            ddlMrnItmStus.DataBind();
            while (ddlOPRItemSts.Items.Count > 1)
            {
                ddlOPRItemSts.Items.RemoveAt(1);
            }
            var v = _statusList.Where(c => c.Mis_cd == "OLDPT").ToList();
            ddlOPRItemSts.DataSource = v ;
            ddlOPRItemSts.DataTextField = "Mis_desc";
            ddlOPRItemSts.DataValueField = "Mis_cd";
            ddlOPRItemSts.DataBind();

            fillStages();
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
        }
        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + _selectedJobNo + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EetimateByJob:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + _selectedJobNo + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP:
                    {

                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10816))
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "3,6,4,5.1" + seperator + "GET_ALL_JOBS" + seperator);
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + ddlJobStage.SelectedValue + seperator + "GET_ALL_JOBS" + seperator + Session["UserDefProf"].ToString() + seperator);
                            break;
                        }
                        else
                        {
                            //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "3,6,4,5.1" + seperator + Session["UserID"].ToString() + seperator);
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + ddlJobStage.SelectedValue + seperator + Session["UserID"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                            break;
                        }

                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc:
                    {
                        if (isWarehouseSearch == true)
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "S" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "O" + seperator);
                            break;
                        }
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void DataAvailable(DataTable _dt, string _valText, string _valCol, string _valToolTipCol = "")
        {
            _ava = false;
            _toolTip = string.Empty;
            foreach (DataRow row in _dt.Rows)
            {
                if (!string.IsNullOrEmpty(row[_valCol].ToString()))
                {
                    if (_valText == row[_valCol].ToString())
                    {
                        _ava = true;
                        if (!string.IsNullOrEmpty(_valToolTipCol))
                        {
                            _toolTip = row[_valToolTipCol].ToString();
                        }
                        break;
                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PopupSearch.Hide();
            _serPopShow = false;
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "ServiceWIPMRN_Loc")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "EetimateByJob")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EetimateByJob);
                    _serData = CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "InvoiceItemUnAssableByModel")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "InvoiceItemUnAssable")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    _serData = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "AODLocation")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKey.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
                _serPopShow = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "ServiceWIPMRN_Loc")
                {
                    txtDispatchRequried.Text = code;
                    txtDispatchRequried_TextChanged(null, null);
                }
                else if (_serType == "WarehouseNewItm")
                {
                    txtNewPartWarehouse.Text = code;
                    txtNewPartWarehouse_TextChanged(null, null);
                }
                else if (_serType == "WarehouseOld")
                {
                    txtOPWarehouse.Text = code;
                    txtOPWarehouse_TextChanged(null, null);
                }
                else if (_serType == "EetimateByJob")
                {
                    //txtEstimate.Text = code;
                    //txtEstimate_TextChanged(null, null);
                }
                else if (_serType == "InvoiceItemUnAssableByModel")
                {
                    txtMrnItm.Text = code;
                    txtMrnItm_TextChanged(null, null);
                }
                else if (_serType == "ServiceJobSearchWIP")
                {
                    txtDispatchRequried.Text = code;
                    txtDispatchRequried_TextChanged(null, null);
                }
                else if (_serType == "InvoiceItemUnAssable")
                {
                    txtOPRItem.Text = code;
                    txtOPRItem_TextChanged(null, null);
                }
                else if (_serType == "AODLocation")
                {
                    txtAodLocation.Text = code;
                    txtAodLocation_TextChanged(null, null);
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
        private void fillStages()
        {
            while (ddlJobStage.Items.Count > 0)
            {
                ddlJobStage.Items.RemoveAt(0);
            }
            ddlJobStage.Items.Add(new ListItem("All", "3,2,6,4,5.1,5"));
            ddlJobStage.Items.Add(new ListItem("NEW JOB OPEN", "2"));
            ddlJobStage.Items.Add(new ListItem("TECHNICIAN ALLOCATED", "3"));
            ddlJobStage.Items.Add(new ListItem("JOB STARTED - TECHNICIAN", "4"));
            ddlJobStage.Items.Add(new ListItem("JOB REOPENED - TECHNICIAN", "5"));
            ddlJobStage.Items.Add(new ListItem("JOB COMMENTED - TECHNICIAN", "6"));
            ddlJobStage.SelectedIndex = 0;
        }

        protected void ddlJobStage_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lbtnSeJobNo_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP);
                _serData = CHNLSVC.CommonSearch.GetServiceJobsWIPWEB(_para, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
                txtFDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                if (_serData.Columns.Contains("ADDRESS2"))
                {
                    _serData.Columns.Remove("ADDRESS2");
                }
                LoadSearchPopupDT("ServiceJobSearchWIP", "SEARCH KEY", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnSearchDt_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "ServiceJobSearchWIP")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP);
                    _serData = CHNLSVC.CommonSearch.GetServiceJobsWIP(_para, "JOB", txtSerByKeyDt.Text.Trim(), Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
                    if (_serData.Columns.Contains("ADDRESS2"))
                    {
                        _serData.Columns.Remove("ADDRESS2");
                    }
                }
                else if (_serType == "CPCompany")
                {
                   
                } 
                dgvResultDt.DataSource = new int[] { };
                if (dgvResultDt.PageIndex > 0)
                { dgvResultDt.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResultDt.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKeyDt.Focus();
                dgvResultDt.DataBind();
                PopupSearchDT.Show();
                _serPopShowDt = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void dgvResultDt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultDt.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResultDt.DataSource = _serData;
                }
                else
                {
                    dgvResultDt.DataSource = new int[] { };
                }
                dgvResultDt.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResultDt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResultDt.SelectedRow.Cells[1].Text;
                if (_serType == "ServiceJobSearchWIP")
                {
                    code = dgvResultDt.SelectedRow.Cells[2].Text;
                    txtJobNo.Text = code;
                    txtJobNo_TextChanged(null, null);
                }
                else if (_serType == "InvoiceItemUnAssableByModel")
                {
                    code = dgvResultDt.SelectedRow.Cells[2].Text;
                    txtMrnItm.Text = code;
                    txtMrnItm_TextChanged(null, null);
                }
                else if (_serType == "MRN")
                {
                    code = dgvResultDt.SelectedRow.Cells[1].Text;
                    txtRequest.Text = code;
                    txtRequest_TextChanged(null, null);
                }
                else if (_serType == "InvoiceItemUnAssable")
                {
                    code = dgvResultDt.SelectedRow.Cells[1].Text;
                    txtOPRItem.Text = code;
                    txtOPRItem_TextChanged(null, null);
                }
                _serPopShowDt = false;
                PopupSearchDT.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            if (_dataSource.Columns.Contains("From Date"))
            {
                _dataSource.Columns.Remove("From Date");
            }
            if (_dataSource.Columns.Contains("To Date"))
            {
                _dataSource.Columns.Remove("To Date");
            }
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }
        public void BindDdlSerByKeyDT(DataTable _dataSource)
        {
            if (_dataSource.Columns.Contains("Date"))
            {
                _dataSource.Columns.Remove("Date");
            }
            if (_dataSource.Columns.Contains("From Date"))
            {
                _dataSource.Columns.Remove("From Date");
            }
            if (_dataSource.Columns.Contains("To Date"))
            {
                _dataSource.Columns.Remove("To Date");
            }
            this.ddlSerByKeyDt.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKeyDt.Items.Add(col.ColumnName);
            }

            this.ddlSerByKeyDt.SelectedIndex = 0;
        }
        private void LoadSearchPopup(string serType, string _colName, string _ordTp, bool _isOrder = true)
        {
            if (_isOrder)
            {
                OrderSearchGridData(_colName, _ordTp);
            }
            try
            {
                dgvResult.DataSource = new int[] { };
                dgvResult.DataBind();
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        dgvResult.DataBind();
                        BindDdlSerByKey(_serData);
                    }
                }
                txtSerByKey.Text = "";
                txtSerByKey.Focus();
                _serType = serType;
                PopupSearch.Show();
                _serPopShow = true;
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void LoadSearchPopupDT(string serType, string _colName, string _ordTp, bool _isOrder = true)
        {
            if (_isOrder)
            {
                OrderSearchGridData(_colName, _ordTp);
            }
            try
            {
                dgvResultDt.DataSource = new int[] { };
                dgvResultDt.DataBind();
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResultDt.DataSource = _serData;
                        dgvResultDt.DataBind();
                        BindDdlSerByKeyDT(_serData);
                    }
                }
                txtSerByKeyDt.Text = "";
                txtSerByKeyDt.Focus();
                _serType = serType;
                PopupSearchDT.Show();
                _serPopShowDt = true;
                if (dgvResultDt.PageIndex > 0)
                { dgvResultDt.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void btnCloseDt_Click(object sender, EventArgs e)
        {
            PopupSearchDT.Hide();
            _serPopShowDt = false;
        }
        private void ClearJobData()
        {
            _jobItmList = new List<TmpServiceWorkingProcess>();
            dgvJobItem.DataSource = new int[] { };
            dgvJobItem.DataBind();
        }
        protected void txtJobNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    getJobJetails();
                }
                else
                {
                    ClearJobData();
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }
        private void getJobJetails()
        {
            _jobItmList = new List<TmpServiceWorkingProcess>();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            //stage = "3,2,6,4,5.1,5";
            stage = ddlJobStage.SelectedValue.ToString();
            //DtDetails = CHNLSVC.CustService.GetTechAllocJobs(Session["UserCompanyCode"].ToString(), Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, Session["UserDefProf"].ToString());

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10816))
            {
                _jobItmList = CHNLSVC.CustService.GetJObsFOrWIPWeb(Session["UserCompanyCode"].ToString(), Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, Session["UserDefProf"].ToString(), "GET_ALL_JOBS");
            }
            else
            {
                _jobItmList = CHNLSVC.CustService.GetJObsFOrWIPWeb(Session["UserCompanyCode"].ToString(), Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, Session["UserDefProf"].ToString(), Session["UserID"].ToString());
            }

            if (_jobItmList.Count > 0)
            {
                _jobItmList = _jobItmList.OrderBy(c => c.jbd_jobline).ToList();
                dgvJobItem.DataSource = _jobItmList;
                dgvJobItem.DataBind();
                modifyJobDetailGrid();
                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, Session["UserCompanyCode"].ToString());

              /*  lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;*/
               // lblAddrss.Text = oJOB_HDR.SJB_B_ADD1 + "  " + oJOB_HDR.SJB_B_ADD2 + "  " + oJOB_HDR.SJB_B_ADD3;
               // lblTele.Text = oJOB_HDR.SJB_B_MOBINO;
               // lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
               // lblConPhone.Text = oJOB_HDR.SJB_CNT_PHNO;
               // lblCustomerCode.Text = oJOB_HDR.SJB_CUST_CD;
                //kapila 23/2/2016
               // lblContEmail.Text = oJOB_HDR.SJB_EMAIL;
               // lblContNo.Text = oJOB_HDR.SJB_CNT_PHNO;
               // lblContName.Text = oJOB_HDR.SJB_CNT_PERSON;

                //lblJobStage.Text = oJOB_HDR.SJB_JOBSTAGE_TEXT;
                lblJobCategori.Text = oJOB_HDR.SJB_JOBCAT == "WW" ? "WORKSHOP" : oJOB_HDR.SJB_JOBCAT == "FF" ? "FIELD" : oJOB_HDR.SJB_JOBCAT;
                lblLevel.Text = oJOB_HDR.SJB_PRORITY;
                getAllocationHeader(txtJobNo.Text, _selectedJobLine);
               // txtInstruction.Text = oJOB_HDR.SJB_TECH_RMK;
               // txtJobRemarks.Text = oJOB_HDR.SJB_JOB_RMK;
                lblJobStageNew.Text = oJOB_HDR.SJB_JOBSTAGE.ToString();
                /*
                //if (oJOB_HDR.SJB_JOBSTAGE > 3)
                //{
                //    button1.Enabled = false;
                //}
               */
                enableDisableBtns(false);
                if (dgvJobItem.Rows.Count == 1)
                {
                    GridViewRow _row = dgvJobItem.Rows[0];
                    CheckBox chkSelect = (CheckBox)_row.FindControl("chkSelect");

                    chkSelect_CheckedChanged(chkSelect, new EventArgs());
                   // dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0, 0));
                }
            }
            else
            {
                DispMsg("Please enter valid job number. ");
                txtJobNo.Text = "";
                ClearJobData();
                //DispMsg("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnClear_Click(null, null);
                return;
            }
        }
        private void enableDisableBtns(bool doEnable)
        {


            if (doEnable == false)
            {
                //btnRequisition.Enabled = true;
                //btnReciepts.Enabled = true;
                //btnStockReturn.Enabled = true;
                //btnOldPart.Enabled = true;
                //btnConsumableItems.Enabled = true;
                //btnComments.Enabled = true;
                //btnVisitComment.Enabled = true;
                //btnSuppWrntyClmReq.Enabled = true;
                //btnSuppWarrtClmReceive.Enabled = true;
                //btnAttachDoc.Enabled = true;
                //btnTechnicians.Enabled = true;
                //btnJobTask.Enabled = true;    //kapila 24/4/2015
                //btnViewPay.Enabled = true;
            }
            else
            {
                //btnRequisition.Enabled = false;
                //btnReciepts.Enabled = false;
                //btnStockReturn.Enabled = false;
                //btnOldPart.Enabled = false;
                //btnConsumableItems.Enabled = false;
                //btnComments.Enabled = false;
                //btnVisitComment.Enabled = false;
                //btnSuppWrntyClmReq.Enabled = false;
                //btnSuppWarrtClmReceive.Enabled = false;
                //btnAttachDoc.Enabled = false;
                //btnTechnicians.Enabled = false;
                //btnJobTask.Enabled = false;
                //btnViewPay.Enabled = false;
            }
        }
        private void modifyJobDetailGrid()
        {
            if (dgvJobItem.Rows.Count > 0)
            {
                for (int i = 0; i < dgvJobItem.Rows.Count; i++)
                {
                    GridViewRow _row = dgvJobItem.Rows[i];
                    Label lbljbd_act = (Label)_row.FindControl("lbljbd_act");
                    CheckBox chkSelect = (CheckBox)_row.FindControl("chkSelect");
                    if (lbljbd_act.Text == "0")
                    {
                        _row.BackColor = Color.Khaki;
                        chkSelect.Checked = false;
                        chkSelect.Enabled = false;
                    }
                    else
                    {
                        chkSelect.Checked = true;
                        chkSelect.Enabled = true;
                    }
                }
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
                Label lbljbd_act = row.FindControl("lbljbd_act") as Label;
                Label lblJbd_jobno = row.FindControl("lblJbd_jobno") as Label;
                Label lbljbd_jobline = row.FindControl("lbljbd_jobline") as Label;
                Label lbljbd_itm_cd = row.FindControl("lbljbd_itm_cd") as Label;
                Label lbljbd_ser1 = row.FindControl("lbljbd_ser1") as Label;
                Int32 _tmpint = 0;
                Int32 _jobline = Int32.TryParse(lbljbd_jobline.Text, out _tmpint)?Convert.ToInt32(lbljbd_jobline.Text):0;
                if (chkbulk.Checked == false)
                {
                    if (lbljbd_act.Text == "0")
                    {
                        DispMsg("This item has replaced by the supplier."); return;
                    }
                    else
                    {
                        foreach (var item in _jobItmList)
                        {
                            item._selectLine = false;
                        }
                        var _sel = _jobItmList.Where(c => c.Jbd_jobno == lblJbd_jobno.Text && c.jbd_jobline == _jobline).FirstOrDefault();
                        if (_sel!=null)
                        {
                            _sel._selectLine = true;

                        }
                          //  LoadDefects(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                           // GetJobEMPS(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString()));
                        _selectedJobLine = Convert.ToInt32(_jobline);
                         //   Serial_No = dgvJobDetails.Rows[e.RowIndex].Cells["SerialNo"].Value.ToString();
                        Int32 linenum = getAllocationHeader(lblJbd_jobno.Text, _jobline);
                        getJobDetails(linenum);
                    }
                }
                else
                {
                    if (lbljbd_act.Text == "0")
                    {
                        DispMsg("This item has replaced by the supplier."); return;
                    }
                    else
                    {
                        var _sel = _jobItmList.Where(c => c.Jbd_jobno == lblJbd_jobno.Text && c.jbd_jobline == _jobline).FirstOrDefault();
                        if (_sel != null)
                        {
                            _sel._selectLine = true;
                        }
                       // LoadDefects(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                       // GetJobEMPS(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString()));
                        _selectedJobLine = Convert.ToInt32(_jobline);
                       // Serial_No = dgvJobDetails.Rows[e.RowIndex].Cells["SerialNo"].Value.ToString();
                        Int32 linenum = getAllocationHeader(lblJbd_jobno.Text, _jobline);
                        getJobDetails(linenum);
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"EX");
            }
        }
        private void getJobDetails(Int32 lineNo)
        {
            List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, lineNo, Session["UserCompanyCode"].ToString());
            lblStageText.Text = ojob_Det[0].StageText;

            if (ojob_Det[0].Jbd_techst_dt_man != DateTime.MinValue)
            {
                lblStartOpenDate.Text = ojob_Det[0].Jbd_techst_dt_man.ToString("dd/MMM/yyyy  hh:mm tt");
                enableDisableBtns(true);
                lbtnStartOpenJob.Enabled = false;
               // FillItemDetails(ojob_Det[0]);
            }
            else
            {
                lblStartOpenDate.Text = "";
                enableDisableBtns(false);
                //FillItemDetails(ojob_Det[0]);
            }
            if (ojob_Det[0].Jbd_techfin_dt_man != DateTime.MinValue)
            {
                lblCompleteDate.Text = ojob_Det[0].Jbd_techfin_dt_man.ToString("dd/MMM/yyyy  hh:mm tt");
            }
            else
            {
                lblCompleteDate.Text = "";
            }

            lblJobStage.Text = ojob_Det[0].StageText;
            lblJobStageNew.Text = ojob_Det[0].Jbd_stage.ToString();

            if (ojob_Det[0].Jbd_stage == 6)
            {
                lbtnStartOpenJob.Enabled = true;
            }
            if (ojob_Det[0].Jbd_stage == 5)
            {
                lblCompleteDate.Text = "";
            }
          decimal  gblJobStage = ojob_Det[0].Jbd_stage;

            if (ojob_Det[0].Jbd_stage >= 3 && ojob_Det[0].Jbd_stage < 6 && lblStartOpenDate.Text.Length > 0)
            {
                //btnRequisition.Enabled = true;
                //btnReciepts.Enabled = true;
                //btnStockReturn.Enabled = true;
                //btnOldPart.Enabled = true;
                //btnConsumableItems.Enabled = true;
                //btnComments.Enabled = true;
                //btnVisitComment.Enabled = true;
                //btnSuppWrntyClmReq.Enabled = true;
                //btnSuppWarrtClmReceive.Enabled = true;
                //btnAttachDoc.Enabled = true;
                //btnTechnicians.Enabled = true;
                //btnJobTask.Enabled = true;
                //btnViewPay.Enabled = true;

                if (ojob_Det[0].Jbd_reqwcn == 1)
                {
                    //btnOldPart.Enabled = false;
                }
                else
                {
                   // btnOldPart.Enabled = true;
                }
            }
            else
            {
                //btnRequisition.Enabled = false;
                //btnReciepts.Enabled = false;
                //btnStockReturn.Enabled = false;
                //btnOldPart.Enabled = false;
                //btnConsumableItems.Enabled = false;
                //btnComments.Enabled = false;
                //btnVisitComment.Enabled = false;
                //btnSuppWrntyClmReq.Enabled = false;
                //btnSuppWarrtClmReceive.Enabled = false;
                //btnTempIssue.Enabled = false;
                //btnAttachDoc.Enabled = false;
                //btnTechnicians.Enabled = false;
                //btnJobTask.Enabled = false;
                //btnViewPay.Enabled = false;
            }

            if (gblJobStage < 3)
            {
                lbtnStartOpenJob.Enabled = false;
                lbtnCompleteJob.Enabled = false;
            }
            else
            {
                if (gblJobStage > 3)
                {
                    lbtnStartOpenJob.Enabled = false;
                }
                lbtnCompleteJob.Enabled = true; ;
            }
            if (gblJobStage == 3)
            {
                lbtnStartOpenJob.Enabled = true;
            }

            //BindOutwardListGridData(txtJobNo.Text, lineNo);



            List<Service_job_Det> _preSerJob = new List<Service_job_Det>();
            _preSerJob = CHNLSVC.CustService.getPrejobDetails(Session["UserCompanyCode"].ToString(), ojob_Det[0].Jbd_ser1, ojob_Det[0].Jbd_itm_cd);
            if (_preSerJob != null && _preSerJob.Count > 0)
            {
                //lblAttempt.Text = (_preSerJob.Count - 1).ToString();
            }
            else
            {
                //lblAttempt.Text = "0";
            }
        }
        private Int32 getAllocationHeader(string jobNo, Int32 lineNo)
        {
            List<Service_Tech_Aloc_Hdr> oheaders = CHNLSVC.CustService.GetJobAllocations(jobNo, lineNo, Session["UserCompanyCode"].ToString());
            if (oheaders != null && oheaders.Count > 0)
            {
                lblJobShdStartOn.Text = oheaders.Min(x => x.STH_FROM_DT).ToString("dd/MMM/yyyy");
                lblScheduleEnd.Text = oheaders.Max(x => x.STH_TO_DT).ToString("dd/MMM/yyyy");
                return oheaders[0].STH_JOBLINE;
            }
            else
            {
                return Convert.ToInt32(lineNo);
            }
        }

        protected void btnRequisition_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedJobNo = "";
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    DispMsg("Please select a job # !");
                    txtJobNo.Focus();
                    return;
                }
                if (_selectedJobLine <= 0)
                {
                    DispMsg("Please select a job item !");
                    return;
                }
                txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                _selectedJobNo = txtJobNo.Text.Trim();
                ddlMrnItmStus.SelectedIndex = 0;// ddlMrnItmStus.Items.IndexOf(ddlMrnItmStus.Items.FindByValue("GOD"));
                dgvMrnItms.DataSource = new int[] { };
                dgvMrnItms.DataBind();
                ClearLayer2();
                ClearLayer3();
                popRequest.Show();
                _popReqShow = true;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }

        protected void lbtnCloseRequest_Click(object sender, EventArgs e)
        {
            _popReqShow = false;
            popRequest.Hide();
        }
        public Boolean IsNumericValue(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string moduleText = "MRN";
            MasterAutoNumber masterAuto;

            masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }
        private void SaveInventoryRequestData()
        {

            try
            {
                int _count = 1;
                _invReqItemList.ForEach(x => x.Itri_line_no = _count++);
                _invReqItemList.ForEach(X => X.Itri_bqty = X.Itri_qty);
                _invReqItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                _invReqItemList.Where(x => string.IsNullOrEmpty(x.Itri_mitm_cd)).ToList().ForEach(y => y.Itri_mitm_cd = y.Itri_itm_cd);

                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                { DispMsg("Please add items to List."); txtMrnItm.Focus(); return; }


                // 29-01-2015 Nadeeka (Blocked to mrn for supplier warranty claim jobs)
                List<Service_job_Det> _jobdetList = CHNLSVC.CustService.getSupplierClaimRequestMRN(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine);


                if (_jobdetList != null && _jobdetList.Count > 0)
                {
                    DispMsg("Can't raise MRN, Supplier warranty requested for the job # : " + _selectedJobNo); txtMrnItm.Focus(); return;

                }


                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                InventoryRequest _inventoryRequest = new InventoryRequest();
                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Itr_req_no = GetRequestNo();
                _inventoryRequest.Itr_tp = "MRN";
                _inventoryRequest.Itr_sub_tp = "SCV";
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequestDate.Text);

                _inventoryRequest.Itr_stus = "S";  //P - Pending , A - Approved.
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10800))
                {
                    _inventoryRequest.Itr_stus = "A";  //P - Pending , A - Approved.
                }
                _inventoryRequest.Itr_job_no = _selectedJobNo;  //Invoice No.
                _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                _inventoryRequest.Itr_note = txtMrnRemark.Text;
                _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                //_inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                //_inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = Session["UserID"].ToString();
                _inventoryRequest.Itr_session_id = Session["SessionID"].ToString();
                _inventoryRequest.Itr_issue_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Itr_job_line = _selectedJobLine;

                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    if (rowsAffected > 0)
                    {
                       string Msg = "Inventory Request Document Successfully saved. " + _docNo;
                        DispMsg(Msg,"S");
                        lbtnReqClear_Click(null, null);
                    }
                    else
                    {
                        DispMsg("Process Terminated" +_docNo,"E");
                        return;
                    }

                }
                else
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    if (rowsAffected > 0)
                    {
                        DispMsg("Inventory Request Document Successfully Updated.","S");
                        lbtnReqClear_Click(null, null);
                    }
                    else
                    {
                        DispMsg("Process Terminated" +_docNo,"E");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                DispMsg(e.Message,"E");
                return;
            }
        }
        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(txtRequest.Text))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = txtRequest.Text;

            return _reqNo;
        }
        protected void lbtnReqSave_Click(object sender, EventArgs e)
        {
            try
            {
                string checkTimeMsg = string.Empty;
                if (CheckServerDateTime(out checkTimeMsg) == false)
                {
                    DispMsg(checkTimeMsg); return;  
                }
                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    DispMsg("Please select a dispatch location !");
                    txtDispatchRequried.Focus();
                    return;
                }
                if (dgvMrnItms.Rows.Count > 0)
                {
                    SaveInventoryRequestData();
                    lbtnReqClear_Click(null, null);
                }
                else
                {
                    DispMsg("Please add records ! ");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void ClearLayer2()
        {
            txtMrnItm.Text = "";
            //txtReservation.Clear();
            txtMrnQty.Text = FormatToCurrency("0");
            lblMrnAvaQty.Text = FormatToCurrency("0");
            lblMrnFreeQty.Text = FormatToCurrency("0");
            txtMrnItmRemarks.Text = "";
        }

        private void ClearLayer3()
        {
            lblMrnDescr.Text = "Description : " + string.Empty;
            lblMrnModel.Text = "Model : " + string.Empty;
            lblMrnBrand.Text = "Brand : " + string.Empty;
            lblItemSubStatus.Text = "Serial Status : " + string.Empty;
        }
        protected void lbtnReqApprove_Click(object sender, EventArgs e)
        {
            try
            {
                bool b10800 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10800);
                if (!b10800)
                {
                    DispMsg("Sorry, You have no permission! ( Advice: Required permission code : 10800)"); return;
                }
                Int32 result = CHNLSVC.CustService.Update_ReqHeaderStatus("A", Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtRequest.Text.Trim());

                if (result > 0)
                {
                    txtRequest.Text = "";
                   DispMsg("MRN approved successfully.","S");
                   getSavedItems();
                   return;
                }
                else
                {
                    DispMsg("Process terminated","E");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void getSavedItems()
        {
            DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine);
            dgvMrnItms.DataSource = new int[]{};
            if (dtTemp.Rows.Count > 0)
            {
                dgvMrnItms.DataSource = dtTemp;
            }
            dgvMrnItms.DataBind();
        }
        private void getSavedItems2()
        {
            DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine);
            dgvPrintMrn.DataSource = new int[] { };
            if (dtTemp.Rows.Count > 0)
            {
                dgvPrintMrn.DataSource = dtTemp;
            }
            dgvPrintMrn.DataBind();
        }
        private void CancelSelectedRequest()
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    DispMsg("Please select request before cancel.");
                    return;
                }

                if (DateTime.Compare(Convert.ToDateTime(txtRequestDate.Text.Trim()), DateTime.Now.Date) != 0)
                {
                    DispMsg("Request date should be current date in order to Cancel.");
                    return;
                }

                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_com = Session["UserCompanyCode"].ToString();
                _inputInvReq.Itr_loc = Session["UserDefLoca"].ToString();
                _inputInvReq.Itr_req_no = txtRequest.Text;
                _inputInvReq.Itr_stus = "C";
                _inputInvReq.Itr_mod_by = Session["UserID"].ToString();
                _inputInvReq.Itr_session_id = Session["SessionID"].ToString();

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);
                result = CHNLSVC.CustService.Update_ReqHeaderStatus("C", Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtRequest.Text);

                if (result > 0)
                    DispMsg("Inventory Request " + _inputInvReq.Itr_req_no + " successfully Cancel.", "S");
                else
                    DispMsg("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Cancel."); ;
            }
            catch (Exception e)
            {
                DispMsg(e.Message, "E");
            }
        }
        protected void lbtnReqCanc_Click(object sender, EventArgs e)
        {
            try
            {
                    CancelSelectedRequest();
                    lbtnReqClear_Click(null, null);
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnReqClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtDispatchRequried.Text="";
                txtRequestDate.Text  = DateTime.Now.ToString("dd/MMM/yyyy");
                txtRequest.Text = "";
                txtMrnItm.Text = "";
                //cmbStatus.SelectedIndex = 0;
                txtMrnQty.Text = "";
                txtMrnRemark.Text = "";
                lblMrnAvaQty.Text = "";
                lblMrnFreeQty.Text = "";
                lblMrnDescr.Text = "Description : ";
                lblMrnModel.Text = "Model : ";
                lblMrnBrand.Text = "Brand : ";
                lblItemSubStatus.Text = "Sub Item Status : ";
                dgvMrnItms.DataSource = new int[] { };
                dgvMrnItms.DataBind();
                txtMrnRemark.Text = "";
               // dgvMRNDetails.DataSource = CHNLSVC.CustService.GetMRNItemsByJobline("ZZ", "zz", -1);
               // pnlMRN.Visible = false;
                txtDispatchRequried.Focus();
                _invReqItemList = new List<InventoryRequestItem>();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        
        }

        protected void lbtnShowMrn_Click(object sender, EventArgs e)
        {
            getSavedItems2();
            popSavedMrn.Show();
        }

        protected void txtDispatchRequried_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDispatchRequried.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, "CODE", txtDispatchRequried.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtDispatchRequried.Text.ToUpper().Trim(), "CODE", "Description");
                    txtDispatchRequried.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtDispatchRequried.Text = string.Empty;
                        txtDispatchRequried.Focus();
                        DispMsg("Please enter valid dispatch location !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeDispRequired_Click(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = true;
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, null, null);
                LoadSearchPopup("ServiceJobSearchWIP", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }

        protected void txtEstimate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtEstimate.ToolTip = string.Empty;
                //if (!string.IsNullOrEmpty(txtEstimate.Text))
                //{
                //    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EetimateByJob);
                //    _serData = CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(_para, "CODE", txtEstimate.Text.ToUpper().Trim());
                //    DataAvailable(_serData, txtEstimate.Text.ToUpper().Trim(), "CODE", "Description");
                //    txtEstimate.ToolTip = _ava ? _toolTip : "";
                //    if (_ava)
                //    {
                //        //txtYearFrom.Focus();
                //    }
                //    else
                //    {
                //        txtEstimate.Text = string.Empty;
                //        txtEstimate.Focus();
                //        DispMsg("Please enter valid estimate !");
                //        return;
                //    }
                //}
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        

        

        protected void txtMrnQty_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnMrnGrdRemove_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnMrnGrdEdit_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSeEstimate_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EetimateByJob);
                _serData = CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(_para, null, null);
                LoadSearchPopup("EetimateByJob", "ESTIMATE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }

        protected void lbtnSeMrn_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                _serData = CHNLSVC.Inventory.GetSearchMRNWebByJobNo(_para, null, null);
                LoadSearchPopupDT("MRN", "MRN No", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }
        protected void txtRequest_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtRequest.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtRequest.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                    _serData = CHNLSVC.Inventory.GetSearchMRNWebByJobNo(_para, "MRN NO", txtRequest.Text.ToUpper().Trim());
                    lbtnReqApprove.Enabled = false;
                    lbtnReqApprove.OnClientClick = "";
                    DataAvailable(_serData, txtRequest.Text.ToUpper().Trim(), "MRN NO", "MRN NO");
                    txtRequest.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        InventoryRequest _inputInvReq = new InventoryRequest();
                        _inputInvReq.Itr_req_no = txtRequest.Text.Trim();
                        InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);

                        if (_selectedInventoryRequest != null)
                            if (!string.IsNullOrEmpty(_selectedInventoryRequest.Itr_com))
                            {
                                this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
                                return;
                            }
                    }
                    else
                    {
                        txtRequest.Text = string.Empty;
                        txtRequest.Focus();
                        DispMsg("Please enter valid request no !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                txtMrnRemark.Text = _selectedInventoryRequest.Itr_note;
                txtDispatchRequried.Text = _selectedInventoryRequest.Itr_loc;
                //Set Item details.
                if (_selectedInventoryRequest.InventoryRequestItemList != null)
                {
                    dgvMrnItms.DataSource = _selectedInventoryRequest.InventoryRequestItemList;
                    dgvMrnItms.DataBind();
                    _invReqItemList = _selectedInventoryRequest.InventoryRequestItemList;
                }
                else 
                {
                    DispMsg("There are no pending items");
                }

                //Set relevant buttons according to the MRN status.
                if (_selectedInventoryRequest.Itr_stus == "P")
                {
                    lbtnReqApprove.Enabled = true;
                }
            }
        }
        private void DisplayAvailableQty(string _item, Label _avalQty, Label _freeQty, string _status)
        {
            List<InventoryLocation> _inventoryLocation = null;
            if (_status=="0")
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text, _item.Trim(), string.Empty);
            else
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text, _item.Trim(), _status);

            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _aQty = _inventoryLocation.Select(x => x.Inl_qty).Sum();
                    var _aFree = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    _avalQty.Text = FormatToQty(Convert.ToString(_aQty));
                    _freeQty.Text = FormatToQty(Convert.ToString(_aFree));
                }
                else { _avalQty.Text = FormatToQty("0"); _freeQty.Text = FormatToQty("0"); }
            else { _avalQty.Text = FormatToQty("0"); _freeQty.Text = FormatToQty("0"); }
        }
        private void LoadDispatchLocationInventoryBalance(string _item)
        {
            //List<InventoryLocation> _lst = CHNLSVC.Inventory.GetInventoryBalanceSCMnSCM2(Session["UserCompanyCode"].ToString(), "", _item, string.Empty);
            //if (_lst != null)
            //    if (_lst.Count > 0)
            //    {
            //        //pnlBalance.Visible = true;
            //        gvBalance.DataSource = new DataTable();
            //        gvBalance.DataSource = _lst;
            //        gvBalance.Focus();
            //    }
        }
        private bool LoadItemDetail(string _item)
        {
            lblMrnDescr.Text = "Description : " + string.Empty;
            lblMrnModel.Text = "Model : " + string.Empty;
            lblMrnBrand.Text = "Brand : " + string.Empty;
            lblItemSubStatus.Text = "Sub Item Status : " + string.Empty;
            _isDecimalAllow = false;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd) && _itemdetail.Mi_itm_tp != "V")
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_subitem == true ? "Available" : "Non";

                    lblMrnDescr.Text = "Description : " + _description;
                    lblMrnModel.Text = "Model : " + _model;
                    lblMrnBrand.Text = "Brand : " + _brand;
                    lblItemSubStatus.Text = "Sub Item Status : " + _serialstatus;
                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);

                }

            return _isValid;
        }
        protected void txtMrnItm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtMrnItm.ToolTip = string.Empty;
                lblMrnAvaQty.Text = "0.00";
                lblMrnFreeQty.Text = "0.00";
                dgvMrnItms.DataSource = new int[] { };
                dgvMrnItms.DataBind();
                if (!string.IsNullOrEmpty(txtMrnItm.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, "ITEM", txtMrnItm.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtMrnItm.Text.ToUpper().Trim(), "ITEM", "Description");
                    txtMrnItm.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        LoadItemDetail(txtMrnItm.Text.Trim());
                        if (ddlMrnItmStus.SelectedIndex>0)
                            DisplayAvailableQty(txtMrnItm.Text.Trim(), lblMrnAvaQty, lblMrnFreeQty, ddlMrnItmStus.SelectedValue.ToString());
                        else
                            DisplayAvailableQty(txtMrnItm.Text.Trim(), lblMrnAvaQty, lblMrnFreeQty, string.Empty);
                        if (_itemdetail.Mi_itm_tp != "V")
                            LoadDispatchLocationInventoryBalance(txtMrnItm.Text.Trim());

                        //if (!String.IsNullOrEmpty(txtEstimate.Text))
                        //{
                        //    List<Service_Estimate_Item> oEstimateItems = CHNLSVC.CustService.GetServiceEstimateItems(txtEstimate.Text);
                        //    if (oEstimateItems.FindAll(x => x.ESI_ITM_CD == txtMrnItm.Text).Count == 0)
                        //    {
                        //        DispMsg("Selected item is not in the estimate-" + txtEstimate.Text + " Please enter valied item.");
                        //        txtMrnItm.Text = "";
                        //        return;
                        //    }
                        //}
                    }
                    else
                    {
                        txtMrnItm.Text = string.Empty;
                        LoadItemDetail(txtMrnItm.Text);
                        txtMrnItm.Focus();
                        DispMsg("Please enter valid item code !");
                        return;
                    }
                }
                else
                {
                    LoadItemDetail(txtMrnItm.Text);
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnSeMrnItm_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, null, null);
                LoadSearchPopup("InvoiceItemUnAssableByModel", "ITEM", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }

        protected void lbtnMrnItmAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMrnItm.Text))
                {
                    DispMsg("Please enter item code.");
                    txtMrnItm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlMrnItmStus.Text))
                {
                    DispMsg("Please select a item status.");
                    ddlMrnItmStus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMrnQty.Text))
                {
                    DispMsg("Please enter required quantity.");
                    txtMrnQty.Focus();
                    return;
                }

                if (IsNumericValue(txtMrnQty.Text.Trim()) == false)
                {
                    DispMsg("Please enter valid quantity.");
                    txtMrnQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtMrnQty.Text.ToString()) <= 0)
                {
                    DispMsg("Please enter valid quantity.");
                    txtMrnQty.Focus();
                    return;
                }

                //get job item details_____________
                List<Service_job_Det> _jobItems = CHNLSVC.CustService.GetJobDetails(_selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString());
                string _jobItemCode = _jobItems[0].Jbd_itm_cd.ToString();

                if (txtMrnItm.Text.ToString().ToUpper() == _jobItemCode.ToString().ToUpper())
                {
                    DispMsg("Can not request job item in request!");
                    txtMrnItm.Focus();
                    return;
                }


                //Get existing items details from the grid.

                string _mainItemCode = txtMrnItm.Text.Trim().ToUpper();
                string _itemStatus = ddlMrnItmStus.SelectedValue.ToString();
                // string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? string.Empty : txtReservation.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtMrnQty.Text)) ? 0 : Convert.ToDecimal(txtMrnQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtMrnItmRemarks.Text.Trim()) ? string.Empty : txtMrnItmRemarks.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                List<InventoryRequestItem> _temp = _invReqItemList;
                //This is a temporary collation for newly added items.
                List<InventoryRequestItem> _resultList = null;

                //Check whether that Master Item have sub Items.
            Outer:// Nadeeka 07-08-2015
                if (_isSubItemHave)
                {
                    //Get the relevant sub items.
                    List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);
                    if (_itemComponentList == null)
                    {
                        _isSubItemHave = false;
                        goto Outer;
                    }
                    if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                    {
                        //Update qty for existing items.
                        foreach (MasterItemComponent _itemCompo in _itemComponentList)
                        {
                            if (_invReqItemList != null)
                                if (_invReqItemList.Count > 0)
                                    _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(_itemCompo.ComponentItem.Mi_cd) && x.Itri_itm_stus == _itemStatus).ToList();

                            // If selected sub item exists in the grid,update the qty.
                            if ((_resultList != null) && (_resultList.Count > 0))
                                foreach (InventoryRequestItem _result in _resultList)
                                    _result.Itri_qty = _result.Itri_qty + (_mainItemQty * _itemCompo.Micp_qty);
                            else
                            {
                                // If selected sub item does not exists in the grid add it.
                                InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                                decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;

                                MasterItem _componentItem = new MasterItem();
                                _componentItem.Mi_cd = _itemCompo.ComponentItem.Mi_cd;
                                _inventoryRequestItem.Itri_itm_cd = _itemCompo.ComponentItem.Mi_cd;
                                _componentItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _inventoryRequestItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _componentItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _inventoryRequestItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _componentItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.MasterItem = _componentItem;

                                _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                                //_inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                                _inventoryRequestItem.Itri_app_qty = _subItemQty;

                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;

                                _inventoryRequestItem.Itri_job_no = _selectedJobNo;
                                _inventoryRequestItem.Itri_job_line = _selectedJobLine;

                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                }
                else
                {
                    if (_invReqItemList != null)
                        if (_invReqItemList.Count > 0)
                            _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(txtMrnItm.Text.Trim()) && x.Itri_itm_stus == _itemStatus).ToList();

                    // If selected sub item exists in the grid,update the qty.
                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        DispMsg(txtMrnItm.Text + " already added !");
                        return;
                        //if (DispMsg(txtMrnItm.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    foreach (InventoryRequestItem _result in _resultList)
                        //        _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }
                    else
                    {
                        //Add new item to existing list.
                        InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();

                        MasterItem _masterItem = new MasterItem();
                        _masterItem.Mi_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_itm_cd = _mainItemCode;
                        _masterItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _inventoryRequestItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _masterItem.Mi_model = _itemdetail.Mi_model;
                        _inventoryRequestItem.Mi_model = _itemdetail.Mi_model;
                        _masterItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.MasterItem = _masterItem;

                        _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                        //_inventoryRequestItem.Itri_res_no = _reservationNo;
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = _mainItemQty;

                        //Add Main item details.
                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;

                        _inventoryRequestItem.Itri_job_no = _selectedJobNo;
                        _inventoryRequestItem.Itri_job_line = _selectedJobLine;

                        _invReqItemList.Add(_inventoryRequestItem);
                    }
                }

                //Clear add new data.
                ClearLayer2();
                ClearLayer3();

                //Bind the updated list to grid.
                dgvMrnItms.DataSource = new int[] { };
                if (_invReqItemList.Count > 0)
                {
                    dgvMrnItms.DataSource = _invReqItemList;
                }
                dgvMrnItms.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnStartOpenJob_Click(object sender, EventArgs e)
        {
            if (_selectedJobLine <= 0)
            {
                DispMsg("Please select a job item");
                return;
            }

            if (!string.IsNullOrEmpty(lblStartOpenDate.Text))
            {
                DispMsg("This job is already started");
                return;
            }
            txtSelectedDateTime.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt");
            _pnlSelectTag = "S";
            popCompleate.Show();
        }
        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }
        protected void lbtnCompConf_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsDateTime(txtSelectedDateTime.Text))
                {
                    DispMsg("Please select the valid date time !"); return;
                }
                if (_pnlSelectTag == "S")
                {
                    DateTime _confDt = Convert.ToDateTime(txtSelectedDateTime.Text);
                    lblStartOpenDate.Text = _confDt.ToString("dd/MMM/yyyy hh:mm tt");
                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, Session["UserCompanyCode"].ToString());
                    if (_confDt < oJOB_HDR.SJB_DT)
                    {
                        DispMsg("Please select a valid date");
                        lblStartOpenDate.Text = "";
                        return;
                    }

                    int jobstage = Convert.ToInt32(lblJobStageNew.Text);
                    if (jobstage <= 3)
                    {
                        int result1 = CHNLSVC.CustService.Update_Job_dates(txtJobNo.Text, _selectedJobLine, DateTime.Now, DateTime.MinValue, Convert.ToDateTime(lblStartOpenDate.Text), DateTime.MinValue);
                        int result = CHNLSVC.CustService.Update_JobDetailStage(txtJobNo.Text, _selectedJobLine, 4);

                        Service_Job_StageLog oLog = new Service_Job_StageLog();
                        oLog.SJL_REQNO = "";
                        oLog.SJL_JOBNO = txtJobNo.Text;
                        oLog.SJL_JOBLINE = _selectedJobLine;
                        oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                        oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                        oLog.SJL_JOBSTAGE = 4;
                        oLog.SJL_CRE_BY = Session["UserID"].ToString();
                        oLog.SJL_CRE_DT = DateTime.Now;
                        oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                        oLog.SJL_INFSUP = 0;
                        result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);

                        if (result1 > 0)
                        {
                            DispMsg("Record updated successfully","S");
                            //btnRequisition.Enabled = true;
                            enableDisableBtns(false);
                            //dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0,0));
                            lbtnStartOpenJob.Enabled = false;
                           // selectSameJobItem();
                            return;
                        }
                        else
                        {
                            DispMsg("Process Terminated.","E");
                            return;
                        }
                    }
                }
                else if (_pnlSelectTag == "E")
                {
                    lblCompleteDate.Text = txtSelectedDateTime.Text;
                    int jobstage = Convert.ToInt32(lblJobStageNew.Text);
                    if (jobstage == 6)
                    {
                        int result1 = CHNLSVC.CustService.Update_Job_dates(txtJobNo.Text, _selectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), Convert.ToDateTime(lblStartOpenDate.Text));
                        int result = CHNLSVC.CustService.Update_JobDetailStage(txtJobNo.Text, _selectedJobLine, 5);
                        Service_Job_StageLog oLog = new Service_Job_StageLog();
                        oLog.SJL_REQNO = "";
                        oLog.SJL_JOBNO = txtJobNo.Text;
                        oLog.SJL_JOBLINE = _selectedJobLine;
                        oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                        oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                        oLog.SJL_JOBSTAGE = jobstage;
                        oLog.SJL_CRE_BY = Session["UserID"].ToString();
                        oLog.SJL_CRE_DT = DateTime.Now;
                        oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                        oLog.SJL_INFSUP = 0;
                        result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                        if (result1 > 0)
                        {
                            DispMsg("Record updated successfully","S");
                            return;
                        }
                        else
                        {
                            DispMsg("Process Terminated.","E");
                            return;
                        }
                    }
                    else
                    {
                        int result1 = CHNLSVC.CustService.Update_Job_dates(txtJobNo.Text, _selectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), Convert.ToDateTime(lblStartOpenDate.Text));
                        int result = CHNLSVC.CustService.Update_JobDetailStage(txtJobNo.Text, _selectedJobLine, 6);
                        Service_Job_StageLog oLog = new Service_Job_StageLog();
                        oLog.SJL_REQNO = "";
                        oLog.SJL_JOBNO = txtJobNo.Text;
                        oLog.SJL_JOBLINE = _selectedJobLine;
                        oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                        oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                        oLog.SJL_JOBSTAGE = jobstage;
                        oLog.SJL_CRE_BY = Session["UserID"].ToString();
                        oLog.SJL_CRE_DT = DateTime.Now;
                        oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                        oLog.SJL_INFSUP = 0;
                        result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                        if (result1 > 0)
                        {
                            DispMsg("Record updated successfully","S");
                            return;
                        }
                        else
                        {
                            DispMsg("Process Terminated.","E");
                            return;
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnCompNotConf_Click(object sender, EventArgs e)
        {

        }
        private Int32 JobCloseAdjusmentProcess(out string _resMsg)
        {
            List<ReptPickSerials> _rptSerialList = new List<ReptPickSerials>();
            List<ReptPickSerialsSub> _rptSubSerialList = new List<ReptPickSerialsSub>();
            List<TmpValidation> _errList = new List<TmpValidation>();
            _resMsg = "";
            string _docAdjMines="", _docAdjPlus="", _docAodOutNo="", _error="";
            Int32 _res=0;
            MasterAutoNumber _autonoMinus = new MasterAutoNumber();
            #region Fill MasterAutoNumber
            _autonoMinus.Aut_cate_cd = Session["UserDefLoca"].ToString();
            _autonoMinus.Aut_cate_tp = "LOC";
            _autonoMinus.Aut_direction = null;
            _autonoMinus.Aut_modify_dt = null;
            _autonoMinus.Aut_moduleid = "ADJ";
            _autonoMinus.Aut_number = 5;
            _autonoMinus.Aut_start_char = "ADJ";
            _autonoMinus.Aut_year = null;
            #endregion
            InventoryHeader _invHdrAdjMines = new InventoryHeader();

            MasterAutoNumber _autonoPlus = new MasterAutoNumber();
            #region Fill MasterAutoNumber
            _autonoPlus.Aut_cate_cd = Session["UserDefLoca"].ToString();
            _autonoPlus.Aut_cate_tp = "LOC";
            _autonoPlus.Aut_direction = null;
            _autonoPlus.Aut_modify_dt = null;
            _autonoPlus.Aut_moduleid = "ADJ";
            _autonoPlus.Aut_number = 5;
            _autonoPlus.Aut_start_char = "ADJ";
            _autonoPlus.Aut_year = null;
            #endregion
            InventoryHeader _invHdrAdjPlus = new InventoryHeader();

            MasterAutoNumber _autAodOut = new MasterAutoNumber();
            #region Fill MasterAutoNumber AOD PLUS
            _autAodOut.Aut_cate_cd = Session["UserDefLoca"].ToString();
            _autAodOut.Aut_cate_tp = "LOC";
            _autAodOut.Aut_direction = null;
            _autAodOut.Aut_modify_dt = null;
            _autAodOut.Aut_moduleid = "AOD";
            _autAodOut.Aut_start_char = "AOD";
            _autAodOut.Aut_year = Convert.ToDateTime(DateTime.Now).Year;
            #endregion
            

            InventoryHeader _invHdrAodOut = new InventoryHeader();
            #region AODOUT Inventory Header Value Assign
            _invHdrAodOut.Ith_acc_no = string.Empty;
            _invHdrAodOut.Ith_anal_1 = string.Empty;
            _invHdrAodOut.Ith_anal_10 = true;//Direct AOD
            _invHdrAodOut.Ith_anal_11 = false;
            _invHdrAodOut.Ith_anal_12 = false;
            _invHdrAodOut.Ith_anal_2 = string.Empty;
            _invHdrAodOut.Ith_anal_3 = string.Empty;
            _invHdrAodOut.Ith_anal_4 = string.Empty;
            _invHdrAodOut.Ith_anal_5 = string.Empty;
            _invHdrAodOut.Ith_anal_6 = 0;
            _invHdrAodOut.Ith_anal_7 = 0;
            _invHdrAodOut.Ith_anal_8 = Convert.ToDateTime(DateTime.Now).Date;
            _invHdrAodOut.Ith_anal_9 = Convert.ToDateTime(DateTime.Now).Date;
            _invHdrAodOut.Ith_bus_entity = string.Empty;
            _invHdrAodOut.Ith_cate_tp = "NOR";
            _invHdrAodOut.Ith_channel = string.Empty;
            _invHdrAodOut.Ith_com = Session["UserCompanyCode"].ToString();
            _invHdrAodOut.Ith_com_docno = string.Empty;
            _invHdrAodOut.Ith_cre_by = Session["UserID"].ToString();
            _invHdrAodOut.Ith_cre_when = DateTime.Now.Date;
            _invHdrAodOut.Ith_del_add1 = string.Empty;
            _invHdrAodOut.Ith_del_add2 = string.Empty;
            _invHdrAodOut.Ith_del_code = string.Empty;
            _invHdrAodOut.Ith_del_party = string.Empty;
            _invHdrAodOut.Ith_del_town = string.Empty;
            _invHdrAodOut.Ith_direct = false;
            _invHdrAodOut.Ith_doc_date = Convert.ToDateTime(DateTime.Now);
            _invHdrAodOut.Ith_doc_no = string.Empty;
            _invHdrAodOut.Ith_doc_tp = "AOD";
            _invHdrAodOut.Ith_doc_year = Convert.ToDateTime(DateTime.Now).Date.Year;
            //_invHdrAodOut.Ith_entry_no = _requestno;
            _invHdrAodOut.Ith_entry_tp = string.Empty;
            _invHdrAodOut.Ith_git_close = false;
            _invHdrAodOut.Ith_git_close_date = Convert.ToDateTime(DateTime.Now).Date;
            _invHdrAodOut.Ith_git_close_doc = string.Empty;
            _invHdrAodOut.Ith_is_manual = true;
            _invHdrAodOut.Ith_isprinted = false;
            _invHdrAodOut.Ith_job_no = string.Empty;
            _invHdrAodOut.Ith_loading_point = string.Empty;
            _invHdrAodOut.Ith_loading_user = string.Empty;
            _invHdrAodOut.Ith_loc = Session["UserDefLoca"].ToString();
            _invHdrAodOut.Ith_manual_ref = "WP";
            _invHdrAodOut.Ith_mod_by = Session["UserID"].ToString();
            _invHdrAodOut.Ith_mod_when = DateTime.Now.Date;
            _invHdrAodOut.Ith_noofcopies = 0;
            //_invHdrAodOut.Ith_oth_loc = txtDispatchRequried.Text.Trim();
            //_invHdrAodOut.Ith_oth_docno = chkDirectOut.Checked ? string.Empty : _requestno;
            _invHdrAodOut.Ith_oth_docno = string.Empty;
            //_invHdrAodOut.Ith_remarks = txtRemarks.Text;
            _invHdrAodOut.Ith_sbu = string.Empty;
            //_invHdrAodOut.Ith_seq_no = 0; removed by Chamal 12-05-2013
            _invHdrAodOut.Ith_session_id = Session["SessionID"].ToString();
            _invHdrAodOut.Ith_stus = "A";
            _invHdrAodOut.Ith_sub_tp = string.Empty;
            _invHdrAodOut.Ith_vehi_no = string.Empty;
            _invHdrAodOut.Ith_oth_com = Session["UserCompanyCode"].ToString();
            //_invHdrAodOut.Ith_anal_1 =  "0";
            //_invHdrAodOut.Ith_anal_2 = chkManualRef.Checked ? ddlManType.Text : string.Empty;
            _invHdrAodOut.Ith_anal_2 = string.Empty;
            //_invHdrAodOut.Ith_sub_tp = ddlType.SelectedValue.ToString();
            _invHdrAodOut.Ith_sub_tp = "NOR";
            _invHdrAodOut.Ith_session_id = Session["SessionID"].ToString();
            _invHdrAodOut.Ith_pc = Session["UserDefProf"].ToString();
            //_invHdrAodOut.Ith_vehi_no = txtVehicle.Text;//add rukshan 06/jan/2016
            //_invHdrAodOut.Ith_anal_3 = ddlDeliver.SelectedItem.Text;//add rukshan 06/jan/2016
            //if (_ServiceJobBase == true)
            //{
            //    //_invHdrAodOut.Ith_isjobbase = true;
            //    _invHdrAodOut.Ith_job_no = JobNo;
            //    _invHdrAodOut.Ith_cate_tp = "SERVICE";
            //    _invHdrAodOut.Ith_sub_tp = "NOR";
            //    _invHdrAodOut.Ith_sub_docno = JobNo;
            //}

            #endregion

           // _res = CHNLSVC.Inventory.ServiceWorkingProcessComplete(_autonoMinus, _autonoPlus, _autAodOut, _invHdrAdjMines, _invHdrAdjPlus, _invHdrAodOut,
           //_rptSerialList, _rptSubSerialList,
           // out  _docAdjMines, out  _docAdjPlus, out  _docAodOutNo, out  _error, out  _errList);
            return _res;
        }

        protected void lbtnCompleteJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbulk.Checked == false)
                {
                    if (_selectedJobLine < 1)
                    {
                        DispMsg("Please select a job item");
                        return;
                    }

                    if (string.IsNullOrEmpty(txtJobNo.Text))
                    {
                        DispMsg("Please select a job # !");
                        txtJobNo.Focus();
                        return;
                    }

                    if (String.IsNullOrEmpty(lblStartOpenDate.Text))
                    {
                        DispMsg("Please start the job !");
                        return;
                    }
                    _selectedJobNo = txtJobNo.Text.Trim();
                    Service_Chanal_parameter _Parameters = null;
                    _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                    //if (_Parameters != null)
                    //{
                    //    if (_Parameters.SP_AUTO_REMOVE_OLD_PART == 1)
                    //    {
                    //        if (!IsOldItemAdded)
                    //        {
                    //            dgvOPR.DataSource = new int[] { };
                    //            dgvOPR.DataBind();
                    //            txtOldPartRemDt.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    //            // ddlOPRItemSts.SelectedIndex = ddlOPRItemSts.Items.IndexOf(ddlOPRItemSts.Items.FindByValue("OLDPT"));
                    //            // ddlOPRItemSts.Enabled = false;
                    //            lbtnOPRClear_Click(null, null);
                    //            if (AddOldItemAuto)
                    //            {
                    //                Auto_RemoveOldPart();
                    //            }
                    //            lbtnOPRSave.Visible = false;
                    //            popOldPartRemShow = true;
                    //            popOldPartRem.Show();
                    //            return;
                    //            //ServiceWIP_oldPartRemove frm = new ServiceWIP_oldPartRemove(txtJobNo.Text, SelectedJobLine);
                    //            //frm.StartPosition = FormStartPosition.Manual;
                    //            //frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
                    //            //frm.AddOldItemAuto = true;
                    //            //frm.ShowDialog();
                    //        }
                    //    }
                    //}

                    decimal jobstage = Convert.ToDecimal(lblJobStageNew.Text);
                    int[] jobLine = new int[] { };

                    dgvNewParts.DataSource = new int[] { };
                    dgvNewParts.DataBind();
                    dgvOldPart.DataSource = new int[] { };
                    dgvOldPart.DataBind();
                    txtJobCloseDt.Text = DateTime.Now.ToString();
                    _popJobCloseShow = true;
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10824))
                    {
                        txtJobCloseDt.Enabled = false;
                    }
                    else
                    {
                        txtJobCloseDt.Enabled = true;
                    }
                    popJobClose.Show();

                    //ServiceWIP_JobClose frmClose = new ServiceWIP_JobClose(txtJobNo.Text, SelectedJobLine, jobstage, lblStartOpenDate.Text, lblWarStus.Text, chkbulk.Checked, jobLine, IsHavingGitItems);

                    isActionTaken = 0;
                    //frmClose.StartPosition = FormStartPosition.Manual;
                    //frmClose.Location = new Point(this.Location.X + this.Width - 120 - frmClose.Width, this.Location.Y + 80);
                    //frmClose.ShowDialog();
                    //isActionTaken = frmClose.isActionTaken;
                    //selectSameJobItem();
                    ////lblCompleteDate.Text = "";
                    //if (isActionTaken == 1)
                    //{
                    //    clrScreen();
                    //}
                }

                else
                {


                    //if (string.IsNullOrEmpty(txtJobNo.Text))
                    //{
                    //    DispMsg("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtJobNo.Focus();
                    //    return;
                    //}


                    //if (String.IsNullOrEmpty(lblStartOpenDate.Text))
                    //{
                    //    DispMsg("Please start the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}


                    //int[] jobLine = new int[dgvJobDetails.Rows.Count];
                    //for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                    //{
                    //    if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells["select"].Value) == true)
                    //    {
                    //        Int32 _line = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value);
                    //        jobLine[i] = _line;
                    //    }
                    //}


                    //decimal jobstage = Convert.ToDecimal(lblJobStageNew.Text);
                    //ServiceWIP_JobClose frm = new ServiceWIP_JobClose(txtJobNo.Text, SelectedJobLine, jobstage, lblStartOpenDate.Text, lblWarStus.Text, chkbulk.Checked, jobLine, IsHavingGitItems);

                    isActionTaken = 0;
                    //frm.StartPosition = FormStartPosition.Manual;
                    //frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
                    //frm.ShowDialog();
                    //isActionTaken = frm.isActionTaken;
                    //selectSameJobItem();
                    //lblCompleteDate.Text = "";
                    //if (isActionTaken == 1)
                    //{
                    //    clrScreen();
                    //}

                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnJobCloseClose_Click(object sender, EventArgs e)
        {
            _popJobCloseShow = false;
            popJobClose.Hide();
        }

        protected void chkNewPartSelect_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkOldPartSelect_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkOldPartReplace_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSeWaraeHouse_Click(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = false;
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, null, null);
                LoadSearchPopup("WarehouseOld", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }
        private bool isAnySelectedOldPart()
        {
            bool status = false;
            if (dgvOldPart.Rows.Count > 0)
            {
                foreach (GridViewRow _row in dgvOldPart.Rows)
                {
                    CheckBox chkOldPartSelect = _row.FindControl("chkOldPartSelect") as CheckBox;
                    if (chkOldPartSelect.Checked)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }
        protected void lbtnOPReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOPWarehouse.Text))
                {
                    DispMsg("please select a warehouse !");
                    txtOPWarehouse.Focus();
                    return;
                }
                lbtnOPReturn.Enabled = false;    //kapila 9/2/2016
                List<Tuple<Int32, String, String>> _ReturnItemList = new List<Tuple<Int32, String, String>>();

                if (isAnySelectedOldPart())
                {
                    List<Service_Job_Det_Sub> selectedITem = new List<Service_Job_Det_Sub>();
                    foreach (GridViewRow _row in dgvOldPart.Rows)
                    {
                        CheckBox chkOldPartSelect = _row.FindControl("chkOldPartSelect") as CheckBox;
                        Label lblsop_seqno = _row.FindControl("lblsop_seqno") as Label;
                        Label lblSourceTable = _row.FindControl("lblSourceTable") as Label;
                        Label lblSOP_OLDITMSER1 = _row.FindControl("lblSOP_OLDITMSER1") as Label;
                        Label lblSOP_REQWCN = _row.FindControl("lblSOP_REQWCN") as Label;
                        CheckBox chkOldPartReplace = _row.FindControl("chkOldPartReplace") as CheckBox;
                        Label lblNewItemCode = _row.FindControl("lblNewItemCode") as Label;
                        Label lblNewSerial = _row.FindControl("lblNewSerial") as Label;

                        Int32 _tmpint = 0;
                        Int32 SeqNo =Int32.TryParse(lblsop_seqno.Text,out _tmpint)?Convert.ToInt32(lblsop_seqno.Text):0;
                        if (chkOldPartSelect.Checked)
                        {
                            _ReturnItemList.Add(new Tuple<Int32, String, String>(SeqNo, lblSourceTable.Text, lblSOP_OLDITMSER1.Text));
                        }
                        if (lblSourceTable.Text == "SUB" && chkOldPartReplace.Checked)
                        {
                            Service_Job_Det_Sub subItem = new Service_Job_Det_Sub();
                            subItem.JBDS_SEQ_NO = SeqNo;
                            subItem.JBDS_LINE = Int32.TryParse(lblSOP_REQWCN.Text, out _tmpint) ? Convert.ToInt32(lblSOP_REQWCN.Text) : 0;
                            subItem.JBDS_JOBLINE = _selectedJobLine;
                            subItem.JBDS_ITM_CD = lblNewItemCode.Text;
                            subItem.JBDS_SER1 = lblNewSerial.Text;
                            subItem.JBDS_JOBNO = _selectedJobNo;
                            selectedITem.Add(subItem);
                        }
                    }
                    Int32 result = 0;
                    string docNum = string.Empty;
                    result = 0;// CHNLSVC.CustService.Update_Olppart_ReturnWarehouse(_ReturnItemList, Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), BaseCls.GlbDefaultBin, Session["SessionID"].ToString(), txtOPWarehouse.Text, selectedITem, out docNum, _selectedJobNo, _selectedJobLine);

                    if (result != -99 && result >= 0)
                    {
                        DispMsg("Successfully Saved!  \n Doc num : " + docNum,"S");
                        lbtnCloseJobView_Click(null, null);

                        //string _repname = string.Empty;
                        //string _papersize = string.Empty;
                        //BaseCls.GlbReportTp = "OUTWARDWIP";
                        //CHNLSVC.General.CheckReportName(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);

                        //if (!(_repname == null || _repname == ""))
                        //{
                        //    //Sanjeewa
                        //    FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory _views = new FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory();
                        //    BaseCls.GlbReportName = string.Empty;
                        //    GlbReportName = string.Empty;
                        //    _views.GlbReportName = string.Empty;
                        //    _views.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "Outward_Docs.rpt" : "Outward_Docs.rpt";
                        //    _views.GlbReportDoc = docNum;
                        //    _views.Show();
                        //    _views = null;
                        //}
                    }
                    else
                    {
                        DispMsg("Process Terminated.\n" + docNum, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnSeWhHouseNewItmRtn_Click(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = true;
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, null, null);
                LoadSearchPopup("WarehouseNewItm", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }
        private bool isAnySelectedNewPart()
        {
            bool status = false;
            if (dgvNewParts.Rows.Count > 0)
            {
                foreach (GridViewRow _row in dgvNewParts.Rows)
                {
                    CheckBox chkNewPartSelect = _row.FindControl("chkNewPartSelect") as CheckBox;
                    if (chkNewPartSelect.Checked)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }
        protected void lbtnNPReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNewPartWarehouse.Text))
                {
                    DispMsg("please select a warehouse ");
                    txtNewPartWarehouse.Focus();
                    return;
                }

                if (isAnySelectedNewPart())
                {
                    List<Service_stockReturn> oNewItems = new List<Service_stockReturn>();
                    foreach (GridViewRow _row in dgvNewParts.Rows)
                    {
                        //CheckBox chkNewPartSelect = _row.FindControl("chkNewPartSelect") as CheckBox;
                        //if (chkNewPartSelect.Checked)
                        //{
                        //    Service_stockReturn item = new Service_stockReturn();
                        //    item.ITEM_CODE = dgvNewParts.Rows[i].Cells["Item"].Value.ToString();
                        //    item.STATUS_CODE = dgvNewParts.Rows[i].Cells["Status"].Value.ToString();
                        //    item.SERIAL_NO = dgvNewParts.Rows[i].Cells["Serial"].Value.ToString();
                        //    item.QTY = Convert.ToDecimal(dgvNewParts.Rows[i].Cells["Qty"].Value.ToString());
                        //    item.SERIAL_ID = dgvNewParts.Rows[i].Cells["SerialID"].Value.ToString();

                        //    if (item.SERIAL_ID == "N/A")
                        //    {
                        //        item.SERIAL_ID = String.Empty;
                        //    }
                        //    item.JOB_NO = _selectedJobNo;
                        //    item.JOB_LINE = _selectedJobLine;
                        //    item.Desc = txtRemarkNewItem.Text.Trim();

                        //    MasterItem oItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.ITEM_CODE);
                        //    if (oItem.Mi_is_ser1 == -1)
                        //    {
                        //        if (oNewItems.Count > 0 && oNewItems.FindAll(x => x.ITEM_CODE == item.ITEM_CODE && x.STATUS_CODE == item.STATUS_CODE).Count > 0)
                        //        {
                        //            Service_stockReturn oOldRecord = oNewItems.Find(x => x.ITEM_CODE == item.ITEM_CODE && x.STATUS_CODE == item.STATUS_CODE);
                        //            oOldRecord.QTY = oOldRecord.QTY + item.QTY;
                        //        }
                        //        else
                        //        {
                        //            oNewItems.Add(item);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        oNewItems.Add(item);
                        //    }
                        //}
                    }

                    Int32 result = 0;
                    string docNum = string.Empty;
                    result = CHNLSVC.CustService.Update_NewItems_ReturnWarehouse(oNewItems, Session["SessionID"].ToString(), Session["UserCompanyCode"].ToString(),
                        Session["UserDefLoca"].ToString(), Session["GlbDefaultBin"].ToString(), Session["SessionID"].ToString(), txtNewPartWarehouse.Text, Convert.ToDateTime(txtJobCloseDt.Text), out docNum);

                    if (result != -99 && result >= 0)
                    {
                        DispMsg("Successfully Saved!\nDocument Numbers " + docNum );
                        lbtnCloseJobView_Click(null, null);
                    }
                    else
                    {
                        DispMsg("Process Terminated.","E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void txtNewPartWarehouse_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNewPartWarehouse.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtNewPartWarehouse.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, "CODE", txtNewPartWarehouse.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtNewPartWarehouse.Text.ToUpper().Trim(), "CODE", "Description");
                    txtNewPartWarehouse.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtNewPartWarehouse.Text = string.Empty;
                        txtNewPartWarehouse.Focus();
                        DispMsg("Please enter valid warehouse !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtOPWarehouse_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtOPWarehouse.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtOPWarehouse.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, "CODE", txtOPWarehouse.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtOPWarehouse.Text.ToUpper().Trim(), "CODE", "Description");
                    txtOPWarehouse.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtOPWarehouse.Text = string.Empty;
                        txtOPWarehouse.Focus();
                        DispMsg("Please enter valid warehouse !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void loadCloseTypes()
        {
            MasterLocationPriorityHierarchy _locHir = CHNLSVC.General.GET_MST_LOC_INFO_DATA(Session["UserDefLoca"].ToString(), "SCHNL");
            if (_locHir == null)
            {
                DispMsg("Please check the sub channel !"); return;
            }
            List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(Session["UserCompanyCode"].ToString(), _locHir.Mli_val);
            ddlCloseType.Items.Clear();
            ddlCloseType.DataTextField = "SCT_DESC";
            ddlCloseType.DataValueField = "SCT_TP";
            ddlCloseType.DataSource = oCloseType;
            ddlCloseType.DataBind();
            ddlCloseType.SelectedIndex = ddlCloseType.Items.IndexOf(ddlCloseType.Items.FindByValue("CMP"));
        }
        private void modifyGrid()
        {
            //if (dgvOldPart.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dgvOldPart.Rows.Count; i++)
            //    {
            //        if (dgvOldPart.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
            //        {
            //            dgvOldPart.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
            //        }

            //        if (dgvOldPart.Rows[i].Cells["SourceTable"].Value.ToString() != "SUB")
            //        {
            //            dgvOldPart.Rows[i].Cells["NewReplace"].ReadOnly = true;
            //            dgvOldPart.Rows[i].Cells["NewItems"].ReadOnly = true;
            //            dgvOldPart.Rows[i].Cells["NewSerial"].ReadOnly = true;
            //        }
            //    }
            //}
            //if (dgvNewParts.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dgvNewParts.Rows.Count; i++)
            //    {
            //        if (dgvNewParts.Rows[i].Cells["Serial"].Value.ToString() == "N/A")
            //        {
            //            dgvNewParts.Rows[i].Cells["Qty"].ReadOnly = false;
            //        }
            //    }
            //}
        }
        private void getOldParts()
        {
            if (chkbulk.Checked == false)
            {
                List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, string.Empty, string.Empty);
                List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1 && x.SOP_RTN_WH == 0 || (x.SOP_REQWCN == 2 && x.SOP_RTN_WH == 0)));
                if (oldPartList.Count > 0)
                {
                    foreach (Service_OldPartRemove item in oldPartList)
                    {
                        item.SourceTable = "OLD";
                    }

                    dgvOldPart.DataSource = oldPartList;
                    dgvOldPart.DataBind();
                    modifyGrid();
                }

                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(_selectedJobNo, _selectedJobLine);
                if (oSubItems.Count > 0)
                {
                    foreach (Service_Job_Det_Sub item in oSubItems)
                    {
                        if (item.JBDS_RTN_WH == 1)
                        {
                            continue;
                        }
                        Service_OldPartRemove oldItem = new Service_OldPartRemove();
                        oldItem.SOP_SEQNO = item.JBDS_SEQ_NO;
                        oldItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
                        oldItem.SOP_OLDITMSTUS_Text = item.JBDS_ITM_STUS_TEXT;
                        oldItem.SOP_OLDITMSTUS = item.JBDS_ITM_STUS;
                        oldItem.SOP_OLDITMSER1 = item.JBDS_SER1;
                        oldItem.SOP_OLDITMQTY = item.JBDS_QTY;
                        oldItem.SOP_CRE_DT = item.JBDS_CRE_DT;
                        oldItem.SOP_RMK = item.JBDS_WARR_RMK;
                        oldItem.SOP_REQWCN = item.JBDS_LINE; // use when update the line
                        oldItem.SourceTable = "SUB";
                        oldPartList.Add(oldItem);
                    }
                    dgvOldPart.DataSource = new int[]{};
                    dgvOldPart.DataBind();
                    dgvOldPart.DataSource = oldPartList;
                    dgvOldPart.DataBind();
                    //modifyGrid();
                }
            }

            else
            {
                List<Service_OldPartRemove> oldPartList1 = new List<Service_OldPartRemove>();
                List<Service_OldPartRemove> oldPartList = new List<Service_OldPartRemove>();
                List<Service_Job_Det_Sub> oSubItems = new List<Service_Job_Det_Sub>();
                //int[] GblarrJobLine = _selectedJobLine;
                //foreach (int i in GblarrJobLine)
                //{
                List<Service_OldPartRemove> oldPartList1tmp = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, string.Empty, string.Empty);
                    List<Service_OldPartRemove> oldPartListtmp = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1 || x.SOP_REQWCN == 2));
                    oldPartList1.AddRange(oldPartList1tmp);
                    oldPartList.AddRange(oldPartListtmp);
                    List<Service_Job_Det_Sub> oSubItemstemp = CHNLSVC.CustService.GetServiceJobDetailSubItems(_selectedJobNo, _selectedJobLine);
                    oSubItems.AddRange(oSubItemstemp);
                //}


                if (oldPartList.Count > 0)
                {
                    foreach (Service_OldPartRemove item in oldPartList)
                    {
                        item.SourceTable = "OLD";
                    }

                    dgvOldPart.DataSource = oldPartList;
                    dgvOldPart.DataBind();
                    modifyGrid();
                }

                //   List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(_selectedJobNo, i);
                if (oSubItems.Count > 0)
                {
                    foreach (Service_Job_Det_Sub item in oSubItems)
                    {
                        if (item.JBDS_RTN_WH == 1)
                        {
                            continue;
                        }
                        Service_OldPartRemove oldItem = new Service_OldPartRemove();
                        oldItem.SOP_SEQNO = item.JBDS_SEQ_NO;
                        oldItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
                        oldItem.SOP_OLDITMSTUS_Text = item.JBDS_ITM_STUS_TEXT;
                        oldItem.SOP_OLDITMSTUS = item.JBDS_ITM_STUS;
                        oldItem.SOP_OLDITMSER1 = item.JBDS_SER1;
                        oldItem.SOP_OLDITMQTY = item.JBDS_QTY;
                        oldItem.SOP_CRE_DT = item.JBDS_CRE_DT;
                        oldItem.SOP_RMK = item.JBDS_WARR_RMK;
                        oldItem.SOP_REQWCN = item.JBDS_LINE; // use when update the line
                        oldItem.SourceTable = "SUB";
                        oldPartList.Add(oldItem);
                    }
                    dgvOldPart.DataSource = new int[] { };
                    if (oldPartList!= null)
                    {
                        if (oldPartList.Count > 0)
                        {
                            dgvOldPart.DataSource = oldPartList; 
                        }
                    }
                    dgvOldPart.DataBind();
                    modifyGrid();
                }

            }
        }
        private void getNewItems()
        {
            if (chkbulk.Checked == false)
            {
                List<Service_stockReturn> oNewItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems
                    (Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine, string.Empty, Session["UserDefLoca"].ToString());
                dgvNewParts.DataSource = new List<Service_stockReturn>();

                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(_selectedJobNo, _selectedJobLine);

                foreach (Service_Job_Det_Sub item in oSubItems)
                {
                    oNewItems.RemoveAll(x => x.SERIAL_ID == item.JBDS_REPL_SERID.ToString());
                }

                dgvNewParts.DataSource = oNewItems;

                modifyGrid();
            }

            else
            {
                List<Service_stockReturn> oNewItems = new List<Service_stockReturn>();
                List<Service_Job_Det_Sub> oSubItems = new List<Service_Job_Det_Sub>();
                dgvNewParts.DataSource = new List<Service_stockReturn>();
                //foreach (int i in GblarrJobLine)
                //{
                List<Service_stockReturn> oNewItemstemp = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(
                    Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine, string.Empty, Session["UserDefLoca"].ToString());
                    oNewItems.AddRange(oNewItemstemp);

                    List<Service_Job_Det_Sub> oSubItemstemp = CHNLSVC.CustService.GetServiceJobDetailSubItems(_selectedJobNo, _selectedJobLine);
                    oSubItems.AddRange(oSubItemstemp);
                //}

                foreach (Service_Job_Det_Sub item in oSubItems)
                {
                    oNewItems.RemoveAll(x => x.SERIAL_ID == item.JBDS_REPL_SERID.ToString());
                }

                dgvNewParts.DataSource = oNewItems;

                modifyGrid();


            }
        }
        protected void lbtnCloseJobView_Click(object sender, EventArgs e)
        {
            txtRemarkNewItem.Text = "";
            getOldParts();
            getNewItems();
        }

        protected void lbtnCloseJobSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblJobStageNew.Text == "6")
                {
                    int result1 = CHNLSVC.CustService.Update_Job_dates(_selectedJobNo, _selectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(txtJobCloseDt.Text),
                        Convert.ToDateTime(txtJobCloseDt.Text));
                    result1 = CHNLSVC.CustService.Update_JobDetailStage(_selectedJobNo, _selectedJobLine, 5);
                    //result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, _selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = _selectedJobNo;
                    oLog.SJL_JOBLINE = _selectedJobLine;
                    oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                    oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                    oLog.SJL_JOBSTAGE = 5;
                    oLog.SJL_CRE_BY = Session["UserID"].ToString();
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                    oLog.SJL_INFSUP = 0;
                    result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);



                    if (result1 > 0)
                    {
                        DispMsg("Successfully job Re-Opened.","S");
                        isActionTaken = 2;
                        popJobClose.Hide();
                        _popJobCloseShow = false;
                        return;
                    }
                    else
                    {
                        DispMsg("Process Terminated.","E");
                        return;
                    }
                }
                else
                {
                    //By akila 2017/05/08 - if git items available, service not allow to close
                    if (IsHavingGitItems)
                    {
                         DispMsg("Job cannot be closed when ther are GIT available");
                         return;
                    }

                    //Sanjeewa 2017-02-25
                    if (Session["UserCompanyCode"].ToString() != "ABE") //Removed as per request 2017-03-10
                    {
                        DataTable _mrn = CHNLSVC.CustService.checkAppMRNforJob(_selectedJobNo);
                        string _mrnno = "";
                        if (_mrn.Rows.Count > 0)
                        {
                            for (int i = 0; i < _mrn.Rows.Count; i++)
                            {
                                _mrnno = _mrnno + "";
                            }
                            DispMsg("Pending Approved MRN(s) available. Please cancel the MRN(s) to close the job. " + _mrnno);
                            return;
                        }
                    }

                    //kapila 3/9/2015
                    _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                    if (_scvParam != null)
                        if (_scvParam.sp_need_act_def == 1)
                        {
                            DataTable _dtDef = CHNLSVC.General.get_job_defects(_selectedJobNo, "W");
                            if (_dtDef.Rows.Count == 0)
                            {
                                DispMsg("Please enter the actual defects.");
                                return;
                            }
                        }

                    popJobCloseConf.Show();
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        private void JobCloseProcess1()
        {
            string msg;
            Boolean _isWarRep12 = false;
            _isWarRep12 = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(_selectedJobNo);

            if (_isWarRep12 == false) //Sanjeewa 2016-03-21
            {
                if (!CheckSupplierClaim(out msg))
                {
                    DispMsg(msg, "Job Close");
                    return;
                }
            }
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam.SP_IS_RTN_OLDPT == 1 && !checkIsRetunAllSubItems())
            {
                //DispMsg("Please return all old parts and accresories to close the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(_selectedJobNo, Session["UserCompanyCode"].ToString());

            if (Convert.ToDateTime(txtJobCloseDt.Text) < oJOB_HDR.SJB_DT)
            {
                DispMsg("Please select a valid date");
                return;
            }

            Int32 pendingCount = 0;
            if (chkbulk.Checked == false)
            {
                pendingCount = getPendingMRNCount(_selectedJobNo, _selectedJobLine);

                if (pendingCount > 0)
                {
                    DispMsg("Pending MRN available for this job item.");
                    return;
                }
            }
            else
            {
                //foreach (int i in GblarrJobLine)
                //{

                //    pendingCount = getPendingMRNCount(_selectedJobNo, i);

                //    if (pendingCount > 0)
                //    {
                //        DispMsg("Pending MRN available for this job item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                pendingCount = getPendingMRNCount(_selectedJobNo, _selectedJobLine);

                if (pendingCount > 0)
                {
                    DispMsg("Pending MRN available for this job item.");
                    return;
                }
            }



            string itmCode;
            if (ddlCloseType.SelectedValue == "WRPL")
            {
                if (chkbulk.Checked == false)
                {
                    if (!checkSuppWarranty(out itmCode))
                    {
                        DispMsg("Please request for supplier warranty claim.\nItem Code : " + itmCode);
                        return;
                    }
                }
                else
                {

                    if (!checkSuppWarrantyBulk(out itmCode))
                    {
                        DispMsg("Please request for supplier warranty claim.\nItem Code : " + itmCode);
                        return;
                    }
                }


            }

            int result1 = 0;
            Int32 _res = 0;
            string _resMsg ="";
            //kapila 27/2/2016 get pending acceptance stage is allowed or not
            decimal _jbStage = 6;
            DataTable _dtJobHdr = CHNLSVC.CustService.sp_get_job_hdrby_jobno(_selectedJobNo); //kapila 22/4/2016
            //Comment Darshana --- as per sandun request......
            //DataTable _dtPend = CHNLSVC.CustService.GetPendingAcceptanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _dtJobHdr.Rows[0]["SJB_JOBCAT"].ToString());
            //if (_dtPend.Rows.Count > 0)
            //    if (Convert.ToDecimal(_dtPend.Rows[0]["scs_pend_accept"]) == 1)
            //        _jbStage = Convert.ToDecimal(5.2);

            if (chkbulk.Checked == false)
            {
                result1 = CHNLSVC.CustService.Update_Job_dates(_selectedJobNo, _selectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), Convert.ToDateTime(txtJobCloseDt.Text));
                result1 = CHNLSVC.CustService.Update_JobDetailStage(_selectedJobNo, _selectedJobLine, _jbStage);
                result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, _selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                Service_Job_StageLog oLog = new Service_Job_StageLog();
                oLog.SJL_REQNO = "";
                oLog.SJL_JOBNO = _selectedJobNo;
                oLog.SJL_JOBLINE = _selectedJobLine;
                oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                oLog.SJL_JOBSTAGE = _jbStage;
                oLog.SJL_CRE_BY = Session["UserID"].ToString();
                oLog.SJL_CRE_DT = DateTime.Now;
                oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                oLog.SJL_INFSUP = 0;
                result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                JobCloseAodProcess();
            }

            else
            {

                //foreach (int i in GblarrJobLine)
                //{
                //    result1 = CHNLSVC.CustService.Update_Job_dates(_selectedJobNo, i, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), dtpDate.Value);
                //    result1 = CHNLSVC.CustService.Update_JobDetailStage(_selectedJobNo, i, _jbStage);
                //    result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, _selectedJobNo, i, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                //    Service_Job_StageLog oLog = new Service_Job_StageLog();
                //    oLog.SJL_REQNO = "";
                //    oLog.SJL_JOBNO = _selectedJobNo;
                //    oLog.SJL_JOBLINE = i;
                //    oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                //    oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                //    oLog.SJL_JOBSTAGE = _jbStage;
                //    oLog.SJL_CRE_BY = Session["UserID"].ToString();
                //    oLog.SJL_CRE_DT = DateTime.Now;
                //    oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                //    oLog.SJL_INFSUP = 0;
                //    result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                //}
            }

            if (result1 > 0)
            {
                if (_res > 0)
                {
                    DispMsg("Record updated successfully");
                    isActionTaken = 1;
                    popJobClose.Hide();
                    _popJobCloseShow = false;
                }
                else
                {
                    DispMsg("Process Terminated." + _resMsg,"E");
                    return;
                }
            }
            else
            {
                DispMsg("Process Terminated.");
                return;
            }
        }
        protected void lbtnCloseJobClear_Click(object sender, EventArgs e)
        {
            txtRemarkNewItem.Text = "";
            txtAodLocation.Text = "";
            //lblBackDateInfor.Text = "";
            dgvOldPart.DataSource = new int[] { };
            dgvNewParts.DataSource = new List<Service_stockReturn>();
            txtNewPartWarehouse.Text = "";
            txtOPWarehouse.Text = "";
            lbtnOPReturn.Enabled = true; //kapila 9/2/2016

            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10824))
            {
                txtJobCloseDt.Enabled = false;
            }
            else
            {
                txtJobCloseDt.Enabled = true;
            }
        }

        protected void btnJobCloseNotConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnJobCloseConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAodLocation.Text))
                {
                    DispMsg("Please select the Dispatch location !"); return;
                }
                JobCloseProcess1();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private bool CheckSupplierClaim(out string msg)
        {
            bool status = true;
            msg = string.Empty;

            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ViewStockItems(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine, "", Session["UserDefLoca"].ToString());

            if (stockReturnItems != null && stockReturnItems.Count > 0)
            {
                status = true;
                return status;
            }

            List<Service_job_Det> oJobDets = CHNLSVC.CustService.GetJobDetails(_selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString());
            if (oJobDets != null && oJobDets.Count > 0)
            {
                Service_job_Det oJobDet = oJobDets[0];
                if (oJobDet.Jbd_reqwcn == 1 && oJobDet.Jbd_recwcn == 0 && oJobDet.Jbd_sentwcn == 1)
                {
                    msg = oJobDet.Jbd_itm_cd + " is sent to supplier claim.";
                    status = false;
                    return status;
                }

                List<Service_OldPartRemove> oOldparts = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, string.Empty, string.Empty);
                if (oOldparts != null && oOldparts.Count > 0)
                {
                    if (oOldparts.FindAll(x => x.SOP_REQWCN == 1 && x.SOP_RECWNC == 0).Count > 0)
                    {
                        string items = string.Empty;
                        List<Service_OldPartRemove> oOldpartsNew = oOldparts.FindAll(x => x.SOP_REQWCN == 1 && x.SOP_RECWNC == 0);
                        foreach (Service_OldPartRemove item in oOldpartsNew)
                        {
                            items += item.SOP_OLDITMCD + ", ";
                        }

                        msg = "Old parts send to supplier claim. Items : " + items;
                        status = false;
                        return status;
                    }
                }
            }

            return status;
        }
        private bool checkIsRetunAllSubItems()
        {
            bool status = true;

            if (lblWarStus.Text.ToUpper() == "UNDER WARRANTY")
            {
                List<Service_stockReturn> oNewItemsFromGrd = (List<Service_stockReturn>)dgvNewParts.DataSource;

                List<Service_OldPartRemove> oldPartListFromGrd = (List<Service_OldPartRemove>)dgvOldPart.DataSource;

                foreach (Service_stockReturn oNewItem in oNewItemsFromGrd)
                {
                    MasterItem oItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), oNewItem.ITEM_CODE);
                    oNewItem.Desc = oItem.Mi_itm_tp;
                }


                List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, string.Empty, string.Empty);
                List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => x.SOP_RTN_WH == 1);
                List<Service_Job_Det_Sub> oSubItems1 = CHNLSVC.CustService.GetServiceJobDetailSubItems(_selectedJobNo, _selectedJobLine);
                List<Service_Job_Det_Sub> oSubItems = oSubItems1.FindAll(x => x.JBDS_RTN_WH == 1);
                foreach (Service_OldPartRemove item in oldPartList1)
                {
                    MasterItem oItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.SOP_OLDITMCD);
                    item.SOP_TP_text = oItem.Mi_itm_tp;
                }

                foreach (Service_Job_Det_Sub item in oSubItems)
                {
                    Service_OldPartRemove oNewItem = new Service_OldPartRemove();
                    oNewItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
                    oNewItem.SOP_OLDITMSER1 = item.JBDS_SER1;
                    oNewItem.SOP_TP_text = item.JBDS_ITM_TP;
                    oldPartList.Add(oNewItem);
                }

                List<String> oItemTypes = oNewItemsFromGrd.Select(x => x.Desc).ToList();

                foreach (String itemType in oItemTypes)
                {

                    if (oNewItemsFromGrd.Count(x => x.Desc == itemType) > oldPartList.Count(x => x.SOP_TP_text == itemType))
                    {
                        string TypeText = string.Empty;
                        if (itemType == "M")
                        {
                            TypeText = "Main";
                        }
                        else if (itemType == "A")
                        {
                            TypeText = "Accessory";
                        }
                        else
                        {
                            TypeText = "other";
                        }

                       DispMsg("Please return '" + TypeText + "' type item");
                        status = false;
                    }
                }
            }

            return status;
        }
        private Int32 getPendingMRNCount(String jobNum, Int32 jobLine)
        {
            int RecCount = 0;

            try
            {
                InventoryHeader _inventoryRequest = new InventoryHeader();
                _inventoryRequest.Ith_oth_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Ith_doc_tp = "AOD";
                _inventoryRequest.Ith_oth_loc = Session["UserDefLoca"].ToString();
                _inventoryRequest.FromDate = "01-01-1900";
                _inventoryRequest.ToDate = "31-12-2999";

                DataTable dtTemp = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                if (dtTemp.Select("ith_direct = 0 AND ith_job_no='" + jobNum + "'").Length > 0)
                {
                    DataTable dtNew = dtTemp.Select("ith_direct = 0 AND ith_job_no='" + jobNum + "'").CopyToDataTable();
                    if (dtNew.Rows.Count > 0)
                    {
                        RecCount = dtNew.Rows.Count;
                    }
                    else
                    {
                        return RecCount;
                    }
                }
                else
                {
                    return RecCount;

                }

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "System Error");
            }
            return RecCount;
        }
        private bool checkSuppWarranty(out string itmCode)
        {
            bool status = true;
            itmCode = string.Empty;

            List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(_selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString());
            if (ojob_Det != null && ojob_Det.Count > 0)
            {
                Service_job_Det oJobDetail = ojob_Det[0];
                DateTime dtWarrDate;
                Int32 Period;

                if (oJobDetail.Jbd_swarrstartdt != null && oJobDetail.Jbd_swarrstartdt.Date != Convert.ToDateTime("01-Jan-1900").Date && oJobDetail.Jbd_swarrstartdt != DateTime.MinValue)
                {
                    dtWarrDate = oJobDetail.Jbd_swarrstartdt;
                    Period = oJobDetail.Jbd_swarrperiod;
                }
                else
                {
                    dtWarrDate = oJobDetail.Jbd_warrstartdt;
                    Period = oJobDetail.Jbd_warrperiod;
                }

                if (dtWarrDate.AddMonths(Period).Date >= DateTime.Now.Date)
                {
                    List<Service_supp_claim_itm> oSuppClaimItems = CHNLSVC.CustService.getSupClaimItems(Session["UserCompanyCode"].ToString(), oJobDetail.Jbd_supp_cd, null);
                    if (oSuppClaimItems != null && oSuppClaimItems.Count > 0)
                    {
                        if (oSuppClaimItems.FindAll(x => x.SSC_BRND == oJobDetail.Jbd_brand && x.SSC_SUPP == oJobDetail.Jbd_supp_cd).Count > 0)
                        {
                            if (dgvOldPart.Rows.Count > 0)
                            {
                                List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, string.Empty, string.Empty);
                                List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1));

                                foreach (Service_OldPartRemove oOldPartRemove in oldPartList)
                                {
                                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), oOldPartRemove.SOP_OLDITMCD);
                                    if (oOldPartRemove.SOP_REQUESTNO == "0")
                                    {
                                        status = false;
                                        itmCode = oOldPartRemove.SOP_OLDITMCD;
                                    }
                                }
                            }
                            else
                            {
                                if (oJobDetail.Jbd_reqwcn == 0)
                                {
                                    itmCode = oJobDetail.Jbd_itm_cd;
                                    status = false;
                                }
                            }
                        }
                    }
                }
            }

            return status;
        }
        private bool checkSuppWarrantyBulk(out string itmCode)
        {
            bool status = false;
            itmCode = "";
            //itmCode = string.Empty;
            //foreach (int i in GblarrJobLine)
            //{
            //    List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(_selectedJobNo, i, Session["UserCompanyCode"].ToString());
            //    if (ojob_Det != null && ojob_Det.Count > 0)
            //    {
            //        Service_job_Det oJobDetail = ojob_Det[0];
            //        DateTime dtWarrDate;
            //        Int32 Period;

            //        if (oJobDetail.Jbd_swarrstartdt != null && oJobDetail.Jbd_swarrstartdt.Date != Convert.ToDateTime("01-Jan-1900").Date && oJobDetail.Jbd_swarrstartdt != DateTime.MinValue)
            //        {
            //            dtWarrDate = oJobDetail.Jbd_swarrstartdt;
            //            Period = oJobDetail.Jbd_swarrperiod;
            //        }
            //        else
            //        {
            //            dtWarrDate = oJobDetail.Jbd_warrstartdt;
            //            Period = oJobDetail.Jbd_warrperiod;
            //        }

            //        if (dtWarrDate.AddMonths(Period).Date >= DateTime.Now.Date)
            //        {
            //            List<Service_supp_claim_itm> oSuppClaimItems = CHNLSVC.CustService.getSupClaimItems(Session["UserCompanyCode"].ToString(), oJobDetail.Jbd_supp_cd, null);
            //            if (oSuppClaimItems != null && oSuppClaimItems.Count > 0)
            //            {
            //                if (oSuppClaimItems.FindAll(x => x.SSC_BRND == oJobDetail.Jbd_brand && x.SSC_SUPP == oJobDetail.Jbd_supp_cd).Count > 0)
            //                {
            //                    if (dgvOldPart.Rows.Count > 0)
            //                    {
            //                        List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, i, string.Empty, string.Empty);
            //                        List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1));

            //                        foreach (Service_OldPartRemove oOldPartRemove in oldPartList)
            //                        {
            //                            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), oOldPartRemove.SOP_OLDITMCD);
            //                            if (oOldPartRemove.SOP_REQUESTNO == "0")
            //                            {
            //                                status = false;
            //                                itmCode = oOldPartRemove.SOP_OLDITMCD;

            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (oJobDetail.Jbd_reqwcn == 0)
            //                        {
            //                            itmCode = oJobDetail.Jbd_itm_cd;
            //                            status = false;

            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            return status;
        }

        protected void lbtnOldPartRemove_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedJobNo = "";
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    DispMsg("Please select a job # !");
                    txtJobNo.Focus();
                    return;
                }
                if (_selectedJobLine <= 0)
                {
                    DispMsg("Please select a job item !");
                    return;
                }
                _selectedJobNo = txtJobNo.Text.Trim();
                dgvOPR.DataSource = new int[] { };
                dgvOPR.DataBind();
                txtOldPartRemDt.Text = DateTime.Now.ToString("dd/MMM/yyyy");
               // ddlOPRItemSts.SelectedIndex = ddlOPRItemSts.Items.IndexOf(ddlOPRItemSts.Items.FindByValue("OLDPT"));
               // ddlOPRItemSts.Enabled = false;
                lbtnOPRClear_Click(null, null);
                if (AddOldItemAuto)
                {
                    Auto_RemoveOldPart();
                }
                lbtnOPRSave.Visible = false;
                lbtnOPRView_Click(null, null);
                popOldPartRemShow = true;
                popOldPartRem.Show();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
            
        }

        protected void lbtnOldPartRemClose_Click(object sender, EventArgs e)
        {
            popOldPartRemShow = false;
            popOldPartRem.Hide();
        }

        protected void chkIsPeri_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnLocationErrOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
        }

        protected void lbtnLocationErrNo_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txtOPRItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtOPRItem.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtOPRItem.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    _serData = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_para, "ITEM", txtOPRItem.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtOPRItem.Text.ToUpper().Trim(), "ITEM", "Description");
                    txtOPRItem.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        string _cat1="",_cat2="",_cat3="";
                        LoadItemDetail(txtOPRItem.Text, out _cat1, out _cat2, out _cat3);
                        if ((_cat1 == "PH" && _cat2 == "MOB" && _cat3 == "MU") || (_cat1 == "PHACC" && _cat2 == "MOACC" && _cat3 == "MPCB"))
                        {
                            txtOPRSerial.Text = _serial;
                        }
                    }
                    else
                    {
                        txtOPRItem.Text = string.Empty;
                        txtOPRItem.Focus();
                        DispMsg("Please enter valid item code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private bool LoadItemDetail(string _item, out string _cat1, out string _cat2, out string _cat3)
        {
            lblOPRDes.Text = "Description : " + string.Empty;
            lblOPRModel.Text = "Model : " + string.Empty;
            lblOPRBrand.Text = "Brand : " + string.Empty;
            lblOPRStus.Text = "Serial Status : " + string.Empty;
            _itemdetail = new MasterItem();
            _cat1 = "";
            _cat2 = "";
            _cat3 = "";

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                _cat1 = _itemdetail.Mi_cate_1;
                _cat2 = _itemdetail.Mi_cate_2;
                _cat3 = _itemdetail.Mi_cate_3;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblOPRDes.Text = "Description : " + _description;
                lblOPRModel.Text = "Model : " + _model;
                lblOPRBrand.Text = "Brand : " + _brand;
                lblOPRStus.Text = "Serial Status : " + _serialstatus;

              //  lblDesc.Text = _description;
                //lblPartNo.Text = _itemdetail.Mi_part_no;

                if (_itemdetail.Mi_is_ser1 == 0)
                {
                    txtOPRSerial.Enabled = false;
                    txtOPRSerial.Text = "N/A";
                    txtOPRQty.Enabled = true;
                    txtOPRQty.Text="";
                }
                else
                {
                    txtOPRSerial.Enabled = true;
                    txtOPRSerial.Text = "";
                    txtOPRQty.Enabled = false;
                    txtOPRQty.Text = "1";
                }

                if (!chkIsPeri.Checked)
                {
                    List<MST_ITM_CAT_COMP> oTemp = CHNLSVC.CustService.getMasterItemCategoryComByItem(_item);
                    if (oTemp != null && oTemp.Count > 0)
                    {
                        _isValid = false;
                    }
                }
            }

            else _isValid = false;
            return _isValid;
        }

        protected void lbtnSeOprItm_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                _serData = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_para, null, null);
                LoadSearchPopup("InvoiceItemUnAssable", "ITEM", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }

        protected void txtOPRSerial_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txtOPRQty_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnOPRAddItem_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnOPRAddRemark_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOPRItem.Text))
                {
                    DispMsg("Please enter item code.");
                    txtOPRItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtOPRQty.Text))
                {
                    DispMsg("Please enter quantity.");
                    txtOPRQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtOPRSerial.Text))
                {
                    DispMsg("Please enter serial.");
                    txtOPRSerial.Focus();
                    return;
                }
                if (ddlOPRItemSts.SelectedIndex == 0)
                {
                    DispMsg("Please Item Status.");
                    ddlOPRItemSts.Focus();
                    return;
                }
                if (MailList.Count > 0)
                {
                    if (txtOPRSerial.Text == "N/A")
                    {
                        if (MailList.FindAll(x => x.SOP_OLDITMCD == txtOPRItem.Text && x.SOP_OLDITMSTUS == ddlOPRItemSts.SelectedValue.ToString()).Count > 0)
                        {
                            Service_OldPartRemove itemExsist = new Service_OldPartRemove();
                            itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtOPRItem.Text && x.SOP_OLDITMSTUS == ddlOPRItemSts.SelectedValue.ToString());
                            itemExsist.SOP_OLDITMQTY = itemExsist.SOP_OLDITMQTY + Convert.ToDecimal(txtOPRQty.Text);
                            clerEntryLine();
                            bindData();
                            return;
                        }
                    }
                    else
                    {
                        if (MailList.FindAll(x => x.SOP_OLDITMCD == txtOPRItem.Text && x.SOP_OLDITMSTUS == ddlOPRItemSts.SelectedValue.ToString() && x.SOP_OLDITMSER1 == txtOPRSerial.Text).Count > 0)
                        {
                            Service_OldPartRemove itemExsist = new Service_OldPartRemove();
                            itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtOPRItem.Text && x.SOP_OLDITMSTUS == ddlOPRItemSts.SelectedValue.ToString() && x.SOP_OLDITMSER1 == txtOPRSerial.Text);
                            DispMsg("This serialized item is already added.");
                            return;
                        }
                    }
                }

                Service_OldPartRemove item = new Service_OldPartRemove();
                item.dgvLine = MailList.Count > 0 ? MailList.Count + 1 : 1;
                item.SOP_DT = DateTime.Today;
                item.SOP_COM = Session["UserCompanyCode"].ToString();
                item.SOP_JOBNO = _selectedJobNo;
                item.SOP_JOBLINE = _selectedJobLine;
                item.SOP_OLDITMCD = txtOPRItem.Text;
                item.SOP_OLDITMSTUS = ddlOPRItemSts.SelectedValue.ToString();
                item.SOP_OLDITMSTUS_Text = ddlOPRItemSts.Text.ToString();
                item.SOP_OLDITMSER1 = txtOPRSerial.Text;
                item.SOP_OLDITMWARR = "";
                item.SOP_OLDITMQTY = Convert.ToDecimal(txtOPRQty.Text);
                item.SOP_DOC_NO = "";
                item.SOP_IS_SETTLED = 0;
                item.SOP_BASE_DOC = "";
                item.SOP_REQWCN = 0;
                item.SOP_SENTWCN = 0;
                item.SOP_RECWNC = 0;
                item.SOP_TAKEWCN = 0;
                item.SOP_CRE_BY = Session["UserID"].ToString();
                item.SOP_CRE_DT = DateTime.Today;
                item.SOP_REFIX = 0;
                item.SOP_RMK = txtOPRRemark.Text;
                item.DESCRIPTION = lblOPRDes.Text;
                //item.PARTNO = lbloprPartNo.Text;
                if (chkIsPeri.Checked)
                {
                    item.SOP_TP = "P";
                }
                else
                {
                    item.SOP_TP = "O";
                }
                if (oCateComp != null && oCateComp.Mcc_supp_warr == 1)
                {
                    item.SOP_ISSUPPWARR = 1;
                }
                else
                {
                    item.SOP_ISSUPPWARR = 0;
                }

                MailList.Add(item);
                clerEntryLine();
                bindData();
                AddOldItemAuto = false;
                //toolStrip1.Focus();
                //btnSave.Select();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void clerEntryLine()
        {
            txtOPRItem.Text = "";
            txtOPRSerial.Text = "";
            //cmbStatus.SelectedValue = "OLDPT";
            txtOPRQty.Text = "";
            txtOldPartRemDt.Text = DateTime.Today.ToString("dd/MMM/yyyy");

            lblOPRDes.Text = "Description : " + string.Empty;
            lblOPRModel.Text = "Model : " + string.Empty;
            lblOPRBrand.Text = "Brand : " + string.Empty;
            lblOPRStus.Text = "Serial Status : " + string.Empty;
            txtOPRRemark.Text = "";
        }

        protected void chkOPRSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnOPRView_Click(object sender, EventArgs e)
        {
            try
            {
                MailList = new List<Service_OldPartRemove>();
                MailList.AddRange(CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, txtOPRItem.Text, txtOPRSerial.Text));

                if (MailList.Count > 0)
                {
                    foreach (Service_OldPartRemove item in MailList)
                    {
                        item.dgvLine = MailList.Max(x => x.dgvLine) + 1;
                    }
                }

                bindData();

                //if (dgvItems.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dgvItems.Rows.Count; i++)
                //    {
                //        if (dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
                //        {
                //            dgvItems.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
                //        }
                //        if (dgvItems.Rows[i].Cells["SOP_TP"].Value.ToString() == "P")
                //        {
                //            dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Peripheral";
                //        }
                //        else
                //        {
                //            dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Old Part";
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }
        private void bindData()
        {
            dgvOPR.DataSource = new List<Service_OldPartRemove>();
            dgvOPR.DataBind();
            if (MailList.Count > 0)
            {
                dgvOPR.DataSource = MailList;
                //for (int i = 0; i < dgvOPR.Rows.Count; i++)
                //{
                //    //if (dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
                //    //{
                //    //    dgvItems.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
                //    //}
                //    //if (dgvItems.Rows[i].Cells["SOP_TP"].Value.ToString() == "P")
                //    //{
                //    //    dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Peripheral";
                //    //}
                //    //else
                //    //{
                //    //    dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Old Part";
                //    //}
                //}
               dgvOPR.DataBind();
            }

            txtOPRItem.Focus();
        }
        protected void lbtnOPRSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MailList.Count > 0)
                {
                    if (isAnySelected())
                    {
                        List<Service_OldPartRemove> SaveList = new List<Service_OldPartRemove>();
                        foreach (GridViewRow row  in dgvOPR.Rows)
                        {
                            CheckBox chkOPRSelect = row.FindControl("chkOPRSelect") as CheckBox;
                            Label lblSOP_OLDITMCD = row.FindControl("lblSOP_OLDITMCD") as Label;
                            Label lblSOP_OLDITMSTUS = row.FindControl("lblSOP_OLDITMSTUS") as Label;
                            Label lblSOP_OLDITMSER1New = row.FindControl("lblSOP_OLDITMSER1New") as Label;
                            if (chkOPRSelect.Checked)
                            {
                                string item = string.Empty;
                                string status = string.Empty;
                                string Serial = string.Empty;
                                item = lblSOP_OLDITMCD.Text;
                                status = lblSOP_OLDITMSTUS.Text;

                                //By Akila 2017/05/09
                                string _newSerial = lblSOP_OLDITMSER1New.Text;
                                Serial = _newSerial;
                                //Serial = dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString();

                                SaveList.Add(MailList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial));
                            }
                        }
                        string err;
                        int result = CHNLSVC.CustService.Save_OldParts(SaveList, out err);

                        if (result > 0)
                        {
                            DispMsg("Record insert successfully.","S");
                            IsOldItemAdded = true;
                            lbtnOPRClear_Click(null, null);
                            return;
                        }
                        else
                        {
                            IsOldItemAdded = false;
                            DispMsg("Process Terminated. ","E");
                            return;
                        }
                    }
                    else
                    {
                        IsOldItemAdded = false;
                        DispMsg("Please select items to process. ");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnOPRRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (MailList.Count > 0)
                {
                    if (AddOldItemAuto)
                    {
                        int _selactedRowCount = 0;
                        if (dgvOPR.Rows.Count > 0)
                        {
                            DataTable _removedOldParts = new DataTable();
                            _removedOldParts = CHNLSVC.CustService.GetPartRemoveByJobline(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine);

                            foreach (GridViewRow row in dgvOPR.Rows)
                            {
                                CheckBox chkOPRSelect = row.FindControl("chkOPRSelect") as CheckBox;
                                if (chkOPRSelect.Checked)
                                {
                                    Label lblSOP_OLDITMSER1New = row.FindControl("lblSOP_OLDITMSER1New") as Label;
                                    Label lblSOP_OLDITMCD = row.FindControl("lblSOP_OLDITMCD") as Label;
                                    if (string.IsNullOrEmpty(lblSOP_OLDITMSER1New.Text))
                                    {
                                        DispMsg("Please Enter New Serial #","N");
                                        return;
                                    }
                                    if (_removedOldParts.Rows.Count > 0)
                                    {
                                        string _newSerialNo = lblSOP_OLDITMSER1New.Text;
                                        string _item = lblSOP_OLDITMCD.Text;

                                        foreach (DataRow _oldPart in _removedOldParts.Rows)
                                        {
                                            string _itemCode = _oldPart["task_ref"] == DBNull.Value ? string.Empty : _oldPart["task_ref"].ToString();
                                            string _serialNo = _oldPart["serial_no"] == DBNull.Value ? string.Empty : _oldPart["serial_no"].ToString();

                                            // prevent removing same item continously;
                                            if ((_item == _itemCode) && (_newSerialNo == _serialNo))
                                            {
                                                DispMsg("This part all ready has been removed " + _serialNo);
                                                return;
                                            }
                                        }
                                    }
                                    _selactedRowCount += 1;

                                }
                            }
                            if (_selactedRowCount > (dgvOPR.Rows.Count - _removedOldParts.Rows.Count))
                            {
                                DispMsg("Cannot remove parts. Some of the parts all ready have been removed.");
                                return;
                            }
                        }

                        if (_selactedRowCount < 1)
                        {
                            DispMsg("Please select a part to remove ");
                            return;
                        }

                        UpdateRemovePartList();
                    }
                    // Akial 2017/05/09 will be removed selected items only               

                    string err;
                    int result = CHNLSVC.CustService.Save_OldParts(MailList, out err);

                    if (result > 0)
                    {
                        DispMsg("Record insert successfully.","S");
                        lbtnOPRClear_Click(null, null);
                        lbtnOPRView_Click(null, null);
                        return;
                    }
                    else
                    {
                        DispMsg("Process Terminated." ,"E");
                        return;
                    }
                }
                else
                {
                    DispMsg("Please add items.");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private bool isAnySelected()
        {
            bool status = false;

            if (dgvOPR.Rows.Count > 0)
            {
                foreach (GridViewRow row in dgvOPR.Rows)
                {
                    CheckBox chkOPRSelect = row.FindControl("chkOPRSelect") as CheckBox;
                    if (chkOPRSelect.Checked)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }
        protected void lbtnOPRRefix_Click(object sender, EventArgs e)
        {
            try
            {
                if (MailList.Count > 0)
                {
                    if (isAnySelected())
                    {
                        List<Service_OldPartRemove> SaveList = new List<Service_OldPartRemove>();
                        foreach (GridViewRow row in dgvOPR.Rows)
                        { 
                          CheckBox chkOPRSelect = row.FindControl("chkOPRSelect") as CheckBox;
                          if (chkOPRSelect.Checked)
                          {
                              Label lblSOP_OLDITMSER1New = row.FindControl("lblSOP_OLDITMSER1New") as Label;
                              Label lblSOP_OLDITMCD = row.FindControl("lblSOP_OLDITMCD") as Label;
                              Label lblSOP_OLDITMSTUS = row.FindControl("lblSOP_OLDITMSTUS") as Label;
                              Label lblSOP_OLDITMQTY = row.FindControl("lblSOP_OLDITMQTY") as Label;
                              string item = string.Empty;
                              string status = string.Empty;
                              string Serial = string.Empty;
                              decimal DgvQty = 0;
                              item = lblSOP_OLDITMCD.Text;
                              status = lblSOP_OLDITMSTUS.Text;
                              //By Akila 2017/05/09
                              string _newSerial = lblSOP_OLDITMSER1New.Text;
                              Serial = lblSOP_OLDITMSER1New.Text;
                              DgvQty = Convert.ToDecimal(lblSOP_OLDITMQTY.Text);
                              Service_OldPartRemove tempItem = MailList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial);
                              List<Service_OldPartRemove> DBList = CHNLSVC.CustService.Get_SCV_Oldparts(_selectedJobNo, _selectedJobLine, txtOPRItem.Text, txtOPRSerial.Text);
                              Service_OldPartRemove editedItem = DBList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial);
                              if (editedItem == null)
                              {
                                  return;
                              }
                              tempItem.SOP_OLDITMQTY = editedItem.SOP_OLDITMQTY - DgvQty;
                              SaveList.Add(tempItem);
                          }
                        }
                       
                        string err;
                        int result = CHNLSVC.CustService.Update_SCV_Oldpart_Refix(SaveList, out err);

                        if (result > 0)
                        {
                            DispMsg("Successfully re-fixed.","S");
                            lbtnOPRClear_Click(null, null);
                            lbtnOPRView_Click(null, null);
                            return;
                        }
                        else
                        {
                            DispMsg("Process Terminated.\n" + err,"E");
                            return;
                        }
                    }
                    else
                    {
                        DispMsg("Please select items to re-fix");
                        return;
                    }
                }
                else
                {
                    DispMsg("Please add items.");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnOPRClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtOPRItem.Text = "";
                txtOPRSerial.Text = "";
                txtOPRQty.Text = "";
                //cmbStatus.SelectedValue = "OLDPT"; Sanjeewa 2017-02-09
                txtOldPartRemDt.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                dgvOPR.DataSource = new int[] { };
                dgvOPR.DataBind();
                MailList = new List<Service_OldPartRemove>();

                lblOPRDes.Text = "Description : " + string.Empty;
                lblOPRModel.Text = "Model : " + string.Empty;
                lblOPRBrand.Text = "Brand : " + string.Empty;
                lblOPRStus.Text = "Serial Status : " + string.Empty;
               // chkSelectAll.Checked = false;
                txtOPRRemark.Text = "";
                _serial = "";
                //this.colNewSerial.Visible = false;
                oItem = new Service_job_Det();
                oItms = CHNLSVC.CustService.GetJobDetails(_selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString());
                if (oItms.Count > 0)
                {
                    oItem = oItms[0];
                    _serial = oItms[0].Jbd_ser1;
                    if (oItem.Jbd_sentwcn == 1)
                    {
                        DispMsg("Job item has send to warranty claim","N");
                        lbtnOPRRemove.Enabled = false;
                        lbtnOPRRefix.Enabled = false;
                        return;
                    }
                }
                oHeader = CHNLSVC.CustService.GetServiceJobHeader(_selectedJobNo, Session["UserCompanyCode"].ToString());
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void UpdateRemovePartList()
        {
            try
            {
                if (dgvOPR.Rows.Count > 0)
                {
                    foreach (GridViewRow row in dgvOPR.Rows)
                    {
                        CheckBox chkOPRSelect = row.FindControl("chkOPRSelect") as CheckBox;
                        Label lblSOP_OLDITMSER1New = row.FindControl("lblSOP_OLDITMSER1New") as Label;
                        Label lblSOP_OLDITMCD = row.FindControl("lblSOP_OLDITMCD") as Label;
                        Label lblSOP_OLDITMSER1 = row.FindControl("lblSOP_OLDITMSER1") as Label;
                        if (chkOPRSelect.Checked)
                        {
                            string _newSerial = lblSOP_OLDITMSER1New.Text;
                            string _item = lblSOP_OLDITMCD.Text;
                            string _serial = lblSOP_OLDITMSER1.Text;
                            if ((!string.IsNullOrEmpty(_newSerial)) && (!string.IsNullOrWhiteSpace(_newSerial)))
                            {
                                MailList.Where(x => x.SOP_OLDITMCD == _item && x.SOP_OLDITMSER1 == _serial).ToList().ForEach(x => x.SOP_OLDITMSER1 = _newSerial);
                            }
                        }
                        
                    }
                    dgvOPR.DataSource = MailList;
                    dgvOPR.DataBind();
                }
            }
            catch (Exception ex)
            {
                DispMsg("An error occured while updating old item serial" ,"E");
            }
        }

        protected void lbtnReqPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(dgvMRNDetails.SelectedRows[0].Cells[1].Value.ToString())) return;
                //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                //BaseCls.GlbReportName = string.Empty;
                //GlbReportName = string.Empty;
                //_view.GlbReportName = string.Empty;
                //_view.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                //_view.GlbReportDoc = dgvMRNDetails.SelectedRows[0].Cells[1].Value.ToString();
                //_view.Show();
                //_view = null;
            }
            catch (Exception ex)
            {
                ////this.Cursor = Cursors.Default; SystemErrorMessage(ex); 
                //DispMsg("Please select MRN number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void lbtnCloseReq_Click(object sender, EventArgs e)
        {

        }

        public void Auto_RemoveOldPart()
        {

            try
            {
                List<Service_stockReturn> SelectedParts = new List<Service_stockReturn>();
                SelectedParts = CHNLSVC.CustService.Get_ServiceWIP_ViewStockItems(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine, "", Session["UserDefLoca"].ToString());

                MailList = new List<Service_OldPartRemove>();
                if (SelectedParts != null)
                {
                    if (SelectedParts.Count > 0)
                    {
                        foreach (Service_stockReturn _parts in SelectedParts)
                        {
                            Service_OldPartRemove item = new Service_OldPartRemove();
                            item.dgvLine += 1;
                            item.SOP_DT = DateTime.Today;
                            item.SOP_COM = Session["UserCompanyCode"].ToString();
                            item.SOP_JOBNO = _selectedJobNo;
                            item.SOP_JOBLINE = _selectedJobLine;
                            item.SOP_OLDITMCD = _parts.ITEM_CODE;
                            item.SOP_OLDITMSTUS = "OLDPT";
                            item.SOP_OLDITMSTUS_Text = "OLD_PART";
                            item.SOP_OLDITMSER1 = "N/A";
                            item.SOP_OLDITMWARR = "";
                            item.SOP_OLDITMQTY = Convert.ToDecimal(_parts.QTY);
                            item.SOP_DOC_NO = "";
                            item.SOP_IS_SETTLED = 0;
                            item.SOP_BASE_DOC = "";
                            item.SOP_REQWCN = 0;
                            item.SOP_SENTWCN = 0;
                            item.SOP_RECWNC = 0;
                            item.SOP_TAKEWCN = 0;
                            item.SOP_CRE_BY = Session["UserID"].ToString();
                            item.SOP_CRE_DT = DateTime.Today;
                            item.SOP_REFIX = 0;
                            item.SOP_RMK = "";
                            item.DESCRIPTION = _parts.Desc;
                            item.PARTNO = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _parts.ITEM_CODE).Mi_part_no;
                            item.SOP_TP = "O";
                            item.SOP_ISSUPPWARR = 0;
                            item.SOP_OLDSERID = string.IsNullOrEmpty(_parts.SERIAL_ID) ? 0 : Convert.ToInt32(_parts.SERIAL_ID); //Add by akila
                            MailList.Add(item);
                        }

                        bindData();
                        //foreach (var item in collection)
                        //{
                        //    this.colNewSerial.Visible = true;
                        //}
                        
                        IsOldItemAdded = true;
                    }
                }

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }
        private void JobCloseProcess()
        {
            //Tharanga 2017?may/31
            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters.SP_IS_SPCPER_JBOPN == 1)
            {
               // btnSave.Enabled = false;
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10825))
                {
                   // btnSave.Enabled = false;
                    DispMsg("You don't have permission. Permission code : 10825");
                   // return;
                }
                else
                {
                   // btnSave.Enabled = true;
                }

            }
            else
            {
                //btnSave.Enabled = true;
            }



            if (lblJobStageNew.Text == "6")
            {
                //if (DispMsg("Do you want to Re-Open the job?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}

                ////DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(Session["UserCompanyCode"].ToString(), _selectedJobNo, _selectedJobLine);
                ////if (dtTemp != null && dtTemp.Rows.Count > 0 && dtTemp.Select("MSS_DESC <> 'CANCEL'").ToDataTable().Rows.Count > 0)
                ////{
                ////    DispMsg("Pending MRN available for this job item.", "Question", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////    return;
                ////}


                //int result1 = CHNLSVC.CustService.Update_Job_dates(_selectedJobNo, _selectedJobLine, DateTime.Now, DateTime.Now, dtpDate.Value, dtpDate.Value);
                //result1 = CHNLSVC.CustService.Update_JobDetailStage(_selectedJobNo, _selectedJobLine, 5);
                ////result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, _selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                //Service_Job_StageLog oLog = new Service_Job_StageLog();
                //oLog.SJL_REQNO = "";
                //oLog.SJL_JOBNO = _selectedJobNo;
                //oLog.SJL_JOBLINE = _selectedJobLine;
                //oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                //oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                //oLog.SJL_JOBSTAGE = 5;
                //oLog.SJL_CRE_BY = Session["UserID"].ToString();
                //oLog.SJL_CRE_DT = DateTime.Now;
                //oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                //oLog.SJL_INFSUP = 0;
                //result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);



                //if (result1 > 0)
                //{
                //    DispMsg("Successfully job Re-Opened.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    isActionTaken = 2;
                //    this.Close();
                //    return;
                //}
                //else
                //{
                //    DispMsg("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }
            else
            {
                //By akila 2017/05/08 - if git items available, service not allow to close
                if (IsHavingGitItems)
                {
                    DispMsg("Job cannot be closed when ther are GIT available");
                    return;
                }

                //Sanjeewa 2017-02-25
                if (Session["UserCompanyCode"].ToString() != "ABE") //Removed as per request 2017-03-10
                {
                    DataTable _mrn = CHNLSVC.CustService.checkAppMRNforJob(_selectedJobNo);
                    string _mrnno = "";
                    if (_mrn.Rows.Count > 0)
                    {
                        for (int i = 0; i < _mrn.Rows.Count; i++)
                        {
                            _mrnno = _mrnno + "";
                        }
                        DispMsg("Pending Approved MRN(s) available. Please cancel the MRN(s) to close the job. " + _mrnno);
                        return;
                    }
                }

                //kapila 3/9/2015
                _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                if (_scvParam != null)
                    if (_scvParam.sp_need_act_def == 1)
                    {
                        DataTable _dtDef = CHNLSVC.General.get_job_defects(_selectedJobNo, "W");
                        if (_dtDef.Rows.Count == 0)
                        {
                            DispMsg("Please enter the actual defects.");
                            return;
                        }
                    }

                string msg;
                Boolean _isWarRep12 = false;
                _isWarRep12 = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(_selectedJobNo);

                if (_isWarRep12 == false) //Sanjeewa 2016-03-21
                {
                    if (!CheckSupplierClaim(out msg))
                    {
                        DispMsg(msg, "Job Close");
                        return;
                    }
                }

                if (_scvParam.SP_IS_RTN_OLDPT == 1 && !checkIsRetunAllSubItems())
                {
                    //DispMsg("Please return all old parts and accresories to close the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(_selectedJobNo, Session["UserCompanyCode"].ToString());
                DateTime _tmpDt = DateTime.MinValue;
                DateTime _JobCloseDate = DateTime.TryParse(txtJobCloseDt.Text.Trim(), out _tmpDt)?Convert.ToDateTime(txtJobCloseDt.Text.Trim()):DateTime.MinValue;
                if (_JobCloseDate == DateTime.MinValue)
                {
                    DispMsg("Please select a valid date"); return;
                }
                if (_JobCloseDate < oJOB_HDR.SJB_DT)
                {
                    DispMsg("Please select a valid date");
                    return;
                }

                Int32 pendingCount = 0;
                if (chkbulk.Checked == false)
                {
                    pendingCount = getPendingMRNCount(_selectedJobNo, _selectedJobLine);

                    if (pendingCount > 0)
                    {
                        DispMsg("Pending MRN available for this job item.");
                        return;
                    }
                }
                else
                {
                    //foreach (int i in GblarrJobLine)
                    //{

                    //    pendingCount = getPendingMRNCount(_selectedJobNo, i);

                    //    if (pendingCount > 0)
                    //    {
                    //        DispMsg("Pending MRN available for this job item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}
                }



                string itmCode;
                if (ddlCloseType.SelectedValue == "WRPL")
                {
                    if (chkbulk.Checked == false)
                    {
                        if (!checkSuppWarranty(out itmCode))
                        {
                            DispMsg("Please request for supplier warranty claim.\nItem Code : " + itmCode);
                            return;
                        }
                    }
                    else
                    {

                        if (!checkSuppWarrantyBulk(out itmCode))
                        {
                            DispMsg("Please request for supplier warranty claim.\nItem Code : " + itmCode);
                            return;
                        }
                    }


                }

                int result1 = 0;

                //kapila 27/2/2016 get pending acceptance stage is allowed or not
                decimal _jbStage = 6;
                DataTable _dtJobHdr = CHNLSVC.CustService.sp_get_job_hdrby_jobno(_selectedJobNo); //kapila 22/4/2016
                //Comment Darshana --- as per sandun request......
                //DataTable _dtPend = CHNLSVC.CustService.GetPendingAcceptanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _dtJobHdr.Rows[0]["SJB_JOBCAT"].ToString());
                //if (_dtPend.Rows.Count > 0)
                //    if (Convert.ToDecimal(_dtPend.Rows[0]["scs_pend_accept"]) == 1)
                //        _jbStage = Convert.ToDecimal(5.2);

                if (chkbulk.Checked == false)
                {
                    #region move to JobClosed
                    /*
                    result1 = CHNLSVC.CustService.Update_Job_dates(_selectedJobNo, _selectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), _JobCloseDate);
                    result1 = CHNLSVC.CustService.Update_JobDetailStage(_selectedJobNo, _selectedJobLine, _jbStage);
                    result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, _selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = _selectedJobNo;
                    oLog.SJL_JOBLINE = _selectedJobLine;
                    oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                    oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                    oLog.SJL_JOBSTAGE = _jbStage;
                    oLog.SJL_CRE_BY = Session["UserID"].ToString();
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                    oLog.SJL_INFSUP = 0;
                    result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog); */
                    #endregion
                    
                    string _resMsg = "";
                    JobCloseAdjusmentProcess(out _resMsg);
                }

                else
                {

                    //foreach (int i in GblarrJobLine)
                    //{
                    //    result1 = CHNLSVC.CustService.Update_Job_dates(_selectedJobNo, i, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), dtpDate.Value);
                    //    result1 = CHNLSVC.CustService.Update_JobDetailStage(_selectedJobNo, i, _jbStage);
                    //    result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, _selectedJobNo, i, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                    //    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    //    oLog.SJL_REQNO = "";
                    //    oLog.SJL_JOBNO = _selectedJobNo;
                    //    oLog.SJL_JOBLINE = i;
                    //    oLog.SJL_COM = Session["UserCompanyCode"].ToString();
                    //    oLog.SJL_LOC = Session["UserDefLoca"].ToString();
                    //    oLog.SJL_JOBSTAGE = _jbStage;
                    //    oLog.SJL_CRE_BY = Session["UserID"].ToString();
                    //    oLog.SJL_CRE_DT = DateTime.Now;
                    //    oLog.SJL_SESSION_ID = Session["SessionID"].ToString();
                    //    oLog.SJL_INFSUP = 0;
                    //    result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                    //}
                }

                if (result1 > 0)
                {
                    DispMsg("Record updated successfully", "S");
                    isActionTaken = 1;
                }
                else
                {
                    DispMsg("Process Terminated.","E");
                    return;
                }
            }
        }

        protected void ddlMrnItmStus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMrnItm_TextChanged(null,null);
        }

        protected void lbtnSeAodLocation_Click(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = true;
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, null, null);
                LoadSearchPopup("AODLocation", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "EX");
            }
        }

        protected void txtAodLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = true;
                txtAodLocation.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtAodLocation.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, "CODE", txtAodLocation.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtAodLocation.Text.ToUpper().Trim(), "CODE", "Description");
                    txtAodLocation.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtAodLocation.Text = string.Empty;
                        txtAodLocation.Focus();
                        DispMsg("Please enter valid warehouse !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void JobCloseAodProcess()
        {
            try
            {
                if (string.IsNullOrEmpty(txtAodLocation.Text))
                {
                    DispMsg("Please select the Dispatch location !"); return;
                }
                List<ReptPickSerials> _rptSerialList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> _rptSubSerialList = new List<ReptPickSerialsSub>();
                List<TmpValidation> _errList = new List<TmpValidation>();
                string _docAdjMines = "", _docAdjPlus = "", _docAodOutNo = "",_docAdjMinesREQ="", _error = "";
                MasterLocation _mstOutLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                #region ADJ Mines for Job Serial
                #region Fill MasterAutoNumber
                MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                _autonoMinus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoMinus.Aut_cate_tp = "LOC";
                _autonoMinus.Aut_direction = null;
                _autonoMinus.Aut_modify_dt = null;
                _autonoMinus.Aut_moduleid = "ADJ";
                _autonoMinus.Aut_number = 5;
                _autonoMinus.Aut_start_char = "ADJ";
                _autonoMinus.Aut_year = null;
                #endregion
                #region Fill InventoryHeader ADJ -
                InventoryHeader _invHdrAdjMines = new InventoryHeader();
                _invHdrAdjMines.Ith_sbu = _mstOutLoc.Ml_ope_cd;
                _invHdrAdjMines.Ith_channel = _mstOutLoc.Ml_cate_2;
                _invHdrAdjMines.Ith_anal_1 = "";
                _invHdrAdjMines.Ith_anal_2 = "";
                _invHdrAdjMines.Ith_anal_3 = "";
                _invHdrAdjMines.Ith_anal_4 = "";
                _invHdrAdjMines.Ith_anal_5 = "";
                _invHdrAdjMines.Ith_anal_6 = 0;
                _invHdrAdjMines.Ith_anal_7 = 0;
                _invHdrAdjMines.Ith_anal_8 = DateTime.MinValue;
                _invHdrAdjMines.Ith_anal_9 = DateTime.MinValue;
                _invHdrAdjMines.Ith_anal_10 = false;
                _invHdrAdjMines.Ith_anal_11 = false;
                _invHdrAdjMines.Ith_anal_12 = false;
                _invHdrAdjMines.Ith_bus_entity = "";
                _invHdrAdjMines.Ith_cate_tp = "NOR";
                _invHdrAdjMines.Ith_com = Session["UserCompanyCode"].ToString();
                _invHdrAdjMines.Ith_com_docno = "";
                _invHdrAdjMines.Ith_cre_by = Session["UserID"].ToString();
                _invHdrAdjMines.Ith_cre_when = DateTime.Now;
                _invHdrAdjMines.Ith_del_add1 = "";
                _invHdrAdjMines.Ith_del_add2 = "";
                _invHdrAdjMines.Ith_del_code = "";
                _invHdrAdjMines.Ith_del_party = "";
                _invHdrAdjMines.Ith_del_town = "";
                _invHdrAdjMines.Ith_direct = false;
                _invHdrAdjMines.Ith_doc_date = Convert.ToDateTime(txtJobCloseDt.Text).Date;
                _invHdrAdjMines.Ith_doc_no = string.Empty;
                _invHdrAdjMines.Ith_doc_tp = "ADJ";
                _invHdrAdjMines.Ith_doc_year = _invHdrAdjMines.Ith_doc_date.Year;
                _invHdrAdjMines.Ith_git_close = true;
                _invHdrAdjMines.Ith_git_close_date = DateTime.MinValue;
                _invHdrAdjMines.Ith_git_close_doc = string.Empty;
                _invHdrAdjMines.Ith_isprinted = false;//????
                _invHdrAdjMines.Ith_is_manual = false;//????
                _invHdrAdjMines.Ith_job_no = "";
                _invHdrAdjMines.Ith_loading_point = string.Empty;
                _invHdrAdjMines.Ith_loading_user = string.Empty;
                _invHdrAdjMines.Ith_loc = Session["UserDefLoca"].ToString();
                _invHdrAdjMines.Ith_manual_ref = _selectedJobNo;
                _invHdrAdjMines.Ith_mod_by = Session["UserID"].ToString();
                _invHdrAdjMines.Ith_mod_when = DateTime.Now;
                _invHdrAdjMines.Ith_noofcopies = 0;
                _invHdrAdjMines.Ith_oth_loc = Session["UserDefLoca"].ToString();
                _invHdrAdjMines.Ith_remarks = "";// txtRemarks.Text;
                _invHdrAdjMines.Ith_session_id = Session["SessionID"].ToString();
                _invHdrAdjMines.Ith_stus = "A";
                _invHdrAdjMines.Ith_sub_tp = "NOR";
                _invHdrAdjMines.Ith_vehi_no = string.Empty;
                _invHdrAdjMines.Ith_pc = Session["UserDefProf"].ToString();
                _invHdrAdjMines.Ith_anal_10 = false;
                _invHdrAdjMines.Ith_anal_2 = "";
                _invHdrAdjMines.Ith_oth_docno = _selectedJobNo;
                _invHdrAdjMines.Ith_acc_no = "SWEB-KD-JOB-CLS";
                #endregion
                #endregion
                #region ADJ Plus for Job Serial
                #region Fill MasterAutoNumber
                MasterAutoNumber _autonoPlus = new MasterAutoNumber();
                _autonoPlus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoPlus.Aut_cate_tp = "LOC";
                _autonoPlus.Aut_direction = null;
                _autonoPlus.Aut_modify_dt = null;
                _autonoPlus.Aut_moduleid = "ADJ";
                _autonoPlus.Aut_number = 5;
                _autonoPlus.Aut_start_char = "ADJ";
                _autonoPlus.Aut_year = null;
                #endregion
                #region Fill InventoryHeader ADJ +
                InventoryHeader _invHdrAdjPlus = new InventoryHeader();
                _invHdrAdjPlus.Ith_channel = _invHdrAdjMines.Ith_channel;
                _invHdrAdjPlus.Ith_anal_1 = "";
                _invHdrAdjPlus.Ith_anal_2 = "";
                _invHdrAdjPlus.Ith_anal_3 = "";
                _invHdrAdjPlus.Ith_anal_4 = "";
                _invHdrAdjPlus.Ith_anal_5 = "";
                _invHdrAdjPlus.Ith_anal_6 = 0;
                _invHdrAdjPlus.Ith_anal_7 = 0;
                _invHdrAdjPlus.Ith_anal_8 = DateTime.MinValue;
                _invHdrAdjPlus.Ith_anal_9 = DateTime.MinValue;
                _invHdrAdjPlus.Ith_anal_10 = false;
                _invHdrAdjPlus.Ith_anal_11 = false;
                _invHdrAdjPlus.Ith_anal_12 = false;
                _invHdrAdjPlus.Ith_bus_entity = "";
                _invHdrAdjPlus.Ith_cate_tp = "NOR";
                _invHdrAdjPlus.Ith_com = _invHdrAdjMines.Ith_com;
                _invHdrAdjPlus.Ith_com_docno = "";
                _invHdrAdjPlus.Ith_cre_by = _invHdrAdjMines.Ith_cre_by;
                _invHdrAdjPlus.Ith_cre_when = DateTime.Now;
                _invHdrAdjPlus.Ith_del_add1 = "";
                _invHdrAdjPlus.Ith_del_add2 = "";
                _invHdrAdjPlus.Ith_del_code = "";
                _invHdrAdjPlus.Ith_del_party = "";
                _invHdrAdjPlus.Ith_del_town = "";
                _invHdrAdjPlus.Ith_direct = true;
                _invHdrAdjPlus.Ith_doc_date = _invHdrAdjMines.Ith_doc_date.Date;
                _invHdrAdjPlus.Ith_doc_no = string.Empty;
                _invHdrAdjPlus.Ith_doc_tp = "ADJ";
                _invHdrAdjPlus.Ith_doc_year = _invHdrAdjMines.Ith_doc_date.Year;
                _invHdrAdjPlus.Ith_git_close = true;
                _invHdrAdjPlus.Ith_git_close_date = DateTime.MinValue;
                _invHdrAdjPlus.Ith_git_close_doc = string.Empty;
                _invHdrAdjPlus.Ith_isprinted = false;//????
                _invHdrAdjPlus.Ith_is_manual = false;//????
                _invHdrAdjPlus.Ith_loading_point = string.Empty;
                _invHdrAdjPlus.Ith_loading_user = string.Empty;
                _invHdrAdjPlus.Ith_loc = _invHdrAdjMines.Ith_loc;
                _invHdrAdjPlus.Ith_manual_ref = _invHdrAdjMines.Ith_manual_ref;
                _invHdrAdjPlus.Ith_mod_by = _invHdrAdjMines.Ith_mod_by;
                _invHdrAdjPlus.Ith_mod_when = DateTime.Now;
                _invHdrAdjPlus.Ith_noofcopies = 0;
                _invHdrAdjPlus.Ith_oth_loc = _invHdrAdjMines.Ith_loc;
                _invHdrAdjPlus.Ith_remarks = "";// txtRemarks.Text;
                _invHdrAdjPlus.Ith_session_id = _invHdrAdjMines.Ith_session_id;
                _invHdrAdjPlus.Ith_stus = "A";
                _invHdrAdjPlus.Ith_sub_tp = "NOR";
                _invHdrAdjPlus.Ith_vehi_no = string.Empty;
                _invHdrAdjPlus.Ith_session_id = _invHdrAdjMines.Ith_session_id;

                _invHdrAdjPlus.Ith_anal_10 = false;
                _invHdrAdjPlus.Ith_anal_2 = "";
                _invHdrAdjPlus.Ith_pc = _invHdrAdjMines.Ith_pc;
                _invHdrAdjPlus.Ith_oth_docno = "";
                _invHdrAdjPlus.Ith_acc_no = "SWEB-KD-JOB-CLS";
                #endregion
                #endregion
                #region AOD Out for Job Serial
                #region Fill MasterAutoNumber AOD 
                MasterAutoNumber _autAodOut = new MasterAutoNumber();
                _autAodOut.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autAodOut.Aut_cate_tp = "LOC";
                _autAodOut.Aut_direction = null;
                _autAodOut.Aut_modify_dt = null;
                _autAodOut.Aut_moduleid = "AOD";
                _autAodOut.Aut_start_char = "AOD";
                _autAodOut.Aut_year = Convert.ToDateTime(DateTime.Now).Year;
                #endregion
                #region AODOUT Inventory Header Value Assign
                InventoryHeader _invHdrAodOut = new InventoryHeader();
                _invHdrAodOut.Ith_acc_no = string.Empty;
                _invHdrAodOut.Ith_anal_1 = string.Empty;
                _invHdrAodOut.Ith_anal_10 = true;
                _invHdrAodOut.Ith_anal_11 = false;
                _invHdrAodOut.Ith_anal_12 = false;
                _invHdrAodOut.Ith_anal_2 = string.Empty;
                _invHdrAodOut.Ith_anal_3 = string.Empty;
                _invHdrAodOut.Ith_anal_4 = string.Empty;
                _invHdrAodOut.Ith_anal_5 = string.Empty;
                _invHdrAodOut.Ith_anal_6 = 0;
                _invHdrAodOut.Ith_anal_7 = 0;
                _invHdrAodOut.Ith_anal_8 = Convert.ToDateTime(DateTime.Now).Date;
                _invHdrAodOut.Ith_anal_9 = Convert.ToDateTime(DateTime.Now).Date;
                _invHdrAodOut.Ith_bus_entity = string.Empty;
                _invHdrAodOut.Ith_cate_tp = "NOR";
                _invHdrAodOut.Ith_channel = string.Empty;
                _invHdrAodOut.Ith_com = Session["UserCompanyCode"].ToString();
                _invHdrAodOut.Ith_com_docno = string.Empty;
                _invHdrAodOut.Ith_cre_by = Session["UserID"].ToString();
                _invHdrAodOut.Ith_cre_when = DateTime.Now.Date;
                _invHdrAodOut.Ith_del_add1 = string.Empty;
                _invHdrAodOut.Ith_del_add2 = string.Empty;
                _invHdrAodOut.Ith_del_code = string.Empty;
                _invHdrAodOut.Ith_del_party = string.Empty;
                _invHdrAodOut.Ith_del_town = string.Empty;
                _invHdrAodOut.Ith_direct = false;
                _invHdrAodOut.Ith_doc_date = Convert.ToDateTime(DateTime.Now);
                _invHdrAodOut.Ith_doc_no = string.Empty;
                _invHdrAodOut.Ith_doc_tp = "AOD";
                _invHdrAodOut.Ith_doc_year = Convert.ToDateTime(DateTime.Now).Date.Year;
                //_invHdrAodOut.Ith_entry_no = _requestno;
                _invHdrAodOut.Ith_entry_tp = string.Empty;
                _invHdrAodOut.Ith_git_close = false;
                _invHdrAodOut.Ith_git_close_date = Convert.ToDateTime(DateTime.Now).Date;
                _invHdrAodOut.Ith_git_close_doc = string.Empty;
                _invHdrAodOut.Ith_is_manual = true;
                _invHdrAodOut.Ith_isprinted = false;
                _invHdrAodOut.Ith_job_no = _selectedJobNo;
                _invHdrAodOut.Ith_loading_point = string.Empty;
                _invHdrAodOut.Ith_loading_user = string.Empty;
                _invHdrAodOut.Ith_loc = Session["UserDefLoca"].ToString();
                _invHdrAodOut.Ith_manual_ref = _selectedJobNo;
                _invHdrAodOut.Ith_mod_by = Session["UserID"].ToString();
                _invHdrAodOut.Ith_mod_when = DateTime.Now.Date;
                _invHdrAodOut.Ith_noofcopies = 0;
                _invHdrAodOut.Ith_oth_loc = txtDispatchRequried.Text.Trim();
                _invHdrAodOut.Ith_oth_docno = string.Empty;
                //_invHdrAodOut.Ith_remarks = txtRemarks.Text;
                _invHdrAodOut.Ith_sbu = string.Empty;
                _invHdrAodOut.Ith_session_id = Session["SessionID"].ToString();
                _invHdrAodOut.Ith_stus = "A";
                _invHdrAodOut.Ith_sub_tp = string.Empty;
                _invHdrAodOut.Ith_vehi_no = string.Empty;
                _invHdrAodOut.Ith_oth_com = Session["UserCompanyCode"].ToString();
                //_invHdrAodOut.Ith_anal_1 =  "0";
                //_invHdrAodOut.Ith_anal_2 = chkManualRef.Checked ? ddlManType.Text : string.Empty;
                _invHdrAodOut.Ith_anal_2 = string.Empty;
                //_invHdrAodOut.Ith_sub_tp = ddlType.SelectedValue.ToString();
                _invHdrAodOut.Ith_sub_tp = "NOR";
                _invHdrAodOut.Ith_session_id = Session["SessionID"].ToString();
                _invHdrAodOut.Ith_pc = Session["UserDefProf"].ToString();
                _invHdrAodOut.Ith_isjobbase = true;
                _invHdrAodOut.Ith_job_no = _selectedJobNo;
                _invHdrAodOut.Ith_cate_tp = "SERVICE";
                _invHdrAodOut.Ith_sub_tp = "NOR";
                _invHdrAodOut.Ith_sub_docno = _selectedJobNo;
                #endregion
                #endregion
                #region Adj - for REQ data
                #region Fill MasterAutoNumber
                MasterAutoNumber _autonoMinusReq = new MasterAutoNumber();
                _autonoMinusReq.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoMinusReq.Aut_cate_tp = "LOC";
                _autonoMinusReq.Aut_direction = null;
                _autonoMinusReq.Aut_modify_dt = null;
                _autonoMinusReq.Aut_moduleid = "ADJ";
                _autonoMinusReq.Aut_number = 5;
                _autonoMinusReq.Aut_start_char = "ADJ";
                _autonoMinusReq.Aut_year = null;
                #endregion
                #region Fill InventoryHeader ADJ -
                InventoryHeader _invHdrAdjMinesReq = new InventoryHeader();
                _invHdrAdjMinesReq.Ith_sbu = _mstOutLoc.Ml_ope_cd;
                _invHdrAdjMinesReq.Ith_channel = _mstOutLoc.Ml_cate_2;
                _invHdrAdjMinesReq.Ith_anal_1 = "";
                _invHdrAdjMinesReq.Ith_anal_2 = "";
                _invHdrAdjMinesReq.Ith_anal_3 = "";
                _invHdrAdjMinesReq.Ith_anal_4 = "";
                _invHdrAdjMinesReq.Ith_anal_5 = "";
                _invHdrAdjMinesReq.Ith_anal_6 = 0;
                _invHdrAdjMinesReq.Ith_anal_7 = 0;
                _invHdrAdjMinesReq.Ith_anal_8 = DateTime.MinValue;
                _invHdrAdjMinesReq.Ith_anal_9 = DateTime.MinValue;
                _invHdrAdjMinesReq.Ith_anal_10 = false;
                _invHdrAdjMinesReq.Ith_anal_11 = false;
                _invHdrAdjMinesReq.Ith_anal_12 = false;
                _invHdrAdjMinesReq.Ith_bus_entity = "";
                _invHdrAdjMinesReq.Ith_cate_tp = "NOR";
                _invHdrAdjMinesReq.Ith_com = Session["UserCompanyCode"].ToString();
                _invHdrAdjMinesReq.Ith_com_docno = "";
                _invHdrAdjMinesReq.Ith_cre_by = Session["UserID"].ToString();
                _invHdrAdjMinesReq.Ith_cre_when = DateTime.Now;
                _invHdrAdjMinesReq.Ith_del_add1 = "";
                _invHdrAdjMinesReq.Ith_del_add2 = "";
                _invHdrAdjMinesReq.Ith_del_code = "";
                _invHdrAdjMinesReq.Ith_del_party = "";
                _invHdrAdjMinesReq.Ith_del_town = "";
                _invHdrAdjMinesReq.Ith_direct = false;
                _invHdrAdjMinesReq.Ith_doc_date = Convert.ToDateTime(txtJobCloseDt.Text).Date;
                _invHdrAdjMinesReq.Ith_doc_no = string.Empty;
                _invHdrAdjMinesReq.Ith_doc_tp = "ADJ";
                _invHdrAdjMinesReq.Ith_doc_year = _invHdrAdjMinesReq.Ith_doc_date.Year;
                _invHdrAdjMinesReq.Ith_git_close = true;
                _invHdrAdjMinesReq.Ith_git_close_date = DateTime.MinValue;
                _invHdrAdjMinesReq.Ith_git_close_doc = string.Empty;
                _invHdrAdjMinesReq.Ith_isprinted = false;//????
                _invHdrAdjMinesReq.Ith_is_manual = false;//????
                _invHdrAdjMinesReq.Ith_job_no = _selectedJobNo;
                _invHdrAdjMinesReq.Ith_loading_point = string.Empty;
                _invHdrAdjMinesReq.Ith_loading_user = string.Empty;
                _invHdrAdjMinesReq.Ith_loc = Session["UserDefLoca"].ToString();
                _invHdrAdjMinesReq.Ith_manual_ref = "";
                _invHdrAdjMinesReq.Ith_mod_by = Session["UserID"].ToString();
                _invHdrAdjMinesReq.Ith_mod_when = DateTime.Now;
                _invHdrAdjMinesReq.Ith_noofcopies = 0;
                _invHdrAdjMinesReq.Ith_oth_loc = Session["UserDefLoca"].ToString();
                _invHdrAdjMinesReq.Ith_remarks = "";// txtRemarks.Text;
                _invHdrAdjMinesReq.Ith_session_id = Session["SessionID"].ToString();
                _invHdrAdjMinesReq.Ith_stus = "A";
                _invHdrAdjMinesReq.Ith_sub_tp = "NOR";
                _invHdrAdjMinesReq.Ith_vehi_no = string.Empty;
                _invHdrAdjMinesReq.Ith_pc = Session["UserDefProf"].ToString();
                _invHdrAdjMinesReq.Ith_anal_10 = false;
                _invHdrAdjMinesReq.Ith_anal_2 = "";
                _invHdrAdjMinesReq.Ith_oth_docno = "";
                _invHdrAdjMinesReq.Ith_acc_no = "SW-KD-JOB-CLS-MRN";
                #endregion
                Service_job_Det _tmpJobDet = CHNLSVC.CustService.GetJobDetails(_selectedJobNo, _selectedJobLine, Session["UserCompanyCode"].ToString()).FirstOrDefault();
                //List<InventorySerialN> _inrSer = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                //{
                //    Ins_itm_cd = _tmpJobDet.Jbd_itm_cd,
                //    Ins_ser_1 = _tmpJobDet.Jbd_ser1,
                //    Ins_available = 1
                //});

                ReptPickSerials reptPickSerial_ =  CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(),
                    "", _tmpJobDet.Jbd_itm_cd, Convert.ToInt32(_tmpJobDet.Jbd_ser_id));
                reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                reptPickSerial_.Tus_job_no = _selectedJobNo;
                reptPickSerial_.Tus_job_line = _selectedJobLine;
                _rptSerialList.Add(reptPickSerial_);
                #endregion
                _invHdrAdjMines.Ith_gen_frm ="SCMWEB"; 
                _invHdrAdjPlus.Ith_gen_frm ="SCMWEB"; 
                _invHdrAodOut.Ith_gen_frm ="SCMWEB";
                Int32 _res = CHNLSVC.Inventory.ServiceWorkingProcessComplete(_autonoMinus, _autonoPlus, _autAodOut, _invHdrAdjMines, _invHdrAdjPlus, _invHdrAodOut,
                _rptSerialList, _rptSubSerialList,
                 _autonoMinusReq, _invHdrAdjMinesReq,_selectedJobNo,_selectedJobLine,
                    out  _docAdjMines, out  _docAdjPlus, out  _docAodOutNo, out  _docAdjMinesREQ, out  _error, out  _errList);
                if (_res > 0)
                {
                    DispMsg("Document Successfully : "+_docAodOutNo,"S");
                    lbtnCloseJobClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
    }
}