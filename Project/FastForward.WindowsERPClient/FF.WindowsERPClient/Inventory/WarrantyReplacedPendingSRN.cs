using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Inventory
{
    public partial class WarrantyReplacedPendingSRN : FF.WindowsERPClient.Base
    {
        private List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
        private List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        string _defBin = "";

        public WarrantyReplacedPendingSRN()
        {
            InitializeComponent();
            String _tempDefBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (!string.IsNullOrEmpty(_tempDefBin))
            {
                _defBin = _tempDefBin;
            }
            else
            {
                _defBin = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to clear the screen?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clear();
            }
        }

        private void clear()
        {
            WarrantyReplacedPendingSRN formnew = new WarrantyReplacedPendingSRN();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnGetInvoices_Click(object sender, EventArgs e)
        {
            DataTable _dt = CHNLSVC.CustService.getCreditNote4PendingSRN(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefLoca, txtFindCustomer.Text, txtDocumentNo.Text, 1);
            dvPendingInvoices.AutoGenerateColumns = false;
            dvPendingInvoices.DataSource = _dt;
        }

        private void dvPendingInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                string _invNo = dvPendingInvoices.Rows[e.RowIndex].Cells[1].Value.ToString();
                DataTable _dtInv = CHNLSVC.Sales.GetSalesHdr(_invNo);
                txtCustCode.Text = _dtInv.Rows[0]["sah_cus_cd"].ToString();
                txtCustName.Text = _dtInv.Rows[0]["SAH_CUS_NAME"].ToString();
                txtCustAddress1.Text = _dtInv.Rows[0]["SAH_CUS_ADD1"].ToString();
                txtCustAddress2.Text = _dtInv.Rows[0]["SAH_CUS_ADD2"].ToString();
                txtManualRefNo.Text = _dtInv.Rows[0]["SAH_MAN_REF"].ToString();
                txtRemarks.Text = _dtInv.Rows[0]["SAH_REMARKS"].ToString();
                txtInvcNo.Text = _dtInv.Rows[0]["SAH_INV_NO"].ToString();
                txtInvcDate.Text =Convert.ToDateTime( _dtInv.Rows[0]["SAH_DT"]).Date.ToShortDateString();
                txtInvNoOrg.Text = _dtInv.Rows[0]["SAH_REF_DOC"].ToString();
                

                load_Item_serial();
            }
        }

        private void load_Item_serial()
        {
            List<InvoiceItem> _temp = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            _doitemserials = new List<ReptPickSerials>();

            //get invoice items
            _temp = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvNoOrg.Text, "DELIVERD");
            foreach (InvoiceItem item in _temp)
            {
                _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);              
                _doitemserials.AddRange(_tempDOSerials);
                txtDONo.Text = _tempDOSerials[0].Tus_doc_no.ToString();
            }

            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();

            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvInvItem.DataSource = _temp;

            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();
            dgvDelDetails.DataSource = _doitemserials;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dgvDelDetails.Rows.Count==0)
            {
                MessageBox.Show("Serial details not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            InventoryHeader inHeader = new InventoryHeader();
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            //inventory document

            if (_doitemserials.Count > 0)
            {
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
                inHeader.Ith_acc_no = "STOCK_EX";
                inHeader.Ith_anal_1 = string.Empty;
                inHeader.Ith_anal_2 = string.Empty;
                inHeader.Ith_anal_3 = string.Empty;
                inHeader.Ith_anal_4 = string.Empty;
                inHeader.Ith_anal_5 = string.Empty;
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = txtCustCode.Text;
                inHeader.Ith_cate_tp = "REP";
                inHeader.Ith_com = BaseCls.GlbUserComCode;
                inHeader.Ith_com_docno = string.Empty;
                inHeader.Ith_cre_by = BaseCls.GlbUserID;
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = txtCustAddress1.Text;
                inHeader.Ith_del_add2 = txtCustAddress2.Text;
                inHeader.Ith_del_code = string.Empty;
                inHeader.Ith_del_party = txtCustCode.Text;
                inHeader.Ith_del_town = string.Empty;
                inHeader.Ith_direct = true;
                inHeader.Ith_doc_date = dtpDODate.Value.Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "SRN";
                inHeader.Ith_doc_year = dtpDODate.Value.Date.Year;
                inHeader.Ith_entry_no = txtInvcNo.Text.Trim();
                inHeader.Ith_entry_tp = "";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_is_manual = false;
                // inHeader.Ith_job_no = txtJobNo.Text.Trim();
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                inHeader.Ith_manual_ref = "";
                inHeader.Ith_mod_by = BaseCls.GlbUserID;
                inHeader.Ith_mod_when = DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_oth_docno = txtDONo.Text;
                inHeader.Ith_sub_docno = "";
                inHeader.Ith_remarks = "";
                inHeader.Ith_bus_entity =txtCustCode.Text;
                inHeader.Ith_del_add1 = txtCustAddress1.Text;

                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "NOR";
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_pc =BaseCls.GlbUserDefProf;

                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "SRN";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "SRN";
                masterAuto.Aut_year = null;
            }

            string documntNo = string.Empty;
            _doitemserials.ForEach(x => x.Tus_bin = _defBin);
            _doitemserials.ForEach(x => x.Tus_ser_id = CHNLSVC.Inventory.GetSerialID());
            _doitemserials.ForEach(x => x.Tus_usrseq_no = CHNLSVC.Inventory.GetSerialID());

            #region Check receving serials are duplicating :: Chamal 08-May-2014

            string _err = string.Empty;
            if (CHNLSVC.Inventory.CheckDuplicateSerialFound(inHeader.Ith_com, inHeader.Ith_loc, _doitemserials, out _err) <= 0)
            {
                MessageBox.Show(_err.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (InvoiceItem _ser in _InvDetailList)
            {

                //_doitemserials.Where(y => y.Tus_itm_stus == _ser.Sad_itm_stus).ToList().ForEach(x => x.Tus_itm_cd = _ser.Sad_itm_cd);
                _doitemserials.Where(x => x.Tus_itm_cd == _ser.Sad_itm_cd).ToList().ForEach(y => y.Tus_itm_stus = _ser.Sad_itm_stus);//06-07-2015 Nadeeka

            }

            #endregion Check receving serials are duplicating :: Chamal 08-May-2014
            Int32 result = CHNLSVC.Inventory.SaveExchangeInward(inHeader, _doitemserials, null, masterAuto, out documntNo,1);

            if (result >= 1)
            {
                MessageBox.Show("Successfully completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            else
            {
                MessageBox.Show(documntNo, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator + txtFindCustomer.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
  
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerId:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindCustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtFindCustomer.Select();
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

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {


                    if (!string.IsNullOrEmpty(txtFindCustomer.Text))
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                        DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtFindInvoiceNo;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();
                        txtFindInvoiceNo.Select();
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtFindInvoiceNo;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();
                        txtFindInvoiceNo.Select();
                    }
                
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_srch_acc_no_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtFindAcc;
            _CommonSearch.ShowDialog();
            txtFindAcc.Select();
        }

        private void txtFindCustomer_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Customer_Click(null, null);
        }

        private void txtFindCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                btnSearch_Customer_Click(null, null);
        }

        private void txtFindInvoiceNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtFindInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                btnSearch_Invoice_Click(null, null);
        }

        private void txtFindAcc_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_acc_no_Click(null, null);
        }

        private void txtFindAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                btn_srch_acc_no_Click(null, null);
        }

        //private bool LoadFromSCM2(string _invoice, string _serial, string _serial2, string _warranty, string _item, int _waraperiod, DateTime _waradate)
        //{
        //    bool _isValid = true;

        //    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
        //    if (_isService == false)
        //    {
        //        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, _invoice, "D", null, null);
        //    }
        //    else
        //    {
        //        _invHdr = CHNLSVC.Sales.GetPendingInvoices(null, null, string.Empty, _invoice, "D", null, null);
        //    }


        //    if (_invHdr == null || _invHdr.Count <= 0)
        //    { MessageBox.Show("There is no such invoice available for the selected invoice", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); _isValid = false; return _isValid; }

        //    if (_invHdr[0].Sah_stus != "R" && _invHdr[0].Sah_stus != "H")
        //    {
        //       // LoadCustomer(_invHdr[0], _invHdr[0].Sah_cus_cd, _invHdr[0].Sah_cus_name, _invHdr[0].Sah_cus_add1 + " " + _invHdr[0].Sah_cus_add2);
        //        DataTable _t = CHNLSVC.Sales.GetDeliveryOrader(txtInvcNo.Text.Trim());
        //        if (_t != null && _t.Rows.Count > 0)
        //        { 
        //            txtDO.Text = _t.Rows[0].Field<string>("ith_doc_no");
        //        }
        //        LoadBindData(_invoice, _serial, _serial2, _warranty, _item, _waraperiod, _waradate, txtDO.Text.Trim());
        //        //txtPc.Text = _invHdr[0].Sah_pc;
        //    }
        //    else if (_invHdr[0].Sah_stus == "R")
        //        MessageBox.Show("Invoice is already reversed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    else if (_invHdr[0].Sah_stus == "H")
        //        MessageBox.Show("Invoice is already hold.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    return _isValid;
        //}

        //private void LoadBindData(string _invoice, string _serial, string _serial2, string _warranty, string _item, int _waraperiod, DateTime _waradate, string _deliveryorder)
        //{
        //    dgvInvItem.AutoGenerateColumns = false;
        //    dgvInvItem.DataSource = new List<InvoiceItem>();
        //    dgvDelDetails.AutoGenerateColumns = false;
        //    dgvDelDetails.DataSource = new List<ReptPickSerials>();

        //    decimal _unitprice = 0;
        //    decimal _taxamt = 0;
        //    MasterItem _mitm = LoadItem(_item);
        //    _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, Convert.ToString(cmbAdvBook.SelectedValue), Convert.ToString(cmbAdvLevel.SelectedValue), string.Empty, _item, 1, Convert.ToDateTime(txtDate.Text));
        //    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
        //    {
        //        List<PriceDetailRef> _tempPrice = new List<PriceDetailRef>();
        //        var _specialpriceforpc = _priceDetailRef.Where(x => x.Sapd_price_type == 4).ToList();
        //        if (_specialpriceforpc != null && _specialpriceforpc.Count > 0)
        //        {
        //            _tempPrice = _specialpriceforpc;
        //        }
        //        else
        //        {
        //            var _normalprice = _priceDetailRef.Where(x => x.Sapd_price_type == 0).ToList();
        //            if (_normalprice != null && _normalprice.Count > 0)
        //            {
        //                _tempPrice = _normalprice;
        //            }
        //        }
        //        if (_tempPrice != null && _tempPrice.Count > 0)
        //        {
        //            _unitprice = FigureRoundUp(TaxCalculation(_item, ItemStatus.GOD.ToString(), 1, _priceBookLevelRef, _tempPrice[0].Sapd_itm_price, 0, 0, false), true);
        //            _taxamt = FigureRoundUp(TaxCalculation(_item, ItemStatus.GOD.ToString(), 1, _priceBookLevelRef, _tempPrice[0].Sapd_itm_price, 0, 0, true), true);

        //            if (_InvDetailList == null || _InvDetailList.Count <= 0)
        //                _InvDetailList = new List<InvoiceItem>();

        //            if (_doitemserials == null || _doitemserials.Count <= 0)
        //                _doitemserials = new List<ReptPickSerials>();

        //            InvoiceItem _one = new InvoiceItem();
        //            _one.Sad_do_qty = 1;
        //            _one.Sad_inv_no = _invoice;
        //            _one.Sad_itm_cd = _item;
        //            _one.Sad_itm_line = 1;
        //            _one.Sad_itm_stus = ItemStatus.GOD.ToString();
        //            _one.Sad_pb_lvl = _company.Mc_anal8;
        //            _one.Sad_pbook = _company.Mc_anal7;
        //            _one.Sad_qty = 1;
        //            _one.Sad_srn_qty = 1;
        //            _one.Sad_tot_amt = _unitprice + _taxamt;
        //            _one.Sad_unit_amt = _unitprice;
        //            _one.Sad_unit_rt = _unitprice;
        //            _one.Sad_warr_period = _waraperiod;
        //            _one.Sad_warr_remarks = _waradate.Date.ToShortDateString();
        //            _InvDetailList.Add(_one);

        //            ReptPickSerials _two = new ReptPickSerials();
        //            _two.Tus_base_doc_no = _invoice;
        //            _two.Tus_base_itm_line = 1;
        //            _two.Tus_batch_line = 1;
        //            _two.Tus_bin = BaseCls.GlbDefaultBin;
        //            _two.Tus_com = BaseCls.GlbUserComCode;
        //            _two.Tus_doc_dt = txtDate.Value.Date;
        //            _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
        //            _two.Tus_exist_grndt = txtDate.Value.Date;
        //            _two.Tus_itm_brand = _mitm.Mi_brand;
        //            _two.Tus_itm_cd = _item;
        //            _two.Tus_itm_desc = _mitm.Mi_longdesc;
        //            _two.Tus_itm_line = 1;
        //            _two.Tus_itm_model = _mitm.Mi_model;
        //            _two.Tus_itm_stus = ItemStatus.GOD.ToString();
        //            _two.Tus_loc = txtReturnLoc.Text.Trim();
        //            _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
        //            _two.Tus_orig_grndt = txtDate.Value.Date;
        //            _two.Tus_qty = 1;
        //            _two.Tus_ser_1 = _serial;
        //            _two.Tus_ser_2 = _serial2;
        //            _two.Tus_unit_cost = 0;
        //            _two.Tus_unit_price = 0;
        //            _two.Tus_warr_no = _warranty;
        //            _two.Tus_warr_period = _waraperiod;
        //            _two.Tus_doc_no = _deliveryorder;
        //            _doitemserials.Add(_two);

        //            dgvInvItem.AutoGenerateColumns = false;
        //            dgvInvItem.DataSource = new List<InvoiceItem>();
        //            dgvInvItem.DataSource = _InvDetailList;

        //            dgvDelDetails.AutoGenerateColumns = false;
        //            dgvDelDetails.DataSource = new List<ReptPickSerials>();
        //            dgvDelDetails.DataSource = _doitemserials;

        //            var _creditvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
        //            decimal _issueval = 0;
        //            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
        //                _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
        //            lblCreditValue.Text = FormatToCurrency(Convert.ToString(_creditvalue));
        //            lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
        //            lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
        //        }
        //        else
        //        {
        //            MessageBox.Show("There is no price for the item " + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            lblCreditValue.Text = "0.00";
        //            lblIssueValue.Text = "0.00";
        //            lblDifference.Text = "0.00";
        //        }
        //    }
        //}

    }
}
