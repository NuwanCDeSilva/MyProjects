using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using FF.BusinessObjects.Sales;

namespace FF.WindowsERPClient.Purchase
{
    public partial class PurchaseOrder : Base
    {
        private List<PurchaseOrderDetail> _POItemList = new List<PurchaseOrderDetail>();
        private List<PurchaseOrderDelivery> _POItemDel = new List<PurchaseOrderDelivery>();
        private List<PurchaseOrderAlloc> _POItemAloc = new List<PurchaseOrderAlloc>();
        private MasterBusinessEntity _supDet = new MasterBusinessEntity();
        private List<mst_busentity_itm> Getcustomerpluupdate = new List<mst_busentity_itm>();
        Int32 _lineNo = 0;
        Int32 _delLineNo = 0;
        Int32 _delAlocLineNo = 0;
        decimal _AddDelQty = 0;
        Boolean _IsRecall = false;
        string _POstatus = "";
        Int32 _POSeqNo = 0;
        decimal _totPoQty = 0;
        private Boolean _isStrucBaseTax = false;
        bool IsPoCanCancel; //by akila 2017/07/03
        string POSubType = null;

        public PurchaseOrder()
        {
            InitializeComponent();
            //kapila 1/7/2016
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
        }


        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POSupplier://updated by akila 2017/06/28
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Supplier:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POByDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "2,2.1,3,4,4.1,5,5.1" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {

            cmbType.Text = "NORMAL";
            dtpPoDate.Value = Convert.ToDateTime(DateTime.Now).Date;
            cmbPayTerm.Text = "CASH";
            txtCreditPeriod.Text = "0";
            txtCurrency.Text = "LKR";
            lblEx.Text = "0.00";
            LoadExRate();
            txtRemarks.Text = "";
            txtPurNo.Text = "";
            txtSupCode.Text = "";
            txtSupRef.Text = "";
            txtJob.Text = "";
            txtStatus.Enabled = true;
            txtStatus.Text = "GDLP";
            chkBaseToConsGrn.Checked = false;
            chkBaseToConsGrn.Enabled = false;
            txtJob.Enabled = false;
            btnJobSearch.Enabled = false;
            lblDelItem.Text = "";
            lblDelStatus.Text = "";
            lblItemLine.Text = "";
            lblAlocItem.Text = "";
            lblAlocStatus.Text = "";
            lblAlocItemLine.Text = "";
            lblStatus.Text = "";
            lblSupName.Text = "";

            lblSubTot.Text = "0.00";
            lblDisAmt.Text = "0.00";
            lblTotAfterDis.Text = "0.00";
            lblTaxAmt.Text = "0.00";
            lblTotal.Text = "0.00";

            btnApprove.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = false;
            cmdAddItem.Enabled = true;
            btnUploadFile_spv.Enabled = true;
            btnAddAloc.Enabled = true;
            btnAddAlocAll.Enabled = true;
            _IsRecall = false;
            _POstatus = "";
            _POSeqNo = 0;

            _POItemList = new List<PurchaseOrderDetail>();
            _POItemDel = new List<PurchaseOrderDelivery>();
            _POItemAloc = new List<PurchaseOrderAlloc>();
            _supDet = new MasterBusinessEntity();

            _lineNo = 0;
            _delLineNo = 0;
            _delAlocLineNo = 0;
            _AddDelQty = 0;
            clear_item();
            dgvPOItems.AutoGenerateColumns = false;
            dgvPOItems.DataSource = new List<PurchaseOrderDetail>();

            dgvDel.AutoGenerateColumns = false;
            dgvDel.DataSource = new List<PurchaseOrderDelivery>();

            dgvAloc.AutoGenerateColumns = false;
            dgvAloc.DataSource = new List<PurchaseOrderAlloc>();

            dgvQuo.AutoGenerateColumns = false;
            dgvQuo.DataSource = new List<QuotationHeader>();

            txtSupCode.Enabled = true;
            pnlAloc.Visible = false;

            txtFileName.Text = "";
            optItms.Checked = true;
            cmbType.Focus();

            IsPoCanCancel = false; //by akila 2017/07/03
            POSubType = null;
        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10161))//16023
            {
                txtUnitPrice.Enabled = true;
            }
            else
            {
                txtUnitPrice.Enabled = false;
            }
            Clear_Data();
        }

        private void txtCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCurrency;
                    _CommonSearch.ShowDialog();
                    txtCurrency.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSupCode.Focus();
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

        private void txtCurrency_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCurrency.Text))
                {
                    List<MasterCurrency> _currency = new List<MasterCurrency>();
                    _currency = CHNLSVC.General.GetAllCurrency(txtCurrency.Text.Trim());

                    if (_currency == null)
                    {
                        MessageBox.Show("Invalid currency code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCurrency.Text = "";
                        lblEx.Text = "";
                        return;
                    }
                    else
                    {
                        LoadExRate();
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


        protected void LoadExRate()
        {
            try
            {
                decimal _curExRate = 0;
                MasterExchangeRate _ExRate = CHNLSVC.Sales.GetLaterstExchangeRate(BaseCls.GlbUserComCode, txtCurrency.Text.Trim(), Convert.ToDateTime(dtpPoDate.Value).Date);

                if (_ExRate != null)
                {
                    _curExRate = _ExRate.Mer_bnksel_rt;
                    lblEx.Text = _curExRate.ToString("0.00");
                    btnSave.Enabled = true;
                    // txtCurrency.Focus();
                }
                else
                {
                    MessageBox.Show("Exchange rate is not define for above currency.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblEx.Text = "";
                    txtCurrency.Text = "";
                    btnSave.Enabled = false;
                    btnApprove.Enabled = false;
                    txtCurrency.Focus();
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

        private void btnCurrencySearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCurrency;
                _CommonSearch.ShowDialog();
                txtCurrency.Select();
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

        private void txtSupCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnSupSearch_Click(null, null);//add by akila 2017/06/29

                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    //DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = txtSupCode;
                    //_CommonSearch.ShowDialog();
                    //txtSupCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSupRef.Focus();
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

        private void btnSupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //updated by akila 2017/06/28
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsSearchEnter = true;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POSupplier);
                DataTable _result = CHNLSVC.CommonSearch.SearchSupplierData(_CommonSearch.SearchParams, null, null);

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                //DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSupCode;
                _CommonSearch.ShowDialog();
                txtSupCode.Select();
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

        private void txtSupCode_DoubleClick(object sender, EventArgs e)
        {
            btnSupSearch_Click(null, null);//add by akila 2017/06/29

            //try
            //{
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtSupCode;
            //    _CommonSearch.ShowDialog();
            //    txtSupCode.Select();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void txtSupCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupCode.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(BaseCls.GlbUserComCode, txtSupCode.Text, 1, "S"))
                    {
                        MessageBox.Show("Invalid supplier code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSupCode.Text = "";
                        lblSupName.Text = "";
                        txtCreditPeriod.Text = "0";
                        txtSupCode.Focus();
                        return;
                    }
                    else
                    {
                        _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtSupCode.Text.Trim(), null, null, "S");
                        lblSupName.Text = _supDet.Mbe_name;
                        txtCreditPeriod.Text = _supDet.MBE_CR_PERIOD.ToString();
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

        private void cmbType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbType.Text))
            {
                MessageBox.Show("Please select valid type.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbType.Text = "NORMAL";
                cmbType.Focus();
                return;
            }

        }

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "NORMAL")
            {
                txtJob.Enabled = false;
                btnJobSearch.Enabled = false;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = false;
                txtStatus.Text = "GDLP";
                txtStatus.Enabled = true;
            }
            else if (cmbType.Text == "SERVICE")
            {
                txtJob.Enabled = true;
                btnJobSearch.Enabled = true;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = false;
                txtStatus.Text = "GDLP";
                txtStatus.Enabled = true;
            }
            else if (cmbType.Text == "CONSIGNMENT")
            {
                txtJob.Enabled = false;
                btnJobSearch.Enabled = false;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = true;
                txtStatus.Text = "CONS";
                txtStatus.Enabled = false;
            }
            else if (cmbType.Text == "IMPORTS")
            {
                txtJob.Enabled = false;
                btnJobSearch.Enabled = false;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = true;
                txtStatus.Text = "GOD";
                txtStatus.Enabled = false;
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpPoDate.Focus();
            }
        }

        private void cmbPayTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayTerm.Text == "CASH")
            {
                txtCreditPeriod.Text = "0";
            }
            else
            {
                txtCreditPeriod.Text = "30";
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtStatus.Enabled == true)
                    {
                        txtStatus.Focus();
                    }
                    else
                    {
                        txtQty.Focus();
                    }
                }
                else if (e.KeyCode == Keys.F3)
                {
                    if (cmbType.Text == "SERVICE")
                        if (!string.IsNullOrEmpty(txtItem.Text))
                        {
                            txtItemDesc.Visible = true;
                            txtItemDesc.Focus();
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

        private void txtStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        MessageBox.Show("First you have to select item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtStatus;
                    _CommonSearch.ShowDialog();
                    txtStatus.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtQty.Focus();
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

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    if (!IsNumeric(txtQty.Text))
                    {
                        MessageBox.Show("Qty should be numiric.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQty.Text = "";
                        txtQty.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtSupCode.Text))
                    {
                        MessageBox.Show("Please select supplier.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQty.Text = "";
                        txtSupCode.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        MessageBox.Show("Please select item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQty.Text = "";
                        txtItem.Focus();
                        return;
                    }
                    
                    GetSupplierQuotation();
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

        private void GetSupplierQuotation()
        {
            String _type = "";
            if (cmbType.Text == "CONSIGNMENT")
            {
                _type = "C";
            }
            else if (cmbType.Text == "NORMAL")
            {
                _type = "N";
            }
            else if (cmbType.Text == "SERVICE")
            {
                _type = "S";
            }
            else if (cmbType.Text == "IMPORTS")
            {
                _type = "I";
            }
            else
            {
                MessageBox.Show("Quotations will not load due to undefine order type.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            List<QuotationHeader> _list = CHNLSVC.Inventory.GetLatestValidQuotation(BaseCls.GlbUserComCode, txtSupCode.Text, "S", _type, Convert.ToDateTime(dtpPoDate.Value).Date, Convert.ToDecimal(txtQty.Text), "A", txtItem.Text);
            dgvQuo.AutoGenerateColumns = false;
            dgvQuo.DataSource = new List<QuotationHeader>();
            dgvQuo.DataSource = _list;
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetSupplierQuotation();
                
                if (txtUnitPrice.Enabled == true)
                {
                    txtUnitPrice.Focus();
                }
                else
                {
                    if (txtUnitPrice.Text=="" || string.IsNullOrEmpty(txtUnitPrice.Text))
                    {
                        MessageBox.Show("Select Quation value", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    txtAmount.Focus();
                
                }
               
            }
        
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    if (!IsNumeric(txtUnitPrice.Text))
                    {
                        MessageBox.Show("Invalid unit amount.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUnitPrice.Text = "";
                        txtUnitPrice.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtQty.Text))
                    {
                        MessageBox.Show("Please enter qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUnitPrice.Text = "";
                        txtQty.Focus();
                    }
                    else
                    {
                        decimal _unitAmt = 0;
                        decimal _qty = 0;
                        decimal _amount = 0;
                        _qty = Convert.ToDecimal(txtQty.Text);
                        _unitAmt = Convert.ToDecimal(txtUnitPrice.Text);
                        //txtUnitPrice.Text = _unitAmt.ToString("n");
                        _amount = _qty * _unitAmt;

                        txtAmount.Text = _amount.ToString("n");                        
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

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisRate.Focus();
            }
        }

        private void txtDisRate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    if (!IsNumeric(txtDisRate.Text))
                    {
                        MessageBox.Show("Invalid discount rate.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "0";
                        txtDisRate.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(txtDisRate.Text) > 100)
                    {
                        MessageBox.Show("Invalid discount rate.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "0";
                        txtDisRate.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtAmount.Text))
                    {
                        MessageBox.Show("Amount is not calculate. Please enter unit price and press enter key.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "0";
                        txtUnitPrice.Focus();
                        return;
                    }
                    else
                    {
                        decimal _amount = 0;
                        decimal _disRate = 0;
                        decimal _disAmt = 0;
                        _amount = Convert.ToDecimal(txtAmount.Text);
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                        _disAmt = _amount * _disRate / 100;
                        txtDiscount.Text = _disAmt.ToString("n");
                        txtDisRate.Text = _disRate.ToString("0.00000");

                    }
                }
                else
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    txtDisRate.Text = _disRate.ToString("0.00000");
                    txtDiscount.Text = _disAmt.ToString("n");
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

        private void txtDisRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiscount.Focus();
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal _amt = 0;
                decimal _disrate = 0;
                decimal _disamt = 0;
                decimal _tax = 0;
                decimal _total = 0;

                if (!string.IsNullOrEmpty(txtDiscount.Text))
                {
                    if (!IsNumeric(txtDiscount.Text))
                    {
                        MessageBox.Show("Invalid discount amount.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDiscount.Text = "0";
                        txtDisRate.Text = "0";
                        txtDiscount.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(txtDiscount.Text) > Convert.ToDecimal(txtAmount.Text))
                    {
                        MessageBox.Show("Invalid discount amount.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDiscount.Text = "0";
                        txtDisRate.Text = "0";
                        txtDiscount.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtAmount.Text))
                    {
                        MessageBox.Show("Amount is not calculate. Please enter unit price and press enter key.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "0";
                        txtDiscount.Text = "0";
                        txtAmount.Focus();
                        return;
                    }
                    else
                    {
                        _amt = Convert.ToDecimal(txtAmount.Text);
                        _disamt = Convert.ToDecimal(txtDiscount.Text);
                        if (_amt > 0)
                        {
                            _disrate = _disamt / _amt * 100;
                        }
                        else
                        {
                            _disrate = 0;
                        }
                        txtDisRate.Text = _disrate.ToString("0.00000");
                        txtDiscount.Text = _disamt.ToString("n");
                        _tax = TaxCalculation(BaseCls.GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), _amt - _disamt, 0);
                        txtTax.Text = _tax.ToString("n");
                        _total = (_amt - _disamt) + _tax;
                        txtTotal.Text = _total.ToString("n");

                        decimal _unitAmt = 0;
                        decimal _qty = 0;
                        decimal _amount = 0;

                        _qty = Convert.ToDecimal(txtQty.Text);
                        _unitAmt = Convert.ToDecimal(txtUnitPrice.Text);
                        _amount = _qty * _unitAmt;

                        //kapila 25/4/2017
                        if (_supDet != null)
                        {
                            if (!string.IsNullOrEmpty(_supDet.Mbe_cate))
                            {

                                MasterItemTaxClaim _claimTax = new MasterItemTaxClaim();
                                decimal _claimTax_Rt = 0;
                                _claimTax = CHNLSVC.Sales.GetTaxClaimDet(BaseCls.GlbUserComCode, txtItem.Text.Trim(), _supDet.Mbe_cate);
                                if (_claimTax != null)
                                {
                                    _claimTax_Rt = _claimTax.Mic_claim;
                                }
                                lblCRate.Text = _claimTax_Rt.ToString("0.00");


                                decimal _disRate = string.IsNullOrEmpty(txtDisRate.Text) ? 0 : Convert.ToDecimal(txtDisRate.Text);
                                decimal _disAmount = _amount * _disRate / 100;

                                decimal _taxRate = getClaimableTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), _amount - _disAmount, _supDet.Mbe_cate, 0);

                                //kapila 26/5/2017
                                if (_taxRate > 0)
                                    lblCTax.Text = (_tax / _taxRate * _claimTax_Rt).ToString("0.00");
                                lblCAmt.Text = (_total - Convert.ToDecimal(lblCTax.Text)).ToString("0.00");
                            }
                        }
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

        private decimal TaxCalculation(string _com, string _item, string _status, decimal _UnitPrice, decimal _TaxVal)
        {
            decimal _totNBT = 0;
            decimal _NBT = 0;
            decimal _oTax = 0;
            _TaxVal = 0;

            if (_supDet.Mbe_is_tax == true)
            {
                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)       //kapila 9/5/2017
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_com, _item);
                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(_com, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                }
                else
                    _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);

                var _Tax = from _itm in _taxs
                           select _itm;
                foreach (MasterItemTax _one in _Tax)
                {
                    if (_one.Mict_tax_cd == "NBT")
                    {
                        _NBT = _UnitPrice * _one.Mict_tax_rate / 100;
                        _TaxVal = _TaxVal + _NBT;
                        _totNBT = _totNBT + _NBT;
                    }
                    //_TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                }

                foreach (MasterItemTax _two in _Tax)
                {
                    if (_two.Mict_tax_cd != "NBT")
                    {
                        _oTax = (_UnitPrice + _totNBT) * _two.Mict_tax_rate / 100;
                        _TaxVal = _TaxVal + _oTax;

                    }
                }
            }
            else
            {
                _TaxVal = 0;
            }


            return _TaxVal;
        }

        private decimal TaxCalculationActualCost(string _com, string _item, string _status, decimal _UnitPrice, string _supTaxCate, decimal _actTaxVal)
        {
            decimal _totNBT = 0;
            decimal _NBT = 0;
            decimal _oTax = 0;
            decimal _claimTaxRt = 0;

            _actTaxVal = 0;

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila 9/5/2017
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_com, _item);
                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(_com, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
            }
            else
                _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            var _Tax = from _itm in _taxs
                       select _itm;

            MasterItemTaxClaim _claimTax = new MasterItemTaxClaim();
            _claimTax = CHNLSVC.Sales.GetTaxClaimDet(_com, _item, _supTaxCate);


            foreach (MasterItemTax _one in _Tax)
            {
                if (_one.Mict_tax_cd == "NBT")
                {
                    _NBT = _UnitPrice * _one.Mict_tax_rate / 100;
                    _actTaxVal = _actTaxVal + _NBT;
                    _totNBT = _totNBT + _NBT;
                }
                //_TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
            }

            foreach (MasterItemTax _two in _Tax)
            {
                if (_two.Mict_tax_cd != "NBT")
                {
                    if (_claimTax != null)
                    {
                        _claimTaxRt = _two.Mict_tax_rate - _claimTax.Mic_claim;
                    }
                    else
                    {
                        _claimTaxRt = _two.Mict_tax_rate;
                    }

                    _oTax = (_UnitPrice + _totNBT) * _claimTaxRt / 100;
                    _actTaxVal = _actTaxVal + _oTax;

                }
            }


            return _actTaxVal;
        }


        private decimal getClaimableTax(string _com, string _item, string _status, decimal _UnitPrice, string _supTaxCate, decimal _actTaxRate)
        {
            _actTaxRate = 0;

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila 9/5/2017
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_com, _item);
                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(_com, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
            }
            else
                _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            var _Tax = from _itm in _taxs
                       select _itm;

            foreach (MasterItemTax _two in _Tax)
            {
                if (_two.Mict_tax_cd != "NBT")
                {
                    _actTaxRate = _two.Mict_tax_rate;
                }
            }

            return _actTaxRate;
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdAddItem.Focus();
            }
        }

        private void cmdAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isVatClaim = false;
                string _suppTaxCate = "";
                if (string.IsNullOrEmpty(txtSupCode.Text))
                {
                    MessageBox.Show("Please select supplier code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSupCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    MessageBox.Show("Please select currecy code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCurrency.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please enter purchasing item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtStatus.Text))
                {
                    MessageBox.Show("Please select item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter purchasing qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    MessageBox.Show("Please enter item unit price.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    MessageBox.Show("Amount is not calculation. Please re-enter unit price.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    MessageBox.Show("Discount rate is not found.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDiscount.Text))
                {
                    MessageBox.Show("Discount amount is not found.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiscount.Focus();
                    return;
                }

                Boolean _proceed = check_Total(Convert.ToDecimal(txtTotal.Text));

                if (_proceed == false)
                {
                    MessageBox.Show("Found amount mismatches. Please re-enter this item details.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var currrange = (from cur in _POItemList
                                 where cur.Pod_itm_cd == txtItem.Text.Trim() && cur.Pod_unit_price == Convert.ToDecimal(txtUnitPrice.Text) && cur.Pod_itm_stus == txtStatus.Text.Trim()
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {
                    MessageBox.Show("Selected item already exsist with same price.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();
                PurchaseOrderAlloc _tmpPoAloc = new PurchaseOrderAlloc();
                MasterItem _tmpItem = new MasterItem();

                _tmpItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                if (_POItemList.Count > 0)
                {
                    var max_Query =
                 (from tab1 in _POItemList
                  select tab1.Pod_line_no).Max();

                    _lineNo = max_Query;
                }
                else
                {
                    _lineNo = 0;
                }

                if (_supDet != null)
                {
                    _isVatClaim = _supDet.Mbe_is_tax;
                    _suppTaxCate = _supDet.Mbe_cate;
                }
                else
                {
                    MessageBox.Show("Cannot find supplier details.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal _taxForActual = 0;

                if (_tmpItem != null)
                {
                    _lineNo = _lineNo + 1;
                    // Add po items ______________________

                    if (string.IsNullOrEmpty(_suppTaxCate))
                    {
                        _tmpPoDetails.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text) + Convert.ToDecimal(txtTax.Text)) / Convert.ToDecimal(txtQty.Text);
                    }
                    else
                    {
                        decimal _unitVal = Convert.ToDecimal(txtUnitPrice.Text);
                        decimal _qty = Convert.ToDecimal(txtQty.Text);
                        decimal _amt = _unitVal * _qty;
                        decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                        decimal _disAmount = _amt * _disRate / 100;

                        _taxForActual = TaxCalculationActualCost(BaseCls.GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), _amt - _disAmount, _suppTaxCate, 0);
                        _tmpPoDetails.Pod_act_unit_price = ((Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text)) + _taxForActual) / Convert.ToDecimal(txtQty.Text);
                    }
                    _tmpPoDetails.Pod_dis_amt = Convert.ToDecimal(txtDiscount.Text);
                    _tmpPoDetails.Pod_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                    _tmpPoDetails.Pod_grn_bal = Convert.ToDecimal(txtQty.Text);
                    //kapila 13/6/2017
                    if (cmbType.Text == "SERVICE")
                        _tmpPoDetails.Pod_item_desc = lblItemDescription.Text;
                    else
                        _tmpPoDetails.Pod_item_desc = _tmpItem.Mi_shortdesc;
                    _tmpPoDetails.Pod_itm_cd = txtItem.Text.Trim();
                    _tmpPoDetails.Pod_itm_stus = txtStatus.Text.Trim();
                    _tmpPoDetails.Pod_itm_tp = _tmpItem.Mi_itm_tp;
                    _tmpPoDetails.Pod_kit_itm_cd = "";
                    _tmpPoDetails.Pod_kit_line_no = 0;
                    _tmpPoDetails.Pod_lc_bal = 0;
                    _tmpPoDetails.Pod_line_amt = Convert.ToDecimal(txtTotal.Text);
                    _tmpPoDetails.Pod_line_no = _lineNo;
                    _tmpPoDetails.Pod_line_tax = Convert.ToDecimal(txtTax.Text);
                    _tmpPoDetails.Pod_line_val = Convert.ToDecimal(txtAmount.Text);
                    _tmpPoDetails.Pod_nbt = 0;
                    _tmpPoDetails.Pod_nbt_before = 0;
                    _tmpPoDetails.Pod_pi_bal = 0;
                    _tmpPoDetails.Pod_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDetails.Pod_ref_no = txtSupRef.Text;
                    _tmpPoDetails.Pod_seq_no = 12;
                    _tmpPoDetails.Pod_si_bal = 0;
                    _tmpPoDetails.Pod_tot_tax_before = 0;
                    _tmpPoDetails.Pod_unit_price = Convert.ToDecimal(txtUnitPrice.Text);
                    _tmpPoDetails.Pod_uom = _tmpItem.Mi_itm_uom;
                    _tmpPoDetails.Pod_vat = 0;
                    _tmpPoDetails.Pod_vat_before = 0;
                    //kapila 25/4/2017
                    _tmpPoDetails.Claim_rate = Convert.ToDecimal(lblCRate.Text);
                    _tmpPoDetails.Claim_amt = Convert.ToDecimal(lblCTax.Text);
                    _tmpPoDetails.Claim_price = Convert.ToDecimal(lblCAmt.Text);
                    #region get warrnty details :: by tharanga 2017/11/19
                    //Getcustomerpluupdate = CHNLSVC.Sales.Getcustomerpluupdate(BaseCls.GlbUserComCode, txtSupCode.Text.Trim().ToUpper(), txtItem.Text.ToString());
                    //if (Getcustomerpluupdate.Count > 0)
                    //{
                    //    foreach (mst_busentity_itm _mst_busentity_itm in Getcustomerpluupdate)
                    //    {
                    //        _tmpPoDetails.Pod_warr_per = _mst_busentity_itm.mbii_warr_peri.ToString();
                    //        _tmpPoDetails.Pod_warr_rmk = _mst_busentity_itm.mbii_warr_rmk;
                    //        _tmpPoDel.Podi_warr_period = _mst_busentity_itm.mbii_warr_peri;
                    //        _tmpPoDel.Podi_warr_remrk = _mst_busentity_itm.mbii_warr_rmk;
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Supplier warranty not setup", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    #endregion
                    

                    _POItemList.Add(_tmpPoDetails);

                    if (_POItemDel.Count > 0)
                    {
                        //var max_DelQuery = (from tab1 in _POItemDel select tab1.Podi_del_line_no).Max();
                        //_delLineNo = max_DelQuery;
                        //Edit by Chamal 24/06/2013
                        var result = (from rs in _POItemDel where rs.Podi_line_no == _lineNo select rs.Podi_del_line_no).ToList();
                        if (result != null && result.Count > 0)
                        {
                            _delLineNo = Convert.ToInt32(result.Max());
                        }
                        else _delLineNo = 0;
                    }
                    else
                    {
                        _delLineNo = 0;
                    }

                    // Add po defualt delivery details
                    _delLineNo = _delLineNo + 1;
                    _tmpPoDel.Podi_seq_no = 12;
                    _tmpPoDel.Podi_line_no = _lineNo;
                    _tmpPoDel.Podi_del_line_no = _delLineNo;
                    _tmpPoDel.Podi_loca = BaseCls.GlbUserDefLoca;
                    _tmpPoDel.Podi_itm_cd = txtItem.Text.Trim();
                    _tmpPoDel.Podi_itm_stus = txtStatus.Text.Trim();
                    _tmpPoDel.Podi_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDel.Podi_bal_qty = Convert.ToDecimal(txtQty.Text);
                    
                    _POItemDel.Add(_tmpPoDel);

                    //kapila 28/10/2015
                    //if (_POItemAloc.Count > 0)
                    //{
                    //    var result = (from rs in _POItemDel where rs.Podi_line_no == _lineNo select rs.Podi_del_line_no).ToList();
                    //    if (result != null && result.Count > 0)
                    //    {
                    //        _delAlocLineNo = Convert.ToInt32(result.Max());
                    //    }
                    //    else _delAlocLineNo = 0;
                    //}
                    //else
                    //{
                    //    _delAlocLineNo = 0;
                    //}

                    //// Add po defualt allocation details
                    //_delAlocLineNo = _delAlocLineNo + 1;
                    //_tmpPoAloc.poal_seq_no = 12;
                    //_tmpPoAloc.poal_line_no = _lineNo;
                    //_tmpPoAloc.poal_del_line_no = _delAlocLineNo;
                    //_tmpPoAloc.poal_loca = BaseCls.GlbUserDefLoca;
                    //_tmpPoAloc.poal_itm_cd = txtItem.Text.Trim();
                    //_tmpPoAloc.poal_itm_stus = txtStatus.Text.Trim();
                    //_tmpPoAloc.poal_qty = Convert.ToDecimal(txtQty.Text);
                    //_tmpPoAloc.poal_bal_qty = Convert.ToDecimal(txtQty.Text);

                    //_POItemAloc.Add(_tmpPoAloc);
                }
                else
                {
                    MessageBox.Show("Item details not loading.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvPOItems.AutoGenerateColumns = false;
                dgvPOItems.DataSource = new List<PurchaseOrderDetail>();
                dgvPOItems.DataSource = _POItemList;

                dgvDel.AutoGenerateColumns = false;
                dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                dgvDel.DataSource = _POItemDel;

                dgvAloc.AutoGenerateColumns = false;
                dgvAloc.DataSource = new List<PurchaseOrderAlloc>();
                dgvAloc.DataSource = _POItemAloc;

                dgvQuo.AutoGenerateColumns = false;
                dgvQuo.DataSource = new List<QoutationDetails>();


                //kapila 3/11/2015
                if (BaseCls.GlbUserComCode == "AST")
                {
                    DataTable _dt = new DataTable();
                    DataRow dr;

                    lblAlocItem.Text = txtItem.Text;
                    lblAlocStatus.Text = txtStatus.Text;
                    txtAlocQty.Text = txtQty.Text;

                    _dt.Columns.Add("LOC", typeof(string));
                    _dt.Columns.Add("QTY", typeof(decimal));

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);

                    foreach (DataRow row in _result.Rows)
                    {
                        dr = _dt.NewRow();

                        dr["LOC"] = row["code"].ToString();
                        dr["QTY"] = 0;

                        _dt.Rows.Add(dr);
                    }

                    grvAlocLoc.AutoGenerateColumns = false;
                    grvAlocLoc.DataSource = _dt;

                    pnl_aloc.Visible = true;
                }

                Cal_Totals();

                clear_item();
                txtSupCode.Enabled = false;


                txtItem.Focus();
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

        private Boolean check_Total(decimal _tot)
        {
            decimal _unitVal = 0;
            decimal _qty = 0;
            decimal _amt = 0;
            decimal _disRate = 0;
            decimal _disAmount = 0;
            decimal _tax = 0;
            decimal _total = 0;
            Boolean _correct = false;
            decimal _diff = 0;


            _unitVal = Convert.ToDecimal(txtUnitPrice.Text);
            _qty = Convert.ToDecimal(txtQty.Text);

            _amt = Math.Round(_unitVal * _qty, 2);
       
            _disRate = Convert.ToDecimal(txtDisRate.Text);
            _disAmount = Math.Round(_amt * _disRate / 100, 2);
            _tax = TaxCalculation(BaseCls.GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), _amt - _disAmount, 0);
            _total = Math.Round((_amt - _disAmount) + _tax, 2);
            _diff = _tot - _total;

            if (_tot == _total)
            {
                _correct = true;
            }
            else if (_diff > 0)
            {
                if (_diff > 10)
                {
                    _correct = false;
                }
                else
                {
                    _correct = true;
                }
            }
            else if (_diff < 0)
            {
                if (_diff > -10)
                {
                    _correct = false;
                }
                else
                {
                    _correct = true;
                }
            }
            else
            {
                _correct = false;
            }

            return _correct;
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    Boolean _isValid = false;
                    _isValid = CHNLSVC.Inventory.IsValidCompanyItem(BaseCls.GlbUserComCode, txtItem.Text, 1);

                    if (_isValid == false)
                    {
                        MessageBox.Show("Invalid item selected.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Text = "";
                        lblItemDescription.Text = "";
                        txtItemDesc.Visible = false;
                        txtItem.Focus();
                        return;
                    }

                    //kapila 14/8/2015
                    MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                    if (_itemdetail != null)
                    {
                        //kapila 29/1/2016
                        lblItemDescription.Text = _itemdetail.Mi_shortdesc;
                        //commented kapila on 12/6/2017 req by darshana/asanka AAL implementation
                        if (cmbType.Text != "SERVICE")
                            if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                            {
                                if (_itemdetail.Mi_itm_tp == "V")
                                {
                                    MessageBox.Show("Virtual item not allowed", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtItem.Clear();
                                }
                            }
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

        private void clear_item()
        {
            txtItem.Text = "";
            lblItemDescription.Text = "";
            if (cmbType.Text == "CONSIGNMENT")
            {
                txtStatus.Text = "CONS";
            }
            else if (cmbType.Text == "IMPORTS")
            {
                txtStatus.Text = "GOD";
            }
            else
            {
                txtStatus.Text = "GDLP";
            }
            txtQty.Text = "";
            txtUnitPrice.Text = "";
            txtAmount.Text = "";
            txtDisRate.Text = "";
            txtDiscount.Text = "";
            txtTax.Text = "";
            txtTotal.Text = "";
            lblCAmt.Text = "0.00";
            lblCRate.Text = "0.00";
            lblCTax.Text = "0.00";

        }

        private void Cal_Totals()
        {
            decimal _amtBeforeDis = 0;
            decimal _disAmt = 0;
            decimal _amtAfterDis = 0;
            decimal _taxAmt = 0;
            decimal _totAmt = 0;

            foreach (PurchaseOrderDetail _tmpPo in _POItemList)
            {
                _amtBeforeDis = _amtBeforeDis + _tmpPo.Pod_line_val;
                _disAmt = _disAmt + _tmpPo.Pod_dis_amt;
                _amtAfterDis = _amtBeforeDis - _disAmt;
                _taxAmt = _taxAmt + _tmpPo.Pod_line_tax;
                _totAmt = _totAmt + _tmpPo.Pod_line_amt;
            }

            lblSubTot.Text = Convert.ToString(_amtBeforeDis.ToString("n"));
            lblDisAmt.Text = Convert.ToString(_disAmt.ToString("n"));
            lblTotAfterDis.Text = Convert.ToString(_amtAfterDis.ToString("n"));
            lblTaxAmt.Text = Convert.ToString(_taxAmt.ToString("n"));
            lblTotal.Text = Convert.ToString(_totAmt.ToString("n"));
        }

        private void dgvPOItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 11 && e.RowIndex != -1)
                {
                    if (MessageBox.Show("Do you want to remove selected item details ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_POItemList == null || _POItemList.Count == 0) return;

                        Int32 _line = Convert.ToInt32(dgvPOItems.Rows[e.RowIndex].Cells["col_seq"].Value);


                        List<PurchaseOrderDetail> _temp = new List<PurchaseOrderDetail>();
                        _temp = _POItemList;

                        _temp.RemoveAll(x => x.Pod_line_no == _line);
                        _POItemList = _temp;

                        dgvPOItems.AutoGenerateColumns = false;
                        dgvPOItems.DataSource = new List<PurchaseOrderDetail>();
                        dgvPOItems.DataSource = _POItemList;

                        if (_POItemDel == null || _POItemDel.Count == 0) return;

                        List<PurchaseOrderDelivery> _tmpDel = new List<PurchaseOrderDelivery>();
                        _tmpDel = _POItemDel;

                        _tmpDel.RemoveAll(x => x.Podi_line_no == _line);
                        _POItemDel = _tmpDel;

                        dgvDel.AutoGenerateColumns = false;
                        dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                        dgvDel.DataSource = _POItemDel;

                        //kapila 28/10/2015
                        List<PurchaseOrderAlloc> _tmpAloc = new List<PurchaseOrderAlloc>();
                        _tmpAloc = _POItemAloc;

                        _tmpAloc.RemoveAll(x => x.poal_line_no == _line);
                        _POItemAloc = _tmpAloc;

                        dgvAloc.AutoGenerateColumns = false;
                        dgvAloc.DataSource = new List<PurchaseOrderAlloc>();
                        dgvAloc.DataSource = _POItemAloc;

                        Cal_Totals();

                        if (_POItemList.Count == 0)
                        {
                            txtSupCode.Enabled = true;
                        }
                    }
                }
                else if (e.ColumnIndex == 1 && e.RowIndex != -1)
                {
                    if (dgvPOItems.Rows.Count == 0) return;
                    _AddDelQty = 0;
                    Int32 _line = Convert.ToInt32(dgvPOItems.Rows[e.RowIndex].Cells["col_seq"].Value);
                    string _itm = Convert.ToString(dgvPOItems.Rows[e.RowIndex].Cells["col_Item"].Value);
                    string _status = Convert.ToString(dgvPOItems.Rows[e.RowIndex].Cells["col_Status"].Value);
                    _AddDelQty = Convert.ToDecimal(dgvPOItems.Rows[e.RowIndex].Cells["col_Qty"].Value);

                    lblItemLine.Text = _line.ToString();
                    lblDelItem.Text = _itm;
                    lblDelStatus.Text = _status;

                    //kapila
                    lblAlocItemLine.Text = _line.ToString();
                    lblAlocItem.Text = _itm;
                    lblAlocStatus.Text = _status;

                    decimal _delQty = 0;
                    decimal _balDelQty = 0;

                    foreach (PurchaseOrderDelivery _tmpDel in _POItemDel)
                    {
                        if (_tmpDel.Podi_line_no == Convert.ToInt32(lblItemLine.Text) && _tmpDel.Podi_itm_cd == lblDelItem.Text)
                        {
                            _delQty = _delQty + _tmpDel.Podi_qty;
                        }
                    }

                    _balDelQty = _AddDelQty - _delQty;
                    txtDelQty.Text = _balDelQty.ToString();

                    _balDelQty = 0;
                    _delQty = 0;
                    foreach (PurchaseOrderAlloc _tmpDel in _POItemAloc)
                    {
                        if (_tmpDel.poal_line_no == Convert.ToInt32(lblAlocItemLine.Text) && _tmpDel.poal_itm_cd == lblAlocItem.Text)
                        {
                            _delQty = _delQty + _tmpDel.poal_qty;
                        }
                    }

                    _balDelQty = _AddDelQty - _delQty;
                    txtAlocQty.Text = _balDelQty.ToString();
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

        private void dgvDel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5 && e.RowIndex != -1)
                {
                    if (MessageBox.Show("Do you want to remove selected delivery details ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_POItemDel == null || _POItemDel.Count == 0) return;

                        Int32 _line = Convert.ToInt32(dgvDel.Rows[e.RowIndex].Cells["col_dNo"].Value);


                        List<PurchaseOrderDelivery> _temp = new List<PurchaseOrderDelivery>();
                        _temp = _POItemDel;

                        _temp.RemoveAll(x => x.Podi_del_line_no == _line);
                        _POItemDel = _temp;

                        dgvDel.AutoGenerateColumns = false;
                        dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                        dgvDel.DataSource = _POItemDel;
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

        private void btnAddDelAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDelLoca.Text))
                {
                    MessageBox.Show("Please select relavant delivery location.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelLoca.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    MessageBox.Show("Order is already approved.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    MessageBox.Show("Order is already cancelled.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                _delLineNo = 0;
                _POItemDel = new List<PurchaseOrderDelivery>();
                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();

                foreach (PurchaseOrderDetail _tmpList in _POItemList)
                {
                    if (_POItemDel.Count > 0)
                    {
                        var max_DelQuery =
                        (from tab1 in _POItemDel
                         select tab1.Podi_del_line_no).Max();

                        _delLineNo = max_DelQuery;
                    }
                    else
                    {
                        _delLineNo = 0;
                    }
                    _tmpPoDel = new PurchaseOrderDelivery();
                    _delLineNo = _delLineNo + 1;
                    _tmpPoDel.Podi_seq_no = 12;
                    _tmpPoDel.Podi_line_no = _tmpList.Pod_line_no;
                    _tmpPoDel.Podi_del_line_no = _delLineNo;
                    _tmpPoDel.Podi_loca = txtDelLoca.Text.Trim();
                    _tmpPoDel.Podi_itm_cd = _tmpList.Pod_itm_cd;
                    _tmpPoDel.Podi_itm_stus = _tmpList.Pod_itm_stus;
                    _tmpPoDel.Podi_qty = _tmpList.Pod_qty;
                    _tmpPoDel.Podi_bal_qty = _tmpList.Pod_qty;

                    _POItemDel.Add(_tmpPoDel);
                }
                dgvDel.AutoGenerateColumns = false;
                dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                dgvDel.DataSource = _POItemDel;
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

        private void btnAddDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDelQty.Text))
                {
                    MessageBox.Show("Please enter delivery qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDelLoca.Text))
                {
                    MessageBox.Show("Please enter delivery location.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelLoca.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    MessageBox.Show("Order is already approved.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    MessageBox.Show("Order is already cancelled.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                decimal _delQty = 0;
                decimal _qty = Convert.ToDecimal(txtDelQty.Text);

                foreach (PurchaseOrderDelivery _tmpDel in _POItemDel)
                {
                    if (_tmpDel.Podi_line_no == Convert.ToInt32(lblItemLine.Text) && _tmpDel.Podi_loca == txtDelLoca.Text.Trim())
                    {
                        MessageBox.Show("Already exit same item with same location. Please remove that and add again.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_tmpDel.Podi_line_no == Convert.ToInt32(lblItemLine.Text) && _tmpDel.Podi_itm_cd == lblDelItem.Text)
                    {
                        _delQty = _delQty + _tmpDel.Podi_qty;
                    }
                }

                if (_AddDelQty < (_qty + _delQty))
                {
                    MessageBox.Show("Delivery schedule QTY exceeds Purchase QTY.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelQty.Text = _AddDelQty.ToString();
                    txtDelQty.Focus();
                    return;
                }


                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();

                if (_POItemDel.Count > 0)
                {
                    var max_DelQuery =
                       (from tab1 in _POItemDel
                        select tab1.Podi_del_line_no).Max();

                    _delLineNo = max_DelQuery;
                }
                else
                {
                    _delLineNo = 0;
                }

                _delLineNo = _delLineNo + 1;
                _tmpPoDel.Podi_seq_no = 12;
                _tmpPoDel.Podi_line_no = Convert.ToInt32(lblItemLine.Text);
                _tmpPoDel.Podi_del_line_no = _delLineNo;
                _tmpPoDel.Podi_loca = txtDelLoca.Text.Trim();
                _tmpPoDel.Podi_itm_cd = lblDelItem.Text.Trim();
                _tmpPoDel.Podi_itm_stus = lblDelStatus.Text.Trim();
                _tmpPoDel.Podi_qty = Convert.ToDecimal(txtDelQty.Text);
                _tmpPoDel.Podi_bal_qty = Convert.ToDecimal(txtDelQty.Text);

                _POItemDel.Add(_tmpPoDel);

                dgvDel.AutoGenerateColumns = false;
                dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                dgvDel.DataSource = _POItemDel;

                if (_AddDelQty - (_qty + _delQty) > 0)
                {
                    txtDelQty.Text = Convert.ToString(_AddDelQty - (_qty + _delQty));
                    txtDelLoca.Text = "";
                    txtDelLoca.Focus();
                }
                else if (_AddDelQty - (_qty + _delQty) == 0)
                {
                    lblDelItem.Text = "";
                    lblDelStatus.Text = "";
                    lblItemLine.Text = "";
                    txtDelQty.Text = "";
                    txtDelLoca.Text = "";
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

        private void txtPurNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                    //DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = txtPurNo;
                    //_CommonSearch.ShowDialog();
                    //txtPurNo.Select();
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
                    _CommonSearch.obj_TragetTextBox = txtPurNo;
                    this.Cursor = Cursors.Default;
                    //_CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtPurNo.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSupCode.Focus();
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

        private void btnOrderSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                //DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtPurNo;
                //_CommonSearch.ShowDialog();
                //txtPurNo.Select();

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
                _CommonSearch.obj_TragetTextBox = txtPurNo;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtPurNo.Select();

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

        private void txtPurNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                //DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtPurNo;
                //_CommonSearch.ShowDialog();
                //txtPurNo.Select();
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
                _CommonSearch.obj_TragetTextBox = txtPurNo;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtPurNo.Select();
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

        private void LoadSaveDocument()
        {

            _IsRecall = false;
            _POstatus = "";
            IsPoCanCancel = false; //by akila 2017/07/03

            FF.BusinessObjects.PurchaseOrder _POHeader = new FF.BusinessObjects.PurchaseOrder();
            _POHeader = CHNLSVC.Inventory.GetPOHeader(BaseCls.GlbUserComCode, txtPurNo.Text.Trim(), "L");

            if (_POHeader != null)
            {
                txtSupCode.Text = _POHeader.Poh_supp;
                txtSupRef.Text = _POHeader.Poh_ref;
                txtRemarks.Text = _POHeader.Poh_remarks;
                txtCurrency.Text = _POHeader.Poh_cur_cd;
                lblEx.Text = _POHeader.Poh_ex_rt.ToString();
                dtpPoDate.Text = Convert.ToDateTime(_POHeader.Poh_dt).ToShortDateString();
                lblSubTot.Text = Convert.ToDecimal(_POHeader.Poh_sub_tot).ToString("n");
                lblDisAmt.Text = Convert.ToDecimal(_POHeader.Poh_dis_amt).ToString("n");
                lblTotAfterDis.Text = Convert.ToDecimal(_POHeader.Poh_sub_tot - _POHeader.Poh_dis_amt).ToString("n");
                lblTaxAmt.Text = Convert.ToDecimal(_POHeader.Poh_tax_tot).ToString("n");
                lblTotal.Text = Convert.ToDecimal(_POHeader.Poh_tot).ToString("n");
                cmbPayTerm.Text = _POHeader.Poh_pay_term;
                txtCreditPeriod.Text = _POHeader.Poh_cre_period;
                txtJob.Text = _POHeader.Poh_job_no;

                POSubType = _POHeader.Poh_sub_tp;

                if (_POHeader.Poh_stus == "A")
                {
                    lblStatus.Text = "APPROVED";
                    btnApprove.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                }
                else if (_POHeader.Poh_stus == "P")
                {
                    lblStatus.Text = "PENDING";
                    btnApprove.Enabled = true;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                }
                else if (_POHeader.Poh_stus == "C")
                {
                    lblStatus.Text = "CANCELLED";
                    btnApprove.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                }
                else if (_POHeader.Poh_stus == "F")
                {
                    lblStatus.Text = "COMPLETED";
                    btnApprove.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                }

                if (_POHeader.Poh_sub_tp == "N")
                {
                    cmbType.Text = "NORMAL";
                }
                else if (_POHeader.Poh_sub_tp == "C")
                {
                    cmbType.Text = "CONSIGNMENT";
                }
                else if (_POHeader.Poh_sub_tp == "S")
                {
                    cmbType.Text = "SERVICE";
                }
                else if (_POHeader.Poh_sub_tp == "I")
                {
                    cmbType.Text = "IMPORTS";
                }

                if (_POHeader.Poh_tp == "I")
                {
                    cmbType.Text = "IMPORTS";
                }

                if (_POHeader.poh_is_conspo == 0)
                {
                    chkBaseToConsGrn.Checked = false;
                }
                else if (_POHeader.poh_is_conspo == 1)
                {
                    chkBaseToConsGrn.Checked = true;
                }
                else
                {
                    chkBaseToConsGrn.Checked = false;
                }

                _POstatus = _POHeader.Poh_stus;
                _POSeqNo = _POHeader.Poh_seq_no;
                if (_POHeader.Poh_sub_tp == "C")
                {
                    //_IsCons = true;
                    //chkIsCons.Checked = true;
                    txtStatus.Text = "CONS";
                    txtStatus.Enabled = false;
                    chkBaseToConsGrn.Enabled = true;
                }
                else
                {
                    // _IsCons = false;
                    // chkIsCons.Checked = false;
                    txtStatus.Text = "";
                    txtStatus.Enabled = true;
                    chkBaseToConsGrn.Enabled = false;
                }

                PurchaseOrderDetail _paramPOItems = new PurchaseOrderDetail();

                _paramPOItems.Pod_seq_no = _POSeqNo;

                List<PurchaseOrderDetail> _list = CHNLSVC.Inventory.GetPOItems(_paramPOItems);
                _POItemList = new List<PurchaseOrderDetail>();
                _POItemList = _list;

                var max_Query =
             (from tab1 in _POItemList
              select tab1.Pod_line_no).Max();

                _lineNo = max_Query;

                dgvPOItems.AutoGenerateColumns = false;
                dgvPOItems.DataSource = new List<PurchaseOrderDetail>();
                dgvPOItems.DataSource = _POItemList;


                PurchaseOrderDelivery _paramPOdelItems = new PurchaseOrderDelivery();

                _paramPOdelItems.Podi_seq_no = _POSeqNo;

                List<PurchaseOrderDelivery> _delList = CHNLSVC.Inventory.GetPODelItems(_paramPOdelItems);
                _POItemDel = new List<PurchaseOrderDelivery>();
                _POItemDel = _delList;

                if (_POItemDel != null)
                {
                    var max_DelQuery =
                 (from tab1 in _POItemDel
                  select tab1.Podi_del_line_no).Max();

                    _delLineNo = max_DelQuery;
                    dgvDel.AutoGenerateColumns = false;
                    dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                    dgvDel.DataSource = _POItemDel;
                }
                //kapila
                DataTable _dtItemAloc = CHNLSVC.Inventory.GetPOAlocItems(_POHeader.Poh_doc_no);
                dgvAloc.AutoGenerateColumns = false;
                dgvAloc.DataSource = _dtItemAloc;


                //BindSavePOItems(_POHeader.Poh_seq_no);
                //bindSavePODelItems(_POHeader.Poh_seq_no);
                _IsRecall = true;

                //btnApprove.Enabled = true;
                //btnCancel.Enabled = true;
                //btnSave.Enabled = false;

                //Add by Akila 2017/07/03 
                IsPoCanCancel = IsCancelPO(_POHeader);
                if (IsPoCanCancel) { btnCancel.Enabled = true; }
                else { btnCancel.Enabled = false; }
            }
            else
            {
                MessageBox.Show("Invalid purchase order.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
                return;
            }


        }

        private void txtPurNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPurNo.Text))
            {
                LoadSaveDocument();
            }
        }

        //Update purchase order
        private void UpdatePurchaseOrder()
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;

                string _type = string.Empty;

                if (cmbType.Text == "CONSIGNMENT")
                {
                    _type = "C";
                }
                else if (cmbType.Text == "NORMAL")
                {
                    _type = "N";
                }
                else if (cmbType.Text == "SERVICE")
                {
                    _type = "S";
                }
                else if (cmbType.Text == "IMPORTS")
                {
                    _type = "I";
                }

                if (chkBaseToConsGrn.Checked == true)
                {
                    _isBaseCons = 1;
                }
                else
                {
                    _isBaseCons = 0;
                }


                FF.BusinessObjects.PurchaseOrder _PurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
                _PurchaseOrder.Poh_seq_no = _POSeqNo;
                _PurchaseOrder.Poh_tp = "L";
                _PurchaseOrder.Poh_sub_tp = _type;
                _PurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
                _PurchaseOrder.Poh_com = BaseCls.GlbUserComCode;
                _PurchaseOrder.Poh_ope = "INV";
                _PurchaseOrder.Poh_profit_cd = BaseCls.GlbUserDefProf;
                _PurchaseOrder.Poh_dt = Convert.ToDateTime(dtpPoDate.Value).Date;
                _PurchaseOrder.Poh_ref = txtSupRef.Text;
                _PurchaseOrder.Poh_job_no = txtJob.Text;
                _PurchaseOrder.Poh_pay_term = cmbPayTerm.Text;
                _PurchaseOrder.Poh_supp = txtSupCode.Text;
                _PurchaseOrder.Poh_cur_cd = txtCurrency.Text;
                _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(lblEx.Text);
                _PurchaseOrder.Poh_trans_term = "";
                _PurchaseOrder.Poh_port_of_orig = "";
                _PurchaseOrder.Poh_cre_period = txtCreditPeriod.Text;
                _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(dtpPoDate.Value).Year;
                _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(dtpPoDate.Value).Month;
                _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(dtpPoDate.Value).Year;
                _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(dtpPoDate.Value).Month;
                _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(dtpPoDate.Value).Date;
                _PurchaseOrder.Poh_contain_kit = false;
                _PurchaseOrder.Poh_sent_to_vendor = false;
                _PurchaseOrder.Poh_sent_by = "";
                _PurchaseOrder.Poh_sent_via = "";
                _PurchaseOrder.Poh_sent_add = "";
                _PurchaseOrder.Poh_stus = "P";
                _PurchaseOrder.Poh_remarks = txtRemarks.Text;
                _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblSubTot.Text);
                _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblTaxAmt.Text);
                _PurchaseOrder.Poh_dis_rt = 0;
                _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblDisAmt.Text);
                _PurchaseOrder.Poh_oth_tot = 0;
                _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblTotal.Text);
                _PurchaseOrder.Poh_reprint = false;
                _PurchaseOrder.Poh_tax_chg = false;
                _PurchaseOrder.poh_is_conspo = _isBaseCons;
                _PurchaseOrder.Poh_cre_by = BaseCls.GlbUserID;


                List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                foreach (PurchaseOrderDetail line in _POItemList)
                {
                    line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                    _POItemListSave.Add(line);
                }

                List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                foreach (PurchaseOrderDelivery line in _POItemDel)
                {
                    line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                    _PODelSave.Add(line);
                }



                row_aff = (Int32)CHNLSVC.Inventory.UpdateSavedPO(_PurchaseOrder, _POItemListSave, _PODelSave, _PurchaseOrder.Poh_seq_no);

                if (row_aff == 1)
                {
                    MessageBox.Show("Purchase order updated.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        //save Purchase Ordedr
        private void SavePOHeader()
        {
            try
            {
                Int32 row_aff = 0;
                string _PONo = string.Empty;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;
                string _type = string.Empty;

                if (cmbType.Text == "CONSIGNMENT")
                {
                    _type = "C";
                }
                else if (cmbType.Text == "NORMAL")
                {
                    _type = "N";
                }
                else if (cmbType.Text == "SERVICE")
                {
                    _type = "S";
                }
                else if (cmbType.Text == "IMPORTS")
                {
                    _type = "N";
                }

                if (chkBaseToConsGrn.Checked == true)
                {
                    _isBaseCons = 1;
                }
                else
                {
                    _isBaseCons = 0;
                }


                FF.BusinessObjects.PurchaseOrder _PurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
                _PurchaseOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "PO", 1, BaseCls.GlbUserComCode);
                if (cmbType.Text == "IMPORTS")
                {
                    _PurchaseOrder.Poh_tp = "I";
                }
                else
                {
                    _PurchaseOrder.Poh_tp = "L";
                }
                _PurchaseOrder.Poh_sub_tp = _type;
                _PurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
                _PurchaseOrder.Poh_com = BaseCls.GlbUserComCode;
                _PurchaseOrder.Poh_ope = "INV";
                _PurchaseOrder.Poh_profit_cd = BaseCls.GlbUserDefProf;
                _PurchaseOrder.Poh_dt = Convert.ToDateTime(dtpPoDate.Value).Date;
                _PurchaseOrder.Poh_ref = txtSupRef.Text;
                _PurchaseOrder.Poh_job_no = txtJob.Text;
                _PurchaseOrder.Poh_pay_term = cmbPayTerm.Text;
                _PurchaseOrder.Poh_supp = txtSupCode.Text;
                _PurchaseOrder.Poh_cur_cd = txtCurrency.Text;
                _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(lblEx.Text);
                _PurchaseOrder.Poh_trans_term = "";
                _PurchaseOrder.Poh_port_of_orig = "";
                _PurchaseOrder.Poh_cre_period = txtCreditPeriod.Text;
                _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(dtpPoDate.Value).Year;
                _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(dtpPoDate.Value).Month;
                _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(dtpPoDate.Value).Year;
                _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(dtpPoDate.Value).Month;
                _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(dtpPoDate.Value).Date;
                _PurchaseOrder.Poh_contain_kit = false;
                _PurchaseOrder.Poh_sent_to_vendor = false;
                _PurchaseOrder.Poh_sent_by = "";
                _PurchaseOrder.Poh_sent_via = "";
                _PurchaseOrder.Poh_sent_add = "";
                _PurchaseOrder.Poh_stus = "P";
                _PurchaseOrder.Poh_remarks = txtRemarks.Text;
                _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblSubTot.Text);
                _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblTaxAmt.Text);
                _PurchaseOrder.Poh_dis_rt = 0;
                _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblDisAmt.Text);
                _PurchaseOrder.Poh_oth_tot = 0;
                _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblTotal.Text);
                _PurchaseOrder.Poh_reprint = false;
                _PurchaseOrder.Poh_tax_chg = false;
                _PurchaseOrder.poh_is_conspo = _isBaseCons;
                _PurchaseOrder.Poh_cre_by = BaseCls.GlbUserID;


                List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                foreach (PurchaseOrderDetail line in _POItemList)
                {
                    line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                    _POItemListSave.Add(line);
                }

                List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                foreach (PurchaseOrderDelivery line in _POItemDel)
                {
                    line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                    _PODelSave.Add(line);
                }

                List<PurchaseOrderAlloc> _POAlocSave = new List<PurchaseOrderAlloc>();
                foreach (PurchaseOrderAlloc line in _POItemAloc)
                {
                    line.poal_seq_no = _PurchaseOrder.Poh_seq_no;
                    _POAlocSave.Add(line);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PUR";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "PUR";
                masterAuto.Aut_year = null;

                string QTNum;
                //add null parameter Rukshan 29/sep/2015
                row_aff = (Int32)CHNLSVC.Inventory.SaveNewPO(_PurchaseOrder, _POItemListSave, _PODelSave, _POAlocSave, masterAuto, null, null, out QTNum);

                if (row_aff == 1)
                {
                    MessageBox.Show("Purchase order generated.PO # : " + QTNum, "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(QTNum))
                    {
                        MessageBox.Show(QTNum, "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Faild to generate", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupCode.Text))
                {
                    MessageBox.Show("Please select supplier code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSupCode.Focus();
                    return;
                }


                if (dgvPOItems.Rows.Count == 0)
                {
                    MessageBox.Show("Cannot find po items.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    MessageBox.Show("Order is already approved.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "F")
                {
                    MessageBox.Show("Order is already completed.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    MessageBox.Show("Order is already cancelled.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    MessageBox.Show("Please select po currency.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCurrency.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblEx.Text))
                {
                    MessageBox.Show("Exchange rate is missing.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCurrency.Focus();
                    return;
                }

                if (Convert.ToDecimal(lblEx.Text) <= 0)
                {
                    MessageBox.Show("Invalid exchange rate.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCurrency.Focus();
                    return;
                }

                if (cmbType.Text == "SERVICE")
                {
                    if (string.IsNullOrEmpty(txtJob.Text))
                    {
                        MessageBox.Show("Please select job number.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJob.Focus();
                        return;
                    }
                }

                if (MessageBox.Show("Do you want to save this order ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                decimal _poQty = 0;
                decimal _delQty = 0;

                foreach (PurchaseOrderDetail _tmpItm in _POItemList)
                {
                    _poQty = _tmpItm.Pod_qty;
                    _delQty = 0;
                    foreach (PurchaseOrderDelivery _tmpDel in _POItemDel)
                    {
                        if (_tmpDel.Podi_line_no == _tmpItm.Pod_line_no)
                        {
                            _delQty = _delQty + _tmpDel.Podi_qty;
                        }
                    }

                    if (_delQty != _poQty)
                    {
                        MessageBox.Show("Delivery schedule QTY and Purchase QTY mismatch for the item " + _tmpItm.Pod_itm_cd, "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                if (_IsRecall == false)
                {
                    SavePOHeader();
                }
                else if (_IsRecall == true)
                {
                    UpdatePurchaseOrder();
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

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    MessageBox.Show("Please select purchase order #.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_IsRecall == false)
                {
                    MessageBox.Show("Please recall saved order.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(_POstatus))
                {
                    MessageBox.Show("Cannot retrive current po status.Please re-try.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    MessageBox.Show("Selected order already approved.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    MessageBox.Show("Selected order is canceled.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "F")
                {
                    MessageBox.Show("Selected order is completed.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }



                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16023))
                {

                }
                else
                {
                    MessageBox.Show("You haven't permission to approve. Permission code : 16023", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (MessageBox.Show("Do you want to approve this order ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                UpdatePOStatus("A", true);

                if (MessageBox.Show("Do you want to print the purchase order?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //BaseCls.GlbReportName = "PurchaseOrderPrint.rpt";
                    //PurchaseOrderPrintUpdate.rpt
                    BaseCls.GlbReportName = "PurchaseOrderPrintUpdate.rpt";
                    BaseCls.GlbReportDoc = txtPurNo.Text.Trim();
                    _view.Show();
                    _view = null;
                }

                Clear_Data();
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

        private void UpdatePOStatus(string _POUpdateStatus, bool _isApproved)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            FF.BusinessObjects.PurchaseOrder _UpdatepurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
            _UpdatepurchaseOrder.Poh_seq_no = _POSeqNo;
            _UpdatepurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
            _UpdatepurchaseOrder.Poh_stus = _POUpdateStatus;
            _UpdatepurchaseOrder.Poh_com = BaseCls.GlbUserComCode;
            _UpdatepurchaseOrder.Poh_cre_by = BaseCls.GlbUserID;

            //updated by akila 2014/07/03
            _UpdatepurchaseOrder.Poh_job_no = txtJob.Text.Trim();
            _UpdatepurchaseOrder.Poh_sub_tp = POSubType;

            row_aff = (Int32)CHNLSVC.Inventory.UpdatePOStatusNew(_UpdatepurchaseOrder, null);

            if (row_aff == 1)
            {
                if (_isApproved == true)
                {
                    MessageBox.Show("Successfully approved.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_isApproved == false)
                {
                    MessageBox.Show("Successfully cancelled.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                // Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    MessageBox.Show(_msg, "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Faild to update.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dtpPoDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPayTerm.Focus();
            }
        }

        private void cmbPayTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCreditPeriod.Focus();
            }
        }

        private void txtCreditPeriod_Leave(object sender, EventArgs e)
        {
            if (!IsNumeric(txtCreditPeriod.Text))
            {
                MessageBox.Show("Credit period should be numeric.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCreditPeriod.Text = "0";
                txtCreditPeriod.Focus();
            }
        }

        private void txtCreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCurrency.Focus();
            }
        }

        private void txtSupRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtJob.Enabled == true)
                {
                    txtJob.Focus();
                }
                else
                {
                    txtRemarks.Focus();
                }
            }
        }

        private void txtJob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemarks.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnJobSearch_Click(null, null);// add by akila 2017/06/29

                //this.Cursor = Cursors.WaitCursor;
                //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                //DateTime dtTemp = DateTime.Today.AddMonths(-1);
                //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                //_CommonSearch.dtpFrom.Value = dtTemp;
                //_CommonSearch.dtpTo.Value = DateTime.Today;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtJob;
                //this.Cursor = Cursors.Default;
                ////_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.ShowDialog();
                //txtJob.Select();

            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }

        private void txtDelLoca_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAddDel.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtDelLoca;
                    _CommonSearch.ShowDialog();
                    txtDelLoca.Select();
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

        private void txtDelQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDelLoca.Focus();
            }

        }

        private void txtDelQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDelQty.Text))
            {
                if (!IsNumeric(txtDelQty.Text))
                {
                    MessageBox.Show("Invalid qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelQty.Text = "";
                    txtDelQty.Focus();
                    return;
                }
            }
        }

        private void dgvQuo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex != -1)
            {

                if (dgvQuo.Rows.Count == 0) return;

                decimal _uPrice = Convert.ToDecimal(dgvQuo.Rows[e.RowIndex].Cells["col_qPrice"].Value);

                txtUnitPrice.Text = _uPrice.ToString("n");
                txtUnitPrice.Focus();
            }
        }

        private void btnDelLocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDelLoca;
                _CommonSearch.ShowDialog();
                txtDelLoca.Select();
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

        private void txtDelLoca_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDelLoca;
                _CommonSearch.ShowDialog();
                txtDelLoca.Select();
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

        private void txtDelLoca_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDelLoca.Text))
                {
                    DataTable _result = CHNLSVC.Security.GetUserLocTable(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtDelLoca.Text.Trim());

                    if (_result.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid loction code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDelLoca.Text = "";
                        txtDelLoca.Focus();
                        return;
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

        private void txtStatus_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtStatus.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidItemStatus(txtStatus.Text))
                    {
                        MessageBox.Show("Invalid item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtStatus.Text = "";
                        txtStatus.Focus();
                        return;
                    }
                    else
                    {
                        DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                        var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == txtStatus.Text.ToString()).Select(x => x.Field<string>("mis_cd")).ToList();
                        if (_lp.Count <= 0)
                        {
                            MessageBox.Show("Invalid item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatus.Text = "";
                            txtStatus.Focus();
                            return;
                        }
                        //kapila 29/6/2016
                        List<MasterItemTax> _taxEff = new List<MasterItemTax>();
                        if (_isStrucBaseTax == true)       //kapila 9/5/2017
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                            _taxEff = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), string.Empty, string.Empty, _mstItem.Mi_anal1);
                        }
                        else

                            _taxEff = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), null, string.Empty, DateTime.Now.Date);
                        if (_taxEff.Count <= 0)
                        {
                            MessageBox.Show("Tax is not defined for the selected item and status", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatus.Focus();
                            return;
                        }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    MessageBox.Show("Please select purchase order #.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_IsRecall == false)
                {
                    MessageBox.Show("Please recall saved order.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(_POstatus))
                {
                    MessageBox.Show("Cannot retrive current po status.Please re-try.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                //updated by akila 2017/07/03
                if ((_POstatus == "A") && (!IsPoCanCancel))
                {
                    MessageBox.Show("Selected order is already approved.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if ((_POstatus == "F") && (!IsPoCanCancel))
                {
                    MessageBox.Show("Selected order is already completed.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                //commented by akila 2017/07/03
                //if (_POstatus == "A")
                //{
                //    MessageBox.Show("Selected order is already approved.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPurNo.Focus();
                //    return;
                //}

                //if (_POstatus == "F")
                //{
                //    MessageBox.Show("Selected order is already completed.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPurNo.Focus();
                //    return;
                //}

                if (_POstatus == "C")
                {
                    MessageBox.Show("Selected order is already cancelled.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                //if (MessageBox.Show("Do you want to cancel this order ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}

                if (IsPoCanCancel)
                {
                    if (MessageBox.Show("Do you want to cancel this order ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    UpdatePOStatus("C", false);
                    Clear_Data();
                }
                else
                {
                    MessageBox.Show("Selected PO not allow to cancel.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                //UpdatePOStatus("C", false);
                //Clear_Data();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    MessageBox.Show("Please select purchase order.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }


                if (_IsRecall == false)
                {
                    MessageBox.Show("Please recall purchase order.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (BaseCls.GlbUserComCode != "AST")
                    if (_POstatus == "P")
                    {
                        MessageBox.Show("Selected order is still in pending status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPurNo.Focus();
                        return;
                    }
                if (BaseCls.GlbUserComCode == "AAL")
                {
                    //BaseCls.GlbReportTp = "PO";
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //hasith ast print remove 
                    BaseCls.GlbReportName = "PurchaseOrderPrintUpdate.rpt";
                    BaseCls.GlbReportDoc = txtPurNo.Text.Trim();
                    _view.Show();
                    _view = null;
                }
                else
                {
                    BaseCls.GlbReportTp = "PO";
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //hasith ast print remove 
                    BaseCls.GlbReportName = "PurchaseOrderPrint.rpt";
                    BaseCls.GlbReportDoc = txtPurNo.Text.Trim();
                    _view.Show();
                    _view = null;
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

        private void btnJobSearch_Click(object sender, EventArgs e)
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
                _CommonSearch.obj_TragetTextBox = txtJob;
                this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtJob.Select();
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

        private void txtJob_DoubleClick(object sender, EventArgs e)
        {

            btnJobSearch_Click(null, null);// add by akila 2017/06/29

            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            //    DateTime dtTemp = DateTime.Today.AddMonths(-1);
            //    DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
            //    _CommonSearch.dtpFrom.Value = dtTemp;
            //    _CommonSearch.dtpTo.Value = DateTime.Today;
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtJob;
            //    this.Cursor = Cursors.Default;
            //    _CommonSearch.ShowDialog();
            //    txtJob.Select();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void txtJob_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJob.Text))
            {
                return;
            }
            Boolean _validJob = false;
            List<Service_job_Det> _JobDet = new List<Service_job_Det>();
            _JobDet = CHNLSVC.CustService.GetPcJobDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtJob.Text.Trim());

            if (_JobDet != null && _JobDet.Count > 0)
            {
                foreach (Service_job_Det _jDet in _JobDet)
                {
                    if (_jDet.Jbd_stage < 6)
                    {
                        _validJob = true;
                        goto L1;
                    }

                }
            }
        L1: int I = 0;
            if (_validJob == false)
            {
                MessageBox.Show("Invalid job number.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJob.Text = "";
                txtJob.Focus();
                return;
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
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
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtStatus_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("First you have to select item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtStatus;
                _CommonSearch.ShowDialog();
                txtStatus.Select();
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            pnlAloc.Visible = false;
        }

        private void dgvAloc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5 && e.RowIndex != -1)
                {
                    if (MessageBox.Show("Do you want to remove selected allocation details ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_POItemAloc == null || _POItemAloc.Count == 0) return;

                        Int32 _line = Convert.ToInt32(dgvAloc.Rows[e.RowIndex].Cells["col_aNo"].Value);


                        List<PurchaseOrderAlloc> _temp = new List<PurchaseOrderAlloc>();
                        _temp = _POItemAloc;

                        _temp.RemoveAll(x => x.poal_del_line_no == _line);
                        _POItemAloc = _temp;

                        dgvAloc.AutoGenerateColumns = false;
                        dgvAloc.DataSource = new List<PurchaseOrderAlloc>();
                        dgvAloc.DataSource = _POItemAloc;
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

        private void btnAddAlocAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAlocLoca.Text))
                {
                    MessageBox.Show("Please select relavant allocation location.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelLoca.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    MessageBox.Show("Order is already approved.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    MessageBox.Show("Order is already cancelled.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                _delAlocLineNo = 0;
                _POItemAloc = new List<PurchaseOrderAlloc>();
                PurchaseOrderAlloc _tmpPoAloc = new PurchaseOrderAlloc();

                foreach (PurchaseOrderDetail _tmpList in _POItemList)
                {
                    if (_POItemAloc.Count > 0)
                    {
                        var max_DelQuery =
                        (from tab1 in _POItemAloc
                         select tab1.poal_del_line_no).Max();

                        _delAlocLineNo = max_DelQuery;
                    }
                    else
                    {
                        _delAlocLineNo = 0;
                    }
                    _tmpPoAloc = new PurchaseOrderAlloc();
                    _delAlocLineNo = _delAlocLineNo + 1;
                    _tmpPoAloc.poal_seq_no = 12;
                    _tmpPoAloc.poal_line_no = _tmpList.Pod_line_no;
                    _tmpPoAloc.poal_del_line_no = _delLineNo;
                    _tmpPoAloc.poal_loca = txtDelLoca.Text.Trim();
                    _tmpPoAloc.poal_itm_cd = _tmpList.Pod_itm_cd;
                    _tmpPoAloc.poal_itm_stus = _tmpList.Pod_itm_stus;
                    _tmpPoAloc.poal_qty = _tmpList.Pod_qty;
                    _tmpPoAloc.poal_bal_qty = _tmpList.Pod_qty;

                    _POItemAloc.Add(_tmpPoAloc);
                }
                dgvAloc.AutoGenerateColumns = false;
                dgvAloc.DataSource = new List<PurchaseOrderDelivery>();
                dgvAloc.DataSource = _POItemAloc;
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

        private void btnAddAloc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAlocQty.Text))
                {
                    MessageBox.Show("Please enter allocation qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAlocLoca.Text))
                {
                    MessageBox.Show("Please enter allocation location.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelLoca.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    MessageBox.Show("Order is already approved.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    MessageBox.Show("Order is already cancelled.Cannot amend.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                decimal _delQty = 0;
                decimal _qty = Convert.ToDecimal(txtAlocQty.Text);

                foreach (PurchaseOrderAlloc _tmpDel in _POItemAloc)
                {
                    if (_tmpDel.poal_line_no == Convert.ToInt32(lblAlocItemLine.Text) && _tmpDel.poal_loca == txtAlocLoca.Text.Trim())
                    {
                        MessageBox.Show("Already exit same item with same location. Please remove that and add again.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_tmpDel.poal_line_no == Convert.ToInt32(lblAlocItemLine.Text) && _tmpDel.poal_itm_cd == lblAlocItem.Text)
                    {
                        _delQty = _delQty + _tmpDel.poal_qty;
                    }
                }

                if (_AddDelQty < (_qty + _delQty))
                {
                    MessageBox.Show("Allocation schedule QTY exceeds Purchase QTY.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAlocQty.Text = _AddDelQty.ToString();
                    txtAlocQty.Focus();
                    return;
                }


                PurchaseOrderAlloc _tmpPoAloc = new PurchaseOrderAlloc();

                if (_POItemAloc.Count > 0)
                {
                    var max_DelQuery =
                       (from tab1 in _POItemAloc
                        select tab1.poal_del_line_no).Max();

                    _delAlocLineNo = max_DelQuery;
                }
                else
                {
                    _delAlocLineNo = 0;
                }

                _delAlocLineNo = _delAlocLineNo + 1;
                _tmpPoAloc.poal_seq_no = 12;
                _tmpPoAloc.poal_line_no = Convert.ToInt32(lblAlocItemLine.Text);
                _tmpPoAloc.poal_del_line_no = _delAlocLineNo;
                _tmpPoAloc.poal_loca = txtAlocLoca.Text.Trim();
                _tmpPoAloc.poal_itm_cd = lblAlocItem.Text.Trim();
                _tmpPoAloc.poal_itm_stus = lblAlocStatus.Text.Trim();
                _tmpPoAloc.poal_qty = Convert.ToDecimal(txtAlocQty.Text);
                _tmpPoAloc.poal_bal_qty = Convert.ToDecimal(txtAlocQty.Text);

                _POItemAloc.Add(_tmpPoAloc);

                dgvAloc.AutoGenerateColumns = false;
                dgvAloc.DataSource = new List<PurchaseOrderDelivery>();
                dgvAloc.DataSource = _POItemAloc;

                if (_AddDelQty - (_qty + _delQty) > 0)
                {
                    txtDelQty.Text = Convert.ToString(_AddDelQty - (_qty + _delQty));
                    txtDelLoca.Text = "";
                    txtDelLoca.Focus();
                }
                else if (_AddDelQty - (_qty + _delQty) == 0)
                {
                    lblAlocItem.Text = "";
                    lblAlocStatus.Text = "";
                    lblAlocItemLine.Text = "";
                    txtAlocQty.Text = "";
                    txtAlocLoca.Text = "";
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

        private void btnAlocLocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAlocLoca;
                _CommonSearch.ShowDialog();
                txtAlocLoca.Select();
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

        private void button1_Click(object sender, EventArgs e)
        {
            pnlAloc.Visible = true;
        }

        private void btn_closeAloc_Click(object sender, EventArgs e)
        {
            pnl_aloc.Visible = false;
        }

        private void btn_add_aloc_Click(object sender, EventArgs e)
        {
            decimal _qty = 0;
            foreach (DataGridViewRow row in grvAlocLoc.Rows)
            {
                _qty = _qty + Convert.ToDecimal(row.Cells["QTY"].Value);
            }
            if (_qty > Convert.ToDecimal(txtAlocQty.Text))
            {
                MessageBox.Show("Allocation schedule QTY exceeds Purchase QTY.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow row in grvAlocLoc.Rows)
            {
                if (Convert.ToDecimal(row.Cells["QTY"].Value) > 0)
                {
                    PurchaseOrderAlloc _tmpPoAloc = new PurchaseOrderAlloc();

                    if (_POItemAloc.Count > 0)
                    {
                        var max_DelQuery =
                           (from tab1 in _POItemAloc
                            select tab1.poal_del_line_no).Max();

                        _delAlocLineNo = max_DelQuery;
                    }
                    else
                    {
                        _delAlocLineNo = 0;
                    }

                    _delAlocLineNo = _delAlocLineNo + 1;
                    _tmpPoAloc.poal_seq_no = 12;
                    _tmpPoAloc.poal_line_no = _lineNo;
                    _tmpPoAloc.poal_del_line_no = _delAlocLineNo;
                    _tmpPoAloc.poal_loca = row.Cells["LOC"].Value.ToString();
                    _tmpPoAloc.poal_itm_cd = lblAlocItem.Text.Trim();
                    _tmpPoAloc.poal_itm_stus = lblAlocStatus.Text.Trim();
                    _tmpPoAloc.poal_qty = Convert.ToDecimal(row.Cells["QTY"].Value);
                    _tmpPoAloc.poal_bal_qty = Convert.ToDecimal(row.Cells["QTY"].Value);

                    _POItemAloc.Add(_tmpPoAloc);

                    dgvAloc.AutoGenerateColumns = false;
                    dgvAloc.DataSource = new List<PurchaseOrderDelivery>();
                    dgvAloc.DataSource = _POItemAloc;

                    pnl_aloc.Visible = false;
                    txtItem.Focus();
                }

            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            Boolean _isVatClaim = false;
            string _suppTaxCate = "";
            Int32 _upLine = 0;
            Int32 _upDelLineNo = 0;
            _totPoQty = 0;
            Int32 _locaCount = 0;
            bool _isDecimalAllow = false;
            try
            {
                # region items
                if (optItms.Checked == true)
                {
                    # region validation
                    if (string.IsNullOrEmpty(txtSupCode.Text))
                    {
                        MessageBox.Show("Please select supplier code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSupCode.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCurrency.Text))
                    {
                        MessageBox.Show("Please select currecy code.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCurrency.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtFileName.Text))
                    {
                        MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFileName.Clear();
                        txtFileName.Focus();
                        return;
                    }
                    if (_supDet != null)
                    {
                        _isVatClaim = _supDet.Mbe_is_tax;
                        _suppTaxCate = _supDet.Mbe_cate;
                    }
                    else
                    {
                        MessageBox.Show("Cannot find supplier details.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName.Text);

                    if (fileObj.Exists == false)
                    {
                        MessageBox.Show("Selected file does not exist at the following path.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    #endregion

                    #region open excel
                    string Extension = fileObj.Extension;

                    string conStr = "";

                    if (Extension.ToUpper() == ".XLS")
                    {

                        conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                                 .ConnectionString;
                    }
                    else if (Extension.ToUpper() == ".XLSX")
                    {
                        conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                                  .ConnectionString;

                    }


                    conStr = String.Format(conStr, txtFileName.Text, "NO");
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable _dt = new DataTable();
                    cmdExcel.Connection = connExcel;

                    //Get the name of First Sheet
                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(_dt);
                    connExcel.Close();

                    #endregion

                    _POItemList = new List<PurchaseOrderDetail>();
                    _POItemDel = new List<PurchaseOrderDelivery>();
                    _POItemAloc = new List<PurchaseOrderAlloc>();
                    string _item = "";
                    string _itemDesc = "";
                    decimal _qty = 0;
                    string _stus = "";
                    decimal _uprice = 0;
                    decimal _drate = 0;
                    decimal _tax = 0;


                    StringBuilder _errorLst = new StringBuilder();
                    if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                    if (_dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                        foreach (DataRow _dr in _dt.Rows)
                        {
                            _upLine = Convert.ToInt32(_dr[0]);
                            _item = _dr[1].ToString().Trim();
                            _stus = _dr[2].ToString().Trim();
                            _qty = Convert.ToDecimal(_dr[3]);
                            _uprice = Convert.ToDecimal(_dr[4]);
                            _drate = Convert.ToDecimal(_dr[5]);
                            _tax = Convert.ToDecimal(_dr[6]);

                            #region item validation
                            if (string.IsNullOrEmpty(_stus))
                            {
                                MessageBox.Show("Please select item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            //21/12/2015  kapila
                            if (cmbType.Text == "IMPORTS")
                            {
                                if (_stus != "GOD")
                                {
                                    MessageBox.Show("Invalid item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            else if (cmbType.Text == "CONSIGNMENT")
                            {
                                if (_stus != "CONS")
                                {
                                    MessageBox.Show("Invalid item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            else
                            {
                                DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                                var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _stus).Select(x => x.Field<string>("mis_cd")).ToList();
                                if (_lp.Count <= 0)
                                {
                                    MessageBox.Show("Invalid item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            if (string.IsNullOrEmpty(_item))
                            {
                                MessageBox.Show("Please enter purchasing item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (_drate < 0)
                            {
                                MessageBox.Show("Please enter valid discount rate.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (_uprice < 0)
                            {
                                MessageBox.Show("Please enter valid price.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (_qty <= 0)
                            {
                                MessageBox.Show("Please enter valid purchasing qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (string.IsNullOrEmpty(_uprice.ToString()))
                            {
                                MessageBox.Show("Please enter item unit price.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtUnitPrice.Focus();
                                return;
                            }
                            if (!IsNumeric(_drate.ToString()))
                            {
                                MessageBox.Show("Invalid discount rate.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (_drate > 100)
                            {
                                MessageBox.Show("Invalid discount rate.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                return;
                            }
                            _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                            if (_isDecimalAllow == false) _qty = decimal.Truncate(Convert.ToDecimal(_qty));

                            #endregion

                            PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                            PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();
                            PurchaseOrderAlloc _tmpPoAloc = new PurchaseOrderAlloc();
                            MasterItem _tmpItem = new MasterItem();

                            _tmpItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            decimal _taxForActual = 0;

                            if (_tmpItem != null)
                            {
                                decimal _amt = _uprice * _qty;
                                decimal _disAmount = _amt * _drate / 100;
                                decimal _total = (_amt - _disAmount) + _tax;

                                // Add po items ______________________

                                if (string.IsNullOrEmpty(_suppTaxCate))
                                {
                                    _tmpPoDetails.Pod_act_unit_price = (_qty * _uprice - _disAmount + _tax) / _qty;
                                }
                                else
                                {

                                    _amt = _uprice * _qty;
                                    _disAmount = _amt * _drate / 100;

                                    _taxForActual = TaxCalculationActualCost(BaseCls.GlbUserComCode, _item, _stus, _amt - _disAmount, _suppTaxCate, 0);
                                    _tmpPoDetails.Pod_act_unit_price = ((_qty * _uprice - _disAmount) + _taxForActual) / _qty;
                                }
                                _tmpPoDetails.Pod_dis_amt = _disAmount;
                                _tmpPoDetails.Pod_dis_rt = _drate;
                                _tmpPoDetails.Pod_grn_bal = _qty;
                                _tmpPoDetails.Pod_item_desc = _tmpItem.Mi_shortdesc;
                                _tmpPoDetails.Pod_itm_cd = _item;
                                _tmpPoDetails.Pod_itm_stus = _stus;
                                _tmpPoDetails.Pod_itm_tp = _tmpItem.Mi_itm_tp;
                                _tmpPoDetails.Pod_kit_itm_cd = "";
                                _tmpPoDetails.Pod_kit_line_no = 0;
                                _tmpPoDetails.Pod_lc_bal = 0;
                                _tmpPoDetails.Pod_line_amt = _total;
                                _tmpPoDetails.Pod_line_no = _upLine;
                                _tmpPoDetails.Pod_line_tax = _tax;
                                _tmpPoDetails.Pod_line_val = _amt;
                                _tmpPoDetails.Pod_nbt = 0;
                                _tmpPoDetails.Pod_nbt_before = 0;
                                _tmpPoDetails.Pod_pi_bal = 0;
                                _tmpPoDetails.Pod_qty = _qty;
                                _tmpPoDetails.Pod_ref_no = txtSupRef.Text;
                                _tmpPoDetails.Pod_seq_no = 12;
                                _tmpPoDetails.Pod_si_bal = 0;
                                _tmpPoDetails.Pod_tot_tax_before = 0;
                                _tmpPoDetails.Pod_unit_price = _uprice;
                                _tmpPoDetails.Pod_uom = _tmpItem.Mi_itm_uom;
                                _tmpPoDetails.Pod_vat = 0;
                                _tmpPoDetails.Pod_vat_before = 0;

                                _POItemList.Add(_tmpPoDetails);

                                //delivery allocation
                                _upDelLineNo = _upDelLineNo + 1;
                                _tmpPoDel.Podi_seq_no = 12;
                                _tmpPoDel.Podi_line_no = _upLine;
                                _tmpPoDel.Podi_del_line_no = _upDelLineNo;
                                _tmpPoDel.Podi_loca = BaseCls.GlbUserDefLoca;
                                _tmpPoDel.Podi_itm_cd = _item;
                                _tmpPoDel.Podi_itm_stus = _stus;
                                _tmpPoDel.Podi_qty = _qty;
                                _tmpPoDel.Podi_bal_qty = _qty;

                                _POItemDel.Add(_tmpPoDel);

                                _totPoQty = _totPoQty + _qty;
                            }

                        }

                        dgvPOItems.AutoGenerateColumns = false;
                        dgvPOItems.DataSource = new List<PurchaseOrderDetail>();
                        dgvPOItems.DataSource = _POItemList;

                        dgvDel.AutoGenerateColumns = false;
                        dgvDel.DataSource = new List<PurchaseOrderDelivery>();
                        dgvDel.DataSource = _POItemDel;

                        cmdAddItem.Enabled = false;
                        //btnUploadFile_spv.Enabled = false;
                        btnAddAloc.Enabled = false;
                        btnAddAlocAll.Enabled = false;

                        MessageBox.Show("Done", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Cal_Totals();
                }
                #endregion
                //allocation
                # region allocation
                decimal _totAlocItmQty = 0;
                if (optAloc.Checked == true)
                {
                    if (dgvPOItems.Rows.Count == 0)
                    {
                        MessageBox.Show("Please enter the items", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName.Text);

                    if (fileObj.Exists == false)
                    {
                        MessageBox.Show("Selected file does not exist at the following path.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    #region open excel
                    string Extension = fileObj.Extension;

                    string conStr = "";

                    if (Extension.ToUpper() == ".XLS")
                    {

                        conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                                 .ConnectionString;
                    }
                    else if (Extension.ToUpper() == ".XLSX")
                    {
                        conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                                  .ConnectionString;

                    }


                    conStr = String.Format(conStr, txtFileName.Text, "NO");
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable _dt = new DataTable();
                    cmdExcel.Connection = connExcel;

                    //Get the name of First Sheet
                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(_dt);
                    connExcel.Close();

                    #endregion

                    StringBuilder _errorLst = new StringBuilder();
                    if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                    if (_dt.Rows.Count > 0)
                    {
                        _locaCount = _dt.Columns.Count - 3;

                        if (MessageBox.Show("Are you sure ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;


                        foreach (DataRow _dr in _dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(_dr[1].ToString()))
                            {
                                DataRow _dr1 = _dt.Rows[0];

                                for (int i = 0; i < _locaCount; i++)
                                {
                                    if (Convert.ToDecimal(_dr[3 + i]) < 0)
                                    {
                                        MessageBox.Show("Please enter valid quantity.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }

                                    PurchaseOrderAlloc _tmpPoAloc = new PurchaseOrderAlloc();
                                    _delAlocLineNo = _delAlocLineNo + 1;
                                    _tmpPoAloc.poal_seq_no = 12;
                                    _tmpPoAloc.poal_line_no = Convert.ToInt32(_dr[0]);
                                    _tmpPoAloc.poal_del_line_no = _delAlocLineNo;

                                    _tmpPoAloc.poal_loca = _dr1[3 + i].ToString();

                                    _tmpPoAloc.poal_itm_cd = _dr[1].ToString();
                                    _tmpPoAloc.poal_itm_stus = _dr[2].ToString();
                                    _tmpPoAloc.poal_qty = Convert.ToDecimal(_dr[3 + i]);
                                    _totAlocItmQty = _totAlocItmQty + Convert.ToDecimal(_dr[3 + i]);
                                    _tmpPoAloc.poal_bal_qty = Convert.ToDecimal(_dr[3 + i]);

                                    _POItemAloc.Add(_tmpPoAloc);
                                }

                                var _lst = _POItemList.Where(x => x.Pod_line_no == Convert.ToInt32(_dr[0])).Select(y => y.Pod_qty).Sum();
                                if (Convert.ToDecimal(_lst) != _totAlocItmQty)
                                {
                                    MessageBox.Show("Allocation qty mismtch. Item code " + _dr[1].ToString(), "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                _totAlocItmQty = 0;
                            }
                        }
                    }

                    dgvAloc.AutoGenerateColumns = false;
                    dgvAloc.DataSource = new List<PurchaseOrderDelivery>();
                    dgvAloc.DataSource = _POItemAloc;

                    MessageBox.Show("Done", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                #endregion
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

        private void optItms_CheckedChanged(object sender, EventArgs e)
        {
            txtFileName.Text = "";
        }

        private void optAloc_CheckedChanged(object sender, EventArgs e)
        {
            txtFileName.Text = "";
        }

        private void txtItemDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtStatus.Enabled == true)
                {
                    txtStatus.Focus();
                }
                else
                {
                    txtQty.Focus();
                }
                lblItemDescription.Text = txtItemDesc.Text;
                txtItemDesc.Text = "";
                txtItemDesc.Visible = false;
            }
        }

        //by akila 2017/07/03
        private bool IsCancelPO(FF.BusinessObjects.PurchaseOrder _poHdr)
        {
            bool result = false;

            try
            {
                if (_poHdr != null)
                {
                    if (_poHdr.Poh_sub_tp == "S")//service po
                    {
                        if (_poHdr.Poh_stus == "P") 
                        { 
                            result = true; 
                        }
                        else if (_poHdr.Poh_stus == "A")
                        {
                            //check for confirmed service po details
                            DataTable _confirmedPos = new DataTable();
                            _confirmedPos = CHNLSVC.CustService.GetConfirmedServicePoDetails(_poHdr.Poh_job_no, _poHdr.Poh_doc_no);
                            if (_confirmedPos.Rows.Count > 0) { result = false; }
                            else { result = true; }
                        }
                        else if (_poHdr.Poh_stus == "F")
                        {
                            //check job stage. if job stage <= 5 then allow to cancel
                            List<Service_job_Det> _JobDet = new List<Service_job_Det>();
                            _JobDet = CHNLSVC.CustService.GetPcJobDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _poHdr.Poh_job_no);
                            if ((_JobDet != null) && (_JobDet.Count > 0))
                            {
                                var _jobStage = Convert.ToDecimal(_JobDet.Select(x => x.Jbd_stage).First());
                                if (_jobStage <= 5) { result = true; }
                                else { result = false; }
                            }
                        }
                    }
                    else//other po types
                    {
                        if (_poHdr.Poh_stus == "F"){result = false; }
                        else if (_poHdr.Poh_stus == "P") { result = true; }
                        else if (_poHdr.Poh_stus == "A")
                        {
                            DataTable _grnPos = new DataTable();
                            _grnPos = CHNLSVC.Financial.CusdecCancelIsGRN(BaseCls.GlbUserComCode, _poHdr.Poh_doc_no, 1);
                            if (_grnPos.Rows.Count > 0) { result = false; }
                            else { result = true; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                Cursor = DefaultCursor;
                MessageBox.Show("Error occurred while validating PO details." + Environment.NewLine + ex.Message , "Cancel PO - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            try
            {
                decimal _unitAmt = 0;
                decimal _qty = 0;
                decimal _amount = 0;
                _qty = Convert.ToDecimal(txtQty.Text);
                _unitAmt = Convert.ToDecimal(txtUnitPrice.Text);
                //txtUnitPrice.Text = _unitAmt.ToString("n");
                _amount = _qty * _unitAmt;

                txtAmount.Text = _amount.ToString("n");   
            }
            catch (Exception)
            {

                MessageBox.Show("Please select purchase order #.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                return;
            }
           
        }

        private void txtUnitPrice_Enter(object sender, EventArgs e)
        {
          
        }

        private void txtUnitPrice_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10161))//16023
            {

            }
            else
            {
                MessageBox.Show("You haven't permission change unit price. Permission code : 10161", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
                return;
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtDisRate.Focus();
               
            }
        }
    }
}
