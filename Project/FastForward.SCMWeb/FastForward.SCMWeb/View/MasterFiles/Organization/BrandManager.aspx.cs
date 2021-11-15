using FastForward.SCMWeb.Services;
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

namespace FastForward.SCMWeb.View.MasterFiles.Organization
{
    public partial class BrandManager : BasePage
    {

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
        string _calSerTp
        {
            get { if (Session["_calSerTp"] != null) { return (string)Session["_calSerTp"]; } else { return ""; } }
            set { Session["_calSerTp"] = value; }
        }

        bool _ava
        {
            get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
            set { Session["_ava"] = value; }
        }

        string _toolTip
        {
            get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
            set { Session["_toolTip"] = value; }
        }

        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }

        List<mst_brnd_alloc> _BrandManList
        {
            get { if (Session["_BrandManList"] != null) { return (List<mst_brnd_alloc>)Session["_BrandManList"]; } else { return new List<mst_brnd_alloc>(); } }
            set { Session["_BrandManList"] = value; }
        }

        bool _showExcelPop
        {
            get { if (Session["_showExcelPop"] != null) { return (bool)Session["_showExcelPop"]; } else { return false; } }
            set { Session["_showExcelPop"] = value; }
        }

        string _filPath
        {
            get { if (Session["_filPath"] != null) { return (string)Session["_filPath"]; } else { return ""; } }
            set { Session["_filPath"] = value; }
        }

        bool _showProcPop
        {
            get { if (Session["_showProcPop"] != null) { return (bool)Session["_showProcPop"]; } else { return false; } }
            set { Session["_showProcPop"] = value; }
        }

        List<string> _defByList
        {
            get { if (Session["_defByList"] != null) { return (List<string>)Session["_defByList"]; } else { return new List<string>(); } }
            set { Session["_defByList"] = value; }
        }

        List<mst_brnd_alloc> _brnFormanList
        {
            get { if (Session["_brnFormanList"] != null) { return (List<mst_brnd_alloc>)Session["_brnFormanList"]; } else { return new List<mst_brnd_alloc>(); } }
            set { Session["_brnFormanList"] = value; }
        }

        bool _showDateSearch
        {
            get { if (Session["_showDateSearch"] != null) { return (bool)Session["_showDateSearch"]; } else { return false; } }
            set { Session["_showDateSearch"] = value; }
        }

        List<string> _defOnList
        {
            get { if (Session["_defOnList"] != null) { return (List<string>)Session["_defOnList"]; } else { return new List<string>(); } }
            set { Session["_defOnList"] = value; }
        }
        string _para = "";

        mst_brnd_alloc _managerdetailsob = new mst_brnd_alloc();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                txtSDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtEDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            }
        }

        protected void lbtnSeManager_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetManagersetupSearchData(_para, null, null);
                LoadSearchPopup("Manager", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
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
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
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

        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Manager:
                    {
                        paramsText.Append(txtManager.Text.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForCale:
                    {
                        if (_calSerTp == "SalForCaleCCC")
                        {
                            paramsText.Append(txtManager.Text.Trim().ToUpper() + seperator);
                            break;
                        }
                        paramsText.Append(txtManager.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalForPeriod:
                    {
                        paramsText.Append(txtManager.Text.ToString() + seperator + "" + seperator + "0" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.SalForPeriodParent1:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + (string.IsNullOrEmpty(txtCPParCode.Text) ? "" : txtCPParCode.Text.ToUpper().Trim()) + seperator +
                //           (ddlCPTp.SelectedIndex > 0 ? ddlCPTp.SelectedValue : "0") + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.SalForPeriodParent2:
                //    {
                //        Int32 sfp_pd_tp = 0;
                //        if (ddlCPTp.SelectedIndex > 0)
                //        {
                //            _salForMstPer = new SalesForecastingMasterPeriod() { Mfp_seq = Convert.ToInt32(ddlCPTp.SelectedValue), Mfp_desc = ddlCPTp.SelectedItem.Text };
                //            var v = CHNLSVC.Sales.Get_MST_FOR_PD_TP(_salForMstPer).FirstOrDefault();
                //            if (v != null)
                //            {
                //                sfp_pd_tp = v.Mfp_parent_cd;
                //            }
                //        }
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + (string.IsNullOrEmpty(txtCPParCode.Text) ? "" : txtCPParCode.Text.ToUpper().Trim()) + seperator +
                //           sfp_pd_tp + seperator + (string.IsNullOrEmpty(txtCPCalender.Text) ? "" : txtCPCalender.Text.ToUpper().Trim()) + seperator);
                //        break;
                //    }
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
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub2.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub3.ToString() + seperator + "CAT_Sub3" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + txtCat4.Text + seperator + "CAT_Sub4" + seperator);
                        break;
                    }


                //case CommonUIDefiniton.SearchUserControlType.SalForPeriodCal:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtCalenderCode.Text.ToUpper() + seperator + "" + seperator);
                //        break;
                //    }
                default:
                    break;
            }
            return paramsText.ToString();
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
        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "Manager")
                {
                    txtManager.Text = code;
                    txtManager_TextChanged(null, null);
                }
                else if (_serType == "Pc_HIRC_Company" || _serType == "Pc_HIRC_Channel" || _serType == "Pc_HIRC_SubChannel"
                    || _serType == "Pc_HIRC_Area" || _serType == "Pc_HIRC_Region" || _serType == "Pc_HIRC_Zone" || _serType == "AllProfitCenters")
                {
                    txtDefCode.Text = code;
                    txtDefCode_TextChanged1(null, null);
                }

                else if (_serType == "Brand")
                {
                    txtBrand.Text = code;
                    txtBrand_TextChanged(null, null);
                }

                if (_serType == "CAT_Main")
                {
                    txtCat1.Text = code;
                    txtCat1_TextChanged(null, null);
                }

                if (_serType == "CAT_Sub1")
                {
                    txtCat2.Text = code;
                    txtCat2_TextChanged(null, null);
                }

                if (_serType == "CAT_Sub2")
                {
                    txtCat3.Text = code;
                    txtCat3_TextChanged(null, null);
                }

                if (_serType == "CAT_Sub3")
                {
                    txtCat4.Text = code;
                    txtCat4_TextChanged(null, null);
                }

                if (_serType == "CAT_Sub4")
                {
                    txtCat5.Text = code;
                    txtCat5_TextChanged(null, null);
                }
            }

            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }


        protected void txtManager_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtManager.ToolTip = string.Empty;
                // txtCalenderCode.Text = string.Empty;
                //txtCalenderCode_TextChanged(null, null);
                if (!string.IsNullOrEmpty(txtManager.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                    _serData = CHNLSVC.CommonSearch.GetManagersetupSearchData(_para, "CODE", txtManager.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtManager.Text.ToUpper().Trim(), "CODE", "Description");
                    txtManager.ToolTip = _ava ? _toolTip : "";
                    lblCompany.Text = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtManager.Text = string.Empty;
                        txtManager.Focus();
                        //DispMsg("Please enter valid Manager !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
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
            _serPopShow = false;
            PopupSearch.Hide();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "Manager")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                    _serData = CHNLSVC.CommonSearch.GetManagersetupSearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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
                else if (_serType == "Brand")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    _serData = CHNLSVC.CommonSearch.GetItemBrands(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Main")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Sub1")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Sub2")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Sub3")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "CAT_Sub4")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                txtDefCode.Text = "";
                txtDefCode.Focus();
                //LoadData();
            }
        }




        protected void txtDefCode_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                Label1.Text = "";
                txtDefCode.ToolTip = "";
                if (!string.IsNullOrEmpty(txtDefCode.Text))
                {
                    _ava = false; _toolTip = "";
                    if (DropDownList1.SelectedIndex < 1)
                    {
                        txtDefCode.Text = ""; DropDownList1.Focus(); DispMsg("Please select the define by"); return;
                    }
                    if (DropDownList1.SelectedValue == "COM")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());

                    }
                    else if (DropDownList1.SelectedValue == "CHNL")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (DropDownList1.SelectedValue == "SCHNL")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (DropDownList1.SelectedValue == "AREA")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (DropDownList1.SelectedValue == "REGION")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (DropDownList1.SelectedValue == "ZONE")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    else if (DropDownList1.SelectedValue == "PC")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                        _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", txtDefCode.Text.ToUpper().Trim());
                    }
                    DataAvailable(_serData, txtDefCode.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtDefCode.ToolTip = _toolTip;
                        Label1.Text = _toolTip;
                        // LoadData();
                    }
                    else
                    {
                        txtDefCode.Text = string.Empty;
                        txtDefCode.ToolTip = string.Empty;
                        txtDefCode.Focus();
                        string msgType = "";
                        msgType = DropDownList1.SelectedValue == "COM" ? "company" :
                            DropDownList1.SelectedValue == "CHNL" ? "channel" :
                            DropDownList1.SelectedValue == "SCHNL" ? "sub channel" :
                            DropDownList1.SelectedValue == "AREA" ? "area" :
                            DropDownList1.SelectedValue == "REGION" ? "region" :
                            DropDownList1.SelectedValue == "ZONE" ? "zone" :
                            DropDownList1.SelectedValue == "PC" ? "profit center" :
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

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat1.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat1.Text = string.Empty;
                txtCat1.Focus();
                return;
            }
        }

        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }
        protected void lbtnSrch_Cat1_Click(object sender, EventArgs e)
        {

            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, null, null);
                LoadSearchPopup("CAT_Main", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat2.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat2.Text = string.Empty;
                txtCat2.Focus();
                return;
            }
        }

        protected void lbtnSrch_Cat2_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, null, null);
                LoadSearchPopup("CAT_Sub1", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat3.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat3.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat3.Text = string.Empty;
                txtCat3.Focus();
                return;
            }
        }

        protected void lbtnSrch_Cat3_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, null, null);
                LoadSearchPopup("CAT_Sub2", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat4.Text))
            {
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat4.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat4.Text = string.Empty;
                txtCat4.Focus();
                return;
            }
        }

        protected void lbtnSrch_Cat4_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, null, null);
                LoadSearchPopup("CAT_Sub3", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCat5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat5.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat5.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat5.Text = string.Empty;
                txtCat5.Focus();
                return;
            }
        }

        protected void lbtnSrch_Cat5_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData_FORBM(_para, null, null);
                LoadSearchPopup("CAT_Sub4", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnAddTarItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tmpDec = 0;
                #region Validate Hdr
                if (string.IsNullOrEmpty(txtManager.Text))
                {
                    txtManager.Focus(); DispMsg("Please select a brand manager !"); return;
                }
                if (DropDownList1.SelectedIndex < 1)
                {
                    DropDownList1.Focus(); DispMsg("Please select define by !"); return;
                }
                if (string.IsNullOrEmpty(txtDefCode.Text))
                {
                    txtDefCode.Focus(); DispMsg("Please select a define by code !"); return;
                }

                #endregion
                #region Validate Det

                //if (txtBrand.Enabled && string.IsNullOrEmpty(txtBrand.Text))
                //{
                //    txtBrand.Focus(); DispMsg("Please select brand !"); return;
                //}

                //if (txtCat1.Enabled && string.IsNullOrEmpty(txtCat1.Text))
                //{
                //    txtCat1.Focus(); DispMsg("Please select category 1  !"); return;
                //}
                //if (txtCat2.Enabled && string.IsNullOrEmpty(txtCat2.Text))
                //{
                //    txtCat2.Focus(); DispMsg("Please select category 2  !"); return;
                //}
                //if (txtCat3.Enabled && string.IsNullOrEmpty(txtCat3.Text))
                //{
                //    txtCat3.Focus(); DispMsg("Please select category 3 !"); return;
                //}
                //if (txtCat4.Enabled && string.IsNullOrEmpty(txtCat3.Text))
                //{
                //    txtCat4.Focus(); DispMsg("Please select category 4 !"); return;
                //}
                //if (txtCat5.Enabled && string.IsNullOrEmpty(txtCat3.Text))
                //{
                //    txtCat5.Focus(); DispMsg("Please select category 5 !"); return;
                //}
                if (txtSDate.Enabled && string.IsNullOrEmpty(txtSDate.Text))
                {
                    txtSDate.Focus(); DispMsg("Please select Start Date !"); return;
                }
                if (txtEDate.Enabled && string.IsNullOrEmpty(txtEDate.Text))
                {
                    txtEDate.Focus(); DispMsg("Please select End Date !"); return;
                }
                #endregion
                List<mst_brnd_alloc> _managerdetailsList = Session["_BrandManList"] as List<mst_brnd_alloc>;
                if (_managerdetailsList == null)
                {
                    _managerdetailsList = new List<mst_brnd_alloc>();
                }
                _managerdetailsob = new mst_brnd_alloc();
                _managerdetailsob.Mba_ca1 = txtCat1.Text;
                _managerdetailsob.Mba_ca2 = txtCat2.Text;
                _managerdetailsob.Mba_ca3 = txtCat3.Text;
                _managerdetailsob.Mba_ca4 = txtCat4.Text;
                _managerdetailsob.Mba_ca5 = txtCat5.Text;
                _managerdetailsob.Mba_brnd = txtBrand.Text;
                _managerdetailsob.Mba_emp_id = txtManager.Text;
                _managerdetailsob.Mba_pty_cd = txtDefCode.Text;
                _managerdetailsob.Mba_frm_dt = Convert.ToDateTime(txtSDate.Text);
                _managerdetailsob.Mba_to_dt = Convert.ToDateTime(txtEDate.Text);
                _managerdetailsob.Mba_cre_by = Session["UserID"].ToString();
                _managerdetailsob.Mba_mod_by = Session["UserID"].ToString();
                _managerdetailsob.Mba_act = chkActTarg.Checked ? 1 : 0;
                _managerdetailsob.Mba_com = Session["UserCompanyCode"].ToString();
                _managerdetailsob.Mba_cre_dt = DateTime.Now;
                _managerdetailsob.Mba_mod_dt = DateTime.Now;
                _managerdetailsob.Mba_pty_tp = DropDownList1.SelectedValue.ToString();


                _managerdetailsList.Add(_managerdetailsob);
                Session["_BrandManList"] = _managerdetailsList;
                dgvTarget.DataSource = _managerdetailsList;
                dgvTarget.DataBind();
                ClearPage();
                // _managerdetailsob.Mba_pty_tp = t






            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownList1.SelectedIndex < 1)
                {
                    DropDownList1.Focus(); DispMsg("Please select the define by"); return;
                }
                /*COM,CHNL,SCHNL,AREA,REGION,ZONE,PC*/
                if (DropDownList1.SelectedValue == "COM")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Company", "CODE", "ASC");
                }
                else if (DropDownList1.SelectedValue == "CHNL")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Channel", "CODE", "ASC");
                }
                else if (DropDownList1.SelectedValue == "SCHNL")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_SubChannel", "CODE", "ASC");
                }
                else if (DropDownList1.SelectedValue == "AREA")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Area", "CODE", "ASC");
                }
                else if (DropDownList1.SelectedValue == "REGION")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Region", "CODE", "ASC");
                }
                else if (DropDownList1.SelectedValue == "ZONE")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, null, null);
                    LoadSearchPopup("Pc_HIRC_Zone", "CODE", "ASC");
                }
                else if (DropDownList1.SelectedValue == "PC")
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

        protected void lbtnTarDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblman_cd = dr.FindControl("lblman_cd") as Label;
                Label lblmbaDefBy = dr.FindControl("lblmbaDefBy") as Label;
                Label lblmbaDefCd = dr.FindControl("lblmbaDefCd") as Label;
                Label lblmba_brd = dr.FindControl("lblmba_brd") as Label;
                Label lblmba_cat1 = dr.FindControl("lblmba_cat1") as Label;
                Label lblmba_cat2 = dr.FindControl("lblmba_cat2") as Label;
                Label lblmba_cat3 = dr.FindControl("lblmba_cat3") as Label;
                Label lblmba_cat4 = dr.FindControl("lblmba_cat4") as Label;
                Label lblmba_cat5 = dr.FindControl("lblmba_cat5") as Label;

                List<mst_brnd_alloc> _managerdetailsList = Session["_BrandManList"] as List<mst_brnd_alloc>;
                var del = _managerdetailsList.Where(c => c.Mba_emp_id == lblman_cd.Text && c.Mba_pty_tp == lblmbaDefBy.Text && c.Mba_pty_cd == lblmbaDefCd.Text && c.Mba_brnd == lblmba_brd.Text && c.Mba_ca1 == lblmba_cat1.Text && c.Mba_ca2 == lblmba_cat2.Text && c.Mba_ca3 == lblmba_cat3.Text && c.Mba_ca4 == lblmba_cat4.Text && c.Mba_ca5 == lblmba_cat5.Text).FirstOrDefault();
                if (del != null)
                {
                    _managerdetailsList.Remove(del);
                }

                Session["_BrandManList"] = _managerdetailsList;
                dgvTarget.DataSource = _managerdetailsList;

                dgvTarget.DataBind();





            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int _res = 0;
                List<mst_brnd_alloc> _managerdetailsList = Session["_BrandManList"] as List<mst_brnd_alloc>;
                _res = CHNLSVC.Sales.SaveBrandMangerDtL(_managerdetailsList);
                if (_managerdetailsList.Count < 1)
                {
                    DispMsg("Please add target details", "N"); return;
                }
                if (_res == 1)
                {
                    DispMsg("Successfully Saved!", "S");
                    ClearPageAll();
                }

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lbtnSeManager_Click1(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                _serData = CHNLSVC.CommonSearch.GetManagersetupSearchData(_para, null, null);
                LoadSearchPopup("Manager", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }



        protected void lbtnview_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSDate.Text) && !string.IsNullOrEmpty(txtEDate.Text))
            {
                try
                {

                    List<mst_brnd_alloc> mngerlist = CHNLSVC.Sales.GetBrandManagerDet(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtSDate.Text), Convert.ToDateTime(txtEDate.Text));
                    Session["_BrandManList"] = mngerlist;
                    dgvTarget.DataSource = mngerlist;
                    dgvTarget.DataBind();
                }
                catch (Exception ex)
                {
                    DispMsg(ex.Message, "E");
                }
            }

            else
            {

                DispMsg("Select Strat Date And End Date", "N");

                return;
            }
        }

        protected void lbtnUploadFile_Click(object sender, EventArgs e)
        {

            lblExcelUploadError.Visible = false;
            lblExcelUploadError.Text = "";
            _showExcelPop = true;
            popupExcel.Show();
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
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
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

        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcel.Hide();
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
        protected void lbtnExcelProcess_Click(object sender, EventArgs e)
        {
            try
            {
                List<mst_brnd_alloc> _managerdetailsList = new List<mst_brnd_alloc>();
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
                _managerdetailsList = new List<mst_brnd_alloc>();
                List<mst_brnd_alloc> tmpErrList = new List<mst_brnd_alloc>();
                DateTime _tmpDec = DateTime.Now;
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    mst_brnd_alloc _managerdetailob = new mst_brnd_alloc();
                    _managerdetailob.Mba_com = _dtExData.Rows[i][0].ToString();
                    _managerdetailob.Mba_pty_tp = _dtExData.Rows[i][1].ToString();
                    _managerdetailob.Mba_pty_cd = _dtExData.Rows[i][2].ToString();
                    _managerdetailob.Mba_emp_id = _dtExData.Rows[i][3].ToString();
                    _managerdetailob.Mba_brnd = _dtExData.Rows[i][4].ToString();
                    _managerdetailob.Mba_ca1 = _dtExData.Rows[i][5].ToString();
                    _managerdetailob.Mba_ca2 = _dtExData.Rows[i][6].ToString();
                    _managerdetailob.Mba_ca3 = _dtExData.Rows[i][7].ToString();
                    _managerdetailob.Mba_ca4 = _dtExData.Rows[i][8].ToString();
                    _managerdetailob.Mba_ca5 = _dtExData.Rows[i][9].ToString();
                    _managerdetailob.Mba_frm_dt = DateTime.TryParse(_dtExData.Rows[i][10].ToString(), out _tmpDec) ? Convert.ToDateTime(_dtExData.Rows[i][10].ToString()) : DateTime.Now;
                    _managerdetailob.Mba_to_dt = DateTime.TryParse(_dtExData.Rows[i][10].ToString(), out _tmpDec) ? Convert.ToDateTime(_dtExData.Rows[i][10].ToString()) : DateTime.Now;
                    _managerdetailob.Mba_act = 1;
                    _managerdetailob.Mba_cre_by = Session["UserID"].ToString();
                    _managerdetailob.Mba_cre_dt = DateTime.Now;
                    _managerdetailob.Mba_mod_by = Session["UserID"].ToString();
                    _managerdetailob.Mba_cre_dt = DateTime.Now;
                    _managerdetailsList.Add(_managerdetailob);
                }
                Session["_BrandManList"] = _managerdetailsList;
                Int32 _rNo = 0;

                foreach (var itm in _managerdetailsList)
                {

                    _rNo++;
                    if (!_defByList.Contains(itm.Mba_pty_tp))
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Define by is invalid ! " + "Row No : " + _rNo; continue;
                    }
                    if (_defByList.Contains(itm.Mba_pty_tp))
                    {
                        #region Validate def code
                        if (itm.Mba_pty_tp == "COM")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.Mba_pty_cd);

                        }
                        else if (itm.Mba_pty_tp == "CHNL")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.Mba_pty_cd);
                        }
                        else if (itm.Mba_pty_tp == "SCHNL")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.Mba_pty_cd);
                        }
                        else if (itm.Mba_pty_tp == "AREA")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.Mba_pty_cd);
                        }
                        else if (itm.Mba_pty_tp == "REGION")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.Mba_pty_cd);
                        }
                        else if (itm.Mba_pty_tp == "ZONE")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                            _serData = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_para, "CODE", itm.Mba_pty_cd);
                        }
                        else if (itm.Mba_pty_tp == "PC")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                            _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", itm.Mba_pty_cd);
                        }
                        DataAvailable(_serData, itm.Mba_pty_cd, "CODE", "Description");
                        if (!_ava)
                        {
                            tmpErrList.Add(itm); itm.tmp_err_desc = "Define code is invalid ! " + "Row No : " + _rNo; continue;
                        }
                        #endregion
                    }




                    _managerdetailsList.Add(itm);
                }


                _brnFormanList = _managerdetailsList;
                dgvTarget.DataSource = _managerdetailsList;
                dgvTarget.DataBind();



            }
            catch (Exception ex)
            {
                _showExcelPop = true;
                popupExcel.Show();
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "Error Occurred :" + ex.Message;
            }
        }

        protected void lbtnProcClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcProc.Hide();
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

                Label1.Text = "";
                txtManager.Text = Session["UserCompanyCode"].ToString();
                txtManager_TextChanged(null, null);
                DateTime dt = DateTime.Now;
                txtSDate.Text = dt.ToString("dd/MMM/yyyy");
                txtEDate.Text = dt.ToString("dd/MMM/yyyy");

                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _showExcelPop = false;
                _showProcPop = false;
                _toolTip = "";
                _filPath = "";
                _ava = false;

                //  _brnFormanList = new List<mst_brnd_alloc>();
                txtDefCode.Text = ""; txtBrand.Text = ""; txtCat1.Text = ""; txtCat2.Text = ""; txtCat3.Text = ""; txtCat4.Text = ""; txtCat5.Text = "";
                txtBrand.ToolTip = ""; txtCat1.ToolTip = ""; txtCat2.ToolTip = ""; txtCat3.ToolTip = ""; txtCat4.ToolTip = ""; txtCat5.ToolTip = "";

                // _defByList = new List<string>(); _defOnList = new List<string>();
                GetMasterData();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void ClearPageAll()
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

                Label1.Text = "";
                txtManager.Text = Session["UserCompanyCode"].ToString();
                txtManager_TextChanged(null, null);
                DateTime dt = DateTime.Now;
                txtSDate.Text = dt.ToString("dd/MMM/yyyy");
                txtEDate.Text = dt.ToString("dd/MMM/yyyy");
                dgvTarget.DataSource = new int[] { };
                dgvTarget.DataBind();
                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _showExcelPop = false;
                _showProcPop = false;
                _toolTip = "";
                _filPath = "";
                _ava = false;
                _brnFormanList = new List<mst_brnd_alloc>();
                List<mst_brnd_alloc> _managerdetailsList = Session["_BrandManList"] as List<mst_brnd_alloc>;


                txtDefCode.Text = ""; txtBrand.Text = ""; txtCat1.Text = ""; txtCat2.Text = ""; txtCat3.Text = ""; txtCat4.Text = ""; txtCat5.Text = "";
                txtBrand.ToolTip = ""; txtCat1.ToolTip = ""; txtCat2.ToolTip = ""; txtCat3.ToolTip = ""; txtCat4.ToolTip = ""; txtCat5.ToolTip = "";

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
            tmpDefBy.Add("COM"); tmpDefBy.Add("CHNL"); tmpDefBy.Add("SCHNL"); tmpDefBy.Add("AREA"); tmpDefBy.Add("REGION"); tmpDefBy.Add("ZONE"); tmpDefBy.Add("PC");
            _defByList = tmpDefBy;

        }

        protected void txtEDate_TextChanged(object sender, EventArgs e)
        {
            DateTime temp;

            if ((DateTime.TryParse(txtEDate.Text, out temp)) && (DateTime.TryParse(txtSDate.Text, out temp)))
            {
                DateTime d2 = Convert.ToDateTime(txtSDate.Text);
                DateTime d1 = Convert.ToDateTime(txtEDate.Text);
                if (d1 >= d2)
                {

                }

                else
                {

                    DispMsg("Start date should be less than End date", "N");

                    return;
                }
            }
            else
            {

                DispMsg("Start date should be less than End date", "N");

                return;
            }
        }

        protected void txtSDate_TextChanged(object sender, EventArgs e)
        {
            DateTime temp;

            if ((DateTime.TryParse(txtEDate.Text, out temp)) && (DateTime.TryParse(txtSDate.Text, out temp)))
            {
                DateTime d2 = Convert.ToDateTime(txtSDate.Text);
                DateTime d1 = Convert.ToDateTime(txtEDate.Text);
                if (d1 >= d2)
                {

                }

                else
                {

                    DispMsg("Start date should be less than End date", "N");

                    return;
                }
            }

            else
            {

                DispMsg("Start date should be less than End date", "N");

                return;
            }

        }
    }

}




