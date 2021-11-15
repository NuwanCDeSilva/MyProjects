using FF.BusinessObjects;
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

namespace FastForward.SCMWeb.View.Additional
{
    public partial class CustomClearEntryUpdate : BasePage
    {
        List<ImpCusdecHdr> _uploadData
        {
            get { if (Session["_uploadData"] != null) { return (List<ImpCusdecHdr>)Session["_uploadData"]; } else { return new List<ImpCusdecHdr>(); } }
            set { Session["_uploadData"] = value; }
        }
        
        bool _showExcelPop
        {
            get { if (Session["_showExcelPop"] != null) { return (bool)Session["_showExcelPop"]; } else { return false; } }
            set { Session["_showExcelPop"] = value; }
        }
        bool _showErrPop
        {
            get { if (Session["_showErrPop"] != null) { return (bool)Session["_showErrPop"]; } else { return false; } }
            set { Session["_showErrPop"] = value; }
        }
        string _filPath
        {
            get { if (Session["_filPath"] != null) { return (string)Session["_filPath"]; } else { return ""; } }
            set { Session["_filPath"] = value; }
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
        bool _serPopShow
        {
            get { if (Session["_serCuePopShow"] != null) { return (bool)Session["_serCuePopShow"]; } else { return false; } }
            set { Session["_serCuePopShow"] = value; }
        }
        string _para = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                if (_showExcelPop)
                {
                    popupExcel.Show();
                }
                else
                {
                    popupExcel.Hide();
                }
                if (_showExcelPop)
                {
                     popupExcel.Show();
                }
                else
                {
                    popupExcel.Hide();
                }
            }
        }
        private void ClearData()
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            _serData = new DataTable();
            _serType = "";
            _toolTip = "";
            _ava = false;
            _para = "";
            txtDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            dgvExcel.DataSource = new int[] { };
            dgvExcel.DataBind();
            _filPath = "";
            _showExcelPop = false;
            _uploadData = new List<ImpCusdecHdr>();
            _showErrPop = false;
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
                default:
                    break;
            }
            return paramsText.ToString();
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
        
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void txtEntryNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtEntryDesc.Text = "";
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtEntryNo.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtEntryNo.Text))
                {
                    ImpCusdecHdr _cuhHdr = CHNLSVC.Financial.GET_CUSTDECHDR_DOC(txtEntryNo.Text.ToUpper());
                    if (_cuhHdr != null)
                    {
                        if (_cuhHdr.CUH_STUS == "C")
                        {
                            txtEntryNo.Text = string.Empty;
                            txtEntryNo.Focus();
                            DispMsg("Document # already canceled !");
                            return;
                        }
                        else
                        {
                            txtEntryDesc.Text = _cuhHdr.CUH_CUSDEC_ENTRY_NO;
                            txtDate.Text = _cuhHdr.CUH_CUSDEC_ENTRY_DT.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        txtEntryNo.Text = string.Empty;
                        txtEntryNo.Focus();
                        DispMsg("Please enter valid document # !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeEntryNo_Click(object sender, EventArgs e)
        {
            try
            {
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

        protected void txtEntryDesc_TextChanged(object sender, EventArgs e)
        {

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
                    UploadData();
                    _showExcelPop = false;
                    popupExcel.Hide();
                  //  DispMsg("Excel file upload completed. Do you want to process ? ");
                }
                else
                {
                    DispMsg("Please select the correct upload file path !");
                }
                if (lblExcelUploadError.Visible == true)
                {
                    _showExcelPop = true;
                    popupExcel.Show();
                }
            }
            catch (Exception ex)
            {
                DispMsg( "Error Occurred :" + ex.Message,"E");
            }
        }

        protected void btnExcelDataUpload_Click(object sender, EventArgs e)
        {
            _uploadData = new List<ImpCusdecHdr>();
            dgvExcel.DataSource = new int[] { };
            dgvExcel.DataBind();
            _showExcelPop = true;
            popupExcel.Show();
        }

        protected void btnExcelDataProcess_Click(object sender, EventArgs e)
        {

        }

        protected void btnExcelDataUpdate_Click(object sender, EventArgs e)
        {
            bool b10160 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10160);
            if (!b10160)
            {
                DispMsg("Sorry, You have no permission !  (Advice: Required permission code : 10160 ) ");
                return;
            }
            if (_uploadData.Count == 0)
            {
                DispMsg("No data found !"); return;
            }
            if (_uploadData.Count > 0)
            {
                ValidateExcelData();
            }
        }
        private void UploadData()
        {
            _uploadData = new List<ImpCusdecHdr>();
            ImpCusdecHdr _impCus = new ImpCusdecHdr();
            string _error = "";
            if (string.IsNullOrEmpty(_filPath))
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "File Path Invalid Please Upload ";
                _showExcelPop = true;
                popupExcel.Show();
                return;
            }
            DataTable[] GetExecelTbl = LoadValidateData(_filPath, out _error);
            DateTime _tmpDt = new DateTime();
            if (GetExecelTbl!= null)
            {
                DataTable _dtExData = GetExecelTbl[0];
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    _impCus = new ImpCusdecHdr();
                    _impCus.CUH_DOC_NO = _dtExData.Rows[i][0].ToString();
                    _impCus.CUH_CUSDEC_ENTRY_NO = _dtExData.Rows[i][1].ToString();
                    _impCus.CUH_CUSDEC_ENTRY_DT =DateTime.TryParse(_dtExData.Rows[i][2].ToString(), out _tmpDt)?Convert.ToDateTime(_dtExData.Rows[i][2].ToString()):DateTime.MinValue;
                    _impCus.CUH_MOD_BY = Session["UserID"].ToString();
                    _impCus.CUH_MOD_SESSION = Session["SessionID"].ToString();
                    if (string.IsNullOrEmpty(_impCus.CUH_DOC_NO) && string.IsNullOrEmpty(_impCus.CUH_CUSDEC_ENTRY_NO) && _impCus.CUH_CUSDEC_ENTRY_DT == DateTime.MinValue)
                    {
                       
                    }
                    else
                    {
                        _uploadData.Add(_impCus);
                    }
                }
            }
            dgvExcel.DataSource = new int[] { };
            if (_uploadData.Count > 0)
            {
                dgvExcel.DataSource = _uploadData;
            }
            dgvExcel.DataBind();
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
                    cmdExcel.CommandText = "SELECT F1,F2,F3 From [" + SheetName + "]";
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
        protected void lbtnUpdateEntry_Click(object sender, EventArgs e)
        {
            try
            {
                bool b10160 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10160);
                if (!b10160)
                {
                    DispMsg("Sorry, You have no permission !  (Advice: Required permission code : 10160 ) ");
                    return;
                }
                DateTime _tmpDt = new DateTime();
                if (string.IsNullOrEmpty(txtEntryNo.Text))
                {
                    DispMsg("Please enter document # ! "); return;
                }
                if (string.IsNullOrEmpty(txtEntryDesc.Text))
                {
                    DispMsg("Please enter Entry # ! "); return;
                }
                if (txtEntryDesc.Text=="N/A")
                {
                    DispMsg("Please enter valid Entry # ! "); return;
                }
                DateTime _dtEntry = DateTime.MinValue;
                _dtEntry = DateTime.TryParse(txtDate.Text, out _tmpDt) ? Convert.ToDateTime(txtDate.Text) : DateTime.MinValue;
                if (_dtEntry == DateTime.MinValue)
                {
                    DispMsg("Entry date cannot exceed the current date !"); return;
                }
                if (_dtEntry > DateTime.Today)
                {
                    DispMsg(" Entry date cannot exceed the current date !"); return;
                }
                string _dtErr = "";
                bool _isValid = ChkDateIsValid(_dtEntry, out _dtErr);
                if (!_isValid)
                {
                    DispMsg(_dtErr); return;
                }
                //Sat 19/Aug/2017 12:55 PM mail by wazeem

                //List<ImpCusdecHdr> _tmpHdrList = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_CUSTDEC_ENTRY_NO(txtEntryDesc.Text);
                //if (_tmpHdrList.Count > 1)
                //{
                //    DispMsg("Entry number is already available !"); return;
                //}
                //if (_tmpHdrList.Count == 1)
                //{
                //    if (_tmpHdrList[0].CUH_DOC_NO != txtEntryNo.Text.ToUpper())
                //    {
                //        DispMsg("Entry number is already available !"); return;
                //    }
                //}
                List<ImpCusdecHdr> _tmpSaveList = new List<ImpCusdecHdr>();
                ImpCusdecHdr _ich = new ImpCusdecHdr();
                _ich.CUH_DOC_NO = txtEntryNo.Text.ToUpper();
                _ich.CUH_CUSDEC_ENTRY_NO = txtEntryDesc.Text.ToUpper();
                _ich.CUH_CUSDEC_ENTRY_DT = _dtEntry;
                _ich.CUH_MOD_BY = Session["UserID"].ToString();
                _ich.CUH_MOD_SESSION = Session["SessionID"].ToString();
                _tmpSaveList.Add(_ich);
                string err = "";
                Int32 _res = CHNLSVC.Financial.UpdateCusdecEntryNoDate(_tmpSaveList,out  err);
                if (string.IsNullOrEmpty(err))
                {
                    DispMsg("Successfully updated !","S");
                    txtEntryNo.Text = "";
                    txtEntryDesc.Text = "";
                    txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                }
                else
                {
                    DispMsg(err, "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void ValidateExcelData()
        {
            List<ImpCusdecHdr> _errList = new List<ImpCusdecHdr>();
            ImpCusdecHdr _err= new ImpCusdecHdr();
            List<ImpCusdecHdr> _tmpHdrList = new List<ImpCusdecHdr>();
            foreach (var item in _uploadData)
            {
                _err= new ImpCusdecHdr();
                _err.CUH_DOC_NO = item.CUH_DOC_NO;
                _err.CUH_CUSDEC_ENTRY_NO = item.CUH_CUSDEC_ENTRY_NO;
                _err.CUH_CUSDEC_ENTRY_DT = item.CUH_CUSDEC_ENTRY_DT;
                _err.Tmp_ex_err = "";
                string _erMsg="";
                #region validate doc #
                if (string.IsNullOrEmpty(item.CUH_DOC_NO))
                {
                    _erMsg =  "Please enter document #";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                   // _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                if (string.IsNullOrEmpty(item.CUH_CUSDEC_ENTRY_NO))
                {
                    _erMsg = "Please enter Entry # !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                if (item.CUH_CUSDEC_ENTRY_NO == "N/A")
                {
                    _erMsg = "Please enter valid Entry # !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                ImpCusdecHdr _cuhHdr = CHNLSVC.Financial.GET_CUSTDECHDR_DOC(item.CUH_DOC_NO);
                if (_cuhHdr == null)
                {
                    _erMsg = "Invalid document # !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                if (_cuhHdr.CUH_STUS == "C")
                {
                    _erMsg = "Document # already canceled !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                if (item.CUH_CUSDEC_ENTRY_DT == DateTime.MinValue)
                {
                    _erMsg = "Invalid entry date !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                if (item.CUH_CUSDEC_ENTRY_DT > DateTime.Today)
                {
                    _erMsg = "Entry date cannot exceed the current date !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                string _dtErr = "";
                bool _isValid = ChkDateIsValid(item.CUH_CUSDEC_ENTRY_DT, out _dtErr);
                if (!_isValid)
                {
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _dtErr : _err.Tmp_ex_err;
                }
                var _dupDocNo = _uploadData.Where(c => c.CUH_DOC_NO==item.CUH_DOC_NO).ToList();
                if (_dupDocNo.Count > 1)
                {
                    _erMsg = "Document # duplicated !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                var _dupEntryNo = _uploadData.Where(c => c.CUH_CUSDEC_ENTRY_NO == item.CUH_CUSDEC_ENTRY_NO).ToList();
                if (_dupEntryNo.Count > 1)
                {
                    _erMsg = "Entry # duplicated !";
                    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                }
                if (!string.IsNullOrEmpty(_err.CUH_CUSDEC_ENTRY_NO))
                {
                    if (_err.CUH_CUSDEC_ENTRY_NO !="N/A")
                    {
                        //mail by Sat 19/Aug/2017 12:55 PM wazeem

                        //_tmpHdrList = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_CUSTDEC_ENTRY_NO(_err.CUH_CUSDEC_ENTRY_NO);
                        //if (_tmpHdrList.Count > 1)
                        //{
                        //    _erMsg = "Entry number is already available !";
                        //    _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                        //    //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                        //}
                        //if (_tmpHdrList.Count == 1)
                        //{
                        //    if (_tmpHdrList[0].CUH_DOC_NO != _err.CUH_DOC_NO)
                        //    {
                        //        _erMsg = "Entry number is already available !";
                        //        _err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err;
                        //        //_err.Tmp_ex_err = string.IsNullOrEmpty(_err.Tmp_ex_err) ? _erMsg : _err.Tmp_ex_err + "," + _erMsg;
                        //    }
                        //}
                    }
                }
                
                if (!string.IsNullOrEmpty(_err.Tmp_ex_err))
                {
                    _errList.Add(_err);
                }
                #endregion
            }
            if (_errList.Count > 0)
            {
                dgvError.DataSource = _errList;
                dgvError.DataBind();
                _showErrPop = true;
                popupErro.Show();
            }
            else
            {
                string err = "";
                Int32 _res = CHNLSVC.Financial.UpdateCusdecEntryNoDate(_uploadData, out  err);
                if (string.IsNullOrEmpty(err))
                {
                    DispMsg("Successfully updated !", "S");
                    txtEntryNo.Text = "";
                    txtEntryDesc.Text = "";
                    txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    _uploadData = new List<ImpCusdecHdr>();
                    dgvExcel.DataSource = new int[] { };
                    dgvExcel.DataBind();
                }
                else
                {
                    DispMsg(err, "E");
                }
            }
        }

        protected void lbtnExcClose_Click(object sender, EventArgs e)
        {
            _showErrPop = true;
            popupErro.Hide();
        }
        private Boolean ChkDateIsValid(DateTime _dt,out string _err)
        {
            bool _val = false;
            _err="";
            HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "UPCLRENTRY", DateTime.Now.Date);
            if (_sysPara != null)
            {
                Int32 _noOfDays = Convert.ToInt32(_sysPara.Hsy_val);
                DateTime _tmpDt = DateTime.Today.AddDays(-_noOfDays);
                if (_dt < _tmpDt)
                {
                    _err = "Entry date must be within " + _tmpDt.ToString("dd/MMM/yyyy")+" to "+ DateTime.Today.ToString("dd/MMM/yyyy");
                }
                else
                {
                    _val = true;
                }
            }
            return _val;
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}