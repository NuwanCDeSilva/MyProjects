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
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.Inventory;
using FF.WindowsERPClient.CommonSearch;


namespace FF.WindowsERPClient.HP
{
    public partial class HPReversal : Base
    {
        private RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqAppDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqAppSer = new List<RequestApprovalSerials>();

        private RequestApprovalHeaderLog _ReqAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqAppDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

        private MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
        private List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        private List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
        private List<ReptPickSerialsSub> _doitemSubSerials = new List<ReptPickSerialsSub>();
        private List<RecieptHeader> _recieptHeader = new List<RecieptHeader>();

        private List<RequestApprovalDetail> _repItem = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _repSer = new List<RequestApprovalSerials>();
        private List<RequestAppAddDet> _repAddDet = new List<RequestAppAddDet>();

        private Boolean _isAppUser = false;
        private Int32 _appLvl = 0;
        private string _dCusCode = "";
        private string _dCusAdd1 = "";
        private string _dCusAdd2 = "";
        private string _currency = "";
        private decimal _exRate = 0;
        private string _invTP = "";
        private string _executiveCD = "";
        private string _manCode = "";
        private Boolean _isTax = false;
        private string _defBin = "";
        private Boolean _isFromReq = false;


        public HPReversal()
        {
            InitializeComponent();
        }

        private void SalesReversal_Load(object sender, EventArgs e)
        {
            Clear_Data();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.HPInvoiceByCus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpInvoices:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("SRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HPInvoiceOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeJob:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllScheme:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtInvoice.Focus();
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

        private void dtpSRNdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtManRef.Focus();
            }
        }

        private void txtManRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRNremarks.Focus();
            }
        }



        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                {
                    _isFromReq = false;
                    lblReq.Text = "";
                    lblStatus.Text = "";

                    lblSerItem.Text = "";
                    lblSerial.Text = "";
                    lblWarranty.Text = "";
                    lblSerRem.Text = "";
                    txtJobNo.Text = "";

                    _repSer = new List<RequestApprovalSerials>();

                    dgvSerApp.AutoGenerateColumns = false;
                    dgvSerApp.DataSource = new List<RequestApprovalSerials>();

                    _repAddDet = new List<RequestAppAddDet>();

                    dgvRereportItems.AutoGenerateColumns = false;
                    dgvRereportItems.DataSource = new List<RequestAppAddDet>();

                    lblRevItem.Text = "";
                    txtItem.Text = "";
                    lblInvLine.Text = "";
                    lblRepPrice.Text = "";
                    txtNewSerial.Text = "";
                    txtNewPrice.Text = "";

                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    if (chkOthSales.Checked == false)
                    {
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());
                    }
                    else
                    {
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, null, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());
                    }

                    //List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    //_invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());



                    if (_invHdr.Count == 0)
                    {
                        MessageBox.Show("Invalid invoice number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        lblAccount.Text = "";
                        _dCusCode = "";
                        _dCusAdd1 = "";
                        _dCusAdd2 = "";
                        _currency = "";
                        _exRate = 0;
                        _invTP = "";
                        _executiveCD = "";
                        _manCode = "";
                        lblSalePc.Text = "";
                        _isTax = false;
                        txtInvoice.Focus();
                        return;
                    }
                    else
                    {
                        foreach (InvoiceHeader _tempInv in _invHdr)
                        {
                            if (_tempInv.Sah_inv_tp != "HS")
                            {
                                MessageBox.Show("Not allow to reversed this type invoice for hire sales." + _tempInv.Sah_inv_tp, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtInvoice.Text = "";
                                return;
                            }

                            if ((_tempInv.Sah_inv_sub_tp == "SA") || (_tempInv.Sah_inv_sub_tp == "EXO"))
                            {
                                txtCusCode.Text = _tempInv.Sah_cus_cd;
                                lblInvCusName.Text = _tempInv.Sah_cus_name;
                                lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                                lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                                lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                                lblSalePc.Text = _tempInv.Sah_pc;
                                lblAccount.Text = _tempInv.Sah_acc_no;
                                _dCusCode = _tempInv.Sah_d_cust_cd;
                                _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                                _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                                _currency = _tempInv.Sah_currency;
                                _exRate = _tempInv.Sah_ex_rt;
                                _invTP = _tempInv.Sah_inv_tp;
                                _executiveCD = _tempInv.Sah_sales_ex_cd;
                                _manCode = _tempInv.Sah_man_cd;
                                _isTax = _tempInv.Sah_tax_inv;

                                Load_InvoiceDetails(BaseCls.GlbUserDefProf);
                            }
                            else
                            {
                                MessageBox.Show(_tempInv.Sah_inv_sub_tp + " type not allow to reversal.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtInvoice.Text = "";
                                return;
                            }
                        }
                    }
                    viewReminds(lblAccount.Text);


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

        private void cmbRevType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCusCode.Focus();
            }
        }

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (chkOthSales.Checked == false)
                    {
                        if (!string.IsNullOrEmpty(txtCusCode.Text))
                        {
                            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                            _CommonSearch.ReturnIndex = 0;
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HPInvoiceByCus);
                            DataTable _result = CHNLSVC.CommonSearch.GetHPInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                            _CommonSearch.dvResult.DataSource = _result;
                            _CommonSearch.BindUCtrlDDLData(_result);
                            _CommonSearch.obj_TragetTextBox = txtInvoice;
                            _CommonSearch.ShowDialog();
                            txtInvoice.Select();
                        }
                        else
                        {
                            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                            _CommonSearch.ReturnIndex = 1;
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpInvoices);
                            DataTable _result = CHNLSVC.CommonSearch.GetHpInvoices(_CommonSearch.SearchParams, null, null);
                            _CommonSearch.dvResult.DataSource = _result;
                            _CommonSearch.BindUCtrlDDLData(_result);
                            _CommonSearch.obj_TragetTextBox = txtInvoice;
                            _CommonSearch.ShowDialog();
                            txtInvoice.Select();
                        }
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HPInvoiceOth);
                        DataTable _result = CHNLSVC.CommonSearch.GetHpInvoicesOth(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtInvoice;
                        _CommonSearch.ShowDialog();
                        txtInvoice.Select();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtRemarks.Focus();
                }
                //if (e.KeyCode == Keys.F2)
                //{
                //    if (!string.IsNullOrEmpty(txtCusCode.Text))
                //    {
                //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //        _CommonSearch.ReturnIndex = 0;
                //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HPInvoiceByCus);
                //        DataTable _result = CHNLSVC.CommonSearch.GetHPInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                //        _CommonSearch.dvResult.DataSource = _result;
                //        _CommonSearch.BindUCtrlDDLData(_result);
                //        _CommonSearch.obj_TragetTextBox = txtInvoice;
                //        _CommonSearch.ShowDialog();
                //        txtInvoice.Select();
                //    }
                //    else
                //    {
                //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //        _CommonSearch.ReturnIndex = 0;
                //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpInvoices);
                //        DataTable _result = CHNLSVC.CommonSearch.GetHpInvoices(_CommonSearch.SearchParams, null, null);
                //        _CommonSearch.dvResult.DataSource = _result;
                //        _CommonSearch.BindUCtrlDDLData(_result);
                //        _CommonSearch.obj_TragetTextBox = txtInvoice;
                //        _CommonSearch.ShowDialog();
                //        txtInvoice.Select();
                //    }
                //}
                //else if (e.KeyCode == Keys.Enter)
                //{
                //    txtRemarks.Focus();
                //}
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

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRequest.Focus();
            }
        }


        private void btnReqClear_Click(object sender, EventArgs e)
        {
            txtCusCode.Text = "";
            txtInvoice.Text = "";
            lblInvDate.Text = "";
            lblInvCusName.Text = "";
            lblInvCusAdd1.Text = "";
            lblInvCusAdd2.Text = "";
            txtRemarks.Text = "";
            txtSRNremarks.Text = "";
            txtManRef.Text = "";
            lblAccount.Text = "";
            lblReqPC.Text = "";
            lblReturnLoc.Text = "";
            lblStatus.Text = "";
            lblReq.Text = "";
            txtInvoice.Enabled = true;
            txtCusCode.Enabled = true;
            btnRequest.Enabled = true;
            chkOthSales.Enabled = true;
            chkOthSales.Checked = false;
            lblSalePc.Text = "";
            txtCusCode.Enabled = true;

            _InvDetailList = new List<InvoiceItem>();
            _doitemserials = new List<ReptPickSerials>();

            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvInvItem.Columns["col_invRevQty"].ReadOnly = false;

            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();
            txtCusCode.Focus();
        }

        private void Load_InvoiceDetails(string _pc)
        {
            try
            {
                decimal _unitAmt = 0;
                decimal _disAmt = 0;
                decimal _taxAmt = 0;
                decimal _totAmt = 0;
                string _type = "";
                btnRequest.Enabled = true;
                List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                List<InvoiceItem> _InvList = new List<InvoiceItem>();
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                _doitemserials = new List<ReptPickSerials>();


                if (_isFromReq == false)
                {
                    _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), _type);
                }
                else if (_isFromReq == true)
                {
                    _paramInvoiceItems = CHNLSVC.Sales.GetRevDetailsFromRequest(txtInvoice.Text.Trim(), _type, BaseCls.GlbUserComCode, _pc, lblReq.Text.Trim());
                }

                //_paramInvoiceItems = CHNLSVC.Sales.GetPendingInvoiceItems(txtInvoice.Text.Trim());
                if (_paramInvoiceItems.Count > 0)
                {
                    foreach (InvoiceItem item in _paramInvoiceItems)
                    {
                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));

                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                        item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                        item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                        item.Sad_tot_amt = Convert.ToDecimal(_totAmt);

                        _InvList.Add(item);

                        //_tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
                        //_doitemserials.AddRange(_tempDOSerials);
                        if (_isFromReq == false)
                        {
                            _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForReversal(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
                            _doitemserials.AddRange(_tempDOSerials);
                        }
                        else if (_isFromReq == true)
                        {
                            _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForReversal(BaseCls.GlbUserComCode, lblReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
                            _doitemserials.AddRange(_tempDOSerials);
                        }
                        else
                        {
                            MessageBox.Show("Error generate while loading details.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Details cannot found for " + _type + " Sales.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRequest.Enabled = false;
                }

                _InvDetailList = _InvList;

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;
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

        private void Clear_Data()
        {
            try
            {
                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppAuto = new MasterAutoNumber();
                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();
                _repItem = new List<RequestApprovalDetail>();
                _repSer = new List<RequestApprovalSerials>();
                _repAddDet = new List<RequestAppAddDet>();


                chkAccDet.Checked = false;
                btnSave.Enabled = false;
                _isFromReq = false;
                _isAppUser = false;
                _appLvl = 0;
                txtCusCode.Text = "";
                txtInvoice.Text = "";
                lblInvDate.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                lblAccount.Text = "";
                chkApp.Checked = false;
                chkApp.Enabled = false;
                lblReq.Text = "";
                lblStatus.Text = "";
                txtRemarks.Text = "";
                lblReqPC.Text = "";
                lblReturnLoc.Text = "";
                txtCusCode.Enabled = true;
                txtInvoice.Enabled = true;
                btnRequest.Enabled = true;
                MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode); //kapila 8/2/2016
                if (mst_com.Mc_anal24 == "DIRIYA")
                {
                    txtSubType.Text = "REF";
                    txtSubType.Enabled = false;
                    btnSubTypeSearch.Enabled = false;
                }
                lblSubDesc.Text = "";
                lblSalePc.Text = "";
                lblRevItem.Text = "";
                txtItem.Text = "";
                lblInvLine.Text = "";
                lblSerItem.Text = "";
                lblSerial.Text = "";
                lblWarranty.Text = "";
                lblSerRem.Text = "";
                txtJobNo.Text = "";
                txtNewSch.Text = "";
                lblRepPrice.Text = "";
                txtNewSerial.Text = "";
                txtNewPrice.Text = "";

                dtpSRNDate.Value = Convert.ToDateTime(DateTime.Now).Date;
                dtpSRNDate.Enabled = false;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpSRNDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                chkOthSales.Checked = false;
                txtCusCode.Enabled = true;
                dgvInvItem.Columns["col_invRevQty"].ReadOnly = false;

                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT013", BaseCls.GlbUserID);
                if (_sysApp.Sarp_cd != null)
                {
                    chkApp.Checked = true;
                    chkApp.Enabled = true;
                    _isAppUser = true;
                    _appLvl = _sysApp.Sarp_app_lvl;
                }

                if (_isAppUser == true)
                {
                    btnApp.Enabled = true;
                    btnRej.Enabled = true;
                }
                else
                {
                    btnApp.Enabled = false;
                    btnRej.Enabled = false;
                }

                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", null, null);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", BaseCls.GlbUserID, null);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();
                dgvPendings.DataSource = _TempReqAppHdr;

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();

                String _tempDefBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (!string.IsNullOrEmpty(_tempDefBin))
                {
                    _defBin = _tempDefBin;
                }
                else
                {
                    _defBin = "";
                }

                pnlReRep.Visible = false;

                dgvRereportItems.AutoGenerateColumns = false;
                dgvRereportItems.DataSource = new List<RequestAppAddDet>();

                dgvSerApp.AutoGenerateColumns = false;
                dgvSerApp.DataSource = new List<RequestApprovalSerials>();
                txtRqty.Text = "";
                txtCusCode.Focus();
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

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string _docNo = "";
                string _regAppNo = "";
                string _insAppNo = "";
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Invoice customer is missing.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    MessageBox.Show("Please select invoice number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }

                if (dgvInvItem.Rows.Count <= 0)
                {
                    MessageBox.Show("No items are selected to generate request.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    MessageBox.Show("Please select SRN sub type.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubType.Focus();
                    return;
                }

                if (txtSubType.Text != "REF")
                {
                    if (_repAddDet.Count <= 0)
                    {
                        MessageBox.Show("Please enter re-report item details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DataTable _accLog = new DataTable();
                _accLog = CHNLSVC.MsgPortal.GetAccountLogDetails(lblAccount.Text.Trim());

                foreach (DataRow drow in _accLog.Rows)
                {
                    if (drow["hal_sa_sub_tp"].ToString() == "CC")
                    {
                        MessageBox.Show("This account is cash converted account. Cannot reverse as hire sale reversal.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                //Added by Prabhath on 11/10/2013
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", txtInvoice.Text.Trim()))
                { return; }


                CollectReqApp();
                CollectReqAppLog();
                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, _ReqAppSer, _ReqAppAuto, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo, _repAddDet);

                if (effet == 1)
                {
                    MessageBox.Show("Request generated." + _docNo, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_docNo))
                    {
                        MessageBox.Show(_docNo, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show("Creation fail.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        protected void CollectReqAppLog()
        {
            string _type = "";
            _ReqAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();
            _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

            _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdrLog.Grah_app_tp = "ARQT013";
            _ReqAppHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdrLog.Grah_ref = null;
            _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_app_stus = "P";
            _ReqAppHdrLog.Grah_app_lvl = 0;
            _ReqAppHdrLog.Grah_app_by = string.Empty;
            _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdrLog.Grah_sub_type = txtSubType.Text.Trim();
            _ReqAppHdrLog.Grah_oth_pc = lblSalePc.Text.Trim();

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _tempReqAppDet = new RequestApprovalDetailLog();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "SALES REVERSAL";
                    _tempReqAppDet.Grad_val1 = item.Sad_srn_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = item.Sad_fws_ignore_qty;
                    _tempReqAppDet.Grad_val4 = item.Sad_tot_amt / item.Sad_srn_qty; ;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    foreach (RequestApprovalDetail _tmp in _repItem)
                    {
                        if (_tmp.Grad_anal2 == item.Sad_itm_cd && _tmp.Grad_val2 == item.Sad_itm_line)
                        {
                            _tempReqAppDet.Grad_anal3 = _tmp.Grad_anal3;
                        }
                    }
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    if (!string.IsNullOrEmpty(item.Sad_sim_itm_cd))
                    {
                        _tempReqAppDet.Grad_anal15 = item.Sad_sim_itm_cd;
                    }
                    else
                    {
                        _tempReqAppDet.Grad_anal15 = item.Sad_itm_cd;
                    }
                    _ReqAppDetLog.Add(_tempReqAppDet);
                }
            }

            if (_doitemserials.Count > 0)
            {
                Int32 _line = 0;
                foreach (ReptPickSerials ser in _doitemserials)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                    _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                    _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                    _tempReqAppSer.Gras_anal5 = "";
                    foreach (RequestApprovalSerials _tmp in _repSer)
                    {
                        if (_tmp.Gras_anal2 == ser.Tus_itm_cd && _tmp.Gras_anal3 == ser.Tus_ser_1)
                        {
                            _tempReqAppSer.Gras_anal5 = _tmp.Gras_anal5;

                        }
                    }

                    _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                    _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqAppSerLog.Add(_tempReqAppSer);
                }
            }

        }

        protected void CollectReqApp()
        {
            string _type = "";
            _ReqAppHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqAppDet = new List<RequestApprovalDetail>();
            _ReqAppSer = new List<RequestApprovalSerials>();

            _ReqAppAuto = new MasterAutoNumber();

            _type = null;

            _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = "ARQT013";
            _ReqAppHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdr.Grah_ref = null;
            _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = "P";
            _ReqAppHdr.Grah_app_lvl = 0;
            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdr.Grah_sub_type = txtSubType.Text.Trim();
            _ReqAppHdr.Grah_oth_pc = lblSalePc.Text.Trim();



            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _tempReqAppDet = new RequestApprovalDetail();
                    _tempReqAppDet.Grad_ref = "1";
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "SALES REVERSAL";
                    _tempReqAppDet.Grad_val1 = item.Sad_srn_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = item.Sad_fws_ignore_qty;
                    _tempReqAppDet.Grad_val4 = item.Sad_tot_amt / item.Sad_srn_qty;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    foreach (RequestApprovalDetail _tmp in _repItem)
                    {
                        if (_tmp.Grad_anal2 == item.Sad_itm_cd && _tmp.Grad_val2 == item.Sad_itm_line)
                        { _tempReqAppDet.Grad_anal3 = _tmp.Grad_anal3; }
                    }
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    //Add Grad_anal15 column by Chamal 17-07-2015
                    if (string.IsNullOrEmpty(item.Sad_sim_itm_cd))
                    { _tempReqAppDet.Grad_anal15 = item.Sad_itm_cd; }
                    else
                    { _tempReqAppDet.Grad_anal15 = item.Sad_sim_itm_cd; }
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;

                    _ReqAppDet.Add(_tempReqAppDet);
                }
            }

            if (_doitemserials.Count > 0)
            {
                Int32 _line = 0;
                foreach (ReptPickSerials ser in _doitemserials)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                    _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                    _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                    _tempReqAppSer.Gras_anal5 = "";
                    foreach (RequestApprovalSerials _tmp in _repSer)
                    {
                        if (_tmp.Gras_anal2 == ser.Tus_itm_cd && _tmp.Gras_anal3 == ser.Tus_ser_1)
                        {
                            _tempReqAppSer.Gras_anal5 = _tmp.Gras_anal5;

                        }
                    }


                    _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                    _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqAppSer.Add(_tempReqAppSer);
                }
            }


            _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "RVREQ";
            _ReqAppAuto.Aut_year = null;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", null, null);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", BaseCls.GlbUserID, null);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    MessageBox.Show("No any request / approval found.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvPendings.DataSource = _TempReqAppHdr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        //private void dgvInvItem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    Int32 _line = 0;
        //    string _item = "";
        //    decimal _EditQty = 0;
        //    decimal _invQty = 0;
        //    decimal _unitAmt = 0;
        //    decimal _disAmt = 0;
        //    decimal _taxAmt = 0;
        //    decimal _totAmt = 0;


        //    List<InvoiceItem> _InvList = new List<InvoiceItem>();

        //    _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);
        //    _item = dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value.ToString();
        //    _EditQty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value);
        //    _invQty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invQty"].Value);


        //    if (_invQty < _EditQty)
        //    {
        //        MessageBox.Show("Cannot exceed invoice qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        dgvInvItem.AutoGenerateColumns = false;
        //        dgvInvItem.DataSource = new List<InvoiceItem>();
        //        dgvInvItem.DataSource = _InvDetailList;
        //        return;
        //    }

        //    if (_EditQty <= 0)
        //    {
        //        MessageBox.Show("Invalid return qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        dgvInvItem.AutoGenerateColumns = false;
        //        dgvInvItem.DataSource = new List<InvoiceItem>();
        //        dgvInvItem.DataSource = _InvDetailList;
        //        return;
        //    }

        //    foreach (InvoiceItem item in _InvDetailList)
        //    {
        //        if (item.Sad_itm_cd == _item && item.Sad_itm_line == _line)
        //        {
        //            if (item.Sad_srn_qty < _EditQty)
        //            {
        //                MessageBox.Show("Cannot exceed invoice qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                dgvInvItem.AutoGenerateColumns = false;
        //                dgvInvItem.DataSource = new List<InvoiceItem>();
        //                dgvInvItem.DataSource = _InvDetailList;
        //                return;
        //            }

        //            item.Sad_srn_qty = Convert.ToDecimal(_EditQty);
        //            _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
        //            _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
        //            _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
        //            _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));

        //            item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
        //            item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
        //            item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
        //            item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
        //        }
        //        _InvList.Add(item);
        //    }

        //    _InvDetailList = _InvList;
        //    dgvInvItem.AutoGenerateColumns = false;
        //    dgvInvItem.DataSource = new List<InvoiceItem>();
        //    dgvInvItem.DataSource = _InvDetailList;

        //}

        private void dgvPendings_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string _reqNo = "";
                string _stus = "";
                string _invNo = "";
                string _remarks = "";
                string _type = "";
                string _pc = "";
                string _retLoc = "";
                string _retSubType = "";
                string _salesPC = "";

                this.Cursor = Cursors.WaitCursor;

                txtInvoice.Text = "";
                txtCusCode.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                lblInvDate.Text = "";
                lblAccount.Text = "";
                lblSalePc.Text = "";
                txtInvoice.Enabled = true;
                txtCusCode.Enabled = true;
                btnRequest.Enabled = false;
                btnSave.Enabled = true;

                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();
                _repItem = new List<RequestApprovalDetail>();

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;

                dgvRereportItems.AutoGenerateColumns = false;
                dgvRereportItems.DataSource = new List<RequestAppAddDet>();
                dgvRereportItems.DataSource = _repItem;

                _reqNo = dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value.ToString();
                _stus = dgvPendings.Rows[e.RowIndex].Cells["col_Status"].Value.ToString();
                _invNo = dgvPendings.Rows[e.RowIndex].Cells["col_Inv"].Value.ToString();
                _remarks = dgvPendings.Rows[e.RowIndex].Cells["col_remarks"].Value.ToString();
                _type = dgvPendings.Rows[e.RowIndex].Cells["col_Type"].Value.ToString();
                _pc = dgvPendings.Rows[e.RowIndex].Cells["col_pc"].Value.ToString();
                _retLoc = dgvPendings.Rows[e.RowIndex].Cells["col_Type"].Value.ToString();
                _retSubType = dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value.ToString();
                _salesPC = dgvPendings.Rows[e.RowIndex].Cells["col_OthPC"].Value.ToString();
                //if (_type == "DEL")
                //{
                //    cmbRevType.Text = "Deliverd Sale";
                //}
                //else if (_type == "FOR")
                //{
                //    cmbRevType.Text = "Forward Sale";
                //}

                if (_salesPC != _pc)
                {
                    chkOthSales.Checked = true;
                    chkOthSales.Enabled = false;
                }
                else
                {
                    chkOthSales.Checked = false;
                    chkOthSales.Enabled = false;
                }

                txtInvoice.Text = _invNo;
                lblReq.Text = _reqNo;
                txtRemarks.Text = _remarks;
                lblReturnLoc.Text = _type;
                lblReqPC.Text = _pc;
                txtSubType.Text = _retSubType;
                txtSRNremarks.Text = _remarks;
                lblSalePc.Text = _salesPC;

                lblSubDesc.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtSubType.Text))
                {
                    if (IsValidAdjustmentSubType() == false)
                    {
                        MessageBox.Show("Invalid return sub type.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblSubDesc.Text = string.Empty;
                        txtSubType.Clear();
                        txtSubType.Focus();
                        return;
                    }
                }

                if (_stus == "A")
                {
                    lblStatus.Text = "APPROVED";
                    btnSave.Enabled = true;
                }
                else if (_stus == "P")
                {
                    lblStatus.Text = "PENDING";
                    btnSave.Enabled = false;
                }
                else if (_stus == "R")
                {
                    lblStatus.Text = "REJECT";
                    btnSave.Enabled = false;
                }
                else if (_stus == "F")
                {
                    lblStatus.Text = "FINISHED";
                    btnSave.Enabled = false;
                }

                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, _salesPC, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());

                foreach (InvoiceHeader _tempInv in _invHdr)
                {
                    if (_tempInv.Sah_inv_no == null)
                    {
                        MessageBox.Show("Error loading request.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        lblAccount.Text = "";
                        lblSalePc.Text = "";
                        _dCusCode = "";
                        _dCusAdd1 = "";
                        _dCusAdd2 = "";
                        _currency = "";
                        _exRate = 0;
                        _invTP = "";
                        _executiveCD = "";
                        _manCode = "";
                        _isTax = false;
                        txtInvoice.Focus();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        txtCusCode.Text = _tempInv.Sah_cus_cd;
                        lblInvCusName.Text = _tempInv.Sah_cus_name;
                        lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                        lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                        lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                        lblAccount.Text = _tempInv.Sah_acc_no;
                        lblSalePc.Text = _tempInv.Sah_pc;
                        _dCusCode = _tempInv.Sah_d_cust_cd;
                        _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                        _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                        _currency = _tempInv.Sah_currency;
                        _exRate = _tempInv.Sah_ex_rt;
                        _invTP = _tempInv.Sah_inv_tp;
                        _executiveCD = _tempInv.Sah_sales_ex_cd;
                        _manCode = _tempInv.Sah_man_cd;
                        _isTax = _tempInv.Sah_tax_inv;

                        if (lblStatus.Text == "FINISHED")
                        {
                            MessageBox.Show("Selected request is in FINISHED status.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnApp.Enabled = false;
                            btnSave.Enabled = false;
                            ucHpAccountSummary1.Clear();
                            ucHpAccountDetail1.Clear();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else if (_tempInv.Sah_stus == "C")
                        {
                            MessageBox.Show("Selected invoice is cancelled.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnApp.Enabled = false;
                            btnSave.Enabled = false;
                            ucHpAccountSummary1.Clear();
                            ucHpAccountDetail1.Clear();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else if (_tempInv.Sah_stus == "R")
                        {
                            MessageBox.Show("This invoice is already reversed.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnApp.Enabled = false;
                            btnSave.Enabled = false;
                            ucHpAccountSummary1.Clear();
                            ucHpAccountDetail1.Clear();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else if (lblStatus.Text == "APPROVED")
                        {
                            BindAccountReceipt(lblAccount.Text.Trim(), _pc);
                            ucHpAccountSummary1.Clear();
                            ucHpAccountDetail1.Clear();

                            HpAccount accList = new HpAccount();
                            accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccount.Text.Trim());

                            string _accPC = "";
                            if (_isFromReq == true)
                            {
                                _accPC = lblReqPC.Text.Trim();
                            }
                            else if (_isFromReq == false)
                            {
                                _accPC = BaseCls.GlbUserDefProf;
                            }

                            ucHpAccountSummary1.set_all_values(accList, _accPC, Convert.ToDateTime(dtpSRNDate.Value).Date, _accPC);
                            ucHpAccountDetail1.Uc_hpa_acc_no = accList.Hpa_acc_no;
                            btnSave.Enabled = true;
                        }

                        _isFromReq = true;

                        Load_InvoiceDetails(_pc);

                        DataTable _newRep = new DataTable();
                        //_newRep = CHNLSVC.General.SearchrequestAppDetByRef(_reqNo);
                        _newRep = CHNLSVC.General.SearchrequestAppAddDetByRef(_reqNo);

                        dgvRereportItems.DataSource = _newRep;

                        List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                        DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, _reqNo, out List);


                        dgvSerApp.DataSource = dt;

                        txtCusCode.Enabled = false;
                        txtInvoice.Enabled = false;
                        dgvInvItem.Columns["col_invRevQty"].ReadOnly = true;
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void BindAccountReceipt(string _account, string _pc)
        {
            try
            {
                List<RecieptHeader> _receipt = CHNLSVC.Sales.GetReceiptByAccountNo(BaseCls.GlbUserComCode, _pc, _account);
                if (_receipt != null)
                {
                    //(from _up in _receipt
                    // where _up.Sar_direct == false && _up.Sar_receipt_type != "INSUR" && _up.Sar_receipt_type != "INSURR" && _up.Sar_receipt_type != "VHINSR" && _up.Sar_receipt_type != "VHINSRR"
                    // select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

                    var _list = from _one in _receipt
                                where _one.Sar_receipt_type != "INSUR" && _one.Sar_receipt_type != "INSURR" && _one.Sar_receipt_type != "VHINSR" && _one.Sar_receipt_type != "VHINSRR"
                                group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                                select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };


                    List<RecieptHeader> _reverse = (from _res in _receipt
                                                    where _res.Sar_receipt_type == ("HPREV") || _res.Sar_receipt_type == ("HPDRV") || _res.Sar_receipt_type == ("INSURR") || _res.Sar_receipt_type == ("VHINSRR")
                                                    select _res).ToList<RecieptHeader>();

                    List<RecieptHeader> _removeList = new List<RecieptHeader>();

                    if (_reverse != null && _reverse.Count > 0)
                    {
                        //remove reverse from original list
                        foreach (RecieptHeader _recHdr in _reverse)
                        {
                            _receipt.Remove(_recHdr);
                        }

                        //check for sar_prefix,sar_manual_ref_no
                        foreach (RecieptHeader _recHdr in _receipt)
                        {
                            List<RecieptHeader> _temp = (from _res in _reverse
                                                         where _res.Sar_prefix == _recHdr.Sar_prefix && _res.Sar_manual_ref_no == _recHdr.Sar_manual_ref_no
                                                         select _res).ToList<RecieptHeader>();
                            if (_temp != null && _temp.Count > 0)
                            {

                                if ((_recHdr.Sar_tot_settle_amt - _temp[0].Sar_tot_settle_amt) > 0)
                                {
                                    _recHdr.Sar_tot_settle_amt = _recHdr.Sar_tot_settle_amt - _temp[0].Sar_tot_settle_amt;
                                    _recHdr.Sar_comm_amt = _recHdr.Sar_comm_amt - _temp[0].Sar_comm_amt;
                                }
                                else
                                {
                                    _removeList.Add(_recHdr);
                                }

                            }
                        }
                        if (_removeList != null && _removeList.Count > 0)
                        {
                            foreach (RecieptHeader _recHdr in _removeList)
                            {
                                _receipt.Remove(_recHdr);

                            }
                        }
                        _recieptHeader = _receipt;
                    }
                    //return original list
                    else
                    {
                        _recieptHeader = _receipt;
                    }


                    //_recieptHeader = _receipt;

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

        private void btnApp_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    MessageBox.Show("Please select reversal sub type.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "A";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = txtSubType.Text.Trim();

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully approved.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRej_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "R";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully rejected.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _msg = "";
                if (CheckServerDateTime() == false) return;

                if (lblStatus.Text != "APPROVED")
                {
                    MessageBox.Show("Request is still not approved.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvInvItem.Rows.Count <= 0)
                {
                    MessageBox.Show("Cannot find reverse details.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (BaseCls.GlbUserDefProf != lblReqPC.Text.Trim())
                {
                    MessageBox.Show("Login profit center and request profit center is mismatch.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpSRNDate, lblBackDateInfor, dtpSRNDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpSRNDate.Value.Date != DateTime.Now.Date)
                        {
                            dtpSRNDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpSRNDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpSRNDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpSRNDate.Focus();
                        return;
                    }
                }


                HpAccount _tmpAccDet = new HpAccount();
                _tmpAccDet = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccount.Text);

                if (_tmpAccDet.Hpa_acc_no == null)
                {
                    MessageBox.Show("Account details are missing.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Calculate credit balance______________
                //Get receipt total
                List<RecieptHeader> _CrRec = new List<RecieptHeader>();
                List<HpAdjustment> _adj = new List<HpAdjustment>();
                _CrRec = CHNLSVC.Sales.GetReceiptByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccount.Text.Trim());
                decimal _totPaid = 0;
                decimal _totRev = 0;
                decimal _totAdjVal = 0;
                decimal _finalCrAmt = 0;

                if (_CrRec != null)
                {
                    //    MessageBox.Show("Cannot find collection details.Please check account number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    //else
                    //{
                    foreach (RecieptHeader _tmp in _CrRec)
                    {
                        if (_tmp.Sar_receipt_type == "HPDPM" || _tmp.Sar_receipt_type == "HPDPS" || _tmp.Sar_receipt_type == "HPARS" || _tmp.Sar_receipt_type == "HPARM" || _tmp.Sar_receipt_type == "HPRM" || _tmp.Sar_receipt_type == "HPRS")
                        {
                            _totPaid = _totPaid + _tmp.Sar_tot_settle_amt;
                        }

                        if (_tmp.Sar_receipt_type == "HPDRV" || _tmp.Sar_receipt_type == "HPREV")
                        {
                            _totRev = _totRev + _tmp.Sar_tot_settle_amt;

                        }
                    }
                }

                // Get adjustments ---------------
                _totAdjVal = CHNLSVC.Sales.Get_hp_Adjustment(lblAccount.Text.Trim());

                _finalCrAmt = (_totPaid - _totRev) + _totAdjVal;

                InvoiceHeader _invheader = new InvoiceHeader();

                //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = _currency;
                _invheader.Sah_cus_add1 = lblInvCusAdd1.Text.Trim();
                _invheader.Sah_cus_add2 = lblInvCusAdd2.Text.Trim();
                _invheader.Sah_cus_cd = txtCusCode.Text.Trim();
                _invheader.Sah_cus_name = lblInvCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = _dCusAdd1;
                _invheader.Sah_d_cust_add2 = _dCusAdd2;
                _invheader.Sah_d_cust_cd = _dCusCode;
                _invheader.Sah_direct = false;
                _invheader.Sah_dt = Convert.ToDateTime(dtpSRNDate.Value).Date;
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = _exRate;
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "REV";
                _invheader.Sah_inv_tp = _invTP;
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_cd = _manCode;
                _invheader.Sah_man_ref = txtManRef.Text.Trim();
                _invheader.Sah_manual = false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = _tmpAccDet.Hpa_pc.Trim(); //lblSalePc.Text.Trim(); //BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtInvoice.Text.Trim();
                _invheader.Sah_remarks = txtSRNremarks.Text;
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = _executiveCD;
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_structure_seq = "";
                _invheader.Sah_stus = "A";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_tax_inv = _isTax;
                _invheader.Sah_anal_3 = lblReq.Text.Trim();
                _invheader.Sah_anal_4 = "ARQT013";
                _invheader.Sah_acc_no = lblAccount.Text.Trim();
                _invheader.Sah_anal_5 = txtSubType.Text.Trim();
                _invheader.Sah_anal_7 = _finalCrAmt;


                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = _tmpAccDet.Hpa_pc.Trim();//lblSalePc.Text.Trim(); //BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PC";
                _invoiceAuto.Aut_direction = 0;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "REV";
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = "INREV";
                _invoiceAuto.Aut_year = null;

                InventoryHeader _inventoryHeader = new InventoryHeader();
                MasterAutoNumber _SRNAuto = new MasterAutoNumber();
                //inventory document

                if (_doitemserials.Count > 0)
                {
                    _inventoryHeader.Ith_com = BaseCls.GlbUserComCode;
                    _inventoryHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                    DateTime _docDate = Convert.ToDateTime(dtpSRNDate.Value).Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = txtSubType.Text.Trim();
                    _inventoryHeader.Ith_bus_entity = "";
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_remarks = txtSRNremarks.Text.Trim();
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_pc = _tmpAccDet.Hpa_pc.Trim();


                    _SRNAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(dtpSRNDate.Value).Year;
                }

                //HP Receipt reversals
                List<RecieptHeader> _hpReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _revReceipt = new MasterAutoNumber();

                _revReceipt.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _revReceipt.Aut_cate_tp = "PC";
                _revReceipt.Aut_direction = 1;
                _revReceipt.Aut_modify_dt = null;
                _revReceipt.Aut_moduleid = "HP";
                _revReceipt.Aut_number = 0;
                _revReceipt.Aut_start_char = "HPREV";
                _revReceipt.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;

                List<RecieptHeader> _processRecList = new List<RecieptHeader>();

                foreach (RecieptHeader rec in _recieptHeader)
                {
                    //ADDED BY SACHITH -2014/02/20
                    //CHECK OTHER SHOP REF
                    if (rec.Sar_is_oth_shop)
                    {
                        List<HpTransaction> _txnRef = CHNLSVC.Sales.GetHpTransactionByRef(rec.Sar_receipt_no);
                        if (_txnRef != null && _txnRef.Count > 0)
                        {

                        }
                        else
                        {
                            continue;
                        }
                    }


                    rec.Sar_direct = false;
                    rec.Sar_profit_center_cd = _tmpAccDet.Hpa_pc.Trim(); //BaseCls.GlbUserDefProf;
                    rec.Sar_create_by = BaseCls.GlbUserID;
                    rec.Sar_mod_by = BaseCls.GlbUserID;
                    rec.Sar_is_oth_shop = false;
                    rec.Sar_oth_sr = "";
                    rec.Sar_session_id = BaseCls.GlbUserSessionID;
                    _processRecList.Add(rec);
                }
                //_hpReversReceiptHeader = _recieptHeader;
                _hpReversReceiptHeader = _processRecList;

                if (_hpReversReceiptHeader.Count != 0)
                {
                    if (_hpReversReceiptHeader[0].Sar_receipt_type == "HPDPS" || _hpReversReceiptHeader[0].Sar_receipt_type == "HPARS")
                    { _revReceipt.Aut_start_char = "HPRS"; }
                    else { _revReceipt.Aut_start_char = "HPRM"; }
                }

                MasterAutoNumber _auto = new MasterAutoNumber();
                _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _auto.Aut_cate_tp = "PC";
                _auto.Aut_start_char = "HPT";
                _auto.Aut_direction = 1;
                _auto.Aut_modify_dt = null;
                _auto.Aut_moduleid = "HP";
                _auto.Aut_number = 0;
                _auto.Aut_year = null;
                //string temp = CHNLSVC.Sales.GetRecieptNo(_auto);

                // int serialId = CHNLSVC.Inventory.GetSerialID();
                HpTransaction _transaction = new HpTransaction();
                _transaction.Hpt_com = BaseCls.GlbUserComCode;
                _transaction.Hpt_pc = BaseCls.GlbUserDefProf;
                _transaction.Hpt_cre_by = BaseCls.GlbUserID;
                _transaction.Hpt_cre_dt = DateTime.Now;
                _transaction.Hpt_txn_dt = Convert.ToDateTime(dtpSRNDate.Value).Date;
                _transaction.Hpt_txn_tp = "REV";
                _transaction.Hpt_desc = "SALES REVERSAL";
                _transaction.Hpt_crdt = ucHpAccountSummary1.Uc_AccBalance;
                _transaction.Hpt_ref_no = "1";
                _transaction.Hpt_seq = 0;
                _transaction.Hpt_acc_no = lblAccount.Text.Trim();
                // int res = CHNLSVC.Sales.Save_HpTransaction(_transaction);

                string _ReversNo = "";
                string _crednoteNo = ""; //add by chamal 05-12-2012


                foreach (ReptPickSerials _ser in _doitemserials)
                {
                    string _newStus = "";
                    DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(lblReq.Text);
                    var _newStus1 = (from _res in _dt.AsEnumerable()
                                     where _res["grad_anal15"].ToString() == _ser.Tus_itm_cd
                                     select _res["grad_anal8"].ToString()).ToList();

                    //var _newStus1 = (from _res in _dt.AsEnumerable()
                    //                 select _res["grad_anal8"].ToString()).ToList();

                    if (_newStus1 != null)
                    {
                        _newStus = _newStus1[0];

                        string _stus;
                        string _itm = _ser.Tus_itm_cd;
                        string _orgStus = _ser.Tus_itm_stus;
                        string _serial = _ser.Tus_ser_1;
                        if (!string.IsNullOrEmpty(_newStus))
                        {
                            _stus = _newStus;

                            DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                            if (dt.Rows.Count > 0)
                            {
                                DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                    {
                                        MessageBox.Show("Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial + ". Approved status " + _stus, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                        }

                    }



                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(BaseCls.GlbUserComCode, "RVR", _newStus, (((dtpSRNDate.Value.Year - _ser.Tus_doc_dt.Year) * 12) + dtpSRNDate.Value.Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val;
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                        else
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                    }

                    if (!string.IsNullOrEmpty(_newStus))
                    {
                        _ser.Tus_itm_stus = _newStus;
                    }

                }

                #region Check receving serials are duplicating :: Chamal 08-May-2014
                string _err = string.Empty;
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
                {
                    MessageBox.Show(_err.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                int effect = CHNLSVC.Sales.SaveHPReversal(_invheader, _InvDetailList, _invoiceAuto, true, out _ReversNo, _inventoryHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _revReceipt, _hpReversReceiptHeader, _transaction, _auto, out _crednoteNo);


                if (effect == 1)
                {
                    MessageBox.Show("Successfully created.Reversal No: " + _ReversNo, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ReportViewer _view = new ReportViewer();
                    BaseCls.GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "INV";
                    _view.GlbReportName = "InvoiceHalfPrints.rpt";
                    _view.GlbReportDoc = _ReversNo;
                    _view.GlbSerial = null;
                    _view.GlbWarranty = null;
                    _view.Show();

                    _view = null;

                    if (!string.IsNullOrEmpty(_crednoteNo))
                    {
                        ReportViewerInventory _insu = new ReportViewerInventory();
                        BaseCls.GlbReportName = string.Empty;
                        _insu.GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "INWARD";
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                            _insu.GlbReportName = "SInward_Docs.rpt";
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                            _insu.GlbReportName = "Dealer_Inward_Docs.rpt";
                        else _insu.GlbReportName = "Inward_Docs.rpt";
                        _insu.GlbReportDoc = _crednoteNo;
                        _insu.Show();
                        _insu = null;
                    }
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    { MessageBox.Show(_msg, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else
                    { MessageBox.Show(_ReversNo + " " + _crednoteNo, "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        }

        private void chkAccDet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAccDet.Checked == true)
                {
                    ucHpAccountSummary1.Clear();
                    ucHpAccountDetail1.Clear();
                    if (string.IsNullOrEmpty(lblAccount.Text))
                    {
                        MessageBox.Show("Please select HP invoice.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkAccDet.Checked = false;
                        pnlAcc.Visible = false;
                        return;
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        HpAccount accList = new HpAccount();
                        accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccount.Text.Trim());

                        string _accPC = "";
                        if (_isFromReq == true)
                        {
                            _accPC = lblReqPC.Text.Trim();
                        }
                        else if (_isFromReq == false)
                        {
                            _accPC = BaseCls.GlbUserDefProf;
                        }

                        ucHpAccountSummary1.set_all_values(accList, _accPC, Convert.ToDateTime(dtpSRNDate.Value).Date, _accPC);
                        ucHpAccountDetail1.Uc_hpa_acc_no = accList.Hpa_acc_no;
                        this.Cursor = Cursors.Default;
                        pnlAcc.Visible = true;
                    }
                }
                else
                {
                    if (lblStatus.Text != "APPROVED")
                    {
                        ucHpAccountSummary1.Clear();
                        ucHpAccountDetail1.Clear();
                    }
                    pnlAcc.Visible = false;
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

        private void btnAccClose_Click(object sender, EventArgs e)
        {
            try
            {
                ucHpAccountSummary1.Clear();
                ucHpAccountDetail1.Clear();
                pnlAcc.Visible = false;
                chkAccDet.Checked = false;
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

        private void btnSubTypeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubType;
                _CommonSearch.ShowDialog();
                txtSubType.Select();
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

        private void txtSubType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                    DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSubType;
                    _CommonSearch.ShowDialog();
                    txtSubType.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSRNremarks.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSubType_Leave(object sender, EventArgs e)
        {
            try
            {
                lblSubDesc.Text = string.Empty;
                if (string.IsNullOrEmpty(txtSubType.Text)) return;
                if (IsValidAdjustmentSubType() == false)
                {
                    MessageBox.Show("Invalid return sub type.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblSubDesc.Text = string.Empty;
                    txtSubType.Clear();
                    txtSubType.Focus();
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

        private bool IsValidAdjustmentSubType()
        {

            bool status = false;
            try
            {
                txtSubType.Text = txtSubType.Text.Trim().ToUpper().ToString();
                DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("SRN", txtSubType.Text.ToString());
                if (_adjSubType.Rows.Count > 0)
                {
                    lblSubDesc.Text = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                    status = true;
                }
                else
                {
                    status = false;
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
            return status;
        }

        private void chkOthSales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthSales.Checked == true)
            {
                txtCusCode.Text = "";
                txtCusCode.Enabled = false;

            }
            else
            {
                txtCusCode.Text = "";
                txtCusCode.Enabled = true;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblRevItem.Text))
                {
                    MessageBox.Show("Please select reverse item.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please select re-report item.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblInvLine.Text))
                {
                    MessageBox.Show("Please select reverse item.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtNewPrice.Text))
                {
                    MessageBox.Show("Please enter re-report price.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var currrange = (from cur in _repItem
                                 where cur.Grad_anal2 == lblRevItem.Text.Trim() && cur.Grad_val2 == Convert.ToInt16(lblInvLine.Text) && cur.Grad_anal4 == txtNewSerial.Text.Trim() && cur.Grad_anal3 == txtItem.Text.Trim()
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {
                    MessageBox.Show("Selected item already exsist .", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                RequestApprovalDetail _tmpRepItem = new RequestApprovalDetail();
                _tmpRepItem.Grad_anal2 = lblRevItem.Text.Trim();
                _tmpRepItem.Grad_anal3 = txtItem.Text.Trim();
                _tmpRepItem.Grad_val2 = Convert.ToInt16(lblInvLine.Text);
                _tmpRepItem.Grad_anal4 = txtNewSerial.Text.Trim();

                RequestAppAddDet _tmpAddDet = new RequestAppAddDet();
                _tmpAddDet.Grad_anal1 = lblRevItem.Text.Trim();
                _tmpAddDet.Grad_anal2 = txtItem.Text.Trim();
                _tmpAddDet.Grad_anal3 = txtNewSerial.Text.Trim();
                _tmpAddDet.Grad_anal5 = txtNewSch.Text.Trim();
                _tmpAddDet.Grad_anal6 = Convert.ToDecimal(lblRepPrice.Text);
                _tmpAddDet.Grad_anal7 = Convert.ToDecimal(txtNewPrice.Text);
                _tmpAddDet.Grad_anal8 = Convert.ToInt32(lblInvLine.Text);
                _tmpAddDet.Grad_anal9 = Convert.ToDecimal(txtRqty.Text);


                _repItem.Add(_tmpRepItem);
                _repAddDet.Add(_tmpAddDet);

                dgvRereportItems.AutoGenerateColumns = false;
                dgvRereportItems.DataSource = new List<RequestAppAddDet>();
                dgvRereportItems.DataSource = _repAddDet;

                lblRevItem.Text = "";
                txtItem.Text = "";
                lblInvLine.Text = "";
                lblRepPrice.Text = "";
                txtNewSerial.Text = "";
                txtNewPrice.Text = "";

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

        private void dgvRereportItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 9 && e.RowIndex != -1)
            if (e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you want to remove selected item details ?", "Hire sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_repItem == null || _repItem.Count == 0) return;

                    string _item = dgvRereportItems.Rows[e.RowIndex].Cells["col_OldItm"].Value.ToString();
                    Int16 _engine = Convert.ToInt16(dgvRereportItems.Rows[e.RowIndex].Cells["col_RevLine"].Value);



                    List<RequestAppAddDet> _tmp1 = new List<RequestAppAddDet>();
                    List<RequestApprovalDetail> _temp = new List<RequestApprovalDetail>();
                    _temp = _repItem;
                    _tmp1 = _repAddDet;

                    _temp.RemoveAll(x => x.Grad_anal1 == _item && x.Grad_val2 == _engine);
                    _repItem = _temp;
                    _tmp1.RemoveAll(x => x.Grad_anal1 == _item && x.Grad_anal8 == _engine);
                    _repAddDet = _tmp1;


                    dgvRereportItems.AutoGenerateColumns = false;
                    dgvRereportItems.DataSource = new List<RequestAppAddDet>();
                    dgvRereportItems.DataSource = _repAddDet;
                }
            }
        }

        private void dgvInvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInvItem.Rows.Count == 0) return;

            Int32 _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);
            string _itm = Convert.ToString(dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value);
            decimal _repPrice = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invTot"].Value) / Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value);

            lblInvLine.Text = _line.ToString();
            lblRevItem.Text = _itm;
            lblRepPrice.Text = _repPrice.ToString();
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
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

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
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

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItem;
                    _CommonSearch.ShowDialog();
                    txtItem.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtNewSerial.Focus();
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

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            if (pnlReRep.Visible == true)
            {
                pnlReRep.Visible = false;
            }
            else
            {
                pnlReRep.Visible = true;
            }
        }

        private void btnClsDoItems_Click(object sender, EventArgs e)
        {
            pnlReRep.Visible = false;
        }

        private void btnReOk_Click(object sender, EventArgs e)
        {
            pnlReRep.Visible = false;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _repItem = new List<RequestApprovalDetail>();
            RequestApprovalDetail _tmpApp = new RequestApprovalDetail();

            dgvRereportItems.AutoGenerateColumns = false;
            dgvRereportItems.DataSource = new List<RequestApprovalDetail>();

            foreach (InvoiceItem _tmp in _InvDetailList)
            {
                _tmpApp.Grad_anal2 = _tmp.Sad_itm_cd;
                _tmpApp.Grad_anal3 = _tmp.Sad_itm_cd;
                _tmpApp.Grad_val2 = _tmp.Sad_itm_line;
                //Add Grad_anal15 column by Chamal 17-07-2015
                if (string.IsNullOrEmpty(_tmp.Sad_sim_itm_cd))
                { _tmpApp.Grad_anal15 = _tmp.Sad_itm_cd; }
                else
                { _tmpApp.Grad_anal15 = _tmp.Sad_sim_itm_cd; }

                _repItem.Add(_tmpApp);
            }

            dgvRereportItems.AutoGenerateColumns = false;
            dgvRereportItems.DataSource = new List<RequestApprovalDetail>();
            dgvRereportItems.DataSource = _repItem;
        }

        private void btnAttSerApp_Click(object sender, EventArgs e)
        {
            if (pnlSerApp.Visible == true)
            {
                pnlSerApp.Visible = false;
            }
            else
            {
                pnlSerApp.Visible = true;
            }
        }

        private void btnSearchJobNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch(); _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeJob);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceJob(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result; _CommonSearch.BindUCtrlDDLData(_result); _CommonSearch.obj_TragetTextBox = txtJobNo;
                _CommonSearch.IsSearchEnter = true; this.Cursor = Cursors.Default; _CommonSearch.ShowDialog(); txtJobNo.Select();
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

        private void txtJobNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchJobNo_Click(null, null);
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnSearchJobNo_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddSerApp.Focus();
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

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    return;
                }

                DataTable _jobDet = new DataTable();
                _jobDet = CHNLSVC.Sales.GetServiceJobDet(BaseCls.GlbUserComCode, txtJobNo.Text);

                if (_jobDet.Rows.Count > 0)
                {
                    // lblSerRem.Text = _jobDet.Rows[0]["insa_jb_rem"].ToString();
                    if (_jobDet.Rows[0]["insa_ser"].ToString().Trim() == lblSerial.Text.Trim())
                    {
                        lblSerRem.Text = _jobDet.Rows[0]["insa_jb_rem"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Selected service approval not belongs to return serial. Pls. check.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJobNo.Text = "";
                        lblSerRem.Text = "";
                        txtJobNo.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid or already used service approval.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Text = "";
                    lblSerRem.Text = "";
                    txtJobNo.Focus();
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

        private void btnSerAppClose_Click(object sender, EventArgs e)
        {
            pnlSerApp.Visible = false;
        }

        private void btnServiceAppConf_Click(object sender, EventArgs e)
        {
            pnlSerApp.Visible = false;
        }

        private void btnAddSerApp_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblSerItem.Text))
                {
                    MessageBox.Show("Please select retrun item.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblSerItem.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please select related job #.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Text = "";
                    txtJobNo.Focus();
                    return;
                }


                var currrange = (from cur in _repSer
                                 where cur.Gras_anal2 == lblSerItem.Text.Trim() && cur.Gras_anal3 == lblSerial.Text.Trim() && cur.Gras_anal5 == txtJobNo.Text.Trim()
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {
                    MessageBox.Show("Selected details already exsist .", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Focus();
                    return;
                }

                RequestApprovalSerials _tmpRepSer = new RequestApprovalSerials();
                _tmpRepSer.Gras_anal2 = lblSerItem.Text.Trim();
                _tmpRepSer.Gras_anal3 = lblSerial.Text.Trim();
                _tmpRepSer.Gras_anal5 = txtJobNo.Text.Trim();

                _repSer.Add(_tmpRepSer);

                dgvSerApp.AutoGenerateColumns = false;
                dgvSerApp.DataSource = new List<RequestApprovalSerials>();
                dgvSerApp.DataSource = _repSer;

                lblSerItem.Text = "";
                lblSerial.Text = "";
                lblWarranty.Text = "";
                txtJobNo.Text = "";
                lblSerRem.Text = "";


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

        private void dgvDelDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDelDetails.Rows.Count == 0) return;

            string _itm = Convert.ToString(dgvDelDetails.Rows[e.RowIndex].Cells["col_item"].Value);
            string _ser = Convert.ToString(dgvDelDetails.Rows[e.RowIndex].Cells["col_Serial"].Value);
            string _wara = Convert.ToString(dgvDelDetails.Rows[e.RowIndex].Cells["col_Wara"].Value);

            lblSerItem.Text = _itm;
            lblSerial.Text = _ser;
            lblWarranty.Text = _wara;
        }

        private void dgvSerApp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Do you want to remove selected job details ?", "Hire sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (_repSer == null || _repSer.Count == 0) return;

                string _item = dgvSerApp.Rows[e.RowIndex].Cells["col_SerItem"].Value.ToString();
                string _serial = dgvSerApp.Rows[e.RowIndex].Cells["col_SerSerial"].Value.ToString();
                string _jobNo = dgvSerApp.Rows[e.RowIndex].Cells["col_JobNo"].Value.ToString();




                List<RequestApprovalSerials> _temp = new List<RequestApprovalSerials>();
                _temp = _repSer;

                _temp.RemoveAll(x => x.Gras_anal2 == _item && x.Gras_anal3 == _serial && x.Gras_anal5 == _jobNo);
                _repSer = _temp;


                dgvSerApp.AutoGenerateColumns = false;
                dgvSerApp.DataSource = new List<RequestApprovalSerials>();
                dgvSerApp.DataSource = _repSer;
            }
        }

        private void btnScheme_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNewSch;
                _CommonSearch.ShowDialog();
                txtNewSch.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewSch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNewSch.Text))
                {

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtNewSch.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNewSch.Text = "";
                        txtNewSch.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNewSch.Text = "";
                        txtNewSch.Focus();
                        return;
                    }

                    //txtNewSch.Text = _tmpSch.Hsd_sch_tp;

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewSch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtNewSch;
                    _CommonSearch.ShowDialog();
                    txtNewSch.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtItem.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewSerial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                    DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtNewSerial;
                    _CommonSearch.ShowDialog();
                    txtNewSerial.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtNewPrice.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAddItem.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSerialSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please select new item.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNewSerial;
                _CommonSearch.ShowDialog();
                txtNewSerial.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewSerial_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNewSerial.Text))
                {
                    return;
                }

                ReptPickSerials _serialList = new ReptPickSerials();
                _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), txtNewSerial.Text.Trim(), string.Empty, string.Empty);

                if (_serialList.Tus_ser_1 != null)
                {
                    txtNewSerial.Text = _serialList.Tus_ser_1;
                }
                else
                {
                    MessageBox.Show("Invalid serial.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewSerial.Text = "";
                    txtNewSerial.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNewPrice.Text))
                {
                    if (!IsNumeric(txtNewPrice.Text))
                    {
                        MessageBox.Show("Price should be numeric.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNewPrice.Text = "";
                        txtNewPrice.Focus();
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInvItem.Rows.Count == 0) return;

            Int32 _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);
            string _itm = Convert.ToString(dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value);
            decimal _repPrice = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invTot"].Value) / Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value);

            lblInvLine.Text = _line.ToString();
            lblRevItem.Text = _itm;
            lblRepPrice.Text = _repPrice.ToString();
        }

        private void viewReminds(string accNo)
        {
            bool isReminderOpen = false;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "VIewManagerReminds")
                {
                    isReminderOpen = true;
                }
            }
            if (!isReminderOpen)
            {
                List<HPReminder> oHPReminder = new List<HPReminder>();
                oHPReminder = CHNLSVC.General.Notification_Get_AccountRemindersDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf).FindAll(x => x.Hra_ref == accNo); ;

                if (oHPReminder.Count > 0)
                {
                    VIewManagerReminds frm = new VIewManagerReminds(oHPReminder);
                    frm.ShowDialog();
                }
            }
        }

        private void txtRqty_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRqty.Text))
            {
                MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRqty.Focus();
                return;
            }

            if (!IsNumeric(txtRqty.Text))
            {
                MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRqty.Focus();
                return;
            }

            if (Convert.ToDecimal(txtRqty.Text) <= 0)
            {
                MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRqty.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtNewSerial.Text))
            {
                if (Convert.ToDecimal(txtRqty.Text) > 1)
                {
                    MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRqty.Focus();
                    return;
                }
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (txtItem.Text != "")
            {
                //kapila 10/4/2015
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                if (_item == null)
                {
                    MessageBox.Show("Invalid item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItem.Focus();
                    return;
                }
            }
        }







        //private void dgvInvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (MessageBox.Show("Do you want to remove selected item ?", "Hire sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        if (_InvDetailList == null || _InvDetailList.Count == 0) return;

        //        string _item = dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value.ToString();
        //        Int32 _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);

        //        List<InvoiceItem> _temp = new List<InvoiceItem>();
        //        List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
        //        _doitemserials = new List<ReptPickSerials>();

        //        _temp = _InvDetailList;

        //        _temp.RemoveAll(x => x.Sad_itm_cd == _item && x.Sad_itm_line == _line);
        //        _InvDetailList = _temp;

        //        if (_InvDetailList.Count > 0)
        //        {
        //            foreach (InvoiceItem item in _InvDetailList)
        //            {
        //                _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
        //                _doitemserials.AddRange(_tempDOSerials);
        //            }
        //        }

        //        dgvInvItem.AutoGenerateColumns = false;
        //        dgvInvItem.DataSource = new List<InvoiceItem>();
        //        dgvInvItem.DataSource = _InvDetailList;

        //        dgvDelDetails.AutoGenerateColumns = false;
        //        dgvDelDetails.DataSource = new List<ReptPickSerials>();
        //        dgvDelDetails.DataSource = _doitemserials;
        //    }
        //}

        //private void dgvInvItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    Int32 _line = 0;
        //    string _item = "";
        //    decimal _EditQty = 0;
        //    decimal _invQty = 0;
        //    decimal _unitAmt = 0;
        //    decimal _disAmt = 0;
        //    decimal _taxAmt = 0;
        //    decimal _totAmt = 0;

        //    if (dgvInvItem.Rows.Count > 0)
        //    {
        //        List<InvoiceItem> _InvList = new List<InvoiceItem>();

        //        if (string.IsNullOrEmpty(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value.ToString()))
        //        {
        //            MessageBox.Show("Please enter valid reversal qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            dgvInvItem.AutoGenerateColumns = false;
        //            dgvInvItem.DataSource = new List<InvoiceItem>();
        //            dgvInvItem.DataSource = _InvDetailList;
        //            return;
        //        }

        //        _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);
        //        _item = dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value.ToString();
        //        _EditQty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value);
        //        _invQty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invQty"].Value);


        //        if (_invQty < _EditQty)
        //        {
        //            MessageBox.Show("Cannot exceed invoice qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            dgvInvItem.AutoGenerateColumns = false;
        //            dgvInvItem.DataSource = new List<InvoiceItem>();
        //            dgvInvItem.DataSource = _InvDetailList;
        //            return;
        //        }

        //        if (_EditQty <= 0)
        //        {
        //            MessageBox.Show("Invalid return qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            dgvInvItem.AutoGenerateColumns = false;
        //            dgvInvItem.DataSource = new List<InvoiceItem>();
        //            dgvInvItem.DataSource = _InvDetailList;
        //            return;
        //        }

        //        foreach (InvoiceItem item in _InvDetailList)
        //        {
        //            if (item.Sad_itm_cd == _item && item.Sad_itm_line == _line)
        //            {
        //                if (item.Sad_srn_qty < _EditQty)
        //                {
        //                    MessageBox.Show("Cannot exceed invoice qty.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    dgvInvItem.AutoGenerateColumns = false;
        //                    dgvInvItem.DataSource = new List<InvoiceItem>();
        //                    dgvInvItem.DataSource = _InvDetailList;
        //                    return;
        //                }

        //                item.Sad_srn_qty = Convert.ToDecimal(_EditQty);
        //                _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
        //                _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
        //                _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
        //                _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));

        //                item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
        //                item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
        //                item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
        //                item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
        //            }
        //            _InvList.Add(item);
        //        }

        //        _InvDetailList = _InvList;
        //        dgvInvItem.AutoGenerateColumns = false;
        //        dgvInvItem.DataSource = new List<InvoiceItem>();
        //        dgvInvItem.DataSource = _InvDetailList;
        //    }
        //}


    }


}
