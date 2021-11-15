using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class ReturnRequestApproval : Base
    {

        #region variables

        string InvoiceType = "HS";
        string SSPriceBook = "";
        string SSPriceLevel = "";
        string SSPriceBookItemSequance = "";
        string SSPriceBookSequance = "";
        string SSPromotionCode = "";
        string SSIsLevelSerialized = "";

        decimal SSPriceBookPrice = 0;
        bool IsNoPriceDefinition;
        bool _isEditPrice;
        bool _isEditDiscount;
        bool IsPriceBookChanged;
        bool IsFixQty;
        int _lineNo = 0;
        int _combineCounter = 0;
        int SSCombineLine = 0;
        int SSPRomotionType;
        int _serialId = 0;
        bool _isCombineAdding = false;
        bool SalesReversal = false;

        List<ReptPickSerials> _popUpList = new List<ReptPickSerials>();
        List<ReptPickSerials> _selectedItemList = new List<ReptPickSerials>();
        List<PriceCombinedItemRef> _MainPriceCombinItem = new List<PriceCombinedItemRef>();
        PriceBookLevelRef _priceBookLevelRef = new PriceBookLevelRef();
        List<PriceSerialRef> _tempPriceSerial = new List<PriceSerialRef>();
        List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
        List<PriceCombinedItemRef> _tempPriceCombinItem;
        List<InvoiceItem> _invoiceItemList = new List<InvoiceItem>();
        List<PriceSerialRef> _MainPriceSerial = new List<PriceSerialRef>();
        List<PriceSerialRef> _tempPriceSerialItm = new List<PriceSerialRef>();
        PriceDefinitionRef _priceDefinitionRef;
        List<RequestApprovalSerials> _serialList = new List<RequestApprovalSerials>();
        #endregion


        public ReturnRequestApproval()
        {
            InitializeComponent();
        }

        private void cmbDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocType.SelectedItem != null)
            {
                if (cmbDocType.SelectedItem.ToString() == "Cash")
                {
                    lblDocNo.Text = "Account No";
                    lblRequest.Visible = false;
                    lblRequestStatus.Visible = false;
                }
                else if (cmbDocType.SelectedItem.ToString() == "Hire")
                {
                    lblDocNo.Text = "Account No";
                    lblRequest.Visible = false;
                    lblRequestStatus.Visible = false;
                }
                else
                {
                    lblDocNo.Text = "Request No";
                    lblRequest.Visible = true;
                    lblRequestStatus.Visible = true;
                }


                txtDocumentNo.Text = "";
                grvIssuingDetails.DataSource = null;
                grvReturningDetails.DataSource = null;
                grvTrade.DataSource = null;
            }
        }

        private void ReturnRequestApproval_Load(object sender, EventArgs e)
        {
            try
            {
                cmbDocType.SelectedIndex = 0;
                cmbUsage.SelectedIndex = 0;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblError, string.Empty, out _allowCurrentTrans);
                LoadAppLevelStatus("", "", "");
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
            if (MessageBox.Show("Are you want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void btnWarranty_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarraWarrantyNo);
                DataTable _result = CHNLSVC.CommonSearch.GetWarrantySearchByWarrantyNoSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtWarranty;
                _CommonSearch.ShowDialog();
                txtWarranty.Select();
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

        private void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarraSerialNo);
                DataTable _result = CHNLSVC.CommonSearch.GetWarrantySearchBySerialNoSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialNo;
                _CommonSearch.ShowDialog();
                txtSerialNo.Select();
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

        private void btnPB_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(InvoiceType))
                {
                    MessageBox.Show("Please select the invoice type!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtInvType.Focus();
                    return;
                }


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPB;
                _CommonSearch.ShowDialog();
                txtPB.Select();
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

        private void btnPLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(InvoiceType))
                {
                    MessageBox.Show("Please select the invoice type!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtInvType.Focus();
                    return;
                }


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPLevel;
                _CommonSearch.ShowDialog();
                txtPLevel.Select();
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

        private void btnDocument_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDocType.SelectedItem != null && cmbDocType.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select the invoice type!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtInvType.Focus();
                    return;
                }
                DataTable dataSource = null;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                if (cmbDocType.SelectedItem.ToString().Equals(CommonUIDefiniton.ReturnRequestDocumentType.Cash.ToString()))
                {

                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoice);
                    dataSource = CHNLSVC.CommonSearch.GetInvoiceByInvType(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.ReturnIndex = 1;
                }

                if (cmbDocType.SelectedItem.ToString().Equals(CommonUIDefiniton.ReturnRequestDocumentType.Hire.ToString()))
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                    dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.ReturnIndex = 1;
                }

                if (cmbDocType.SelectedItem.ToString().Equals(CommonUIDefiniton.ReturnRequestDocumentType.Request.ToString()))
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GeneralRequest);
                    dataSource = CHNLSVC.CommonSearch.GetGeneralRequestSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.ReturnIndex = 0;
                }
                DataTable _result = dataSource;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocumentNo;
                _CommonSearch.ShowDialog();
                txtDocumentNo.Select();
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                //p_com in NVARCHAR2,p_pcenter IN NVARCHAR2,p_doctype in NVARCHAR2,p_invoicetype in NVARCHAR2,p_isHire in NUMBER
                case CommonUIDefiniton.SearchUserControlType.WarraWarrantyNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarraSerialNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GeneralRequest:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.SalesType.INV.ToString() + seperator + CommonUIDefiniton.InvoiceType.HS.ToString() + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HireInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.SalesType.INV.ToString() + seperator + CommonUIDefiniton.InvoiceType.HS.ToString() + seperator + 1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + InvoiceType + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + InvoiceType + seperator + txtPB.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceItem:
                    {
                        paramsText.Append(txtPB.Text + seperator + txtPLevel.Text + seperator + 1 + seperator + DateTime.Now.Date.ToShortDateString() + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text + seperator + txtPLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeJob:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("SRN" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumentNo.Text)) return;

                grvReturningDetails.DataSource = null;
                _selectedItemList = new List<ReptPickSerials>();
                grvIssuingDetails.DataSource = null;
                _invoiceItemList = new List<InvoiceItem>();
                if (cmbDocType.SelectedItem.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Cash.ToString())
                {
                    HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(txtDocumentNo.Text);
                    if (account != null && account.Hpa_stus == "A")
                    {
                        BindAccountItem(txtDocumentNo.Text.Trim(), false);
                        List<InvoiceHeader> _hdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text);
                        if (_hdr != null)
                        {

                            if (_hdr.Count > 0)
                            {
                                InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_hdr[0].Sah_inv_no)[0];
                                txtPB.Text = _invItem.Sad_pbook;
                                txtPLevel.Text = _invItem.Sad_pb_lvl;
                                lblPB.Text = _invItem.Sad_pbook;
                                lblPblevel.Text = _invItem.Sad_pb_lvl;
                                SSPriceBook = _invItem.Sad_pbook;
                                SSPriceLevel = _invItem.Sad_pb_lvl;
                                LoadInLevelStatus("CS", lblPB.Text.ToUpper(), lblPblevel.Text.ToUpper());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid invoice number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account no is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (cmbDocType.SelectedItem.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Hire.ToString())
                {
                    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtDocumentNo.Text.Trim().ToUpper());
                    if (_acc != null)
                    {

                        BindAccountItem(txtDocumentNo.Text.Trim(), true);
                        List<InvoiceHeader> _hdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text);
                        if (_hdr != null)
                            if (_hdr.Count > 0)
                            {
                                InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_hdr[0].Sah_inv_no)[0];
                                txtPB.Text = _invItem.Sad_pbook;
                                txtPLevel.Text = _invItem.Sad_pb_lvl;
                                lblPB.Text = _invItem.Sad_pbook;
                                lblPblevel.Text = _invItem.Sad_pb_lvl;

                                SSPriceBook = _invItem.Sad_pbook;
                                SSPriceLevel = _invItem.Sad_pb_lvl;
                                LoadInLevelStatus("CS", lblPB.Text.ToUpper(), lblPblevel.Text.ToUpper());
                            }
                    }
                    else
                    {
                        MessageBox.Show("Invalid account number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (cmbDocType.SelectedItem.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Request.ToString())
                {

                    BindAccountItem(txtDocumentNo.Text.Trim(), false);
                    BindItemsFromRequest(txtDocumentNo.Text.Trim());

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

        private void BindAccountItem(string _account, bool _isHireSale)
        {
            grvTrade.AutoGenerateColumns = false;
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            InvoiceHeader _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_account);

            if (_isHireSale)
            {

                if (_hdrs != null)
                    _account = _hdrs.Sah_anal_2;
                List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);

                DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
                if (_table.Rows.Count <= 0)
                {
                    //_table = SetEmptyRow(_table);
                    grvTrade.DataSource = _table;
                }
                else if (_invoice != null)
                    if (_invoice.Count > 0)
                    {
                        _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                        #region New Method
                        var _sales = from _lst in _invoice
                                     where _lst.Sah_direct == true
                                     select _lst;

                        foreach (InvoiceHeader _hdr in _sales)
                        {
                            string _invoiceno = _hdr.Sah_inv_no;
                            List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                            var _forwardSale = from _lst in _invItem
                                               where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                               select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                            var _deliverdSale = from _lst in _invItem
                                                where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                                select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                            if (_forwardSale.Count() > 0)
                                _itemList.AddRange(_forwardSale);

                            if (_deliverdSale.Count() > 0)
                                _itemList.AddRange(_deliverdSale);
                        }
                        #endregion
                        grvTrade.DataSource = _itemList;
                    }

            }
            else
            {
                if (cmbDocType.SelectedItem.ToString() == "Cash")
                {
                    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_account);
                    List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_acc.Hpa_invc_no);
                    if (_invItem != null)
                    {
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);
                    }

                }
                else if (cmbDocType.SelectedItem.ToString() == "Request")
                {
                    RequestApprovalHeader _hdrss = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                    lblRequestStatus.Text = _hdrss.Grah_app_stus;

                    //get request type
                    if (_hdrss.Grah_app_tp == "ARQT008")
                    {

                        txtSubType.Text = _hdrss.Grah_sub_type;
                        txtremarks.Text = _hdrss.Grah_remaks;
                        //get permission level
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSEXCH, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

                        if (BaseCls.GlbReqIsFinalApprovalUser)
                        {
                            btnReject.Enabled = true;
                            btnSave.Text = "Approve";
                        }
                        else
                        {
                            btnReject.Enabled = false;
                            btnSave.Text = "Process";
                        }

                    }
                    else if (_hdrss.Grah_app_tp == "ARQT013")
                    {
                        //get permission level
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

                        if (BaseCls.GlbReqIsFinalApprovalUser)
                        {
                            btnReject.Enabled = true;
                            btnSave.Text = "Approve";
                        }
                        else
                        {
                            btnReject.Enabled = false;
                            btnSave.Text = "Process";
                        }

                    }

                    if (_hdrss.Grah_remaks == "Cash")
                    {
                        HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(_hdrss.Grah_fuc_cd);

                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(account.Hpa_invc_no);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);

                    }
                    //hire
                    else
                    {
                        if (_hdrs != null)
                            _account = _hdrs.Sah_anal_2;
                        List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);

                        DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
                        if (_table.Rows.Count <= 0)
                        {
                            //_table = SetEmptyRow(_table);
                            grvTrade.DataSource = _table;
                        }
                        else if (_invoice != null)
                            if (_invoice.Count > 0)
                            {
                                _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                                #region New Method
                                var _sales = from _lst in _invoice
                                             where _lst.Sah_direct == true
                                             select _lst;

                                foreach (InvoiceHeader _hdr in _sales)
                                {
                                    string _invoiceno = _hdr.Sah_inv_no;
                                    List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                                    var _forwardSale = from _lst in _invItem
                                                       where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                                       select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                                    var _deliverdSale = from _lst in _invItem
                                                        where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                                        select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                                    if (_forwardSale.Count() > 0)
                                        _itemList.AddRange(_forwardSale);

                                    if (_deliverdSale.Count() > 0)
                                        _itemList.AddRange(_deliverdSale);
                                }
                                #endregion
                                grvTrade.DataSource = _itemList;
                            }
                    }
                    //else
                    //{
                    //    if (_hdrss.Grah_app_tp == "ARQT008" || _hdrss.Grah_app_tp == "ARQT013")
                    //    {
                    //        MessageBox.Show("Invalid Request\n\nTECHNICAL INFO:REQUEST NOT GENERATED FROM RETURN REUEST APPROVAL\nHEADER REMARK INVALID -" + _hdrss.Grah_remaks, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Invalid Request\nPlease select Document Type ARQT008 or ARQT013", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}
                }
                grvTrade.DataSource = _itemList;
            }

            if (_hdrs != null)
                BindCustomerDetails(_hdrs);
            else
                if (_isHireSale)
                {
                    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_account);
                    _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_acc.Hpa_invc_no);
                    BindCustomerDetails(_hdrs);
                }

        }

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            try
            {
                lblCusCode.Text = _hdr.Sah_cus_cd;
                lblName.Text = _hdr.Sah_cus_name;
                lblAddress1.Text = _hdr.Sah_cus_add1;
                lblAddress2.Text = _hdr.Sah_cus_add2;
            }
            catch (Exception) { }
        }

        private void BindItemsFromRequest(string _reqno)
        {
            try
            {
                grvReturningDetails.AutoGenerateColumns = false;
                grvIssuingDetails.AutoGenerateColumns = false;
                RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _reqno);
                string _documentType = _hdr.Grah_remaks;
                List<RequestApprovalDetail> _lst = null;

                if (_documentType == "Cash")
                {
                    RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                    _lst = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                }
                else
                    if (_hdr.Grah_app_tp == "ARQT013")
                    {
                        _lst = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                        // RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _lst = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                        _lst = (from _res in _lst
                                where _res.Grad_date_param == _lst.Max(x => x.Grad_date_param)
                                select _res).ToList<RequestApprovalDetail>();
                    }
                    else if (_hdr.Grah_app_tp == "ARQT008")
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSEXCH, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        GlbReqUserPermissionLevel = 3;
                        _lst = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                        //_lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _hdr.Grah_fuc_cd, HirePurchasModuleApprovalCode.ARQT008.ToString(), 0, GlbReqUserPermissionLevel);
                        //_lst = (from _res in _lst
                        //        where _res.Grad_date_param == _lst.Max(x => x.Grad_date_param)
                        //        select _res).ToList<RequestApprovalHeader>();
                    }

                if (_lst != null)
                    if (_lst.Count > 0)
                    {
                        var _in = (from _k in _lst
                                   where _k.Grad_anal5 == "IN"
                                   select _k).ToList();

                        var _out = (from _k in _lst
                                    where _k.Grad_anal5 == "OUT"
                                    select _k).ToList();

                        var _other = (from _k in _lst
                                      where _k.Grad_anal5 == "AMT"
                                      select _k).ToList();

                        if (_other != null && _other.Count > 0)
                        {
                            txtAmount.Text = _other[0].Grad_val2.ToString();

                        }


                        List<ReptPickSerials> _ins = new List<ReptPickSerials>();
                        List<InvoiceItem> _outs = new List<InvoiceItem>();

                        string _usageDocType = string.Empty;
                        decimal _value = 0;

                        if (_in != null)
                            if (_in.Count > 0)
                                foreach (RequestApprovalDetail _one in _in)
                                {
                                    ReptPickSerials _single = new ReptPickSerials();
                                    //ra_det.Grad_ref = ra_hdr.Grah_ref;
                                    //ra_det.Grad_line = _count;
                                    //ra_det.Grad_req_param = _in.Tus_itm_cd;

                                    //ra_det.Grad_val1 = _in.Tus_qty;
                                    //ra_det.Grad_val2 = _in.Tus_unit_price;
                                    //ra_det.Grad_val3 = _single.Tus_base_itm_line;
                                    //ra_det.Grad_val4 = _single.Tus_ser_line;
                                    //ra_det.Grad_val5 = _single.Tus_ser_id;

                                    //ra_det.Grad_anal1 = txtADocumentNo.Text;
                                    //ra_det.Grad_anal2 = _in.Tus_base_doc_no;
                                    //ra_det.Grad_anal3 = _in.Tus_doc_no;
                                    //ra_det.Grad_anal4 = _in.Tus_ser_1;
                                    //ra_det.Grad_anal5 = "IN";
                                    _single.Tus_itm_cd = _one.Grad_req_param;
                                    _single.Tus_qty = _one.Grad_val1;
                                    _single.Tus_unit_cost = _one.Grad_val2;
                                    _single.Tus_base_itm_line = Convert.ToInt32(_one.Grad_val3);
                                    _single.Tus_ser_line = Convert.ToInt32(_one.Grad_val4);
                                    _single.Tus_ser_id = Convert.ToInt32(_one.Grad_val5);
                                    _single.Tus_base_doc_no = _one.Grad_anal2;
                                    _single.Tus_doc_no = _one.Grad_anal3;
                                    _single.Tus_ser_1 = _one.Grad_anal4;
                                    _single.Tus_itm_stus = _one.Grad_anal7;
                                    _single.Tus_ser_2 = _one.Grad_anal8;
                                    _ins.Add(_single);

                                    List<InvoiceItem> _invItm = CHNLSVC.Sales.GetInvoiceItems(_one.Grad_anal2);

                                    LoadAppLevelStatus("CS", _invItm[0].Sad_pbook, _invItm[0].Sad_pb_lvl);


                                    List<InvoiceItem> _temp = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_one.Grad_anal2);
                                    List<InvoiceItem> invIte = _temp.Where(x => x.Sad_itm_cd == _one.Grad_req_param).ToList<InvoiceItem>();

                                    txtPB.Text = invIte[0].Sad_pbook;
                                    txtPB.Enabled = false;
                                    txtPLevel.Text = invIte[0].Sad_pb_lvl;
                                    txtPLevel.Enabled = false;
                                    lblPB.Text = invIte[0].Sad_pbook;
                                    lblPblevel.Text = invIte[0].Sad_pb_lvl;
                                    SSPriceBook = txtPB.Text;
                                    SSPriceLevel = txtPLevel.Text;
                                }

                        if (_out != null)
                            if (_out.Count > 0)
                                foreach (RequestApprovalDetail _one in _out)
                                {

                                    InvoiceItem _single = new InvoiceItem();
                                    _single.Sad_itm_cd = _one.Grad_req_param;
                                    _single.Sad_qty = _one.Grad_val1;
                                    _single.Sad_unit_rt = _one.Grad_val2;
                                    _single.Sad_unit_amt = _one.Grad_val2 + _one.Grad_val4;
                                    _single.Sad_pb_price = _one.Grad_val3;
                                    _single.Sad_tot_amt = (_one.Grad_val2 + _one.Grad_val4) * _one.Grad_val1;
                                    _single.Sad_itm_tax_amt = _one.Grad_val4;
                                    _single.Sad_seq = Convert.ToInt32(_one.Grad_val5);
                                    _single.Sad_pbook = _one.Grad_anal9;
                                    _single.Sad_pb_lvl = _one.Grad_anal10;


                                    _single.Sad_itm_stus = _one.Grad_anal2;
                                    _single.Sad_promo_cd = _one.Grad_anal3;
                                    _single.Sad_seq_no = Convert.ToInt32(_one.Grad_anal4);

                                    _outs.Add(_single);

                                }

                        if (_other != null)
                            if (_other.Count > 0)
                                foreach (RequestApprovalDetail _one in _other)
                                {
                                    _usageDocType = _one.Grad_req_param;
                                    _value = _one.Grad_val2;
                                }

                        if (_outs != null)
                            if (_outs.Count > 0)
                            {
                                grvReturningDetails.DataSource = _ins;
                                _selectedItemList = _ins;


                                grvIssuingDetails.DataSource = _outs;
                                _invoiceItemList = _outs;


                            }

                        if (_ins != null)
                            if (_ins.Count > 0)
                            {
                                grvIssuingDetails.DataSource = _outs;
                                _invoiceItemList = _outs;

                                grvReturningDetails.DataSource = _ins;
                                _selectedItemList = _ins;

                            }
                        if (string.IsNullOrEmpty(_usageDocType))
                        {
                            cmbUsage.SelectedItem = _usageDocType;
                            txtAmount.Text = FormatToCurrency(_value.ToString());
                        }
                        CalculateTotalNewandOldAmount();
                        //grvReturningDetails.ReadOnly = true;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while Processing.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void grvTrade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (string.IsNullOrEmpty(cmbInStus.Text))
                    {
                        MessageBox.Show("Please select the item status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbItemStatus.Focus();
                        return;
                    } 

                    if (string.IsNullOrEmpty(cmbInStus.SelectedItem.ToString()))
                    {
                        MessageBox.Show("Please select the item status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbItemStatus.Focus();
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    //DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no"
                    string _sad_itm_cd = grvTrade.Rows[e.RowIndex].Cells[1].Value.ToString();
                    decimal _sad_qty = Convert.ToDecimal(grvTrade.Rows[e.RowIndex].Cells[4].Value);
                    decimal _sad_unit_rt = Convert.ToDecimal(grvTrade.Rows[e.RowIndex].Cells[6].Value);
                    Int32 _sad_itm_line = Convert.ToInt32(grvTrade.Rows[e.RowIndex].Cells[5].Value);
                    bool _isForwardSale = Convert.ToBoolean(grvTrade.Rows[e.RowIndex].Cells[7].Value);
                    string _invoice = grvTrade.Rows[e.RowIndex].Cells[8].Value.ToString();
                    string _status = grvTrade.Rows[e.RowIndex].Cells[9].Value.ToString();
                    decimal _total = Convert.ToDecimal(grvTrade.Rows[e.RowIndex].Cells[10].Value);
                    MasterItem _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _sad_itm_cd);

                    string promocode = grvTrade.Rows[e.RowIndex].Cells["_promo"].Value.ToString();
                    if (!string.IsNullOrEmpty(promocode) && _sad_unit_rt > 0)
                    {
                        if (promocode != "N/A")
                        {
                            SalesReversal = true;
                        }
                    }


                    if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == false)
                    {

                        if (_itms.Mi_is_ser1 == -1)
                        {
                            MessageBox.Show("Exchange not processing for decimal allow items", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        List<InventoryBatchN> _lst = new List<InventoryBatchN>();
                        _lst = CHNLSVC.Inventory.GetDeliveryOrderDetail(BaseCls.GlbUserComCode, _invoice, _sad_itm_line);

                        string _docno = string.Empty;
                        int _itm_line = -1;
                        int _batch_line = -1;

                        List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerialForReversal(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, string.Empty, _invoice, _sad_itm_line);
                        _popUpList = _serLst;
                        if (_lst != null && _serLst != null)
                            if (_sad_qty > 1)
                            {
                                //More than one qty
                                BindPopSerial(_serLst, false);
                                pnlPopUp.Visible = true;
                                pnlMain.Enabled = false;
                            }
                            else
                            {
                                //only one qty
                                _docno = _lst[0].Inb_doc_no;
                                _itm_line = _lst[0].Inb_itm_line;
                                _batch_line = _lst[0].Inb_batch_line;
                                AddInItem(_serLst[0]);
                                foreach (ReptPickSerials _lt in _popUpList)
                                {
                                    string _item = _lt.Tus_itm_cd;
                                    Int32 _serialID = _lt.Tus_ser_id;
                                    MasterItem _items = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    if (_items.Mi_is_ser1 != -1)
                                    {
                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_ser_id == Convert.ToInt32(_serialID)
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lssst in _selectedItemList
                                                                         where _lssst.Tus_ser_id == _one.Tus_ser_id
                                                                         select _lssst;

                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _one.Tus_ser_2 = cmbInStus.SelectedValue.ToString(); ;
                                                            _one.Tus_unit_cost = _total;
                                                            _selectedItemList.Add(_one);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _one.Tus_ser_2 = cmbInStus.SelectedValue.ToString(); ;
                                                        _one.Tus_unit_cost = _total;
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }
                                    else
                                    {

                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_itm_cd == _item
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lsst in _selectedItemList
                                                                         where _lsst.Tus_itm_cd == _one.Tus_itm_cd
                                                                         select _lsst;
                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _one.Tus_ser_2 = cmbInStus.SelectedValue.ToString();
                                                            _one.Tus_unit_cost = _total;
                                                            _selectedItemList.Add(_one);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _one.Tus_ser_2 = cmbInStus.SelectedValue.ToString();
                                                        _one.Tus_unit_cost = _total;
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }

                                }

                            }
                        BindSelectedItems(_selectedItemList);
                    }
                    else if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == true)
                    {
                        if (_sad_qty > 1)
                        {

                            var _duplicate = from _lsst in _selectedItemList
                                             where _lsst.Tus_itm_cd == _sad_itm_cd && _lsst.Tus_itm_stus == _status && _lsst.Tus_unit_price == _sad_unit_rt
                                             select _lsst;
                            if (_duplicate.Count() <= 0)
                            {

                                //string Msg = "<script>var name = prompt('Please enter your name', " + _sad_qty + ");if (name!=null && name!='') { document.getElementById('" + hdnUserQty.ClientID + "').value=name; } else { document.getElementById('<%=hdnUserQty.ClientID%>').value='-1';}  </script>";


                                //if (hdnUserQty.Value == "-1" || string.IsNullOrEmpty(hdnUserQty.Value.ToString())) return;

                                ReptPickSerials _one = new ReptPickSerials();
                                _one.Tus_base_doc_no = _invoice;
                                _one.Tus_base_itm_line = _sad_itm_line;
                                _one.Tus_bin = string.Empty;
                                _one.Tus_com = BaseCls.GlbUserComCode;
                                _one.Tus_cre_by = BaseCls.GlbUserID;
                                _one.Tus_cre_dt = DateTime.Now.Date;
                                _one.Tus_cross_batchline = -1;
                                _one.Tus_cross_itemline = -1;
                                _one.Tus_cross_seqno = -1;
                                _one.Tus_cross_serline = -1;
                                _one.Tus_doc_dt = DateTime.Now.Date;
                                _one.Tus_doc_no = _invoice;
                                _one.Tus_exist_grncom = string.Empty;
                                _one.Tus_exist_grndt = DateTime.Now.Date;
                                _one.Tus_exist_grnno = string.Empty;
                                _one.Tus_exist_supp = string.Empty;
                                _one.Tus_itm_brand = string.Empty;
                                _one.Tus_itm_cd = _sad_itm_cd;
                                _one.Tus_itm_desc = _itms.Mi_longdesc;
                                _one.Tus_itm_line = -1;
                                _one.Tus_itm_model = _itms.Mi_model;
                                _one.Tus_itm_stus = _status;
                                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                                _one.Tus_new_remarks = string.Empty;
                                _one.Tus_new_status = string.Empty;
                                _one.Tus_orig_grncom = string.Empty;
                                _one.Tus_orig_grndt = DateTime.Now.Date;
                                _one.Tus_orig_grnno = string.Empty;
                                _one.Tus_orig_supp = string.Empty;
                                _one.Tus_out_date = DateTime.Now.Date;
                                _one.Tus_qty = Convert.ToDecimal(1);
                                _one.Tus_seq_no = -1;
                                _one.Tus_ser_1 = string.Empty;
                                _one.Tus_ser_2 = cmbInStus.SelectedValue.ToString(); ;
                                _one.Tus_ser_3 = string.Empty;
                                _one.Tus_ser_4 = string.Empty;
                                _one.Tus_ser_id = -1;
                                _one.Tus_ser_line = -1;
                                _one.Tus_serial_id = _serialId++.ToString();
                                _one.Tus_session_id = BaseCls.GlbUserSessionID;
                                _one.Tus_unit_cost = _total;
                                _one.Tus_unit_price = _sad_unit_rt;
                                _one.Tus_usrseq_no = -1;
                                _one.Tus_warr_no = string.Empty;
                                _one.Tus_warr_period = 0;

                                _selectedItemList.Add(_one);

                            }

                        }
                        else
                        {

                            var _duplicate = from _lsst in _selectedItemList
                                             where _lsst.Tus_itm_cd == _sad_itm_cd && _lsst.Tus_itm_stus == _status && _lsst.Tus_unit_price == _sad_unit_rt
                                             select _lsst;

                            if (_duplicate.Count() <= 0)
                            {

                                ReptPickSerials _one = new ReptPickSerials();
                                _one.Tus_base_doc_no = _invoice;
                                _one.Tus_base_itm_line = _sad_itm_line;
                                _one.Tus_bin = string.Empty;
                                _one.Tus_com = BaseCls.GlbUserComCode;
                                _one.Tus_cre_by = BaseCls.GlbUserID;
                                _one.Tus_cre_dt = DateTime.Now.Date;
                                _one.Tus_cross_batchline = -1;
                                _one.Tus_cross_itemline = -1;
                                _one.Tus_cross_seqno = -1;
                                _one.Tus_cross_serline = -1;
                                _one.Tus_doc_dt = DateTime.Now.Date;
                                _one.Tus_doc_no = _invoice;
                                _one.Tus_exist_grncom = string.Empty;
                                _one.Tus_exist_grndt = DateTime.Now.Date;
                                _one.Tus_exist_grnno = string.Empty;
                                _one.Tus_exist_supp = string.Empty;
                                _one.Tus_itm_brand = string.Empty;
                                _one.Tus_itm_cd = _sad_itm_cd;
                                _one.Tus_itm_desc = _itms.Mi_longdesc;
                                _one.Tus_itm_line = -1;
                                _one.Tus_itm_model = _itms.Mi_model;
                                _one.Tus_itm_stus = _status;
                                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                                _one.Tus_new_remarks = string.Empty;
                                _one.Tus_new_status = string.Empty;
                                _one.Tus_orig_grncom = string.Empty;
                                _one.Tus_orig_grndt = DateTime.Now.Date;
                                _one.Tus_orig_grnno = string.Empty;
                                _one.Tus_orig_supp = string.Empty;
                                _one.Tus_out_date = DateTime.Now.Date;
                                _one.Tus_qty = _sad_qty;
                                _one.Tus_seq_no = -1;
                                _one.Tus_ser_1 = string.Empty;
                                _one.Tus_ser_2 = cmbInStus.SelectedValue.ToString(); ;
                                _one.Tus_ser_3 = string.Empty;
                                _one.Tus_ser_4 = string.Empty;
                                _one.Tus_ser_id = -1;
                                _one.Tus_ser_line = -1;
                                _one.Tus_serial_id = _serialId++.ToString();
                                _one.Tus_session_id = BaseCls.GlbUserSessionID;
                                _one.Tus_unit_cost = _total;
                                _one.Tus_unit_price = _sad_unit_rt;
                                _one.Tus_usrseq_no = -1;
                                _one.Tus_warr_no = string.Empty;
                                _one.Tus_warr_period = 0;

                                _selectedItemList.Add(_one);
                            }
                        }
                        BindSelectedItems(_selectedItemList);
                    }
                    CalculateTotalNewandOldAmount();
                    this.Cursor = Cursors.Default;

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void BindPopSerial(List<ReptPickSerials> _list, bool _isByRequest)
        {
            if (_isByRequest == false)
            {
                grvPopSerial.AutoGenerateColumns = false;
                grvPopSerial.DataSource = _list;

            }
            else
            {

            }
        }
        private void BindSelectedItems(List<ReptPickSerials> _list)
        {
            grvReturningDetails.AutoGenerateColumns = false;
            BindingSource source = new BindingSource();
            source.DataSource = _list;
            if (_list == null) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserName, -1, string.Empty); grvReturningDetails.DataSource = _table; }
            else if (_list.Count <= 0) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserName, -1, string.Empty); grvReturningDetails.DataSource = _table; }
            else { grvReturningDetails.DataSource = source; }

        }

        private void AddInItem(ReptPickSerials _ser)
        {
            _popUpList.Add(_ser);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPriceBookChanged)
                {
                    MessageBox.Show("Price book not allow to enter request", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AddItem(SSPromotionCode == "0" ? false : true);
                CalculateTotalNewandOldAmount();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CalculateTotalNewandOldAmount()
        {
            decimal _inAmt = 0;
            decimal _outAmt = 0;
            if (_selectedItemList != null)
                if (_selectedItemList.Count > 0)
                {
                    var _sumReturn = (from _l in _selectedItemList
                                      select _l.Tus_unit_cost * _l.Tus_qty).Sum();
                    _inAmt = _sumReturn;
                    lblOldTotal.Text = FormatToCurrency(_sumReturn.ToString());
                }
                else lblOldTotal.Text = "0.00";


            if (_invoiceItemList != null)
                if (_invoiceItemList.Count > 0)
                {
                    var _sumIssue = (from _l in _invoiceItemList
                                     select _l.Sad_unit_amt).Sum();
                    _outAmt = _sumIssue;
                    lblNewTotal.Text = FormatToCurrency(_sumIssue.ToString());
                }
                else lblNewTotal.Text = "0.00";

            lblDiffreence.Text = _outAmt - _inAmt <= 0 ? FormatToCurrency(((_outAmt - _inAmt) * -1).ToString()) : FormatToCurrency((_outAmt - _inAmt).ToString());

            if (_outAmt - _inAmt < 0)
            {
                cmbUsage.SelectedItem = "Usage Charge";
                //cmbUsage.Enabled = false;
            }
        }

        protected void CheckQty()
        {
            if (string.IsNullOrEmpty(txtQty.Text)) return;
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text) || Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; return; }



            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please select the item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Focus();
                return;
            }



            if (string.IsNullOrEmpty(cmbItemStatus.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select the item status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbItemStatus.Focus();
                return;
            }

            //Load Price Level Details
            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, SSPriceBook, SSPriceLevel);

            //Check for tax setup  - under darshana confirmation on 02/06/2012
            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString(), string.Empty, string.Empty);
            if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
            {
                MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact costing dept.");
                cmbItemStatus.Focus();
                return;
            }

            bool _isMRP = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text).Mi_anal3;

            if (_isMRP)
            {

                MessageBox.Show("Consumer products not allow here", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            if (_priceBookLevelRef.Sapl_is_serialized)
            {
                //Serialized price level
                //User directing for select the serial from pick the serials,
                //It should fire after enter item code, and without enter qty.
                //After selecting serial, the selected serials will goes to DO grid and the items will add to the sales entry end.

                //The event should be performed in lostforcus of the item as same at the "Add Item"  button
                List<PriceSerialRef> _list = CHNLSVC.Sales.GetAllPriceSerial(SSPriceBook, SSPriceLevel, txtItemCode.Text, dateTimePickerDate.Value.Date, string.Empty, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list.Count < Convert.ToDecimal(txtQty.Text))
                {
                    MessageBox.Show("Selected qty is exceeds available serials!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Text = "Qty";
                    txtQty.Focus();
                    IsNoPriceDefinition = true;
                    return;
                }
                if (_list.Count > 0)
                {

                    BindSerializedPriceSerial(_list);
                    grvPopSerialPricePick.Visible = true;
                    grvPopPricePick.Visible = false;
                    gvPopConsumPricePick.Visible = false;
                    gvItemInventoryCombine.Visible = false;

                    IsNoPriceDefinition = false;
                    pnlMain.Enabled = false;
                    pnlPopUp2.Visible = true;
                    return;
                }
                return;
            }

            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, InvoiceType, SSPriceBook, SSPriceLevel, string.Empty, txtItemCode.Text, Convert.ToDecimal(txtQty.Text), dateTimePickerDate.Value.Date);

            if (_priceDetailRef.Count <= 0)
            {
                //Msg for no price define
                MessageBox.Show("There is no price for the selected item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IsNoPriceDefinition = true;
                return;
            }
            else
            {
                if (_priceDetailRef.Count > 1)
                {
                    //Find More than one price for the selected item
                    //Load price prices for the grid and popup for user confirmation

                    /*
                    IsNoPriceDefinition = false;
                    grvPopSerialPricePick.Visible = false;
                    grvPopPricePick.Visible = true;
                    gvPopConsumPricePick.Visible = false;
                    gvItemInventoryCombine.Visible = false;
                    gvPopConsumPricePick.Visible = false;
                    pnlMain.Enabled = false;
                    pnlPopUp2.Visible = true;
                   
                    BindPriceSerial(_priceDetailRef);
                     */
                    try
                    {
                        var _one = (from _res in _priceDetailRef
                                    where string.IsNullOrEmpty(_res.Sapd_promo_cd)
                                    select _res).ToList<PriceDetailRef>();

                        if (_one == null || _one.Count <= 0)
                        {
                            MessageBox.Show("Normal price details not available", "Warning", MessageBoxButtons.OK);
                            return;
                        }

                        _one = (from _itm in _one
                                where _itm.Sapd_mod_when == (from d in _one select d.Sapd_mod_when).Max()
                                select _itm).ToList<PriceDetailRef>();



                        if (_one == null || _one.Count <= 0)
                        {
                            MessageBox.Show("Price details not available");
                            return;
                        }

                        if (_one[0].Sapd_price_stus == "S")
                        {
                            MessageBox.Show("Price has been suspended");
                            return;
                        }

                        decimal UnitPrice = TaxCalculation(txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _one[0].Sapd_itm_price, false);

                        txtUnitRate.Text = FormatToCurrency(UnitPrice.ToString());
                        SSPriceBookPrice = UnitPrice;
                        SSPriceBookSequance = _one[0].Sapd_pb_seq.ToString();
                        SSPriceBookItemSequance = _one[0].Sapd_seq_no.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error at multiple price detail processing...\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CHNLSVC.CloseChannel();

                    }
                    //MessageBox.Show("Promotional Items can not add", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (_priceDetailRef.Count == 1)
                {

                    var _one = from _itm in _priceDetailRef
                               select _itm;
                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;

                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        if (_single.Sapd_price_stus == "S")
                        {
                            MessageBox.Show("Price has been suspended");
                            return;
                        }

                        //Tax Calculation
                        decimal UnitPrice = TaxCalculation(txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, false);

                        txtUnitRate.Text = FormatToCurrency(UnitPrice.ToString());
                        SSPriceBookPrice = UnitPrice;
                        SSPriceBookSequance = _single.Sapd_pb_seq.ToString();
                        SSPriceBookItemSequance = _single.Sapd_seq_no.ToString();

                        Int32 _pbSq = _single.Sapd_pb_seq;
                        string _mItem = _single.Sapd_itm_cd;

                        //If Promotion Available
                        if (_promotion.Sarpt_is_com)
                        {
                            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                            _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSq, _mItem, string.Empty);
                            if (_tempPriceCombinItem != null)
                            {
                                /*
                                grvPriceItemCombine.DataSource = _tempPriceCombinItem;
                                grvPriceItemCombine.Visible = true;
                                grvPopSerialPricePick.Visible = false;
                                grvPopPricePick.Visible = false;
                                gvPopConsumPricePick.Visible = false;
                                gvItemInventoryCombine.Visible = false;
                                pnlPopUp2.Visible = true;
                                pnlMain.Enabled = false;
                                 */
                                MessageBox.Show("Promotional Items can not add", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                grvPriceItemCombine.Visible = false;
                                lblMsg.Text = "There is no such combine items pick";
                                pnlPopUp2.Visible = true;
                                pnlMain.Enabled = false;
                                return;
                            }
                        }
                        else
                        {
                            txtUnitRate.Focus();
                        }
                    }
                }
            }
            _isEditPrice = false;
            _isEditDiscount = false;

            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "Qty";

            txtQty.Text = FormatToQty(txtQty.Text);
            CalculateItem();
        }

        protected void BindPriceSerial(List<PriceDetailRef> _list)
        {
            grvPopPricePick.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _list;
            grvPopPricePick.DataSource = _source;

        }

        protected void BindSerializedPriceSerial(List<PriceSerialRef> _list)
        {
            BindingSource _source = new BindingSource();
            _source.DataSource = _list;
            grvPopSerialPricePick.DataSource = _source;

        }

        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;

            }
            return _list;
        }

        private void btnInvCombineSerClose_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = false;
            pnlMain.Enabled = true;
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, bool _isTaxPotion)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxPotion == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString()); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxPotion == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                    }
                }
                else
                    if (_isTaxPotion) _pbUnitPrice = 0;

            return _pbUnitPrice;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                decimal _vatPortion = TaxCalculation(txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitRate.Text.Trim()), true);
                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitRate.Text);
                decimal _disAmt = 0;

                _totalAmount = _totalAmount + _vatPortion - _disAmt;

                txtUnitAmt.Text = FormatToCurrency(_totalAmount.ToString());
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCode;
                _CommonSearch.ShowDialog();
                txtItemCode.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void AddItem(bool _isPromotion)
        {

            if (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) { MessageBox.Show("Please select the valid price", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrEmpty(txtQty.Text.Trim())) { MessageBox.Show("Please select the valid qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (Convert.ToDecimal(txtQty.Text) == 0) { MessageBox.Show("Please select the valid qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { MessageBox.Show("Please select the valid qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (string.IsNullOrEmpty(txtUnitRate.Text.Trim())) { MessageBox.Show("Please select the valid unit price", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }


            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                MessageBox.Show("Please select the item");
                txtItemCode.Focus();
                return;
            }



            if (string.IsNullOrEmpty(cmbItemStatus.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select the item status");
                cmbItemStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please select the qty");
                txtQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtUnitRate.Text))
            {
                MessageBox.Show("Please select the unit price");
                txtUnitRate.Focus();
                return;
            }



            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString(), string.Empty, string.Empty);
            if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
            {
                MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact costing dept.");
                cmbItemStatus.Focus();
                return;
            }


            CalculateItem();



            if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
            //No Records
            {
                _lineNo += 1;
                if (!_isCombineAdding) SSCombineLine = _lineNo;
                _invoiceItemList.Add(AssignDataToObject(_isPromotion));
            }
            else
            //Having some records
            {
                var _similerItem = from _list in _invoiceItemList
                                   where _list.Sad_itm_cd == txtItemCode.Text && _list.Sad_itm_stus == cmbItemStatus.SelectedItem.ToString() && _list.Sad_pbook == SSPriceBook && _list.Sad_pb_lvl == SSPriceLevel && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitRate.Text)
                                   select _list;

                if (_similerItem.Count() > 0)
                //Similer item available
                {
                    foreach (var _similerList in _similerItem)
                    {
                        _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(0);//txtDiscountAmt.Text
                        _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(0);//txtVATAmt.Text
                        _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                        _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtUnitAmt.Text);

                    }
                }
                else
                //No similer item found
                {
                    _lineNo += 1;
                    if (!_isCombineAdding) SSCombineLine = _lineNo;
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion));
                }

            }

            //TODO: VAT amount
            // CalculateGrandTotal(Convert.ToDecimal(txtOutQty.Text), Convert.ToDecimal(txtOutUnitRate.Text), 0, 0, true);


            if (_MainPriceCombinItem != null)
            {
                string _combineStatus = string.Empty;
                decimal _combineQty = 0;

                if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                {
                    _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbItemStatus.SelectedItem.ToString();
                    if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);

                    foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                    {
                        txtItemCode.Text = _list.Sapc_itm_cd;
                        // txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                        cmbItemStatus.SelectedItem = _combineStatus;
                        txtUnitRate.Text = _list.Sapc_price.ToString();
                        txtQty.Text = (_list.Sapc_qty * _combineQty).ToString();
                        txtUnitAmt.Text = "";
                        CalculateItem();
                        AddItem(_isPromotion);
                        _combineCounter += 1;


                    }
                }

                if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); SSCombineLine = 0; }
            }
            //TODO: Check for delivery now! tag

            ClearAfterAddItem();
            txtItemCode.Focus();
            BindAddItem();
            SetDecimalTextBoxForZero(true);


        }

        private void SetDecimalTextBoxForZero(bool _isUnit)
        {
            txtQty.Text = "";
            txtUnitAmt.Text = "";
            if (_isUnit) txtUnitRate.Text = "";
        }

        protected void BindAddItem()
        {
            //if (_invoiceItemList.Count > 0)
            //{
            grvIssuingDetails.AutoGenerateColumns = false;
            BindingSource source = new BindingSource();
            source.DataSource = _invoiceItemList;
            grvIssuingDetails.DataSource = source;
            //}
        }

        private void ClearAfterAddItem()
        {
            txtItemCode.Text = "";
            cmbItemStatus.DataSource = null;
            txtQty.Text = "";
            txtUnitRate.Text = "";
            txtUnitAmt.Text = "";

        }

        private InvoiceItem AssignDataToObject(bool _isPromotion)
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            InvoiceItem _tempItem = new InvoiceItem();
            _tempItem.Sad_alt_itm_cd = "";
            _tempItem.Sad_alt_itm_desc = "";
            _tempItem.Sad_comm_amt = 0;
            _tempItem.Sad_disc_amt = 0;
            _tempItem.Sad_disc_rt = 0;
            _tempItem.Sad_do_qty = 0;
            _tempItem.Sad_fws_ignore_qty = 0;
            _tempItem.Sad_inv_no = "";
            _tempItem.Sad_is_promo = _isPromotion;
            _tempItem.Sad_itm_cd = txtItemCode.Text;
            _tempItem.Sad_itm_line = _lineNo;
            _tempItem.Sad_itm_seq = 0;//Convert.ToInt32(SSPriceBookItemSequance);
            _tempItem.Sad_itm_stus = cmbItemStatus.SelectedItem.ToString();
            //TODO: Vat Amount
            _tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtUnitAmt.Text) - Convert.ToDecimal(txtUnitRate.Text);
            _tempItem.Sad_itm_tp = "";
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_merge_itm = "";
            _tempItem.Sad_pb_lvl = txtPLevel.Text.Trim();
            _tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Sad_pbook = txtPB.Text.Trim();
            _tempItem.Sad_print_stus = false;
            _tempItem.Sad_promo_cd = SSPromotionCode;
            _tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            _tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Sad_seq_no = 0;
            _tempItem.Sad_srn_qty = 0;
            _tempItem.Sad_tot_amt = Convert.ToDecimal(txtUnitAmt.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitAmt.Text);
            _tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitRate.Text);
            _tempItem.Sad_uom = "";
            _tempItem.Sad_warr_based = false;
            _tempItem.Sad_warr_period = 0;
            _tempItem.Sad_warr_remarks = "";
            //TODO: description
            _tempItem.Mi_longdesc = "";
            _tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            return _tempItem;
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                CheckQty();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDocument_Click(null, null);
            }
        }

        private void txtDocumentNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDocument_Click(null, null);
        }



        private void txtPLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPLevel_Click(null, null);
        }

        private void txtPLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPLevel_Click(null, null);
        }

        private void txtPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPB_Click(null, null);
        }

        private void txtPB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPB_Click(null, null);
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItem_Click(null, null);
        }

        private void txtItemCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem_Click(null, null);
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                bool isGenerate;
                if (_selectedItemList == null)
                    if (_selectedItemList.Count <= 0)
                    {
                        MessageBox.Show("Please select the return item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                if (_invoiceItemList == null && (cmbDocType.SelectedItem.ToString() == "Hire" || cmbDocType.SelectedItem.ToString() == "Request"))
                    if (_invoiceItemList.Count <= 0)
                    {
                        MessageBox.Show("Please select the issue item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    MessageBox.Show("Please select the usage amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (grvReturningDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("Returning items can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblError, dateTimePickerDate.Value.ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dateTimePickerDate.Value.Date != _date.Date)
                        {
                            MessageBox.Show("Back date not allow for selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Back date not allow for selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (cmbUsage.SelectedItem.ToString().ToUpper() == "DISCOUNT" && _invoiceItemList.Sum(x => x.Sad_unit_rt + x.Sad_itm_tax_amt) < Convert.ToDecimal(txtAmount.Text))
                {
                    MessageBox.Show("You can not enter discount greater than Issuing item Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    MessageBox.Show("Category Type cannot be empty", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014;
                string _documentType = string.Empty;


                if (cmbDocType.SelectedItem.ToString() == "Cash")
                {
                    RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                    _documentType = "Cash";
                }

                if (grvReturningDetails.Rows.Count == 1 && grvIssuingDetails.Rows.Count ==1)
                {
                    string _rtnItm = "";
                    string _issuItm = "";

                    for (int i = 0; i < grvReturningDetails.Rows.Count; i++)
                    {

                        _rtnItm = grvReturningDetails.Rows[i].Cells["Column8"].Value.ToString();
                    }

                    for (int i = 0; i < grvIssuingDetails.Rows.Count; i++)
                    {

                        _issuItm = grvIssuingDetails.Rows[i].Cells["Column13"].Value.ToString();
                    }

                    if (_rtnItm == _issuItm)
                    {
                        SalesReversal = false;
                    }
                }

                if (cmbDocType.SelectedItem.ToString() == "Hire")
                {
                    if (IsPriceBookChanged || SalesReversal)
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                    }
                    else
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSEXCH, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008;
                        if (grvIssuingDetails.Rows.Count <= 0)
                        {
                            MessageBox.Show("Issuing items can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    _documentType = "Hire";
                }

                if (cmbDocType.SelectedItem.ToString() == "Request")
                {
                    RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                    _documentType = _hdr.Grah_remaks;

                    if (_documentType == "Cash")
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;

                        ////Added by Prabhath on 11/10/2013
                        //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HSACREV", txtDocumentNo.Text.Trim()))
                        //{ return; }
                    }

                    if (_documentType == "Hire")
                    {
                        if (IsPriceBookChanged)
                        {
                            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                            _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;

                            ////Added by Prabhath on 11/10/2013
                            //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", txtDocumentNo.Text.Trim()))
                            //{ return; }
                        }
                        else
                        {
                            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSEXCH, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                            _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008;

                            //Added by Prabhath on 11/10/2013
                            //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT008", txtDocumentNo.Text.Trim()))
                            //{ return; }
                        }
                    }
                }
                //block reversal request
                if (_approvalCode == CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013)
                {
                    MessageBox.Show("You can not request for exchange.\nPlease make a sales reversal request");
                    return;
                }

                //get pending requests
                List<RequestApprovalHeader> _pending = new List<RequestApprovalHeader>();
                foreach (ReptPickSerials itm in _selectedItemList)
                {
                    List<RequestApprovalHeader> _hdr = CHNLSVC.General.GetPendingExchangeRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, itm.Tus_itm_cd, txtDocumentNo.Text);
                    if (_hdr != null && _hdr.Count > 0)
                    {
                        _pending.AddRange(_hdr);
                    }

                }
                if (_pending.Count > 0)
                {
                    if (BaseCls.GlbReqIsFinalApprovalUser)
                    {
                        List<RequestApprovalHeader> _app = (from _res in _pending
                                                            where _res.Grah_app_stus == "A"
                                                            select _res).ToList<RequestApprovalHeader>();
                        if (_app != null && _app.Count > 0)
                        {
                            MessageBox.Show("This item has approved Reuqest.Pleses Finish approve request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        List<RequestApprovalHeader> _pen = (from _res in _pending
                                                            where _res.Grah_app_stus == "P"
                                                            select _res).ToList<RequestApprovalHeader>();
                        if (_pen != null && _pen.Count > 0)
                        {
                            if (cmbDocType.SelectedItem.ToString() == "Request")
                            {
                                foreach (RequestApprovalHeader _head in _pen)
                                {
                                    if (_head.Grah_ref != txtDocumentNo.Text.Trim())
                                    {
                                        MessageBox.Show("This item has pending Reuqest.Pleses approve pending request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        List<RequestApprovalHeader> _app = (from _res in _pending
                                                            where _res.Grah_app_stus == "A"
                                                            select _res).ToList<RequestApprovalHeader>();
                        if (_app != null && _app.Count > 0)
                        {
                            MessageBox.Show("This item has approved Reuqest.Pleses Finish approve request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        List<RequestApprovalHeader> _pen = (from _res in _pending
                                                            where _res.Grah_app_stus == "P"
                                                            select _res).ToList<RequestApprovalHeader>();
                        if (_pen != null && _pen.Count > 0)
                        {
                            MessageBox.Show("This item has pending request, Please approve pending request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }

                }


                //GlbReqIsApprovalNeed = true;
                //GlbReqUserPermissionLevel = 3;
                //GlbReqIsFinalApprovalUser = true;
                //GlbReqIsRequestGenerateUser = true;

                List<RequestApprovalHeader> _request = null;
                if (cmbDocType.SelectedItem.ToString() == "Request")
                {
                    List<RequestApprovalHeader> _lstTem = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT008", string.Empty, string.Empty);
                    List<RequestApprovalHeader> _lstTem1 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", string.Empty, string.Empty);

                    List<RequestApprovalHeader> _lst = new List<RequestApprovalHeader>();
                    if (_lstTem != null)
                        _lst.AddRange(_lstTem);
                    else if (_lstTem1 != null)
                        _lst.AddRange(_lstTem1);


                    _request = (from _res in _lst
                                where _res.Grah_ref == txtDocumentNo.Text
                                select _res).ToList<RequestApprovalHeader>();
                }
                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                if (_request != null && _request.Count > 0)
                {
                    ra_hdr = _request[0];
                    isGenerate = false;

                    if (BaseCls.GlbReqIsFinalApprovalUser)
                        ra_hdr.Grah_app_stus = "A";

                    ra_hdr.Grah_remaks = txtremarks.Text;
                    ra_hdr.Grah_sub_type = txtSubType.Text;
                }
                else
                {
                    isGenerate = true;
                    #region fill RequestApprovalHeader


                    ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_app_dt = dateTimePickerDate.Value.Date;
                    ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                    ra_hdr.Grah_app_stus = "P";
                    ra_hdr.Grah_app_tp = _approvalCode.ToString();
                    ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                    ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_cre_dt = dateTimePickerDate.Value.Date;
                    ra_hdr.Grah_fuc_cd = txtDocumentNo.Text;
                    ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// GlbUserDefLoca;
                    ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_mod_dt = dateTimePickerDate.Value.Date;
                    ra_hdr.Grah_oth_loc = BaseCls.GlbUserDefProf;
                    ra_hdr.Grah_remaks = _documentType;
                    ra_hdr.Grah_remaks = txtremarks.Text;
                    ra_hdr.Grah_sub_type = txtSubType.Text;

                    if (BaseCls.GlbReqIsFinalApprovalUser)
                        ra_hdr.Grah_app_stus = "A";

                    if (cmbDocType.SelectedItem.ToString() != "Request") ra_hdr.Grah_ref = txtDocumentNo.Text;
                    else ra_hdr.Grah_ref = null;
                }



                    #endregion

                #region fill List<RequestApprovalDetail> with Log
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

                Int32 _count = 1;

                foreach (ReptPickSerials _in in _selectedItemList)
                {
                    RequestApprovalDetail ra_det = new RequestApprovalDetail();
                    RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                    ra_det.Grad_ref = ra_hdr.Grah_ref;
                    ra_det.Grad_line = _count;
                    ra_det.Grad_req_param = _in.Tus_itm_cd; //Item

                    ra_det.Grad_val1 = _in.Tus_qty; //Qty
                    ra_det.Grad_val2 = _in.Tus_unit_cost;//Unit Price
                    ra_det.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                    ra_det.Grad_val4 = _in.Tus_ser_line; //serial line
                    ra_det.Grad_val5 = _in.Tus_ser_id; //serial id

                    ra_det.Grad_anal7 = _in.Tus_itm_stus;
                    ra_det.Grad_anal8 = _in.Tus_ser_2;
                    if (cmbDocType.SelectedItem.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Request.ToString())
                    {
                        string _stus = "";
                        grvReturningDetails.EndEdit();

                        foreach (DataGridViewRow _gvr in grvReturningDetails.Rows)
                        {
                            try
                            {
                                _stus = _gvr.Cells[5].Value.ToString();
                                if (_stus == "System.Data.DataRowView")
                                {
                                    _stus = _in.Tus_itm_stus;
                                }
                            }
                            catch (Exception) { _stus = _in.Tus_itm_stus; }
                        }
                        ra_det.Grad_anal8 = _stus;
                    }

                    ra_det.Grad_anal1 = txtDocumentNo.Text; //account no
                    ra_det.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                    ra_det.Grad_anal3 = _in.Tus_doc_no;//DO no
                    ra_det.Grad_anal4 = _in.Tus_ser_1;//serial no
                    ra_det.Grad_anal5 = "IN";
                    ra_det.Grad_date_param = _date;

                    ra_det_List.Add(ra_det);

                    ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                    ra_det_log.Grad_date_param = _date;
                    ra_det_log.Grad_line = _count;
                    ra_det_log.Grad_req_param = _in.Tus_itm_cd;//Item

                    ra_det_log.Grad_val1 = _in.Tus_qty;//Qty
                    ra_det_log.Grad_val2 = _in.Tus_unit_cost;//Unit Price
                    ra_det_log.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                    ra_det_log.Grad_val4 = _in.Tus_ser_line;//serial line
                    ra_det_log.Grad_val5 = _in.Tus_ser_id;//serial id

                    ra_det_log.Grad_anal1 = txtDocumentNo.Text;//account no
                    ra_det_log.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                    ra_det_log.Grad_anal3 = _in.Tus_doc_no;//DO no
                    ra_det_log.Grad_anal4 = _in.Tus_ser_1;//serial no
                    ra_det_log.Grad_anal5 = "IN";



                    ra_det_log.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;
                    ra_detLog_List.Add(ra_det_log);
                    _count += 1;
                }

                foreach (InvoiceItem _out in _invoiceItemList)
                {
                    RequestApprovalDetail ra_det = new RequestApprovalDetail();
                    RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                    ra_det.Grad_ref = ra_hdr.Grah_ref;
                    ra_det.Grad_line = _count;

                    ra_det.Grad_req_param = _out.Sad_itm_cd;//Item
                    ra_det.Grad_val1 = _out.Sad_qty;//Qty
                    ra_det.Grad_val2 = _out.Sad_unit_rt;//Unit Rate
                    ra_det.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                    ra_det.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                    ra_det.Grad_val5 = _out.Sad_seq;//PB SEQ

                    ra_det.Grad_anal1 = txtDocumentNo.Text;//account no
                    ra_det.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                    ra_det.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                    ra_det.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                    ra_det.Grad_anal5 = "OUT";
                    ra_det.Grad_anal10 = _out.Sad_pb_lvl;
                    ra_det.Grad_anal9 = _out.Sad_pbook;
                    ra_det_List.Add(ra_det);

                    ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                    ra_det_log.Grad_line = _count;

                    ra_det_log.Grad_req_param = _out.Sad_itm_cd;//Item
                    ra_det_log.Grad_val1 = _out.Sad_qty;//Qty
                    ra_det_log.Grad_val2 = _out.Sad_unit_rt;//Unit Rate
                    ra_det_log.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                    ra_det_log.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                    ra_det_log.Grad_val5 = _out.Sad_seq;//PB SEQ

                    ra_det_log.Grad_anal1 = txtDocumentNo.Text;//account no
                    ra_det_log.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                    ra_det_log.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                    ra_det_log.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                    ra_det_log.Grad_anal5 = "OUT";
                    ra_det_log.Grad_date_param = _date;
                    ra_det_log.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;


                    ra_detLog_List.Add(ra_det_log);

                    _count += 1;
                }

                RequestApprovalDetail ra_det_one = new RequestApprovalDetail();
                ra_det_one.Grad_ref = ra_hdr.Grah_ref;
                ra_det_one.Grad_line = _count;
                ra_det_one.Grad_anal5 = "AMT";
                ra_det_one.Grad_req_param = cmbUsage.SelectedItem.ToString().ToUpper();
                ra_det_one.Grad_val1 = 1;
                ra_det_one.Grad_val2 = Convert.ToDecimal(txtAmount.Text.Trim());
                ra_det_one.Grad_anal1 = txtDocumentNo.Text;

                ra_det_List.Add(ra_det_one);


                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();
                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = _count;
                ra_detLog.Grad_anal5 = "AMT";
                ra_detLog.Grad_req_param = cmbUsage.SelectedItem.ToString().ToUpper();
                ra_detLog.Grad_val1 = 1;
                ra_detLog.Grad_val2 = Convert.ToDecimal(txtAmount.Text.Trim());
                ra_detLog.Grad_anal1 = txtDocumentNo.Text;
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;
                ra_detLog.Grad_date_param = _date;
                ra_detLog_List.Add(ra_detLog);


                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = dateTimePickerDate.Value.Date;
                ra_hdrLog.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = _approvalCode.ToString();
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = dateTimePickerDate.Value;
                ra_hdrLog.Grah_fuc_cd = txtDocumentNo.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = dateTimePickerDate.Value;

                ra_hdrLog.Grah_oth_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = _documentType;

                if (BaseCls.GlbReqIsFinalApprovalUser)
                    ra_hdrLog.Grah_app_stus = "A";

                #endregion


                MasterAutoNumber auto = new MasterAutoNumber();
                auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                auto.Aut_cate_tp = "PC";
                auto.Aut_direction = 1;
                auto.Aut_modify_dt = null;
                auto.Aut_number = 0;
                if (_approvalCode.ToString() == "ARQT008")
                {
                    auto.Aut_moduleid = "HSACRSC";
                    auto.Aut_start_char = "HSACRSC";
                }
                else if (_approvalCode.ToString() == "ARQT013")
                {
                    auto.Aut_moduleid = "HSACREV";
                    auto.Aut_start_char = "HSACREV";
                }
                else
                {
                    auto.Aut_moduleid = "HSACREV";
                    auto.Aut_start_char = "HSACREV";
                }
                string userseq;

                string referenceNo;
                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(auto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, isGenerate, out userseq, out referenceNo);



                if (eff > 0)
                {
                    //save serials
                    if (_serialList != null && _serialList.Count > 0)
                    {
                        _serialList.ForEach(x => x.Gras_ref = userseq);
                        CHNLSVC.General.Save_RequestApprove_Ser_and_log(_serialList, BaseCls.GlbReqUserPermissionLevel);
                    }
                    MessageBox.Show("Request Sent Successfully\nRequest No = " + userseq, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Request not sent", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void bntClose2_Click(object sender, EventArgs e)
        {
            pnlPopUp2.Visible = false;
            pnlMain.Enabled = true;
        }



        private void grvPopSerialPricePick_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    foreach (DataGridViewRow gr in grvPopSerialPricePick.Rows)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)gr.Cells[0];
                        if (cell.Value.ToString() == "True")
                        {


                            string _mainSerial = gr.Cells[3].Value.ToString();

                            Int32 _priceType = Convert.ToInt32(gr.Cells[7].Value);
                            Int32 _pbSq = Convert.ToInt32(gr.Cells[8].Value);
                            string _mItem = Convert.ToString(gr.Cells[9].Value);
                            IsFixQty = Convert.ToBoolean(gr.Cells[10].Value);

                            PriceTypeRef _list = TakePromotion(_priceType);
                            SSPRomotionType = _priceType;
                            SSPromotionCode = gr.Cells[1].Value.ToString();

                            if (_list.Sarpt_is_com)
                            {
                                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                                _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSq, _mItem, _mainSerial);
                                if (_tempPriceCombinItem != null)
                                {
                                    grvPriceItemCombine.DataSource = _tempPriceCombinItem;

                                    pnlMain.Enabled = false;
                                    pnlPopUp2.Visible = true;
                                    return;
                                }
                                else
                                {
                                    lblMsg.Text = "There is no such combine items pick";
                                    pnlMain.Enabled = false;
                                    pnlPopUp2.Visible = true;
                                    return;
                                }

                            }
                            pnlMain.Enabled = false;
                            pnlPopUp2.Visible = true;
                            return;
                        }
                        else
                        {
                            pnlMain.Enabled = false;
                            pnlPopUp2.Visible = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvPopPricePick_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    string _item = Convert.ToString(grvPopPricePick.Rows[e.RowIndex].Cells[5].Value); //Item
                    int _pbseq = Convert.ToInt32(grvPopPricePick.Rows[e.RowIndex].Cells[7].Value);//Pb Seq No
                    IsFixQty = Convert.ToBoolean(grvPopPricePick.Rows[e.RowIndex].Cells[9].Value);//Is Fix Qty
                    Int32 _priceType = Convert.ToInt32(grvPopPricePick.Rows[e.RowIndex].Cells[6].Value);//Price Type - combine/Free/Normal
                    decimal _unitPrice = Convert.ToDecimal(grvPopPricePick.Rows[e.RowIndex].Cells[3].Value);//Price 
                    Int32 _itmLine = Convert.ToInt32(grvPopPricePick.Rows[e.RowIndex].Cells[10].Value);//item line no
                    _unitPrice = TaxCalculation(txtItemCode.Text.Trim(), cmbItemStatus.SelectedItem.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), false);
                    String _promoCD = Convert.ToString(grvPopPricePick.Rows[e.RowIndex].Cells[1].Value);

                    PriceTypeRef _list = TakePromotion(_priceType);
                    if (_list.Sarpt_is_com)
                    {
                        _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                        _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbseq, _item, string.Empty);
                        if (_tempPriceCombinItem != null)
                        {
                            grvPriceItemCombine.DataSource = _tempPriceCombinItem;
                            grvPriceItemCombine.Visible = true;
                            gvPopConsumPricePick.Visible = false;
                            grvPopPricePick.Visible = false;
                            gvItemInventoryCombine.Visible = false;
                            grvPopSerialPricePick.Visible = false;



                            SSPriceBookPrice = _unitPrice;
                            SSPriceBookSequance = _pbseq.ToString();
                            SSPriceBookItemSequance = _itmLine.ToString();
                            SSPromotionCode = _promoCD;
                            SSPRomotionType = _priceType;


                            pnlMain.Enabled = false;
                            pnlPopUp2.Visible = true;
                            return;
                        }
                        else
                        {

                            lblMsg.Text = "There is no such combine items pick";
                            pnlMain.Enabled = false;
                            pnlPopUp2.Visible = true;
                            return;
                        }
                    }

                    SSPriceBookPrice = _unitPrice;
                    SSPriceBookSequance = _pbseq.ToString();
                    SSPriceBookItemSequance = _itmLine.ToString();
                    SSPromotionCode = _promoCD;
                    SSPRomotionType = _priceType;

                    txtUnitRate.Text = FormatToCurrency(_unitPrice.ToString());
                    txtUnitRate.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void gvPopConsumPricePick_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                decimal _unitPrice = Convert.ToDecimal(gvPopConsumPricePick.Rows[e.RowIndex].Cells[5].Value); //Item
                txtUnitRate.Text = FormatToCurrency(Convert.ToDecimal(_unitPrice).ToString());
                txtUnitRate.Focus();
                pnlMain.Enabled = true;
                pnlPopUp2.Visible = false;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                #region Confirmation Serialized Price Level Price

                if (grvPopSerialPricePick.Visible)
                {
                    Int32 _counter = 0;
                    if (_MainPriceSerial == null) _MainPriceSerial = new List<PriceSerialRef>();
                    _tempPriceSerialItm = new List<PriceSerialRef>();

                    foreach (DataGridViewRow row in grvPopSerialPricePick.Rows)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (cell.Value.ToString() == "True")
                        {
                            string _serial = row.Cells[3].Value.ToString();
                            _counter++;

                            var _obj = from _list in _tempPriceSerial
                                       where _list.Sars_ser_no == _serial && _list.Sars_itm_cd == txtItemCode.Text
                                       select _list;
                            if (_obj != null)
                            {
                                foreach (PriceSerialRef _one in _obj)
                                {
                                    _tempPriceSerialItm.Add(_one);
                                }
                            }
                        }
                    }


                    if (_counter != Convert.ToDecimal(txtQty.Text))
                    {
                        lblMsg.Text = "Select serial and qty mismatch!";
                        pnlMain.Enabled = false;
                        pnlPopUp2.Visible = true;
                        return;
                    }


                    string _item = txtItemCode.Text;
                    string _status = cmbItemStatus.SelectedItem.ToString();
                    string _duplicateSerials = "";

                    foreach (PriceSerialRef _one in _tempPriceSerialItm)
                    {

                        txtItemCode.Text = _item;
                        cmbItemStatus.SelectedItem = _status;
                        txtQty.Text = "1.00";
                        txtUnitRate.Text = FormatToCurrency(_one.Sars_itm_price.ToString());

                        var _duplicate = from _dup in _MainPriceSerial
                                         where _dup.Sars_pb_seq == _one.Sars_pb_seq && _dup.Sars_pbook == _one.Sars_pbook && _dup.Sars_price_lvl == _one.Sars_price_lvl && _dup.Sars_itm_cd == _one.Sars_itm_cd && _dup.Sars_ser_no == _one.Sars_ser_no
                                         select _dup;

                        if (_duplicate.Count() <= 0)
                        {
                            _MainPriceSerial.Add(_one);

                            SSPriceBookPrice = _one.Sars_itm_price;
                            SSPriceBookSequance = _one.Sars_pb_seq.ToString();
                            SSPriceBookItemSequance = "1"; //TODO : Table does not having item line no

                            txtUnitRate.Focus();



                            txtAmount.Focus();
                            CalculateItem();
                            AddItem(true);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(_duplicateSerials))
                                _duplicateSerials = _one.Sars_ser_no;
                            else
                                _duplicateSerials += ", " + _one.Sars_ser_no;

                        }

                    }

                    if (!string.IsNullOrEmpty(_duplicateSerials))
                    {
                        lblMsg.Text = "Duplicate serial found - " + _duplicateSerials;
                        pnlMain.Enabled = false;
                        pnlPopUp2.Visible = true;
                        return;
                    }


                    txtUnitRate.Focus();

                }
                #endregion

                #region  Confirmation Serialized Price Promotion Item

                if (grvPopPricePick.Visible)
                {
                    if (_tempPriceCombinItem != null)
                    {
                        if (_MainPriceCombinItem == null) _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                        foreach (PriceCombinedItemRef _item in _tempPriceCombinItem)
                        {
                            _MainPriceCombinItem.Add(_item);
                        }
                    }


                    pnlMain.Enabled = false;
                    pnlPopUp2.Visible = true;
                    return;
                }

                #endregion

                #region Confirmation Non-Serialized Price Level Price
                if (grvPopPricePick.Visible)
                {
                    txtUnitRate.Text = FormatToCurrency(SSPriceBookPrice.ToString());
                    if (_tempPriceCombinItem != null)
                    {
                        if (_MainPriceCombinItem == null) _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                        string _duplicateSerials = "";
                        foreach (PriceCombinedItemRef _item in _tempPriceCombinItem)
                        {
                            var _duplicate = from _list in _MainPriceCombinItem
                                             where _list.Sapc_main_itm_cd == _item.Sapc_main_itm_cd && _list.Sapc_itm_cd == _item.Sapc_itm_cd && _list.Sapc_pb_seq == _item.Sapc_pb_seq && _list.Sapc_price == _item.Sapc_price
                                             select _list;

                            if (_duplicate.Count() <= 0)
                            {
                                _MainPriceCombinItem.Add(_item);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(_duplicateSerials))
                                    _duplicateSerials = _item.Sapc_itm_cd;
                                else
                                    _duplicateSerials += ", " + _item.Sapc_itm_cd;
                            }
                        }

                        if (!string.IsNullOrEmpty(_duplicateSerials))
                        {
                            lblMsg.Text = "Duplicate serial found - " + _duplicateSerials;
                            pnlMain.Enabled = false;
                            pnlPopUp2.Visible = true;
                            return;
                        }


                        txtUnitRate.Focus();
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void linkLabelProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow gvr in this.grvPopSerial.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)gvr.Cells[0];
                    //if (cell.Value.ToString() == "True") 
                    if (cell.Selected == true) //Add by Chamal 07-May-2014
                    {
                        string _serialId = gvr.Cells[5].Value.ToString();
                        String _item = gvr.Cells[6].Value.ToString();
                        MasterItem _items = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        if (_items.Mi_is_ser1 != -1)
                        {
                            var _match = (from _lst in _popUpList
                                          where _lst.Tus_ser_id == Convert.ToInt32(_serialId)
                                          select _lst);
                            if (_match != null)
                                foreach (ReptPickSerials _one in _match)
                                {
                                    if (_selectedItemList != null)
                                        if (_selectedItemList.Count > 0)
                                        {
                                            var _duplicate = from _lst in _selectedItemList
                                                             where _lst.Tus_ser_id == _one.Tus_ser_id
                                                             select _lst;

                                            if (_duplicate.Count() <= 0)
                                            {
                                                _selectedItemList.Add(_one);
                                            }
                                        }
                                        else
                                        {
                                            _selectedItemList.Add(_one);
                                        }
                                }
                        }
                        else
                        {
                            var _match = (from _lst in _popUpList
                                          where _lst.Tus_itm_cd == _item
                                          select _lst);
                            if (_match != null)
                                foreach (ReptPickSerials _one in _match)
                                {
                                    if (_selectedItemList != null)
                                        if (_selectedItemList.Count > 0)
                                        {
                                            var _duplicate = from _lst in _selectedItemList
                                                             where _lst.Tus_itm_cd == _one.Tus_itm_cd
                                                             select _lst;
                                            if (_duplicate.Count() <= 0)
                                            {
                                                _selectedItemList.Add(_one);
                                            }

                                        }
                                        else
                                        {
                                            _selectedItemList.Add(_one);
                                        }
                                }
                        }

                    }
                }
                BindSelectedItems(_selectedItemList);
                CalculateTotalNewandOldAmount();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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
                if (cmbUsage.SelectedItem.ToString() == "Cash") { MessageBox.Show("There is no request for castoff", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (cmbUsage.SelectedItem.ToString() == "Hire") { MessageBox.Show("There is no request for castoff", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (string.IsNullOrEmpty(txtDocumentNo.Text)) { MessageBox.Show("Please select the request no to castoff", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());

                _hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                _hdr.Grah_app_stus = "C";
                _hdr.Grah_loc = BaseCls.GlbUserDefProf;
                _hdr.Grah_mod_by = BaseCls.GlbUserID;
                _hdr.Grah_mod_dt = dateTimePickerDate.Value;

                Int32 _effect = CHNLSVC.General.UpdateApprovalStatus(_hdr);

                if (_effect > 0)
                {
                    MessageBox.Show("Request castoff!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Request not castoff!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtAmount.Text = "";
            txtDocumentNo.Text = "";
            txtPB.Text = "";
            txtPLevel.Text = "";
            txtQty.Text = "";
            txtSerialNo.Text = "";
            txtUnitAmt.Text = "";
            txtUnitRate.Text = "";
            txtWarranty.Text = "";
            lblAddress1.Text = "";
            lblAddress2.Text = "";
            lblBackDateInfor.Text = "";
            lblCusCode.Text = "";
            lblDiffreence.Text = "";
            lblDocNo.Text = "";
            lblMsg.Text = "";
            lblName.Text = "";
            lblNewTotal.Text = "";
            lblOldTotal.Text = "";
            txtAmount.Text = "0";

            grvIssuingDetails.DataSource = null;
            grvPopPricePick.DataSource = null;
            grvPopSerial.DataSource = null;
            grvPopSerialPricePick.DataSource = null;
            grvPriceItemCombine.DataSource = null;
            grvReturningDetails.DataSource = null;
            grvTrade.DataSource = null;
            grvReturningDetails.ReadOnly = false;

            //variables
            InvoiceType = "CS";
            SSPriceBook = "";
            SSPriceLevel = "";
            SSPriceBookItemSequance = "";
            SSPriceBookSequance = "";
            SSPromotionCode = "";

            SSPriceBookPrice = 0;
            IsNoPriceDefinition = false;
            _isEditPrice = false;
            _isEditDiscount = false;
            IsPriceBookChanged = false;
            IsFixQty = false;
            _lineNo = 0;
            _combineCounter = 0;
            SSCombineLine = 0;
            SSPRomotionType = 0;
            _isCombineAdding = false;

            _popUpList = new List<ReptPickSerials>();
            _selectedItemList = new List<ReptPickSerials>();
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            _priceBookLevelRef = new PriceBookLevelRef();
            _tempPriceSerial = new List<PriceSerialRef>();
            _priceDetailRef = new List<PriceDetailRef>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _invoiceItemList = new List<InvoiceItem>();
            _MainPriceSerial = new List<PriceSerialRef>();
            _tempPriceSerialItm = new List<PriceSerialRef>();
            _serialList = new List<RequestApprovalSerials>();
            dgvSerApp.DataSource = null;

            cmbDocType_SelectedIndexChanged(null, null);
            lblRequest.Visible = false;
            lblRequestStatus.Visible = false;
            btnReject.Enabled = false;
            btnSave.Text = "Process";

            txtPB.Enabled = true;
            txtPLevel.Enabled = true;
            SalesReversal = false;

            lblPB.Text = "";
            lblPblevel.Text = "";
            pnlMain.Enabled = true;
            pnlSerApp.Visible = false;
            txtremarks.Text = "";
            txtSubType.Text = "";
        }


        private void LoadLevelStatus(string _invType, string _book, string _level)
        {
            DataTable _levelStatus = null;
            string _initPara = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            _levelStatus = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(_initPara, null, null);
            if (_levelStatus != null)
                if (_levelStatus.Rows.Count > 0)
                {

                    var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                    _types.Add("");
                    cmbItemStatus.DataSource = _types;
                    //  cmbItemStatus.SelectedIndex = cmbItemStatus.Items.Count - 1;
                }
                else
                    cmbItemStatus.DataSource = null;
            else
                cmbItemStatus.DataSource = null;
        }

        private void LoadInLevelStatus(string _invType, string _book, string _level)
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
            cmbInStus.DataSource = dt;
            cmbInStus.DisplayMember = "MIS_DESC";
            cmbInStus.ValueMember = "MIC_CD";
            cmbInStus.SelectedValue = "GOD";
        }

        private void LoadAppLevelStatus(string _invType, string _book, string _level)
        {
            DataTable dt = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr1 = dt.Rows[i];
                if (dr1["MIC_CD"].ToString() == "CONS")
                    dr1.Delete();
            }
            app_stus.DataSource = dt;
            app_stus.DisplayMember = "MIS_DESC";
            app_stus.ValueMember = "MIC_CD";


        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCode.Text != "")
                {
                    //kapila 6/4/2015
                    MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
                    if (_item == null)
                    {
                        MessageBox.Show("Invalid item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemCode.Focus();
                        return;
                    }
                    LoadLevelStatus("CS", txtPB.Text.ToUpper(), txtPLevel.Text.ToUpper());
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvReturningDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (_selectedItemList == null) return;
                        if (_selectedItemList.Count <= 0) return;

                        int row_id = e.RowIndex;
                        Int32 _serialID = Convert.ToInt32(grvReturningDetails.Rows[e.RowIndex].Cells[5].Value.ToString());
                        string _item = grvReturningDetails.Rows[e.RowIndex].Cells[6].Value.ToString();

                        MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        if (_itm.Mi_is_ser1 != -1)
                        {
                            List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                            _selectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                        }
                        else
                        {
                            _selectedItemList.RemoveAll(x => x.Tus_itm_cd == _item);
                        }

                        BindSelectedItems(_selectedItemList);
                    }
                }
                if (e.RowIndex != -1 && e.ColumnIndex == 5)
                {


                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvIssuingDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        {
                            Int32 _combineLine = Convert.ToInt32(grvIssuingDetails.Rows[e.RowIndex].Cells[16].Value);

                            if (_MainPriceSerial != null)
                                if (_MainPriceSerial.Count > 0)
                                {

                                    //sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty
                                    string _item = grvIssuingDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                                    decimal _uRate = Convert.ToDecimal(grvIssuingDetails.Rows[e.RowIndex].Cells[8].Value);
                                    string _pbook = grvIssuingDetails.Rows[e.RowIndex].Cells[9].Value.ToString();
                                    string _level = grvIssuingDetails.Rows[e.RowIndex].Cells[10].Value.ToString();

                                    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                                    var _remove = from _list in _tempSerial
                                                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                                  select _list;

                                    foreach (PriceSerialRef _single in _remove)
                                    {
                                        _tempSerial.Remove(_single);
                                    }

                                    _MainPriceSerial = _tempSerial;
                                }

                            List<InvoiceItem> _tempList = _invoiceItemList;
                            var _promo = (from _pro in _invoiceItemList
                                          where _pro.Sad_job_line == _combineLine
                                          select _pro).ToList();

                            if (_promo.Count() > 0)
                            {
                                foreach (InvoiceItem code in _promo)
                                {
                                    //CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                                    //_tempList.Remove(code);
                                }
                                _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                            }
                            else
                            {
                                //CalculateGrandTotal(Convert.ToDecimal(gvExchangeOutItm.DataKeys[row_id][5]), (decimal)gvExchangeOutItm.DataKeys[row_id][2], (decimal)gvExchangeOutItm.DataKeys[row_id][6], (decimal)gvExchangeOutItm.DataKeys[row_id][7], false);
                                _tempList.RemoveAt(e.RowIndex);
                            }
                            _invoiceItemList = _tempList;
                            BindAddItem();
                        }
                    }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPB_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPB.Text != "")
                {
                    SSPriceBook = txtPB.Text;
                    if (string.IsNullOrEmpty(txtPB.Text.Trim())) return;

                    if (string.IsNullOrEmpty(InvoiceType)) { MessageBox.Show("Please select the invoice type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    List<PriceDefinitionRef> _def = new List<PriceDefinitionRef>();
                    _def = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(BaseCls.GlbUserComCode, txtPB.Text.Trim(), string.Empty, InvoiceType, BaseCls.GlbUserDefProf);

                    if (_def.Count == 0)
                    {
                        MessageBox.Show("Please select the valid price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPB.Text = string.Empty;
                        txtPB.Focus();
                        return;
                    }
                    SSPriceBook = txtPB.Text.Trim();
                    GetPriceDetail(txtDocumentNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void GetPriceDetail(string _account)
        {
            IsPriceBookChanged = false;
            MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            if (cmbDocType.SelectedItem.ToString() == "Hire")
            {
                List<InvoiceHeader> _hdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
                if (_hdr != null)
                    if (_hdr.Count > 0)
                    {
                        InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_hdr[0].Sah_inv_no)[0];
                        if (_invItem.Sad_pbook != txtPB.Text.Trim() && _invItem.Sad_pb_lvl != txtPLevel.Text.Trim() && !(txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = true;
                        if (_invItem.Sad_pbook != txtPB.Text.Trim() && !(txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = true;
                        if (_invItem.Sad_pbook != txtPB.Text.Trim() && !(txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = true;
                        if ((_invItem.Sad_pbook == txtPB.Text.Trim() && _invItem.Sad_pb_lvl == txtPLevel.Text.Trim()) || (txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = false;
                    }
            }
            if (cmbDocType.SelectedItem.ToString() == "Request")
            {
                RequestApprovalHeader _Rhdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
                List<InvoiceHeader> _hdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _Rhdr.Grah_fuc_cd);
                if (_hdr != null)
                    if (_hdr.Count > 0)
                    {
                        InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_hdr[0].Sah_inv_no)[0];
                        if (_invItem.Sad_pbook != txtPB.Text.Trim() && _invItem.Sad_pb_lvl != txtPLevel.Text.Trim() && !(txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = true;
                        else if (_invItem.Sad_pbook != txtPB.Text.Trim() && !(txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = true;
                        else if (_invItem.Sad_pbook != txtPB.Text.Trim() && !(txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = true;
                        else if ((_invItem.Sad_pbook == txtPB.Text.Trim() && _invItem.Sad_pb_lvl == txtPLevel.Text.Trim()) || (txtPB.Text.Trim() == _mastercompany.Mc_anal7 && txtPLevel.Text.Trim() == _mastercompany.Mc_anal8))
                            IsPriceBookChanged = false;
                    }
            }


        }

        private void txtPLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPLevel.Text != "")
                    SSPriceLevel = txtPLevel.Text;
                if (string.IsNullOrEmpty(txtPLevel.Text)) { return; }


                if (string.IsNullOrEmpty(txtPLevel.Text))
                {
                    MessageBox.Show("Price book not select. It is set to profit center default", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPB.Text = BaseCls.GlbMasterProfitCenter.Mpc_def_pb;
                }


                List<PriceDefinitionRef> _def = new List<PriceDefinitionRef>();
                _def = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(BaseCls.GlbUserComCode, txtPB.Text.Trim(), txtPLevel.Text.Trim(), InvoiceType, BaseCls.GlbUserDefProf);
                if (_def.Count == 0)
                {
                    MessageBox.Show("Please select the valid price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPLevel.Text = string.Empty;
                    txtPLevel.Focus();
                    return;
                }


                _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPB.Text, txtPLevel.Text);
                if (_priceBookLevelRef != null)
                {
                    if (_priceBookLevelRef.Sapl_is_serialized) SSIsLevelSerialized = "1";
                    else SSIsLevelSerialized = "0";

                    _priceDefinitionRef = new PriceDefinitionRef();
                    _priceDefinitionRef = CHNLSVC.Sales.GetPriceDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, InvoiceType, txtPB.Text, txtPLevel.Text);
                }
                else
                {
                    MessageBox.Show("Please select the valid level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPLevel.Text = "";
                    txtPLevel.Focus();
                    return;

                }

                SSPriceLevel = txtPLevel.Text.Trim();

                GetPriceDetail(txtDocumentNo.Text.Trim());
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAdd.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSerialNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSerialNo.Text != "" && txtSerialNo.Text != "N/A")
                {
                    List<InventorySerialMaster> _serial = CHNLSVC.Inventory.GetWarrantyDetails("%", "%", "%", txtSerialNo.Text, "%");
                    if (_serial != null)
                    {
                        Int32 _isAvailable = _serial.Count;

                        if (_isAvailable <= 0)
                        {
                            MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtSerialNo.Clear();
                            return;
                        }

                        if (_isAvailable > 1)
                        {
                            gvMultipleItem.AutoGenerateColumns = false;
                            pnlMain.Enabled = false;
                            pnlMultipleItem.Visible = true;
                            gvMultipleItem.DataSource = _serial;
                            return;
                        }
                        LoadSerial(_serial[0].Irsm_itm_cd, _serial[0].Irsm_ser_1, null);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Serial Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void LoadSerial(string _item, string _serial, string _warr)
        {
            try
            {
                DataTable _dt = CHNLSVC.Inventory.GetInvoiceAccountNoFromItem(_item, _serial, _warr);
                if (_dt.Rows.Count > 0)
                {
                    txtDocumentNo.Text = _dt.Rows[0]["VAL"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Serial or Warranty do not have valid account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void txtWarranty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtWarranty.Text != "")
                {
                    List<InventorySerialMaster> _warranty = CHNLSVC.Inventory.GetWarrantyDetails("%", "%", "%", "%", txtWarranty.Text);
                    if (_warranty != null)
                    {
                        Int32 _isAvailable = _warranty.Count;

                        if (_isAvailable <= 0)
                        {
                            MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtSerialNo.Clear();
                            return;
                        }

                        if (_isAvailable > 1)
                        {
                            gvMultipleItem.AutoGenerateColumns = false;
                            pnlMain.Enabled = false;
                            pnlMultipleItem.Visible = true;
                            gvMultipleItem.DataSource = _warranty;
                            return;
                        }
                        LoadSerial(_warranty[0].Irsm_itm_cd, null, _warranty[0].Irsm_warr_no);
                    }
                    else
                    {
                        MessageBox.Show("Invalid warranty Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void gvMultipleItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    string _item = gvMultipleItem.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string _serial = gvMultipleItem.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string _warranty = gvMultipleItem.Rows[e.RowIndex].Cells[5].Value.ToString();

                    if (_serial != "" || _serial != "N/A")
                    {
                        LoadSerial(_item, _serial, null);
                        pnlMultipleItem.Visible = false;
                        pnlMain.Enabled = true;
                        gvMultipleItem.DataSource = null;
                    }
                    else if (_warranty != "" || _warranty != "N/A")
                    {
                        LoadSerial(_item, null, _warranty);
                        pnlMultipleItem.Visible = false;
                        pnlMain.Enabled = true;
                        gvMultipleItem.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPnlMuItemClose_Click(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
            gvMultipleItem.DataSource = null;
        }

        private void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in grvTrade.Rows)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no"
                    string _sad_itm_cd = dr.Cells[1].Value.ToString();
                    decimal _sad_qty = Convert.ToDecimal(dr.Cells[4].Value);
                    decimal _sad_unit_rt = Convert.ToDecimal(dr.Cells[6].Value);
                    Int32 _sad_itm_line = Convert.ToInt32(dr.Cells[5].Value);
                    bool _isForwardSale = Convert.ToBoolean(dr.Cells[7].Value);
                    string _invoice = dr.Cells[8].Value.ToString();
                    string _status = dr.Cells[9].Value.ToString();

                    MasterItem _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _sad_itm_cd);

                    if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == false)
                    {

                        if (_itms.Mi_is_ser1 == -1)
                        {
                            MessageBox.Show("Exchange not processing for decimal allow items", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        List<InventoryBatchN> _lst = new List<InventoryBatchN>();
                        _lst = CHNLSVC.Inventory.GetDeliveryOrderDetail(BaseCls.GlbUserComCode, _invoice, _sad_itm_line);

                        string _docno = string.Empty;
                        int _itm_line = -1;
                        int _batch_line = -1;

                        List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerialForReversal(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, string.Empty, _invoice, _sad_itm_line);
                        _popUpList = _serLst;
                        if (_lst != null && _serLst != null)
                            if (_sad_qty > 1)
                            {
                                //More than one qty
                                BindPopSerial(_serLst, false);
                                pnlPopUp.Visible = true;
                                pnlMain.Enabled = false;
                            }
                            else
                            {
                                //only one qty
                                _docno = _lst[0].Inb_doc_no;
                                _itm_line = _lst[0].Inb_itm_line;
                                _batch_line = _lst[0].Inb_batch_line;
                                AddInItem(_serLst[0]);
                                foreach (ReptPickSerials _lt in _popUpList)
                                {
                                    string _item = _lt.Tus_itm_cd;
                                    Int32 _serialID = _lt.Tus_ser_id;
                                    MasterItem _items = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    if (_items.Mi_is_ser1 != -1)
                                    {
                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_ser_id == Convert.ToInt32(_serialID)
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lssst in _selectedItemList
                                                                         where _lssst.Tus_ser_id == _one.Tus_ser_id
                                                                         select _lssst;

                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _selectedItemList.Add(_one);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }
                                    else
                                    {

                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_itm_cd == _item
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lsst in _selectedItemList
                                                                         where _lsst.Tus_itm_cd == _one.Tus_itm_cd
                                                                         select _lsst;
                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _selectedItemList.Add(_one);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }

                                }

                            }
                        BindSelectedItems(_selectedItemList);
                    }
                    else if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == true)
                    {
                        if (_sad_qty > 1)
                        {

                            var _duplicate = from _lsst in _selectedItemList
                                             where _lsst.Tus_itm_cd == _sad_itm_cd && _lsst.Tus_itm_stus == _status && _lsst.Tus_unit_price == _sad_unit_rt
                                             select _lsst;
                            if (_duplicate.Count() <= 0)
                            {

                                //string Msg = "<script>var name = prompt('Please enter your name', " + _sad_qty + ");if (name!=null && name!='') { document.getElementById('" + hdnUserQty.ClientID + "').value=name; } else { document.getElementById('<%=hdnUserQty.ClientID%>').value='-1';}  </script>";


                                //if (hdnUserQty.Value == "-1" || string.IsNullOrEmpty(hdnUserQty.Value.ToString())) return;

                                ReptPickSerials _one = new ReptPickSerials();
                                _one.Tus_base_doc_no = _invoice;
                                _one.Tus_base_itm_line = _sad_itm_line;
                                _one.Tus_bin = string.Empty;
                                _one.Tus_com = BaseCls.GlbUserComCode;
                                _one.Tus_cre_by = BaseCls.GlbUserID;
                                _one.Tus_cre_dt = DateTime.Now.Date;
                                _one.Tus_cross_batchline = -1;
                                _one.Tus_cross_itemline = -1;
                                _one.Tus_cross_seqno = -1;
                                _one.Tus_cross_serline = -1;
                                _one.Tus_doc_dt = DateTime.Now.Date;
                                _one.Tus_doc_no = _invoice;
                                _one.Tus_exist_grncom = string.Empty;
                                _one.Tus_exist_grndt = DateTime.Now.Date;
                                _one.Tus_exist_grnno = string.Empty;
                                _one.Tus_exist_supp = string.Empty;
                                _one.Tus_itm_brand = string.Empty;
                                _one.Tus_itm_cd = _sad_itm_cd;
                                _one.Tus_itm_desc = _itms.Mi_longdesc;
                                _one.Tus_itm_line = -1;
                                _one.Tus_itm_model = _itms.Mi_model;
                                _one.Tus_itm_stus = _status;
                                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                                _one.Tus_new_remarks = string.Empty;
                                _one.Tus_new_status = string.Empty;
                                _one.Tus_orig_grncom = string.Empty;
                                _one.Tus_orig_grndt = DateTime.Now.Date;
                                _one.Tus_orig_grnno = string.Empty;
                                _one.Tus_orig_supp = string.Empty;
                                _one.Tus_out_date = DateTime.Now.Date;
                                _one.Tus_qty = Convert.ToDecimal(1);
                                _one.Tus_seq_no = -1;
                                _one.Tus_ser_1 = string.Empty;
                                _one.Tus_ser_2 = string.Empty;
                                _one.Tus_ser_3 = string.Empty;
                                _one.Tus_ser_4 = string.Empty;
                                _one.Tus_ser_id = -1;
                                _one.Tus_ser_line = -1;
                                _one.Tus_serial_id = _serialId++.ToString();
                                _one.Tus_session_id = BaseCls.GlbUserSessionID;
                                _one.Tus_unit_cost = _sad_unit_rt;
                                _one.Tus_unit_price = _sad_unit_rt;
                                _one.Tus_usrseq_no = -1;
                                _one.Tus_warr_no = string.Empty;
                                _one.Tus_warr_period = 0;

                                _selectedItemList.Add(_one);

                            }

                        }
                        else
                        {

                            var _duplicate = from _lsst in _selectedItemList
                                             where _lsst.Tus_itm_cd == _sad_itm_cd && _lsst.Tus_itm_stus == _status && _lsst.Tus_unit_price == _sad_unit_rt
                                             select _lsst;

                            if (_duplicate.Count() <= 0)
                            {

                                ReptPickSerials _one = new ReptPickSerials();
                                _one.Tus_base_doc_no = _invoice;
                                _one.Tus_base_itm_line = _sad_itm_line;
                                _one.Tus_bin = string.Empty;
                                _one.Tus_com = BaseCls.GlbUserComCode;
                                _one.Tus_cre_by = BaseCls.GlbUserID;
                                _one.Tus_cre_dt = DateTime.Now.Date;
                                _one.Tus_cross_batchline = -1;
                                _one.Tus_cross_itemline = -1;
                                _one.Tus_cross_seqno = -1;
                                _one.Tus_cross_serline = -1;
                                _one.Tus_doc_dt = DateTime.Now.Date;
                                _one.Tus_doc_no = _invoice;
                                _one.Tus_exist_grncom = string.Empty;
                                _one.Tus_exist_grndt = DateTime.Now.Date;
                                _one.Tus_exist_grnno = string.Empty;
                                _one.Tus_exist_supp = string.Empty;
                                _one.Tus_itm_brand = string.Empty;
                                _one.Tus_itm_cd = _sad_itm_cd;
                                _one.Tus_itm_desc = _itms.Mi_longdesc;
                                _one.Tus_itm_line = -1;
                                _one.Tus_itm_model = _itms.Mi_model;
                                _one.Tus_itm_stus = _status;
                                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                                _one.Tus_new_remarks = string.Empty;
                                _one.Tus_new_status = string.Empty;
                                _one.Tus_orig_grncom = string.Empty;
                                _one.Tus_orig_grndt = DateTime.Now.Date;
                                _one.Tus_orig_grnno = string.Empty;
                                _one.Tus_orig_supp = string.Empty;
                                _one.Tus_out_date = DateTime.Now.Date;
                                _one.Tus_qty = _sad_qty;
                                _one.Tus_seq_no = -1;
                                _one.Tus_ser_1 = string.Empty;
                                _one.Tus_ser_2 = string.Empty;
                                _one.Tus_ser_3 = string.Empty;
                                _one.Tus_ser_4 = string.Empty;
                                _one.Tus_ser_id = -1;
                                _one.Tus_ser_line = -1;
                                _one.Tus_serial_id = _serialId++.ToString();
                                _one.Tus_session_id = BaseCls.GlbUserSessionID;
                                _one.Tus_unit_cost = _sad_unit_rt;
                                _one.Tus_unit_price = _sad_unit_rt;
                                _one.Tus_usrseq_no = -1;
                                _one.Tus_warr_no = string.Empty;
                                _one.Tus_warr_period = 0;

                                _selectedItemList.Add(_one);
                            }
                        }
                        BindSelectedItems(_selectedItemList);
                    }
                    CalculateTotalNewandOldAmount();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                bool isGenerate;
                if (_selectedItemList == null)
                    if (_selectedItemList.Count <= 0)
                    {
                        MessageBox.Show("Please select the return item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                if (_invoiceItemList == null && (cmbDocType.SelectedItem.ToString() == "Hire" || cmbDocType.SelectedItem.ToString() == "Request"))
                    if (_invoiceItemList.Count <= 0)
                    {
                        MessageBox.Show("Please select the issue item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    MessageBox.Show("Please select the usage amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (grvReturningDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("Returning items can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, string.Empty, dateTimePickerDate, lblError, dateTimePickerDate.Value.ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dateTimePickerDate.Value.Date != _date.Date)
                        {
                            MessageBox.Show("Back date not allow for selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Back date not allow for selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }




                CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014;
                string _documentType = string.Empty;


                if (cmbDocType.SelectedItem.ToString() == "Cash")
                {
                    RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                    _documentType = "Cash";
                }

                if (cmbDocType.SelectedItem.ToString() == "Hire")
                {
                    if (IsPriceBookChanged)
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                    }
                    else
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSEXCH, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008;
                        if (grvIssuingDetails.Rows.Count <= 0)
                        {
                            MessageBox.Show("Issuing items can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    _documentType = "Hire";
                }

                if (cmbDocType.SelectedItem.ToString() == "Request")
                {
                    RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocumentNo.Text.Trim());
                    _documentType = _hdr.Grah_remaks;

                    if (_documentType == "Cash")
                    {
                        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                    }

                    if (_documentType == "Hire")
                    {
                        if (IsPriceBookChanged)
                        {
                            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACREV, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                            _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                        }
                        else
                        {
                            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSEXCH, dateTimePickerDate.Value.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                            _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008;
                        }
                    }
                }

                //GlbReqIsApprovalNeed = true;
                //GlbReqUserPermissionLevel = 3;
                //GlbReqIsFinalApprovalUser = true;
                //GlbReqIsRequestGenerateUser = true;

                List<RequestApprovalHeader> _request = null;
                if (cmbDocType.SelectedItem.ToString() == "Request")
                {
                    List<RequestApprovalHeader> _lstTem = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT008", string.Empty, string.Empty);
                    List<RequestApprovalHeader> _lstTem1 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT013", string.Empty, string.Empty);

                    List<RequestApprovalHeader> _lst = new List<RequestApprovalHeader>();
                    if (_lstTem != null)
                        _lst.AddRange(_lstTem);
                    else if (_lstTem1 != null)
                        _lst.AddRange(_lstTem1);


                    _request = (from _res in _lst
                                where _res.Grah_ref == txtDocumentNo.Text
                                select _res).ToList<RequestApprovalHeader>();
                }
                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                if (_request != null && _request.Count > 0)
                {
                    ra_hdr = _request[0];
                    isGenerate = false;
                    ra_hdr.Grah_app_stus = "R";
                }
                else
                {
                    isGenerate = true;
                    #region fill RequestApprovalHeader


                    ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_app_dt = dateTimePickerDate.Value.Date;
                    ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdr.Grah_app_stus = "R";
                    ra_hdr.Grah_app_tp = _approvalCode.ToString();
                    ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                    ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_cre_dt = dateTimePickerDate.Value.Date;
                    ra_hdr.Grah_fuc_cd = txtDocumentNo.Text;
                    ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// GlbUserDefLoca;
                    ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_mod_dt = dateTimePickerDate.Value.Date;
                    ra_hdr.Grah_oth_loc = BaseCls.GlbUserDefProf;
                    ra_hdr.Grah_remaks = _documentType;

                    if (cmbDocType.SelectedItem.ToString() != "Request") ra_hdr.Grah_ref = txtDocumentNo.Text;
                    else ra_hdr.Grah_ref = null;
                }



                    #endregion

                #region fill List<RequestApprovalDetail> with Log
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

                Int32 _count = 1;

                foreach (ReptPickSerials _in in _selectedItemList)
                {
                    RequestApprovalDetail ra_det = new RequestApprovalDetail();
                    RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                    ra_det.Grad_ref = ra_hdr.Grah_ref;
                    ra_det.Grad_line = _count;
                    ra_det.Grad_req_param = _in.Tus_itm_cd; //Item

                    ra_det.Grad_val1 = _in.Tus_qty; //Qty
                    ra_det.Grad_val2 = _in.Tus_unit_price;//Unit Price
                    ra_det.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                    ra_det.Grad_val4 = _in.Tus_ser_line; //serial line
                    ra_det.Grad_val5 = _in.Tus_ser_id; //serial id

                    ra_det.Grad_anal1 = txtDocumentNo.Text; //account no
                    ra_det.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                    ra_det.Grad_anal3 = _in.Tus_doc_no;//DO no
                    ra_det.Grad_anal4 = _in.Tus_ser_1;//serial no
                    ra_det.Grad_anal5 = "IN";
                    ra_det.Grad_date_param = _date;

                    ra_det_List.Add(ra_det);

                    ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                    ra_det_log.Grad_date_param = _date;
                    ra_det_log.Grad_line = _count;
                    ra_det_log.Grad_req_param = _in.Tus_itm_cd;//Item

                    ra_det_log.Grad_val1 = _in.Tus_qty;//Qty
                    ra_det_log.Grad_val2 = _in.Tus_unit_price;//Unit Price
                    ra_det_log.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                    ra_det_log.Grad_val4 = _in.Tus_ser_line;//serial line
                    ra_det_log.Grad_val5 = _in.Tus_ser_id;//serial id

                    ra_det_log.Grad_anal1 = txtDocumentNo.Text;//account no
                    ra_det_log.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                    ra_det_log.Grad_anal3 = _in.Tus_doc_no;//DO no
                    ra_det_log.Grad_anal4 = _in.Tus_ser_1;//serial no
                    ra_det_log.Grad_anal5 = "IN";

                    ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                    ra_detLog_List.Add(ra_det_log);
                    _count += 1;
                }

                foreach (InvoiceItem _out in _invoiceItemList)
                {
                    RequestApprovalDetail ra_det = new RequestApprovalDetail();
                    RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                    ra_det.Grad_ref = ra_hdr.Grah_ref;
                    ra_det.Grad_line = _count;

                    ra_det.Grad_req_param = _out.Sad_itm_cd;//Item
                    ra_det.Grad_val1 = _out.Sad_qty;//Qty
                    ra_det.Grad_val2 = _out.Sad_unit_amt;//Unit Rate
                    ra_det.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                    ra_det.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                    ra_det.Grad_val5 = _out.Sad_seq;//PB SEQ

                    ra_det.Grad_anal1 = txtDocumentNo.Text;//account no
                    ra_det.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                    ra_det.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                    ra_det.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                    ra_det.Grad_anal5 = "OUT";
                    ra_det.Grad_anal10 = _out.Sad_pb_lvl;
                    ra_det.Grad_anal9 = _out.Sad_pbook;
                    ra_det_List.Add(ra_det);

                    ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                    ra_det_log.Grad_line = _count;

                    ra_det_log.Grad_req_param = _out.Sad_itm_cd;//Item
                    ra_det_log.Grad_val1 = _out.Sad_qty;//Qty
                    ra_det_log.Grad_val2 = _out.Sad_unit_amt;//Unit Rate
                    ra_det_log.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                    ra_det_log.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                    ra_det_log.Grad_val5 = _out.Sad_seq;//PB SEQ

                    ra_det_log.Grad_anal1 = txtDocumentNo.Text;//account no
                    ra_det_log.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                    ra_det_log.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                    ra_det_log.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                    ra_det_log.Grad_anal5 = "OUT";
                    ra_det_log.Grad_date_param = _date;
                    ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                    ra_detLog_List.Add(ra_det_log);

                    _count += 1;
                }

                RequestApprovalDetail ra_det_one = new RequestApprovalDetail();
                ra_det_one.Grad_ref = ra_hdr.Grah_ref;
                ra_det_one.Grad_line = _count++;
                ra_det_one.Grad_anal5 = "AMT";
                ra_det_one.Grad_req_param = cmbUsage.SelectedItem.ToString().ToUpper();
                ra_det_one.Grad_val1 = 1;
                ra_det_one.Grad_val2 = Convert.ToDecimal(txtAmount.Text.Trim());
                ra_det_one.Grad_anal1 = txtDocumentNo.Text;
                ra_det_List.Add(ra_det_one);


                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();
                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = _count;
                ra_detLog.Grad_anal5 = "AMT";
                ra_detLog.Grad_req_param = cmbUsage.SelectedItem.ToString().ToUpper();
                ra_detLog.Grad_val1 = 1;
                ra_detLog.Grad_val2 = Convert.ToDecimal(txtAmount.Text.Trim());
                ra_detLog.Grad_anal1 = txtDocumentNo.Text;
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog.Grad_date_param = _date;
                ra_detLog_List.Add(ra_detLog);


                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = dateTimePickerDate.Value.Date;
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "R";
                ra_hdrLog.Grah_app_tp = _approvalCode.ToString();
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = dateTimePickerDate.Value;
                ra_hdrLog.Grah_fuc_cd = txtDocumentNo.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = dateTimePickerDate.Value;

                ra_hdrLog.Grah_oth_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = _documentType;

                #endregion


                MasterAutoNumber auto = new MasterAutoNumber();
                auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                auto.Aut_cate_tp = "PC";
                auto.Aut_direction = 1;
                auto.Aut_modify_dt = null;
                auto.Aut_number = 0;
                auto.Aut_year = _date.Year;
                if (_approvalCode.ToString() == "ARQT008")
                {
                    auto.Aut_moduleid = "HSACRSC";
                    auto.Aut_start_char = "HSACRSC";
                }
                else if (_approvalCode.ToString() == "ARQT013")
                {
                    auto.Aut_moduleid = "HSACREV";
                    auto.Aut_start_char = "HSACREV";
                }
                else
                {
                    auto.Aut_moduleid = "HSACREV";
                    auto.Aut_start_char = "HSACREV";
                }
                string userseq;
                ra_hdr.Grah_app_stus = "R";
                string referenceNo;
                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(auto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, isGenerate, out userseq, out referenceNo);

                if (eff > 0)
                {
                    if (_serialList != null && _serialList.Count > 0)
                    {

                        CHNLSVC.General.Save_approve_ser_and_Log(_serialList, true, userseq, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser);
                    }
                    MessageBox.Show("Request Sent Successfully\nRequest No = " + userseq, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Request not sent", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAttSerApp_Click(object sender, EventArgs e)
        {
            pnlSerApp.Visible = true;
            pnlMain.Enabled = false;

            //get item to item

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


                var currrange = (from cur in _serialList
                                 where cur.Gras_anal2 == lblSerItem.Text.Trim() && cur.Gras_anal3 == lblSerial.Text.Trim() && cur.Gras_anal5 == txtJobNo.Text.Trim()
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {
                    MessageBox.Show("Selected details already exsist .", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Focus();
                    return;
                }

                //no validation to job code
                //must check job numbere invoice,serial,item match with request details


                RequestApprovalSerials _tmpRepSer = new RequestApprovalSerials();
                _tmpRepSer.Gras_anal2 = lblSerItem.Text.Trim();
                _tmpRepSer.Gras_anal3 = lblSerial.Text.Trim();
                _tmpRepSer.Gras_anal5 = txtJobNo.Text.Trim();

                _serialList.Add(_tmpRepSer);

                dgvSerApp.AutoGenerateColumns = false;
                dgvSerApp.DataSource = new List<RequestApprovalSerials>();
                dgvSerApp.DataSource = _serialList;

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

        private void btnSerAppClose_Click(object sender, EventArgs e)
        {
            pnlSerApp.Visible = false;
            pnlMain.Enabled = true;
        }

        private void grvReturningDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSerItem.Text = grvReturningDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
            lblSerial.Text = grvReturningDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dgvSerApp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Do you want to remove selected job details ?", "Hire sales reversal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (_serialList == null || _serialList.Count == 0) return;

                string _item = dgvSerApp.Rows[e.RowIndex].Cells["col_SerItem"].Value.ToString();
                string _serial = dgvSerApp.Rows[e.RowIndex].Cells["col_SerSerial"].Value.ToString();
                string _jobNo = dgvSerApp.Rows[e.RowIndex].Cells["col_JobNo"].Value.ToString();




                List<RequestApprovalSerials> _temp = new List<RequestApprovalSerials>();
                _temp = _serialList;

                _temp.RemoveAll(x => x.Gras_anal2 == _item && x.Gras_anal3 == _serial && x.Gras_anal5 == _jobNo);
                _serialList = _temp;


                dgvSerApp.AutoGenerateColumns = false;
                dgvSerApp.DataSource = new List<RequestApprovalSerials>();
                dgvSerApp.DataSource = _serialList;
            }
        }

        private void btnServiceAppConf_Click(object sender, EventArgs e)
        {
            pnlSerApp.Visible = false;
            pnlMain.Enabled = true;
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
                    lblSerRem.Text = _jobDet.Rows[0]["insa_jb_rem"].ToString();
                }
                else
                {
                    MessageBox.Show("Invalid service job # selected.", "Hire sales Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



    }
}
