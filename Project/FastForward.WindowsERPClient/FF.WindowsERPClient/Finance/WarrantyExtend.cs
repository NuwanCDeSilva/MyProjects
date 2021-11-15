using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Globalization;
using System.Diagnostics;

namespace FF.WindowsERPClient.Finance
{
    public partial class WarrantyExtend : Base
    {
        #region propeties

        public string _invTP
        {
            get { return _invTp; }
            set { _invTp = value; }
        }

        public decimal _maxAllowDays
        {
            get { return maxAllowDays; }
            set { maxAllowDays = value; }
        }

        public bool _IsRecall
        {
            get { return isRecall; }
            set { isRecall = value; }
        }

        public bool _RecStatus
        {
            get { return recStatus; }
            set { recStatus = value; }
        }

        public bool _sunUpload
        {
            get { return sunUpload; }
            set { sunUpload = value; }
        }


        public List<ReceiptWaraExtend> _recWaraExtend
        {
            get { return recWaraExtended; }
            set { recWaraExtended = value; }
        }

        public bool _isPartPay
        {
            get { return isPartPay; }
            set { isPartPay = value; }
        }

        public decimal _totComm
        {
            get { return totComm; }
            set { totComm = value; }
        }

        public int _lineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }

        public List<PriceBookLevelRef> _PriceBook
        {
            get { return priceBook; }
            set { priceBook = value; }
        }

        public List<PriceDetailRef> _price
        {
            get { return price; }
            set { price = value; }
        }

        public RecieptHeader _ReceiptHeader
        {
            get { return recieptHeader; }
            set { recieptHeader = value; }
        }

        public HpSystemParameters _SystemPara
        {
            get { return systemPara; }
            set { systemPara = value; }
        }

        public int _SeqID
        {
            get { return seqId; }
            set { seqId = value; }
        }

        public decimal _comAmt
        {
            get { return comAmt; }
            set { comAmt = value; }
        }

        public decimal _examount
        {
            get { return examount; }
            set { examount = value; }
        }

        public decimal GrndTotal
        {
            get { return grandTotal; }
            set { grandTotal = value; }
        }

        public MasterBankAccount _bankAccounts
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }


        string _invTp;
        decimal maxAllowDays;
        bool isRecall;
        bool recStatus;
        bool sunUpload;
        List<ReceiptWaraExtend> recWaraExtended;
        List<PriceBookLevelRef> priceBook;
        List<PriceDetailRef> price;
        RecieptHeader recieptHeader;
        HpSystemParameters systemPara;
        MasterBankAccount bankAccount;
        bool isPartPay;
        decimal totComm;
        int lineNo;
        int seqId;
        decimal comAmt;
        decimal examount;
        decimal grandTotal;
        int _daysDiff = 0;
        string _bkNo = "";


        #endregion

        decimal gvval = 0;
        string isgvrate = "";
        string gvtype = "";
        int gvexperiod = 0;
        public WarrantyExtend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to exit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #region page load

        private void WarrantyExtend_Load(object sender, EventArgs e)
        {
            try
            {
                _recWaraExtend = new List<ReceiptWaraExtend>();
                _PriceBook = new List<PriceBookLevelRef>();
                _price = new List<PriceDetailRef>();
                _ReceiptHeader = new RecieptHeader();
                _SystemPara = new HpSystemParameters();
                gvWaraDetails.AutoGenerateColumns = false;

                Clear_Data();
                chkOthPc.Checked = false;
                txtInv.Select();
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtDate, lblBackDate, string.Empty, out _allowCurrentTrans);
                ucPayModes1.Date = Convert.ToDateTime(dtDate.Text).Date; 
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


        #endregion

        #region search methods

        private void buttonInvoiceSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkOthPc.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtOthPCDet.Text))
                    {
                        MessageBox.Show("Please select other profit center code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthPCDet.Text = "";
                        txtOthPCDet.Focus();
                        return;
                    }
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarrantExtendItem);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchWarrantyExtend(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInv;
                _CommonSearch.ShowDialog();
                txtInv.Focus();
                LoadDetail();
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


        private void buttonDocNoSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
                DataTable _result = CHNLSVC.CommonSearch.GetReceiptsByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExNo;
                _CommonSearch.ShowDialog();
                txtExNo.Focus();
                LoadReciept();
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

        private void buttonItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInv.Text.Trim()))
                {
                    MessageBox.Show("Please select related invoice #.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvItem.Text = "";
                    txtInv.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvItem;
                _CommonSearch.ShowDialog();
                txtInvItem.Focus();
                CheckValidInvoiceItem();
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

        private void CheckValidInvoiceItem()
        {
            if (!string.IsNullOrEmpty(txtInvItem.Text))
            {
                InvoiceItem _invItem = new InvoiceItem();
                _invItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInv.Text.Trim(), txtInvItem.Text.Trim());

                if (_invItem == null)
                {
                    MessageBox.Show("Invalid invoice item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvItem.Text = "";
                    txtInvItem.Focus();
                    return;
                }
                else
                {
                    if (_invItem.Sad_do_qty <= 0)
                    {
                        MessageBox.Show("Item not dilivered can not extend warranty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }


                    decimal _invAmt = _invItem.Sad_tot_amt / _invItem.Sad_qty;
                    lblInvAmt.Text = _invAmt.ToString("0.00");
                }
            }
        }

        private void buttonSerialSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvItem.Text.Trim()))
                {
                    MessageBox.Show("Please select related invoice item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvItem.Focus();
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DeliverdSerials);
                DataTable _result = CHNLSVC.CommonSearch.GetDeliverdInvoiceItemSerials(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvSerial;
                _CommonSearch.ShowDialog();
                txtInvSerial.Focus();
                LoadSerial();
                CheckDodate();
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
                case CommonUIDefiniton.SearchUserControlType.WarrantExtendItem:
                    {
                        if (chkOthPc.Checked == true)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtOthPCDet.Text.Trim() + seperator + dtDate.Value.Date.ToString("dd/MMM/yyyy") + seperator + dtDate.Value.Date.AddDays(7).ToString("dd/MMM/yyyy") + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + dtDate.Value.Date.ToString("dd/MMM/yyyy") + seperator + dtDate.Value.Date.AddDays(7).ToString("dd/MMM/yyyy") + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "WAREX" + seperator + null + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInv.Text.Trim() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.DeliverdSerials:
                    {
                        if (chkOthPc.Checked == true)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator + txtInv.Text.Trim() + seperator + txtInvItem.Text.Trim() + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator + txtInv.Text.Trim() + seperator + txtInvItem.Text.Trim() + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();

        }

        #endregion

        #region data load methods

        private void LoadDetail()
        {
            Int32 _daysDiff = 0;
            btnSave.Enabled = true;
            if (!string.IsNullOrEmpty(txtInv.Text))
            {
                if (!ValidateInvoice(txtInv.Text))
                {
                    MessageBox.Show("Only Deliverd Can extend.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInv.Text = "";
                    return;
                }
                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                if (chkOthPc.Checked == true)
                {
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtOthPCDet.Text.Trim(), string.Empty, txtInv.Text.Trim(), "D", lblInvDate.Text, lblInvDate.Text);
                }
                else
                {
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, txtInv.Text.Trim(), "D", lblInvDate.Text, lblInvDate.Text);
                }

                if (_invHdr.Count == 0)
                {
                    MessageBox.Show("Invalid invoice.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInv.Text = "";
                    return;
                }

                foreach (InvoiceHeader _tempInv in _invHdr)
                {
                    if (_tempInv.Sah_inv_no == null)
                    {
                        MessageBox.Show("Invalid invoice.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInv.Text = "";
                        lblCusName.Text = "";
                        lblCusCode.Text = "";
                        lblCusAdd1.Text = "";
                        lblCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        _invTP = "";
                        txtInv.Focus();
                        return;
                    }
                    else
                    {
                        lblInvDate.Text = Convert.ToDateTime(_tempInv.Sah_dt).ToShortDateString();
                        lblCusCode.Text = _tempInv.Sah_cus_cd;
                        lblCusName.Text = _tempInv.Sah_cus_name;
                        lblCusAdd1.Text = _tempInv.Sah_cus_add1;
                        lblCusAdd2.Text = _tempInv.Sah_cus_add2;
                        _invTP = _tempInv.Sah_inv_tp;
                    }
                }
                //TimeSpan a = Convert.ToDateTime(dtDate.Value.Date) - Convert.ToDateTime(lblInvDate.Text.Trim());
                //_daysDiff = a.Days;

                //if (_daysDiff > _maxAllowDays)
                //{
                //    MessageBox.Show("Date Expire.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    btnSave.Enabled = false;
                //    return;
                //}
            }
        }


        private void chkIsMannual_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkIsMannual.Checked == true)
                {
                    txtManual.Text = "";
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_AVREC");
                    if (_NextNo != 0)
                    {
                        txtManual.Text = _NextNo.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find any available advance receipts.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtManual.Text = "";
                        chkIsMannual.Checked = false;
                        txtManual.Focus();
                        return;
                    }
                }

                else
                {
                    txtManual.Text = string.Empty;

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


        private void LoadReciept()
        {
            if (!string.IsNullOrEmpty(txtExNo.Text))
            {
                LoadSaveReceipt();
            }
        }

        private void LoadSaveReceipt()
        {

            _IsRecall = false;
            _RecStatus = false;
            _sunUpload = false;

            RecieptHeader _ReceiptHeader = null;
            _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtExNo.Text.Trim());
            if (_ReceiptHeader != null)
            {
                if (!ValidateReciept(txtExNo.Text))
                {
                    MessageBox.Show("Already Cancelled!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtExNo.Text = "";
                    return;
                }
                txtExNo.Text = _ReceiptHeader.Sar_receipt_no;
                txtManual.Text = _ReceiptHeader.Sar_manual_ref_no;
                txtRemarks.Text = _ReceiptHeader.Sar_remarks;
                txtTotal.Text = Convert.ToDecimal(_ReceiptHeader.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblCusCode.Text = _ReceiptHeader.Sar_debtor_cd;
                lblCusAdd1.Text = _ReceiptHeader.Sar_debtor_add_1;
                lblCusAdd2.Text = _ReceiptHeader.Sar_debtor_add_2;
                lblCusName.Text = _ReceiptHeader.Sar_debtor_name;
                _RecStatus = _ReceiptHeader.Sar_act;
                _sunUpload = _ReceiptHeader.Sar_uploaded_to_finance;
                dtDate.Value = _ReceiptHeader.Sar_receipt_date;
            }

            BindSaveReceiptDetails(_ReceiptHeader.Sar_receipt_no);
            BindSaveExtendWarra(_ReceiptHeader.Sar_receipt_no);
            _IsRecall = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = true;
            btnPrint.Enabled = true;

        }

        private void BindSaveReceiptDetails(string _RecNo)
        {
            RecieptItem _paramRecDetails = new RecieptItem();

            _paramRecDetails.Sard_receipt_no = _RecNo;

            List<RecieptItem> _list = CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);
            ucPayModes1.MainGrid.DataSource = _list;
            ucPayModes1.MainGrid.Enabled = false;
        }

        private void BindSaveExtendWarra(string _RecNo)
        {
            List<ReceiptWaraExtend> _list = CHNLSVC.Sales.GetWarrantyExtendReceipt(_RecNo);
            _recWaraExtend = new List<ReceiptWaraExtend>();
            _recWaraExtend = _list;
            gvWaraDetails.DataSource = _recWaraExtend;
        }

        private void LoadSerial()
        {
            if (!string.IsNullOrEmpty(txtInvSerial.Text))
            {

                InventorySerialN _delList = new InventorySerialN();
                _delList = CHNLSVC.Inventory.GetDeliveredSerialForItem(BaseCls.GlbUserComCode, txtInv.Text.Trim(), txtInvItem.Text.Trim(), txtInvSerial.Text.Trim());

                if (_delList != null)
                {
                    if (_delList.Ins_doc_no != null)
                    {
                        txtInvSerial.Text = _delList.Ins_ser_1;
                        lblOthSerial.Text = _delList.Ins_ser_2;
                        _SeqID = _delList.Ins_ser_id;
                        lblDo.Text = _delList.Ins_doc_no;
                        lblWarPeriod.Text = _delList.Ins_warr_period.ToString();
                        lblWaraNo.Text = _delList.Ins_warr_no;
                        lblDoDate.Text = Convert.ToDateTime(_delList.Ins_doc_dt).ToShortDateString();

                        TimeSpan a = Convert.ToDateTime(dtDate.Value.Date) - Convert.ToDateTime(lblDoDate.Text.Trim());
                        _daysDiff = a.Days;

                        if (_daysDiff > _maxAllowDays)
                        {
                            MessageBox.Show("Date Expire.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = false;
                            return;
                        }

                        ddlBook.DataSource = null;


                        List<PriceBookLevelRef> _def3 = CHNLSVC.Sales.getWarrExBook(BaseCls.GlbUserComCode, string.Empty, string.Empty);
                        _PriceBook = new List<PriceBookLevelRef>();
                        if (_def3 != null)
                        {
                            _PriceBook.AddRange(_def3);
                        }


                        var _final = (from _lst in _PriceBook
                                      select _lst.Sapl_pb).ToList().Distinct();

                        if (_final != null)
                        {

                            var source = new BindingSource();
                            source.DataSource = _final;
                            ddlBook.DataSource = source;

                            ddlBook_SelectionChangeCommitted(null, null);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid serial #.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvSerial.Text = "";
                        lblOthSerial.Text = "";
                        _SeqID = 0;
                        lblDo.Text = "";
                        lblWarPeriod.Text = "";
                        lblWaraNo.Text = "";

                        ddlBook.DataSource = null;

                        txtInvSerial.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid serial #.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvSerial.Text = "";
                    lblOthSerial.Text = "";
                    _SeqID = 0;
                    lblDo.Text = "";
                    lblWarPeriod.Text = "";
                    lblWaraNo.Text = "";

                    ddlBook.DataSource = null;

                    txtInvSerial.Focus();
                    return;
                }
            }
        }

        protected void BindPaymentType(ComboBox _ddl)
        {
            _ddl.DataSource = null;
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, "WAREX", dtDate.Value.Date, 1);
            if (_paymentTypeRef != null)
            {
                var source = new BindingSource();
                source.DataSource = _paymentTypeRef.OrderBy(items => items.Stp_pay_tp);
                _ddl.DataSource = source;
                _ddl.DisplayMember = "Stp_pay_tp";
                _ddl.ValueMember = "Stp_pay_tp";
            }
        }







        #endregion

        #region  button click events

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Data();
                chkOthPc.Checked = false;
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Process();
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
                Cancel();
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


        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //CHECK PERMISSION
                if (btnCancel.Enabled == true)
                {
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportTp = "REC";
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = txtExNo.Text;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;
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

        private void Process()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtDate, lblBackDate, dtDate.Value.ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtDate.Value.Date != _date.Date)
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
            if (string.IsNullOrEmpty(lblCusCode.Text))
            {
                MessageBox.Show("Customer is missing.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInv.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtInv.Text))
            {
                MessageBox.Show("Invoice is missing.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInv.Focus();
                return;
            }

            if (gvWaraDetails.Rows.Count <= 0)
            {
                MessageBox.Show("Extended details are not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInv.Focus();
                return;
            }
            if (ucPayModes1.RecieptItemList == null)
            {
                MessageBox.Show("Please add payment details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInv.Focus();
                return;
            }
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;
                decimal _commission = 0;

                foreach (RecieptItem _com in ucPayModes1.RecieptItemList)
                {
                    _commission = _commission + _com.Sard_anal_3;
                }

                RecieptHeader _ReceiptHeader = new RecieptHeader();
                _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "EXREC", 1, BaseCls.GlbUserComCode);
                _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _ReceiptHeader.Sar_receipt_type = "WAREX";
                _ReceiptHeader.Sar_receipt_no = txtExNo.Text.Trim();
                _ReceiptHeader.Sar_prefix = "WAREX";
                _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
                _ReceiptHeader.SAR_BK_NO = _bkNo;   //kapila 25/4/2016
                _ReceiptHeader.Sar_receipt_date = dtDate.Value.Date;
                _ReceiptHeader.Sar_direct = true;
                _ReceiptHeader.Sar_acc_no = "";
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
                _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _ReceiptHeader.Sar_debtor_cd = lblCusCode.Text.Trim();
                _ReceiptHeader.Sar_debtor_name = lblCusName.Text.Trim();
                _ReceiptHeader.Sar_debtor_add_1 = lblCusAdd1.Text.Trim();
                _ReceiptHeader.Sar_debtor_add_2 = lblCusAdd2.Text.Trim(); ;
                _ReceiptHeader.Sar_tel_no = "";
                _ReceiptHeader.Sar_mob_no = "";
                _ReceiptHeader.Sar_nic_no = "";
                _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(txtTotal.Text);
                _ReceiptHeader.Sar_comm_amt = _commission;
                _ReceiptHeader.Sar_is_mgr_iss = false;
                _ReceiptHeader.Sar_esd_rate = 0;
                _ReceiptHeader.Sar_wht_rate = 0;
                _ReceiptHeader.Sar_epf_rate = 0;
                _ReceiptHeader.Sar_currency_cd = "LKR";
                _ReceiptHeader.Sar_uploaded_to_finance = false;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                _ReceiptHeader.Sar_direct_deposit_branch = "";
                _ReceiptHeader.Sar_remarks = txtRemarks.Text.Trim();
                _ReceiptHeader.Sar_is_used = false;
                _ReceiptHeader.Sar_ref_doc = "";
                _ReceiptHeader.Sar_ser_job_no = "";
                _ReceiptHeader.Sar_used_amt = 0;
                _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _ReceiptHeader.Sar_anal_1 = "";
                _ReceiptHeader.Sar_anal_2 = "";
                _ReceiptHeader.Sar_anal_3 = "";
                _ReceiptHeader.Sar_anal_4 = "";
                _ReceiptHeader.Sar_anal_5 = 0;
                _ReceiptHeader.Sar_anal_6 = 0;
                _ReceiptHeader.Sar_anal_7 = 0;
                _ReceiptHeader.Sar_anal_8 = 0;
                _ReceiptHeader.Sar_anal_9 = 0;

                List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
                Int32 _line = 0;
                foreach (RecieptItem line in ucPayModes1.RecieptItemList)
                {
                    line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                    _line = _line + 1;
                    line.Sard_line_no = _line;
                    _ReceiptDetailsSave.Add(line);
                }


                List<ReceiptWaraExtend> _ReceiptWaraExtendSave = new List<ReceiptWaraExtend>();
                foreach (ReceiptWaraExtend Ext in _recWaraExtend)
                {
                    Ext.Srw_seq_no = _ReceiptHeader.Sar_seq_no;
                    if (chkPromo.Checked == true)
                    {
                        Ext.Srw_is_promo = 1;
                    }
                    else
                    {
                        Ext.Srw_is_promo = 0;
                    }

                    if (!string.IsNullOrEmpty(lblSeq.Text))
                    {
                        Ext.Srw_promo_seq = Convert.ToInt32(lblSeq.Text);
                    }
                    else
                    {
                        Ext.Srw_promo_seq = 0;
                    }
                    _ReceiptWaraExtendSave.Add(Ext);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "WAREX";
                masterAuto.Aut_year = null;

                string QTNum;
                
                row_aff = (Int32)CHNLSVC.Sales.SaveWarrExReceipt(_ReceiptHeader, _ReceiptDetailsSave, _ReceiptWaraExtendSave, masterAuto, out QTNum);

                if (chkIsMannual.Checked == true)
                {
                    CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManual.Text), QTNum);
                }
                if (row_aff == 1)
                {
                    MessageBox.Show("Successfully created.Receipt No: " + QTNum, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (string.IsNullOrEmpty(QTNum)) return;


                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportTp = "REC";
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = QTNum;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;

                    Clear_Data();
                    chkOthPc.Checked = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void Cancel()
        {
            if (string.IsNullOrEmpty(txtExNo.Text))
            {
                MessageBox.Show("Please select receipt #.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExNo.Focus();
                return;
            }

            if (_RecStatus == false)
            {
                MessageBox.Show("Select receipt is already cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExNo.Focus();
                return;
            }

            if (_sunUpload == true)
            {
                MessageBox.Show("Cannot cancel.Already uploaded to accounts.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtDate.Value.Date != DateTime.Now.Date)
            {
                //txtDate.Enabled = true;
                this.Cursor = Cursors.Default;
                MessageBox.Show("Cannot cancel. Receipt is back date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateRecStatus(false);
            _recWaraExtend = new List<ReceiptWaraExtend>();
            GrndTotal = 0;
            _IsRecall = false;
            _RecStatus = false;
            _lineNo = 0;
            _sunUpload = false;
            txtInv.ReadOnly = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = true;
        }

        #endregion



        #region key down events

        private void txtInv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadDetail();
                    txtExNo.Focus();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonInvoiceSearch_Click(null, null);
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

        private void txtExNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadReciept();
                    txtManual.Focus();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonDocNoSearch_Click(null, null);
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

        private void txtManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInvItem.Focus();
        }

        private void txtInvItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CheckValidInvoiceItem();
                    txtInvSerial.Focus();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonItemSearch_Click(null, null);
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

        private void txtInvSerial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadSerial();
                    ddlBook.Focus();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonSerialSearch_Click(null, null);
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

        //By darshana 30-01-2014
        private void LoadExPromo()
        {
            string _type = "";
            string _value = "";
            string _mainCat = "";
            string _subCat = "";
            string _brnd = "";
            Boolean _found = false;

            if (Convert.ToDecimal(lblInvAmt.Text) <= 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtInvItem.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(txtInvSerial.Text))
            {
                return;
            }

            //Get item propoties
            MasterItem _itmDet = new MasterItem();
            _itmDet = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, txtInvItem.Text.Trim(), 1);

            if (_itmDet.Mi_cd != null)
            {
                _mainCat = _itmDet.Mi_cate_1;
                _subCat = _itmDet.Mi_cate_2;
                _brnd = _itmDet.Mi_brand;
            }
            else
            {
                MessageBox.Show("Invalid item selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _mainCat = "";
                _subCat = "";
                _brnd = "";
                txtInvItem.Focus();
                return;
            }


            List<ExtendWaraParam> _exWaraList = new List<ExtendWaraParam>();
            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            DataTable curserdet = new DataTable();
            List<GiftVoucherPages> _gvDetails = new List<GiftVoucherPages>();

            if (_Saleshir.Count > 0)
            {

                foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    //check customer wise definition
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "CUS", lblCusCode.Text.Trim(), txtInvItem.Text.Trim(), null, null, null, null, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //check serial wise definition
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "SER", null, txtInvItem.Text.Trim(), txtInvSerial.Text.Trim(), null, null, null, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //check item wise definition
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "ITM", null, txtInvItem.Text.Trim(), null, null, null, null, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //check promotion wise definiont - PRO
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "PRO", null, null, null, null, null, null, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //Brand wise sub category
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "BRDSUBCAT", null, null, null, _brnd, _mainCat, _subCat, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //Brand wise main category
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "BRDMAINCAT", null, null, null, _brnd, _mainCat, null, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //Sub Cate wise
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "SUBCATE", null, null, null, null, _mainCat, _subCat, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }

                    //Main Cate wise
                    _exWaraList = CHNLSVC.Sales.GetExWaraParam(_type, _value, dtDate.Value.Date, Convert.ToDecimal(lblInvAmt.Text), _daysDiff, "MAINCAT", null, null, null, null, _mainCat, null, null);
                    if (_exWaraList != null)
                    {
                        _found = true;
                        goto L01;
                    }
                }
            }

        L01:
            if (_found == true)
            {
                bool Isfound = false;
                curserdet = CHNLSVC.Sales.Get_warr_det_by_ser(txtInvItem.Text, txtInvSerial.Text);
                if (curserdet.Rows.Count > 0)
                {
                    Isfound = true;
                    //foreach(DataRow row in curserdet.Rows)
                    //{
                    //    _gvDetails = CHNLSVC.Inventory.GetGiftVoucherByOthRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, row["srw_rec_no"].ToString());

                    //    if(_gvDetails != null)
                    //    {
                    //        Isfound = true;
                    //    }
                    //}
                }

                if (!Isfound)
                {
                    dgvPromoWarra.AutoGenerateColumns = false;
                    dgvPromoWarra.DataSource = new List<ExtendWaraParam>();
                    dgvPromoWarra.DataSource = _exWaraList;
                }
              
            }
            else
            {
                dgvPromoWarra.AutoGenerateColumns = false;
                dgvPromoWarra.DataSource = new List<ExtendWaraParam>();
            }


        }

        private void ddlBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlLevel.Focus();
        }



        #endregion

        #region textbox double click events

        private void txtInv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonInvoiceSearch_Click(null, null);
        }

        private void txtExNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonDocNoSearch_Click(null, null);
        }

        private void txtInvItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonItemSearch_Click(null, null);
        }

        private void txtInvSerial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSerialSearch_Click(null, null);
        }

        #endregion

        #region textbox leave events

        private void txtInvSerial_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadSerial();
                CheckDodate();
                LoadExPromo();
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

        private void txtInv_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadDetail();
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

        private void txtExNo_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadReciept();
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

        private void txtInvItem_Leave(object sender, EventArgs e)
        {
            try
            {
                CheckValidInvoiceItem();
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

        #endregion

        #region ddl selection change


        private void ddlBook_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (ddlBook.SelectedValue != null)
                {
                    List<PriceBookLevelRef> _def4 = CHNLSVC.Sales.getWarrExBook(BaseCls.GlbUserComCode, ddlBook.SelectedValue.ToString(), string.Empty);
                    _PriceBook = new List<PriceBookLevelRef>();
                    if (_def4 != null)
                    {
                        _PriceBook.AddRange(_def4);
                    }

                    var _final = (from _lst in _PriceBook
                                  select _lst.Sapl_pb_lvl_cd).ToList().Distinct();

                    if (_final != null)
                    {
                        var source = new BindingSource();
                        source.DataSource = _final;
                        ddlLevel.DataSource = source;
                        AutoPriceLevelChange();
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

        private void ddlLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                AutoPriceLevelChange();

                ucPayModes1.TotalAmount = Convert.ToDecimal(lblAmt.Text);
                ucPayModes1.PayModeCombo.Focus();
                ucPayModes1.InvoiceType = "WAREX";
                ucPayModes1.LoadData();
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

        private void AutoPriceLevelChange()
        {
            decimal _tax = 11;

            //if (Convert.ToDateTime(dtDate.Value) >= Convert.ToDateTime("24-Jan-2016"))
            //{
            //    _tax = Convert.ToDecimal("11");
            //}
            //else if (Convert.ToDateTime(dtDate.Value) >= Convert.ToDateTime("01-Jan-2016"))
            //{
            //    _tax = Convert.ToDecimal("12.5");
            //}
            if (Convert.ToDateTime(dtDate.Value) >= Convert.ToDateTime("16-Jul-2016"))
            {
                _tax = Convert.ToDecimal("11");
            }
            else
            {
                _tax = Convert.ToDecimal("11");
            }
            //kapila 20/7/2016
            _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", "ALL", "WREXTXRT", dtDate.Value.Date);

            if (_SystemPara.Hsy_cd != null)
            {
                _tax = _SystemPara.Hsy_val;
            }

            Int32 i = 0;
            List<PriceBookLevelRef> _def5 = CHNLSVC.Sales.getWarrExBook(BaseCls.GlbUserComCode, ddlBook.SelectedValue.ToString(), ddlLevel.SelectedValue.ToString());
            _PriceBook = new List<PriceBookLevelRef>();
            if (_def5 != null)
            {
                _PriceBook.AddRange(_def5);
            }

            foreach (PriceBookLevelRef j in _PriceBook)
            {
                lblNewPeriod.Text = j.Sapl_warr_period.ToString();
            }

            _price = new List<PriceDetailRef>();

            grvPriceDet.Visible = false;
            grvPriceDet.AutoGenerateColumns = false;
            grvPriceDet.DataSource = new List<PriceDetailRef>();

            _price = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, ddlBook.SelectedValue.ToString(), ddlLevel.SelectedValue.ToString(), string.Empty, txtInvItem.Text.Trim(), 1, Convert.ToDateTime(dtDate.Value));

            if (_price.Count <= 0)
            {
                lblAmt.Text = "0.00";
                lblWRemarks.Text = "";
            }
            else
            {
                if (_price.Count > 1)        //kapila 6/4/2017
                {
                    grvPriceDet.AutoGenerateColumns = false;
                    grvPriceDet.DataSource = new List<PriceDetailRef>();
                    grvPriceDet.DataSource = _price;
                    grvPriceDet.Visible = true;

                    lblAmt.Text = "0.00";
                    _examount = 0;
                    lblWRemarks.Text = "";
                }
                else
                {
                    foreach (PriceDetailRef h in _price)
                    {
                        lblAmt.Text = h.Sapd_itm_price.ToString("0.00");
                        _examount = h.Sapd_itm_price + (h.Sapd_itm_price * _tax / 100);
                        lblWRemarks.Text = h.Sapd_warr_remarks;
                        goto L1;
                    }
                }
            }
        L1: i = i + 1;
            lblAmt.Text = Convert.ToString(Convert.ToDecimal(lblAmt.Text) + (Convert.ToDecimal(lblAmt.Text) * _tax / 100));


            ucPayModes1.TotalAmount = Convert.ToDecimal(lblAmt.Text);
            ucPayModes1.InvoiceType = "WAREX";
            ucPayModes1.LoadData();
        }

        #endregion

        #region check methods

        private bool CheckBank(string bank)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetails(bank, "BANK");

                if (_bankAccounts.Mbi_cd != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool CheckBankBranch(string bank, string branch)
        {
            if (!string.IsNullOrEmpty(branch))
            {
                bool valid = CHNLSVC.Sales.validateBank_and_Branch(bank, branch, "BANK");
                return valid;
            }
            else
            {
                return false;
            }
        }

        protected void CheckValidManualRef(object sender, EventArgs e)
        {
            if (chkIsMannual.Checked == true)
            {
                if (txtManual.Text != "")
                {
                    _bkNo = "";
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManual.Text));
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtManual.Text = "";
                        txtManual.Focus();
                    }
                    //kapila 25/4/2016
                    DataTable _dtBk = CHNLSVC.Inventory.GetManualDocBookNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManual.Text), null);
                    if (_dtBk.Rows.Count > 0) _bkNo = _dtBk.Rows[0]["mdd_bk_no"].ToString();
                }
                else
                {
                    if (chkIsMannual.Checked == true)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtManual.Focus();
                    }
                }
            }
        }

        #endregion

        #region pay control enable disable



        #endregion

        private void Clear_Data()
        {
            txtInv.ReadOnly = false;
            txtInv.Text = "";
            txtExNo.Text = "";
            txtInvItem.Text = "";
            txtInvSerial.Text = "";
            lblOthSerial.Text = "";
            lblInvDate.Text = "";
            lblDoDate.Text = "";
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblDo.Text = "";
            lblWaraNo.Text = "";
            lblWarPeriod.Text = "";
            lblCusAdd1.Text = "";
            lblCusAdd2.Text = "";
            lblNewPeriod.Text = "";
            lblAmt.Text = "0.00";
            txtTotal.Text = "0.00";
            txtManual.Text = "";
            lblInvAmt.Text = "0.00";
            lblSeq.Text = "";
            txtOthPCDet.Text = "";
            txtOthPCDet.Enabled = false;
            btnSearchOthSR.Enabled = false;
            _daysDiff = 0;
            lblWRemarks.Text = "";
            txtRemarks.Text = "";

            _isPartPay = false;
            _totComm = 0;
            btnSave.Enabled = true;

            _lineNo = 0;
            _PriceBook = new List<PriceBookLevelRef>();
            _price = new List<PriceDetailRef>();
            _recWaraExtend = new List<ReceiptWaraExtend>();
            _ReceiptHeader = new RecieptHeader();


            dgvPromoWarra.AutoGenerateColumns = false;
            dgvPromoWarra.DataSource = new List<ExtendWaraParam>();

            _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "WAREXDTS", dtDate.Value.Date);

            if (_SystemPara.Hsy_cd != null)
            {
                _maxAllowDays = _SystemPara.Hsy_val;
            }

            DataTable _Itemtable = new DataTable();

            gvWaraDetails.DataSource = _Itemtable;
            ddlBook.DataSource = null;
            ddlLevel.DataSource = null;

            ucPayModes1.ClearControls();

            btnCancel.Enabled = false;
            btnPrint.Enabled = false;
            chkPromo.Enabled = true;
            chkPromo.Checked = false;

            txtInv.Focus();
        }

        private void CalculateGrandTotal(decimal _amt, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndTotal = GrndTotal + Convert.ToDecimal(_amt);

                txtTotal.Text = (GrndTotal).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            else//--
            {
                GrndTotal = GrndTotal - Convert.ToDecimal(_amt);

                txtTotal.Text = (GrndTotal).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
        }

        protected void BindAddExtendDet()
        {
            gvWaraDetails.DataSource = _recWaraExtend;
        }

        private ReceiptWaraExtend AssignWaraDataToObject()
        {
            ReceiptWaraExtend _tempWara = new ReceiptWaraExtend();

            _tempWara.Srw_seq_no = 1;
            _tempWara.Srw_line = _lineNo;
            _tempWara.Srw_rec_no = "1";
            _tempWara.Srw_inv_no = txtInv.Text.Trim();
            _tempWara.Srw_do_no = lblDo.Text.Trim();
            _tempWara.Srw_date = dtDate.Value.Date;
            _tempWara.Srw_itm = txtInvItem.Text.Trim();
            _tempWara.Srw_ser = txtInvSerial.Text.Trim();
            _tempWara.Srw_oth_ser = lblOthSerial.Text.Trim();
            _tempWara.Srw_warra = lblWaraNo.Text.Trim();
            _tempWara.Srw_ex_period = Convert.ToInt32(lblWarPeriod.Text);
            if (gvtype == "SGV")
            {
                _tempWara.Srw_new_period = 0;
            }
            else
            {
                _tempWara.Srw_new_period = Convert.ToInt32(lblNewPeriod.Text);
            }
           
            _tempWara.Srw_pb = ddlBook.SelectedValue.ToString();
            _tempWara.Srw_lvl = ddlLevel.SelectedValue.ToString();
            _tempWara.Srw_amt = _examount;
            _tempWara.Srw_cre_by = BaseCls.GlbUserID;
            _tempWara.Srw_cre_when = Convert.ToDateTime(DateTime.Now).Date;
            _tempWara.Srw_ser_id = _SeqID;
            _tempWara.Srw_comm_amt = _totComm;
            _tempWara.Srw_wara_rmk = lblWRemarks.Text.Trim();
            _tempWara.Swp_dis_val = gvval;
            _tempWara.Swp_is_drt = int.Parse(isgvrate);
            _tempWara.Swp_tp = gvtype;
            _tempWara.Swp_gv_period = gvexperiod;

            return _tempWara;
        }

        private void UpdateRecStatus(Boolean _RecUpdateStatus)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            try
            {
                RecieptHeader _UpdateReceipt = new RecieptHeader();
                _UpdateReceipt.Sar_receipt_no = txtExNo.Text.Trim();
                _UpdateReceipt.Sar_act = _RecUpdateStatus;
                _UpdateReceipt.Sar_com_cd = BaseCls.GlbUserComCode;
                _UpdateReceipt.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _UpdateReceipt.Sar_mod_by = BaseCls.GlbUserID;

                List<ReceiptWaraExtend> _tempWaraList = new List<ReceiptWaraExtend>();
                _tempWaraList = _recWaraExtend;

                row_aff = (Int32)CHNLSVC.Sales.CancelWaraRec(_UpdateReceipt, _tempWaraList);

                if (row_aff == 1)
                {
                    {
                        MessageBox.Show("Successfully cancelled. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Clear_Data();
                    chkOthPc.Checked = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void chkIsMannual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsMannual.Checked == true)
                {
                    txtManual.Text = "";
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_AVREC");
                    if (_NextNo != 0)
                    {
                        txtManual.Text = _NextNo.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find any available advance receipts.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtManual.Text = "";
                        chkIsMannual.Checked = false;
                        txtManual.Focus();
                        return;
                    }
                }
                else
                {
                    txtManual.Text = string.Empty;
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

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                if (ucPayModes1.Balance <= 0)
                {
                    _recWaraExtend = new List<ReceiptWaraExtend>();
                    _recWaraExtend.Add(AssignWaraDataToObject());
                    BindAddExtendDet();
                    toolStrip1.Focus();
                    btnSave.Select();
                }
                txtTotal.Text = ucPayModes1.PaidAmountLabel.Text;
                chkPromo.Enabled = false;
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

        protected bool ValidateInvoice(string invNo)
        {

            InvoiceHeader inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInv.Text);
            if (inv != null)
            {
                //only deliverd invoice
                if (inv.Sah_stus == "D" || inv.Sah_stus == "A")
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public void CheckDodate()
        {
            if (!string.IsNullOrEmpty(txtInv.Text) && !string.IsNullOrEmpty(txtInvItem.Text) && !string.IsNullOrEmpty(txtInvSerial.Text))
            {
                InventoryHeader _hdr = CHNLSVC.Sales.GetWarrDoDate(txtInv.Text, txtInvItem.Text, txtInvSerial.Text);
                if (_hdr != null)
                {
                    TimeSpan a = Convert.ToDateTime(dtDate.Value.Date) - Convert.ToDateTime(_hdr.Ith_doc_date);
                    _daysDiff = a.Days;

                    if (_daysDiff > _maxAllowDays)
                    {
                        MessageBox.Show("Date Expire.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                        return;
                    }
                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invoice item\\serial do date can not find", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = false;
                }
            }
        }

        protected bool ValidateReciept(string recNo)
        {
            RecieptHeader rec = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, recNo);
            if (rec != null)
            {
                if (rec.Sar_act)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        private void chkPromo_CheckedChanged(object sender, EventArgs e)
        {

            if (chkPromo.Checked == true)
            {
                lblNewPeriod.Text = "0";
                lblAmt.Text = "0.00";
                ddlBook.Enabled = false;
                ddlLevel.Enabled = false;
            }
            else
            {
                lblNewPeriod.Text = "0";
                lblAmt.Text = "0.00";
                ddlBook.Enabled = true;
                ddlLevel.Enabled = true;
            }
            ucPayModes1.TotalAmount = Convert.ToDecimal(lblAmt.Text);
            ucPayModes1.InvoiceType = "WAREX";
            ucPayModes1.LoadData();

        }

        private void dgvPromoWarra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (chkPromo.Checked == false)
                {
                    MessageBox.Show("Please select get from promotion option.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnSave.Enabled = true;

                Int32 _seqNo = 0;
                Int32 _isRate = 0;
                decimal _rtAmt = 0;
                Int32 _exPeriod = 0;
                Decimal _exChrg = 0;
                string _wRmk = "";


                foreach (DataGridViewRow row in dgvPromoWarra.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        chk.Value = false;
                    }

                }

                _seqNo = Convert.ToInt32(dgvPromoWarra.Rows[e.RowIndex].Cells["Seq"].Value);
                _isRate = Convert.ToInt32(dgvPromoWarra.Rows[e.RowIndex].Cells["col_IsRt"].Value);
                _rtAmt = Convert.ToDecimal(dgvPromoWarra.Rows[e.RowIndex].Cells["col_RtAmt"].Value);
                _exPeriod = Convert.ToInt32(dgvPromoWarra.Rows[e.RowIndex].Cells["col_ExPd"].Value);
                _wRmk = dgvPromoWarra.Rows[e.RowIndex].Cells["col_Wrmk"].Value.ToString();

                lblNewPeriod.Text = _exPeriod.ToString();
                if (_isRate == 1)
                {
                    _exChrg = (Convert.ToDecimal(lblInvAmt.Text) / 100) * _rtAmt;
                }
                else
                {
                    _exChrg = _rtAmt;
                }

                _examount = _exChrg;
                lblAmt.Text = _exChrg.ToString("0.00");
                lblSeq.Text = _seqNo.ToString();
                lblWRemarks.Text = _wRmk;

                gvval = Convert.ToDecimal(dgvPromoWarra.Rows[e.RowIndex].Cells["colGiftvou"].Value);
                isgvrate = dgvPromoWarra.Rows[e.RowIndex].Cells["colisrate"].Value == null ? string.Empty : dgvPromoWarra.Rows[e.RowIndex].Cells["colisrate"].Value.ToString();
                gvtype = dgvPromoWarra.Rows[e.RowIndex].Cells["colgvtype"].Value == null ? string.Empty : dgvPromoWarra.Rows[e.RowIndex].Cells["colgvtype"].Value.ToString();
                gvexperiod = _exPeriod;

                if (_exChrg <= 0)
                {
                    MessageBox.Show("Cannot proceed invoice amount.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    return;
                }

                ucPayModes1.TotalAmount = Convert.ToDecimal(lblAmt.Text);
                ucPayModes1.PayModeCombo.Focus();
                ucPayModes1.InvoiceType = "WAREX";
                ucPayModes1.LoadData();

                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvPromoWarra.Rows[dgvPromoWarra.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "False":
                        {
                            ch1.Value = true;
                            break;
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

        private void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchOthSR_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthPCDet; //txtBox;
                _CommonSearch.ShowDialog();
                txtOthPCDet.Select();

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

        private void chkOthPc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthPc.Checked == true)
            {
                Clear_Data();
                txtOthPCDet.Enabled = true;
                txtOthPCDet.Text = "";
                btnSearchOthSR.Enabled = true;
            }
            else
            {
                Clear_Data();
                chkOthPc.Checked = false;
                txtOthPCDet.Text = "";
                txtOthPCDet.Enabled = false;
                btnSearchOthSR.Enabled = false;
            }
        }

        private void txtOthPCDet_Leave(object sender, EventArgs e)
        {
            try
            {
                Boolean _isValid = false;

                if (!string.IsNullOrEmpty(txtOthPCDet.Text))
                {
                    if (chkOthPc.Checked == false)
                    {
                        MessageBox.Show("you have to select other PC option before select other profit center.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtOthPCDet.Text = "";
                        txtOthPCDet.Focus();
                        return;
                    }

                    _isValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtOthPCDet.Text.Trim());

                    if (_isValid == false)
                    {
                        MessageBox.Show("Please select valid profit center.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtOthPCDet.Text = "";
                        txtOthPCDet.Focus();
                        return;
                    }
                    else
                    {
                        if (BaseCls.GlbUserDefProf == txtOthPCDet.Text.Trim())
                        {
                            MessageBox.Show("Cannot select same profit center as other profit center.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtOthPCDet.Text = "";
                            txtOthPCDet.Focus();
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

        private void txtOthPCDet_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {

                    TextBox txtBox = new TextBox();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthPCDet; //txtBox;
                    _CommonSearch.ShowDialog();
                    txtOthPCDet.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtInv.Focus();
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

        private void txtOthPCDet_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthPCDet; //txtBox;
                _CommonSearch.ShowDialog();
                txtOthPCDet.Select();
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

        private void grvPriceDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal _tax = 11;
            if (e.RowIndex != -1)
            {
                decimal _price = Convert.ToDecimal(grvPriceDet.Rows[e.RowIndex].Cells[4].Value);
                _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", "ALL", "WREXTXRT", dtDate.Value.Date);

                if (_SystemPara.Hsy_cd != null)
                {
                    _tax = _SystemPara.Hsy_val;
                }

                lblAmt.Text = _price.ToString("0.00");
                _examount = _price + (_price * _tax / 100);
                lblWRemarks.Text = grvPriceDet.Rows[e.RowIndex].Cells[8].Value.ToString();

                lblAmt.Text = Convert.ToString(Convert.ToDecimal(lblAmt.Text) + (Convert.ToDecimal(lblAmt.Text) * _tax / 100));


                ucPayModes1.TotalAmount = Convert.ToDecimal(lblAmt.Text);
                ucPayModes1.InvoiceType = "WAREX";
                ucPayModes1.LoadData();
            }
        }
    }
}
