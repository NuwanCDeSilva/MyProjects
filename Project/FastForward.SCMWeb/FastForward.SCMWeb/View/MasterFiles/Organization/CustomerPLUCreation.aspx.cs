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
    public partial class CustomerPLUCreation : BasePage
    {
        string _para = "";

        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
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

        string _filPath
        {
            get { if (Session["_filPath"] != null) { return (string)Session["_filPath"]; } else { return ""; } }
            set { Session["_filPath"] = value; }
        }

        List<string> _defByList
        {
            get { if (Session["_defByList"] != null) { return (List<string>)Session["_defByList"]; } else { return new List<string>(); } }
            set { Session["_defByList"] = value; }
        }

        List<mst_busentity_itm> _prlforList
        {
            get { if (Session["_prlforList"] != null) { return (List<mst_busentity_itm>)Session["_prlforList"]; } else { return new List<mst_busentity_itm>(); } }
            set { Session["_prlforList"] = value; }
        }





        mst_busentity_itm _pludetailsob = new mst_busentity_itm();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                ClearPageAll();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int _res = 0;
                List<mst_busentity_itm> _pludetailslist = Session["_plulist"] as List<mst_busentity_itm>;
                if (_pludetailslist != null)
                {
                    _res = CHNLSVC.Sales.SavePluCreationDetails(_pludetailslist);
                    if (_pludetailslist.Count < 1)
                    {
                        DispMsg("Please add target details", "N"); return;
                    }
                    if (_res == 1)
                    {
                        DispMsg("Successfully Saved!", "S");
                        ClearPageAll();
                    }
                }
                else
                {
                    DispMsg("Please Add Items!!!", "N"); return;
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

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                int _res = 0;
                List<mst_busentity_itm> _pludetailslist = Session["_plulist"] as List<mst_busentity_itm>;
                _res = CHNLSVC.Sales.UpdatePluCreationDetails(_pludetailslist);
                if (_pludetailslist.Count < 1)
                {
                    DispMsg("Please add details", "N"); return;
                }
                if (_res == 1)
                {
                    DispMsg("Successfully Updated!", "S");
                    ClearPageAll();
                }

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }



        }

        protected void lbtnUploadFile_Click(object sender, EventArgs e)
        {
            lblExcelUploadError.Visible = false;
            lblExcelUploadError.Text = "";
            _showExcelPop = true;
            popupExcel.Show();
        }

        protected void txtcuscode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtcuscode.ToolTip = "";
                if (!string.IsNullOrEmpty(txtcuscode.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.cuscode);
                    _serData = CHNLSVC.CommonSearch.GetCustomerCodeDetails(_para, "CODE", txtcuscode.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtcuscode.Text.ToUpper().Trim(), "CODE", "NAME");
                    if (_ava)
                    {
                        txtcuscode.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtcuscode.Text = string.Empty;
                        txtcuscode.Focus();
                        DispMsg("Please enter valid Customer Code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtncuscode_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.cuscode);
                _serData = CHNLSVC.CommonSearch.GetCustomerCodeDetails(_para, null, null);
                LoadSearchPopup("cuscode", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }



        protected void txtitmcd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtitmcd.ToolTip = "";
                if (!string.IsNullOrEmpty(txtitmcd.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.itmcode);
                    _serData = CHNLSVC.CommonSearch.GetItemCodeDetails(_para, "CODE", txtitmcd.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtitmcd.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtitmcd.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtitmcd.Text = string.Empty;
                        txtitmcd.Focus();
                        DispMsg("Please enter valid Item Code !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnitmcd_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.itmcode);
                _serData = CHNLSVC.CommonSearch.GetItemCodeDetails(_para, null, null);
                LoadSearchPopup("itmcode", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }


        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.cuscode:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.itmcode:
                    {
                        paramsText.Append(txtitmcd.Text.ToString().Trim() + seperator);
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

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;

                if (_serType == "cuscode")
                {
                    txtcuscode.Text = code;
                    txtcuscode_TextChanged(null, null);
                }
                else if (_serType == "itmcode")
                {
                    txtitmcd.Text = code;
                    txtitmcd_TextChanged(null, null);
                }

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

        protected void lbtnAddTarItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tmpDec = 0;
                #region Validate Hdr
                if (string.IsNullOrEmpty(txtcuscode.Text))
                {
                    txtcuscode.Focus(); DispMsg("Please select a Customer Code !"); return;
                }

                if (string.IsNullOrEmpty(txtitmcd.Text))
                {
                    txtitmcd.Focus(); DispMsg("Please select a Item Code !"); return;
                }

                if (string.IsNullOrEmpty(txtdplucd.Text))
                {
                    txtdplucd.Focus(); DispMsg("Please select a PLU Code !"); return;
                }
                #endregion

                List<mst_busentity_itm> _pludetailslist = Session["_plulist"] as List<mst_busentity_itm>;
                if (_pludetailslist == null)
                {
                    _pludetailslist = new List<mst_busentity_itm>();
                }
                var _sameitem = _pludetailslist.Where(a => a.mbii_itm_cd == txtitmcd.Text.ToString() && a.mbii_cd == txtcuscode.Text.ToString()).ToList();
                if (_sameitem != null)
                {
                    if (_sameitem.Count > 0)
                    {
                        DispMsg("PLU Codes has been already added for the item and customer", "N");
                        return;
                    }
                }

                _pludetailsob = new mst_busentity_itm();
                _pludetailsob.mbii_cd = txtcuscode.Text;
                _pludetailsob.mbii_itm_cd = txtitmcd.Text;
                _pludetailsob.mbii_plu_cd = txtdplucd.Text.ToUpper();
                _pludetailsob.mbii_tp = "C";
                _pludetailsob.mbii_cre_by = Session["UserID"].ToString();
                _pludetailsob.mbii_mod_by = Session["UserID"].ToString();
                _pludetailsob.mbii_act = chkActTarg.Checked ? 1 : 0;
                _pludetailsob.mbii_com = Session["UserCompanyCode"].ToString();
                _pludetailsob.mbii_cre_dt = DateTime.Now;
                _pludetailsob.mbii_mod_dt = DateTime.Now;
                _pludetailslist.Add(_pludetailsob);
                Session["_plulist"] = _pludetailslist;
                dgvTarget.DataSource = _pludetailslist;
                dgvTarget.DataBind();
                ClearPage();
                // _managerdetailsob.Mba_pty_tp = t

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }


        private void ClearPage()
        {
            try
            {

                _serData = new DataTable();
                _serType = "";
                //_calSerTp = "";

                _ava = false;
                _toolTip = string.Empty;
                _serPopShow = false;



                DateTime dt = DateTime.Now;
                _serData = new DataTable();
                _serType = "";

                _showExcelPop = false;
                _showProcPop = false;
                _toolTip = "";
                _filPath = "";
                _ava = false;


                txtcuscode.Text = ""; txtitmcd.Text = ""; txtdplucd.Text = "";
                txtcuscode.ToolTip = ""; txtitmcd.ToolTip = ""; txtdplucd.ToolTip = "";


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

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "cuscode")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.cuscode);
                    _serData = CHNLSVC.CommonSearch.GetCustomerCodeDetails(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "itmcode")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.itmcode);
                    _serData = CHNLSVC.CommonSearch.GetItemCodeDetails(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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

        private void ClearPageAll()
        {
            try
            {
                //ucLoactionSearch.PnlHeading.Visible = false;
                _serData = new DataTable();
                _serType = "";
                _ava = false;
                _toolTip = string.Empty;
                _serPopShow = false;



                DateTime dt = DateTime.Now;

                dgvTarget.DataSource = null;
                dgvTarget.DataBind();
                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _showExcelPop = false;
                _showProcPop = false;
                _toolTip = "";
                _filPath = "";
                _ava = false;

                Session["_plulist"] = null;


                txtcuscode.Text = ""; txtitmcd.Text = ""; txtdplucd.Text = "";
                txtcuscode.ToolTip = ""; txtitmcd.ToolTip = ""; txtdplucd.ToolTip = "";


            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
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
                    lblProcess.Visible = false;
                    lblProcess.Text = "";
                    lblerror.Visible = true;
                    lblerror.Text = "Please select the correct upload file path !";
                    _showProcPop = true;
                    popupExcProc.Show();
                    return;
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

        protected void lbtnExcelProcess_Click(object sender, EventArgs e)
        {
            try
            {
                List<mst_busentity_itm> _pludetailslist = new List<mst_busentity_itm>();
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
                _pludetailslist = new List<mst_busentity_itm>();
                List<mst_busentity_itm> tmpErrList = new List<mst_busentity_itm>();
                DateTime _tmpDec = DateTime.Now;
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    DataTable _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.itmcode);
                    _serData = CHNLSVC.CommonSearch.GetItemCodeDetails(_para, "CODE", _dtExData.Rows[i][2].ToString());
                    if (_serData != null)
                    {
                        if (_serData.Rows.Count == 0)
                        {
                            DispMsg("Invalid Item " + _dtExData.Rows[i][2].ToString(), "N");
                            return;
                        }
                    }
                    else
                    {
                        DispMsg("Invalid Item " + _dtExData.Rows[i][2].ToString(), "N");
                        return;
                    }


                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.cuscode);
                    _serData = CHNLSVC.CommonSearch.GetCustomerCodeDetails(_para, "CODE", _dtExData.Rows[i][1].ToString());
                    if (_serData != null)
                    {
                        if (_serData.Rows.Count == 0)
                        {
                            DispMsg("Invalid Customer " + _dtExData.Rows[i][1].ToString(), "N");
                            return;
                        }
                    }
                    else
                    {
                        DispMsg("Invalid Customer " + _dtExData.Rows[i][1].ToString(), "N");
                        return;
                    }


                    mst_busentity_itm _pludetailsob1 = new mst_busentity_itm();
                    _pludetailsob1.mbii_com = _dtExData.Rows[i][0].ToString();
                    _pludetailsob1.mbii_cd = _dtExData.Rows[i][1].ToString();
                    _pludetailsob1.mbii_itm_cd = _dtExData.Rows[i][2].ToString();
                    _pludetailsob1.mbii_plu_cd = _dtExData.Rows[i][3].ToString();
                    _pludetailsob1.mbii_act = 1;
                    _pludetailsob1.mbii_tp = "C";
                    _pludetailsob1.mbii_cre_by = Session["UserID"].ToString();
                    _pludetailsob1.mbii_cre_dt = DateTime.Now;
                    _pludetailsob1.mbii_mod_by = Session["UserID"].ToString();
                    _pludetailsob1.mbii_mod_dt = DateTime.Now;
                    _pludetailslist.Add(_pludetailsob1);
                }
                Session["_plulist"] = _pludetailslist;
                Int32 _rNo = 0;
                foreach (var itm in _pludetailslist)
                {

                    _rNo++;
                    if (!_defByList.Contains(itm.mbii_itm_cd))
                    {
                        tmpErrList.Add(itm); itm.tmp_err_desc = "Define by is invalid ! " + "Row No : " + _rNo; continue;
                    }
                    if (_defByList.Contains(itm.mbii_itm_cd))
                    {
                        #region Validate def code
                        if (itm.mbii_itm_cd == "itmcode")
                        {
                            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.itmcode);
                            _serData = CHNLSVC.CommonSearch.GetItemCodeDetails(_para, "CODE", itm.mbii_itm_cd);

                        }

                        DataAvailable(_serData, itm.mbii_itm_cd, "CODE", "Description");
                        if (!_ava)
                        {
                            tmpErrList.Add(itm); itm.tmp_err_desc = "Item code is invalid ! " + "Row No : " + _rNo; continue;
                        }
                        #endregion
                    }




                    _pludetailslist.Add(itm);


                }


                _prlforList = _pludetailslist;
                dgvTarget.DataSource = _pludetailslist;
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

        protected void lbtnTarDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblcus_cd = dr.FindControl("lblcus_cd") as Label;
                Label lblitm_cd = dr.FindControl("lblitm_cd") as Label;
                TextBox lblplu_cd = dr.FindControl("lblplu_cd") as TextBox;


                List<mst_busentity_itm> _pludetailslist = Session["_plulist"] as List<mst_busentity_itm>;
                var del = _pludetailslist.Where(c => c.mbii_cd == lblcus_cd.Text && c.mbii_itm_cd == lblitm_cd.Text && c.mbii_plu_cd == lblplu_cd.Text).FirstOrDefault();
                if (del != null)
                {
                    _pludetailslist.Remove(del);
                }

                Session["_plulist"] = _pludetailslist;
                dgvTarget.DataSource = _pludetailslist;

                dgvTarget.DataBind();

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcuscode.Text))
            {
                try
                {

                    List<mst_busentity_itm> _pludetailslist = CHNLSVC.Sales.Getcustomerpluupdate(Session["UserCompanyCode"].ToString(), txtcuscode.Text, txtitmcd.Text);
                    Session["_plulist"] = _pludetailslist;
                    dgvTarget.DataSource = _pludetailslist;
                    dgvTarget.DataBind();
                }
                catch (Exception ex)
                {
                    DispMsg(ex.Message, "E");
                }
            }

            else
            {

                DispMsg("Select Customer Code", "N");

                return;
            }
        }

        protected void btnsavelpdes_Click(object sender, EventArgs e)
        {
            try
            {
                mst_busentity_itm editlistob = new mst_busentity_itm();
                GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
                Label cuscd = drSelect.FindControl("lblcus_cd") as Label;
                Label itmcd = drSelect.FindControl("lblitm_cd") as Label;
                TextBox olucd = drSelect.FindControl("lblplu_cd") as TextBox;
                CheckBox chkact = drSelect.FindControl("lblplu_act") as CheckBox;
                int act = 0;
                if (chkact.Checked) act = 1;
                List<mst_busentity_itm> _pludetailslist = Session["_plulist"] as List<mst_busentity_itm>;

                //List<mst_busentity_itm> _pludetailslist = editlist;

                //CHECK ALREDY ADD AND UPDATE
                Int32 count = _pludetailslist.Where(a => a.mbii_cd == cuscd.Text.ToString() && a.mbii_itm_cd == itmcd.Text.ToString()).Count();
                if (count > 0)
                {
                    _pludetailslist.Where(w => w.mbii_cd == cuscd.Text.ToString() && w.mbii_itm_cd == itmcd.Text.ToString()).ToList().ForEach(i => i.mbii_plu_cd = olucd.Text.ToString());
                    _pludetailslist.Where(w => w.mbii_cd == cuscd.Text.ToString() && w.mbii_itm_cd == itmcd.Text.ToString()).ToList().ForEach(i => i.mbii_act = act);
                    olucd.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }


    }
}
