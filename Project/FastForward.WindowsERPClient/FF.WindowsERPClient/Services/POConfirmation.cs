using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class POConfirmation : Base
    {
        List<Service_job_Det> _loadJobDet = new List<Service_job_Det>();
        List<Service_Purchase_Approval> _poAppDet = new List<Service_Purchase_Approval>();
        List<Service_Purchase_Approval> _tmpAppList = new List<Service_Purchase_Approval>();
        Boolean _isJob = false;

        public POConfirmation()
        {
            InitializeComponent();

        }

        private void ClearScreen()
        {
            try
            {
                Cursor.Current = Cursors.Default;
                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-5).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                dtpDODate.Value = CHNLSVC.Security.GetServerDateTime().Date;

                GetPendingPurchaseOrders(dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), txtFindSupplier.Text, txtFindPONo.Text, txtFindJobNo.Text, false);

                lblBackDateInfor.Text = string.Empty;
                txtPONo.Text = string.Empty;
                dtpPODate.Value = DateTime.Now.Date;
                txtPORefNo.Text = string.Empty;

                txtSuppCode.Text = string.Empty;
                txtSuppName.Text = string.Empty;
                txtFindSupplier.Text = string.Empty;
                txtFindPONo.Text = string.Empty;
                txtFindJobNo.Text = string.Empty;
                txtJobNo.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                _isJob = false;

                List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                _tmpAppList = new List<Service_Purchase_Approval>();
                _loadJobDet = new List<Service_job_Det>();
                _poAppDet = new List<Service_Purchase_Approval>();
                _emptyserList = null;
                pnlJobItm.Visible = false;
                //   dvDOSerials.AutoGenerateColumns = false;
                //    dvDOSerials.DataSource = _emptyserList;

                List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                _emptyinvoiceitemList = null;
                //    dvDOItems.AutoGenerateColumns = false;
                //    dvDOItems.DataSource = _emptyinvoiceitemList;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDODate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                //  GetUserPermission();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Common Searching Area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "2,2.1,3,4,4.1,5,5.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POByDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area

        private void btnSearch_Supplier_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindSupplier;
                _CommonSearch.ShowDialog();
                txtFindSupplier.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindSupplier_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindSupplier;
                _CommonSearch.ShowDialog();
                txtFindSupplier.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void txtFindSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtFindSupplier;
                    _CommonSearch.ShowDialog();
                    txtFindSupplier.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtFindPONo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindSupplier_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindSupplier.Text)) return;

                if (!CHNLSVC.Inventory.IsValidSupplier(BaseCls.GlbUserComCode, txtFindSupplier.Text, 1, "S"))
                {
                    MessageBox.Show("Please select the valid supplier", "Supplier Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindSupplier.Text = "";
                    txtFindSupplier.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                _CommonSearch.dtpFrom.Value = dtTemp;
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindJobNo;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtFindJobNo.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindJobNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                _CommonSearch.dtpFrom.Value = dtTemp;
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindJobNo;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtFindJobNo.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_PO_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                //DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtFindPONo;
                //_CommonSearch.ShowDialog();
                //txtFindPONo.Select();
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrdersByDate(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                _CommonSearch.dtpFrom.Value = dtTemp;
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindPONo;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtFindPONo.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindPONo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                //DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtFindPONo;
                //_CommonSearch.ShowDialog();
                //txtFindPONo.Select();
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrdersByDate(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                _CommonSearch.dtpFrom.Value = dtTemp;
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindPONo;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtFindPONo.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ClearBody()
        {
            Cursor.Current = Cursors.Default;
            //chkManualRef.Checked = false;
            txtPONo.Text = string.Empty;
            dtpDODate.Value = CHNLSVC.Security.GetServerDateTime().Date;
            txtPORefNo.Text = string.Empty;

            txtSuppCode.Text = string.Empty;
            txtSuppName.Text = string.Empty;
            //txtFindSupplier.Text = string.Empty;
            //txtFindPONo.Text = string.Empty;
            txtJobNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblPoItm.Text = string.Empty;
            lblAUnitCost.Text = string.Empty;
            lblPODelLine.Text = string.Empty;
            lblPOLine.Text = string.Empty;
            lblPendingQty.Text = string.Empty;
            lblJobLine.Text = string.Empty;
            lblJobItm.Text = string.Empty;
            txtAppQty.Text = string.Empty;
            grvDefItms.AutoGenerateColumns = false;
            grvDefItms.DataSource = new DataTable();


            List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
            _emptyinvoiceitemList = null;
            dvDOItems.AutoGenerateColumns = false;
            dvDOItems.DataSource = _emptyinvoiceitemList;

            dgvAppItms.AutoGenerateColumns = false;
            dgvAppItms.DataSource = new DataTable();
        }

        private void txtFindPONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_PO_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtFindJobNo.Focus();
        }

        private void txtFindPONo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindPONo.Text)) return;

                PurchaseOrder _hdr = CHNLSVC.Inventory.GetPOHeader(BaseCls.GlbUserComCode, txtFindPONo.Text.Trim(), "L");
                if (_hdr == null)
                {
                    MessageBox.Show("Please select the valid purchase order no", "Purchase Order No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindPONo.Text = string.Empty;
                    txtFindPONo.Focus();
                    return;
                }
                else
                {
                    //txtSupplier.Text = _POHeader.Poh_supp;
                    //txtSupRef.Text = _POHeader.Poh_ref;
                    //txtRemarks.Text = _POHeader.Poh_remarks;
                    //ddlCur.Text = _POHeader.Poh_cur_cd;
                    //lblExRate.Text = _POHeader.Poh_ex_rt.ToString();
                    //txtDate.Text = Convert.ToDateTime(_POHeader.Poh_dt).ToShortDateString();
                    //lblSubAmt.Text = Convert.ToDecimal(_POHeader.Poh_sub_tot).ToString("0,0.00", CultureInfo.InvariantCulture);
                    //lblDisAmt.Text = Convert.ToDecimal(_POHeader.Poh_dis_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
                    //lblTaxAmt.Text = Convert.ToDecimal(_POHeader.Poh_tax_tot).ToString("0,0.00", CultureInfo.InvariantCulture);
                    //lblTotAmt.Text = Convert.ToDecimal(_POHeader.Poh_tot).ToString("0,0.00", CultureInfo.InvariantCulture);
                    //GrndSubTotal = Convert.ToDecimal(_POHeader.Poh_sub_tot);
                    //GrndDiscount = Convert.ToDecimal(_POHeader.Poh_dis_amt);
                    //GrndTax = Convert.ToDecimal(_POHeader.Poh_tax_tot);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchJob_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetPO.Focus();
        }

        #region Pending Orders

        private void GetPendingPurchaseOrders(string _fromDate, string _toDate, string _supCode, string _docNo, string _jobNo, Boolean _showErrMsg)
        {
            try
            {
                PurchaseOrder _paramPurchaseOrder = new PurchaseOrder();

                _paramPurchaseOrder.Poh_com = BaseCls.GlbUserComCode;
                _paramPurchaseOrder.MasterLocation = new MasterLocation() { Ml_loc_cd = BaseCls.GlbUserDefLoca };
                _paramPurchaseOrder.Poh_stus = "A";
                _paramPurchaseOrder.MasterBusinessEntity = new MasterBusinessEntity() { Mbe_tp = "S" };
                _paramPurchaseOrder.FromDate = _fromDate;
                _paramPurchaseOrder.ToDate = _toDate;
                _paramPurchaseOrder.Poh_supp = _supCode;
                _paramPurchaseOrder.Poh_doc_no = _docNo;
                _paramPurchaseOrder.Poh_sub_tp = "S";
                _paramPurchaseOrder.Poh_job_no = _jobNo;

                DataTable pending_list = CHNLSVC.Inventory.GetAllPendingServicePurchaseOrderDataTable(_paramPurchaseOrder);

                if (pending_list.Rows.Count > 0)
                {
                    dvPendingPO.AutoGenerateColumns = false;
                    dvPendingPO.DataSource = pending_list;
                }
                else
                {
                    if (_showErrMsg == true)
                    {
                        pending_list = null;
                        MessageBox.Show("No pending purchase orders found!", "Pending Purchases", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        dvPendingPO.AutoGenerateColumns = false;
                        dvPendingPO.DataSource = pending_list;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Pending Orders

        private void btnGetPO_Click(object sender, EventArgs e)
        {
            try
            {
                ClearBody();
                Cursor.Current = Cursors.WaitCursor;
                GetPendingPurchaseOrders(dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), txtFindSupplier.Text, txtFindPONo.Text, txtFindJobNo.Text, true);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen ?", "PO Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            ClearScreen();
            ClearBody();
        }

        private void dvPendingPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvPendingPO.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    
                    Cursor.Current = Cursors.WaitCursor;
                    ClearBody();
                    _tmpAppList = new List<Service_Purchase_Approval>();
                    _poAppDet = new List<Service_Purchase_Approval>();
                    txtPONo.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_DOC_NO"].Value.ToString();
                    dtpPODate.Value = Convert.ToDateTime(dvPendingPO.Rows[e.RowIndex].Cells["POH_DT"].Value.ToString()).Date;
                    txtPORefNo.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_REF"].Value.ToString();
                    txtSuppCode.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_SUPP"].Value.ToString();
                    txtSuppName.Text = dvPendingPO.Rows[e.RowIndex].Cells["MBE_NAME"].Value.ToString();
                    txtJobNo.Text = dvPendingPO.Rows[e.RowIndex].Cells["poh_job_no"].Value.ToString();
                    lblPOSeqNo.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_SEQ_NO"].Value.ToString();
                    //_profitCenter = dvPendingPO.Rows[e.RowIndex].Cells["POH_PROFIT_CD"].Value.ToString();
                    // _poType = dvPendingPO.Rows[e.RowIndex].Cells["POH_TP"].Value.ToString(); //Add by Chamal 31/07/2014
                    LoadPOItems(txtPONo.Text.ToString());
                    txtManualRefNo.Clear();
                    txtRemarks.Clear();
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadPOItems(string _poNo)
        {
            try
            {
                //Get Invoice Items Details
                DataTable po_items = CHNLSVC.Inventory.GetSerPOItemsDataTable(BaseCls.GlbUserComCode, _poNo, BaseCls.GlbUserDefLoca);
                if (po_items.Rows.Count > 0)
                {
                    dvDOItems.Enabled = true;
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = new DataTable();
                    dvDOItems.DataSource = po_items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //get all from sat_itm
        }

        private void POConfirmation_Load(object sender, EventArgs e)
        {
            ClearScreen();
            ClearBody();
        }

        private void dvDOItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //decimal _EditQty = 0;
            //decimal _BalQty = 0;

            //if (dvDOItems.Rows.Count > 0)
            //{
            //    if (string.IsNullOrEmpty(dvDOItems.Rows[e.RowIndex].Cells["col_AAppQty"].Value.ToString()))
            //    {
            //        MessageBox.Show("Please enter valid reversal qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    _EditQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_AAppQty"].Value);
            //    _BalQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_ARemQty"].Value);


            //    if (_BalQty < _EditQty)
            //    {
            //        MessageBox.Show("Cannot exceed invoice qty.", "PO Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        dvDOItems.Rows[e.RowIndex].Cells["col_AAppQty"].Value = _BalQty;
            //        return;
            //    }

            //    if (_EditQty <= 0)
            //    {
            //        MessageBox.Show("Invalid return qty.", "PO Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        dvDOItems.Rows[e.RowIndex].Cells["col_AAppQty"].Value = _BalQty;
            //        return;
            //    }
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _appQty = 0;
                Boolean _exitApp = false;
                string _msg = string.Empty;
                foreach (DataGridViewRow row in dvDOItems.Rows)
                {
                    _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);

                    if (_appQty > 0)
                    {
                        _exitApp = true;
                        goto L1;
                    }
                }
            L1: int I = 0;
                if (_exitApp == false)
                {
                    MessageBox.Show("Cannot find any approved qty.", "PO Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_poAppDet.Count <= 0)
                {
                    MessageBox.Show("Cannot find any approved qty.", "PO Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to confirm ?", "PO Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                PurchaseOrder _savePO = new PurchaseOrder();
                List<PurchaseOrderDelivery> _saveDelList = new List<PurchaseOrderDelivery>();


                _savePO.Poh_doc_no = txtPONo.Text.Trim();
                _savePO.Poh_cre_by = BaseCls.GlbUserID;

                foreach (DataGridViewRow row in dvDOItems.Rows)
                {
                    PurchaseOrderDelivery _tmpDel = new PurchaseOrderDelivery();
                    Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                    Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ADelLine"].Value);
                    string _poItm = row.Cells["col_AItm"].Value.ToString();
                    decimal _poAppQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);

                    _tmpDel.Podi_del_line_no = _poDelLine;
                    _tmpDel.Podi_itm_cd = _poItm;
                    _tmpDel.Podi_bal_qty = 0;
                    _tmpDel.Podi_line_no = _poLine;
                    _tmpDel.Podi_loca = BaseCls.GlbUserDefLoca;
                    _tmpDel.Podi_qty = _poAppQty;
                    _tmpDel.Podi_seq_no = Convert.ToInt32(lblPOSeqNo.Text);
                    _saveDelList.Add(_tmpDel);
                }

                Int32 _effect = CHNLSVC.CustService.Save_PO_Confirmation(_savePO, _saveDelList, _poAppDet, out _msg);

                if (_effect == 1)
                {
                    MessageBox.Show("Purchase order approved.", "PO Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearBody();
                    ClearScreen();
                }
                else
                {
                    MessageBox.Show("PO approval terminate." + _msg, "PO Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dvDOItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _isJob = false;
                if (dvDOItems.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    #region Add Serials

                    if (e.ColumnIndex == 0)
                    {
                        //string _itemstatus = dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_stus"].Value.ToString();
                        decimal _remQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_ARemQty"].Value.ToString());
                        decimal _appQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_AAppQty"].Value.ToString());
                        string _poItem = dvDOItems.Rows[e.RowIndex].Cells["col_AItm"].Value.ToString();
                        decimal _poLine = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_ALineNo"].Value.ToString());
                        decimal _poDelLine = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_ADelLine"].Value.ToString());
                        decimal _poActCost = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["col_AUnitCost"].Value.ToString());

                        if (_remQty <= _appQty)
                        {
                            MessageBox.Show("No remaining qty available.", "PO Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        if (!string.IsNullOrEmpty(txtJobNo.Text))
                        {
                            LoadJobItemDetails(txtJobNo.Text);
                            _isJob = true;
                        }
                        lblPoItm.Text = _poItem;
                        lblPendingQty.Text = (_remQty - _appQty).ToString();
                        lblPOLine.Text = _poLine.ToString();
                        lblPODelLine.Text = _poDelLine.ToString();
                        lblAUnitCost.Text = _poActCost.ToString();
                        _tmpAppList = new List<Service_Purchase_Approval>();
                        pnlJobItm.Visible = true;
                    }

                    #endregion Add Serials
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadJobItemDetails(string _jobNo)
        {
            if (!string.IsNullOrEmpty(_jobNo))
            {

                _loadJobDet = new List<Service_job_Det>();
                List<Service_job_Det> _JobDet = new List<Service_job_Det>();
                _JobDet = CHNLSVC.CustService.GetPcJobDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtJobNo.Text.Trim());

                if (_JobDet != null && _JobDet.Count > 0)
                {
                    foreach (Service_job_Det _jDet in _JobDet)
                    {
                        if (_jDet.Jbd_stage < 6) //2,2.1,3,4,4.1,5,5.1
                        {
                            _loadJobDet.Add(_jDet);
                        }

                    }

                    _loadJobDet = _loadJobDet.OrderBy(x => x.Jbd_jobline).ToList();


                    grvDefItms.AutoGenerateColumns = false;
                    grvDefItms.DataSource = new DataTable();
                    grvDefItms.DataSource = _loadJobDet;

                    foreach (DataGridViewRow row in grvDefItms.Rows)
                    {

                        DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                        ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[0].Cells[0];

                        //grvDefItms.Rows[0].Cells[0].Value = true;
                        //grvDefItms_CellClick(grvDefItms, new DataGridViewCellEventArgs(0, 0));

                        if (ch1.Value == null)
                            ch1.Value = false;
                        switch (ch1.Value.ToString())
                        {
                            case "False":
                                {
                                    ch1.Value = true;
                                    string _jobItm = grvDefItms.Rows[0].Cells["col_JItm"].Value.ToString();
                                    string _jobLine = grvDefItms.Rows[0].Cells["col_JLine"].Value.ToString();
                                    lblJobItm.Text = _jobItm;
                                    lblJobLine.Text = _jobLine.ToString();
                                    break;
                                }

                        }
                        goto L1;
                    }

                L1: int I = 0;
                    //Service_JOB_HDR _JobHdr = new Service_JOB_HDR();
                    //_JobHdr = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text.Trim(), BaseCls.GlbUserComCode);

                    //if (_JobHdr != null)
                    //{
                    //    lblCusName.Text = _JobHdr.SJB_CUST_NAME;
                    //    lblCusCode.Text = _JobHdr.SJB_CUST_CD;
                    //}
                }
                else
                {
                    MessageBox.Show("Invalid job number", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Text = "";
                    txtJobNo.Focus();
                    return;
                }


            }
        }

        private void grvDefItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in grvDefItms.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }

            }

            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[grvDefItms.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        string _jobItm = grvDefItms.Rows[grvDefItms.CurrentRow.Index].Cells["col_JItm"].Value.ToString();
                        string _jobLine = grvDefItms.Rows[grvDefItms.CurrentRow.Index].Cells["col_JLine"].Value.ToString();
                        lblJobItm.Text = _jobItm;
                        lblJobLine.Text = _jobLine.ToString();
                        break;
                    }

            }


            txtAppQty.Focus();

        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppQty.Text))
            {
                MessageBox.Show("Please enter approve Qty", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAppQty.Focus();
                return;
            }

            if (!IsNumeric(txtAppQty.Text))
            {
                MessageBox.Show("Please enter valid approve Qty", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAppQty.Focus();
                return;
            }

            if (Convert.ToDecimal(lblPendingQty.Text) < Convert.ToDecimal(txtAppQty.Text))
            {
                MessageBox.Show("Approve qty exceed pending qty.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAppQty.Text = "0";
                txtAppQty.Focus();
                return;
            }

            if (Convert.ToDecimal(txtAppQty.Text) <= 0)
            {
                MessageBox.Show("Please enter valid qty.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAppQty.Text = "0";
                txtAppQty.Focus();
                return;
            }

            if (_isJob == true)
            {
                if (string.IsNullOrEmpty(lblJobItm.Text))
                {
                    MessageBox.Show("Please select job item.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(lblJobLine.Text))
                {
                    MessageBox.Show("Please select job item.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }


            foreach (DataGridViewRow tmp in dgvAppTmp.Rows)
            {
                string _Poitem = tmp.Cells["col_TmpPOItm"].Value.ToString();
                Int16 _PoLine = Convert.ToInt16(tmp.Cells["col_TmpPoLine"].Value);
                Int16 _PoDelLine = Convert.ToInt16(tmp.Cells["col_TmpDelLine"].Value);
                string _JobNo = tmp.Cells["col_TmpJob"].Value.ToString();
                string _JobItm = tmp.Cells["col_TmpJobItm"].Value.ToString();
                Int16 _JobLine = Convert.ToInt16(tmp.Cells["col_tmpJobLine"].Value);
                decimal _appTmpQty = Convert.ToDecimal(tmp.Cells["col_tmpQty"].Value);

                if (_Poitem == lblPoItm.Text && _PoLine == Convert.ToInt16(lblPOLine.Text) && _PoDelLine == Convert.ToInt16(lblPODelLine.Text) && _JobNo == txtJobNo.Text && _JobItm == lblJobItm.Text && _JobLine == Convert.ToInt16(lblJobLine.Text))
                {
                    MessageBox.Show("Selected item already exsist in approve details.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }



            Service_Purchase_Approval _listApp = new Service_Purchase_Approval();

            _listApp.Posa_job_itm = lblJobItm.Text;
            _listApp.Posa_job_line = Convert.ToInt16(lblJobLine.Text);
            _listApp.Posa_job_no = txtJobNo.Text;
            _listApp.Posa_po_del_line = Convert.ToInt16(lblPODelLine.Text);
            _listApp.Posa_po_itm = lblPoItm.Text;
            _listApp.Posa_po_itm_line = Convert.ToInt16(lblPOLine.Text);
            _listApp.Posa_po_no = txtPONo.Text;
            _listApp.Posa_po_seq = Convert.ToInt32(lblPOSeqNo.Text);
            _listApp.Posa_qty = Convert.ToDecimal(txtAppQty.Text);
            _listApp.Posa_unit_cost = Convert.ToDecimal(lblAUnitCost.Text);
            _listApp.Posa_act = 1;
            _listApp.Posa_cre_by = BaseCls.GlbUserID;
            _listApp.Posa_mod_by = BaseCls.GlbUserID;
            _listApp.Posa_app_dt = DateTime.Now.Date;
            _tmpAppList.Add(_listApp);

            lblPendingQty.Text = (Convert.ToDecimal(lblPendingQty.Text) - Convert.ToDecimal(txtAppQty.Text)).ToString();


            foreach (DataGridViewRow row in dvDOItems.Rows)
            {
                Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ADelLine"].Value);
                string _poItm = row.Cells["col_AItm"].Value.ToString();
                decimal _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);
                decimal _remQty = Convert.ToDecimal(row.Cells["col_ARemQty"].Value);

                if (_poItm == lblPoItm.Text && _poLine == Convert.ToInt16(lblPOLine.Text) && _poDelLine == Convert.ToInt16(lblPODelLine.Text))
                {
                    if (_remQty < _appQty + Convert.ToDecimal(txtAppQty.Text))
                    {
                        MessageBox.Show("Approve qty exceed.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    row.Cells["col_AAppQty"].Value = _appQty + Convert.ToDecimal(txtAppQty.Text);
                }
            }


            dgvAppTmp.AutoGenerateColumns = false;
            dgvAppTmp.DataSource = new List<Service_Purchase_Approval>();
            dgvAppTmp.DataSource = _tmpAppList;

            lblJobItm.Text = string.Empty;
            lblJobLine.Text = string.Empty;
            txtAppQty.Text = string.Empty;
        }

        private void txtAppQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAppQty.Text))
            {
                if (!IsNumeric(txtAppQty.Text))
                {
                    MessageBox.Show("Please enter valid approve Qty", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAppQty.Focus();
                    return;
                }
            }
        }

        private void txtAppQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddApp.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_tmpAppList.Count <= 0)
            {
                MessageBox.Show("Please add approve item details.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _poAppDet.AddRange(_tmpAppList);

            dgvAppItms.AutoGenerateColumns = false;
            dgvAppItms.DataSource = new List<Service_Purchase_Approval>();
            dgvAppItms.DataSource = _poAppDet;


            dgvAppTmp.AutoGenerateColumns = false;
            dgvAppTmp.DataSource = new List<Service_Purchase_Approval>();

            lblPoItm.Text = string.Empty;
            lblPOLine.Text = string.Empty;

            lblPODelLine.Text = string.Empty;
            txtAppQty.Text = string.Empty;
            lblJobItm.Text = string.Empty;
            lblJobLine.Text = string.Empty;
            lblAUnitCost.Text = string.Empty;

            grvDefItms.AutoGenerateColumns = false;
            grvDefItms.DataSource = new DataTable();

            pnlJobItm.Visible = false;

        }

        private void grvDefItms_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvDefItms.IsCurrentCellDirty)
            {
                grvDefItms.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAppTmp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you want to remove selected details ?", "PO Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_tmpAppList == null || _tmpAppList.Count == 0) return;

                    string _Poitem = dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpPOItm"].Value.ToString();
                    Int16 _PoLine = Convert.ToInt16(dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpPoLine"].Value);
                    Int16 _PoDelLine = Convert.ToInt16(dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpDelLine"].Value);
                    string _JobNo = dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpJob"].Value.ToString();
                    string _JobItm = dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpJobItm"].Value.ToString();
                    Int16 _JobLine = Convert.ToInt16(dgvAppTmp.Rows[e.RowIndex].Cells["col_tmpJobLine"].Value);
                    decimal _appTmpQty = Convert.ToDecimal(dgvAppTmp.Rows[e.RowIndex].Cells["col_tmpQty"].Value);


                    List<Service_Purchase_Approval> _temp = new List<Service_Purchase_Approval>();
                    _temp = _tmpAppList;

                    _temp.RemoveAll(x => x.Posa_po_itm == _Poitem && x.Posa_po_itm_line == _PoLine && x.Posa_po_del_line == _PoDelLine && x.Posa_job_no == _JobNo && x.Posa_job_itm == _JobItm && x.Posa_job_line == _JobLine);
                    _tmpAppList = _temp;

                    lblPendingQty.Text = (Convert.ToDecimal(lblPendingQty.Text) + _appTmpQty).ToString();

                    dgvAppTmp.AutoGenerateColumns = false;
                    dgvAppTmp.DataSource = new List<Service_Purchase_Approval>();
                    dgvAppTmp.DataSource = _tmpAppList;

                    foreach (DataGridViewRow row in dvDOItems.Rows)
                    {
                        Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                        Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                        string _poItm = row.Cells["col_AItm"].Value.ToString();
                        decimal _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);
                        decimal _remQty = Convert.ToDecimal(row.Cells["col_ARemQty"].Value);

                        if (_poItm == _Poitem && _poLine == _PoLine && _poDelLine == _PoDelLine)
                        {
                            if (_remQty < (_appQty - Convert.ToDecimal(_appTmpQty)))
                            {
                                MessageBox.Show("Approve qty exceed.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            row.Cells["col_AAppQty"].Value = _appQty - Convert.ToDecimal(_appTmpQty);
                        }
                    }

                    foreach (DataGridViewRow row in grvDefItms.Rows)
                    {

                        DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                        ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[0].Cells[0];
                        
                        if (ch1.Value == null)
                            ch1.Value = false;
                        switch (ch1.Value.ToString())
                        {
                            case "True":
                                {
                                    ch1.Value = false;
                                    break;
                                }

                        }
                        
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow tmp in dgvAppTmp.Rows)
            {
                string _Poitem = tmp.Cells["col_TmpPOItm"].Value.ToString();
                Int16 _PoLine = Convert.ToInt16(tmp.Cells["col_TmpPoLine"].Value);
                Int16 _PoDelLine = Convert.ToInt16(tmp.Cells["col_TmpDelLine"].Value);
                string _JobNo = tmp.Cells["col_TmpJob"].Value.ToString();
                string _JobItm = tmp.Cells["col_TmpJobItm"].Value.ToString();
                Int16 _JobLine = Convert.ToInt16(tmp.Cells["col_tmpJobLine"].Value);
                decimal _appTmpQty = Convert.ToDecimal(tmp.Cells["col_tmpQty"].Value);

                foreach (DataGridViewRow row in dvDOItems.Rows)
                {
                    Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                    Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ADelLine"].Value);
                    string _poItm = row.Cells["col_AItm"].Value.ToString();
                    decimal _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);
                    decimal _remQty = Convert.ToDecimal(row.Cells["col_ARemQty"].Value);

                    if (_poItm == _Poitem && _poLine == _PoLine && _poDelLine == _PoDelLine)
                    {
                        if (_remQty < _appQty - Convert.ToDecimal(_appTmpQty))
                        {
                            MessageBox.Show("Approve qty exceed.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        row.Cells["col_AAppQty"].Value = _appQty - Convert.ToDecimal(_appTmpQty);
                    }
                }
            }

            _tmpAppList = new List<Service_Purchase_Approval>();
            dgvAppTmp.AutoGenerateColumns = false;
            dgvAppTmp.DataSource = new List<Service_Purchase_Approval>();

            lblPoItm.Text = string.Empty;
            lblPOLine.Text = string.Empty;

            lblPODelLine.Text = string.Empty;
            txtAppQty.Text = string.Empty;
            lblJobItm.Text = string.Empty;
            lblJobLine.Text = string.Empty;
            lblAUnitCost.Text = string.Empty;

            grvDefItms.AutoGenerateColumns = false;
            grvDefItms.DataSource = new DataTable();

            pnlJobItm.Visible = false;
        }

        private void dgvAppItms_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you want to remove selected details ?", "PO Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_poAppDet == null || _poAppDet.Count == 0) return;

                    string _Poitem = dgvAppItms.Rows[e.RowIndex].Cells["col_APoItm"].Value.ToString();
                    Int16 _PoLine = Convert.ToInt16(dgvAppItms.Rows[e.RowIndex].Cells["col_APoItmLine"].Value);
                    Int16 _PoDelLine = Convert.ToInt16(dgvAppItms.Rows[e.RowIndex].Cells["col_APoDelLine"].Value);
                    string _JobNo = dgvAppItms.Rows[e.RowIndex].Cells["col_AjobNo"].Value.ToString();
                    string _JobItm = dgvAppItms.Rows[e.RowIndex].Cells["col_AJobItm"].Value.ToString();
                    Int16 _JobLine = Convert.ToInt16(dgvAppItms.Rows[e.RowIndex].Cells["col_AJobLine"].Value);
                    decimal _appTmpQty = Convert.ToDecimal(dgvAppItms.Rows[e.RowIndex].Cells["col_AQty"].Value);


                    List<Service_Purchase_Approval> _temp = new List<Service_Purchase_Approval>();
                    _temp = _poAppDet;

                    _temp.RemoveAll(x => x.Posa_po_itm == _Poitem && x.Posa_po_itm_line == _PoLine && x.Posa_po_del_line == _PoDelLine && x.Posa_job_no == _JobNo && x.Posa_job_itm == _JobItm && x.Posa_job_line == _JobLine);
                    _tmpAppList = _temp;



                    dgvAppItms.AutoGenerateColumns = false;
                    dgvAppItms.DataSource = new List<Service_Purchase_Approval>();
                    dgvAppItms.DataSource = _poAppDet;

                    foreach (DataGridViewRow row in dvDOItems.Rows)
                    {
                        Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                        Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ADelLine"].Value);
                        string _poItm = row.Cells["col_AItm"].Value.ToString();
                        decimal _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);
                        decimal _remQty = Convert.ToDecimal(row.Cells["col_ARemQty"].Value);

                        if (_poItm == _Poitem && _poLine == _PoLine && _poDelLine == _PoDelLine)
                        {
                            if (_remQty < (_appQty - Convert.ToDecimal(_appTmpQty)))
                            {
                                MessageBox.Show("Approve qty exceed.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            row.Cells["col_AAppQty"].Value = _appQty - Convert.ToDecimal(_appTmpQty);
                        }
                    }
                }
            }
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpToDate.Focus();
            }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFindSupplier.Focus();
            }
        }

        private void dgvAppItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvAppItms.ColumnCount > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Do you want to remove selected details ?", "PO Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_poAppDet == null || _poAppDet.Count == 0) return;

                        string _Poitem = dgvAppItms.Rows[e.RowIndex].Cells["col_APoItm"].Value.ToString();
                        Int16 _PoLine = Convert.ToInt16(dgvAppItms.Rows[e.RowIndex].Cells["col_APoItmLine"].Value);
                        Int16 _PoDelLine = Convert.ToInt16(dgvAppItms.Rows[e.RowIndex].Cells["col_APoDelLine"].Value);
                        string _JobNo = dgvAppItms.Rows[e.RowIndex].Cells["col_AjobNo"].Value.ToString();
                        string _JobItm = dgvAppItms.Rows[e.RowIndex].Cells["col_AJobItm"].Value.ToString();
                        Int16 _JobLine = Convert.ToInt16(dgvAppItms.Rows[e.RowIndex].Cells["col_AJobLine"].Value);
                        decimal _appTmpQty = Convert.ToDecimal(dgvAppItms.Rows[e.RowIndex].Cells["col_AQty"].Value);


                        List<Service_Purchase_Approval> _temp = new List<Service_Purchase_Approval>();
                        _temp = _poAppDet;

                        _temp.RemoveAll(x => x.Posa_po_itm == _Poitem && x.Posa_po_itm_line == _PoLine && x.Posa_po_del_line == _PoDelLine && x.Posa_job_no == _JobNo && x.Posa_job_itm == _JobItm && x.Posa_job_line == _JobLine);
                        _tmpAppList = _temp;



                        dgvAppItms.AutoGenerateColumns = false;
                        dgvAppItms.DataSource = new List<Service_Purchase_Approval>();
                        dgvAppItms.DataSource = _poAppDet;

                        foreach (DataGridViewRow row in dvDOItems.Rows)
                        {
                            Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                            Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ADelLine"].Value);
                            string _poItm = row.Cells["col_AItm"].Value.ToString();
                            decimal _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);
                            decimal _remQty = Convert.ToDecimal(row.Cells["col_ARemQty"].Value);

                            if (_poItm == _Poitem && _poLine == _PoLine && _poDelLine == _PoDelLine)
                            {
                                if (_remQty < (_appQty - Convert.ToDecimal(_appTmpQty)))
                                {
                                    MessageBox.Show("Approve qty exceed.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                row.Cells["col_AAppQty"].Value = _appQty - Convert.ToDecimal(_appTmpQty);
                            }
                        }
                    }
                }
            }
        }

        private void dgvAppTmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAppTmp.ColumnCount > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Do you want to remove selected details ?", "PO Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_tmpAppList == null || _tmpAppList.Count == 0) return;

                        string _Poitem = dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpPOItm"].Value.ToString();
                        Int16 _PoLine = Convert.ToInt16(dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpPoLine"].Value);
                        Int16 _PoDelLine = Convert.ToInt16(dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpDelLine"].Value);
                        string _JobNo = dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpJob"].Value.ToString();
                        string _JobItm = dgvAppTmp.Rows[e.RowIndex].Cells["col_TmpJobItm"].Value.ToString();
                        Int16 _JobLine = Convert.ToInt16(dgvAppTmp.Rows[e.RowIndex].Cells["col_tmpJobLine"].Value);
                        decimal _appTmpQty = Convert.ToDecimal(dgvAppTmp.Rows[e.RowIndex].Cells["col_tmpQty"].Value);


                        List<Service_Purchase_Approval> _temp = new List<Service_Purchase_Approval>();
                        _temp = _tmpAppList;

                        _temp.RemoveAll(x => x.Posa_po_itm == _Poitem && x.Posa_po_itm_line == _PoLine && x.Posa_po_del_line == _PoDelLine && x.Posa_job_no == _JobNo && x.Posa_job_itm == _JobItm && x.Posa_job_line == _JobLine);
                        _tmpAppList = _temp;

                        lblPendingQty.Text = (Convert.ToDecimal(lblPendingQty.Text) + _appTmpQty).ToString();

                        dgvAppTmp.AutoGenerateColumns = false;
                        dgvAppTmp.DataSource = new List<Service_Purchase_Approval>();
                        dgvAppTmp.DataSource = _tmpAppList;

                        foreach (DataGridViewRow row in dvDOItems.Rows)
                        {
                            Int16 _poLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                            Int16 _poDelLine = Convert.ToInt16(row.Cells["col_ALineNo"].Value);
                            string _poItm = row.Cells["col_AItm"].Value.ToString();
                            decimal _appQty = Convert.ToDecimal(row.Cells["col_AAppQty"].Value);
                            decimal _remQty = Convert.ToDecimal(row.Cells["col_ARemQty"].Value);

                            if (_poItm == _Poitem && _poLine == _PoLine && _poDelLine == _PoDelLine)
                            {
                                if (_remQty < (_appQty - Convert.ToDecimal(_appTmpQty)))
                                {
                                    MessageBox.Show("Approve qty exceed.", "PO confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                row.Cells["col_AAppQty"].Value = _appQty - Convert.ToDecimal(_appTmpQty);
                            }
                        }

                        foreach (DataGridViewRow row in grvDefItms.Rows)
                        {

                            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                            ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[0].Cells[0];

                            if (ch1.Value == null)
                                ch1.Value = false;
                            switch (ch1.Value.ToString())
                            {
                                case "True":
                                    {
                                        ch1.Value = false;
                                        break;
                                    }

                            }

                        }
                    }
                }
            }
        }


    }
}
