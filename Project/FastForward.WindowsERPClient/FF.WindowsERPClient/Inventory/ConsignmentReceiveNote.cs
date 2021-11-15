using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Inventory;
using System.Linq;
using FF.WindowsERPClient.Barcode;

namespace FF.WindowsERPClient.Inventory
{
    //pkg_search.sp_get_PurchaseOrders  =NEW
    public partial class ConsignmentReceiveNote : Base
    {
        private List<InvoiceItem> invoice_items = null;
        private List<InvoiceItem> invoice_items_bind = null;

        private List<PurchaseOrderDelivery> podel_items = null; 

        private string _profitCenter = "";
        private bool IsGrn = false;

        #region Clear Screen

        private void ClearScreen()
        {
            try
            {
                Cursor.Current = Cursors.Default;
                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-5).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                dtpDODate.Value = CHNLSVC.Security.GetServerDateTime().Date;

                GetPendingPurchaseOrders(dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), txtFindSupplier.Text, txtFindPONo.Text, false);
                chkManualRef.Checked = false;
                //SetManualRefNo();

                lblBackDateInfor.Text = string.Empty;
                txtPONo.Text = string.Empty;
                dtpPODate.Value = DateTime.Now.Date;
                txtPORefNo.Text = string.Empty;

                txtSuppCode.Text = string.Empty;
                txtSuppName.Text = string.Empty;
                txtFindSupplier.Text = string.Empty;
                txtFindPONo.Text = string.Empty;
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

                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDODate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ClearBody()
        {
            Cursor.Current = Cursors.Default;
            chkManualRef.Checked = false;
            txtPONo.Text = string.Empty;
            dtpPODate.Value = DateTime.Now.Date;
            txtPORefNo.Text = string.Empty;

            txtSuppCode.Text = string.Empty;
            txtSuppName.Text = string.Empty;
            txtFindSupplier.Text = string.Empty;
            txtFindPONo.Text = string.Empty;
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
        }
        #endregion

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ConsRecNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "ADJ" + seperator + "1" + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.POrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "C" + seperator+"A");
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

        #endregion

        #region Pending Orders

        private void GetPendingPurchaseOrders(string _fromDate, string _toDate, string _supCode, string _docNo, Boolean _showErrMsg)
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
                _paramPurchaseOrder.Poh_sub_tp = "C";

                DataTable pending_list = CHNLSVC.Inventory.GetAllPendingPurchaseOrderDataTable(_paramPurchaseOrder);

                if (pending_list.Rows.Count >= 0)
                {
                    dvPendingPO.AutoGenerateColumns = false;
                    dvPendingPO.DataSource = pending_list;
                }
                else
                {
                    if (_showErrMsg == true)
                    {
                        pending_list = null;
                        MessageBox.Show("No pending requests found!", "Pending Requests", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        dvPendingPO.AutoGenerateColumns = false;
                        dvPendingPO.DataSource = pending_list;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        #endregion

        public ConsignmentReceiveNote()
        {
            InitializeComponent();
        }

        private void GoodsReceivedNote_Load(object sender, EventArgs e)
        {
            try
            {
                ClearScreen();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
          
          /*
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "GRN", 1, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "GRN";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = true; //direction always (-) for change status
            RPH.Tuh_doc_no = txtPONo.Text;
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
            */
            try
            {
                Int32 _userSeqNo = 0;
                _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "ADJ", 1, BaseCls.GlbUserComCode);

                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_doc_no = txtPONo.Text; ;//_selectedReqNo;
                _inputReptPickHeader.Tuh_doc_tp = "ADJ";
                _inputReptPickHeader.Tuh_ischek_itmstus = true;//false; not sure
                _inputReptPickHeader.Tuh_ischek_reqqty = true;//false; not sure
                _inputReptPickHeader.Tuh_ischek_simitm = true;//false; not sure
                _inputReptPickHeader.Tuh_session_id = BaseCls.GlbUserSessionID;
                _inputReptPickHeader.Tuh_usr_com = BaseCls.GlbUserComCode;
                _inputReptPickHeader.Tuh_usr_id = BaseCls.GlbUserID;
                _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;
                _inputReptPickHeader.Tuh_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;

                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(_inputReptPickHeader);
                if (affected_rows == 1)
                {
                    return _userSeqNo;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
                return 0;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        private void LoadPOItems(string _poNo)
        {
            try
            {



                //Get Invoice Items Details
                DataTable po_items = CHNLSVC.Inventory.GetPOItemsDataTable(BaseCls.GlbUserComCode, _poNo,1);
                if (po_items.Rows.Count > 0)
                {
                    dvDOItems.Enabled = true;

                    // Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, _poNo, 1);
                    int user_seq_num = 0;
                    //Need to check Whether that is there any record in temp_pick_hdr table in SCMREP DB.
                    try
                    {
                        user_seq_num = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, _poNo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No records in report db.\n" + ex.Message);
                        return;
                    }


                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "DO");
                    //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "GRN");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in po_items.Rows)
                            {
                                
                                MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, row["PODI_ITM_CD"].ToString());
                                if (_mstItm.Mi_is_ser1 == -1)
                                {
                                    var _scanItems1 = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Tus_qty) });
                                    foreach (var itm1 in _scanItems1)
                                    {
                                        if (itm1.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString())
                                        {
                                            row["GRN_QTY"] = itm1.theSum;
                                        }
                                    }

                                }
                                else
                                {
                                    if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                                    {
                                        row["GRN_QTY"] = itm.theCount; // Current scan qty
                                    }
                                }
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
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = po_items;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //get all from sat_itm
        }

        private void dvDOItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvDOItems.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    #region Add Serials
                    if (e.ColumnIndex == 0)
                    {
                        //string _itemstatus = dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_stus"].Value.ToString();
                        decimal _grnQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["PODI_BAL_QTY"].Value.ToString());
                        decimal _scanQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["PickQty"].Value.ToString());

                        if (_grnQty <= 0) return;
                        if (_grnQty <= _scanQty) return;


                        CommonSearch.CommonInScan _commonInScan = new CommonSearch.CommonInScan();
                        _commonInScan.ModuleTypeNo = 1;
                        _commonInScan.ScanDocument = txtPONo.Text.ToString();
                        //  _commonInScan.DocumentType = "GRN";
                        _commonInScan.DocumentType = "ADJ";
                        _commonInScan.PopupItemCode = dvDOItems.Rows[e.RowIndex].Cells["PODI_ITM_CD"].Value.ToString();
                        _commonInScan.ItemStatus = "CONS";
                        _commonInScan.ItemLineNo = Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["PODI_LINE_NO"].Value.ToString());
                        _commonInScan.UnitCost = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["UNIT_PRICE"].Value.ToString());
                        _commonInScan.UnitPrice = 0;
                        _commonInScan.DocQty = _grnQty;
                        _commonInScan.ScanQty = _scanQty;

                        _commonInScan.Location = new Point(((this.Width - _commonInScan.Width) / 2), ((this.Height - _commonInScan.Height) / 2) + 50);
                        _commonInScan.ShowDialog();

                        LoadPOItems(txtPONo.Text.ToString());

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
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void DeliveryOrderCustomer_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Enabled == true)
                {
                    //MessageBox.Show("OK"); 

                    LoadPOItems(txtPONo.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
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
                Process();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Process()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtPONo.Text))
                {
                    MessageBox.Show("Select the Request no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkManualRef.Checked)
                    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                    {
                        MessageBox.Show("You have not entered a valid manual document no.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "N/A";
                if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                //int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
                int resultDate = DateTime.Compare(dtpPODate.Value.Date, dtpDODate.Value.Date);
                if (resultDate > 0)
                {
                    MessageBox.Show("Request date should be greater than or equal to Consignment date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDODate, lblH1, dtpDODate.Value.Date.ToString(), out _allowCurrentTrans) == false)
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

                Cursor.Current = Cursors.WaitCursor;

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                //Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, txtPONo.Text, 1);
                //reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "GRN");

                //Get the user seq no for selected requestNo.
                int _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, txtPONo.Text.Trim());
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "ADJ");
                //_reptPickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "ADJ");
                if (reptPickSerialsList == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                // reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "GRN");
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ");

                #region Check Referance Date and the Doc Date
                if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }
                #endregion

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


                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(BaseCls.GlbUserComCode, txtPONo.Text, BaseCls.GlbUserDefLoca);

                if (reptPickSerialsList != null)
                {
                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
                            if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
                            {
                                _invItem.Actual_qty = itm.theCount; // Current scan qty
                            }
                    }

                }
                //Done by Nadeeka 02-03-2015 
                foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                {
                    Int32 _count = 0;
                    List<ReptPickSerials> oSelectedItms = new List<ReptPickSerials>();
                    oSelectedItms = reptPickSerialsList.FindAll(x => x.Tus_itm_cd == item.MasterItem.Mi_cd && x.Tus_itm_stus == item.Podi_itm_stus);
                    foreach (ReptPickSerials _itemSer in oSelectedItms)
                    {
                        _count++;
                    }

                    //if (item.Podi_bal_qty < _count || _count==0) //kapila 30/5/2016
                    if (item.Podi_bal_qty < _count)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Following item serials and item quantities not matching. " + item.Podi_itm_cd, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                  
                }


                //foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                //{
                //    Int32 _count = 0;

                //    foreach (ReptPickSerials items in _reptPickSerialList)
                //    {

                //        if (item.Podi_line_no == items.Tus_itm_line)
                //        {
                //            _count++;

                //        }

                //        //_tempPickSerialList = _reptPickSerialList.Where(x => x.Tus_itm_cd.Equals(item.MasterItem.Mi_cd) && x.Tus_itm_line==items.Tus_itm_line ).ToList();
                //        //item.Actual_qty = _tempPickSerialList.Count;
                //    }
                //    item.Actual_qty = Convert.ToDecimal(_count);
                //    _purList.Add(item);
                //}

                InventoryHeader _invHeader = new InventoryHeader();

                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _invHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _invHeader.Ith_channel = string.Empty;
                    }
                }

                _invHeader.Ith_com = BaseCls.GlbUserComCode;//
                _invHeader.Ith_loc = BaseCls.GlbUserDefLoca;//
                _invHeader.Ith_doc_date = dtpDODate.Value.Date;//
                _invHeader.Ith_doc_year = dtpDODate.Value.Date.Year;//
                _invHeader.Ith_direct = true;//
                _invHeader.Ith_doc_tp = "ADJ";//"GRN";
                _invHeader.Ith_cate_tp = "CONSIGN";//"NOR";
                //  _invHeader.Ith_sub_tp = "LOCAL";  TODO:
                _invHeader.Ith_bus_entity = txtSuppCode.Text;//
                if (chkManualRef.Checked == true)
                {
                    _invHeader.Ith_is_manual = true;//
                }
                else
                {
                    _invHeader.Ith_is_manual = false;//
                }
                _invHeader.Ith_manual_ref = txtManualRefNo.Text;//
                _invHeader.Ith_remarks = txtRemarks.Text;//
                _invHeader.Ith_stus = "A";//
                _invHeader.Ith_cre_by = BaseCls.GlbUserID;//
                _invHeader.Ith_cre_when = CHNLSVC.Security.GetServerDateTime().Date;// DateTime.Now;//
                _invHeader.Ith_mod_by = BaseCls.GlbUserID;//
                _invHeader.Ith_mod_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;//
                _invHeader.Ith_session_id = BaseCls.GlbUserSessionID;//
                _invHeader.Ith_oth_docno = txtPONo.Text;//
                _invHeader.Ith_acc_no = "CONS_INS";//?????
                _invHeader.Ith_sub_tp = "CONS"; //????

                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "CONS";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "CONS";
                _masterAuto.Aut_year = null;

                result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo);
                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (chkManualRef.Checked == false)
                    {
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        BaseCls.GlbReportName = string.Empty; //add on 16-Jul-2013
                        _view.GlbReportName = string.Empty; //add on 16-Jul-2013

                        if (_invHeader.Ith_direct == true) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                        {
                            if (_invHeader.Ith_direct == true) _view.GlbReportName = "Inward_Docs.rpt";
                            else _view.GlbReportName = "Outward_Docs.rpt";
                        }
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                        {
                            if (_invHeader.Ith_direct == true) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                            else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                        }
                        else
                        { 
                            if (_invHeader.Ith_direct == true) _view.GlbReportName = "Inward_Docs.rpt";
                            else _view.GlbReportName = "Outward_Docs.rpt";
                        }
                        // BaseCls.GlbReportName = string.Empty; //add NEW
                        // _view.GlbReportName = string.Empty; //add on NEW

                        // _view.GlbReportName = "Inward_Docs.rpt";
                        _view.GlbReportDoc = documntNo;
                        _view.Show();
                        _view = null;
                    }
                    ClearScreen();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Save Failed!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            dtpFromDate.Focus();
        }

        private void dvPendingInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }   

        private void DeliveryOrderCustomer_Activated(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                dtpToDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dvDOItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                if (  !string.IsNullOrEmpty(txtManualRefNo.Text) && chkManualRef.Checked == false)
                {
                    //TODO: Check N Validate N Load Serials.
                    string _msg = string.Empty;
                    CHNLSVC.Inventory.GetSCMAOD(dtpDODate.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, txtSuppCode.Text.Trim(), txtManualRefNo.Text.Trim(), txtPONo.Text.Trim(), BaseCls.GlbUserID, out _msg);
                    if (string.IsNullOrEmpty(_msg))
                 
                        
                        LoadPOItems(txtPONo.Text.Trim());
                    else MessageBox.Show(_msg, "Upload Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
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
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_GRN");
                    if (_NextNo != 0)
                        txtManualRefNo.Text = _NextNo.ToString();
                    else
                        txtManualRefNo.Text = string.Empty;
                }
                else
                    txtManualRefNo.Text = string.Empty;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManualRef.Checked)
                {
                    SetManualRefNo();
                    txtManualRefNo.Enabled = false;
                }
                else
                {
                    txtManualRefNo.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dvDOSerials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvDOSerials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvDOSerials.RowCount > 0)
                {
                    int _rowCount = e.RowIndex;
                    if (_rowCount != -1)
                    {
                        if (dvDOSerials.Columns[e.ColumnIndex].Name == "ser_Remove")
                            if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                OnRemoveFromSerialGrid(_rowCount);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

                Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", BaseCls.GlbUserComCode, txtPONo.Text, 1);

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

                LoadPOItems(txtPONo.Text);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(BaseCls.GlbCommonErrorMessage, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally { this.Cursor = Cursors.Default;
            CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        

        #region Search Supplier Code wise
        private void txtFindSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFindSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Supplier_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetPO.Focus();
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

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindSupplier_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Supplier_Click(null, null);
        }

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

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        #region Search PO No wise

        private void txtFindPONo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_PO_Click(null, null);
        }

        private void txtFindPONo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindPONo.Text)) return;

                PurchaseOrder _hdr = CHNLSVC.Inventory.GetPOHeader(BaseCls.GlbUserComCode, txtFindPONo.Text.Trim(), "L");
                if (_hdr == null)
                {
                    MessageBox.Show("Please select the valid consignment request no", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindPONo.Text = string.Empty;
                    txtFindPONo.Focus();
                    return;
                }
                else
                {
                    if (_hdr.Poh_sub_tp != "C")
                    {
                        MessageBox.Show("Please select the valid consignment request no", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFindPONo.Text = string.Empty;
                        txtFindPONo.Focus();
                        return;
                    }
                    if (_hdr.Poh_stus == "P")
                    {
                        MessageBox.Show("Request has not approved yet!", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                        //POH_STUS='P' then 'PENDING'
                        //POH_STUS='A' then 'APPROVED'
                        // POH_STUS='C' then 'CANCELED'
                        // POH_STUS='F' then 'FINISHED'
                    }
                    if (_hdr.Poh_stus == "F")
                    {
                        MessageBox.Show("Request compleated!", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_hdr.Poh_stus == "C")
                    {
                        MessageBox.Show("This is a cancelled request!", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.btnGetPO_Click(null, null);
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

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFindPONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_PO_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetPO.Focus();
        }

        private void btnSearch_PO_Click(object sender, EventArgs e)
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
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POrder);
                DataTable _result = CHNLSVC.CommonSearch.Search_PurchaseOrders(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindPONo;
                _CommonSearch.ShowDialog();
                txtFindPONo.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        private void btnGetPO_Click(object sender, EventArgs e)
        {
            try
            {
                string reqNo = txtFindPONo.Text.Trim();
                DateTime FromDt = dtpFromDate.Value.Date;
                DateTime ToDt = dtpToDate.Value.Date;
                string supplier = txtFindSupplier.Text.Trim();

                ClearBody();
                dtpFromDate.Value = FromDt;
                dtpToDate.Value = ToDt;
                txtFindSupplier.Text = supplier;
                txtFindPONo.Text = reqNo;

                Cursor.Current = Cursors.WaitCursor;
                GetPendingPurchaseOrders(dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), supplier, reqNo, true);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dvPendingPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvPendingPO.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    txtPONo.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_DOC_NO"].Value.ToString();
                    dtpPODate.Value = Convert.ToDateTime(dvPendingPO.Rows[e.RowIndex].Cells["POH_DT"].Value.ToString()).Date;
                    txtPORefNo.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_REF"].Value.ToString();
                    txtSuppCode.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_SUPP"].Value.ToString();
                    txtSuppName.Text = dvPendingPO.Rows[e.RowIndex].Cells["MBE_NAME"].Value.ToString();
                    //_profitCenter = dvPendingPO.Rows[e.RowIndex].Cells["POH_PROFIT_CD"].Value.ToString();
                    LoadPOItems(txtPONo.Text.ToString());
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dvPendingPO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtManualRefNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrintBC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDocNo.Text))
            {
                MessageBox.Show("Please select the document number", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDocNo.Focus();
                return;
            }

            BaseCls.GlbReportDoc = txtDocNo.Text;
            MultipleBarcode _barcode = new MultipleBarcode();
            _barcode.Show();
        }

        private void btnSrchDocNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ConsRecNo);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null, Convert.ToDateTime(dtpFromDate.Value).Date, Convert.ToDateTime(dtpToDate.Value).Date);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocNo;
                _CommonSearch.ShowDialog();
                txtDocNo.Select();
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

        }

     



    }
}
