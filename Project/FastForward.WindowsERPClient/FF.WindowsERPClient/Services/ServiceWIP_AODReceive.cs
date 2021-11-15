using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_AODReceive : Base
    {
        private string JobNumber = string.Empty;
        private Int32 jobLineNum = -11;
        private MasterItem _masterItem = null;
        private const string InvoiceBackDateName = "INTERCOMPANYINWARDENTRY";
        private string OutwardNo = string.Empty;
        private string OutwardType = string.Empty;
        private string OutwardCompany = string.Empty;
        private string OutwardLocation = string.Empty;
        private Int32 UserSeqNo = 0;
        private List<ReptPickSerials> PickSerialsList = null;
        private string hdnAllowBin = "0";
        private DateTime? hdnOutwarddate = null;

        private string _supplier = string.Empty;
        private string _subdoc = string.Empty;

        public bool IsGitAvailable = false; //By akila 2017/05/08 - if git items available, service not allow to close
        public List<ReptPickSerials> _selectedItems = new List<ReptPickSerials>();

        public ServiceWIP_AODReceive(string job, Int32 jobLine)
        {
            JobNumber = job;
            jobLineNum = jobLine;
            InitializeComponent();
            try
            {
                gvPending.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
                gvItem.AutoGenerateColumns = false;
                InitializeForm();

                dgvItemsD13.Visible = false;

                dgvItemsD13.Size = new System.Drawing.Size(779, 452);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #region Events

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItemsD13.Visible = false;
                SearchRequest();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void gvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvPending.RowCount > 0)
                {
                    int _rowIndex = e.RowIndex;
                    if (_rowIndex != -1)
                        BindSelectedOutwardNo(_rowIndex);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void gvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvSerial.RowCount > 0)
                {
                    int _rowIndex = e.RowIndex;
                    if (_rowIndex != -1)
                    {
                        if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            OnRemoveFromSerialGrid(_rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckServerDateTime() == false)
            {
                return;
            }
            if (MessageBox.Show("Do you need to process?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (IsBackDateOk() == false)
                return;
            if (gvItem.RowCount <= 0)
            {
                MessageBox.Show("Please select the items", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gvSerial.RowCount <= 0)
            {
                MessageBox.Show("Please select the serials", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(BaseCls.GlbUserComCode))
            {
                MessageBox.Show("Session expired! Please re-login to system.");
                txtDate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MessageBox.Show("Please select the date");
                txtDate.Focus();
                return;
            }
            if (IsDate(txtDate.Text, DateTimeStyles.None) == false)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                MessageBox.Show("Invalid Date.");
                return;
            }
            //if (string.IsNullOrEmpty(lblIssuedDocNo.Text))
            //{
            //    lblIssuedDocNo.Text = "N/A";
            //}
            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                txtRemarks.Text = "N/A";
            }
            //if (string.IsNullOrEmpty(txtVehicle.Text))
            //{
            //    txtVehicle.Text = "N/A";
            //}
            if (DateTime.Compare(Convert.ToDateTime(hdnOutwarddate.Value.ToString()).Date, Convert.ToDateTime(txtDate.Text).Date) > 0)
            {
                MessageBox.Show("Inward entry date should be greater than or equal to outward entry date.");
                return;
            }

            InventoryHeader invHdr = new InventoryHeader();

            invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
            invHdr.Ith_com = BaseCls.GlbUserComCode;
            invHdr.Ith_oth_docno = OutwardNo;
            invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
            invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
            invHdr.Ith_doc_tp = "AOD";
            invHdr.Ith_cate_tp = "SERVICE";
            invHdr.Ith_sub_tp = "NOR";

            invHdr.Ith_is_manual = false; invHdr.Ith_stus = "A";
            invHdr.Ith_cre_by = BaseCls.GlbUserID; invHdr.Ith_mod_by = BaseCls.GlbUserID;
            invHdr.Ith_direct = true; invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
            invHdr.Ith_manual_ref = "N/A"; invHdr.Ith_remarks = txtRemarks.Text;
            invHdr.Ith_vehi_no = string.Empty;
            invHdr.Ith_bus_entity = string.Empty;
            invHdr.Ith_oth_com = BaseCls.GlbUserComCode;
            if (!String.IsNullOrEmpty(gvPending.SelectedRows[0].Cells["pen_issueLocation"].Value.ToString()))
            {
                invHdr.Ith_oth_loc = gvPending.SelectedRows[0].Cells["pen_issueLocation"].Value.ToString();
            }
            invHdr.Ith_sub_docno = JobNumber;
            invHdr.Ith_job_no = JobNumber;
            invHdr.Ith_isjobbase = true;
            invHdr.Ith_pc = BaseCls.GlbUserDefProf; Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, BaseCls.GlbUserComCode, OutwardNo, 1);
            if (PickSerialsList == null)
            { MessageBox.Show("No item found!"); return; }
            btnSave.Enabled = false;
            //Add by Chamal 23-May-2014
            int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(invHdr.Ith_oth_com, invHdr.Ith_com, invHdr.Ith_doc_tp, OutwardNo, invHdr.Ith_doc_date.Date, BaseCls.GlbUserID);

            PickSerialsList.ForEach(x => x.Tus_doc_dt = invHdr.Ith_doc_date.Date);

            // PickSerialsList.ForEach(x => {x.Tus_doc_dt = invHdr.Ith_doc_date.Date};);

            List<ReptPickSerialsSub> reptPickSerials_SubList = new List<ReptPickSerialsSub>();
            reptPickSerials_SubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(user_seq_num, OutwardType);
            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
            masterAutoNum.Aut_cate_cd = BaseCls.GlbUserDefLoca; masterAutoNum.Aut_cate_tp = "LOC"; masterAutoNum.Aut_direction = 1;
            masterAutoNum.Aut_modify_dt = null; masterAutoNum.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            string documntNo = string.Empty;
            Int32 result = -99;
            bool _isok = IsUserProcessed(user_seq_num, OutwardNo);
            if (_isok) return;
            try
            {
                #region Check receving serials are duplicating :: Chamal 08-May-2014

                string _err = string.Empty;
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(invHdr.Ith_com, invHdr.Ith_loc, PickSerialsList, out _err) <= 0)
                {
                    MessageBox.Show(_err.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion Check receving serials are duplicating :: Chamal 08-May-2014

                this.Cursor = Cursors.WaitCursor;
                if (OutwardType == "AOD")
                {
                    masterAutoNum.Aut_moduleid = "AOD";
                    masterAutoNum.Aut_start_char = "AOD";
                    result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);
                    IsGitAvailable = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

            if (result != -99 && result > 0)
            {
                PickSerialsList.ForEach(x => x.Tus_com = Convert.ToString(gvPending.SelectedRows[0].Cells["pen_issueCompany"].Value));
                string _refdc = Convert.ToString(gvPending.SelectedRows[0].Cells["pen_docno"].Value);
                CHNLSVC.Inventory.SetOffRefDocumentSerial(PickSerialsList, _refdc);
                string Msg = "AOD Receipt Successfully Saved! Document No. : " + documntNo + "";
                btnSave.Enabled = true;
                MessageBox.Show(Msg, "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                    Controls[0].Dispose();
                InitializeComponent();
                InitializeForm();
                //lblIssueLocDesc.Text = string.Empty;
                this.Cursor = Cursors.Default;
            }
            else
            {
                btnSave.Enabled = true;
                MessageBox.Show(documntNo, "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0)
                Controls[0].Dispose();
            InitializeComponent();
            InitializeForm();
            BackDatePermission();
            btnSave.Enabled = true;
            //lblIssueLocDesc.Text = "";
            this.Cursor = Cursors.Default;
            gvPending.AutoGenerateColumns = false;
            gvSerial.AutoGenerateColumns = false;

            dgvItemsD13.Visible = false;
        }

        private void Service_AODReceive_Load(object sender, EventArgs e)
        {
            gvItem.AutoGenerateColumns = false;
            dgvItemsD13.AutoGenerateColumns = false;
            dgvItemsD13.Size = new System.Drawing.Size(779, 452);
            dgvItemsD13.Visible = false;
            try
            {
                BackDatePermission();
                BtnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Events

        #region method

        private void BindOutwardListGridData()
        {
            IsGitAvailable = false; //Add by akila 2017/06/17
            try
            {
                InventoryHeader _inventoryRequest = new InventoryHeader();
                _inventoryRequest.Ith_oth_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Ith_doc_tp = "AOD";
                _inventoryRequest.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                _inventoryRequest.FromDate = "01-01-1900";
                _inventoryRequest.ToDate = "31-12-2999";

                DataTable _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                if (_table.Rows.Count <= 0)
                {
                    var _tblItems = from dr in _table.AsEnumerable()
                                    group dr by new
                                        {
                                            ith_doc_no = dr["ith_doc_no"],
                                            ith_doc_date = dr["ith_doc_date"],
                                            ith_doc_tp = dr["ith_doc_tp"],
                                            ith_manual_ref = dr["ith_manual_ref"],
                                            ith_com = dr["ith_com"],
                                            ith_loc = dr["ith_loc"],
                                            ith_bus_entity = dr["ith_bus_entity"],
                                            ith_sub_docno = dr["ith_sub_docno"]
                                        }
                                        into item
                                        select new
                                            {
                                                ith_doc_no = item.Key.ith_doc_no,
                                                ith_doc_date = item.Key.ith_doc_date,
                                                ith_doc_tp = item.Key.ith_doc_tp,
                                                ith_manual_ref = item.Key.ith_manual_ref,
                                                ith_com = item.Key.ith_com,
                                                ith_loc = item.Key.ith_loc,
                                                ith_bus_entity = item.Key.ith_bus_entity,
                                                ith_sub_docno = item.Key.ith_sub_docno
                                            };
                    gvPending.DataSource = _tblItems;
                }
                else
                {
                    DataTable dtTemp = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                    if (dtTemp.Select("ith_direct = 0 AND ith_job_no='" + JobNumber + "'").Length > 0)
                    {
                        DataTable dtNew = dtTemp.Select("ith_direct = 0 AND ith_job_no='" + JobNumber + "'").CopyToDataTable();
                        if (dtNew.Rows.Count > 0)
                        {
                            gvPending.DataSource = dtNew;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                if (gvPending.Rows.Count > 0) { IsGitAvailable = true; } else { IsGitAvailable = false; }

                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _table = new DataTable();
                _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, OutwardType);
                if (_table.Rows.Count <= 0)
                {
                    gvSerial.DataSource = _table;
                    var _tblItems = from dr in _table.AsEnumerable() group dr by new { Tus_itm_cd = dr["Tus_itm_cd"], Tus_itm_desc = dr["Tus_itm_desc"], Tus_itm_model = dr["Tus_itm_model"], Tus_itm_stus = dr["Tus_itm_stus"] } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => 0) };
                    gvItem.DataSource = _tblItems;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }

        private void InitializeForm()
        {
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            OutwardType = string.Empty;
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (_mstLoc != null)
            {
                if (_mstLoc.Ml_allow_bin == false)
                {
                    hdnAllowBin = "0";
                }
                else
                {
                    hdnAllowBin = "1";
                }
            }
            String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);

            if (!string.IsNullOrEmpty(_defBin))
            {
                hdnAllowBin = _defBin;
            }
            else
            {
                MessageBox.Show("Default Bin Not Setup For Location : " + BaseCls.GlbUserDefLoca, "Default Bin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            BindOutwardListGridData();
            hdnOutwarddate = null;

            dgvItemsD13.Size = new System.Drawing.Size(779, 452);
            dgvItemsD13.Visible = false;
        }

        protected void SearchRequest()
        {
            BindOutwardListGridData();
        }

        private string _dono = string.Empty;

        protected void BindOutwardItems()
        {
            try
            {
                _dono = string.Empty; PickSerialsList = null;
                ReptPickHeader _reptPickHdr = new ReptPickHeader(); Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, OutwardNo);
                UserSeqNo = _seq; _reptPickHdr.Tuh_direct = true; _reptPickHdr.Tuh_doc_no = OutwardNo; _reptPickHdr.Tuh_doc_tp = OutwardType;
                _reptPickHdr.Tuh_ischek_itmstus = false; _reptPickHdr.Tuh_ischek_reqqty = true; _reptPickHdr.Tuh_ischek_simitm = false; _reptPickHdr.Tuh_session_id = BaseCls.GlbUserSessionID;
                _reptPickHdr.Tuh_usr_com = BaseCls.GlbUserComCode; _reptPickHdr.Tuh_usr_id = BaseCls.GlbUserID; _reptPickHdr.Tuh_usrseq_no = _seq; string _unavailableitemlist = string.Empty;
                List<ReptPickSerials> PickSerialsAll = CHNLSVC.Inventory.GetOutwarditems(BaseCls.GlbUserDefLoca, hdnAllowBin, _reptPickHdr, out _unavailableitemlist);
                List<ReptPickSerials> PickSerials = new List<ReptPickSerials>();
                PickSerials = PickSerialsAll.FindAll(x => x.Tus_job_line == jobLineNum);

                if (!string.IsNullOrEmpty(_unavailableitemlist))
                { btnSave.Enabled = false; MessageBox.Show("Following item does not setup in the current system.\nItem List " + _unavailableitemlist, "Unavailable Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else btnSave.Enabled = true;
                if (PickSerials != null)
                {
                    if (Convert.ToString(gvPending.SelectedRows[0].Cells["pen_Type"].Value) == "PRN")
                    {
                        DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                        int _adhocline = 1;
                        foreach (ReptPickSerials _pik in PickSerials)
                        {
                            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_pik.Tus_ser_id);
                            if (_master != null && !string.IsNullOrEmpty(_master.Irsm_com))
                            {
                                _pik.Tus_new_remarks = _master.Irsm_anal_2;
                                _dono = _master.Irsm_anal_2; DataTable _tbl = CHNLSVC.Inventory.GetPOLine(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dono, _pik.Tus_ser_id);
                                if (_tbl != null && _tbl.Rows.Count > 0)
                                { _pik.Tus_itm_stus = _tbl.Rows[0].Field<string>("itb_itm_stus"); _pik.Tus_new_status = Convert.ToString(_tbl.Rows[0].Field<Int32>("itb_base_refline")); _pik.Tus_base_itm_line = _tbl.Rows[0].Field<Int32>("itb_base_refline"); }
                                else
                                { var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_scm2_imp")).ToList(); _pik.Tus_itm_stus = Convert.ToString(_lp[0]); _pik.Tus_new_status = Convert.ToString(_adhocline); _pik.Tus_base_itm_line = _adhocline; _adhocline += 1; }
                            }
                        }
                    }
                    else if (Convert.ToString(gvPending.SelectedRows[0].Cells["pen_Type"].Value) == "DO")
                    {
                        DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                        //int _adhocline = 1;
                        foreach (ReptPickSerials _pik in PickSerials)
                        {
                            var _lp = _status.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_lp_cd")).ToList();

                            _pik.Tus_itm_stus = Convert.ToString(_lp[0]);
                            //_pik.Tus_new_status = Convert.ToString(_adhocline);
                            //_pik.Tus_base_itm_line = _adhocline; _adhocline += 1;
                        }
                    }
                    var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                    BindingSource _sourceItem = new BindingSource(); BindingSource _sourceSerial = new BindingSource();
                    _sourceItem.DataSource = _tblItems;
                    gvItem.DataSource = _sourceItem;
                    _sourceSerial.DataSource = PickSerials; gvSerial.DataSource = _sourceSerial;
                    PickSerialsList = PickSerials;

                    //Add by akila 2017/05/08
                    if (_tblItems != null)
                    {
                        _selectedItems = new List<ReptPickSerials>();
                        foreach (var tmpItem in _tblItems)
                        {
                            ReptPickSerials _item = new ReptPickSerials();
                            _item.Tus_itm_cd = tmpItem.Tus_itm_cd;
                            _item.Tus_itm_desc = tmpItem.Tus_itm_desc;
                            _item.Tus_itm_model = tmpItem.Tus_itm_model;
                            _item.Tus_itm_stus = tmpItem.Tus_itm_stus;
                            _item.Tus_qty = tmpItem.Tus_qty;
                            _selectedItems.Add(_item);
                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally
            { this.Cursor = Cursors.Default; }
        }

        protected void BindSelectedOutwardNo(int _rowIndex)
        {
            try
            {
                _supplier = string.Empty; _subdoc = string.Empty;

                this.Cursor = Cursors.WaitCursor;

                OutwardNo = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_docno"].Value);

                DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(BaseCls.GlbUserComCode, OutwardNo);

                if (_headerchk != null && _headerchk.Rows.Count > 0)
                {
                    string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                    string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));
                    if (!string.IsNullOrEmpty(_headerUser))
                        if (BaseCls.GlbUserID.Trim() != _headerUser.Trim())
                        {
                            MessageBox.Show("Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate, "Scanned Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                }

                hdnOutwarddate = Convert.ToDateTime(gvPending.Rows[_rowIndex].Cells["pen_Date"].Value);

                OutwardType = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_Type"].Value);

                //lblIssuedDocNo.Text = OutwardNo;

                //lblIssedCompany.Text = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_issueCompany"].Value);

                //lblIssuedLocation.Text = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_issueLocation"].Value);

                _supplier = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_supcode"].Value);

                _subdoc = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_subdoc"].Value);

                //DataTable _tbl = null;

                //if (!string.IsNullOrEmpty(lblIssuedLocation.Text) && !string.IsNullOrEmpty(lblIssedCompany.Text))
                //    _tbl = CHNLSVC.Inventory.Get_location_by_code(lblIssedCompany.Text.Trim(), lblIssuedLocation.Text.Trim());

                //if (_tbl != null && _tbl.Rows.Count > 0)
                //    lblIssueLocDesc.Text = _tbl.Rows[0].Field<string>("ml_loc_desc");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BindOutwardItems();
            }
        }

        protected void BindPickSerials()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, OutwardType);
                PickSerialsList = _list; if (PickSerialsList != null) if (PickSerialsList.Count > 0)
                    { var _tblItems = (from _pickSerials in PickSerialsList group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList(); BindingSource _sourceItem = new BindingSource(); BindingSource _sourceSerial = new BindingSource(); _sourceItem.DataSource = _tblItems; gvItem.DataSource = _sourceItem; _sourceSerial.DataSource = PickSerialsList; gvSerial.DataSource = _sourceSerial; }
                    else
                    { PickSerialsList = new List<ReptPickSerials>(); BindingSource _sourceItem = new BindingSource(); BindingSource _sourceSerial = new BindingSource(); _sourceItem.DataSource = PickSerialsList; gvItem.DataSource = _sourceItem; _sourceSerial.DataSource = PickSerialsList; gvSerial.DataSource = _sourceSerial; }
                else { PickSerialsList = new List<ReptPickSerials>(); BindingSource _sourceItem = new BindingSource(); BindingSource _sourceSerial = new BindingSource(); _sourceItem.DataSource = PickSerialsList; gvItem.DataSource = _sourceItem; _sourceSerial.DataSource = PickSerialsList; gvSerial.DataSource = _sourceSerial; }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        protected void OnRemoveFromSerialGrid(int _rowIndex)
        {
            try
            {
                Int32 _serialID = -1;
                if (OutwardNo == null)
                {
                    MessageBox.Show("(R)Select the outward document!", "Outward Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Cursor = Cursors.WaitCursor; int row_id = _rowIndex;
                if (string.IsNullOrEmpty(gvSerial.Rows[_rowIndex].Cells["ser_Bin"].Value.ToString()))
                    return;
                string _item = Convert.ToString(gvSerial.Rows[_rowIndex].Cells["ser_Item"].Value);
                _serialID = Convert.ToInt32(gvSerial.Rows[_rowIndex].Cells["ser_serialid"].Value);
                string _bin = Convert.ToString(gvSerial.Rows[_rowIndex].Cells["ser_Bin"].Value);

                if (_serialID == 0)
                {
                    MessageBox.Show("Can not remove none-serialized items!", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                //modify Rukshan 05/oct/2015 add two parameters
                CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID),null,null);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                BindPickSerials();
            }
        }

        private bool IsBackDateOk()
        {
            bool _isOK = true; bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus(); _isOK = false; return _isOK;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    _isOK = false;
                    return _isOK;
                }
            }
            return _isOK;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion method

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            dgvItemsD13.AutoGenerateColumns = false;
            dgvItemsD13.Visible = true;
            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
//            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ViewStockItems(BaseCls.GlbUserComCode, JobNumber, jobLineNum, "", BaseCls.GlbUserDefProf);
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ViewStockItems(BaseCls.GlbUserComCode, JobNumber, jobLineNum, "", BaseCls.GlbUserDefLoca);
            dgvItemsD13.DataSource = stockReturnItems;
            modifyGrid();

            if (stockReturnItems.Count == 1)
            {
                gvPending_CellClick(stockReturnItems, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void modifyGrid()
        {
            if (dgvItemsD13.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItemsD13.Rows.Count; i++)
                {
                    if (dgvItemsD13.Rows[i].Cells["serial_no"].Value.ToString() == "N/A")
                    {
                        dgvItemsD13.Rows[i].Cells["Qty"].ReadOnly = false;
                    }
                    else
                    {
                        dgvItemsD13.Rows[i].Cells["Qty"].ReadOnly = true;
                    }
                }
            }
        }
    }
}