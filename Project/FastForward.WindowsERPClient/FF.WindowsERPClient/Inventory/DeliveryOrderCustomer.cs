using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Inventory;
using System.IO;
using System.Data.OleDb;
using System.Configuration;

namespace FF.WindowsERPClient.Inventory
{
    public partial class DeliveryOrderCustomer : Base
    {
        private List<InvoiceItem> invoice_items = null;
        private List<InvoiceItem> invoice_items_bind = null;
        private string _profitCenter = "";
        private string _accNo = "";
        private string _invoiceType = "";
        private bool IsGrn = false;
        private bool IsdoShedule = false;
        CommonSearch.CommonSearch _commonSearch = null;
        private MasterBusinessEntity _masterBusinessCompany = null;
        string sales_ex_cd = "";

        private Boolean _isExsit = false;

        #region Clear Screen
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void ClearScreen()
        {
            try
            {
                Cursor.Current = Cursors.Default;
                if (BaseCls.GlbIsManChkLoc == true) txtManualRefNo.ReadOnly = true;
                _accNo = string.Empty;
                pnlDealerInvoice.Visible = false;
                txtSCMInvcNo.Clear();
                txtSCMInvcNo.Clear();
                txtSCMInvcDate.Clear();
                txtSCMCustCode.Clear();
                txtSCMCustName.Clear();
                btnUploadSerials.Enabled = true;

                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                dtpDODate.Value = CHNLSVC.Security.GetServerDateTime().Date;

                DataTable dt = CHNLSVC.Sales.GetPendingInvoicesToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefLoca, txtFindCustomer.Text, txtFindInvoiceNo.Text, chkDelFrmAnyLoc.Checked ? 1 : 0);
                if (dt.Rows.Count > 0)
                {
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                }

                chkManualRef.Checked = false;
                chkEditAddress.Checked = false;
                txtManualRefNo.Clear();
                //SetManualRefNo();

                lblBackDateInfor.Text = string.Empty;
                lblInvoiceNo.Text = string.Empty;
                txtInvcNo.Text = string.Empty;
                lblInvoiceDate.Text = string.Empty;
                txtInvcDate.Text = string.Empty;

                txtCustAddress1.Text = string.Empty;
                txtCustAddress2.Text = string.Empty;
                txtCustCode.Text = string.Empty;
                txtCustName.Text = string.Empty;
                txtFindCustomer.Text = string.Empty;
                txtFindInvoiceNo.Text = string.Empty;
                txtVehicleNo.Text = string.Empty;
                txtRemarks.Text = string.Empty;

                List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                _emptyserList = null;
                dvDOSerials.AutoGenerateColumns = false;
                dvDOSerials.DataSource = _emptyserList;

                List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                _emptyinvoiceitemList = null;
                dvDOItems.AutoGenerateColumns = false;
                dvDOItems.DataSource = _emptyinvoiceitemList;
                btnSave.Enabled = true;
                btnGetInvoices.Enabled = true;
                txtDocumentNo.Clear();

                pnlBOCSerials.Visible = false;
                btnBOC.Visible = false;

                btnExcel.Text = "Find Excel File";
                lblExcelPath.Text = "";

                txt_d_cust_cd.Text = "";
                txt_d_cust_name.Text="";
                txt_d_cust_nic.Text = "";
                txt_d_cust_mob.Text = "";
                txt_d_cust_add1.Text = "";
                txt_d_cust_add2.Text = "";
                txt_d_cust_cd.ReadOnly = false;
                txt_d_cust_name.ReadOnly = false;
                txt_d_cust_nic.ReadOnly = false;
                txt_d_cust_mob.ReadOnly = false;
                txt_d_cust_add1.ReadOnly = false;
                txt_d_cust_add2.ReadOnly = false;

                grdshedItm.Visible = false;
                grdshedItm.DataSource = null;
                pnlSheItm.Visible = false;
             

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10097) == false)
                {
                    btnExcel.Visible = false;
                    btnUploadSerials.Visible = false;
                }
                else
                {
                    btnExcel.Visible = true;
                    btnUploadSerials.Visible = true;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Clear Screen

        #region Common Searching Area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + 1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "DO" + seperator + "0" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area

        public DeliveryOrderCustomer()
        {
            InitializeComponent();
            pnlGv.Size = new Size(590, 197);
        }

        private void DeliveryOrderCustomer_Load(object sender, EventArgs e)
        {
            ClearScreen();
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.Text.Replace(" ", ""), dtpDODate, lblBackDateInfor, string.Empty);
            //Edit Chamal on  22/03/2013 according to new back date module
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDODate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }

        private void btnGetInvoices_Click(object sender, EventArgs e)
        {
            try
            {
               
                DataTable dt = CHNLSVC.Sales.GetPendingInvoicesToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefLoca, txtFindCustomer.Text, txtFindInvoiceNo.Text, chkDelFrmAnyLoc.Checked ? 1 : 0);
               
                if (dt.Rows.Count > 0)
                {
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                }
                else
                {
                    dt = null;
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                    MessageBox.Show("No pending invoices found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generate new user seq no

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "DO", 1, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "DO";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = false; //direction always (-) for change status
            RPH.Tuh_doc_no = lblInvoiceNo.Text;
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            CHNLSVC.CloseAllChannels();
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        #endregion Generate new user seq no

        private void LoadInvoiceItems(string _invNo, string _pc)
        {
            invoice_items_bind = new List<InvoiceItem>();
            //Get Invoice Items Details
            invoice_items = CHNLSVC.Sales.GetAllSaleDocumentItemList(BaseCls.GlbUserComCode, _pc, "INV", _invNo, "A");
            if (invoice_items != null)
            {
                if (invoice_items.Count > 0)
                {
                    dvDOItems.Enabled = true;
                    //Check serial reserved for vehicle registration
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("RECEIPT", BaseCls.GlbUserComCode, _invNo, 0);
                    if (user_seq_num != -1)
                    {
                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = invoice_items;
                        //dvDOItems.Enabled = false;
                        MessageBox.Show("Insuarance dept. still not issue cover note.", "Vehicle Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", BaseCls.GlbUserComCode, _invNo, 0);
                    if (user_seq_num == -1)
                    {
                        //Generate new user seq no and add new row to pick_hdr
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "DO");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (InvoiceItem _invItem in invoice_items)
                                if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                {
                                    //it.Sad_do_qty = q.theCount;
                                    //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                    _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                }
                        }
                        dvDOSerials.AutoGenerateColumns = false;
                        dvDOSerials.DataSource = _serList;
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        dvDOSerials.AutoGenerateColumns = false;
                        dvDOSerials.DataSource = emptyGridList;
                    }

                    if (gvSGv.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow _row in gvSGv.Rows)
                        {
                            Int32 _invline = Convert.ToInt32(_row.Cells["sgv_invline"].Value);
                            foreach (InvoiceItem _invItem in invoice_items)
                                if (_invline == _invItem.Sad_itm_line)
                                    _invItem.Sad_srn_qty += 1;
                        }
                    }
                    if (_serList == null)
                    {
                        //add by tharanga 2018/06/07 ) SRN qty.
                        foreach (InvoiceItem _invItem in invoice_items)
                        {
                            _invItem.Sad_srn_qty = 0;
                        }
                    }
                  
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = invoice_items;
                }
            }
            else
            {
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                dvDOSerials.AutoGenerateColumns = false;
                dvDOSerials.DataSource = emptyGridList;

                InvoiceItem it = new InvoiceItem();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Sad_alt_itm_desc = "";// mi.Mi_shortdesc;
                it.Mi_model = "";// mi.Mi_model;
                it.Sad_qty = 0;
                it.Sad_tot_amt = 0;

                invoice_items_bind = new List<InvoiceItem>();

                //invoice_items.Clear();
                //invoice_items = null;
                //invoice_items_bind.Clear();
                //invoice_items_bind = null;
                invoice_items_bind.Add(it);

                dvDOItems.DataSource = invoice_items_bind;

                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending items found for this invoice!");
                return;
            }

            //get all from sat_itm
        }

        //private void dvPendingInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //if (e.ColumnIndex == dvPendingInvoices.Columns["SAH_INV_NO"].Index && e.RowIndex >= 0)
        //{
        //    //MessageBox.Show(dvPendingInvoices.Item(e.ColumnIndex, e.RowIndex).Value.ToString);
        //    //MessageBox.Show(dvPendingInvoices.CurrentCell.Value.ToString());
        //    string _invoiceNo = dvPendingInvoices.CurrentCell.Value.ToString();
        //    LoadInvoiceItems(_invoiceNo);

        //}
        //}

        private void dvDOItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool _autoScroll = false;
            int _itemLineNo = 0;
            try
            {
                if (dvDOItems.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    #region Add Serials

                    if (e.ColumnIndex == 0)
                    {
                       
                        //MessageBox.Show(dvDOItems.Rows[e.RowIndex].Cells["sad_itm_line"].Value.ToString());
                        string _invoiceNo = dvDOItems.Rows[e.RowIndex].Cells["SAD_INV_NO"].Value.ToString();
                        _itemLineNo = Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["sad_itm_line"].Value.ToString());

                        string _itemCode = dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString();
                        string _similaritemCode = dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value.ToString();
                        if (!string.IsNullOrEmpty(_similaritemCode))
                        {
                            if (_itemCode != _similaritemCode)
                            {
                                _itemCode = _similaritemCode;
                            }
                        }
                        string _itemstatus = dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_stus"].Value.ToString();
                        decimal _invoiceQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["Sad_qty"].Value.ToString());
                        decimal _doQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["Sad_do_qty"].Value.ToString());
                        decimal _scanQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["PickQty"].Value.ToString());
                        string _priceBook = dvDOItems.Rows[e.RowIndex].Cells["sad_pbook"].Value.ToString();
                        string _priceLevel = dvDOItems.Rows[e.RowIndex].Cells["sad_pb_lvl"].Value.ToString();
                        int pbCount = CHNLSVC.Sales.GetDOPbCount(BaseCls.GlbUserComCode, _priceBook, _priceLevel);
                        string _promotioncd = Convert.ToString(dvDOItems.Rows[e.RowIndex].Cells["SAD_PROMO_CD"].Value.ToString());
                        bool _isAgePriceLevel = false;
                        bool _isSerializedPrice = false;
                        int _ageingDays = -1;

                        if (string.IsNullOrEmpty(_profitCenter))
                        {
                            MessageBox.Show("Profit center cannot be null", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCode);
                         MasterLocation _MasterLocation = CHNLSVC.General.GetAllLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 0);

                         if (_MasterLocation.Ml_is_serial == false)
                         {
                             if (_item.Mi_is_ser1 != -1)
                             {
                                 MessageBox.Show("This Location is not setup for serial maintain Pls. contact Inventory Department. ", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 return;
                             }
                         }


                        DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_item.Mi_cate_1);
                        List<PriceBookLevelRef> _level = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _priceBook, _priceLevel);
                        if (_level != null)
                            if (_level.Count > 0)
                            {
                                var _lvl = _level.Where(x => x.Sapl_isage).ToList();
                                if (_lvl != null) if (_lvl.Count() > 0)
                                        _isAgePriceLevel = true;

                                var _lvl1 = _level.Where(x => x.Sapl_is_serialized).ToList();
                                if (_lvl1 != null) if (_lvl1.Count() > 0)
                                        _isSerializedPrice = true;
                            }

                        if (_categoryDet != null && _isAgePriceLevel)
                            if (_categoryDet.Rows.Count > 0)
                            {
                                if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                                    _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                                else _ageingDays = 0;
                            }

                        if ((_invoiceQty - _doQty) <= 0) return;
                        if ((_invoiceQty - _doQty) <= _scanQty) return;

                        //kapila 20/5/2016 check allow limit for DO without approval is exceeded
                        if (dvDOItems.Rows.Count > 0)
                        {
                            for (int i = 0; i < dvDOItems.Rows.Count; i++)
                            {

                                if (Convert.ToBoolean(dvDOItems.Rows[e.RowIndex].Cells["SAD_ISCOVERNOTE"].Value) != true)
                                {
                                    MessageBox.Show("For item code " + dvDOItems.Rows[i].Cells["SAD_ITM_CD"].Value.ToString() + ", still not issue cover note!", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        //add by tharanga 2017/09/06
                        if (grdshedItm.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow row in grdshedItm.Rows)
                            {
                                if (row.Cells["sid_itm_cd"].Value.ToString() == _itemCode)
                                {
                                    if (row.Cells["sid_d_cust_cd"].Value.ToString() != txt_d_cust_cd.Text.ToString().Trim())
                                    {
                                        MessageBox.Show("Deliver Customer Different.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    { break; }

                                }
                            }
                        }
                      
                        decimal _maxDO = 0;
                        decimal _maxDoDays = 0;
                        Boolean _isOk = false;

                        HpSystemParameters _SystemPara = new HpSystemParameters();
                        _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSDO", Convert.ToDateTime(dtpDODate.Text).Date);
                        if (_SystemPara.Hsy_cd != null)
                        {
                            _maxDO = _SystemPara.Hsy_val;
                        }
                        if (_SystemPara.Hsy_cd == null)
                        {
                            _SystemPara = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSDO", Convert.ToDateTime(dtpDODate.Text).Date);
                            if (_SystemPara.Hsy_cd != null)
                            {
                                _maxDO = _SystemPara.Hsy_val;
                            }
                        }
                        if (_SystemPara.Hsy_cd == null)
                        {
                            _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSDO", Convert.ToDateTime(dtpDODate.Text).Date);
                            if (_SystemPara.Hsy_cd != null)
                            {
                                _maxDO = _SystemPara.Hsy_val;
                            }
                        }


                        if (_maxDO > 0)
                        {
                            //maximum DO days
                            _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSMAXDO", Convert.ToDateTime(dtpDODate.Text).Date);
                            if (_SystemPara.Hsy_cd != null)
                            {
                                _maxDoDays = _SystemPara.Hsy_val;
                            }
                            if (_SystemPara.Hsy_cd == null)
                            {
                                _SystemPara = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSMAXDO", Convert.ToDateTime(dtpDODate.Text).Date);
                                if (_SystemPara.Hsy_cd != null)
                                {
                                    _maxDoDays = _SystemPara.Hsy_val;
                                }
                            }
                            if (_SystemPara.Hsy_cd == null)
                            {
                                _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSMAXDO", Convert.ToDateTime(dtpDODate.Text).Date);
                                if (_SystemPara.Hsy_cd != null)
                                {
                                    _maxDoDays = _SystemPara.Hsy_val;
                                }
                            }

                            DataTable _dt = CHNLSVC.Financial.IsDoDaysExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_maxDoDays));
                            if (_maxDoDays > 0)
                            {
                                if (_dt.Rows.Count > 0)
                                {
                                    MessageBox.Show("Exceed the allowed no of days from delivery for " + _dt.Rows[0]["sah_inv_no"].ToString() + ". Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            }

                            decimal _totsaleqty = 0;
                            int _effc = CHNLSVC.Financial.GetTotSadQty(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out _totsaleqty);

                            if (dvDOItems.Rows.Count > 0)
                            {
                                for (int i = 0; i < dvDOItems.Rows.Count; i++)
                                {
                                    if (Convert.ToBoolean(dvDOItems.Rows[e.RowIndex].Cells["SAD_ISAPP"].Value) != true)
                                    {
                                        if (_maxDO > 0)
                                        {
                                            if (_maxDO <= _totsaleqty)
                                            {
                                                MessageBox.Show("Exceed the allowed no of DCNs without having registration approval. Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            _totsaleqty = _totsaleqty + Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["sad_do_qty"].Value);

                                        }
                                    }
                                }
                            }
                        }

                        //if (Convert.ToBoolean(dvDOItems.Rows[e.RowIndex].Cells["SAD_ISAPP"].Value) != true)
                        //{
                        //    MessageBox.Show("Item is not approved for delivery!", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        //if (Convert.ToBoolean(dvDOItems.Rows[e.RowIndex].Cells["SAD_ISCOVERNOTE"].Value) != true)
                        //{
                        //    MessageBox.Show("Still not issue cover note!", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}

                        _autoScroll = true;

                        //DataTable dt = CHNLSVC.Sales.GetPendingInvoiceItemsByItemDT(_invoiceNo, _itemCode);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    if (Convert.ToInt32(dt.Rows[0]["SAD_ISAPP"]) != 1 || Convert.ToInt32(dt.Rows[0]["SAD_ISCOVERNOTE"]) != 1)
                        //    {
                        //        //CanSave = false;
                        //        MessageBox.Show("Not Approved to release item", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        return;
                        //    }
                        //}
                        pnlGv.Visible = false;
                        lblGvItem.Text = string.Empty; ;
                        lblGvStatus.Text = string.Empty;
                        lblGvQty.Text = string.Empty;
                        lblInvLine.Text = string.Empty;
                        if (_item.Mi_itm_tp == "G" && !string.IsNullOrEmpty(_promotioncd))
                        {
                            MessageBox.Show("This gift voucher referring promotion", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (_item.Mi_itm_tp == "G" && string.IsNullOrEmpty(_promotioncd))
                        {
                            DataTable _voucher = CHNLSVC.Sales.GetInvoiceVoucher(lblInvoiceNo.Text.Trim(), _itemCode);
                            if (_voucher != null)
                                if (_voucher.Rows.Count > 0)
                                {
                                    pnlGv.Visible = true;
                                    lblGvItem.Text = _itemCode;
                                    lblGvStatus.Text = _itemstatus;
                                    lblGvQty.Text = Convert.ToString(_invoiceQty - _doQty);
                                    lblInvLine.Text = Convert.ToString(_itemLineNo);
                                }
                        }
                        else if (chkChangeSimilarItem.Checked == false)
                        {
                            CommonSearch.CommonOutScan _commonOutScan = new CommonSearch.CommonOutScan();
                            _commonOutScan.PriceBook = _priceBook;
                            _commonOutScan.PriceLevel = _priceLevel;
                            _commonOutScan.ModuleTypeNo = 1;
                            _commonOutScan.ScanDocument = _invoiceNo;
                            _commonOutScan.DocumentType = "DO";
                            _commonOutScan.PopupItemCode = _itemCode;
                            _commonOutScan.ItemStatus = _itemstatus;
                            _commonOutScan.ItemLineNo = _itemLineNo;
                            _commonOutScan.PopupQty = _invoiceQty - _doQty;
                            _commonOutScan.ApprovedQty = _doQty;
                            _commonOutScan.ScanQty = _scanQty;
                            _commonOutScan.IsAgePriceLevel = _isAgePriceLevel;
                            _commonOutScan.IsSerializedPrice = _isSerializedPrice; //Add by Chamal 23/07/2014
                            _commonOutScan.DocumentDate = dtpDODate.Value.Date;
                            _commonOutScan.NoOfDays = _ageingDays;
                            if (pbCount <= 0) _commonOutScan.IsCheckStatus = false;
                            else _commonOutScan.IsCheckStatus = true;

                            _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                            //this.Enabled = false;
                            _commonOutScan.ShowDialog();
                            //this.Enabled = true;
                        }
                        else if (chkChangeSimilarItem.Checked)
                        {
                            DataTable _dtTable;
                            if (_level != null && _level.Count > 0)
                            {
                                foreach (PriceBookLevelRef _lvlDet in _level)
                                {
                                    //Add Chamal 29/03/2013
                                    decimal _balQty = _invoiceQty - _doQty;
                                    if (_isAgePriceLevel == false)
                                        _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), _lvlDet.Sapl_itm_stuts);
                                    else
                                        _dtTable = CHNLSVC.Inventory.GetInventoryBalanceByBatch(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value), _lvlDet.Sapl_itm_stuts);

                                    if (_dtTable != null)
                                    {
                                        if (_dtTable.Rows.Count > 0)
                                        {
                                            bool _isInventoryBalanceAvailable = false;

                                            if (_isAgePriceLevel == false)
                                                _isInventoryBalanceAvailable = true;
                                            else
                                            {
                                                var _isChkStus = _level.Where(x => x.Sapl_chk_st_tp).Count();
                                                if (_isChkStus > 0)
                                                {
                                                    var _isAvailable = _dtTable.AsEnumerable().Where(x => x.Field<string>("inb_itm_stus") == _itemstatus && x.Field<DateTime>("inb_doc_dt").Date <= Convert.ToDateTime(dtpDODate.Value.Date).Date.AddDays(-_ageingDays)).Count();
                                                    if (_isAvailable > 0) _isInventoryBalanceAvailable = true;
                                                }
                                                else
                                                {
                                                    var _isAvailable = _dtTable.AsEnumerable().Where(x => x.Field<DateTime>("inb_doc_dt").Date <= Convert.ToDateTime(dtpDODate.Value.Date).Date.AddDays(-_ageingDays)).Count();
                                                    if (_isAvailable > 0) _isInventoryBalanceAvailable = true;
                                                }
                                            }

                                            if (_isInventoryBalanceAvailable)
                                            {
                                                MessageBox.Show("Cannot select the similar item! Because stock balance are available for invoice item", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }

                            CommonSearch.SearchSimilarItems _similarItems = new CommonSearch.SearchSimilarItems();
                            _similarItems.DocumentType = "S";
                            _similarItems.ItemCode = _itemCode;
                            _similarItems.FunctionDate = dtpDODate.Value.Date;
                            _similarItems.DocumentNo = lblInvoiceNo.Text;
                            _similarItems.PromotionCode = dvDOItems.Rows[e.RowIndex].Cells["sad_promo_cd"].Value.ToString();
                            _similarItems.obj_TragetTextBox = txtDocumentNo;
                            _similarItems.ShowDialog();
                            if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                            {
                                dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value = txtDocumentNo.Text;
                                txtDocumentNo.Clear();
                                CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(lblInvoiceNo.Text, _itemLineNo, dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value.ToString());
                            }
                            chkChangeSimilarItem.Checked = false;
                        }                       

                        LoadInvoiceItems(_invoiceNo, _profitCenter);
                    }

                    #endregion Add Serials

                    #region remove simmilar item
                    if (chkChangeSimilarItem.Checked == true)
                    {
                        if (e.RowIndex != -1)
                        {
                            if (dvDOItems.Columns[e.ColumnIndex].Name == "Sad_sim_itm_cd" && !string.IsNullOrEmpty(dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value.ToString()))
                            {
                                if (Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["PickQty"].Value.ToString()) == 0)
                                {
                                    if (MessageBox.Show("Do you need to remove this sim?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(dvDOItems.Rows[e.RowIndex].Cells["SAD_INV_NO"].Value.ToString(), Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["sad_itm_line"].Value.ToString()), dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), string.Empty);
                                        dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
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
                if (_autoScroll == true)
                {
                    if (dvDOItems.Rows.Count > 0)
                    {
                        if (dvDOItems.Rows.Count <= _itemLineNo)
                        {
                            dvDOItems.Rows[_itemLineNo - 1].Selected = true;
                            dvDOItems.FirstDisplayedScrollingRowIndex = _itemLineNo - 1;
                        }
                        else
                        {
                            dvDOItems.Rows[_itemLineNo].Selected = true;
                            dvDOItems.FirstDisplayedScrollingRowIndex = _itemLineNo;
                        }
                    }
                }
            }
        }

        private void DeliveryOrderCustomer_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == true)
            {
                //MessageBox.Show("OK");
                LoadInvoiceItems(lblInvoiceNo.Text, _profitCenter);
            }
        }

        private bool IsGiftVoucherAvailable()
        {
            var _count = invoice_items.Where(x => x.Sad_itm_tp == "G" && string.IsNullOrEmpty(x.Sad_promo_cd) && x.Sad_qty - x.Sad_do_qty > 0).Count();
            if (_count <= 0)
                return false;
            else
                return true;
        }

        private bool IsGiftVoucherNAttachedItemTally()
        {
            var _attachedItem = (from DataGridViewRow _row in gvSGv.Rows
                                 select Convert.ToString(_row.Cells["sgv_attacheditem"].Value)).ToList().Distinct();

            foreach (var _itm in _attachedItem)
            {
                var _scanqty = invoice_items.Where(x => x.Sad_itm_cd == _itm).ToList().Sum(x => x.Sad_srn_qty);
                int _pickgv = 0;
                foreach (DataGridViewRow _row in gvSGv.Rows)
                {
                    string _item = Convert.ToString(_row.Cells["sgv_attacheditem"].Value);
                    if (_item == _itm)
                        _pickgv += 1;
                }

                if (_scanqty != _pickgv)
                {
                    MessageBox.Show("Item and verified gift voucher are mismatched. Attach item : " + _itm, "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        private List<InvoiceVoucher> GetInvoiceVoucher()
        {
            List<InvoiceVoucher> _lst = null;
            foreach (DataGridViewRow _row in gvGiftVoucher.Rows)
            {
                InvoiceVoucher _one = new InvoiceVoucher();
                _one.Stvo_bookno = Convert.ToInt32(_row.Cells["sgv_book"].Value);
                _one.Stvo_gv_itm = Convert.ToString(_row.Cells["sgv_item"].Value);
                _one.Stvo_inv_no = Convert.ToString(lblInvoiceNo.Text.Trim());
                _one.Stvo_itm_cd = Convert.ToString(_row.Cells["sgv_attacheditem"].Value);
                _one.Stvo_pageno = Convert.ToInt32(_row.Cells["sgv_page"].Value);
                _one.Stvo_stus = Convert.ToInt32(2);
            }
            return _lst;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void Process()
        {
            try
            {
                InventoryDoShedule invHdrDoShedule = new InventoryDoShedule();
                List<InventoryDoShedule> _InventoryDoSheduleList = new List<InventoryDoShedule>();

                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(lblInvoiceNo.Text))
                {
                    MessageBox.Show("Select the invoice no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
              //  if (string.IsNullOrEmpty(txt_d_cust_mob.Text))
               // {
               //     MessageBox.Show("Enter Delivered Customer Mobile Number ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
               //     return;
               // }
                if (chkManualRef.Checked)
                    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                    {
                        MessageBox.Show("You do not enter a valid manual document no.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                //add by tharanga 2017/09/06
                if ( grdshedItm.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in grdshedItm.Rows)
                    {
                        foreach (DataGridViewRow _dvDOItems in dvDOItems.Rows)
                        {
                            if (row.Cells["sid_itm_cd"].Value.ToString() == _dvDOItems.Cells["SAD_ITM_CD"].Value.ToString())
                            {
                                if (Convert.ToInt32(row.Cells["sid_qty"].Value.ToString()) < Convert.ToInt32(_dvDOItems.Cells["PickQty"].Value.ToString()))
                                {
                                    MessageBox.Show("Pic Qty Incorrect." + _dvDOItems.Cells["SAD_ITM_CD"].Value.ToString(), "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            }

                        }
                    }
                 

                    foreach (DataGridViewRow _row in grdshedItm.Rows)
                    {
                       // invHdrDoShedule.sid_seq_no = Convert.ToInt32(_row.Cells["Sad_seq_no"].Value.ToString());
                        //invHdrDoShedule.sid_itm_line = Convert.ToInt32(row.Cells["SAD_ITM_LINE"].Value.ToString());

                        invHdrDoShedule.sid_inv_no = _row.Cells["sid_inv_no"].Value.ToString();
                        invHdrDoShedule.sid_itm_cd = _row.Cells["sid_itm_cd"].Value.ToString();
                        foreach (DataGridViewRow _dvDOItems in dvDOItems.Rows)
                        {
                            if (invHdrDoShedule.sid_itm_cd==_dvDOItems.Cells["SAD_ITM_CD"].Value.ToString())
                            {
                                invHdrDoShedule.sid_do_qty = Convert.ToInt32(_dvDOItems.Cells["PickQty"].Value.ToString());
                            }
                        
                        }
                        invHdrDoShedule.sid_del_line = Convert.ToInt32(_row.Cells["sid_del_line"].Value.ToString());
                        invHdrDoShedule.sid_del_com = BaseCls.GlbUserComCode;
                        invHdrDoShedule.sid_del_loc = BaseCls.GlbUserDefLoca;
                        invHdrDoShedule.sid_cre_by = BaseCls.GlbUserID;
                        invHdrDoShedule.sid_cre_session = BaseCls.GlbUserSessionID;
                    }
                    _InventoryDoSheduleList.Add(invHdrDoShedule);
 
                }
                

                if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "0";
                if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
                if (resultDate > 0)
                {
                    MessageBox.Show("Delivery date should be greater than or equal to invoice date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDODate, lblH1, dtpDODate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpDODate.Value.Date != DateTime.Now.Date)
                        {
                            dtpDODate.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpDODate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpDODate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpDODate.Focus();
                        return;
                    }
                }

                if (IsGiftVoucherAvailable())
                {
                    if (gvSGv.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please verify the gift voucher.", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (IsGiftVoucherNAttachedItemTally() == false)
                        return;
                }

                //kapila 20/5/2016 check allow limit for DO without approval is exceeded

                decimal _maxDO = 0;
                decimal _maxDoDays = 0;
                Boolean _isOk = false;

                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSDO", Convert.ToDateTime(dtpDODate.Text).Date);
                if (_SystemPara.Hsy_cd != null)
                {
                    _maxDO = _SystemPara.Hsy_val;
                }
                if (_SystemPara.Hsy_cd == null)
                {
                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSDO", Convert.ToDateTime(dtpDODate.Text).Date);
                    if (_SystemPara.Hsy_cd != null)
                    {
                        _maxDO = _SystemPara.Hsy_val;
                    }
                }
                if (_SystemPara.Hsy_cd == null)
                {
                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSDO", Convert.ToDateTime(dtpDODate.Text).Date);
                    if (_SystemPara.Hsy_cd != null)
                    {
                        _maxDO = _SystemPara.Hsy_val;
                    }
                }

                if (_maxDO > 0)
                {
                    //maximum DO days
                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSMAXDO", Convert.ToDateTime(dtpDODate.Text).Date);
                    if (_SystemPara.Hsy_cd != null)
                    {
                        _maxDoDays = _SystemPara.Hsy_val;
                    }
                    if (_SystemPara.Hsy_cd == null)
                    {
                        _SystemPara = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSMAXDO", Convert.ToDateTime(dtpDODate.Text).Date);
                        if (_SystemPara.Hsy_cd != null)
                        {
                            _maxDoDays = _SystemPara.Hsy_val;
                        }
                    }
                    if (_SystemPara.Hsy_cd == null)
                    {
                        _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSMAXDO", Convert.ToDateTime(dtpDODate.Text).Date);
                        if (_SystemPara.Hsy_cd != null)
                        {
                            _maxDoDays = _SystemPara.Hsy_val;
                        }
                    }
                    decimal _totsaleqty = 0;
                    int _effc = CHNLSVC.Financial.GetTotSadQty(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out _totsaleqty);

                    DataTable _dt = CHNLSVC.Financial.IsDoDaysExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_maxDoDays));
                    if (_maxDoDays > 0)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Exceed the allowed no of days from delivery for " + _dt.Rows[0]["sah_inv_no"].ToString() + ". Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }

                    if (dvDOItems.Rows.Count > 0)
                    {
                        for (int i = 0; i < dvDOItems.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dvDOItems.Rows[i].Cells["SAD_ISAPP"].Value) != true)
                            {
                                if (_maxDO > 0)
                                {
                                    if (_maxDO <= _totsaleqty)
                                    {
                                        MessageBox.Show("Exceed the allowed no of DCNs without having registration approval. Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    _totsaleqty = _totsaleqty + Convert.ToInt32(dvDOItems.Rows[i].Cells["sad_do_qty"].Value);

                                }
                            }
                        }
                    }
                }


                #region Check more qty pick against the invoice qty

                //if (invoice_items != null)
                //{
                //    if (invoice_items.Count > 0)
                //    {
                //        foreach (InvoiceItem _i in invoice_items)
                //        {
                //            if (_i.Sad_qty - _i.Sad_do_qty < _i.Sad_srn_qty)
                //            {
                //                MessageBox.Show(_i.Sad_itm_cd + "- Scanning qty grater than the invoice pending qty!", "Scanning Qty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                return;
                //            }
                //        }
                //    }
                //}

                #endregion Check more qty pick against the invoice qty

                Cursor.Current = Cursors.WaitCursor;

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", BaseCls.GlbUserComCode, lblInvoiceNo.Text, 0);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "DO");

                //updated by akila 2018/01/09
                if ((reptPickSerialsList == null) && (!IsGvAvailableAsSimilerItm()))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No delivery items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {                    
                    string _invNo = lblInvoiceNo.Text.ToUpper().Trim();
                    List<InvoiceItem> _invItms = CHNLSVC.Sales.GetAllInvoiceItems(_invNo);
                    if (_invItms != null)
                    {
                        var _invItmsWithRes = _invItms.Where(c => !string.IsNullOrEmpty(c.Sad_res_no) && c.Sad_res_no != "N/A" && c.Sad_res_no != "PROMO_VOU").ToList();
                        if (_invItmsWithRes != null && _invItmsWithRes.Count > 0)
                        {
                            foreach (var item in _invItmsWithRes)
                            {
                                foreach (var _pickSer in reptPickSerialsList)
                                {
                                    if (_pickSer.Tus_itm_cd == item.Sad_itm_cd && _pickSer.Tus_base_itm_line == item.Sad_itm_line)
                                    {
                                        _pickSer.Tus_resqty = _pickSer.Tus_qty;
                                    }
                                }
                            }
                            invHdr.UpdateResLog = true;
                        }
                    }
                }
               //add by tharanga 2017/11/07
                List<InvoiceItem> _invItmsnew = CHNLSVC.Sales.GetAllInvoiceItems(lblInvoiceNo.Text.ToUpper().Trim());
                List<ReceiptItemDetails> ReceiptItemDetails = CHNLSVC.Sales.ReceiptItemDetailsNew(lblInvoiceNo.Text.ToUpper().Trim());
                if (ReceiptItemDetails!=null)
                {
                    if (ReceiptItemDetails.Count > 0)
                    {
                        foreach (var _ser in reptPickSerialsList)
                        {
                            var _satItmList = _invItmsnew.Where(c => c.Sad_itm_line == _ser.Tus_base_itm_line).ToList();
                            foreach (var _invItm in _satItmList)
                            {
                                var _recItmList = ReceiptItemDetails.Where(c => c.Sari_line == _invItm.Sad_itm_line && c.sari_is_res == 1 && c.sari_res_qty > 0).ToList();
                                foreach (ReceiptItemDetails _ReceiptItemDetails in _recItmList)
                                {
                                    _ser.Tus_resqty = _ser.Tus_qty;
                                }
                            }
                        }
                    }
                }
               
                //foreach (InvoiceItem item in _invItmsnew)
                //{
                //    foreach (ReceiptItemDetails _ReceiptItemDetails in ReceiptItemDetails)
                //    {
                //        if (item.Sad_itm_line==_ReceiptItemDetails.Sari_line)
                //        {

                //             _pickSer.Tus_resqty = _pickSer.Tus_qty;
                //        }
                //    }

                //}

                #region Check Registration Txn Serials
                bool _isRegTxnFound = false;
                List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblInvoiceNo.Text, String.Empty);
                if (_tmpReg != null && _tmpReg.Count > 0) _isRegTxnFound = true;

                if (_isRegTxnFound == true)
                {
                    foreach (ReptPickSerials _serRow in reptPickSerialsList)
                    {
                        MasterItem _itm = CHNLSVC.Inventory.GetItem("", _serRow.Tus_itm_cd);
                        if (_itm.Mi_need_reg == true)
                        {
                            int _countReg = _tmpReg.Where(x => x.P_srvt_itm_cd == _serRow.Tus_itm_cd && x.P_svrt_engine == _serRow.Tus_ser_1).Count();
                            if (_countReg <= 0)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Invliad delivery item/serial [" + _serRow.Tus_itm_cd + "] - [" + _serRow.Tus_ser_1 + "]", "Wrong Serials Pick", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region Check Reference Date and the Doc Date
                //updated by akila 2018/01/09
                if (reptPickSerialsList != null && reptPickSerialsList.Count > 0)
                {
                    if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }               
                #endregion Check Reference Date and the Doc Date

                #region Check Duplicate Serials
                if(reptPickSerialsList != null && reptPickSerialsList.Count > 0)
                {
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                

                #endregion Check Duplicate Serials

                #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                //updated by akila 2018/01/09
                if (reptPickSerialsList != null && reptPickSerialsList.Count > 0)
                {
                    //add by darshana on 12-Mar-2014 - To Gold operation totally operate as consignment base and no need to generate grn.
                    MasterCompany _masterComp = null;
                    _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                    if (_masterComp.Mc_anal13 == 0)
                    {
                        if (CHNLSVC.Inventory.Check_Cons_Item_has_Quo(BaseCls.GlbUserComCode, dtpDODate.Value.Date, reptPickSerialsList, out documntNo) < 0)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(documntNo, "Quotation not define", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    //add by Chamal on 22-Aug-2014
                    var _consSupp = from _ListConsSupp in reptPickSerialsList
                                    where _ListConsSupp.Tus_itm_stus == "CONS"
                                    group _ListConsSupp by new { _ListConsSupp.Tus_orig_supp } into list
                                    select new { supp = list.Key.Tus_orig_supp };
                    foreach (var listsSupp in _consSupp)
                    {
                        MasterBusinessEntity _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, listsSupp.supp.ToString(), null, null, "S");
                        if (_supDet == null)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Cannot find supplier details.", "Consignment Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                #endregion Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "DO");

                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    invHdr.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        invHdr.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        invHdr.Ith_channel = string.Empty;
                    }

                    if (System.DBNull.Value != r["ML_CATE_1"])
                    {
                        invHdr.Ith_cate_tp = (string)r["ML_CATE_1"];
                    }
                    else
                    {
                        invHdr.Ith_cate_tp = _invoiceType;
                    }
                    
                }

                #region Fill DO Header

                invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                invHdr.Ith_com = BaseCls.GlbUserComCode;
                invHdr.Ith_doc_tp = "DO";
                invHdr.Ith_doc_date = dtpDODate.Value.Date;
                invHdr.Ith_doc_year = dtpDODate.Value.Date.Year;
                invHdr.Ith_sub_tp = "DPS";
                invHdr.Ith_is_manual = false;
                invHdr.Ith_stus = "A";
                invHdr.Ith_cre_by = BaseCls.GlbUserID;
                invHdr.Ith_mod_by = BaseCls.GlbUserID;
                invHdr.Ith_direct = false;
                invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                invHdr.Ith_manual_ref = txtManualRefNo.Text;
                invHdr.Ith_vehi_no = txtVehicleNo.Text;
                invHdr.Ith_remarks = txtRemarks.Text;
                invHdr.Ith_anal_1 = _userSeqNo.ToString();
                invHdr.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                invHdr.Ith_entry_no = lblInvoiceNo.Text.ToString();
                invHdr.Ith_bus_entity = txtCustCode.Text;
                //invHdr.Ith_del_add1 = txtCustAddress1.Text;
                //invHdr.Ith_del_add2 = txtCustAddress2.Text;

                invHdr.Ith_del_code = txt_d_cust_cd.Text;
                invHdr.Ith_del_cust_name = txt_d_cust_name.Text;
                invHdr.Ith_del_add1 = txt_d_cust_add1.Text;
                invHdr.Ith_del_add2 = txt_d_cust_add2.Text;

                invHdr.Ith_gen_frm = "SCMWIN";
                invHdr.Ith_acc_no = _accNo;
                invHdr.Ith_pc = _profitCenter;
                
                #endregion Fill DO Header

                MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                masterAutoNum.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                masterAutoNum.Aut_cate_tp = "LOC";
                masterAutoNum.Aut_direction = 0;
                masterAutoNum.Aut_moduleid = "DO";
                masterAutoNum.Aut_start_char = "DO";
                masterAutoNum.Aut_year = dtpDODate.Value.Date.Year;
                List<ReptPickSerials> reptPickSerialsListGRN = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsListGRN = new List<ReptPickSerialsSub>();
                if (reptPickSerialsList != null)
                { reptPickSerialsListGRN = reptPickSerialsList; }

                if (reptPickSubSerialsList != null)
                { reptPickSubSerialsListGRN = reptPickSubSerialsList; }

                InventoryHeader _invHeaderGRN = null;
                MasterAutoNumber _masterAutoGRN = null;
                string documntNoGRN = "";
                if (IsGrn)
                {
                    _invHeaderGRN = new InventoryHeader();
                    _invHeaderGRN.Ith_com = BaseCls.GlbUserComCode;
                    _invHeaderGRN.Ith_loc = BaseCls.GlbUserDefLoca;
                    _invHeaderGRN.Ith_doc_date = dtpDODate.Value.Date;
                    _invHeaderGRN.Ith_doc_year = dtpDODate.Value.Date.Year;
                    _invHeaderGRN.Ith_direct = true;
                    _invHeaderGRN.Ith_doc_tp = "GRN";
                    _invHeaderGRN.Ith_cate_tp = "NOR";
                    _invHeaderGRN.Ith_sub_tp = "LOCAL";
                    //_invHeader.Ith_bus_entity = lblSupplierCode.Text;
                    _invHeaderGRN.Ith_is_manual = true;
                    _invHeaderGRN.Ith_manual_ref = txtManualRefNo.Text;
                    _invHeaderGRN.Ith_remarks = txtRemarks.Text;
                    _invHeaderGRN.Ith_stus = "A";
                    _invHeaderGRN.Ith_cre_by = BaseCls.GlbUserID;
                    _invHeaderGRN.Ith_cre_when = DateTime.Now;
                    _invHeaderGRN.Ith_mod_by = BaseCls.GlbUserID;
                    _invHeaderGRN.Ith_mod_when = DateTime.Now;
                    _invHeaderGRN.Ith_session_id = BaseCls.GlbUserSessionID;
                    _invHeaderGRN.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                    _invHeaderGRN.Ith_entry_no = lblInvoiceNo.Text.ToString();

                    _masterAutoGRN = new MasterAutoNumber();
                    _masterAutoGRN.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _masterAutoGRN.Aut_cate_tp = "LOC";
                    _masterAutoGRN.Aut_direction = null;
                    _masterAutoGRN.Aut_modify_dt = null;
                    _masterAutoGRN.Aut_moduleid = "GRN";
                    _masterAutoGRN.Aut_number = 0;
                    _masterAutoGRN.Aut_start_char = "GRN";
                    _masterAutoGRN.Aut_year = _invHeaderGRN.Ith_doc_date.Date.Year;

                    //result = CHNLSVC.Inventory.DeliveryOrder_Auto(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out documntNoGRN);
                } 
                #region check com item delivery add by tharanga 2018/march/03
                if (chkDelFrmAnyLoc.Checked == true)
                {
                    foreach (ReptPickSerials item in reptPickSerialsList)
                    {
                        if (item.Tus_unit_cost == 0)
                        {
                            DataTable odata = new DataTable();
                            var _prom_code = _invItmsnew.Where(r => r.Sad_itm_cd == item.Tus_itm_cd).Select(a => a.Sad_promo_cd);
                            foreach (var itemnew in _prom_code)
                            {
                                odata = CHNLSVC.Sales.GetInvoicecom_item_details(lblInvoiceNo.Text.ToUpper().Trim(), itemnew, null);
                            }
                            //DataTable odata = CHNLSVC.Sales.GetInvoicecom_item_details(lblInvoiceNo.Text.ToUpper().Trim(), _prom_code.ToString(), null);
                            if (odata != null && odata.Rows.Count > 0)
                            {
                                foreach (DataRow _dr2 in odata.Rows)
                                {
                                    if (BaseCls.GlbUserDefLoca != _dr2["ITB_LOC"].ToString())
                                    {
                                        MessageBox.Show(item.Tus_itm_cd + " item can't deliver. Main item already deliverd form " + _dr2["ITB_LOC"].ToString(), "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                        }

                    }

                }
                #endregion
                result = CHNLSVC.Inventory.DeliveryOrderEntry(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out  documntNoGRN, IsGrn, null);
                string Error = "";
                
                
                // add by tharanga 2017/08/23 send email to the pc and SMS to sales ex
                #region send email to the pc and SMS to sales ex
                if (chkDelFrmAnyLoc.Checked==true)
                {
                    if (result > 0)
                    {
                        string email = "";
                        string Item = "";
                        string model = "";
                        string qty = "";
                        DataTable pc = new DataTable();
                        DataTable maillist = new DataTable();
                        pc = CHNLSVC.CustService.get_profitcenter(invHdr.Ith_com, invHdr.Ith_pc);
                        maillist = CHNLSVC.CustService.get_msg_info_MAIL(invHdr.Ith_com, invHdr.Ith_pc, "INV"); //define mail and ph no
                        foreach (ReptPickSerials dr in reptPickSerialsListGRN)
                        {
                            Item += dr.Tus_itm_cd + " " + ",";
                            model += dr.Tus_itm_model + " " + ",";
                            qty += dr.Tus_itm_cd + " " + "-" + dr.Tus_qty + ",";
                        }

                        foreach (DataRow dr in maillist.Rows)
                        {
                             email = dr["MMI_EMAIL"].ToString();
                             if (IsValidEmail(email))
                             {
                                 string _mail = "";
                                 _mail += "Invoice ref. Number -" + invHdr.Ith_entry_no + Environment.NewLine;
                                 _mail += "Customer Code -" + txtCustCode.Text + " " + Environment.NewLine;
                                 _mail += "Customer Name -" + txtCustName.Text + " " + Environment.NewLine;
                                 _mail += "Delivery Name -" + invHdr.Ith_del_cust_name + " " + Environment.NewLine;
                                 _mail += "Deliver address 1 -" + invHdr.Ith_del_add1 + " " + Environment.NewLine;
                                 _mail += "Deliver address 2 -" + invHdr.Ith_del_add2 + " " + Environment.NewLine;

                                 _mail += "Delivery order number - " + documntNo + " " + Environment.NewLine;
                                 _mail += "Date -" + invHdr.Ith_doc_date.ToShortDateString() + " " + Environment.NewLine;
                                 _mail += "Item Code - " + Item + " " + Environment.NewLine;
                                 _mail += "Model - " + model + " " + Environment.NewLine;
                                 _mail += "Qty- " + qty + " " + Environment.NewLine;
                                 _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;
                                 CHNLSVC.CommonSearch.Send_SMTPMail(email, "Deliverd Invoice Details", _mail);
                             }
                        }
                        #region old code
                        //if (IsValidEmail(email))
                        //{

                        //    foreach (ReptPickSerials dr in reptPickSerialsListGRN)
                        //    {
                        //        Item += dr.Tus_itm_cd + " " + ","; 
                        //        model += dr.Tus_itm_model + " " + ","; 
                        //        qty += dr.Tus_itm_cd + " " + "-" + dr.Tus_qty+ ","; 
                        //    }

                        //    string _mail = "";
                        //    _mail += "Invoice ref. Number -" + invHdr.Ith_entry_no + Environment.NewLine;
                        //    _mail += "Customer Code -" + txtCustCode.Text + " " + Environment.NewLine;
                        //    _mail += "Customer Name -" + txtCustName.Text+ " " + Environment.NewLine;
                        //    _mail += "Delivery Name -" + invHdr.Ith_del_cust_name + " " + Environment.NewLine;
                        //    _mail += "Deliver address 1 -" + invHdr.Ith_del_add1 + " " + Environment.NewLine;
                        //    _mail += "Deliver address 2 -" + invHdr.Ith_del_add2 + " "  + Environment.NewLine ;

                        //    _mail += "Delivery order number - " + documntNo + " " + Environment.NewLine ;
                        //    _mail += "Date -" + invHdr.Ith_doc_date.ToShortDateString() + " " + Environment.NewLine;
                        //    _mail += "Item Code - " + Item + " " + Environment.NewLine ;
                        //    _mail += "Model - " + model + " " + Environment.NewLine ;
                        //    _mail += "Qty- " + qty + " " + Environment.NewLine;
                        //    _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;
                        //    CHNLSVC.CommonSearch.Send_SMTPMail(email, "Deliverd Invoice Details", _mail);
                        //}
                        #endregion
                        string mobilNo = null, msg = null;
                        List<SmsOutMember> _smsOutLst = new List<SmsOutMember>();
                        List<Sms_Ref_Log> _smsRefLog = new List<Sms_Ref_Log>();
                        DataTable _tbl = CHNLSVC.Sales.GetEmployee(invHdr.Ith_com,sales_ex_cd);
                        foreach (DataRow dr in _tbl.Rows)
                        {
                            mobilNo = dr["esep_mobi_no"].ToString();
                            if (mobilNo != null)
                            {
                                string ValidaMobileNumber;
                                ValidaMobileNumber = mobilNo;
                                if (IsValidMobileNo(mobilNo, out ValidaMobileNumber, "", ""))
                                {
                                    OutSMS smsout = new OutSMS();
                                    smsout.Msg = "Inv.No-" + invHdr.Ith_entry_no + " " + "DO.No - " + documntNo + " " + "DO Name-" + invHdr.Ith_del_cust_name + " " + "DO Mob-" + txt_d_cust_mob.Text.Trim().ToString();
                                    //smsout.Msg = "Inv.No";
                                    smsout.Receiver = dr["esep_name_initials"].ToString();
                                    smsout.Receiverphno = ValidaMobileNumber;
                                    smsout.Sender = BaseCls.GlbUserID;
                                    smsout.Senderphno = ValidaMobileNumber;
                                    //smsout.Seqno = _refno;
                                    smsout.Msgstatus = 1;
                                    smsout.Msgtype = "DO";
                                    smsout.Createtime = DateTime.Now;

                                    Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                                }
                            }
                        }

                       
                        #region old code
                        //foreach (DataRow dr in _tbl.Rows)
                        //{
                        //    mobilNo = dr["esep_mobi_no"].ToString();
                        //}

                        //if (mobilNo != null)
                        //{
                        //    string ValidaMobileNumber;
                        //    ValidaMobileNumber = mobilNo;
                        //    if (IsValidMobileNo(mobilNo, out ValidaMobileNumber, "", ""))
                        //    {
                        //        OutSMS smsout = new OutSMS();
                        //        smsout.Msg = "Inv.No-" + invHdr.Ith_entry_no + " " + "DO.No - " + documntNo + " " + "DO Name-" + invHdr.Ith_del_cust_name + " " + "DO Mob-" + txt_d_cust_mob.Text.Trim().ToString();
                        //        //smsout.Msg = "Inv.No";
                        //        smsout.Receiver = invHdr.Ith_pc;
                        //        smsout.Receiverphno = ValidaMobileNumber;
                        //        smsout.Sender = BaseCls.GlbUserID;
                        //        smsout.Senderphno = ValidaMobileNumber;
                        //        //smsout.Seqno = _refno;
                        //        smsout.Msgstatus = 1;
                        //        smsout.Msgtype = "DO";
                        //        smsout.Createtime = DateTime.Now;

                        //        Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                        //    }
                        //}
                        #endregion

                    }
                }
                #endregion
                if (result != -99 && result >= 0)
                {
                    // add by tharanga 2017/09/06
                    int a = CHNLSVC.Inventory.update_InventoryDoShedule(_InventoryDoSheduleList);
                    Cursor.Current = Cursors.Default;

                    //updated by akila 2018/01/15
                    if (documntNo == "GV_PAGE")
                    {
                        MessageBox.Show("Document details updated successfully", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Delivery Order Note Successfully Saved! Document No : " + documntNo + ".\n Do you want to print now?", "Process Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            BaseCls.GlbReportDoc = documntNo;
                            clsInventoryRep objInv = new clsInventoryRep();
                            clsSalesRep objSales = new clsSalesRep();
                            if (objSales.checkIsDirectPrintDO() == true && objSales.removeIsDirectPrint() == false && BaseCls.GlbDefChannel != "AUTO_DEL")
                            {
                                objInv.DoRecPrint_Direct();
                            }
                            else
                            {
                                ReportViewerInventory _views = new ReportViewerInventory();
                                BaseCls.GlbReportTp = "OUTWARD";
                                _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                                _views.GlbReportDoc = documntNo;
                                _views.Show();
                                _views = null;
                            }
                        }
                    }
                    
                    ClearScreen();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    if (documntNo.Contains("EMS.CHK_INLFREEQTY"))
                    {
                        MessageBox.Show("There is no free stock balance available." + "\n" + "Please check the stock balances.", "No Free Location Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (documntNo.Contains("EMS.CHK_INBFREEQTY"))
                    {
                        MessageBox.Show("There is no free stock balance available." + "\n" + "Please check the stock balances.", "No Free Batch Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please check the issues of " + documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            dtpFromDate.Focus();
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindCustomer;
                _CommonSearch.ShowDialog();
                txtFindCustomer.Select();
                CHNLSVC.CloseAllChannels();
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

        private void txtFindCustomer_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtFindCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Customer_Click(null, null);
        }

        private void txtFindCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Customer_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetInvoices.Focus();
        }

        private void txtFindCustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindCustomer.Text)) return;

                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtFindCustomer.Text, string.Empty, string.Empty, "C");
                CHNLSVC.CloseAllChannels();
                if (_masterBusinessCompany.Mbe_cd == null)
                {
                    MessageBox.Show("Please select the valid customer", "Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindCustomer.Text = "";
                    txtFindCustomer.Focus();
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

        private void DeliveryOrderCustomer_Shown(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
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

        private void dvPendingInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtFindInvoiceNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindInvoiceNo.Text)) return;
                InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtFindInvoiceNo.Text);
                if (_hdr == null)
                {
                    MessageBox.Show("Please select the valid invoice no", "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindInvoiceNo.Text = string.Empty;
                    txtFindInvoiceNo.Focus();
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

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindInvoiceNo;
                _CommonSearch.ShowDialog();
                txtFindInvoiceNo.Select();
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

        private void txtFindInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtFindInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetInvoices.Focus();
        }

        private void DeliveryOrderCustomer_Activated(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
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

        private void txtManualRefNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtManualRefNo.Text != "" && chkManualRef.Checked)
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_DO", Convert.ToInt32(txtManualRefNo.Text));
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "Manual Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtManualRefNo.Text = "";
                        txtManualRefNo.Focus();
                        return;
                    }
                }
                else
                {
                    if (chkManualRef.Checked == true)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "Manual Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtManualRefNo.Focus();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void SetManualRefNo()
        {
            try
            {
                if (chkManualRef.Checked == true)
                {
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_DO");
                    if (_NextNo != 0)
                        txtManualRefNo.Text = _NextNo.ToString();
                    else
                        txtManualRefNo.Text = string.Empty;
                }
                else
                {
                    txtManualRefNo.Text = string.Empty;
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

        private void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManualRef.Checked)
            {
                SetManualRefNo();
                txtManualRefNo.Enabled = false;
            }
            else
            {
                txtManualRefNo.Enabled = true;
                txtManualRefNo.Clear();
            }
        }

        private void dvDOSerials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvDOSerials.RowCount > 0)
            {
                if (btnSave.Enabled == true)
                {
                    int _rowCount = e.RowIndex;
                    if (_rowCount != -1)
                    {
                        if (dvDOSerials.Columns[e.ColumnIndex].Name == "ser_Remove")
                        {
                            #region Deleting Row

                            bool _isDelete = CHNLSVC.Inventory.Is_Serial_Can_Remove(BaseCls.GlbUserComCode, txtInvcNo.Text, "VEHI_REG", Convert.ToString(dvDOSerials.Rows[_rowCount].Cells["TUS_ITM_CD"].Value), Convert.ToString(dvDOSerials.Rows[_rowCount].Cells["TUS_SER_1"].Value));
                            if (_isDelete == true)
                            {
                                if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) OnRemoveFromSerialGrid(_rowCount);
                            }
                            else
                            {
                                MessageBox.Show("Can't remove this serial!", "Reserved Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            #endregion Deleting Row
                        }
                    }
                }
            }
        }

        #region Rooting for Serial Grid Events

        protected void OnRemoveFromSerialGrid(int _rowIndex)
        {
            try
            {
                int row_id = _rowIndex;
                if (string.IsNullOrEmpty(dvDOSerials.Rows[row_id].Cells["TUS_ITM_CD"].Value.ToString())) return;
                Cursor.Current = Cursors.WaitCursor;
                string _item = Convert.ToString(dvDOSerials.Rows[row_id].Cells["TUS_ITM_CD"].Value);
                string _status = Convert.ToString(dvDOSerials.Rows[row_id].Cells["TUS_ITM_STUS"].Value);
                Int32 _serialID = Convert.ToInt32(dvDOSerials.Rows[row_id].Cells["TUS_SER_ID"].Value);
                string _bin = Convert.ToString(dvDOSerials.Rows[row_id].Cells["TUS_BIN"].Value);
                string serial_1 = Convert.ToString(dvDOSerials.Rows[row_id].Cells["TUS_SER_1"].Value);

                Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", BaseCls.GlbUserComCode, lblInvoiceNo.Text, 0);

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    //modify Rukshan 05/oct/2015 add two parameters
                    CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID), null, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, _item, _status);
                }

                LoadInvoiceItems(lblInvoiceNo.Text, _profitCenter);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Rooting for Serial Grid Events

        private void dvPendingInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvPendingInvoices.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //MessageBox.Show(dvPendingInvoices.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    //MessageBox.Show(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString());

                    //lblInvoiceNo.Text = "Invoice No : " + dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString();
                    lblInvoiceNo.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString();
                    txtInvcNo.Text = lblInvoiceNo.Text;
                    lblInvoiceDate.Text = Convert.ToDateTime(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_DT"].Value.ToString()).Date.ToString("dd/MMM/yyyy");
                    txtInvcDate.Text = lblInvoiceDate.Text;
                    txtCustCode.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_CD"].Value.ToString();
                    txtCustName.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_NAME"].Value.ToString();
                    txtCustAddress1.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_ADD1"].Value.ToString();
                    txtCustAddress2.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_ADD2"].Value.ToString();
                    _profitCenter = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_PC"].Value.ToString();
                    _invoiceType = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_TP"].Value.ToString();
                    _accNo = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_ACC_NO"].Value.ToString();
                    // add by tharanga 2017/08/22
                    txt_d_cust_cd.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_cd"].Value.ToString();
                    txt_d_cust_name.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_name"].Value.ToString();
                    txt_d_cust_add1.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_add1"].Value.ToString();
                    txt_d_cust_add2.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_add2"].Value.ToString();
                    sales_ex_cd = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_sales_ex_cd"].Value.ToString();
                   // Load_cust_details(txt_d_cust_cd.Text.Trim().ToString());
                    //
                    if (txtCustCode.Text == "AHDR2B0002")
                    { btnBOC.Visible = true; }
                    else
                    { btnBOC.Visible = false; }
                    
                        LoadInvoiceItems(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString(), _profitCenter);
                    //add by tharanga 2017/09/04
                        DataTable shedulitm = CHNLSVC.Sales.getinvshed_item(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString());
                        if (shedulitm.Rows.Count > 0)
                        {
                            pnlSheItm.Visible = true;
                            grdshedItm.Visible = true;
                            grdshedItm.AutoGenerateColumns = false;
                            grdshedItm.DataSource = shedulitm;
                        }
                        else
                        { pnlSheItm.Visible = false; }

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

        private void btnSearch_DocumentNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null, dtpDODate.Value.Date.AddMonths(-1), dtpDODate.Value.Date);
                _CommonSearch.dtpFrom.Value = dtpDODate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = dtpDODate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocumentNo;
                _CommonSearch.ShowDialog();
                txtDocumentNo.Select();
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

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    bool _invalidDoc = true;

                    #region Clear Data

                    Cursor.Current = Cursors.Default;
                    dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                    dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                    dtpDODate.Value = CHNLSVC.Security.GetServerDateTime().Date;

                    DataTable dt = null;
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;

                    chkManualRef.Checked = false;
                    chkEditAddress.Checked = false;
                    txtManualRefNo.Clear();

                    lblBackDateInfor.Text = string.Empty;
                    lblInvoiceNo.Text = string.Empty;
                    lblInvoiceDate.Text = string.Empty;

                    txtCustAddress1.Text = string.Empty;
                    txtCustAddress2.Text = string.Empty;
                    txtCustCode.Text = string.Empty;
                    txtCustName.Text = string.Empty;
                    txtFindCustomer.Text = string.Empty;
                    txtFindInvoiceNo.Text = string.Empty;
                    txtVehicleNo.Text = string.Empty;
                    txtRemarks.Text = string.Empty;

                    List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                    _emptyserList = null;
                    dvDOSerials.AutoGenerateColumns = false;
                    dvDOSerials.DataSource = _emptyserList;

                    List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                    _emptyinvoiceitemList = null;
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = _emptyinvoiceitemList;

                    #endregion Clear Data

                    InventoryHeader _invHdr = new InventoryHeader();
                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

                    #region Check Valid Document No

                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "DO")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        CHNLSVC.CloseAllChannels();
                        MessageBox.Show("Invalid Document No!", "Delivery Order No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Clear();
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        btnGetInvoices.Enabled = false;
                        btnSave.Enabled = false;
                        dtpDODate.Value = _invHdr.Ith_doc_date.Date;
                        lblInvoiceNo.Text = _invHdr.Ith_oth_docno;
                        txtInvcNo.Text = lblInvoiceNo.Text;
                        txtCustCode.Text = _invHdr.Ith_bus_entity;
                        txtCustAddress1.Text = _invHdr.Ith_del_add1;
                        txtCustAddress2.Text = _invHdr.Ith_del_add2;
                        txtManualRefNo.Text = _invHdr.Ith_manual_ref;
                        txtRemarks.Text = _invHdr.Ith_remarks;

                        InvoiceHeader _saleHdr = new InvoiceHeader();
                        _saleHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invHdr.Ith_oth_docno);
                        txtCustName.Text = _saleHdr.Sah_cus_name;
                        lblInvoiceDate.Text = _saleHdr.Sah_dt.Date.ToString("dd/MMM/yyyy");
                        txtInvcDate.Text = lblInvoiceDate.Text;
                    }

                    #endregion Check Valid Document No

                    #region Get Serials

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    invoice_items = CHNLSVC.Sales.GetAllSaleDocumentItemList(BaseCls.GlbUserComCode, _invHdr.Ith_pc, "INV", _invHdr.Ith_oth_docno, string.Empty);
                    if (invoice_items != null)
                    {
                        if (_serList != null)
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (InvoiceItem _invItem in invoice_items)
                                    if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                    {
                                        //it.Sad_do_qty = q.theCount;
                                        //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                        _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                    }
                            }
                        }
                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = invoice_items;
                        dvDOSerials.AutoGenerateColumns = false;
                        dvDOSerials.DataSource = _serList;
                    }
                    else
                    {
                        CHNLSVC.CloseAllChannels();
                        MessageBox.Show("Item not found!", "Delivery Order No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Clear();
                        txtDocumentNo.Focus();
                        return;
                    }

                    #endregion Get Serials
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

        private void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_DocumentNo_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCustCode.Focus();
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(false, sender, e);
        }

        private void btnAddGv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPage.Text))
                {
                    MessageBox.Show("Please select the page no", "Page No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPage.Clear();
                    txtPage.Focus();
                    return;
                }

                DataTable _voucher = CHNLSVC.Sales.GetInvoiceVoucher(lblInvoiceNo.Text.Trim(), lblGvItem.Text.Trim());
                if (_voucher != null)
                    if (_voucher.Rows.Count > 0)
                    {
                        var _isExist = _voucher.AsEnumerable().Where(x => (x.Field<Int16>("stvo_stus") == 1 || x.Field<Int16>("stvo_stus") == 2) && x.Field<Int64>("stvo_pageno") == Convert.ToInt64(txtPage.Text.Trim())).ToList();
                        if (_isExist == null || _isExist.Count <= 0)
                        {
                            CHNLSVC.CloseAllChannels();
                            MessageBox.Show("There is no such gift voucher available for the selected item.", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (_isExist != null)
                            if (_isExist.Count > 0)
                            {
                                var _alreadySelect = from DataGridViewRow _row in gvGiftVoucher.Rows
                                                     where Convert.ToString(_row.Cells["gv_item"].Value) == lblGvItem.Text.Trim() && Convert.ToInt64(_row.Cells["gv_page"].Value) == Convert.ToInt64(txtPage.Text)
                                                     select _row;

                                if (_alreadySelect == null || _alreadySelect.Count() <= 0)
                                {
                                    string _item = lblGvItem.Text.Trim();
                                    string _status = lblGvStatus.Text.Trim();
                                    int _book = Convert.ToInt32(_isExist[0].Field<Int64>("stvo_bookno"));
                                    int _page = Convert.ToInt32(txtPage.Text.Trim());
                                    string _attachedItem = Convert.ToString(_isExist[0].Field<string>("stvo_itm_cd"));

                                    var _itmExist = invoice_items.Where(x => (x.Sad_itm_cd == _attachedItem || x.Sad_sim_itm_cd == _attachedItem) && (x.Sad_qty - x.Sad_do_qty) > 0).ToList();
                                    if (_itmExist == null || _itmExist.Count <= 0)
                                    {
                                        CHNLSVC.CloseAllChannels();
                                        MessageBox.Show("This gift voucher attached item is " + _attachedItem + " and this item not available in current invoice");
                                        txtPage.Clear();
                                        return;
                                    }

                                    object[] _obj = new object[7];

                                    _obj.SetValue(_item, 1);
                                    _obj.SetValue(_status, 2);
                                    _obj.SetValue(_book, 3);
                                    _obj.SetValue(_page, 4);
                                    _obj.SetValue(_attachedItem, 5);
                                    _obj.SetValue(lblInvLine.Text.Trim(), 6);

                                    gvGiftVoucher.AllowUserToAddRows = true;
                                    gvGiftVoucher.Rows.Insert(gvGiftVoucher.NewRowIndex, _obj);
                                    gvGiftVoucher.AllowUserToAddRows = false;

                                    txtPage.Clear();
                                    txtPage.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("You have already selected this no", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtPage.Clear();
                                    txtPage.Focus();
                                    return;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void pnlDiscountReqClose_Click(object sender, EventArgs e)
        {
            pnlGv.Visible = false;
            gvGiftVoucher.Rows.Clear();
        }

        private void btnConfirmGv_Click(object sender, EventArgs e)
        {
            if (gvGiftVoucher.Rows.Count <= 0)
            {
                MessageBox.Show("Please select the gift voucher(s).", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataGridViewRow _row in gvGiftVoucher.Rows)
            {
                string _item = Convert.ToString(_row.Cells["gv_item"].Value);
                string _status = Convert.ToString(_row.Cells["gv_status"].Value);
                int _book = Convert.ToInt32(Convert.ToString(_row.Cells["gv_book"].Value));
                int _page = Convert.ToInt32(Convert.ToString(_row.Cells["gv_page"].Value));
                string _attachedItem = Convert.ToString(Convert.ToString(_row.Cells["gv_attacheditem"].Value));
                string _invline = Convert.ToString(Convert.ToString(_row.Cells["gv_invline"].Value));

                object[] _obj = new object[7];
                _obj.SetValue(_item, 1);
                _obj.SetValue(_status, 2);
                _obj.SetValue(_book, 3);
                _obj.SetValue(_page, 4);
                _obj.SetValue(_attachedItem, 5);
                _obj.SetValue(_invline, 6);

                gvSGv.AllowUserToAddRows = true;
                gvSGv.Rows.Insert(gvSGv.NewRowIndex, _obj);
                gvSGv.AllowUserToAddRows = false;
            }
            gvGiftVoucher.Rows.Clear();
            pnlGv.Visible = false;
            LoadInvoiceItems(lblInvoiceNo.Text.Trim(), _profitCenter);
        }

        private void gvGiftVoucher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvGiftVoucher.Rows.Count > 0) if (e.RowIndex != -1) if (e.ColumnIndex == 0) if (MessageBox.Show("Do you need to remove this validated voucher?", "Validated Voucher", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            gvGiftVoucher.Rows.RemoveAt(e.RowIndex);
                        }
        }

        private void gvSGv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvSGv.Rows.Count > 0) if (e.RowIndex != -1) if (e.ColumnIndex == 0) if (MessageBox.Show("Do you need to remove this validated voucher?", "Validate Voucher", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            gvSGv.Rows.RemoveAt(e.RowIndex);
                            LoadInvoiceItems(lblInvoiceNo.Text.Trim(), _profitCenter);
                        }
        }

        private void txtPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddGv.Focus();
        }

        #region Upload SCM Invoice - 27-07-2013 Chamal

        private void btnUploadInvoice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnSave.Enabled == true)
            {
                if (pnlDealerInvoice.Visible == false)
                {
                    pnlDealerInvoice.Visible = true;
                    txtSCMInvcNo.Focus();
                }
                else
                {
                    pnlDealerInvoice.Visible = false;
                    txtSCMInvcNo.Clear();
                    txtSCMInvcNo.Clear();
                    txtSCMInvcDate.Clear();
                    txtSCMCustCode.Clear();
                    txtSCMCustName.Clear();
                }
            }
        }

        private void txtSCMInvcNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSCMInvcNo.Text)) return;

                DateTime _bondDate = DateTime.Now.Date;
                txtSCMInvcDate.Clear();
                txtSCMCustCode.Clear();
                txtSCMCustName.Clear();

                DataTable _dt = CHNLSVC.Sales.GetSalesHdr(txtSCMInvcNo.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    MessageBox.Show("Already Uploaded", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSCMInvcNo.Clear();
                    txtSCMInvcDate.Clear();
                    txtSCMCustCode.Clear();
                    txtSCMCustName.Clear();
                    txtSCMInvcNo.Focus();
                    return;
                }

                DataTable _dt1 = CHNLSVC.Sales.GetSCMInvc(txtSCMInvcNo.Text);
                if (_dt1 != null && _dt1.Rows.Count > 0)
                {
                    foreach (DataRow _dr1 in _dt1.Rows)
                    {
                        txtSCMCustCode.Text = _dr1["CUSTOMER_CODE"].ToString();

                        DataTable _dt2 = CHNLSVC.Sales.GetCustomerDetails(BaseCls.GlbUserComCode, txtSCMCustCode.Text);
                        if (_dt2 != null && _dt2.Rows.Count > 0)
                        {
                            foreach (DataRow _dr2 in _dt2.Rows)
                            {
                                txtSCMCustName.Text = _dr2["COMPANY_NAME"].ToString();
                                break;
                            }
                        }
                        DateTime _date = Convert.ToDateTime(_dr1["INVOICE_DATE"].ToString()).Date;
                        txtSCMInvcDate.Text = _date.ToString("dd/MMM/yyyy");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Invoice No", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSCMInvcNo.Clear();
                    txtSCMInvcNo.Clear();
                    txtSCMInvcDate.Clear();
                    txtSCMCustCode.Clear();
                    txtSCMCustName.Clear();
                    txtSCMInvcNo.Focus();
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
                GC.Collect();
            }
        }

        private void btnUploadSCMInvc_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(txtSCMInvcNo.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    int _uploaded = CHNLSVC.Sales.UploadSCMInvoice(txtSCMInvcNo.Text, BaseCls.GlbUserID, out _msg);
                    if (_uploaded == 1)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Invoice no - " + txtSCMInvcNo.Text + " uploaded!", "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFindInvoiceNo.Text = txtSCMInvcNo.Text;
                        txtSCMInvcNo.Clear();
                        txtSCMInvcDate.Clear();
                        txtSCMCustCode.Clear();
                        txtSCMCustName.Clear();
                        txtFindInvoiceNo.Focus();
                        txtFindInvoiceNo.SelectAll();
                        pnlDealerInvoice.Visible = false;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_msg, "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                GC.Collect();
            }
        }

        private void txtSCMInvcNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnUploadInvoice.Focus();
        }

        private void btnPnlImportsPOClose_Click(object sender, EventArgs e)
        {
            pnlDealerInvoice.Visible = false;
            txtSCMInvcNo.Clear();
            txtSCMInvcDate.Clear();
            txtSCMCustCode.Clear();
            txtSCMCustName.Clear();
            txtSCMInvcNo.Focus();
        }

        #endregion Upload SCM Invoice - 27-07-2013 Chamal

        private void chkEditAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAddress.Checked)
            {
                txt_d_cust_cd.Text = "";
                txt_d_cust_name.Text = "";
                txt_d_cust_nic.Text = "";
                txt_d_cust_mob.Text = "";
                txt_d_cust_add1.Text = "";
                txt_d_cust_add2.Text = "";
                txt_d_cust_cd.ReadOnly = false;
                txt_d_cust_name.ReadOnly = false;
                txt_d_cust_nic.ReadOnly = false;
                txt_d_cust_mob.ReadOnly = false;
                txt_d_cust_add1.ReadOnly = false;
                txt_d_cust_add2.ReadOnly = false;
                txt_d_cust_cd.Enabled = true;
                txt_d_cust_cd.Focus();
                txt_d_cust_add1.SelectAll();
               

            }
            else
            {
                txt_d_cust_add1.ReadOnly = true;
                txt_d_cust_add2.ReadOnly = true;
                txt_d_cust_cd.ReadOnly = true;
            }
        }

        #region BOC Project :: Code by Chamal 20-Aug-2014

        private void btnBOC_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvcNo.Text))
            {
                if (pnlBOCSerials.Visible == true)
                {
                    pnlBOCSerials.Visible = false;
                }
                else
                {
                    pnlBOCSerials.Visible = true;
                    LoadBOCScanBatches();
                }
            }
        }

        private void btnCloseBOCUploadPannel_Click(object sender, EventArgs e)
        {
            pnlBOCSerials.Visible = false;
        }

        private void btnUploadBOCSerails_Click(object sender, EventArgs e)
        {
            GetBOCSerials();
        }

        private void LoadBOCScanBatches()
        {
            dvBOCScanBatch.DataSource = CHNLSVC.Inventory.GetBOCScanSummary(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
        }

        private void GetBOCSerials()
        {
            if (dvBOCScanBatch.Rows.Count <= 0) return;

            Cursor.Current = Cursors.WaitCursor;

            bool _selectBatch = false;
            List<string> _listBatchNo = new List<string>();
            for (int i = 0; i < dvBOCScanBatch.RowCount; i++)
            {
                if (dvBOCScanBatch.Rows[i].Cells["pnlBOCSelect"].Value.ToString().ToUpper() == "TRUE")
                {
                    _selectBatch = true;
                    _listBatchNo.Add(dvBOCScanBatch.Rows[i].Cells["BATCH_NO"].Value.ToString().ToUpper());
                }
            }

            if (_selectBatch == false)
            {
                MessageBox.Show("Please select the batch(s)", "Scanning Batch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_listBatchNo.Count <= 0)
            {
                MessageBox.Show("Please select the batch(s)", "Scanning Batch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string _msg = string.Empty;
            int _err = CHNLSVC.Inventory.GetBOCSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, txtInvcNo.Text, _listBatchNo, BaseCls.GlbUserID, out _msg);
            if (_err == -99)
            {
                MessageBox.Show(_msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                LoadInvoiceItems(txtInvcNo.Text, _profitCenter);
                MessageBox.Show("Serials uploaded!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            pnlBOCSerials.Visible = false;
        }

        #endregion BOC Project :: Code by Chamal 20-Aug-2014

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblInvoiceNo.Text))
            {
                MessageBox.Show("Select the invoice no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dvDOSerials.Rows.Count > 0)
            {
                MessageBox.Show("Already pick serials!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            openFileDialog1.Title = "Browse Excel Files";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            lblExcelPath.Text = openFileDialog1.FileName;
        }

        private void btnUploadSerials_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblInvoiceNo.Text))
                {
                    MessageBox.Show("Select the invoice no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 row_aff = 0;
                string _msg = string.Empty;
                if (string.IsNullOrEmpty(lblExcelPath.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Upload Serial(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(lblExcelPath.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Upload Serial(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension == ".xls")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"].ConnectionString;
                }
                else if (Extension == ".xlsx")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"].ConnectionString;

                }
                else
                {
                    MessageBox.Show("Invalid File Extension.", "Upload Serial(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                Cursor.Current = Cursors.WaitCursor;

                conStr = String.Format(conStr, lblExcelPath.Text, "NO");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
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
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count <= 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No data found!", "Upload Serial(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<ReptPickItems> _itemList = new List<ReptPickItems>();

                //add to list
                foreach (DataRow _row in dt.Rows)
                {
                    ReptPickItems _itemOne = new ReptPickItems();
                    _itemOne.Tui_req_itm_cd = _row[0].ToString();//Item Code
                    _itemOne.Tui_pic_itm_cd = _row[1].ToString();//Serial No

                    if (string.IsNullOrEmpty(_itemOne.Tui_req_itm_cd) || string.IsNullOrEmpty(_itemOne.Tui_pic_itm_cd))
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Invalid file contents found", "Upload Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    _itemList.Add(_itemOne);
                }

                conStr = "";
                var _result = _itemList.GroupBy(x => new { x.Tui_req_itm_cd, x.Tui_pic_itm_cd }).Select(g => new { g.Key.Tui_req_itm_cd, g.Key.Tui_pic_itm_cd, qty = g.Count() }).Where(a => a.qty > 1).ToList();
                foreach (var prodCount in _result)
                {
                    conStr = conStr + "Item code : " + prodCount.Tui_req_itm_cd + " | serial no : " + prodCount.Tui_pic_itm_cd + "\n";
                }

                if (!string.IsNullOrEmpty(conStr))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Duplicate contents found!\n" + conStr, "Upload Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int _status = CHNLSVC.Inventory.GetExcelSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, lblInvoiceNo.Text, _itemList, BaseCls.GlbUserID, out conStr);
                if (_status == -99)
                {
                    MessageBox.Show(conStr, "Upload Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                LoadInvoiceItems(lblInvoiceNo.Text, _profitCenter);
                Cursor.Current = Cursors.Default;
                btnUploadSerials.Enabled = false;
                MessageBox.Show("Upload Successfully!", "Uploaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_d_cust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txt_d_cust_cd;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013 
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txt_d_cust_cd.Select();
            }
            catch (Exception ex) { txt_d_cust_cd.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void Load_cust_details(string custcode)
    {
        if (!string.IsNullOrEmpty(custcode))
        {
            //btnCreate.Enabled = true;
            MasterBusinessEntity custProf = GetbyCustCD(custcode.Trim().ToUpper());
            if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
            {
                _isExsit = true;
                LoadCustProf(custProf);
            }
            else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
            {
                MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                LoadCustProf(custProf);
            }
            else
            {
                if (_isExsit == true)
                {
                    string cusCD = txt_d_cust_cd.Text.Trim().ToUpper();
                    MasterBusinessEntity cust_null = new MasterBusinessEntity();
                    LoadCustProf(cust_null);
                    txt_d_cust_cd.Text = cusCD;
                }
                //Check the group level
                

                
                if (custProf.Mbe_cd == null)
                {
                    MessageBox.Show("Invalid customer code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_d_cust_cd.Text = "";
                }
            }
          
        }
    
    
    }


        public MasterBusinessEntity GetbyCustCD(string custCD)
    {
        return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, BaseCls.GlbUserComCode);
    }


        public void LoadCustProf(MasterBusinessEntity cust)
    {
      
        txt_d_cust_cd.Text = cust.Mbe_cd;
        txt_d_cust_name.Text = cust.Mbe_name;
        txt_d_cust_nic.Text = cust.Mbe_nic;
        txt_d_cust_mob.Text = cust.Mbe_mob;
        txt_d_cust_add1.Text = cust.Mbe_add1;
        txt_d_cust_add2.Text = cust.Mbe_add2;

        txt_d_cust_name.ReadOnly = true;
        txt_d_cust_nic.ReadOnly = true;
        txt_d_cust_mob.ReadOnly = true;

        txt_d_cust_cd.Enabled = false;
    

    }

        private void txt_d_cust_cd_Leave(object sender, EventArgs e)
    {
        Load_cust_details(txt_d_cust_cd.Text.Trim().ToString());
    }

        private void linknewcust_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            this.Cursor = Cursors.WaitCursor;
            General.CustomerCreation _CusCre = new General.CustomerCreation();
            _CusCre._isFromOther = true;
            _CusCre.obj_TragetTextBox = txt_d_cust_cd;
            this.Cursor = Cursors.Default;
            _CusCre.ShowDialog();
            txt_d_cust_cd.Select();
         //   if (chkDeliverLater.Checked) txtItem.Focus(); else txtSerialNo.Focus();
        }
        catch (Exception ex)
        { txt_d_cust_cd.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
    }
        private void tabPage5_Click(object sender, EventArgs e)
    {

    }
        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, BaseCls.GlbUserComCode);
        }
        private void txt_d_cust_nic_Leave(object sender, EventArgs e) //add by tharanga 2017/08/23
        {
            if (!string.IsNullOrEmpty(txt_d_cust_nic.Text))
            {
                _isExsit = false;
                Boolean _isValid = IsValidNIC(txt_d_cust_nic.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Please enter a valid NIC", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_d_cust_nic.Text = "";
                    txt_d_cust_nic.Focus();
                    return;
                }
                else
                {
                    //check multiple Add By Chamal 24/04/2014
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txt_d_cust_nic.Text.Trim(), "", "", "", "", 1);
                    if (_custList != null && _custList.Count > 1 && txt_d_cust_nic.Text.ToUpper() != "N/A")
                    {
                        string _custNIC = "Duplicate customers found!\n";
                        foreach (var _nicCust in _custList)
                        {
                            _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                        }
                        // _custNIC = _custNIC + "\nPlease contact accounts department";
                        MessageBox.Show(_custNIC, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_d_cust_nic.Text = "";
                        txt_d_cust_nic.Focus();
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyNIC(txt_d_cust_nic.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null )
                    {
                    
                        LoadCustProf(custProf);
                        txt_d_cust_nic.ReadOnly = true;
                        txt_d_cust_mob.ReadOnly = true;
                        Int32 _attemt = 0;
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
                        LoadCustProf(custProf);
                    }
                    else//added on 01/10/2012
                    {
                        if (_isExsit == false)
                        {

                            MessageBox.Show("Customer Details Not Found.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt_d_cust_nic.Text = "";
                            txt_d_cust_nic.Focus();
                            return;
                        }
                    
                    
                    }
               
                }
            }
        }

        private void txt_d_cust_mob_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_d_cust_mob.Text))
            {
          
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                Boolean _isValid = IsValidMobileOrLandNo(txt_d_cust_mob.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_d_cust_mob.Text = "";
                    txt_d_cust_mob.Focus();
                    return;
                }

                string _current_cust = string.Empty;
                Int32 _cnt = 0;
                //List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMob.Text, "C");
                List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txt_d_cust_mob.Text, "C");
                if (_fk != null && _fk.Count > 0)
                {
                    _cnt = _fk.Count;
                    _current_cust = _fk[0].Mbe_cd;
                    //if (_fk.Count > 1)
                    //{
                    //    MessageBox.Show("There are " + _fk.Count + " number of customers available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    txtMob.Text = "";
                    //    txtMob.Focus();
                    //    return;
                    //}
                }
                Int32 _attemt = 0;
                if (_current_cust != txt_d_cust_mob.Text || _cnt > 1)
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txt_d_cust_mob.Text, "C");
                    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                    {
                        _isExsit = true;

                        #region Mobileno
                        if (_isExsit == true)
                        {

                            _isExsit = true;
                            LoadCustProf(_masterBusinessCompany);



                        }
                        #endregion


                        else if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == false)
                        {
                            MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LoadCustProf(_masterBusinessCompany);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Customer Details Not Found.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_d_cust_mob.Text = "";
                        txt_d_cust_mob.Focus();
                        return;
                    }
                }
        
                else
                {
              
                }


            }
        }

        private bool IsValidMobileNo(string _mobile, out string MobileNo, string jobNo, string customerName)
        {
            int _cnvMobileNo = 0;
            string _mobileNo = _mobile;
            //if (_mobile.Contains("+94"))
            //{
            //    _mobileNo =  _mobile.Replace("+94", "0");
            //}
            if (int.TryParse(_mobileNo, out _cnvMobileNo) | _mobileNo.Substring(0, 1).ToString() == "+")
            {
                string lterOne = _mobileNo.Substring(0, 1).ToString();
                switch (lterOne)
                {
                    case "0":
                        if (_mobileNo.Length == 10)
                        {
                            string lterTwo = _mobileNo.Substring(0, 2).ToString();
                            if (_mobileNo.Substring(0, 2).ToString() == "07")
                            {
                                _mobileNo = _mobileNo.Substring(0, 1).Replace("0", "+94") + _mobileNo.Substring(1, 9);
                                MobileNo = _mobileNo;
                                return true;
                            }
                        }
                        break;
                    case "7":
                        if (_mobileNo.Length == 9)
                        {
                            MobileNo = "+94" + _mobileNo;
                            return true;
                        }
                        break;
                    case "+":
                        if (_mobileNo.Length == 12)
                        {
                            MobileNo = _mobileNo;
                            return true;
                        }
                        if (_mobileNo.Length == 13)
                        {
                            MobileNo = _mobileNo;
                            return true;
                        }
                        break;

                    default:
                        MobileNo = null;
                        return false;
                }
            }

            MobileNo = null;
            return false;
        }

        private void btnsheitmclose_Click(object sender, EventArgs e)
        {
            pnlSheItm.Visible = false;
        }

        private void grdshedItm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdshedItm.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    txt_d_cust_cd.Text = grdshedItm.Rows[e.RowIndex].Cells["sid_d_cust_cd"].Value.ToString();
                    txt_d_cust_name.Text = grdshedItm.Rows[e.RowIndex].Cells["sid_d_cust_name"].Value.ToString();
                    txt_d_cust_add1.Text = grdshedItm.Rows[e.RowIndex].Cells["sid_d_cust_add1"].Value.ToString();
                    txt_d_cust_add2.Text = grdshedItm.Rows[e.RowIndex].Cells["sid_d_cust_add2"].Value.ToString();
            
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

        private bool IsGvAvailableAsSimilerItm()
        {
            bool _isAvailable = false;

            try
            {
                if (dvDOItems.Rows.Count > 0)
                {
                    if (invoice_items != null && invoice_items.Count > 0)
                    {
                        var _similerItmList = invoice_items.Where(x => (!string.IsNullOrEmpty(x.Sad_sim_itm_cd))).ToList();
                        if (_similerItmList != null && _similerItmList.Count > 0)
                        {
                            foreach (var _similerItem in _similerItmList)
                            {
                                MasterItem _itemMaster = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _similerItem.Sad_sim_itm_cd);
                                if (_itemMaster != null && _itemMaster.Mi_itm_tp == "G")
                                {
                                    _isAvailable = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                _isAvailable = false;
                MessageBox.Show("An error occurred wile validating inventory details", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _isAvailable;
        }
  
    }
}