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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FF.BusinessObjects; 
using FF.WindowsERPClient.Reports.Sales;
 

namespace FF.WindowsERPClient.Inventory
{
    public partial class DeliveryOrderQuotation : Base
    {
        private List<QoutationDetails> invoice_items = null;
        private List<QoutationDetails> invoice_items_bind = null;
        private List<RecieptItem> _recieptItem = null;
        private DataTable MasterChannel = null;
        private string _profitCenter = "";
        private string _accNo = "";
        private string _invoiceType = "";
        private bool IsGrn = false;
        private MasterProfitCenter _MasterProfitCenter = null;
        private List<InvoiceItem> _invoiceItemList = null;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private decimal GrndSubTotal = 0; private decimal GrndDiscount = 0; private decimal GrndTax = 0;
        private Int32 _lineNo = 0;
        private Int32 SSCombineLine = 0;
        private LoyaltyType _loyaltyType;
        private bool _serialMatch = true;
        private bool _isNeedRegistrationReciept = false;
        private List<ReptPickSerials> BuyBackItemList = null;
        private List<ReptPickSerials> ScanSerialList = null;
        private List<InvoiceSerial> InvoiceSerialList = null;
        private List<InvoiceItem> _invoiceItemListWithDiscount = null;
        private int _discountSequence;
        private MasterBusinessEntity _businessEntity = null;
        private MasterBusinessEntity _masterBusinessCompany = null;
        private DateTime _serverDt = DateTime.Now.Date;
        private PriceBookLevelRef _priceBookLevelRef = null;
        private bool IsSaleFigureRoundUp = false;
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private string DefaultInvoiceType = string.Empty;
        private bool _isInventoryCombineAdded = false; private Int32 ScanSequanceNo = 0;   private bool IsPriceLevelAllowDoAnyStatus = false; private string WarrantyRemarks = string.Empty; private Int32 WarrantyPeriod = 0; private string ScanSerialNo = string.Empty; private string DefaultItemStatus = string.Empty;
        private List<MasterItemTax> MainTaxConstant = null; private List<ReptPickSerials> _promotionSerial = null; private List<ReptPickSerials> _promotionSerialTemp = null;
        private Dictionary<decimal, decimal> ManagerDiscount = null; private CashGeneralEntiryDiscountDef GeneralDiscount = null; private string DefaultBook = string.Empty; private string DefaultLevel = string.Empty;   private string DefaultStatus = string.Empty; private string DefaultBin = string.Empty; private MasterItem _itemdetail = null;
        private bool valid;
        #region Clear Screen

        private void ClearScreen()
        {
            try
            {
                btnSave.Enabled = true;
                Cursor.Current = Cursors.Default;
                if (BaseCls.GlbIsManChkLoc == true) txtManualRefNo.ReadOnly = true;
                _accNo = string.Empty;
                pnlDealerInvoice.Visible = false;
                txtSCMInvcNo.Clear();
                txtSCMInvcNo.Clear();
                txtSCMInvcDate.Clear();
                txtSCMCustCode.Clear();
                txtSCMCustName.Clear();
                txtInvcNo.Text = "";
                txtInvcDate.Text = "";
                txtDelAdd1.Text = "";
                txtDelAdd2.Text = "";
                txtDelCusCd.Text = "";
                txtDelCusName.Text = "";
                btnUploadSerials.Enabled = true;

                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                dtpDODate.Value = CHNLSVC.Security.GetServerDateTime().Date;


                dtpInvFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                dtpInvToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;

                DataTable dt = CHNLSVC.Sales.GetPendingInvoicesToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefLoca, txtFindCustomer.Text, txtFindInvoiceNo.Text, chkDelFrmAnyLoc.Checked ? 1 : 0);
                if (dt.Rows.Count > 0)
                {
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                }
                else
                {
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = null;
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
                LoadPayMode();
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
               // btnGetInvoices_Click(null, null);
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
                case CommonUIDefiniton.SearchUserControlType.CustomerQuo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.MovementQuoDateSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "DO" + seperator + "QUO" + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerQuoInv:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Do_qua_serch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "DO" + seperator + "QUO" + seperator + "0" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area

        public DeliveryOrderQuotation()
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
            VariableInitialization();
            LoadCachedObjects();
            LoadExecutive();
            LoadPriceDefaultValue();
            LoadPayMode();
            int _eff = 0;
            get_invoices(out _eff);
          
        }

        private void btnGetInvoices_Click(object sender, EventArgs e)
        {
            int _eff=0;
          get_invoices(out _eff);
           if (_eff== 0)
           { MessageBox.Show("No pending invoices found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }
        public int   get_invoices(out int _eff)
        {
            _eff = 0;
            try
            {
                
                DataTable dt = CHNLSVC.Sales.GetPendingQuotationToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, txtFindCustomer.Text, txtFindInvoiceNo.Text, "A", BaseCls.GlbUserDefProf);
                if (dt.Rows.Count > 0)
                {
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                    _eff = 1;
                }
                else
                {
                    dt = null;
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                   // MessageBox.Show("No pending invoices found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
             
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _eff;
        }

        #region Generate new user seq no

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "QUO", 1, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "QUO";
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
            invoice_items_bind = new List<QoutationDetails>();
            //Get Invoice Items Details
            invoice_items = CHNLSVC.Sales.GetAllQuotationItemList(_invNo);
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

                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", BaseCls.GlbUserComCode, _invNo, 0);
                    if (user_seq_num == -1)
                    {
                        //Generate new user seq no and add new row to pick_hdr
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                   // _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "QUO");
                    if (_serList != null && _serList.Count > 0)
                    {
                        foreach (QoutationDetails _item in invoice_items)
                        {
                            _serList.Where(x => x.Tus_itm_cd == _item.Qd_itm_cd && x.Tus_itm_stus == _item.Qd_itm_stus).ToList()
                                .ForEach(y => { y.Tus_warr_period = _item.Qd_warr_pd; y.Tus_Warranty_Remark = _item.Qd_warr_rmk; });
                        }

                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (QoutationDetails _invItem in invoice_items)
                                if (itm.Peo.Tus_itm_cd == _invItem.Qd_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Qd_line_no)
                                {
                                    //it.Sad_do_qty = q.theCount;
                                    //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                    _invItem.Qd_pick_qty = itm.theCount; // Current scan qty
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
                            foreach (QoutationDetails _invItem in invoice_items)
                                if (_invline == _invItem.Qd_line_no)
                                    _invItem.Qd_pick_qty += 1;
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

                QoutationDetails it = new QoutationDetails();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Qd_itm_desc = "";// mi.Mi_shortdesc;
                it.Mi_model = "";// mi.Mi_model;
                it.Qd_frm_qty = 0;
                it.Qd_tot_amt = 0;

                invoice_items_bind = new List<QoutationDetails>();

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
                        string _promotioncd = string.Empty; //Convert.ToString(dvDOItems.Rows[e.RowIndex].Cells["SAD_PROMO_CD"].Value.ToString());
                        bool _isAgePriceLevel = false;
                        bool _isSerializedPrice = false;
                        int _ageingDays = -1;

                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCode);
                        //add by tharanga 2018/10/19
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
                            _commonOutScan.ModuleTypeNo = 7;
                            _commonOutScan.ScanDocument = _invoiceNo;
                            _commonOutScan.DocumentType = "QUO";
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
                            //Add Chamal 29/03/2013
                            decimal _balQty = _invoiceQty - _doQty;
                            if (_isAgePriceLevel == false)
                                _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), string.Empty);
                            else
                                _dtTable = CHNLSVC.Inventory.GetInventoryBalanceByBatch(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value), string.Empty);

                            if (_dtTable != null)
                            {
                                if (_dtTable.Rows.Count > 0)
                                {
                                    //foreach (DataRow dtRow in _dtTable.Rows)
                                    //{
                                    //    foreach (DataColumn dc in _dtTable.Columns)
                                    //    {
                                    //        var field1 = dtRow[dc].ToString();
                                    //    }
                                    //}
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
                                        CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(dvDOItems.Rows[e.RowIndex].Cells["SAD_INV_NO"].Value.ToString(), Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["sad_itm_line"].Value.ToString()), dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value.ToString());
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
        { // Nadee
            var _count = invoice_items.Where(x => x.Mi_type == "G"   && x.Qd_frm_qty - x.Qd_issue_qty > 0).Count();
            //var _count = invoice_items.Where(x => x.Mi_type == "G" && string.IsNullOrEmpty(x.Sad_promo_cd) && x.Qd_frm_qty - x.Qd_issue_qty > 0).Count();
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
                var _scanqty = invoice_items.Where(x => x.Qd_itm_cd == _itm).ToList().Sum(x => x.Qd_pick_qty);
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
            if (MessageBox.Show("Are you sure want to process this ?", "Delivery Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (chkInv.Checked == false && pnlInvoice.Visible == false)
                {
                    //Added Darshana 06-Jun-2016 as per the request mail from mr. lasanga
                    //MessageBox.Show("Not authorize do this process.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;

                    //commenct darshana 06-Jan-2016 as per the request mail from mr. lasanga to block DCN
                    Process(); // uncomment 2017/12/13
                    btnGetInvoices_Click(null, null);

                }
                else if (pnlInvoice.Visible == true)
                {
                    InvoiceProcess();
                    btnGetDo_Click(null, null);
                }
            }
        }

        private void Process()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(lblInvoiceNo.Text))
                {
                    MessageBox.Show("Select the Quotation no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkManualRef.Checked)
                    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                    {
                        MessageBox.Show("You do not enter a valid manual document no.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "N/A";
                if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
                if (resultDate > 0)
                {
                    MessageBox.Show("Delivery date should be greater than or equal to quotation date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                
  


     

                Cursor.Current = Cursors.WaitCursor;

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", BaseCls.GlbUserComCode, lblInvoiceNo.Text, 0);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "QUO");

                if (invoice_items == null || invoice_items.Count < 1)
                {
                    invoice_items = CHNLSVC.Sales.GetAllQuotationItemList(lblInvoice.Text);
                }

                foreach (QoutationDetails _item in invoice_items)
                {
                    reptPickSerialsList.Where(x => x.Tus_itm_cd == _item.Qd_itm_cd && x.Tus_itm_stus == _item.Qd_itm_stus).ToList()
                        .ForEach(y => { y.Tus_warr_period = _item.Qd_warr_pd; y.Tus_Warranty_Remark = _item.Qd_warr_rmk; });
                }

                if (reptPickSerialsList == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No delivery items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    reptPickSerialsList.ForEach(x => x.Tus_session_id = BaseCls.GlbUserSessionID);

                    foreach (QoutationDetails _serRow in invoice_items)
                    {
                        //05-10-2015 modifed by Nadeeka
                        reptPickSerialsList.Where(w => w.Tus_itm_cd == _serRow.Qd_itm_cd && w.Tus_base_itm_line == _serRow.Qd_line_no).ToList().ForEach(s => s.Tus_unit_price = (_serRow.Qd_tot_amt - _serRow .Qd_itm_tax)/ _serRow.Qd_frm_qty);
                    }
                }

                #region Check more qty pick against the invoice qty
                //11-11-2015 Nadeeka
                Boolean _ismainAvilable = false;
                if (reptPickSerialsList != null)
                {
                    if (reptPickSerialsList.Count > 0)
                    {
                        foreach (ReptPickSerials _i in reptPickSerialsList)
                        {

                            DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetItemfromAssambleItem(_i.Tus_itm_cd);
                            if (_invnetoryCombinAnalalize != null)
                            {
                                if (_invnetoryCombinAnalalize.Rows.Count > 0)
                                {
                                    foreach (DataRow r in _invnetoryCombinAnalalize.Rows)
                                    {
                                        string _item = (string)r["MICP_COMP_ITM_CD"];
                                        Boolean _exist = reptPickSerialsList.Exists(x => x.Tus_itm_cd == _item);

                                        //if (_exist == false)
                                        //{
                                        //    MessageBox.Show("Components not found for main unit - " + _i.Tus_itm_cd, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return;

                                        //}

                                    }
                                }
                            }

                            MasterItem _itm = CHNLSVC.Inventory.GetItem("", _i.Tus_itm_cd);
                            if(_itm.Mi_itm_tp=="M")
                            {
                                _ismainAvilable = true;
                            }
                        }
                        if (_ismainAvilable == false)//19-11-2015
                        {
                            MessageBox.Show("Main Item not available", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        }
                    }
                }

                #endregion Check more qty pick against the invoice qty




                #region Check Registration Txn Serials 
                bool _isRegTxnFound  =false; 
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

                if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }

                #endregion Check Reference Date and the Doc Date

                #region Check Duplicate Serials

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

                #endregion Check Duplicate Serials

                #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

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

                #endregion Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "QUO");





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
                }

                #region Fill DO Header

                invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                invHdr.Ith_com = BaseCls.GlbUserComCode;
                invHdr.Ith_doc_tp = "DO";
                invHdr.Ith_doc_date = dtpDODate.Value.Date;
                invHdr.Ith_doc_year = dtpDODate.Value.Date.Year;
                invHdr.Ith_cate_tp = _invoiceType;
                invHdr.Ith_sub_tp = "DPS";
                invHdr.Ith_is_manual = false;
                invHdr.Ith_stus = "A";
                invHdr.Ith_cre_by = BaseCls.GlbUserID;
                invHdr.Ith_mod_by = BaseCls.GlbUserID;
                invHdr.Ith_direct = false;
                invHdr.Ith_session_id = BaseCls.GlbUserSessionID;//GlbUserSessionID;
                invHdr.Ith_manual_ref = txtManualRefNo.Text;
                invHdr.Ith_vehi_no = txtVehicleNo.Text;
                invHdr.Ith_remarks = txtRemarks.Text;
                invHdr.Ith_anal_1 = _userSeqNo.ToString();
                invHdr.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                invHdr.Ith_entry_no = lblInvoiceNo.Text.ToString();
                invHdr.Ith_bus_entity = txtCustCode.Text;
                invHdr.Ith_del_add1 = txtCustAddress1.Text;
                invHdr.Ith_del_add2 = txtCustAddress2.Text;
                invHdr.Ith_acc_no = _accNo;
                invHdr.Ith_pc = _profitCenter;
                invHdr.Ith_sub_docno  = lblInvoiceNo.Text.ToString();
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
                    _masterAutoGRN.Aut_year = _invHeaderGRN.Ith_doc_date.Year;

                    //result = CHNLSVC.Inventory.DeliveryOrder_Auto(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out documntNoGRN);
                }
                result = CHNLSVC.Inventory.DeliveryOrderEntryQuotation_Based(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out  documntNoGRN, IsGrn, null);

                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show("Delivery Order Note Successfully Saved! Document No : " + documntNo + ".\n Do you want to print now?", "Process Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        ReportViewerInventory _views = new ReportViewerInventory();
                        BaseCls.GlbReportTp = "OUTWARD";
                        _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                        _views.GlbReportDoc = documntNo;
                        _views.Show();
                        _views = null;

/*
                        //BaseCls.GlbReportTp = "OUTWARDDELCONF";
                        BaseCls.GlbReportTp = "OUTWARD";
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view1 = new Reports.Inventory.ReportViewerInventory();
                        //_view1.GlbReportName = "Outward_Docs_Del_Conf.rpt";
                        //BaseCls.GlbReportName = "Outward_Docs_Del_Conf.rpt";
                        _view1.GlbReportName = "Outward_Docs.rpt";
                        BaseCls.GlbReportName = "Outward_Docs.rpt";
                        _view1.GlbReportDoc = documntNo;
                        _view1.Show();
                        _view1 = null;*/

                    }
                    ClearScreen();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
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
                    // MessageBox.Show(documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (MessageBox.Show("Are you sure want to clear?", "Delivery Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Clear();
                int _eff = 0;
                get_invoices(out _eff);
            }
        }

        private void Clear()
        {
            ClearScreen();
            ClearVariable();
            ClearInv();
            dtpFromDate.Focus();
        }
        private void ClearInv()
        {
          //  gvInvoiceItem = null;
            txtInvRem.Text ="";
            txtAdd2.Text = "";
            txtAdd1.Text = "";
            txtPoNo.Text = "";
            txtMobile.Text = "";
            txtNIC.Text = "";
            txtmRef.Text = "";
            txtCust.Text = "";
            txtCusName.Text = "";
            cmbTitle.Text = "";
            lblAccountBalance.Text = "";
            lblAvailableCredit.Text = "";
            txtInvoiceNo.Text = "";
            txtQuoDate.Text = "";
            txtQuo.Text = "";
            txtInvDO.Text = "";
            cmbInvType.Text = "";
            cmbExecutive.Text = "";
            chkTaxPayable.Checked = false;
            _invoiceItemList = new  List<InvoiceItem>() ;
           
            gvInvoiceItem.AutoGenerateColumns = false;
            gvInvoiceItem.DataSource = _invoiceItemList;
             
            ucPayModes1.ClearControls();
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
                _CommonSearch.IsSearchEnter = true;
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

                DataTable dt = CHNLSVC.Sales.GetPendingQuotationToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, txtFindCustomer.Text, txtFindInvoiceNo.Text, "A", BaseCls.GlbUserDefProf);
                if (dt == null)
                {
                    MessageBox.Show("Please select the valid quotation no", "Quotation No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindInvoiceNo.Text = string.Empty;
                    txtFindInvoiceNo.Focus();
                    return;
                }
                else { btnGetInvoices_Click(null, null); }
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
            //try
            //{
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
            //    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtFindInvoiceNo;
            //    _CommonSearch.ShowDialog();
            //    txtFindInvoiceNo.Select();
            //}
            //catch (Exception err)
            //{
            //    Cursor.Current = Cursors.Default;
            //    CHNLSVC.CloseChannel();
            //    MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}


            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuoInv);
                DataTable _result = CHNLSVC.CommonSearch.GetQuotation4Inv(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindInvoiceNo;
                _CommonSearch.IsSearchEnter = true;
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

                            QuotationHeader _saveHdr = new QuotationHeader();
                            _saveHdr = CHNLSVC.Sales.Get_Quotation_HDR(lblInvoiceNo.Text);

                            if (_saveHdr != null)
                            {
                                if (Convert.ToBoolean(_saveHdr.Qh_anal_5) == true)
                                {// Nadeeka 09-10-2015
                                    List<QuotationSerial> _recallSerList = new List<QuotationSerial>();
                                    _recallSerList = CHNLSVC.Sales.GetQuoSerials(lblInvoiceNo.Text);
                                    foreach (QuotationSerial _tmp in _recallSerList)
                                    {
                                        if (_tmp.Qs_item == Convert.ToString(dvDOSerials.Rows[_rowCount].Cells["TUS_ITM_CD"].Value) && _tmp.Qs_ser == Convert.ToString(dvDOSerials.Rows[_rowCount].Cells["TUS_SER_1"].Value))
                                        {
                                            MessageBox.Show("Can't remove this serial, Serial reserved in quotation!", "Reserved Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }

                                }
                            }




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

                Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", BaseCls.GlbUserComCode, lblInvoiceNo.Text, 0);

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    //modify Rukshan 05/oct/2015 add two parameters
                    CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID),null,null);
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
                    _invoiceType = "QUO";// dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_TP"].Value.ToString();
                    _accNo = string.Empty; // dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_ACC_NO"].Value.ToString();
                    if (txtCustCode.Text == "AHDR2B0002")
                    { btnBOC.Visible = true; }
                    else
                    { btnBOC.Visible = false; }
                    LoadInvoiceItems(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString(), _profitCenter);
                    Cursor.Current = Cursors.Default;
                   // LoadInvoiceItems(txtInvcNo.Text , _profitCenter);
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
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementQuoDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_Quotation(_CommonSearch.SearchParams, null, null, dtpDODate.Value.Date.AddMonths(-1), dtpDODate.Value.Date);
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

                    List<QoutationDetails> _emptyinvoiceitemList = new List<QoutationDetails>();
                    _emptyinvoiceitemList = null;
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = _emptyinvoiceitemList;

                    #endregion Clear Data

                    InventoryHeader _invHdr = new InventoryHeader();
                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

                    if (_invHdr.Ith_stus=="C")
                    {
                        MessageBox.Show("Delivery Order already cancelled!", "Delivery Order No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

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
                    if (_invHdr.Ith_cate_tp != "QUO")
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
                        dtpDODate.Enabled = false;
                        dtpDODate.Value = _invHdr.Ith_doc_date.Date;
                        lblInvoiceNo.Text = _invHdr.Ith_oth_docno;
                        txtInvcNo.Text = lblInvoiceNo.Text;
                        txtCustCode.Text = _invHdr.Ith_bus_entity;
                        txtCustAddress1.Text = _invHdr.Ith_del_add1;
                        txtCustAddress2.Text = _invHdr.Ith_del_add2;
                        txtManualRefNo.Text = _invHdr.Ith_manual_ref;
                        txtRemarks.Text = _invHdr.Ith_remarks;

                        DataTable dtinv = CHNLSVC.Sales.GetPendingQuotationToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, txtFindCustomer.Text, _invHdr.Ith_oth_docno, "D", BaseCls.GlbUserDefProf);
                        if (dtinv.Rows.Count > 0 && dtinv != null)
                        {
                            txtCustName.Text = dtinv.Rows[0]["QH_PARTY_NAME"].ToString();
                            lblInvoiceDate.Text =Convert.ToDateTime( dtinv.Rows[0]["QH_DT"].ToString()).Date.ToString("dd/MMM/yyyy");
                            txtInvcDate.Text = lblInvoiceDate.Text;
                        }

                    
                      
                    }

                    #endregion Check Valid Document No

                    #region Get Serials

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    invoice_items = CHNLSVC.Sales.GetAllQuotationItemList(_invHdr.Ith_oth_docno);
                    if (invoice_items != null)
                    {
                        if (_serList != null)
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (QoutationDetails _invItem in invoice_items)
                                    if (itm.Peo.Tus_itm_cd == _invItem.Qd_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Qd_line_no)
                                    {
                                        //it.Sad_do_qty = q.theCount;
                                        //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                        _invItem.Qd_pick_qty = itm.theCount; // Current scan qty
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

                                    var _itmExist = invoice_items.Where(x => (x.Qd_itm_cd == _attachedItem || x.Qd_itm_cd == _attachedItem) && (x.Qd_frm_qty - x.Qd_issue_qty) > 0).ToList();
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
                //if (string.IsNullOrEmpty(txtSCMInvcNo.Text)) return;

                //DateTime _bondDate = DateTime.Now.Date;
                //txtSCMInvcDate.Clear();
                //txtSCMCustCode.Clear();
                //txtSCMCustName.Clear();

                //DataTable _dt = CHNLSVC.Sales.GetSalesHdr(txtSCMInvcNo.Text);
                //if (_dt != null && _dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("Already Uploaded", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtSCMInvcNo.Clear();
                //    txtSCMInvcDate.Clear();
                //    txtSCMCustCode.Clear();
                //    txtSCMCustName.Clear();
                //    txtSCMInvcNo.Focus();
                //    return;
                //}

                //DataTable _dt1 = CHNLSVC.Sales.GetSCMInvc(txtSCMInvcNo.Text);
                //if (_dt1 != null && _dt1.Rows.Count > 0)
                //{
                //    foreach (DataRow _dr1 in _dt1.Rows)
                //    {
                //        txtSCMCustCode.Text = _dr1["CUSTOMER_CODE"].ToString();

                //        DataTable _dt2 = CHNLSVC.Sales.GetCustomerDetails(BaseCls.GlbUserComCode, txtSCMCustCode.Text);
                //        if (_dt2 != null && _dt2.Rows.Count > 0)
                //        {
                //            foreach (DataRow _dr2 in _dt2.Rows)
                //            {
                //                txtSCMCustName.Text = _dr2["COMPANY_NAME"].ToString();
                //                break;
                //            }
                //        }
                //        DateTime _date = Convert.ToDateTime(_dr1["INVOICE_DATE"].ToString()).Date;
                //        txtSCMInvcDate.Text = _date.ToString("dd/MMM/yyyy");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Invalid Invoice No", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtSCMInvcNo.Clear();
                //    txtSCMInvcNo.Clear();
                //    txtSCMInvcDate.Clear();
                //    txtSCMCustCode.Clear();
                //    txtSCMCustName.Clear();
                //    txtSCMInvcNo.Focus();
                //    return;
                //}
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
                //if (!string.IsNullOrEmpty(txtSCMInvcNo.Text))
                //{
                //    Cursor.Current = Cursors.WaitCursor;
                //    int _uploaded = CHNLSVC.Sales.UploadSCMInvoice(txtSCMInvcNo.Text, BaseCls.GlbUserID, out _msg);
                //    if (_uploaded == 1)
                //    {
                //        Cursor.Current = Cursors.Default;
                //        MessageBox.Show("Invoice no - " + txtSCMInvcNo.Text + " uploaded!", "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtFindInvoiceNo.Text = txtSCMInvcNo.Text;
                //        txtSCMInvcNo.Clear();
                //        txtSCMInvcDate.Clear();
                //        txtSCMCustCode.Clear();
                //        txtSCMCustName.Clear();
                //        txtFindInvoiceNo.Focus();
                //        txtFindInvoiceNo.SelectAll();
                //        pnlDealerInvoice.Visible = false;
                //    }
                //    else
                //    {
                //        Cursor.Current = Cursors.Default;
                //        MessageBox.Show(_msg, "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
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
                txtCustAddress1.ReadOnly = false;
                txtCustAddress2.ReadOnly = false;
                txtCustAddress1.Focus();
                txtCustAddress1.SelectAll();
            }
            else
            {
                txtCustAddress1.ReadOnly = true;
                txtCustAddress2.ReadOnly = true;
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

        private void txtFindInvoiceNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dvPendingInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvDOItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGetDo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetPendingDoToInv(BaseCls.GlbUserComCode, dtpInvFromDate.Value.Date, dtpInvToDate.Value.Date, txtDo.Text, "A", BaseCls.GlbUserDefProf);
                if (dt.Rows.Count > 0)
                {
                    dgvDo.AutoGenerateColumns = false;
                    dgvDo.DataSource = dt;
                }
                else
                {
                    dt = null;
                    dgvDo.AutoGenerateColumns = false;
                    dgvDo.DataSource = dt;
                    MessageBox.Show("No pending deliver orders found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadItemDetails(out Boolean valid)
        {
            #region vsalidate do det
            List<InvoiceItem> _invoiceItemlistnew = null;
            _invoiceItemList = new List<InvoiceItem>();
            foreach (DataGridViewRow dgvr in dgvDo.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["chkdo"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    string do_no = dgvr.Cells["Delivery_Order"].Value.ToString();
                   
                
                    _invoiceItemlistnew = CHNLSVC.Sales.GetQuotationDetailforInvoice(BaseCls.GlbUserComCode, do_no);
                    _invoiceItemList.AddRange(_invoiceItemlistnew);

                }
            }

             _invoiceItemList = (from sg in _invoiceItemList
                                                          //where sg.TPSD_ELEMENT_CD != "REFUND"
                                                          group sg by new
                                                          {
                                                              Sad_itm_stus = sg.Sad_itm_stus,
                                                              Sad_itm_cd = sg.Sad_itm_cd,
                                                              Sad_itm_line = sg.Sad_itm_line
                                                          } into sgd
                                                        select new InvoiceItem 
                                                          {
                                                              Sad_do_qty = (sgd.Sum(x => x.Sad_do_qty) ),
                                                              Sad_unit_rt = sgd.Select(x => x.Sad_unit_rt).FirstOrDefault(),
                                                              Sad_itm_line = sgd.Select(x => x.Sad_itm_line).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_itm_cd = sgd.Select(x => x.Sad_itm_cd).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_itm_stus = sgd.Select(x => x.Sad_itm_stus).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_job_line = sgd.Select(x => x.Sad_job_line).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_disc_rt = sgd.Select(x => x.Sad_disc_rt).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_disc_amt = sgd.Select(x => x.Sad_disc_amt).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_pbook = sgd.Select(x => x.Sad_pbook).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_pb_lvl = sgd.Select(x => x.Sad_pb_lvl).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_unit_amt = sgd.Select(x => x.Sad_unit_amt).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              Sad_qty = (sgd.Sum(x => x.Sad_qty)),
                                                              Sad_itm_tax_amt = (sgd.Sum(x => x.Sad_itm_tax_amt)),
                                                              Sad_tot_amt = (sgd.Sum(x => x.Sad_tot_amt) )
                                                          }).ToList();

     

         
            #endregion

           
            //_invoiceItemList = CHNLSVC.Sales.GetQuotationDetailforInvoice(BaseCls.GlbUserComCode, txtInvDO.Text.Trim());

            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
            {
                _invoiceItemList.ForEach(X => X.Sad_job_line = X.Sad_itm_line);


                GrndSubTotal = 0;
                GrndDiscount = 0;
                GrndTax = 0;

                foreach (InvoiceItem itm in _invoiceItemList)
                {

                    txtUnitPrice.Text = Convert.ToString(itm.Sad_unit_rt);
                    txtDisRate.Text = Convert.ToString(itm.Sad_disc_rt);
                    txtDisAmt.Text = Convert.ToString(itm.Sad_disc_amt);

                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, itm.Sad_pbook, itm.Sad_pb_lvl);
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, itm.Sad_pbook, itm.Sad_pb_lvl);

                    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(itm.Sad_itm_cd, itm.Sad_itm_stus, Convert.ToDecimal(itm.Sad_qty), _priceBookLevelRef, Convert.ToDecimal(itm.Sad_unit_rt), Convert.ToDecimal(itm.Sad_disc_amt), Convert.ToDecimal(itm.Sad_disc_rt), true)));
                    txtLineTotAmt.Text = FormatToCurrency("0"); CalculateItem(itm.Sad_itm_cd, Convert.ToDecimal(itm.Sad_qty), itm.Sad_itm_stus);


                    //     decimal UnitPrice = TaxCalculation(itm.Sad_itm_cd, itm.Sad_itm_stus, itm.Sad_qty, _priceBookLevelRef, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_disc_rt, false);
                    itm.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                    itm.Sad_unit_amt = Convert.ToDecimal(txtUnitAmt.Text);
                    itm.Sad_disc_rt = Convert.ToDecimal(txtDisRate.Text);
                    itm.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
                    itm.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
                    itm.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                    itm.Sad_do_qty = itm.Sad_qty;
                    CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1;

                    txtUnitPrice.Text = Convert.ToString(0);
                    txtUnitAmt.Text = Convert.ToString(0);
                    txtDisRate.Text = Convert.ToString(0);
                    txtDisAmt.Text = Convert.ToString(0);
                    txtTaxAmt.Text = Convert.ToString(0);
                    txtLineTotAmt.Text = Convert.ToString(0);
                }

                var _invlst = new BindingList<InvoiceItem>(_invoiceItemList);
                gvInvoiceItem.AutoGenerateColumns = false;
                gvInvoiceItem.DataSource = _invlst;
                if (Convert.ToDecimal(lblGrndTotalAmount.Text) > 0)
                {
                    ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text);
                }
                ucPayModes1.InvoiceItemList = _invoiceItemList;
                ucPayModes1.SerialList = InvoiceSerialList;
                if (Convert.ToDecimal(lblGrndTotalAmount.Text) > 0)
                {
                    ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text);
                }
                if (_loyaltyType != null)
                {
                    ucPayModes1.LoyaltyCard = _loyaltyType.Salt_loty_tp;
                }
                if (ucPayModes1.HavePayModes)
                    ucPayModes1.LoadData();
                valid = true;
            }
            else
            {
                Clear();
                int _eff = 0;
                get_invoices(out _eff);
                valid = false;
            }
        }

        private void dgvDo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int x = (dgvDo.Rows[e.RowIndex].Cells["chkdo"].Value==null)?0:(int)dgvDo.Rows[e.RowIndex].Cells["chkdo"].Value;
                if (x == 1)
                {
                    dgvDo.Rows[e.RowIndex].Cells["chkdo"].Value = 0;
                }
                else
                { dgvDo.Rows[e.RowIndex].Cells["chkdo"].Value = 1; }
                if (dgvDo.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    #region vsalidate Del customer
                    foreach (DataGridViewRow dgvr in dgvDo.Rows)
                    {
                        DataGridViewCheckBoxCell chk = dgvr.Cells["chkdo"] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            string cust = dgvr.Cells["col_cus"].Value.ToString();
                            string quo_no = dgvr.Cells["quo_no"].Value.ToString();
                            if (!string.IsNullOrEmpty(txtCust.Text))
                            {
                                if (txtCust.Text != cust)
                                {
                                    MessageBox.Show("Deliver Customer Different.", "Customer Deferent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgvDo.Rows[e.RowIndex].Cells["chkdo"].Value = 0;
                                    return;
                                }   
                            }
                            if (!string.IsNullOrEmpty(txtQuo.Text))
                            {
                                if (txtQuo.Text != quo_no)
                                {
                                    MessageBox.Show("Quotation is Different.", "Quotation Deferent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgvDo.Rows[e.RowIndex].Cells["chkdo"].Value = 0;
                                    return;
                                }
                            }
                           
                        }
                    }
                    #endregion



                    chkTaxPayable.Checked = false;
                    Cursor.Current = Cursors.WaitCursor;
                   
                        
                    
                   
                    
                    txtInvDO.Text = dgvDo.Rows[e.RowIndex].Cells["Delivery_Order"].Value.ToString();

                    // lblInvoiceDate.Text = Convert.ToDateTime(dgvDo.Rows[e.RowIndex].Cells["SAH_DT"].Value.ToString()).Date.ToString("dd/MMM/yyyy");

                    txtQuo.Text = dgvDo.Rows[e.RowIndex].Cells["quo_no"].Value.ToString();
                    txtCust.Text = dgvDo.Rows[e.RowIndex].Cells["col_cus"].Value.ToString();
                    txtCusName.Text = dgvDo.Rows[e.RowIndex].Cells["col_cusname"].Value.ToString();
                    txtAdd1.Text = dgvDo.Rows[e.RowIndex].Cells["col_add1"].Value.ToString();
                    txtAdd2.Text = dgvDo.Rows[e.RowIndex].Cells["col_add2"].Value.ToString();
                    _profitCenter = dgvDo.Rows[e.RowIndex].Cells["col_pc"].Value.ToString();
                    cmbInvType.Text = dgvDo.Rows[e.RowIndex].Cells["QuoType"].Value.ToString();
                    //  _invoiceType = "QUO";// dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_TP"].Value.ToString();
                    // _accNo = string.Empty; // dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_ACC_NO"].Value.ToString();
                    txtDelAdd1.Text = dgvDo.Rows[e.RowIndex].Cells["coldelAdd1"].Value.ToString();
                    txtDelAdd2.Text = dgvDo.Rows[e.RowIndex].Cells["coldelAdd2"].Value.ToString();
                    txtDelCusCd.Text = dgvDo.Rows[e.RowIndex].Cells["coldelCusCd"].Value.ToString();
                    txtDelCusName.Text = dgvDo.Rows[e.RowIndex].Cells["coldelCusName"].Value.ToString();
                    DataTable dt = CHNLSVC.Sales.GetPendingQuotationToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, txtFindCustomer.Text, txtQuo.Text, "D", BaseCls.GlbUserDefProf);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbExecutive.Text = Convert.ToString(dt.Rows[0]["qh_sales_ex"]);
                        txtInvRem.Text = Convert.ToString(dt.Rows[0]["qh_remarks"]);
                        txtmRef.Text = Convert.ToString(dt.Rows[0]["qh_ref"]);
                        txtCust.Text = Convert.ToString(dt.Rows[0]["qh_party_cd"]);
                        txtCusName.Text = Convert.ToString(dt.Rows[0]["qh_party_name"]);
                        txtAdd1.Text = Convert.ToString(dt.Rows[0]["qh_add1"]);
                        txtAdd2.Text = Convert.ToString(dt.Rows[0]["qh_add2"]);
                        txtQuoDate.Text = Convert.ToDateTime(dt.Rows[0]["qh_dt"]).Date.ToString("dd/MMM/yyyy");
                    
                    }
                    
                    LoadItemDetails(out valid);
                    if (valid==true)
                    {
                        LoadCustomerDetailsByCustomer(null, null);
                        SetCustomerAndDeliveryDetails(false, null);  
                    }
                 else
                    {
                        Clear();
                        int _eff = 0;
                        get_invoices(out _eff);
                    }
                    





                    //  CalculateGrandTotal();
                   // Cursor.Current = Cursors.Default;
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

        #region Invoice
        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCust.Clear();
            txtCusName.Clear();
            txtAdd1.Clear();
            txtAdd2.Clear();
            txtMobile.Clear();
            txtNIC.Clear();
            chkTaxPayable.Checked = false;

        }
        private class CenterWinDialog : IDisposable
        {
            private int mTries = 0;
            private Form mOwner;

            public CenterWinDialog(Form owner)
            {
                mOwner = owner;
                owner.BeginInvoke(new MethodInvoker(findDialog));
            }

            private void findDialog()
            {
                // Enumerate windows to find the message box
                if (mTries < 0) return;
                EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
                if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
                {
                    if (++mTries < 10) mOwner.BeginInvoke(new MethodInvoker(findDialog));
                }
            }

            private bool checkWindow(IntPtr hWnd, IntPtr lp)
            {
                // Checks if <hWnd> is a dialog
                StringBuilder sb = new StringBuilder(260);
                GetClassName(hWnd, sb, sb.Capacity);
                if (sb.ToString() != "#32770") return true;
                // Got it
                Rectangle frmRect = new Rectangle(mOwner.Location, mOwner.Size);
                RECT dlgRect;
                GetWindowRect(hWnd, out dlgRect);
                MoveWindow(hWnd,
                    frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                    frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                    dlgRect.Right - dlgRect.Left,
                    dlgRect.Bottom - dlgRect.Top, true);
                return false;
            }

            public void Dispose()
            {
                mTries = -1;
            }

            // P/Invoke declarations
            private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);

            [DllImport("user32.dll")]
            private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

            [DllImport("kernel32.dll")]
            private static extern int GetCurrentThreadId();

            [DllImport("user32.dll")]
            private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

            [DllImport("user32.dll")]
            private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);

            [DllImport("user32.dll")]
            private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

            private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
        }
        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCust.Text)) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cmbInvType.Text.Trim() == "CRED" && txtCust.Text.Trim() == "CASH")
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(false);
                    txtCust.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCust.Text))
                    //_masterBusinessCompany = txtCust.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCust.Text, null, null, null, null, BaseCls.GlbUserComCode);

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCust.Focus();
                        return;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtCust.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        if (_isAvailable == null || _isAvailable.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            ClearCustomer(true);
                            txtCust.Focus();
                            return;
                        }
                    }
                    else if (cmbInvType.Text != "CS")
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("Selected Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCust.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCust.Text = _masterBusinessCompany.Mbe_cd;
                        // SetCustomerAndDeliveryDetails(false, null);
                        //ClearCustomer(false);
                    }
                    else
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        //  SetCustomerAndDeliveryDetails(false, null);
                    }

                    //  ViewCustomerAccountDetail(txtCustomer.Text);
                    //txtLoyalty.Text = ReturnLoyaltyNo();
                    //cmbTitle_SelectedIndexChanged(null, null);
                    //txtLoyalty_Leave(null, null);
                }
                else
                {


                }
                // ViewCustomerAccountDetail(txtCustomer.Text);
                //  txtLoyalty.Text = ReturnLoyaltyNo();
                //cmbTitle_SelectedIndexChanged(null, null);
                // txtLoyalty_Leave(null, null);
                EnableDisableCustomer();
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
        private void EnableDisableCustomer()
        {
            if (txtCust.Text == "CASH")
            {
                txtCust.Enabled = true;
                txtCusName.Enabled = true;
                txtAdd1.Enabled = true;
                txtAdd2.Enabled = true;
                txtMobile.Enabled = true;
                txtNIC.Enabled = true;

                //  btnSearch_NIC.Enabled = true;
                btnSearch_Customer.Enabled = true;
                //   btnSearch_Mobile.Enabled = true;
            }
            else
            {
                //txtCustomer.Enabled = false;
                txtCusName.Enabled = false;
                txtAdd1.Enabled = false;
                txtAdd2.Enabled = false;
                txtMobile.Enabled = false;
                txtNIC.Enabled = false;

                //btnSearch_NIC.Enabled = false;
                //btnSearch_Customer.Enabled = false;
                //btnSearch_Mobile.Enabled = false;
            }
        }
        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }
        private void InvoiceProcess()
        {
           // button1.Focus();
            if (btnSave.Enabled == false) return;
            _serialMatch = true;
            try
            {
                btnSave.Enabled = false;
                if (CheckServerDateTime() == false) return;

                //ADDED 2013/12/09
                //IF REGISTRATION NEED CAN NOT PROCEESS
                //WITHOUT REGISTRATION RECIEPT
                if (Convert.ToDecimal(lblGrndTotalAmount.Text) <= 0)
                {
                    btnSave.Enabled = true;
                    MessageBox.Show("Total invoice amount cannot be zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //29-09-2015
                #region Commnt by tharanga
                //InventoryHeader _invHdr = new InventoryHeader();
                //_invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtInvDO.Text);

                //DataTable _dt = CHNLSVC.Sales.GetSalesHdr(_invHdr.Ith_oth_docno);
                //if (_dt != null && _dt.Rows.Count > 0)
                //{
                //    btnSave.Enabled = true;
                //    MessageBox.Show("Invoice already raised", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                #endregion
                #region vsalidate do det
                Dictionary<string, string> DOdet = new Dictionary<string, string>();
                foreach (DataGridViewRow dgvr in dgvDo.Rows)
                {
                    DataGridViewCheckBoxCell chk = dgvr.Cells["chkdo"] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string do_no = dgvr.Cells["Delivery_Order"].Value.ToString();
                        string quo_no = dgvr.Cells["quo_no"].Value.ToString();
                        DOdet.Add(do_no, quo_no);
                       
                        InventoryHeader _invHdrnew = new InventoryHeader();
                        _invHdrnew = CHNLSVC.Inventory.Get_Int_Hdr(do_no);

                        DataTable _dtnew = CHNLSVC.Sales.GetSalesHdr(_invHdrnew.Ith_oth_docno);
                        if (_dtnew != null && _dtnew.Rows.Count > 0)
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Invoice already raised", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                }

                #endregion

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtInvDate, lblH1, txtInvDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (txtInvDate.Value.Date != DateTime.Now.Date)
                        {
                            btnSave.Enabled = true;
                            txtInvDate.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpDODate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        txtInvDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvDate.Focus();
                        return;
                    }
                }



                if (_isNeedRegistrationReciept)
                {
                    //if (_List == null || _List.Count <= 0)
                    //{
                    //    MessageBox.Show("Registration Details not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //decimal _payAmt = ucPayModes2.RecieptItemList.Sum(x => x.Sard_settle_amt);
                    //if (_payAmt < _totalRegistration)
                    //{
                    //    MessageBox.Show("Please enter full Registration Amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }

                //END

                if (string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
                {
                    btnSave.Enabled = true;
                    MessageBox.Show("Please select executive before save.", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

          

                //add by darshana on 12-Mar-2014 - To Gold operation totally operate as consignment base and no need to generate grn.
                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                //if (_masterComp.Mc_anal13 == 0)
                //{
                //    #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                //    string documntNo = string.Empty;
                //    if ((chkDeliverLater.Checked == false || chkDeliverNow.Checked) && ScanSerialList != null && ScanSerialList.Count > 0)
                //        if (CHNLSVC.Inventory.Check_Cons_Item_has_Quo(BaseCls.GlbUserComCode, txtDate.Value.Date, ScanSerialList, out documntNo) < 0)
                //        {
                //            Cursor.Current = Cursors.Default;
                //            MessageBox.Show(documntNo, "Quotation not define", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //            return;
                //        }

                //    #endregion Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013
                //}
                //if (chkDeliverLater.Checked)
                //{
                //    if (CHNLSVC.Sales.IsForwardSaleExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf))
                //    {
                //        this.Cursor = Cursors.Default;
                //        using (new CenterWinDialog(this)) { MessageBox.Show("No of forward sales are restricted. Please contact inventory dept.", "Max. Forward Sale", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //        return;
                //    }
                //}
                if (cmbInvType.Text.Trim() == "CRED" && txtCust.Text.Trim() == "CASH")
                {
                    btnSave.Enabled = true;
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtCust.Clear();
                    txtCust.Focus();
                    return;
                }
                if (chkManualRef.Checked && string.IsNullOrEmpty(txtManualRefNo.Text))
                {
                    using (new CenterWinDialog(this))
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("Please select the manual no", "Manual No", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (cmbInvType.Text.Trim() == "CRED")
                    if (string.IsNullOrEmpty(txtPoNo.Text))
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this))
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Please select the PO number", "Purchase Order Number", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtPoNo.Clear();
                        txtPoNo.Focus();
                        return;
                    }
                    else if (txtPoNo.Text.Trim() == "N/A" || txtPoNo.Text.Trim() == "NA" || txtPoNo.Text.Trim() == ".")
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this))
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Please select the valid PO number", "Purchase Order Number", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtPoNo.Clear();
                        txtPoNo.Focus();
                        return;
                    }


                //kapila 31/8/2015 check payment is exceeded the allowed
                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "CRDPAYRT", DateTime.Now.Date);
                if (_SystemPara.Hsy_desc != null)
                {
                    decimal _realTotal = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    decimal _totalPaid = _recieptItem.Sum(x => x.Sard_settle_amt);

                    if ((_realTotal / 100 * _SystemPara.Hsy_val) <= _totalPaid)
                    {
                        MessageBox.Show("Settle amount is greater than allowed (Rate: " + _SystemPara.Hsy_val + ")", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                bool IsBuyBackItemAvailable = false;
                var _bbQty = _invoiceItemList.Where(x => x.Sad_merge_itm == "3" && x.Sad_unit_rt != 0).Sum(x => x.Sad_qty);
                if (_bbQty > 0)
                {
                    if (BuyBackItemList == null || BuyBackItemList.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this))
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("Please select the buy back item", "Buy Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        // pnlBuyBack.Visible = true;
                        IsBuyBackItemAvailable = false;
                        return;
                    }
                    else
                    {
                        var _purBB = BuyBackItemList.Sum(x => x.Tus_qty);
                        if (_purBB != _bbQty)
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this))
                            {
                                btnSave.Enabled = true;
                                MessageBox.Show("Please select " + _bbQty.ToString() + " buy back item(s)", "Buy Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            // pnlBuyBack.Visible = true;
                            IsBuyBackItemAvailable = false;
                            return;
                        }
                    }
                }
                else if (_bbQty <= 0 && BuyBackItemList != null)
                {
                    if (BuyBackItemList.Count > 0)
                    {
                        //tabControl1.SelectedTab = tabPage4;
                        if (MessageBox.Show("There is no buy back promotion selected, but buy back return item already available. Do you need to remove selected return buy-back item and continue?", "Return Item - Buy Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BuyBackItemList = null;
                        }
                        else
                        {
                            // tabControl1.SelectedTab = tabPage4;
                            IsBuyBackItemAvailable = false;
                            btnSave.Enabled = true;
                            return;
                        }

                        IsBuyBackItemAvailable = true;
                    }
                    else IsBuyBackItemAvailable = false;
                }
                else if (_bbQty > 0 && BuyBackItemList != null) if (BuyBackItemList.Count > 0) IsBuyBackItemAvailable = true;
                //if (chkGiftVoucher.Checked)
                //{
                //    var _isExistGv = _invoiceItemList.Where(x => IsGiftVoucher(x.Sad_itm_tp)).Count();
                //    if (_isExistGv <= 0)
                //    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You can't process without gift vouchers", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                //    string _notMapped = string.Empty;
                //    var _gvitm = (from DataGridViewRow _row in gvGiftVoucher.Rows where _row.Index != -1 select _row).ToList();
                //    Parallel.ForEach(_gvitm, _row => { var _isPromotion = _invoiceItemList.Where(x => x.Sad_itm_line == Convert.ToInt32(_row.Cells["gf_baseItemLine"].Value) && !string.IsNullOrEmpty(x.Sad_promo_cd)).Select(x => x.Sad_promo_cd).Count(); if (_isPromotion <= 0) { string _mappedItem = Convert.ToString(_row.Cells[7].EditedFormattedValue); string _gvNo = Convert.ToString(_row.Cells["gf_serial1"].EditedFormattedValue); if (string.IsNullOrEmpty(_mappedItem)) if (string.IsNullOrEmpty(_notMapped)) _notMapped = _gvNo; else _notMapped += ", " + _gvNo; } });
                //    if (!string.IsNullOrEmpty(_notMapped))
                //    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please attach the issuing item to the gift voucher for the following gift voucher(s). " + _notMapped + ".", "Attach Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); } tabControl1.SelectedTab = tabPage3; return; }
                //}
                //  if (pnlMain.Enabled == false) return;
                ///  if (IsBackDateOk(chkDeliverLater.Checked, IsBuyBackItemAvailable) == false) return;
                bool _isHoldInvoiceProcess = false;
                InvoiceHeader _hdr = new InvoiceHeader();
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                    if (_hdr != null)
                        if (_hdr.Sah_stus != "H")
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this))
                            {
                                btnSave.Enabled = true;
                                MessageBox.Show("You can not edit already saved invoice", "Invoice Re-call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                }
                if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;
                //if (_isHoldInvoiceProcess && chkDeliverLater.Checked == false)
                //{
                //    this.Cursor = Cursors.Default;
                //    using (new CenterWinDialog(this)) { MessageBox.Show("You can not use 'Deliver Now!' option for hold invoice", "Invoice Hold", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //    return;
                //}

                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this))
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbInvType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCust.Text))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this))
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtCust.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(cmbBook.Text))
                //{
                //    this.Cursor = Cursors.Default;
                //    using (new CenterWinDialog(this)) { MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //    cmbBook.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(cmbLevel.Text))
                //{
                //    this.Cursor = Cursors.Default;
                //    using (new CenterWinDialog(this)) { MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //    cmbLevel.Focus();
                //    return;
                //}
                if (_invoiceItemList.Count <= 0 || gvInvoiceItem.Rows.Count <= 0  )
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this))
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("Please select the items for invoice", "Invoice item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                bool _isExeMust = false;
                if (MasterChannel != null && MasterChannel.Rows.Count > 0)
                    _isExeMust = Convert.ToBoolean(MasterChannel.Rows[0].Field<Int16>("msc_needsalexe"));
                if (string.IsNullOrEmpty(txtExecutive.Text))
                {
                    if (_isExeMust)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("Please select the executive code", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Focus();
                        return;
                    }
                    else txtExecutive.Text = "N/A";
                }
                if (!string.IsNullOrEmpty(txtExecutive.Text) && _isExeMust)
                {
                    if (txtExecutive.Text.Trim().ToUpper() == "N/A" || txtExecutive.Text.Trim().ToUpper() == "NA")
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("Sales executive is mandatory to this channel", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Clear();
                        txtExecutive.Focus();
                        cmbExecutive.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(lblCurrency.Text))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("Please select the currency code", "Currency", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    lblCurrency.Focus();
                    return;
                }
                if (_MasterProfitCenter.Mpc_check_pay && _recieptItem.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("This profit center is not allow for raise invoice without payment. Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { btnSave.Enabled = true;  MessageBox.Show("Please enter the customer name", "Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(txtAdd1.Text) && string.IsNullOrEmpty(txtAdd2.Text))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { btnSave.Enabled = true;  MessageBox.Show("Please enter the customer address", "Customer Address", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem == null)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { btnSave.Enabled = true;  MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        return;
                    }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { btnSave.Enabled = true;  MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count >= 0)
                        {
                            decimal _realPay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                            else
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                            decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                            if (_totlaPay != _realPay)
                            {
                                this.Cursor = Cursors.Default;
                                using (new CenterWinDialog(this)) { btnSave.Enabled = true;  MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                            string LoyaltyCard = "";
                            if (_loyaltyType != null)
                            {
                                LoyaltyCard = _loyaltyType.Salt_loty_tp;
                            }

                            //paymode restriction
                            //added 2014/03/26
                            List<PayTypeRestrict> _restrictList = new List<PayTypeRestrict>();
                            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in _invoiceItemList)
                                {
                                    /*
                                    itm,loty,promo - lv1
                                    itm,promo -lv2
                                    loty,promo-lv3
                                    itm,loty -lv4
                                    itm-lv5
                                    promo-lv6
                                    loty-lv7
                                     */
                                    List<PayTypeRestrict> _resPay = CHNLSVC.Sales.GetPaymodeRestriction(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvDate.Value.Date);
                                    if (_resPay != null && _resPay.Count > 0)
                                    {
                                        //lv 01
                                        List<PayTypeRestrict> _temp;
                                        _temp = (from _t in _resPay
                                                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (_t.Stpr_loty == LoyaltyCard || _t.Stpr_loty == "ALL")
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                        _temp = null;
                                        //lv 02
                                        _temp = (from _t in _resPay
                                                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (string.IsNullOrEmpty(_t.Stpr_loty))
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                        _temp = null;
                                        //lv 03
                                        _temp = (from _t in _resPay
                                                 where (string.IsNullOrEmpty(_t.Stpr_loty)) && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (string.IsNullOrEmpty(_t.Stpr_loty))
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                        _temp = null;
                                        //lv 04
                                        _temp = (from _t in _resPay
                                                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (string.IsNullOrEmpty(_t.Stpr_promo_cd)) && (string.IsNullOrEmpty(_t.Stpr_loty))
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                        _temp = null;
                                        //lv 05
                                        _temp = (from _t in _resPay
                                                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (string.IsNullOrEmpty(_t.Stpr_promo_cd)) && (string.IsNullOrEmpty(_t.Stpr_loty))
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                        _temp = null;
                                        //lv 06
                                        _temp = (from _t in _resPay
                                                 where (string.IsNullOrEmpty(_t.Stpr_itm)) && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (string.IsNullOrEmpty(_t.Stpr_loty))
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                        _temp = null;
                                        //lv 07
                                        _temp = (from _t in _resPay
                                                 where (string.IsNullOrEmpty(_t.Stpr_itm)) && (string.IsNullOrEmpty(_t.Stpr_promo_cd)) && (_t.Stpr_loty == LoyaltyCard || _t.Stpr_loty == "ALL")
                                                 select _t).ToList<PayTypeRestrict>();

                                        if (_temp != null && _temp.Count > 0)
                                        {
                                            _restrictList.AddRange(_temp);
                                        }
                                    }
                                }
                            }
                            if (_restrictList != null && _restrictList.Count > 0)
                            {
                                foreach (RecieptItem _recItm in _recieptItem)
                                {
                                    List<PayTypeRestrict> _tRes = (from _res in _restrictList
                                                                   where _res.Stpr_pay_mode == _recItm.Sard_pay_tp
                                                                   select _res).ToList<PayTypeRestrict>();
                                    if (_tRes != null && _tRes.Count > 0)
                                    {
                                        if (_recItm.Sard_pay_tp == "CRCD")
                                        {
                                            foreach (PayTypeRestrict _payres in _tRes)
                                            {
                                                if (_payres.Stpr_alw_non_promo)
                                                {
                                                    if (_recItm.Sard_cc_period > 0)
                                                    {
                                                        using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("Cannot process invoice paymode- " + _recItm.Sard_pay_tp + " restricted for promotions.\nPlease remove paymode and check again.", "Invoice Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("Cannot process invoice paymode- " + _recItm.Sard_pay_tp + " restricted.\nPlease remove paymode and check again.", "Invoice Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            using (new CenterWinDialog(this)) { btnSave.Enabled = true;  MessageBox.Show("Cannot process invoice paymode- " + _recItm.Sard_pay_tp + " restricted.\nPlease remove paymode and check again.", "Invoice Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                            return;
                                        }
                                    }
                                }
                            }
                            //end
                        }
                string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text);
                if (string.IsNullOrEmpty(_invoicePrefix))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { btnSave.Enabled = true; MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                Int32 _count = 1;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                        _recieptItem.ForEach(x => x.Sard_line_no = _count++);
                _count = 1;
                List<InvoiceItem> _linedInvoiceItem = new List<InvoiceItem>();
                _invoiceItemList = _invoiceItemList.OrderBy(x => x.Sad_itm_line).ToList();
                ScanSerialList = ScanSerialList.OrderBy(x => x.Tus_base_itm_line).ToList();
                foreach (InvoiceItem _item in _invoiceItemList)
                {
                    //Int32 _currentLine = _item.Sad_itm_line;
                    //if (ScanSerialList != null)
                    //    if (ScanSerialList.Count > 0)
                    //        ScanSerialList.Where(x => x.Tus_base_itm_line == _currentLine).ToList().ForEach(x => x.Tus_base_itm_line = _count);
                    //if (InvoiceSerialList != null)
                    //    if (InvoiceSerialList.Count > 0)
                    //        InvoiceSerialList.Where(x => x.Sap_itm_line == _currentLine).ToList().ForEach(x => x.Sap_itm_line = _count);
                   // _item.Sad_itm_line = _count; 04-11-2015 Nadeeka
                    _linedInvoiceItem.Add(_item);
                    _count += 1;
                }

                _linedInvoiceItem.ForEach(x => x.Sad_isapp = true);
                _linedInvoiceItem.ForEach(x => x.Sad_iscovernote = true);
                _invoiceItemList = new List<InvoiceItem>();
                _invoiceItemList = _linedInvoiceItem;
                if (chkDeliverLater.Checked == false && IsReferancedDocDateAppropriate(ScanSerialList, Convert.ToDateTime(txtInvDate.Text).Date) == false)
                    return;
                if (chkDeliverLater.Checked == false)
                {
                    string _itmList = string.Empty;
                    bool _isqtyNserialOk = true;

                    if (_isqtyNserialOk == false)
                    {
                        //if (!chkDeliverNow.Checked)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    using (new CenterWinDialog(this)) { MessageBox.Show("Invoice qty and no. of serials are mismatched. Please check the following item for its serials and qty.\nItem List : " + _itmList, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                        //    _serialMatch = false;
                        //    return;
                        //}
                        //else
                        //{
                        //    _serialMatch = false;
                        //}
                    }
                }
                //if (chkDeliverLater.Checked == false)
                //{
                //    string _nottallylist = string.Empty;
                //    bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                //    if (_isTallywithinventory == false)
                //    {
                //        if (!chkDeliverNow.Checked)
                //        {
                //            this.Cursor = Cursors.Default;
                //            using (new CenterWinDialog(this)) { MessageBox.Show("Following item does not having inventory balance for raise delivery order; " + _nottallylist, "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                //            _serialMatch = false;
                //            return;
                //        }
                //        else
                //        {
                //            _serialMatch = false;
                //        }
                //    }
                //}

                #region sachith/process serial select

                //if (!_serialMatch)
                //{
                //    if (chkDeliverNow.Checked)
                //    {
                //        dvDOItems.AutoGenerateColumns = false;
                //        dvDOItems.DataSource = _invoiceItemList;
                //        pnlDoNowItems.Visible = true;
                //        pnlMain.Enabled = false;

                //        return;
                //    }
                //}

                #endregion sachith/process serial select

                MasterBusinessEntity _entity = new MasterBusinessEntity();
                InvoiceHeader _invheader = new InvoiceHeader();
                RecieptHeader _recHeader = new RecieptHeader();
                InventoryHeader invHdr = new InventoryHeader();
                InventoryHeader _buybackheader = new InventoryHeader();
                MasterAutoNumber _buybackAuto = new MasterAutoNumber();
                bool _isCustomerHasCompany = false;
                string _customerCompany = string.Empty;
                string _customerLocation = string.Empty;
                _entity = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCust.Text, string.Empty, string.Empty, "C");
                if (_entity != null)
                    if (_entity.Mbe_cd != null)
                        if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                        { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }
                //invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                //invHdr.Ith_com = BaseCls.GlbUserComCode;
                //invHdr.Ith_doc_tp = "DO";
                //invHdr.Ith_doc_date = Convert.ToDateTime(txtInvDate.Text).Date;
                //invHdr.Ith_doc_year = Convert.ToDateTime(txtInvDate.Text).Year;
                //invHdr.Ith_cate_tp = cmbInvType.Text.Trim();
                //invHdr.Ith_sub_tp = "DPS";
                //invHdr.Ith_bus_entity = txtCust.Text.Trim();
                //invHdr.Ith_del_add1 = txtAdd1.Text.Trim();
                //invHdr.Ith_del_add1 = txtAdd2.Text.Trim();
                //invHdr.Ith_is_manual = false;
                //invHdr.Ith_stus = "A";
                //invHdr.Ith_cre_by = BaseCls.GlbUserID;
                //invHdr.Ith_mod_by = BaseCls.GlbUserID;
                //invHdr.Ith_direct = false;
                //invHdr.Ith_session_id = GlbUserSessionID;
                //invHdr.Ith_manual_ref = txtManualRefNo.Text;
                //invHdr.Ith_vehi_no = string.Empty;
                //invHdr.Ith_remarks = string.Empty;
                //MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                //_masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                //_masterAutoDo.Aut_cate_tp = "LOC";
                //_masterAutoDo.Aut_direction = 0;
                //_masterAutoDo.Aut_moduleid = "DO";
                //_masterAutoDo.Aut_start_char = "DO";
                //_masterAutoDo.Aut_year = Convert.ToDateTime(txtInvDate.Text).Year;
                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = "LKR";
                _invheader.Sah_cus_add1 = txtAdd1.Text.Trim();
                _invheader.Sah_cus_add2 = txtAdd2.Text.Trim();
                _invheader.Sah_cus_cd = txtCust.Text.Trim();
                _invheader.Sah_cus_name = txtCusName.Text.Trim();
               
                _invheader.Sah_d_cust_add1 = txtDelAdd1.Text.Trim();
                _invheader.Sah_d_cust_add2 = txtDelAdd2.Text.Trim();
                _invheader.Sah_d_cust_cd = txtDelCusCd.Text.Trim();
                _invheader.Sah_d_cust_name = txtDelCusName.Text.Trim();
                _invheader.Sah_direct = true;
                _invheader.Sah_dt = Convert.ToDateTime(txtInvDate.Text);
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = 1;
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "SA";
                _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_ref = txtManualRefNo.Text;
                _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtInvDO.Text;
                _invheader.Sah_remarks = "";
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_structure_seq = txtQuo.Text.Trim();


                _invheader.Sah_stus = "D";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_direct = true;
                _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                _invheader.Sah_anal_11 = 1;
                _invheader.Sah_del_loc = string.Empty;
                _invheader.Sah_grn_com = _customerCompany;
                _invheader.Sah_grn_loc = _customerLocation;
                _invheader.Sah_is_grn = _isCustomerHasCompany;
                _invheader.Sah_grup_cd = string.Empty;
                _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
                _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
                _invheader.Sah_anal_4 = txtPoNo.Text.Trim();
                _invheader.Sah_anal_6 = string.Empty;
                _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
                _invheader.Sah_is_dayend = 0;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();
                //if (string.IsNullOrEmpty(Convert.ToString(cmbTechnician.SelectedValue))) _invheader.Sah_anal_1 = string.Empty;
                //else _invheader.Sah_anal_1 = Convert.ToString(cmbTechnician.SelectedValue);
                _invheader.Sah_anal_1 = string.Empty;
                if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());
                _recHeader.Sar_acc_no = "";
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = DateTime.Now;
                _recHeader.Sar_currency_cd = "LKR";
                _recHeader.Sar_debtor_add_1 = txtAdd1.Text;
                _recHeader.Sar_debtor_add_2 = txtAdd2.Text;
                _recHeader.Sar_debtor_cd = txtCust.Text;
                _recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                _recHeader.Sar_is_oth_shop = false;
                _recHeader.Sar_is_used = false;
                _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                _recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_nic_no = txtNIC.Text;
                _recHeader.Sar_oth_sr = "";
                _recHeader.Sar_prefix = "";
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = Convert.ToDateTime(txtInvDate.Text);
                _recHeader.Sar_receipt_no = "na";
                _recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = txtRemarks.Text;
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _recHeader.Sar_tel_no = string.Empty;
                _recHeader.Sar_tot_settle_amt = 0;
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PRO";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = cmbInvType.Text;
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = _invoicePrefix;
                _invoiceAuto.Aut_year = Convert.ToDateTime(txtInvDate.Text).Year;
                MasterAutoNumber _receiptAuto = null;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(txtInvDate.Text).Year;
                    }
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    _buybackheader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _buybackheader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _buybackheader.Ith_channel = string.Empty;
                    }
                }

                _count = 1;
                string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (BuyBackItemList != null) if (BuyBackItemList.Count > 0)
                    {
                        BuyBackItemList.ForEach(X => X.Tus_bin = _bin);
                        BuyBackItemList.ForEach(X => X.Tus_itm_line = _count++);
                        BuyBackItemList.ForEach(X => X.Tus_serial_id = "N/A");
                        BuyBackItemList.ForEach(x => x.Tus_exist_grndt = Convert.ToDateTime(txtInvDate.Value).Date);
                        BuyBackItemList.ForEach(x => x.Tus_orig_grndt = Convert.ToDateTime(txtInvDate.Value).Date);
                    }
                if (txtCust.Text.Trim() != "CASH")
                {
                    MasterBusinessEntity _en = CHNLSVC.Sales.GetCustomerProfile(txtCust.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                    if (_en != null)
                        if (string.IsNullOrEmpty(_en.Mbe_com))
                        {
                            _invheader.Sah_tax_exempted = _en.Mbe_tax_ex;
                            _invheader.Sah_is_svat = _en.Mbe_is_svat;
                        }
                }
                else
                {

                }
                CollectBusinessEntity();
                string _invoiceNo = "";
                string _receiptNo = "";
                string _deliveryOrderNo = "";
                _invoiceItemListWithDiscount = new List<InvoiceItem>();
                List<InvoiceItem> _discounted = null;
                bool _isDifferent = false;
                decimal _tobepay = 0;
                decimal _tobepay1 = 0;
                bool _canSaveWithoutDiscount = false;
                try
                {
                    //Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    //CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                    //_invoiceItemListWithDiscount = _discounted;

                    Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    if (_discountSequence == 0)
                    {
                        foreach (InvoiceItem itm in _invoiceItemList)
                        {
                            bool isMulti;
                            int seq;
                            List<InvoiceItem> _item = new List<InvoiceItem>();
                            _item.Add(itm);
                            DataTable _discount = CHNLSVC.Sales.GetPromotionalDiscountSequences(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtInvDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtInvDate.Text.Trim()), _item, _recieptItem, _invheader, out isMulti, out seq);
                            //show pop up
                            if (_discount == null)
                            {
                                _discountSequence = seq;
                                if (isMulti)
                                {
                                    _isDifferent = false;
                                    _discountSequence = -9999;
                                }
                                else
                                {
                                    if (_discountSequence != -9999 && _discountSequence != 0)
                                    {
                                        // if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //  {
                                        _isDifferent = true;
                                        _discountSequence = seq;
                                        CHNLSVC.Sales.GetGeneralPromotionProcess(_discountSequence, BaseCls.GlbUserComCode, _item, out _discounted, out _isDifferent, out _tobepay1, _invheader);
                                        // _tobepay = _tobepay + _tobepay1;
                                        _invoiceItemListWithDiscount.AddRange(_discounted);
                                        CashPromotionDiscountHeader _discountHdr = CHNLSVC.General.GetPromotionalDiscountBySeq(seq);
                                        if (_discountHdr != null)
                                        {
                                            _canSaveWithoutDiscount = _discountHdr.Spdh_is_alw_normal;
                                        }
                                        //    }
                                    }
                                    else
                                    {
                                        if (!ucPayModes1.IsDiscounted)
                                        {
                                            _isDifferent = false;
                                            _discountSequence = -9999;
                                            //       if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            //       {
                                            // _discountSequence = 0;
                                            //  return;

                                            //    }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //  if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //   {
                                if (_discount.Rows.Count > 0)
                                {
                                    //show popup
                                    //pnlDiscount.Visible = true;
                                    //pnlMain.Enabled = false;
                                    //gvDiscount.DataSource = _discount;
                                    return;
                                }
                                //      }
                                else
                                {
                                    _isDifferent = false;
                                    _discountSequence = -9999;
                                }
                            }
                        }
                    }
                    else if (_discountSequence != -9999)
                    {
                        //Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                        CHNLSVC.Sales.GetGeneralPromotionProcess(_discountSequence, BaseCls.GlbUserComCode, _invoiceItemList, out _discounted, out _isDifferent, out _tobepay, _invheader);

                        _invoiceItemListWithDiscount = _discounted;
                    }
                    if (_invoiceItemListWithDiscount != null && _invoiceItemListWithDiscount.Count > 0)
                    {
                        _isDifferent = true;
                        foreach (InvoiceItem invItm in _invoiceItemList)
                        {
                            List<InvoiceItem> _itmList = (from _res in _invoiceItemListWithDiscount
                                                          where _res.Mi_itm_stus == invItm.Mi_itm_stus && _res.Sad_itm_cd == invItm.Sad_itm_cd
                                                          select _res).ToList<InvoiceItem>();
                            if (_itmList == null || _itmList.Count <= 0)
                            {
                                _invoiceItemListWithDiscount.Add(invItm);
                            }
                        }
                        _tobepay = _invoiceItemListWithDiscount.Select(X => X.Sad_tot_amt).Sum();
                    }

                    //if ((_discountSequence == -9999 && _isDifferent) || (_discountSequence == -9999 && ucPayModes1.IsDiscounted))
                    //{
                    //    if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //    {
                    //    }
                    //    else
                    //    {
                    //        _isDifferent = false;
                    //        _discountSequence = 0;
                    //    }
                    //}

                    //if (_discountSequence == -9999) {
                    //    CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                    //    _invoiceItemListWithDiscount = _discounted;

                    //    if (_isDifferent) {
                    //        if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //        {
                    //            _isDifferent = false;
                    //        }
                    //    }

                    //}

                    //added sachith
                    //2013/09/04

                    if (!_isDifferent && !ucPayModes1.IsDiscounted)
                    {
                        //credit note discount (if invoice pay mode has credit note and invoice don't have discount)
                        List<RecieptItem> _creditNote = (from _res in _recieptItem
                                                         where _res.Sard_pay_tp == "CRNOTE" || _res.Sard_pay_tp == "ADVAN"
                                                         select _res).ToList<RecieptItem>();
                        if (_creditNote != null && _creditNote.Count > 0)
                        {
                            Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                            CHNLSVC.Sales.GetGeneralPromotionDiscountAdvanCredit(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtInvDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtInvDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                            _invoiceItemListWithDiscount = _discounted;
                            foreach (InvoiceItem _invItm in _invoiceItemListWithDiscount)
                            {
                                if (_invItm.Sad_dis_type == "P")
                                {
                                    CashPromotionDiscountHeader _discountHdr = CHNLSVC.General.GetPromotionalDiscountBySeq(_invItm.Sad_dis_seq);
                                    if (_discountHdr != null)
                                    {
                                        _canSaveWithoutDiscount = _discountHdr.Spdh_is_alw_normal;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exs)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show(exs.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    CHNLSVC.CloseChannel();
                    return;
                }
                if (_isDifferent || ucPayModes1.IsDiscounted)
                {
                    //if (MessageBox.Show("Discount applicable for selected paymodes,Do you want to ammend payments\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //}
                    //else
                    //{
                    //    if (_canSaveWithoutDiscount)
                    //    {
                    //        //if (MessageBox.Show("Invoice will save without Discount", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //        //{
                    //        //    _isDifferent = false;
                    //        //    _discountSequence = 0;
                    //        //}
                    //        //else
                    //        //{
                    //        //    _isDifferent = false;
                    //        //    _discountSequence = 0;
                    //        //    return;
                    //        //}
                    //        //return;
                    //    }
                    //    else
                    //    {
                    //        //MessageBox.Show("Can not process invoice because discount circular not allow to process without discount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        //_isDifferent = false;
                    //        //_discountSequence = 0;
                    //        //return;
                    //    }
                    //}
                }
                else
                {
                    //if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    //    _discountSequence = 0;
                    //    return;
                    //}
                }




                int effect = -1;
                string _error = string.Empty;
                string _buybackadj = string.Empty;
                string _registration = "";
                try
                {
                    btnSave.Enabled = false;
                    _invoiceItemList.ForEach(x => x.Sad_srn_qty = 0);
                    List<RecieptItem> _registrationReciept = new List<RecieptItem>();
                    // _registrationReciept = ucPayModes2.RecieptItemList;
                    List<VehicalRegistration> _registrationList = new List<VehicalRegistration>();

                    effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransactionQuo(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error, null, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, DOdet);

                }
                catch (Exception ex)
                {                 
                   //    if (_giftVoucher != null && _giftVoucher.Count > 0) { _giftVoucher = new List<InvoiceVoucher>(); ScanSerialList.AddRange(_giftVoucherSerial); _giftVoucherSerial = new List<ReptPickSerials>(); }
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    CHNLSVC.CloseChannel();
                    return;
                }
                finally
                {
                    string Msg = string.Empty;

                    if (effect != -1)
                    {

                        Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";

                        if (cmbInvType.Text.Trim() == "CS")
                        {
                            var _isCashPaymentExsit = _recieptItem.Where(x => x.Sard_pay_tp == "CASH").ToList();
                            if (_isCashPaymentExsit != null)
                                if (_isCashPaymentExsit.Count > 0)
                                {
                                    decimal _cashamt = _isCashPaymentExsit.Sum(x => x.Sard_settle_amt);
                                    string _customerGiven = PaymentBalanceConfirmation(Msg, _cashamt);
                                    if (!string.IsNullOrEmpty(_customerGiven))
                                    {
                                        this.Cursor = Cursors.Default;
                                        string BalanceToGive = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_customerGiven) - _cashamt));
                                        using (new CenterWinDialog(this)) { MessageBox.Show("You have to give back as balance " + BalanceToGive + "\n in " + lblCurrency.Text + ".", "Balance To Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    }
                                }
                        }
                        else
                        { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(Msg, "Saved Documents", MessageBoxButtons.OK, MessageBoxIcon.Information); } }
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        MasterBusinessEntity _itm = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCust.Text, string.Empty, string.Empty, "C");
                        bool _isAskDO = false;
                        if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;

                        if (chkManualRef.Checked == false)
                        {
                            bool _isPrintElite = false;
                            if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                            { if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2" || _MasterProfitCenter.Mpc_chnl.Trim() == "APPLE") { BaseCls.GlbReportTp = "INV"; ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; _isPrintElite = true; } }
                            //AUTO_DEL
                            //get permission
                            bool _permission = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11055);
                            if (!_permission)
                            {
                                if (_MasterProfitCenter.Mpc_chnl.Trim() == "AUTO_DEL")
                                {
                                    if (cmbInvType.Text.Trim() == "CRED")
                                    {
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "DealerCreditInvoicePrints.rpt"; BaseCls.GlbReportName = "DealerCreditInvoicePrints.rpt"; _view.GlbReportDoc = _invoiceNo; BaseCls.GlbReportDoc = _invoiceNo;
                                        _view.Show(); _view = null; _isPrintElite = true;
                                    }
                                    else
                                    {
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "DealerInvoicePrints.rpt"; BaseCls.GlbReportName = "DealerInvoicePrints.rpt"; _view.GlbReportDoc = _invoiceNo; BaseCls.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; _isPrintElite = true;
                                    }
                                }
                            }
                            if (_isPrintElite == false)
                            {
                                if (_itm.Mbe_sub_tp != "C.")
                                {
                                    //Showroom
                                    //========================= INVOCIE  CASH/CREDIT/ HIRE
                                    if (chkTaxPayable.Checked == false)
                                    { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; }
                                    else
                                    {
                                        //Add Code by Chamal 27/04/2013
                                        //====================  TAX INVOICE
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                        if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                        //====================  TAX INVOICE
                                    }
                                }
                                else
                                {
                                    //Dealer
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoicePrintTax.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                    if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                }
                            }

                            if (BuyBackItemList != null)
                                if (BuyBackItemList.Count > 0)
                                {
                                    Reports.Inventory.ReportViewerInventory _viewBB = new Reports.Inventory.ReportViewerInventory();

                                    BaseCls.GlbReportName = string.Empty;
                                    GlbReportName = string.Empty;
                                    _viewBB.GlbReportName = string.Empty;
                                    BaseCls.GlbReportTp = "INWARD";
                                    if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                                        _viewBB.GlbReportName = "Inward_Docs.rpt";
                                    else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                                        _viewBB.GlbReportName = "Dealer_Inward_Docs.rpt";
                                    else
                                        _viewBB.GlbReportName = "Inward_Docs.rpt";
                                    _viewBB.GlbReportDoc = _buybackadj;
                                    _viewBB.Show();
                                    _viewBB = null;
                                }
                        }

                        if (_isNeedRegistrationReciept)
                        {
                            MasterBusinessEntity _tem = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCust.Text, string.Empty, string.Empty, "C");

                            if (_tem.Mbe_sub_tp == "C.")
                            {
                                ReportViewer _view = new ReportViewer();
                                BaseCls.GlbReportName = string.Empty;
                                _view.GlbReportName = string.Empty;
                                _view.GlbReportName = "ReceiptPrintDealers.rpt";
                                _view.GlbReportDoc = _registration;
                                _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                                _view.Show();
                                _view = null;
                            }
                            else
                            {
                                ReportViewer _view = new ReportViewer();
                                BaseCls.GlbReportName = string.Empty;
                                _view.GlbReportName = string.Empty;
                                BaseCls.GlbReportTp = "REC";
                                _view.GlbReportName = "ReceiptPrints.rpt";
                                _view.GlbReportDoc = _registration;
                                _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                                _view.Show();
                                _view = null;
                            }
                        }
                      //  btnClear_Click(null, null);
                        //change chk value
                        Clear();

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_error))
                            //  { if (_giftVoucher != null && _giftVoucher.Count > 0) { _giftVoucher = new List<InvoiceVoucher>(); ScanSerialList.AddRange(_giftVoucherSerial); _giftVoucherSerial = new List<ReptPickSerials>(); } this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { 
                              MessageBox.Show("Generating Invoice is terminated due to following reason, " + _error, "Generated Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                           this.Cursor = Cursors.Default; using (new CenterWinDialog(this))
                            CHNLSVC.CloseChannel();
                    }
                    CHNLSVC.CloseAllChannels();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseChannel();
            }
            finally
            {
                btnSave.Enabled = true;
                CHNLSVC.CloseAllChannels();
            }
        }

        private string PaymentBalanceConfirmation(string Msg, decimal _cashamount)
        {
            string _cashGiven = Microsoft.VisualBasic.Interaction.InputBox(Msg + "\nPlease enter customer tender amount.", "Balance", FormatToCurrency(Convert.ToString(_cashamount)), -1, -1);
            if (!string.IsNullOrEmpty(_cashGiven))
            {
                if (IsNumeric(_cashGiven) == false)
                {
                    Msg = "Invalid amount. ";
                    PaymentBalanceConfirmation(Msg, _cashamount);
                }

                if (Convert.ToDecimal(_cashGiven) < _cashamount)
                {
                    Msg = "Invalid amount. ";
                    PaymentBalanceConfirmation(Msg, _cashamount);
                }
            }

            return _cashGiven;
        }
        private void CollectBusinessEntity()
        {
            _businessEntity = new MasterBusinessEntity();
            _businessEntity.Mbe_act = true;
            _businessEntity.Mbe_add1 = txtAdd1.Text;
            _businessEntity.Mbe_add2 = txtAdd2.Text;
            _businessEntity.Mbe_cd = "c1";
            _businessEntity.Mbe_com = BaseCls.GlbUserComCode;
            _businessEntity.Mbe_contact = string.Empty;
            _businessEntity.Mbe_email = string.Empty;
            _businessEntity.Mbe_fax = string.Empty;
            _businessEntity.Mbe_is_tax = false;
            _businessEntity.Mbe_mob = txtMobile.Text;
            _businessEntity.Mbe_name = txtCusName.Text;
            _businessEntity.Mbe_nic = txtNIC.Text;
            _businessEntity.Mbe_tax_no = string.Empty;
            _businessEntity.Mbe_tel = string.Empty;
            _businessEntity.Mbe_tp = "C";
            _businessEntity.Mbe_pc_stus = "GOOD";
            _businessEntity.Mbe_ho_stus = "GOOD";
            _businessEntity.MBE_TIT = cmbTitle.Text;
            _businessEntity.Mbe_cate = "INDIVIDUAL";
        }
        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCust.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtAdd1.Text = _masterBusinessCompany.Mbe_add1;
            txtAdd2.Text = _masterBusinessCompany.Mbe_add2;
            txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            cmbTitle.Text = _masterBusinessCompany.MBE_TIT;
            ucPayModes1.Customer_Code = txtCust.Text.Trim();
            ucPayModes1.Mobile = txtMobile.Text.Trim();

            if (_isRecall == false)
            {
                /* if (string.IsNullOrEmpty(txtDelAddress1.Text))*/
                txtAdd1.Text = _masterBusinessCompany.Mbe_add1;
                /* if (string.IsNullOrEmpty(txtDelAddress2.Text))*/
                txtAdd2.Text = _masterBusinessCompany.Mbe_add2;
                /* if (string.IsNullOrEmpty(txtDelCustomer.Text) || txtDelCustomer.Text.Trim() == "CASH")*/
                txtCust.Text = _masterBusinessCompany.Mbe_cd;
                /* if (string.IsNullOrEmpty(txtDelName.Text))*/
                txtAdd1.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtAdd1.Text = _hdr.Sah_cus_add1;
                txtAdd2.Text = _hdr.Sah_cus_add2;

                txtAdd1.Text = _hdr.Sah_d_cust_add1;
                txtAdd2.Text = _hdr.Sah_d_cust_add2;
                txtCust.Text = _hdr.Sah_d_cust_cd;
                txtCusName.Text = _hdr.Sah_d_cust_name;
                //txtDelLocation.Text = _hdr.Sah_del_loc;
            }

            if (_isRecall == false)
            {
                if (_masterBusinessCompany.Mbe_is_tax) { chkTaxPayable.Checked = true; chkTaxPayable.Enabled = true; } else { chkTaxPayable.Checked = false; chkTaxPayable.Enabled = false; }
            }

            if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
            if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
            //  GetNICGender();
            if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
            else
            {
                string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                bool _exist = cmbTitle.Items.Contains(_title);
                if (_exist)
                    cmbTitle.Text = _title;
            }
        }
        private void ViewCustomerAccountDetail(string _customer)
        {
            if (string.IsNullOrEmpty(_customer.Trim())) return;
            if (_customer != "CASH")
            {
                CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(BaseCls.GlbUserComCode, txtCust.Text.Trim());
                lblAccountBalance.Text = FormatToCurrency(Convert.ToString(_account.Saca_acc_bal));
                lblAvailableCredit.Text = FormatToCurrency(Convert.ToString((_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal)));
            }
        }
        private void ClearVariable()
        {
            btnSave.Enabled = true;
            txtInvoiceNo.Enabled = true;

            _recieptItem = new List<RecieptItem>();
            ScanSerialList = new List<ReptPickSerials>();

            _invoiceItemList = new List<InvoiceItem>();
            InvoiceSerialList = new List<InvoiceSerial>();

            _lineNo = 1;

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            SSCombineLine = 1;

            _serialMatch = true;

            dvDOSerials.DataSource = null;
            dvDOItems.DataSource = null;
            _discountSequence = 0;

            _isNeedRegistrationReciept = false;

            lblAccountBalance.Text = FormatToCurrency("0");
            lblAvailableCredit.Text = FormatToCurrency("0");
            _loyaltyType = null;
        }

        private void LoadExecutive()
        {
            cmbExecutive.DataSource = null;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_tblExecutive != null)
            {
                var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                cmbExecutive.ValueMember = "esep_epf"; cmbExecutive.DisplayMember = "esep_first_name";
                if (_lst != null && _lst.Count > 0) cmbExecutive.DataSource = _lst.CopyToDataTable(); cmbExecutive.DropDownWidth = 200;
                if (_tblExecutive != null && _MasterProfitCenter !=null)
                {
                    cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man;
                }
                //MSR channel load default executive with null record
                if (_MasterProfitCenter.Mpc_chnl == "ELITE")
                {
                    //DataRow dr = _tblExecutive.NewRow();
                    //dr["esep_epf"] = "";
                    //dr["esep_first_name"] = "";
                    //_tblExecutive.Rows.Add(dr);
                    cmbExecutive.SelectedIndex = -1;
                }

            }
        }
        private void LoadCachedObjects()
        {
            { _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString()); _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString()); MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString()); IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString()); }

             
        }

        private void VariableInitialization()
        { InvItm_Qty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_UPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_UnitAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_DisRate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_DisAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_TaxAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_LineAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_Qty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_UPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_UnitAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_DisRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_DisAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_TaxAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_LineAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; InvItm_Qty.DefaultCellStyle.Format = "0.0000"; InvItm_UPrice.DefaultCellStyle.Format = "N"; InvItm_UnitAmt.DefaultCellStyle.Format = "N"; InvItm_DisRate.DefaultCellStyle.Format = "N"; InvItm_DisAmt.DefaultCellStyle.Format = "N"; InvItm_TaxAmt.DefaultCellStyle.Format = "N"; InvItm_LineAmt.DefaultCellStyle.Format = "N"; btnSave.Enabled = true; txtInvoiceNo.Enabled = true; _recieptItem = new List<RecieptItem>(); ScanSerialList = new List<ReptPickSerials>(); _invoiceItemList = new List<InvoiceItem>(); InvoiceSerialList = new List<InvoiceSerial>(); _lineNo = 0; GrndSubTotal = 0; GrndDiscount = 0; GrndTax = 0; }


        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (txtInvDate.Value.Date == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            if (lblVatExemptStatus.Text != "Available")
                            {
                                if (_isTaxfaction == false)
                                    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                            }
                            else
                            {
                                if (_isTaxfaction) _pbUnitPrice = 0;
                            }
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, txtInvDate.Value.Date); else _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", txtInvDate.Value.Date);
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction) _pbUnitPrice = 0;
                                }
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, txtInvDate.Value.Date); else _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", txtInvDate.Value.Date);
                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        _pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction) _pbUnitPrice = 0;
                                }
                            }
                        }
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            //else return RoundUpForPlace(value, 2);
            else return Math.Round(value, 2);
        }
        private void CalculateItem(string item, decimal _qty, string _status)
        {
            if (!string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(_qty), true)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(item, _status, Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(_qty) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, item, Convert.ToString(_status), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                            }
                        }
                    }

                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }
        private void LoadPayments()
        {
            if (Convert.ToDecimal(lblGrndAfterDiscount.Text) > 0)
            {
                ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndAfterDiscount.Text);
            }

            List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                                           where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                                           select _invItm).ToList<InvoiceItem>();
            if (_temItems != null && _temItems.Count > 0)
            {
                ucPayModes1.ISPromotion = true;
            }
            else
                ucPayModes1.ISPromotion = false;
            ucPayModes1.InvoiceItemList = _invoiceItemList;
            ucPayModes1.SerialList = null;
            if ( (ucPayModes1.TotalAmount) > 0)
            {
                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(ucPayModes1.TotalAmount));
            }
            if (ucPayModes1.HavePayModes)
                ucPayModes1.LoadData();
        }

        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }

            lblGrndAfterDiscount.Text = FormatToCurrency(Convert.ToString(GrndSubTotal - GrndDiscount));

            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));

        }
        #endregion

        private void chkInv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInv.Checked == true)
            {
                pnlInvoice.Visible = true;
                pnlInvoice.Width = 993;
                pnlInvoice.Height=641;
                btnGetDo_Click(null,null);
            }
            else
            {
                pnlInvoice.Visible = false;
            }
        }

        private void cmbExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {
                txtExecutive.Text = Convert.ToString(cmbExecutive.SelectedValue);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _recieptItem = ucPayModes1.RecieptItemList;
                decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                if (_totlaPay == Convert.ToDecimal(lblGrndTotalAmount.Text))
                {
                    toolStrip1.Focus();
                    btnSave.Select();
                }
            }
            catch (Exception ex)
            { //this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private bool LoadInvoiceType()
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");
                    cmbInvType.DataSource = _types;
                    cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbInvType.Text = DefaultInvoiceType;

              
                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;

            return _isAvailable;
        }
        private void LoadPriceDefaultValue()
        { if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0) { var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList(); if (_defaultValue != null)                        if (_defaultValue.Count > 0) { DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp; DefaultBook = _defaultValue[0].Sadd_pb; DefaultLevel = _defaultValue[0].Sadd_p_lvl; DefaultStatus = _defaultValue[0].Sadd_def_stus; DefaultItemStatus = _defaultValue[0].Sadd_def_stus; LoadInvoiceType(); } } cmbTitle.SelectedIndex = 0; }
        private void LoadPayMode()
        { ucPayModes1.InvoiceType = cmbInvType.Text.Trim(); ucPayModes1.Customer_Code = txtCust.Text.Trim(); ucPayModes1.Mobile = txtMobile.Text.Trim(); ucPayModes1.LoadPayModes(); }

        private void btnInv_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable _result = CHNLSVC.CommonSearch.SearchInvoice(_CommonSearch.SearchParams, null, null, txtInvDate.Value.Date.AddMonths(-1), txtInvDate.Value.Date);
                _CommonSearch.dtpFrom.Value = txtInvDate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = txtInvDate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                //_commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
            }
            catch (Exception ex)
            { txtInvoiceNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void CheckInvoiceNo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) { txtCust.Focus(); return; }
            try
            {
                 
                RecallInvoice();
            }
            catch (Exception ex)
            { txtInvoiceNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCust.Focus();
        }

        private void txtInvoiceNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }
        private void RecallInvoice()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text);
            if (_hdr == null) { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid invoice", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Text = string.Empty; return; }
            //Add by Chamal 20-07-2014
            if (_hdr.Sah_pc != BaseCls.GlbUserDefProf.ToString()) { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid invoice", "Invalid Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Text = string.Empty; return; }
            //Add by Chamal 25-08-2014
            if (_hdr.Sah_tp != "INV") { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid invoice", "Invalid Invoice Category", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Text = string.Empty; return; }
            if (_hdr.Sah_inv_tp == "CS")
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else if (_hdr.Sah_inv_tp == "CRED")
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this))
                { MessageBox.Show("Please select the valid invoice", "Invalid Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Text = string.Empty; return;
            }

            this.Cursor = Cursors.Default;

            AssignInvoiceHeaderDetail(_hdr);
            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
            _invoiceItemList = _list;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            InvoiceSerialList = new List<InvoiceSerial>();
            ScanSerialList = new List<ReptPickSerials>();
            InvoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(txtInvoiceNo.Text.Trim());
            foreach (InvoiceItem itm in _list)
            { CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1; }
            if (InvoiceSerialList == null)
                InvoiceSerialList = new List<InvoiceSerial>();
            gvInvoiceItem.AutoGenerateColumns = false;
            gvInvoiceItem.DataSource = _list;

            //load invoice serials
            if (InvoiceSerialList != null && InvoiceSerialList.Count > 0)
            {
                foreach (InvoiceSerial invSer in InvoiceSerialList)
                {
                    ReptPickSerials _rept = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, invSer.Sap_itm_cd, invSer.Sap_ser_1, "N/A", "");
                    if (_rept != null)
                    {
                        List<InvoiceItem> _item = (from _res in _invoiceItemList
                                                   where _res.Sad_itm_cd == invSer.Sap_itm_cd &&
                                                   _res.Sad_itm_line == invSer.Sap_itm_line
                                                   select _res).ToList<InvoiceItem>();
                        if (_item == null || _item.Count <= 0)
                        {
                            MessageBox.Show("Error occurred while recalling invoice\nItem - " + invSer.Sap_itm_cd + " not found on item list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        _rept.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                        _rept.Tus_base_itm_line = _item[0].Sad_itm_line;
                        _rept.Tus_usrseq_no = -100;
                        _rept.Tus_unit_price = _rept.Tus_unit_price;
                        MasterItem msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, invSer.Sap_itm_cd);
                        //get item status

                        _rept.Tus_new_status = _item[0].Mi_itm_stus;
                        _rept.ItemType = msitem.Mi_itm_tp;
                        ScanSerialList.Add(_rept);
                    }
                }
            }
       

            //end load invoice serials

            List<RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());
            ucPayModes1.RecieptItemList = _itms;
            _recieptItem = _itms;
            ucPayModes1.LoadRecieptGrid();

            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
            ucPayModes1.LoadData();
            if (_hdr.Sah_stus != "H")
            {
                btnSave.Enabled = false;
          
            }
            else
            {
                btnSave.Enabled = true;
          
            }
        }
        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            cmbInvType.Text = _hdr.Sah_inv_tp;
            txtInvDate.Text = _hdr.Sah_dt.ToString("dd/MM/yyyy"); ;
            txtCust.Text = _hdr.Sah_cus_cd;
         //   txtLoyalty.Text = _hdr.Sah_anal_6;
            _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCust.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            ViewCustomerAccountDetail(txtCust.Text);
            txtExecutive.Text = _hdr.Sah_sales_ex_cd;
            DataTable _recallemp = CHNLSVC.Sales.GetinvEmp(BaseCls.GlbUserComCode, _hdr.Sah_sales_ex_cd);
            string _name = string.Empty;
            string _code = "";
            if (_recallemp != null && _recallemp.Rows.Count > 0)
            {
                _name = _recallemp.Rows[0].Field<string>("esep_first_name");
                _code = _recallemp.Rows[0].Field<string>("esep_epf");
            }
            //cmbExecutive.DataSource = null;
            //cmbExecutive.Items.Clear();
            //cmbExecutive.Items.Add(_name);
            cmbExecutive.SelectedValue = _code;
            lblCurrency.Text = _hdr.Sah_currency;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
             txtQuo.Text = _hdr.Sah_ref_doc;
            txtPoNo.Text = _hdr.Sah_anal_4;
            txtRemarks.Text = _hdr.Sah_remarks;
        }

        private void label101_Click(object sender, EventArgs e)
        {

        }

        private void btnHide_Click(object sender, EventArgs e)
        {

        }

        private void txtmRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtManualRefNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtmRef_Leave(object sender, EventArgs e)
        {
            if (chkmref.Checked == false) return;

            if (IsNumeric(txtmRef.Text) == false && chkmref.Checked)
            {
                MessageBox.Show("Please enter only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmRef.Clear();
                return;
            }

            Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "MDOC_INV", string.Empty, Convert.ToInt32(txtmRef.Text.Trim()), GlbModuleName);
            if (X == false)
            {
                using (new CenterWinDialog(this)) { MessageBox.Show("Invalid Manual no", "Manual No", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                txtmRef.Clear();
            }
        }

        private void pnlGeneralInfor_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void txtDelAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDelAdd2.Focus();
            }
        }

        private void txtDelAdd2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtDelAdd2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11068))
            {
                
            }
            else
            {
                MessageBox.Show("you don't have permission to cancel ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            
            
            
            if (string.IsNullOrEmpty(txtDocumentNo.Text))
            {
                MessageBox.Show("Select the Delivery Order no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          //  dtpDate.Value = DateTime.Now.Date;
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


            //if (dtpDate.Value.Date != dtpDODate.Value.Date)
            //{
            //    MessageBox.Show("Document date not match with the selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dtpDate.Focus();
            //    return;
            //}

            
            
            if (MessageBox.Show("Do you want to cancel this delivery order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { string _err=string.Empty ;
            InventoryHeader _invHdr = new InventoryHeader();
            _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

            DataTable _dt = CHNLSVC.Sales.GetSalesHdr(_invHdr.Ith_oth_docno);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                MessageBox.Show("Unable to cancel this DO, Invoice already raised", "Dealer Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region Check Duplicate Serials
                //14-11-2015 Nadeeka
            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            reptPickSerialsList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
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
                MessageBox.Show("Following item serials are duplicating.  " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion


            Int32 result = CHNLSVC.Inventory.CancelOutwardDoc(txtDocumentNo.Text, BaseCls.GlbUserID, txtInvcNo.Text , out _err);

               if (result != -99 && result >= 0)
               {
                   Cursor.Current = Cursors.Default;
                   txtDocumentNo.Text = "";
                   MessageBox.Show("Successfully Cancelled! Document No : " + txtDocumentNo.Text + ".?", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                   
               }
            }
        }

        private void dvDOSerials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCustCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReverce_Click(object sender, EventArgs e)
        {
               //14-11-2015 Nadeeka

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11068))
            {

            }
            else
            {
                MessageBox.Show("you don't have permission to reverse ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable _t = CHNLSVC.Sales.GetDeliveryOrader(txtDocumentNo.Text);
                     if (_t != null && _t.Rows.Count > 0)
                     {
                         MessageBox.Show("This Delivery order is already reversed!", "Delivery Order No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                     }

            InventoryHeader _invHdr = new InventoryHeader();
            _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

            if (_invHdr.Ith_stus == "C")
            {
                MessageBox.Show("Delivery Order already cancelled!", "Delivery Order No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            reptPickSerialsList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
 

        
         
            if (reptPickSerialsList == null)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

    

            if (MessageBox.Show("Do you want to reverce this delivery Order?", "Saving... : ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                Cursor.Current = Cursors.Default;
                return;
            }
            #region Check Duplicate Serials
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
            #endregion
            InventoryHeader inHeader = new InventoryHeader();
            #region Fill InventoryHeader
            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            foreach (DataRow r in dt_location.Rows)
            {
                // Get the value of the wanted column and cast it to string
                inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                if (System.DBNull.Value != r["ML_CATE_2"])
                {
                    inHeader.Ith_channel = (string)r["ML_CATE_2"];
                }
                else
                {
                    inHeader.Ith_channel = string.Empty;
                }
            }
            inHeader.Ith_acc_no = "STOCK_ADJ";
            inHeader.Ith_anal_1 = "";
            inHeader.Ith_anal_2 = "";
            inHeader.Ith_anal_3 = "";
            inHeader.Ith_anal_4 = "";
            inHeader.Ith_anal_5 = "";
            inHeader.Ith_anal_6 = 0;
            inHeader.Ith_anal_7 = 0;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_anal_10 = false;
            inHeader.Ith_anal_11 = false;
            inHeader.Ith_anal_12 = false;
            inHeader.Ith_bus_entity = "";
            inHeader.Ith_cate_tp = "NOR";
            inHeader.Ith_com = BaseCls.GlbUserComCode;
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = BaseCls.GlbUserID;
            inHeader.Ith_cre_when = DateTime.Now;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";
            inHeader.Ith_direct = true;
           
            inHeader.Ith_doc_date = dtpDate.Value.Date;
            inHeader.Ith_doc_no = string.Empty;
            inHeader.Ith_doc_tp = "ADJ";
            inHeader.Ith_doc_year = dtpDate.Value.Date.Year;
            inHeader.Ith_entry_no = _invHdr.Ith_entry_no;
            inHeader.Ith_entry_tp = _invHdr.Ith_entry_tp;
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = string.Empty;
            inHeader.Ith_isprinted = false;
            inHeader.Ith_is_manual = false;
            inHeader.Ith_job_no = string.Empty;
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            inHeader.Ith_manual_ref = _invHdr.Ith_manual_ref;
            inHeader.Ith_mod_by = BaseCls.GlbUserID;
            inHeader.Ith_mod_when = DateTime.Now;
            inHeader.Ith_noofcopies = 0;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_oth_docno = _invHdr.Ith_doc_no;
            inHeader.Ith_remarks = txtRemarks.Text;
            //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
            inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = "QUO";
            inHeader.Ith_vehi_no = string.Empty;
            #endregion
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            #region Fill MasterAutoNumber
            masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "ADJ";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "ADJ";
            masterAuto.Aut_year = null;

            #endregion

            string documntNo = "";
            Int32 result = -99;
            #region Save Adj+  
        
                result = CHNLSVC.Inventory.ADJPlus(inHeader, reptPickSerialsList, null, masterAuto, out documntNo);

                if (result != -99 && result >= 0)
                {
                    Int32 _eff = CHNLSVC.Sales.Update_Quotation_HDR_status(_invHdr.Ith_entry_no, "R");       //kapila 8/3/2016
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Successfully Reversed! Document No : " + documntNo + "\n", "Process Completed : ", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(documntNo, "Process Terminated : ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            #endregion

        }

        private void btn_qut_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Do_qua_serch);
                DataTable _result = CHNLSVC.CommonSearch.get_quo_to_inv(_CommonSearch.SearchParams, null, null, dtpDODate.Value.Date.AddMonths(-1), dtpDODate.Value.Date);
                _CommonSearch.dtpFrom.Value = dtpDODate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = dtpDODate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtQuatation_no;
                _CommonSearch.ShowDialog();
                txtQuatation_no.Select();
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

        private void txtQuatation_no_Leave(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.Sales.get_quo_to_inv(BaseCls.GlbUserComCode, dtpInvFromDate.Value.Date, dtpInvToDate.Value.Date, txtQuatation_no.Text, "A", BaseCls.GlbUserDefProf);
            if (dt.Rows.Count > 0)
            {
                dgvDo.AutoGenerateColumns = false;
                dgvDo.DataSource = dt;
            }
            else
            {
                dt = null;
                dgvDo.AutoGenerateColumns = false;
                dgvDo.DataSource = dt;
                MessageBox.Show("No pending deliver orders found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
