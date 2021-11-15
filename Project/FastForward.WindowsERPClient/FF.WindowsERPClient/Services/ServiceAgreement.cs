using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Drawing;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceAgreement : Base
    {
        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }


        private Boolean _IsVirtualItem = false;
        private List<MasterItemTax> MainTaxConstant = null;
        private string _warrSearchtp = string.Empty;
        private string _warrSearchorder = string.Empty;
        private Service_Chanal_parameter _scvParam = null;
        private bool IsSaleFigureRoundUp = false;

        private List<SCV_AGR_ITM> oAgr_Items = new List<SCV_AGR_ITM>();
        private List<SCV_AGR_COVER_ITM> oAgr_CoverItems = new List<SCV_AGR_COVER_ITM>();
        private List<SCV_AGR_SES> oAgr_Ses = new List<SCV_AGR_SES>();
        private List<scv_agr_cha> oAgr_Cha = new List<scv_agr_cha>();
        private List<scv_agr_payshed> oAgr_Payshed = new List<scv_agr_payshed>();

        private List<SCV_AGR_COVER_ITM> tmp_CoverItems = new List<SCV_AGR_COVER_ITM>();

        MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
        private MasterProfitCenter _MasterProfitCenter = null;
        private List<RecieptItem> _recieptItem = null;
        private string _invoiceType = string.Empty;
        private Int32 _itemLine = 1;
        private Int32 _chaLine = 1;
        private Int32 _isPart = 0;
        private Int32 _term = 1;
        Int32 _selItmline = 0;
        private Boolean _isGroup = false;
        private DataTable dtmerge=new DataTable ();

        public ServiceAgreement()
        {
            InitializeComponent();

            // ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            //ucPayModes1.PaidAmountLabel.Text = "0.00";
            //ucPayModes1.ClearControls();

            //ucPayModes1.InvoiceType = "SAG";
            //ucPayModes1.LoadData();

            _invoiceType = "CS";
            getAgrClaimType();

            pnlGen.Size = new Size(556, 355);
            pnlGen.Location = new System.Drawing.Point(439, 30);


        }

        private void bind_period_basis()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("MONTH", "Monthly");
            PartyTypes.Add("DAY", "Daily");

            cmbBasis.DataSource = new BindingSource(PartyTypes, null);
            cmbBasis.DisplayMember = "Value";
            cmbBasis.ValueMember = "Key";

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Agreement:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        paramsText.Append(_warrSearchtp + seperator + _warrSearchorder + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AgrType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AgrClaimType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew:
                    {
                        paramsText.Append("V" + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }

        #region events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateSave())
            {
                return;
            }

            if (MessageBox.Show("Do you want to save?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _invoiceType);
            if (string.IsNullOrEmpty(_invoicePrefix))
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region add master auto
            MasterAutoNumber _agrAuto = new MasterAutoNumber();
            _agrAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _agrAuto.Aut_cate_tp = "PC";
            _agrAuto.Aut_direction = null;
            _agrAuto.Aut_modify_dt = null;
            _agrAuto.Aut_moduleid = "AGR";
            _agrAuto.Aut_number = 0;
            _agrAuto.Aut_start_char = "AGR";
            _agrAuto.Aut_year = DateTime.Today.Year;

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = 1;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = _invoiceType;
            _invoiceAuto.Aut_number = 0;
            _invoiceAuto.Aut_start_char = _invoicePrefix;
            _invoiceAuto.Aut_year = dtpDate.Value.Year;
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
                    _receiptAuto.Aut_year = dtpDate.Value.Year;
                }

            #endregion

            #region add agreement header
            SCV_AGR_HDR oHeader = new SCV_AGR_HDR();
            oHeader.Sag_seq_no = Convert.ToInt32((lblSeq.Text == "") ? "-99" : lblSeq.Text);
            //oHeader.Sag_agr_no = 
            oHeader.Sag_com = BaseCls.GlbUserComCode;
            oHeader.Sag_tp = txtType.Text;
            oHeader.Sag_clm_tp = txtClaimType.Text;
            oHeader.Sag_chnl = BaseCls.GlbDefChannel;
            oHeader.Sag_schnl = BaseCls.GlbDefSubChannel;
            oHeader.Sag_pc = BaseCls.GlbUserDefProf;
            oHeader.Sag_multi_pc = chk.Checked ? true : false;
            oHeader.Sag_commdt = Convert.ToDateTime(dtFrom.Value.Date);
            oHeader.Sag_exdt = Convert.ToDateTime(dtTo.Value.Date);
            oHeader.Sag_rewldt = Convert.ToDateTime(dtRenew.Value.Date);
            oHeader.Sag_custcd = txtCustCode.Text;
            oHeader.Sag_cust_town = txtTown.Tag.ToString();
            oHeader.Sag_town_rmk = txtEquipLoc.Text;
            oHeader.Sag_cont_person = txtContPersn.Text;
            oHeader.Sag_cont_add = txtContLoc.Text;
            oHeader.Sag_cont_no = txtContNo.Text;
            oHeader.Sag_manualref = "";
            oHeader.Sag_otherref = "";
            oHeader.Sag_rmk = "";
            oHeader.Sag_gl_debtor_cd = "";
            oHeader.Sag_tot_amt = Convert.ToDecimal(lblGrndTotalAmount.Text);
            oHeader.Sag_set_amt = 0;
            oHeader.Sag_period_basis = cmbBasis.SelectedValue.ToString();
            oHeader.Sag_ser_attempt = Convert.ToInt32(txtAttempt.Text);
            oHeader.Sag_top = txtTOP.Text;
            oHeader.Sag_tac = txtTAC.Text;
            oHeader.Sag_work_inc = txtWrkInc.Text;
            oHeader.Sag_svc_freq = txtFreq.Text;
            //oHeader.Sag_period = 
            oHeader.Sag_stus = "P";
            //oHeader.Sag_stus_rmk = 
            //oHeader.Sag_authoby1 = 
            //oHeader.Sag_authoby2 = 
            oHeader.Sag_cre_by = BaseCls.GlbUserID;
            oHeader.Sag_mod_by = BaseCls.GlbUserID;

            #endregion

            //#region add invoice header
            //InvoiceHeader _invheader = new InvoiceHeader();
            //List<InvoiceItem> _invoiceItem = new List<InvoiceItem>();
            //List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();

            //_invheader.Sah_com = BaseCls.GlbUserComCode;
            //_invheader.Sah_cre_by = BaseCls.GlbUserID;
            //_invheader.Sah_cre_when = DateTime.Now;
            //_invheader.Sah_currency = "LKR";
            //_invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
            //_invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
            //_invheader.Sah_cus_cd = txtCustCode.Text;
            //_invheader.Sah_cus_name = txtCusName.Text.Trim();
            //_invheader.Sah_d_cust_add1 = txtAddress1.Text.Trim();
            //_invheader.Sah_d_cust_add2 = txtAddress2.Text.Trim();
            //_invheader.Sah_d_cust_cd = txtCustCode.Text.Trim();
            //_invheader.Sah_d_cust_name = txtCusName.Text.Trim();
            //_invheader.Sah_direct = true;
            //_invheader.Sah_dt = dtpDate.Value.Date;
            //_invheader.Sah_epf_rt = 0;
            //_invheader.Sah_esd_rt = 0;
            //_invheader.Sah_ex_rt = 1;
            //_invheader.Sah_inv_no = "na";
            //_invheader.Sah_inv_sub_tp = "SA";
            //_invheader.Sah_inv_tp = _invoiceType;
            //_invheader.Sah_is_acc_upload = false;
            //_invheader.Sah_man_ref = "";
            //_invheader.Sah_manual = false;
            //_invheader.Sah_mod_by = BaseCls.GlbUserID;
            //_invheader.Sah_mod_when = DateTime.Now;
            //_invheader.Sah_pc = BaseCls.GlbUserDefProf;
            //_invheader.Sah_pdi_req = 0;
            //_invheader.Sah_ref_doc = "";
            //_invheader.Sah_remarks = "";
            //_invheader.Sah_sales_chn_cd = "";
            //_invheader.Sah_sales_chn_man = "";
            //_invheader.Sah_sales_ex_cd = BaseCls.GlbUserID;
            //_invheader.Sah_sales_region_cd = "";
            //_invheader.Sah_sales_region_man = "";
            //_invheader.Sah_sales_sbu_cd = "";
            //_invheader.Sah_sales_sbu_man = "";
            //_invheader.Sah_sales_str_cd = "";
            //_invheader.Sah_sales_zone_cd = "";
            //_invheader.Sah_sales_zone_man = "";
            //_invheader.Sah_seq_no = 1;
            //_invheader.Sah_session_id = BaseCls.GlbUserSessionID;
            //// _invheader.Sah_structure_seq = txtQuotation.Text.Trim();
            //_invheader.Sah_stus = "A";
            ////  if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) _invheader.Sah_stus = "D";
            //_invheader.Sah_town_cd = "";
            //_invheader.Sah_tp = "INV";
            //_invheader.Sah_wht_rt = 0;
            //_invheader.Sah_direct = true;
            //_invheader.Sah_tax_inv = false;
            ////_invheader.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
            ////_invheader.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
            //_invheader.Sah_del_loc = string.Empty;
            ////_invheader.Sah_grn_com = _customerCompany;
            ////_invheader.Sah_grn_loc = _customerLocation;
            ////_invheader.Sah_is_grn = _isCustomerHasCompany;
            ////_invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
            //_invheader.Sah_is_svat = false;
            //_invheader.Sah_tax_exempted = false;
            //_invheader.Sah_anal_2 = "";
            ////_invheader.Sah_anal_6 = txtLoyalty.Text.Trim();
            //_invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
            //_invheader.Sah_is_dayend = 0;



            //if (txtCustCode.Text.Trim() != "CASH")
            //{
            //    MasterBusinessEntity _en = CHNLSVC.Sales.GetCustomerProfile(txtCustCode.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
            //    if (_en != null)
            //        if (string.IsNullOrEmpty(_en.Mbe_com))
            //        {
            //            _invheader.Sah_tax_exempted = _en.Mbe_tax_ex;
            //            _invheader.Sah_is_svat = _en.Mbe_is_svat;
            //        }
            //}

            //#endregion

            //#region add invoice detail
            //MasterItem _itemdetail = new MasterItem();
            //_itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

            //string _pb = string.Empty;
            //string _pblvl = string.Empty;

            //Service_Chanal_parameter _Parameters = null;
            //_Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            //if (_Parameters != null)
            //{
            //    _pb = _Parameters.SP_DEFPB;
            //    _pblvl = _Parameters.SP_DEFPBLVL;
            //}
            //InvoiceItem _tempItem = new InvoiceItem();
            //IsVirtual(_itemdetail.Mi_itm_tp);
            //_tempItem.Sad_alt_itm_cd = "";
            //_tempItem.Sad_alt_itm_desc = "";
            //_tempItem.Sad_comm_amt = 0;
            //_tempItem.Sad_disc_amt = 0;
            //_tempItem.Sad_disc_rt = 0;
            //_tempItem.Sad_do_qty = Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_inv_no = "";
            //_tempItem.Sad_is_promo = false;
            //_tempItem.Sad_itm_cd = txtItem.Text;
            //_tempItem.Sad_itm_line = 1;
            //_tempItem.Sad_itm_seq = 0;
            //_tempItem.Sad_itm_stus = "GOD";
            //_tempItem.Sad_itm_tax_amt = 0;
            //_tempItem.Sad_itm_tp = _itemdetail.Mi_itm_tp;
            //_tempItem.Sad_job_no = "";
            //_tempItem.Sad_res_line_no = 0;
            //_tempItem.Sad_res_no = "";

            //_tempItem.Sad_merge_itm = "";
            //_tempItem.Sad_pb_lvl = _pblvl;
            //_tempItem.Sad_pb_price = Convert.ToDecimal(txtUnitPrice.Text);
            //_tempItem.Sad_pbook = _pb;
            //_tempItem.Sad_print_stus = false;
            //_tempItem.Sad_promo_cd = "";
            //_tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_seq = 0;
            //_tempItem.Sad_seq_no = 0;
            //_tempItem.Sad_srn_qty = 0;
            //_tempItem.Sad_tot_amt = Convert.ToDecimal(txtUnitAmt.Text);
            //_tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            //_tempItem.Sad_uom = "";
            //_tempItem.Sad_warr_based = false;
            //_tempItem.Mi_longdesc = _itemdetail.Mi_longdesc;
            //_tempItem.Mi_itm_tp = _itemdetail.Mi_itm_tp;
            //_tempItem.Mi_brand = _itemdetail.Mi_brand;
            //_tempItem.Mi_cate_1 = _itemdetail.Mi_cate_1;
            //_tempItem.Mi_cate_2 = _itemdetail.Mi_cate_2;
            //_tempItem.Sad_job_line = 0;
            //_tempItem.Sad_warr_period = 0;
            //_tempItem.Sad_warr_remarks = "";
            //_tempItem.Sad_sim_itm_cd = txtItem.Text;
            //_tempItem.Sad_merge_itm = "0";   // _itemdetail.Mi_itm_tp != "M" ? "0" : Convert.ToString(SSPRomotionType);
            //_invoiceItem.Add(_tempItem);
            //#endregion

            //#region add receipt header
            //RecieptHeader _recHeader = new RecieptHeader();
            //{
            //    MasterBusinessEntity GetCustomerProfile = CHNLSVC.Sales.GetCustomerProfile(txtCustCode.Text, string.Empty, string.Empty, string.Empty, string.Empty);
            //    _recHeader.Sar_acc_no = "";
            //    _recHeader.Sar_act = true;
            //    _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
            //    _recHeader.Sar_comm_amt = 0;
            //    _recHeader.Sar_create_by = BaseCls.GlbUserID;
            //    _recHeader.Sar_create_when = DateTime.Now;
            //    _recHeader.Sar_currency_cd = "LKR";
            //    _recHeader.Sar_debtor_add_1 = GetCustomerProfile.Mbe_add1;
            //    _recHeader.Sar_debtor_add_2 = GetCustomerProfile.Mbe_add2;
            //    _recHeader.Sar_debtor_cd = txtCustCode.Text;
            //    _recHeader.Sar_debtor_name = GetCustomerProfile.Mbe_name;
            //    _recHeader.Sar_direct = true;
            //    _recHeader.Sar_direct_deposit_bank_cd = "";
            //    _recHeader.Sar_direct_deposit_branch = "";
            //    _recHeader.Sar_epf_rate = 0;
            //    _recHeader.Sar_esd_rate = 0;
            //    _recHeader.Sar_is_mgr_iss = false;
            //    _recHeader.Sar_is_oth_shop = false;
            //    _recHeader.Sar_is_used = false;
            //    _recHeader.Sar_manual_ref_no = "";
            //    _recHeader.Sar_mob_no = txtMobile.Text;
            //    _recHeader.Sar_mod_by = BaseCls.GlbUserID;
            //    _recHeader.Sar_mod_when = DateTime.Now;
            //    _recHeader.Sar_nic_no = GetCustomerProfile.Mbe_nic;
            //    _recHeader.Sar_oth_sr = "";
            //    _recHeader.Sar_prefix = "";
            //    _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
            //    _recHeader.Sar_receipt_date = dtpDate.Value.Date;
            //    _recHeader.Sar_receipt_no = "na";
            //    _recHeader.Sar_receipt_type = _invoiceType == "CRED" ? "DEBT" : "DIR";
            //    _recHeader.Sar_ref_doc = "";
            //    _recHeader.Sar_remarks = "";
            //    _recHeader.Sar_seq_no = 1;
            //    _recHeader.Sar_ser_job_no = "";
            //    _recHeader.Sar_session_id = GlbUserSessionID;
            //    _recHeader.Sar_tel_no = GetCustomerProfile.Mbe_mob;
            //    //  _recHeader.Sar_tot_settle_amt = ucPayModes1.TotalAmount;
            //    _recHeader.Sar_uploaded_to_finance = false;
            //    _recHeader.Sar_used_amt = 0;
            //    _recHeader.Sar_wht_rate = 0;
            //}

            //#endregion

            //int reLine = 0;
            //foreach (RecieptItem _reCitem in _recieptItem)
            //{
            //    reLine = reLine + 1;
            //    _reCitem.Sard_line_no = reLine;
            //}
          


            string _agrNumber = string.Empty;
            int result = CHNLSVC.Sales.Process_Service_Agreement(oHeader, _agrAuto, oAgr_Items, oAgr_Ses, oAgr_CoverItems, oAgr_Cha, oAgr_Payshed, _invoiceAuto, out _agrNumber);

            if (result > 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Record insert successfully.\n Agreement No :" + _agrNumber, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearAll();

                return;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Process Terminated." + "\nError is " + _agrNumber, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }


        private bool IsVirtual(string _type)
        { if (_type == "V") { _IsVirtualItem = true; return true; } else { _IsVirtualItem = false; return false; } }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
                btnSave.Enabled = true;
            }
        }

        #endregion events

        #region Methods

        private void GetAgrType()
        {

        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;

            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ValidatItemAdd()
        {
            bool status = false;
            if (!string.IsNullOrEmpty(txtESerial.Text))
            {
                //kapila 10/11/2015
                DataTable _dt = CHNLSVC.CustService.getSCVAGRITM_bySer(txtESerial.Text);
                if (_dt.Rows.Count > 0)
                {
                    status = true;
                    MessageBox.Show("Already this serial has an agreement", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEItem.Focus();
                    return status;
                }
            }
            if (string.IsNullOrEmpty(txtEItem.Text))
            {
                status = true;
                MessageBox.Show("Please selete a item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEItem.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtAttempt.Text))
            {
                status = true;
                MessageBox.Show("Please enter the attempts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAttempt.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtEQty.Text))
            {
                status = true;
                MessageBox.Show("Please enter the quantity", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEQty.Focus();
                return status;
            }
            if (Convert.ToDateTime(dtFrom.Value.Date) > Convert.ToDateTime(dtTo.Value.Date))
            {
                status = true;
                MessageBox.Show("Invalid date range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return status;

            }
            if (Convert.ToDateTime(dtTo.Value.Date) > Convert.ToDateTime(dtRenew.Value.Date))
            {
                status = true;
                MessageBox.Show("Invalid renew date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return status;
            }
            if (_isPart == 1 && grvCovItm.Rows.Count == 0)
            {
                status = true;
                MessageBox.Show("Please add cover items", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return status;
            }
            return status;
        }

        private bool validateSave()
        {
            bool status = false;

            if (String.IsNullOrEmpty(txtType.Text))
            {
                status = true;
                MessageBox.Show("Please select the Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtType.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtClaimType.Text))
            {
                status = true;
                MessageBox.Show("Please select the Claim Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtClaimType.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtAttempt.Text))
            {
                status = true;
                MessageBox.Show("Please enter the no of attempts", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAttempt.Focus();
                return status;
            }
            //if (String.IsNullOrEmpty(txtItem.Text))
            //{
            //    status = true;
            //    MessageBox.Show("Please select the charge item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtItem.Focus();
            //    return status;
            //}
            if (Convert.ToDecimal(txtTotSch.Text) != Convert.ToDecimal(lblGrndTotalAmount.Text))
            {
                status = true;
                MessageBox.Show("Payment schedule is not completed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return status;
            }
            if (dtTo.Value.Date < dtFrom.Value.Date)
            {
                status = true;
                MessageBox.Show("Invalid date range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return status;
            }
            if (string.IsNullOrEmpty(txtTown.Text))
            {
                status = true;
                MessageBox.Show("select town ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTown.Focus();
                return status;
            }
            //if (dtFrom.Value.Date < dtpDate.Value.Date)
            //{
            //    status = true;
            //    MessageBox.Show("Invalid from date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return status;
            //}
            //if (dtTo.Value.Date < dtpDate.Value.Date)
            //{
            //    status = true;
            //    MessageBox.Show("Invalid to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return status;
            //}
            //if (dtRenew.Value.Date < dtpDate.Value.Date)
            //{
            //    status = true;
            //    MessageBox.Show("Invalid renew date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return status;
            //}
            return status;
        }

        private void ClearVariable()
        {
            btnSave.Enabled = true;

        }

        private void clearAll()
        {

            ServiceAgreement formnew = new ServiceAgreement();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
            //ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            //ucPayModes1.PaidAmountLabel.Text = "0.00";
            //ucPayModes1.ClearControls();

            //ucPayModes1.InvoiceType = "SAG";
            //ucPayModes1.LoadData();

            //txtAttempt.Enabled = true;

            //lblSeq.Text = "";
            //txtAttempt.Enabled = true;
            //dtFrom.Enabled = true;
            //dtTo.Enabled = true;
            //_recieptItem = new List<RecieptItem>();

        }

        #endregion Methods

        private void ServiceAgreement_Load(object sender, EventArgs e)
        {
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam == null)
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
                return;
            }


            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());


            //ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            //ucPayModes1.PaidAmountLabel.Text = "0.00";
            //ucPayModes1.ClearControls();
            //ucPayModes1.InvoiceItemList = null;
            //ucPayModes1.SerialList = null;

            //ucPayModes1.InvoiceType = "SAG";
            //ucPayModes1.LoadData();

            txtAttempt.Enabled = true;

            lblSeq.Text = "";
            txtAttempt.Enabled = true;
            dtFrom.Enabled = true;
            dtTo.Enabled = true;
            txtClaimType.Enabled = true;
            btn_Srch_EstiType.Enabled = true;

            _recieptItem = new List<RecieptItem>();

            GetAgrType();
            bind_period_basis();

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (ValidatItemAdd())
            {
                return;
            }
            //Int32 _months = (dtTo.Value.Date.Year * dtTo.Value.Date.Month) - (dtFrom.Value.Date.Year * dtFrom.Value.Date.Month);
            var _var = Math.Abs((dtTo.Value.Date.Year * 12 + (dtTo.Value.Date.Month - 1)) - (dtFrom.Value.Date.Year * 12 + (dtFrom.Value.Date.Month - 1)));
            Int32 _months = _var;
            if (_months % Convert.ToInt32(txtAttempt.Text) != 0)
            {
                MessageBox.Show("Agreement period can't divide totally by no of attempt !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No) return;

            if (oAgr_Items.Count > 0)
            {
                if (oAgr_Items.FindAll(x => x.Shi_itm_cd == txtEItem.Text && x.Shi_ser1 == txtESerial.Text).Count > 0)
                {
                    SCV_AGR_ITM oItemExist = oAgr_Items.Find(x => x.Shi_itm_cd == txtEItem.Text && x.Shi_ser1 == txtESerial.Text);
                    if (oItemExist != null)
                    {
                        oItemExist.Sai_seq_no = 0;
                        oItemExist.Sai_agr_no = txtAgrNo.Text;
                        oItemExist.Shi_itm_cd = txtEItem.Text;
                        oItemExist.Shi_itm_desc = txtEItemDesc.Text;
                        oItemExist.Shi_brand = txtEBrand.Text;
                        oItemExist.Shi_model = txtEModel.Text;
                        oItemExist.Shi_ser1 = txtESerial.Text;
                        oItemExist.Shi_ser2 = txtESerial2.Text;
                        oItemExist.Shi_ser_id = "0";
                        oItemExist.Shi_warrno = txtEWarr.Text;
                        oItemExist.Shi_warr_stus = false;
                        oItemExist.Shi_regno = "";
                        oItemExist.Shi_cate1 = txtCat1.Text;
                        oItemExist.Shi_cate2 = txtCat2.Text;
                        oItemExist.Shi_cate3 = txtCat3.Text;
                        oItemExist.Shi_qty = Convert.ToInt32(txtEQty.Text);
                        oItemExist.Shi_inv_no = txtEInvNo.Text;
                        //oItemExist.Shi_sessions = 

                        BindAddItem();


                        return;
                    }
                    txtAttempt.Enabled = false;
                    return;
                }
            }
            SCV_AGR_ITM _items = new SCV_AGR_ITM();
            _items.Sai_seq_no = 0;
            _items.Sai_agr_no = txtAgrNo.Text;
            _items.Sai_line = _itemLine;
            _items.Shi_itm_cd = txtEItem.Text;
            _items.Shi_itm_desc = txtEItemDesc.Text;
            _items.Shi_brand = txtEBrand.Text;
            _items.Shi_model = txtEModel.Text;
            _items.Shi_ser1 = txtESerial.Text;
            _items.Shi_ser2 = txtESerial2.Text;
            _items.Shi_ser_id = "0";
            _items.Shi_warrno = txtEWarr.Text;
            _items.Shi_warr_stus = false;
            _items.Shi_regno = "";
            _items.Shi_cate1 = txtCat1.Text;
            _items.Shi_cate2 = txtCat2.Text;
            _items.Shi_cate3 = txtCat3.Text;
            _items.Shi_qty = Convert.ToInt32(txtEQty.Text);
            _items.Shi_sessions = Convert.ToInt32(txtAttempt.Text);
            _items.Shi_inv_no = txtEInvNo.Text;


            oAgr_Items.Add(_items);

            //add sessions
            DateTime _stDate = dtFrom.Value.Date;
            DateTime _enDate = dtTo.Value.Date;

            Int32 _due = _months / Convert.ToInt32(txtAttempt.Text);
            for (Int32 i = 0; i < Convert.ToInt32(txtAttempt.Text); i++)
            {
                if (i == 0)
                {
                    _enDate = _stDate.AddMonths(_due);
                    _enDate = _enDate.AddDays(-1);
                    add_sessions(i + 1, _stDate, _enDate);
                    _stDate = _enDate.AddDays(1);
                }
                else if (i == Convert.ToInt32(txtAttempt.Text) - 1)
                {
                    _enDate = dtTo.Value.Date.AddMonths(_due);
                    _enDate = dtTo.Value.Date.AddDays(-1);
                    add_sessions(i + 1, _stDate, _enDate);
                }
                else
                {
                    _enDate = _stDate.AddMonths(_due);
                    _enDate = _enDate.AddDays(-1);
                    add_sessions(i + 1, _stDate, _enDate);
                    _stDate = _enDate.AddDays(1);
                }

            }
            //add cover items
            if (grvCovItm.Rows.Count > 0)
            {
                Int32 i = 1;
                foreach (DataGridViewRow row in grvCovItm.Rows)
                {
                    SCV_AGR_COVER_ITM _agrCovItm = new SCV_AGR_COVER_ITM();
                    _agrCovItm.Saic_agr_no = "";
                    _agrCovItm.Saic_itm_cd = Convert.ToString(row.Cells["saic_itm_cd"].Value);
                    _agrCovItm.Saic_itm_desc = Convert.ToString(row.Cells["saic_itm_cd"].Value);
                    _agrCovItm.Saic_itm_line = _itemLine;
                    _agrCovItm.Saic_line = i;
                    _agrCovItm.Saic_seq_no = 0;
                    oAgr_CoverItems.Add(_agrCovItm);
                    i = i + 1;

                }
            }

            tmp_CoverItems = new List<SCV_AGR_COVER_ITM>();

            BindItemSession(_itemLine);
            BindAddItem();

            _itemLine = _itemLine + 1;

            if (MessageBox.Show("Do you want to add another item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                txtEItem.Focus();
            if (string.IsNullOrEmpty( txtEQty.Text))
            {
                MessageBox.Show("Pleas enter qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clear_item();
            txtAttempt.Enabled = false;
            dtFrom.Enabled = false;
            dtTo.Enabled = false;
            txtClaimType.Enabled = false;
            btn_Srch_EstiType.Enabled = false;
        }

        private void add_sessions(Int32 _line, DateTime _fDate, DateTime _tDate)
        {
            SCV_AGR_SES _sess = new SCV_AGR_SES();
            _sess.Saga_agr_no = "";
            _sess.Saga_from_dt = _fDate;
            _sess.Saga_itm_line = _itemLine;
            _sess.Saga_line = _line;
            _sess.Saga_seq_no = 0;
            _sess.Saga_stus = "A";
            _sess.Saga_to_dt = _tDate;
            oAgr_Ses.Add(_sess);
        }

        private void clear_item()
        {
            txtESerial2.Text = "";
            txtESerial.Text = "";
            txtEWarr.Text = "";
            txtEInvNo.Text = "";
            txtEItem.Text = "";
            txtEModel.Text = "";
            txtEBrand.Text = "";
            txtCat1.Text = "";
            txtCat2.Text = "";
            txtCat3.Text = "";
            txtEItemDesc.Text = "";
            txtEQty.Text = "";
            grvCovItm.AutoGenerateColumns = false;
            grvCovItm.DataSource = null;
        }

        protected void BindAddItem()
        {
            dgvAgrItems.AutoGenerateColumns = false;
            dgvAgrItems.DataSource = new List<SCV_AGR_ITM>();
            dgvAgrItems.DataSource = oAgr_Items;
        }
        protected void BindItemSession(Int32 _itmLine)
        {
            List<SCV_AGR_SES> _tmpSess = (from _rec in oAgr_Ses
                                          where _rec.Saga_itm_line == _itmLine
                                          select _rec).ToList<SCV_AGR_SES>();

            dgvAgrSess.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _tmpSess;
            dgvAgrSess.DataSource = _source;
        }



        private void btn_srch_itm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEItem;
            _CommonSearch.ShowDialog();
            txtEItem.Focus();

            txtEItem_Leave(null, null);
        }

        private void txtEItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_itm_Click(null, null);
        }

        private void txtEItem_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_itm_Click(null, null);
        }

        private void txtEItem_Leave(object sender, EventArgs e)
        {
            //txtQty.Enabled = false;
            if (!string.IsNullOrEmpty(txtEItem.Text))
            {
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtEItem.Text.ToUpper());
                if (_item != null)
                {
                    txtEItemDesc.Text = _item.Mi_shortdesc;
                    txtEModel.Text = _item.Mi_model;
                    txtEBrand.Text = _item.Mi_brand;
                    txtCat1.Text = _item.Mi_cate_1;
                    txtCat2.Text = _item.Mi_cate_2;
                    txtCat3.Text = _item.Mi_cate_3;
                    if (_item.Mi_is_ser1 == 0)
                    {
                        txtEQty.Enabled = true;
                        txtEQty.Text = "0";
                    }
                    else
                    {
                        txtEQty.Text = "1";
                        txtEQty.Enabled = false;
                    }

                }
                else
                {
                    MessageBox.Show("Invalid item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEItem.Focus();
                    return;
                }
            }
        }

        private void btnSearch_CustCode_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch.ReturnIndex = 0;
            _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _commonSearch.dvResult.DataSource = _result;
            _commonSearch.BindUCtrlDDLData(_result);
            _commonSearch.obj_TragetTextBox = txtCustCode;
            _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
            this.Cursor = Cursors.Default;
            _commonSearch.ShowDialog();
            txtCustCode.Select();
        }

        private void txtCustCode_Leave(object sender, EventArgs e)
        {
            if (txtCustCode.Text == "CASH")
            {
                MessageBox.Show("Please enter a valid customer code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCustCode.Text = "";
                txtCustCode.Focus();
                return;
            }

            LoadCustomerDetailsByCustomer();
            FillPriority();
        }

        private void FillPriority()
        {
            List<MST_BUSPRIT_LVL> oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(txtCustCode.Text.Trim(), BaseCls.GlbUserComCode);
            if (oItems != null && oItems.Count > 0)
            {
                ucServicePriority1.GblCustCode = txtCustCode.Text.Trim();
                ucServicePriority1.LoadData();
            }
            else
            {
                ucServicePriority1.GblCustCode = "CASH";
                ucServicePriority1.LoadData();
            }
        }

        protected void LoadCustomerDetailsByCustomer()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustCode.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH";
                        EnableDisableCustomer();
                    }
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCode.Text, null, null, null, null, null);
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCustCode.Focus();
                        return;
                    }


                    ////if (txtCustCode.Text.Trim() != "CASH")
                    ////{
                    ////    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtCustCode.Text.Trim());
                    ////    if (_table != null && _table.Rows.Count > 0)
                    ////    {
                    ////        if (_table.Select("MBSA_SA_TP = 'CRED'").Length > 0)
                    ////            _invoiceType = "CRED";
                    ////    }
                    ////    else
                    ////    {
                    ////        MessageBox.Show("Selected Customer is not allow for enter transaction.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////        txtCustCode.Text = "";
                    ////        txtCustCode.Focus();
                    ////        return;
                    ////    }

                    ////}

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                        //ClearCustomer(false);
                    }
                    else
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(true);
                    txtCustCode.Focus();
                    return;
                }
                EnableDisableCustomer();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCustCode.Clear();
            txtCusName.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtMobile.Clear();
            txtNIC.Clear();
            txtTown.Clear();
            txtCusName.Enabled = true;
            txtAddress1.Enabled = true;
            txtAddress2.Enabled = true;
            txtMobile.Enabled = true;
            txtNIC.Enabled = true;

        }

        private void EnableDisableCustomer()
        {
            if (txtCustCode.Text == "CASH")
            {
                txtCustCode.Enabled = true;
                txtCusName.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtMobile.Enabled = true;
                txtNIC.Enabled = true;

                btnSearch_NIC.Enabled = true;
                btnSearch_CustCode.Enabled = true;
                btnSearch_Mobile.Enabled = true;
            }
            else
            {
                txtCusName.Enabled = false;
                txtAddress1.Enabled = false;
                txtAddress2.Enabled = false;
                txtMobile.Enabled = false;
                txtNIC.Enabled = false;

            }
        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
            txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
            txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            cmbTitle.Text = _masterBusinessCompany.MBE_TIT;
            //ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            //ucPayModes1.Mobile = txtMobile.Text.Trim();

            if (_isRecall == false)
            {
                //txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                //txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;
                //txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                //txtDelName.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtAddress1.Text = _hdr.Sah_cus_add1;
                txtAddress2.Text = _hdr.Sah_cus_add2;

                //txtDelAddress1.Text = _hdr.Sah_d_cust_add1;
                //txtDelAddress2.Text = _hdr.Sah_d_cust_add2;
                //txtDelCustomer.Text = _hdr.Sah_d_cust_cd;
                //txtDelName.Text = _hdr.Sah_d_cust_name;
            }

            if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
            if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
            GetNICGender();
            if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
            else
            {
                string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                bool _exist = cmbTitle.Items.Contains(_title);
                if (_exist)
                    cmbTitle.Text = _title;
            }
            if (_masterBusinessCompany.Mbe_is_tax) 
            { 
                chkTaxPayable.Checked = true; 
                chkTaxPayable.Enabled = true;
                LoadTaxDetail(_masterBusinessCompany);

            }
            else
            {
                chkTaxPayable.Checked = false; 
                chkTaxPayable.Enabled = false;
                LoadTaxDetail(_masterBusinessCompany);
            }
        }
        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }
        private void GetNICGender()
        {
            String nic_ = txtNIC.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = (nicarray[2]).ToString();
            if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
            { cmbTitle.Text = "MS."; }
            else
            { cmbTitle.Text = "MR."; }
        }

        private void btnSearchType_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AgrType);
            DataTable _result = CHNLSVC.CommonSearch.GetAgrTypeSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtType;
            _CommonSearch.ShowDialog();
            txtType.Focus();
            getAgrType();
        }

        private void btn_Srch_EstiType_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AgrClaimType);
            DataTable _result = CHNLSVC.CommonSearch.GetAgrClaimTypeSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtClaimType;
            _CommonSearch.ShowDialog();
            txtClaimType.Focus();
            getAgrClaimType();
        }

        private void txtType_DoubleClick(object sender, EventArgs e)
        {
            btnSearchType_Click(null, null);
        }

        private void txtClaimType_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_EstiType_Click(null, null);
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchType_Click(null, null);
        }

        private void txtClaimType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_EstiType_Click(null, null);
        }

        private void txtType_Leave(object sender, EventArgs e)
        {
            getAgrType();
        }

        private void getAgrType()
        {
            if (!string.IsNullOrEmpty(txtType.Text))
            {
                DataTable _dt = CHNLSVC.CustService.GetAgrType(txtType.Text);
                if (_dt.Rows.Count > 0)
                    lblType.Text = _dt.Rows[0]["sagt_desc"].ToString();
                else
                {
                    MessageBox.Show("Please enter a valid type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lblType.Text = "";
                    txtType.Focus();
                }

            }
        }

        private void getAgrClaimType()
        {
            _isPart = 0;
            btnCovItm.Enabled = false;

            if (!string.IsNullOrEmpty(txtClaimType.Text))
            {
                DataTable _dt = CHNLSVC.CustService.GetAgrClaimType(txtClaimType.Text);
                if (_dt.Rows.Count > 0)
                {
                    lblEstimate.Text = _dt.Rows[0]["sagc_desc"].ToString();
                    _isPart = Convert.ToInt32(_dt.Rows[0]["sagc_is_part"]);
                }
                else
                {
                    MessageBox.Show("Please enter a valid type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lblEstimate.Text = "";
                    txtClaimType.Focus();
                }
            }
            if (_isPart == 1)
                btnCovItm.Enabled = true;
        }

        private void txtClaimType_Leave(object sender, EventArgs e)
        {
            getAgrClaimType();
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustCode;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustCode.Select();
                txtCustCode_Leave(null, null);
            }
            catch (Exception ex)
            { txtCustCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtMobile;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtMobile.Select();
                if (_commonSearch.GlbSelectData == null) return;
                string[] args = _commonSearch.GlbSelectData.Split('|');
                string _cuscode = args[4];
                if (string.IsNullOrEmpty(txtCustCode.Text) || txtCustCode.Text.Trim() == "CASH") txtCustCode.Text = _cuscode;
                else if (txtCustCode.Text.Trim() != _cuscode && txtCustCode.Text.Trim() != "CASH")
                {
                    DialogResult _res = MessageBox.Show("Currently selected customer code " + txtCustCode.Text + " is differ which selected (" + _cuscode + ") from here. Do you need to change current customer code from selected customer", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_res == System.Windows.Forms.DialogResult.Yes)
                    {
                        txtCustCode.Text = _cuscode;
                        txtCustCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btn_srch_town_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                _CommonSearch.IsSearchEnter = true;
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTown;
                _CommonSearch.ShowDialog();
                txtTown.Select();
                txtTown.Focus();
            }
            catch (Exception ex) { txtCustCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtNIC;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtNIC.Select();
            }
            catch (Exception ex)
            { txtNIC.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btn_srch_serial_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "SER";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtESerial;
                _CommonSearch.ShowDialog();
                txtESerial.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                //DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtUnitPrice.Focus();
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

        private void load_charge()
        {
            //ucPayModes1.TotalAmount = Convert.ToDecimal(txtUnitAmt.Text);
            //ucPayModes1.Amount.Text = Convert.ToString(ucPayModes1.TotalAmount - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text));
            //ucPayModes1.Date = Convert.ToDateTime(dtpDate.Value.Date);
            //ucPayModes1.IsZeroAllow = true;
            //ucPayModes1.Customer_Code = txtCustCode.Text.Trim();
            //ucPayModes1.LoadData();
        }

        private void txtUnitAmt_TextChanged(object sender, EventArgs e)
        {
            load_charge();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            //if(Convert.ToDecimal(txtUnitPrice.Text)<0)
            //{
            //    SystemInformationMessage("Invalid unit price.", "Warning");
            //    return;
            //}
            if (!string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));
                CalculateItem();
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));
            CalculateItem();
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            //else return RoundUpForPlace(value, 2);
            else return Math.Round(value, 2);
        }

        private void txtESerial_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtESerial.Text))
                Load_Serial_Infor(txtESerial, string.Empty);
        }

        private void Load_Serial_Infor(TextBox _txt, string _warrNo)
        {
            if (ChkExternal.Checked == true)
            {

            }
            else
            {
                int _returnStatus = 0;
                string _returnMsg = string.Empty;
                List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
                List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
                Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;

                string _ser = null;
                string _warr = null;
                string _regno = null;
                string _invcno = null;

                if (string.IsNullOrEmpty(_warrNo))
                {
                    if (_txt == txtESerial) _ser = txtESerial.Text.ToString();
                    if (_txt == txtEWarr) _warr = txtEWarr.Text.ToString();
                    // if (_txt == txtRegNo) _regno = txtWar.Text.ToString();
                }
                else
                {
                    _warr = _warrNo;
                }

                //_warrMstDic = CHNLSVC.CustService.GetWarrantyMasterAgr(_ser, "", _regno, _warr, "", "", 0, out _returnStatus, out _returnMsg);
                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, "", _regno, _warr, "", "", 0, out _returnStatus, out _returnMsg);
                if (_warrMstDic == null)
                {
                    SystemInformationMessage("There is no warranty details available.", "No warranty");
                    _txt.Clear();
                    _txt.Focus();
                    return;
                }

                foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                {
                    _warrMst = pair.Key;
                    _warrMstSub = pair.Value;
                }
                if (_warrMst == null)
                {
                    SystemInformationMessage("There is no warranty details available.", "No warranty");
                    _txt.Clear();
                    _txt.Focus();
                    return;
                }
                if (_warrMst.Count <= 0)
                {
                    SystemInformationMessage("There is no warranty details available.", "No warranty");
                    _txt.Clear();
                    _txt.Focus();
                    return;
                }


                if (_warrMst.Count > 0)
                {
                    txtESerial.Text = _warrMst[0].Irsm_ser_1.ToString();
                    txtEWarr.Text = _warrMst[0].Irsm_warr_no.ToString();
                    txtEItem.Text = _warrMst[0].Irsm_itm_cd.ToString();
                    txtEInvNo.Text = _warrMst[0].Irsm_invoice_no.ToString();
                    //txtEBrand.Text = _warrMst[0].Irsm_itm_brand.ToString();
                    //txtEModel.Text = _warrMst[0].Irsm_itm_model.ToString();
                    //txtEItemDesc.Text = _warrMst[0].Irsm_itm_desc.ToString();
                    //MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtEItem.Text);
                    //txtCat1.Text = _mstItm.Mi_cate_1;
                    //txtCat2.Text = _mstItm.Mi_cate_2;
                    //txtCat3.Text = _mstItm.Mi_cate_3;
                    txtCustCode.Text = _warrMst[0].Irsm_cust_cd;
                    txtCustCode_Leave(null, null);
                    txtEItem_Leave(null, null);
                }

            }
        }

        //private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        _recieptItem = ucPayModes1.RecieptItemList;
        //        decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);

        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default; SystemErrorMessage(ex);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
        //    }
        //}

        private void dgvAgrItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 _tmpLine = Convert.ToInt32(dgvAgrItems.Rows[e.RowIndex].Cells["sai_line"].Value);
                    oAgr_Items.RemoveAt(e.RowIndex);

                    oAgr_Ses.RemoveAll(x => x.Saga_itm_line == _tmpLine);

                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = oAgr_Items;
                    dgvAgrItems.DataSource = _bnding;

                    dgvAgrSess.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = oAgr_Ses;
                    dgvAgrSess.DataSource = _source;

                }
            }
            else
            {
                Int32 _selItmline = Convert.ToInt32(dgvAgrItems.Rows[e.RowIndex].Cells["sai_line"].Value);
                txtEItem.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_ITM_CD"].Value.ToString();
                txtEBrand.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_BRAND"].Value.ToString();
                txtEItemDesc.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_ITM_DESC"].Value.ToString();
                txtEModel.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_MODEL"].Value.ToString();
                txtEQty.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_QTY"].Value.ToString();
                txtESerial.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_SER1"].Value.ToString();
                txtESerial2.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_SER2"].Value.ToString();
                txtEWarr.Text = dgvAgrItems.Rows[e.RowIndex].Cells["SHI_WARRNO"].Value.ToString();
                txtCat1.Text = dgvAgrItems.Rows[e.RowIndex].Cells["Shi_cate1"].Value.ToString();
                txtCat2.Text = dgvAgrItems.Rows[e.RowIndex].Cells["Shi_cate2"].Value.ToString();
                txtCat3.Text = dgvAgrItems.Rows[e.RowIndex].Cells["Shi_cate3"].Value.ToString();
                txtEInvNo.Text = dgvAgrItems.Rows[e.RowIndex].Cells["Shi_inv_no"].Value.ToString();
                BindItemSession(_selItmline);

            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.ToUpper());
                if (_item == null)
                {
                    MessageBox.Show("Please enter a valid item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItem.Focus();
                }
                else
                {
                    if (_item.Mi_itm_tp != "V")
                    {
                        MessageBox.Show("Please enter a valid virtual item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItem.Focus();
                    }
                }
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtUnitPrice.Select();
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Select();
        }

        private void dgvAgrItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCloseCovItm_Click(object sender, EventArgs e)
        {
            pnlCovItm.Visible = false;
        }

        private void btnCovItm_Click(object sender, EventArgs e)
        {
            pnlCovItm.Visible = true;
        }

        private void btn_srch_covitm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCItem;
            _CommonSearch.ShowDialog();
            txtCItem.Focus();

            AddCoverItems();
        }

        private void txtCItem_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    AddCoverItems();
            if (e.KeyCode == Keys.F2)
                btn_srch_covitm_Click(null, null);
        }

        private void AddCoverItems()
        {
            SCV_AGR_COVER_ITM _agrCovItm = new SCV_AGR_COVER_ITM();
            _agrCovItm.Saic_itm_cd = Convert.ToString(txtCItem.Text);

            tmp_CoverItems.Add(_agrCovItm);

            grvCovItm.AutoGenerateColumns = false;
            grvCovItm.DataSource = new List<SCV_AGR_COVER_ITM>();
            grvCovItm.DataSource = tmp_CoverItems;

            //foreach (DataGridViewRow row in grvCovItm.Rows)
            //{
            //    if (row.Cells[0].ToString()!=txtCItem.Text)
            //    {
            //        Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            //        PartyTypes.Add("ITEM", txtCItem.Text);
            //        PartyTypes.Add("DESCRIPTION", txtCItmDesc.Text);

            //        grvCovItm.AutoGenerateColumns = false;
            //        grvCovItm.DataSource = new BindingSource(PartyTypes, null);

            //    }
            //}
        }

        private void btnJobSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Agreement);
            DataTable _result = CHNLSVC.CommonSearch.SearchAgreementData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAgrNo;
            _CommonSearch.ShowDialog();
            txtAgrNo.Focus();

            load_agreement();
            clear_charge();
            txtSchAmt.Text = "0.00";
        }

        private void load_agreement()
        {
            SCV_AGR_HDR _scvAgrHdr = new SCV_AGR_HDR();
            oAgr_Items = new List<SCV_AGR_ITM>();
            oAgr_Ses = new List<SCV_AGR_SES>();
            oAgr_Payshed = new List<scv_agr_payshed>();
            oAgr_Cha = new List<scv_agr_cha>();

            int _returnStatus = 0;
            string _returnMsg = string.Empty;

            if (!string.IsNullOrEmpty(txtAgrNo.Text))
            {
                _returnStatus = CHNLSVC.CustService.GetScvAgreement(BaseCls.GlbUserComCode, txtAgrNo.Text, out _scvAgrHdr, out  oAgr_Items, out  oAgr_Ses, out oAgr_Cha, out oAgr_Payshed, out  _returnMsg);
                if (_returnStatus != 1)
                {
                    SystemInformationMessage(_returnMsg, "Service Agreement");
                    txtAgrNo.Clear();
                    txtAgrNo.Focus();
                    return;
                }
                if (_scvAgrHdr.Sag_stus == "F")
                {
                    btnGen.Enabled = true;
                }
                else
                { btnGen.Enabled = false; }
                txtType.Text = _scvAgrHdr.Sag_tp;
                txtClaimType.Text = _scvAgrHdr.Sag_clm_tp;
                chk.Checked = _scvAgrHdr.Sag_multi_pc;
                dtFrom.Value = _scvAgrHdr.Sag_commdt;
                dtTo.Value = _scvAgrHdr.Sag_exdt;
                dtRenew.Value = _scvAgrHdr.Sag_rewldt;
                txtCustCode.Text = _scvAgrHdr.Sag_custcd;
                txtTown.Text = _scvAgrHdr.Sag_cust_town;
                if (!string.IsNullOrEmpty(_scvAgrHdr.Sag_cust_town))
                {
                    DataTable odt = CHNLSVC.General.GetTownByCode(_scvAgrHdr.Sag_cust_town);
                    if (odt.Rows.Count > 0)
                    {
                        txtTown.Text = odt.Rows[0]["MT_DESC"].ToString();

                    }
                }
                //_scvAgrHdr.Sag_town_rmk = 
                txtContPersn.Text = _scvAgrHdr.Sag_cont_person;
                txtContLoc.Text = _scvAgrHdr.Sag_cont_add;
                txtContNo.Text = _scvAgrHdr.Sag_cont_no;
                txtAttempt.Text = _scvAgrHdr.Sag_ser_attempt.ToString();
                txtEquipLoc.Text = _scvAgrHdr.Sag_town_rmk;
                txtTAC.Text = _scvAgrHdr.Sag_tac;
                txtTOP.Text = _scvAgrHdr.Sag_top;
                txtWrkInc.Text = _scvAgrHdr.Sag_work_inc;
                txtFreq.Text = _scvAgrHdr.Sag_svc_freq;
                lblStus.Text = "";
                btnSave.Enabled = true;

                if (_scvAgrHdr.Sag_stus == "F")
                {
                    lblStus.Text = "Approved";
                    btnSave.Enabled = false;
                }
                else if (_scvAgrHdr.Sag_stus == "A")
                {
                    lblStus.Text = "Customer Approved";
                    btnSave.Enabled = false;
                }
                else if (_scvAgrHdr.Sag_stus == "P")
                    lblStus.Text = "Pending";
                else if (_scvAgrHdr.Sag_stus == "C")
                {
                    lblStus.Text = "Canceled";
                    btnSave.Enabled = false;
                }
                else if (_scvAgrHdr.Sag_stus == "R")
                {
                    lblStus.Text = "Rejected";
                    btnSave.Enabled = false;
                }
                else if (_scvAgrHdr.Sag_stus == "J")
                {
                    lblStus.Text = "Customer Reject";
                    btnSave.Enabled = false;
                }

                LoadCustomerDetailsByCustomer();
                FillPriority();

                dgvAgrItems.AutoGenerateColumns = false;
                dgvAgrItems.DataSource = new List<SCV_AGR_ITM>();
                dgvAgrItems.DataSource = oAgr_Items;

                grvCha.AutoGenerateColumns = false;
                grvCha.DataSource = new List<scv_agr_cha>();
                grvCha.DataSource = oAgr_Cha;

                grvSch.AutoGenerateColumns = false;
                grvSch.DataSource = new List<scv_agr_payshed>();
                grvSch.DataSource = oAgr_Payshed;

                dgvAgrSess.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = oAgr_Ses;
                dgvAgrSess.DataSource = _source;

                btnSave.Enabled = false;
            }
        }

        private void txtESerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_serial_Click(null, null);
        }

        private void txtESerial_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_serial_Click(null, null);
        }

        private void btn_srch_war_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "WARR";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEWarr;
                _CommonSearch.ShowDialog();
                txtEWarr.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }
        }

        private void txtCItem_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_covitm_Click(null, null);
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void txtNIC_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_NIC_Click(null, null);
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_NIC_Click(null, null);
        }

        private void txtTown_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_town_Click(null, null);
        }

        private void txtTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_town_Click(null, null);
        }

        private void txtCustCode_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_CustCode_Click(null, null);
        }

        private void txtCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_CustCode_Click(null, null);
        }

        private void txtMobile_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Mobile_Click(null, null);
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Mobile_Click(null, null);
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMobile.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Text = "";
                    txtMobile.Focus();
                    return;
                }
                else
                {
                    LoadCustomerDetailsByMobile();
                }
            }
        }

        protected void LoadCustomerDetailsByMobile()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH";
                        EnableDisableCustomer();
                    }
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid mobile", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMobile.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count > 1)
                        {
                            //Tempory removed by Chamal 26-04-2014
                            //if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _custList.Count + " number of customers are available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtMobile.Clear(); txtMobile.Focus(); return;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtCustCode.Text) && txtCustCode.Text.Trim() != "CASH")
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustCode.Text.Trim(), string.Empty, txtMobile.Text, "C");
                    else
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                }
                //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                        //ViewCustomerAccountDetail(txtCustomer.Text);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustCode.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, txtMobile.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                    }
                    else
                    {
                        _isGroup = false;
                    }
                }
                EnableDisableCustomer();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void SetCustomerAndDeliveryDetailsGroup(GroupBussinessEntity _cust)
        {
            txtCustCode.Text = _cust.Mbg_cd;
            txtCusName.Text = _cust.Mbg_name;
            txtAddress1.Text = _cust.Mbg_add1;
            txtAddress2.Text = _cust.Mbg_add2;
            txtMobile.Text = _cust.Mbg_mob;
            txtNIC.Text = _cust.Mbg_nic;
            cmbTitle.Text = _cust.Mbg_tit;
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid NIC.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    LoadCustomerDetailsByNIC();
                }
            }
        }

        protected void LoadCustomerDetailsByNIC()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH";
                        EnableDisableCustomer();
                    }
                    return;
                }

                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (!IsValidNIC(txtNIC.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNIC.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count > 1)
                        {
                            //Tempory removed by Chamal 26-04-2014
                            //if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _custList.Count + " number of active customers are available for the selected NIC.\nPlease contact Accounts Dept.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNIC.Clear(); txtNIC.Focus(); return;
                        }
                    }

                    //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNIC.Text, null, null, null, BaseCls.GlbUserComCode);
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                        //ViewCustomerAccountDetail(txtCustomer.Text);
                        GetNICGender();
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustCode.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, txtNIC.Text.Trim().ToUpper(), null, null, null, null);
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        GetNICGender();
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                    }
                }

                EnableDisableCustomer();
                txtMobile.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtTown_Leave(object sender, EventArgs e)
        {
            //txtPerDistrict.Text = "";
            //txtPerProvince.Text = "";
            //txtPerPostal.Text = "";
            //txtPerCountry.Text = "";

            if (!string.IsNullOrEmpty(txtTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                        txtTown.Tag = dt.Rows[0]["TOWN_ID"].ToString();
                        //txtPerDistrict.Text = district;
                        //txtPerProvince.Text = province;
                        //txtPerPostal.Text = postalCD;
                        //txtPerCountry.Text = countryCD;
                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTown.Text = "";
                        txtTown.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTown.Text = "";
                    txtTown.Focus();
                }
            }
        }

        private void txtEWarr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_war_Click(null, null);
        }

        private void txtEWarr_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_war_Click(null, null);
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (lblStus.Text == "Pending")
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int _eff = CHNLSVC.CustService.updateAgreement(txtAgrNo.Text, "A", BaseCls.GlbUserID);
                    MessageBox.Show("Successfully Approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Invalid agreement number.", "Agreement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContNo.Text = "";
            }
        }

        private void btnCustApprove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _eff = CHNLSVC.CustService.updateAgreement(txtAgrNo.Text, "F", BaseCls.GlbUserID);
                MessageBox.Show("Successfully Approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _eff = CHNLSVC.CustService.updateAgreement(txtAgrNo.Text, "C", BaseCls.GlbUserID);
                clearAll();
                MessageBox.Show("Successfully Cancelled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
        }

        private void txtContNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtContNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContNo.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtContNo.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContNo.Text = "";
                    txtContNo.Focus();
                    return;
                }
            }
        }

        private void txtEWarr_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEWarr.Text))
                Load_Serial_Infor(txtEWarr, txtEWarr.Text);
        }

        private void txtAttempt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAttempt.Text))
            {
                if (IsNumeric(txtAttempt.Text) == false)
                {
                    MessageBox.Show("Please select valid attempt", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAttempt.Focus();
                    return;
                }
            }
        }

        private void ServiceAgreement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnGenClose_Click(object sender, EventArgs e)
        {
            pnlGen.Hide();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            pnlGen.Show();
        }

        private void btnGenProcess_Click(object sender, EventArgs e)
        {
            Int32 _reqLine = 1;
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            List<Service_Req_Det> _reqDetList = new List<Service_Req_Det>();
            List<Service_Req_Def> _scvDefList = new List<Service_Req_Def>();

            foreach (DataGridViewRow row in grvAgr.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    #region fill request header
                    Service_Req_Hdr _custReqHdr = new Service_Req_Hdr();

                    _custReqHdr.Srb_dt = Convert.ToDateTime(row.Cells["SAG_EXDT"].Value);
                    _custReqHdr.Srb_com = BaseCls.GlbUserComCode;
                    _custReqHdr.Srb_jobcat = "WW";
                    _custReqHdr.Srb_jobtp = "I";
                    _custReqHdr.Srb_jobstp = "MTAGRMNT";
                    _custReqHdr.Srb_manualref = "";
                    _custReqHdr.Srb_otherref = row.Cells["sag_agr_no"].Value.ToString();
                    _custReqHdr.Srb_refno = "";  //aod #
                    _custReqHdr.Srb_jobstage = 1;
                    _custReqHdr.Srb_rmk = "";
                    _custReqHdr.Srb_prority = "NORMAL";
                    _custReqHdr.Srb_st_dt = Convert.ToDateTime(row.Cells["SAG_COMMDT"].Value);
                    _custReqHdr.Srb_ed_dt = Convert.ToDateTime(row.Cells["SAG_EXDT"].Value);
                    _custReqHdr.Srb_noofprint = 0;
                    _custReqHdr.Srb_lastprintby = "";
                    _custReqHdr.Srb_orderno = "";
                    _custReqHdr.Srb_custexptdt = Convert.ToDateTime(row.Cells["SAG_EXDT"].Value);
                    _custReqHdr.Srb_substage = "1";

                    //-----shop details
                    _custReqHdr.Srb_cust_cd = BaseCls.GlbUserDefProf;
                    MasterProfitCenter _mstPc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                    if (_mstPc != null)
                    {
                        //_custReqHdr.Srb_cust_tit = 
                        _custReqHdr.Srb_cust_name = _mstPc.Mpc_desc;
                        //_custReqHdr.Srb_nic = 
                        //_custReqHdr.Srb_dl = 
                        //_custReqHdr.Srb_pp = 
                        if (_mstPc.Mpc_tel.Length > 20)
                            _custReqHdr.Srb_mobino = (_mstPc.Mpc_tel).Substring(1, 20);
                        else
                            _custReqHdr.Srb_mobino = (_mstPc.Mpc_tel);
                        _custReqHdr.Srb_add1 = _mstPc.Mpc_add_1;
                        _custReqHdr.Srb_add2 = _mstPc.Mpc_add_2;
                        //_custReqHdr.Srb_add3 = 
                        //_custReqHdr.Srb_town =
                        _custReqHdr.Srb_phno = _mstPc.Mpc_tel;
                        _custReqHdr.Srb_faxno = _mstPc.Mpc_fax;
                        _custReqHdr.Srb_email = _mstPc.Mpc_email;
                        _custReqHdr.Srb_cnt_person = txtContPersn.Text;
                        _custReqHdr.Srb_cnt_add1 = txtContLoc.Text;
                        _custReqHdr.Srb_cnt_add2 = "";
                        _custReqHdr.Srb_cnt_phno = txtContNo.Text;
                        _custReqHdr.Srb_job_rmk = "";
                        //_custReqHdr.Srb_tech_rmk = 
                    }

                    //------original cust details
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, row.Cells["sag_custcd"].Value.ToString(), null, null, "C");

                    foreach (var _nicCust in _custList)
                    {
                        _custReqHdr.Srb_b_cust_cd = row.Cells["sag_custcd"].Value.ToString();

                        _custReqHdr.Srb_b_cust_name = _nicCust.Mbe_name;
                        _custReqHdr.Srb_b_mobino = _nicCust.Mbe_mob;
                        _custReqHdr.Srb_b_add1 = _nicCust.Mbe_add1;
                        _custReqHdr.Srb_b_add2 = _nicCust.Mbe_add2;
                        _custReqHdr.Srb_b_phno = _nicCust.Mbe_tel;
                        _custReqHdr.Srb_b_email = _nicCust.Mbe_email;
                        _custReqHdr.Srb_b_cust_tit = _nicCust.MBE_TIT;

                        _custReqHdr.Srb_b_nic = _nicCust.Mbe_nic;
                        _custReqHdr.Srb_b_dl = _nicCust.Mbe_dl_no;
                        _custReqHdr.Srb_b_pp = _nicCust.Mbe_pp_no;
                        _custReqHdr.Srb_b_town = _nicCust.Mbe_town_cd;

                        _custReqHdr.Srb_b_fax = _nicCust.Mbe_fax;
                    }

                    //_custReqHdr.Srb_infm_person = 
                    //_custReqHdr.Srb_infm_add1 = 
                    //_custReqHdr.Srb_infm_add2 = 
                    //_custReqHdr.Srb_infm_phno = 
                    _custReqHdr.Srb_stus = "P";
                    _custReqHdr.Srb_cre_by = BaseCls.GlbUserID;
                    //_custReqHdr.Srb_cre_dt = 
                    _custReqHdr.Srb_mod_by = BaseCls.GlbUserID;
                    //_custReqHdr.Srb_mod_dt = 

                    #endregion

                    #region fill request items
                    //get item details from agreement items
                    _reqLine = 1;
                    _reqDetList = new List<Service_Req_Det>();
                    _scvDefList = new List<Service_Req_Def>();
                    DataTable _dtAgrItms = CHNLSVC.CustService.getAgreementItems(row.Cells["sag_agr_no"].Value.ToString());
                    foreach (DataRow r in _dtAgrItms.Rows)
                    {
                        Service_Req_Det _custReqDet = new Service_Req_Det();

                        _custReqDet.Jrd_reqline = _reqLine;
                        _custReqDet.Jrd_loc = BaseCls.GlbUserDefLoca;
                        DataTable LocDes = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", BaseCls.GlbUserDefLoca);
                        if (LocDes.Rows.Count > 0)
                            _custReqDet.Jrd_pc = LocDes.Rows[0]["ml_def_pc"].ToString();

                        _custReqDet.Jrd_itm_cd = r["SHI_ITM_CD"].ToString();
                        _custReqDet.Jrd_itm_stus = "GOD";

                        MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, r["SHI_ITM_CD"].ToString());
                        _custReqDet.Jrd_itm_desc = _mstItm.Mi_shortdesc;
                        _custReqDet.Jrd_brand = _mstItm.Mi_brand;
                        _custReqDet.Jrd_model = _mstItm.Mi_model;
                        _custReqDet.Jrd_itm_cost = 0;
                        _custReqDet.Jrd_ser1 = r["SHI_SER1"].ToString();
                        if (string.IsNullOrEmpty(r["SHI_SER2"].ToString()))
                            _custReqDet.Jrd_ser2 = "N/A";
                        else
                            _custReqDet.Jrd_ser2 = r["SHI_SER2"].ToString();
                        _custReqDet.Jrd_warr = r["SHI_WARRNO"].ToString();
                        //_custReqDet.Jrd_regno = 
                        //_custReqDet.Jrd_milage = 

                        ////if (txtRCCType.Text == "STK")
                        ////{
                        _custReqDet.Jrd_warr_stus = 1;
                        _custReqDet.Jrd_onloan = 1;
                        ////}
                        ////else
                        ////{
                        ////    _custReqDet.Jrd_warr_stus = Convert.ToInt32(txtWarPeriod.Text) > 0 ? 1 : 0;
                        ////    _custReqDet.Jrd_onloan = 0;
                        ////}

                        // _custReqDet.Jrd_chg_warr_stdt = InvDate;
                        // _custReqDet.Jrd_chg_warr_rmk = WarRem;
                        //_custReqDet.Jrd_sentwcn = 
                        //_custReqDet.Jrd_isinsurance = 
                        //_custReqDet.Jrd_ser_term = 
                        //_custReqDet.Jrd_lastwarr_stdt = 
                        //_custReqDet.Jrd_issued = 
                        //_custReqDet.Jrd_mainitmcd = 
                        //_custReqDet.Jrd_mainitmser = 
                        //_custReqDet.Jrd_mainitmwarr = 
                        //_custReqDet.Jrd_itmmfc = 
                        //_custReqDet.Jrd_mainitmmfc = 
                        _custReqDet.Jrd_availabilty = 1;
                        //_custReqDet.Jrd_usejob = 
                        //_custReqDet.Jrd_msnno = 
                        // _custReqDet.Jrd_itmtp = ItemType;
                        //_custReqDet.Jrd_serlocchr = 
                        //_custReqDet.Jrd_custnotes = 
                        //_custReqDet.Jrd_mainreqno = 
                        //_custReqDet.Jrd_mainreqloc = 
                        //_custReqDet.Jrd_mainjobno = 
                        ////if (txtRCCType.Text == "STK")
                        ////    _custReqDet.Jrd_isstockupdate = 1;
                        ////else
                        _custReqDet.Jrd_isstockupdate = 0;
                        //_custReqDet.Jrd_needgatepass = 
                        _custReqDet.Jrd_iswrn = 0;
                        _custReqDet.Jrd_warrperiod = r["irsm_warr_period"] == DBNull.Value ? 0 : Convert.ToInt32(r["irsm_warr_period"]);
                        //  Convert.ToInt32(r["irsm_warr_period"].ToString() == null ? 0 : Convert.ToInt32(r["irsm_warr_period"].ToString()));

                        //_custReqDet.Jrd_warrstartdt = Convert.ToDateTime(r["irsm_doc_dt"]).Date;
                        _custReqDet.Jrd_warrstartdt = r["irsm_warr_period"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(r["irsm_doc_dt"]).Date;
                        _custReqDet.Jrd_warrrmk = r["irsm_warr_rem"].ToString();
                        //_custReqDet.Jrd_warrstartdt = InvDate;
                        _custReqDet.Jrd_warrreplace = 0;
                        //_custReqDet.Jrd_date_pur = 
                        // _custReqDet.Jrd_invc_no = txtInvoice.Text;
                        //_custReqDet.Jrd_waraamd_seq = 
                        //_custReqDet.Jrd_waraamd_by = 
                        //_custReqDet.Jrd_waraamd_dt = 
                        //_custReqDet.Jrd_invc_showroom = 
                        _custReqDet.Jrd_aodissueloc = BaseCls.GlbUserDefLoca;
                        //   _custReqDet.Jrd_aodissuedt = Convert.ToDateTime(txtDate.Value);
                        //_custReqDet.Jrd_aodissueno = 
                        //_custReqDet.Jrd_aodrecno = 
                        //_custReqDet.Jrd_techst_dt = 
                        //_custReqDet.Jrd_techfin_dt = 
                        //_custReqDet.Jrd_msn_no = 
                        _custReqDet.Jrd_isexternalitm = 0;
                        //_custReqDet.Jrd_conf_dt = 
                        //_custReqDet.Jrd_conf_cd = 
                        //_custReqDet.Jrd_conf_desc = 
                        //_custReqDet.Jrd_conf_rmk = 
                        //_custReqDet.Jrd_tranf_by = 
                        //_custReqDet.Jrd_tranf_dt = 
                        _custReqDet.Jrd_do_invoice = 0;
                        //_custReqDet.Jrd_insu_com = 
                        //_custReqDet.Jrd_agreeno = 
                        _custReqDet.Jrd_issrn = 0;
                        //_custReqDet.Jrd_isagreement = 
                        //_custReqDet.Jrd_cust_agreeno = 
                        //_custReqDet.Jrd_quo_no = 
                        _custReqDet.Jrd_stage = 1;
                        _custReqDet.Jrd_com = BaseCls.GlbUserComCode;
                        // _custReqDet.Jrd_ser_id = SerialID.ToString();
                        _custReqDet.Jrd_used = 0;
                        //_custReqDet.Jrd_jobno = 
                        //_custReqDet.Jrd_jobline = 

                        _reqDetList.Add(_custReqDet);
                        _reqLine = _reqLine + 1;
                    }
                    #endregion

                    #region Job Auto Number
                    MasterAutoNumber _jobAuto = new MasterAutoNumber();
                    _jobAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _jobAuto.Aut_cate_tp = "LOC";
                    _jobAuto.Aut_moduleid = "SVREQ";
                    _jobAuto.Aut_direction = 0;
                    _jobAuto.Aut_year = DateTime.Now.Date.Year;
                    _jobAuto.Aut_start_char = "SVREQ";
                    #endregion

                    string jobNo;
                    string receiptNo = string.Empty;
                    string _msg = "";
                    int eff = CHNLSVC.CustService.Save_Req(_custReqHdr, _reqDetList, _scvDefList, null, null, BaseCls.GlbDefSubChannel, "", "", 0, _jobAuto, out _msg, out jobNo, 1, dtpGenFrom.Value.Date, dtpGenTo.Value.Date);

                    #region Update Agreement Session
                    eff = CHNLSVC.CustService.UpdateAgreementSession(row.Cells["sag_agr_no"].Value.ToString(), Convert.ToInt16(row.Cells["saga_itm_line"].Value), Convert.ToInt16(row.Cells["sagaline"].Value), jobNo);
                    #endregion

                }
            }
            MessageBox.Show("Successfully processed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            grvAgrDet.AutoGenerateColumns = false;
            grvAgrDet.DataSource = null;

            grvAgr.AutoGenerateColumns = false;
            grvAgr.DataSource = null;

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable _dt = CHNLSVC.CustService.getAgrNo4ReqGen(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dtpGenFrom.Value.Date, dtpGenTo.Value.Date, txtSrchAgrNo.Text);

            grvAgr.AutoGenerateColumns = false;
            grvAgr.DataSource = _dt;

        }

        private void grvAgr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    //DataTable shipmtDetailnew = new DataTable();

            //    //foreach (DataGridViewRow dgvr in grvAgr.Rows)
            //    //{
            //    //    DataGridViewCheckBoxCell chk = dgvr.Cells["Column1"] as DataGridViewCheckBoxCell;
            //    //    if (Convert.ToBoolean(chk.Value) == true)
            //    //    {
            //    //        DataTable _dt = CHNLSVC.CustService.getAgrDetailsGenReq(grvAgr.Rows[e.RowIndex].Cells["sag_agr_no"].Value.ToString(), Convert.ToInt32(grvAgr.Rows[e.RowIndex].Cells["sagaline"].Value));

            //    //        shipmtDetailnew.Merge(_dt);
            //    //    }
            //    //}

            //    ////DataTable _dt = CHNLSVC.CustService.getAgrDetailsGenReq(grvAgr.Rows[e.RowIndex].Cells["sag_agr_no"].Value.ToString(), Convert.ToInt32(grvAgr.Rows[e.RowIndex].Cells["sagaline"].Value));

            //    //grvAgrDet.AutoGenerateColumns = false;
            //    //grvAgrDet.DataSource = shipmtDetailnew;
            //    load_dgvr();
            //}
        }
        private void load_dgvr()
        {
            DataTable shipmtDetailnew = new DataTable();

            foreach (DataGridViewRow dgvr in grvAgr.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Column1"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    DataTable _dt = CHNLSVC.CustService.getAgrDetailsGenReq(dgvr.Cells["sag_agr_no"].Value.ToString(), Convert.ToInt32(dgvr.Cells["sagaline"].Value));

                    shipmtDetailnew.Merge(_dt);
                }
            }

            //DataTable _dt = CHNLSVC.CustService.getAgrDetailsGenReq(grvAgr.Rows[e.RowIndex].Cells["sag_agr_no"].Value.ToString(), Convert.ToInt32(grvAgr.Rows[e.RowIndex].Cells["sagaline"].Value));

            grvAgrDet.AutoGenerateColumns = false;
            grvAgrDet.DataSource = shipmtDetailnew;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

                //if (cmbStatus.SelectedValue == null)
                //{
                //    MessageBox.Show("please select a item status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //    decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                //    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));
                decimal _vatPortion = 0;
                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                    _vatPortion = Convert.ToDecimal(txtTaxAmt.Text);

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    //if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    //{
                    //    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                    //    if (_tax != null && _tax.Count > 0)
                    //    {
                    //        decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                    //        txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                    //    }
                    //}


                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (!string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                            _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                        else
                            _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                    }
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text), true);
                }
                else
                    _totalAmount = FigureRoundUp(_totalAmount - _disAmt, true);

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }

        protected void BindAddcharges()
        {
            grvCha.AutoGenerateColumns = false;
            grvCha.DataSource = new List<scv_agr_cha>();
            grvCha.DataSource = oAgr_Cha;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLineTotAmt.Text))
            {
                MessageBox.Show("Invalid amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Convert.ToDecimal(txtLineTotAmt.Text) == 0)
            {
                MessageBox.Show("Item values cannot be zero", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            txtQty.Text = decimal.Truncate(Convert.ToDecimal(txtQty.Text.ToString())).ToString();

            if (oAgr_Cha.Count > 0)
            {
                if (oAgr_Cha.FindAll(x => x.Sac_itm_cd == txtItem.Text).Count == 0)
                {

                    scv_agr_cha _chrgs = new scv_agr_cha();

                    _chrgs.Sac_line = _chaLine;
                    _chrgs.Sac_itm_cd = txtItem.Text;
                    MasterItem _mst = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                    _chrgs.Sac_itm_desc = _mst.Mi_shortdesc;
                    _chrgs.Sac_qty = Convert.ToInt32(txtQty.Text);
                    _chrgs.Sac_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                    _chrgs.Sac_unit_amt = Convert.ToDecimal(txtUnitAmt.Text);

                    if (string.IsNullOrEmpty(txtDisRate.Text))
                        _chrgs.Sac_dis_rt = 0;
                    else
                        _chrgs.Sac_dis_rt = Convert.ToDecimal(txtDisRate.Text);

                    if (string.IsNullOrEmpty(txtDisAmt.Text))
                        _chrgs.Sac_dis_amt = 0;
                    else
                        _chrgs.Sac_dis_amt = Convert.ToDecimal(txtDisAmt.Text);

                    if (string.IsNullOrEmpty(txtTaxAmt.Text))
                        _chrgs.Sac_tax = 0;
                    else
                        _chrgs.Sac_tax = Convert.ToDecimal(txtTaxAmt.Text);

                    _chrgs.Sac_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);

                    oAgr_Cha.Add(_chrgs);
                    _chaLine = _chaLine + 1;
                    lblGrndTotalAmount.Text = FormatToCurrency((Convert.ToDecimal(lblGrndTotalAmount.Text) + Convert.ToDecimal(txtLineTotAmt.Text)).ToString());
                    txtSchAmt.Text = FormatToCurrency(lblGrndTotalAmount.Text);

                }
                else
                {
                    SystemInformationMessage("Charge already exists.", "Warning");
                    return;
                }

            }
            else
            {
                scv_agr_cha _chrgs = new scv_agr_cha();

                _chrgs.Sac_line = _chaLine;
                _chrgs.Sac_itm_cd = txtItem.Text;
                MasterItem _mst = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                _chrgs.Sac_itm_desc = _mst.Mi_shortdesc;
                _chrgs.Sac_qty = Convert.ToInt32(txtQty.Text);
                _chrgs.Sac_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                _chrgs.Sac_unit_amt = Convert.ToDecimal(txtUnitAmt.Text);
                if (string.IsNullOrEmpty(txtDisRate.Text))
                    _chrgs.Sac_dis_rt = 0;
                else
                    _chrgs.Sac_dis_rt = Convert.ToDecimal(txtDisRate.Text);

                if (string.IsNullOrEmpty(txtDisAmt.Text))
                    _chrgs.Sac_dis_amt = 0;
                else
                    _chrgs.Sac_dis_amt = Convert.ToDecimal(txtDisAmt.Text);

                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                    _chrgs.Sac_tax = 0;
                else
                    _chrgs.Sac_tax = Convert.ToDecimal(txtTaxAmt.Text);

                _chrgs.Sac_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);

                oAgr_Cha.Add(_chrgs);
                _chaLine = _chaLine + 1;
                lblGrndTotalAmount.Text = FormatToCurrency((Convert.ToDecimal(lblGrndTotalAmount.Text) + Convert.ToDecimal(txtLineTotAmt.Text)).ToString());
                txtSchAmt.Text = FormatToCurrency(lblGrndTotalAmount.Text);

            }
            txtItem.Text = string.Empty;
            txtUnitPrice.Text = "0.00";
            txtQty.Text = "1.000";

            BindAddcharges();
        }

        private void txtDisRate_TextChanged(object sender, EventArgs e)
        {
            CalculateItem();

        }

        private void txtTaxAmt_TextChanged(object sender, EventArgs e)
        {
            CalculateItem();
        }

        private void btnAddSch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSchAmt.Text))
            {
                MessageBox.Show("Please enter the amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAmt.Focus(); return;
            }
            if (Convert.ToDecimal(txtSchAmt.Text) == 0)
            {
                MessageBox.Show("Please enter the amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAmt.Focus(); return;
            }
            if (!IsNumeric(txtSchAmt.Text))
            {
                MessageBox.Show("Please enter Valid amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAmt.Text = ""; txtSchAmt.Focus(); return;
            }
            if (dtSch.Value.Date < dtFrom.Value.Date)
            {
                MessageBox.Show("Invalid schedule date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (Convert.ToDecimal(txtSchAmt.Text) == 0)
            {
                MessageBox.Show("Amount cannot be zero", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (Convert.ToDecimal(txtSchAmt.Text) > Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(txtTotSch.Text))
            {
                MessageBox.Show("Payment exceeded ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAmt.Focus(); return;
            }
            scv_agr_payshed _payshed = new scv_agr_payshed();
            _payshed.Sap_amt = Convert.ToDecimal(txtSchAmt.Text);
            _payshed.Sap_bal_amt = Convert.ToDecimal(txtSchAmt.Text);
            _payshed.Sap_due_dt = Convert.ToDateTime(dtSch.Value.Date);
            _payshed.Sap_term = _term;
            oAgr_Payshed.Add(_payshed);
            _term = _term + 1;

            txtTotSch.Text = FormatToCurrency((Convert.ToDecimal(txtTotSch.Text) + Convert.ToDecimal(txtSchAmt.Text)).ToString());

            BindAddPayShed();
        }

        protected void BindAddPayShed()
        {
            grvSch.AutoGenerateColumns = false;
            ///List<scv_agr_payshed> _LIST = new List<scv_agr_payshed>();
            //grvSch.DataSource = new List<scv_agr_payshed>();
            grvSch.DataSource = null;
            grvSch.DataSource = oAgr_Payshed;
            grvSch.Refresh(); 

        }

        private void clear_charge()
        {
            txtItem.Text = "";
            txtQty.Text = "1.000";
            txtUnitAmt.Text = "0.00";
            txtUnitPrice.Text = "0.00";
            txtLineTotAmt.Text = "0.00";
            txtDisAmt.Text = "0.00";
            txtDisRate.Text = "";
            txtTaxAmt.Text = "0.00";

        }

        private void grvCovItm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tmp_CoverItems.RemoveAt(e.RowIndex);

                    grvCovItm.AutoGenerateColumns = false;
                    grvCovItm.DataSource = new List<SCV_AGR_COVER_ITM>();
                    grvCovItm.DataSource = tmp_CoverItems;

                }
            }
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            dtRenew.Value = dtTo.Value.Date.AddDays(1);
        }

        private void txtCItem_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCItem.Text))
            {
                MasterItem _itemdetail = new MasterItem();
                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtCItem.Text);
                if (_itemdetail == null)
                {
                    MessageBox.Show("Invalid Item code", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCItem.Focus(); return;
                }
            }
        }

        private void txtDisAmt_Leave(object sender, EventArgs e)
        {
            decimal _disAmt = Convert.ToDecimal(txtDisAmt.Text);
            decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
            decimal _qty = Convert.ToDecimal(txtQty.Text);
            decimal _totAmt = _uRate * _qty;
            decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

            CalculateItem();

            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
            {
                _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
            }
            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
            {
                txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
            }

            CalculateItem();
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _eff = CHNLSVC.CustService.updateAgreement(txtAgrNo.Text, "R", BaseCls.GlbUserID);
                MessageBox.Show("Successfully Rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnCustRej_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _eff = CHNLSVC.CustService.updateAgreement(txtAgrNo.Text, "J", BaseCls.GlbUserID);
                MessageBox.Show("Successfully Customer Rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtDisAmt_TextChanged(object sender, EventArgs e)
        {
            //txtDisRate.Text = Convert.ToDecimal(txtDisAmt.Text) * 100 / Convert.ToDecimal(txtUnitAmt).ToString();

        }

        private void txtDisAmt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDisRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisRate.Text))
            {
                if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    txtDisAmt.Enabled = false;
                else
                    txtDisAmt.Enabled = true;
            }
        }

        private void btn_srch_agrmnt_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Agreement);
            DataTable _result = CHNLSVC.CommonSearch.SearchAgreementData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchAgrNo;
            _CommonSearch.ShowDialog();
            txtSrchAgrNo.Focus();
        }

        private void pnlGen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvCha_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lblGrndTotalAmount.Text = FormatToCurrency((Convert.ToDecimal(lblGrndTotalAmount.Text) - oAgr_Cha[e.RowIndex].Sac_tot_amt).ToString());
                    txtSchAmt.Text = FormatToCurrency(lblGrndTotalAmount.Text);

                    oAgr_Cha.RemoveAt(e.RowIndex);

                    grvCha.AutoGenerateColumns = false;
                    grvCha.DataSource = new List<scv_agr_cha>();
                    grvCha.DataSource = oAgr_Cha;

                }
            }
        }

        private void grvSch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) //delete button has been clicked
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow dataGridViewRow = grvSch.Rows[e.RowIndex];

                    if (dataGridViewRow.Cells.Count > 1)
                    {
                        //DeleteClient(dataGridViewRow.Cells[e.ColumnIndex + 1].FormattedValue.ToString());
                    }
                }
                else
                {
                    //LogToFile(e.RowIndex.ToString());
                }
            }
        }

        private void btngrvSch_Click(object sender, EventArgs e)
        {
            oAgr_Payshed = new List<scv_agr_payshed>();
            txtSchAmt.Text="0.00";
            //lblGrndTotalAmount.Text="0.00";
            txtTotSch.Text = "0.00";
            BindAddPayShed();
        }

        private void btngrvChacclear_Click(object sender, EventArgs e)
        {
            oAgr_Cha = new List<scv_agr_cha>();
            BindAddcharges();
            lblGrndTotalAmount.Text = "0.00";
            btngrvSch_Click(null, null);
        }

        private void btncustupdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtAgrNo.Text))
	{
		 
                MessageBox.Show("Please enter Agrement amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAmt.Focus(); return;
	}
            if (MessageBox.Show("Do you want to Update Customer Details ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;


            SCV_AGR_HDR oHeader = new SCV_AGR_HDR();
            oHeader.Sag_seq_no = Convert.ToInt32((lblSeq.Text == "") ? "-99" : lblSeq.Text);
            oHeader.Sag_agr_no = txtAgrNo.Text;
            oHeader.Sag_com = BaseCls.GlbUserComCode;
            oHeader.Sag_tp = txtType.Text;
            oHeader.Sag_clm_tp = txtClaimType.Text;
            oHeader.Sag_chnl = BaseCls.GlbDefChannel;
            oHeader.Sag_schnl = BaseCls.GlbDefSubChannel;
            oHeader.Sag_pc = BaseCls.GlbUserDefProf;
            oHeader.Sag_multi_pc = chk.Checked ? true : false;
            oHeader.Sag_commdt = Convert.ToDateTime(dtFrom.Value.Date);
            oHeader.Sag_exdt = Convert.ToDateTime(dtTo.Value.Date);
            oHeader.Sag_rewldt = Convert.ToDateTime(dtRenew.Value.Date);
            oHeader.Sag_custcd = txtCustCode.Text;
            oHeader.Sag_cust_town = txtTown.Tag.ToString();
            oHeader.Sag_town_rmk = txtEquipLoc.Text;
            oHeader.Sag_cont_person = txtContPersn.Text;
            oHeader.Sag_cont_add = txtContLoc.Text;
            oHeader.Sag_cont_no = txtContNo.Text;
            oHeader.Sag_manualref = "";
            oHeader.Sag_otherref = "";
            oHeader.Sag_rmk = "";
            oHeader.Sag_gl_debtor_cd = "";
            oHeader.Sag_tot_amt = Convert.ToDecimal(lblGrndTotalAmount.Text);
            oHeader.Sag_set_amt = 0;
            oHeader.Sag_period_basis = cmbBasis.SelectedValue.ToString();
            oHeader.Sag_ser_attempt = Convert.ToInt32(txtAttempt.Text);
            oHeader.Sag_top = txtTOP.Text;
            oHeader.Sag_tac = txtTAC.Text;
            oHeader.Sag_work_inc = txtWrkInc.Text;
            oHeader.Sag_svc_freq = txtFreq.Text;
            oHeader.Sag_stus = "P";
          
            oHeader.Sag_cre_by = BaseCls.GlbUserID;
            oHeader.Sag_mod_by = BaseCls.GlbUserID;
            string _msg = "";
            int _eff = CHNLSVC.CustService.update_scv_agr_hdr(oHeader, out _msg);
            if (_eff != -1)
            {
                MessageBox.Show("Successfully Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(_msg, "Error Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
            }
           
        }

        private void grvAgr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{


            //    //DataTable shipmtDetailnew = new DataTable();

            //    //foreach (DataGridViewRow dgvr in grvAgr.Rows)
            //    //{
            //    //    DataGridViewCheckBoxCell chk = dgvr.Cells["Column1"] as DataGridViewCheckBoxCell;
            //    //    if (Convert.ToBoolean(chk.Value) == true)
            //    //    {
            //    DataTable _dt = CHNLSVC.CustService.getAgrDetailsGenReq(grvAgr.Rows[e.RowIndex].Cells["sag_agr_no"].Value.ToString(), Convert.ToInt32(grvAgr.Rows[e.RowIndex].Cells["sagaline"].Value));

            //            dtmerge.Merge(_dt);
            //    //    }
            //    //}

            //    //DataTable _dt = CHNLSVC.CustService.getAgrDetailsGenReq(grvAgr.Rows[e.RowIndex].Cells["sag_agr_no"].Value.ToString(), Convert.ToInt32(grvAgr.Rows[e.RowIndex].Cells["sagaline"].Value));

            //    grvAgrDet.AutoGenerateColumns = false;
            //    grvAgrDet.DataSource = dtmerge;
            //}
        }

        private void grvAgr_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //foreach (DataGridViewRow dgvr in grvAgr.Rows)
                //{
                DataGridViewCheckBoxCell chk = grvAgr.Rows[e.RowIndex].Cells["Column1"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    grvAgr.Rows[e.RowIndex].Cells["Column1"].Value = Convert.ToBoolean(0);
                }
                else
                { grvAgr.Rows[e.RowIndex].Cells["Column1"].Value = Convert.ToBoolean(1); }

                //}
                load_dgvr();
            }
        }

        private void ChkExternal_CheckedChanged(object sender, EventArgs e)
        {

         
        }

        private void ChkExternal_Click(object sender, EventArgs e)
        {
            if (ChkExternal.Checked == true)
            {
                btn_srch_serial.Enabled = false;
                btn_srch_war.Enabled = false;
            }
            else
            {

                btn_srch_serial.Enabled = true;
                btn_srch_war.Enabled = true;
            }
         
        }


    }
}