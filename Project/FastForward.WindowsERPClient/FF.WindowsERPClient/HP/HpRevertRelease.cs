using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FF.BusinessObjects;

//Web base system written by Prabhath (Original)
//Windows base system written by Prabhath on 7/01/2013 according to the web

namespace FF.WindowsERPClient.HP
{
    public partial class HpRevertRelease : FF.WindowsERPClient.Base
    {
        private List<HpAccount> AccountsList;
        private List<RecieptHeader> Receipt_List;
        private List<ReptPickSerials> SelectedItemList;
        private List<ReptPickSerials> OriginalRevertedItemList;
        public Decimal PaidAmount;
        public Decimal BalanceAmount;
        protected decimal _paidAmount;
        protected List<RecieptItem> _recieptItem;
        protected string RevertNo;
        protected string RevertInventoryDocument;
        public List<HpTransaction> Transaction_List;
        List<InvoiceItem> _itemList = new List<InvoiceItem>();
        private CommonSearch.CommonOutScan _commonOutScan = null;
        private Boolean _isECD = false;
        private string _ECDType = "";

        //txtDate
        //txtProfitCenter
        //txtAccountNo
        //ddlRequestNo
        //txtRPartRelease
        //txtRDiscount
        //lblReqReleaseAmount
        //lblReqDiscountAmount
        //chkApproved
        //gvATradeItem
        //gvRevetedItem
        //gvRevertedSerial
        //ucPayModes1
        //ucReciept1
        //ucHpAccountSummary1
        //lblSumAccBalance
        //lblSumReleaseAmt
        //lblSumDiscountAmt
        //lblTotalReceivable
        //lblSumReceipt
        //lblSumPay
        private Decimal eCDValue;

        public Decimal ECDValue
        {
            get { return eCDValue; }
            set { eCDValue = value; }
        }

        private string apprReqNo;

        public string ApprReqNo
        {
            get { return apprReqNo; }
            set
            {
                apprReqNo = value;
                txtVoucherNum.Text = value;
                set_ApprovedReqDet(apprReqNo);
            }
        }

        private void set_ApprovedReqDet(string app_reqNo)
        {
            try
            {
                //  string selectedApprovedReqNum = uc_ViewApprovedRequests1.SelectedReqNum;
                string selectedApprovedReqNum = app_reqNo;
                if (selectedApprovedReqNum == "")
                {
                    ddlECDType.SelectedValue = "";
                    return;
                }
                ApproveRequestUC APPROVE_ = new ApproveRequestUC();
                DataTable dt = new DataTable();
                dt = CHNLSVC.General.GetApprovedRequestDetails(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), lblAccountNo.Text.Trim(), "ARQT009", 1, 0);
                if (dt == null)
                {
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    #region

                    foreach (DataRow row in dt.Rows)
                    {
                        string reqno = row["GRAH_REF"].ToString();
                        if (reqno == selectedApprovedReqNum)
                        {
                            Decimal value = Convert.ToDecimal(row["GRAD_VAL1"]);
                            Int32 isRate = Convert.ToInt32(row["GRAD_IS_RT1"]);
                            if (isRate == 1)
                            {
                                Decimal ECDvalue_ = (value * Convert.ToDecimal(ucHpAccountSummary1.Uc_AccBalance) / 100);

                                lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_AccBalance - ECDvalue_);

                                // Session["ECDValue"] = ECDvalue_;
                                ECDValue = ECDvalue_;
                                divECDbal.Visible = true;
                            }
                            else
                            {
                                Decimal ECDvalue_ = value;
                                lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_AccBalance - ECDvalue_);
                                //lblApprovedReqECDval.Text = value.ToString();
                                // Session["ECDValue"] = ECDvalue_;
                                ECDValue = ECDvalue_; ;
                                divECDbal.Visible = true;
                            }
                            break;
                        }
                        else
                        {
                            lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_AccBalance);
                        }
                    }
                    #endregion
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

        private string isValidVoucher;

        public string IsValidVoucher
        {
            get { return isValidVoucher; }
            set { isValidVoucher = value; }
        }
        private void Ad_hoc_Session()
        {
            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";
            //BaseCls.GlbUserID = "ADMIN";

            //BaseCls.GlbUserComCode = "SGL";
            //BaseCls.GlbUserDefLoca = "SGMTR";
            //BaseCls.GlbUserDefProf = "SGMTR";
            //BaseCls.GlbUserID = "PRABHATH";

            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";
            //BaseCls.GlbUserID = "ADMIN";
        }

        private void button1_Click(object sender, EventArgs e)
        { if (MessageBox.Show("Do you need to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { this.Close(); } }

        public HpRevertRelease()
        {
            InitializeComponent(); InitializeComponents(); InitializeVariable(); _commonOutScan = new CommonSearch.CommonOutScan(); Ad_hoc_Session(); gvATradeItem.RowPrePaint += new DataGridViewRowPrePaintEventHandler(gvATradeItem_RowPrePaint); txtProfitCenter.Text = BaseCls.GlbUserDefProf; CLearRequestLabel(); ClearSummaryLabel(); ucReciept1.ValuePanl.Visible = false; ucReciept1.LoadRecieptPrefix(true); gvATradeItem.AutoGenerateColumns = false; gvRevetedItem.AutoGenerateColumns = false; gvRevertedSerial.AutoGenerateColumns = false; txtAccountNo.Focus();
            _commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.ClearControls();
           
            LoadPayMode();

            //kapila 31/3/2016
            MasterProfitCenter _MasterProfitCenter = new MasterProfitCenter();
            _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            txtMan.Text = _MasterProfitCenter.Mpc_man;
            ucReciept1.SelectedManager = txtMan.Text;

            //kapila
            btnECD.Enabled = false;
            MasterCompany COM_det = new MasterCompany();
            COM_det = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode); ;
            if (COM_det.MC_IS_ECD == 1)
                btnECD.Enabled = true;
        
        }

        private void InitializeComponents()
        { txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy"); ucHpAccountSummary1.Clear(); ucHpAccountSummary1.BorderStyle = BorderStyle.None; ucReciept1.FormName = this.Name; LoadPayMode(); radPartRelease.Enabled = true; txtRPartRelease.Enabled = true; radDiscount.Enabled = true; txtRDiscount.Enabled = true; }

        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            ucPayModes1.Date = Convert.ToDateTime(txtDate.Text).Date; 
        }

        private void LoadPayMode()
        { ucPayModes1.InvoiceType = "HPR"; ucPayModes1.ReceiptSubType = "RVT"; ucPayModes1.InvoiceItemList = _itemList; ucPayModes1.Customer_Code = lblCusCd.Text; ucPayModes1.LoadPayModes(); }

        private void InitializeVariable()
        { Receipt_List = new List<RecieptHeader>(); _recieptItem = new List<RecieptItem>(); Transaction_List = new List<HpTransaction>(); _paidAmount = 0; BalanceAmount = 0; PaidAmount = 0; _oneSide = false; ucReciept1.FormName = this.Name; btnECD.Enabled = true; }

        private void CLearRequestLabel()
        { lblReqReleaseAmount.Text = FormatToCurrency("0"); lblReqDiscountAmount.Text = FormatToCurrency("0"); }

        private void ClearSummaryLabel()
        { lblSumAccBalance.Text = FormatToCurrency("0"); lblSumReleaseAmt.Text = FormatToCurrency("0"); lblSumDiscountAmt.Text = FormatToCurrency("0"); lblTotalReceivable.Text = FormatToCurrency("0"); lblSumReceipt.Text = FormatToCurrency("0"); lblSumPay.Text = FormatToCurrency("0"); }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder(); string seperator = "|"; paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters: { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.HpAccount: { paramsText.Append(BaseCls.GlbUserComCode + seperator + txtProfitCenter.Text.Trim() + seperator + "R" + seperator); break; }
                default: break;
            }
            return paramsText.ToString();
        }

        private void btnSearch_ProfitCenter_Click(object sender, EventArgs e)
        { CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch(); _CommonSearch.ReturnIndex = 0; _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters); DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null); _CommonSearch.dvResult.DataSource = _result; _CommonSearch.BindUCtrlDDLData(_result); _CommonSearch.obj_TragetTextBox = txtProfitCenter; _CommonSearch.ShowDialog(); txtProfitCenter.Select(); }

        private void txtProfitCenter_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_ProfitCenter_Click(null, null); }

        private void txtProfitCenter_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter)                txtAccountNo.Focus(); if (e.KeyCode == Keys.F2)                btnSearch_ProfitCenter_Click(null, null); }

        private void btnSearch_Account_Click(object sender, EventArgs e)
        { if (string.IsNullOrEmpty(txtProfitCenter.Text)) { MessageBox.Show("Please select the valid profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch(); _CommonSearch.ReturnIndex = 0; _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount); DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null); _CommonSearch.dvResult.DataSource = _result; _CommonSearch.BindUCtrlDDLData(_result); _CommonSearch.obj_TragetTextBox = txtAccountNo; _CommonSearch.ShowDialog(); txtAccountNo.Select(); }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Enter)                btnRequest.Focus(); if (e.KeyCode == Keys.F2)                btnSearch_Account_Click(null, null); }

        private void txtAccountNo_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_Account_Click(null, null); }

        private void ProfitCenterValidate()
        { if (string.IsNullOrEmpty(txtProfitCenter.Text)) return; MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim()); if (_pc == null) { MessageBox.Show("Please select the valid profit center.", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); txtProfitCenter.Clear(); return; } if (string.IsNullOrEmpty(_pc.Mpc_com)) { MessageBox.Show("Please select the valid profit center.", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); txtProfitCenter.Clear(); return; } }

        private void txtProfitCenter_Leave(object sender, EventArgs e)
        { this.Cursor = Cursors.WaitCursor; ProfitCenterValidate(); this.Cursor = Cursors.Default; }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(txtAccountNo.Text)) { this.Cursor = Cursors.Default; return; }
            if (IsNumeric(txtAccountNo.Text.Trim()) == false)
            { MessageBox.Show("Please select a valid account no.", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Focus(); return; }
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            { MessageBox.Show("Please select the valid profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); txtProfitCenter.Text = string.Empty; this.Cursor = Cursors.Default; return; }

            string location = txtProfitCenter.Text.Trim();
            string acc_seq = txtAccountNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "R");
            AccountsList = accList;//save in view state
            if (accList == null || accList.Count == 0)
            { MessageBox.Show("Enter valid account number!", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return; }
            else if (accList.Count == 1)
            {
                foreach (HpAccount ac in accList)
                {
                    LoadAccountDetail(ac.Hpa_acc_no, DateTime.Now.Date);
                    InvoiceHeader _invHdr = new InvoiceHeader();
                    _invHdr = CHNLSVC.Sales.GetInvoiceHdrByCom(BaseCls.GlbUserComCode, ac.Hpa_invc_no);
                    lblCusCd.Text = _invHdr.Sah_cus_cd;


                }
            }
            else if (accList.Count > 1)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = lblAccountNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.ShowDialog();
                if (!string.IsNullOrEmpty(lblAccountNo.Text))
                    LoadAccountDetail(lblAccountNo.Text, DateTime.Now.Date);
            }



            LoadPayMode();
            this.Cursor = Cursors.Default;
        }

        private void BindAccountItem(string _account)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            InvoiceHeader _hdrs = null;
            if (_invoice != null)
                if (_invoice.Count > 0)
                    _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);
            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            if (_table.Rows.Count <= 0)
                gvATradeItem.DataSource = _table;
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

                    #endregion New Method

                    gvATradeItem.DataSource = new List<InvoiceItem>();
                    gvATradeItem.DataSource = _itemList;
                }
        }

        private HpRevertHeader GetRevertHeaderfromAccountnoForRelease(string _account, Int16 _status)
        {
            return CHNLSVC.Inventory.GetRevertHeaderfromAccountnoForRelease(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), _account, _status);
        }

        private void BindRevertItem(object _list)
        {
            if (_list != null)
                gvRevetedItem.DataSource = _list;
            else
                gvRevetedItem.DataSource = new List<ReptPickSerials>();
        }

        private void BindRevertSerial(List<ReptPickSerials> _list)
        {
            if (_list.Count>0)
            {
            AB:
                foreach (ReptPickSerials templist in _list)
                {
                    DataTable odt = CHNLSVC.Inventory.GetItems_git_get(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, templist.Tus_ser_id, "RVT");
                    if (odt.Rows.Count > 0)
                    {
                        _list.RemoveAll(x => x.Tus_ser_id == templist.Tus_ser_id);//remove othr loc itm
                        foreach (DataGridViewRow row in gvRevetedItem.Rows)
                        {
                            var newlist = _list.Where(r => r.Tus_itm_cd == Convert.ToString(row.Cells["Itm_Item"].Value)).ToList();
                            if (newlist.Count == 0)
                            {
                                row.Cells["Itm_AvlQty"].Value = 0;
                            }
                          
                        }
                        goto AB;
                    }
                } 
            }
           
          

            if (_list != null)
                if (_list.Count > 0)
                    gvRevertedSerial.DataSource = _list;
                else
                    gvRevertedSerial.DataSource = new List<ReptPickSerials>();
            else
                gvRevertedSerial.DataSource = new List<ReptPickSerials>();
        }

        private void BindRevertItemNSerial(List<ReptPickSerials> _list)
        {
            bool _isLocationNoRevertItem = false;
            string _avaLoc = string.Empty;
            string _avaitm = "";
            if (_list != null)
                if (_list.Count > 0)
                {
                    //_list.Where(x => string.IsNullOrEmpty(x.Tus_serial_id)).ToList().ForEach(y => y.Tus_serial_id = "0");
                    _avaLoc = _list[0].Tus_loc;
                    _avaitm = _list[0].Tus_itm_cd;      //kapila 28/7/2016

                    var _loc = (from _l in _list
                                where _l.Tus_loc != BaseCls.GlbUserDefLoca
                                select _l).ToList();
                       //kapila 28/7/2016

                    if (_loc != null)
                    {
                        if (_loc.Count > 0)
                        {
                            MessageBox.Show("Reverted item (" + _loc[0].Tus_itm_cd + ") available in different location, " + _loc[0].Tus_loc + ". You have to pick different compatible serial or transfer the product before release.", "Reverted Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                   // _list.Where(y => y.Tus_new_status == "1" && y.Tus_loc == _avaLoc && Convert.ToDecimal(y.Tus_serial_id) < y.Tus_qty).ToList().ForEach(x => x.Tus_serial_id = Convert.ToString(Convert.ToDecimal(x.Tus_serial_id) + 1));
                    //_list.RemoveAll(x => x.Tus_loc != BaseCls.GlbUserDefLoca);
                    var _item = (from _lst in _list
                                 group _lst by new { _lst.Tus_itm_cd, _lst.Tus_itm_desc, _lst.Tus_itm_model, _lst.Tus_itm_stus, _lst.Tus_serial_id, _lst.Tus_ser_line, _lst.Tus_doc_no, _lst.Tus_loc, _lst.Tus_avl_qty } into itm
                                 select new { Tus_itm_cd = itm.Key.Tus_itm_cd, Tus_itm_desc = itm.Key.Tus_itm_desc, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_serial_id = itm.Key.Tus_serial_id, Tus_ser_line = itm.Key.Tus_ser_line, Tus_doc_no = itm.Key.Tus_doc_no, Tus_loc = itm.Key.Tus_loc, Tus_avl_qty = itm.Key.Tus_avl_qty, Tus_qty = itm.Sum(p => p.Tus_qty) }).ToList();
                    //_item.Where(r => r.Tus_loc == BaseCls.GlbUserDefLoca).ToList().ForEach(y => y.Tmp_used_qty =  y.Tus_qty));
                    var _litmcopy=_item;
                   
                    BindRevertItem(_item);
                  
                    
                    //foreach (var itemlist in _item)
                    //{
                        //if (itemlist.Tus_loc == BaseCls.GlbUserDefLoca)
                        //{
                            foreach (DataGridViewRow row in gvRevetedItem.Rows)
                            {
                                var newlist=_litmcopy.Where(r=>r.Tus_itm_cd==Convert.ToString(row.Cells["Itm_Item"].Value)).ToList();
                                foreach (var itemlist in newlist)
                                {
                                    if (itemlist.Tus_loc == BaseCls.GlbUserDefLoca)
                                   // if (Convert.ToString(row.Cells["Itm_Item"].Value) == itemlist.Tus_itm_cd && )
                                    {
                                        row.Cells["Itm_AvlQty"].Value = itemlist.Tus_qty;
                                    }
                                    else
                                    {

                                        row.Cells["Itm_AvlQty"].Value = 0;
                                    }


                                }
                            }
                       // }
                        
                    //}
                }
            if (_avaLoc == BaseCls.GlbUserDefLoca)
            {
                var _avaLst = (from _l in _list
                               where Convert.ToInt32(_l.Tus_new_status) != 1
                               select _l).ToList();
                if (_avaLst != null)
                    if (_avaLst.Count > 0)
                    {
                        _isLocationNoRevertItem = true;
                        _list.RemoveAll(x => x.Tus_loc != BaseCls.GlbUserDefLoca);//remove othr loc itm
                        var _availableLst = _list.Where(x => Convert.ToInt32(x.Tus_new_status) == 1).ToList();
                        BindRevertSerial(_list);
                        //SelectedItemList = _availableLst;
                        SelectedItemList = _list;
                        bool _isOneSerialied = false;
                        Parallel.ForEach(_avaLst, x =>
                        {
                            int _val = CHNLSVC.Inventory.CheckItemSerialStatus(x.Tus_itm_cd);
                            if (_val != -2 && _val == 1) _isOneSerialied = true;
                        });
                        if (_isOneSerialied) _isLocationNoRevertItem = true; else _isLocationNoRevertItem = false;
                    }
                    else
                    {
                        _list.RemoveAll(x => x.Tus_loc != BaseCls.GlbUserDefLoca);//remove othr loc itm
                        BindRevertSerial(_list.Where(x => x.Tus_new_status == "1").ToList());
                        SelectedItemList = _list.Where(x => x.Tus_new_status == "1").ToList();
                    }
            }
            else
            {
                _isLocationNoRevertItem = true;
                _list.RemoveAll(x => x.Tus_loc != BaseCls.GlbUserDefLoca);//remove othr loc itm
                BindRevertSerial(_list.Where(x => x.Tus_new_status == "0").ToList());
                SelectedItemList = _list.Where(x => x.Tus_new_status == "0").ToList();
            }
            if (_isLocationNoRevertItem)
            { 
                //MessageBox.Show("Reverted item (" + _avaitm + ") available in different location, " + _avaLoc + ". You have to pick different compatible serial or transfer the product before release.", "Reverted Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //gvRevertedSerial.DataSource = null; 
            }
            return;
        }

        private List<ReptPickSerials> _list = new List<ReptPickSerials>();

        private void LoadRevertItemNSerial(string _account, Int16 _status)
        {
            HpRevertHeader _hdr = GetRevertHeaderfromAccountnoForRelease(_account, _status);
            if (_hdr != null)
            {
                string _revertno = _hdr.Hrt_ref;
                RevertNo = _revertno;
                _list = CHNLSVC.Inventory.GetAdjustmentDetailFromRevertNo(BaseCls.GlbUserComCode, _account, _revertno);
                OriginalRevertedItemList = _list;


               //// if (_list == null || _list.Count <= 0)
               // //{
                List<ReptPickSerials> _listtemp = new List<ReptPickSerials>();

                _listtemp = CHNLSVC.Inventory.GetAdjustmentDetailFromRvtintser(BaseCls.GlbUserComCode, _account, _revertno);
                _listtemp.ForEach(X => X.Tus_new_status = "0");
                foreach (ReptPickSerials item_temp in _list)
                {

                    _listtemp.RemoveAll(x => x.Tus_ser_id == item_temp.Tus_ser_id);
                }
                _list.AddRange(_listtemp);

                //}

                if (_list == null || _list.Count <= 0)
                {
                    MessageBox.Show("There is no reverted serials. Please contact IT Dept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!_list[0].Tus_loc.Contains(BaseCls.GlbUserDefLoca))
                    _list.ForEach(x => x.Tus_new_status = "0");

                RevertInventoryDocument = _list[0].Tus_doc_no;

                BindRevertItemNSerial(_list);
                return;
            }
            MessageBox.Show("There is no revert details available", "Revert Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CalculateRequest()
        {
            if (radDiscount.Checked)
                if (!string.IsNullOrEmpty(txtRDiscount.Text))
                {
                    if (!IsNumeric(txtRDiscount.Text.Trim()))
                    { MessageBox.Show("Please select valid discount", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRDiscount.Text = ""; lblReqDiscountAmount.Text = ""; lblReqReleaseAmount.Text = ""; txtRDiscount.Focus(); return; }

                    decimal _dis = Convert.ToDecimal(txtRDiscount.Text.Trim());
                    if (_dis > 100)
                    { MessageBox.Show("Rate can not be grater than 100%", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRDiscount.Text = ""; lblReqDiscountAmount.Text = ""; lblReqReleaseAmount.Text = ""; txtRDiscount.Focus(); return; }
                    lblReqReleaseAmount.Text = "";
                    lblReqDiscountAmount.Text = FormatToCurrency(Convert.ToString(ucHpAccountSummary1.Uc_AccBalance - ((ucHpAccountSummary1.Uc_AccBalance * Convert.ToDecimal(txtRDiscount.Text)) / 100)));
                }
            if (radPartRelease.Checked)
                if (!string.IsNullOrEmpty(txtRPartRelease.Text))
                {
                    if (!IsNumeric(txtRPartRelease.Text.Trim()))
                    { MessageBox.Show("Please select valid partial release", "Partial Release", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRPartRelease.Text = ""; lblReqDiscountAmount.Text = ""; lblReqReleaseAmount.Text = ""; txtRPartRelease.Focus(); return; }

                    decimal _dis = Convert.ToDecimal(txtRPartRelease.Text.Trim());
                    if (_dis > 100)
                    { MessageBox.Show("Rate can not be grater than 100%", "Partial Release Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRPartRelease.Text = ""; lblReqDiscountAmount.Text = ""; lblReqReleaseAmount.Text = ""; txtRPartRelease.Focus(); return; }
                    lblReqDiscountAmount.Text = "";
                    lblReqReleaseAmount.Text = FormatToCurrency(Convert.ToString(ucHpAccountSummary1.Uc_AccBalance * Convert.ToDecimal(txtRPartRelease.Text) / 100));
                }
        }

        private void CalculateBalanceSheet()
        {
            if (ucHpAccountSummary1.Uc_AccBalance != 0)
            {
                lblSumAccBalance.Text = FormatToCurrency(Convert.ToString(ucHpAccountSummary1.Uc_AccBalance));
                if (!string.IsNullOrEmpty(ddlRequestNo.Text) && chkApproved.Checked)
                {
                    decimal _value = 0;
                    CalculateRequest();
                    if (radDiscount.Checked)
                        _value = Convert.ToDecimal(lblReqDiscountAmount.Text);
                    else if (radPartRelease.Checked)
                        _value = Convert.ToDecimal(lblReqReleaseAmount.Text);
                    if (radDiscount.Checked) lblSumDiscountAmt.Text = FormatToCurrency(lblReqDiscountAmount.Text);
                    if (radPartRelease.Checked) lblSumReleaseAmt.Text = FormatToCurrency(lblReqReleaseAmount.Text);
                    lblTotalReceivable.Text = FormatToCurrency(Convert.ToString(_value));
                }
                else
                    lblTotalReceivable.Text = FormatToCurrency(lblSumAccBalance.Text);
                lblSumReceipt.Text = FormatToCurrency(lblTotalReceivable.Text);
                lblSumPay.Text = FormatToCurrency(lblTotalReceivable.Text);
            }
        }

        private void BindRequestsToDropDown(string _account, ComboBox _ddl, string _selectreq)
        {
            //if (BaseCls.GlbReqIsApprovalNeed)
            //{
            int _isApproval = 0;
            if (BaseCls.GlbReqIsRequestGenerateUser)
                if (chkApproved.Checked) _isApproval = 1;
                else _isApproval = 0;
            else _isApproval = 0;
            _ddl.DataSource = new List<RequestApprovalHeader>().OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref).ToList();
            txtRPartRelease.Clear();
            txtRDiscount.Clear();
            lblReqDiscountAmount.Text = "0.00";
            lblReqReleaseAmount.Text = "0.00";
            lblSumDiscountAmt.Text = "0.00";
            lblSumReleaseAmt.Text = "0.00";
            radPartRelease.Enabled = true;
            txtRPartRelease.Enabled = true;
            radDiscount.Enabled = true;
            txtRDiscount.Enabled = true;
            CalculateBalanceSheet();
            List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006.ToString(), _isApproval, BaseCls.GlbReqUserPermissionLevel);
            if (_lst != null)
            {
                if (_lst.Count > 0)
                {
                    var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref).ToList();
                    _ddl.DataSource = query;
                    if (!string.IsNullOrEmpty(_selectreq)) _ddl.Text = _selectreq;
                    RequestApprovalHeader _l = _lst.Where(X => X.Grad_ref == _ddl.Text).ToList()[0];
                    if (_l.Grad_req_param == "RVTRELEASE_RELEASE")
                    {
                        radPartRelease.Checked = true;
                        txtRPartRelease.Text = FormatToCurrency(Convert.ToString(_l.Grad_val1));
                        radDiscount.Enabled = false;
                        txtRDiscount.Enabled = false;
                    }
                    else if (_l.Grad_req_param == "RVTRELEASE_DISCOUNT")
                    {
                        radDiscount.Checked = true;
                        txtRDiscount.Text = FormatToCurrency(Convert.ToString(_l.Grad_val1));
                        radPartRelease.Enabled = false;
                        txtRPartRelease.Enabled = false;
                    }
                    CalculateBalanceSheet();
                }
            }
            //}
        }

        private void set_UcReceipt_values(bool _isfocus)
        {
            ucReciept1.SelectedManager = txtMan.Text;
            ucReciept1.RecieptDate = Convert.ToDateTime(txtDate.Text).Date;
            ucReciept1.SelectedProfitCenter = txtProfitCenter.Text.Trim();
            ucReciept1.AccountNo = lblAccountNo.Text;
            ucReciept1.AmountToPay = Convert.ToDecimal(lblSumReceipt.Text.Trim());
            ucReciept1.NeedCalculation = true;
            if (_isfocus) ucReciept1.RecieptNo.Focus();
        }

        private void LoadAccountDetail(string _account, DateTime _date)
        {
            lblAccountNo.Text = _account;
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(DateTime.Now.Date).Date, _account);
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in AccountsList)
            {
                if (_account == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }
            ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);
            BindAccountItem(account.Hpa_acc_no);
            LoadRevertItemNSerial(account.Hpa_acc_no, 0);
            btnSave.Enabled = true;
            BindRequestsToDropDown(account.Hpa_acc_no, ddlRequestNo, string.Empty);
            CalculateBalanceSheet();
            set_UcReceipt_values(false);
            set_PaidAmount();
            ucReciept1.SelectedManager = txtMan.Text;
        }

        private void RequestOptionChage(object sender, EventArgs e)
        {
            if (radDiscount.Checked)
            {
                txtRDiscount.Text = string.Empty;
                lblReqDiscountAmount.Text = FormatToCurrency("0");
                txtRPartRelease.Text = string.Empty;
                lblReqReleaseAmount.Text = FormatToCurrency("0");
                txtRDiscount.Focus();
            }
            else
                if (radPartRelease.Checked)
                {
                    txtRDiscount.Text = string.Empty;
                    lblReqDiscountAmount.Text = FormatToCurrency("0");
                    txtRPartRelease.Text = string.Empty;
                    lblReqReleaseAmount.Text = FormatToCurrency("0");
                    txtRPartRelease.Focus();
                }
        }

        private void gvATradeItem_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((bool)gvATradeItem["tra_IsForwardSale", e.RowIndex].Value)
                gvATradeItem.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
        }

        public static void ShowFormInControl(ref Control ctl, ref Form frm)
        {
            if (ctl != null && frm != null)
            {
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Visible = true;
                ctl.Controls.Add(frm);
            }
        }

        private void gvRevetedItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvRevetedItem.ColumnCount > 0)
            {
                Int32 _rowIndex = e.RowIndex;
                if (_rowIndex != -1 && e.ColumnIndex == 0)
                {
                    string _item = gvRevetedItem.Rows[_rowIndex].Cells["Itm_Item"].Value.ToString();
                    MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    //if (_itm.Mi_is_ser1 == 1)
                    //{
                    //    MessageBox.Show("Can't select serialized Item ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    string _approvedQty = gvRevetedItem.Rows[_rowIndex].Cells["Itm_Qty"].Value.ToString();
                    string _scanQty = gvRevetedItem.Rows[_rowIndex].Cells["Itm_AvlQty"].Value.ToString();
                    string _status = gvRevetedItem.Rows[_rowIndex].Cells["Itm_Status"].Value.ToString();
                    if (Convert.ToString(Convert.ToDecimal(_approvedQty) - Convert.ToDecimal(_scanQty)) != "0")
                    {
                        _commonOutScan.ScanDocument = RevertNo;
                        _commonOutScan.SelectedItemList = null;
                        _commonOutScan.DocumentType = "ADJ";
                        _commonOutScan.PopupItemCode = _item;
                        _commonOutScan.PopupQty = Convert.ToDecimal(_approvedQty);
                        _commonOutScan.ApprovedQty = Convert.ToDecimal(_approvedQty);
                        _commonOutScan.ScanQty = Convert.ToDecimal(_scanQty);
                        _commonOutScan.ItemStatus = _status;
                        _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                        //_commonOutScan.IsRevertStatus = true;
                        _commonOutScan.IsRevertStatus = false;
                        _commonOutScan.IsCheckStatus = true;
                        _commonOutScan.ModuleTypeNo = 2;
                        _commonOutScan.ShowDialog();
                    }
                }
            }
        }

        private void _commonOutScan_AddSerialClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_commonOutScan.SelectedItemList != null)
                    if (_commonOutScan.SelectedItemList.Count > 0)
                    {
                        MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _commonOutScan.PopupItemCode);
                        StringBuilder _errorserial = new StringBuilder();
                        StringBuilder _pickedserial = new StringBuilder();

                        foreach (ReptPickSerials gvr in _commonOutScan.SelectedItemList)
                        {
                            Int32 serID = Convert.ToInt32(gvr.Tus_ser_id);
                            string binCode = gvr.Tus_bin;
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, gvr.Tus_itm_cd, serID);

                            if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                            {
                                if (_itm.Mi_is_ser1 == 1)
                                    _pickedserial.Append(gvr.Tus_ser_1 + "/");
                            }
                        }

                        if (!string.IsNullOrEmpty(_pickedserial.ToString()))
                        {
                            MessageBox.Show("Selected Serial " + _pickedserial.ToString() + " already picked by other process. Please check your inventory", "Picked Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        foreach (ReptPickSerials gvr in _commonOutScan.SelectedItemList)
                        {
                            Int32 serID = Convert.ToInt32(gvr.Tus_ser_id);
                            //-------------
                            string binCode = gvr.Tus_bin;
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, gvr.Tus_itm_cd, serID);
                            if (_reptPickSerial_ == null || _reptPickSerial_.Tus_com == null)
                            {
                                if (_itm.Mi_is_ser1 == 0)
                                {
                                    _reptPickSerial_ = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _commonOutScan.PopupItemCode, _commonOutScan.ItemStatus, 1)[0];
                                    serID = _reptPickSerial_.Tus_ser_id;
                                }

                                if (_itm.Mi_is_ser1 == 1)
                                {
                                    MessageBox.Show("Selected serial " + gvr.Tus_ser_1 + " already picked by another process", "Picked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue;
                                }
                            }
                            List<ReptPickSerials> _ser = new List<ReptPickSerials>();
                            _list.RemoveAll(X => X.Tus_new_status == "0" && X.Tus_itm_cd == _commonOutScan.PopupItemCode);
                            _reptPickSerial_.Tus_new_status = "1";
                            _list.Add(_reptPickSerial_);
                            _ser.Add(_reptPickSerial_);
                            BindRevertItemNSerial(_list);
                            OriginalRevertedItemList = _list;
                        }
                    }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        public void set_PaidAmount()
        {
            Decimal PaidAmount = 0;
            if (ucReciept1.RecieptList != null)
            {
                foreach (RecieptHeader rh in ucReciept1.RecieptList)
                {
                    PaidAmount = PaidAmount + rh.Sar_tot_settle_amt;
                }
            }
            ucPayModes1.TotalAmount = PaidAmount;
            ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
            ucPayModes1.LoadData();
            if (ucReciept1.Balance == 0)
                ucPayModes1.button1.Focus();
        }

        private void ucReciept1_ItemAdded_1(object sender, EventArgs e)
        {
            set_PaidAmount();
        }

        private void fill_Transactions(RecieptHeader r_hdr)
        {
            HpTransaction tr = new HpTransaction();
            tr.Hpt_acc_no = lblAccountNo.Text.Trim();
            tr.Hpt_ars = 0;
            tr.Hpt_bal = 0;
            tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;
            tr.Hpt_cre_by = BaseCls.GlbUserID;
            tr.Hpt_cre_dt = Convert.ToDateTime(txtDate.Text);
            tr.Hpt_dbt = 0;
            tr.Hpt_com = BaseCls.GlbUserComCode;
            tr.Hpt_pc = BaseCls.GlbUserDefProf;
            if (r_hdr.Sar_is_oth_shop == true)
            {
                tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + BaseCls.GlbUserDefProf; ;
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
            }
            else
                tr.Hpt_desc = ("Payment receive").ToUpper();
            if (r_hdr.Sar_is_mgr_iss)
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no + (" issues").ToUpper();
            else
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
            tr.Hpt_pc = BaseCls.GlbUserDefProf;
            tr.Hpt_ref_no = "";
            tr.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;

                tr.Hpt_txn_ref = "";
            tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
            tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();
            if (Transaction_List == null)
                Transaction_List = new List<HpTransaction>();
            Transaction_List.Add(tr);
        }

        private bool IsBackDateOk(bool _isCheckLocation)
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty.ToUpper().ToString(), BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    _isOK = false;
                    return _isOK;
                }
            }
            return _isOK;
        }

        protected void Process(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRDiscount.Text))
            {
                if (Convert.ToDecimal(txtRDiscount.Text) >= 100 || Convert.ToDecimal(txtRDiscount.Text) < 0)
                {
                    if (Convert.ToDecimal(txtRDiscount.Text) == 100)
                    {
                        MessageBox.Show("You are not allowd for 100% discount.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("invalid discount.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            if (CheckServerDateTime() == false) return;
            if (IsBackDateOk(true) == false) return;
            if (MessageBox.Show("Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            bool _isPartlyPayment = radPartRelease.Checked;
            decimal _earlyClosingDiscount = 0;
            decimal _totalPaymentToBeDone = Convert.ToDecimal(lblTotalReceivable.Text.Trim());
            if (gvRevetedItem.Rows.Count == 0)
            { MessageBox.Show("Please select the reverted items", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (gvRevertedSerial.Rows.Count == 0)
            { MessageBox.Show("Please select the reverted serial", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (ucReciept1.RecieptList == null && _totalPaymentToBeDone > 0)
            { MessageBox.Show("Please select receipt detail", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if ((ucReciept1.RecieptList == null || ucReciept1.RecieptList.Count() <= 0) && _totalPaymentToBeDone > 0)
            { MessageBox.Show("Please select receipt detail", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (ucPayModes1.RecieptItemList == null && _totalPaymentToBeDone > 0)
            { MessageBox.Show("Please select the payment detail", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if ((ucPayModes1.RecieptItemList == null || ucPayModes1.RecieptItemList.Count == 0) && _totalPaymentToBeDone > 0)
            { MessageBox.Show("Please select the payment detail", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            { MessageBox.Show("Please select the profit center", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); txtProfitCenter.Focus(); return; }
            if (string.IsNullOrEmpty(txtDate.Text))
            { MessageBox.Show("Please select the date", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); return; }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            { MessageBox.Show("Please select the account no", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Focus(); return; }
            if (Math.Round(ucReciept1.Balance) > 0)
            { MessageBox.Show("Please settle the correct amount.", "Settle Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            decimal _receiptTotal = 0;
            decimal _paymentTotal = 0;
            if (ucReciept1.RecieptList != null && ucReciept1.RecieptList.Count > 0) _receiptTotal = ucReciept1.RecieptList.Sum(x => x.Sar_tot_settle_amt);
            if (ucPayModes1.RecieptItemList != null && ucPayModes1.RecieptItemList.Count > 0) _paymentTotal = ucPayModes1.RecieptItemList.Sum(x => x.Sard_settle_amt);
            decimal _accountBalance = Convert.ToDecimal(lblSumAccBalance.Text.Trim());
            decimal _partialReleaseTotal = Convert.ToDecimal(lblSumReleaseAmt.Text.Trim());
            decimal _partialDiscountTotal = Convert.ToDecimal(lblSumDiscountAmt.Text.Trim());
            if (_paymentTotal != _receiptTotal && _totalPaymentToBeDone > 0)
            { MessageBox.Show("Full payment required!. You have to pay total amount of " + FormatToCurrency(_accountBalance.ToString()), "With Out Approval", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(ddlRequestNo.Text))
            {
                if ((_receiptTotal - _accountBalance < 0 && _receiptTotal - _accountBalance > 10 || _receiptTotal != _paymentTotal) && _totalPaymentToBeDone > 0)
                { MessageBox.Show("Full payment required!. You have to pay total amount of " + FormatToCurrency(_accountBalance.ToString()), "With Out Approval", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            }
            else
            {
                if (radPartRelease.Checked)
                {
                    if (_receiptTotal != _paymentTotal)
                    { MessageBox.Show("Full payment required!. You have to pay total amount of " + FormatToCurrency(_receiptTotal.ToString()), "With Out Approval", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    _isPartlyPayment = true;
                }
                else if (radDiscount.Checked)
                {
                    if (_receiptTotal - _totalPaymentToBeDone < 0 && _receiptTotal - _totalPaymentToBeDone > 10 || _receiptTotal != _paymentTotal)
                    { MessageBox.Show("Full payment required!. You have to pay total amount of " + FormatToCurrency(_accountBalance.ToString()), "With Out Approval", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    _earlyClosingDiscount = _accountBalance - Convert.ToDecimal(lblSumDiscountAmt.Text.Trim());
                }
            }
            if (_isECD == true)
            {
                _earlyClosingDiscount = Convert.ToDecimal(lblSumDiscountAmt.Text.Trim());
                //  _accountBalance = _accountBalance -  _earlyClosingDiscount;

            }
            string _bin = BaseCls.GlbDefaultBin;
            OriginalRevertedItemList.ForEach(x => x.Tus_loc = BaseCls.GlbUserDefLoca);
            OriginalRevertedItemList.ForEach(x => x.Tus_bin = _bin);
            var _notAvailableNonSerialLst = OriginalRevertedItemList.Where(x => string.IsNullOrEmpty(x.Tus_new_status) || Convert.ToDecimal(x.Tus_new_status) == 0).ToList();
            if (_notAvailableNonSerialLst != null && _notAvailableNonSerialLst.Count >= 0)
            {
                var _statusWiseItemQty = (from _one in _notAvailableNonSerialLst group _one by new { _one.Tus_itm_cd, _one.Tus_itm_stus } into itm select new { Tus_itm_cd = itm.Key.Tus_itm_cd, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_qty = itm.Sum(p => p.Tus_qty) }).ToList();
                string _errorlst = string.Empty;
                foreach (var _one in _statusWiseItemQty)
                {
                    int _val = CHNLSVC.Inventory.CheckItemSerialStatus(_one.Tus_itm_cd);
                    if (_val != -2 && _val == 0)
                    {
                        List<ReptPickSerials> _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _one.Tus_itm_cd, _one.Tus_itm_stus, _one.Tus_qty, txtDate.Value.Date);
                        if (_chk == null || _chk.Count <= 0)
                            if (string.IsNullOrEmpty(_errorlst)) _errorlst = _one.Tus_itm_cd + "-" + _one.Tus_itm_stus + "-" + _one.Tus_qty;
                            else _errorlst = "/ " + _one.Tus_itm_cd + "-" + _one.Tus_itm_stus + "-" + _one.Tus_qty;
                        OriginalRevertedItemList.RemoveAll(x => x.Tus_itm_cd == _one.Tus_itm_cd && x.Tus_itm_stus == _one.Tus_itm_stus);
                        OriginalRevertedItemList.AddRange(_chk);
                    }
                }
            }

            Boolean isAccountClose = false;
            string ECD_type = "N/A";
            Decimal HED_ECD_VAL = 0;
            Decimal HED_ECD_CLS_VAL = 0;
            Int32 HED_IS_USE = 0;
            string HED_VOU_NO = string.Empty;
            string ECD_reqNo = string.Empty;
            Decimal ProtectionVal = ucHpAccountSummary1.Uc_ProtectionPRefund;
            List<HpTransaction> ECD_Txn_List = new List<HpTransaction>();

            //kapila 3/6/2016
            if (_isECD == true)
            {
                Decimal tot_receipt_amt = 0;
                HpTransaction tr = new HpTransaction();

                foreach (RecieptHeader rh in ucReciept1.RecieptList)
                {
                    tot_receipt_amt = tot_receipt_amt + rh.Sar_tot_settle_amt;
                    tr.Hpt_mnl_ref = rh.Sar_prefix + "-" + rh.Sar_manual_ref_no;
                    tr.Hpt_ref_no = rh.Sar_seq_no.ToString();
                }

                if (tot_receipt_amt < ucHpAccountSummary1.Uc_ECDnormalBal - ucHpAccountSummary1.Uc_ProtectionPRefund && _ECDType == "Normal")//Updated on 20-09-2012
                {
                    MessageBox.Show("Total Receipt amount is less than ECD Normal Balance!");
                    return;
                }
                if (_ECDType == "Normal")
                {
                    ECD_type = "N";
                    HED_ECD_VAL = Math.Round(ucHpAccountSummary1.Uc_ECDnormal, 2);
                }

                if (tot_receipt_amt < ucHpAccountSummary1.Uc_ECDspecialBal - ucHpAccountSummary1.Uc_ProtectionPRefund && _ECDType == "Special")
                {
                    MessageBox.Show("Total Receipt amount is less than ECD Special Balance!");
                    return;
                }
                if (_ECDType == "Special")
                {
                    ECD_type = "S";
                    HED_ECD_VAL = Math.Round(ucHpAccountSummary1.Uc_ECDspecial, 2);
                }
                //-------------------------
                if (_ECDType == "Voucher")
                {
                    if (txtVoucherNum.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter ECD voucher number!", "Voucher Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataTable dt = CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtDate.Text), lblAccountNo.Text.Trim(), txtVoucherNum.Text.Trim());
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("Invalid Voucher number or voucher date!");
                        IsValidVoucher = "InV";
                        lblECD_Balance.Text = "";
                        return;
                    }
                    else
                    {
                        IsValidVoucher = "V";
                        HED_VOU_NO = txtVoucherNum.Text.Trim();
                    }

                    try
                    {
                        Convert.ToDecimal(lblECD_Balance.Text);
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show("Voucher ECD balance not found!", "ECD Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (_ECDType == "Voucher" && IsValidVoucher.ToString() != "V")
                {

                    MessageBox.Show("Invalid Voucher#. Please enter correct values.");
                    return;
                }

                if (_ECDType == "Voucher" && tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text) - ucHpAccountSummary1.Uc_ProtectionPRefund)
                {

                    MessageBox.Show("Total Receipt amount is less than voucher balance!");
                    return;
                }

                if (_ECDType == "Voucher")
                {
                    ECD_type = "V";

                    HED_ECD_VAL = Convert.ToDecimal(txtVoucherAmt.Text.Trim());// Math.Round(ECDValue, 2); //changed on 25-06-2013

                    HED_ECD_CLS_VAL = tot_receipt_amt;//Math.Round(tot_receipt_amt, 2); //changed on 25-06-2013
                    HED_IS_USE = 1;
                }
                if (_ECDType == "Approved Req." && ApprReqNo != "none")
                {

                    if (tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text) - ucHpAccountSummary1.Uc_ProtectionPRefund)
                    {

                        MessageBox.Show("Total Receipt amount should match the Approved Request ECD balance!");
                        return;
                    }
                    ECD_type = "A";
                    ECD_reqNo = ApprReqNo; //uc_ViewApprovedRequests1.SelectedReqNum;
                    HED_ECD_VAL = Math.Round(ECDValue, 2);
                }

                isAccountClose = true; //when ECD is given, account is closed

                //-----------add on 05-04-2013
                if (_ECDType == "Approved Req." && ApprReqNo == "none")
                {
                    isAccountClose = false;
                }

                tr.Hpt_acc_no = lblAccountNo.Text.Trim();
                tr.Hpt_ars = 0;
                tr.Hpt_bal = 0;
                tr.Hpt_cre_by = BaseCls.GlbUserID;//BaseCls.GlbUserID;
                tr.Hpt_cre_dt = Convert.ToDateTime(txtDate.Text);
                tr.Hpt_com = BaseCls.GlbUserComCode;
                tr.Hpt_pc = BaseCls.GlbUserDefProf;

                tr.Hpt_pc = BaseCls.GlbUserDefProf;
                tr.Hpt_ref_no = "";
                tr.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;
                tr.Hpt_txn_ref = "";
                tr.Hpt_txn_tp = "ECD";
                tr.Hpt_desc = "EARLY CLOSING DISCOUNT";
                tr.Hpt_crdt = Convert.ToDecimal(HED_ECD_VAL); //ecd value
                tr.Hpt_dbt = 0; //Convert.ToDecimal(listECD_info[4]); //ecd value
                tr.Hpt_txn_ref = ECD_type;

                ECD_Txn_List.Add(tr);

            }
            //--------------------------------------------------------------------------
            HpRevertHeader _rvhdr = new HpRevertHeader();
            _rvhdr.Hrt_acc_no = lblAccountNo.Text;
            _rvhdr.Hrt_bal = 0;
            _rvhdr.Hrt_bal_cap = 0;
            _rvhdr.Hrt_bal_intr = 0;
            _rvhdr.Hrt_com = BaseCls.GlbUserComCode;
            _rvhdr.Hrt_cre_by = BaseCls.GlbUserID;
            _rvhdr.Hrt_cre_dt = DateTime.Now.Date;
            _rvhdr.Hrt_is_rls = false;
            _rvhdr.Hrt_mod_by = BaseCls.GlbUserID;
            _rvhdr.Hrt_mod_dt = DateTime.Now.Date;
            _rvhdr.Hrt_pc = txtProfitCenter.Text;
            _rvhdr.Hrt_ref = "0";
            _rvhdr.Hrt_rvt_dt = Convert.ToDateTime(txtDate.Text);
            _rvhdr.Hrt_seq = 0;
            _rvhdr.Hrt_rvt_comment = string.Empty;
            //ADDED BY SACHITH 2012/11/05
            //ADD HRT_RVT_BY,HRT_RLS_DT
            string _rvtBy = BaseCls.GlbUserID;
            _rvhdr.Hrt_rvt_by = _rvtBy;
            _rvhdr.Hrt_rls_dt = new DateTime(9999, 12, 31);
            //END
            InventoryHeader inHeader = new InventoryHeader();
            inHeader.Ith_acc_no = lblAccountNo.Text;
            inHeader.Ith_anal_10 = false;
            inHeader.Ith_anal_11 = false;
            inHeader.Ith_anal_12 = false;
            inHeader.Ith_anal_2 = lblAccountNo.Text;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_bus_entity = string.Empty;
            inHeader.Ith_cate_tp = "NOR";
            inHeader.Ith_channel = string.Empty;
            inHeader.Ith_com = BaseCls.GlbUserComCode;
            inHeader.Ith_com_docno = string.Empty;
            inHeader.Ith_cre_by = string.Empty;
            inHeader.Ith_cre_when = DateTime.MinValue;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";
            inHeader.Ith_direct = false;
            inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text.Trim());
            inHeader.Ith_doc_no = string.Empty;
            inHeader.Ith_doc_tp = "ADJ";
            inHeader.Ith_doc_year = DateTime.Today.Year;
            inHeader.Ith_entry_no = string.Empty;
            inHeader.Ith_entry_tp = string.Empty;
            inHeader.Ith_is_manual = true;
            inHeader.Ith_job_no = string.Empty;
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            inHeader.Ith_manual_ref = string.Empty;
            inHeader.Ith_mod_by = BaseCls.GlbUserID;
            inHeader.Ith_cre_by = BaseCls.GlbUserID;
            inHeader.Ith_mod_when = DateTime.MinValue;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_oth_docno = RevertNo;
            inHeader.Ith_remarks = string.Empty;
            inHeader.Ith_sbu = string.Empty;
            //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
            inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = "RV";
            inHeader.Ith_vehi_no = string.Empty;
            inHeader._warrNotupdate = true;//add by tharanga 2017/11/20
            inHeader.Ith_gen_frm = "SCMWIN";
            MasterAutoNumber invAuto = new MasterAutoNumber();
            invAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            invAuto.Aut_cate_tp = "LOC";
            invAuto.Aut_direction = null;
            invAuto.Aut_modify_dt = null;
            invAuto.Aut_moduleid = "ADJ";
            invAuto.Aut_number = 0;
            invAuto.Aut_start_char = "ADJ";
            invAuto.Aut_year = null;
            MasterAutoNumber rvAuto = new MasterAutoNumber();
            rvAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            rvAuto.Aut_cate_tp = "PC";
            rvAuto.Aut_direction = 1;
            rvAuto.Aut_modify_dt = null;
            rvAuto.Aut_moduleid = "RV";
            rvAuto.Aut_number = 0;
            rvAuto.Aut_start_char = "RV";
            rvAuto.Aut_year = null;
            string _rvdoc = string.Empty;
            string _adjdoc = string.Empty;
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "HP";
            _receiptAuto.Aut_number = 0;
            _receiptAuto.Aut_year = null;
            List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
            receiptHeaderList = ucReciept1.RecieptList; ;
            List<RecieptItem> receipItemList = new List<RecieptItem>();
            receipItemList = ucPayModes1.RecieptItemList;
            List<RecieptItem> save_receipItemList = new List<RecieptItem>();
            List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
            Int32 tempHdrSeq = 0;
            Transaction_List = new List<HpTransaction>();
            if (receiptHeaderList != null && receiptHeaderList.Count > 0)
                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_seq_no = tempHdrSeq;

                    if (_h.Sar_is_oth_shop == true)
                    {
                        _h.Sar_oth_sr = txtProfitCenter.Text.Trim();
                    }
                    else
                    {
                        _h.Sar_oth_sr = null;
                    }
                    fill_Transactions(_h);
                    tempHdrSeq--;
                    Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;
                    _h.Sar_anal_5 = ucHpAccountSummary1.Uc_Inst_CommRate;
                    _h.Sar_comm_amt = (ucHpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                    _h.Sar_session_id = BaseCls.GlbUserSessionID;

                    foreach (RecieptItem _i in receipItemList)
                    {
                        if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                        {
                            RecieptItem ri = new RecieptItem();
                            ri.Sard_settle_amt = _i.Sard_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            ri.Sard_ref_no = _i.Sard_ref_no;
                            ri.Sard_anal_3 = _i.Sard_anal_3;
                            save_receipItemList.Add(ri);
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                            _i.Sard_settle_amt = 0;
                        }
                        else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                        {
                            RecieptItem ri = new RecieptItem();
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;
                            ri.Sard_settle_amt = _h.Sar_tot_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            ri.Sard_ref_no = _i.Sard_ref_no;
                            save_receipItemList.Add(ri);
                            _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;
                        }
                    }
                    _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;
                }
            MasterAutoNumber _transactionAuto = new MasterAutoNumber();
            _transactionAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _transactionAuto.Aut_cate_tp = "PC";
            _transactionAuto.Aut_direction = 1;
            _transactionAuto.Aut_modify_dt = null;
            _transactionAuto.Aut_moduleid = "HP";
            _transactionAuto.Aut_number = 0;
            _transactionAuto.Aut_start_char = "HPT";
            _transactionAuto.Aut_year = null;
            string _outInvDocument = string.Empty;
            string _outSaleDocument = string.Empty;
            int _effect = 0;

            RecieptHeader Rh = null;
            if (receiptHeaderList != null && receiptHeaderList.Count > 0)//Add this chamal 08-May-2014
            {
                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    Rh = null;
                    Rh = CHNLSVC.Sales.Get_ReceiptHeader(_h.Sar_prefix.Trim(), _h.Sar_manual_ref_no.Trim());

                    if (Rh != null)
                    {
                        MessageBox.Show("Receipt number : " + _h.Sar_manual_ref_no + "already used.", "HP Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            try
            {
                btnSave.Enabled = false;
                btnClear.Enabled = false;
                
                _effect = CHNLSVC.Sales.SaveRevertRelease(txtProfitCenter.Text.Trim(), inHeader, invAuto, OriginalRevertedItemList, null, receiptHeaderList, _receiptAuto, save_receipItemList, out _outInvDocument, out _outSaleDocument, _isPartlyPayment, _accountBalance, _receiptTotal, _earlyClosingDiscount, _accountBalance - _receiptTotal - _earlyClosingDiscount, Transaction_List, _transactionAuto, ECD_Txn_List);
                btnSave.Enabled = true;
                btnClear.Enabled = true;

                if (_effect == -1 || _effect == -99)
                {
                    MessageBox.Show("Error generated while processing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                btnSave.Enabled = true;
                btnClear.Enabled = true;
                MessageBox.Show("Document not generated due to internal error. Please retry!");
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                btnSave.Enabled = true;
                btnClear.Enabled = true;
                if (_effect >= 1)
                {
                    string Msg = "Successfully Saved! Document No : " + _outSaleDocument + " and inventory document : " + _outInvDocument + ".";
                    MessageBox.Show(Msg, "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //BaseCls.GlbReportName = string.Empty;
                    //GlbReportName = string.Empty;
                    //_view.GlbReportName = string.Empty;
                    //BaseCls.GlbReportTp = "RVT";
                    //BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRevertSRN.rpt" : "RevertSRN.rpt";
                    //BaseCls.GlbReportDoc = _outInvDocument;
                    //_view.Show();
                    //_view = null;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "OUTWARD";
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                    _view.GlbReportDoc = _outInvDocument;
                    _view.Show();
                    _view = null;

                    BaseCls.GlbReportTp = "INV";
                    FF.WindowsERPClient.Reports.Sales.ReportViewer _views = new FF.WindowsERPClient.Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    _views.GlbReportName = string.Empty;
                    _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt";
                    _views.GlbReportDoc = _outSaleDocument;
                    _views.Show();
                    _views = null; 
                 


                    if (ucReciept1.radioButtonSystem.Checked == true)
                    {
                        //Reports.HP.ReportViewerHP _hpRec = new Reports.HP.ReportViewerHP();
                        //BaseCls.GlbReportName = string.Empty;
                        //_hpRec.GlbReportName = string.Empty;
                        //BaseCls.GlbReportTp = "REC";
                        //if (BaseCls.GlbUserComCode == "SGL") BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                        //else BaseCls.GlbReportName = "HPReceiptPrint.rpt";

                        //BaseCls.GlbReportDoc = _outSaleDocument;
                        //_hpRec.Show();
                        //_hpRec = null;
                    }
                    btnClear_Click(null, null);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            InitializeComponents();
            InitializeVariable();
            _itemList = new List<InvoiceItem>();
            Ad_hoc_Session();
            gvATradeItem.RowPrePaint += new DataGridViewRowPrePaintEventHandler(gvATradeItem_RowPrePaint);
            txtProfitCenter.Text = BaseCls.GlbUserDefProf;
            CLearRequestLabel();
            ClearSummaryLabel();
            ucReciept1.ValuePanl.Visible = false;
            ucReciept1.LoadRecieptPrefix(true);
            gvATradeItem.AutoGenerateColumns = false;
            gvRevetedItem.AutoGenerateColumns = false;
            gvRevertedSerial.AutoGenerateColumns = false;
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.ClearControls();
            LoadPayMode();
            BackDatePermission();
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
            MasterProfitCenter _MasterProfitCenter = new MasterProfitCenter();
            _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            txtMan.Text = _MasterProfitCenter.Mpc_man;
            ucReciept1.SelectedManager = txtMan.Text;

            //kapila
            btnECD.Enabled = false;
            MasterCompany COM_det = new MasterCompany();
            COM_det = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode); ;
            if (COM_det.MC_IS_ECD == 1)
                btnECD.Enabled = true;

            txtAccountNo.Focus();
        }

        protected void btnSendEcdReq_Click(object sender, EventArgs e)
        {
            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSRVTRL, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

            if (radDiscount.Checked && string.IsNullOrEmpty(txtRDiscount.Text))
            {
                MessageBox.Show("Please select the discount", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (radPartRelease.Checked && string.IsNullOrEmpty(txtRPartRelease.Text))
            {
                MessageBox.Show("Please select the part release", "part release", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Added by Prabhath on 11/10/2013
            if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT006", lblAccountNo.Text.Trim()))
            { return; }

            if (BaseCls.GlbReqIsApprovalNeed == true)
            {
                #region Auto Number

                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "HSRVTRL";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_start_char = "HSRVTRL";
                _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Value.Date).Year;

                #endregion Auto Number

                //send custom request.

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006.ToString();
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdr.Grah_fuc_cd = lblAccountNo.Text;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// GlbUserDefLoca;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);

                if (BaseCls.GlbUserDefProf != txtProfitCenter.Text.Trim())
                {
                    ra_hdr.Grah_oth_loc = "1";
                }
                else
                { ra_hdr.Grah_oth_loc = "0"; }

                if (string.IsNullOrEmpty(ddlRequestNo.Text.ToString()))
                {
                    ra_hdr.Grah_ref = string.Empty;
                }
                else
                {
                    ra_hdr.Grah_ref = ddlRequestNo.Text.Trim();
                }
                // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdr.Grah_remaks = "HP Revert Release";

                #endregion fill RequestApprovalHeader

                #region fill List<RequestApprovalDetail>

                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = radDiscount.Checked ? "RVTRELEASE_DISCOUNT" : "RVTRELEASE_RELEASE";
                ra_det.Grad_val1 = radDiscount.Checked ? Convert.ToDecimal(txtRDiscount.Text.Trim()) : radPartRelease.Checked ? Convert.ToDecimal(txtRPartRelease.Text.Trim()) : 0;
                ra_det.Grad_is_rt1 = true;
                ra_det.Grad_anal1 = lblAccountNo.Text;
                ra_det.Grad_date_param = Convert.ToDateTime(txtDate.Text).AddDays(10);
                ra_det_List.Add(ra_det);

                #endregion fill List<RequestApprovalDetail>

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006.ToString();
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);
                if (BaseCls.GlbUserDefProf != txtProfitCenter.Text.Trim())
                {
                    ra_hdrLog.Grah_oth_loc = "1";
                }
                else
                { ra_hdrLog.Grah_oth_loc = "0"; }

                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion fill RequestApprovalHeaderLog

                #region fill List<RequestApprovalDetailLog>

                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = radDiscount.Checked ? "RVTRELEASE_DISCOUNT" : "RVTRELEASE_RELEASE";
                ra_detLog.Grad_val1 = radDiscount.Checked ? Convert.ToDecimal(txtRDiscount.Text.Trim()) : radPartRelease.Checked ? Convert.ToDecimal(txtRPartRelease.Text.Trim()) : 0;
                ra_detLog.Grad_is_rt1 = true;
                ra_detLog.Grad_anal1 = lblAccountNo.Text;
                ra_detLog.Grad_date_param = Convert.ToDateTime(txtDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                #endregion fill List<RequestApprovalDetailLog>

                string referenceNo = string.Empty;
                Int32 eff = -1;
                try
                {
                    eff = CHNLSVC.General.SaveHirePurchasRequestForRevertRelease(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, BaseCls.GlbReqIsRequestGenerateUser, out referenceNo, _receiptAuto);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    string Msg = string.Empty;
                    if (eff > 0)
                        Msg = "Request Successfully Saved! Request No : " + referenceNo + ".";
                    else
                        Msg = "Request Fail!";
                    MessageBox.Show(Msg, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                    CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
                    btnClear_Click(null, null);
                }
            }
        }

        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo, string.Empty);
            CalculateRequest();
            CalculateBalanceSheet();
            set_UcReceipt_values(false);
            set_PaidAmount();
        }

        private bool _oneSide = false;

        private void ddlRequestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_oneSide == false)
            {
                _oneSide = true;
                BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo, ddlRequestNo.Text.Trim());
                CalculateRequest();
                CalculateBalanceSheet();
                set_UcReceipt_values(false);
                set_PaidAmount();
                _oneSide = false;
            }
        }

        private void HpRevertRelease_Load(object sender, EventArgs e)
        {
            BackDatePermission();
            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSRVTRL, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
            BaseCls.GlbReqIsRequestGenerateUser = true; //Add by Chamal 31-07-2014 as per Dilanda
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HpRevertRelease));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.lblBackDateInfor = new System.Windows.Forms.ToolStripLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel_ecd = new System.Windows.Forms.Panel();
            this.btnNewCloseFlag = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chekApplyECD = new System.Windows.Forms.CheckBox();
            this.divECDbal = new System.Windows.Forms.Panel();
            this.lblECDInsuBal = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblECD_Balance = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.divCustomRequest = new System.Windows.Forms.Panel();
            this.btnSendEcdReq = new System.Windows.Forms.Button();
            this.txtReques = new System.Windows.Forms.TextBox();
            this.chkIsECDrate = new System.Windows.Forms.CheckBox();
            this.ddlPendinReqNo = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.Panel_voucher = new System.Windows.Forms.Panel();
            this.txtVoucherNum = new System.Windows.Forms.TextBox();
            this.txtVoucherAmt = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.ddlECDType = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.ucPayModes1 = new FF.WindowsERPClient.UserControls.ucPayModes();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.ucReciept1 = new FF.WindowsERPClient.UserControls.ucReciept();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblSumAccBalance = new System.Windows.Forms.Label();
            this.lblSumPay = new System.Windows.Forms.Label();
            this.lblSumReceipt = new System.Windows.Forms.Label();
            this.lblTotalReceivable = new System.Windows.Forms.Label();
            this.lblSumReleaseAmt = new System.Windows.Forms.Label();
            this.lblSumDiscountAmt = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.ucHpAccountSummary1 = new FF.WindowsERPClient.UserControls.UcHpAccountSummary();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.gvRevertedSerial = new System.Windows.Forms.DataGridView();
            this.ser_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_Serial1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_Serial2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_Warranty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_SerialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ser_hdnIsPicked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.gvRevetedItem = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.gvATradeItem = new System.Windows.Forms.DataGridView();
            this.tra_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_UPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_Lineno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_IsForwardSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tra_InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblReqDiscountAmount = new System.Windows.Forms.Label();
            this.lblReqReleaseAmount = new System.Windows.Forms.Label();
            this.btnRequest = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRDiscount = new System.Windows.Forms.TextBox();
            this.txtRPartRelease = new System.Windows.Forms.TextBox();
            this.radDiscount = new System.Windows.Forms.RadioButton();
            this.radPartRelease = new System.Windows.Forms.RadioButton();
            this.ddlRequestNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkApproved = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCusCd = new System.Windows.Forms.TextBox();
            this.lblAccountNo = new System.Windows.Forms.TextBox();
            this.btnSearch_ProfitCenter = new System.Windows.Forms.Button();
            this.txtProfitCenter = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSearch_Account = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMan = new System.Windows.Forms.TextBox();
            this.btn_srch_man = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnECD = new System.Windows.Forms.Button();
            this.lbl_isECDapplied = new System.Windows.Forms.Label();
            this.lblHpInsBal = new System.Windows.Forms.Label();
            this.Itm_pick = new System.Windows.Forms.DataGridViewImageColumn();
            this.Itm_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itm_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itm_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itm_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itm_AvlQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itm_BaseDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel_ecd.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.divECDbal.SuspendLayout();
            this.divCustomRequest.SuspendLayout();
            this.Panel_voucher.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRevertedSerial)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRevetedItem)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvATradeItem)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator2,
            this.btnPrint,
            this.toolStripSeparator3,
            this.btnSave,
            this.lblBackDateInfor});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(1000, 25);
            this.toolStrip1.TabIndex = 11;
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = false;
            this.btnClear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(2);
            this.btnClear.Size = new System.Drawing.Size(60, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnPrint
            // 
            this.btnPrint.AutoSize = false;
            this.btnPrint.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(2);
            this.btnPrint.Size = new System.Drawing.Size(60, 22);
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(2);
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.Text = "Process";
            this.btnSave.Click += new System.EventHandler(this.Process);
            // 
            // lblBackDateInfor
            // 
            this.lblBackDateInfor.Name = "lblBackDateInfor";
            this.lblBackDateInfor.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel_ecd);
            this.pnlMain.Controls.Add(this.panel10);
            this.pnlMain.Controls.Add(this.panel9);
            this.pnlMain.Controls.Add(this.panel8);
            this.pnlMain.Controls.Add(this.panel7);
            this.pnlMain.Controls.Add(this.panel6);
            this.pnlMain.Controls.Add(this.panel5);
            this.pnlMain.Controls.Add(this.panel4);
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1000, 601);
            this.pnlMain.TabIndex = 19;
            // 
            // panel_ecd
            // 
            this.panel_ecd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_ecd.Controls.Add(this.btnNewCloseFlag);
            this.panel_ecd.Controls.Add(this.label9);
            this.panel_ecd.Controls.Add(this.groupBox3);
            this.panel_ecd.Location = new System.Drawing.Point(167, 196);
            this.panel_ecd.Name = "panel_ecd";
            this.panel_ecd.Size = new System.Drawing.Size(281, 209);
            this.panel_ecd.TabIndex = 37;
            this.panel_ecd.Visible = false;
            // 
            // btnNewCloseFlag
            // 
            this.btnNewCloseFlag.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.panelcloseicon;
            this.btnNewCloseFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewCloseFlag.Location = new System.Drawing.Point(250, 0);
            this.btnNewCloseFlag.Name = "btnNewCloseFlag";
            this.btnNewCloseFlag.Size = new System.Drawing.Size(24, 23);
            this.btnNewCloseFlag.TabIndex = 208;
            this.btnNewCloseFlag.UseVisualStyleBackColor = true;
            this.btnNewCloseFlag.Click += new System.EventHandler(this.btnNewCloseFlag_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.MidnightBlue;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(279, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "ECD Infomation";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chekApplyECD);
            this.groupBox3.Controls.Add(this.divECDbal);
            this.groupBox3.Controls.Add(this.divCustomRequest);
            this.groupBox3.Controls.Add(this.Panel_voucher);
            this.groupBox3.Controls.Add(this.ddlECDType);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(30, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 147);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // chekApplyECD
            // 
            this.chekApplyECD.AutoSize = true;
            this.chekApplyECD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chekApplyECD.ForeColor = System.Drawing.Color.Blue;
            this.chekApplyECD.Location = new System.Drawing.Point(9, 101);
            this.chekApplyECD.Name = "chekApplyECD";
            this.chekApplyECD.Size = new System.Drawing.Size(82, 17);
            this.chekApplyECD.TabIndex = 16;
            this.chekApplyECD.Text = "Apply ECD";
            this.chekApplyECD.UseVisualStyleBackColor = true;
            this.chekApplyECD.CheckedChanged += new System.EventHandler(this.chekApplyECD_CheckedChanged);
            // 
            // divECDbal
            // 
            this.divECDbal.Controls.Add(this.lblECDInsuBal);
            this.divECDbal.Controls.Add(this.label26);
            this.divECDbal.Controls.Add(this.lblECD_Balance);
            this.divECDbal.Controls.Add(this.label27);
            this.divECDbal.Location = new System.Drawing.Point(9, 51);
            this.divECDbal.Name = "divECDbal";
            this.divECDbal.Size = new System.Drawing.Size(160, 46);
            this.divECDbal.TabIndex = 15;
            // 
            // lblECDInsuBal
            // 
            this.lblECDInsuBal.AutoSize = true;
            this.lblECDInsuBal.Location = new System.Drawing.Point(106, 26);
            this.lblECDInsuBal.Name = "lblECDInsuBal";
            this.lblECDInsuBal.Size = new System.Drawing.Size(23, 12);
            this.lblECDInsuBal.TabIndex = 3;
            this.lblECDInsuBal.Text = "0.00";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 12);
            this.label26.TabIndex = 2;
            this.label26.Text = "Insu. Balance :";
            // 
            // lblECD_Balance
            // 
            this.lblECD_Balance.AutoSize = true;
            this.lblECD_Balance.Location = new System.Drawing.Point(107, 8);
            this.lblECD_Balance.Name = "lblECD_Balance";
            this.lblECD_Balance.Size = new System.Drawing.Size(23, 12);
            this.lblECD_Balance.TabIndex = 1;
            this.lblECD_Balance.Text = "0.00";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 8);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(69, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "ECD Balance :";
            // 
            // divCustomRequest
            // 
            this.divCustomRequest.Controls.Add(this.btnSendEcdReq);
            this.divCustomRequest.Controls.Add(this.txtReques);
            this.divCustomRequest.Controls.Add(this.chkIsECDrate);
            this.divCustomRequest.Controls.Add(this.ddlPendinReqNo);
            this.divCustomRequest.Controls.Add(this.label28);
            this.divCustomRequest.Controls.Add(this.label29);
            this.divCustomRequest.Location = new System.Drawing.Point(200, 52);
            this.divCustomRequest.Name = "divCustomRequest";
            this.divCustomRequest.Size = new System.Drawing.Size(416, 57);
            this.divCustomRequest.TabIndex = 14;
            this.divCustomRequest.Visible = false;
            // 
            // btnSendEcdReq
            // 
            this.btnSendEcdReq.Location = new System.Drawing.Point(220, 9);
            this.btnSendEcdReq.Name = "btnSendEcdReq";
            this.btnSendEcdReq.Size = new System.Drawing.Size(118, 23);
            this.btnSendEcdReq.TabIndex = 14;
            this.btnSendEcdReq.Text = "Submit Request";
            this.btnSendEcdReq.UseVisualStyleBackColor = false;
            // 
            // txtReques
            // 
            this.txtReques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReques.Location = new System.Drawing.Point(62, 11);
            this.txtReques.Name = "txtReques";
            this.txtReques.Size = new System.Drawing.Size(94, 19);
            this.txtReques.TabIndex = 9;
            // 
            // chkIsECDrate
            // 
            this.chkIsECDrate.AutoSize = true;
            this.chkIsECDrate.Location = new System.Drawing.Point(165, 14);
            this.chkIsECDrate.Name = "chkIsECDrate";
            this.chkIsECDrate.Size = new System.Drawing.Size(54, 16);
            this.chkIsECDrate.TabIndex = 10;
            this.chkIsECDrate.Text = "Is Rate";
            this.chkIsECDrate.UseVisualStyleBackColor = true;
            // 
            // ddlPendinReqNo
            // 
            this.ddlPendinReqNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPendinReqNo.FormattingEnabled = true;
            this.ddlPendinReqNo.Location = new System.Drawing.Point(221, 35);
            this.ddlPendinReqNo.Name = "ddlPendinReqNo";
            this.ddlPendinReqNo.Size = new System.Drawing.Size(192, 19);
            this.ddlPendinReqNo.TabIndex = 13;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(145, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(68, 12);
            this.label28.TabIndex = 12;
            this.label28.Text = "Get Pendings:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 14);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(49, 12);
            this.label29.TabIndex = 8;
            this.label29.Text = "ECD Amt.";
            // 
            // Panel_voucher
            // 
            this.Panel_voucher.Controls.Add(this.txtVoucherNum);
            this.Panel_voucher.Controls.Add(this.txtVoucherAmt);
            this.Panel_voucher.Controls.Add(this.label30);
            this.Panel_voucher.Controls.Add(this.label31);
            this.Panel_voucher.Location = new System.Drawing.Point(201, 12);
            this.Panel_voucher.Name = "Panel_voucher";
            this.Panel_voucher.Size = new System.Drawing.Size(346, 38);
            this.Panel_voucher.TabIndex = 7;
            this.Panel_voucher.Visible = false;
            // 
            // txtVoucherNum
            // 
            this.txtVoucherNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherNum.Location = new System.Drawing.Point(57, 11);
            this.txtVoucherNum.Name = "txtVoucherNum";
            this.txtVoucherNum.Size = new System.Drawing.Size(99, 19);
            this.txtVoucherNum.TabIndex = 3;
            this.txtVoucherNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVoucherNum_KeyPress);
            // 
            // txtVoucherAmt
            // 
            this.txtVoucherAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherAmt.Enabled = false;
            this.txtVoucherAmt.Location = new System.Drawing.Point(219, 11);
            this.txtVoucherAmt.Name = "txtVoucherAmt";
            this.txtVoucherAmt.Size = new System.Drawing.Size(118, 19);
            this.txtVoucherAmt.TabIndex = 6;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(5, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(50, 12);
            this.label30.TabIndex = 2;
            this.label30.Text = "Voucher#";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(178, 15);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(41, 12);
            this.label31.TabIndex = 5;
            this.label31.Text = "Amount";
            // 
            // ddlECDType
            // 
            this.ddlECDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlECDType.FormattingEnabled = true;
            this.ddlECDType.Items.AddRange(new object[] {
            "",
            "Normal",
            "Special",
            ""});
            this.ddlECDType.Location = new System.Drawing.Point(12, 29);
            this.ddlECDType.Name = "ddlECDType";
            this.ddlECDType.Size = new System.Drawing.Size(155, 19);
            this.ddlECDType.TabIndex = 4;
            this.ddlECDType.SelectedIndexChanged += new System.EventHandler(this.ddlECDType_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(12, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(58, 12);
            this.label32.TabIndex = 0;
            this.label32.Text = "ECD Type :";
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.label23);
            this.panel10.Controls.Add(this.ucPayModes1);
            this.panel10.Location = new System.Drawing.Point(3, 403);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(994, 197);
            this.panel10.TabIndex = 36;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.MidnightBlue;
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.ForeColor = System.Drawing.Color.Azure;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(992, 15);
            this.label23.TabIndex = 35;
            this.label23.Text = "Payment Detail";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPayModes1
            // 
            this.ucPayModes1.Allow_Plus_balance = false;
            this.ucPayModes1.BackColor = System.Drawing.Color.White;
            this.ucPayModes1.CreditCardTransLog = null;
            this.ucPayModes1.CurrancyAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucPayModes1.CurrancyCode = "";
            this.ucPayModes1.Customer_Code = null;
            this.ucPayModes1.Date = new System.DateTime(((long)(0)));
            this.ucPayModes1.ExchangeRate = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucPayModes1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucPayModes1.HavePayModes = true;
            this.ucPayModes1.InvoiceItemList = null;
            this.ucPayModes1.InvoiceNo = null;
            this.ucPayModes1.IsDutyFree = false;
            this.ucPayModes1.ItemList = null;
            this.ucPayModes1.Location = new System.Drawing.Point(0, 15);
            this.ucPayModes1.LoyaltyCard = null;
            this.ucPayModes1.MaximumSize = new System.Drawing.Size(997, 189);
            this.ucPayModes1.Name = "ucPayModes1";
            this.ucPayModes1.PcAllowBanks = null;
            this.ucPayModes1.SerialList = null;
            this.ucPayModes1.Size = new System.Drawing.Size(994, 181);
            this.ucPayModes1.SystemModule = null;
            this.ucPayModes1.TabIndex = 34;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.label22);
            this.panel9.Controls.Add(this.ucReciept1);
            this.panel9.Location = new System.Drawing.Point(416, 260);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(581, 141);
            this.panel9.TabIndex = 35;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.MidnightBlue;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.ForeColor = System.Drawing.Color.Azure;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(579, 15);
            this.label22.TabIndex = 6;
            this.label22.Text = "Receipt Entry";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucReciept1
            // 
            this.ucReciept1.AmountToPay = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucReciept1.BackColor = System.Drawing.Color.GhostWhite;
            this.ucReciept1.FormHeight = 127;
            this.ucReciept1.FormName = null;
            this.ucReciept1.ISCancel = false;
            this.ucReciept1.IsEcd = false;
            this.ucReciept1.ISSys = false;
            this.ucReciept1.Location = new System.Drawing.Point(3, 13);
            this.ucReciept1.MainGridHeight = 75;
            this.ucReciept1.MaximumSize = new System.Drawing.Size(578, 128);
            this.ucReciept1.Name = "ucReciept1";
            this.ucReciept1.NeedCalculation = false;
            this.ucReciept1.Size = new System.Drawing.Size(576, 127);
            this.ucReciept1.TabIndex = 33;
            this.ucReciept1.ItemAdded += new System.EventHandler(this.ucReciept1_ItemAdded_1);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.lblSumAccBalance);
            this.panel8.Controls.Add(this.lblSumPay);
            this.panel8.Controls.Add(this.lblSumReceipt);
            this.panel8.Controls.Add(this.lblTotalReceivable);
            this.panel8.Controls.Add(this.lblSumReleaseAmt);
            this.panel8.Controls.Add(this.lblSumDiscountAmt);
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label18);
            this.panel8.Controls.Add(this.label17);
            this.panel8.Controls.Add(this.label16);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Location = new System.Drawing.Point(851, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(146, 257);
            this.panel8.TabIndex = 32;
            // 
            // lblSumAccBalance
            // 
            this.lblSumAccBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblSumAccBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumAccBalance.ForeColor = System.Drawing.Color.Red;
            this.lblSumAccBalance.Location = new System.Drawing.Point(31, 44);
            this.lblSumAccBalance.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblSumAccBalance.Name = "lblSumAccBalance";
            this.lblSumAccBalance.Size = new System.Drawing.Size(112, 13);
            this.lblSumAccBalance.TabIndex = 43;
            this.lblSumAccBalance.Text = "0,000,000.00";
            this.lblSumAccBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSumPay
            // 
            this.lblSumPay.BackColor = System.Drawing.Color.Transparent;
            this.lblSumPay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumPay.ForeColor = System.Drawing.Color.DarkRed;
            this.lblSumPay.Location = new System.Drawing.Point(31, 239);
            this.lblSumPay.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblSumPay.Name = "lblSumPay";
            this.lblSumPay.Size = new System.Drawing.Size(112, 13);
            this.lblSumPay.TabIndex = 42;
            this.lblSumPay.Text = "0,000,000.00";
            this.lblSumPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSumReceipt
            // 
            this.lblSumReceipt.BackColor = System.Drawing.Color.Transparent;
            this.lblSumReceipt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumReceipt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblSumReceipt.Location = new System.Drawing.Point(31, 209);
            this.lblSumReceipt.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblSumReceipt.Name = "lblSumReceipt";
            this.lblSumReceipt.Size = new System.Drawing.Size(112, 13);
            this.lblSumReceipt.TabIndex = 41;
            this.lblSumReceipt.Text = "0,000,000.00";
            this.lblSumReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalReceivable
            // 
            this.lblTotalReceivable.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalReceivable.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReceivable.ForeColor = System.Drawing.Color.Red;
            this.lblTotalReceivable.Location = new System.Drawing.Point(31, 165);
            this.lblTotalReceivable.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblTotalReceivable.Name = "lblTotalReceivable";
            this.lblTotalReceivable.Size = new System.Drawing.Size(112, 13);
            this.lblTotalReceivable.TabIndex = 40;
            this.lblTotalReceivable.Text = "0,000,000.00";
            this.lblTotalReceivable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSumReleaseAmt
            // 
            this.lblSumReleaseAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblSumReleaseAmt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSumReleaseAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumReleaseAmt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblSumReleaseAmt.Location = new System.Drawing.Point(31, 88);
            this.lblSumReleaseAmt.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblSumReleaseAmt.Name = "lblSumReleaseAmt";
            this.lblSumReleaseAmt.Size = new System.Drawing.Size(112, 13);
            this.lblSumReleaseAmt.TabIndex = 39;
            this.lblSumReleaseAmt.Text = "0,000,000.00";
            this.lblSumReleaseAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSumDiscountAmt
            // 
            this.lblSumDiscountAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblSumDiscountAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumDiscountAmt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblSumDiscountAmt.Location = new System.Drawing.Point(31, 122);
            this.lblSumDiscountAmt.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblSumDiscountAmt.Name = "lblSumDiscountAmt";
            this.lblSumDiscountAmt.Size = new System.Drawing.Size(112, 13);
            this.lblSumDiscountAmt.TabIndex = 38;
            this.lblSumDiscountAmt.Text = "0,000,000.00";
            this.lblSumDiscountAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Black;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(4, 141);
            this.label25.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(139, 2);
            this.label25.TabIndex = 27;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Black;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 18);
            this.label24.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(139, 2);
            this.label24.TabIndex = 26;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(5, 222);
            this.label20.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "To be Pay";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(5, 191);
            this.label19.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "To be Receipt";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(1, 147);
            this.label18.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 14);
            this.label18.TabIndex = 23;
            this.label18.Text = "Receivable";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(8, 105);
            this.label17.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Discounted Amount";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(8, 71);
            this.label16.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Release Amount";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1, 26);
            this.label15.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 14);
            this.label15.TabIndex = 20;
            this.label15.Text = "Account Balance";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.MidnightBlue;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.ForeColor = System.Drawing.Color.Azure;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(144, 15);
            this.label14.TabIndex = 4;
            this.label14.Text = "Summary";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label21);
            this.panel7.Controls.Add(this.ucHpAccountSummary1);
            this.panel7.Location = new System.Drawing.Point(416, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(429, 256);
            this.panel7.TabIndex = 31;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.MidnightBlue;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.ForeColor = System.Drawing.Color.Azure;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(429, 15);
            this.label21.TabIndex = 5;
            this.label21.Text = "Account Summary";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucHpAccountSummary1
            // 
            this.ucHpAccountSummary1.BackColor = System.Drawing.Color.White;
            this.ucHpAccountSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucHpAccountSummary1.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucHpAccountSummary1.ForeColor = System.Drawing.Color.Black;
            this.ucHpAccountSummary1.Location = new System.Drawing.Point(3, 10);
            this.ucHpAccountSummary1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucHpAccountSummary1.Name = "ucHpAccountSummary1";
            this.ucHpAccountSummary1.Size = new System.Drawing.Size(424, 244);
            this.ucHpAccountSummary1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.gvRevertedSerial);
            this.panel6.Location = new System.Drawing.Point(3, 296);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(404, 106);
            this.panel6.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.MidnightBlue;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.ForeColor = System.Drawing.Color.Azure;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(402, 15);
            this.label13.TabIndex = 3;
            this.label13.Text = "Reverted Serial Detail ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvRevertedSerial
            // 
            this.gvRevertedSerial.AllowUserToAddRows = false;
            this.gvRevertedSerial.AllowUserToDeleteRows = false;
            this.gvRevertedSerial.AllowUserToResizeRows = false;
            this.gvRevertedSerial.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvRevertedSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(114)))), ((int)(((byte)(205)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRevertedSerial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvRevertedSerial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvRevertedSerial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ser_Line,
            this.ser_Item,
            this.ser_Status,
            this.ser_Qty,
            this.ser_Serial1,
            this.ser_Serial2,
            this.ser_Warranty,
            this.ser_SerialID,
            this.ser_hdnIsPicked});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvRevertedSerial.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvRevertedSerial.EnableHeadersVisualStyles = false;
            this.gvRevertedSerial.GridColor = System.Drawing.Color.White;
            this.gvRevertedSerial.Location = new System.Drawing.Point(-1, 14);
            this.gvRevertedSerial.Name = "gvRevertedSerial";
            this.gvRevertedSerial.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRevertedSerial.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvRevertedSerial.RowHeadersVisible = false;
            this.gvRevertedSerial.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvRevertedSerial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRevertedSerial.Size = new System.Drawing.Size(403, 96);
            this.gvRevertedSerial.TabIndex = 6;
            // 
            // ser_Line
            // 
            this.ser_Line.HeaderText = "#";
            this.ser_Line.Name = "ser_Line";
            this.ser_Line.ReadOnly = true;
            this.ser_Line.Width = 20;
            // 
            // ser_Item
            // 
            this.ser_Item.DataPropertyName = "tus_itm_cd";
            this.ser_Item.HeaderText = "Item";
            this.ser_Item.Name = "ser_Item";
            this.ser_Item.ReadOnly = true;
            this.ser_Item.Width = 54;
            // 
            // ser_Status
            // 
            this.ser_Status.DataPropertyName = "tus_itm_stus";
            this.ser_Status.HeaderText = "Status";
            this.ser_Status.Name = "ser_Status";
            this.ser_Status.ReadOnly = true;
            this.ser_Status.Width = 63;
            // 
            // ser_Qty
            // 
            this.ser_Qty.DataPropertyName = "tus_qty";
            this.ser_Qty.HeaderText = "Qty";
            this.ser_Qty.Name = "ser_Qty";
            this.ser_Qty.ReadOnly = true;
            this.ser_Qty.Width = 50;
            // 
            // ser_Serial1
            // 
            this.ser_Serial1.DataPropertyName = "tus_ser_1";
            this.ser_Serial1.HeaderText = "Serial 1";
            this.ser_Serial1.Name = "ser_Serial1";
            this.ser_Serial1.ReadOnly = true;
            this.ser_Serial1.Width = 67;
            // 
            // ser_Serial2
            // 
            this.ser_Serial2.DataPropertyName = "tus_ser_2";
            this.ser_Serial2.HeaderText = "Serial 2";
            this.ser_Serial2.Name = "ser_Serial2";
            this.ser_Serial2.ReadOnly = true;
            this.ser_Serial2.Width = 67;
            // 
            // ser_Warranty
            // 
            this.ser_Warranty.DataPropertyName = "tus_warr_no";
            this.ser_Warranty.HeaderText = "Warranty";
            this.ser_Warranty.Name = "ser_Warranty";
            this.ser_Warranty.ReadOnly = true;
            this.ser_Warranty.Width = 78;
            // 
            // ser_SerialID
            // 
            this.ser_SerialID.DataPropertyName = "tus_ser_id";
            this.ser_SerialID.HeaderText = "Serial ID";
            this.ser_SerialID.Name = "ser_SerialID";
            this.ser_SerialID.ReadOnly = true;
            this.ser_SerialID.Width = 72;
            // 
            // ser_hdnIsPicked
            // 
            this.ser_hdnIsPicked.DataPropertyName = "Tus_new_remarks";
            this.ser_hdnIsPicked.HeaderText = "hdnIsPicked";
            this.ser_hdnIsPicked.Name = "ser_hdnIsPicked";
            this.ser_hdnIsPicked.ReadOnly = true;
            this.ser_hdnIsPicked.Visible = false;
            this.ser_hdnIsPicked.Width = 89;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.gvRevetedItem);
            this.panel5.Location = new System.Drawing.Point(3, 205);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(404, 91);
            this.panel5.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.MidnightBlue;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.ForeColor = System.Drawing.Color.Azure;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(402, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "Reverted Item Detail ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvRevetedItem
            // 
            this.gvRevetedItem.AllowUserToAddRows = false;
            this.gvRevetedItem.AllowUserToDeleteRows = false;
            this.gvRevetedItem.AllowUserToResizeRows = false;
            this.gvRevetedItem.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvRevetedItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(114)))), ((int)(((byte)(205)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRevetedItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvRevetedItem.ColumnHeadersHeight = 21;
            this.gvRevetedItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvRevetedItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Itm_pick,
            this.Itm_No,
            this.Itm_Item,
            this.Itm_Status,
            this.Itm_Qty,
            this.Itm_AvlQty,
            this.Itm_BaseDoc});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvRevetedItem.DefaultCellStyle = dataGridViewCellStyle5;
            this.gvRevetedItem.EnableHeadersVisualStyles = false;
            this.gvRevetedItem.GridColor = System.Drawing.Color.White;
            this.gvRevetedItem.Location = new System.Drawing.Point(0, 14);
            this.gvRevetedItem.Name = "gvRevetedItem";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRevetedItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvRevetedItem.RowHeadersVisible = false;
            this.gvRevetedItem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvRevetedItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRevetedItem.Size = new System.Drawing.Size(402, 81);
            this.gvRevetedItem.TabIndex = 6;
            this.gvRevetedItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRevetedItem_CellClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.gvATradeItem);
            this.panel4.Location = new System.Drawing.Point(3, 124);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(404, 82);
            this.panel4.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.MidnightBlue;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.ForeColor = System.Drawing.Color.Azure;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(402, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Account Item Detail ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvATradeItem
            // 
            this.gvATradeItem.AllowUserToAddRows = false;
            this.gvATradeItem.AllowUserToDeleteRows = false;
            this.gvATradeItem.AllowUserToResizeRows = false;
            this.gvATradeItem.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvATradeItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(114)))), ((int)(((byte)(205)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvATradeItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvATradeItem.ColumnHeadersHeight = 21;
            this.gvATradeItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvATradeItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tra_Item,
            this.tra_Description,
            this.tra_Model,
            this.tra_Qty,
            this.tra_UPrice,
            this.tra_Lineno,
            this.tra_IsForwardSale,
            this.tra_InvoiceNo});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvATradeItem.DefaultCellStyle = dataGridViewCellStyle8;
            this.gvATradeItem.EnableHeadersVisualStyles = false;
            this.gvATradeItem.GridColor = System.Drawing.Color.White;
            this.gvATradeItem.Location = new System.Drawing.Point(-1, 14);
            this.gvATradeItem.Name = "gvATradeItem";
            this.gvATradeItem.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvATradeItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gvATradeItem.RowHeadersVisible = false;
            this.gvATradeItem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvATradeItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvATradeItem.Size = new System.Drawing.Size(402, 70);
            this.gvATradeItem.TabIndex = 5;
            // 
            // tra_Item
            // 
            this.tra_Item.DataPropertyName = "Sad_itm_cd";
            this.tra_Item.HeaderText = "Item";
            this.tra_Item.Name = "tra_Item";
            this.tra_Item.ReadOnly = true;
            this.tra_Item.Width = 99;
            // 
            // tra_Description
            // 
            this.tra_Description.DataPropertyName = "Mi_longdesc";
            this.tra_Description.HeaderText = "Description";
            this.tra_Description.Name = "tra_Description";
            this.tra_Description.ReadOnly = true;
            this.tra_Description.Width = 99;
            // 
            // tra_Model
            // 
            this.tra_Model.DataPropertyName = "Mi_model";
            this.tra_Model.HeaderText = "Model";
            this.tra_Model.Name = "tra_Model";
            this.tra_Model.ReadOnly = true;
            this.tra_Model.Width = 99;
            // 
            // tra_Qty
            // 
            this.tra_Qty.DataPropertyName = "Sad_qty";
            this.tra_Qty.HeaderText = "Qty";
            this.tra_Qty.Name = "tra_Qty";
            this.tra_Qty.ReadOnly = true;
            this.tra_Qty.Width = 99;
            // 
            // tra_UPrice
            // 
            this.tra_UPrice.DataPropertyName = "Sad_unit_rt";
            this.tra_UPrice.HeaderText = "Unit Price";
            this.tra_UPrice.Name = "tra_UPrice";
            this.tra_UPrice.ReadOnly = true;
            this.tra_UPrice.Visible = false;
            this.tra_UPrice.Width = 77;
            // 
            // tra_Lineno
            // 
            this.tra_Lineno.DataPropertyName = "Sad_itm_line";
            this.tra_Lineno.HeaderText = "Line no";
            this.tra_Lineno.Name = "tra_Lineno";
            this.tra_Lineno.ReadOnly = true;
            this.tra_Lineno.Visible = false;
            this.tra_Lineno.Width = 66;
            // 
            // tra_IsForwardSale
            // 
            this.tra_IsForwardSale.DataPropertyName = "Mi_act";
            this.tra_IsForwardSale.HeaderText = "IsForwardSale";
            this.tra_IsForwardSale.Name = "tra_IsForwardSale";
            this.tra_IsForwardSale.ReadOnly = true;
            this.tra_IsForwardSale.Visible = false;
            this.tra_IsForwardSale.Width = 101;
            // 
            // tra_InvoiceNo
            // 
            this.tra_InvoiceNo.DataPropertyName = "sad_inv_no";
            this.tra_InvoiceNo.HeaderText = "InvoiceNo";
            this.tra_InvoiceNo.Name = "tra_InvoiceNo";
            this.tra_InvoiceNo.ReadOnly = true;
            this.tra_InvoiceNo.Visible = false;
            this.tra_InvoiceNo.Width = 80;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblReqDiscountAmount);
            this.panel3.Controls.Add(this.lblReqReleaseAmount);
            this.panel3.Controls.Add(this.btnRequest);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtRDiscount);
            this.panel3.Controls.Add(this.txtRPartRelease);
            this.panel3.Controls.Add(this.radDiscount);
            this.panel3.Controls.Add(this.radPartRelease);
            this.panel3.Controls.Add(this.ddlRequestNo);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.chkApproved);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(3, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(404, 64);
            this.panel3.TabIndex = 27;
            // 
            // lblReqDiscountAmount
            // 
            this.lblReqDiscountAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReqDiscountAmount.ForeColor = System.Drawing.Color.Red;
            this.lblReqDiscountAmount.Location = new System.Drawing.Point(242, 46);
            this.lblReqDiscountAmount.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblReqDiscountAmount.Name = "lblReqDiscountAmount";
            this.lblReqDiscountAmount.Size = new System.Drawing.Size(99, 13);
            this.lblReqDiscountAmount.TabIndex = 38;
            this.lblReqDiscountAmount.Text = "0,000,000.00";
            this.lblReqDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReqReleaseAmount
            // 
            this.lblReqReleaseAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReqReleaseAmount.ForeColor = System.Drawing.Color.Red;
            this.lblReqReleaseAmount.Location = new System.Drawing.Point(242, 24);
            this.lblReqReleaseAmount.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblReqReleaseAmount.Name = "lblReqReleaseAmount";
            this.lblReqReleaseAmount.Size = new System.Drawing.Size(99, 13);
            this.lblReqReleaseAmount.TabIndex = 37;
            this.lblReqReleaseAmount.Text = "0,000,000.00";
            this.lblReqReleaseAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRequest
            // 
            this.btnRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRequest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRequest.ForeColor = System.Drawing.Color.Black;
            this.btnRequest.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRequest.Location = new System.Drawing.Point(346, 20);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(55, 23);
            this.btnRequest.TabIndex = 36;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnSendEcdReq_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "%";
            // 
            // txtRDiscount
            // 
            this.txtRDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRDiscount.Location = new System.Drawing.Point(169, 40);
            this.txtRDiscount.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.txtRDiscount.MaxLength = 30;
            this.txtRDiscount.Name = "txtRDiscount";
            this.txtRDiscount.Size = new System.Drawing.Size(54, 21);
            this.txtRDiscount.TabIndex = 29;
            // 
            // txtRPartRelease
            // 
            this.txtRPartRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRPartRelease.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRPartRelease.Location = new System.Drawing.Point(169, 18);
            this.txtRPartRelease.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.txtRPartRelease.MaxLength = 30;
            this.txtRPartRelease.Name = "txtRPartRelease";
            this.txtRPartRelease.Size = new System.Drawing.Size(54, 21);
            this.txtRPartRelease.TabIndex = 28;
            // 
            // radDiscount
            // 
            this.radDiscount.AutoSize = true;
            this.radDiscount.Checked = true;
            this.radDiscount.Location = new System.Drawing.Point(78, 42);
            this.radDiscount.Name = "radDiscount";
            this.radDiscount.Size = new System.Drawing.Size(69, 17);
            this.radDiscount.TabIndex = 27;
            this.radDiscount.TabStop = true;
            this.radDiscount.Text = "Discount ";
            this.radDiscount.UseVisualStyleBackColor = true;
            this.radDiscount.CheckedChanged += new System.EventHandler(this.RequestOptionChage);
            // 
            // radPartRelease
            // 
            this.radPartRelease.AutoSize = true;
            this.radPartRelease.Location = new System.Drawing.Point(78, 20);
            this.radPartRelease.Name = "radPartRelease";
            this.radPartRelease.Size = new System.Drawing.Size(95, 17);
            this.radPartRelease.TabIndex = 26;
            this.radPartRelease.Text = "Patial Release ";
            this.radPartRelease.UseVisualStyleBackColor = true;
            this.radPartRelease.CheckedChanged += new System.EventHandler(this.RequestOptionChage);
            // 
            // ddlRequestNo
            // 
            this.ddlRequestNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRequestNo.DropDownWidth = 150;
            this.ddlRequestNo.FormattingEnabled = true;
            this.ddlRequestNo.Location = new System.Drawing.Point(2, 32);
            this.ddlRequestNo.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.ddlRequestNo.Name = "ddlRequestNo";
            this.ddlRequestNo.Size = new System.Drawing.Size(73, 21);
            this.ddlRequestNo.TabIndex = 25;
            this.ddlRequestNo.SelectedIndexChanged += new System.EventHandler(this.ddlRequestNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Request";
            // 
            // chkApproved
            // 
            this.chkApproved.AutoSize = true;
            this.chkApproved.BackColor = System.Drawing.Color.MidnightBlue;
            this.chkApproved.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkApproved.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkApproved.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.chkApproved.Location = new System.Drawing.Point(284, -2);
            this.chkApproved.Name = "chkApproved";
            this.chkApproved.Size = new System.Drawing.Size(113, 17);
            this.chkApproved.TabIndex = 4;
            this.chkApproved.Text = "Approved Request";
            this.chkApproved.UseVisualStyleBackColor = false;
            this.chkApproved.CheckedChanged += new System.EventHandler(this.chkApproved_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MidnightBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.Azure;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(402, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Request Detail";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblCusCd);
            this.panel2.Controls.Add(this.lblAccountNo);
            this.panel2.Controls.Add(this.btnSearch_ProfitCenter);
            this.panel2.Controls.Add(this.txtProfitCenter);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.btnSearch_Account);
            this.panel2.Controls.Add(this.txtDate);
            this.panel2.Controls.Add(this.txtAccountNo);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(404, 56);
            this.panel2.TabIndex = 26;
            // 
            // lblCusCd
            // 
            this.lblCusCd.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblCusCd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCusCd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusCd.ForeColor = System.Drawing.Color.Yellow;
            this.lblCusCd.Location = new System.Drawing.Point(123, 0);
            this.lblCusCd.Name = "lblCusCd";
            this.lblCusCd.ReadOnly = true;
            this.lblCusCd.Size = new System.Drawing.Size(128, 13);
            this.lblCusCd.TabIndex = 30;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.ForeColor = System.Drawing.Color.Yellow;
            this.lblAccountNo.Location = new System.Drawing.Point(275, 1);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.ReadOnly = true;
            this.lblAccountNo.Size = new System.Drawing.Size(128, 13);
            this.lblAccountNo.TabIndex = 28;
            // 
            // btnSearch_ProfitCenter
            // 
            this.btnSearch_ProfitCenter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch_ProfitCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_ProfitCenter.BackgroundImage")));
            this.btnSearch_ProfitCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ProfitCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_ProfitCenter.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch_ProfitCenter.Location = new System.Drawing.Point(221, 32);
            this.btnSearch_ProfitCenter.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_ProfitCenter.Name = "btnSearch_ProfitCenter";
            this.btnSearch_ProfitCenter.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_ProfitCenter.TabIndex = 27;
            this.btnSearch_ProfitCenter.UseVisualStyleBackColor = false;
            this.btnSearch_ProfitCenter.Click += new System.EventHandler(this.btnSearch_ProfitCenter_Click);
            // 
            // txtProfitCenter
            // 
            this.txtProfitCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProfitCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProfitCenter.Location = new System.Drawing.Point(135, 32);
            this.txtProfitCenter.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.txtProfitCenter.MaxLength = 30;
            this.txtProfitCenter.Name = "txtProfitCenter";
            this.txtProfitCenter.Size = new System.Drawing.Size(85, 21);
            this.txtProfitCenter.TabIndex = 26;
            this.txtProfitCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProfitCenter_KeyDown);
            this.txtProfitCenter.Leave += new System.EventHandler(this.txtProfitCenter_Leave);
            this.txtProfitCenter.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtProfitCenter_MouseDoubleClick);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.MidnightBlue;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.ForeColor = System.Drawing.Color.Azure;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(402, 15);
            this.label10.TabIndex = 2;
            this.label10.Text = "Searching Detail";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch_Account
            // 
            this.btnSearch_Account.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_Account.BackgroundImage")));
            this.btnSearch_Account.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_Account.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_Account.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch_Account.Location = new System.Drawing.Point(379, 32);
            this.btnSearch_Account.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_Account.Name = "btnSearch_Account";
            this.btnSearch_Account.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_Account.TabIndex = 25;
            this.btnSearch_Account.UseVisualStyleBackColor = true;
            this.btnSearch_Account.Click += new System.EventHandler(this.btnSearch_Account_Click);
            // 
            // txtDate
            // 
            this.txtDate.Checked = false;
            this.txtDate.CustomFormat = "dd/MMM/yyyy";
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.Location = new System.Drawing.Point(3, 32);
            this.txtDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(104, 21);
            this.txtDate.TabIndex = 20;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAccountNo.Location = new System.Drawing.Point(271, 32);
            this.txtAccountNo.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.txtAccountNo.MaxLength = 30;
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(107, 21);
            this.txtAccountNo.TabIndex = 24;
            this.txtAccountNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountNo_KeyDown);
            this.txtAccountNo.Leave += new System.EventHandler(this.txtAccountNo_Leave);
            this.txtAccountNo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtAccountNo_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(271, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Account Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Sale Point";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(304, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(6, 10);
            this.button1.TabIndex = 29;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMan
            // 
            this.txtMan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMan.Location = new System.Drawing.Point(328, 3);
            this.txtMan.Name = "txtMan";
            this.txtMan.Size = new System.Drawing.Size(78, 21);
            this.txtMan.TabIndex = 37;
            this.txtMan.DoubleClick += new System.EventHandler(this.txtMan_DoubleClick);
            this.txtMan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMan_KeyDown);
            this.txtMan.Leave += new System.EventHandler(this.txtMan_Leave);
            // 
            // btn_srch_man
            // 
            this.btn_srch_man.BackColor = System.Drawing.Color.White;
            this.btn_srch_man.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.btn_srch_man.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_srch_man.Location = new System.Drawing.Point(412, 2);
            this.btn_srch_man.Name = "btn_srch_man";
            this.btn_srch_man.Size = new System.Drawing.Size(25, 23);
            this.btn_srch_man.TabIndex = 39;
            this.btn_srch_man.UseVisualStyleBackColor = false;
            this.btn_srch_man.Click += new System.EventHandler(this.btn_srch_man_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(233, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Collect Manager:";
            // 
            // btnECD
            // 
            this.btnECD.Location = new System.Drawing.Point(456, 2);
            this.btnECD.Name = "btnECD";
            this.btnECD.Size = new System.Drawing.Size(51, 23);
            this.btnECD.TabIndex = 40;
            this.btnECD.Text = "ECD ...";
            this.btnECD.UseVisualStyleBackColor = true;
            this.btnECD.Click += new System.EventHandler(this.btnECD_Click);
            // 
            // lbl_isECDapplied
            // 
            this.lbl_isECDapplied.AutoSize = true;
            this.lbl_isECDapplied.ForeColor = System.Drawing.Color.Green;
            this.lbl_isECDapplied.Location = new System.Drawing.Point(513, 7);
            this.lbl_isECDapplied.Name = "lbl_isECDapplied";
            this.lbl_isECDapplied.Size = new System.Drawing.Size(74, 13);
            this.lbl_isECDapplied.TabIndex = 41;
            this.lbl_isECDapplied.Text = "ECD Balance: ";
            this.lbl_isECDapplied.Visible = false;
            // 
            // lblHpInsBal
            // 
            this.lblHpInsBal.AutoSize = true;
            this.lblHpInsBal.ForeColor = System.Drawing.Color.Green;
            this.lblHpInsBal.Location = new System.Drawing.Point(662, 7);
            this.lblHpInsBal.Name = "lblHpInsBal";
            this.lblHpInsBal.Size = new System.Drawing.Size(74, 13);
            this.lblHpInsBal.TabIndex = 42;
            this.lblHpInsBal.Text = "ECD Balance: ";
            this.lblHpInsBal.Visible = false;
            // 
            // Itm_pick
            // 
            this.Itm_pick.HeaderText = "";
            this.Itm_pick.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.Itm_pick.Name = "Itm_pick";
            this.Itm_pick.Width = 20;
            // 
            // Itm_No
            // 
            this.Itm_No.DataPropertyName = "tus_ser_line";
            this.Itm_No.HeaderText = "#";
            this.Itm_No.MinimumWidth = 2;
            this.Itm_No.Name = "Itm_No";
            this.Itm_No.Width = 20;
            // 
            // Itm_Item
            // 
            this.Itm_Item.DataPropertyName = "tus_itm_cd";
            this.Itm_Item.HeaderText = "Item";
            this.Itm_Item.Name = "Itm_Item";
            // 
            // Itm_Status
            // 
            this.Itm_Status.DataPropertyName = "tus_itm_stus";
            this.Itm_Status.HeaderText = "Status";
            this.Itm_Status.Name = "Itm_Status";
            this.Itm_Status.Width = 63;
            // 
            // Itm_Qty
            // 
            this.Itm_Qty.DataPropertyName = "tus_qty";
            this.Itm_Qty.HeaderText = "Qty";
            this.Itm_Qty.Name = "Itm_Qty";
            this.Itm_Qty.Width = 50;
            // 
            // Itm_AvlQty
            // 
            this.Itm_AvlQty.DataPropertyName = "Tmp_used_qty";
            this.Itm_AvlQty.HeaderText = "Available Qty";
            this.Itm_AvlQty.Name = "Itm_AvlQty";
            this.Itm_AvlQty.Width = 96;
            // 
            // Itm_BaseDoc
            // 
            this.Itm_BaseDoc.DataPropertyName = "tus_doc_no";
            this.Itm_BaseDoc.HeaderText = "Base Document";
            this.Itm_BaseDoc.Name = "Itm_BaseDoc";
            this.Itm_BaseDoc.Width = 106;
            // 
            // HpRevertRelease
            // 
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(1000, 627);
            this.Controls.Add(this.lblHpInsBal);
            this.Controls.Add(this.lbl_isECDapplied);
            this.Controls.Add(this.btnECD);
            this.Controls.Add(this.btn_srch_man);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMan);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1016, 666);
            this.Name = "HpRevertRelease";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Revert Release";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HpRevertRelease_FormClosing);
            this.Load += new System.EventHandler(this.HpRevertRelease_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.panel_ecd.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.divECDbal.ResumeLayout(false);
            this.divECDbal.PerformLayout();
            this.divCustomRequest.ResumeLayout(false);
            this.divCustomRequest.PerformLayout();
            this.Panel_voucher.ResumeLayout(false);
            this.Panel_voucher.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvRevertedSerial)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvRevetedItem)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvATradeItem)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void HpRevertRelease_FormClosing(object sender, FormClosingEventArgs e)
        {
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btn_srch_man_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMan;
                _CommonSearch.txtSearchbyword.Text = txtMan.Text;
                _CommonSearch.ShowDialog();
                txtMan.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_man_Click(null, null);
        }

        private void txtMan_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_man_Click(null, null);
        }

        private void txtMan_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMan.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtMan.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid manager code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMan.Text = "";
                    ucReciept1.SelectedManager = "";
                    txtMan.Focus();
                    return;
                }
                ucReciept1.SelectedManager = txtMan.Text;
            }
        }

        private void btnECD_Click(object sender, EventArgs e)
        {
            panel_ecd.Visible = true;
        }

        private void chekApplyECD_CheckedChanged(object sender, EventArgs e)
        {
            if (chekApplyECD.Checked == false)
            {
                try
                {
                    ddlECDType.SelectedItem = "";
                }
                catch (Exception ex)
                {
                }
            }

            if (chekApplyECD.Checked == true)
            {
                if (ddlECDType.SelectedValue == null)
                { return; }

                if (ddlECDType.SelectedValue.ToString() == "Custom")
                {
                    ddlECDType.SelectedItem = "";
                }
            }
        }

        protected void GetUserAppLevel(HirePurchasModuleApprovalCode CD)
        {
            RequestApprovalCycleDefinition(false, CD, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        }

        private void ddlECDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chekApplyECD.Checked = false;

                if (ddlECDType.SelectedValue == null)
                { return; }

                if (ddlECDType.SelectedValue.ToString() != "Custom")
                {
                    if (ddlECDType.SelectedValue.ToString() != "")
                    {
                        chekApplyECD.Enabled = true;

                    }
                }

                GetUserAppLevel(HirePurchasModuleApprovalCode.HSSPDIS);

                lblECD_Balance.Text = "";
                lblECDInsuBal.Text = "";
                divECDbal.Visible = false;

                if (ddlECDType.SelectedValue.ToString() == "Normal")
                {
                    divECDbal.Visible = true;
                    lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_ECDnormalBal);
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccountNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                    divECDbal.Visible = true;
                }
                if (ddlECDType.SelectedValue.ToString() == "Special")
                {
                    divECDbal.Visible = true;
                    lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_ECDspecialBal);
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccountNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                    divECDbal.Visible = true;
                }

                if (ddlECDType.SelectedValue.ToString() == "Voucher")
                {
                    Panel_voucher.Visible = true;
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccountNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                    divECDbal.Visible = true;
                }
                else
                {
                    Panel_voucher.Visible = false;
                }
                if (ddlECDType.SelectedValue.ToString() == "Custom")
                {
                    divCustomRequest.Visible = true;
                    BindRequestsToDropDown(lblAccountNo.Text, ddlPendinReqNo, string.Empty);
                }
                else
                {
                    divCustomRequest.Visible = false;
                }

                IsValidVoucher = "N/A";


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

        private void btnNewCloseFlag_Click(object sender, EventArgs e)
        {
            lbl_isECDapplied.Text = "ECD Balance: ";
            lblHpInsBal.Text = "HP Ins. Balance : ";
            panel_ecd.Visible = false;
            _isECD = false;

            if (chekApplyECD.Checked == true)
            {
                lbl_isECDapplied.Visible = true;
                lblHpInsBal.Visible = true;
                lbl_isECDapplied.Text = lbl_isECDapplied.Text + lblECD_Balance.Text;
                lblHpInsBal.Text = lblHpInsBal.Text + lblECDInsuBal.Text;
                _isECD = true;
                _ECDType = ddlECDType.SelectedValue.ToString();
            }
            else
            {
                ddlECDType.SelectedItem = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Visible = false;

                chekApplyECD.Checked = false;
                chekApplyECD.Enabled = false;
            }
            if (ApprReqNo == "none")
            {
                ddlECDType.SelectedItem = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Visible = false;

                chekApplyECD.Checked = false;
                chekApplyECD.Enabled = false;
            }

            if (ApprReqNo == "none")
            {
                ddlECDType.SelectedItem = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Visible = false;

                chekApplyECD.Checked = false;
                chekApplyECD.Enabled = false;
            }

            //----------------15-July-2013-----------------------------------------------------
            if (lbl_isECDapplied.Visible == true)
            {
                ucReciept1.IsEcd = true;
                ucPayModes1.TotalAmount = Math.Round(Convert.ToDecimal(lblECD_Balance.Text), 0);
                ucPayModes1.LoadData();
                ucReciept1.AmountToPay = Math.Round(Convert.ToDecimal(lblECD_Balance.Text), 0);

                lblTotalReceivable.Text = ucReciept1.AmountToPay.ToString();
                lblSumPay.Text = ucReciept1.AmountToPay.ToString();
                lblSumReceipt.Text = ucReciept1.AmountToPay.ToString();

                lblSumDiscountAmt.Text = Math.Round(ucHpAccountSummary1.Uc_ECDnormal, 2).ToString();

                btnECD.Enabled = false;

            }
            else
            {
                ucReciept1.IsEcd = false;
                btnECD.Enabled = true;
            }
        }

        private void txtVoucherNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtVoucherNum.Text.Trim() != "")
                    {
                        DataTable dt = CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtDate.Text), lblAccountNo.Text.Trim(), txtVoucherNum.Text.Trim());
                        if (dt.Rows.Count < 1)
                        {
                            MessageBox.Show("Invalid Voucher number or voucher date!");
                            IsValidVoucher = "InV";
                            lblECD_Balance.Text = "";
                            return;
                        }
                        else
                        {
                            txtVoucherAmt.Text = dt.Rows[0]["hed_val"].ToString();

                            Decimal ecd = 0;
                            if (dt.Rows[0]["hed_ecd_is_rt"].ToString() == "1")
                            {
                                ecd = ucHpAccountSummary1.Uc_AccBalance * Convert.ToDecimal(txtVoucherAmt.Text.Trim()) / 100;
                            }
                            else
                            {
                                ecd = Convert.ToDecimal(txtVoucherAmt.Text.Trim());
                            }
                            txtVoucherAmt.Text = ecd.ToString();
                            lblECD_Balance.Text = (ucHpAccountSummary1.Uc_AccBalance - ecd).ToString();
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
    }
}