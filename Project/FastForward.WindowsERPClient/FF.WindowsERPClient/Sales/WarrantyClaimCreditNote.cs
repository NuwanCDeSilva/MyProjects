using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;


namespace FF.WindowsERPClient.Sales
{
    public partial class WarrantyClaimCreditNote : FF.WindowsERPClient.Base
    {

        public bool IsApproveUser;
        private string CustomerCompany = string.Empty;
        private bool _isDecimalAllow = false;
        MasterItem _itemdetail = null;
        List<RequestApprovalDetail> _detail = null;
        bool itemAdding = false;
        string InvoiceItemLine = string.Empty;
        string RequestNo = string.Empty;
        string CustomerCode = string.Empty;
        DateTime RequestDate;
        string AdjustmentNo = string.Empty;


        #region Form Initialize
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel(); 
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LoadCustomerDetail(string _customer)
        {
            if (string.IsNullOrEmpty(_customer))
            {
                lblCus_Address.Text = string.Empty;
                lblCus_Mobile.Text = string.Empty;
                lblCus_Name.Text = string.Empty;
                return;
            }

            MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(CustomerCompany, _customer, string.Empty, string.Empty, "C");
            if (_masterBusinessCompany == null || string.IsNullOrEmpty(_masterBusinessCompany.Mbe_com)) { lblCus_Address.Text = string.Empty; lblCus_Mobile.Text = string.Empty; lblCus_Name.Text = string.Empty; return; }
            lblCus_Address.Text = _masterBusinessCompany.Mbe_add1 + " " + _masterBusinessCompany.Mbe_add2;
            lblCus_Mobile.Text = _masterBusinessCompany.Mbe_mob;
            lblCus_Name.Text = _masterBusinessCompany.Mbe_name;

        }
        private void BindUserCompanyItemStatusDDLData(ComboBox ddl)
        {
            DataTable _tbl = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            //var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            //_s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIC_CD";
        }
        private void UserPermissionforDirectOut()
        {

            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, CommonUIDefiniton.UserPermissionType.WARCLC.ToString()))
            {
                //Approved User
                IsApproveUser = true;
                btnSave.Enabled = true;
                btnRequest.Enabled = false;
                pnlItemAdd.Enabled = false;
                pnlReceiving.Enabled = true;
            }
            else
            {
                //Request User
                IsApproveUser = false;
                btnSave.Enabled = false;
                btnRequest.Enabled = true;
                pnlItemAdd.Enabled = true;
                pnlReceiving.Enabled = false;
            }
        }
        private void InitializeForm()
        {
            RequestNo = string.Empty;
            InvoiceItemLine = string.Empty;
            IsApproveUser = false;
            CustomerCompany = string.Empty;
            _detail = new List<RequestApprovalDetail>();
            CustomerCode = string.Empty;
            AdjustmentNo = string.Empty;

            UserPermissionforDirectOut();
            pnlSerial.Size = new Size(601, 157);
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");

            DataTable _tbl = new DataTable();
            if (!IsApproveUser) _tbl = CHNLSVC.Sales.LoadWarrantyClaimCreditNote(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_tbl.Rows.Count > 0)
            {
                var _type = _tbl.AsEnumerable().Select(x => x.Field<string>("sarsp_doc_tp")).Distinct().ToList();
                var _supplier = _tbl.AsEnumerable().Select(x => x.Field<string>("sarsp_sup_cd")).Distinct().ToList();
                cmbInvType.DataSource = _type;
                cmbSupplier.DataSource = _supplier;
                var _customer = _tbl.AsEnumerable().Where(x => x.Field<string>("sarsp_sup_cd") == cmbSupplier.Text.Trim()).ToList().Select(x => x.Field<string>("sarsp_cus_cd")).Distinct().ToList();
                cmbCustomer.DataSource = _customer;
                var _customerCompany = _tbl.AsEnumerable().Where(x => x.Field<string>("sarsp_cus_cd") == cmbCustomer.Text.Trim()).ToList().Select(x => x.Field<string>("sarsp_cus_com")).Distinct().ToList();
                if (_customerCompany != null)
                    if (_customerCompany.Count > 0)
                        CustomerCompany = Convert.ToString(_customerCompany[0]);
                if (!string.IsNullOrEmpty(CustomerCompany)) LoadCustomerDetail(cmbCustomer.Text.Trim());
            }
            if (IsApproveUser)
            {
                _tbl = CHNLSVC.Inventory.GetWarrantyClaimReqyest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT016.ToString(), 1, "P", Convert.ToDateTime(txtFromDt.Value).Date, Convert.ToDateTime(txtToDt.Value).Date);
                BindingSource _source = new BindingSource();
                _source.DataSource = _tbl;
                gvPending.DataSource = _source;
                btnPrint.Enabled = true;
            }
            else
            {
                _tbl = CHNLSVC.Inventory.GetWarrantyClaimReqyest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT016.ToString(), 0, "P", Convert.ToDateTime(txtFromDt.Value).Date, Convert.ToDateTime(txtToDt.Value).Date);
                BindingSource _source = new BindingSource();
                _source.DataSource = _tbl;
                gvPending.DataSource = _source;
                btnPrint.Enabled = false;
            }

            gvInvoiceItem.AutoGenerateColumns = false;
            cmbItemType.SelectedIndex = 0;

            //Add by akila 
            txtSapNo.ReadOnly = false;
            txtClaimNo.ReadOnly = false;

        }
        public WarrantyClaimCreditNote()
        {
            InitializeComponent();
            try
            {
                InitializeForm();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;

            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                    lblItemDescription.Text = "Description : " + _description;
                    lblItemModel.Text = "Model : " + _model;
                    lblItemBrand.Text = "Brand : " + _brand;
                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                }

            return _isValid;
        }
        #endregion

        #region Rooting for Load Warranty Detail
        private bool GetWarrantyDetail(string _serialID)
        {
            lblWara_Period.Text = string.Empty;
            lblWara_StDate.Text = string.Empty;
            pnlWarranty.BackColor = Color.Lavender;
            bool _correct = false;

            if (_serialID == "-1")
            {
                _correct = false;
                return _correct;
            }

            DataTable _tbl = CHNLSVC.Sales.GetInvoiceServiceItemSerDet(cmbInvoice.Text.Trim(), _serialID);
        //    DataTable _tbl = CHNLSVC.Inventory.GetSCMWarranty(txtItem.Text.Trim(), _serialID, cmbInvoice.Text.Trim());

            if (_tbl.Rows.Count <= 0)
            {
                MessageBox.Show("There is no warranty details available. Please check with IT Dept", "No Warranty Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _correct = false;
                return _correct;
            }


            if (_tbl != null)
            {

                   
                DateTime _stDate = _tbl.Rows[0].Field<DateTime>("irsm_warr_start_dt").Date;
            //    DateTime _stDate = _tbl.Rows[0].Field<DateTime>("warrantystartdate").Date;

                DateTime _cDate;
                if (!IsApproveUser)
                    _cDate = CHNLSVC.Security.GetServerDateTime().Date;
                else
                    _cDate = RequestDate.Date;
                lblWara_StDate.Text = _stDate.ToString("dd/MMM/yyyy");
             //   lblWara_Period.Text = Convert.ToString(_tbl.Rows[0].Field<Int32>("warrantyperiod"));

                lblWara_Period.Text = Convert.ToString(_tbl.Rows[0].Field<Int32>("irsm_warr_period"));

                int _diff = ((_cDate.Year - _stDate.Year) * 365) + _cDate.Day - _stDate.Day;
                _diff = (_cDate.Date - _stDate.Date).Days;

                if (_diff < Convert.ToInt32(lblWara_Period.Text.Trim()) / 12 * 365)
                    pnlWarranty.BackColor = Color.LawnGreen;
                else
                    
                    pnlWarranty.BackColor = Color.Crimson;
                _correct = true;
            }
            return _correct;

        }
        #endregion

        #region Rooting for Common Search
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarrantyClaimInvoice:
                    {
                        paramsText.Append(CustomerCompany + seperator + cmbCustomer.Text.Trim() + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarrantyClaimSerial:
                    {
                        paramsText.Append(txtInvoiceNo.Text.Trim() + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }


                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnSearch_Pc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRecePc;
                _CommonSearch.ShowDialog();
                txtRecePc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void btnSearch_Loc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReceLocation;
                _CommonSearch.ShowDialog();
                txtReceLocation.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtRecePc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Pc_Click(null, null);
        }
        private void txtRecePc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Pc_Click(null, null);
        }
        private void txtReceLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Loc_Click(null, null);
        }
        private void txtReceLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Loc_Click(null, null);
        }
        #endregion

        #region Rooting for Item
        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
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
        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
{
            btnSearch_Item_Click(null, null);
        }
        private void txtItem_KeyDown(object sender, KeyEventArgs e)
    {
            if (e.KeyCode == Keys.F2) btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter) cmbInvoice.Focus();

        }
        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;
            try
            {
                if (LoadItemDetail(txtItem.Text.Trim()) == false)
                { MessageBox.Show("Please check the item code", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Clear(); txtItem.Focus(); return; }

                DataTable _tbl = new DataTable();
                //if (cmbItemType.Text.Trim() == "MAIN") { _tbl = CHNLSVC.Sales.GetSCMInvoice(CustomerCompany, string.Empty, txtItem.Text.Trim()); }
                //else { _tbl = CHNLSVC.Sales.GetSCMInvoice(CustomerCompany, cmbCustomer.Text.Trim(), txtItem.Text.Trim()); }

                if (cmbItemType.Text.Trim() == "MAIN") { _tbl = CHNLSVC.Sales.GetSCMInvoice2(CustomerCompany, string.Empty, txtItem.Text.Trim()); }
                else { _tbl = CHNLSVC.Sales.GetSCMInvoice2(CustomerCompany, cmbCustomer.Text.Trim(), txtItem.Text.Trim()); }

                itemAdding = true;
                cmbInvoice.DataSource = _tbl;
                cmbInvoice.ValueMember = "INVOICE_NO";
                cmbInvoice.DisplayMember = "INVOICE_NO";
                itemAdding = false;
                if (_tbl.Rows.Count > 0)
                    cmbInvoice_SelectedIndexChanged(null, null);
                else
                    if (cmbItemType.Text == "CLAIM")
                        MessageBox.Show("There is no invoice available for the given item for the particular customer", "No Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("There is no invoice available for the given item", "No Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Invoice No
        private void cmbInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemAdding) return;
            try
             {
                string _invoice = cmbInvoice.Text.Trim();
                GetWarrantyDetail("-1");

                 // DataTable _tbl = CHNLSVC.Sales.GetSCMInvoiceSerial(CustomerCompany, _invoice, txtItem.Text.Trim());
                // Tahrindu 201-06-14
                DataTable _tbl = CHNLSVC.Sales.GetInvoiceSerial_SCM2(CustomerCompany, _invoice, txtItem.Text.Trim());
                gvSerial.DataSource = _tbl;

                if (_tbl.Rows.Count > 0)
                {
                    DataTable _saleqty = CHNLSVC.Sales.GetInvoiceDetail(CustomerCompany, cmbInvoice.Text.Trim(), txtItem.Text.Trim());
                    if (_saleqty.Rows.Count <= 0)
                    {
                        MessageBox.Show("This invoice does not having available qty. Please check with IT Dept.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    decimal _qty = Convert.ToDecimal(_saleqty.Rows[0].Field<decimal>("SAD_DO_QTY"));
                    txtQty.Text = Convert.ToString(_qty);
                    txtQty_Leave(null, null);
                    InvoiceItemLine = _saleqty.Rows[0].Field<string>("SAD_ITM_LINE");
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Qty
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSerialNo.Focus();
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(_isDecimalAllow, sender, e);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQty.Text)) return;
            try
            {
                DataTable _saleqty = CHNLSVC.Sales.GetInvoiceDetail(CustomerCompany, cmbInvoice.Text.Trim(), txtItem.Text.Trim());
                if (_saleqty.Rows.Count <= 0)
                {
                    MessageBox.Show("This invoice does not having base qty. Please check with IT Dept.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                decimal _qty = Convert.ToDecimal(_saleqty.Rows[0].Field<decimal>("SAD_DO_QTY"));
                decimal _actualQty = Convert.ToDecimal(_saleqty.Rows[0].Field<decimal>("ACTUAL_QTY"));
                decimal _userQty = Convert.ToDecimal(txtQty.Text.Trim());

                //List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
                //var _qty = _list.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Sum(y => y.Sad_do_qty);

                if (_userQty > _qty)
                {
                    MessageBox.Show("Delivered Qty is " + _qty.ToString() + ". You can not add more than delivered qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Text = Convert.ToString(_qty);
                    lblPrice.Text = FormatToCurrency("0");
                    return;
                }

                decimal _price = (Convert.ToDecimal(_saleqty.Rows[0].Field<decimal>("SAD_TOT_AMT")) / _actualQty) * _userQty;
                lblPrice.Text = FormatToCurrency(Convert.ToString(_price));
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Serial Pick
        private void btnPickSerial_Click(object sender, EventArgs e)
        {
            if (pnlSerial.Visible)
                pnlSerial.Visible = false;
            else
                pnlSerial.Visible = true;
        }
        private void btnSerial_Close_Click(object sender, EventArgs e)
        {
            pnlSerial.Visible = false;
        }
        private void gvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvSerial.RowCount > 0)
                if (e.RowIndex != -1)
                {
                    if (cmbItemType.Text.Trim() == "CLAIM")
                    {
                        pnlWarranty.BackColor = Color.Lavender;
                        DataGridViewCheckBoxCell _chk = gvSerial.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell; //COZ COLUMN NAME NOT SUPPORT HERE

                        if (Convert.ToBoolean(_chk.Value) == false)
                            _chk.Value = true;
                        else
                            _chk.Value = false;
                    }
                    else if (cmbItemType.Text.Trim() == "MAIN")
                    {
                        bool _isSelected = false;
                        DataGridViewCheckBoxCell _chk = gvSerial.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell; //COZ COLUMN NAME NOT SUPPORT HERE
                        if (Convert.ToBoolean(_chk.Value) == true)
                        {
                            _isSelected = true;
                        }
                        string _serial = Convert.ToString(gvSerial.Rows[e.RowIndex].Cells[1].Value);  //COZ COLUMN NAME NOT SUPPORT HERE

                        for (int i = 0; i < gvSerial.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell _chk0 = gvSerial.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(_chk0.Value) == true)
                                _chk0.Value = false;
                        }

                        if (_isSelected == false)
                        {
                            _chk.Value = true;
                            bool _correct = GetWarrantyDetail(_serial);

                            if (pnlWarranty.BackColor == Color.Crimson && _correct == true)
                            {
                                MessageBox.Show("The item already void it's warranty", "Warranty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _chk.Value = false;
                                return;
                            }
                            txtMainSerial.Text = _serial;
                        }
                        else
                        {
                            _chk.Value = false;
                            pnlWarranty.BackColor = Color.Lavender;
                        }

                    }
                }
        }
        #endregion

        #region Removed Area
        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarrantyClaimInvoice);
            DataTable _result = CHNLSVC.CommonSearch.GetWarrantyClaimableInvoice(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
            _CommonSearch.ShowDialog();
            txtInvoiceNo.Select();
        }
        private void txtInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }
        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtQty.Focus();

        }
        private void txtInvoiceNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarrantyClaimInvoice);
            DataTable _result = CHNLSVC.CommonSearch.GetWarrantyClaimableInvoice(SearchParams, null, null);

            var _check = _result.AsEnumerable().Where(x => x.Field<string>("INVOICE") == txtInvoiceNo.Text.Trim());
            if (_check == null || _check.Count() <= 0)
            {
                MessageBox.Show("Please check the invoice no", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoiceNo.Clear();
                txtInvoiceNo.Focus();
                return;
            }

            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
            var _qty = _list.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Sum(y => y.Sad_do_qty);
            txtQty.Text = Convert.ToString(_qty);
        }
        #endregion

        #region Removed Area
        private void btnSearch_Serial_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarrantyClaimSerial);
            DataTable _result = CHNLSVC.CommonSearch.GetWarrantyClaimableSerial(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSerialNo;
            _CommonSearch.ShowDialog();
            txtSerialNo.Select();
        }
        private void txtSerialNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Serial_Click(null, null);
        }
        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Serial_Click(null, null);
        }
        private void txtSerialNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSerialNo.Text)) return;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarrantyClaimSerial);
            DataTable _result = CHNLSVC.CommonSearch.GetWarrantyClaimableSerial(SearchParams, null, null);

            var _check = _result.AsEnumerable().Where(x => x.Field<string>("SERIAL 1") == txtSerialNo.Text.Trim());
            if (_check == null || _check.Count() <= 0)
            {
                MessageBox.Show("Please select the valid serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSerialNo.Clear();
                txtSerialNo.Focus();
                return;
            }

            var _serialId = _result.AsEnumerable().Where(x => x.Field<string>("SERIAL 1") == txtSerialNo.Text.Trim()).Select(y => y.Field<Int64>("Serial ID")).ToList();

            if (_serialId != null)
                if (_serialId.Count() > 0)
                    GetWarrantyDetail(Convert.ToString(_serialId[0]));


        }
        #endregion

        #region Add Item
        private void MaintainCreditTotalAmount()
        {
            if (_detail != null)
                if (_detail.Count > 0)
                {
                    var _total = _detail.Where(x => x.Grad_anal8 == "CLAIM").Sum(x => Convert.ToDecimal(x.Grad_anal13));
                    lblTotalCreditAmount.Text = FormatToCurrency(Convert.ToString(_total));
                    return;
                }
            lblTotalCreditAmount.Text = FormatToCurrency("0");
        }
        private void AddItem()
        {

            if (pnlWarranty.BackColor == Color.Crimson && cmbItemType.Text.Trim() == "MAIN")
            {
                MessageBox.Show("The item already void it's warranty", "Warranty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbItemType.Text.Trim() == "MAIN")
            {
                if (_itemdetail.Mi_is_ser1 == 1 && string.IsNullOrEmpty(txtMainSerial.Text))
                {
                    MessageBox.Show("Please select the main item serial", "Main Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtMainSerial.Text))
                {
                    MessageBox.Show("Please select the main item serial", "Main Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Clear();
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Clear();
                txtQty.Focus();
                return;
            }

            if (Convert.ToDecimal(txtQty.Text) != 1 && cmbItemType.Text.Trim() == "MAIN")
            {
                MessageBox.Show("You should select one qty at a time for the warranty item", "Warranty Main Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();
                return;
            }

            if (_detail != null)
                if (_detail.Count > 0 && cmbItemType.Text.Trim() == "MAIN")
                {
                    var _mainCount = _detail.Where(x => x.Grad_anal7 == txtMainSerial.Text.Trim() && x.Grad_anal8 == cmbItemType.Text.Trim()).Count();
                    if (_mainCount != null)
                        if (_mainCount == 1)
                        {
                            MessageBox.Show("You can not add same warranty item twice for the request", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                }



            if (_itemdetail.Mi_is_ser1 == 1)
            {
                //Serialized Area
                if (gvSerial.RowCount <= 0)
                {
                    MessageBox.Show("There is no serial for the selected invoice. please contact IT Dept", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var _check = from DataGridViewRow _l in gvSerial.Rows
                             where Convert.ToBoolean(((DataGridViewCheckBoxCell)(_l.Cells["ser_select"])).Value) == true
                             select _l;

                if (_check == null || _check.Count() <= 0)
                {
                    MessageBox.Show("Please select the serial(s)", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPickSerial_Click(null, null);
                    return;
                }

                if (string.IsNullOrEmpty(InvoiceItemLine))
                {
                    MessageBox.Show("Please select the invoice.", "No Item Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbInvoice.Focus();
                }

                foreach (DataGridViewRow _row in _check)
                {
                    string _item = txtItem.Text.Trim();
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _invoice = cmbInvoice.Text.Trim();
                    string _qty = _itemdetail.Mi_is_ser1 == 1 ? "1" : txtQty.Text.Trim();
                    string _Serial1 = Convert.ToString(_row.Cells[1].Value);  //COZ COLUMN NAME NOT SUPPORT HERE
                    string _Serial2 = Convert.ToString(_row.Cells[2].Value);  //COZ COLUMN NAME NOT SUPPORT HERE
                    string _returnStatus = string.Empty;
                    string _returnSerial = string.Empty;
                    string _mainserial = txtMainSerial.Text.Trim();

                    if (cmbItemType.Text.Trim() == "MAIN") _returnStatus = string.Empty; else _returnStatus = "OLDPT";

                    if (cmbItemType.Text.Trim() == "CLAIM" && _itemdetail.Mi_is_ser1 == 1)
                    {
                    xy:
                        _returnSerial = Microsoft.VisualBasic.Interaction.InputBox("Please enter the return serial.", "Return Serial", "", -1, -1);
                        if (string.IsNullOrEmpty(_returnSerial))
                            goto xy;

                        if (_returnSerial.ToUpper() == "N/A" || _returnSerial.ToUpper() == "NA" || _returnSerial.ToUpper() == ".")
                        {
                            MessageBox.Show("You are not allow to enter serial as N/A", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto xy;
                        }

                        if (_detail != null)
                            if (_detail.Count > 0)
                            {
                                var _dup = _detail.Where(x => x.Grad_anal6 == _returnSerial && x.Grad_req_param == _item).ToList();
                                if (_dup != null)
                                    if (_dup.Count > 0)
                                    {
                                        MessageBox.Show("Entered serial already picked.", "Duplicate serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        goto xy;
                                    }
                                //TODO: Need to check on SCM for serial availability.

                            }

                    }

                    if (_detail == null) _detail = new List<RequestApprovalDetail>();
                    RequestApprovalDetail _l = new RequestApprovalDetail();
                    _l.Grad_req_param = _item;
                    _l.Grad_anal1 = _invoice;
                    _l.Grad_anal2 = _qty;
                    _l.Grad_anal3 = _Serial1;
                    _l.Grad_anal4 = _Serial2;
                    _l.Grad_anal5 = _returnStatus;
                    _l.Description = _description;
                    _l.Model = _model;
                    _l.Grad_anal6 = _returnSerial;
                    _l.Grad_anal7 = _mainserial;
                    _l.Grad_anal8 = cmbItemType.Text.Trim();
                    _l.Grad_anal9 = InvoiceItemLine;

                    if(_itemdetail.Mi_is_ser1 == 1)
                    {
                        decimal price = Convert.ToDecimal(lblPrice.Text);
                        decimal qty = Convert.ToDecimal(txtQty.Text.Trim());
                         _l.Grad_anal13 = Convert.ToDecimal(price / qty).ToString();
                    }
                    else
                    {
                        _l.Grad_anal13 = lblPrice.Text;
                    }
                  
                    _detail.Add(_l);
                }
            }
            else
            {
                //Non-Serialized Area
                string _item = txtItem.Text.Trim();
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _invoice = cmbInvoice.Text.Trim();
                string _qty = txtQty.Text.Trim();
                string _Serial1 = "N/A";
                string _Serial2 = "N/A";
                string _returnStatus = string.Empty;
                string _returnSerial = string.Empty;
                string _mainserial = txtMainSerial.Text.Trim();

                if (cmbItemType.Text.Trim() == "MAIN") _returnStatus = string.Empty; else _returnStatus = "OLDPT";
                if (_detail == null) _detail = new List<RequestApprovalDetail>();
                RequestApprovalDetail _l = new RequestApprovalDetail();
                _l.Grad_req_param = _item;
                _l.Grad_anal1 = _invoice;
                _l.Grad_anal2 = _qty;
                _l.Grad_anal3 = _Serial1;
                _l.Grad_anal4 = _Serial2;
                _l.Grad_anal5 = _returnStatus;
                _l.Description = _description;
                _l.Model = _model;
                _l.Grad_anal6 = _returnSerial;
                _l.Grad_anal7 = _mainserial;
                _l.Grad_anal8 = cmbItemType.Text.Trim();
                _l.Grad_anal9 = InvoiceItemLine;
                _l.Grad_anal13 = lblPrice.Text;
                _detail.Add(_l);
            }


            BindingSource _source = new BindingSource();
            _source.DataSource = _detail;
            gvInvoiceItem.DataSource = _source;
            InvoiceItemLine = string.Empty;
            MaintainCreditTotalAmount();

            itemAdding = true;
            txtItem.Clear();
            cmbInvoice.DataSource = null;
            txtQty.Clear();
            BindingSource _sources = new BindingSource();
            _sources.DataSource = new DataTable();
            gvSerial.DataSource = _sources;
            LoadItemDetail(string.Empty);
            GetWarrantyDetail("-1");
            itemAdding = false;
            if (cmbItemType.Text.Trim() == "MAIN") cmbItemType.SelectedIndex = 1;
            pnlSerial.Visible = false;
            lblPrice.Text = FormatToCurrency("0");
            txtItem.Focus();

        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        #endregion

        #region Save Request
        private void btnRequest_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Do you need to finalize this request?", "Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            if (gvInvoiceItem.Rows.Count <= 0)
            {
                MessageBox.Show("Please select the item", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_detail == null || _detail.Count <= 0)
            {
                MessageBox.Show("Please select the item", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var _mainserial = (from DataGridViewRow _r in gvInvoiceItem.Rows
                               select _r.Cells["itm_mainSerial"].Value).Distinct();
            if (_mainserial != null)
                if (_mainserial.Count() > 0)
                {
                    foreach (var _mserial in _mainserial)
                    {
                        string _mainS = Convert.ToString(_mserial);
                        var _count = (from DataGridViewRow _r in gvInvoiceItem.Rows
                                      where Convert.ToString(_r.Cells["itm_mainSerial"].Value) == _mainS
                                      select _r).Count();
                        if (_count == 1)
                        {
                            MessageBox.Show("The Warranty serial " + _mainS + " does not having claim items.", "No Claim Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }


            RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();

            _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT016.ToString();
            _ReqAppHdr.Grah_fuc_cd = cmbInvType.Text.Trim();
            _ReqAppHdr.Grah_ref = RequestNo;
            _ReqAppHdr.Grah_oth_loc = txtRecePc.Text.Trim();
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = "P";
            _ReqAppHdr.Grah_app_lvl = 0;
            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdr.Grah_anal3 = txtSapNo.Text; // updated by akila 2017/09/06
            _ReqAppHdr.Grah_anal4 = txtClaimNo.Text;// updated by akila 2017/09/06

            int _counter = 0;
            _detail.ForEach(x => x.Grad_line = ++_counter);
            _detail.ForEach(x => x.Grad_ref = null);
            List<RequestApprovalSerials> _serial = new List<RequestApprovalSerials>();
            RequestApprovalSerials _one = new RequestApprovalSerials();
            _one.Gras_ref = RequestNo;
            _one.Gras_line = 1;
            _one.Gras_anal1 = CustomerCompany;
            _one.Gras_anal2 = txtReceLocation.Text.Trim();
            _one.Gras_anal3 = txtRecePc.Text.Trim();
            _one.Gras_anal4 = cmbSupplier.Text.Trim();
            _one.Gras_anal5 = cmbCustomer.Text.Trim();
            _one.Gras_anal6 = 0;//will maintain as status of the document in future
            _one.Gras_anal7 = 0;
            _one.Gras_anal8 = 0;
            _one.Gras_anal9 = 0;
            _one.Gras_anal10 = 0;
            _serial.Add(_one);


            MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "RVREQ";
            _ReqAppAuto.Aut_year = null;

            string _requestNo = string.Empty;
            string _othrequestNo = string.Empty;
            string _othrequestNo1 = string.Empty;

            try
            {

                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _detail, _serial, _ReqAppAuto, null, null, null, null, null, null, null, null, null, null, false, null, null, null, null, null, null, null, false, out _requestNo, out _othrequestNo, out _othrequestNo, null);

                if (effet == 1)
                {
                    MessageBox.Show("Generated request no is " + _requestNo, "Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (this.Controls.Count > 0)
                    {
                        Controls[0].Dispose();
                    }
                    InitializeComponent();
                    InitializeForm();
                }


            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 

            }

        }
        #endregion

        #region Pending Request
        private string ItemDes(string item)
        {
            return CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item).Mi_longdesc; ;
        }
        private void gvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPending.RowCount > 0)
                if (e.RowIndex != -1)
                {
                    string _reqno = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_reqno"].Value);
                    string _recPc = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_recPc"].Value);
                    string _recLoc = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_recloc"].Value);
                    string _customer = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_customer"].Value);
                    DateTime _reqDate = Convert.ToDateTime(gvPending.Rows[e.RowIndex].Cells["p_date"].Value);
                    string _crnote = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_crnote"].Value);
                    string _adjno = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_AdjNo"].Value);
                    DateTime _date = Convert.ToDateTime(gvPending.Rows[e.RowIndex].Cells["p_date"].Value);
                    string _rmk = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_rmk"].Value);
                    AdjustmentNo = Convert.ToString(gvPending.Rows[e.RowIndex].Cells["p_crnote"].Value);//add by tharanga 2018/02/21

                    //updated by akila - 2017/09/06
                    txtSapNo.Text = gvPending.Rows[e.RowIndex].Cells["p_anal3"].Value == null ? string.Empty : gvPending.Rows[e.RowIndex].Cells["p_anal3"].Value.ToString();
                    txtClaimNo.Text = gvPending.Rows[e.RowIndex].Cells["p_anal4"].Value == null ? string.Empty : gvPending.Rows[e.RowIndex].Cells["p_anal4"].Value.ToString();
                    txtSapNo.ReadOnly = true;
                    txtClaimNo.ReadOnly = true;

                    RequestDate = _reqDate.Date;
                    CustomerCompany = BaseCls.GlbUserComCode;
                    CustomerCode = _customer;
                    txtDate.Value = _date.Date;
                    txtRemarks.Text = _rmk;

                    RequestNo = _reqno;
                    txtReceLocation.Text = _recLoc;
                    txtRecePc.Text = _recPc;
                    AdjustmentNo = _crnote;
                   // AdjustmentNo = _adjno;
                    _detail = CHNLSVC.Inventory.GetWarrantyClaimReqDetail(_reqno);
                    cmbCustomer.Text = CustomerCode;
                    LoadCustomerDetail(_customer);
                    MaintainCreditTotalAmount();
                    if (string.IsNullOrEmpty(_crnote))
                    {
                        BindingSource _souce = new BindingSource();
                        _souce.DataSource = _detail;
                        gvInvoiceItem.DataSource = _souce;
                    }
                    else
                    {
                        BindingSource _souce = new BindingSource();
                        _souce.DataSource = new List<RequestApprovalDetail>();
                        gvInvoiceItem.DataSource = _souce;
                    }

                }
        }
        #endregion

        #region Invoice Item Grid
        private void gvInvoiceItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvInvoiceItem.RowCount > 0)
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {

                        if (MessageBox.Show("Do you want to remove this record?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;


                        string _mainserial = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_mainSerial"].Value);
                        string _type = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_type"].Value);

                        string _serial = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_RtnSerial1"].Value);
                        string _invoice = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_Invoice"].Value);
                        string _returnserial = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_rtnSerial"].Value);
                        string _item = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_Item"].Value);

                        if (_type.ToString() == "MAIN")
                        {
                            _detail.RemoveAll(x => x.Grad_anal7 == _mainserial);
                        }
                        else
                        {
                            _detail.RemoveAll(x => x.Grad_req_param == _item && x.Grad_anal1 == _invoice && x.Grad_anal3 == _serial && x.Grad_anal6 == _returnserial && x.Grad_anal7 == _mainserial);
                        }
                        MaintainCreditTotalAmount();
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _detail;
                        gvInvoiceItem.DataSource = _source;
                        cmbItemType.SelectedIndex = 0;
                    }

                    if (e.ColumnIndex != 0)
                    {

                        lblWara_Period.Text = string.Empty;
                        lblWara_StDate.Text = string.Empty;
                        pnlWarranty.BackColor = Color.Lavender;

                        string _type = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_type"].Value);
                        string _item = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_item"].Value);
                        string _invoice = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_invoice"].Value);
                        string _serial = Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["itm_RtnSerial1"].Value);

                        if (_type == "MAIN")
                        {
                            DataTable _tbl = CHNLSVC.Inventory.GetSCMWarranty(_item, _serial, _invoice);
                            if (_tbl != null)
                                if (_tbl.Rows.Count > 0)
                                {
                                    DateTime _stDate = _tbl.Rows[0].Field<DateTime>("warrantystartdate").Date;
                                    DateTime _cDate = CHNLSVC.Security.GetServerDateTime().Date;
                                    lblWara_StDate.Text = _stDate.ToString("dd/MMM/yyyy");
                                    lblWara_Period.Text = Convert.ToString(_tbl.Rows[0].Field<Int32>("warrantyperiod"));

                                    int _diff = ((_cDate.Year - _stDate.Year) * 12) + _cDate.Month - _stDate.Month;

                                    if (_diff < Convert.ToInt32(lblWara_Period.Text.Trim()))
                                    {
                                        pnlWarranty.BackColor = Color.LawnGreen;
                                        txtMainSerial.Text = _serial;
                                    }
                                    else
                                        pnlWarranty.BackColor = Color.Crimson;

                                }

                        }
                    }
                }
        }
        #endregion

        #region Item Type Change
        private void cmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItemType.Text.Trim() == "MAIN")
            {
                itemAdding = true;
                txtItem.Clear();
                cmbInvoice.DataSource = null;
                txtQty.Clear();
                BindingSource _sources = new BindingSource();
                _sources.DataSource = new DataTable();
                gvSerial.DataSource = _sources;
                LoadItemDetail(string.Empty);
                GetWarrantyDetail("-1");
                itemAdding = false;
                txtMainSerial.Clear();
                txtItem.Focus();
            }
        }
        #endregion

        #region Process
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (MessageBox.Show("Do you need to process this entry?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            { this.Cursor = Cursors.Default; return; }
            
            if (string.IsNullOrEmpty(txtRecePc.Text))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRecePc.Focus();
                return;
            }

            if (_detail == null || _detail.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the item", "Item List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRecePc.Focus();
                return;
            }

            var _mainserial = (from DataGridViewRow _r in gvInvoiceItem.Rows
                               select _r.Cells["itm_mainSerial"].Value).Distinct();
            if (_mainserial != null)
                if (_mainserial.Count() > 0)
                {
                    foreach (var _mserial in _mainserial)
                    {
                        string _mainS = Convert.ToString(_mserial);
                        var _count = (from DataGridViewRow _r in gvInvoiceItem.Rows
                                      where Convert.ToString(_r.Cells["itm_mainSerial"].Value) == _mainS
                                      select _r).Count();
                        if (_count == 1)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("The Warranty serial " + _mainS + " does not having claim items.", "No Claim Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                     
                    }
                }


                    foreach (var item in _detail)
                    {
                        MasterItem _itmdet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.Grad_req_param);

                        if (_itmdet.Mi_is_ser1 == 1)
                        {
                            DataTable _dtser1 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", item.Grad_req_param, item.Grad_anal6); //Grad_anal3

                            if (_dtser1 != null)
                            {
                                if (_dtser1.Rows.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Serial already available" +" " + item.Grad_anal6, "Duplicate serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            DataTable _dtser = CHNLSVC.Inventory.CheckSerialAvailabilityscm(item.Grad_req_param, item.Grad_anal6); //Grad_anal3

                            if (_dtser != null)
                            {
                                if (_dtser.Rows.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Serial already available" + " " + item.Grad_anal6, "Duplicate serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                     
                     }



            string _crNote = string.Empty;
            string _adjPlus = string.Empty;
            int _effect = -1;
            string _mesg = string.Empty;

            try
            {
                _effect = CHNLSVC.Sales.GenerateWarrantyClaimCredit(Convert.ToDateTime(txtDate.Value).Date, "CRED", RequestNo, CustomerCode, txtRemarks.Text.Trim(), BaseCls.GlbUserComCode, txtRecePc.Text.Trim(), txtReceLocation.Text.Trim(), BaseCls.GlbUserID, null, null, _detail,BaseCls.GlbUserSessionID, out _crNote, out _adjPlus, out _effect, out _mesg,txtSapNo.Text, txtClaimNo.Text);
                if (_effect < 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(_mesg, "Terminate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Warranty Claim Credit Note Successfully Generated. " + _crNote + " Inventory Document is " + _adjPlus, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                //Given by Nadeeka on 8/06/2013
                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;
                BaseCls.GlbReportTp = "WARR_CLAIM";
                BaseCls.GlbReportName = "WarrantyClaimNote.rpt";
                BaseCls.GlbReportDoc = _crNote;
                _view.Show();
                _view = null;
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            InitializeForm();
        }
        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(AdjustmentNo))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the approved request.", "Approved Request Need", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            //BaseCls.GlbReportName = "WarrantyClaim";
            //BaseCls.GlbReportDoc = AdjustmentNo;
            //_view.Show();
            //_view = null;
            this.Cursor = Cursors.Default;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            BaseCls.GlbReportName = string.Empty;
            GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;
            BaseCls.GlbReportTp = "WARR_CLAIM";
            BaseCls.GlbReportName = "WarrantyClaimNote.rpt";
            BaseCls.GlbReportDoc = AdjustmentNo;
            _view.Show();
            _view = null;
        }
        #endregion

        #region Search Approve/Pending Requests
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _detail = new List<RequestApprovalDetail>();
                BindingSource _source1 = new BindingSource();
                _source1.DataSource = _detail;
                gvInvoiceItem.DataSource = _source1;

                if (IsApproveUser)
                {
                    DataTable _tbl = CHNLSVC.Inventory.GetWarrantyClaimReqyest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT016.ToString(), 1, chkApproved.Checked ? "F" : "P", Convert.ToDateTime(txtFromDt.Value).Date, Convert.ToDateTime(txtToDt.Value).Date);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _tbl;
                    gvPending.DataSource = _source;
                }
                else
                {
                    DataTable _tbl = CHNLSVC.Inventory.GetWarrantyClaimReqyest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT016.ToString(), 0, chkApproved.Checked ? "F" : "P", Convert.ToDateTime(txtFromDt.Value).Date, Convert.ToDateTime(txtToDt.Value).Date);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _tbl;
                    gvPending.DataSource = _source;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Customer Load
        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetail(cmbCustomer.Text.Trim());
                DataTable _tbl = new DataTable();
                if (!IsApproveUser) _tbl = CHNLSVC.Sales.LoadWarrantyClaimCreditNote(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                var _customer = _tbl.AsEnumerable().Where(x => x.Field<string>("sarsp_sup_cd") == cmbSupplier.Text.Trim()).ToList().Select(x => x.Field<string>("sarsp_cus_cd")).Distinct().ToList();
                var _customerCompany = _tbl.AsEnumerable().Where(x => x.Field<string>("sarsp_cus_cd") == cmbCustomer.Text.Trim()).ToList().Select(x => x.Field<string>("sarsp_cus_com")).Distinct().ToList();
                if (_customerCompany != null)
                    if (_customerCompany.Count > 0)
                        CustomerCompany = Convert.ToString(_customerCompany[0]);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        private void WarrantyClaimCreditNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            CHNLSVC.CloseAllChannels();
            GC.Collect();
        }

        private void gvPending_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbCustomer_Leave(object sender, EventArgs e)
        {
            DataTable _tbl = new DataTable();
            _tbl = CHNLSVC.Sales.LoadWarrantyClaimCreditNote(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            var _customer = _tbl.AsEnumerable().Where(x => x.Field<string>("sarsp_cus_cd") == cmbCustomer.Text.Trim()).ToList().Select(x => x.Field<string>("sarsp_cus_cd")).Distinct().ToList();

            if (_customer != null)
                if (_customer.Count == 0)
                    {
                        MessageBox.Show("Invalid Customer.", "Invalid Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCustomer.Text = "";
                    }
        }

    }
}
