using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text;
using System.Transactions;
using System.Globalization;
using System.Data;


namespace FF.WebERPClient.Inventory_Module
{
    public partial class GoodReceiveNote : BasePage
    {
        static List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();

        private void Ad_hoc_sessions()
        {
            //Session["UserID"] = "ADMIN";
            //Session["UserCompanyCode"] = "ABL";
            //Session["SessionID"] = "666";
            //Session["UserDefLoca"] = "MSR16";
            //Session["UserDefProf"] = "39";


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ad_hoc_sessions();
            //for supplier
            txtSupplier.Attributes.Add("onKeyup", "return clickButton(event,'" + imgFindSupplier.ClientID + "')");

            if (!Page.IsPostBack)
            {
                DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                //dt = dt.Date;
                //txtAdjustmentNo.Text = "";
                DateTime thisDate = dt;
                DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                CultureInfo culture = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate.ToString("d", culture));
                string frm_dt = thisDate.ToString("d", culture);

                DateTime thisDate_ = DateTime.Now;
                DateTime date_ = new DateTime(thisDate_.Year, thisDate_.Month, thisDate_.Day);
                CultureInfo culture_ = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate_.ToString("d", culture_));
                string to_dt = thisDate_.ToString("d", culture_);

                DateTime today = DateTime.Now.Date;
                BindPendingConsignmentRequestGridData(frm_dt, to_dt, null, null);
                //BindPendingConsignmentRequestGridData(null, null, null, null);     

                BindItemStatus(ddlmpeItemStatus);
            }
        }

        protected void BindItemStatus(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            DataTable _tbls = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            Int32 its = 0;
            foreach (DataRow _r in _tbl.Rows)
            {

                if (_r[0].ToString() == "CONS") _tbls.Rows.RemoveAt(its);
                its += 1;
            }

            _ddl.DataSource = _tbls;
            _ddl.DataTextField = "Mic_cd";
            _ddl.DataValueField = "Mic_cd";
            _ddl.DataBind();

        }

        protected void btnRequestSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFromDate.Text))
                {
                    DateTime frmdt = Convert.ToDateTime(txtFromDate.Text);

                }
                if (!string.IsNullOrEmpty(txtToDate.Text))
                {
                    DateTime todt = Convert.ToDateTime(txtToDate.Text);
                }
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Date Format not correct!");
                return;
            }
            //string _fromDate = DateTime.MinValue.ToString();
            //string _toDate = DateTime.Now.ToString();
            string _fromDate = null;
            string _toDate = null;

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                _fromDate = txtFromDate.Text.Trim();
                _toDate = txtToDate.Text.Trim();
            }
            else
            {
                DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                //dt = dt.Date;
                //txtAdjustmentNo.Text = "";
                DateTime thisDate = dt;
                DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                CultureInfo culture = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate.ToString("d", culture));
                _fromDate = thisDate.ToString("d", culture);

                DateTime thisDate_ = DateTime.Now;
                DateTime date_ = new DateTime(thisDate_.Year, thisDate_.Month, thisDate_.Day);
                CultureInfo culture_ = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate_.ToString("d", culture_));
                _toDate = thisDate_.ToString("d", culture_);

                DateTime today = DateTime.Now.Date;
                //BindPendingConsignmentRequestGridData(_fromDate, _toDate, null, null);
                //BindPendingConsignmentRequestGridData(null, null, null, null);           
            }

            string _supCode = string.IsNullOrEmpty(txtSupplier.Text) ? null : txtSupplier.Text.Trim();
            string _docNo = string.IsNullOrEmpty(txtRequestNo.Text) ? null : txtRequestNo.Text.Trim();

            this.BindPendingConsignmentRequestGridData(_fromDate, _toDate, _supCode, _docNo);
        }

        #region User Defined Methods

        private void BindPendingConsignmentRequestGridData(string _fromDate, string _toDate, string _supCode, string _docNo)
        {
            PurchaseOrder _paramPurchaseOrder = new PurchaseOrder();

            _paramPurchaseOrder.Poh_com = GlbUserComCode;
            _paramPurchaseOrder.MasterLocation = new MasterLocation() { Ml_loc_cd = GlbUserDefLoca };
            _paramPurchaseOrder.Poh_stus = "A";
            _paramPurchaseOrder.MasterBusinessEntity = new MasterBusinessEntity() { Mbe_tp = "S" };
            _paramPurchaseOrder.FromDate = _fromDate;
            _paramPurchaseOrder.ToDate = _toDate;
            _paramPurchaseOrder.Poh_supp = _supCode;
            _paramPurchaseOrder.Poh_doc_no = _docNo;
            _paramPurchaseOrder.Poh_sub_tp = "N";

            List<PurchaseOrder> pending_list = CHNLSVC.Inventory.GetAllPendingConsignmentRequestData(_paramPurchaseOrder);

            gvPendingRequests.DataSource = pending_list;
            gvPendingRequests.DataBind();

        }

        private string RequestNo
        {
            get { return Session["ReqeustNo"].ToString(); }
            set { Session["ReqeustNo"] = value; }
        }

        private void SetSelectedConsignRequestData(string _reqNo)
        {
            RequestNo = _reqNo;
            List<ReptPickSerials> _reptPickSerialList = null;

            List<PurchaseOrderDelivery> _purList = new List<PurchaseOrderDelivery>();

            //Get all consignment request details.
            PurchaseOrder _purchaseOrder = CHNLSVC.Inventory.GetConsignmentRequestDetails(GlbUserComCode, _reqNo, GlbUserDefLoca);

            if (_purchaseOrder != null)
            {
                //Set header details.
                lblSupplierCode.Text = _purchaseOrder.Poh_supp;
                lblReqRefNo.Text = _purchaseOrder.Poh_ref;
                lblRequestNo.Text = _purchaseOrder.Poh_doc_no;
                //lblSupplierName.Text = string.Empty;
                lblSupplierName.Text = string.Empty;
                txtRefNo.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                //txtReceiptDate.Text = DateTime.Now.ToShortDateString();
                // txtReceiptDate.Text = _purchaseOrder.Poh_dt.ToString();

                //DateTime thisDate = _purchaseOrder.Poh_dt;
                //DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                //CultureInfo culture = new CultureInfo("pt-BR");
                //Console.WriteLine(thisDate.ToString("d", culture));
                //txtReceiptDate.Text = thisDate.ToString("d", culture);

                DateTime thisDate = DateTime.Now.Date;
                DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                CultureInfo culture = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate.ToString("d", culture));
                txtReceiptDate.Text = thisDate.ToString("d", culture);

                //Get the Purchase Order DeliveryList.
                _purchaseOrderDeliveryList = _purchaseOrder.PurchaseOrderDeliveryList;

                //Get the user seq no for selected requestNo.
                int _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, _reqNo);

                if (_userSeqNo > 0)
                {
                    //Load all scan serial list.
                    _reptPickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");

                    //--------added by shani----------------+
                    if (_reptPickSerialList != null)
                    {
                        //  var sortLPOserList = _reptPickSerialList.OrderBy(x => x.Tus_itm_cd).ToList();
                        var sortLPOserList = from po in _reptPickSerialList
                                             orderby po.Tus_itm_cd
                                             select po;
                        gvAllItemSerials.DataSource = sortLPOserList;
                        gvAllItemSerials.DataBind();

                    }
                    //--------------------------------------+
                    else
                    {
                        gvAllItemSerials.DataSource = _reptPickSerialList;
                        gvAllItemSerials.DataBind();
                    }



                    //Commeted By Prabhath on 21/05/2012
                    //Edit Code : 0001

                    //if ((_purchaseOrderDeliveryList != null) && (_purchaseOrderDeliveryList.Count > 0) && (_reptPickSerialList != null) && (_reptPickSerialList.Count> 0))
                    //{
                    //    //Update the previous entered qty to actual qty.
                    //    foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                    //    {
                    //        _tempPickSerialList = _reptPickSerialList.Where(x => x.Tus_itm_cd.Equals(item.MasterItem.Mi_cd)).ToList();
                    //        item.Actual_qty = _tempPickSerialList.Count;
                    //    }
                    //}


                    if ((_purchaseOrderDeliveryList != null) && (_purchaseOrderDeliveryList.Count > 0) && (_reptPickSerialList != null) && (_reptPickSerialList.Count > 0))
                    {
                        //Update the previous entered qty to actual qty.

                        foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                        {
                            Int32 _count = 0;

                            foreach (ReptPickSerials items in _reptPickSerialList)
                            {

                                if (item.Podi_line_no == items.Tus_itm_line)
                                {
                                    _count++;

                                }

                                //_tempPickSerialList = _reptPickSerialList.Where(x => x.Tus_itm_cd.Equals(item.MasterItem.Mi_cd) && x.Tus_itm_line==items.Tus_itm_line ).ToList();
                                //item.Actual_qty = _tempPickSerialList.Count;
                            }
                            item.Actual_qty = Convert.ToDecimal(_count);
                            _purList.Add(item);
                        }
                    }


                }

                //Bind the receipt item details.

                //----add by shani---------------------+
                //var sortLPODelvList = from data in _purchaseOrderDeliveryList 
                //      orderby data.Podi_itm_cd ascending 
                //      select data; 
                if (_purList.Count > 0)
                { //Edit Code : 0001
                    var sortLPODelvList = _purList.OrderBy(x => x.Podi_itm_cd).ToList();
                    gvReceiptItemDetails.DataSource = sortLPODelvList;
                    gvReceiptItemDetails.DataBind();
                }
                //-------------------------------------+
                else
                {
                    gvReceiptItemDetails.DataSource = _purchaseOrderDeliveryList;
                    gvReceiptItemDetails.DataBind();

                }

            }

        }


        private void LoadSerialModal(string _selectedItemDetails, int _gvRowIndex)
        {
            string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
            string _selectedItemCode = arr[0];
            hdnUnitPrice.Value = arr[1];
            hdnLineNo.Value = arr[2];
            lblReqQty.Text = arr[3];
            hdngvRowIndex.Value = _gvRowIndex.ToString();
            //---added by shani----+
            txtActualQty.Text = "";
            txtSerialNo1.Text = "";
            txtSerialNo2.Text = "";
            txtSerialNo3.Text = "";
            txtWarrantyNo.Text = "";
            //---------------------+
            MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _selectedItemCode);

            //Set Serial modal popup data.
            lblmpeItemCode.Text = _selectedItem.Mi_cd + " - " + _selectedItem.Mi_longdesc + " - " + _selectedItem.Mi_model + " - " + _selectedItem.Mi_brand;

            //Load bin codes.
            List<string> bincodes = CHNLSVC.Inventory.GetBinCodesforInventoryInward(GlbUserComCode, GlbUserDefLoca);
            ddlmpeBinCode.DataSource = bincodes;
            ddlmpeBinCode.DataBind();

            ////-----added by shani----------+
            //if (bincodes.Count == 2)
            //{
            //    ddlmpeBinCode.SelectedValue = bincodes[1].ToString();
            //    ddlmpeBinCode.Enabled = false;
            //}
            //else
            //{

            //}
            ////-----------------------------+
            //Load default bincode
            string default_bin = CHNLSVC.Inventory.Get_default_binCD(GlbUserComCode, GlbUserDefLoca);
            if (default_bin != null)
            {
                ddlmpeBinCode.SelectedValue = default_bin;
            }

            gvItemSerials.DataSource = null;
            gvItemSerials.DataBind();

            if (_selectedItem.Mi_is_ser1 == 1) //Is Serilize item.
            {
                divSerial.Visible = true;
                divNonSerial.Visible = false;

                //Set Serial number textboxes.
                txtSerialNo1.Enabled = (_selectedItem.Mi_is_ser1 == 1) ? true : false;
                txtSerialNo2.Enabled = true;// _selectedItem.Mi_is_ser2;
                txtSerialNo3.Enabled = true; //_selectedItem.Mi_is_ser3;

                //Disable actual qty textbox.
                //txtActualQty.Visible = false;
            }
            else if ((_selectedItem.Mi_is_ser1 == 0) || (_selectedItem.Mi_is_ser1 == -1)) //Is non Serilize item.
            {
                //divSerial.Visible = false; //commented by shani
                //-------------added by shani----------------+
                divSerial.Visible = true;

                txtSerialNo1.Enabled = false;
                txtSerialNo2.Enabled = false;
                txtSerialNo3.Enabled = false;
                //-------------------------------------------+
                divNonSerial.Visible = true;
                //Enable actual qty textbox.
                //txtActualQty.Enabled = true;


            }


            //Get the user seq no for selected requestNo.
            int _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, lblRequestNo.Text.Trim());

            //Bind if there are any existing serials in DB.
            if (_userSeqNo > 0)
            {
                //Load all scan serial list.
                List<ReptPickSerials> _reptPickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");

                if ((_reptPickSerialList != null) && (_reptPickSerialList.Count > 0))
                {
                    //Edit Code : 0001
                    List<ReptPickSerials> _resultSerialList = _reptPickSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value.ToString())).ToList();
                    gvItemSerials.DataSource = _resultSerialList;
                    gvItemSerials.DataBind();
                }
            }

            serialmdpExtender.Show();

        }

        private void SaveConsignReceiptData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtReceiptDate.Text))
                    throw new UIValidationException("Please enter Receipt Date.");

                string _createdDocNo = string.Empty;

                InventoryHeader _invHeader = new InventoryHeader();
                _invHeader.Ith_com = GlbUserComCode;
                _invHeader.Ith_loc = GlbUserDefLoca;
                DateTime _docDate = Convert.ToDateTime(txtReceiptDate.Text);
                _invHeader.Ith_doc_date = _docDate;
                _invHeader.Ith_doc_year = _docDate.Year;
                _invHeader.Ith_direct = true;
                _invHeader.Ith_doc_tp = "GRN";
                _invHeader.Ith_cate_tp = "LOCAL";
                //_invHeader.Ith_sub_tp = "CONSIGN";
                _invHeader.Ith_bus_entity = lblSupplierCode.Text;
                _invHeader.Ith_is_manual = true;
                _invHeader.Ith_manual_ref = txtRefNo.Text;
                _invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = GlbUserName;
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = GlbUserName;
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = GlbUserSessionID;
                _invHeader.Ith_oth_docno = lblRequestNo.Text;


                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = GlbUserDefLoca;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "GRN";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "GRN";
                _masterAuto.Aut_year = null;

                //Get the user seq no for selected requestNo.
                int _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, lblRequestNo.Text.Trim());

                List<ReptPickSerials> _reptPickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");

                if (_reptPickSerialList == null)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No scanned items to save!");
                    return;
                }
                CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, _reptPickSerialList, null, _masterAuto, _purchaseOrderDeliveryList, out _createdDocNo);

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "  " + _createdDocNo + " Sucessfully saved.");
                ClearMasterData();
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }

        }


        private void SubmitItemSerialData()
        {
            //Get item searials count.           
            int editRowIndex = Convert.ToInt32(hdngvRowIndex.Value);
            string[] arr = lblmpeItemCode.Text.Split(new string[] { " - " }, StringSplitOptions.None);
            string _selectedItemCode = arr[0];

            List<PurchaseOrderDelivery> _purList = new List<PurchaseOrderDelivery>();

            MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _selectedItemCode);

            //if (_selectedItem.Mi_is_ser1 == 1)  //Delete serialize item.
            if (_selectedItem.Mi_is_ser1 == 1 || (_selectedItem.Mi_is_ser1 == 0))  //Delete serialize item.
            {
                int _itemSearialsCount = (gvItemSerials != null) ? gvItemSerials.Rows.Count : 0;
                //_purchaseOrderDeliveryList.Where(x => x.MasterItem.Mi_cd.Equals(_selectedItemCode)).ToList()
                //Update the actual qty.
                foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                {
                    if (item.MasterItem.Mi_cd.Equals(_selectedItemCode) && item.Podi_line_no == Convert.ToInt32(hdnLineNo.Value))
                    {
                        item.Actual_qty = _itemSearialsCount;
                    }

                    _purList.Add(item);
                }
                //Edit Code : 0001


            }
            //else if ((_selectedItem.Mi_is_ser1 == 0) || (_selectedItem.Mi_is_ser1 == -1))  //Delete non-serialize item.
            else if ((_selectedItem.Mi_is_ser1 == -1))  //Delete non-serialize item.
            {
                int _actualCount = string.IsNullOrEmpty(txtActualQty.Text) ? 0 : Convert.ToInt32(txtActualQty.Text);

                foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                {
                    if (item.MasterItem.Mi_cd.Equals(_selectedItemCode) && item.Podi_line_no == Convert.ToInt32(hdnLineNo.Value))
                    {
                        item.Actual_qty = _actualCount;
                    }
                    _purList.Add(item);
                }
            }

            gvReceiptItemDetails.DataSource = _purList;
            gvReceiptItemDetails.DataBind();

            //pnlReceiptItemDetails.GroupingText = "Receipt Item Details : Request No.  " + lblRequestNo.Text;
            serialmdpExtender.Hide();
        }


        private void DeleteSelectedItem(string _selectedItemDetails, out string _LineNo)
        {
            string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
            string _serialId = arr[0];
            string _itemCode = arr[1];
            string _serialNo1 = arr[2];
            string _binCode = arr[3];
            string _lineNo;
            if (arr.Length > 4) _lineNo = arr[4];
            else _lineNo = "0";


            //Get the user seq no for selected requestNo.
            int _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, lblRequestNo.Text.Trim());

            MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _itemCode);

            if (_selectedItem.Mi_is_ser1 == 1)  //Delete serialize item.
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId));
            }
            //else if ((_selectedItem.Mi_is_ser1 == 0) || (_selectedItem.Mi_is_ser1 == -1))  //Delete non-serialize item.
            //{
            //    CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId), _itemCode, _binCode);
            //}
            else if ((_selectedItem.Mi_is_ser1 == 0))  //Delete non-serialize item.
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId));
                lblActQty_.Text = (Convert.ToDecimal(lblActQty_.Text) - 1).ToString();
            }
            else if ((_selectedItem.Mi_is_ser1 == -1))  //Delete non-serialize item.
            {
                CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId), _itemCode, _binCode);
            }

            _LineNo = _lineNo;
        }


        private void AddItemQuantites()
        {
            //Edit Code : 0001

            try
            {
                if (ddlmpeBinCode.SelectedItem.Text.ToUpper().Equals("--SELECT--"))
                    throw new UIValidationException("Please select Bin.");

                string _selectedReqNo = lblRequestNo.Text;
                string[] arr = lblmpeItemCode.Text.Split(new string[] { " - " }, StringSplitOptions.None);
                string _selectedItemCode = arr[0];
                string _selectedItemDesc = arr[1];
                string _selectedItemModel = arr[2];
                string _selectedItemBrand = arr[3];

                int _userSeqNo = 0;
                //Need to check Whether that is there any record in temp_pick_hdr table in SCMREP DB.
                _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, _selectedReqNo);

                using (TransactionScope _tr = new TransactionScope())
                {

                    if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
                    {
                        _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "GRN", 1, GlbUserComCode);

                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_doc_no = _selectedReqNo;
                        _inputReptPickHeader.Tuh_doc_tp = "GRN";
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_session_id = GlbUserSessionID;
                        _inputReptPickHeader.Tuh_usr_com = GlbUserComCode;
                        _inputReptPickHeader.Tuh_usr_id = GlbUserName;
                        _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

                        //Save it to the scmrep.temp_pick_hdr header table. 
                        Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                    }

                    //Get the selected Item
                    MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _selectedItemCode);
                    string _binCode = ddlmpeBinCode.SelectedValue;
                    string _itemStatus = ddlmpeItemStatus.SelectedValue;

                    if (_selectedItem.Mi_is_ser1 == 1) //(Serialize Item = 1)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo1.Text))
                            throw new UIValidationException("Please enter Serial No 1.");

                        string _serialNo1 = (txtSerialNo1.Enabled) ? txtSerialNo1.Text.Trim() : string.Empty;
                        string _serialNo2 = (txtSerialNo2.Enabled) ? txtSerialNo2.Text.Trim() : string.Empty;
                        string _serialNo3 = (txtSerialNo3.Enabled) ? txtSerialNo3.Text.Trim() : string.Empty;
                        string _warrantyno = (txtWarrantyNo.Enabled) ? txtWarrantyNo.Text.Trim() : string.Empty;


                        if ((CHNLSVC.Inventory.IsExistInSerialMaster(GlbUserComCode, _selectedItemCode, _serialNo1)) > 0)
                            throw new UIValidationException("Serial No1 is already exist.");

                        if ((CHNLSVC.Inventory.IsExistInTempPickSerial(GlbUserComCode, _userSeqNo.ToString(), _selectedItemCode, _serialNo1)) > 0)
                            throw new UIValidationException("Serial No1 is already in use. Enter with different Serial No1");//exists in the temp-pick-ser

                        if (!string.IsNullOrEmpty(txtWarrantyNo.Text))
                            if (txtWarrantyNo.Text.Trim() != "N/A" && txtWarrantyNo.Text.Trim() != "NA")
                            {
                                if ((CHNLSVC.Inventory.IsExistInWarrantyMaster(GlbUserComCode, _warrantyno)) > 0)
                                    throw new UIValidationException("Warranty is already exist.");

                                if ((CHNLSVC.Inventory.IsExistWarrantyInTempPickSerial(GlbUserComCode, _warrantyno)) > 0)
                                    throw new UIValidationException("Warranty is already in use. Enter with different Warranty");
                            }

                        //Write to the Picked items serilals table.
                        ReptPickSerials _inputReptPickSerials = new ReptPickSerials();

                        _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                        _inputReptPickSerials.Tus_doc_no = _selectedReqNo;
                        _inputReptPickSerials.Tus_seq_no = 0;
                        _inputReptPickSerials.Tus_itm_line = 0;
                        _inputReptPickSerials.Tus_batch_line = 0;
                        _inputReptPickSerials.Tus_ser_line = 0;
                        _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _inputReptPickSerials.Tus_com = GlbUserComCode;
                        _inputReptPickSerials.Tus_loc = GlbUserDefLoca;
                        _inputReptPickSerials.Tus_bin = _binCode;
                        _inputReptPickSerials.Tus_itm_cd = _selectedItemCode;
                        _inputReptPickSerials.Tus_itm_stus = _itemStatus;
                        _inputReptPickSerials.Tus_unit_cost = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                        _inputReptPickSerials.Tus_unit_price = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                        _inputReptPickSerials.Tus_qty = 1;
                        _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                        _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                        _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                        _inputReptPickSerials.Tus_warr_no = _warrantyno;
                        _inputReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _inputReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _inputReptPickSerials.Tus_itm_brand = _selectedItemBrand;
                        _inputReptPickSerials.Tus_itm_line = string.IsNullOrEmpty(hdnLineNo.Value) ? 0 : Convert.ToInt32(hdnLineNo.Value);
                        _inputReptPickSerials.Tus_cre_by = GlbUserName;
                        _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        _inputReptPickSerials.Tus_session_id = GlbUserSessionID;

                        _inputReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _inputReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _inputReptPickSerials.Tus_itm_brand = _selectedItemBrand;


                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");

                        //-------added by shani
                        var serCount = 0;
                        if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                        {
                            serCount = (from c in _resultItemsSerialList
                                        where c.Tus_itm_cd == _selectedItemCode && c.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)
                                        select c).Count();
                        }
                        //for non serials
                        //var serCount_2 = (from c in _resultItemsSerialList
                        //                  select c.Tus_qty).Sum();


                        if (serCount < Convert.ToDecimal(lblReqQty.Text.Trim()))
                        {
                            CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                        }
                        else
                        {
                            throw new UIValidationException("Cannot exceed the required Qty!");

                            // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the required Qty!");
                        }
                        //-------added by shani
                        //Save to the temp table.



                        //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)
                        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");
                        gvItemSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                        gvItemSerials.DataBind();

                        //Rebind all item serials to grid.
                        var sortedserList = from po in _ResultItemsSerialList
                                            orderby po.Tus_itm_cd
                                            select po;
                        gvAllItemSerials.DataSource = sortedserList;
                        gvAllItemSerials.DataBind();

                        //SetSelectedConsignRequestData(_selectedReqNo);
                        SubmitItemSerialData();
                        // serialmdpExtender.Show();

                    }
                    else if (_selectedItem.Mi_is_ser1 == 0) //(Non serialize Item = 0)
                    {
                        if (string.IsNullOrEmpty(txtActualQty.Text))
                            throw new UIValidationException("Please enter Actual Qty.");

                        Decimal difference;
                        try
                        {
                            try
                            {
                                difference = Convert.ToDecimal(lblReqQty.Text) - Convert.ToDecimal(lblActQty_.Text);
                            }
                            catch (Exception e)
                            {
                                throw new UIValidationException("Enter a valid Quantity.");
                            }

                        }
                        catch (Exception e)
                        {
                            throw new UIValidationException("Enter a valid Quantity.");
                        }

                        if (difference < Convert.ToDecimal(txtActualQty.Text))
                        {
                            throw new UIValidationException("Cannot Add more than Required Qty.");
                        }



                        int _actualQty = Convert.ToInt32(txtActualQty.Text.Trim());
                        string _warrantyno = (txtWarrantyNo.Enabled) ? txtWarrantyNo.Text.Trim() : string.Empty;

                        if (!string.IsNullOrEmpty(txtWarrantyNo.Text))
                            if (txtWarrantyNo.Text.Trim() != "N/A" && txtWarrantyNo.Text.Trim() != "NA")
                            {
                                if (_actualQty>1 && !string.IsNullOrEmpty (txtWarrantyNo.Text.Trim ()) )
                                    throw new UIValidationException("Can not enter a warranty no for more than one qty");

                                if ((CHNLSVC.Inventory.IsExistInWarrantyMaster(GlbUserComCode, _warrantyno)) > 0)
                                    throw new UIValidationException("Warranty is already exist.");

                                if ((CHNLSVC.Inventory.IsExistWarrantyInTempPickSerial(GlbUserComCode, _warrantyno)) > 0)
                                    throw new UIValidationException("Warranty is already in use. Enter with different Warranty");
                            }


                        for (int i = 0; i < _actualQty; i++)
                        {
                            //Write to the Picked items serilals table.
                            ReptPickSerials _newReptPickSerials = new ReptPickSerials();

                            _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
                            _newReptPickSerials.Tus_doc_no = _selectedReqNo;
                            _newReptPickSerials.Tus_seq_no = 0;
                            _newReptPickSerials.Tus_itm_line = 0;
                            _newReptPickSerials.Tus_batch_line = 0;
                            _newReptPickSerials.Tus_ser_line = 0;
                            _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                            _newReptPickSerials.Tus_com = GlbUserComCode;
                            _newReptPickSerials.Tus_loc = GlbUserDefLoca;
                            _newReptPickSerials.Tus_bin = _binCode;
                            _newReptPickSerials.Tus_itm_cd = _selectedItemCode;
                            _newReptPickSerials.Tus_itm_stus = _itemStatus;
                            _newReptPickSerials.Tus_unit_cost = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                            _newReptPickSerials.Tus_unit_price = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                            _newReptPickSerials.Tus_qty = 1;
                            _newReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                            _newReptPickSerials.Tus_ser_1 = "N/A";
                            _newReptPickSerials.Tus_ser_2 = "N/A";
                            _newReptPickSerials.Tus_ser_3 = "N/A";
                            _newReptPickSerials.Tus_warr_no = _warrantyno;
                            _newReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                            _newReptPickSerials.Tus_itm_model = _selectedItemModel;
                            _newReptPickSerials.Tus_itm_brand = _selectedItemBrand;
                            _newReptPickSerials.Tus_itm_line = string.IsNullOrEmpty(hdnLineNo.Value) ? 0 : Convert.ToInt32(hdnLineNo.Value);
                            _newReptPickSerials.Tus_cre_by = GlbUserName;
                            _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                            _newReptPickSerials.Tus_session_id = GlbUserSessionID;

                            _newReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                            _newReptPickSerials.Tus_itm_model = _selectedItemModel;
                            _newReptPickSerials.Tus_itm_brand = _selectedItemBrand;

                            //Save to the temp table.
                            //CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);//commented by shani
                            //_newReptPickSerials = null; //commented by shani

                            //-------added by shani
                            List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");
                            var serCount = 0;
                            if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                            {
                                serCount = (from c in _resultItemsSerialList
                                            where c.Tus_itm_cd == _selectedItemCode && c.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)
                                            select c).Count();
                            }
                            //for non serial, decimal allowed
                            //var serCount_2 = (from c in _resultItemsSerialList
                            //                  select c.Tus_qty).Sum();

                            if (serCount < Convert.ToInt32(lblReqQty.Text.Trim()))
                            {
                                //CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                                CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);

                                lblActQty_.Text = (Convert.ToDecimal(lblActQty_.Text) + 1).ToString();
                                _newReptPickSerials = null;

                                //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)
                                List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");
                                gvItemSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                                gvItemSerials.DataBind();

                                //Rebind all item serials to grid.
                                var sortedserList = from po in _ResultItemsSerialList
                                                    orderby po.Tus_itm_cd
                                                    select po;
                                //  gvAllItemSerials.DataSource = _ResultItemsSerialList;
                                gvAllItemSerials.DataSource = sortedserList;
                                gvAllItemSerials.DataBind();

                                SubmitItemSerialData();
                                //SubmitItemSerialData();
                                //SetSelectedConsignRequestData(_selectedReqNo);
                                serialmdpExtender.Show();
                            }
                            else
                            {
                                throw new UIValidationException("Cannot exceed the required Qty!");
                                // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the required Qty!");
                            }
                            //-------added by shani
                            //Save to the temp table.

                        }
                    }
                    else if (_selectedItem.Mi_is_ser1 == -1) //(Non serialize decimal Item = -1))
                    {
                        if (string.IsNullOrEmpty(txtActualQty.Text))
                            throw new UIValidationException("Please enter Actual Qty.");

                        int _actualQty = Convert.ToInt32(txtActualQty.Text.Trim());

                        //Write to the Picked items serilals table.
                        ReptPickSerials _decimalReptPickSerials = new ReptPickSerials();

                        _decimalReptPickSerials.Tus_usrseq_no = _userSeqNo;
                        _decimalReptPickSerials.Tus_doc_no = _selectedReqNo;
                        _decimalReptPickSerials.Tus_seq_no = 0;
                        _decimalReptPickSerials.Tus_itm_line = 0;
                        _decimalReptPickSerials.Tus_batch_line = 0;
                        _decimalReptPickSerials.Tus_ser_line = 0;
                        _decimalReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _decimalReptPickSerials.Tus_com = GlbUserComCode;
                        _decimalReptPickSerials.Tus_loc = GlbUserDefLoca;
                        _decimalReptPickSerials.Tus_bin = _binCode;
                        _decimalReptPickSerials.Tus_itm_cd = _selectedItemCode;
                        _decimalReptPickSerials.Tus_itm_stus = _itemStatus;
                        _decimalReptPickSerials.Tus_unit_cost = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                        _decimalReptPickSerials.Tus_unit_price = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                        _decimalReptPickSerials.Tus_qty = _actualQty;
                        //_decimalReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        _decimalReptPickSerials.Tus_ser_1 = "N/A";
                        _decimalReptPickSerials.Tus_ser_2 = "N/A";
                        _decimalReptPickSerials.Tus_ser_3 = "N/A";
                        _decimalReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _decimalReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _decimalReptPickSerials.Tus_itm_brand = _selectedItemBrand;
                        _decimalReptPickSerials.Tus_itm_line = string.IsNullOrEmpty(hdnLineNo.Value) ? 0 : Convert.ToInt32(hdnLineNo.Value);
                        _decimalReptPickSerials.Tus_cre_by = GlbUserName;
                        _decimalReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        _decimalReptPickSerials.Tus_session_id = GlbUserSessionID;

                        _decimalReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _decimalReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _decimalReptPickSerials.Tus_itm_brand = _selectedItemBrand;

                        //Save to the temp table.

                        //CHNLSVC.Inventory.SavePickedSerialsDecimalItems(_decimalReptPickSerials); //commented by shani
                        //_decimalReptPickSerials = null; //commented by shani


                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");


                        //var serCount = (from c in _resultItemsSerialList
                        //                select c).Count();

                        //for non serial, decimal allowed
                        var serCount_2 = (from c in _resultItemsSerialList
                                          select c.Tus_qty).Sum();

                        if (serCount_2 < Convert.ToInt32(lblReqQty.Text.Trim()))
                        {
                            //CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                            CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, null);
                            _decimalReptPickSerials = null;


                            //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)
                            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "GRN");
                            gvItemSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                            gvItemSerials.DataBind();

                            //Rebind all item serials to grid.
                            var sortedserList = from po in _ResultItemsSerialList
                                                orderby po.Tus_itm_cd
                                                select po;
                            //  gvAllItemSerials.DataSource = _ResultItemsSerialList;
                            gvAllItemSerials.DataSource = sortedserList;
                            //gvAllItemSerials.DataSource = _ResultItemsSerialList;
                            gvAllItemSerials.DataBind();

                            //SetSelectedConsignRequestData(_selectedReqNo);
                            SubmitItemSerialData();
                            // serialmdpExtender.Show();
                        }
                        else
                        {
                            throw new UIValidationException("Cannot exceed the required Qty!");
                            // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the required Qty!");
                        }
                        //-------added by shani
                        //Save to the temp table.
                   }
                    _tr.Complete();
                }

            }
            catch (UIValidationException uiex)
            {
                this.uc_SerialPopUpMsgInfo.SetMessage(CommonUIDefiniton.MessageType.Error, uiex.ErrorMessege);
            }
            catch (Exception ex)
            {
                this.uc_SerialPopUpMsgInfo.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }


            serialmdpExtender.Show();

            this.ClearModalWindow();

        }


        private void ClearModalWindow()
        {
            txtActualQty.Text = string.Empty;
            txtSerialNo1.Text = string.Empty;
            txtSerialNo2.Text = string.Empty;
            txtSerialNo3.Text = string.Empty;
            txtWarrantyNo.Text = string.Empty;

            ddlmpeBinCode.SelectedIndex = 1;
            ddlmpeItemStatus.SelectedIndex = 0;
        }

        private void ClearMasterData()
        {
            txtFromDate.Text = string.Empty;
            txtReceiptDate.Text = string.Empty;
            txtRefNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtRequestNo.Text = string.Empty;
            txtSupplier.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ClearModalWindow();
        }

        #endregion

        protected void gvAllItemSerials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {//Edit Code : 0001
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelAllSerial = (ImageButton)e.CommandSource;
                        string _selectedItemDetails = imgbtndelAllSerial.CommandArgument;
                        string _lineNo;
                        DeleteSelectedItem(_selectedItemDetails, out _lineNo);

                        //----added by shani---------------
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        string itemcd = row.Cells[1].Text.ToString();
                        Int32 usrSeqNo = Convert.ToInt32(row.Cells[0].Text.ToString());
                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrSeqNo, "GRN");

                        if (_resultItemsSerialList == null)
                        { // SetSelectedConsignRequestData(string _reqNo)
                            //gvItemSerials.DataSource = null;
                            //gvItemSerials.DataBind();

                            //gvAllItemSerials.DataSource = null;
                            //gvAllItemSerials.DataBind();
                            // Response.Redirect("~/Inventory_Module/ConsignmentReceiveNote.aspx");
                            // return;
                            //Edit Code : 0001
                            SetSelectedConsignRequestData(RequestNo);
                            return;

                        }
                        //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)
                        gvItemSerials.DataSource = _resultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(itemcd) && x.Tus_itm_line == Convert.ToInt32(_lineNo)).ToList();
                        gvItemSerials.DataBind();

                        //Rebind all item serials to grid.
                        gvAllItemSerials.DataSource = _resultItemsSerialList;
                        gvAllItemSerials.DataBind();

                        SetSelectedConsignRequestData(lblRequestNo.Text);
                        //---------------------------------
                        break;
                    }
            }
        }


        protected void gvItemSerials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                //Edit Code : 0001
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelSerial = (ImageButton)e.CommandSource;
                        string _selectedItemDetails = imgbtndelSerial.CommandArgument;
                        string _lineNo;
                        DeleteSelectedItem(_selectedItemDetails, out _lineNo);

                        //-------------added by shani---------------------------

                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        string itemcd = row.Cells[0].Text;

                        string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
                        string _selectedItemCode = arr[1];

                        Int32 usrSeqNo = Convert.ToInt32(row.Cells[0].Text.ToString());
                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrSeqNo, "GRN");

                        //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)


                        //Rebind all item serials to grid.
                        // gvAllItemSerials.DataSource = _resultItemsSerialList;
                        //gvAllItemSerials.DataBind();
                        if (_resultItemsSerialList != null)
                        {
                            var sortedList = from po in _resultItemsSerialList
                                             orderby po.Tus_itm_cd
                                             select po;
                            gvAllItemSerials.DataSource = sortedList;
                            gvAllItemSerials.DataBind();

                            gvItemSerials.DataSource = _resultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                            gvItemSerials.DataBind();
                        }
                        else
                        {
                            gvAllItemSerials.DataSource = _resultItemsSerialList;
                            gvAllItemSerials.DataBind();

                            gvItemSerials.DataSource = null;
                            gvItemSerials.DataBind();
                        }

                        //gvItemSerials.DataSource = _resultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode)).ToList();
                        //gvItemSerials.DataBind();

                        SubmitItemSerialData();//**********
                        serialmdpExtender.Show();
                        //--------------------------------------------------------
                        //Load default bincode
                        string default_bin = CHNLSVC.Inventory.Get_default_binCD(GlbUserComCode, GlbUserDefLoca);
                        if (default_bin != null)
                        {
                            ddlmpeBinCode.SelectedValue = default_bin;
                        }
                        break;
                    }
            }
        }


        protected void gvPendingRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "SELECTREQUEST":
                    {
                        LinkButton lnkbtnReqNo = (LinkButton)e.CommandSource;
                        string _selectedReqNo = lnkbtnReqNo.CommandArgument;
                        SetSelectedConsignRequestData(_selectedReqNo);
                        //pnlReceiptItemDetails.GroupingText = "Receipt Item Details : Request No.  " + lblRequestNo.Text;
                        break;
                    }
            }
        }


        protected void gvReceiptItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADDSERIALS":
                    {
                        Button btnAddSerials = (Button)e.CommandSource;
                        string _selectedItemDetails = btnAddSerials.CommandArgument;
                        GridViewRow gvr = (GridViewRow)btnAddSerials.NamingContainer;
                        TextBox lbl_ActQty = (TextBox)gvr.Cells[6].FindControl("txtActualQty");
                        lblActQty_.Text = lbl_ActQty.Text;
                        // string serial_1 = (string)gvr.Cells[1].Text.Trim();

                        int editRowIndex = gvr.RowIndex;
                        LoadSerialModal(_selectedItemDetails, editRowIndex);
                        break;
                    }

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddItemQuantites();

        }

        protected void gvReceiptItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtActualQty = (TextBox)e.Row.FindControl("txtActualQty");
                Button btnAddSerials = (Button)e.Row.FindControl("btnAddSerials");
                HiddenField hdnItemIsSerialize1 = (HiddenField)e.Row.FindControl("hdnItemIsSerialize1");

                txtActualQty.Enabled = false;
                btnAddSerials.Enabled = true;

                //if ((txtActualQty != null) && (hdnItemIsSerialize1 != null) && (btnAddSerials != null))
                //{
                //    int _serializeStatus = Convert.ToInt32(hdnItemIsSerialize1.Value);
                //    txtActualQty.Enabled = (_serializeStatus == 1) ? false : true;
                //    btnAddSerials.Enabled = (_serializeStatus == 1) ? true : false;

                //}
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitItemSerialData();
        }

        protected void btnConsReceiptSave_Click(object sender, EventArgs e)
        {

            if (gvReceiptItemDetails.Rows.Count < 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No items to save. Please select Request No. from pending requests!");
                return;
            }

            SaveConsignReceiptData();
            ClearItmGrids();
            refreshPendignReqGrid(); //now the saved PO should be disapear from the grid.

            // pnlReceiptItemDetails.GroupingText = "Receipt Item Details";
        }

        protected void btnConsReceiptClear_Click(object sender, EventArgs e)
        {
            ClearItmGrids();
            Response.Redirect("~/Inventory_Module/GoodReceiveNote.aspx");
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void ImageBtnSupplier_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSupplierData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSupplier.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgPOSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPurchaseOrders(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtRequestNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void ClearItmGrids()
        {
            gvReceiptItemDetails.DataSource = null;
            gvReceiptItemDetails.DataBind();

            gvAllItemSerials.DataSource = null;
            gvAllItemSerials.DataBind();

            lblSupplierCode.Text = "";
            lblReqRefNo.Text = "";
            lblRequestNo.Text = "";
            txtRefNo.Text = "N/A";
            lblSupplierName.Text = "";
            txtRemarks.Text = "N/A";

            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
            CultureInfo culture = new CultureInfo("pt-BR");
            Console.WriteLine(thisDate.ToString("d", culture));
            txtReceiptDate.Text = thisDate.ToString("d", culture);
        }

        protected void refreshPendignReqGrid()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFromDate.Text))
                {
                    DateTime frmdt = Convert.ToDateTime(txtFromDate.Text);

                }
                if (!string.IsNullOrEmpty(txtToDate.Text))
                {
                    DateTime todt = Convert.ToDateTime(txtToDate.Text);
                }
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Date Format not correct!");
                return;
            }

            string _fromDate = null;
            string _toDate = null;

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                _fromDate = txtFromDate.Text.Trim();
                _toDate = txtToDate.Text.Trim();
            }

            string _supCode = string.IsNullOrEmpty(txtSupplier.Text) ? null : txtSupplier.Text.Trim();
            string _docNo = string.IsNullOrEmpty(txtRequestNo.Text) ? null : txtRequestNo.Text.Trim();

            this.BindPendingConsignmentRequestGridData(_fromDate, _toDate, _supCode, _docNo);
        }

    }
}
