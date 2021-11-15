using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Barcode;

namespace FF.WindowsERPClient.Inventory
{
    public partial class GoodsReceivedNote : Base
    {
        private List<InvoiceItem> invoice_items = null;
        private List<InvoiceItem> invoice_items_bind = null;
        private List<PurchaseOrderDelivery> podel_items = null;
        private List<InventoryRequestItem> ScanItemList = null;
        private Int32 _selRow = 0;
        private string _profitCenter = "";
        private bool IsGrn = false;
        private bool _isInterCompanyGRN = false;
        private string _poType = string.Empty;
        DataTable po_items_new = new DataTable();   //kapila 5/8/2015

        private void ProcessUnscanItems()
        {
            //try
            //{
            //    if (CheckServerDateTime() == false) return;

            //    if (string.IsNullOrEmpty(txtPONo.Text))
            //    {
            //        MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    if (chkManualRef.Checked)
            //        if (string.IsNullOrEmpty(txtManualRefNo.Text))
            //        {
            //            MessageBox.Show("You do not enter a valid manual document no.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return;
            //        }

            //    if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "N/A";
            //    if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
            //    if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

            //    //int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
            //    int resultDate = DateTime.Compare(dtpPODate.Value.Date, dtpDODate.Value.Date);
            //    if (resultDate > 0)
            //    {
            //        MessageBox.Show("GRN date should be greater than or equal to PO date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    bool _allowCurrentTrans = false;
            //    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDODate, lblH1, dtpDODate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            //    {
            //        if (_allowCurrentTrans == true)
            //        {
            //            if (dtpDODate.Value.Date != DateTime.Now.Date)
            //            {
            //                dtpDODate.Enabled = true;
            //                MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                dtpDODate.Focus();
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            dtpDODate.Enabled = true;
            //            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            dtpDODate.Focus();
            //            return;
            //        }
            //    }

            //    Cursor.Current = Cursors.WaitCursor;

            //////    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            //    InventoryHeader invHdr = new InventoryHeader();
            //    string documntNo = "";
            //    Int32 result = -99;

            //    Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, txtPONo.Text, 1);
            //    ////reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "GRN");

            //    ////if (reptPickSerialsList == null)
            //    ////{
            //    ////    Cursor.Current = Cursors.Default;
            //    ////    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    ////    return;
            //    ////}

            //    ////List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
            //    ////reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "GRN");


            //    ////#region Check Duplicate Serials

            //    ////var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

            //    ////string _duplicateItems = string.Empty;
            //    ////bool _isDuplicate = false;
            //    ////if (_dup != null)
            //    ////    if (_dup.Count > 0)
            //    ////        foreach (Int32 _id in _dup)
            //    ////        {
            //    ////            Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
            //    ////            if (_counts > 1)
            //    ////            {
            //    ////                _isDuplicate = true;
            //    ////                var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
            //    ////                foreach (string _str in _item)
            //    ////                    if (string.IsNullOrEmpty(_duplicateItems))
            //    ////                        _duplicateItems = _str;
            //    ////                    else
            //    ////                        _duplicateItems += "," + _str;
            //    ////            }
            //    ////        }
            //    ////if (_isDuplicate)
            //    ////{
            //    ////    Cursor.Current = Cursors.Default;
            //    ////    MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    ////    return;
            //    ////}

            //    ////#endregion Check Duplicate Serials

            //    reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = BaseCls.GlbUserComCode; i.Tus_exist_grndt = dtpDODate.Value.Date; i.Tus_exist_supp = txtSuppCode.Text.ToString(); i.Tus_orig_grncom = BaseCls.GlbUserComCode; i.Tus_orig_grndt = dtpDODate.Value.Date; i.Tus_orig_supp = txtSuppCode.Text.ToString(); });

            //    List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
            //    _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(BaseCls.GlbUserComCode, txtPONo.Text, BaseCls.GlbUserDefLoca);

            //    if (reptPickSerialsList != null)
            //    {
            //        var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
            //        foreach (var itm in _scanItems)
            //        {
            //            foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
            //                if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
            //                {
            //                    _invItem.Actual_qty = itm.theCount; // Current scan qty
            //                }
            //        }
            //    }

            //    InventoryHeader _invHeader = new InventoryHeader();

            //    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            //    foreach (DataRow r in dt_location.Rows)
            //    {
            //        // Get the value of the wanted column and cast it to string
            //        _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
            //        if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
            //    }

            //    _invHeader.Ith_com = BaseCls.GlbUserComCode;
            //    _invHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            //    _invHeader.Ith_doc_date = dtpDODate.Value.Date;
            //    _invHeader.Ith_doc_year = dtpDODate.Value.Date.Year;
            //    _invHeader.Ith_direct = true;
            //    _invHeader.Ith_doc_tp = "GRN";
            //    if (_poType == "I")
            //    {
            //        _invHeader.Ith_cate_tp = "IMPORTS";
            //        _invHeader.Ith_sub_tp = "IMPORTS";
            //    }
            //    else
            //    {
            //        _invHeader.Ith_cate_tp = "LOCAL";
            //        _invHeader.Ith_sub_tp = "LOCAL";
            //    }
            //    _invHeader.Ith_bus_entity = txtSuppCode.Text;
            //    if (chkManualRef.Checked == true) _invHeader.Ith_is_manual = true; else _invHeader.Ith_is_manual = false;
            //    _invHeader.Ith_manual_ref = txtManualRefNo.Text;
            //    _invHeader.Ith_remarks = txtRemarks.Text;
            //    _invHeader.Ith_stus = "A";
            //    _invHeader.Ith_cre_by = BaseCls.GlbUserID;
            //    _invHeader.Ith_cre_when = DateTime.Now;
            //    _invHeader.Ith_mod_by = BaseCls.GlbUserID;
            //    _invHeader.Ith_mod_when = DateTime.Now;
            //    _invHeader.Ith_session_id = GlbUserSessionID;
            //    _invHeader.Ith_oth_docno = txtPONo.Text;

            //    MasterAutoNumber _masterAuto = new MasterAutoNumber();
            //    _masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca; ;
            //    _masterAuto.Aut_cate_tp = "LOC";
            //    _masterAuto.Aut_direction = null;
            //    _masterAuto.Aut_modify_dt = null;
            //    _masterAuto.Aut_moduleid = "GRN";
            //    _masterAuto.Aut_number = 0;
            //    _masterAuto.Aut_start_char = "GRN";
            //    _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

            //    //Add by Chamal 23-May-2014
            //    int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, txtPONo.Text, _invHeader.Ith_doc_date.Date, BaseCls.GlbUserID);
            //    reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
            //    if (_invHeader.Ith_doc_tp == "GRN")
            //    {
            //        if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
            //        {
            //            reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
            //            reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
            //        }
            //        if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
            //        {
            //            reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
            //            reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
            //        }
            //    }

            //    result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo);
            //    if (result != -99 && result >= 0)
            //    {
            //        Cursor.Current = Cursors.Default;
            //        if (chkManualRef.Checked == false)
            //        {
            //            DialogResult _res = MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\n" + "Do you want print now?", "Goods Received Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //            txtGRNno.Text = documntNo;
            //            if (_res == System.Windows.Forms.DialogResult.Yes)
            //            {
            //                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            //                BaseCls.GlbReportTp = "INWARD";
            //                if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
            //                    _view.GlbReportName = "SInward_Docs.rpt";
            //                else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
            //                    _view.GlbReportName = "Dealer_Inward_Docs.rpt";
            //                else
            //                    _view.GlbReportName = "Inward_Docs.rpt";
            //                _view.GlbReportDoc = documntNo;
            //                _view.Show();
            //                _view = null;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Goods Received Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        ClearScreen();
            //    }
            //    else
            //    {
            //        Cursor.Current = Cursors.Default;
            //        MessageBox.Show(documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception err)
            //{
            //    Cursor.Current = Cursors.Default;
            //    CHNLSVC.CloseChannel();
            //    MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        #region Clear Screen

        private void ClearScreen()
        {
            try
            {
                Cursor.Current = Cursors.Default;
                _poType = string.Empty;
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
                txtGRNno.Text = "";

                List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                _emptyserList = null;
                dvDOSerials.AutoGenerateColumns = false;
                dvDOSerials.DataSource = _emptyserList;

                List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                _emptyinvoiceitemList = null;
                dvDOItems.AutoGenerateColumns = false;
                dvDOItems.DataSource = _emptyinvoiceitemList;
                bool _allowCurrentTrans = false;

                if (BaseCls.GlbUserComCode == "ABL" || BaseCls.GlbUserComCode == "LRP" || BaseCls.GlbUserComCode == "ARL")
                {
                    lblImportsPO.Visible = false; //Sanjeewa 2016-10-21 
                }
                
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDODate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                GetUserPermission();
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

        #endregion Clear Screen

        #region Common Searching Area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.GRNNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TempGRNNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "GRN" + seperator + 1 + seperator);
                        break;
                    }
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

                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area

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
                _paramPurchaseOrder.Poh_sub_tp = "N";

                DataTable pending_list = CHNLSVC.Inventory.GetAllPendingPurchaseOrderDataTable(_paramPurchaseOrder);

                _paramPurchaseOrder = new PurchaseOrder();

                _paramPurchaseOrder.Poh_com = BaseCls.GlbUserComCode;
                _paramPurchaseOrder.MasterLocation = new MasterLocation() { Ml_loc_cd = BaseCls.GlbUserDefLoca };
                _paramPurchaseOrder.Poh_stus = "A";
                _paramPurchaseOrder.MasterBusinessEntity = new MasterBusinessEntity() { Mbe_tp = "S" };
                _paramPurchaseOrder.FromDate = _fromDate;
                _paramPurchaseOrder.ToDate = _toDate;
                _paramPurchaseOrder.Poh_supp = _supCode;
                _paramPurchaseOrder.Poh_doc_no = _docNo;
                _paramPurchaseOrder.Poh_sub_tp = "S";

                DataTable pending_listSup = CHNLSVC.Inventory.GetAllPendingPurchaseOrderDataTable(_paramPurchaseOrder);

                pending_list.Merge(pending_listSup);

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

        public GoodsReceivedNote()
        {
            InitializeComponent();
            GetUserPermission();
            pnlTemp.Size = new Size(656, 158);
            pnlUnscan.Size = new Size(882, 389);
            pnlSI.Location = new Point(533,44);
        }

        private void GetUserPermission()
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10066)) _isInterCompanyGRN = true;
            else _isInterCompanyGRN = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10108))
            {
                optChange.Enabled = true;
                optNew.Enabled = true;
            }
            else
            {
                optChange.Enabled = false;
                optNew.Enabled = false;
            }

            DataTable _dt = CHNLSVC.Inventory.GetPendingTemSavedGRN(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null);
            grvPending.AutoGenerateColumns = false;
            grvPending.DataSource = _dt;
        }

        private void GoodsReceivedNote_Load(object sender, EventArgs e)
        {
            ClearScreen();
        }

        #region Generate new user seq no

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return 0;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Generate new user seq no

        private void LoadPOItemsbyItem(string _poNo, string _item, Int32 _line)
        {
            try
            {
                //Get Invoice Items Details
                //kapila 20/11/2015 (view only pending items)
                DataTable po_items = null;
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10118))
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(BaseCls.GlbUserComCode, _poNo, 0);
                else
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(BaseCls.GlbUserComCode, _poNo, 1);

                if (po_items.Rows.Count > 0)
                {

                    dvDOItems.Enabled = true;

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, _poNo, 1);
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "GRN");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.Where(z => z.Tus_itm_cd == _item && z.Tus_itm_line == _line).GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in po_items.Rows)
                            {
                                //kapila 8/9/2015 pick qty bug for decimal allow items
                                MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, row["PODI_ITM_CD"].ToString());
                                if (_mstItm.Mi_is_ser1 == -1)
                                {
                                    var _scanItems1 = _serList.Where(z => z.Tus_itm_cd == row["PODI_ITM_CD"].ToString()).GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Tus_qty) });
                                    foreach (var itm1 in _scanItems1)
                                    {

                                        row["GRN_QTY"] = itm1.theSum;
                                        break;
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

                    //kapila 10/8/2015
                    DataTable _dtPO = CHNLSVC.Inventory.getTempPOItems(BaseCls.GlbUserComCode, _poNo);

                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "GRN");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in _dtPO.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                                {
                                    row["GRN_QTY"] = itm.theCount; // Current scan qty
                                }
                            }
                        }
                    }

                    po_items_new = po_items;    //kapila 5/8/2015

                    if (_dtPO.Rows.Count > 0)
                        po_items.Merge(_dtPO);

                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = po_items;

                    try
                    {
                        dvDOItems.CurrentCell = dvDOItems[4, _selRow + 1];      //kapila 20/11/2015
                    }
                    catch
                    { }

                    dvUnscanItems.AutoGenerateColumns = false;
                    dvUnscanItems.DataSource = po_items;
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
        private void LoadPOItems(string _poNo)
        {
            try
            {
                //Get Invoice Items Details
                //kapila 20/11/2015 (view only pending items)
                DataTable po_items = null;
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10118))
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(BaseCls.GlbUserComCode, _poNo, 0);
                else
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(BaseCls.GlbUserComCode, _poNo, 1);

                if (po_items.Rows.Count > 0)
                {

                    dvDOItems.Enabled = true;

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, _poNo, 1);
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "GRN");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in po_items.Rows)
                            {
                                //kapila 8/9/2015 pick qty bug for decimal allow items
                                MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, row["PODI_ITM_CD"].ToString());
                                if (_mstItm.Mi_is_ser1 == -1)
                                {
                                    var _scanItems1 = _serList.Where(z => z.Tus_itm_cd == row["PODI_ITM_CD"].ToString()).GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Tus_qty) });
                                    foreach (var itm1 in _scanItems1)
                                    {
                                        //  if (itm1.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString())
                                        //  {
                                        row["GRN_QTY"] = itm1.theSum;

                                        //  }
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

                    //kapila 10/8/2015
                    DataTable _dtPO = CHNLSVC.Inventory.getTempPOItems(BaseCls.GlbUserComCode, _poNo);

                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "GRN");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in _dtPO.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                                {
                                    row["GRN_QTY"] = itm.theCount; // Current scan qty
                                }
                            }
                        }
                    }

                    po_items_new = po_items;    //kapila 5/8/2015

                    if (_dtPO.Rows.Count > 0)
                        po_items.Merge(_dtPO);

                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = po_items;

                    try
                    {
                        dvDOItems.CurrentCell = dvDOItems[4, _selRow + 1];      //kapila 20/11/2015
                    }
                    catch
                    { }

                    dvUnscanItems.AutoGenerateColumns = false;
                    dvUnscanItems.DataSource = po_items;
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

                        //kapila 19/11/2015
                        _selRow = e.RowIndex;
                        CommonSearch.CommonInScan _commonInScan = new CommonSearch.CommonInScan();
                        _commonInScan.ModuleTypeNo = 1;
                        _commonInScan.ScanDocument = txtPONo.Text.ToString();
                        _commonInScan.DocumentType = "GRN";
                        _commonInScan.PopupItemCode = dvDOItems.Rows[e.RowIndex].Cells["PODI_ITM_CD"].Value.ToString();
                        _commonInScan.ItemStatus = dvDOItems.Rows[e.RowIndex].Cells["POD_ITM_STUS"].Value.ToString();
                        _commonInScan.ItemLineNo = Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["PODI_LINE_NO"].Value.ToString());
                        _commonInScan.UnitCost = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["UNIT_PRICE"].Value.ToString());
                        _commonInScan.UnitPrice = 0;
                        _commonInScan.DocQty = _grnQty;
                        _commonInScan.ScanQty = _scanQty;
                        _commonInScan.IsNew = dvDOItems.Rows[e.RowIndex].Cells["IS_NEW"].Value.ToString();

                        _commonInScan.Location = new Point(((this.Width - _commonInScan.Width) / 2), ((this.Height - _commonInScan.Height) / 2) + 50);
                        _commonInScan.ShowDialog();

                      //  LoadPOItems(txtPONo.Text.ToString());
                        //kapila 16/12/2015
                        LoadPOItemsbyItem(txtPONo.Text.ToString(), dvDOItems.Rows[e.RowIndex].Cells["PODI_ITM_CD"].Value.ToString(), Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["PODI_LINE_NO"].Value.ToString()));
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

        private void Process()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtPONo.Text))
                {
                    MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                //int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
                int resultDate = DateTime.Compare(dtpPODate.Value.Date, dtpDODate.Value.Date);
                if (resultDate > 0)
                {
                    MessageBox.Show("GRN date should be greater than or equal to PO date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, txtPONo.Text, 1);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "GRN");

                if (reptPickSerialsList == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "GRN");

                #region Check Reference Date and the Doc Date

                //if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                //{
                //    Cursor.Current = Cursors.Default;
                //    return;
                //}

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

                reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = BaseCls.GlbUserComCode; i.Tus_exist_grndt = dtpDODate.Value.Date; i.Tus_exist_supp = txtSuppCode.Text.ToString(); i.Tus_orig_grncom = BaseCls.GlbUserComCode; i.Tus_orig_grndt = dtpDODate.Value.Date; i.Tus_orig_supp = txtSuppCode.Text.ToString(); });

                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(BaseCls.GlbUserComCode, txtPONo.Text, BaseCls.GlbUserDefLoca);

                if (reptPickSerialsList != null)
                {
                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
                        {
                            //kapila 8/9/2015 pick qty bug for decimal allow items
                            MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _invItem.MasterItem.Mi_cd);
                            if (_mstItm.Mi_is_ser1 == -1)
                            {
                                var _scanItems1 = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Tus_qty) });
                                foreach (var itm1 in _scanItems1)
                                {
                                    if (itm1.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd)
                                    {
                                        _invItem.Actual_qty = itm1.theSum;
                                    }
                                }
                            }
                            else
                            {
                                if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
                                {
                                    _invItem.Actual_qty = itm.theCount; // Current scan qty
                                }
                            }
                        }
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
                    if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
                }

                _invHeader.Ith_com = BaseCls.GlbUserComCode;
                _invHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                _invHeader.Ith_doc_date = dtpDODate.Value.Date;
                _invHeader.Ith_doc_year = dtpDODate.Value.Date.Year;
                _invHeader.Ith_direct = true;
                _invHeader.Ith_doc_tp = "GRN";
                if (_poType == "I")
                {
                    _invHeader.Ith_cate_tp = "IMPORTS";
                    _invHeader.Ith_sub_tp = "IMPORTS";
                }
                else
                {
                    _invHeader.Ith_cate_tp = "LOCAL";
                    _invHeader.Ith_sub_tp = "LOCAL";
                }
                _invHeader.Ith_bus_entity = txtSuppCode.Text;
                if (chkManualRef.Checked == true) _invHeader.Ith_is_manual = true; else _invHeader.Ith_is_manual = false;
                _invHeader.Ith_manual_ref = txtManualRefNo.Text;
                _invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = BaseCls.GlbUserID;
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = BaseCls.GlbUserID;
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                _invHeader.Ith_oth_docno = txtPONo.Text;
                //kapila 12/12/2015
                if (chkTemp.Checked == true)
                {
                    _invHeader.Ith_anal_10 = true;
                    _invHeader.Ith_anal_2 = txtGRNno.Text;
                }

                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca; ;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "GRN";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "GRN";
                _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

                //Add by Chamal 23-May-2014
                int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, txtPONo.Text, _invHeader.Ith_doc_date.Date, BaseCls.GlbUserID);
                reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
                if (_invHeader.Ith_doc_tp == "GRN")
                {
                    if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                    if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                }

                result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo);
                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    if (chkManualRef.Checked == false)
                    {
                        DialogResult _res = MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\n" + "Do you want print now?", "Goods Received Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        txtGRNno.Text = documntNo;
                        if (_res == System.Windows.Forms.DialogResult.Yes)
                        {
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            if (BaseCls.GlbUserComCode=="AAL") // tharanga 2017/07/17 ABE compny
                            {
                                // Tharanga 
                                BaseCls.GlbReportTp = "GRN";
                                _view.GlbReportName = "GRNreport.rpt";
                            }
                            else
                            { 
                            BaseCls.GlbReportTp = "INWARD";
                            if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                                _view.GlbReportName = "SInward_Docs.rpt";
                            else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                                _view.GlbReportName = "Dealer_Inward_Docs.rpt";

                            else
                                _view.GlbReportName = "Inward_Docs.rpt";
                             }
                            // Tharanga 
                                //_view.GlbReportName = "GRNreport.rpt";
                          
                            _view.GlbReportDoc = documntNo;
                            _view.Show();
                            _view = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Goods Received Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ClearScreen();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                ClearScreen();
                dtpFromDate.Focus();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void ProcessTempSave()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtPONo.Text))
                {
                    MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                //int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
                int resultDate = DateTime.Compare(dtpPODate.Value.Date, dtpDODate.Value.Date);
                if (resultDate > 0)
                {
                    MessageBox.Show("GRN date should be greater than or equal to PO date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, txtPONo.Text, 1);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "GRN");

                if (reptPickSerialsList == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "GRN");

                #region Check Reference Date and the Doc Date

                //if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                //{
                //    Cursor.Current = Cursors.Default;
                //    return;
                //}

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

                reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = BaseCls.GlbUserComCode; i.Tus_exist_grndt = dtpDODate.Value.Date; i.Tus_exist_supp = txtSuppCode.Text.ToString(); i.Tus_orig_grncom = BaseCls.GlbUserComCode; i.Tus_orig_grndt = dtpDODate.Value.Date; i.Tus_orig_supp = txtSuppCode.Text.ToString(); });

                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(BaseCls.GlbUserComCode, txtPONo.Text, BaseCls.GlbUserDefLoca);

                if (reptPickSerialsList != null)
                {
                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
                        {
                            //kapila 8/9/2015 pick qty bug for decimal allow items
                            MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _invItem.MasterItem.Mi_cd);
                            if (_mstItm.Mi_is_ser1 == -1)
                            {
                                var _scanItems1 = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Tus_qty) });
                                foreach (var itm1 in _scanItems1)
                                {
                                    if (itm1.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd)
                                    {
                                        _invItem.Actual_qty = itm1.theSum;
                                    }
                                }
                            }
                            else
                            {
                                if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
                                {
                                    _invItem.Actual_qty = itm.theCount; // Current scan qty
                                }
                            }
                        }
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
                    if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
                }

                _invHeader.Ith_com = BaseCls.GlbUserComCode;
                _invHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                _invHeader.Ith_doc_date = dtpDODate.Value.Date;
                _invHeader.Ith_doc_year = dtpDODate.Value.Date.Year;
                _invHeader.Ith_direct = true;
                _invHeader.Ith_doc_tp = "GRN";
                if (_poType == "I")
                {
                    _invHeader.Ith_cate_tp = "IMPORTS";
                    _invHeader.Ith_sub_tp = "IMPORTS";
                }
                else
                {
                    _invHeader.Ith_cate_tp = "LOCAL";
                    _invHeader.Ith_sub_tp = "LOCAL";
                }
                _invHeader.Ith_bus_entity = txtSuppCode.Text;
                if (chkManualRef.Checked == true) _invHeader.Ith_is_manual = true; else _invHeader.Ith_is_manual = false;
                _invHeader.Ith_manual_ref = txtManualRefNo.Text;
                _invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "P";
                _invHeader.Ith_cre_by = BaseCls.GlbUserID;
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = BaseCls.GlbUserID;
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                _invHeader.Ith_oth_docno = txtPONo.Text;

                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca; ;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "GRN";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "GRN";
                _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

                //Add by Chamal 23-May-2014
                int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, txtPONo.Text, _invHeader.Ith_doc_date.Date, BaseCls.GlbUserID);
                reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
                if (_invHeader.Ith_doc_tp == "GRN")
                {
                    if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                    if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                }

                result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo, true);
                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    if (chkManualRef.Checked == false)
                    {
                        DialogResult _res = MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\n" + "Do you want print now?", "Goods Received Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        txtGRNno.Text = documntNo;
                        if (_res == System.Windows.Forms.DialogResult.Yes)
                        {
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            BaseCls.GlbReportTp = "INWARD";
                            if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                                _view.GlbReportName = "SInward_Docs.rpt";
                            else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                                _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                            else
                                _view.GlbReportName = "Inward_Docs.rpt";
                            _view.GlbReportDoc = documntNo;
                            _view.Show();
                            _view = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Goods Received Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ClearScreen();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dvDOItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtManualRefNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_isInterCompanyGRN && !string.IsNullOrEmpty(txtManualRefNo.Text) && chkManualRef.Checked == false)
                {
                    //TODO: Check N Validate N Load Serials.
                    string _msg = string.Empty;
                    CHNLSVC.Inventory.GetSCMDeliveryOrder(dtpDODate.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, txtSuppCode.Text.Trim(), txtManualRefNo.Text.Trim(), txtPONo.Text.Trim(), BaseCls.GlbUserID, out _msg);
                    if (string.IsNullOrEmpty(_msg))
                        LoadPOItems(txtPONo.Text.Trim());
                    else MessageBox.Show(_msg, "Upload Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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

                Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, txtPONo.Text, 1);

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
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Rooting for Serial Grid Events

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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Search Supplier Code wise

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

        private void txtFindPONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_PO_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetPO.Focus();
        }

        private void btnSearch_PO_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindPONo;
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

        #endregion Search PO No wise

        private void btnGetPO_Click(object sender, EventArgs e)
        {
            try
            {
                ClearBody();
                Cursor.Current = Cursors.WaitCursor;
                GetPendingPurchaseOrders(dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), txtFindSupplier.Text, txtFindPONo.Text, true);
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
                    _poType = dvPendingPO.Rows[e.RowIndex].Cells["POH_TP"].Value.ToString(); //Add by Chamal 31/07/2014
                    txtRemarks.Text = dvPendingPO.Rows[e.RowIndex].Cells["POH_REMARKS"].Value.ToString();// added by  Nadeeka 18-02-2015
                    LoadPOItems(txtPONo.Text.ToString());
                    txtManualRefNo.Clear();
                    txtVehicleNo.Clear();
                    //   txtRemarks.Clear();  
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

        private void dvPendingPO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        #region Upload Imports PO - Bond Nos

        private void lblImportsPO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnSave.Enabled == true)
            {
                if (pnlImportsPO.Visible == false)
                {
                    pnlImportsPO.Visible = true;
                    txtImpPONo.Focus();
                }
                else
                {
                    pnlImportsPO.Visible = false;
                    txtImpPONo.Text = "";
                    txtLCNo.Text = "";
                    txtSINo.Text = "";
                    txtImpSupp.Text = "";
                    txtBondDate.Text = "";
                }
            }
        }

        private void btnPnlImportsPOClose_Click(object sender, EventArgs e)
        {
            pnlImportsPO.Visible = false;
            txtImpPONo.Clear();
            txtSINo.Clear();
            txtLCNo.Clear();
        }

        private void txtImpPONo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtImpPONo.Text)) return;

                string _costSheetRef = string.Empty;
                DateTime _bondDate = DateTime.Now.Date;
                string _lcNo = string.Empty;
                string _siNo = string.Empty;
                string _supp = string.Empty;

                PurchaseOrder _poHdr = new PurchaseOrder();
                _poHdr = CHNLSVC.Inventory.GetPOHeader(BaseCls.GlbUserComCode, txtImpPONo.Text, "L");
                if (_poHdr != null)
                {
                    MessageBox.Show("Already Uploaded", "Cusdec Ref. No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtImpPONo.Text = "";
                    txtLCNo.Text = "";
                    txtSINo.Text = "";
                    txtImpSupp.Text = "";
                    txtBondDate.Text = "";
                    txtImpPONo.Focus();
                    return;
                }

                if (CHNLSVC.Inventory.CheckSCMBondNo(BaseCls.GlbUserComCode, txtImpPONo.Text, out _siNo, out _lcNo, out _costSheetRef, out _bondDate, out _supp) == false)
                {
                    MessageBox.Show("Invalid Cusdec Ref. No", "Cusdec Ref. No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtImpPONo.Text = "";
                    txtLCNo.Text = "";
                    txtSINo.Text = "";
                    txtImpSupp.Text = "";
                    txtBondDate.Text = "";
                    txtImpPONo.Focus();
                    return;
                }

                txtLCNo.Text = _lcNo;
                txtSINo.Text = _siNo;
                txtImpSupp.Text = _supp;
                txtBondDate.Text = _bondDate.Date.ToString("dd/MMM/yyyy");
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

        private void txtImpPONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnUploadImpPO.Focus();
        }

        private void btnUploadImpPO_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtImpPONo.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    bool _uploaded = CHNLSVC.Inventory.SaveSCMBondAsPO(txtImpPONo.Text.ToString(), Convert.ToDateTime(txtBondDate.Text.ToString()).Date, txtImpSupp.Text.ToString(), txtSINo.Text.ToString(), txtLCNo.Text.ToString(), BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca);
                    if (_uploaded == true)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Cusdec Ref. No - " + txtImpPONo.Text + " uploaded!", "Cusdec Ref. No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtImpPONo.Text = "";
                        txtLCNo.Text = "";
                        txtSINo.Text = "";
                        txtImpSupp.Text = "";
                        txtBondDate.Text = "";
                        pnlImportsPO.Visible = false;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Not uploaded!", "Cusdec Ref. No", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtImpPONo_TextChanged(object sender, EventArgs e)
        {
        }

        #endregion Upload Imports PO - Bond Nos

        private void btnSearch_Item_Click(object sender, EventArgs e)
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

                txtItem_Leave(null, null);
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
            lblItemDesc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                if (_itm == null)
                {
                    MessageBox.Show("Invalid Item Code", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    lblItemDesc.Text = _itm.Mi_shortdesc;
                    lblModel.Text = _itm.Mi_model;
                    lblBrand.Text = _itm.Mi_brand;
                }
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Select the new item code", "Add Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Enter the qty", "Add Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();
                return;
            }
            if (Convert.ToInt32(txtQty.Text) == 0)
            {
                MessageBox.Show("Enter the qty", "Add Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();
                return;
            }
            DataTable _dtNew = new DataTable();
            DataRow dr;

            _dtNew.Clear();
            _dtNew.Columns.Add("PODI_SEQ_NO", typeof(Int64));
            _dtNew.Columns.Add("PODI_LINE_NO", typeof(Int32));
            //_dtNew.Columns.Add("PODI_DEL_LINE_NO", typeof(Int32));
            _dtNew.Columns.Add("PODI_ITM_CD", typeof(string));
            _dtNew.Columns.Add("MI_LONGDESC", typeof(string));
            _dtNew.Columns.Add("MI_MODEL", typeof(string));
            _dtNew.Columns.Add("MI_BRAND", typeof(string));
            _dtNew.Columns.Add("POD_ITM_STUS", typeof(string));
            _dtNew.Columns.Add("PODI_QTY", typeof(decimal));
            _dtNew.Columns.Add("PODI_BAL_QTY", typeof(decimal));
            _dtNew.Columns.Add("GRN_QTY", typeof(decimal));
            _dtNew.Columns.Add("PODI_LOCA", typeof(string));
            _dtNew.Columns.Add("PODI_REMARKS", typeof(string));
            _dtNew.Columns.Add("UNIT_PRICE", typeof(decimal));
            _dtNew.Columns.Add("IS_NEW", typeof(string));

            dr = _dtNew.NewRow();
            dr["PODI_SEQ_NO"] = Convert.ToInt64(lblpodseq.Text);
            if (optChange.Checked == true)
                dr["PODI_LINE_NO"] = Convert.ToInt32(lblpodline.Text);
            else
                dr["PODI_LINE_NO"] = 0;
            //dr["PODI_DEL_LINE_NO"] = 0;
            dr["PODI_ITM_CD"] = txtItem.Text;
            dr["MI_LONGDESC"] = lblItemDesc.Text;
            dr["MI_MODEL"] = lblModel.Text;
            dr["MI_BRAND"] = lblBrand.Text;
            dr["POD_ITM_STUS"] = txtItemStatus.Text;
            dr["PODI_QTY"] = Convert.ToDecimal(txtQty.Text);
            dr["PODI_BAL_QTY"] = Convert.ToDecimal(txtQty.Text);
            dr["GRN_QTY"] = 0;
            dr["PODI_LOCA"] = "";
            dr["PODI_REMARKS"] = "";
            if (optChange.Checked == true)
                dr["UNIT_PRICE"] = Convert.ToDecimal(lblUnitPrice.Text);
            else
                dr["UNIT_PRICE"] = 0;
            dr["IS_NEW"] = "1";


            _dtNew.Rows.Add(dr);

            po_items_new.Merge(_dtNew);

            dvDOItems.AutoGenerateColumns = false;
            dvDOItems.DataSource = po_items_new;

            clear_new_item();
            pnlAdd.Hide();
        }

        private void clear_new_item()
        {
            txtCurItm.Text = "";
            txtItem.Text = "";
            txtItemStatus.Text = "";
            txtQty.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblItemDesc.Text = "";
            lblpodseq.Text = "";
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void dvDOItems_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dvDOItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10108)) //kapila 27/8/2015
            {
                if (Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["IS_NEW"].Value) == 1)
                {
                    if (MessageBox.Show("This is a new item. Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["PickQty"].Value) > 0)
                        {
                            MessageBox.Show("Remove the serials before remove the item !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        po_items_new.Rows.RemoveAt(e.RowIndex);

                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = po_items_new;
                    }

                }
                else
                {
                    pnlAdd.Visible = true;
                    txtCurItm.Text = dvDOItems.Rows[e.RowIndex].Cells["PODI_ITM_CD"].Value.ToString();
                    lblpodseq.Text = dvDOItems.Rows[e.RowIndex].Cells["PODI_SEQ_NO"].Value.ToString();
                    txtItemStatus.Text = dvDOItems.Rows[e.RowIndex].Cells["POD_ITM_STUS"].Value.ToString();
                    lblpodline.Text = dvDOItems.Rows[e.RowIndex].Cells["PODI_LINE_NO"].Value.ToString();
                    lblUnitPrice.Text = dvDOItems.Rows[e.RowIndex].Cells["UNIT_PRICE"].Value.ToString();
                    lblItemLine.Text = dvDOItems.Rows[e.RowIndex].Cells["PODI_LINE_NO"].Value.ToString();
                    txtItem.Focus();
                }
            }
        }

        private void btn_Srch_Itm_Stus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemStatus;
            _CommonSearch.ShowDialog();
            txtItemStatus.Focus();
        }

        private void txtItemStatus_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemStatus.Text))
            {
                if (!CHNLSVC.Inventory.IsValidItemStatus(txtItemStatus.Text))
                {
                    MessageBox.Show("Invalid item status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItemStatus.Text = "";
                    txtItemStatus.Focus();
                    return;
                }
            }
        }

        private void btnCloseAdd_Click(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtCurItm.Text = "";
            lblItemDesc.Text = "";
            lblModel.Text = "";
            txtQty.Text = "";
            lblpodseq.Text = "";
            pnlAdd.Visible = false;
        }

        private void optChange_CheckedChanged(object sender, EventArgs e)
        {
            if (optChange.Checked == true)
            {
                lblAdd.Text = "      Model Change";
                lblCur.Visible = true;
                txtCurItm.Visible = true;
            }
            else
            {
                lblAdd.Text = "      Add New Item";
                lblCur.Visible = false;
                txtCurItm.Visible = false;
            }
        }

        private void optNew_CheckedChanged(object sender, EventArgs e)
        {
            if (optNew.Checked == false)
            {
                lblAdd.Text = "      Model Change";
                lblCur.Visible = true;
                txtCurItm.Visible = true;
            }
            else
            {
                lblAdd.Text = "      Add New Item";
                lblCur.Visible = false;
                txtCurItm.Visible = false;
            }
        }

        private void btnPrintBC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGRNno.Text))
            {
                MessageBox.Show("Please select the GRN number", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGRNno.Focus();
                return;
            }

            BaseCls.GlbReportDoc = txtGRNno.Text;
            MultipleBarcode _barcode = new MultipleBarcode();
            _barcode.Show();

        }

        private void btn_srch_grn_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            if (chkTemp.Checked == false)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
                _result = CHNLSVC.CommonSearch.searchGRNData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TempGRNNo);
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(_CommonSearch.SearchParams, null, null, DateTime.Now.Date.AddDays(-30), DateTime.Now.Date);
            }

            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtGRNno;
            _CommonSearch.ShowDialog();
            // txtGRNno.Select();

            txtGRNno_Leave(null, null);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddNew.Focus();
        }

        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessTempSave();
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

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void txtItemStatus_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Stus_Click(null, null);
        }

        private void txtItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Itm_Stus_Click(null, null);
        }

        private void lblTemp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlTemp.Visible = true;
        }

        private void btnCloseTemp_Click(object sender, EventArgs e)
        {
            pnlTemp.Visible = false;
        }

        private void btnProc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTempGRN.Text))
            {
                MessageBox.Show("Please select the temporary GRN number", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Boolean _isCHk = false;
            List<InventoryHeader> _lstInvH = new List<InventoryHeader>();

            if (MessageBox.Show("Are You Sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;

            foreach (DataGridViewRow row in grvPending.Rows)
            {


                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _isCHk = true;
                    InventoryHeader _invH = new InventoryHeader();
                    _invH.Ith_doc_no = row.Cells["ith_doc_no"].Value.ToString();
                    _invH.Ith_oth_docno = row.Cells["Ith_oth_docno"].Value.ToString();
                    _lstInvH.Add(_invH);
                }
            }
            if (_isCHk == false)
            {
                MessageBox.Show("Please select the GRN number", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string _msg = "";
            int _eff = CHNLSVC.Inventory.ConfirmTempGRN(_lstInvH, out _msg);
            if (_eff != -99 && _eff >= 0)
                MessageBox.Show("Successfully processed", "Goods Received Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_msg, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUnscan_Click(object sender, EventArgs e)
        {
            pnlUnscan.Visible = true;
        }

        private void btnCloseUnscan_Click(object sender, EventArgs e)
        {
            pnlUnscan.Visible = false;
        }

        private void btnProcUnscan_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessUnscanItems();
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

        private void grvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTempGRN.Text = grvPending.Rows[e.RowIndex].Cells["ith_doc_no"].Value.ToString();
        }

        private void GetTempDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    Int32 affected_rows;
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    _direction = 1;
                    Int32 _userSeqNo;
                    #region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    dvDOSerials.AutoGenerateColumns = false;
                    dvDOSerials.DataSource = _emptySer;

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = _emptyItm;

                    //txtManualRef.Text = string.Empty;
                    //txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;

                    #endregion
                    InventoryHeader _invHdr = new InventoryHeader();
                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr_Temp(DocNo);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "GRN")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true && _direction == 0)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == false && _direction == 1)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        MessageBox.Show("Invalid Document No!", "GRN No", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtGRNno.Text = "";
                        txtGRNno.Focus();
                        return;
                    }
                    else
                    {
                        //cmdPrint.Enabled = true;
                        //grdItems.ReadOnly = true;
                        //gvSerial.ReadOnly = true;
                        //foreach (DataGridViewRow gvr in dvDOItems.Rows)
                        //{
                        //    LinkButton Addrow = gvr.FindControl("lbtnGet") as LinkButton;


                        //    Addrow.Enabled = true;

                        //}
                        //foreach (DataGridViewRow gvr in dvDOSerials.Rows)
                        //{
                        //    LinkButton Addrow = gvr.FindControl("lbtnRemove") as LinkButton;
                        //    Addrow.Enabled = true;
                        //    Addrow.OnClientClick = "return DeleteConfirm();";
                        //}



                        //ddlAdjSubType.SelectedItem.Text = _invHdr.Ith_sub_tp;
                        //txtManualRef.Text = _invHdr.Ith_manual_ref;
                        //txtOtherRef.Text = _invHdr.Ith_bus_entity;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        //txtUserSeqNo.Clear();
                        //ddlSeqNo.Text = string.Empty;
                    }
                    #endregion
                    //string Seq = _invHdr.Ith_entry_no;
                    _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, DocNo, 1);
                    if (_userSeqNo == -1)
                    {
                        _userSeqNo = GenerateNewUserSeqNo("GRN", _invHdr.Ith_oth_docno);
                        #region Get Serials
                        List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                        List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                        _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(DocNo);
                        List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                        if ((_serList != null && _serList.Count != 0))
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems)
                            {
                                _lineNo += 1;
                                InventoryRequestItem _itm = new InventoryRequestItem();
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                                _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                                _itm.Itri_line_no = _lineNo;
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                                _itm.Mi_model = itm.Peo.Tus_itm_model;
                                _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                                _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                                _itmList.Add(_itm);
                                ScanItemList = _itmList;
                            }


                            List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                            foreach (InventoryRequestItem _addedItem in ScanItemList)
                            {
                                ReptPickItems _reptitm = new ReptPickItems();
                                _reptitm.Tui_usrseq_no = _userSeqNo;
                                _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                                _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                                _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                                _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                                _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_itm_stus);
                                _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                                _saveonly.Add(_reptitm);
                            }
                            CHNLSVC.Inventory.SavePickedItems(_saveonly);

                            foreach (ReptPickSerials serial in _serList)
                            {
                                var _Batch = _serListT.Where(x => x.Inb_itm_cd == serial.Tus_itm_cd && x.Inb_itm_stus == serial.Tus_itm_stus).ToList();
                                foreach (InventoryBatchN _BItem in _Batch)
                                {
                                    serial.Tus_qty = _BItem.Inb_qty;
                                    serial.Tus_unit_cost = _BItem.Inb_unit_cost;
                                }
                                serial.Tus_usrseq_no = Convert.ToInt32(_userSeqNo);

                                //serial.Tus_qty = Session["Inb_qty"];
                                affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);

                            }
                        }
                        else         //kapila 11/12/2015
                        {
                            List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                            foreach (InventoryBatchN _serialItem in _nonserial)
                            {
                                ReptPickSerials _serial = new ReptPickSerials();
                                MasterItem _itms = new MasterItem();
                                _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _serialItem.Inb_itm_cd);
                                _serial.Tus_qty = _serialItem.Inb_qty;
                                _serial.Tus_com = _serialItem.Inb_com;
                                _serial.Tus_doc_dt = _serialItem.Inb_doc_dt;
                                _serial.Tus_itm_cd = _serialItem.Inb_itm_cd;
                                _serial.Tus_itm_stus = _serialItem.Inb_itm_stus;
                                _serial.Tus_loc = _serialItem.Inb_loc;
                                _serial.Tus_unit_price = _serialItem.Inb_unit_price;
                                _serial.Tus_unit_cost = _serialItem.Inb_unit_cost;      //kapila 20/1/2016
                                _serial.Tus_bin = _serialItem.Inb_bin;
                                _serial.Tus_itm_desc = _itms.Mi_shortdesc;
                                _serial.Tus_itm_model = _itms.Mi_model;
                                _serial.Tus_itm_brand = _itms.Mi_brand;
                                _serial.Tus_doc_no = _serialItem.Inb_doc_no;
                                _serial.Tus_ser_1 = "N/A";


                                _serial.Tus_usrseq_no = Convert.ToInt32(_userSeqNo);

                                //serial.Tus_qty = Session["Inb_qty"];
                                affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_serial, null);

                            }

                        }




                        #endregion
                        LoadItems(_userSeqNo.ToString());
                        txtSuppCode.Text = _invHdr.Ith_bus_entity;
                        txtPONo.Text = _invHdr.Ith_oth_docno;
                        List<InventoryBatchN> _serListT1 = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                        GetItemSave(_serListT1);
                    }
                    else
                    {
                        LoadItems(_userSeqNo.ToString());
                        txtSuppCode.Text = _invHdr.Ith_bus_entity;
                        txtPONo.Text = _invHdr.Ith_oth_docno;
                        List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                        GetItemSave(_serListT);
                    }



                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GetItemSave(List<InventoryBatchN> _serListT)
        {

            DataTable __Itemtbl = new DataTable();
            DataRow dr = null;
            __Itemtbl.Columns.Add(new DataColumn("PODI_SEQ_NO", typeof(int)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_LINE_NO", typeof(int)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_ITM_CD", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("MI_LONGDESC", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("MI_MODEL", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("MI_BRAND", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("POD_ITM_STUS", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_QTY", typeof(decimal)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_BAL_QTY", typeof(decimal)));
            __Itemtbl.Columns.Add(new DataColumn("GRN_QTY", typeof(decimal)));
            __Itemtbl.Columns.Add(new DataColumn("UNIT_PRICE", typeof(decimal)));


            foreach (InventoryBatchN _addedItem in _serListT)
            {
                dr = __Itemtbl.NewRow();
                dr["PODI_SEQ_NO"] = _addedItem.Inb_batch_line;
                dr["PODI_LINE_NO"] = _addedItem.Inb_base_itmline;
                dr["PODI_ITM_CD"] = _addedItem.Inb_base_itmcd;

                DataTable _result = CHNLSVC.Inventory.Get_Item_Infor(_addedItem.Inb_base_itmcd);
                if (_result.Rows.Count > 0)
                {
                    dr["MI_LONGDESC"] = _result.Rows[0][2].ToString();
                    dr["MI_MODEL"] = _result.Rows[0][8].ToString();
                    dr["MI_BRAND"] = _result.Rows[0][9].ToString();
                }
                DataTable _resultNew = CHNLSVC.Inventory.GetPoQty(_addedItem.Inb_base_doc_no, _addedItem.Inb_base_itmline);
                dr["POD_ITM_STUS"] = _addedItem.Inb_base_itmstus;
                dr["PODI_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_qty"]);
                dr["PODI_BAL_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_grn_bal"]);
                dr["GRN_QTY"] = _addedItem.Inb_qty;
                dr["UNIT_PRICE"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_unit_price"]);
                __Itemtbl.Rows.Add(dr);


            }
            dvDOItems.DataSource = __Itemtbl;

            return;
        }

        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                _direction = 1;
                //}

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "GRN", BaseCls.GlbUserID, _direction, _seqNo);
                if (_seqNo == "")
                {
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }
                }
                else
                {
                    user_seq_num = Convert.ToInt32(_seqNo);
                }




                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList();
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "GRN");
                if (_serList != null)
                {
                    dvDOSerials.AutoGenerateColumns = false;
                    dvDOSerials.DataSource = _serList;


                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    dvDOSerials.AutoGenerateColumns = false;
                    dvDOSerials.DataSource = emptyGridList;


                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, DocumentType, 1, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = DocumentType;
            RPH.Tuh_cre_dt = Convert.ToDateTime(dtpDODate.Value.Date);// DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = true; //direction always (-) for change status
            RPH.Tuh_doc_no = _scanDocument;
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
        }

        private void txtGRNno_Leave(object sender, EventArgs e)
        {
            if (chkTemp.Checked == true)   //kapila 13/11/2015
                GetTempDocData(txtGRNno.Text);
        }

        private void chkTemp_CheckedChanged(object sender, EventArgs e)
        {
            txtGRNno.Text = "";
        }

        private void dvDOSerials_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", BaseCls.GlbUserComCode, txtPONo.Text, 1);
            ReptPickSerials _setTemp=new ReptPickSerials();
            _setTemp.Tus_usrseq_no=_userSeqNo;
            _setTemp.Tus_itm_cd = dvDOSerials.Rows[e.RowIndex].Cells["TUS_ITM_CD"].Value.ToString();
            _setTemp.Tus_itm_stus = dvDOSerials.Rows[e.RowIndex].Cells["TUS_ITM_STUS"].Value.ToString();
            _setTemp.Tus_qty =Convert.ToInt32(dvDOSerials.Rows[e.RowIndex].Cells["TUS_QTY"].Value);

            Int16 _eff = CHNLSVC.Inventory.UpdateAllScanSerials(_setTemp);
        }

        private void btnCloseSI_Click(object sender, EventArgs e)
        {
            pnlSI.Visible = false;
            txtSI.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlSI.Visible = true;
        }

        private void btnDownloadSI_Click(object sender, EventArgs e)
        {
            //validate

            if (MessageBox.Show("Are You Sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
            Int32 _eff = CHNLSVC.Inventory.Import_SI(txtSI.Text,BaseCls.GlbUserDefLoca);
            if(_eff ==0)
            {
                MessageBox.Show("Invalid SI number", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (_eff == 2)
            {
                MessageBox.Show("Costing is not done for this SI number", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Successfully Downloaded", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlSI.Visible = false;
                txtSI.Clear();
                ClearScreen();
            }

        }



    }
}