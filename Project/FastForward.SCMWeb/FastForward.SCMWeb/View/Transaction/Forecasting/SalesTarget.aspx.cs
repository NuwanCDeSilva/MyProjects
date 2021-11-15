using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Forecasting
{
    public partial class SalesTarget : BasePage
    {
        #region PropertyData
        string _calSerTp
        {
            get { if (Session["_calSerTp"] != null) { return (string)Session["_calSerTp"]; } else { return ""; } }
            set { Session["_calSerTp"] = value; }
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
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        bool _showDateSearch
        {
            get { if (Session["_showDateSearch"] != null) { return (bool)Session["_showDateSearch"]; } else { return false; } }
            set { Session["_showDateSearch"] = value; }
        }
        bool _calPopShow
        {
            get { if (Session["_calPopShow"] != null) { return (bool)Session["_calPopShow"]; } else { return false; } }
            set { Session["_calPopShow"] = value; }
        }
        bool _combinePopShow
        {
            get { if (Session["_combinePopShow"] != null) { return (bool)Session["_combinePopShow"]; } else { return false; } }
            set { Session["_combinePopShow"] = value; }
        }
        bool _crePerPopShow
        {
            get { if (Session["_crePerPopShow"] != null) { return (bool)Session["_crePerPopShow"]; } else { return false; } }
            set { Session["_crePerPopShow"] = value; }
        }
        bool _showErrPop
        {
            get { if (Session["_showErrPop"] != null) { return (bool)Session["_showErrPop"]; } else { return false; } }
            set { Session["_showErrPop"] = value; }
        }
        bool _showExcelPop
        {
            get { if (Session["_showExcelPop"] != null) { return (bool)Session["_showExcelPop"]; } else { return false; } }
            set { Session["_showExcelPop"] = value; }
        }
        bool _showProcPop
        {
            get { if (Session["_showProcPop"] != null) { return (bool)Session["_showProcPop"]; } else { return false; } }
            set { Session["_showProcPop"] = value; }
        }
        bool _showExcSave
        {
            get { if (Session["_showExcSave"] != null) { return (bool)Session["_showExcSave"]; } else { return false; } }
            set { Session["_showExcSave"] = value; }
        }
        string _toolTip
        {
            get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
            set { Session["_toolTip"] = value; }
        }
        string _filPath
        {
            get { if (Session["_filPath"] != null) { return (string)Session["_filPath"]; } else { return ""; } }
            set { Session["_filPath"] = value; }
        }
        bool _ava
        {
            get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
            set { Session["_ava"] = value; }
        }
        List<string> _defByList
        {
            get { if (Session["_defByList"] != null) { return (List<string>)Session["_defByList"]; } else { return new List<string>(); } }
            set { Session["_defByList"] = value; }
        }
        List<string> _defOnList
        {
            get { if (Session["_defOnList"] != null) { return (List<string>)Session["_defOnList"]; } else { return new List<string>(); } }
            set { Session["_defOnList"] = value; }
        }
        List<SalesForecastingCalendarCom> _salForCasCompList
        {
            get { if (Session["_salForCasCompList"] != null) { return (List<SalesForecastingCalendarCom>)Session["_salForCasCompList"]; } else { return new List<SalesForecastingCalendarCom>(); } }
            set { Session["_salForCasCompList"] = value; }
        }
        List<SalesForecastingDetail> _salForDetList
        {
            get { if (Session["_salForDetList"] != null) { return (List<SalesForecastingDetail>)Session["_salForDetList"]; } else { return new List<SalesForecastingDetail>(); } }
            set { Session["_salForDetList"] = value; }
        }
        string _para = "";
        SalesForecastingCalendar _salForCal = new SalesForecastingCalendar();
        MasterCompany _mstComp = new MasterCompany();
        SalesForecastingCalendarCom _salForCasComp = new SalesForecastingCalendarCom();
        SalesForecastingMasterPeriod _salForMstPer = new SalesForecastingMasterPeriod();
        SalesForecastingPeriod _salForCasPer = new SalesForecastingPeriod();
        List<SalesForecastingMasterPeriod> _salForMstPerList = new List<SalesForecastingMasterPeriod>();
        List<SalesForecastingDetail> _salForDetails = new List<SalesForecastingDetail>();
        SalesForecastingHeader _salForHdr = new SalesForecastingHeader();
        SalesForecastingDetail _salForDet = new SalesForecastingDetail();
        MasterItem _mstItem = new MasterItem();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    ClearPage();
                }
                catch (Exception ex)
                {
                    DispMsg("Error Occurred :" + ex.Message, "E");
                }
            }
            else
            {
                if (_serPopShow)
                {
                    PopupSearch.Show();
                }
                else
                {
                    _serPopShow = false;
                    PopupSearch.Hide();
                }
                if (_calPopShow)
                {
                    PopupCreCal.Show();
                }
                else
                {
                    _calPopShow = false;
                    PopupCreCal.Hide();
                }
                if (_combinePopShow)
                {
                    PopupComCompany.Show();
                }
                else
                {
                    _combinePopShow = false;
                    PopupComCompany.Hide();
                }
                if (_crePerPopShow)
                {
                    PopupCrePer.Show();
                }
                else
                {
                    _crePerPopShow = false;
                    PopupCrePer.Hide();
                }
                if (_showExcelPop)
                {
                    popupExcel.Show();
                }
                else
                {
                    _showExcelPop = false;
                    popupExcel.Hide();
                }
                if (_showErrPop)
                {
                    popupErro.Show();
                }
                else
                {
                    _showErrPop = false;
                    popupErro.Hide();
                }
                if (_showExcSave)
                {
                    popupExcelSave.Show();
                }
                else
                {
                    _showExcSave = false;
                    popupExcelSave.Hide();
                }
                if (_showDateSearch)
                {
                    PopupSearch2.Show();
                }
                else
                {
                    _showDateSearch = false;
                    PopupSearch2.Hide();
                }
            }
        }

        private void ClearPage()
        {
            try
            {
                //ucLoactionSearch.PnlHeading.Visible = false;
                _serData = new DataTable();
                _serType = "";
                _calSerTp = "";
                _showDateSearch = false;
                _ava = false;
                _toolTip = string.Empty;
                _serPopShow = false;
                _calPopShow = false;
                _crePerPopShow = false;
                _combinePopShow = false;
                lblDefCdDes.Text = "";
                txtCompany.Text = Session["UserCompanyCode"].ToString();
                txtCompany_TextChanged(null, null);
                DateTime dt = DateTime.Now;
                txtCPFrom.Text = dt.ToString("dd/MMM/yyyy");
                txtCPTo.Text = dt.ToString("dd/MMM/yyyy");
                txtCPToDt.Text = dt.ToString("dd/MMM/yyyy");
                txtCPFromDt.Text = dt.AddMonths(-1).ToString("dd/MMM/yyyy");
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DateTime.Today.Month.ToString()));
                ddlMonthEnd.SelectedIndex = ddlMonthEnd.Items.IndexOf(ddlMonthEnd.Items.FindByValue(DateTime.Today.Month.ToString()));
                txtCCCompany.Text = string.Empty;
                txtCCCompany.ToolTip = string.Empty;
                ClearCalCreate();
                ClearCombineComp();
                BindPeriodTp();
                dgvTarget.DataSource = new int[] { };
                dgvTarget.DataBind();
                ddlCatDefOn.SelectedIndex = 0;
                ddlCatDefOn_SelectedIndexChanged(null, null);
                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _calPopShow = false;
                _combinePopShow = false;
                _crePerPopShow = false;
                _showExcelPop = false;
                _showProcPop = false;
                _showErrPop = false;
                _showExcSave = false;
                _toolTip = "";
                _filPath = "";
                _ava = false;
                _salForCasCompList = new List<SalesForecastingCalendarCom>();
                _salForDetList = new List<SalesForecastingDetail>();
                txtItemCode.Text = ""; txtModel.Text = ""; txtBrand.Text = ""; txtCat1.Text = ""; txtCat2.Text = ""; txtCat3.Text = "";
                txtItemCode.ToolTip = ""; txtModel.ToolTip = ""; txtBrand.ToolTip = ""; txtCat1.ToolTip = ""; txtCat2.ToolTip = ""; txtCat3.ToolTip = "";
                txtQty.Text = ""; txtVal.Text = ""; txtGp.Text = "";
                txtDefCode.Text = "";
                _defByList = new List<string>(); _defOnList = new List<string>();
                GetMasterData();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void GetMasterData()
        {
            List<string> tmpDefBy = new List<string>();
            List<string> tmpDefOn = new List<string>();
            tmpDefBy.Add("COM"); tmpDefBy.Add("CHNL"); tmpDefBy.Add("SCHNL"); tmpDefBy.Add("AREA"); tmpDefBy.Add("REGION"); tmpDefBy.Add("ZONE"); tmpDefBy.Add("PC");
            tmpDefOn.Add("CAT1"); tmpDefOn.Add("CAT2"); tmpDefOn.Add("CAT3"); tmpDefOn.Add("MODEL"); tmpDefOn.Add("BRAND"); tmpDefOn.Add("ITEM"); tmpDefOn.Add("CAT1_MODEL"); tmpDefOn.Add("MODEL_BRAND");
            _defByList = tmpDefBy;
            _defOnList = tmpDefOn;
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
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForCale:
                    {
                        if (_calSerTp == "SalForCaleCCC")
                        {
                            paramsText.Append(txtCCCompany.Text.Trim().ToUpper() + seperator);
                            break;
                        }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForPeriod:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForPeriodParent1:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + (string.IsNullOrEmpty(txtCPParCode.Text) ? "" : txtCPParCode.Text.ToUpper().Trim()) + seperator +
                           (ddlCPTp.SelectedIndex > 0 ? ddlCPTp.SelectedValue : "0") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForPeriodParent2:
                    {
                        Int32 sfp_pd_tp = 0;
                        if (ddlCPTp.SelectedIndex > 0)
                        {
                            _salForMstPer = new SalesForecastingMasterPeriod() { Mfp_seq = Convert.ToInt32(ddlCPTp.SelectedValue), Mfp_desc = ddlCPTp.SelectedItem.Text };
                            var v = CHNLSVC.Sales.Get_MST_FOR_PD_TP(_salForMstPer).FirstOrDefault();
                            if (v != null)
                            {
                                sfp_pd_tp = v.Mfp_parent_cd;
                            }
                        }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + (string.IsNullOrEmpty(txtCPParCode.Text) ? "" : txtCPParCode.Text.ToUpper().Trim()) + seperator +
                           sfp_pd_tp + seperator + (string.IsNullOrEmpty(txtCPCalender.Text) ? "" : txtCPCalender.Text.ToUpper().Trim()) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub2.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + CommonUIDefiniton.SearchUserControlType.CAT_Sub3.ToString() + seperator + "" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                //    {
                //        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + txtCat4.Text + CommonUIDefiniton.SearchUserControlType.CAT_Sub4.ToString() + seperator + "" + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForPeriodCal:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtCalenderCode.Text.ToUpper() + seperator + "" + seperator);
                        break;
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
        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCompany.ToolTip = string.Empty;
                txtCalenderCode.Text = string.Empty;
                txtCalenderCode_TextChanged(null, null);
                if (!string.IsNullOrEmpty(txtCompany.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtCompany.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCompany.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCompany.ToolTip = _ava ? _toolTip : "";
                    lblCompany.Text = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtCompany.Text = string.Empty;
                        txtCompany.Focus();
                        DispMsg("Please enter valid company !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
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

        public void BindDdlSerByKey2(DataTable _dataSource)
        {
            this.ddlSerByKey2.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey2.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey2.SelectedIndex = 0;
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

        private void LoadSearchPopup2(string serType, string _colName, string _ordTp)
        {
            OrderSearchGridData(_colName, _ordTp);
            try
            {
                dgvResult2.DataSource = new int[] { };
                if (_serData != null)
                {
                    BindDdlSerByKey2(_serData);
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult2.DataSource = _serData;
                    }
                }
                dgvResult2.DataBind();
                txtSerByKey2.Text = "";
                txtSerByKey2.Focus();
                _serType = serType;
                PopupSearch2.Show();
                _showDateSearch = true;
                // _serPopShow = true;
                if (dgvResult2.PageIndex > 0)
                { dgvResult2.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
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
                if (_serType == "Company")
                {
                    txtCompany.Text = code;
                    txtCompany_TextChanged(null, null);
                }
                else if (_serType == "CCCompany")
                {
                    txtCCCompany.Text = code;
                    txtCCCompany_TextChanged(null, null);
                }
                else if (_serType == "SalForCale")
                {
                    txtCalenderCode.Text = code;
                    txtCalenderCode_TextChanged(null, null);
                }
                else if (_serType == "SalForCaleCCB")
                {
                    txtCCBCalender.Text = code;
                    txtCCBCalender_TextChanged(null, null);
                }
                else if (_serType == "SalForCaleCCC")
                {
                    txtCCCalenderCode.Text = code;
                    txtCCCalenderCode_TextChanged(null, null);
                }
                else if (_serType == "CCBCompany")
                {
                    txtCCBCompany.Text = code;
                    txtCCBCompany_TextChanged(null, null);
                }
                else if (_serType == "CPCompany")
                {
                    txtCPCom.Text = code;
                    txtCPCom_TextChanged(null, null);
                }
                else if (_serType == "CPSalForCale")
                {
                    txtCPCalender.Text = code;
                    txtCPCalender_TextChanged(null, null);
                }
                else if (_serType == "CPSalForPeriod")
                {
                    txtCPCode.Text = code;
                    txtCPCode_TextChanged(null, null);
                }
                //else if (_serType == "CPSalForPeriodParent")
                //{
                //    txtCPParCode.Text = code;
                //    txtCPParCode_TextChanged(null, null);
                //}
                else if (_serType == "SalForPeriodCal")
                {
                    txtPeriod.Text = code;
                    txtPeriod_TextChanged(null, null);
                }
                else if (_serType == "Pc_HIRC_Company" || _serType == "Pc_HIRC_Channel" || _serType == "Pc_HIRC_SubChannel"
                    || _serType == "Pc_HIRC_Area" || _serType == "Pc_HIRC_Region" || _serType == "Pc_HIRC_Zone" || _serType == "AllProfitCenters")
                {
                    txtDefCode.Text = code;
                    txtDefCode_TextChanged(null, null);
                }
                else if (_serType == "Item")
                {
                    txtItemCode.Text = code;
                    txtItemCode_TextChanged(null, null);
                }
                else if (_serType == "Brand")
                {
                    txtBrand.Text = code;
                    txtBrand_TextChanged(null, null);
                }
                else if (_serType == "Model")
                {
                    txtModel.Text = code;
                    txtModel_TextChanged(null, null);
                }
                else if (_serType == "CAT_Main")
                {
                    txtCat1.Text = code;
                    txtCat1_TextChanged(null, null);
                }
                else if (_serType == "CAT_Sub1")
                {
                    txtCat2.Text = code;
                    txtCat2_TextChanged(null, null);
                }
                else if (_serType == "CAT_Sub2")
                {
                    txtCat3.Text = code;
                    txtCat3_TextChanged(null, null);
                }
                else if (_serType == "EpfNo")
                {
                    txtexc.Text = code;

                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCompany_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                LoadSearchPopup("Company", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "Company")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CPCompany")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CCCompany")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "SalForCale")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);

                    // _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                    _serData = GetSerchDataTable(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "SalForCaleCCB")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    // _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                    _serData = GetSerchDataTable(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "SalForCaleCCC")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    //_serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                    _serData = GetSerchDataTable(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CPSalForCale")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    // _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                    _serData = GetSerchDataTable(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CPSalForPeriod")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriod);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim(), txtCPFromDt.Text, txtCPToDt.Text);
                }
                //else if (_serType == "CPSalForPeriodParent")
                //{
                //    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodParent1);
                //    _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text, txtCPFromDt.Text, txtCPToDt.Text);
                //}
                else if (_serType == "SalForPeriodCal")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodCal);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingPeriodByCal(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Pc_HIRC_Company")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Pc_HIRC_Channel")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Pc_HIRC_SubChannel")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Pc_HIRC_Area")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Pc_HIRC_Region")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Pc_HIRC_Zone")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "AllProfitCenters")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Item")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Brand")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    _serData = CHNLSVC.CommonSearch.GetItemBrands(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Model")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    _serData = CHNLSVC.CommonSearch.GetAllModels(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Main")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Sub1")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Sub2")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "EpfNo")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                    _serData = CHNLSVC.CommonSearch.Get_Sales_Ex(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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

        protected void lbtnSearch2_Click(object sender, EventArgs e)
        {
            if (_serType == "CPSalForPeriodParent")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodParent1);
                _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, ddlSerByKey2.SelectedValue, txtSerByKey2.Text, txtCPFromDt.Text, txtCPToDt.Text);
            }
            else if (_serType == "CPSalForPeriod")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriod);
                _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, ddlSerByKey2.SelectedValue, txtSerByKey2.Text.Trim(), DateTime.MinValue.ToShortDateString(), DateTime.MaxValue.ToShortDateString());
            }
            dgvResult2.DataSource = new int[] { };
            if (dgvResult2.PageIndex > 0)
            { dgvResult2.SetPageIndex(0); }
            if (_serData.Rows.Count > 0)
            {
                dgvResult2.DataSource = _serData;
            }
            //txtSerByKey.Text = "";
            txtSerByKey2.Focus();
            dgvResult2.DataBind();
            PopupSearch2.Show();
            _showDateSearch = true;
            // _serPopShow = true;
        }

        protected void lbtnCreateCalender_Click(object sender, EventArgs e)
        {
            try
            {
                txtCCCompany.Text = Session["UserCompanyCode"].ToString();
                txtCCCompany_TextChanged(null, null);
                PopupCreCal.Show();
                _calPopShow = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void btnCreCalClose_Click(object sender, EventArgs e)
        {
            PopupCreCal.Hide();
            _calPopShow = false;
            lbtnCCClear_Click(sender, e);
        }

        protected void lbtnSeCCCompany_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                LoadSearchPopup("CCCompany", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCCCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCCCompany.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCCCompany.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtCCCompany.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCCCompany.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCCCompany.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        txtYearFrom.Focus();
                    }
                    else
                    {
                        txtCCCompany.Text = string.Empty;
                        txtCCCompany.Focus();
                        DispMsg("Please enter valid company !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnCCClear_Click(object sender, EventArgs e)
        {
            txtCCCompany.Text = Session["UserCompanyCode"].ToString();
            txtCCCompany_TextChanged(null, null);
            ClearCalCreate();
            txtCCCalenderCode.Text = "";
            txtCCCompany.Enabled = true;
            txtYearFrom.Enabled = true;
            txtYearTo.Enabled = true;
            ddlMonth.Enabled = true;
            ddlMonthEnd.Enabled = true;
        }

        private void ClearCalCreate()
        {
            try
            {

                txtYearFrom.Text = string.Empty;
                txtYearTo.Text = string.Empty;
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DateTime.Today.Month.ToString()));
                ddlMonthEnd.SelectedIndex = ddlMonthEnd.Items.IndexOf(ddlMonthEnd.Items.FindByValue(DateTime.Today.Month.ToString()));
                txtCCDesc.Text = string.Empty;
                chkCCActive.Checked = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void ClearCombineComp()
        {
            try
            {
                // txtCCBCalender.Text = string.Empty;
                txtCCBCalender.ToolTip = string.Empty;
                lblCCBDesc.Text = string.Empty;
                lblCCBStart.Text = string.Empty;
                lblCCBEnd.Text = string.Empty;
                lblCCBStartMonth.Text = string.Empty;
                lblCCBEndMonth.Text = string.Empty;
                txtCCBCompany.Text = string.Empty;
                txtCCBCompDesc.Text = string.Empty;
                _salForCasCompList = new List<SalesForecastingCalendarCom>();
                dgvCCBCompany.DataSource = new int[] { };
                dgvCCBCompany.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private bool IsInteger(string _val)
        {
            bool b = false;
            Int32 _tmpVal = 0;
            b = Int32.TryParse(_val, out _tmpVal) ? true : false;
            return b;
        }
        protected void lbtnCCSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(txtCCCompany.Text))
                {
                    txtCCCompany.Focus(); DispMsg("Please select the company !"); return;
                }
                if (string.IsNullOrEmpty(txtYearFrom.Text))
                {
                    txtYearFrom.Focus(); DispMsg("Please enter the year from !"); return;
                }
                if (string.IsNullOrEmpty(txtYearTo.Text))
                {
                    txtYearFrom.Focus(); DispMsg("Please enter the year to !"); return;
                }
                if (!IsInteger(txtYearFrom.Text))
                {
                    txtYearFrom.Focus(); DispMsg("Please enter valid year from !"); return;
                }
                if (!IsInteger(txtYearTo.Text))
                {
                    txtYearTo.Focus(); DispMsg("Please enter valid year to !"); return;
                }
                if (string.IsNullOrEmpty(txtCCDesc.Text))
                {
                    txtCCDesc.Focus(); DispMsg("Please Enter Description !"); return;
                }
                Int32 frYear = Convert.ToInt32(txtYearFrom.Text);
                Int32 toYear = Convert.ToInt32(txtYearTo.Text);
                if (frYear > toYear)
                {
                    txtYearTo.Focus(); DispMsg("From Year cannot exceed the To Year !"); return;
                }
                #endregion
                Int32 _res = -1;
                string _docNo = "";
                string _error = "";
                MasterAutoNumber _mstAutoSalForCal = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _mstAutoSalForCal.Aut_cate_cd = "";
                _mstAutoSalForCal.Aut_cate_tp = "";
                _mstAutoSalForCal.Aut_direction = null;
                _mstAutoSalForCal.Aut_modify_dt = null;
                _mstAutoSalForCal.Aut_moduleid = "SFC";
                _mstAutoSalForCal.Aut_number = 0;
                _mstAutoSalForCal.Aut_start_char = "SFC";
                _mstAutoSalForCal.Aut_year = null;
                #endregion

                #region FillSalForCal
                _salForCal = new SalesForecastingCalendar();
                _salForCal.Sfc_cd = string.IsNullOrEmpty(txtCCCalenderCode.Text) ? "" : txtCCCalenderCode.Text.ToUpper().Trim();
                _salForCal.Sfc_com = txtCCCompany.Text.ToUpper().Trim();
                _salForCal.Sfc_desc = txtCCDesc.Text;
                _salForCal.Sfc_ye_frm = Convert.ToInt32(txtYearFrom.Text);
                _salForCal.Sfc_ye_to = Convert.ToInt32(txtYearTo.Text);
                _salForCal.Sfc_st_mnt = Convert.ToInt32(ddlMonth.SelectedValue);
                _salForCal.Sfc_ye_to = Convert.ToInt32(txtYearTo.Text);
                _salForCal.Sfc_act = chkCCActive.Checked ? 1 : 0;
                _salForCal.Sfc_cre_by = Session["UserID"].ToString();
                _salForCal.Sfc_cre_dt = DateTime.Now;
                _salForCal.Sfc_mod_by = Session["UserID"].ToString();
                _salForCal.Sfc_mod_dt = DateTime.Now;
                _salForCal.sfc_ed_mnt = Convert.ToInt32(ddlMonthEnd.SelectedValue);
                #endregion

                _res = CHNLSVC.Sales.SaveSalesForecastingCalendar(_mstAutoSalForCal, _salForCal, out _docNo, out _error);
                if (_res > 0)
                {
                    DispMsg("Successfully saved. Calendar code - " + _docNo, "S");
                    txtCCCompany.Text = Session["UserCompanyCode"].ToString();
                    txtCCCompany_TextChanged(null, null);
                    ClearCalCreate();
                    txtCCCalenderCode.Text = "";
                    txtCCCalenderCode.ToolTip = "";
                    txtCCCompany.Enabled = true;
                    txtYearFrom.Enabled = true;
                    txtYearTo.Enabled = true;
                    ddlMonth.Enabled = true;
                    ddlMonthEnd.Enabled = true;
                }
                else
                {
                    DispMsg(_error, "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnCCBSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                Int32 _res = -1;
                if (_salForCasCompList.Count < 1)
                {
                    DispMsg("Please add company details !"); return;
                }
                _res = CHNLSVC.Sales.SaveSalesForecastingCompany(_salForCasCompList, out _error);
                if (_res > 0)
                {
                    DispMsg("Successfully saved !", "S");
                    ClearCombineComp();
                    txtCCBCalender.Text = "";
                    txtCCBCalender.Text = "";
                    txtCCBCompany.Text = "";
                    txtCCBCompany.ToolTip = "";
                    txtCCBCompDesc.Text = "";
                }
                else
                {
                    DispMsg(_error, "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnCCBClear_Click(object sender, EventArgs e)
        {
            ClearCombineComp();
            txtCCBCalender.Text = "";
            txtCCBCalender.ToolTip = "";
        }

        protected void lbtnCCBClose_Click(object sender, EventArgs e)
        {
            txtCCBCalender.Text = string.Empty;
            ClearCombineComp();
            _combinePopShow = false;
            PopupComCompany.Hide();
        }

        protected void lbtnSeCCBCal_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                //  _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, null, null);
                _serData = GetSerchDataTable(_para, null, null);
                LoadSearchPopup("SalForCaleCCB", "CODE", "DESC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnComCompany_Click(object sender, EventArgs e)
        {
            try
            {
                _combinePopShow = true;
                PopupComCompany.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    txtCompany.Focus(); DispMsg("Please select the company !"); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                _serData = GetSerchDataTable(_para, null, null);
                LoadSearchPopup("SalForCale", "CODE", "ASC", false);
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private DataTable GetSerchDataTable(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtNew = new DataTable();
            _dtNew.Columns.Add("Code");
            _dtNew.Columns.Add("Description");
            _dtNew.Columns.Add("From Date");
            _dtNew.Columns.Add("To Date");
            DataTable _dt = CHNLSVC.Sales.SearchSalesForecastingCalendarWithDate(_initialSearchParams, _searchCatergory, _searchText);
            Int32 _tmp = 0, _stYear = 0, _stMonth, _enYear = 0, _enMonth = 0;
            DateTime _frDate = new DateTime(), _toDate = new DateTime();
            if (_dt.Rows.Count > 0)
            {
                DataView dv = _dt.DefaultView;
                dv.Sort = "sfc_ye_frm desc,sfc_st_mnt asc";
                _dt = dv.ToTable();
            }
            foreach (DataRow dr in _dt.Rows)
            {
                DataRow _newRow = _dtNew.NewRow();
                _newRow["Code"] = dr["sfc_cd"].ToString();
                _newRow["Description"] = dr["sfc_desc"].ToString();
                _stYear = Int32.TryParse(dr["sfc_ye_frm"].ToString(), out _tmp) ? Convert.ToInt32(dr["sfc_ye_frm"].ToString()) : 1991;
                _stMonth = Int32.TryParse(dr["sfc_st_mnt"].ToString(), out _tmp) ? Convert.ToInt32(dr["sfc_st_mnt"].ToString()) : 01;
                _enYear = Int32.TryParse(dr["sfc_ye_to"].ToString(), out _tmp) ? Convert.ToInt32(dr["sfc_ye_to"].ToString()) : 1991;
                _enMonth = Int32.TryParse(dr["sfc_ed_mnt"].ToString(), out _tmp) ? Convert.ToInt32(dr["sfc_ed_mnt"].ToString()) : 01;
                _frDate = new DateTime(_stYear, _stMonth, 1);
                _toDate = (new DateTime(_enYear, _enMonth, 1)).AddMonths(1).AddDays(-1);
                _newRow["From Date"] = _frDate.ToString("dd/MMM/yyyy");
                _newRow["To Date"] = _toDate.ToString("dd/MMM/yyyy");
                _dtNew.Rows.Add(_newRow);
            }
            return _dtNew;
        }
        protected void txtCalenderCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCalFrom.Text = "";
                txtCalTo.Text = "";
                txtCalenderCode.ToolTip = string.Empty;
                txtPeriod.Text = "";
                txtPeriod_TextChanged(null, null);
                lblCalCode.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtCalenderCode.Text))
                {
                    if (string.IsNullOrEmpty(txtCompany.Text))
                    {
                        txtCalenderCode.Text = ""; txtCompany.Focus(); DispMsg("Please select the company !"); return;
                    }

                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, "CODE", txtCalenderCode.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCalenderCode.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCalenderCode.ToolTip = _toolTip;
                        lblCalCode.Text = _toolTip;
                        _salForCal = CHNLSVC.Sales.GetSarForCal(new SalesForecastingCalendar() { Sfc_cd = txtCalenderCode.Text }).FirstOrDefault();
                        if (_salForCal != null)
                        {
                            DateTime dtTmp = new DateTime(_salForCal.Sfc_ye_frm, _salForCal.Sfc_st_mnt, 1);
                            txtCalFrom.Text = dtTmp.Date.ToString("dd/MMM/yyyy");
                            dtTmp = new DateTime(_salForCal.Sfc_ye_to, _salForCal.sfc_ed_mnt, 1);
                            dtTmp = dtTmp.AddMonths(1).AddDays(-1);
                            txtCalTo.Text = dtTmp.Date.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        txtCalenderCode.Text = string.Empty;
                        txtCalenderCode.Focus();
                        DispMsg("Please enter valid calendar code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCCBCalender_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearCombineComp();
                if (!string.IsNullOrEmpty(txtCCBCalender.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, "CODE", txtCCBCalender.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCCBCalender.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCCBCalender.ToolTip = _toolTip;
                        lblCCBDesc.Text = _toolTip;
                        LoadCombineCompanyData(txtCCBCalender.Text.Trim().ToUpper());
                    }
                    else
                    {
                        txtCCBCalender.Text = string.Empty;
                        txtCCBCalender.Focus();
                        DispMsg("Please enter valid calendar code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void LoadCombineCompanyData(string _code)
        {
            //load calende data
            _salForCal = new SalesForecastingCalendar() { Sfc_cd = _code };
            var v = CHNLSVC.Sales.GetSarForCal(_salForCal).FirstOrDefault();
            if (v != null)
            {
                lblCCBStart.Text = v.Sfc_ye_frm.ToString();
                lblCCBEnd.Text = v.Sfc_ye_to.ToString();
                DateTime tmpDt = new DateTime(DateTime.Today.Year, v.Sfc_st_mnt, DateTime.Today.Day);
                lblCCBStartMonth.Text = tmpDt.Date.ToString("MMMM");
                tmpDt = new DateTime(DateTime.Today.Year, v.sfc_ed_mnt, DateTime.Today.Day);
                lblCCBEndMonth.Text = tmpDt.Date.ToString("MMMM");
            }
            _salForCasCompList = new List<SalesForecastingCalendarCom>();
            _salForCasComp = new SalesForecastingCalendarCom() { Sfm_cale_cd = txtCCBCalender.Text };
            //load combine data
            _salForCasCompList = CHNLSVC.Sales.GetSarForCom(_salForCasComp).ToList();
            if (_salForCasCompList != null)
            {
                foreach (var item in _salForCasCompList)
                {
                    _mstComp = CHNLSVC.General.GetCompByCode(item.Sfm_alw_com);
                    item.tmp_com_desc = _mstComp != null ? _mstComp.Mc_desc : "";
                }
            }
            BindCCbCompanyGrid();
        }

        protected void lbtnSeCCBCom_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                LoadSearchPopup("CCBCompany", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCCBCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCCBCompany.ToolTip = string.Empty;
                txtCCBCompDesc.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCCBCompany.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtCCBCompany.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCCBCompany.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCCBCompany.ToolTip = _ava ? _toolTip : "";
                    txtCCBCompDesc.Text = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtCCBCompany.Text = string.Empty;
                        txtCCBCompany.Focus();
                        DispMsg("Please enter valid company !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnCCBComAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCCBCalender.Text))
                {
                    txtCCBCalender.Focus(); DispMsg("Please select the calender !"); return;
                }
                if (string.IsNullOrEmpty(txtCCBCompany.Text))
                {
                    txtCCBCompany.Focus(); DispMsg("Please select the company !"); return;
                }
                var v = _salForCasCompList.Where(c => c.Sfm_alw_com == txtCCBCompany.Text.ToUpper().Trim() && c.Sfm_cale_cd == txtCCBCalender.Text.Trim().ToUpper()).FirstOrDefault();
                if (v != null)
                {
                    DispMsg("Selected company has been already inserted !"); return;
                }

                _salForCasComp = new SalesForecastingCalendarCom();
                _salForCasComp.Sfm_act = 1;
                _salForCasComp.Sfm_cale_cd = txtCCBCalender.Text.ToUpper().Trim();
                _salForCasComp.Sfm_alw_com = txtCCBCompany.Text.ToUpper().Trim();
                _salForCasComp.tmp_com_desc = txtCCBCompDesc.Text;
                _salForCasComp.Sfm_cre_by = Session["UserID"].ToString();
                _salForCasComp.Sfm_cre_dt = DateTime.Now;
                _salForCasComp.Sfm_mod_by = Session["UserID"].ToString();
                _salForCasComp.Sfm_mod_dt = DateTime.Now;
                _salForCasCompList.Add(_salForCasComp);
                BindCCbCompanyGrid();
                txtCCBCompany.Text = string.Empty;
                txtCCBCompany_TextChanged(null, null);
                txtCCBCompDesc.Text = string.Empty;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void BindCCbCompanyGrid()
        {
            dgvCCBCompany.DataSource = new int[] { };
            if (_salForCasCompList.Count > 0)
            {
                dgvCCBCompany.DataSource = _salForCasCompList;
            }
            dgvCCBCompany.DataBind();
        }
        protected void chkCCBComActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var lb = (CheckBox)sender;
                var row = (GridViewRow)lb.NamingContainer;
                CheckBox chkCCActive = row.FindControl("chkCCBComActive") as CheckBox;
                Label lblSfm_alw_com = row.FindControl("lblSfm_alw_com") as Label;
                Label lblSfm_cale_cd = row.FindControl("lblSfm_cale_cd") as Label;
                Int32 act = chkCCActive.Checked ? 1 : 0;
                var v = _salForCasCompList.Where(c => c.Sfm_alw_com == lblSfm_alw_com.Text && c.Sfm_cale_cd == lblSfm_cale_cd.Text).FirstOrDefault();
                if (v != null)
                {
                    v.Sfm_act = act;
                    v.Sfm_mod_by = Session["UserID"].ToString();
                    v.Sfm_mod_dt = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void dgvCCBCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvCCBCompany.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvCCBCompany.DataSource = _salForCasCompList;
                }
                else
                {
                    dgvCCBCompany.DataSource = new int[] { };
                }
                dgvCCBCompany.DataBind();
                PopupComCompany.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtCCCalenderCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearCalCreate();
                if (!string.IsNullOrEmpty(txtCCCalenderCode.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, "CODE", txtCCCalenderCode.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCCCalenderCode.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCCCalenderCode.ToolTip = _toolTip;
                        txtCCCompany.Enabled = false;
                        txtYearFrom.Enabled = false;
                        txtYearTo.Enabled = false;
                        ddlMonth.Enabled = false;
                        ddlMonthEnd.Enabled = false;
                        //lblCCBDesc.Text = _toolTip;
                        LoadCalenderData(txtCCCalenderCode.Text.Trim().ToUpper());
                    }
                    else
                    {
                        txtCCCalenderCode.Text = string.Empty;
                        txtCCCalenderCode.Focus();
                        DispMsg("Please enter valid calendar code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void LoadCalenderData(string _code)
        {
            _salForCal = new SalesForecastingCalendar() { Sfc_cd = _code };
            var v = CHNLSVC.Sales.GetSarForCal(_salForCal).FirstOrDefault();
            if (v != null)
            {
                txtCCCompany.Text = v.Sfc_com;
                txtCCCompany_TextChanged(null, null);
                txtYearFrom.Text = v.Sfc_ye_frm.ToString();
                txtYearTo.Text = v.Sfc_ye_to.ToString();
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(v.Sfc_st_mnt.ToString()));
                ddlMonthEnd.SelectedIndex = ddlMonthEnd.Items.IndexOf(ddlMonthEnd.Items.FindByValue(v.sfc_ed_mnt.ToString()));
                txtCCDesc.Text = v.Sfc_desc;
                chkCCActive.Checked = v.Sfc_act == 1 ? true : false;
            }
        }
        protected void lbtnSeCCCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                _calSerTp = "SalForCaleCCC";
                if (string.IsNullOrEmpty(txtCCCompany.Text))
                {
                    DispMsg("Please select the company !"); txtCCCompany.Focus(); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                //_serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, null, null);
                _serData = GetSerchDataTable(_para, null, null);
                LoadSearchPopup("SalForCaleCCC", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private bool ValidatePeriod(DateTime _frDt, DateTime _toDt, Int32 _rangeVal)
        {
            bool b = false;
            if (_frDt <= _toDt)
            {
                TimeSpan ts = _toDt.Subtract(_frDt);
                if (ts.Days <= _rangeVal)
                {
                    b = true;
                }
            }
            return b;
        }
        private bool ValidatePeriod(DateTime _frDt, DateTime _toDt, Int32 _rangeVal, string _per)
        {
            bool b = false;
            if (_per == "ANNUAL")
            {
                if (_toDt <= _frDt.AddYears(1))
                {
                    b = true;
                }
            }
            if (_per == "MONTHLY")
            {
                if (_toDt <= _frDt.AddMonths(1))
                {
                    b = true;
                }
            }
            if (_per == "WEEKLY")
            {
                if (_toDt <= _frDt.AddDays(7))
                {
                    b = true;
                }
            }
            return b;
        }

        protected void lbtnCPSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                string _docNo = "";
                Int32 _res = -1;
                #region Validate Change Period
                if (string.IsNullOrEmpty(txtCPCom.Text))
                {
                    txtCPCom.Focus(); DispMsg("Please select the company !"); return;
                }
                if (string.IsNullOrEmpty(txtCPCalender.Text))
                {
                    txtCPCalender.Focus(); DispMsg("Please select the calendar !"); return;
                }
                if (ddlCPTp.SelectedIndex < 1)
                {
                    DispMsg("Please select the period !"); return;
                }
                if (string.IsNullOrEmpty(txtCPParCode.Text))
                {
                    _salForMstPer = new SalesForecastingMasterPeriod() { Mfp_seq = Convert.ToInt32(ddlCPTp.SelectedValue), Mfp_desc = ddlCPTp.SelectedItem.Text };
                    var v = CHNLSVC.Sales.Get_MST_FOR_PD_TP(_salForMstPer).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.Mfp_tp_cd != v.Mfp_parent_cd)
                        {
                            txtCPParCode.Focus(); DispMsg("Please select the parent code !"); return;
                        }
                    }
                }
                //if (string.IsNullOrEmpty(txtCPParCode.Text))
                //{
                //    txtCPParCode.Focus(); DispMsg("Please select the parent code !"); return;
                //}
                if (string.IsNullOrEmpty(txtCPFrom.Text))
                {
                    txtCPParCode.Focus(); DispMsg("Please select the period from !"); return;
                }
                if (string.IsNullOrEmpty(txtCPTo.Text))
                {
                    txtCPParCode.Focus(); DispMsg("Please select the period to !"); return;
                }
                if (string.IsNullOrEmpty(txtCPDesc.Text))
                {
                    txtCPParCode.Focus(); DispMsg("Please enter description !"); return;
                }
                DateTime _dtFrom = DateTime.Today;
                DateTime _dtTo = DateTime.Today;
                if (!DateTime.TryParse(txtCPFrom.Text, out _dtFrom))
                {
                    DispMsg("Period from is invalid !"); return;
                }
                if (!DateTime.TryParse(txtCPTo.Text, out _dtTo))
                {
                    DispMsg("Period to is invalid !"); return;
                }
                _salForMstPer = CHNLSVC.Sales.Get_MST_FOR_PD_TP(new SalesForecastingMasterPeriod() { Mfp_desc = ddlCPTp.SelectedItem.Text, Mfp_seq = Convert.ToInt32(ddlCPTp.SelectedValue) }).FirstOrDefault();
                if (_salForMstPer == null)
                {
                    DispMsg("Invalid Date Range !"); return;
                }
                if (_salForMstPer.Mfp_dys == 0)
                {
                    DispMsg("Invalid Date Range !"); return;
                }
                if (!ValidatePeriod(_dtFrom, _dtTo, _salForMstPer.Mfp_dys))
                {
                    DispMsg("Selected Date range is not in Calendar period!"); return;
                }
                if (!ValidatePeriod(_dtFrom, _dtTo, _salForMstPer.Mfp_dys, ddlCPTp.SelectedItem.Text))
                {
                    DispMsg("Invalid Date Range !"); return;
                }
                _salForCal = CHNLSVC.Sales.GetSarForCal(new SalesForecastingCalendar() { Sfc_cd = txtCPCalender.Text.ToUpper().Trim() }).FirstOrDefault();
                if (_salForCal != null)
                {
                    DateTime _calFrom = new DateTime(_salForCal.Sfc_ye_frm, _salForCal.Sfc_st_mnt, 1);
                    DateTime _calTo = new DateTime(_salForCal.Sfc_ye_to, _salForCal.sfc_ed_mnt, 1);
                    _calTo = _calTo.AddMonths(1).AddDays(-1);
                    if (ddlCPTp.SelectedValue == "1")
                    {
                        _calTo = _calTo.AddYears(1).AddDays(-1);
                    }
                    else
                    {
                        _calTo = _calTo.AddMonths(1).AddDays(-1);
                    }

                    if (_dtFrom < _calFrom || _dtFrom > _calTo)
                    {
                        DispMsg("Selected Date range is not in Calendar period!"); return;
                    }
                    if (_dtTo < _calFrom || _dtTo > _calTo)
                    {
                        DispMsg("Invalid Date Range !"); return;
                    }
                }
                if (!string.IsNullOrEmpty(txtCPParCode.Text))
                {
                    _salForCasPer = CHNLSVC.Sales.Get_SAR_FOR_PD(new SalesForecastingPeriod() { Sfp_pd_cd = txtCPParCode.Text.Trim().ToUpper() }).FirstOrDefault();
                    if (_salForCasPer != null)
                    {
                        if (_dtFrom < _salForCasPer.Sfp_frm_pd && _dtTo > _salForCasPer.Sfp_to_pd)
                        {
                            DispMsg("Invalid Date Range !"); return;
                        }
                    }
                }
                #endregion
                //_salForCasPer

                MasterAutoNumber _mstAutoSalForPer = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _mstAutoSalForPer.Aut_cate_cd = "";
                _mstAutoSalForPer.Aut_cate_tp = "";
                _mstAutoSalForPer.Aut_direction = null;
                _mstAutoSalForPer.Aut_modify_dt = null;
                _mstAutoSalForPer.Aut_moduleid = "SFP";
                _mstAutoSalForPer.Aut_number = 0;
                _mstAutoSalForPer.Aut_start_char = "SFP";
                _mstAutoSalForPer.Aut_year = null;
                #endregion

                _salForCasPer = new SalesForecastingPeriod();
                _salForCasPer.Sfp_seq = 0;
                _salForCasPer.Sfp_com = txtCPCom.Text.Trim().ToUpper();
                _salForCasPer.Sfp_pd_tp = Convert.ToInt32(ddlCPTp.SelectedValue);
                _salForCasPer.Sfp_par_cd = string.IsNullOrEmpty(txtCPParCode.Text) ? "N/A" : txtCPParCode.Text.Trim().ToUpper();
                _salForCasPer.Sfp_pd_cd = string.IsNullOrEmpty(txtCPCode.Text) ? null : txtCPCode.Text.Trim().ToUpper();
                _salForCasPer.Sfp_frm_pd = _dtFrom;
                _salForCasPer.Sfp_to_pd = _dtTo;
                _salForCasPer.Sfp_desc = txtCPDesc.Text;
                _salForCasPer.Sfp_act = chkCPActive.Checked ? 1 : 0;
                _salForCasPer.Sfp_cre_by = Session["UserID"].ToString();
                _salForCasPer.Sfp_cre_dt = DateTime.Now;
                _salForCasPer.Sfp_mod_by = Session["UserID"].ToString();
                _salForCasPer.Sfp_mod_dt = DateTime.Now;
                _salForCasPer.Sfp_cal_cd = txtCPCalender.Text.Trim().ToUpper();
                _res = CHNLSVC.Sales.SaveSalesForecastingPeriod(_mstAutoSalForPer, _salForCasPer, out _error, out _docNo);
                if (_res > 0)
                {
                    DispMsg("Successfully saved ! " + _docNo, "S");
                    ClearCrePeriod();
                    txtCPCode.Text = "";
                }
                else
                {
                    DispMsg(_error, "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnCPClear_Click(object sender, EventArgs e)
        {
            ClearCrePeriod();
            txtCPCode.Text = "";
            txtCPCode.ToolTip = "";
        }
        private void ClearCrePeriod()
        {
            txtCPCom.Text = Session["UserCompanyCode"].ToString();
            txtCPCom_TextChanged(null, null);
            lblCPComp.Text = "";
            txtCPCalender.Text = "";
            txtCPCalender.ToolTip = "";
            lblCPCalender.Text = "";
            ddlCPTp.SelectedIndex = 0;
            txtCPParCode.Text = "";
            txtCPParCode.ToolTip = "";
            lblCPParDes.Text = "";
            txtCPFrom.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            txtCPTo.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            txtCPDesc.Text = "";
            chkCPActive.Checked = true;
            DesableCrePeriod(true);
            lblCPduration.Text = "";
        }
        private void DesableCrePeriod(bool b)
        {
            txtCPCom.Enabled = b;
            txtCPCalender.Enabled = b;
            ddlCPTp.Enabled = b;
            txtCPParCode.Enabled = b;
            btnCPFrom.Enabled = b;
            btnCPTo.Enabled = b;
            // txtCPDesc.Enabled = b;
            //chkCPActive.Checked = true;
        }
        protected void lbtnCPClose_Click(object sender, EventArgs e)
        {
            _crePerPopShow = false;
            PopupCrePer.Hide();
            lbtnCPClear_Click(sender, e);
        }

        protected void lbtnCrePeriod_Click(object sender, EventArgs e)
        {
            txtCPCom.Text = Session["UserCompanyCode"].ToString();
            txtCPCom_TextChanged(null, null);
            _crePerPopShow = true;
            PopupCrePer.Show();
        }

        private void BindPeriodTp()
        {
            _salForMstPerList = new List<SalesForecastingMasterPeriod>();
            _salForMstPer = new SalesForecastingMasterPeriod() { };
            _salForMstPerList = CHNLSVC.Sales.Get_MST_FOR_PD_TP(_salForMstPer);
            while (ddlCPTp.Items.Count > 1)
            {
                ddlCPTp.Items.RemoveAt(1);
            }
            if (_salForMstPerList.Count > 0)
            {
                ddlCPTp.DataSource = _salForMstPerList;
                ddlCPTp.DataValueField = "MFP_SEQ";
                ddlCPTp.DataTextField = "MFP_DESC";
                ddlCPTp.DataBind();
            }
        }
        protected void txtCPCom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCPCom.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCPCom.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtCPCom.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCPCom.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCPCom.ToolTip = _ava ? _toolTip : "";
                    lblCPComp.Text = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtCPCom.Text = string.Empty;
                        txtCPCom.Focus();
                        DispMsg("Please enter valid company !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCPCompny_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                LoadSearchPopup("CPCompany", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCPCalender_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<SalesForecastingCalendar> curCalendar = new List<SalesForecastingCalendar>();
                lblCPCalender.Text = "";
                txtCPCalender.ToolTip = "";
                if (!string.IsNullOrEmpty(txtCPCalender.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, "CODE", txtCPCalender.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCPCalender.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCPCalender.ToolTip = _toolTip;
                        lblCPCalender.Text = _toolTip;

                        SalesForecastingCalendar selectCalendar = new SalesForecastingCalendar();
                        selectCalendar.Sfc_cd = txtCPCalender.Text;
                        selectCalendar.Sfc_com = Session["UserCompanyCode"].ToString();
                        curCalendar = CHNLSVC.Sales.GetSarForCal(selectCalendar);
                        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                        string frmMonth = mfi.GetMonthName(curCalendar.FirstOrDefault().Sfc_st_mnt).ToUpper();
                        string toMonth = mfi.GetMonthName(curCalendar.FirstOrDefault().sfc_ed_mnt).ToUpper();

                        string duration = "FROM " + frmMonth + " " + curCalendar.FirstOrDefault().Sfc_ye_frm + " TO " + toMonth + " " + curCalendar.FirstOrDefault().Sfc_ye_to;
                        lblCPduration.Text = duration;
                    }
                    else
                    {
                        txtCPCalender.Text = string.Empty;
                        txtCPCalender.Focus();
                        DispMsg("Please enter valid calendar code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void ddlCPTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCPParCode.Enabled = true;
            lbtnSeCPParCd.Visible = true;
            if (ddlCPTp.SelectedIndex > 0)
            {
                if (ddlCPTp.SelectedItem.ToString() == "ANNUAL")
                {
                    txtCPTo.Text = Convert.ToDateTime(txtCPFrom.Text).AddYears(1).AddDays(-1).ToString("dd/MMM/yyyy");
                    txtCPParCode.Enabled = false;
                    lbtnSeCPParCd.Visible = false;
                }
                if (ddlCPTp.SelectedItem.ToString() == "MONTHLY")
                {
                    txtCPTo.Text = Convert.ToDateTime(txtCPFrom.Text).AddMonths(1).AddDays(-1).ToString("dd/MMM/yyyy");
                }
                if (ddlCPTp.SelectedItem.ToString() == "WEEKLY")
                {
                    txtCPTo.Text = Convert.ToDateTime(txtCPFrom.Text).AddDays(7).AddDays(-1).ToString("dd/MMM/yyyy");
                }
                txtCPParCode.Text = "";
            }
        }

        protected void txtCPParCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblCPParDes.Text = "";
                txtCPParCode.ToolTip = "";
                if (!string.IsNullOrEmpty(txtCPParCode.Text))
                {
                    if (ddlCPTp.SelectedIndex < 1)
                    {
                        txtCPParCode.Text = ""; txtCPParCode.Focus();
                        DispMsg("Please select the period type !"); return;
                    }
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodParent2);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, "CODE", txtCPParCode.Text.ToUpper().Trim(), DateTime.MinValue.ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                    DataAvailable(_serData, txtCPParCode.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCPParCode.ToolTip = _toolTip;
                        lblCPParDes.Text = _toolTip;
                    }
                    else
                    {
                        txtCPParCode.Text = string.Empty;
                        txtCPParCode.Focus();
                        if (ddlCPTp.SelectedItem.Text != "ANNUAL")
                        {
                            DispMsg("Please enter valid parent code !");
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCPParCd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCPTp.SelectedIndex < 1)
                {
                    txtCPParCode.Text = ""; txtCPParCode.Focus();
                    DispMsg("Please select the period type !"); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodParent2);

                DateTime dt = DateTime.Now;
                txtCPToDt.Text = dt.ToString("dd/MMM/yyyy");
                txtCPFromDt.Text = dt.AddMonths(-1).ToString("dd/MMM/yyyy");
                _salForCal = CHNLSVC.Sales.GetSarForCal(new SalesForecastingCalendar() { Sfc_cd = txtCPCalender.Text.ToUpper().Trim() }).FirstOrDefault();
                if (_salForCal != null)
                {
                    DateTime _calFrom = new DateTime(_salForCal.Sfc_ye_frm, _salForCal.Sfc_st_mnt, 1);
                    DateTime _calTo = new DateTime(_salForCal.Sfc_ye_to, _salForCal.sfc_ed_mnt, 1);
                    txtCPFromDt.Text = _calFrom.ToString("dd/MMM/yyyy");
                    txtCPToDt.Text = _calTo.AddMonths(1).AddDays(-1).ToString("dd/MMM/yyyy");
                }
                // _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, null, null, DateTime.MinValue.ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, null, null, txtCPFromDt.Text, txtCPToDt.Text);
                LoadSearchPopup2("CPSalForPeriodParent", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCPCode_TextChanged(object sender, EventArgs e)
        {
            ClearCrePeriod();
            if (!string.IsNullOrEmpty(txtCPCode.Text))
            {
                _salForCasPer = CHNLSVC.Sales.Get_SAR_FOR_PD(new SalesForecastingPeriod() { Sfp_pd_cd = txtCPCode.Text.ToUpper().Trim() }).FirstOrDefault();
                if (_salForCasPer != null)
                {
                    txtCPCom.Text = _salForCasPer.Sfp_com;
                    txtCPCom_TextChanged(null, null);
                    txtCPCalender.Text = _salForCasPer.Sfp_cal_cd;
                    txtCPCalender_TextChanged(null, null);
                    ddlCPTp.SelectedIndex = ddlCPTp.Items.IndexOf(ddlCPTp.Items.FindByValue(_salForCasPer.Sfp_pd_tp.ToString()));
                    txtCPParCode.Text = _salForCasPer.Sfp_par_cd;
                    if (!string.IsNullOrEmpty(txtCPParCode.Text))
                    {
                        txtCPParCode_TextChanged(null, null);
                    }
                    txtCPFrom.Text = _salForCasPer.Sfp_frm_pd.ToString("dd/MMM/yyyy");
                    txtCPTo.Text = _salForCasPer.Sfp_to_pd.ToString("dd/MMM/yyyy");
                    txtCPDesc.Text = _salForCasPer.Sfp_desc;
                    chkCPActive.Checked = _salForCasPer.Sfp_act == 1 ? true : false;

                    DesableCrePeriod(false);
                }
                else
                {
                    txtCPCode.Text = "";
                    txtCPCode.Focus();
                    DispMsg("Invalid period !"); return;
                }
            }
        }

        protected void lbtnSeCPCode_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriod);
                _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, null, null, DateTime.MinValue.ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                LoadSearchPopup2("CPSalForPeriod", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCPCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForCale);
                // _serData = CHNLSVC.Sales.SearchSalesForecastingCalendar(_para, null, null);
                _serData = GetSerchDataTable(_para, null, null);
                LoadSearchPopup("CPSalForCale", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblPer.Text = "";
                txtPeriod.ToolTip = "";
                txtPerFrom.Text = "";
                txtPerTo.Text = "";
                ddlDefTp.SelectedIndex = 0;
                ddlDefTp_SelectedIndexChanged(null, null);
                ddlCatDefOn.SelectedIndex = 0;
                ddlCatDefOn_SelectedIndexChanged(null, null);
                if (!string.IsNullOrEmpty(txtPeriod.Text))
                {
                    if (string.IsNullOrEmpty(txtCalenderCode.Text))
                    {
                        txtPeriod.Text = ""; txtCalenderCode.Focus(); DispMsg("Please select the calendar code !"); return;
                    }

                    //_para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodParent1);
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodCal);
                    _serData = CHNLSVC.Sales.SearchSalesForecastingPeriodByCal(_para, "CODE", txtPeriod.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtPeriod.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtPeriod.ToolTip = _toolTip;
                        lblPer.Text = _toolTip;
                        _salForCasPer = CHNLSVC.Sales.Get_SAR_FOR_PD(new SalesForecastingPeriod() { Sfp_pd_cd = txtPeriod.Text.Trim().ToUpper() }).FirstOrDefault();
                        if (_salForCasPer != null)
                        {
                            txtPerFrom.Text = _salForCasPer.Sfp_frm_pd.ToString("dd/MMM/yyyy");
                            txtPerTo.Text = _salForCasPer.Sfp_to_pd.ToString("dd/MMM/yyyy");
                            LoadData();

                        }
                    }
                    else
                    {
                        txtPeriod.Text = string.Empty;
                        txtPeriod.Focus();
                        DispMsg("Please enter valid period code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSePeriod_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCalenderCode.Text))
                {
                    txtCalenderCode.Focus(); DispMsg("Please select the calendar code !"); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodCal);
                _serData = CHNLSVC.Sales.SearchSalesForecastingPeriodByCal(_para, null, null);
                LoadSearchPopup("SalForPeriodCal", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeDefine_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDefTp.SelectedIndex < 1)
                {
                    ddlDefTp.Focus(); DispMsg("Please select the define by"); return;
                }
                /*COM,CHNL,SCHNL,AREA,REGION,ZONE,PC*/
                if (ddlDefTp.SelectedValue == "COM")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Company", "CODE", "ASC");
                }
                else if (ddlDefTp.SelectedValue == "CHNL")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Channel", "CODE", "ASC");
                }
                else if (ddlDefTp.SelectedValue == "SCHNL")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_SubChannel", "CODE", "ASC");
                }
                else if (ddlDefTp.SelectedValue == "AREA")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Area", "CODE", "ASC");
                }
                else if (ddlDefTp.SelectedValue == "REGION")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Region", "CODE", "ASC");
                }
                else if (ddlDefTp.SelectedValue == "ZONE")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Zone", "CODE", "ASC");
                }
                else if (ddlDefTp.SelectedValue == "PC")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, null, null);
                    LoadSearchPopup("AllProfitCenters", "CODE", "ASC");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtItemCode.ToolTip = "";
                txtBrand.Text = ""; txtBrand.ToolTip = "";
                txtModel.Text = ""; txtModel.ToolTip = "";
                txtCat1.Text = ""; txtCat1.ToolTip = "";
                txtCat2.Text = ""; txtCat2.ToolTip = "";
                txtCat3.Text = ""; txtCat3.ToolTip = "";
                if (!string.IsNullOrEmpty(txtItemCode.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, "ITEM", txtItemCode.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtItemCode.Text.ToUpper().Trim(), "ITEM", "Description");
                    if (_ava)
                    {
                        txtItemCode.ToolTip = _toolTip;
                        _mstItem = CHNLSVC.General.GetItemMaster(txtItemCode.Text.ToUpper().Trim());
                        if (_mstItem != null)
                        {
                            txtBrand.Text = string.IsNullOrEmpty(_mstItem.Mi_brand) ? "N/A" : _mstItem.Mi_brand == "NA" ? "N/A" : _mstItem.Mi_brand;
                            txtBrand_TextChanged(null, null);
                            txtModel.Text = string.IsNullOrEmpty(_mstItem.Mi_model) ? "N/A" : _mstItem.Mi_model == "NA" ? "N/A" : _mstItem.Mi_model;
                            txtModel_TextChanged(null, null);
                            txtCat1.Text = string.IsNullOrEmpty(_mstItem.Mi_cate_1) ? "N/A" : _mstItem.Mi_cate_1 == "NA" ? "N/A" : _mstItem.Mi_cate_1;
                            txtCat1_TextChanged(null, null);
                            txtCat2.Text = string.IsNullOrEmpty(_mstItem.Mi_cate_2) ? "N/A" : _mstItem.Mi_cate_2 == "NA" ? "N/A" : _mstItem.Mi_cate_2;
                            txtCat2_TextChanged(null, null);
                            txtCat3.Text = string.IsNullOrEmpty(_mstItem.Mi_cate_3) ? "N/A" : _mstItem.Mi_cate_3 == "NA" ? "N/A" : _mstItem.Mi_cate_3;
                            txtCat3_TextChanged(null, null);
                        }
                    }
                    else
                    {
                        txtItemCode.Text = string.Empty;
                        txtItemCode.Focus();
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

        protected void lbtnSeItem_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, null, null);
                LoadSearchPopup("Item", "ITEM", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBrand.ToolTip = "";
                if (!string.IsNullOrEmpty(txtBrand.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    _serData = CHNLSVC.CommonSearch.GetItemBrands(_para, "CODE", txtBrand.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtBrand.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtBrand.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtBrand.Text = string.Empty;
                        txtBrand.Focus();
                        DispMsg("Please enter valid brand !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeBrand_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                _serData = CHNLSVC.CommonSearch.GetItemBrands(_para, null, null);
                LoadSearchPopup("Brand", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtModel.ToolTip = "";
                if (!string.IsNullOrEmpty(txtModel.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    _serData = CHNLSVC.CommonSearch.GetAllModels(_para, "CODE", txtModel.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtModel.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtModel.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtModel.Text = string.Empty;
                        txtModel.Focus();
                        DispMsg("Please enter valid model !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeModel_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _serData = CHNLSVC.CommonSearch.GetAllModels(_para, null, null);
                LoadSearchPopup("Model", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCat1.ToolTip = "";
                if (!string.IsNullOrEmpty(txtCat1.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", txtCat1.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCat1.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCat1.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtCat1.Text = string.Empty;
                        txtCat1.Focus();
                        DispMsg("Please enter valid main category !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCat1_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, null, null);
                LoadSearchPopup("CAT_Main", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCat2.ToolTip = "";
                if (!string.IsNullOrEmpty(txtCat2.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", txtCat2.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCat2.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCat2.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtCat2.Text = string.Empty;
                        txtCat2.Focus();
                        DispMsg("Please enter valid sub category !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCat2_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, null, null);
                LoadSearchPopup("CAT_Sub1", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCat3.ToolTip = "";
                if (!string.IsNullOrEmpty(txtCat3.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", txtCat3.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCat3.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtCat3.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtCat3.Text = string.Empty;
                        txtCat3.Focus();
                        DispMsg("Please enter valid category !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCat3_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, null, null);
                LoadSearchPopup("CAT_Sub2", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtDefCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblDefCdDes.Text = "";
                txtDefCode.ToolTip = "";
                if (!string.IsNullOrEmpty(txtDefCode.Text))
                {
                    _ava = false; _toolTip = "";
                    if (ddlDefTp.SelectedIndex < 1)
                    {
                        txtDefCode.Text = ""; ddlDefTp.Focus(); DispMsg("Please select the define by"); return;
                    }
                    if (ddlDefTp.SelectedValue == "COM")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());

                    }
                    else if (ddlDefTp.SelectedValue == "CHNL")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (ddlDefTp.SelectedValue == "SCHNL")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (ddlDefTp.SelectedValue == "AREA")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (ddlDefTp.SelectedValue == "REGION")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (ddlDefTp.SelectedValue == "ZONE")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (ddlDefTp.SelectedValue == "PC")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                        _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    DataAvailable(_serData, txtDefCode.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtDefCode.ToolTip = _toolTip;
                        lblDefCdDes.Text = _toolTip;
                        LoadData();
                    }
                    else
                    {
                        txtDefCode.Text = string.Empty;
                        txtDefCode.ToolTip = string.Empty;
                        txtDefCode.Focus();
                        string msgType = "";
                        msgType = ddlDefTp.SelectedValue == "COM" ? "company" :
                            ddlDefTp.SelectedValue == "CHNL" ? "channel" :
                            ddlDefTp.SelectedValue == "SCHNL" ? "sub channel" :
                            ddlDefTp.SelectedValue == "AREA" ? "area" :
                            ddlDefTp.SelectedValue == "REGION" ? "region" :
                            ddlDefTp.SelectedValue == "ZONE" ? "zone" :
                            ddlDefTp.SelectedValue == "PC" ? "profit center" :
                            "";
                        string _msg = "Please enter valid " + msgType + " !";
                        DispMsg(_msg);
                        return;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlDefTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDefTp.SelectedIndex > 0)
            {
                txtDefCode.Text = "";
                txtDefCode.Focus();
                // LoadData();
            }
        }

        protected void ddlCatDefOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _salForDetList = new List<SalesForecastingDetail>();
                //BindTargGrid();
                List<TextBox> _txtList = new List<TextBox>();
                List<LinkButton> _lbtn = new List<LinkButton>();
                _txtList.Add(txtItemCode);
                _txtList.Add(txtBrand);
                _txtList.Add(txtModel);
                _txtList.Add(txtCat1);
                _txtList.Add(txtCat2);
                _txtList.Add(txtCat3);

                _lbtn.Add(lbtnSeItem);
                _lbtn.Add(lbtnSeBrand);
                _lbtn.Add(lbtnSeModel);
                _lbtn.Add(lbtnSeCat1);
                _lbtn.Add(lbtnSeCat2);
                _lbtn.Add(lbtnSeCat3);

                DesDefControler(_txtList, _lbtn, false);
                _txtList = new List<TextBox>(); _lbtn = new List<LinkButton>();
                if (ddlCatDefOn.SelectedIndex > 0)
                {
                    #region Validate Hdr
                    if (string.IsNullOrEmpty(txtCompany.Text))
                    {
                        ddlCatDefOn.SelectedIndex = 0; txtCompany.Focus(); DispMsg("Please select company !"); return;
                    }
                    if (string.IsNullOrEmpty(txtCalenderCode.Text))
                    {
                        ddlCatDefOn.SelectedIndex = 0; txtCalenderCode.Focus(); DispMsg("Please select calendar !"); return;
                    }
                    if (string.IsNullOrEmpty(txtPeriod.Text))
                    {
                        ddlCatDefOn.SelectedIndex = 0; txtPeriod.Focus(); DispMsg("Please select period !"); return;
                    }
                    if (ddlDefTp.SelectedIndex < 1)
                    {
                        ddlCatDefOn.SelectedIndex = 0; ddlDefTp.Focus(); DispMsg("Please select define by !"); return;
                    }
                    if (string.IsNullOrEmpty(txtDefCode.Text))
                    {
                        ddlCatDefOn.SelectedIndex = 0; txtDefCode.Focus(); DispMsg("Please select define by code !"); return;
                    }

                    #endregion

                    if (ddlCatDefOn.SelectedValue == "CAT1")
                    {
                        _txtList.Add(txtCat1); _lbtn.Add(lbtnSeCat1);
                        DesDefControler(_txtList, _lbtn, true);
                        txtCat1.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "CAT2")
                    {
                        _txtList.Add(txtCat2); _lbtn.Add(lbtnSeCat2);
                        DesDefControler(_txtList, _lbtn, true);
                        txtCat2.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "CAT3")
                    {
                        _txtList.Add(txtCat3); _lbtn.Add(lbtnSeCat3);
                        DesDefControler(_txtList, _lbtn, true);
                        txtCat3.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "MODEL")
                    {
                        _txtList.Add(txtModel); _lbtn.Add(lbtnSeModel);
                        DesDefControler(_txtList, _lbtn, true);
                        txtModel.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "BRAND")
                    {
                        _txtList.Add(txtBrand); _lbtn.Add(lbtnSeBrand);
                        DesDefControler(_txtList, _lbtn, true);
                        txtBrand.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "ITEM")
                    {
                        _txtList.Add(txtItemCode); _lbtn.Add(lbtnSeItem);
                        DesDefControler(_txtList, _lbtn, true);
                        txtItemCode.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "CAT1_MODEL")
                    {
                        _txtList.Add(txtModel); _lbtn.Add(lbtnSeModel);
                        _txtList.Add(txtCat1); _lbtn.Add(lbtnSeCat1);
                        DesDefControler(_txtList, _lbtn, true);
                        txtModel.Focus();
                    }
                    if (ddlCatDefOn.SelectedValue == "MODEL_BRAND")
                    {
                        _txtList.Add(txtBrand); _lbtn.Add(lbtnSeBrand);
                        _txtList.Add(txtModel); _lbtn.Add(lbtnSeModel);
                        DesDefControler(_txtList, _lbtn, true);
                        txtBrand.Focus();
                    }
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void DesDefControler(List<TextBox> _txtList, List<LinkButton> _lbtnList, bool _ena)
        {
            foreach (TextBox txt in _txtList)
            {
                txt.Enabled = _ena;
                txt.Text = "";
            }
            foreach (LinkButton lbtn in _lbtnList)
            {
                lbtn.Enabled = _ena;
                if (_ena)
                {
                    lbtn.Enabled = true;
                    lbtn.CssClass = "buttonUndocolor";
                }
                else
                {
                    lbtn.CssClass = "buttoncolor";
                    lbtn.OnClientClick = "";
                    lbtn.Enabled = false;
                }
            }
        }

        private void ValidateDet()
        {

        }
        protected void lbtnAddTarItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tmpDec = 0;
                #region Validate Hdr
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    txtCompany.Focus(); DispMsg("Please select company !"); return;
                }
                if (string.IsNullOrEmpty(txtCalenderCode.Text))
                {
                    txtCalenderCode.Focus(); DispMsg("Please select calendar !"); return;
                }
                if (string.IsNullOrEmpty(txtPeriod.Text))
                {
                    txtPeriod.Focus(); DispMsg("Please select period !"); return;
                }
                if (ddlDefTp.SelectedIndex < 1)
                {
                    ddlDefTp.Focus(); DispMsg("Please select define by !"); return;
                }
                if (string.IsNullOrEmpty(txtDefCode.Text))
                {
                    txtDefCode.Focus(); DispMsg("Please select define by code !"); return;
                }
                if (ddlCatDefOn.SelectedIndex < 1)
                {
                    ddlCatDefOn.Focus(); DispMsg("Please select category define on !"); return;
                }
                #endregion
                #region Validate Det
                if (txtItemCode.Enabled && string.IsNullOrEmpty(txtItemCode.Text))
                {
                    txtItemCode.Focus(); DispMsg("Please select item !"); return;
                }
                if (txtBrand.Enabled && string.IsNullOrEmpty(txtBrand.Text))
                {
                    txtBrand.Focus(); DispMsg("Please select brand !"); return;
                }
                if (txtModel.Enabled && string.IsNullOrEmpty(txtModel.Text))
                {
                    txtModel.Focus(); DispMsg("Please select model !"); return;
                }
                if (txtCat1.Enabled && string.IsNullOrEmpty(txtCat1.Text))
                {
                    txtCat1.Focus(); DispMsg("Please select main category !"); return;
                }
                if (txtCat2.Enabled && string.IsNullOrEmpty(txtCat2.Text))
                {
                    txtCat2.Focus(); DispMsg("Please select sub category !"); return;
                }
                if (txtCat3.Enabled && string.IsNullOrEmpty(txtCat3.Text))
                {
                    txtCat3.Focus(); DispMsg("Please select category 3 !"); return;
                }
                if (string.IsNullOrEmpty(txtQty.Text) && string.IsNullOrEmpty(txtVal.Text) && string.IsNullOrEmpty(txtGp.Text))
                {
                    txtQty.Focus(); DispMsg("Please enter quantity/value/gp%  !"); return;
                }
                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    if (!decimal.TryParse(txtQty.Text, out tmpDec))
                    {
                        txtQty.Focus(); DispMsg("Please enter valid quantity !"); return;
                    }
                }
                if (!string.IsNullOrEmpty(txtVal.Text))
                {
                    if (!decimal.TryParse(txtVal.Text, out tmpDec))
                    {
                        txtVal.Focus(); DispMsg("Please enter valid value !"); return;
                    }
                }
                if (!string.IsNullOrEmpty(txtGp.Text))
                {
                    if (!decimal.TryParse(txtGp.Text, out tmpDec))
                    {
                        txtGp.Focus(); DispMsg("Please enter valid gp% !"); return;
                    }
                }
                #endregion
                _salForDet = new SalesForecastingDetail();
                _salForDetails = _salForDetList;
                //_salForDet.Sfd_seq =
                //_salForDet.Sfd_line=
                _salForDet.Sfd_itm = string.IsNullOrEmpty(txtItemCode.Text) ? "N/A" : txtItemCode.Text.Trim().ToUpper();
                _salForDet.Sfd_cat1 = string.IsNullOrEmpty(txtCat1.Text) ? "N/A" : txtCat1.Text.Trim().ToUpper();
                _salForDet.Sfd_cat2 = string.IsNullOrEmpty(txtCat2.Text) ? "N/A" : txtCat2.Text.Trim().ToUpper();
                _salForDet.Sfd_cat3 = string.IsNullOrEmpty(txtCat3.Text) ? "N/A" : txtCat3.Text.Trim().ToUpper();
                _salForDet.Sfd_model = string.IsNullOrEmpty(txtModel.Text) ? "N/A" : txtModel.Text.Trim().ToUpper();
                _salForDet.Sfd_brnd = string.IsNullOrEmpty(txtBrand.Text) ? "N/A" : txtBrand.Text.Trim().ToUpper();
                _salForDet.Sfd_qty = decimal.TryParse(txtQty.Text, out tmpDec) ? Convert.ToDecimal(txtQty.Text) : 0;
                _salForDet.Sfd_val = decimal.TryParse(txtVal.Text, out tmpDec) ? Convert.ToDecimal(txtVal.Text) : 0;
                _salForDet.Sfd_gp = decimal.TryParse(txtGp.Text, out tmpDec) ? Convert.ToDecimal(txtGp.Text) : 0;
                _salForDet.Sfd_act = 1;
                _salForDet.tmp_itm_desc = string.IsNullOrEmpty(txtItemCode.Text) ? "N/A" : txtItemCode.ToolTip;
                _salForDet.tmp_itm_desc = txtItemCode.Text == "N/A" ? "N/A" : txtItemCode.ToolTip;
                _salForDet.tmp_def_on = ddlCatDefOn.SelectedValue;
                _salForDet.tmp_def_by = ddlDefTp.SelectedValue;
                _salForDet.tmp_def_cd = txtDefCode.Text;
                _salForDet.sfd_exc = txtexc.Text;
                if (_salForDet.sfd_exc == "") _salForDet.sfd_exc = "All";
                _salForDet.sfd_inv_type = txtinvtype.Text;
                if (_salForDet.sfd_inv_type == "") _salForDet.sfd_inv_type = "All";
                _salForDet.sfd_manager = txtmanager.Text.ToString();

                Int32 _maxLine = 0;
                if (_salForDetails.Count > 0)
                {
                    _maxLine = _salForDetails.Max(c => c.Sfd_line);
                }
                _salForDet.Sfd_line = _maxLine + 1;
                #region MyRegion
                bool _dataAva = false;
                if (ddlCatDefOn.SelectedValue == "CAT1")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_cat1 == _salForDet.Sfd_cat1 && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "CAT2")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_cat2 == _salForDet.Sfd_cat2 && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "CAT3")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_cat3 == _salForDet.Sfd_cat3 && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "MODEL")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_model == _salForDet.Sfd_model && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "BRAND")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_brnd == _salForDet.Sfd_brnd && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "ITEM")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_itm == _salForDet.Sfd_itm && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "CAT1_MODEL")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_cat1 == _salForDet.Sfd_cat1 && c.Sfd_model == _salForDet.Sfd_model && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                else if (ddlCatDefOn.SelectedValue == "MODEL_BRAND")
                {
                    var vAva = _salForDetails.Where(c => c.tmp_def_on == _salForDet.tmp_def_on && c.Sfd_brnd == _salForDet.Sfd_brnd && c.Sfd_model == _salForDet.Sfd_model && c.Sfd_act == _salForDet.Sfd_act).FirstOrDefault();
                    if (vAva != null)
                    {
                        _dataAva = true;
                    }
                }
                if (_dataAva)
                {
                    DispMsg("The entered target details were already added !"); ddlCatDefOn.Focus(); return;
                }
                #endregion
                _salForDetails.Add(_salForDet);
                txtItemCode.Text = ""; txtModel.Text = ""; txtBrand.Text = ""; txtCat1.Text = ""; txtCat2.Text = ""; txtCat3.Text = "";
                txtItemCode.ToolTip = ""; txtModel.ToolTip = ""; txtBrand.ToolTip = ""; txtCat1.ToolTip = ""; txtCat2.ToolTip = ""; txtCat3.ToolTip = "";
                txtQty.Text = ""; txtVal.Text = ""; txtGp.Text = "";
                _salForDetList = _salForDetails;
                BindTargGrid();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnTarDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblSfd_seq = dr.FindControl("lblSfd_seq") as Label;
                Label lblSfd_line = dr.FindControl("lblSfd_line") as Label;
                Int32 seqNo = Convert.ToInt32(lblSfd_seq.Text);
                Int32 linNo = Convert.ToInt32(lblSfd_line.Text);
                if (seqNo == 0)
                {
                    var del = _salForDetList.Where(c => c.Sfd_line == linNo).FirstOrDefault();
                    if (del != null)
                    {
                        _salForDetList.Remove(del);
                    }
                }
                else
                {
                    List<SalesForecastingDetail> _list = new List<SalesForecastingDetail>();
                    var del = _salForDetList.Where(c => c.Sfd_seq == seqNo && c.Sfd_line == linNo).FirstOrDefault();
                    if (del != null)
                    {
                        del.Sfd_act = 0;
                        _list.Add(del);
                        Int32 _delRes = CHNLSVC.Sales.DeleteSalesForecasting(new SalesForecastingHeader(), _list, out _error);
                        if (_delRes > 0)
                        {
                            DispMsg("Successfully deleted and Saved !", "S");
                        }
                        else
                        {
                            DispMsg(_error, "E"); return;
                        }
                    }
                }
                BindTargGrid();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void BindTargGrid()
        {
            dgvTarget.DataSource = new int[] { };
            if (_salForDetList.Count > 0)
            {
                foreach (var item in _salForDetList)
                {
                    _mstItem = CHNLSVC.General.GetItemMaster(item.Sfd_itm);
                    if (_mstItem != null)
                    {
                        item.tmp_itm_desc = _mstItem.Mi_shortdesc;
                    }
                }
                dgvTarget.DataSource = _salForDetList.Where(c => c.Sfd_act == 1).OrderBy(x => x.tmp_def_by).ToList();
            }
            dgvTarget.DataBind();
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validate Hdr
                if (hdfSaveTp.Value == "excel")
                {
                    lbtnExcSave_Click(sender, e);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtCompany.Text))
                    {
                        txtCompany.Focus(); DispMsg("Please select company !"); return;
                    }
                    if (string.IsNullOrEmpty(txtCalenderCode.Text))
                    {
                        txtCalenderCode.Focus(); DispMsg("Please select calendar !"); return;
                    }
                    if (string.IsNullOrEmpty(txtPeriod.Text))
                    {
                        txtPeriod.Focus(); DispMsg("Please select period !"); return;
                    }
                    if (ddlDefTp.SelectedIndex < 1)
                    {
                        ddlDefTp.Focus(); DispMsg("Please select define by !"); return;
                    }
                    if (string.IsNullOrEmpty(txtDefCode.Text))
                    {
                        txtDefCode.Focus(); DispMsg("Please select define by code !"); return;
                    }
                    if (ddlCatDefOn.SelectedIndex < 1)
                    {
                        ddlCatDefOn.Focus(); DispMsg("Please select category define on !"); return;
                    }
                #endregion
                    #region FillHdr
                    string _error = "";
                    Int32 _res = -1;
                    _salForHdr = new SalesForecastingHeader();
                    //_salForHed.Sfh_seq 		=
                    _salForHdr.Sfh_com = txtCompany.Text.ToUpper().Trim();
                    _salForHdr.Sfh_cal_cd = txtCalenderCode.Text.ToUpper().Trim();
                    DateTime dtCalFrom = Convert.ToDateTime(txtCalFrom.Text);
                    DateTime dtCalTo = Convert.ToDateTime(txtCalTo.Text);
                    _salForHdr.Sfh_calfrm_ye = dtCalFrom.Year;
                    _salForHdr.Sfh_calto_ye = dtCalTo.Year;
                    _salForHdr.Sfh_calfrm_mnt = dtCalFrom.Month;
                    _salForHdr.Sfh_calto_mnt = dtCalTo.Month;
                    _salForHdr.Sfh_pd_cd = txtPeriod.Text.ToUpper().Trim();
                    _salForHdr.Sfh_pd_frm = Convert.ToDateTime(txtPerFrom.Text);
                    _salForHdr.Sfh_pd_to = Convert.ToDateTime(txtPerTo.Text);
                    _salForHdr.Sfh_def_by = ddlDefTp.SelectedValue;
                    _salForHdr.Sfh_def_cd = txtDefCode.Text.ToUpper().Trim();
                    _salForHdr.Sfh_def_on = ddlCatDefOn.SelectedValue;
                    _salForHdr.Sfh_act = chkActTarg.Checked ? 1 : 0;
                    _salForHdr.Sfh_cre_by = Session["UserID"].ToString();
                    _salForHdr.Sfh_cre_dt = DateTime.Now;
                    _salForHdr.Sfh_mod_by = Session["UserID"].ToString();
                    _salForHdr.Sfh_mod_dt = DateTime.Now;
                    _salForHdr.sfh_type = "TRGT";
                    #endregion
                    if (_salForDetList.Count < 1)
                    {
                        DispMsg("Please add target details"); return;
                    }
                    _res = CHNLSVC.Sales.SaveSalesForecasting(_salForHdr, _salForDetList, out _error);
                    if (_res > 0)
                    {
                        DispMsg("Successfully saved !", "S");
                        ClearPage();
                    }
                    else
                    {
                        DispMsg(_error, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void LoadData()
        {
            _salForDetails = new List<SalesForecastingDetail>();
            _salForHdr = new SalesForecastingHeader();
            _salForHdr.Sfh_com = string.IsNullOrEmpty(txtCompany.Text) ? "" : txtCompany.Text.ToUpper().Trim();
            _salForHdr.Sfh_cal_cd = string.IsNullOrEmpty(txtCalenderCode.Text) ? "" : txtCalenderCode.Text.ToUpper().Trim();
            _salForHdr.Sfh_pd_cd = string.IsNullOrEmpty(txtPeriod.Text) ? "" : txtPeriod.Text.ToUpper().Trim();
            _salForHdr.Sfh_def_by = ddlDefTp.SelectedIndex < 1 ? "" : ddlDefTp.SelectedValue;
            _salForHdr.Sfh_def_cd = string.IsNullOrEmpty(txtDefCode.Text) ? "" : txtDefCode.Text.ToUpper().Trim();
            _salForHdr.Sfh_def_on = ddlCatDefOn.SelectedIndex < 1 ? "" : ddlCatDefOn.SelectedValue;
            _salForHdr.Sfh_act = 1;
            if (!string.IsNullOrEmpty(_salForHdr.Sfh_com)
                && !string.IsNullOrEmpty(_salForHdr.Sfh_cal_cd)
                && !string.IsNullOrEmpty(_salForHdr.Sfh_pd_cd)
                //&& !string.IsNullOrEmpty(_salForHdr.Sfh_def_by)
                //&& !string.IsNullOrEmpty(_salForHdr.Sfh_def_cd)
                //&& !string.IsNullOrEmpty(_salForHdr.Sfh_def_on)
                )
            {

                _salForDetList = CHNLSVC.Sales.GET_SAR_FOR_DET_BY_HDR(_salForHdr);

                List<SalesForecastingHeader> _salForHdrList = new List<SalesForecastingHeader>();
                _salForHdrList = CHNLSVC.Sales.GET_SAR_FOR_HDR(Session["UserCompanyCode"].ToString(), txtCalenderCode.Text, txtPeriod.Text);

                foreach (SalesForecastingDetail _salDet in _salForDetList)
                {
                    if (_salForHdrList.Where(x => x.Sfh_seq == _salDet.Sfd_seq).ToList().Count > 0)
                    {
                        _salDet.tmp_def_by = _salForHdrList.Where(x => x.Sfh_seq == _salDet.Sfd_seq).FirstOrDefault().Sfh_def_by;
                        _salDet.tmp_def_cd = _salForHdrList.Where(x => x.Sfh_seq == _salDet.Sfd_seq).FirstOrDefault().Sfh_def_cd;
                        _salDet.tmp_def_on = _salForHdrList.Where(x => x.Sfh_seq == _salDet.Sfd_seq).FirstOrDefault().Sfh_def_on;
                    }
                }

                BindTargGrid();
            }
        }

        protected void lbtnUploadFile_Click(object sender, EventArgs e)
        {
            #region Validate Hdr
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                txtCompany.Focus(); DispMsg("Please select company !"); return;
            }
            if (string.IsNullOrEmpty(txtCalenderCode.Text))
            {
                txtCalenderCode.Focus(); DispMsg("Please select calendar !"); return;
            }
            if (string.IsNullOrEmpty(txtPeriod.Text))
            {
                txtPeriod.Focus(); DispMsg("Please select period !"); return;
            }
            #endregion
            lblExcelUploadError.Visible = false;
            lblExcelUploadError.Text = "";
            _showExcelPop = true;
            popupExcel.Show();
        }

        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcel.Hide();
        }

        protected void lbtnUploadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                lblExcelUploadError.Visible = false;
                lblExcelUploadError.Text = "";
                if (fileUploadExcel.HasFile)
                {
                    string FileName = Path.GetFileName(fileUploadExcel.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileUploadExcel.PostedFile.FileName);

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                    {

                    }
                    else
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Please select a valid excel (.xls or .xlsx) file";
                    }

                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string ValidateFilePath = Server.MapPath(FolderPath + FileName);
                    fileUploadExcel.SaveAs(ValidateFilePath);
                    _filPath = ValidateFilePath;
                    _showExcelPop = false;
                    popupExcel.Hide();

                    lblProcess.Visible = true;
                    lblProcess.Text = "Excel file upload completed. Do you want to process ? ";
                    _showProcPop = true;
                    popupExcProc.Show();
                }
                else
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Please select the correct upload file path !";
                }
            }
            catch (Exception ex)
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "Error Occurred :" + ex.Message;
            }
        }

        protected void lbtnProcClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcProc.Hide();
        }
        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }
        public DataTable[] LoadValidateData(string FileName, out string _error)
        {
            _error = "";
            #region Excel Process
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = cn;

                    dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    cn.Close();

                    //Read Data from First Sheet
                    cn.Open();
                    cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);
                }
                catch (Exception ex)
                {
                    _error = ex.Message;
                    return new DataTable[] { Tax };
                }
                return new DataTable[] { Tax };
            }
            #endregion
        }

        protected void lbtnExcelProcess_Click(object sender, EventArgs e)
        {
            try
            {
                List<SalesForecastingDetail> _saveForSalDet = new List<SalesForecastingDetail>();
                string _error = "";
                lblExcelUploadError.Visible = false;
                lblExcelUploadError.Text = "";
                hdfSaveTp.Value = "excel";
                if (string.IsNullOrEmpty(_filPath))
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "File Path Invalid Please Upload ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DataTable[] GetExecelTbl = LoadValidateData(_filPath, out _error);
                if (GetExecelTbl == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = _error;
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DataTable _dtExData = GetExecelTbl[0];
                if (_dtExData.Rows.Count < 1 && _dtExData.Columns.Count < 12)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Excel Data Invalid Please check Excel File and Upload";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                _salForDetails = new List<SalesForecastingDetail>();
                List<SalesForecastingDetail> tmpErrList = new List<SalesForecastingDetail>();
                decimal _tmpDec = 0;
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    _salForDet = new SalesForecastingDetail();
                    _salForDet.tmp_def_by = _dtExData.Rows[i][0].ToString();
                    _salForDet.tmp_def_cd = _dtExData.Rows[i][1].ToString();
                    _salForDet.tmp_def_on = _dtExData.Rows[i][2].ToString();
                    _salForDet.Sfd_itm = _dtExData.Rows[i][3].ToString();
                    _salForDet.Sfd_brnd = _dtExData.Rows[i][4].ToString();
                    _salForDet.Sfd_model = _dtExData.Rows[i][5].ToString();
                    _salForDet.Sfd_cat1 = _dtExData.Rows[i][6].ToString();
                    _salForDet.Sfd_cat2 = _dtExData.Rows[i][7].ToString();
                    _salForDet.Sfd_cat3 = _dtExData.Rows[i][8].ToString();
                    decimal.TryParse(_dtExData.Rows[i][9].ToString(), out _tmpDec);
                    _salForDet.Sfd_qty = decimal.TryParse(_dtExData.Rows[i][9].ToString(), out _tmpDec) ? Convert.ToDecimal(_dtExData.Rows[i][9].ToString()) : 0;
                    _salForDet.Sfd_val = decimal.TryParse(_dtExData.Rows[i][10].ToString(), out _tmpDec) ? Convert.ToDecimal(_dtExData.Rows[i][10].ToString()) : 0;
                    _salForDet.Sfd_gp = decimal.TryParse(_dtExData.Rows[i][11].ToString(), out _tmpDec) ? Convert.ToDecimal(_dtExData.Rows[i][11].ToString()) : 0;
                    _salForDet.Sfd_act = 1;
                    _salForDetails.Add(_salForDet);
                }
                Int32 _rNo = 0;

                foreach (var itm in _salForDetails)
                {
                    #region Validate
                    _rNo++;
                    if (!_defByList.Contains(itm.tmp_def_by))
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Define by is invalid ! " + "Row No : " + _rNo; continue;
                    }
                    if (_defByList.Contains(itm.tmp_def_by))
                    {
                        #region Validate def code
                        if (itm.tmp_def_by == "COM")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.tmp_def_cd);

                        }
                        else if (itm.tmp_def_by == "CHNL")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.tmp_def_cd);
                        }
                        else if (itm.tmp_def_by == "SCHNL")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.tmp_def_cd);
                        }
                        else if (itm.tmp_def_by == "AREA")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.tmp_def_cd);
                        }
                        else if (itm.tmp_def_by == "REGION")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.tmp_def_cd);
                        }
                        else if (itm.tmp_def_by == "ZONE")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.tmp_def_cd);
                        }
                        else if (itm.tmp_def_by == "PC")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                            _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", itm.tmp_def_cd);
                        }
                        DataAvailable(_serData, itm.tmp_def_cd, "CODE", "Description");
                        if (!_ava)
                        {
                            tmpErrList.Add(itm); itm.tmp_err_desc = "Define code is invalid ! " + "Row No : " + _rNo; continue;
                        }
                        #endregion
                    }
                    if (!_defOnList.Contains(itm.tmp_def_on))
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Define on is invalid ! " + "Row No : " + _rNo; continue;
                    }
                    if (itm.Sfd_qty < 0)
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Target quantity is invalid ! " + "Row No : " + _rNo; continue;
                    }
                    if (itm.Sfd_val < 0)
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Target Valus is invalid ! " + "Row No : " + _rNo; continue;
                    }
                    if (itm.Sfd_gp < 0)
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Target GP % is invalid ! " + "Row No : " + _rNo; continue;
                    }

                    if (_defOnList.Contains(itm.tmp_def_on))
                    {
                        #region def ON
                        string _erTp = "";
                        if (itm.tmp_def_on == "CAT1")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                            _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", itm.Sfd_cat1);
                            DataAvailable(_serData, itm.Sfd_cat1, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Main category is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT2")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                            _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", itm.Sfd_cat2);
                            DataAvailable(_serData, itm.Sfd_cat2, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Sub category is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT3")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                            _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", itm.Sfd_cat3);
                            DataAvailable(_serData, itm.Sfd_cat3, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Category 3 is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "MODEL")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                            _serData = CHNLSVC.CommonSearch.GetAllModels(_para, "CODE", itm.Sfd_model);
                            DataAvailable(_serData, itm.Sfd_model, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Model is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "BRAND")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                            _serData = CHNLSVC.CommonSearch.GetItemBrands(_para, "CODE", itm.Sfd_brnd);
                            DataAvailable(_serData, itm.Sfd_brnd, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Brand is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "ITEM")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                            _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, "ITEM", itm.Sfd_itm);
                            DataAvailable(_serData, itm.Sfd_itm, "ITEM", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Item is invalid ! " + "Row No : " + _rNo; continue;
                            }
                            _mstItem = CHNLSVC.General.GetItemMaster(itm.Sfd_itm);
                            itm.Sfd_cat1 = _mstItem.Mi_cate_1;
                            itm.Sfd_cat2 = _mstItem.Mi_cate_2;
                            itm.Sfd_cat3 = _mstItem.Mi_cate_3;
                            itm.Sfd_brnd = _mstItem.Mi_brand;
                            itm.Sfd_model = _mstItem.Mi_model;
                        }
                        if (itm.tmp_def_on == "CAT1_MODEL")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                            _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_para, "CODE", itm.Sfd_cat1);
                            DataAvailable(_serData, itm.Sfd_cat1, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Main category is invalid ! " + "Row No : " + _rNo; continue;
                            }

                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                            _serData = CHNLSVC.CommonSearch.GetAllModels(_para, "CODE", itm.Sfd_model);
                            DataAvailable(_serData, itm.Sfd_model, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Model is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "MODEL_BRAND")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                            _serData = CHNLSVC.CommonSearch.GetAllModels(_para, "CODE", itm.Sfd_model);
                            DataAvailable(_serData, itm.Sfd_model, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Model is invalid ! " + "Row No : " + _rNo; continue;
                            }
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                            _serData = CHNLSVC.CommonSearch.GetItemBrands(_para, "CODE", itm.Sfd_brnd);
                            DataAvailable(_serData, itm.Sfd_brnd, "CODE", "Description");
                            if (!_ava)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "Brand is invalid ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.Sfd_qty <= 0 && itm.Sfd_val <= 0 && itm.Sfd_gp <= 0)
                        {
                            tmpErrList.Add(itm); itm.tmp_err_desc = "No quantity/value/gp data found !" + "Row No : " + _rNo; continue;
                        }
                        #endregion

                        #region Def on already exc
                        bool _dataAva = false;
                        if (itm.tmp_def_on == "CAT1")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat1 == itm.Sfd_cat1 && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The main category is already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT2")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat2 == itm.Sfd_cat2 && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The sub category is already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT3")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat3 == itm.Sfd_cat3 && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The category 3 is already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "MODEL")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_model == itm.Sfd_model && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The model is already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "BRAND")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_brnd == itm.Sfd_brnd && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The brand is already exists   ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "ITEM")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_itm == itm.Sfd_itm && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The item is already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT1_MODEL")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat1 == itm.Sfd_cat1 && c.Sfd_model == itm.Sfd_model && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The category + model already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "MODEL_BRAND")
                        {
                            var vAva = _saveForSalDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_brnd == itm.Sfd_brnd && c.Sfd_model == itm.Sfd_model && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The model + brand already exists ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        #endregion
                        #region already added
                        _salForHdr = new SalesForecastingHeader();
                        _salForHdr.Sfh_com = string.IsNullOrEmpty(txtCompany.Text) ? "" : txtCompany.Text.ToUpper().Trim();
                        _salForHdr.Sfh_cal_cd = string.IsNullOrEmpty(txtCalenderCode.Text) ? "" : txtCalenderCode.Text.ToUpper().Trim();
                        _salForHdr.Sfh_pd_cd = string.IsNullOrEmpty(txtPeriod.Text) ? "" : txtPeriod.Text.ToUpper().Trim();
                        _salForHdr.Sfh_def_by = itm.tmp_def_by;
                        _salForHdr.Sfh_def_cd = itm.tmp_def_cd;
                        _salForHdr.Sfh_def_on = itm.tmp_def_on;
                        _salForHdr.Sfh_act = 1;
                        var sarDet = CHNLSVC.Sales.GET_SAR_FOR_DET_BY_HDR(_salForHdr);
                        foreach (var sarItem in sarDet)
                        {
                            sarItem.tmp_def_by = itm.tmp_def_by;
                            sarItem.tmp_def_cd = itm.tmp_def_cd;
                            sarItem.tmp_def_on = itm.tmp_def_on;
                        }
                        if (itm.tmp_def_on == "CAT1")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat1 == itm.Sfd_cat1 && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The main category is already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT2")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat2 == itm.Sfd_cat2 && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The sub category is already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT3")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat3 == itm.Sfd_cat3 && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The category 3 is already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "MODEL")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_model == itm.Sfd_model && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The model is already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "BRAND")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_brnd == itm.Sfd_brnd && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The brand is already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "ITEM")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_itm == itm.Sfd_itm && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The item is already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "CAT1_MODEL")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_cat1 == itm.Sfd_cat1 && c.Sfd_model == itm.Sfd_model && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The category + model already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        if (itm.tmp_def_on == "MODEL_BRAND")
                        {
                            var vAva = sarDet.Where(c => c.tmp_def_by == itm.tmp_def_by && c.tmp_def_cd == itm.tmp_def_cd && c.tmp_def_on == itm.tmp_def_on && c.Sfd_brnd == itm.Sfd_brnd && c.Sfd_model == itm.Sfd_model && c.Sfd_act == itm.Sfd_act).FirstOrDefault();
                            if (vAva != null)
                            {
                                tmpErrList.Add(itm); itm.tmp_err_desc = "The model + brand already added ! " + "Row No : " + _rNo; continue;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    _saveForSalDet.Add(itm);
                }

                if (tmpErrList.Count > 0)
                {
                    dgvExcel.DataSource = tmpErrList;
                    dgvExcel.DataBind();
                    _showErrPop = true;
                    popupErro.Show();
                }
                else
                {
                    _salForDetList = _saveForSalDet;
                    dgvTarSave.DataSource = _saveForSalDet;
                    dgvTarSave.DataBind();
                    popupExcelSave.Show();
                    _showExcSave = true;
                }

            }
            catch (Exception ex)
            {
                _showExcelPop = true;
                popupExcel.Show();
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "Error Occurred :" + ex.Message;
            }
        }
        protected void lbtnExcClose_Click(object sender, EventArgs e)
        {
            _showErrPop = false;
            popupErro.Hide();
        }


        protected void lbtnExcSaveClos_Click(object sender, EventArgs e)
        {
            _showExcSave = false;
            popupExcelSave.Hide();
        }

        protected void lbtnExcSave_Click(object sender, EventArgs e)
        {
            try
            {
                var _result = _salForDetList.GroupBy(x => new { x.tmp_def_by, x.tmp_def_cd, x.tmp_def_on }).Select(
                    g => new { g.Key.tmp_def_by, g.Key.tmp_def_cd, g.Key.tmp_def_on });
                if (_result != null)
                {
                    string _error = "";
                    Int32 _res = -1;
                    if (_salForDetList.Count < 1)
                    {
                        DispMsg("Please add target details"); return;
                    }
                    foreach (var item in _result)
                    {
                        _salForDetails = new List<SalesForecastingDetail>();
                        _salForDetails = _salForDetList.Where(c => c.tmp_def_by == item.tmp_def_by && c.tmp_def_cd == item.tmp_def_cd && c.tmp_def_on == item.tmp_def_on).ToList();
                        #region Validate Hdr
                        if (string.IsNullOrEmpty(txtCompany.Text))
                        {
                            txtCompany.Focus(); DispMsg("Please select company !"); return;
                        }
                        if (string.IsNullOrEmpty(txtCalenderCode.Text))
                        {
                            txtCalenderCode.Focus(); DispMsg("Please select calendar !"); return;
                        }
                        if (string.IsNullOrEmpty(txtPeriod.Text))
                        {
                            txtPeriod.Focus(); DispMsg("Please select period !"); return;
                        }
                        #endregion
                        #region FillHdr

                        _salForHdr = new SalesForecastingHeader();
                        //_salForHed.Sfh_seq 		=
                        _salForHdr.Sfh_com = txtCompany.Text.ToUpper().Trim();
                        _salForHdr.Sfh_cal_cd = txtCalenderCode.Text.ToUpper().Trim();
                        DateTime dtCalFrom = Convert.ToDateTime(txtCalFrom.Text);
                        DateTime dtCalTo = Convert.ToDateTime(txtCalTo.Text);
                        _salForHdr.Sfh_calfrm_ye = dtCalFrom.Year;
                        _salForHdr.Sfh_calto_ye = dtCalTo.Year;
                        _salForHdr.Sfh_calfrm_mnt = dtCalFrom.Month;
                        _salForHdr.Sfh_calto_mnt = dtCalTo.Month;
                        _salForHdr.Sfh_pd_cd = txtPeriod.Text.ToUpper().Trim();
                        _salForHdr.Sfh_pd_frm = Convert.ToDateTime(txtPerFrom.Text);
                        _salForHdr.Sfh_pd_to = Convert.ToDateTime(txtPerTo.Text);
                        _salForHdr.Sfh_def_by = item.tmp_def_by;
                        _salForHdr.Sfh_def_cd = item.tmp_def_cd;
                        _salForHdr.Sfh_def_on = item.tmp_def_on;
                        _salForHdr.Sfh_act = 1;
                        _salForHdr.Sfh_cre_by = Session["UserID"].ToString();
                        _salForHdr.Sfh_cre_dt = DateTime.Now;
                        _salForHdr.Sfh_mod_by = Session["UserID"].ToString();
                        _salForHdr.Sfh_mod_dt = DateTime.Now;
                        #endregion
                        Int32 line = 0;
                        var _savedData = CHNLSVC.Sales.GET_SAR_FOR_DET_BY_HDR(_salForHdr);
                        if (_savedData != null)
                        {
                            if (_savedData.Count > 0)
                            {
                                line = _savedData.Max(c => c.Sfd_line);
                            }
                        }
                        foreach (var _line in _salForDetails)
                        {
                            line++;
                            _line.Sfd_line = line;
                        }
                        _res = CHNLSVC.Sales.SaveSalesForecasting(_salForHdr, _salForDetails, out _error);
                    }
                    if (_res > 0)
                    {
                        DispMsg("Successfully saved !", "S");
                        ClearPage();
                        popupExcelSave.Hide();
                        _showExcSave = false;
                        hdfSaveTp.Value = "0";
                    }
                    else
                    {
                        DispMsg(_error, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnCPDtSer_Click(object sender, EventArgs e)
        {
            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalForPeriodParent2);
            _serData = CHNLSVC.Sales.SearchSalesForecastingPeriod(_para, null, null, txtCPFromDt.Text, txtCPToDt.Text);
            LoadSearchPopup2("CPSalForPeriodParent", "CODE", "ASC");

        }

        protected void dgvResult2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult2.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult2.DataSource = _serData;
                }
                else
                {
                    dgvResult2.DataSource = new int[] { };
                }
                dgvResult2.DataBind();
                txtSerByKey2.Text = string.Empty;
                txtSerByKey2.Focus();
                PopupSearch2.Show();
                _showDateSearch = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult2.SelectedRow.Cells[1].Text;
                if (_serType == "CPSalForPeriodParent")
                {
                    txtCPParCode.Text = code;
                    txtCPParCode_TextChanged(null, null);
                }
                if (_serType == "CPSalForPeriod")
                {
                    txtCPCode.Text = code;
                    txtCPCode_TextChanged(null, null);
                }
                _showDateSearch = false;
                PopupSearch2.Hide();

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnPop_Click(object sender, EventArgs e)
        {
            PopupSearch2.Hide();
            _showDateSearch = false;
        }

        protected void txtexc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                _serData = CHNLSVC.CommonSearch.Get_Sales_Ex(_para, "CODE", txtexc.Text.ToUpper().Trim());
                if (_serData == null || _serData.Rows.Count==0)
                {
                    DispMsg("Invalid Executive", "N");
                    txtexc.Text = "";
                    return;
                }
            }catch(Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
          
        }

        protected void lbtnexc_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                _serData = CHNLSVC.CommonSearch.Get_Sales_Ex(_para, null, null);
                LoadSearchPopup("EpfNo", "Epf", "ASC");
            }catch(Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
            
        }

        protected void txtinvtype_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtninvtype_Click(object sender, EventArgs e)
        {

        }

        protected void txtinvtype_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void lbtninvtype_Click1(object sender, EventArgs e)
        {

        }

        protected void txtmanager_TextChanged(object sender, EventArgs e)
        {

        }
        
        protected void lbtnchangedate_Click(object sender, EventArgs e)
        {
            try
            {
               popupchngedt.Show();
            }catch(Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnsavedatechange_Click(object sender, EventArgs e)
        {
            try
            {
                int effect = CHNLSVC.Financial.UpdateTargertMonth(Session["UserCompanyCode"].ToString(), txtCalenderCode.Text.ToString(), txtPeriod.Text.ToString(), Convert.ToDateTime(txtchngefdt.Text.ToString()), Convert.ToDateTime(txtchngetdt.Text.ToString()));
                if (effect >0)
                {
                    DispMsg("Succrssfully Saved","S");
                }
                else
                {
                    DispMsg( "Error","E");
                }
            }catch(Exception ex){
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnclosedatechange_Click(object sender, EventArgs e)
        {
        
        }
    }
}