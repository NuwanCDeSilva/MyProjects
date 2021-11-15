using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;
using FF.Interfaces;
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.Inventory;

namespace FF.WindowsERPClient.Inventory
{

    //Web base system written by Prabhath (Original)
    //Windows base system written by Prabhath according to the web 

    public partial class InterCompanyOutWardEntry : FF.WindowsERPClient.Base
    {
        const string COM_OUT = "COM_OUT"; private List<InventoryRequestItem> ScanItemList = null;
        private string _receCompany = string.Empty; private Int32 UserSeqNo = 0;
        private string RequestNo = string.Empty; private string SelectedStatus = string.Empty; private string JobNo = string.Empty; private Int16 JobLine = 1;
        private MasterItem _itemdetail = null; bool _isDecimalAllow = false;
        private List<ReptPickSerials> serial_list = null; private List<ReptPickSerials> SelectedSerialList = null;
        CommonSearch.CommonOutScan _commonOutScan = null; protected DataTable _unFinishedDirectDocument = null;
        bool _ServiceJobBase = false;

        //Akila 2017/05/26
       // List<string> SelectedJobLine = new List<string>();
        int ServiceJobLineNo = 0;        
        private string _isMigration = "N";

        class DocumentType
        {
            string _displayMemener = string.Empty; string _valueMemeber = string.Empty;
            public string DisplayMemener
            {
                get { return _displayMemener; }
                set { _displayMemener = value; }
            }
            public string ValueMemeber
            {
                get { return _valueMemeber; }
                set { _valueMemeber = value; }
            }
        }
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private List<DocumentType> _documentType = null;
        private void FillDocumentType()
        {
            _documentType = new List<DocumentType>(); DocumentType _type = new DocumentType();
            _type.DisplayMemener = "AOD"; _type.ValueMemeber = "MDOC_AOD";
            _documentType.Add(_type); _type = new DocumentType();
            _type.DisplayMemener = "DO"; _type.ValueMemeber = "MDOC_DO";
            _documentType.Add(_type); _type = new DocumentType();
            _type.DisplayMemener = "PRN"; _type.ValueMemeber = "MDOC_PRN";
            _documentType.Add(_type); _type = null;
            ddlManType.DataSource = _documentType; ddlManType.DisplayMember = "DisplayMemener";
            ddlManType.ValueMember = "ValueMemeber";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) this.Close();
        }
        private void BackDatePermission()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; }
        }
        protected void BindMrnDetail(string _mrn)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                BindingSource _source = new BindingSource();
                if (ScanItemList == null) ScanItemList = new List<InventoryRequestItem>();
                if (!string.IsNullOrEmpty(_mrn))
                {
                    List<InventoryRequestItem> _lst = CHNLSVC.Inventory.GetMaterialRequestItemByRequestNoList(_mrn);
                    if (_lst == null || _lst.Count <= 0)
                    { MessageBox.Show("There is no pending item for issue. Please contact IT Dept.", "System Reconciliation Error", MessageBoxButtons.OK, MessageBoxIcon.Information); _source.DataSource = ScanItemList; gvItems.DataSource = _source; return; }
                    _lst.ForEach(X => X.Itri_note = _mrn); _lst.Where(z => z.Itri_note == _mrn).ToList().ForEach(x => x.Itri_qty = 0);
                    if (SelectedSerialList != null)
                    {
                        if (SelectedSerialList.Count > 0)
                        {
                            if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                            {
                                _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd).Sum(z => z.Tus_qty));
                                _lst.ForEach(x => x.Itri_bqty -= SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd).Sum(z => z.Tus_qty));
                            }
                            else
                            {
                                _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd && y.Tus_itm_stus == x.Itri_itm_stus).Sum(z => z.Tus_qty));
                            }
                        }
                    }
                    ScanItemList.AddRange(_lst); _source.DataSource = ScanItemList; gvItems.DataSource = _source;

                    //kapila 24/2/2017

                }
                else
                { _source = new BindingSource(); gvItems.DataSource = _source; }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; }
        }
        protected void BindDirectDetail(string _mrn)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; BindingSource _source = new BindingSource();
                if (ScanItemList == null) ScanItemList = new List<InventoryRequestItem>();
                if (!string.IsNullOrEmpty(_mrn) && SelectedSerialList != null && SelectedSerialList.Count > 0)
                {
                    List<InventoryRequestItem> _lst = new List<InventoryRequestItem>();
                    var _itemdet = (from _l in SelectedSerialList group _l by new { _l.Tus_itm_cd, _l.Tus_itm_stus } into batch select new { Tus_itm_cd = batch.Key.Tus_itm_cd, Tus_itm_stus = batch.Key.Tus_itm_stus, Tus_qty = batch.Sum(p => p.Tus_qty) }).ToList();
                    if (_itemdet != null && _itemdet.Count > 0)
                    {
                        int _line = 1;
                        foreach (var _n in _itemdet)
                        {
                            InventoryRequestItem _one = new InventoryRequestItem();
                            MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _n.Tus_itm_cd);
                            _one.Itri_app_qty = _n.Tus_qty; _one.Itri_bqty = _n.Tus_qty;
                            _one.Itri_itm_cd = _n.Tus_itm_cd; _one.Itri_itm_stus = _n.Tus_itm_stus;
                            _one.Itri_line_no = _line; _line += 1;
                            _one.Itri_mitm_cd = _n.Tus_itm_cd; _one.Itri_mitm_stus = _n.Tus_itm_stus;
                            _one.Itri_mqty = _n.Tus_qty; _one.Itri_note = _mrn;
                            _one.Itri_qty = _n.Tus_qty; _one.Mi_longdesc = _itm.Mi_longdesc;
                            _one.Mi_model = _itm.Mi_model; _lst.Add(_one);
                        }
                    }
                    _lst.ForEach(X => X.Itri_note = _mrn); _lst.Where(z => z.Itri_note == _mrn).ToList().ForEach(x => x.Itri_qty = 0);
                    if (SelectedSerialList != null) if (SelectedSerialList.Count > 0) if (Convert.ToString(ddlType.SelectedValue) == "INTR") _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd).Sum(z => z.Tus_qty));
                            else _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd && y.Tus_itm_stus == x.Itri_itm_stus).Sum(z => z.Tus_qty));
                    ScanItemList.AddRange(_lst); _source.DataSource = ScanItemList; gvItems.DataSource = _source;
                }
                else
                { _source = new BindingSource(); gvItems.DataSource = _source; }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; }
        }
        protected void BindPickSerials(int _userSeqNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SelectedSerialList.RemoveAll(X => X.Tus_usrseq_no == _userSeqNo);
                BindingSource _source = new BindingSource();
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _source.DataSource = _list;
                gvSerial.DataSource = _source;
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, COM_OUT);
                if (_list == null || _list.Count <= 0) { if (SelectedSerialList != null) if (SelectedSerialList.Count > 0) { _source.DataSource = SelectedSerialList; gvSerial.DataSource = _source; } return; }
                SelectedSerialList.AddRange(_list); _source.DataSource = SelectedSerialList; gvSerial.DataSource = _source;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void BindMRNListGridData()
        {
            //try
            //{
            this.Cursor = Cursors.WaitCursor;
            InventoryRequest _inventoryRequest = new InventoryRequest();
            //_inventoryRequest.Itr_com = GlbUserComCode;
            //edit Chamal 18-08-2012
            _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;
            _inventoryRequest.Itr_tp = ddlType.SelectedValue.ToString();
            _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
            _inventoryRequest.FromDate = !string.IsNullOrEmpty(txtFrom.Text.Trim()) ? txtFrom.Text : string.Empty;
            _inventoryRequest.ToDate = !string.IsNullOrEmpty(txtTo.Text.Trim()) ? txtTo.Text : string.Empty;
            _inventoryRequest.Itr_req_no = !string.IsNullOrEmpty(txtReqNo.Text.Trim()) ? txtReqNo.Text : string.Empty;
            _inventoryRequest.Itr_cre_by = !string.IsNullOrEmpty(txtReqBy.Text.Trim()) ? txtReqBy.Text : string.Empty;
            List<InventoryRequest> _lstall = new List<InventoryRequest>();
            BindingSource _source = new BindingSource();
            _lstall = CHNLSVC.Inventory.GetAllMaterialRequestsList(_inventoryRequest);
            if (_lstall != null && _lstall.Count > 0)
            {
                _lstall = _lstall.OrderByDescending(x => x.Itr_cre_dt).ThenBy(x => x.Itr_req_no).ToList();
                _source.DataSource = _lstall;
                gvPending.DataSource = _source;
                var _lst = _lstall.Where(X => X.Itr_tp != "DIN").OrderByDescending(x => x.Itr_dt).ToList();
                _source.DataSource = _lst;
                gvPending.DataSource = _source;
            }
            else
            {
                gvPending.DataSource = _lstall;
            }

            //}
            //catch (Exception ex)
            //{ this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            //finally
            //{ this.Cursor = Cursors.Default; }
        }
        private void BindReceiveCompany()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ddlRecCompany.DataSource = new List<MasterCompany>();
                List<MasterCompany> _list = CHNLSVC.General.GetALLMasterCompaniesData();
                _list.Add(new MasterCompany { Mc_cd = "" });
                var _lst = _list.OrderBy(items => items.Mc_cd).ToList();
                BindingSource _souce = new BindingSource();
                _souce.DataSource = _lst;
                ddlRecCompany.DataSource = _souce.DataSource;
                ddlRecCompany.DisplayMember = "Mc_cd";
                ddlRecCompany.ValueMember = "Mc_cd";
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally { this.Cursor = Cursors.Default; }
        }
        private void BindRequestTypesDDLData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int _selectIndex = 1;
                MasterLocation oLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (oLocation.Ml_loc_tp.ToUpper() == "SERS".ToString())
                {
                    _selectIndex = 3;
                }
                ddlType.Items.Clear();
                List<MasterType> _masterType = CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.REQ.ToString());
                _masterType.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.GRAN.ToString()));
                _masterType.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.PDA.ToString()));

                var _lst = _masterType.OrderBy(items => items.Mtp_desc).Where(x => x.Mtp_desc != "Damage Inform Note").ToList();
                BindingSource _source = new BindingSource(); _source.DataSource = _lst;
                ddlType.DataSource = _source.DataSource; ddlType.DisplayMember = "Mtp_desc";
                ddlType.ValueMember = "Mtp_cd"; ddlType.SelectedIndex = _selectIndex;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally
            { this.Cursor = Cursors.Default; }
        }
        private void InitializeForm(bool _isDocType, bool _isDateReset)
        {
            try
            {
                txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
                this.Cursor = Cursors.WaitCursor;
                FillDocumentType(); SelectedSerialList = new List<ReptPickSerials>();
                ScanItemList = new List<InventoryRequestItem>(); _itemdetail = new MasterItem();
                serial_list = new List<ReptPickSerials>(); if (_isDateReset) txtFrom.Value = Convert.ToDateTime(DateTime.Today.Date.AddMonths(-1));
                gvPending.AutoGenerateColumns = false; gvItems.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
                chkDirectOut.Visible = true;
                if(_isMigration=="Y")
                { btn_migration.Visible = true; }
                else { btn_migration.Visible = false; }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally
            { UserPermissionforDirectOut(); if (_isDocType) BindRequestTypesDDLData(); BindReceiveCompany(); BindMRNListGridData(); BindPickSerials(0); BindMrnDetail(string.Empty); ddlRecCompany.Text = BaseCls.GlbUserComCode; 
              txtDispatchRequried.Enabled = false; 
                btnSearch_RecLocation.Enabled = false;
                ddlRecCompany.Enabled = false; 
                gvPending.Enabled = true; 
                RequestNo = string.Empty; 
                JobNo = string.Empty; 
                JobLine = 1; 
                UserSeqNo = -100; 
                ddlManType.Enabled = false; 
            }
        }
        private void UserPermissionforDirectOut()
        {
            //string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, CommonUIDefiniton.UserPermissionType.DIROUT.ToString()))
            //{
            //    chkDirectOut.Enabled = true;
            //}
            //else
            //{
            //    chkDirectOut.Enabled = false;
            //}
        }
        public InterCompanyOutWardEntry()
        {
            _commonOutScan = new CommonSearch.CommonOutScan();
            InitializeComponent();
            try
            { InitializeForm(true, true); _commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) txtTo.Focus(); }
        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) btnDocSearch.Focus(); }
        private void ddlType_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) gvPending.Focus(); }
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) ddlRecCompany.Focus(); }
        private void ddlRecCompany_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) txtDispatchRequried.Focus(); }
        private void txtVehicle_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyCode == Keys.Enter) 
                txtManualRef.Focus(); 
        }
        private void txtManualRef_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) txtRemarks.Focus(); }
        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter && chkDirectOut.Checked == true) txtItem.Focus(); else btnSave.Select(); }
        private void ddlStatus_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter)  txtQty.Focus(); }
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter) btnAddItem.Focus(); }
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                lblItemDescription.Text = "Description : " + string.Empty; lblItemModel.Text = "Model : " + string.Empty;
                lblItemBrand.Text = "Brand : " + string.Empty; lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                _itemdetail = new MasterItem();
                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail != null)
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true; string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model; string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non"; lblItemDescription.Text = "Description : " + _description;
                        lblItemModel.Text = "Model : " + _model; lblItemBrand.Text = "Brand : " + _brand;
                        lblItemSerialStatus.Text = "Serial Status : " + _serialstatus; _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                    }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return _isValid; }
            finally
            { this.Cursor = Cursors.Default; }
            return _isValid;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    { paramsText.Append(_receCompany + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    { break; }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobsWithStage:
                    {
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + txtDispatchRequried.Text.ToUpper().Trim() + seperator);
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtDispatchRequried.Text.ToUpper().Trim() + seperator + txtVehicle.Text.ToString().Trim()); //Tharanga
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnSearch_Request_Click(object sender, EventArgs e)
        { }
        private void btnSearch_RecLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDispatchRequried;
                _CommonSearch.ShowDialog();
                txtDispatchRequried.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtRequest_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.F2) btnSearch_Request_Click(null, null); if (e.KeyCode == Keys.Enter) txtFrom.Focus(); }
        private void txtRequest_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_Request_Click(null, null); }
        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.F2) btnSearch_RecLocation_Click(null, null); if (e.KeyCode == Keys.Enter) txtVehicle.Focus(); }
        private void txtDispatchRequried_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_RecLocation_Click(null, null); }
        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isMigration == "Y")
            {
                ddlStatus.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.F2) btnSearch_Item_Click(null, null); if (e.KeyCode == Keys.Enter) ddlStatus.Focus();
            }
        }
        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_Item_Click(null, null); }
        private void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManualRef.Checked)
                { ddlManType.Enabled = true; ddlManType_SelectedIndexChanged(null, null); txtManualRef.Enabled = false; }
                else
                { ddlManType.Enabled = false; txtManualRef.Enabled = true; txtManualRef.Clear(); }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void IsValidManualNo(object sender, EventArgs e)
        {
            try
            {
                if (txtManualRef.Text != "" && chkManualRef.Checked)
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(ddlManType.SelectedValue), Convert.ToInt32(txtManualRef.Text));
                    if (_IsValid == false)
                    { MessageBox.Show("Invalid Manual Document Number !", "Manual Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtManualRef.Text = ""; txtManualRef.Focus(); return; }
                }
                else
                {
                    if (chkManualRef.Checked == true)
                    { MessageBox.Show("Invalid Manual Document Number !", "Manual Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtManualRef.Focus(); return; }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void ddlManType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManualRef.Checked == true)
                { Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(ddlManType.SelectedValue)); if (_NextNo != 0) txtManualRef.Text = _NextNo.ToString(); else txtManualRef.Text = string.Empty; }
                else txtManualRef.Text = string.Empty;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void ClearItemDetail()
        { txtItem.Text = string.Empty; txtQty.Text = string.Empty; ddlStatus.DataSource = null; gvBalance.DataSource = null; }
        private string DocumentTypeDecider(Int32 _serialID)
        {
            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serialID);
            string _userCompany = BaseCls.GlbUserComCode;//ABL
            string _selectCompany = ddlRecCompany.Text.ToString();//AAL
            string _itemReceiveCompany = _master.Irsm_rec_com;//AAL
            string _comOutDocType = "NON"; if (_userCompany == _selectCompany) _comOutDocType = "AOD-OUT";
            else if (_selectCompany == _itemReceiveCompany) _comOutDocType = "PRN";
            else if (_selectCompany != _itemReceiveCompany) _comOutDocType = "DO";

            if (_master.Irsm_itm_stus == "CONS") _comOutDocType = "AOD-OUT"; //Add by Chamal 06-May-2014
            return _comOutDocType;
        }
        protected void ReceiveCompany_OnChange(object sender, EventArgs e)
        {
            try
            { this.Cursor = Cursors.WaitCursor; string _selectCompany = ddlRecCompany.Text; _receCompany = _selectCompany; txtDispatchRequried.Text = string.Empty; this.Cursor = Cursors.Default; }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void btnDocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFrom.Text))
                { if (string.IsNullOrEmpty(txtTo.Text)) { MessageBox.Show("Please select the date range!", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTo.Focus(); return; } }
                else
                { if (!string.IsNullOrEmpty(txtTo.Text)) { MessageBox.Show("Please select the date range!", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information); txtFrom.Focus(); return; } }
                InitializeForm(false, false);
                BindMRNListGridData();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void BindSelectedMRNDetail(int _rowIndex)
        {
            bool _isOk = true;
            if (!chkDirectOut.Checked)
            {
                RequestNo = gvPending.Rows[_rowIndex].Cells["pen_RequestNo"].Value.ToString();
                if (gvPending.Rows[_rowIndex].Cells["pen_jobno"].Value != null) JobNo = gvPending.Rows[_rowIndex].Cells["pen_jobno"].Value.ToString();
                if (gvPending.Rows[_rowIndex].Cells["pen_jobline"].Value != null) JobLine = Convert.ToInt16(gvPending.Rows[_rowIndex].Cells["pen_jobline"].Value);
                string GRNAStatus = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_grnaStatus"].Value);
                string AppGRANStatus = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_appStatus"].Value);
                UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, BaseCls.GlbUserComCode, RequestNo, 0);
                int grnInitialUserSeq = UserSeqNo; ddlRecCompany.DataSource = null;
                txtDispatchRequried.Text = string.Empty; string _company = gvPending.Rows[_rowIndex].Cells["pen_ReqCompany"].Value.ToString();
                string _dispatchLocation = gvPending.Rows[_rowIndex].Cells["pen_ReqLocation"].Value.ToString();
                string _reqtype = gvPending.Rows[_rowIndex].Cells["pen_Entry"].Value.ToString();
                string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                ddlRecCompany.Items.Clear(); ddlRecCompany.Items.Add(_company);
                ddlRecCompany.SelectedIndex = 0; txtDispatchRequried.Text = _dispatchLocation;
                if (UserSeqNo == -1) UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, COM_OUT, 1, BaseCls.GlbUserComCode);
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (grnInitialUserSeq == -1 && _reqtype.Trim() == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString())
                    {
                        List<InventoryRequestSerials> _list = CHNLSVC.Inventory.GetAllGRANSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, CommonUIDefiniton.MasterTypeCategory.GRAN.ToString(), RequestNo);
                        if (_list == null || _list.Count <= 0) { MessageBox.Show("It seems GRNA entry does not having serial details. Please check the GRAN entry!", "GRAN Serial Detail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); _isOk = false; DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)gvPending.Rows[_rowIndex].Cells["pen_Select"]; _chk.Value = false; return; }
                        bool _isLowStock = false;
                        string _lowstockitem = string.Empty;
                        foreach (InventoryRequestSerials _one in _list)
                        {
                            string _serial = _one.Itrs_ser_1; string _item = _one.Itrs_itm_cd;
                            Int64 _serialId = _one.Itrs_ser_id; MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            if (msitem.Mi_is_ser1 == 1 || msitem.Mi_is_ser1 == 0)
                            {
                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.GetReservedByserialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, Convert.ToInt32(_serialId));
                                if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                                { if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ",serial : " + _serial; else _lowstockitem += ", item : " + _item + ",serial : " + _serial; _isLowStock = true; }
                            }
                            else
                            {
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _one.Itrs_itm_stus);
                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_one.Itrs_qty > _invBal)
                                        { if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus; else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus; _isLowStock = true; }
                                    }
                                    else
                                    { if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus; else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus; _isLowStock = true; }
                                else
                                { if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus; else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus; _isLowStock = true; }
                            }
                        }
                        if (_isLowStock)
                        { this.Cursor = Cursors.Default; MessageBox.Show("There is no stock for the following item(s)." + _lowstockitem, "Stock Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        foreach (InventoryRequestSerials _one in _list)
                        {
                            string _serial = _one.Itrs_ser_1; string _item = _one.Itrs_itm_cd; Int64 _serialId = _one.Itrs_ser_id; Int32 generated_seq = -1; Int32 userseq_no; Int32 user_seq_num = 0;
                            if (UserSeqNo == 0) user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, BaseCls.GlbUserComCode, RequestNo, 0);
                            else user_seq_num = UserSeqNo;
                            if (user_seq_num != -1)
                            {
                                generated_seq = user_seq_num; userseq_no = generated_seq; UserSeqNo = userseq_no;
                                if (grnInitialUserSeq == -1)
                                {
                                    userseq_no = generated_seq; UserSeqNo = userseq_no; ReptPickHeader RPH = new ReptPickHeader(); RPH.Tuh_doc_tp = COM_OUT;
                                    RPH.Tuh_cre_dt = DateTime.Today; RPH.Tuh_ischek_itmstus = false;
                                    RPH.Tuh_ischek_reqqty = false; RPH.Tuh_ischek_simitm = false; RPH.Tuh_session_id = BaseCls.GlbUserSessionID; RPH.Tuh_usr_com = BaseCls.GlbUserComCode;
                                    RPH.Tuh_usr_id = BaseCls.GlbUserID; RPH.Tuh_usrseq_no = generated_seq;
                                    RPH.Tuh_direct = false; RPH.Tuh_doc_no = RequestNo; int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH); grnInitialUserSeq = 100;
                                }
                            }
                            else
                            {
                                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, COM_OUT, 1, BaseCls.GlbUserComCode);//direction always =1 for this method
                                userseq_no = generated_seq; UserSeqNo = userseq_no; ReptPickHeader RPH = new ReptPickHeader(); RPH.Tuh_doc_tp = COM_OUT;
                                RPH.Tuh_cre_dt = DateTime.Today; RPH.Tuh_ischek_itmstus = false; RPH.Tuh_ischek_reqqty = false; RPH.Tuh_ischek_simitm = false;
                                RPH.Tuh_session_id = BaseCls.GlbUserSessionID; RPH.Tuh_usr_com = BaseCls.GlbUserComCode; RPH.Tuh_usr_id = BaseCls.GlbUserID; RPH.Tuh_usrseq_no = generated_seq;
                                RPH.Tuh_direct = false; RPH.Tuh_doc_no = RequestNo; int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                            }

                            MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            if (msitem.Mi_is_ser1 == 1 || msitem.Mi_is_ser1 == 0)
                            {
                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.GetReservedByserialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, Convert.ToInt32(_serialId));
                                if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com)) continue;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID; _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, Convert.ToInt32(_serialId), -1);
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID; _reptPickSerial_.Tus_base_doc_no = RequestNo;
                                _reptPickSerial_.Tus_base_itm_line = _one.Itrs_line_no; _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model; _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(_reptPickSerial_.Tus_ser_id);
                                _reptPickSerial_.Tus_new_status = _one.Itrs_nitm_stus; Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode; _reptPickSerial_.Tus_base_doc_no = RequestNo; _reptPickSerial_.Tus_base_itm_line = _one.Itrs_line_no; _reptPickSerial_.Tus_bin = _defbin;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID; _reptPickSerial_.Tus_cre_dt = DateTime.Now; _reptPickSerial_.Tus_cross_batchline = 0; _reptPickSerial_.Tus_cross_itemline = 0;
                                _reptPickSerial_.Tus_cross_seqno = 0; _reptPickSerial_.Tus_cross_serline = 0; _reptPickSerial_.Tus_doc_dt = Convert.ToDateTime(txtDate.Text); _reptPickSerial_.Tus_doc_no = "N/A";
                                _reptPickSerial_.Tus_exist_grncom = "N/A"; _reptPickSerial_.Tus_isapp = 1; _reptPickSerial_.Tus_iscovernote = 1; _reptPickSerial_.Tus_itm_brand = msitem.Mi_brand;
                                _reptPickSerial_.Tus_itm_cd = _item; _reptPickSerial_.Tus_itm_desc = msitem.Mi_longdesc; _reptPickSerial_.Tus_itm_line = 0; _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_itm_stus = _one.Itrs_itm_stus; _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_new_status = _one.Itrs_nitm_stus; _reptPickSerial_.Tus_qty = _one.Itrs_qty;
                                _reptPickSerial_.Tus_ser_1 = "N/A"; _reptPickSerial_.Tus_ser_2 = "N/A"; _reptPickSerial_.Tus_ser_id = 0; _reptPickSerial_.Tus_ser_line = 0;
                                _reptPickSerial_.Tus_session_id = BaseCls.GlbUserSessionID; _reptPickSerial_.Tus_unit_cost = 0; _reptPickSerial_.Tus_unit_price = 0; _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_warr_no = "N/A"; _reptPickSerial_.Tus_warr_period = 0; _reptPickSerial_.Tus_new_remarks = "AOD-OUT"; Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                        }
                        grnInitialUserSeq = -1;
                    }
                    txtRequest.Text = RequestNo;
                }
                catch (Exception ex)
                { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); _isOk = false; return; }
                finally
                { this.Cursor = Cursors.Default; if (_isOk) { BindPickSerials(UserSeqNo); BindMrnDetail(RequestNo); } }
            }
            else
            { BindReceiveCompany(); }
        }
        private void gvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                    
                int _rowIndex = e.RowIndex; bool _isReturn = false;

                if (_rowIndex != -1 && gvPending.RowCount > 0)
                {
                    if (gvPending.Columns[e.ColumnIndex].Name == "pen_Select")
                    {
                        DataGridViewCheckBoxCell _chk = null; string _document = string.Empty;
                        string _documenttype = string.Empty;
                        string _selectJobNo = string.Empty;
                        try
                        {
                            _chk = (DataGridViewCheckBoxCell)gvPending.Rows[_rowIndex].Cells["pen_Select"]; _document = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_Requestno"].Value);
                            _documenttype = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_Entry"].Value); string _recCompany = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_ReqCompany"].Value);
                            _selectJobNo = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_jobno"].Value);
                            _ServiceJobBase = false;
                            if (_documenttype == "PDA")
                            {
                                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10001))
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Sorry, You have no permission to direct out!\n( Advice: Required permission code :10001)", "Direct Out", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else
                                {
                                    txtDispatchRequried.Enabled = true;
                                    btnSearch_RecLocation.Enabled = true;
                                    ddlRecCompany.Enabled = true;
                                    chkDirectOut.Visible = false;
                                }
                            }
                            if (_documenttype == "MRN" && Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_SubType"].Value) == "SCV")
                            {
                                int i = 0;
                                bool _alreadySelect = false;
                                string _jobNoCurrentSelect = string.Empty;
                                _ServiceJobBase = true;
                                for (i = 0; i <= gvPending.Rows.Count - 1; i++)
                                {
                                    if (Convert.ToBoolean(gvPending.Rows[i].Cells["pen_Select"].Value) == true)
                                    {
                                        _alreadySelect = true;
                                        _jobNoCurrentSelect = Convert.ToString(gvPending.Rows[i].Cells["pen_jobno"].Value);
                                    }
                                }

                                if (_alreadySelect == true)
                                {
                                    if (_selectJobNo != _jobNoCurrentSelect)
                                    {
                                        _chk.Value = false;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                txtDispatchRequried.Enabled = false;
                                btnSearch_RecLocation.Enabled = false;
                                ddlRecCompany.Enabled = false;
                                chkDirectOut.Visible = false;
                            }

                            #region Job Close check
                            if (_ServiceJobBase == true)
                            {
                                if (CHNLSVC.CustService.check_IsJobClosed(Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_jobno"].Value), Convert.ToInt16(gvPending.Rows[_rowIndex].Cells["pen_jobline"].Value)) == true)
                                {
                                    this.Cursor = Cursors.Default; MessageBox.Show("Job Already Closed by Technician. Job # :" + Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_jobno"].Value), "Job Closed", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                                }
                            }
                            #endregion

                            string _recLocation = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_ReqLocation"].Value); if (gvItems.RowCount > 0 && Convert.ToBoolean(_chk.Value) == false)
                            {
                                string _ddlCompany = ddlRecCompany.Text.Trim(); string _txtDispatchLocation = txtDispatchRequried.Text.Trim();
                                if (_recCompany != _ddlCompany)
                                { this.Cursor = Cursors.Default; MessageBox.Show("Request company is mismatched with the selected document.", "Company Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information); _isReturn = true; return; }
                                if (_recLocation != _txtDispatchLocation)
                                { this.Cursor = Cursors.Default; MessageBox.Show("Request location is mismatched with the selected document.", "Location Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information); _isReturn = true; return; }
                            }

                            if (_isReturn == false)
                                if (_documenttype == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString() && Convert.ToBoolean(_chk.Value) == false)
                                {
                                    _chk.Value = true;
                                    var _notBaseDIN = (from DataGridViewRow _row in gvPending.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_row.Cells[0]).Value) == true && Convert.ToString(_row.Cells[3].Value) == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString() && string.IsNullOrEmpty(Convert.ToString(_row.Cells["pen_BaseDIN"].Value)) select _row).Count();
                                    var _BaseDIN = (from DataGridViewRow _row in gvPending.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_row.Cells[0]).Value) == true && Convert.ToString(_row.Cells[3].Value) == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString() && !string.IsNullOrEmpty(Convert.ToString(_row.Cells["pen_BaseDIN"].Value)) select _row).Count();
                                    _chk.Value = false; if (_notBaseDIN > 0 && _BaseDIN > 0)
                                    { MessageBox.Show("You can not select damage information note(DIN) based GRAN with usual GRAN.", "DIN Base GRAN with Regular GRAN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); _chk.Value = true; }
                                }
                            if (_isReturn == false)
                                if (Convert.ToBoolean(_chk.Value) == true)
                                { _chk.Value = false; ScanItemList.RemoveAll(x => x.Itri_note == _document); gvItems.DataSource = null; gvItems.DataSource = ScanItemList; SelectedSerialList.RemoveAll(x => x.Tus_base_doc_no == _document); gvSerial.DataSource = null; gvSerial.DataSource = SelectedSerialList;
                                RequestNo = string.Empty;
                                }
                                else
                                { _chk.Value = true; BindSelectedMRNDetail(_rowIndex); }
                        }
                        catch (Exception ex)
                        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); _isReturn = true; return; }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally {
               
                this.Cursor = Cursors.Default; }
        }
        private void chkDirectOut_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (chkDirectOut.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10001))
                    { 
                        this.Cursor = Cursors.Default; MessageBox.Show("Sorry, You have no permission to direct out!\n( Advice: Required permission code :10001)", "Direct Out", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                        CheckBox _chk = (CheckBox)sender; 
                        _chk.Checked = false; 
                        return; 
                    }
                }

                if (chkDirectOut.Checked)
                {
                    if (LoadDirectType() == false)
                    {
                        MessageBox.Show("Direct issuing facility not available to your company", "Direct Issue", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        chkDirectOut.Checked = false; 
                        return; 
                    }
                    ddlType.SelectedValue = "INTR";
                    txtDispatchRequried.Enabled = true; 
                    btnSearch_RecLocation.Enabled = true; 
                    ddlRecCompany.Enabled = true; 
                    BindReceiveCompany();
                    ddlRecCompany.Text = BaseCls.GlbUserComCode; 
                    gvPending.Enabled = false; 
                    cmbDirectScan.Enabled = true; 
                    cmbDirType.Enabled = true; 
                    LoadUnFinishedDirectDocument();
                
                    ddlType.Enabled = false;
                    

                }
                else
                { 
                    txtDispatchRequried.Enabled = false; 
                    btnSearch_RecLocation.Enabled = false; 
                    ddlRecCompany.Enabled = false; 
                    gvPending.Enabled = true; 
                    cmbDirectScan.DataSource = null; 
                    cmbDirType.Enabled = false; 
                    cmbDirectScan.Enabled = false; 
                    ddlType.Enabled = true; 
                }
                SelectedSerialList = new List<ReptPickSerials>(); 
                ScanItemList = new List<InventoryRequestItem>(); 
                _itemdetail = new MasterItem();
                serial_list = new List<ReptPickSerials>(); 
                gvPending.AutoGenerateColumns = false; 
                gvItems.AutoGenerateColumns = false; 
                gvSerial.AutoGenerateColumns = false;
                this.Cursor = Cursors.Default;
                
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); return; }
            finally
            { 
                BindReceiveCompany();
                BindMRNListGridData(); 
                BindPickSerials(0); 
                BindMrnDetail(string.Empty); 
                ddlRecCompany.Text = BaseCls.GlbUserComCode; 
                RequestNo = string.Empty; 
                JobNo = string.Empty; 
                UserSeqNo = -100; 
                this.Cursor = Cursors.Default; 
                CHNLSVC.CloseAllChannels(); 
            }
        }
        private bool LoadDirectType()
        {
            bool _isOk = false; DataTable _tbl = CHNLSVC.Inventory.GetDirectOutType(BaseCls.GlbUserComCode); _tbl.Rows.InsertAt(_tbl.NewRow(), 0);
            if (_tbl != null && _tbl.Rows.Count > 0)
            { _isOk = true; cmbDirType.DisplayMember = "dirtp_desc"; cmbDirType.ValueMember = "dirtp_tp"; cmbDirType.DataSource = _tbl; }
            else _isOk = false; return _isOk;
        }
        private void LoadItemStatus()
        {
            bool _isDirectTypeStatushv = false; DataTable _inventoryLocation = null;
            if (!string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue)))
            { _inventoryLocation = CHNLSVC.Inventory.GetDirectStatus(BaseCls.GlbUserComCode, Convert.ToString(cmbDirType.SelectedValue)); if (_inventoryLocation != null && _inventoryLocation.Rows.Count > 0) _isDirectTypeStatushv = true; else _isDirectTypeStatushv = false; }
            if (_isDirectTypeStatushv)
            {
                DataTable _invbal = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty); bool _isAvailable = false;
                foreach (DataRow _r in _inventoryLocation.Rows)
                {
                    string _status = Convert.ToString(_r[0]); var _l = _invbal.AsEnumerable().Where(x => x.Field<string>("inl_itm_stus") == _status).ToList(); if (_l != null && _l.Count > 0)
                    { _isAvailable = true; break; }
                    else _isAvailable = false;
                }
                if (_isAvailable == false) { MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlStatus.DataSource = new List<InventoryLocation>(); return; }
                gvBalance.DataSource = null; gvBalance.DataSource = null; ddlStatus.DataSource = _inventoryLocation; ddlStatus.DisplayMember = "mis_desc"; ddlStatus.ValueMember = "inl_itm_stus"; gvBalance.DataSource = _invbal;
            }
            else
            {
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0) { MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlStatus.DataSource = new List<InventoryLocation>(); return; }
                ddlStatus.DataSource = null; gvBalance.DataSource = null; ddlStatus.DataSource = _inventoryLocation; ddlStatus.DisplayMember = "mis_desc"; ddlStatus.ValueMember = "inl_itm_stus"; gvBalance.DataSource = _inventoryLocation;
            }
        }
        protected void CheckItem()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (BaseCls.GlbUserComCode == "AST")
                {
                    string _item = "";
                    //kapila 18/11/2013
                    if (txtItem.Text.Length == 16)
                        _item = txtItem.Text.Substring(1, 7);
                    else if (txtItem.Text.Length == 15)
                        _item = txtItem.Text.Substring(0, 7);
                    else if (txtItem.Text.Length == 8)
                        _item = txtItem.Text.Substring(1, 7);
                    else if (txtItem.Text.Length == 20)
                        _item = txtItem.Text.Substring(0, 12);
                    else
                        _item = txtItem.Text;

                    txtItem.Text = _item;
                }
                if (string.IsNullOrEmpty(txtRequest.Text))
                    if (!chkDirectOut.Checked)
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please select the reference document or direct method first", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(ddlRecCompany.Text.ToString()))
                { this.Cursor = Cursors.Default; MessageBox.Show("Please select receiving company", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                { this.Cursor = Cursors.Default; MessageBox.Show("Please select receiving location", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                LoadItemDetail(txtItem.Text.Trim()); LoadItemStatus();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally { this.Cursor = Cursors.Default; }
        }
        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            { CheckItem(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void CheckQty()
        {
            if (string.IsNullOrEmpty(txtQty.Text)) return;
            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; }
            if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
            { MessageBox.Show("Please select the status", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlStatus.Focus(); return; }
            this.Cursor = Cursors.Default;
            try
            {
                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                if (_inventoryLocation.Count == 1)
                {
                    foreach (InventoryLocation _loc in _inventoryLocation)
                    {
                        decimal _formQty = Convert.ToDecimal(txtQty.Text);
                        if (_formQty > _loc.Inl_free_qty)
                        { this.Cursor = Cursors.Default; MessageBox.Show("Please check the inventory balance!", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); txtQty.Text = string.Empty; txtQty.Focus(); return; }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally { this.Cursor = Cursors.Default; }
        }
        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            { CheckQty(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { IsDecimalAllow(_isDecimalAllow, sender, e); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void CheckLocation()
        {
            if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(Convert.ToString(ddlRecCompany.Text), txtDispatchRequried.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                { this.Cursor = Cursors.Default; MessageBox.Show("Dispatch location is invalid. Please check the location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Clear(); txtDispatchRequried.Focus(); txtDispatchRequried.Enabled = true; cmbDirType.Enabled = true; return; }
                if (_tbl != null) if (_tbl.Rows.Count > 0)
                    {
                        string _fromcompany = BaseCls.GlbUserComCode; string _fromlocation = BaseCls.GlbUserDefLoca; string _tocompany = Convert.ToString(ddlRecCompany.Text); string _tocategory = _tbl.Rows[0].Field<string>("Ml_cate_3");
                        DataTable _adpoint = CHNLSVC.Inventory.GetSubLocation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        if (_adpoint != null && _adpoint.Rows.Count > 0)
                        {
                            var _one = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_loc_cd") == txtDispatchRequried.Text.Trim()).ToList();
                            var _two = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_main_loc_cd") == txtDispatchRequried.Text.Trim()).ToList();
                            if (_one.Count > 0 && _two.Count <= 0) goto xy;
                            if (_one.Count <= 0 && _two.Count > 0) goto xy;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue))) goto xy;
                        DataTable _permCatwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "AODOUT_DIRECT");
                        DataTable _permLocwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, txtDispatchRequried.Text.Trim(), "AODOUT_DIRECT");
                        if (_permLocwise == null || _permLocwise.Rows.Count <= 0)
                        {
                            if (_permCatwise == null || _permCatwise.Rows.Count <= 0)
                            { this.Cursor = Cursors.Default; MessageBox.Show("Permission Required for dispatch location. Please check the location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Clear(); txtDispatchRequried.Focus(); txtDispatchRequried.Enabled = true; cmbDirType.Enabled = true; return; }
                        }
                    }
            xy:

                //if (!string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue)))
                //{
                //    if (cmbDirType.SelectedValue.ToString() == "DIRBB")
                //    {
                //        var _BB = _tbl.AsEnumerable().Where(x => x.Field<string>("ml_cate_3") == "BYBK").ToList();

                //        if (_BB.Count > 0)
                //        {

                //        }
                //        else
                //        {
                //            this.Cursor = Cursors.Default; MessageBox.Show("Pls. select buy back location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Clear(); txtDispatchRequried.Focus(); txtDispatchRequried.Enabled = true; cmbDirType.Enabled = true; return; 
                //        }
                //    }
                //}

                string _defualloc = BaseCls.GlbUserDefLoca; string _selectedLoc = txtDispatchRequried.Text.Trim(); this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (ddlRecCompany.Text.ToString() == BaseCls.GlbUserComCode)
                    {
                        if (_defualloc.Trim() == _selectedLoc.Trim())
                        {
                            this.Cursor = Cursors.Default; txtDispatchRequried.Text = string.Empty;
                            txtDispatchRequried.Enabled = true; cmbDirType.Enabled = true; MessageBox.Show("You can not out to the same location", "Same Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                        }
                    }
                    txtDispatchRequried.Enabled = false; cmbDirType.Enabled = false;

                    //Add by akila 2017/05/15
                    if (chkDirectOut.Checked)
                    {
                        ValidateServiceLocation(txtDispatchRequried.Text.ToUpper().Trim());
                    }
                    
                }
                catch (Exception ex)
                { txtDispatchRequried.Enabled = true; cmbDirType.Enabled = true; this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
                finally { this.Cursor = Cursors.Default; }
            }
        }
        private void txtDispatchRequried_Leave(object sender, EventArgs e)
        {
            try
            { CheckLocation(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); txtDispatchRequried.Enabled = true; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void AddItem()
        {
            if (chkDirectOut.Checked == false)
            { MessageBox.Show("Item adding only allow for direct out", "Direct Out", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(ddlRecCompany.Text.ToString()))
            { MessageBox.Show("Please select the receiving company", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlRecCompany.Focus(); return; }
            if (string.IsNullOrEmpty(txtDispatchRequried.Text))
            { MessageBox.Show("Please select the receiving location", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }
            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; }
            if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
            { MessageBox.Show("Please select the status", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlStatus.Focus(); return; }
            if (string.IsNullOrEmpty(txtQty.Text))
            { MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); txtQty.Focus(); return; }
            try
            {
                this.Cursor = Cursors.WaitCursor; MasterItem _itms = new MasterItem(); _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim()); InventoryRequestItem _itm = new InventoryRequestItem();
                ////added by Wimal - 07/09/2018 to block item status 
                //commented this restriction on 15/sep/2018 due to request by Mr Indrajith
                //MasterLocation _loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtDispatchRequried.Text.ToUpper());
                //if (_loc.Ml_loc_tp=="WH")
                //{
                //DataTable newdt = CHNLSVC.Inventory.SPGETLOCTMCAT(BaseCls.GlbUserComCode, txtDispatchRequried.Text.ToUpper(), _itms.Mi_cate_1, ddlStatus.SelectedValue.ToString(), "GRAN", ddlStatus.SelectedValue.ToString());
                //if (newdt.Rows.Count <= 0)
                //{
                //    MessageBox.Show("Transfer Location " + txtDispatchRequried.Text.ToUpper() + " Not allowd", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //}

                if (ScanItemList != null)
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString() select _ls;
                        if (_duplicate != null) if (_duplicate.Count() > 0)
                            { this.Cursor = Cursors.Default; MessageBox.Show("Selected item already available", "Item Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        var _maxline = (from _ls in ScanItemList select _ls.Itri_line_no).Max();
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text); _itm.Itri_itm_cd = txtItem.Text.Trim(); _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString(); _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                        _itm.Itri_qty = 0; _itm.Mi_longdesc = _itms.Mi_longdesc; _itm.Mi_model = _itms.Mi_model; _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_bqty = _itm.Itri_bqty == 0 ? Convert.ToDecimal(txtQty.Text) : _itm.Itri_bqty;
                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text); _itm.Itri_itm_cd = txtItem.Text.Trim(); _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString(); _itm.Itri_line_no = 1;
                        _itm.Itri_qty = 0; _itm.Mi_longdesc = _itms.Mi_longdesc; _itm.Mi_model = _itms.Mi_model; _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_bqty = Convert.ToDecimal(txtQty.Text);
                    }

                ScanItemList.Add(_itm);
                if (chkDirectOut.Checked) ScanItemList.Where(x => string.IsNullOrEmpty(x.Itri_note)).ToList().ForEach(x => x.Itri_note = RequestNo);
                
                gvItems.DataSource = new List<InventoryRequestItem>(); gvItems.DataSource = ScanItemList; ddlRecCompany.Enabled = false; txtDispatchRequried.Enabled = false;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally
            { this.Cursor = Cursors.Default; ClearItemDetail(); LoadItemDetail(string.Empty);
            if (_isMigration == "N")
            {
                if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) txtItem.Focus(); else gvItems.Focus();
            }
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            { AddItem(); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void OnRemoveFromItemGrid(int _rowIndex, int _colIndex)
        {
            if (chkDirectOut.Checked == false)
            { MessageBox.Show("You can not remove the item which allocated by request", "Base on Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int row_id = _rowIndex; if (string.IsNullOrEmpty(gvItems.Rows[row_id].Cells["itm_Item"].Value.ToString())) return;
                string _item = Convert.ToString(gvItems.Rows[row_id].Cells["itm_Item"].Value); string _itmStatus = Convert.ToString(gvItems.Rows[row_id].Cells["itm_Status"].Value); decimal _qty = Convert.ToDecimal(gvItems.Rows[row_id].Cells["itm_PickQty"].Value);
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, COM_OUT);
                if (_list != null)
                    if (_list.Count > 0)
                    {
                        var _delete = (from _lst in _list where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus select _lst).ToList();
                        foreach (ReptPickSerials _ser in _delete)
                        {
                            string _items = _ser.Tus_itm_cd; Int32 _serialID = _ser.Tus_ser_id; string _bin = _ser.Tus_bin; string serial_1 = _ser.Tus_ser_1;
                            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                            {

                                CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID), null, null); CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1);
                            }
                            else
                            { CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, _item, _itmStatus); }
                        }
                    }
                ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);
                if (ScanItemList != null) if (ScanItemList.Count > 0) { gvItems.DataSource = null; gvItems.DataSource = ScanItemList; } else BindMrnDetail(RequestNo);
                BindPickSerials(UserSeqNo);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally
            { this.Cursor = Cursors.WaitCursor; }
        }
        private void LoadSerialPickScreen(int _rowIndex, int _colIndex)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string _item = Convert.ToString(gvItems.Rows[_rowIndex].Cells["itm_Item"].Value);
                string _status = Convert.ToString(gvItems.Rows[_rowIndex].Cells["itm_Status"].Value);
                decimal _pickedQty = Convert.ToDecimal(gvItems.Rows[_rowIndex].Cells["itm_PickQty"].Value);
                decimal _approvedQty = Convert.ToDecimal(gvItems.Rows[_rowIndex].Cells["itm_AppQty"].Value);
                int _lineno = Convert.ToInt32(gvItems.Rows[_rowIndex].Cells["itm_Lineno"].Value);
                //By Akila 2017/01/26
                decimal _balanceQty = Convert.ToDecimal(gvItems.Rows[_rowIndex].Cells["itm_balQty"].Value);

                if (_pickedQty == _approvedQty) return;
                string _documentno = Convert.ToString(gvItems.Rows[_rowIndex].Cells["itm_requestno"].Value);

                if (chkDirectOut.Checked && !string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue)) && !string.IsNullOrEmpty(Convert.ToString(cmbDirectScan.SelectedValue))) _documentno = RequestNo;
                UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, BaseCls.GlbUserComCode, _documentno, 0);
                RequestNo = _documentno;
                if (UserSeqNo == -1)
                {
                    UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, COM_OUT, 1, BaseCls.GlbUserComCode);//direction always =1 for this method
                    if (chkDirectOut.Checked) RequestNo = Convert.ToString(UserSeqNo);
                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = COM_OUT; RPH.Tuh_cre_dt = DateTime.Today; RPH.Tuh_ischek_itmstus = false; RPH.Tuh_ischek_reqqty = false;
                    RPH.Tuh_ischek_simitm = false; RPH.Tuh_session_id = BaseCls.GlbUserSessionID; RPH.Tuh_usr_com = BaseCls.GlbUserComCode; RPH.Tuh_usr_id = BaseCls.GlbUserID;
                    RPH.Tuh_usrseq_no = UserSeqNo; RPH.Tuh_direct = false; RPH.Tuh_rec_com = Convert.ToString(ddlRecCompany.SelectedValue); RPH.Tuh_rec_loc = Convert.ToString(txtDispatchRequried.Text);
                    RPH.Tuh_isdirect = chkDirectOut.Checked; RPH.Tuh_usr_loc = BaseCls.GlbUserDefLoca; RPH.Tuh_dir_type = Convert.ToString(cmbDirType.SelectedValue);
                    RPH.Tuh_doc_no = RequestNo; int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    if (chkDirectOut.Checked) ScanItemList.ForEach(x => x.Itri_note = RequestNo);
                }
                _commonOutScan.ScanDocument = RequestNo;
                _commonOutScan.SelectedItemList = new List<ReptPickSerials>();
                _commonOutScan.DocumentType = COM_OUT;
                _commonOutScan.PopupItemCode = _item;
                _commonOutScan.PopLableText = "Balance Qty";
                _commonOutScan.PopupQty = Convert.ToDecimal(_balanceQty); //by akila 2017/01/26
                //_commonOutScan.PopupQty = Convert.ToDecimal(_approvedQty); 
                _commonOutScan.ApprovedQty = Convert.ToDecimal(_approvedQty);
                _commonOutScan.ScanQty = Convert.ToDecimal(_pickedQty);
                _commonOutScan.ItemStatus = _status; _commonOutScan.ItemLineNo = _lineno;
                _commonOutScan.JobNo = gvItems.Rows[_rowIndex].Cells["itri_job_no"].Value.ToString();
                _commonOutScan.MainItemCode = gvItems.Rows[_rowIndex].Cells["itri_mitm_cd"].Value.ToString();
                _commonOutScan.JobLineNo = Convert.ToInt32(gvItems.Rows[_rowIndex].Cells["itri_job_line"].Value);
                if (!string.IsNullOrEmpty(Convert.ToString(ddlType.SelectedValue)))
                {
                    if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                    { 
                        //_commonOutScan.IsCheckStatus = false;
                        _commonOutScan.IsCheckStatus = true;
                    }
                    else
                    {
                        _commonOutScan.IsCheckStatus = true;
                    }
                }
                else
                { _commonOutScan.IsCheckStatus = true; }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10102) == true) _commonOutScan.IsCheckStatus = false;

                _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50); _commonOutScan.ModuleTypeNo = 3;
                _commonOutScan.ShowDialog();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally { this.Cursor = Cursors.Default; }
        }
        private void gvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvItems.RowCount > 0)
                {
                    int _rowIndex = e.RowIndex; int _colIndex = e.ColumnIndex;
                    if (_rowIndex != -1)
                    {
                        if (Convert.ToString(ddlType.SelectedValue).Contains("GRAN"))
                            if (gvItems.Columns[_colIndex].Name == "itm_Remove" || gvItems.Columns[_colIndex].Name == "itm_AddSerial")
                            { MessageBox.Show("You don't need to pick serials for the GRAN.", "Picked", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (gvItems.Columns[_colIndex].Name == "itm_Remove") OnRemoveFromItemGrid(_rowIndex, _colIndex);
                        if (gvItems.Columns[_colIndex].Name == "itm_AddSerial") LoadSerialPickScreen(_rowIndex, _colIndex);
                        return;
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void _commonOutScan_AddSerialClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_commonOutScan.SelectedItemList != null)
                    if (_commonOutScan.SelectedItemList.Count > 0)
                    {
                        MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _commonOutScan.PopupItemCode); StringBuilder _errorserial = new StringBuilder(); StringBuilder _pickedserial = new StringBuilder();
                        foreach (ReptPickSerials gvr in _commonOutScan.SelectedItemList)
                        {
                            Int32 serID = Convert.ToInt32(gvr.Tus_ser_id); string binCode = gvr.Tus_bin; ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, gvr.Tus_itm_cd, serID);
                            if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com)) { if (_itm.Mi_is_ser1 == 1)  _pickedserial.Append(gvr.Tus_ser_1 + "/"); }
                        }
                        if (!string.IsNullOrEmpty(_pickedserial.ToString()))
                        { MessageBox.Show("Selected Serial " + _pickedserial.ToString() + " already picked by other process. Please check your inventory", "Picked Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                        //kapila 24/2/2017
                        Boolean _isReqSerFnd = false;
                        if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                        {
                            //check approved serials are available
                            DataTable _dt = CHNLSVC.General.getreqSerByReqno(_commonOutScan.ScanDocument, _commonOutScan.PopupItemCode, null);
                            if (_dt.Rows.Count > 0)
                                _isReqSerFnd = true;
                        }

                        //kapila 24/2/2017
                        if (_isReqSerFnd)
                        {
                            foreach (ReptPickSerials _gvr in _commonOutScan.SelectedItemList)
                            {

                                DataTable _dt = CHNLSVC.General.getreqSerByReqno(_commonOutScan.ScanDocument, _commonOutScan.PopupItemCode, _gvr.Tus_ser_1);
                                if (_dt.Rows.Count == 0)
                                {
                                    MessageBox.Show("Serial " + _gvr.Tus_ser_1 + " is not approved", "Picked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        foreach (ReptPickSerials gvr in _commonOutScan.SelectedItemList)
                        {
                            Int32 serID = Convert.ToInt32(gvr.Tus_ser_id); string binCode = gvr.Tus_bin;
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, gvr.Tus_itm_cd, serID);
                            if (_reptPickSerial_ == null || _reptPickSerial_.Tus_com == null)
                            {
                                if (_itm.Mi_is_ser1 == 0)
                                { _reptPickSerial_ = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _commonOutScan.PopupItemCode, _commonOutScan.ItemStatus, 1)[0]; serID = _reptPickSerial_.Tus_ser_id; }
                                if (_itm.Mi_is_ser1 == 1)
                                { MessageBox.Show("Selected serial " + gvr.Tus_ser_1 + " already picked by another process", "Picked", MessageBoxButtons.OK, MessageBoxIcon.Information); continue; }
                            }
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, gvr.Tus_itm_cd, serID, -1);
                            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID; _reptPickSerial_.Tus_usrseq_no = UserSeqNo; _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                            _reptPickSerial_.Tus_base_doc_no = _commonOutScan.ScanDocument; _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_commonOutScan.ItemLineNo.ToString()); _reptPickSerial_.Tus_itm_desc = _itm.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = _itm.Mi_model; _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(gvr.Tus_ser_id);
                            _reptPickSerial_.Tus_job_no = _commonOutScan.JobNo;
                            _reptPickSerial_.Tus_pgs_prefix = _commonOutScan.MainItemCode;
                            _reptPickSerial_.Tus_job_line = _commonOutScan.JobLineNo;
                            if (serID > 0) { Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null); }

                        }
                        BindPickSerials(UserSeqNo);
                        if (SelectedSerialList != null)
                            if (SelectedSerialList.Count > 0)
                                if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                                {
                                    //ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode).Sum(z => z.Tus_qty));
                                    //ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode).ToList().ForEach(x => x.Itri_bqty -= _commonOutScan.SelectedItemList.Count);
                                    //add by tharanga

                                    if (_itm.Mi_is_ser1 == -1)
                                    {
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode && y.Tus_itm_stus == _commonOutScan.ItemStatus).Sum(z => z.Tus_qty));
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_bqty -= Convert.ToInt32(_commonOutScan.SelectedItemList.First().Tus_qty));

                                    }
                                    else
                                    {
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode && y.Tus_itm_stus == _commonOutScan.ItemStatus).Sum(z => z.Tus_qty));
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_bqty -= _commonOutScan.SelectedItemList.Count);
                                    }


                                    //ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode).ToList().ForEach(x => x.Itri_bqty -= SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode).Sum(z => z.Tus_qty));
                                }
                                else
                                {
                                    //ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode && y.Tus_itm_stus == _commonOutScan.ItemStatus).Sum(z => z.Tus_qty));
                                    if (_itm.Mi_is_ser1 == -1)
                                    {
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode && y.Tus_itm_stus == _commonOutScan.ItemStatus).Sum(z => z.Tus_qty));
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_bqty -= Convert.ToInt32(_commonOutScan.SelectedItemList.First().Tus_qty));

                                    }
                                    else
                                    {
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _commonOutScan.ScanDocument && y.Tus_itm_cd == _commonOutScan.PopupItemCode && y.Tus_itm_stus == _commonOutScan.ItemStatus).Sum(z => z.Tus_qty));
                                        ScanItemList.Where(n => n.Itri_note == _commonOutScan.ScanDocument && n.Itri_itm_cd == _commonOutScan.PopupItemCode && n.Itri_itm_stus == _commonOutScan.ItemStatus).ToList().ForEach(x => x.Itri_bqty -= _commonOutScan.SelectedItemList.Count);
                                    }
                                }
                        gvItems.DataSource = null; gvItems.DataSource = ScanItemList;
                    }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; _commonOutScan.ShowDialog(); SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void OnRemoveFromSerialGrid(int _rowIndex)
        {
            try
            {
                int row_id = _rowIndex;
                if (string.IsNullOrEmpty(gvSerial.Rows[row_id].Cells["ser_Item"].Value.ToString())) return; string _item = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Item"].Value); string _status = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Status"].Value); Int32 _serialID = Convert.ToInt32(gvSerial.Rows[row_id].Cells["ser_SerialID"].Value);
                string _bin = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Bin"].Value); string serial_1 = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Serial1"].Value); string _requestno = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_requestno"].Value);
                decimal _qty = Convert.ToDecimal (gvSerial.Rows[row_id].Cells["ser_Qty"].Value); 
                UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, BaseCls.GlbUserComCode, _requestno, 0);
                RequestNo = _requestno; MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                { CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID), null, null); CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1); }
                else
                { CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, _item, _status); }

                //ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_note == _requestno).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - 1);
                //ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_note == _requestno).ToList().ForEach(x => x.Itri_bqty += 1);
                //add by tharanga
                if (_masterItem.Mi_is_ser1 == -1)
                {
                    ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _status && t.Itri_note == _requestno).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - _qty);
                    ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _status && t.Itri_note == _requestno).ToList().ForEach(x => x.Itri_bqty += _qty);
                }
                else
                {
                    ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_note == _requestno && t.Itri_itm_stus == _status).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - 1);
                    ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_note == _requestno && t.Itri_itm_stus == _status).ToList().ForEach(x => x.Itri_bqty += 1);

                }


                //ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _status && t.Itri_note == _requestno).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - 1);
                //ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _status && t.Itri_note == _requestno).ToList().ForEach(x => x.Itri_bqty += 1);

                BindPickSerials(UserSeqNo);
                if (ScanItemList != null) if (ScanItemList.Count > 0) { gvItems.DataSource = null; gvItems.DataSource = ScanItemList; } else BindMrnDetail(RequestNo);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); return; }
            finally { this.Cursor = Cursors.Default; }
        }
        private void gvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvSerial.RowCount > 0)
                {
                    int _rowCount = e.RowIndex; if (_rowCount != -1)
                    {
                        if (ddlType.SelectedValue.ToString() == "GRAN")
                        { MessageBox.Show("You can't remove picked serials for the GRAN.", "Picked", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (gvSerial.Columns[e.ColumnIndex].Name == "ser_Remove") if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) OnRemoveFromSerialGrid(_rowCount);
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void gvSerial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                { if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)                        OnRemoveFromSerialGrid(gvSerial.SelectedRows[0].Index); }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { InitializeForm(false, false); }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private bool IsAllScaned(List<ReptPickSerials> _list)
        {
            bool _isok = false;
            if (ScanItemList != null && _list != null)
            {
                foreach (DataGridViewRow _itm in gvItems.Rows)
                {
                    string _item = Convert.ToString(_itm.Cells["itm_Item"].Value); 
                    decimal _scanQty = Convert.ToDecimal(_itm.Cells["itm_PickQty"].Value); 
                    string _document = Convert.ToString(_itm.Cells["itm_requestno"].Value); 
                    string _status = Convert.ToString(_itm.Cells["itm_Status"].Value); 
                    decimal _serialcount = 0;
                    if (chkDirectOut.Checked == false)
                    { 
                        if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                            _serialcount = (from _l in _list where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document && _l.Tus_itm_stus == _status select _l.Tus_qty).Sum(); 
                        else _serialcount = (from _l in _list where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document && _l.Tus_itm_stus == _status select _l.Tus_qty).Sum(); 
                    }
                    else
                    {
                        // _serialcount = (from _l in _list where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document select _l.Tus_qty).Sum();  
                        _serialcount = (from _l in _list where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document && _l.Tus_itm_stus == _status select _l.Tus_qty).Sum(); 
                    }
                    if (_scanQty != _serialcount) 
                    { 
                        _isok = false; break; 
                    } 
                    else 
                        _isok = true;
                }
            }
            return _isok;
        }
        private bool IsBackDateOk()
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    { txtDate.Enabled = true; MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; }
                }
                else
                { txtDate.Enabled = true; MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; }
            }
            return _isOK;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                RequestNo = txtRequest.Text.ToString().Trim();
                if (ddlType.SelectedValue.ToString() == "GRAN")
                {
                    if (string.IsNullOrEmpty(ddlRecCompany.SelectedItem.ToString()))
                    {
                        MessageBox.Show("Please select the receiving company ", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        MessageBox.Show("Please select the receiving location", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!isValid(ddlRecCompany.SelectedItem.ToString(), txtDispatchRequried.Text.Trim())) { return; }
                }


                if (chkDirectOut.Checked)
                {
                    if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(Convert.ToString(ddlRecCompany.Text), txtDispatchRequried.Text.Trim());
                        if (_tbl == null || _tbl.Rows.Count <= 0)
                        { this.Cursor = Cursors.Default; MessageBox.Show("Dispatch location is invalid. Please check the location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (_tbl != null) if (_tbl.Rows.Count > 0)
                            {
                                string _fromcompany = BaseCls.GlbUserComCode; string _fromlocation = BaseCls.GlbUserDefLoca; string _tocompany = Convert.ToString(ddlRecCompany.Text); string _tocategory = _tbl.Rows[0].Field<string>("Ml_cate_3");
                                DataTable _adpoint = CHNLSVC.Inventory.GetSubLocation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                                if (_adpoint != null && _adpoint.Rows.Count > 0)
                                {
                                    var _one = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_loc_cd") == txtDispatchRequried.Text.Trim()).ToList(); var _two = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_main_loc_cd") == txtDispatchRequried.Text.Trim()).ToList(); if (_one.Count > 0 && _two.Count <= 0)
                                        goto xy;
                                    if (_one.Count <= 0 && _two.Count > 0) goto xy;
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue))) goto xy;
                                //DataTable _permission = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "AODOUT_DIRECT");
                                //if (_permission == null || _permission.Rows.Count <= 0)
                                //{ this.Cursor = Cursors.Default; MessageBox.Show("Permission Required for dispatch location. Please check the location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                                //Edit by Chamal 16-Sep-2014
                                DataTable _permCatwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "AODOUT_DIRECT");
                                DataTable _permLocwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, txtDispatchRequried.Text.Trim(), "AODOUT_DIRECT");
                                if (_isMigration == "N")
                                {
                                    if (_permLocwise == null || _permLocwise.Rows.Count <= 0)
                                    {
                                        if (_permCatwise == null || _permCatwise.Rows.Count <= 0)
                                        { this.Cursor = Cursors.Default; MessageBox.Show("Permission Required for dispatch location. Please check the location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                                    }
                                }
                            }
                    xy:
                        string _defualloc = BaseCls.GlbUserDefLoca; string _selectedLoc = txtDispatchRequried.Text.Trim(); this.Cursor = Cursors.WaitCursor;
                        try
                        {
                            if (ddlRecCompany.Text.ToString() == BaseCls.GlbUserComCode)
                            {
                                if (_defualloc.Trim() == _selectedLoc.Trim())
                                { this.Cursor = Cursors.Default; MessageBox.Show("You can not out to the same location", "Same Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                            } txtDispatchRequried.Enabled = false; cmbDirType.Enabled = false;
                        }
                        catch (Exception ex)
                        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
                        finally { this.Cursor = Cursors.Default; }
                    }
                }

                #region Priliminary Checking - 1
                if (CheckServerDateTime() == false) return;

                if (_isMigration == "N")
                {
                    if (MessageBox.Show("Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                }
                if (gvItems.RowCount <= 0)
                {
                    MessageBox.Show("Please select the items you required.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (gvSerial.RowCount <= 0)
                {
                    MessageBox.Show("Please select the serials you required.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region Job Close check
                if (chkServiceJob.Checked) //add by akila 2017/06/21
                {
                    JobNo = txtServiceJobNo.Text;
                    JobLine = Convert.ToInt16(ServiceJobLineNo);
                }

                if (_ServiceJobBase == true)
                {
                    if (CHNLSVC.CustService.check_IsJobClosed(JobNo, JobLine) == true)
                    {
                        this.Cursor = Cursors.Default; MessageBox.Show("Job Already Closed by Technician. Job # :" + JobNo, "Job Closed", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                    }
                }
                #endregion

                #region Back-Date check
                if (IsBackDateOk() == false) return;
                #endregion

                #region GRAN Check
                bool _isGRANfromDIN = false;
                bool _isGRAN = false;

                var _checkdRow = from DataGridViewRow _row in gvPending.Rows
                                 where Convert.ToBoolean(((DataGridViewCheckBoxCell)_row.Cells[0]).Value) == true
                                 select _row;
                foreach (DataGridViewRow _row in _checkdRow)
                {
                    string _reqnor = Convert.ToString(_row.Cells[1].Value);
                    if (!string.IsNullOrEmpty(_reqnor))
                    {
                        InventoryRequest _reqno = new InventoryRequest();
                        _reqno.Itr_req_no = _reqnor;
                        InventoryRequest _din = CHNLSVC.Inventory.GetInventoryRequestData(_reqno);
                        if (_din != null)
                            if (!string.IsNullOrEmpty(_din.Itr_com))
                            {
                                if (!string.IsNullOrEmpty(_din.Itr_anal1))
                                    _isGRANfromDIN = true;
                                else
                                    _isGRANfromDIN = false;

                                if (_din.Itr_tp == "GRAN")
                                    _isGRAN = true;
                                else
                                    _isGRAN = false;
                            }
                    }
                }
                #endregion

                #region Priliminary Checking - 2

                if (!chkDirectOut.Checked)
                    if (string.IsNullOrEmpty(txtRequest.Text) || txtRequest.Text == "N/A")
                    {
                        MessageBox.Show("Please select the direct check box or select request no", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                if (string.IsNullOrEmpty(ddlRecCompany.Text.ToString()))
                {
                    MessageBox.Show("Please select the receiving company");
                    ddlRecCompany.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDispatchRequried.Text.Trim()))
                {
                    MessageBox.Show("Please select the receiving location");
                    txtDispatchRequried.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    MessageBox.Show("Please select the date");
                    txtDate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    txtRequest.Text = "N/A";
                }

                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    txtRemarks.Text = "N/A";
                }

                if (string.IsNullOrEmpty(txtVehicle.Text))
                {
                    txtVehicle.Text = "N/A";
                }

                if (gvItems.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select the item");
                    return;
                }

                
                if (gvSerial.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select the serials");
                    return;
                }
                #endregion

                #region Manual Ref No validation
                if (chkManualRef.Checked)
                    if (string.IsNullOrEmpty(txtManualRef.Text))
                    {
                        MessageBox.Show("You do not enter a valid manual document no.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                #endregion

                string _requestno = RequestNo;
                Int32 _userSeqNo = UserSeqNo;

                InvoiceHeader _invoiceheader = new InvoiceHeader();
                InventoryHeader _inventoryHeader = new InventoryHeader();
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                MasterAutoNumber _inventoryAuto = new MasterAutoNumber();

                List<ReptPickSerials> _oldlst = null;
                List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();

                #region Collecting Data
                if (chkDirectOut.Checked == false)
                {
                    var query = (from DataGridViewRow row in gvPending.Rows
                                 where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells["pen_Select"]).Value) == true
                                 select row.Cells["pen_RequestNo"].Value.ToString()).ToList();
                    foreach (string _request in query)
                    {
                        UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, BaseCls.GlbUserComCode, _request, 0);
                        _oldlst = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, COM_OUT);
                        _reptPickSerials.AddRange(_oldlst);
                    }
                }
                else
                {
                    _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, COM_OUT);
                }
                #endregion

                #region Check for Scan serial with qty
                bool _isOk = IsAllScaned(_reptPickSerials);
                if (_isOk == false)
                {
                    MessageBox.Show("Scan serial count and the serial are mismatch");
                    return;
                }
                #endregion

                #region Check Inter-Company Parameter
                var _document = (from _doc in _reptPickSerials
                                 where _doc.Tus_new_remarks == "DO"
                                 select _doc.Tus_new_remarks).ToList();

                if (_document != null)
                    if (_document.Count > 0)
                    {

                        DataTable _adminT = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        if (_adminT == null || _adminT.Rows.Count <= 0)
                        { MessageBox.Show("Admin team not define", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        string _adminTeam = _adminT.Rows[0].Field<string>("ml_ope_cd");
                        if (string.IsNullOrEmpty(_adminTeam))
                        { MessageBox.Show("Admin team not define", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


                        List<InterCompanySalesParameter> _priceParam = CHNLSVC.Sales.GetInterCompanyParameter(_adminTeam, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRecCompany.Text.ToString(), string.Empty);
                        if (_priceParam == null || _priceParam.Count <= 0)
                        {
                            MessageBox.Show("There is no inter-company parameters define for the company  " + ddlRecCompany.Text.ToString() + " against " + BaseCls.GlbUserComCode + ".", "Inter-Company Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                #endregion


                #region Check Manual with Generated Documents
                bool _isInvalidDocument = false;
                if (chkManualRef.Checked)
                {
                    var _type = _reptPickSerials.Select(x => x.Tus_new_remarks).Distinct().ToList();
                    if (_type != null)
                        if (_type.Count > 0)
                        {
                            foreach (string _one in _type)
                            {
                                if (_one == "NON")
                                {
                                    MessageBox.Show("There is an item which can not identify to the document type automatically. Please contact IT Dept", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }

                            var _notfound = _type.Contains(ddlManType.Text.ToString().Trim());
                            if (_notfound == null)
                            {
                                if (MessageBox.Show("Manual document type and the system recognized document types are mismatching. Do you need to proceed this entry anyway?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    return;
                                else
                                {
                                    _isInvalidDocument = true;
                                }
                            }
                        }
                }

                #endregion

                #region Check Referance Date and the Doc Date
                if (IsReferancedDocDateAppropriate(_reptPickSerials, Convert.ToDateTime(txtDate.Text).Date) == false)
                    return;
                #endregion

                //List<ReptPickSerials> _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, COM_OUT);
                List<ReptPickSerialsSub> _reptPickSerialsSub = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, COM_OUT);

                //

                #region Check Duplicate Serials
                var _dup = _reptPickSerials.Where(x => x.Tus_ser_id != 0).Select(y => y.Tus_ser_id).ToList();

                string _duplicateItems = string.Empty;
                bool _isDuplicate = false;
                if (_dup != null)
                    if (_dup.Count > 0)
                        foreach (Int32 _id in _dup)
                        {
                            Int32 _counts = _reptPickSerials.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                            if (_counts > 1)
                            {
                                _isDuplicate = true;
                                var _item = _reptPickSerials.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                foreach (string _str in _item)
                                    if (string.IsNullOrEmpty(_duplicateItems))
                                        _duplicateItems = _str;
                                    else
                                        _duplicateItems += "," + _str;
                            }
                        }
                if (_isDuplicate)
                {
                    MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                    return;
                }
                #endregion

                #region Invoice Header Value Assign
                _invoiceheader.Sah_com = BaseCls.GlbUserComCode;
                _invoiceheader.Sah_cre_by = BaseCls.GlbUserID;
                _invoiceheader.Sah_cre_when = DateTime.Now;
                _invoiceheader.Sah_currency = "LKR";
                _invoiceheader.Sah_cus_add1 = string.Empty;
                _invoiceheader.Sah_cus_add2 = string.Empty;
                _invoiceheader.Sah_cus_cd = "CASH";
                _invoiceheader.Sah_cus_name = string.Empty;
                _invoiceheader.Sah_d_cust_add1 = string.Empty;
                _invoiceheader.Sah_d_cust_add2 = string.Empty;
                _invoiceheader.Sah_d_cust_cd = "CASH";
                _invoiceheader.Sah_direct = true;
                _invoiceheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
                _invoiceheader.Sah_epf_rt = 0;
                _invoiceheader.Sah_esd_rt = 0;
                _invoiceheader.Sah_ex_rt = 1;
                _invoiceheader.Sah_inv_no = "NA";
                _invoiceheader.Sah_inv_sub_tp = "SA"; //(Old Value - CS)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
                _invoiceheader.Sah_inv_tp = "INTR"; //(Old Value - CRED)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
                _invoiceheader.Sah_is_acc_upload = false;
                _invoiceheader.Sah_man_cd = string.Empty;
                _invoiceheader.Sah_man_ref = string.Empty;
                _invoiceheader.Sah_manual = false;
                _invoiceheader.Sah_mod_by = BaseCls.GlbUserID;
                _invoiceheader.Sah_mod_when = DateTime.Now;
                _invoiceheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invoiceheader.Sah_pdi_req = 0;
                _invoiceheader.Sah_ref_doc = string.Empty;
                _invoiceheader.Sah_remarks = string.Empty;
                _invoiceheader.Sah_sales_chn_cd = string.Empty;
                _invoiceheader.Sah_sales_chn_man = string.Empty;
                _invoiceheader.Sah_sales_ex_cd = string.Empty;
                _invoiceheader.Sah_sales_region_cd = string.Empty;
                _invoiceheader.Sah_sales_region_man = string.Empty;
                _invoiceheader.Sah_sales_sbu_cd = string.Empty;
                _invoiceheader.Sah_sales_sbu_man = string.Empty;
                _invoiceheader.Sah_sales_str_cd = string.Empty;
                _invoiceheader.Sah_sales_zone_cd = string.Empty;
                _invoiceheader.Sah_sales_zone_man = string.Empty;
                _invoiceheader.Sah_seq_no = 1;
                _invoiceheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invoiceheader.Sah_structure_seq = string.Empty;
                _invoiceheader.Sah_stus = "D";
                _invoiceheader.Sah_town_cd = string.Empty;
                _invoiceheader.Sah_tp = "INV";
                _invoiceheader.Sah_wht_rt = 0;
                
                #endregion

                #region Invoice AutoNumber

                _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PRO";
                _invoiceAuto.Aut_direction = null;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "CRED";
                _invoiceAuto.Aut_start_char = "CRED";
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                #endregion

                #region Inventory AutoNumber

                _inventoryAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _inventoryAuto.Aut_cate_tp = "LOC";
                _inventoryAuto.Aut_direction = null;
                _inventoryAuto.Aut_modify_dt = null;
                _inventoryAuto.Aut_moduleid = string.Empty;
                _inventoryAuto.Aut_start_char = string.Empty;
                _inventoryAuto.Aut_modify_dt = null;
                _inventoryAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                #endregion

                #region Inventory Header Value Assign
                _inventoryHeader.Ith_acc_no = string.Empty;
                _inventoryHeader.Ith_anal_1 = string.Empty;
                _inventoryHeader.Ith_anal_10 = chkDirectOut.Checked ? true : false;//Direct AOD
                _inventoryHeader.Ith_anal_11 = false;
                _inventoryHeader.Ith_anal_12 = false;
                _inventoryHeader.Ith_anal_2 = string.Empty;
                _inventoryHeader.Ith_anal_3 = string.Empty;
                _inventoryHeader.Ith_anal_4 = string.Empty;
                _inventoryHeader.Ith_anal_5 = string.Empty;
                _inventoryHeader.Ith_anal_6 = 0;
                _inventoryHeader.Ith_anal_7 = 0;
                _inventoryHeader.Ith_anal_8 = Convert.ToDateTime(txtDate.Text).Date;
                _inventoryHeader.Ith_anal_9 = Convert.ToDateTime(txtDate.Text).Date;
                _inventoryHeader.Ith_bus_entity = string.Empty;
                _inventoryHeader.Ith_cate_tp = string.Empty;
                _inventoryHeader.Ith_channel = string.Empty;
                _inventoryHeader.Ith_com = BaseCls.GlbUserComCode;
                _inventoryHeader.Ith_com_docno = string.Empty;
                _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
                _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
                _inventoryHeader.Ith_del_add1 = string.Empty;
                _inventoryHeader.Ith_del_add2 = string.Empty;
                _inventoryHeader.Ith_del_code = string.Empty;
                _inventoryHeader.Ith_del_party = string.Empty;
                _inventoryHeader.Ith_del_town = string.Empty;
                _inventoryHeader.Ith_direct = false;
                _inventoryHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
                _inventoryHeader.Ith_doc_no = string.Empty;
                _inventoryHeader.Ith_doc_tp = string.Empty;
                _inventoryHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Date.Year;
                _inventoryHeader.Ith_entry_no = string.Empty;
                _inventoryHeader.Ith_entry_tp = string.Empty;
                _inventoryHeader.Ith_git_close = false;
                _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(txtDate.Text).Date;
                _inventoryHeader.Ith_git_close_doc = string.Empty;
                _inventoryHeader.Ith_is_manual = chkManualRef.Checked ? true : false;
                _inventoryHeader.Ith_isprinted = false;
                _inventoryHeader.Ith_job_no = string.Empty;
                _inventoryHeader.Ith_loading_point = string.Empty;
                _inventoryHeader.Ith_loading_user = string.Empty;
                _inventoryHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                _inventoryHeader.Ith_manual_ref = txtManualRef.Text;
                _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
                _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
                _inventoryHeader.Ith_noofcopies = 0;
                _inventoryHeader.Ith_oth_loc = txtDispatchRequried.Text.Trim();
                _inventoryHeader.Ith_oth_docno = chkDirectOut.Checked ? string.Empty : _requestno;
                _inventoryHeader.Ith_remarks = txtRemarks.Text;
                _inventoryHeader.Ith_sbu = string.Empty;
                //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
                _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                _inventoryHeader.Ith_stus = "A";
                _inventoryHeader.Ith_sub_tp = string.Empty;
                _inventoryHeader.Ith_vehi_no = string.Empty;
                _inventoryHeader.Ith_oth_com = ddlRecCompany.Text.ToString();
                _inventoryHeader.Ith_anal_1 = _isInvalidDocument == true ? "1" : "0";
                _inventoryHeader.Ith_anal_2 = chkManualRef.Checked ? ddlManType.Text : string.Empty;
                _inventoryHeader.GRAN_UpdateReqStatus = true;

                if (_ServiceJobBase == true)
                {
                    //_inventoryHeader.Ith_isjobbase = true;
                    _inventoryHeader.Ith_job_no = JobNo;
                    _inventoryHeader.Ith_cate_tp = "SERVICE";
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_sub_docno = JobNo;
                }
                #endregion

                //Add by akila 2017/05/16
                //Update _reptPickSerials with service job and line no.
                if ((chkDirectOut.Checked) && (chkServiceJob.Checked))
                {
                    if ((_reptPickSerials != null) && (_reptPickSerials.Count > 0))
                    {
                        _reptPickSerials.ForEach(x =>
                                                        {
                                                            x.Tus_job_no = txtServiceJobNo.Text;
                                                            x.Tus_job_line = ServiceJobLineNo;
                                                        });

                        //if((SelectedJobLine != null) && (SelectedJobLine.Count > 0))
                        //{
                        //    string _tmpJobNo = string.Empty;
                        //    int _tmpJobLineNo = 0;

                        //    _tmpJobNo = SelectedJobLine[0] == null ? string.Empty : SelectedJobLine[0].ToString();
                        //    _tmpJobLineNo = SelectedJobLine[2] == null ? 0 : Convert.ToInt32(SelectedJobLine[2]);

                        //    _reptPickSerials.ForEach(x => 
                        //                                { 
                        //                                    x.Tus_job_no = _tmpJobNo; 
                        //                                    x.Tus_job_line = _tmpJobLineNo; 
                        //                                });
                        //}
                    }                   
                }

                string _message = string.Empty;
                string _genInventoryDoc = string.Empty;
                string _genSalesDoc = string.Empty;
                this.Cursor = Cursors.WaitCursor;

                #region Save Process
                //Process
                if (ddlRecCompany.Text.ToString() != BaseCls.GlbUserComCode)
                {
                    _inventoryHeader.Ith_process_name = "othcomout";
                }
                Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRecCompany.Text.ToString(), _requestno, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, _reptPickSerialsSub, out _message, out _genSalesDoc, out _genInventoryDoc, _isGRAN, _isGRANfromDIN);
                string Msg = string.Empty;
                if (!string.IsNullOrEmpty(_genInventoryDoc)) _genInventoryDoc.Trim().Remove(_genInventoryDoc.Length - 1);
                if (!string.IsNullOrEmpty(_genSalesDoc)) _genSalesDoc.Trim().Remove(_genSalesDoc.Length - 1);


                if (_effect == -1)
                {
                    this.Cursor = Cursors.Default;
                    if (_message.Contains("EMS.CHK_INLFREEQTY"))
                    {
                        MessageBox.Show("There is no free stock balance available." + "\n" + "Please check the stock balances.", "No Free Location Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (_message.Contains("EMS.CHK_INBFREEQTY"))
                    {
                        MessageBox.Show("There is no free stock balance available." + "\n" + "Please check the stock balances.", "No Free Batch Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please check the issues of " + _message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    //Edit by Chamal 20-Dec-2017 (Add Caption, MessageBoxButtons and MessageBoxIcon)
                    //MessageBox.Show("Successfully processed!. Document No(s) - " + _genInventoryDoc + "  " + _genSalesDoc,"Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    if (_isMigration == "N")
                    {
                        if (MessageBox.Show("Successfully processed!. Document No(s) -  " + _genInventoryDoc + ".\n Do you want to print now?", "Process Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            #region Printing
                            if (chkManualRef.Checked == false)
                            {
                                string[] docs = _genInventoryDoc.Split(',');
                                if (docs.Length > 0)
                                {
                                    foreach (string _doc in docs)
                                    {
                                        if (!string.IsNullOrEmpty(_doc))
                                            if (!_doc.Contains("ADJ"))
                                            {
                                                BaseCls.GlbReportTp = "OUTWARD"; //Sanjeewa
                                                ReportViewerInventory _views = new ReportViewerInventory();
                                                BaseCls.GlbReportName = string.Empty;
                                                GlbReportName = string.Empty;
                                                _views.GlbReportName = string.Empty;
                                                _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : "Outward_Docs.rpt";
                                                _views.GlbReportDoc = _doc;
                                                _views.Show();
                                                _views = null;
                                            }

                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #region Clear Screen
                    this.Cursor = Cursors.WaitCursor;
                    while (this.Controls.Count > 0)
                    {
                        Controls[0].Dispose();
                    }
                    _commonOutScan = new CommonSearch.CommonOutScan();
                    InitializeComponent();
                    InitializeForm(true, true);
                    _commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
                    this.Cursor = Cursors.Default;
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex); return;
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                { Controls[0].Dispose(); }
                _commonOutScan = new CommonSearch.CommonOutScan(); InitializeComponent();
                InitializeForm(true, true); _commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
                _unFinishedDirectDocument = new DataTable(); BackDatePermission(); this.Cursor = Cursors.Default;

                //Add by akila 
                txtServiceJobNo.Enabled = false;
                btnSearchJob.Enabled = false;
                chkServiceJob.Visible = false;
                txtServiceJobNo.Visible = false;
                lblJobNo.Visible = false;
                btnSearchJob.Visible = false;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void InterCompanyOutWardEntry_Load(object sender, EventArgs e)
        {
            try
            {
                //Add by akila 
                chkServiceJob.Visible = false;
                txtServiceJobNo.Visible = false;
                lblJobNo.Visible = false;
                btnSearchJob.Visible = false;
                txtServiceJobNo.Enabled = false;
                btnSearchJob.Enabled = false;

                BackDatePermission(); 
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void LoadUnFinishedDirectDocument()
        {
            if (_unFinishedDirectDocument == null) _unFinishedDirectDocument = new DataTable();
            _unFinishedDirectDocument = CHNLSVC.Inventory.GetDirectUnFinishedDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, COM_OUT);
            if (_unFinishedDirectDocument != null && _unFinishedDirectDocument.Rows.Count > 0)
            {
                DataRow _rows = null; _rows = _unFinishedDirectDocument.NewRow(); _unFinishedDirectDocument.Rows.InsertAt(_rows, 0); cmbDirectScan.DisplayMember = "tuh_usrseq_no";
                cmbDirectScan.ValueMember = "tuh_usrseq_no"; cmbDirectScan.DataSource = _unFinishedDirectDocument; 
            }
        }
        private void cmbDirectScan_Leave(object sender, EventArgs e)
        {
            SelectedSerialList = new List<ReptPickSerials>(); ScanItemList = new List<InventoryRequestItem>();
            _itemdetail = new MasterItem(); serial_list = new List<ReptPickSerials>(); gvPending.AutoGenerateColumns = false;
            gvItems.AutoGenerateColumns = false; gvSerial.AutoGenerateColumns = false;
            if (!string.IsNullOrEmpty(Convert.ToString(cmbDirectScan.SelectedValue)))
            {
                RequestNo = Convert.ToString(cmbDirectScan.SelectedValue); UserSeqNo = Convert.ToInt32(RequestNo);
                var _docdet = _unFinishedDirectDocument.AsEnumerable().Where(y => !string.IsNullOrEmpty(y.Field<string>("tuh_usr_id"))).Where(x => x.Field<Int64>("tuh_usrseq_no") == UserSeqNo && x.Field<string>("tuh_usr_loc") == BaseCls.GlbUserDefLoca).CopyToDataTable();
                if (_docdet != null && _docdet.Rows.Count > 0)
                {
                    ddlRecCompany.DataSource = null; txtDispatchRequried.Text = string.Empty;
                    string _reccompany = _docdet.Rows[0].Field<string>("tuh_rec_com"); string _reclocation = _docdet.Rows[0].Field<string>("tuh_rec_loc"); string _recdirtype = _docdet.Rows[0].Field<string>("tuh_dir_type");
                    ddlRecCompany.Items.Add(_reccompany); ddlRecCompany.SelectedIndex = 0; txtDispatchRequried.Text = _reclocation;
                    //Edit by Chamal 20-Dec-2017
                    if (!string.IsNullOrEmpty(_recdirtype))
                    {
                        cmbDirType.SelectedValue = _recdirtype;
                    }
                    ddlRecCompany.Enabled = false; txtDispatchRequried.Enabled = false; cmbDirType.Enabled = false; string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                }
                else
                { MessageBox.Show("Selected unfinished document is not valid.", "Invalid Direct Document", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            }
            else
            { BindReceiveCompany(); ddlRecCompany.SelectedValue = BaseCls.GlbUserComCode; UserSeqNo = -1; RequestNo = "-1"; ddlRecCompany.Enabled = true; txtDispatchRequried.Enabled = true; cmbDirType.Enabled = true; }
            BindPickSerials(UserSeqNo);
            BindDirectDetail(RequestNo);

        }

        private void gvItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (chkChangeSimilarItem.Checked == true)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();

                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    string _old_item = gvItems.Rows[e.RowIndex].Cells["itm_Item"].Value.ToString();
                    string _new_item = txtItem.Text;
                    gvItems.Rows[e.RowIndex].Cells["itm_Item"].Value = txtItem.Text;
                    txtItem.Clear();
                    CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(gvItems.Rows[e.RowIndex].Cells["itm_requestno"].Value.ToString(), Convert.ToInt32(gvItems.Rows[e.RowIndex].Cells["itm_Lineno"].Value.ToString()), _old_item, _new_item);
                }
            }
            if (chkChangeStatus.Checked)
            {
                string _old_item = gvItems.Rows[e.RowIndex].Cells["itm_Item"].Value.ToString();
                DataTable dtStatus = CHNLSVC.Inventory.GET_ITMSTATUS_BY_LOC_ITM(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _old_item);
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    pnlItmStatus.Visible = true;
                    lstItmStatus.DataSource = dtStatus;
                    lstItmStatus.DisplayMember = "INL_ITM_STUS";
                    lstItmStatus.ValueMember = "INL_ITM_STUS";
                }
            }
        }

        private void btnSearchUsr_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqBy;
                _CommonSearch.ShowDialog();

                txtReqBy.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtReqBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchUsr_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnDocSearch.Focus();
            }
        }

        private void txtReqBy_DoubleClick(object sender, EventArgs e)
        {
            btnSearchUsr_Click(null, null);
        }

        private void btnCloseStuPnl_Click(object sender, EventArgs e)
        {
            pnlItmStatus.Visible = false;
        }

        private void lstItmStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstItmStatus_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstItmStatus.SelectedItem != null)
            {
                if (MessageBox.Show("Do you want to update the item status", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string invoiceNumber = gvItems.SelectedRows[0].Cells["itm_requestno"].Value.ToString();
                    Int32 lineNumber = Convert.ToInt32(gvItems.SelectedRows[0].Cells["itm_Lineno"].Value.ToString());
                    string _old_item = gvItems.SelectedRows[0].Cells["itm_Item"].Value.ToString();
                    String newStatus = lstItmStatus.SelectedValue.ToString();

                    String err;
                    Int32 result = CHNLSVC.Inventory.UPDATE_ITM_STUS(invoiceNumber, lineNumber, _old_item, newStatus, out   err);
                    if (result > 0)
                    {
                        MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlItmStatus.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Err : " + err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void gvPending_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Add by akila 2017/01/10
        private bool isValid(string _recevingComapny, string _recevingLocation)
        {
            bool _isValid = true;

            try
            {
                IEnumerable<DataRow> ItemStatus = CHNLSVC.Inventory.GetItemStatusDefinition_byLocation(_recevingComapny, _recevingLocation).AsEnumerable();
                if (ItemStatus != null)
                {
                    List<string> _availableItemStatus = ItemStatus.Select(x => x.Field<string>("mis_desc")).ToList();
                    if (gvItems.Rows.Count > 0)
                    {
                        // BindingSource _bindSource = (BindingSource)gvItems.DataSource;
                        List<InventoryRequestItem> _requestedSerails = new List<InventoryRequestItem>();
                        // _requestedSerails = _bindSource.DataSource as List<InventoryRequestItem>;
                        _requestedSerails = gvItems.DataSource as List<InventoryRequestItem>;

                        if (_requestedSerails != null)
                        {
                            foreach (InventoryRequestItem _item in _requestedSerails)
                            {
                                int _count = ItemStatus.Where(x => x.Field<string>("mlas_itm_stus") != _item.Itri_itm_stus).Count();
                                if (_count > 0)
                                {
                                    MessageBox.Show("Invalid operation. Items cannot be transfered" + Environment.NewLine + "Selected location only have permission to transfer items having " + string.Join(", ", _availableItemStatus) + " status", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select an item", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating item details." + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _isValid;
        }

        private void btnCloseAppSer_Click(object sender, EventArgs e)
        {
            pnlAppSer.Visible = false;
        }

        private void lnkAppSer_Click(object sender, EventArgs e)
        {
            pnlAppSer.Visible = true;
        }

        //Add by akila  2017/05/15
        private void ValidateServiceLocation(string _location)
        {
            try
            {
                DataTable _locationDetails = new DataTable();
                _locationDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, _location);
                if (_locationDetails.Rows.Count > 0)
                {
                    foreach (DataRow _loc in _locationDetails.Rows)
                    {
                        string _tmpLocType = _loc["ml_loc_tp"] == DBNull.Value ? string.Empty : _loc["ml_loc_tp"].ToString();
                        if (_tmpLocType == "SERC")
                        {
                            txtServiceJobNo.Location = new Point(418, 28);
                            lblJobNo.Location = new Point(418, 15);
                            btnSearchJob.Location = new Point(575, 28);

                            chkServiceJob.Visible = true;
                            txtServiceJobNo.Visible = true;
                            lblJobNo.Visible = true;
                            btnSearchJob.Visible = true;
                            btnSearchJob.Focus();
                        }
                    }                    
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.IsReturnFullRow = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobsWithStage);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceJobInStage(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtServiceJobNo;
                _CommonSearch.ShowDialog();
                txtServiceJobNo.Select();
                txtServiceJobNo.Focus();

                //commented by akila
                //if (!string.IsNullOrEmpty(txtServiceJobNo.Text))
                //{
                //    SelectedJobLine = _CommonSearch.UserSelectedRow.Split(new string[] { "|" }, StringSplitOptions.None).ToList();
                //}


            }
            catch (Exception ex)
            { 
                this.Cursor = Cursors.Default; SystemErrorMessage( new Exception("An error occurred while loading job details " + ex.Message)); 
            }
            finally 
            { 
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); 
            }
        }

        private void chkServiceJob_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServiceJob.Checked)
            {
                txtServiceJobNo.Enabled = true;
                btnSearchJob.Enabled = true;
                _ServiceJobBase = true;
            }
            else 
            {
                txtServiceJobNo.Enabled = false;
                btnSearchJob.Enabled = false;
                _ServiceJobBase = false;
            }
        }

        private void txtServiceJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchJob_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSearchJob.Focus();
            }
        }

        private void txtServiceJobNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearchJob_Click(null, null);
        }

        private void txtServiceJobNo_Leave(object sender, EventArgs e)
        {
            if (! string.IsNullOrEmpty(txtServiceJobNo.Text))
            {
                ServiceJobLineNo = GetJobLineNo();                
            }
        }

        private int GetJobLineNo()
        {
            Int32 _lineNo = 0;

            try
            {
                DataTable _serviceDetails = new DataTable();
                _serviceDetails = CHNLSVC.CustService.GetJobDetailsWithStage(BaseCls.GlbUserComCode, txtDispatchRequried.Text, txtServiceJobNo.Text);
                if (_serviceDetails.Rows.Count > 0)
                {
                    foreach (DataRow _service in _serviceDetails.Rows)
                    {
                        _lineNo = _service["Line No"] == null ? 0 : Convert.ToInt32(_service["Line No"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading job line details." + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtServiceJobNo.Focus();
            }
            return _lineNo;
        }

        private void gvPending_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int count = 0;
            for (count = 0; count <= gvPending.Rows.Count - 1; count++)
            {
                if (Convert.ToBoolean(gvPending.Rows[count].Cells["pen_Select"].Value) == true)
                {

                    txtRequest.Text = Convert.ToString(gvPending.Rows[count].Cells["pen_RequestNo"].Value);
                }
            }
        }

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;            
            _direction = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "COM_OUT", _direction, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "COM_OUT";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)//direction always (-) for change status
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = generated_seq.ToString();
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                //txtUserSeqNo.Text = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        private void btn_migration_Click(object sender, EventArgs e)
        {
            int _direction = 0;
            string _seqNo = "";
            UserSeqNo = -1;
            _isMigration = "Y";
            
            DataTable _costdtl = CHNLSVC.Inventory.get_CostUpdateDetails();
            DataTable _costdtl_loc = _costdtl.DefaultView.ToTable(true, "loc_code");

            if (_costdtl_loc.Rows.Count > 0)
            {
                foreach (DataRow drow in _costdtl_loc.Rows)
                {                    
                    BaseCls.GlbUserDefLoca=drow["loc_code"].ToString();

                    var _costdtl_filterloc = (from tem in _costdtl.AsEnumerable()
                                              where (tem["loc_code"].ToString() == drow["loc_code"].ToString())
                                              group tem by new { LOC = tem["loc_code"], LOCDESC = tem["loc_desc"], ITEMCODE = tem["item_code"], ITEMSTATUS = tem["item_status"], ITEMCOST = tem["unit_cost"] } into ss
                                              select new
                                              {
                                                  LOC_CODE = ss.Key.LOC,
                                                  LOC_DESC = ss.Key.LOCDESC,
                                                  ITEM_CODE = ss.Key.ITEMCODE,
                                                  ITEM_STATUS = ss.Key.ITEMSTATUS,
                                                  ITEM_COST = ss.Key.ITEMCOST,                                                  
                                                  ITEM_QTY = ss.Sum(tem => Convert.ToDecimal(tem["qty"]))
                                              }).ToList();

                    //ADJ Out -----------------------------------
                    ScanItemList.Clear();
                    txtDate.Value = Convert.ToDateTime("28/FEB/2018").Date;
                    _direction = 0;
                    chkDirectOut.Checked = true;
                    ddlRecCompany.Text = "SGL";
                    txtDispatchRequried.Text = "SGCOM"; 
                    txtRemarks.Text = "SGD-SGL Migration - 2018-02-28";

                    //UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, BaseCls.GlbUserComCode, _requestno, 0);
                    UserSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "COM_OUT", BaseCls.GlbUserID, _direction, _seqNo);
                    if (UserSeqNo == -1)
                    {
                        UserSeqNo = GenerateNewUserSeqNo();
                    }
                    RequestNo = UserSeqNo.ToString();

                    for (int i = 0; i < _costdtl_filterloc.Count; i++)
                    {
                        txtItem.Text = _costdtl_filterloc[i].ITEM_CODE.ToString();
                        LoadItemStatus();
                        //txtSup.Text = _costdtl_filterloc[i].SUPP_CODE.ToString();
                        ddlStatus.SelectedValue = _costdtl_filterloc[i].ITEM_STATUS.ToString();
                        //txtUnitCost.Text = _costdtl_filterloc[i].ITEM_COST.ToString();
                        txtQty.Text = _costdtl_filterloc[i].ITEM_QTY.ToString();                        
                        //txtBatch.Text = "";
                        //txtGRNno.Text = _costdtl_filterloc[i].GRN_NO.ToString();
                        //dtGRN.Value = Convert.ToDateTime(_costdtl_filterloc[i].GRN_DT.ToString());
                        btnAddItem_Click(null, null);
                        //AddItem();

                        

                        var _costdtl_serails = _costdtl.AsEnumerable().Where(X => X.Field<string>("loc_code") == drow["loc_code"].ToString() && X.Field<string>("item_code") == _costdtl_filterloc[i].ITEM_CODE.ToString() && X.Field<string>("item_status") == _costdtl_filterloc[i].ITEM_STATUS.ToString() &&
                            X.Field<decimal>("unit_cost") == Convert.ToDecimal(_costdtl_filterloc[i].ITEM_COST.ToString())).Select(s => new
                            {
                                loc_code = s.Field<string>("loc_code"),
                                loc_desc = s.Field<string>("loc_desc"),
                                item_code = s.Field<string>("item_code"),
                                unit_cost = s.Field<decimal>("unit_cost"),
                                new_unit_cost = s.Field<decimal>("new_unit_cost"),
                                ser_id = s.Field<decimal>("ser_id"),
                                ser_no = s.Field<string>("ser_no"),
                                qty = s.Field<decimal>("qty"),
                                grn_dt = s.Field<DateTime>("grn_dt"),
                                item_status = s.Field<string>("item_status"),
                                org_supp = s.Field<string>("org_supp"),
                                exist_supp = s.Field<string>("exist_supp"),
                                ins_bin = s.Field<string>("ins_bin"),
                                doc_no = s.Field<string>("doc_no"),
                                doc_line = s.Field<decimal>("doc_line")
                            }).Distinct().ToList();

                        for (int j = 0; j < _costdtl_serails.Count; j++)
                        {
                            MasterItem _itms = new MasterItem();
                            _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _costdtl_serails[j].item_code.ToString());
                            if (_itms.Mi_is_ser1 != -1)
                            {
                                int rowCount = 0;

                                Int32 serID = Convert.ToInt32(_costdtl_serails[j].ser_id.ToString());
                                string binCode = _costdtl_serails[j].ins_bin.ToString();

                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, _costdtl_serails[j].item_code.ToString(), serID);
                                //Update_inrser_INS_AVAILABLE
                                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _costdtl_serails[j].item_code.ToString(), serID, -1);

                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = _costdtl_serails[j].doc_no.ToString();
                                _reptPickSerial_.Tus_base_itm_line =i+1;
                                _reptPickSerial_.Tus_itm_desc = _itms.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = _itms.Mi_model;
                                _reptPickSerial_.Tus_job_no = "";
                                _reptPickSerial_.Tus_pgs_prefix = "";
                                _reptPickSerial_.Tus_job_line = 0;

                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                rowCount++;

                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = _costdtl_serails[j].doc_no.ToString();
                                _reptPickSerial_.Tus_base_itm_line = i+1;
                                _reptPickSerial_.Tus_itm_desc = _itms.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = _itms.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = _costdtl_filterloc[i].ITEM_CODE.ToString();
                                _reptPickSerial_.Tus_itm_stus = _costdtl_filterloc[i].ITEM_COST.ToString();
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_costdtl_serails[j].doc_line.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_unit_cost = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_job_no = "";
                                _reptPickSerial_.Tus_pgs_prefix = "";
                                _reptPickSerial_.Tus_job_line = 0;
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                        }
                    }
                    
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, "COM_OUT");
                    
                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    foreach (InventoryRequestItem _addedItem in ScanItemList)
                    {
                        var _basedocno = (from _l in _serList where _l.Tus_itm_cd == _addedItem.Itri_itm_cd && _l.Tus_itm_stus == _addedItem.Itri_itm_stus select _l.Tus_base_doc_no).ToList();
                        for (int j = 0; j < _basedocno.Count; j++)
                        {

                            ReptPickItems _reptitm = new ReptPickItems();
                            _reptitm.Tui_usrseq_no = UserSeqNo;
                            _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                            _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                            _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                            _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                            _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                            _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                            _reptitm.Tui_sup = "";
                            _reptitm.Tui_grn = _basedocno[j].ToString();
                            _reptitm.Tui_batch = "" ;
                            _reptitm.Tui_grn_dt = txtDate.Value.Date;

                            _saveonly.Add(_reptitm);
                        }
                    }
                    CHNLSVC.Inventory.SavePickedItems(_saveonly);
                    
                    
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                    _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);

                    foreach (ReptPickItems _reptitem in _reptItems)
                    {
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        MasterItem _itms = new MasterItem();
                        _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reptitem.Tui_req_itm_cd);
                        _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                        _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                        _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                        _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                        _itm.Itri_supplier = _reptitem.Tui_sup;
                        _itm.Itri_batchno = _reptitem.Tui_batch;
                        _itm.Itri_grnno = _reptitem.Tui_grn;
                        _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                        _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                        _itm.Itri_note = _reptitem.Tui_grn;
                        _itmList.Add(_itm);                        
                    }

                    ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = ScanItemList;
                    
                    if (_serList != null)
                    {
                        if (_direction == 0)
                        {
                            //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (DataGridViewRow row in this.gvItems.Rows)
                                {
                                    if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                                    {
                                        row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                    }
                                }
                            }
                        }
                        else
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (DataGridViewRow row in this.gvItems.Rows)
                                {
                                    if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                                    {
                                        row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                    }
                                }
                            }
                        }
                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = _serList;

                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = emptyGridList;

                    }
                    //btnSave_Click(null, null);

                    //ADJ In -----------------------------------
                    //_costdtl_filterloc1.Clear();
                    //ScanItemList.Clear();

                }

            }
            MessageBox.Show("Completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

       
    }
}
