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

namespace FF.WindowsERPClient.Sales
{
    public partial class SalesReversal : Base
    {
        private RequestApprovalHeader _refAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _refAppDet = new List<RequestApprovalDetail>();
        private MasterAutoNumber _refAppAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _refAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _refAppDetLog = new List<RequestApprovalDetailLog>();

        private RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqAppDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqAppSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqAppDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

        private RequestApprovalHeader _ReqRegHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqRegDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqRegSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqRegAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqRegHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqRegDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqRegSerLog = new List<RequestApprovalSerialsLog>();


        private RequestApprovalHeader _ReqInsHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqInsDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqInsSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqInsHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqInsDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqInsSerLog = new List<RequestApprovalSerialsLog>();

        private List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        private List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
        private List<ReptPickSerialsSub> _doitemSubSerials = new List<ReptPickSerialsSub>();
        private List<VehicalRegistration> _regDetails = new List<VehicalRegistration>();
        private List<RecieptHeader> _regRecList = new List<RecieptHeader>();
        private List<RecieptHeader> _insRecList = new List<RecieptHeader>();
        private List<VehicleInsuarance> _insDetails = new List<VehicleInsuarance>();
        private List<RequestApprovalSerials> _repSer = new List<RequestApprovalSerials>();
        private List<RequestApprovalDetail> _repItem = new List<RequestApprovalDetail>();
        private List<RequestAppAddDet> _repAddDet = new List<RequestAppAddDet>();
        private MasterBusinessEntity _masterBusinessCompany = null;
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
        private string _status = "P";


        public SalesReversal()
        {
            InitializeComponent();
        }

        private void SalesReversal_Load(object sender, EventArgs e)
        {
            Clear_Data();
            LoadAppLevelStatus();
            //kapila 29/1/2016
            if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "HUG" || BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
            {
                toolStripButton1.Enabled = false;
                btnRev.Enabled = true;
                txtSubType.Text = "REF";
                txtSubType_Leave(null, null);
            }
            else
                //kapila 6/4/2017 - solution for skechers reversals
                if (BaseCls.GlbUserDefLoca == "MSR01" || BaseCls.GlbUserDefLoca == "AA01B" || BaseCls.GlbUserDefLoca == "PTS003" || BaseCls.GlbUserDefLoca == "AAS01" || BaseCls.GlbUserDefLoca == "PT003A" || BaseCls.GlbUserDefLoca == "SCCB2" || BaseCls.GlbUserDefLoca == "SCLGL" || BaseCls.GlbUserDefLoca == "SCLPW" || BaseCls.GlbUserDefLoca == "SCLNW") 
                {
                    toolStripButton1.Enabled = true;
                    btnRev.Enabled = true;
                    txtSubType.Text = "REF";
                    txtSubType_Leave(null, null);
                }
                else
                {
                    toolStripButton1.Enabled = true;
                    btnRev.Enabled = false;
                }

        }

        private void LoadAppLevelStatus()
        {
            DataTable dt = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr1 = dt.Rows[i];
                if (dr1["MIC_CD"].ToString() == "CONS")
                    dr1.Delete();
            }
            col_appstatus.DataSource = dt;
            col_appstatus.DisplayMember = "MIS_DESC";
            col_appstatus.ValueMember = "MIC_CD";

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

                case CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegistrationDet:
                    {
                        //paramsText.Append(txtInvoice.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + txtRevRegItem.Text.Trim() + seperator);
                        paramsText.Append(txtInvoice.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + lblSalePc.Text.Trim() + seperator + txtRevRegItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuaranceDet:
                    {
                        //paramsText.Append(txtInvoice.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + txtRevInsItem.Text.Trim() + seperator);
                        paramsText.Append(txtInvoice.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + lblSalePc.Text.Trim() + seperator + txtRevInsItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("SRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "INV" + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchReversal:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeJob:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(txtCusCode.Text.Trim() + seperator + Convert.ToDateTime(DateTime.Now).Date.ToString("d") + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion



        private void txtReqLoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtReqLoc;
                    _CommonSearch.ShowDialog();
                    txtReqLoc.Select();

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnRefresh.Focus();
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

        private void txtReqLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReqLoc.Text))
                {
                    Int32 _isValidPC = 0;

                    _isValidPC = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtReqLoc.Text.Trim());

                    if (_isValidPC == 0)
                    {
                        MessageBox.Show("Invalid or accsess denied location.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReqLoc.Text = "";
                        txtReqLoc.Focus();
                    }
                    else
                    {

                    }
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

                    //if ((cmbRevType.Text == "Forward Sale") || (cmbRevType.Text == "Deliverd Sale"))
                    //{

                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    if (chkOthSales.Checked == false)
                    {
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());
                    }
                    else
                    {
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, null, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());
                    }

                    if (_invHdr.Count == 0)
                    {
                        MessageBox.Show("Invalid invoice number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        _dCusCode = "";
                        _dCusAdd1 = "";
                        _dCusAdd2 = "";
                        _currency = "";
                        _exRate = 0;
                        _invTP = "";
                        lblSalePc.Text = "";
                        _executiveCD = "";
                        _manCode = "";
                        _isTax = false;
                        txtInvoice.Focus();
                        return;
                    }
                    else
                    {
                        foreach (InvoiceHeader _tempInv in _invHdr)
                        {
                            if (_tempInv.Sah_inv_tp == "HS")
                            {
                                MessageBox.Show("Invalid invoice.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtInvoice.Text = "";
                                return;
                            }

                            if (_tempInv.Sah_stus != "R")
                            {
                                txtCusCode.Text = _tempInv.Sah_cus_cd;

                                lblInvCusName.Text = _tempInv.Sah_cus_name;
                                lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                                lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                                lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
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
                                chkOthSales.Enabled = false;
                                Load_InvoiceDetails(BaseCls.GlbUserDefProf);
                                Boolean status = true;
                                load_cust_dt(txtCusCode.Text, txtInvoice.Text, out status);//add by tharanga 2018/10/10
                                if (status == false)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                //MessageBox.Show(_tempInv.Sah_inv_sub_tp + " type not allow to reversal.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show("Invoice is already reversed.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtInvoice.Text = "";
                                return;
                            }
                        }
                    }


                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please select type of reversal.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtInvoice.Text = "";
                    //    cmbRevType.Focus();
                    //    return;
                    //}
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
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                            DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                            _CommonSearch.dvResult.DataSource = _result;
                            _CommonSearch.BindUCtrlDDLData(_result);
                            _CommonSearch.obj_TragetTextBox = txtInvoice;
                            _CommonSearch.IsSearchEnter = true;
                            _CommonSearch.ShowDialog();
                            txtInvoice.Select();
                        }
                        else
                        {
                            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                            _CommonSearch.ReturnIndex = 0;
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                            DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(_CommonSearch.SearchParams, null, null);
                            _CommonSearch.dvResult.DataSource = _result;
                            _CommonSearch.BindUCtrlDDLData(_result);
                            _CommonSearch.obj_TragetTextBox = txtInvoice;
                            _CommonSearch.IsSearchEnter = true;
                            _CommonSearch.ShowDialog();
                            txtInvoice.Select();
                        }
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtInvoice;
                        _CommonSearch.ShowDialog();
                        txtInvoice.Select();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSubType.Focus();
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

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRequest.Focus();
            }
        }


        private void btnReqClear_Click(object sender, EventArgs e)
        {
            try
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
                txtInvoice.Enabled = true;
                txtCusCode.Enabled = true;
                btnRequest.Enabled = true;
                _isFromReq = false;
                lblReturnLoc.Text = "";
                lblStatus.Text = "";
                lblReq.Text = "";
                txtReqLoc.Text = "";
                lblReqPC.Text = "";
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";
                lblSalePc.Text = "";
                txtRevEngine.Enabled = false;
                btnRevRegAdd.Enabled = false;
                btnGetRegAll.Enabled = false;
                txtRevRegItem.Enabled = false;
                lblRegReq.Text = "";
                lblRegStatus.Text = "";
                chkRevReg.Checked = false;
                chkRevReg.Enabled = true;
                chkOthSales.Enabled = true;
                chkOthSales.Checked = false;

                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";
                txtRevInsEngine.Enabled = false;
                btnRevInsAdd.Enabled = false;
                btnGetInsAll.Enabled = false;
                txtRevInsItem.Enabled = false;
                lblInsReq.Text = "";
                lblInsStatus.Text = "";
                chkRevIns.Checked = false;
                chkRevIns.Enabled = true;

                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.Columns["col_invRevQty"].ReadOnly = false;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();


                dgvRegDetails.AutoGenerateColumns = false;
                dgvRegDetails.DataSource = new List<VehicalRegistration>();

                dgvInsDetails.AutoGenerateColumns = false;
                dgvInsDetails.DataSource = new List<VehicleInsuarance>();

                txtCusCode.Focus();
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
                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                _doitemserials = new List<ReptPickSerials>();
                _regDetails = new List<VehicalRegistration>();

                //if (cmbRevType.Text == "Deliverd Sale")
                //{
                //    _type = "DELIVERD";
                //}
                //else
                //{
                //    _type = "FORWARD";
                //}
                _type = null;

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

                        if (_isFromReq == false)
                        {
                            _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForReversal(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
                            _doitemserials.AddRange(_tempDOSerials);
                        }


                        //_tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, _pc, txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                        //_regDetails.AddRange(_tempReg);

                    }

                    if (_isFromReq == true)
                    {
                        _tempDOSerials = CHNLSVC.Inventory.GetRevReqSerial(BaseCls.GlbUserComCode, lblReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text.Trim(), lblReq.Text.Trim());
                        _doitemserials.AddRange(_tempDOSerials);

                    }

                }
                else
                {
                    MessageBox.Show("Details cannot found for " + _type + " Sales.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRequest.Enabled = false;
                }

                _InvDetailList = _InvList;

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;

                //dgvRegDetails.AutoGenerateColumns = false;
                //dgvRegDetails.DataSource = new List<VehicalRegistration>();
                //dgvRegDetails.DataSource = _regDetails;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show("Fail to retrieve data due to records mismatch.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _ReqAppSer = new List<RequestApprovalSerials>();
                _ReqAppAuto = new MasterAutoNumber();

                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();
                _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

                _refAppHdr = new RequestApprovalHeader();
                _refAppDet = new List<RequestApprovalDetail>();
                _refAppAuto = new MasterAutoNumber();

                _refAppHdrLog = new RequestApprovalHeaderLog();
                _refAppDetLog = new List<RequestApprovalDetailLog>();

                _ReqRegHdr = new RequestApprovalHeader();
                _ReqRegDet = new List<RequestApprovalDetail>();
                _ReqRegSer = new List<RequestApprovalSerials>();
                _ReqRegAuto = new MasterAutoNumber();

                _ReqRegHdrLog = new RequestApprovalHeaderLog();
                _ReqRegDetLog = new List<RequestApprovalDetailLog>();
                _ReqRegSerLog = new List<RequestApprovalSerialsLog>();

                _ReqInsHdr = new RequestApprovalHeader();
                _ReqInsDet = new List<RequestApprovalDetail>();
                _ReqInsSer = new List<RequestApprovalSerials>();
                _ReqInsAuto = new MasterAutoNumber();

                _ReqInsHdrLog = new RequestApprovalHeaderLog();
                _ReqInsDetLog = new List<RequestApprovalDetailLog>();
                _ReqInsSerLog = new List<RequestApprovalSerialsLog>();

                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();
                _regDetails = new List<VehicalRegistration>();
                _regRecList = new List<RecieptHeader>();
                _insRecList = new List<RecieptHeader>();
                _insDetails = new List<VehicleInsuarance>();
                _repSer = new List<RequestApprovalSerials>();
                _repItem = new List<RequestApprovalDetail>();
                _repAddDet = new List<RequestAppAddDet>();

                btnCancel.Enabled = true;
                _isFromReq = false;
                _isAppUser = false;
                _appLvl = 0;
                txtCusCode.Text = "";
                txtInvoice.Text = "";
                lblInvDate.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                txtRemarks.Text = "";
                txtManRef.Text = "";
                txtSRNremarks.Text = "";
                lblBackDateInfor.Text = "";
                lblSalePc.Text = "";
                dtpSRNDate.Value = Convert.ToDateTime(DateTime.Now).Date;
                dtpSRNDate.Enabled = false;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpSRNDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                chkApp.Checked = false;
                chkApp.Enabled = false;
                chkOthSales.Enabled = true;
                chkOthSales.Checked = false;
                lblReq.Text = "";
                lblStatus.Text = "";
                txtCusCode.Enabled = true;
                txtInvoice.Enabled = true;
                btnRequest.Enabled = true;
                txtReqLoc.Text = "";
                lblReturnLoc.Text = "";
                lblReqPC.Text = "";
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";
                lblTotInvAmt.Text = "";
                lblTotPayAmt.Text = "";
                lblOutAmt.Text = "";
                lblTotRetAmt.Text = "";
                lblCrAmt.Text = "";
                txtRevEngine.Enabled = false;
                btnRevRegAdd.Enabled = false;
                btnGetRegAll.Enabled = false;
                txtRevRegItem.Enabled = false;
                lblRegReq.Text = "";
                lblRegStatus.Text = "";
                chkRevReg.Checked = false;
                chkRevReg.Enabled = true;

                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";
                txtRevInsEngine.Enabled = false;
                btnRevInsAdd.Enabled = false;
                btnGetInsAll.Enabled = false;
                txtRevInsItem.Enabled = false;
                lblInsReq.Text = "";
                lblInsStatus.Text = "";
                chkRevIns.Checked = false;
                chkRevIns.Enabled = true;
                txtSubType.Text = "";
                lblSubDesc.Text = "";
                txtActLoc.Text = "";
                txtActLoc.Enabled = false;
                btnSearchActLoc.Enabled = false;
                txtItem.Text = "";
                lblInvLine.Text = "";
                txtNewSch.Text = "";
                lblRepPrice.Text = "";
                txtNewSerial.Text = "";
                txtNewPrice.Text = "";
                txtRQty.Text = "";
                _manCode = "";
                _executiveCD = "";

                dgvPaymentDetails.Rows.Clear();

                dgvInvItem.Columns["col_invRevQty"].ReadOnly = false;

                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT014", BaseCls.GlbUserID);
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
                    txtReqLoc.Enabled = true;
                }
                else
                {
                    btnApp.Enabled = false;
                    btnRej.Enabled = false;
                    txtReqLoc.Enabled = false;
                }

                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT014", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT014", BaseCls.GlbUserID, txtReqLoc.Text);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();
                dgvPendings.DataSource = _TempReqAppHdr;

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();

                dgvRegDetails.AutoGenerateColumns = false;
                dgvRegDetails.DataSource = new List<VehicalRegistration>();

                dgvInsDetails.AutoGenerateColumns = false;
                dgvInsDetails.DataSource = new List<VehicleInsuarance>();

                String _tempDefBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (!string.IsNullOrEmpty(_tempDefBin))
                {
                    _defBin = _tempDefBin;
                }
                else
                {
                    _defBin = "";
                }

                txtCusCode.Focus();
                lblSerItem.Text = "";
                lblSerial.Text = "";
                lblWarranty.Text = "";
                lblSerRem.Text = "";
                txtJobNo.Text = "";

                dgvSerApp.AutoGenerateColumns = false;
                dgvSerApp.DataSource = new List<RequestApprovalSerials>();

                dgvRereportItems.AutoGenerateColumns = false;
                dgvRereportItems.DataSource = new List<RequestAppAddDet>();

                pnlSerApp.Visible = false;
                pnlCancel.Visible = false;
                pnlReRep.Visible = false;

                SystemAppLevelParam _aodApp = new SystemAppLevelParam();

                _aodApp = CHNLSVC.Sales.CheckApprovePermission("ARQT038", BaseCls.GlbUserID);

                if (_aodApp.Sarp_cd != null)
                {
                    txtActLoc.Enabled = true;
                    btnSearchActLoc.Enabled = true;
                    txtActLoc.Text = "";
                }
                else
                {
                    txtActLoc.Text = "";
                    txtActLoc.Enabled = false;
                    btnSearchActLoc.Enabled = false;
                }

                tbMain.Enabled = true;
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

        private void btnRequest_Click(object sender, EventArgs e)
        {

            try
            {
                string _docNo = "";
                string _regAppNo = "";
                string _insAppNo = "";
                string _msg = string.Empty;

                if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "HUG" || BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
                {
                    MessageBox.Show("This option is disabled", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Invoice customer is missing.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    MessageBox.Show("Please select invoice number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    MessageBox.Show("Please enter remarks.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRemarks.Focus();
                    return;
                }

                if (dgvInvItem.Rows.Count <= 0)
                {
                    MessageBox.Show("No items are selected to generate request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    MessageBox.Show("Please select return category.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblSalePc.Text))
                {
                    MessageBox.Show("Original sales profit center is missing.Please re-enter invoice #.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkRevReg.Checked == true)
                {
                    if (dgvRegDetails.Rows.Count <= 0)
                    {
                        MessageBox.Show("No registration details are not found to generate registration refund request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Focus();
                        return;
                    }
                }

                if (chkRevIns.Checked == true)
                {
                    if (dgvInsDetails.Rows.Count <= 0)
                    {
                        MessageBox.Show("No insuarance details are not found to generate insuarance refund request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Focus();
                        return;
                    }
                }

                //check pending requests for invoice
                List<RequestApprovalHeader> _pendingRequest = CHNLSVC.General.GetPendingSRNRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text, "ARQT014");
                if (_pendingRequest != null && _pendingRequest.Count > 0)
                {
                    List<RequestApprovalHeader> _app = (from _res in _pendingRequest
                                                        where _res.Grah_app_stus == "A"
                                                        select _res).ToList<RequestApprovalHeader>();
                    if (_app != null && _app.Count > 0)
                    {
                        MessageBox.Show("This invoice has approved Reuqest.Pleses Finish approve request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    List<RequestApprovalHeader> _pen = (from _res in _pendingRequest
                                                        where _res.Grah_app_stus == "P"
                                                        select _res).ToList<RequestApprovalHeader>();
                    if (_pen != null && _pen.Count > 0)
                    {
                        MessageBox.Show("This invoice has pending request, Please approve pending request", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }
                }


                decimal rtnQty = 0;
                decimal regQty = 0;

                if (_regDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        regQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicalRegistration tempReg in _regDetails)
                        {
                            if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                            {
                                regQty = regQty + 1;
                            }
                        }

                        if (rtnQty < regQty)
                        {
                            MessageBox.Show("You are going to reverse registration details more than return qty.", "Cash Sale Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                decimal insQty = 0;

                if (_insDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        insQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicleInsuarance tempIns in _insDetails)
                        {
                            if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                            {
                                insQty = insQty + 1;
                            }
                        }

                        if (rtnQty < insQty)
                        {
                            MessageBox.Show("You are going to reverse insuarance details more than return qty.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                _status = "P";

                DataTable _appSt = CHNLSVC.Sales.checkAppStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "P", this.GlbModuleName);
                if (_appSt.Rows.Count > 0)
                {
                    if (lblSalePc.Text.Trim() != BaseCls.GlbUserDefProf)
                    {
                        if (MessageBox.Show("You are going to reverse other profit center sales and need to get approval. Do you want to process request ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {
                            _status = "P";
                        }
                        else
                        {
                            _status = "P";
                            return;
                        }
                    }
                    else
                    {
                        _status = "A";
                    }
                }
                else
                {
                    if (lblSalePc.Text.Trim() == BaseCls.GlbUserDefProf)
                    {
                        if (Convert.ToDateTime(lblInvDate.Text).Date == Convert.ToDateTime(DateTime.Now).Date)
                        {
                            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10057))
                            {
                                _status = "A";
                            }
                            else
                            {
                                _status = "P";
                            }
                        }
                        else
                        {
                            _status = "P";
                        }
                    }
                    else
                    {
                        _status = "P";
                    }
                }

                if (_status == "P")
                {
                    if (txtSubType.Text != "REF")
                    {
                        if (_repAddDet.Count <= 0)
                        {
                            MessageBox.Show("Please enter re-report item details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                CollectReqApp();
                CollectReqAppLog();

                if (chkRevReg.Checked == true)
                {
                    CollectRegApp();
                    CollectRegAppLog();
                }

                if (chkRevIns.Checked == true)
                {
                    CollectInsApp();
                    CollectInsAppLog();
                }


                var _lst = (from n in _ReqAppDet
                            group n by new { n.Grad_anal2 } into r
                            select new { Grad_anal2 = r.Key.Grad_anal2, grad_val3 = r.Sum(p => p.Grad_val3) }).ToList();
                foreach (var s in _lst)
                {
                    string _item = s.Grad_anal2;
                    decimal _qty = s.grad_val3;

                    MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                    decimal _count = _ReqAppSer.Where(X => X.Gras_anal2 == _item).Count();
                    if (_itm != null)
                    {
                        if (_itm.Mi_cd != null)
                        {
                            DataTable _type = new DataTable();
                            _type = CHNLSVC.Sales.GetItemTp(_itm.Mi_itm_tp);

                            if (_type.Rows.Count > 0)
                            {
                                if (Convert.ToInt16(_type.Rows[0]["mstp_is_inv"]) == 1)
                                {
                                    if (_qty != _count)
                                    {
                                        //MessageBox.Show("Deliverd qty and serial mismatch. DO Qty : " + _qty + " Serials :" + _count + " for the item : " + _item, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //return;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Item Details not found : Item code " + _item , "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                  

                }


                List<InvoiceItem> _temp = new List<InvoiceItem>();
                InvoiceItem _orgInvDet = new InvoiceItem();
                _temp = _InvDetailList;
                string _revItem = "";
                string _delItem = "";
                Int32 _line = 0;
                decimal _rtnQty = 0;
                decimal _invQty = 0;
                decimal _doQty = 0;
                decimal _curRtnQty = 0;

                foreach (InvoiceItem itm in _temp)
                {
                    if (!string.IsNullOrEmpty(itm.Sad_sim_itm_cd))
                    {
                        _delItem = itm.Sad_sim_itm_cd;
                    }
                    else
                    {
                        _delItem = itm.Sad_itm_cd;
                    }
                    _line = itm.Sad_itm_line;
                    _rtnQty = itm.Sad_srn_qty;
                    _revItem = itm.Sad_itm_cd;

                    _orgInvDet = CHNLSVC.Sales.GetInvDetByLine(itm.Sad_inv_no, _revItem, _line);

                    if (_orgInvDet.Sad_inv_no == null)
                    {
                        MessageBox.Show("Cannot load item details in invoice. " + _revItem, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    _invQty = _orgInvDet.Sad_qty;
                    _doQty = _orgInvDet.Sad_do_qty;
                    _curRtnQty = _orgInvDet.Sad_srn_qty;

                    if (_invQty < _rtnQty)
                    {
                        MessageBox.Show("You are going to revers more then invoice qty.Item : " + _revItem + "Inv. Qty : " + _invQty + "Rtn. Qty : " + _rtnQty, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if ((_invQty - _curRtnQty) < _rtnQty)
                    {
                        MessageBox.Show("You are going to revers more then current available qty.Item : " + _revItem + "Inv. Qty : " + _invQty + "Current Rtn. Qty : " + _rtnQty + "Already Rtn. Qty : " + _curRtnQty, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    MasterItem _itmDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _revItem);
                    if (_itmDet.Mi_is_ser1 != -1)
                    {
                    DataTable _rtnItmtype = new DataTable();
                    _rtnItmtype = CHNLSVC.Sales.GetItemTp(_itmDet.Mi_itm_tp);

                    if (_rtnItmtype.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(_rtnItmtype.Rows[0]["mstp_is_inv"]) == 1)
                        {
                            decimal _serCount = 0;
                            if (_itmDet.Mi_is_ser1 == -1)
                            {
                                _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Sum(x => x.Gras_anal8);
                            }
                            else { _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Count(); }
                             
                            //decimal _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Count();

                            if (_doQty < _serCount)
                            {
                                MessageBox.Show("You are going to revers more than deliverd qty.Item : " + _revItem + "Del. Qty : " + _doQty + "Serial : " + _serCount, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //imagin previous srns are having
                            if (_rtnQty != _serCount)
                            {
                                if (_invQty - _doQty == 0)
                                {
                                    if (_rtnQty != _serCount)
                                    {
                                        MessageBox.Show("Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    if (_rtnQty < _serCount)
                                    {
                                        MessageBox.Show("Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    }


                    //if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                    //{
                    //    itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
                    //    itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
                    //}

                }

                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, _ReqAppSer, _ReqAppAuto, _ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegAuto, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, chkRevReg.Checked, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsAuto, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, chkRevIns.Checked, out _docNo, out _regAppNo, out _insAppNo, _repAddDet);

                if (effet == 1)
                {
                    if (chkRevReg.Checked == false && chkRevIns.Checked == false)
                    {
                        MessageBox.Show("Request generated." + _docNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (chkRevReg.Checked == true && chkRevIns.Checked == false)
                    {
                        MessageBox.Show("Request generated.Rev. req. # : " + _docNo + " Reg. refund Req. # : " + _regAppNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (chkRevReg.Checked == false && chkRevIns.Checked == true)
                    {
                        MessageBox.Show("Request generated.Rev. req. # : " + _docNo + " Ins. refund Req. # : " + _insAppNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (chkRevReg.Checked == true && chkRevIns.Checked == true)
                    {
                        MessageBox.Show("Request generated.Rev. req. # : " + _docNo + " Ins. refund Req. # : " + _insAppNo + " Reg. refund req. # : " + _regAppNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_docNo))
                    { MessageBox.Show(_docNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                    else
                    { MessageBox.Show("Creation fail.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
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

        protected void CollectInsAppLog()
        {
            string _type = "";
            _ReqInsHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqInsDetLog = new List<RequestApprovalDetailLog>();
            _ReqInsSerLog = new List<RequestApprovalSerialsLog>();


            _ReqInsHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqInsHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqInsHdrLog.Grah_app_tp = "ARQT017";
            _ReqInsHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqInsHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqInsHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqInsHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqInsHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_app_stus = "P";
            _ReqInsHdrLog.Grah_app_lvl = 0;
            _ReqInsHdrLog.Grah_app_by = string.Empty;
            _ReqInsHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_remaks = txtRemarks.Text.Trim();

            decimal _insQty = 0;

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _insQty = 0;
                    foreach (VehicleInsuarance tmpIns in _insDetails)
                    {
                        if (item.Sad_itm_cd == tmpIns.Svit_itm_cd && item.Sad_inv_no == tmpIns.Svit_inv_no)
                        {
                            _insQty = _insQty + 1;
                        }
                    }

                    if (_insQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetailLog();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL INSUARANCE REQUEST";
                        _tempReqAppDet.Grad_val1 = _insQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _insQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqInsDetLog.Add(_tempReqAppDet);
                    }
                }
            }

            if (_insDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicleInsuarance ser in _insDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.Svit_itm_cd && _tmpInvItm.Sad_inv_no == ser.Svit_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:

                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Svit_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.Svit_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Svit_engine;
                    _tempReqAppSer.Gras_anal4 = ser.Svit_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.Svit_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.Svit_ins_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqInsSerLog.Add(_tempReqAppSer);
                }
            }


        }


        protected void CollectInsApp()
        {
            string _type = "";
            _ReqInsHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqInsDet = new List<RequestApprovalDetail>();
            _ReqInsSer = new List<RequestApprovalSerials>();
            _ReqInsAuto = new MasterAutoNumber();

            _ReqInsHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqInsHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqInsHdr.Grah_app_tp = "ARQT017";
            _ReqInsHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqInsHdr.Grah_ref = null;
            _ReqInsHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqInsHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqInsHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqInsHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdr.Grah_app_stus = "P";
            _ReqInsHdr.Grah_app_lvl = 0;
            _ReqInsHdr.Grah_app_by = string.Empty;
            _ReqInsHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdr.Grah_remaks = txtRemarks.Text.Trim();

            decimal _insQty = 0;
            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _insQty = 0;
                    foreach (VehicleInsuarance tmpIns in _insDetails)
                    {
                        if (item.Sad_itm_cd == tmpIns.Svit_itm_cd && item.Sad_inv_no == tmpIns.Svit_inv_no)
                        {
                            _insQty = _insQty + 1;
                        }
                    }

                    if (_insQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetail();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL INSUARANCE REQUEST";
                        _tempReqAppDet.Grad_val1 = _insQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _insQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqInsDet.Add(_tempReqAppDet);
                    }
                }
            }

            if (_insDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicleInsuarance ser in _insDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.Svit_itm_cd && _tmpInvItm.Sad_inv_no == ser.Svit_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Svit_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.Svit_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Svit_engine;
                    _tempReqAppSer.Gras_anal4 = ser.Svit_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.Svit_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.Svit_ins_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqInsSer.Add(_tempReqAppSer);
                }
            }


            _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqInsAuto.Aut_cate_tp = "PC";
            _ReqInsAuto.Aut_direction = 1;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "REQ";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "CSINSRF";
            _ReqInsAuto.Aut_year = null;
        }

        protected void CollectRegApp()
        {
            string _type = "";
            _ReqRegHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqRegDet = new List<RequestApprovalDetail>();
            _ReqRegSer = new List<RequestApprovalSerials>();
            _ReqRegAuto = new MasterAutoNumber();

            _ReqRegHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqRegHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqRegHdr.Grah_app_tp = "ARQT016";
            _ReqRegHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqRegHdr.Grah_ref = null;
            _ReqRegHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqRegHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqRegHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqRegHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdr.Grah_app_stus = "P";
            _ReqRegHdr.Grah_app_lvl = 0;
            _ReqRegHdr.Grah_app_by = string.Empty;
            _ReqRegHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdr.Grah_remaks = txtRemarks.Text.Trim();

            decimal _regQty = 0;
            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _regQty = 0;
                    foreach (VehicalRegistration tmpReg in _regDetails)
                    {
                        if (item.Sad_itm_cd == tmpReg.P_srvt_itm_cd && item.Sad_inv_no == tmpReg.P_svrt_inv_no)
                        {
                            _regQty = _regQty + 1;
                        }
                    }

                    if (_regQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetail();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL REGISTRATION REQUEST";
                        _tempReqAppDet.Grad_val1 = _regQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _regQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqRegDet.Add(_tempReqAppDet);
                    }
                }
            }

            if (_regDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicalRegistration ser in _regDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.P_srvt_itm_cd && _tmpInvItm.Sad_inv_no == ser.P_svrt_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.P_svrt_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.P_srvt_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.P_svrt_engine;
                    _tempReqAppSer.Gras_anal4 = ser.P_svrt_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.P_srvt_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.P_svrt_reg_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqRegSer.Add(_tempReqAppSer);
                }
            }


            _ReqRegAuto = new MasterAutoNumber();
            _ReqRegAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqRegAuto.Aut_cate_tp = "PC";
            _ReqRegAuto.Aut_direction = 1;
            _ReqRegAuto.Aut_modify_dt = null;
            _ReqRegAuto.Aut_moduleid = "REQ";
            _ReqRegAuto.Aut_number = 0;
            _ReqRegAuto.Aut_start_char = "CSREGRF";
            _ReqRegAuto.Aut_year = null;
        }


        protected void CollectRegAppLog()
        {
            string _type = "";
            _ReqRegHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqRegDetLog = new List<RequestApprovalDetailLog>();
            _ReqRegSerLog = new List<RequestApprovalSerialsLog>();


            _ReqRegHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqRegHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqRegHdrLog.Grah_app_tp = "ARQT016";
            _ReqRegHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqRegHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqRegHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqRegHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqRegHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdrLog.Grah_app_stus = "P";
            _ReqRegHdrLog.Grah_app_lvl = 0;
            _ReqRegHdrLog.Grah_app_by = string.Empty;
            _ReqRegHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdrLog.Grah_remaks = txtRemarks.Text.Trim();

            decimal _regQty = 0;

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _regQty = 0;
                    foreach (VehicalRegistration tmpReg in _regDetails)
                    {
                        if (item.Sad_itm_cd == tmpReg.P_srvt_itm_cd && item.Sad_inv_no == tmpReg.P_svrt_inv_no)
                        {
                            _regQty = _regQty + 1;
                        }
                    }

                    if (_regQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetailLog();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL REGISTRATION REQUEST";
                        _tempReqAppDet.Grad_val1 = _regQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _regQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqRegDetLog.Add(_tempReqAppDet);
                    }
                }
            }

            if (_regDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicalRegistration ser in _regDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.P_srvt_itm_cd && _tmpInvItm.Sad_inv_no == ser.P_svrt_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:

                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.P_svrt_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.P_srvt_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.P_svrt_engine;
                    _tempReqAppSer.Gras_anal4 = ser.P_svrt_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.P_srvt_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.P_svrt_reg_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqRegSerLog.Add(_tempReqAppSer);
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


            _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = "ARQT014";
            _ReqAppHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdr.Grah_ref = null;
            _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = _status;
            //kapila 18/4/2017
            if (_status == "F")
                _ReqAppHdr.Grah_is_alw_freitmisu = 1;
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
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "SALES REVERSAL";
                    _tempReqAppDet.Grad_val1 = item.Sad_srn_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = item.Sad_fws_ignore_qty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    _tempReqAppDet.Grad_inv_line = item.Sad_itm_line;
                   
                    //Add Grad_anal15 column by Chamal 17-07-2015
                    if (string.IsNullOrEmpty(item.Sad_sim_itm_cd))
                    { _tempReqAppDet.Grad_anal15 = item.Sad_itm_cd; }
                    else
                    { _tempReqAppDet.Grad_anal15 = item.Sad_sim_itm_cd; }
                    try
                    {
                        List<string> values = dgvDelDetails.Rows.Cast<DataGridViewRow>().Where(row => (string)row.Cells["col_item"].Value == item.Sad_itm_cd).Select(row => (string)row.Cells["col_appstatus"].Value).ToList<string>();
                        _tempReqAppDet.Grad_anal8 = values[0];

                    }
                    catch (Exception) { _tempReqAppDet.Grad_anal8 = item.Mi_itm_stus; }
                    //MasterItem _mstItm = CHNLSVC.Inventory.GetItem("", item.Sad_sim_itm_cd);
                    // if (_mstItm.Mi_is_ser1 == -1)
                    // {
                    //     _tempReqAppDet.Grad_anal8 = _mstItm.Mi_itm_stus;
                    // }
                   
                    _ReqAppDet.Add(_tempReqAppDet);
                }
            }

            if (_doitemserials.Count > 0)
            {
                Int32 _line = 0;
                foreach (ReptPickSerials ser in _doitemserials)
                {
                   List<RequestApprovalDetail> _ReqAppDetNEW = new List<RequestApprovalDetail>();
                  // _ReqAppDetNEW = _ReqAppDet.Where(r => r.Grad_anal2 == ser.Tus_itm_cd).ToList();
                   _ReqAppDetNEW = _ReqAppDet.Where(r => r.Grad_line == ser.Tus_base_itm_line).ToList();
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    //add by tharanga gen_reqapp_ser gras line insert change 2017/11/
                    if (_ReqAppDetNEW.Count > 0)
                    {
                        _tempReqAppSer.Gras_line = Convert.ToInt32(_ReqAppDetNEW.FirstOrDefault().Grad_line.ToString());
                    }
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
                    //kapila 25/9/2015 mod for decimal allow items
                    MasterItem _mstItm = CHNLSVC.Inventory.GetItem("", ser.Tus_itm_cd);
                    if (_mstItm.Mi_is_ser1 == -1)
                    {
                        _tempReqAppSer.Gras_anal8 = ser.Tus_qty;
                        _tempReqAppSer.Gras_anal3 = "0"; //add by tharanga  2017/10/03
                        _tempReqAppSer.Gras_anal4 = "0"; //add by tharanga 2017/10/03
                    }
                    else
                    {
                        foreach (DataGridViewRow dgvr in dgvDelDetails.Rows)
                        {
                            if (ser.Tus_itm_cd == dgvr.Cells["col_item"].Value.ToString() && ser.Tus_ser_id == Convert.ToInt32(dgvr.Cells["col_SerID"].Value.ToString()) && ser.Tus_base_itm_line == Convert.ToInt32(dgvr.Cells["col_BaseLine"].Value.ToString()))
                            {
                                _tempReqAppSer.Gras_anal8 = Convert.ToInt32(dgvr.Cells["col_Qty"].Value.ToString());
                                break;
                            }
                        }
                        //_tempReqAppSer.Gras_anal8 = 1;
                    }
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_inv_line = ser.Tus_base_itm_line;
                    _tempReqAppSer.Gras_itm_line = ser.Tus_itm_line;
                    _tempReqAppSer.Gras_batch_line = ser.Tus_batch_line;
                    _tempReqAppSer.Gras_ser_line = ser.Tus_ser_line;

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
            _ReqAppHdrLog.Grah_app_tp = "ARQT014";
            _ReqAppHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdrLog.Grah_ref = null;
            _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_app_stus = _status;
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
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    //Add Grad_anal15 column by Darshana 20-07-2015
                    if (string.IsNullOrEmpty(item.Sad_sim_itm_cd))
                    { _tempReqAppDet.Grad_anal15 = item.Sad_itm_cd; }
                    else
                    { _tempReqAppDet.Grad_anal15 = item.Sad_sim_itm_cd; }
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


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT014", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT014", BaseCls.GlbUserID, txtReqLoc.Text);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvPendings.DataSource = _TempReqAppHdr;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //        MessageBox.Show("Cannot exceed invoice qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        dgvInvItem.AutoGenerateColumns = false;
        //        dgvInvItem.DataSource = new List<InvoiceItem>();
        //        dgvInvItem.DataSource = _InvDetailList;
        //        return;
        //    }

        //    if (_EditQty <= 0)
        //    {
        //        MessageBox.Show("Invalid return qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //                MessageBox.Show("Cannot exceed invoice qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DateTime _reqDt;
                DateTime _doDt;
                string _doNo = "";
                Boolean _is_SVAT = false;   //kapila 19/4/2017
                decimal _tot_tax_amt = 0;

                txtInvoice.Text = "";
                txtCusCode.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                lblInvDate.Text = "";
                lblSalePc.Text = "";
                txtInvoice.Enabled = true;
                txtCusCode.Enabled = true;
                btnRequest.Enabled = false;

                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();


                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;

                dgvPaymentDetails.Rows.Clear();

                _reqNo = dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value.ToString();
                _stus = dgvPendings.Rows[e.RowIndex].Cells["col_Status"].Value.ToString();
                _invNo = dgvPendings.Rows[e.RowIndex].Cells["col_Inv"].Value.ToString();
                _remarks = dgvPendings.Rows[e.RowIndex].Cells["col_remarks"].Value.ToString();
                _type = dgvPendings.Rows[e.RowIndex].Cells["col_Type"].Value.ToString();
                _pc = dgvPendings.Rows[e.RowIndex].Cells["col_pc"].Value.ToString();
                _retLoc = dgvPendings.Rows[e.RowIndex].Cells["col_Type"].Value.ToString();
                _retSubType = dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value.ToString();
                _salesPC = dgvPendings.Rows[e.RowIndex].Cells["col_OthPC"].Value.ToString();
                _reqDt = Convert.ToDateTime(dgvPendings.Rows[e.RowIndex].Cells["col_reqDate"].Value);

                //check deliveries available after raised request
                DataTable _DOList = new DataTable();
                _DOList = CHNLSVC.Sales.GetDeliveryOrader(_invNo);

                //kapila 15/12/2016
                InvoiceHeader _invH = CHNLSVC.Sales.GetInvoiceHeader(_invNo);
                if (_invH.Sah_inv_tp == "HS")
                {
                    foreach (DataRow drow in _DOList.Rows)
                    {
                        _doDt = Convert.ToDateTime(drow["ith_cre_when"]);
                        _doNo = drow["ith_doc_no"].ToString();

                        if (_reqDt < _doDt)
                        {
                            MessageBox.Show("Deliveries found after request for reversal. DO # " + _doNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

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
                txtSRNremarks.Text = _remarks;
                lblReturnLoc.Text = _type;
                lblReqPC.Text = _pc;
                txtSubType.Text = _retSubType;
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

                //btnRegDetails.Enabled = false;
                if (_stus == "A")
                {
                    lblStatus.Text = "APPROVED";
                    //  btnRegDetails.Enabled =  true;
                }
                else if (_stus == "P")
                {
                    lblStatus.Text = "PENDING";
                }
                else if (_stus == "R")
                {
                    lblStatus.Text = "REJECT";
                }
                else if (_stus == "F")
                {
                    lblStatus.Text = "FINISHED";
                }

                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, _salesPC, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());

                foreach (InvoiceHeader _tempInv in _invHdr)
                {
                    if (_tempInv.Sah_inv_no == null)
                    {
                        MessageBox.Show("Error loading request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        lblSalePc.Text = "";
                        lblTotInvAmt.Text = "";
                        lblTotPayAmt.Text = "";
                        lblOutAmt.Text = "";
                        lblTotRetAmt.Text = "";
                        lblTotalRevAmt.Text = "";

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
                        return;
                    }
                    else
                    {
                        txtCusCode.Text = _tempInv.Sah_cus_cd;
                        lblInvCusName.Text = _tempInv.Sah_cus_name;
                        lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                        lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                        lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                        lblSalePc.Text = _tempInv.Sah_pc;
                        lblTotInvAmt.Text = _tempInv.Sah_anal_7.ToString("n");
                        lblTotPayAmt.Text = _tempInv.Sah_anal_8.ToString("n");
                        lblOutAmt.Text = (_tempInv.Sah_anal_7 - _tempInv.Sah_anal_8).ToString("n");

                        _dCusCode = _tempInv.Sah_d_cust_cd;
                        _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                        _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                        _currency = _tempInv.Sah_currency;
                        _exRate = _tempInv.Sah_ex_rt;
                        _invTP = _tempInv.Sah_inv_tp;
                        _executiveCD = _tempInv.Sah_sales_ex_cd;
                        _manCode = _tempInv.Sah_man_cd;
                        _isTax = _tempInv.Sah_tax_inv;
                        _is_SVAT = _tempInv.Sah_is_svat;

                        if (lblStatus.Text == "FINISHED")
                        {
                            MessageBox.Show("Selected request is in FINISHED status.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnApp.Enabled = false;
                            btnCancel.Enabled = false;
                            return;
                        }
                        else if (_tempInv.Sah_stus == "C")
                        {
                            MessageBox.Show("Selected invoice is cancelled.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnApp.Enabled = false;
                            btnCancel.Enabled = false;
                            return;
                        }
                        else if (_tempInv.Sah_stus == "R")
                        {
                            MessageBox.Show("This invoice is already reversed.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnApp.Enabled = false;
                            btnCancel.Enabled = false;
                            return;
                        }
                        else if (lblStatus.Text == "APPROVED")
                        {
                            btnCancel.Enabled = true;
                        }
                        _isFromReq = true;
                        chkRevReg.Checked = false;
                        chkRevReg.Enabled = false;
                        chkRevIns.Checked = false;
                        chkRevIns.Enabled = false;
                        Load_InvoiceDetails(_pc);
                        Load_Registration_Details(BaseCls.GlbUserComCode, _pc, "ARQT016", _reqNo);
                        Load_Insuarance_Details(BaseCls.GlbUserComCode, _pc, "ARQT017", _reqNo);

                        txtCusCode.Enabled = false;
                        txtInvoice.Enabled = false;
                        btnRequest.Enabled = false;
                        dgvInvItem.Columns["col_invRevQty"].ReadOnly = true;



                        List<InvoiceItem> _tmpInv = new List<InvoiceItem>();
                        List<InvoiceItem> _newList = new List<InvoiceItem>();
                        List<ReptPickSerials> _tempser = new List<ReptPickSerials>();

                        decimal _rtnSerQty = 0;
                        decimal _fwsQty = 0;

                        _tmpInv = _InvDetailList;
                        _InvDetailList = null;


                        foreach (InvoiceItem itm in _tmpInv)
                        {
                            _rtnSerQty = 0;
                            _fwsQty = 0;
                            foreach (ReptPickSerials _tmpser in _doitemserials)
                            {

                                string _item = _tmpser.Tus_itm_cd;
                                Int32 _line = _tmpser.Tus_base_itm_line;

                                if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                                {
                                    _rtnSerQty = _rtnSerQty + 1;
                                    //itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
                                    //itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
                                }

                            }

                            _fwsQty = itm.Sad_srn_qty - _rtnSerQty;
                            itm.Sad_fws_ignore_qty = _fwsQty;
                            _newList.Add(itm);

                        }

                        _InvDetailList = _newList;

                        dgvInvItem.AutoGenerateColumns = false;
                        dgvInvItem.DataSource = new List<InvoiceItem>();
                        dgvInvItem.DataSource = _InvDetailList;


                        decimal _totRetAmt = 0;
                        decimal _crAmt = 0;
                        decimal _outAmt = 0;
                        decimal _preRevAmt = 0;
                        decimal _preCrAmt = 0;
                        decimal _balCrAmt = 0;
                        decimal _newOut = 0;


                        DataTable _revAmt = CHNLSVC.Sales.GetTotalRevAmtByInv(txtInvoice.Text.Trim());
                        if (_revAmt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_revAmt.Rows[0]["tot_rtn"].ToString()))
                            {
                                _preRevAmt = Convert.ToDecimal(_revAmt.Rows[0]["tot_rtn"]);
                            }
                        }

                        DataTable _preCr = CHNLSVC.Sales.GetTotalCRAmtByInv(txtInvoice.Text.Trim());
                        if (_preCr.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_preCr.Rows[0]["tot_Cr"].ToString()))
                            {
                                _preCrAmt = Convert.ToDecimal(_preCr.Rows[0]["tot_Cr"]);
                            }
                        }


                        foreach (InvoiceItem _temp in _InvDetailList)
                        {
                            _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
                        }

                        lblTotalRevAmt.Text = _preRevAmt.ToString("n");
                        lblTotRetAmt.Text = _totRetAmt.ToString("n");

                        _outAmt = Convert.ToDecimal(lblOutAmt.Text);
                        _outAmt = Convert.ToDecimal(lblTotInvAmt.Text) - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

                        _balCrAmt = Convert.ToDecimal(lblTotPayAmt.Text) - _preCrAmt;

                        if (_balCrAmt < 0)
                        {
                            _balCrAmt = 0;
                        }

                        if (_outAmt > 0)
                        {
                            _crAmt = _balCrAmt - _outAmt;
                        }
                        else
                        {
                            if (_totRetAmt <= _balCrAmt)
                            {
                                _crAmt = _totRetAmt;
                            }
                            else
                            {
                                _crAmt = _balCrAmt;
                            }
                        }


                        //_newOut = _outAmt - _totRetAmt;

                        /////////
                        //if (_balCrAmt - _newOut <= 0)
                        //{
                        //    _crAmt = 0;
                        //}
                        //else
                        //{
                        //    if (_balCrAmt - _newOut < _totRetAmt)
                        //    {
                        //        _crAmt = _balCrAmt - _newOut;
                        //    }
                        //    else
                        //    {
                        //        _crAmt = _totRetAmt;
                        //    }
                        //}

                        /////////

                        //if (Convert.ToDecimal(lblTotInvAmt.Text) == Convert.ToDecimal(lblTotPayAmt.Text))
                        //{
                        //    if (_outAmt >= 0)
                        //    {
                        //        _crAmt = _totRetAmt - _outAmt;
                        //    }
                        //    else
                        //    {
                        //        _crAmt = _totRetAmt;
                        //    }
                        //}
                        //else
                        //{
                        //    if (_outAmt >= 0)
                        //    {
                        //        _crAmt = _totRetAmt - _outAmt;
                        //    }
                        //    else
                        //    {
                        //        _crAmt = _totRetAmt;
                        //    }
                        //}

                        //kapila 19/4/2017
                        if (_is_SVAT == true)
                        {
                            List<InvoiceItem> _paramInvItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), null);
                            foreach (InvoiceItem item in _paramInvItems)
                            {
                                _tot_tax_amt = _tot_tax_amt+(Convert.ToDecimal(item.Sad_itm_tax_amt));                         
                            }
                            _crAmt = _crAmt + _tot_tax_amt;
                            lblOutAmt.Text = (Convert.ToDecimal(lblOutAmt.Text) - _tot_tax_amt).ToString("0.00");
                            lblTotPayAmt.Text = (Convert.ToDecimal(lblTotPayAmt.Text) + _tot_tax_amt).ToString("0.00");

                        }

                        if (_crAmt > 0)
                        {
                            lblCrAmt.Text = _crAmt.ToString("n");
                        }
                        else
                        {
                            lblCrAmt.Text = "0";
                        }

                        DataTable _newRep = new DataTable();
                        //_newRep = CHNLSVC.General.SearchrequestAppDetByRef(_reqNo);
                        _newRep = CHNLSVC.General.SearchrequestAppAddDetByRef(_reqNo);

                        dgvRereportItems.DataSource = _newRep;

                        //Load collection deatails
                        DataTable _collDet = CHNLSVC.Sales.GetInvoiceReceiptDet(txtInvoice.Text.Trim());
                        if (_collDet.Rows.Count > 0)
                        {
                            foreach (DataRow drow in _collDet.Rows)
                            {
                                if (drow["SAR_RECEIPT_TYPE"].ToString() == "DEBT" || drow["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                                {
                                    dgvPaymentDetails.Rows.Add();
                                    dgvPaymentDetails["col_recSeq", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_receipt_no"].ToString();
                                    dgvPaymentDetails["col_recNo", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_anal_3"].ToString();
                                    dgvPaymentDetails["col_recDT", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_receipt_date"].ToString();
                                    dgvPaymentDetails["col_recPayTp", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_pay_tp"].ToString();
                                    dgvPaymentDetails["col_recPayRef", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_ref_no"].ToString();
                                    dgvPaymentDetails["col_recAmt", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_settle_amt"].ToString();
                                }

                            }

                        }


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

        private void Load_Insuarance_Details(string _com, string _pc, string _appCode, string _revReqNo)
        {
            _insDetails = new List<VehicleInsuarance>();
            List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
            VehicleInsuarance _tempAddIns = new VehicleInsuarance();
            Int32 I = 0;

            RequestApprovalHeader _tempInsReq = new RequestApprovalHeader();
            _tempInsReq = CHNLSVC.General.GetRelatedRequestByRef(_com, _pc, _revReqNo, _appCode);

            if (_tempInsReq != null)
            {
                lblInsReq.Text = _tempInsReq.Grah_ref;
                if (_tempInsReq.Grah_app_stus == "A")
                {
                    lblInsStatus.Text = "APPROVED";
                    chkRevIns.Checked = true;
                    chkRevIns.Enabled = false;
                    //  btnRegDetails.Enabled =  true;
                }
                else if (_tempInsReq.Grah_app_stus == "P")
                {
                    MessageBox.Show("Related insuarance refund request is still not approved. You cannot generate insuarance refund at this time. You have to refund it seperatly after getting approval.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInsStatus.Text = "PENDING";
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                }
                else if (_tempInsReq.Grah_app_stus == "R")
                {
                    MessageBox.Show("Related insuarance refund request is rejected. Please contact insuarace dept. for more information.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInsStatus.Text = "REJECT";
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                }
                else if (_tempInsReq.Grah_app_stus == "F")
                {
                    MessageBox.Show("Related insuarance refund is already done.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInsStatus.Text = "FINISHED";
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                }

                //load request registration refund details
                _tempIns = CHNLSVC.Sales.GetRefundReqVehIns(BaseCls.GlbUserComCode, _pc, lblInsReq.Text.Trim(), "ARQT017");

                foreach (VehicleInsuarance insDet in _tempIns)
                {
                    if (insDet.Svit_cvnt_issue == 2)
                    {
                        MessageBox.Show("Request engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevIns.Checked = false;
                        chkRevIns.Enabled = false;
                        // goto skipAdd;
                    }
                    else if (insDet.Svit_polc_stus == true)
                    {
                        MessageBox.Show("Request engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already issue policy.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevIns.Checked = false;
                        chkRevIns.Enabled = false;
                        //goto skipAdd;
                    }

                    _tempAddIns = insDet;
                    _insDetails.Add(_tempAddIns);
                    //skipAdd:
                    I = I + 1;
                }


                dgvInsDetails.AutoGenerateColumns = false;
                dgvInsDetails.DataSource = new List<VehicalRegistration>();
                dgvInsDetails.DataSource = _insDetails;
            }

        }


        private void Load_Registration_Details(string _com, string _pc, string _appCode, string _revReqNo)
        {
            _regDetails = new List<VehicalRegistration>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            VehicalRegistration _tempAddReg = new VehicalRegistration();
            Int32 I = 0;

            RequestApprovalHeader _tempRegReq = new RequestApprovalHeader();
            _tempRegReq = CHNLSVC.General.GetRelatedRequestByRef(_com, _pc, _revReqNo, _appCode);

            if (_tempRegReq != null)
            {
                lblRegReq.Text = _tempRegReq.Grah_ref;
                if (_tempRegReq.Grah_app_stus == "A")
                {
                    lblRegStatus.Text = "APPROVED";
                    chkRevReg.Checked = true;
                    chkRevReg.Enabled = false;
                    //  btnRegDetails.Enabled =  true;
                }
                else if (_tempRegReq.Grah_app_stus == "P")
                {
                    MessageBox.Show("Related registration refund request is still not approved. You cannot generate registration refund at this time. You have to refund it seperatly after getting approval.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRegStatus.Text = "PENDING";
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }
                else if (_tempRegReq.Grah_app_stus == "R")
                {
                    MessageBox.Show("Related registration refund request is rejected. Please contact registration department.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRegStatus.Text = "REJECT";
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }
                else if (_tempRegReq.Grah_app_stus == "F")
                {
                    MessageBox.Show("Related registration refund is already done.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRegStatus.Text = "FINISHED";
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }

                //load request registration refund details
                _tempReg = CHNLSVC.Sales.GetRefundReqVehReg(BaseCls.GlbUserComCode, _pc, lblRegReq.Text.Trim(), "ARQT016");
                // chkRevReg.Checked = true;
                // chkRevReg.Enabled = false;
                foreach (VehicalRegistration regDet in _tempReg)
                {
                    if (regDet.P_svrt_prnt_stus == 2)
                    {
                        MessageBox.Show("Request engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevReg.Checked = false;
                        chkRevReg.Enabled = false;
                        // goto skipAdd;
                    }
                    else if (regDet.P_srvt_rmv_stus != 0)
                    {
                        MessageBox.Show("Request engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already send to RMV.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevReg.Checked = false;
                        chkRevReg.Enabled = false;
                        //goto skipAdd;
                    }

                    _tempAddReg = regDet;
                    _regDetails.Add(_tempAddReg);
                    //skipAdd:
                    I = I + 1;
                }


                dgvRegDetails.AutoGenerateColumns = false;
                dgvRegDetails.DataSource = new List<VehicalRegistration>();
                dgvRegDetails.DataSource = _regDetails;

                if (dgvRegDetails.Rows.Count == 0)
                {
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }
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
                    MessageBox.Show("Please select request number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    MessageBox.Show("Please select reversal sub type.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //set_approveUser_infor("ARQT014");

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

                //update request details
                foreach (DataGridViewRow grv in dgvDelDetails.Rows)
                {

                    string _stus;
                    string _itm = grv.Cells["col_item"].Value.ToString();
                    string _orgStus = grv.Cells["col_itmstatus"].Value.ToString();
                    string _serial = grv.Cells["col_Serial"].Value.ToString();
                    if (grv.Cells["col_appstatus"].Value != null)
                    {
                        _stus = grv.Cells["col_appstatus"].Value.ToString();

                        DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                            if (dt1.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                {
                                    MessageBox.Show("Cannot approved different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        _stus = grv.Cells["col_itmstatus"].Value.ToString();
                    }
                    CHNLSVC.Sales.UpdateRequestAppStatus(_stus, lblReq.Text, _itm);
                }

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);

                //_rowEffect=CHNLSVC.Sales.UpdateRequestAppStatus(

                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void set_approveUser_infor(string app_tp_code)
        {
            if (app_tp_code == "ARQT014")//VEHICLE REGISTRATION RECEIPT REFUND
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.CSINREV);
            }
            //if (app_tp_code == "ARQT021")//VEHICLE INSURANCE RECEIPT REFUND
            //{
            //    GetUserAppLevel(HirePurchasModuleApprovalCode.VHINSRF);
            //}
            //if (app_tp_code == "ARQT019")//CONVERTED INVOICE VEH. INSURANCE REFUND -ARQT019
            //{
            //    GetUserAppLevel(HirePurchasModuleApprovalCode.CCINSRF);
            //}
            //if (app_tp_code == "ARQT018")//CONVERTED INVOICE REGISTRATION REFUND -ARQT018
            //{
            //    GetUserAppLevel(HirePurchasModuleApprovalCode.CCREGRF);
            //}
            //if (app_tp_code == "ARQT017")//REVESED INVOICE VEH. INSURANCE REFUND -ARQT017
            //{
            //    GetUserAppLevel(HirePurchasModuleApprovalCode.CSINSRF);
            //}
            //if (app_tp_code == "ARQT016")//REVESED INVOICE REGISTRATION REFUND -ARQT016
            //{
            //    GetUserAppLevel(HirePurchasModuleApprovalCode.CSREGRF);
            //}
        }

        protected void GetUserAppLevel(HirePurchasModuleApprovalCode CD)
        {
            RequestApprovalCycleDefinition(false, CD, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        }

        private void btnRej_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = txtSubType.Text.Trim();

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully rejected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _msg = "";
                decimal _retVal = 0;
                Boolean _isOthRev = false;
                string _orgPC = "";

                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(lblReq.Text))
                {
                    MessageBox.Show("Please selected approved request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblReqPC.Text != BaseCls.GlbUserDefProf)
                {
                    MessageBox.Show("Request profit center and your profit center is not match.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblReturnLoc.Text != BaseCls.GlbUserDefLoca)
                {
                    MessageBox.Show("Request location and your profit center is not match.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text != "APPROVED")
                {
                    MessageBox.Show("Request is still not approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvInvItem.Rows.Count <= 0)
                {
                    MessageBox.Show("Cannot find reverse details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable _dtcls = CHNLSVC.CustService.check_Invoiced_JobClosed(txtInvoice.Text); //Sanjeewa 2016-03-24
                string _msg1 = "";
                if (_dtcls != null)
                {
                    if (_dtcls.Rows.Count > 0)
                    {
                        foreach (DataRow drow in _dtcls.Rows)
                        {
                            _msg1 += drow["jbd_jobno"].ToString() + ", ";
                        }
                    }
                }
                if (_msg1 != "")
                {
                    MessageBox.Show("Can not continue the Sales Reversal. Following job numbers are already delivered. " + _msg1, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }

                if (chkOthSales.Checked == true)
                {
                    _isOthRev = true;
                }
                else
                {
                    _isOthRev = false;
                }

                _orgPC = lblSalePc.Text.Trim();

                decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtpSRNDate.Value).Date, out _wkNo, BaseCls.GlbUserComCode);

                if (_weekNo == 0)
                {
                    MessageBox.Show("Week Definition is still not setup for current date.Please contact retail accounts dept.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                //Add by akila 2018/01/19
                if (HasInvoiceAlreadyReversed()) { return; }


                decimal rtnQty = 0;
                decimal regQty = 0;

                if (chkRevReg.Checked == true)
                {
                    if (_regDetails.Count > 0)
                    {
                        foreach (InvoiceItem temp in _InvDetailList)
                        {
                            rtnQty = 0;
                            regQty = 0;
                            rtnQty = temp.Sad_srn_qty;

                            foreach (VehicalRegistration tempReg in _regDetails)
                            {
                                if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                                {
                                    regQty = regQty + 1;
                                }
                            }

                            if (rtnQty < regQty)
                            {
                                MessageBox.Show("You are going to reverse registration details more than return qty.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot find registration details.", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                decimal insQty = 0;
                rtnQty = 0;
                if (chkRevIns.Checked == true)
                {
                    if (_insDetails.Count > 0)
                    {
                        foreach (InvoiceItem temp in _InvDetailList)
                        {
                            rtnQty = 0;
                            insQty = 0;
                            rtnQty = temp.Sad_srn_qty;

                            foreach (VehicleInsuarance tempIns in _insDetails)
                            {
                                if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                                {
                                    insQty = insQty + 1;
                                }
                            }

                            if (rtnQty < insQty)
                            {
                                MessageBox.Show("You are going to reverse insuarance details more than return qty.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot find insuarance details.", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                if (chkCashRefund.Checked == true)
                {
                    CollectRefApp();
                    CollectRefAppLog();
                }

                _retVal = 0;

                foreach (InvoiceItem tmpItem in _InvDetailList)
                {
                    _retVal = _retVal + tmpItem.Sad_tot_amt;
                }
                Boolean status = true;
                load_cust_dt(txtCusCode.Text, txtInvoice.Text, out status);//add by tharanga 2018/10/10
                if (status == false)
                {
                    return;
                }

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
                _invheader.Sah_pc = lblSalePc.Text.Trim(); //BaseCls.GlbUserDefProf;
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
                _invheader.Sah_anal_5 = txtSubType.Text.Trim();
                _invheader.Sah_anal_3 = lblReq.Text.Trim();
                _invheader.Sah_anal_4 = "ARQT014";
                _invheader.Sah_anal_7 = Convert.ToDecimal(lblCrAmt.Text);
                _invheader.Sah_anal_6 = txtLoyalty.Text.ToString();



                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = lblSalePc.Text.Trim();
                _invoiceAuto.Aut_cate_tp = "PC";
                _invoiceAuto.Aut_direction = 0;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "REV";
                _invoiceAuto.Aut_number = 0;
                if (BaseCls.GlbUserComCode == "LRP")
                {
                    _invoiceAuto.Aut_start_char = "RINREV";
                }
                else
                {
                    _invoiceAuto.Aut_start_char = "INREV";
                }
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
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_remarks = txtSRNremarks.Text.Trim(); ;
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                    _inventoryHeader.Ith_pc = lblSalePc.Text.Trim();
                    _inventoryHeader.Ith_oth_loc = txtActLoc.Text.Trim();


                    _SRNAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(dtpSRNDate.Value).Year;
                }

                List<RecieptHeader> _regReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _regRevReceipt = new MasterAutoNumber();

                if (chkRevReg.Checked == true)
                {
                    //revers registration receipts
                    var _lst = (from n in _regDetails
                                group n by new { n.P_srvt_ref_no } into r
                                select new { P_srvt_ref_no = r.Key.P_srvt_ref_no, P_svrt_reg_val = r.Sum(p => p.P_svrt_reg_val) }).ToList();

                    RecieptHeader _revRecHdr = new RecieptHeader();
                    _regRecList = new List<RecieptHeader>();

                    foreach (var s in _lst)
                    {
                        //_revRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, s.P_srvt_ref_no);
                        _revRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), s.P_srvt_ref_no);
                        _revRecHdr.Sar_tot_settle_amt = s.P_svrt_reg_val;
                        _revRecHdr.Sar_direct = false;
                        _regRecList.Add(_revRecHdr);
                    }

                    _regReversReceiptHeader = new List<RecieptHeader>();
                    _regRevReceipt = new MasterAutoNumber();

                    _regRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                    _regRevReceipt.Aut_cate_tp = "PC";
                    _regRevReceipt.Aut_direction = null;
                    _regRevReceipt.Aut_modify_dt = null;
                    _regRevReceipt.Aut_moduleid = "RECEIPT";
                    _regRevReceipt.Aut_number = 0;
                    _regRevReceipt.Aut_start_char = "RGRF";
                    _regRevReceipt.Aut_year = null;

                    _regReversReceiptHeader = _regRecList;
                }

                List<RecieptHeader> _insReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _insRevReceipt = new MasterAutoNumber();

                if (chkRevIns.Checked == true)
                {
                    //revers registration receipts
                    var _lst = (from n in _insDetails
                                group n by new { n.Svit_ref_no } into r
                                select new { Svit_ref_no = r.Key.Svit_ref_no, Svit_ins_val = r.Sum(p => p.Svit_ins_val) }).ToList();

                    RecieptHeader _revInsRecHdr = new RecieptHeader();
                    _insRecList = new List<RecieptHeader>();

                    foreach (var s in _lst)
                    {
                        //_revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, s.Svit_ref_no);
                        _revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), s.Svit_ref_no);
                        _revInsRecHdr.Sar_tot_settle_amt = s.Svit_ins_val;
                        _revInsRecHdr.Sar_direct = false;
                        _insRecList.Add(_revInsRecHdr);
                    }

                    _insReversReceiptHeader = new List<RecieptHeader>();
                    _insRevReceipt = new MasterAutoNumber();

                    _insRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                    _insRevReceipt.Aut_cate_tp = "PC";
                    _insRevReceipt.Aut_direction = null;
                    _insRevReceipt.Aut_modify_dt = null;
                    _insRevReceipt.Aut_moduleid = "RECEIPT";
                    _insRevReceipt.Aut_number = 0;
                    _insRevReceipt.Aut_start_char = "RGRF";
                    _insRevReceipt.Aut_year = null;

                    _insReversReceiptHeader = _insRecList;
                }

                string _ReversNo = "";
                string _crednoteNo = ""; //add by chamal 05-12-2012

                foreach (ReptPickSerials _ser in _doitemserials)
                {
                    if (_ser.Tus_ser_id==-1)//add by tharanga/2017/11/09
                    {
                        _ser.Tus_ser_id = 0;
                    }
                    string _newStus = "";
                    DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(lblReq.Text);
                    var _newStus1 = (from _res in _dt.AsEnumerable()
                                     where _res["grad_anal15"].ToString() == _ser.Tus_itm_cd
                                     select _res["grad_anal8"].ToString()).ToList();
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
                                        MessageBox.Show("Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (!ChechServiceInvoice())
                {
                    return;
                }

                #region Check receving serials are duplicating :: Chamal 08-May-2014
                string _err = string.Empty;
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
                {
                    MessageBox.Show(_err.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                //kapila 11/7/2017
                foreach (var item in _doitemserials)
                {
                    item.Tus_orig_grndt = _inventoryHeader.Ith_doc_date;
                }
                foreach (var item in _doitemserials)//add by tharanga inr_batch not update job no and line when serivce invoice reversal
                {
                    item.Tus_job_no = _InvDetailList.FirstOrDefault().Sad_job_no.ToString();
                    item.Tus_job_line = Convert.ToInt32(_InvDetailList.FirstOrDefault().Sad_job_line.ToString());
                }
              
                int effect = CHNLSVC.Sales.SaveReversalNew(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _regRevReceipt, _regReversReceiptHeader, _regDetails, chkRevReg.Checked, _insRevReceipt, _insReversReceiptHeader, _insDetails, chkRevIns.Checked, _isOthRev, BaseCls.GlbUserDefProf, _refAppHdr, _refAppDet, _refAppAuto, _refAppHdrLog, _refAppDetLog, chkCashRefund.Checked, out _crednoteNo);

                Clear_Data();

                if (effect == 1)
                {
                    MessageBox.Show("Successfully created.Reversal No: " + _ReversNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
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
                            _insu.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SInward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                            _insu.GlbReportDoc = _crednoteNo;
                            _insu.Show();
                            _insu = null;
                       
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(_ReversNo))
                    {
                        MessageBox.Show(_ReversNo, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

            if (_isFromReq == true)
            {
                MessageBox.Show("Cannot edit requested details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to remove selected item ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (_InvDetailList == null || _InvDetailList.Count == 0) return;

                string _item = dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value.ToString();
                Int32 _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);

                List<InvoiceItem> _temp = new List<InvoiceItem>();
                //List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                //_doitemserials = new List<ReptPickSerials>();

                _temp = _InvDetailList;
                _temp.RemoveAll(x => x.Sad_itm_cd == _item && x.Sad_itm_line == _line);
                _InvDetailList = _temp;

                //if (_InvDetailList.Count > 0)
                //{
                //    foreach (InvoiceItem item in _InvDetailList)
                //    {
                //        _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
                //        _doitemserials.AddRange(_tempDOSerials);
                //    }
                //}

                //updated by akila 2017/12/22
                if (_doitemserials != null && _doitemserials.Count > 0)
                {
                    _doitemserials.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_base_itm_line == _line);
                }

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;
            }
        }

        private void dgvInvItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Int32 _line = 0;
            string _item = "";
            decimal _EditQty = 0;
            decimal _invQty = 0;
            decimal _unitAmt = 0;
            decimal _disAmt = 0;
            decimal _taxAmt = 0;
            decimal _totAmt = 0;

            if (dgvInvItem.Rows.Count > 0)
            {
                List<InvoiceItem> _InvList = new List<InvoiceItem>();

                if (string.IsNullOrEmpty(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value.ToString()))
                {
                    MessageBox.Show("Please enter valid reversal qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    dgvInvItem.DataSource = _InvDetailList;
                    return;
                }

                _line = Convert.ToInt32(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);
                _item = dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value.ToString();
                _EditQty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invRevQty"].Value);
                _invQty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invQty"].Value);


                if (_invQty < _EditQty)
                {
                    MessageBox.Show("Cannot exceed invoice qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    dgvInvItem.DataSource = _InvDetailList;
                    return;
                }

                if (_EditQty <= 0)
                {
                    MessageBox.Show("Invalid return qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    dgvInvItem.DataSource = _InvDetailList;
                    return;
                }

                foreach (InvoiceItem item in _InvDetailList)
                {
                    if (item.Sad_itm_cd == _item && item.Sad_itm_line == _line)
                    {
                        if (item.Sad_srn_qty < _EditQty)
                        {
                            MessageBox.Show("Cannot exceed invoice qty.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvInvItem.AutoGenerateColumns = false;
                            dgvInvItem.DataSource = new List<InvoiceItem>();
                            dgvInvItem.DataSource = _InvDetailList;
                            return;
                        }

                        item.Sad_srn_qty = Convert.ToDecimal(_EditQty);
                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
                        _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
                        _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));
                        _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(_invQty) * Convert.ToDecimal(_EditQty));

                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                        item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                        item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                        item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                    }
                    _InvList.Add(item);
                }

                _InvDetailList = _InvList;
                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;
            }
        }

        private void dgvDelDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (_doitemserials == null || _doitemserials.Count <= 0) return;

            if (_isFromReq == true)
            {
                MessageBox.Show("Cannot edit requested details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to remove selected serial ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                string _item = dgvDelDetails.Rows[e.RowIndex].Cells["col_item"].Value.ToString();
                Int32 _line = Convert.ToInt32(dgvDelDetails.Rows[e.RowIndex].Cells["col_BaseLine"].Value);
                string _serial = dgvDelDetails.Rows[e.RowIndex].Cells["col_Serial"].Value.ToString();
                decimal _qty = Convert.ToDecimal(dgvDelDetails.Rows[e.RowIndex].Cells["col_Qty"].Value);
                Int32 _serID = Convert.ToInt32(dgvDelDetails.Rows[e.RowIndex].Cells["col_SerID"].Value);
      
                List<InvoiceItem> _temp = new List<InvoiceItem>();
                List<InvoiceItem> _newList = new List<InvoiceItem>();
                List<ReptPickSerials> _tempser = new List<ReptPickSerials>();

                _temp = _InvDetailList;
                _InvDetailList = null;

                foreach (InvoiceItem itm in _temp)
                {
                    if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                    {
                        itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
                        itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
                        //itm.Sad_unit_amt = itm.Sad_unit_amt / itm.Sad_qty * itm.Sad_srn_qty;
                        //itm.Sad_itm_tax_amt = itm.Sad_itm_tax_amt / itm.Sad_qty * itm.Sad_srn_qty;
                        //itm.Sad_disc_amt = itm.Sad_disc_amt / itm.Sad_qty * itm.Sad_srn_qty;
                        //itm.Sad_tot_amt = itm.Sad_tot_amt / itm.Sad_qty * itm.Sad_srn_qty;
                    }


                    _newList.Add(itm);

                }

                _newList.RemoveAll(x => x.Sad_srn_qty <= 0);
                _InvDetailList = _newList;

                _tempser = _doitemserials;
                _doitemserials = null;

                //updated by akila 2017/12/22
                _tempser.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_base_itm_line == _line && x.Tus_ser_1 == _serial && x.Tus_ser_id == _serID && x.Tus_qty == _qty);
                //_tempser.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_base_itm_line == _line && x.Tus_ser_1 == _serial && x.Tus_ser_id == _serID);
                _doitemserials = _tempser;


                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;

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
            //if (e.ColumnIndex == 10 && e.RowIndex != -1)
            //{
            //    if (_isFromReq == true)
            //    {
            //        MessageBox.Show("Cannot edit requested details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    if (MessageBox.Show("Do you want to remove selected item ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
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
        }

        private void dgvDelDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_doitemserials == null || _doitemserials.Count <= 0) return;
            if (dgvDelDetails.Rows.Count == 0) return;

            string _itm = Convert.ToString(dgvDelDetails.Rows[e.RowIndex].Cells["col_item"].Value);
            string _ser = Convert.ToString(dgvDelDetails.Rows[e.RowIndex].Cells["col_Serial"].Value);
            string _wara = Convert.ToString(dgvDelDetails.Rows[e.RowIndex].Cells["col_Wara"].Value);

            lblSerItem.Text = _itm;
            lblSerial.Text = _ser;
            lblWarranty.Text = _wara;
            //if (e.ColumnIndex == 10 && e.RowIndex != -1)
            //{
            //    if (_isFromReq == true)
            //    {
            //        MessageBox.Show("Cannot edit requested details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    if (MessageBox.Show("Do you want to remove selected serial ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        string _item = dgvDelDetails.Rows[e.RowIndex].Cells["col_item"].Value.ToString();
            //        Int32 _line = Convert.ToInt32(dgvDelDetails.Rows[e.RowIndex].Cells["col_BaseLine"].Value);
            //        string _serial = dgvDelDetails.Rows[e.RowIndex].Cells["col_Serial"].Value.ToString();
            //        decimal _qty = Convert.ToDecimal(dgvDelDetails.Rows[e.RowIndex].Cells["col_Qty"].Value);
            //        Int32 _serID = Convert.ToInt32(dgvDelDetails.Rows[e.RowIndex].Cells["col_SerID"].Value);

            //        List<InvoiceItem> _temp = new List<InvoiceItem>();
            //        List<InvoiceItem> _newList = new List<InvoiceItem>();
            //        List<ReptPickSerials> _tempser = new List<ReptPickSerials>();

            //        _temp = _InvDetailList;
            //        _InvDetailList = null;

            //        foreach (InvoiceItem itm in _temp)
            //        {
            //            if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
            //            {
            //                itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
            //                itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
            //            }

            //            _newList.Add(itm);

            //        }

            //        _newList.RemoveAll(x => x.Sad_srn_qty <= 0);
            //        _InvDetailList = _newList;

            //        _tempser = _doitemserials;
            //        _doitemserials = null;

            //        _tempser.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_base_itm_line == _line && x.Tus_ser_1 == _serial && x.Tus_ser_id == _serID);
            //        _doitemserials = _tempser;


            //        dgvInvItem.AutoGenerateColumns = false;
            //        dgvInvItem.DataSource = new List<InvoiceItem>();
            //        dgvInvItem.DataSource = _InvDetailList;

            //        dgvDelDetails.AutoGenerateColumns = false;
            //        dgvDelDetails.DataSource = new List<ReptPickSerials>();
            //        dgvDelDetails.DataSource = _doitemserials;

            //    }
            //}
        }

        private void chkIsMan_CheckedChanged(object sender, EventArgs e)
        {

            if (chkIsMan.Checked == true)
            {
                txtManRef.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_SRN");
                if (_NextNo != 0)
                {
                    txtManRef.Text = _NextNo.ToString();
                }
                else
                {
                    MessageBox.Show("Cannot find valid manual document.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRef.Text = "";
                }
            }
            else
            {
                txtManRef.Text = string.Empty;

            }

        }

        private void btnRegDetails_Click(object sender, EventArgs e)
        {
            if (pnlReg.Visible == true)
            {
                pnlReg.Visible = false;
            }
            else
            {
                pnlReg.Visible = true;
            }
        }



        private void dgvRegDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 && e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you want to remove selected registration details ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_regDetails == null || _regDetails.Count == 0) return;

                    string _item = dgvRegDetails.Rows[e.RowIndex].Cells["col_regItm"].Value.ToString();
                    string _engine = dgvRegDetails.Rows[e.RowIndex].Cells["col_regEngine"].Value.ToString();

                    //if (_doitemserials.Count > 0)
                    // {
                    //     foreach (ReptPickSerials tempSer in _doitemserials)
                    //     {
                    //         if (tempSer.Tus_itm_cd == _item && tempSer.Tus_ser_1 == _engine)
                    //         {
                    //             MessageBox.Show("Cannot delete selected reqistration details due to you are going to revers related engine #.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //             return;
                    //         }
                    //     }
                    // }


                    List<VehicalRegistration> _temp = new List<VehicalRegistration>();
                    _temp = _regDetails;

                    _temp.RemoveAll(x => x.P_srvt_itm_cd == _item && x.P_svrt_engine == _engine);
                    _regDetails = _temp;


                    dgvRegDetails.AutoGenerateColumns = false;
                    dgvRegDetails.DataSource = new List<VehicalRegistration>();
                    dgvRegDetails.DataSource = _regDetails;
                }
            }
        }

        private void txtRevEngine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (string.IsNullOrEmpty(txtRevRegItem.Text))
                {
                    MessageBox.Show("Please select invoice item.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRevEngine;
                _CommonSearch.ShowDialog();
                txtRevEngine.Select();

            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnRevRegAdd.Focus();
            }
        }

        private void btnGetRegAll_Click(object sender, EventArgs e)
        {
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            VehicalRegistration _tempAddReg = new VehicalRegistration();
            _regDetails = new List<VehicalRegistration>();
            Int32 I = 0;

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    //_tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                    _tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                    foreach (VehicalRegistration regDet in _tempReg)
                    {
                        if (regDet.P_svrt_prnt_stus == 2)
                        {
                            MessageBox.Show("Engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto skipAdd;
                        }
                        else if (regDet.P_srvt_rmv_stus != 0)
                        {
                            MessageBox.Show("Engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already send to RMV.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto skipAdd;
                        }

                        _tempAddReg = regDet;
                        _regDetails.Add(_tempAddReg);
                    skipAdd:
                        I = I + 1;
                    }

                }

            }
            else
            {
                MessageBox.Show("Cannot find invoice details.First you have to select invoice details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvRegDetails.AutoGenerateColumns = false;
            dgvRegDetails.DataSource = new List<VehicalRegistration>();
            dgvRegDetails.DataSource = _regDetails;
        }

        private void chkRevReg_CheckedChanged(object sender, EventArgs e)
        {
            if (_isFromReq == true) return;
            if (chkRevReg.Checked == true)
            {
                txtRevEngine.Enabled = true;
                btnRevRegAdd.Enabled = true;
                btnGetRegAll.Enabled = true;
                txtRevRegItem.Enabled = true;
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";
                _regDetails = new List<VehicalRegistration>();

                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                VehicalRegistration _tempAddReg = new VehicalRegistration();
                _regDetails = new List<VehicalRegistration>();
                Int32 I = 0;

                if (_InvDetailList.Count > 0)
                {
                    foreach (InvoiceItem item in _InvDetailList)
                    {
                        //_tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                        _tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                        foreach (VehicalRegistration regDet in _tempReg)
                        {
                            foreach (ReptPickSerials _tmpSer in _doitemserials)
                            {
                                if (_tmpSer.Tus_itm_cd == regDet.P_srvt_itm_cd && _tmpSer.Tus_ser_1 == regDet.P_svrt_engine)
                                {
                                    if (regDet.P_svrt_prnt_stus == 2)
                                    {
                                        MessageBox.Show("You cannot refund reverse engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " due to it is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        goto skipAdd;
                                    }
                                    else if (regDet.P_srvt_rmv_stus != 0)
                                    {
                                        MessageBox.Show("You cannot refund reverse engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " due to it is already send to RMV.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        goto skipAdd;
                                    }
                                    _tempAddReg = regDet;
                                    _regDetails.Add(_tempAddReg);
                                skipAdd:
                                    I = I + 1;
                                }

                            }

                        }

                    }

                }
                else
                {
                    MessageBox.Show("Cannot find invoice details.First you have to select invoice details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                dgvRegDetails.AutoGenerateColumns = false;
                dgvRegDetails.DataSource = new List<VehicalRegistration>();
                dgvRegDetails.DataSource = _regDetails;

                if (dgvRegDetails.Rows.Count == 0)
                {
                    chkRevReg.Checked = false;
                }
            }
            else
            {
                txtRevEngine.Enabled = false;
                btnRevRegAdd.Enabled = false;
                btnGetRegAll.Enabled = false;
                txtRevRegItem.Enabled = false;
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";

                _regDetails = new List<VehicalRegistration>();
                dgvRegDetails.AutoGenerateColumns = false;
                dgvRegDetails.DataSource = new List<VehicalRegistration>();
                dgvRegDetails.DataSource = _regDetails;
            }
        }

        private void txtRevRegItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRevRegItem;
                _CommonSearch.ShowDialog();
                txtRevRegItem.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtRevEngine.Focus();
            }
        }

        private void txtRevRegItem_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRevRegItem.Text))
            {
                InvoiceItem _tempInvItem = new InvoiceItem();
                _tempInvItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoice.Text.Trim(), txtRevRegItem.Text.Trim());

                if (_tempInvItem != null)
                {
                    if (_tempInvItem.Sad_inv_no == null)
                    {
                        MessageBox.Show("Canot find such item in selected invoice.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRevRegItem.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Canot find such item in selected invoice.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRevRegItem.Focus();
                    return;
                }
            }
        }

        private void btnRevRegAdd_Click(object sender, EventArgs e)
        {
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            VehicalRegistration _tempOne = new VehicalRegistration();
            Boolean _isExsist = false;

            if (string.IsNullOrEmpty(txtRevRegItem.Text))
            {
                MessageBox.Show("Item is missing.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRevRegItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRevEngine.Text))
            {
                MessageBox.Show("Engine # is missing.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRevEngine.Focus();
                return;
            }

            foreach (VehicalRegistration _tempregList in _regDetails)
            {
                if (_tempregList.P_srvt_itm_cd == txtRevRegItem.Text.Trim() && _tempregList.P_svrt_engine == txtRevEngine.Text.Trim())
                {
                    MessageBox.Show("selected engine # is already exsist.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRevEngine.Focus();
                    return;
                }
            }

            decimal rtnQty = 0;
            decimal regQty = 0;

            if (_regDetails.Count > 0)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    regQty = 0;
                    rtnQty = temp.Sad_qty;

                    foreach (VehicalRegistration tempReg in _regDetails)
                    {
                        if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                        {
                            regQty = regQty + 1;
                        }
                    }

                    regQty = regQty + 1;

                    if (rtnQty < regQty)
                    {
                        MessageBox.Show("You are going to add registration details more than return qty for above item.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            //_tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), txtRevRegItem.Text.Trim(), 2);
            _tempReg = CHNLSVC.Sales.GetVehRegForRev(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), txtRevRegItem.Text.Trim(), 2);
            _isExsist = false;

            if (_tempReg.Count > 0)
            {
                foreach (VehicalRegistration _one in _tempReg)
                {
                    if (_one.P_srvt_itm_cd == txtRevRegItem.Text.Trim() && _one.P_svrt_engine == txtRevEngine.Text.Trim())
                    {
                        _isExsist = true;
                        if (_one.P_svrt_prnt_stus == 2)
                        {
                            MessageBox.Show("Selected engine # is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isExsist = false;
                            return;
                        }
                        else if (_one.P_srvt_rmv_stus != 0)
                        {
                            MessageBox.Show("Selected engine # is already send to RMV.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isExsist = false;
                            return;
                        }

                        if (_isExsist == true)
                        {
                            _tempOne = _one;
                            _regDetails.Add(_tempOne);
                        }
                    }

                }
            }
            else if (_tempReg.Count == 0)
            {

                MessageBox.Show("Cannot find relavant engine / chassis for above item.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isExsist = false;
                txtRevEngine.Text = "";
                txtRevEngine.Focus();
                return;

            }

            if (_isExsist == false)
            {
                MessageBox.Show("Cannot find relavant engine / chassis for above item.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isExsist = false;
                txtRevEngine.Text = "";
                txtRevEngine.Focus();
                return;
            }


            dgvRegDetails.AutoGenerateColumns = false;
            dgvRegDetails.DataSource = new List<VehicalRegistration>();
            dgvRegDetails.DataSource = _regDetails;

            txtRevRegItem.Text = "";
            txtRevEngine.Text = "";
            txtRevRegItem.Focus();
        }

        private void chkRevIns_CheckedChanged(object sender, EventArgs e)
        {
            if (_isFromReq == true) return;
            if (chkRevIns.Checked == true)
            {
                txtRevInsEngine.Enabled = true;
                btnRevInsAdd.Enabled = true;
                btnGetInsAll.Enabled = true;
                txtRevInsItem.Enabled = true;
                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";
                _insDetails = new List<VehicleInsuarance>();

                List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
                VehicleInsuarance _tempAddIns = new VehicleInsuarance();
                _insDetails = new List<VehicleInsuarance>();
                Int32 I = 0;

                if (_InvDetailList.Count > 0)
                {
                    foreach (InvoiceItem item in _InvDetailList)
                    {
                        //_tempIns = CHNLSVC.Sales.GetVehInsForRev(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                        _tempIns = CHNLSVC.Sales.GetVehInsForRev(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                        foreach (VehicleInsuarance insDet in _tempIns)
                        {
                            foreach (ReptPickSerials _tmpSer in _doitemserials)
                            {
                                if (_tmpSer.Tus_itm_cd == insDet.Svit_itm_cd && _tmpSer.Tus_ser_1 == insDet.Svit_engine)
                                {
                                    if (insDet.Svit_cvnt_issue == 2)
                                    {
                                        MessageBox.Show("You cannot refund reverse engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " due to it is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        goto skipAdd;
                                    }
                                    else if (insDet.Svit_polc_stus == true)
                                    {
                                        MessageBox.Show("You cannot refund reverse engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " due to it is already issue cover note.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        goto skipAdd;
                                    }
                                    _tempAddIns = insDet;
                                    _insDetails.Add(_tempAddIns);
                                skipAdd:
                                    I = I + 1;
                                }

                            }

                        }

                    }

                }
                else
                {
                    MessageBox.Show("Cannot find invoice details.First you have to select invoice details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                dgvInsDetails.AutoGenerateColumns = false;
                dgvInsDetails.DataSource = new List<VehicleInsuarance>();
                dgvInsDetails.DataSource = _insDetails;

                if (dgvInsDetails.Rows.Count == 0)
                {
                    chkRevIns.Checked = false;
                }
            }
            else
            {
                txtRevInsEngine.Enabled = false;
                btnRevInsAdd.Enabled = false;
                btnGetInsAll.Enabled = false;
                txtRevInsItem.Enabled = false;
                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";

                _insDetails = new List<VehicleInsuarance>();
                dgvInsDetails.AutoGenerateColumns = false;
                dgvInsDetails.DataSource = new List<VehicleInsuarance>();
                dgvInsDetails.DataSource = _insDetails;
            }
        }

        private void txtRevInsItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRevInsItem;
                _CommonSearch.ShowDialog();
                txtRevInsItem.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtRevInsItem.Focus();
            }
        }

        private void txtRevInsItem_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRevInsItem.Text))
            {
                InvoiceItem _tempInvItem = new InvoiceItem();
                _tempInvItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoice.Text.Trim(), txtRevInsItem.Text.Trim());

                if (_tempInvItem != null)
                {
                    if (_tempInvItem.Sad_inv_no == null)
                    {
                        MessageBox.Show("Canot find such item in selected invoice.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRevInsItem.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Canot find such item in selected invoice.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRevInsItem.Focus();
                    return;
                }
            }
        }

        private void txtRevInsEngine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (string.IsNullOrEmpty(txtRevInsItem.Text))
                {
                    MessageBox.Show("Please select invoice item.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRevInsEngine;
                _CommonSearch.ShowDialog();
                txtRevInsEngine.Select();

            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnRevInsAdd.Focus();
            }
        }

        private void btnRevInsAdd_Click(object sender, EventArgs e)
        {
            List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
            VehicleInsuarance _tempOne = new VehicleInsuarance();
            Boolean _isExsist = false;

            if (string.IsNullOrEmpty(txtRevInsItem.Text))
            {
                MessageBox.Show("Item is missing.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRevInsItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRevInsEngine.Text))
            {
                MessageBox.Show("Engine # is missing.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRevInsEngine.Focus();
                return;
            }

            foreach (VehicleInsuarance _tempinsList in _insDetails)
            {
                if (_tempinsList.Svit_itm_cd == txtRevInsItem.Text.Trim() && _tempinsList.Svit_engine == txtRevInsEngine.Text.Trim())
                {
                    MessageBox.Show("selected engine # is already exsist.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRevInsEngine.Focus();
                    return;
                }
            }

            decimal rtnQty = 0;
            decimal insQty = 0;

            if (_insDetails.Count > 0)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    insQty = 0;
                    rtnQty = temp.Sad_qty;

                    foreach (VehicleInsuarance tempins in _insDetails)
                    {
                        if (temp.Sad_itm_cd == tempins.Svit_itm_cd)
                        {
                            insQty = insQty + 1;
                        }
                    }

                    insQty = insQty + 1;

                    if (rtnQty < insQty)
                    {
                        MessageBox.Show("You are going to add registration details more than return qty for above item.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            //_tempIns = CHNLSVC.Sales.GetVehInsForRev(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), txtRevInsItem.Text.Trim(), 2);
            _tempIns = CHNLSVC.Sales.GetVehInsForRev(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), txtRevInsItem.Text.Trim(), 2);
            _isExsist = false;

            foreach (VehicleInsuarance _one in _tempIns)
            {
                if (_one.Svit_itm_cd == txtRevInsItem.Text.Trim() && _one.Svit_engine == txtRevInsEngine.Text.Trim())
                {
                    _isExsist = true;
                    if (_one.Svit_cvnt_issue == 2)
                    {
                        MessageBox.Show("Selected engine # is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isExsist = false;
                        return;
                    }
                    else if (_one.Svit_polc_stus == true)
                    {
                        MessageBox.Show("Selected engine # already issue policy.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isExsist = false;
                        return;
                    }

                    if (_isExsist == true)
                    {
                        _tempOne = _one;
                        _insDetails.Add(_tempOne);
                    }
                }

            }

            if (_isExsist == false)
            {
                MessageBox.Show("Invalid engine # selected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRevInsEngine.Text = "";
                txtRevInsEngine.Focus();
                return;
            }


            dgvInsDetails.AutoGenerateColumns = false;
            dgvInsDetails.DataSource = new List<VehicleInsuarance>();
            dgvInsDetails.DataSource = _insDetails;

            txtRevInsItem.Text = "";
            txtRevInsEngine.Text = "";
            txtRevInsItem.Focus();
        }

        private void btnGetInsAll_Click(object sender, EventArgs e)
        {
            List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
            VehicleInsuarance _tempAddIns = new VehicleInsuarance();
            _insDetails = new List<VehicleInsuarance>();
            Int32 I = 0;

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    //_tempIns = CHNLSVC.Sales.GetVehInsForRev(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                    _tempIns = CHNLSVC.Sales.GetVehInsForRev(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                    foreach (VehicleInsuarance insDet in _tempIns)
                    {
                        if (insDet.Svit_cvnt_issue == 2)
                        {
                            MessageBox.Show("Engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto skipAdd;
                        }
                        else if (insDet.Svit_polc_stus == true)
                        {
                            MessageBox.Show("Engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already issue cover note.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto skipAdd;
                        }

                        _tempAddIns = insDet;
                        _insDetails.Add(_tempAddIns);
                    skipAdd:
                        I = I + 1;
                    }

                }

            }
            else
            {
                MessageBox.Show("Cannot find invoice details.First you have to select invoice details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvInsDetails.AutoGenerateColumns = false;
            dgvInsDetails.DataSource = new List<VehicleInsuarance>();
            dgvInsDetails.DataSource = _insDetails;
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
                    txtRemarks.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidAdjustmentSubType()
        {
            bool status = false;
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
            return status;
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
        }

        private void btnCusSearch_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusCode_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInvSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkOthSales.Checked == false)
                {

                    if (!string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                        DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtInvoice;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();
                        txtInvoice.Select();
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtInvoice;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();
                        txtInvoice.Select();
                    }
                }
                else
                {
                    //if (!string.IsNullOrEmpty(txtCusCode.Text))
                    //{
                    //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //    _CommonSearch.ReturnIndex = 0;
                    //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                    //    DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                    //    _CommonSearch.dvResult.DataSource = _result;
                    //    _CommonSearch.BindUCtrlDDLData(_result);
                    //    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    //    _CommonSearch.ShowDialog();
                    //    txtInvoice.Select();
                    //}
                    //else
                    //{
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();
                    //}
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtInvoice_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (chkOthSales.Checked == false)
                {
                    if (!string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                        DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtInvoice;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();
                        txtInvoice.Select();
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtInvoice;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();
                        txtInvoice.Select();
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtSubType_DoubleClick(object sender, EventArgs e)
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
        }

        private void chkOthSales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthSales.Checked == true)
            {
                txtCusCode.Text = "";
                txtCusCode.Enabled = false;
                btnCusSearch.Enabled = false;
            }
            else
            {
                txtCusCode.Text = "";
                txtCusCode.Enabled = true;
                btnCusSearch.Enabled = true;
            }
        }

        private void tbReg_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (lblStatus.Text != "APPROVED")
            {
                if (tbReg.SelectedTab == tbReg.TabPages[2])
                {
                    MessageBox.Show("Sales reversal request is still not approved.", "Cash Sales Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbReg.SelectTab(0);
                    return;
                }
            }
        }


        protected void CollectRefApp()
        {
            string _type = "";

            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            _refAppHdr = new RequestApprovalHeader();
            _refAppDet = new List<RequestApprovalDetail>();
            _refAppAuto = new MasterAutoNumber();


            _refAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _refAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _refAppHdr.Grah_app_tp = "ARQT022";
            _refAppHdr.Grah_fuc_cd = null;
            _refAppHdr.Grah_ref = null;
            _refAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _refAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _refAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _refAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_app_stus = "P";
            _refAppHdr.Grah_app_lvl = 0;
            _refAppHdr.Grah_app_by = string.Empty;
            _refAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _refAppHdr.Grah_sub_type = txtSubType.Text.Trim();
            _refAppHdr.Grah_oth_pc = lblSalePc.Text.Trim();



            _tempReqAppDet = new RequestApprovalDetail();
            _tempReqAppDet.Grad_ref = null;
            _tempReqAppDet.Grad_line = 1;
            _tempReqAppDet.Grad_req_param = "SALES CASH REFUND";
            _tempReqAppDet.Grad_val1 = Convert.ToDecimal(lblCrAmt.Text);
            _tempReqAppDet.Grad_val2 = 0;
            _tempReqAppDet.Grad_val3 = 0;
            _tempReqAppDet.Grad_val4 = 0;
            _tempReqAppDet.Grad_val5 = 0;
            _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
            _tempReqAppDet.Grad_anal2 = "";
            _tempReqAppDet.Grad_anal3 = "";
            _tempReqAppDet.Grad_anal4 = "";
            _tempReqAppDet.Grad_anal5 = "";
            _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
            _tempReqAppDet.Grad_is_rt1 = false;
            _tempReqAppDet.Grad_is_rt2 = false;

            _refAppDet.Add(_tempReqAppDet);



            _refAppAuto = new MasterAutoNumber();
            _refAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _refAppAuto.Aut_cate_tp = "PC";
            _refAppAuto.Aut_direction = 1;
            _refAppAuto.Aut_modify_dt = null;
            _refAppAuto.Aut_moduleid = "REQ";
            _refAppAuto.Aut_number = 0;
            _refAppAuto.Aut_start_char = "CSREF";
            _refAppAuto.Aut_year = null;
        }

        protected void CollectRefAppLog()
        {
            string _type = "";
            _refAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();

            _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdrLog.Grah_app_tp = "ARQT022";
            _ReqAppHdrLog.Grah_fuc_cd = null;
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


            _tempReqAppDet = new RequestApprovalDetailLog();
            _tempReqAppDet.Grad_ref = null;
            _tempReqAppDet.Grad_line = 1;
            _tempReqAppDet.Grad_req_param = "SALES CASH REFUND";
            _tempReqAppDet.Grad_val1 = Convert.ToDecimal(lblCrAmt.Text);
            _tempReqAppDet.Grad_val2 = 0;
            _tempReqAppDet.Grad_val3 = 0;
            _tempReqAppDet.Grad_val4 = 0;
            _tempReqAppDet.Grad_val5 = 0;
            _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
            _tempReqAppDet.Grad_anal2 = "";
            _tempReqAppDet.Grad_anal3 = "";
            _tempReqAppDet.Grad_anal4 = "";
            _tempReqAppDet.Grad_anal5 = "";
            _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
            _tempReqAppDet.Grad_is_rt1 = false;
            _tempReqAppDet.Grad_is_rt2 = false;
            _ReqAppDetLog.Add(_tempReqAppDet);


        }

        private void btnCancelPop_Click(object sender, EventArgs e)
        {
            tbMain.Enabled = true;
            pnlCancel.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlCancel.Visible = true;
            tbMain.Enabled = false;
        }

        private void linkLabelProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double housrs = 0;
            List<Hpr_SysParameter> _list = CHNLSVC.Sales.GetAll_hpr_Para("REVPERIOD", "COM", BaseCls.GlbUserComCode);
            if (_list != null && _list.Count > 0)
            {
                housrs = (double)_list[0].Hsy_val;
            }

            //get reversal
            InvoiceHeader _header = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtCancelInvoice.Text.Trim().ToUpper());
            //validate
            if (_header == null)
            {
                MessageBox.Show("Invalid invoice no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_header.Sah_direct != false)
            {
                MessageBox.Show("Invalid invoice no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //if (_header.Sah_cre_when.AddHours(housrs) > DateTime.Now)
            //{
            //    MessageBox.Show("Cancelation Time expired", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            // Added by Nadeeka 13-05-2015
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpSRNDate, lblBackDateInfor, dtpSRNDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (_header.Sah_dt.Date != DateTime.Now.Date)
                    {

                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                }
                else
                {

                    MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
            }

            string _error = "";


            //check crnote used or not
            List<RecieptItem> _recList = CHNLSVC.Sales.GetRecieptItemByRef(txtCancelInvoice.Text.Trim());
            if (_recList != null && _recList.Count > 0)
            {
                MessageBox.Show("Credit Note used as Payment, can not cancel", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string _do = "";
            // Check SRN items available or not =====  Nadeeka 12-05-2015
            DataTable _tblSRN = CHNLSVC.Sales.Check_SRN_Stock_Avilability(txtCancelInvoice.Text.Trim());
            if (_tblSRN.Rows.Count > 0)
            {
                foreach (DataRow drow in _tblSRN.Rows)
                {
                    _do = _do + " Doc # " + drow["ITH_DOC_NO"].ToString() + " Doc Date " + Convert.ToDateTime(drow["ITH_DOC_DATE"].ToString()).ToString("dd-MM-yyyy") + "\n";
                }
            }
            if (_tblSRN != null && _tblSRN.Rows.Count > 0)
            {
                MessageBox.Show("SRN Items not available, can not cancel  \n" + _do, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }




            //get items 


            //List<ReptPickSerials> _srnSerials = CHNLSVC.Inventory.GetSerialsByDocument(0, _header.Sah_man_ref);
            //foreach (ReptPickSerials _serials in _srnSerials) {
            //    ReptPickSerials _rept = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serials.Tus_itm_cd,_serials.Tus_ser_1,_serials.Tus_ser_2, _serials.Tus_ser_id.ToString());
            //    if (_rept.Tus_doc_no == null) {
            //        MessageBox.Show("Below item/serial not available in inventory,Can not process cancel\nItem - "+_serials.Tus_itm_cd+" Serial - "+_serials.Tus_ser_1+" Serial Id - "+_serials.Tus_ser_id);
            //        return;
            //    }

            //}

            //process
            int result = CHNLSVC.Sales.ProcessReversalCancel(_header, BaseCls.GlbUserID, DateTime.Now, out _error);
            if (!string.IsNullOrEmpty(_error))
            {
                MessageBox.Show("Error occurred while processing\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("Successfully Cancelled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            pnlCancel.Visible = false;
            tbMain.Enabled = true;

        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCancelInvoice.Text = "";
        }

        private void btnCancelCir_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchReversal);
            DataTable _result = CHNLSVC.CommonSearch.GetInvoiceReversal(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.obj_TragetTextBox = txtCancelInvoice;
            _CommonSearch.ShowDialog();
            txtCancelInvoice.Select();
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

        private void btnSerAppClose_Click(object sender, EventArgs e)
        {
            pnlSerApp.Visible = false;
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
                    MessageBox.Show("Invalid or already used service approval.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtJobNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchJobNo_Click(null, null);
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

        private void btnSearchActLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtActLoc;
                _CommonSearch.ShowDialog();
                txtActLoc.Select();
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

        private void txtActLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchActLoc_Click(null, null);
        }

        private void txtActLoc_DoubleClick(object sender, EventArgs e)
        {
            btnSearchActLoc_Click(null, null);
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
                    txtRQty.Text = "1";
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

        private void txtNewPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtRQty.Focus();
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
                _tmpAddDet.Grad_anal9 = Convert.ToDecimal(txtRQty.Text);


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

        private void btnReOk_Click(object sender, EventArgs e)
        {
            pnlReRep.Visible = false;
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

        private void txtRQty_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRQty.Text))
            {
                MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRQty.Focus();
                return;
            }

            if (!IsNumeric(txtRQty.Text))
            {
                MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRQty.Focus();
                return;
            }

            if (Convert.ToDecimal(txtRQty.Text) <= 0)
            {
                MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRQty.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtNewSerial.Text))
            {
                if (Convert.ToDecimal(txtRQty.Text) > 1)
                {
                    MessageBox.Show("Enter valid qty .", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRQty.Focus();
                    return;
                }
            }
        }

        private void txtRQty_KeyDown(object sender, KeyEventArgs e)
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

        private void txtCancelInvoice_TextChanged(object sender, EventArgs e)
        {

        }

        private bool ChechServiceInvoice()
        {
            bool status = false;
            String err;
            InvoiceHeader _header = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text.Trim().ToUpper());
            if (_header.Sah_anal_2 == "SCV")
            {
                // Check ADO in by other location
                status = CHNLSVC.CustService.CheckPendignAODForInvoiceReversal(txtInvoice.Text.Trim(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, out err);
                if (status == false)
                {
                    MessageBox.Show("Cannot cancel this invoice. ADO in by other location", "Service Job Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return status;
                }

                //Check warranty replacements
                List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoice.Text.Trim());
                var Job_n_Lines = _list.Select(e => new { e.Sad_job_no, e.Sad_job_line }).Distinct();
                foreach (var Job_n_Line in Job_n_Lines)
                {
                    DataTable dtDocs;
                    DataTable dtRecSerial;
                    DataTable dtIssuedSer;
                    DataTable dtRequest = CHNLSVC.CustService.GET_WRR_RPLC_DETAILS(BaseCls.GlbUserComCode, Job_n_Line.Sad_job_no, out dtDocs, out dtRecSerial, out dtIssuedSer);
                    if (dtRequest != null && dtRequest.Rows.Count > 0)
                    {
                        if (dtRequest.Select("GRAH_APP_STUS = 'F' OR GRAH_APP_STUS = 'N'").Length > 0)
                        {
                            MessageBox.Show("Cannot cancel this invoice. Warranty replacements details found.", "Service Job Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            status = false;
                            return status;
                        }
                    }
                }
            }
            else
            {
                status = true;
            }
            return status;
        }

        private void btnRev_Click(object sender, EventArgs e)
        {
            if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "HUG" || BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
            {
                //kapila
                DataTable _dt = CHNLSVC.Financial.get_SignOn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Now.Date));
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Process Halted ! You have not sign on", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10110))    //kapila 31/8/2015
            {
                MessageBox.Show("Sorry, You have no permission for reverse!\n( Advice: Required permission code :10110)", "Sale Reversal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string _docNo = "";
            string _regAppNo = "";
            string _insAppNo = "";
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                MessageBox.Show("Invoice customer is missing.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCusCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtInvoice.Text))
            {
                MessageBox.Show("Please select invoice number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoice.Focus();
                return;
            }

            DataTable _dtcls = CHNLSVC.CustService.check_Invoiced_JobClosed(txtInvoice.Text);//Sanjeewa 2016-03-24
            string _msg1 = "";
            if (_dtcls != null)
            {
                if (_dtcls.Rows.Count > 0)
                {
                    foreach (DataRow drow in _dtcls.Rows)
                    {
                        _msg1 += drow["jbd_jobno"].ToString() + ", ";
                    }
                }
            }
            if (_msg1 != "")
            {
                MessageBox.Show("Can not continue the Sales Reversal. Following job numbers are already delivered. " + _msg1, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoice.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                MessageBox.Show("Please enter remarks.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarks.Focus();
                return;
            }

            if (dgvInvItem.Rows.Count <= 0)
            {
                MessageBox.Show("No items are selected to generate request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoice.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSubType.Text))
            {
                MessageBox.Show("Please select return category.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lblSalePc.Text))
            {
                MessageBox.Show("Original sales profit center is missing.Please re-enter invoice #.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (chkRevReg.Checked == true)
            {
                if (dgvRegDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("No registration details are not found to generate registration refund request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }
            }

            if (chkRevIns.Checked == true)
            {
                if (dgvInsDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("No insuarance details are not found to generate insuarance refund request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }
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

            //check pending requests for invoice
            List<RequestApprovalHeader> _pendingRequest = CHNLSVC.General.GetPendingSRNRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text, "ARQT014");
            if (_pendingRequest != null && _pendingRequest.Count > 0)
            {
                List<RequestApprovalHeader> _app = (from _res in _pendingRequest
                                                    where _res.Grah_app_stus == "A"
                                                    select _res).ToList<RequestApprovalHeader>();
                if (_app != null && _app.Count > 0)
                {
                    MessageBox.Show("This invoice has approved Reuqest.Pleses Finish approve request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<RequestApprovalHeader> _pen = (from _res in _pendingRequest
                                                    where _res.Grah_app_stus == "P"
                                                    select _res).ToList<RequestApprovalHeader>();
                if (_pen != null && _pen.Count > 0)
                {
                    MessageBox.Show("This invoice has pending request, Please approve pending request", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
            }

            //Add by akila 2018/01/19
            if (HasInvoiceAlreadyReversed()){ return;}


            decimal rtnQty = 0;
            decimal regQty = 0;

            if (_regDetails.Count > 0)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    regQty = 0;
                    rtnQty = temp.Sad_srn_qty;

                    foreach (VehicalRegistration tempReg in _regDetails)
                    {
                        if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                        {
                            regQty = regQty + 1;
                        }
                    }

                    if (rtnQty < regQty)
                    {
                        MessageBox.Show("You are going to reverse registration details more than return qty.", "Cash Sale Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            decimal insQty = 0;

            if (_insDetails.Count > 0)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    insQty = 0;
                    rtnQty = temp.Sad_srn_qty;

                    foreach (VehicleInsuarance tempIns in _insDetails)
                    {
                        if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                        {
                            insQty = insQty + 1;
                        }
                    }

                    if (rtnQty < insQty)
                    {
                        MessageBox.Show("You are going to reverse insuarance details more than return qty.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            _status = "A";

            //DataTable _appSt = CHNLSVC.Sales.checkAppStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "P", this.GlbModuleName);
            //if (_appSt.Rows.Count > 0)
            //{
            //    if (lblSalePc.Text.Trim() != BaseCls.GlbUserDefProf)
            //    {
            //        if (MessageBox.Show("You are going to reverse other profit center sales and need to get approval. Do you want to process request ?", "Cash sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            _status = "P";
            //        }
            //        else
            //        {
            //            _status = "P";
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        _status = "A";
            //    }
            //}
            //else
            //{
            //    if (lblSalePc.Text.Trim() == BaseCls.GlbUserDefProf)
            //    {
            //        if (Convert.ToDateTime(lblInvDate.Text).Date == Convert.ToDateTime(DateTime.Now).Date)
            //        {
            //            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10057))
            //            {
            //                _status = "A";
            //            }
            //            else
            //            {
            //                _status = "P";
            //            }
            //        }
            //        else
            //        {
            //            _status = "P";
            //        }
            //    }
            //    else
            //    {
            //        _status = "P";
            //    }
            //}

            if (_status == "P")
            {
                if (txtSubType.Text != "REF")
                {
                    if (_repAddDet.Count <= 0)
                    {
                        MessageBox.Show("Please enter re-report item details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            CollectReqApp();
            CollectReqAppLog();

            if (chkRevReg.Checked == true)
            {
                CollectRegApp();
                CollectRegAppLog();
            }

            if (chkRevIns.Checked == true)
            {
                CollectInsApp();
                CollectInsAppLog();
            }


            var _lst = (from n in _ReqAppDet
                        group n by new { n.Grad_anal2 } into r
                        select new { Grad_anal2 = r.Key.Grad_anal2, grad_val3 = r.Sum(p => p.Grad_val3) }).ToList();
            foreach (var s in _lst)
            {
                string _item = s.Grad_anal2;
                decimal _qty = s.grad_val3;

                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                decimal _count = _ReqAppSer.Where(X => X.Gras_anal2 == _item).Count();

                if (_itm.Mi_cd != null)
                {
                    DataTable _type = new DataTable();
                    _type = CHNLSVC.Sales.GetItemTp(_itm.Mi_itm_tp);

                    if (_type.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(_type.Rows[0]["mstp_is_inv"]) == 1)
                        {
                            if (_qty != _count)
                            {
                                //MessageBox.Show("Deliverd qty and serial mismatch. DO Qty : " + _qty + " Serials :" + _count + " for the item : " + _item, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //return;
                            }
                        }
                    }

                }

            }


            List<InvoiceItem> _temp = new List<InvoiceItem>();
            InvoiceItem _orgInvDet = new InvoiceItem();
            _temp = _InvDetailList;
            string _revItem = "";
            string _delItem = "";
            Int32 _line = 0;
            decimal _rtnQty = 0;
            decimal _invQty = 0;
            decimal _doQty = 0;
            decimal _curRtnQty = 0;

            foreach (InvoiceItem itm in _temp)
            {
                if (!string.IsNullOrEmpty(itm.Sad_sim_itm_cd))
                {
                    _delItem = itm.Sad_sim_itm_cd;
                }
                else
                {
                    _delItem = itm.Sad_itm_cd;
                }
                _line = itm.Sad_itm_line;
                _rtnQty = itm.Sad_srn_qty;
                _revItem = itm.Sad_itm_cd;

                _orgInvDet = CHNLSVC.Sales.GetInvDetByLine(itm.Sad_inv_no, _revItem, _line);

                if (_orgInvDet.Sad_inv_no == null)
                {
                    MessageBox.Show("Cannot load item details in invoice. " + _revItem, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _invQty = _orgInvDet.Sad_qty;
                _doQty = _orgInvDet.Sad_do_qty;
                _curRtnQty = _orgInvDet.Sad_srn_qty;

                if (_invQty < _rtnQty)
                {
                    MessageBox.Show("You are going to revers more than invoice qty.Item : " + _revItem + "Inv. Qty : " + _invQty + "Rtn. Qty : " + _rtnQty, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((_invQty - _curRtnQty) < _rtnQty)
                {
                    MessageBox.Show("You are going to revers more than current available qty.Item : " + _revItem + "Inv. Qty : " + _invQty + "Current Rtn. Qty : " + _rtnQty + "Already Rtn. Qty : " + _curRtnQty, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MasterItem _itmDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _revItem);

                DataTable _rtnItmtype = new DataTable();
                _rtnItmtype = CHNLSVC.Sales.GetItemTp(_itmDet.Mi_itm_tp);

                if (_rtnItmtype.Rows.Count > 0)
                {
                    if (Convert.ToInt16(_rtnItmtype.Rows[0]["mstp_is_inv"]) == 1)
                    {
                        //kapila 25/9/2015 mod for decimal allow items
                        MasterItem _mstItm = CHNLSVC.Inventory.GetItem("", _delItem);
                        decimal _serCount = 0;
                        if (_mstItm.Mi_is_ser1 == -1)
                        {
                            var _scanItems1 = _ReqAppSer.GroupBy(x => new { x.Gras_anal2, x.Gras_anal7 }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Gras_anal8) });
                            foreach (var itm1 in _scanItems1)
                            {
                                if (itm1.Peo.Gras_anal2 == _delItem && itm1.Peo.Gras_anal7 == _line)
                                {
                                    _serCount = itm1.theSum;
                                }
                            }
                        }
                        else
                            _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Count();

                        if (_doQty < _serCount)
                        {
                            MessageBox.Show("You are going to revers more than deliverd qty.Item : " + _revItem + "Del. Qty : " + _doQty + "Serial : " + _serCount, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //imagin previous srns are having
                        if (_rtnQty != _serCount)
                        {
                            if (_invQty - _doQty == 0)
                            {
                                if (_rtnQty != _serCount)
                                {
                                    MessageBox.Show("Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (_rtnQty < _serCount)
                                {
                                    MessageBox.Show("Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }

                //if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                //{
                //    itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
                //    itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
                //}

            }

            int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, _ReqAppSer, _ReqAppAuto, _ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegAuto, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, chkRevReg.Checked, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsAuto, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, chkRevIns.Checked, out _docNo, out _regAppNo, out _insAppNo, _repAddDet);
         
            if (effet == 1)
            {
                DataTable _dtReq = CHNLSVC.Sales.getReqHdrByReqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _docNo);

                load_approved_req(_dtReq.Rows[0]["grah_ref"].ToString(), _dtReq.Rows[0]["grah_app_stus"].ToString(), _dtReq.Rows[0]["grah_fuc_cd"].ToString(), _dtReq.Rows[0]["grah_remaks"].ToString(), _dtReq.Rows[0]["grah_oth_loc"].ToString(), _dtReq.Rows[0]["grah_loc"].ToString(), _dtReq.Rows[0]["grah_sub_type"].ToString(), _dtReq.Rows[0]["grah_oth_pc"].ToString());

                save_reverse();

                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_docNo))
                { MessageBox.Show(_docNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                else
                { MessageBox.Show("Creation fail.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }



        }

        private void save_reverse()
        {

            string _msg = "";
            decimal _retVal = 0;
            Boolean _isOthRev = false;
            string _orgPC = "";

            if (CheckServerDateTime() == false) return;

            if (string.IsNullOrEmpty(lblReq.Text))
            {
                MessageBox.Show("Please selected approved request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lblReqPC.Text != BaseCls.GlbUserDefProf)
            {
                MessageBox.Show("Request profit center and your profit center is not match.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lblReturnLoc.Text != BaseCls.GlbUserDefLoca)
            {
                MessageBox.Show("Request location and your profit center is not match.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lblStatus.Text != "APPROVED")
            {
                MessageBox.Show("Request is still not approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvInvItem.Rows.Count <= 0)
            {
                MessageBox.Show("Cannot find reverse details.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (chkOthSales.Checked == true)
            {
                _isOthRev = true;
            }
            else
            {
                _isOthRev = false;
            }

            _orgPC = lblSalePc.Text.Trim();

            decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtpSRNDate.Value).Date, out _wkNo, BaseCls.GlbUserComCode);

            if (_weekNo == 0)
            {
                MessageBox.Show("Week Definition is still not setup for current date.Please contact retail accounts dept.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


            decimal rtnQty = 0;
            decimal regQty = 0;

            if (chkRevReg.Checked == true)
            {
                if (_regDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        regQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicalRegistration tempReg in _regDetails)
                        {
                            if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                            {
                                regQty = regQty + 1;
                            }
                        }

                        if (rtnQty < regQty)
                        {
                            MessageBox.Show("You are going to reverse registration details more than return qty.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot find registration details.", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            decimal insQty = 0;
            rtnQty = 0;
            if (chkRevIns.Checked == true)
            {
                if (_insDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        insQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicleInsuarance tempIns in _insDetails)
                        {
                            if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                            {
                                insQty = insQty + 1;
                            }
                        }

                        if (rtnQty < insQty)
                        {
                            MessageBox.Show("You are going to reverse insuarance details more than return qty.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot find insuarance details.", "Sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            if (chkCashRefund.Checked == true)
            {
                CollectRefApp();
                CollectRefAppLog();
            }

            _retVal = 0;

            foreach (InvoiceItem tmpItem in _InvDetailList)
            {
                _retVal = _retVal + tmpItem.Sad_tot_amt;
            }

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
            _invheader.Sah_pc = lblSalePc.Text.Trim(); //BaseCls.GlbUserDefProf;
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
            _invheader.Sah_anal_5 = txtSubType.Text.Trim();
            _invheader.Sah_anal_3 = lblReq.Text.Trim();
            _invheader.Sah_anal_4 = "ARQT014";
            _invheader.Sah_anal_7 = Convert.ToDecimal(lblCrAmt.Text);



            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = lblSalePc.Text.Trim();
            _invoiceAuto.Aut_cate_tp = "PC";
            _invoiceAuto.Aut_direction = 0;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "REV";
            _invoiceAuto.Aut_number = 0;
            if (BaseCls.GlbUserComCode == "LRP")
            {
                _invoiceAuto.Aut_start_char = "RINREV";
            }
            else
            {
                _invoiceAuto.Aut_start_char = "INREV";
            }
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
                _inventoryHeader.Ith_sub_tp = "NOR";
                _inventoryHeader.Ith_remarks = txtSRNremarks.Text.Trim(); ;
                _inventoryHeader.Ith_stus = "A";
                _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
                _inventoryHeader.Ith_cre_when = DateTime.Now;
                _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
                _inventoryHeader.Ith_mod_when = DateTime.Now;
                _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                _inventoryHeader.Ith_pc = lblSalePc.Text.Trim();
                _inventoryHeader.Ith_oth_loc = txtActLoc.Text.Trim();


                _SRNAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _SRNAuto.Aut_cate_tp = "LOC";
                _SRNAuto.Aut_direction = 1;
                _SRNAuto.Aut_modify_dt = null;
                _SRNAuto.Aut_moduleid = "SRN";
                _SRNAuto.Aut_number = 0;
                _SRNAuto.Aut_start_char = "SRN";
                _SRNAuto.Aut_year = Convert.ToDateTime(dtpSRNDate.Value).Year;
            }

            List<RecieptHeader> _regReversReceiptHeader = new List<RecieptHeader>();
            MasterAutoNumber _regRevReceipt = new MasterAutoNumber();

            if (chkRevReg.Checked == true)
            {
                //revers registration receipts
                var _lst = (from n in _regDetails
                            group n by new { n.P_srvt_ref_no } into r
                            select new { P_srvt_ref_no = r.Key.P_srvt_ref_no, P_svrt_reg_val = r.Sum(p => p.P_svrt_reg_val) }).ToList();

                RecieptHeader _revRecHdr = new RecieptHeader();
                _regRecList = new List<RecieptHeader>();

                foreach (var s in _lst)
                {
                    //_revRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, s.P_srvt_ref_no);
                    _revRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), s.P_srvt_ref_no);
                    _revRecHdr.Sar_tot_settle_amt = s.P_svrt_reg_val;
                    _revRecHdr.Sar_direct = false;
                    _regRecList.Add(_revRecHdr);
                }

                _regReversReceiptHeader = new List<RecieptHeader>();
                _regRevReceipt = new MasterAutoNumber();

                _regRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                _regRevReceipt.Aut_cate_tp = "PC";
                _regRevReceipt.Aut_direction = null;
                _regRevReceipt.Aut_modify_dt = null;
                _regRevReceipt.Aut_moduleid = "RECEIPT";
                _regRevReceipt.Aut_number = 0;
                _regRevReceipt.Aut_start_char = "RGRF";
                _regRevReceipt.Aut_year = null;

                _regReversReceiptHeader = _regRecList;
            }

            List<RecieptHeader> _insReversReceiptHeader = new List<RecieptHeader>();
            MasterAutoNumber _insRevReceipt = new MasterAutoNumber();

            if (chkRevIns.Checked == true)
            {
                //revers registration receipts
                var _lst = (from n in _insDetails
                            group n by new { n.Svit_ref_no } into r
                            select new { Svit_ref_no = r.Key.Svit_ref_no, Svit_ins_val = r.Sum(p => p.Svit_ins_val) }).ToList();

                RecieptHeader _revInsRecHdr = new RecieptHeader();
                _insRecList = new List<RecieptHeader>();

                foreach (var s in _lst)
                {
                    //_revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, s.Svit_ref_no);
                    _revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), s.Svit_ref_no);
                    _revInsRecHdr.Sar_tot_settle_amt = s.Svit_ins_val;
                    _revInsRecHdr.Sar_direct = false;
                    _insRecList.Add(_revInsRecHdr);
                }

                _insReversReceiptHeader = new List<RecieptHeader>();
                _insRevReceipt = new MasterAutoNumber();

                _insRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                _insRevReceipt.Aut_cate_tp = "PC";
                _insRevReceipt.Aut_direction = null;
                _insRevReceipt.Aut_modify_dt = null;
                _insRevReceipt.Aut_moduleid = "RECEIPT";
                _insRevReceipt.Aut_number = 0;
                _insRevReceipt.Aut_start_char = "RGRF";
                _insRevReceipt.Aut_year = null;

                _insReversReceiptHeader = _insRecList;
            }

            string _ReversNo = "";
            string _crednoteNo = ""; //add by chamal 05-12-2012

            foreach (ReptPickSerials _ser in _doitemserials)
            {
                string _newStus = "";
                DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(lblReq.Text);
                var _newStus1 = (from _res in _dt.AsEnumerable()
                                 where _res["grad_anal2"].ToString() == _ser.Tus_itm_cd
                                 select _res["grad_anal8"].ToString()).ToList();
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
                                    MessageBox.Show("Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (!ChechServiceInvoice())
            {
                return;
            }

            #region Check receving serials are duplicating :: Chamal 08-May-2014
            string _err = string.Empty;
            if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
            {
                MessageBox.Show(_err.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            //kapila 11/7/2017
            foreach (var item in _doitemserials)
            {
                item.Tus_orig_grndt = _inventoryHeader.Ith_doc_date;
            }
            int effect = CHNLSVC.Sales.SaveReversalNew(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _regRevReceipt, _regReversReceiptHeader, _regDetails, chkRevReg.Checked, _insRevReceipt, _insReversReceiptHeader, _insDetails, chkRevIns.Checked, _isOthRev, BaseCls.GlbUserDefProf, _refAppHdr, _refAppDet, _refAppAuto, _refAppHdrLog, _refAppDetLog, chkCashRefund.Checked, out _crednoteNo);

            Clear_Data();

            if (effect == 1)
            {
                MessageBox.Show("Successfully created.Reversal No: " + _ReversNo, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReportViewer _view = new ReportViewer();
                BaseCls.GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;

                if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "HUG" || BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
                {
                    clsSalesRep obj = new clsSalesRep();
                    BaseCls.GlbReportDoc = _ReversNo;
                    BaseCls.GlbReportIsCostPrmission = 0;

                    obj.POSCredNoteDirectPrint();
                }
                else
                {
                   
                        _view.GlbReportName = "InvoiceHalfPrints.rpt";
                        BaseCls.GlbReportTp = "INV";
                        _view.GlbReportDoc = _ReversNo;
                        _view.GlbSerial = null;
                        _view.GlbWarranty = null;
                        _view.Show();
                        _view = null;
                    
                    //kapila 1/12/2015
                    if (!string.IsNullOrEmpty(_crednoteNo))
                    {
                      
                            ReportViewerInventory _insu = new ReportViewerInventory();
                            BaseCls.GlbReportName = string.Empty;
                            _insu.GlbReportName = string.Empty;
                            BaseCls.GlbReportTp = "INWARD";
                            _insu.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SInward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                            _insu.GlbReportDoc = _crednoteNo;
                            _insu.Show();
                            _insu = null;
                       
                    }
                }




            }
            else
            {
                if (!string.IsNullOrEmpty(_ReversNo))
                {
                    MessageBox.Show(_ReversNo, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void load_approved_req(string _reqno, string _staus, string _invno, string _rem, string _tp, string _prof, string _subtp, string _othpc)
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
            Boolean _is_SVAT = false;
            decimal _tot_tax_amt = 0;

            txtInvoice.Text = "";
            txtCusCode.Text = "";
            lblInvCusName.Text = "";
            lblInvCusAdd1.Text = "";
            lblInvCusAdd2.Text = "";
            lblInvDate.Text = "";
            lblSalePc.Text = "";
            txtInvoice.Enabled = true;
            txtCusCode.Enabled = true;
            btnRequest.Enabled = false;

            //_InvDetailList = new List<InvoiceItem>();
            //_doitemserials = new List<ReptPickSerials>();
            //_doitemSubSerials = new List<ReptPickSerialsSub>();


            //dgvInvItem.AutoGenerateColumns = false;
            //dgvInvItem.DataSource = new List<InvoiceItem>();
            //dgvInvItem.DataSource = _InvDetailList;

            //dgvDelDetails.AutoGenerateColumns = false;
            //dgvDelDetails.DataSource = new List<ReptPickSerials>();
            //dgvDelDetails.DataSource = _doitemserials;

            dgvPaymentDetails.Rows.Clear();

            _reqNo = _reqno;
            _stus = _staus;
            _invNo = _invno;
            _remarks = _rem;
            _type = _tp;
            _pc = _prof;
            //  _retLoc = dgvPendings.Rows[e.RowIndex].Cells["col_Type"].Value.ToString();
            _retSubType = _subtp;
            _salesPC = _othpc;



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
            txtSRNremarks.Text = _remarks;
            lblReturnLoc.Text = _type;
            lblReqPC.Text = _pc;
            txtSubType.Text = _retSubType;
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

            //btnRegDetails.Enabled = false;
            if (_stus == "A")
            {
                lblStatus.Text = "APPROVED";
                //  btnRegDetails.Enabled =  true;
            }
            else if (_stus == "P")
            {
                lblStatus.Text = "PENDING";
            }
            else if (_stus == "R")
            {
                lblStatus.Text = "REJECT";
            }
            else if (_stus == "F")
            {
                lblStatus.Text = "FINISHED";
            }

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, _salesPC, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());

            foreach (InvoiceHeader _tempInv in _invHdr)
            {
                if (_tempInv.Sah_inv_no == null)
                {
                    MessageBox.Show("Error loading request.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Text = "";
                    txtCusCode.Text = "";
                    lblInvCusName.Text = "";
                    lblInvCusAdd1.Text = "";
                    lblInvCusAdd2.Text = "";
                    lblInvDate.Text = "";
                    lblSalePc.Text = "";
                    lblTotInvAmt.Text = "";
                    lblTotPayAmt.Text = "";
                    lblOutAmt.Text = "";
                    lblTotRetAmt.Text = "";
                    lblTotalRevAmt.Text = "";

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
                    return;
                }
                else
                {
                    txtCusCode.Text = _tempInv.Sah_cus_cd;
                    lblInvCusName.Text = _tempInv.Sah_cus_name;
                    lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                    lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                    lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                    lblSalePc.Text = _tempInv.Sah_pc;
                    lblTotInvAmt.Text = _tempInv.Sah_anal_7.ToString("n");
                    lblTotPayAmt.Text = _tempInv.Sah_anal_8.ToString("n");
                    lblOutAmt.Text = (_tempInv.Sah_anal_7 - _tempInv.Sah_anal_8).ToString("n");

                    _dCusCode = _tempInv.Sah_d_cust_cd;
                    _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                    _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                    _currency = _tempInv.Sah_currency;
                    _exRate = _tempInv.Sah_ex_rt;
                    _invTP = _tempInv.Sah_inv_tp;
                    _executiveCD = _tempInv.Sah_sales_ex_cd;
                    _manCode = _tempInv.Sah_man_cd;
                    _isTax = _tempInv.Sah_tax_inv;
                    _is_SVAT = _tempInv.Sah_is_svat;

                    if (lblStatus.Text == "FINISHED")
                    {
                        MessageBox.Show("Selected request is in FINISHED status.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnApp.Enabled = false;
                        btnCancel.Enabled = false;
                        return;
                    }
                    else if (_tempInv.Sah_stus == "C")
                    {
                        MessageBox.Show("Selected invoice is cancelled.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnApp.Enabled = false;
                        btnCancel.Enabled = false;
                        return;
                    }
                    else if (_tempInv.Sah_stus == "R")
                    {
                        MessageBox.Show("This invoice is already reversed.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnApp.Enabled = false;
                        btnCancel.Enabled = false;
                        return;
                    }
                    else if (lblStatus.Text == "APPROVED")
                    {
                        btnCancel.Enabled = true;
                    }
                    _isFromReq = true;
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                    //  Load_InvoiceDetails(_pc);
                    //    Load_Registration_Details(BaseCls.GlbUserComCode, _pc, "ARQT016", _reqNo);
                    //  Load_Insuarance_Details(BaseCls.GlbUserComCode, _pc, "ARQT017", _reqNo);

                    txtCusCode.Enabled = false;
                    txtInvoice.Enabled = false;
                    btnRequest.Enabled = false;
                    dgvInvItem.Columns["col_invRevQty"].ReadOnly = true;



                    List<InvoiceItem> _tmpInv = new List<InvoiceItem>();
                    List<InvoiceItem> _newList = new List<InvoiceItem>();
                    List<ReptPickSerials> _tempser = new List<ReptPickSerials>();

                    decimal _rtnSerQty = 0;
                    decimal _fwsQty = 0;

                    _tmpInv = _InvDetailList;
                    _InvDetailList = null;


                    foreach (InvoiceItem itm in _tmpInv)
                    {
                        _rtnSerQty = 0;
                        _fwsQty = 0;
                        foreach (ReptPickSerials _tmpser in _doitemserials)
                        {

                            string _item = _tmpser.Tus_itm_cd;
                            Int32 _line = _tmpser.Tus_base_itm_line;

                            if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                            {
                                //kapila 25/9/2015 mod for decimal allow items
                                MasterItem _mstItm = CHNLSVC.Inventory.GetItem("", _item);
                                if (_mstItm.Mi_is_ser1 == -1)
                                    _rtnSerQty = _rtnSerQty + itm.Sad_qty;
                                else
                                    _rtnSerQty = _rtnSerQty + 1;
                                //itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
                                //itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
                            }

                        }

                        _fwsQty = itm.Sad_srn_qty - _rtnSerQty;
                        itm.Sad_fws_ignore_qty = _fwsQty;
                        _newList.Add(itm);

                    }

                    _InvDetailList = _newList;

                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    dgvInvItem.DataSource = _InvDetailList;


                    decimal _totRetAmt = 0;
                    decimal _crAmt = 0;
                    decimal _outAmt = 0;
                    decimal _preRevAmt = 0;
                    decimal _preCrAmt = 0;
                    decimal _balCrAmt = 0;
                    decimal _newOut = 0;


                    DataTable _revAmt = CHNLSVC.Sales.GetTotalRevAmtByInv(txtInvoice.Text.Trim());
                    if (_revAmt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(_revAmt.Rows[0]["tot_rtn"].ToString()))
                        {
                            _preRevAmt = Convert.ToDecimal(_revAmt.Rows[0]["tot_rtn"]);
                        }
                    }

                    DataTable _preCr = CHNLSVC.Sales.GetTotalCRAmtByInv(txtInvoice.Text.Trim());
                    if (_preCr.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(_preCr.Rows[0]["tot_Cr"].ToString()))
                        {
                            _preCrAmt = Convert.ToDecimal(_preCr.Rows[0]["tot_Cr"]);
                        }
                    }


                    foreach (InvoiceItem _temp in _InvDetailList)
                    {
                        _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
                    }

                    lblTotalRevAmt.Text = _preRevAmt.ToString("n");
                    lblTotRetAmt.Text = _totRetAmt.ToString("n");

                    _outAmt = Convert.ToDecimal(lblOutAmt.Text);
                    _outAmt = Convert.ToDecimal(lblTotInvAmt.Text) - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

                    _balCrAmt = Convert.ToDecimal(lblTotPayAmt.Text) - _preCrAmt;

                    if (_balCrAmt < 0)
                    {
                        _balCrAmt = 0;
                    }

                    if (_outAmt > 0)
                    {
                        _crAmt = _balCrAmt - _outAmt;
                    }
                    else
                    {
                        if (_totRetAmt <= _balCrAmt)
                        {
                            _crAmt = _totRetAmt;
                        }
                        else
                        {
                            _crAmt = _balCrAmt;
                        }
                    }


                    //kapila 19/4/2017
                    if (_is_SVAT == true)
                    {
                        List<InvoiceItem> _paramInvItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), null);
                        foreach (InvoiceItem item in _paramInvItems)
                        {
                            _tot_tax_amt = _tot_tax_amt + (Convert.ToDecimal(item.Sad_itm_tax_amt));
                        }
                        _crAmt = _crAmt + _tot_tax_amt;
                        lblOutAmt.Text = (Convert.ToDecimal(lblOutAmt.Text) - _tot_tax_amt).ToString("0.00");
                        lblTotPayAmt.Text = (Convert.ToDecimal(lblTotPayAmt.Text) + _tot_tax_amt).ToString("0.00");
                    }



                    if (_crAmt > 0)
                    {
                        lblCrAmt.Text = _crAmt.ToString("n");
                    }
                    else
                    {
                        lblCrAmt.Text = "0";
                    }

                    DataTable _newRep = new DataTable();
                    //_newRep = CHNLSVC.General.SearchrequestAppDetByRef(_reqNo);
                    _newRep = CHNLSVC.General.SearchrequestAppAddDetByRef(_reqNo);

                    dgvRereportItems.DataSource = _newRep;

                    //Load collection deatails
                    DataTable _collDet = CHNLSVC.Sales.GetInvoiceReceiptDet(txtInvoice.Text.Trim());
                    if (_collDet.Rows.Count > 0)
                    {
                        foreach (DataRow drow in _collDet.Rows)
                        {
                            if (drow["SAR_RECEIPT_TYPE"].ToString() == "DEBT" || drow["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                            {
                                dgvPaymentDetails.Rows.Add();
                                dgvPaymentDetails["col_recSeq", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_receipt_no"].ToString();
                                dgvPaymentDetails["col_recNo", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_anal_3"].ToString();
                                dgvPaymentDetails["col_recDT", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_receipt_date"].ToString();
                                dgvPaymentDetails["col_recPayTp", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_pay_tp"].ToString();
                                dgvPaymentDetails["col_recPayRef", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_ref_no"].ToString();
                                dgvPaymentDetails["col_recAmt", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_settle_amt"].ToString();
                            }

                        }

                    }


                }
            }

        }

        private void txtInvoice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_checkline_Click(object sender, EventArgs e)
        {
            string _Invoice = txtInvoice.Text;
            if(string.IsNullOrEmpty(_Invoice))
            {
                MessageBox.Show("Enter the Invoice Number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }

        private void txtSubType_TextChanged(object sender, EventArgs e)
        {

        }

        private bool HasInvoiceAlreadyReversed()
        {
            bool _hasAlreadyRevered = false;

            try
            {
                List<InvoiceHeader> _reversInvDetails = new List<InvoiceHeader>();
                _reversInvDetails = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, lblSalePc.Text.Trim(), txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpSRNDate.Value.ToString(), dtpSRNDate.Value.ToString());
                if (_reversInvDetails != null && _reversInvDetails.Count > 0)
                {
                    var _reversDetCount = _reversInvDetails.Where(x => x.Sah_stus == "R").ToList().Count();
                    if (_reversDetCount > 0)
                    {
                        _hasAlreadyRevered = true;
                        MessageBox.Show("This invoice already has been revered !", "Cash sales reversal - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // add by tharanga 2018/10/25
                    {
                        //DataTable _revert = CHNLSVC.Sales.GetRevertAccountDetail(_reversInvDetails.First().Sah_acc_no);
                        DataTable _revert = CHNLSVC.Financial.GetRevertReleaseAccountDetail(BaseCls.GlbUserComCode, null, _reversInvDetails.First().Sah_acc_no, null);//--RBDL-HS-06877
                        if (_revert.Rows.Count > 0)
                        {
                            if (_revert.Rows[0]["HRT_IS_RLS"].ToString() == "0")
                            {
                                _hasAlreadyRevered = true;
                                MessageBox.Show("Cannot complete the proces. This Account is already reverted ", "HS sales reversal - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                            //dt_location.Rows[0]["ml_loc_tp"].ToString()
                        }
                    }

                }
            }
            catch (Exception)
            {
                CHNLSVC.CloseAllChannels();
                _hasAlreadyRevered = true;
                MessageBox.Show("An error occurred while validating invoice details !", "Cash sales reversal - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _hasAlreadyRevered;
        }

        private void load_cust_dt(string _custcd, string _invno, out Boolean status)
        {
            status = true;
            _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(_custcd, null, null, null, null, BaseCls.GlbUserComCode);
            string Loyalty = ReturnLoyaltyNo();
            txtLoyalty.Text = Loyalty.ToString();
            LoyaltyMemeber _LoyaltyDetails = CHNLSVC.Sales.getLoyaltyDetails(_custcd, Loyalty);
            InvoiceLoyalty _tInvoiceLoyalty = CHNLSVC.Sales.GetInvoiceLoyalty(_invno);
            if (_LoyaltyDetails !=null && _tInvoiceLoyalty!=null)
            {
                if (_LoyaltyDetails.Salcm_bal_pt < _tInvoiceLoyalty.Stlt_pt)
                {
                    if (MessageBox.Show("Sorry, Your Loyalty poins are not enough to Reverse this invoice. Do you want to Continue?", "Loyalty Card", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        //MessageBox.Show("Sorry, Your Loyalty poins are not enough to Reverse this invoice", "Loyalty Card", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear_Data();
                        status = false;
                    }
                }
                
            }

        }
        private string ReturnLoyaltyNo()
        {
            string _no = string.Empty;
            try
            {
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
              
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(_commonSearch.SearchParams, null, null);
                if (_result != null && _result.Rows.Count > 0)
                {
                    if (_result.Rows.Count > 1)
                    {
                        MessageBox.Show("Customer having multiple loyalty cards. Please select one of them.", "Loyalty Card", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLoyalty.BackColor = Color.White;
                        return _no;
                    }
                    _no = _result.Rows[0].Field<string>("Card No");
                    txtLoyalty.BackColor = Color.Red;
                }
                else txtLoyalty.BackColor = Color.White;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default;  }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            return _no;
        }

    }
}
