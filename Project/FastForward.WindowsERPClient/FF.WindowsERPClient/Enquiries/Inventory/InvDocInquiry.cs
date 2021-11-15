using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;
using System.Globalization;

//kapila

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class InvDocInquiry : FF.WindowsERPClient.Base
    {
        private MasterItem _masterItem = null;
        const string InvoiceBackDateName = "InvDocInquiry";
        private string OutwardNo = string.Empty;
        private string OutwardType = string.Empty;
        private string OutwardCompany = string.Empty;
        private string OutwardLocation = string.Empty;
        private Int32 UserSeqNo = 0;
        private List<ReptPickSerials> PickSerialsList = null;
        private string hdnAllowBin = "0";
        private DateTime? hdnOutwarddate = null;
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { this.Close(); }
        }
        private void BindRequestTypesDDLData()
        {
            ddlType.Items.Clear();
            List<MasterType> _masterType = CHNLSVC.General.GetOutwardTypes();
            _masterType.Add(new MasterType { Mtp_cd = "-1", Mtp_desc = "" });
            BindingSource _source = new BindingSource();
            if (_masterType != null) if (_masterType.Count > 0)
                { var _lst = _masterType.OrderBy(items => items.Mtp_desc).ToList(); _source.DataSource = _lst; ddlType.DisplayMember = "Mtp_desc"; ddlType.ValueMember = "Mtp_cd"; ddlType.DataSource = _source.DataSource; this.Cursor = Cursors.Default; return; }
            ddlType.DataSource = _source.Current;
        }
        private void BindOutwardListGridData()
        {
            try
            {
                InventoryHeader _inventoryRequest = new InventoryHeader();
                _inventoryRequest.Ith_oth_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Ith_doc_tp = ddlType.SelectedValue.ToString() == "-1" ? string.Empty : ddlType.SelectedValue.ToString();
                _inventoryRequest.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                _inventoryRequest.FromDate = !string.IsNullOrEmpty(txtFrom.Text.Trim()) ? txtFrom.Text : string.Empty;
                _inventoryRequest.ToDate = !string.IsNullOrEmpty(txtTo.Text.Trim()) ? txtTo.Text : string.Empty;
                if (!string.IsNullOrEmpty(_inventoryRequest.Ith_doc_tp))
                { if (_inventoryRequest.Ith_doc_tp == "AOD-") _inventoryRequest.Ith_doc_tp = "AOD"; }
                DataTable _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                if (_table.Rows.Count <= 0)
                { var _tblItems = from dr in _table.AsEnumerable() group dr by new { ith_doc_no = dr["ith_doc_no"], ith_doc_date = dr["ith_doc_date"], ith_doc_tp = dr["ith_doc_tp"], ith_manual_ref = dr["ith_manual_ref"], ith_com = dr["ith_com"], ith_loc = dr["ith_loc"], ith_bus_entity = dr["ith_bus_entity"], ith_sub_docno = dr["ith_sub_docno"] } into item select new { ith_doc_no = item.Key.ith_doc_no, ith_doc_date = item.Key.ith_doc_date, ith_doc_tp = item.Key.ith_doc_tp, ith_manual_ref = item.Key.ith_manual_ref, ith_com = item.Key.ith_com, ith_loc = item.Key.ith_loc, ith_bus_entity = item.Key.ith_bus_entity, ith_sub_docno = item.Key.ith_sub_docno }; gvPending.DataSource = _tblItems; }
                else
                { gvPending.DataSource = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest); }
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _table = new DataTable();
                _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, OutwardType);
                if (_table.Rows.Count <= 0)
                { gvSerial.DataSource = _table; var _tblItems = from dr in _table.AsEnumerable() group dr by new { Tus_itm_cd = dr["Tus_itm_cd"], Tus_itm_desc = dr["Tus_itm_desc"], Tus_itm_model = dr["Tus_itm_model"], Tus_itm_stus = dr["Tus_itm_stus"] } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => 0) }; gvItem.DataSource = _tblItems; }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally
            { this.Cursor = Cursors.Default; }
        }
        private void BackDatePermission()
        { bool _allowCurrentTrans = false; IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans); }
        private void InitializeForm()
        {
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            OutwardType = string.Empty;
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (_mstLoc != null)
            { if (_mstLoc.Ml_allow_bin == false) { hdnAllowBin = "0"; } else { hdnAllowBin = "1"; } }
            String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (!string.IsNullOrEmpty(_defBin)) { hdnAllowBin = _defBin; }
            else { MessageBox.Show("Default Bin Not Setup For Location : " + BaseCls.GlbUserDefLoca, "Default Bin", MessageBoxButtons.OK, MessageBoxIcon.Information); this.Close(); return; }
            BindRequestTypesDDLData();
            BindOutwardListGridData();
            hdnOutwarddate = null;
            txtFrom.Value = Convert.ToDateTime(DateTime.Today.Date.AddMonths(-1));
        }
        public InvDocInquiry()
        {
            InitializeComponent();
            try
            { gvPending.AutoGenerateColumns = false; gvItem.AutoGenerateColumns = false; gvSerial.AutoGenerateColumns = false; InitializeForm(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) txtTo.Focus(); }
        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) btnDocSearch.Focus(); }
        private void ddlType_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) txtDate.Focus(); }
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) txtVehicle.Focus(); }
        private void txtVehicle_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter)  txtRemarks.Focus(); }
        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter)  btnSave.Select(); }
        protected void SearchRequest()
        {
            if (!string.IsNullOrEmpty(txtFrom.Text))
            { if (string.IsNullOrEmpty(txtTo.Text)) { MessageBox.Show("Please select the date range!", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTo.Focus(); return; } }
            else
            { if (!string.IsNullOrEmpty(txtTo.Text)) { MessageBox.Show("Please select the date range!", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information); txtFrom.Focus(); return; } }
            BindOutwardListGridData();
        }
        private void btnDocSearch_Click(object sender, EventArgs e)
        {
            try
            { SearchRequest(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        string _dono = string.Empty;
        protected void BindOutwardItems()
        {
            try
            {
                _dono = string.Empty; PickSerialsList = null;
                ReptPickHeader _reptPickHdr = new ReptPickHeader(); Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, OutwardNo);
                UserSeqNo = _seq; _reptPickHdr.Tuh_direct = true; _reptPickHdr.Tuh_doc_no = OutwardNo; _reptPickHdr.Tuh_doc_tp = OutwardType;
                _reptPickHdr.Tuh_ischek_itmstus = false; _reptPickHdr.Tuh_ischek_reqqty = true; _reptPickHdr.Tuh_ischek_simitm = false; _reptPickHdr.Tuh_session_id = BaseCls.GlbUserSessionID;
                _reptPickHdr.Tuh_usr_com = BaseCls.GlbUserComCode; _reptPickHdr.Tuh_usr_id = BaseCls.GlbUserID; _reptPickHdr.Tuh_usrseq_no = _seq; string _unavailableitemlist = string.Empty;
                List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(BaseCls.GlbUserDefLoca, hdnAllowBin, _reptPickHdr, out _unavailableitemlist);
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
                    _sourceItem.DataSource = _tblItems; gvItem.DataSource = _sourceItem;
                    _sourceSerial.DataSource = PickSerials; gvSerial.DataSource = _sourceSerial;
                    PickSerialsList = PickSerials;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally
            { this.Cursor = Cursors.Default; }

        }
        string _supplier = string.Empty; string _subdoc = string.Empty;
        protected void BindSelectedOutwardNo(int _rowIndex)
        {
            try
            {
                _supplier = string.Empty; _subdoc = string.Empty;
                this.Cursor = Cursors.WaitCursor; OutwardNo = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_docno"].Value);
                DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(BaseCls.GlbUserComCode, OutwardNo);
                if (_headerchk != null && _headerchk.Rows.Count > 0)
                { string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id"); string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt")); if (!string.IsNullOrEmpty(_headerUser)) if (BaseCls.GlbUserID.Trim() != _headerUser.Trim()) { MessageBox.Show("Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate, "Scanned Document", MessageBoxButtons.OK, MessageBoxIcon.Information); } }
                hdnOutwarddate = Convert.ToDateTime(gvPending.Rows[_rowIndex].Cells["pen_Date"].Value);
                OutwardType = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_Type"].Value);
                lblIssuedDocNo.Text = OutwardNo; lblIssedCompany.Text = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_issueCompany"].Value);
                lblIssuedLocation.Text = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_issueLocation"].Value); _supplier = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_supcode"].Value);
                _subdoc = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_subdoc"].Value); DataTable _tbl = null;
                if (!string.IsNullOrEmpty(lblIssuedLocation.Text) && !string.IsNullOrEmpty(lblIssedCompany.Text)) _tbl = CHNLSVC.Inventory.Get_location_by_code(lblIssedCompany.Text.Trim(), lblIssuedLocation.Text.Trim());
                if (_tbl != null && _tbl.Rows.Count > 0) lblIssueLocDesc.Text = _tbl.Rows[0].Field<string>("ml_loc_desc");
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally
            { BindOutwardItems(); }
        }
        private void gvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { if (gvPending.RowCount > 0) { int _rowIndex = e.RowIndex; if (_rowIndex != -1) BindSelectedOutwardNo(_rowIndex); } }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
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
                { MessageBox.Show("(R)Select the outward document!", "Outward Document", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                this.Cursor = Cursors.WaitCursor; int row_id = _rowIndex;
                if (string.IsNullOrEmpty(gvSerial.Rows[_rowIndex].Cells["ser_Bin"].Value.ToString())) return;
                string _item = Convert.ToString(gvSerial.Rows[_rowIndex].Cells["ser_Item"].Value); _serialID = Convert.ToInt32(gvSerial.Rows[_rowIndex].Cells["ser_serialid"].Value); string _bin = Convert.ToString(gvSerial.Rows[_rowIndex].Cells["ser_Bin"].Value);
                if (_serialID == 0)
                { MessageBox.Show("Can not remove none-serialized items!", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                //modify Rukshan 05/oct/2015 add two parameters
                CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID),null,null);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally
            { BindPickSerials(); }
        }
        private void gvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { if (gvSerial.RowCount > 0) { int _rowIndex = e.RowIndex; if (_rowIndex != -1) { if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) OnRemoveFromSerialGrid(_rowIndex); } } }
            catch (Exception ex) { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private bool IsBackDateOk()
        {
            bool _isOK = true; bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            { if (_allowCurrentTrans == true) { if (txtDate.Value.Date != DateTime.Now.Date) { txtDate.Enabled = true; MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; } } else { txtDate.Enabled = true; MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; } }
            return _isOK;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckServerDateTime() == false) { return; }
            if (MessageBox.Show("Do you need to process?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }
            if (IsBackDateOk() == false) return;
            if (gvItem.RowCount <= 0)
            { MessageBox.Show("Please select the items", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (gvSerial.RowCount <= 0)
            { MessageBox.Show("Please select the serials", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(BaseCls.GlbUserComCode))
            { MessageBox.Show("Session expired! Please re-login to system."); txtDate.Focus(); return; }
            if (string.IsNullOrEmpty(txtDate.Text))
            { MessageBox.Show("Please select the date"); txtDate.Focus(); return; }
            if (IsDate(txtDate.Text, DateTimeStyles.None) == false)
            { txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy"); MessageBox.Show("Invalid Date."); return; }
            if (string.IsNullOrEmpty(lblIssuedDocNo.Text))
            { lblIssuedDocNo.Text = "N/A"; }
            if (string.IsNullOrEmpty(txtRemarks.Text))
            { txtRemarks.Text = "N/A"; }
            if (string.IsNullOrEmpty(txtVehicle.Text))
            { txtVehicle.Text = "N/A"; }
            if (DateTime.Compare(Convert.ToDateTime(hdnOutwarddate.Value.ToString()).Date, Convert.ToDateTime(txtDate.Text).Date) > 0)
            { MessageBox.Show("Inward entry date should be greater than or equal to outward entry date."); return; }
            InventoryHeader invHdr = new InventoryHeader();
            invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
            invHdr.Ith_com = BaseCls.GlbUserComCode;
            invHdr.Ith_oth_docno = OutwardNo;
            invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
            invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
            if (OutwardType == "AOD")
            { invHdr.Ith_doc_tp = "AOD"; invHdr.Ith_cate_tp = "NOR"; invHdr.Ith_sub_tp = "NOR"; }
            else if (OutwardType == "DO")
            { invHdr.Ith_doc_tp = "GRN"; invHdr.Ith_cate_tp = "LOCAL"; invHdr.Ith_sub_tp = "LOCAL"; invHdr.Ith_oth_docno = Convert.ToString(gvPending.SelectedRows[0].Cells["pen_subdoc"].Value); invHdr.Ith_sub_docno = OutwardNo; }
            else if (OutwardType == "PRN")
            { invHdr.Ith_doc_tp = "SRN"; invHdr.Ith_cate_tp = "NOR"; invHdr.Ith_sub_tp = "NOR"; invHdr.Ith_sub_docno = OutwardNo; }
            else
            { MessageBox.Show("New outward document type found!"); return; }
            PurchaseOrder _pohdr = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(BaseCls.GlbUserComCode, invHdr.Ith_oth_docno);
            PurchaseOrderDetail _poone = new PurchaseOrderDetail();
            List<PurchaseOrderDetail> _poLst = new List<PurchaseOrderDetail>();
            string _supplierclaimcode = string.Empty;
            if (OutwardType == "DO")
            {
                _poone.Pod_seq_no = _pohdr.Poh_seq_no; _poLst = CHNLSVC.Inventory.GetPOItems(_poone);
                DataTable _supCode = CHNLSVC.Inventory.GetSupplier(BaseCls.GlbUserComCode, _pohdr.Poh_supp);
                if (_supCode != null && _supCode.Rows.Count > 0) _supplierclaimcode = _supCode.Rows[0].Field<string>("MBE_CATE");
            }
            invHdr.Ith_is_manual = false; invHdr.Ith_stus = "A";
            invHdr.Ith_cre_by = BaseCls.GlbUserID; invHdr.Ith_mod_by = BaseCls.GlbUserID;
            invHdr.Ith_direct = true; invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
            invHdr.Ith_manual_ref = "N/A"; invHdr.Ith_remarks = txtRemarks.Text;
            invHdr.Ith_vehi_no = txtVehicle.Text; invHdr.Ith_bus_entity = OutwardType == "DO" ? _pohdr.Poh_supp : string.Empty;
            invHdr.Ith_oth_com = lblIssedCompany.Text; invHdr.Ith_oth_loc = lblIssuedLocation.Text;
            invHdr.Ith_pc = BaseCls.GlbUserDefProf; Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, BaseCls.GlbUserComCode, OutwardNo, 1);
            if (PickSerialsList == null)
            { MessageBox.Show("Please select outward document!"); return; }
            btnSave.Enabled = false;
            //Add by Chamal 23-May-2014
            int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(invHdr.Ith_oth_com, invHdr.Ith_com, invHdr.Ith_doc_tp, OutwardNo, invHdr.Ith_doc_date.Date, BaseCls.GlbUserID);

            PickSerialsList.ForEach(x => x.Tus_doc_dt = invHdr.Ith_doc_date.Date);
            if (invHdr.Ith_doc_tp == "GRN")
            {
                if (invHdr.Ith_oth_com == "ABL" && invHdr.Ith_com == "LRP")
                {
                    PickSerialsList.ForEach(x => x.Tus_orig_grndt = invHdr.Ith_doc_date.Date);
                    PickSerialsList.ForEach(x => x.Tus_exist_grndt = invHdr.Ith_doc_date.Date);
                }
                if (invHdr.Ith_oth_com == "SGL" && invHdr.Ith_com == "SGD")
                {
                    PickSerialsList.ForEach(x => x.Tus_orig_grndt = invHdr.Ith_doc_date.Date);
                    PickSerialsList.ForEach(x => x.Tus_exist_grndt = invHdr.Ith_doc_date.Date);
                }
            }

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
                #endregion

                this.Cursor = Cursors.WaitCursor;
                if (OutwardType == "AOD")
                { masterAutoNum.Aut_moduleid = "AOD"; masterAutoNum.Aut_start_char = "AOD"; result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo); }
                else if (OutwardType == "DO")
                {
                    //DataTable _lp = CHNLSVC.General.GetItemLPStatus();
                    DataTable _lp = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                    var lst = PickSerialsList.Where(x => x.Tus_ser_4 != "1").ToList();
                    foreach (ReptPickSerials s in lst)
                    {
                        string _item = s.Tus_itm_cd; string _status = s.Tus_itm_stus; decimal _qty = s.Tus_qty;
                        //var _lpstatus = _lp.AsEnumerable().Where(x => x.Field<string>("mis_scm2_imp") == _status).Select(x => x.Field<string>("mis_cd")).ToList();
                        var _lpstatus = _lp.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _status).Select(x => x.Field<string>("mis_lp_cd")).ToList();
                        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _lpstatus[0], "VAT", string.Empty);
                        //var actualprice = _poLst.Where(x => x.Pod_itm_stus == _lpstatus[0] && x.Pod_itm_cd == _item).Select(x => x.Pod_act_unit_price / x.Pod_qty).ToList();
                        //Edit by Chamal 02-Feb-2015
                        var actualprice = _poLst.Where(x => x.Pod_itm_stus == _lpstatus[0] && x.Pod_itm_cd == _item).Select(x => x.Pod_act_unit_price).ToList();
                        if (_tax == null || _tax.Count <= 0)
                        { MessageBox.Show("Company item tax is not setup for the " + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); s.Tus_ser_4 = "0"; return; }
                        else
                        {
                            if (string.IsNullOrEmpty(_supplierclaimcode))
                            {
                                s.Tus_unit_price = s.Tus_unit_cost; s.Tus_unit_cost = actualprice[0];
                            }
                            else
                            {
                                foreach (MasterItemTax _t in _tax)
                                {
                                    if (_t.Mict_tax_rate > 0)
                                    {
                                        MasterItemTaxClaim _claim = CHNLSVC.Sales.GetTaxClaimDet(BaseCls.GlbUserComCode, _item, _supplierclaimcode);
                                        if (_claim == null || string.IsNullOrEmpty(_claim.Mic_com))
                                        {
                                            MessageBox.Show("There is no definition for VAT claimable rate for " + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            s.Tus_ser_4 = "0";
                                            return;
                                        }
                                        decimal _comTx = _tax[0].Mict_tax_rate; decimal _claimableRate = (_claim.Mic_tax_rt - _claim.Mic_claim) / _claim.Mic_tax_rt;
                                        decimal _actualUnitCost = 0; if (_claimableRate > 0) _actualUnitCost = (actualprice[0] * (_comTx + 100) / 100) * (100 - _claimableRate) / 100; else _actualUnitCost = actualprice[0];
                                        s.Tus_unit_price = RoundUpForPlace(s.Tus_unit_cost, 2); s.Tus_unit_cost = RoundUpForPlace(_actualUnitCost, 2);
                                    }
                                    else
                                    {
                                        s.Tus_unit_price = s.Tus_unit_cost; s.Tus_unit_cost = actualprice[0];
                                    }
                                }
                            }
                        }
                        s.Tus_itm_stus = _lpstatus[0]; s.Tus_ser_4 = "1";
                    }
                    PickSerialsList.ForEach(x => x.Tus_ser_4 = null);
                    PickSerialsList.OrderBy(X => X.Tus_itm_cd);

                    masterAutoNum.Aut_moduleid = "GRN"; 
                    masterAutoNum.Aut_start_char = "GRN"; 
                    masterAutoNum.Aut_direction = null;
                    masterAutoNum.Aut_year = invHdr.Ith_doc_date.Year;

                    result = CHNLSVC.Inventory.GRNEntry(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);
                }
                else if (OutwardType == "PRN")
                {
                    DataTable Invoice = null;
                    if (string.IsNullOrEmpty(_dono))
                    {
                        MessageBox.Show("Can't find delivery order details!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        Invoice = CHNLSVC.Inventory.GetInvoiceDet(BaseCls.GlbUserComCode, _dono);
                        if (Invoice == null || Invoice.Rows.Count <= 0)
                        {
                            //Check DO in SCM database :: Chamal 29-04-2014 ::
                            Invoice = CHNLSVC.Inventory.GetSCMInvoiceDet(BaseCls.GlbUserComCode, _dono);
                            if (Invoice == null || Invoice.Rows.Count <= 0)
                            {
                                MessageBox.Show("Invalid delivery order no!/nTECH INFOR - see the inr_ser.irsm_anal_2 value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }



                    string _invTp = Invoice.Rows[0].Field<string>("sah_inv_tp");
                    string _pc = Invoice.Rows[0].Field<string>("sah_pc");
                    string _invoiceno = Invoice.Rows[0].Field<string>("sah_inv_no");
                    string _customercode = Invoice.Rows[0].Field<string>("sah_cus_cd");

                    _invTp = "CRED";


                    InvoiceHeader _invheader = new InvoiceHeader();
                    _invheader.Sah_com = BaseCls.GlbUserComCode; _invheader.Sah_cre_by = BaseCls.GlbUserID;
                    _invheader.Sah_cre_when = DateTime.Now;
                    _invheader.Sah_currency = "LKR"; _invheader.Sah_cus_add1 = string.Empty;
                    _invheader.Sah_cus_add2 = string.Empty; _invheader.Sah_cus_cd = _customercode; _invheader.Sah_cus_name = string.Empty;
                    _invheader.Sah_d_cust_add1 = string.Empty; _invheader.Sah_d_cust_add2 = string.Empty; _invheader.Sah_d_cust_cd = _customercode;
                    _invheader.Sah_direct = false; _invheader.Sah_dt = Convert.ToDateTime(txtDate.Value).Date; _invheader.Sah_epf_rt = 0;
                    _invheader.Sah_esd_rt = 0; _invheader.Sah_ex_rt = 1; _invheader.Sah_inv_no = "na";
                    _invheader.Sah_inv_sub_tp = "REV"; _invheader.Sah_inv_tp = _invTp; _invheader.Sah_is_acc_upload = false;
                    _invheader.Sah_man_cd = "N/A"; _invheader.Sah_man_ref = string.Empty; _invheader.Sah_manual = false;
                    _invheader.Sah_mod_by = BaseCls.GlbUserID; _invheader.Sah_mod_when = DateTime.Now; _invheader.Sah_pc = _pc; //BaseCls.GlbUserDefProf;
                    _invheader.Sah_pdi_req = 0; _invheader.Sah_ref_doc = _invoiceno; _invheader.Sah_remarks = string.Empty;
                    _invheader.Sah_sales_chn_cd = string.Empty; _invheader.Sah_sales_chn_man = string.Empty; _invheader.Sah_sales_ex_cd = "N/A";
                    _invheader.Sah_sales_region_cd = string.Empty; _invheader.Sah_sales_region_man = string.Empty; _invheader.Sah_sales_sbu_cd = string.Empty;
                    _invheader.Sah_sales_sbu_man = string.Empty; _invheader.Sah_sales_str_cd = string.Empty; _invheader.Sah_sales_zone_cd = string.Empty;
                    _invheader.Sah_sales_zone_man = string.Empty; _invheader.Sah_seq_no = 1;
                    _invheader.Sah_session_id = BaseCls.GlbUserSessionID; _invheader.Sah_structure_seq = string.Empty;
                    _invheader.Sah_stus = "A"; _invheader.Sah_town_cd = string.Empty;
                    _invheader.Sah_tp = "INV"; _invheader.Sah_wht_rt = 0;
                    _invheader.Sah_tax_inv = false; _invheader.Sah_anal_5 = _dono;
                    MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                    _invoiceAuto.Aut_cate_cd = _pc; _invoiceAuto.Aut_cate_tp = "PC";
                    _invoiceAuto.Aut_direction = 0; _invoiceAuto.Aut_modify_dt = null;
                    _invoiceAuto.Aut_moduleid = "REV"; _invoiceAuto.Aut_number = 0;
                    if (BaseCls.GlbUserComCode == "LRP") _invoiceAuto.Aut_start_char = "RINREV"; else _invoiceAuto.Aut_start_char = "INREV";
                    _invoiceAuto.Aut_year = null;
                    decimal _unitAmt = 0; decimal _disAmt = 0; decimal _taxAmt = 0; decimal _totAmt = 0;
                    //List<InvoiceItem> _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(_invoiceno, "DELIVERD");                  
                    //Chamal edit on 30/04/2014
                    List<InvoiceItem> _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForIntrPRN(_invoiceno, "DELIVERD", OutwardNo);
                    List<InvoiceItem> CreditNoteLst = new List<InvoiceItem>();
                    if (_paramInvoiceItems != null && _paramInvoiceItems.Count > 0)
                    {
                        foreach (ReptPickSerials s in PickSerialsList)
                        {
                            List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "", string.Empty, _invoiceno, Convert.ToInt32(s.Tus_new_status));
                            var _ucost = _serLst.Where(x => x.Tus_ser_id == s.Tus_ser_id).Select(x => x.Tus_unit_cost).ToList();
                            if (_ucost != null && _ucost.Count() > 0) s.Tus_unit_cost = _ucost[0];
                            var InvoiceItem = _paramInvoiceItems.Where(x => x.Sad_itm_line == Convert.ToInt32(s.Tus_new_status)).ToList();
                            if (InvoiceItem != null && InvoiceItem.Count > 0)
                            {
                                foreach (InvoiceItem item in InvoiceItem)
                                {
                                    _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty));
                                    item.Sad_unit_amt = Convert.ToDecimal(_unitAmt); item.Sad_disc_amt = Convert.ToDecimal(_disAmt); item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt); item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                                    CreditNoteLst.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        int q = 1;
                        //Check SCM database :: Chamal 29/04/2014
                        _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversalSCM(_invoiceno, "DELIVERD", OutwardNo);
                        if (_paramInvoiceItems != null && _paramInvoiceItems.Count > 0)
                        {
                            foreach (ReptPickSerials s in PickSerialsList)
                            {
                                //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "", string.Empty, _invoiceno, Convert.ToInt32(s.Tus_new_status));
                                //var _ucost = _serLst.Where(x => x.Tus_ser_id == s.Tus_ser_id).Select(x => x.Tus_unit_cost).ToList();
                                //if (_ucost != null && _ucost.Count() > 0) s.Tus_unit_cost = _ucost[0];

                                int _isSer = 0;
                                if (s.Tus_ser_1 != "N/A")
                                {
                                    _isSer = 1;
                                }

                                DataTable _dtscmcost = CHNLSVC.Inventory.GetItemCostSerialSCM(_dono, s.Tus_itm_cd, s.Tus_itm_stus, s.Tus_ser_1, _isSer);
                                if (_dtscmcost != null || _dtscmcost.Rows.Count > 0)
                                {
                                    s.Tus_unit_cost = _dtscmcost.Rows[0].Field<decimal>("UNIT_COST");
                                }
                                else
                                {
                                    s.Tus_unit_cost = 0;
                                }

                                var InvoiceItem = _paramInvoiceItems.Where(x => x.Sad_itm_cd == s.Tus_itm_cd).ToList();
                                if (InvoiceItem != null && InvoiceItem.Count > 0)
                                {
                                    foreach (InvoiceItem item in InvoiceItem)
                                    {
                                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty));
                                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt); item.Sad_disc_amt = Convert.ToDecimal(_disAmt); item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt); item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                                        item.Sad_itm_line = q;
                                        CreditNoteLst.Add(item);
                                        q += 1;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invoice items not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    _invheader.Sah_anal_7 = CreditNoteLst.Sum(X => X.Sad_tot_amt);
                    masterAutoNum.Aut_moduleid = "SRN"; masterAutoNum.Aut_start_char = "SRN";
                    PickSerialsList.OrderBy(X => X.Tus_itm_cd); string _crno = string.Empty;
                    invHdr.Ith_oth_loc = string.Empty; invHdr.Ith_cate_tp = "INTR";
                    result = CHNLSVC.Sales.SaveReversalNew(_invheader, CreditNoteLst, _invoiceAuto, false, out _crno, invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, null, null, null, false, null, null, null, false, false, _invheader.Sah_pc, null, null, null, null, null, false, out documntNo);
                }
                else
                { this.Cursor = Cursors.Default; MessageBox.Show("Invalid outward document type found!"); return; }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally
            { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

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
                lblIssueLocDesc.Text = string.Empty;
                this.Cursor = Cursors.Default;
            }
            else
            {
                btnSave.Enabled = true;
                MessageBox.Show(documntNo, "Process Terminate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                CHNLSVC.CloseChannel();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0) Controls[0].Dispose();
            InitializeComponent(); InitializeForm();
            BackDatePermission(); btnSave.Enabled = true;
            lblIssueLocDesc.Text = ""; this.Cursor = Cursors.Default;
        }
        private void InterCompanyInWardEntry_Load(object sender, EventArgs e)
        {
            try
            { BackDatePermission(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtAODNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtAODNumber.Text))
                {
                    BindOutwardListGridData();
                    DataTable dtTemp = (DataTable)gvPending.DataSource;
                    dtTemp.CaseSensitive = false;
                    if (dtTemp != null && dtTemp.Rows.Count > 0 && dtTemp.Select("ITH_DOC_NO LIKE '" + txtAODNumber.Text + "%'").Length > 0)
                    {
                        DataTable dtFilterd = dtTemp.Select("ITH_DOC_NO LIKE '" + txtAODNumber.Text + "%'").CopyToDataTable();
                        gvPending.DataSource = dtFilterd;
                    }
                    else
                    {
                        dtTemp.Rows.Clear();
                        gvPending.DataSource = dtTemp;
                    }
                }
                else
                {
                    BindOutwardListGridData();
                }
            }
        }
    }
}
