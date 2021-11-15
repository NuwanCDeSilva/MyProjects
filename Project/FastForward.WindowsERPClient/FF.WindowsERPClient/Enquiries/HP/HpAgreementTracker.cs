using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Services;
using FF.WindowsERPClient.CommonSearch;

namespace FF.WindowsERPClient.Enquiries.HP
{
    public partial class HpAgreementTracker : Base
    {
        //sp_get_RequiredDocuments = update
        //sp_save_acc_agreement_doc =new
        //sp_GetAccAgreeDocReceiveDet =NEW
        //get_HPT_AccountAgreement= new
        //get_HPT_AccountAgreementDocs =new
        //sp_save_AccAgreementContact = new
        //sp_save_AccAgreement_Product =new
        //sp_save_AccAgreement_Scheme =new
        //sp_del_AccAgreementProduct =new 
        //get_Accounts_Agreement_Contact =new
        //get_Acc_Agreement_StartDt =new 
        //sp_save_accAgreement_startdate= new
        //get_Acc_Agreement_products =new
        //-----------------------------------------------------
        //try
        //   {

        //   }
        //   catch (Exception ex)
        //   {
        //       MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
        //   }
        //   finally
        //   {
        //       CHNLSVC.CloseAllChannels();
        //   }

        string selectPC = BaseCls.GlbUserDefProf;

        List<InvoiceItem> _FinalItemList = new List<InvoiceItem>();

        public HpAgreementTracker()
        {
            InitializeComponent();
            tabControl1.TabPages.RemoveAt(1);
            tabControl1.TabPages.RemoveAt(1);

            try
            {
                txtPC.Text = selectPC;//BaseCls.GlbUserDefProf;
                txtPC_2.Text = selectPC;//BaseCls.GlbUserDefProf;
                DataTable dt = CHNLSVC.General.Get_RequiredDocuments("HPAGREE");
                grvDocs.AutoGenerateColumns = false;
                grvDocs.DataSource = dt;

                //------Tab2----------------------
                _FinalItemList = new List<InvoiceItem>();

                dtReturnDt.Value = Convert.ToDateTime("12/Dec/2999");

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            HpAgreementTracker formnew = new HpAgreementTracker();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AccountDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Scheme:
                    {
                        paramsText.Append(txtSchemeCD_MM_new.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void BtnSearcgAccNo_Click(object sender, EventArgs e)
        {
            selectPC = txtPC.Text.Trim();

            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AccountDate);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountDateSearchData(_CommonSearch.SearchParams, null, null, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date);
            _CommonSearch.dtpFrom.Value = DateTime.Now.Date.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAcc_no;
            //_commonSearch.IsSearchEnter = true; 
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtAcc_no.Select();


            //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();

            //_CommonSearch.ReturnIndex = 1;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AccountDate);

            //DataTable _result = CHNLSVC.CommonSearch.GetHpAccountDateSearchData(_CommonSearch.SearchParams, null, null, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date);
            //_CommonSearch.dtpFrom.Value = DateTime.Now.Date.AddMonths(-1);
            //_CommonSearch.dtpTo.Value = DateTime.Now.Date;
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtAcc_no;
            //_CommonSearch.ShowDialog();
            //txtAcc_no.Select();

            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //// _CommonSearch.ReturnIndex = 0;
            //_CommonSearch.ReturnIndex = 1;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            //DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtAcc_no;
            //_CommonSearch.ShowDialog();
            //txtAcc_no.Focus();
        }

        private void Account_Load()
        {
            BindAccountItem(txtAcc_no.Text.Trim());
            BindGurantors(txtAcc_no.Text.Trim());
            //------------------------------
            DateTime today = CHNLSVC.Security.GetServerDateTime().Date;
            Decimal bal = CHNLSVC.Sales.Get_AccountBalance(today, txtAcc_no.Text.Trim());
            txtAccBalance.Text = string.Format("{0:n2}", bal);
            //-------------------
            HpAccount Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAcc_no.Text.Trim());
            txtAccStatus.Text = Acc.Hpa_stus == "A" ? "Active" : (Acc.Hpa_stus == "C" ? "Close" : (Acc.Hpa_stus == "R" ? "Revert" : "NOT FOUND"));
            txtHireVal.Text = string.Format("{0:n2}", Acc.Hpa_hp_val);//Acc.Hpa_hp_val;
            decimal _value = CHNLSVC.General.GetRentalValue(txtAcc_no.Text.Trim());
            txtRental.Text = string.Format("{0:n2}", _value);
            txtSchemeCD_MM.Text = Acc.Hpa_sch_cd;
            txtSchemeCD_MM_new.Text = Acc.Hpa_sch_cd;

            HpSchemeDetails _SchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(txtSchemeCD_MM.Text.ToString());
            txtSchmeDesc_MM.Text = _SchemeDetails.Hsd_desc;
            txtSchmePeriod_MM.Text = _SchemeDetails.Hsd_term.ToString();
            txtSchmeDesc_MM_new.Text = _SchemeDetails.Hsd_desc;
            txtSchmePeriod_MM_new.Text = _SchemeDetails.Hsd_term.ToString();


            //--------------load previously saved data------------------------
            DataTable DT_AgrHdr = CHNLSVC.General.Get_AccountAgreementHdr(txtAcc_no.Text.Trim());
            if (DT_AgrHdr != null)
            {
                if (DT_AgrHdr.Rows.Count > 0)
                {
                    dtReceived.Value = Convert.ToDateTime(DT_AgrHdr.Rows[0]["AGR_DATE_RECEIVED"].ToString());
                    txtSlipNo.Text = DT_AgrHdr.Rows[0]["AGR_SLIPNO"].ToString();
                    dtReturnDt.Value = Convert.ToDateTime(DT_AgrHdr.Rows[0]["AGR_DATERETURNED"].ToString());
                    txtHandOvName.Text = DT_AgrHdr.Rows[0]["AGR_HANDOVERNAME"].ToString();
                    txtHandOvID.Text = DT_AgrHdr.Rows[0]["AGR_HANDOVERID"].ToString();
                    chkClosure.Checked = DT_AgrHdr.Rows[0]["AGR_CLOSURE"].ToString() == "1" ? true : false;
                    try
                    {
                        ddlHO_to.SelectedItem = DT_AgrHdr.Rows[0]["AGR_HANDOVER"].ToString();
                        ddlClosureTp.SelectedItem = DT_AgrHdr.Rows[0]["AGR_CLOSURETYPE"].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    dtReceived.Value = DateTime.Now.Date;
                    txtSlipNo.Text = "";
                    dtReturnDt.Value = Convert.ToDateTime("12/Dec/2999");
                    txtHandOvName.Text = "";
                    txtHandOvID.Text = "";
                    chkClosure.Checked = false;
                    try
                    {
                        ddlHO_to.SelectedIndex = -1;
                        ddlClosureTp.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            List<AccountAgreementDoc> receivedDocs = CHNLSVC.General.Get_HPT_AccountAgreementDocs(txtAcc_no.Text.Trim(), "1");
            if (receivedDocs != null)
            {
                foreach (AccountAgreementDoc doc in receivedDocs)
                {
                    foreach (DataGridViewRow row in grvDocs.Rows)
                    {
                        string DOC = Convert.ToString(doc.Agrd_prd_cd);
                        string GRV_DOC = row.Cells["HSP_PRD_CD"].Value.ToString();
                        if (DOC == GRV_DOC)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.Value = true;
                        }
                    }
                }
                grvDocs.EndEdit();

            }


            //-----------------------------------------------------------------

        }
        private void BindGurantors(string AccNo)
        {
            DataTable dt = CHNLSVC.Sales.Get_gurantors(AccNo);
            grvGuarantors.DataSource = null;
            grvGuarantors.AutoGenerateColumns = false;
            grvGuarantors.DataSource = dt;
        }
        private void BindAccountItem(string _account)
        {

            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, txtPC.Text.Trim(), _account);
            InvoiceHeader _hdrs = null;

            if (_invoice != null)
                if (_invoice.Count > 0)
                    _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, txtPC.Text.Trim(), _account);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvATradeItem.AutoGenerateColumns = false;
                gvATradeItem.DataSource = _table;
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
                    gvATradeItem.AutoGenerateColumns = false;
                    gvATradeItem.DataSource = _itemList;
                }

            if (_hdrs != null)
            {
                BindCustomerDetails(_hdrs);
            }
            else
            {
                BindCustomerDetails(new InvoiceHeader());
            }



        }

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            txtCustID.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtCustAddr.Text = string.Empty;
            if (_hdr != null)
            {
                txtCustID.Text = _hdr.Sah_cus_cd;
                txtCustName.Text = _hdr.Sah_cus_name;
                txtCustAddr.Text = _hdr.Sah_d_cust_add1 + " " + _hdr.Sah_d_cust_add2;
            }
        }
        private void txtAcc_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                #region
                if (string.IsNullOrEmpty(txtAcc_no.Text)) return;
                Clear_Agreenement(false, false);
                HpAccount _ac = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAcc_no.Text);
                if (_ac == null || string.IsNullOrEmpty(_ac.Hpa_com)) { MessageBox.Show("Please select the valid account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); Clear_Agreenement(false, true); return; }
                txtUnitPrice.Text = string.Format("{0:n2}", _ac.Hpa_cash_val);
                Account_Load();

                #endregion
            }
        }

        private void txtAcc_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.BtnSearcgAccNo_Click(null, null);
            }
        }

        private void imgBtnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
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

        private void txtPC_TextChanged(object sender, EventArgs e)
        {
            txtAcc_no.Text = "";
            txtAccStatus.Text = "";
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void chkAllDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDoc.Checked == true)
            {
                this.btnAll_docs_Click(null, null);
            }
            else
            {
                this.btnNon_docs_Click(null, null);
            }
        }

        private void btnAll_docs_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvDocs.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvDocs.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_docs_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvDocs.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvDocs.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void txtAcc_no_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Account_Load();
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

        private void rdoMM_QuickVeiw_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoMM_QuickVeiw.Checked == true)
            //{
            //    panel_custDetQuickView.Visible = true;
            //}
            //else
            //{
            //    panel_custDetQuickView.Visible = false;
            //}
        }

        private void txtPC2_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC_2;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                txtPC_2.Focus();
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

        private void btnAccSearch2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPC_2.Text.Trim() == "")
                {
                    MessageBox.Show("Please select profit center first!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                selectPC = txtPC_2.Text.Trim();

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                // _CommonSearch.ReturnIndex = 0;
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAcc_no2;
                _CommonSearch.ShowDialog();
                txtAcc_no2.Focus();
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

        private void txtAcc_no2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveAgreement_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                //-------------------Validation-------------------------------------
                if (txtAcc_no.Text.Trim() == "")
                {
                    MessageBox.Show("Enter account first!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (chkClosure.Checked && ddlClosureTp.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select closure type!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //------------------------------------------------------------------

                Boolean _isInComplete = false;
                List<AccountAgreementDoc> doc_list = new List<AccountAgreementDoc>();

                foreach (DataGridViewRow dgvr in grvDocs.Rows)
                {
                    Int32 prdCd = Convert.ToInt32(dgvr.Cells["HSP_SCH_CD"].Value.ToString());
                    string docName = dgvr.Cells["hpd_desc"].Value.ToString();
                    AccountAgreementDoc doc = new AccountAgreementDoc();
                    doc.Agrd_accno = txtAcc_no.Text.Trim();
                    doc.Agrd_doc_name = docName;
                    doc.Agrd_prd_cd = prdCd;

                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        doc.Agrd_is_recieve = true;
                    }
                    else
                    {
                        doc.Agrd_is_recieve = false;
                        _isInComplete = true;
                    }

                    doc_list.Add(doc);
                }
                //-------------------------------------    
                AccountAgreement Agree = new AccountAgreement();
                Agree.Agr_account = txtAcc_no.Text.Trim().ToUpper();
                Agree.Agr_closure = chkClosure.Checked;

                Agree.Agr_closuredate = DateTime.Now.Date;
                // Agree.Agr_closuretype = ddlClosureTp.SelectedText.ToString();
                if (ddlClosureTp.SelectedIndex == -1)
                {
                    Agree.Agr_closuretype = "";
                }
                else
                {
                    Agree.Agr_closuretype = ddlClosureTp.Items[ddlClosureTp.SelectedIndex].ToString();//ddlHO_to.SelectedText.ToString();
                }
                Agree.Agr_date_received = dtReceived.Value.Date;
                Agree.Agr_datereturned = dtReturnDt.Value.Date;
                if (ddlHO_to.SelectedIndex == -1)
                {
                    Agree.Agr_handover = "";
                }
                else
                {
                    Agree.Agr_handover = ddlHO_to.Items[ddlHO_to.SelectedIndex].ToString();//ddlHO_to.SelectedText.ToString();
                }

                Agree.Agr_handoverid = txtHandOvID.Text.Trim();
                Agree.Agr_handovername = txtHandOvName.Text.Trim();
                Agree.Agr_location = txtPC.Text.Trim();
                Agree.Agr_modifieddate = DateTime.Now.Date;
                Agree.Agr_sessionid = BaseCls.GlbUserSessionID;
                Agree.Agr_slipno = txtSlipNo.Text.Trim();
                //Agree.Agr_timereturned =;
                //Agree.Agr_timestamp;
                //Agree.Agr_writtenoff;
                Agree.Agr_comcd = BaseCls.GlbUserComCode;

                if (_isInComplete == true)
                {
                    Agree.Agr_stus = "P";
                    Agree.Agr_closedt = Convert.ToDateTime("12/12/9998");
                }
                else
                {
                    Agree.Agr_stus = "F";
                    Agree.Agr_closedt = DateTime.Now.Date;
                }


                Decimal notRecCount = CHNLSVC.General.GetNotReciveCount(txtAcc_no.Text.Trim().ToUpper());
                if (notRecCount == 0) //all received
                {
                    //phase compleated
                    MessageBox.Show("Phase compleated already. Cannot update again!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (notRecCount > 0) // has pending docs
                {
                    if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    Int32 eff = CHNLSVC.General.Save_AccountAgreementDetails(txtAcc_no.Text.Trim().ToUpper(), Agree, doc_list);
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.btnClear_Click(null, null);
   
                    }
                    else
                    {
                        MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                #endregion
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

        private void chkClosure_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClosure.Checked == false)
            {
                ddlClosureTp.SelectedIndex = -1;
            }
        }

        private void ddlClosureTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClosureTp.SelectedIndex != -1)
            {
                chkClosure.Checked = true;
            }
        }

        private void btnCustID_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtHandOvID;
            _CommonSearch.ShowDialog();
            txtHandOvID.Focus();

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }


        //----------------------------------------------------------------------------------------------
        private List<InvoiceItem> BindAccountItemForMisMatch(string _account)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, txtPC.Text.Trim(), _account);
            InvoiceHeader _hdrs = null;

            if (_invoice != null)
                if (_invoice.Count > 0)
                    _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, txtPC.Text.Trim(), _account);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                grvCurrItems.AutoGenerateColumns = false;
                grvCurrItems.DataSource = _table;
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
                    grvCurrItems.AutoGenerateColumns = false;
                    grvCurrItems.DataSource = _itemList;
                }
            //----------------------------------------------------------------------------------------------------------
            //_FinalItemList = _itemList;
            //grvAgrItems.DataSource = null;
            //grvAgrItems.AutoGenerateColumns = false;
            //grvAgrItems.DataSource = _FinalItemList;
            return _itemList;
        }

        private void grvAgrItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow row = grvAgrItems.Rows[rowIndex];
                string ItemCd = row.Cells["a_Sad_itm_cd"].Value.ToString();
                if (MessageBox.Show("Do you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                _FinalItemList.RemoveAll(x => x.Sad_itm_cd == ItemCd);

                BindingSource _source = new BindingSource();
                _source.DataSource = _FinalItemList;
                grvAgrItems.DataSource = _source;

                BindAccountItemForMisMatch(txtAcc_no2.Text.Trim());

                //Delete_AccountAgreementProduct
                Int32 eff = CHNLSVC.General.Delete_AccountAgreementProduct(txtAcc_no2.Text.Trim(), ItemCd);
            }
        }

        private void grvAgrItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            BindingSource _source = new BindingSource();
            _source.DataSource = _FinalItemList;
            grvAgrItems.DataSource = _source;
        }


        private void grvCurrItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            BindingSource _source = new BindingSource();
            _source.DataSource = _FinalItemList;
            grvAgrItems.DataSource = _source;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                txtQtyAdd.Focus();
                Convert.ToDecimal(txtQtyAdd.Text.Trim());
                txtPriceAdd.Focus();
                Convert.ToDecimal(txtPriceAdd.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter valid Price and Qty.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //_FinalItemList
            InvoiceItem invItm = new InvoiceItem();
            invItm = new InvoiceItem();
            invItm.Mi_act = true;
            //invItm.Mi_brand;
            //invItm.Mi_cd;
            invItm.Mi_cre_by = BaseCls.GlbUserID;
            invItm.Mi_cre_dt = DateTime.Now.Date;
            //invItm.Mi_is_ser1;=;
            invItm.Mi_session_id = BaseCls.GlbUserSessionID;
            // invItm.Mi_std_price= ;
            invItm.Sad_itm_cd = txtItmCdAdd.Text.Trim();
            invItm.Sad_qty = Convert.ToDecimal(txtQtyAdd.Text.Trim());
            //invItm.ToString
            invItm.Sad_unit_rt = Convert.ToDecimal(txtPriceAdd.Text.Trim());

            DataTable dt_itm = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, txtItmCdAdd.Text.Trim());
            if (dt_itm != null)
            {
                if (dt_itm.Rows.Count > 0)
                {
                    string Item_desc = dt_itm.Rows[0]["mi_shortdesc"].ToString();
                    invItm.Mi_longdesc = Item_desc;
                }
                else
                {
                    MessageBox.Show("Wrong item code!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Wrong item code!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_FinalItemList == null)
            {
                _FinalItemList = new List<InvoiceItem>();
            }
            _FinalItemList.Add(invItm);


            AccountAgreementProduct agrProd = new AccountAgreementProduct();
            agrProd.Itm_account = txtAcc_no2.Text.Trim();
            agrProd.Itm_code = txtItmCdAdd.Text.Trim();
            agrProd.Itm_desc = invItm.Mi_longdesc;
            agrProd.Itm_qty = Convert.ToDecimal(txtQtyAdd.Text.Trim());
            agrProd.Itm_unitprice = Convert.ToDecimal(txtPriceAdd.Text.Trim());

            Int32 eff = CHNLSVC.General.Save_AccountAgreementProduct(agrProd);

            BindingSource _source = new BindingSource();
            _source.DataSource = _FinalItemList;
            grvAgrItems.DataSource = _source;

            txtItmCdAdd.Clear();
            txtPriceAdd.Clear();
            txtQtyAdd.Clear();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCurrToList_Click(object sender, EventArgs e)
        {
            _FinalItemList = BindAccountItemForMisMatch(txtAcc_no2.Text.Trim());

            grvAgrItems.DataSource = null;
            grvAgrItems.AutoGenerateColumns = false;
            grvAgrItems.DataSource = _FinalItemList;


            //TODO: CHECK IF ALREADY AVALABLE IN TABLE
            foreach (InvoiceItem ITM in _FinalItemList)
            {
                CHNLSVC.General.Delete_AccountAgreementProduct(txtAcc_no2.Text.Trim(), ITM.Sad_itm_cd);

                AccountAgreementProduct prod = new AccountAgreementProduct();
                AccountAgreementProduct agrProd = new AccountAgreementProduct();
                agrProd.Itm_account = txtAcc_no2.Text.Trim();
                agrProd.Itm_code = ITM.Sad_itm_cd;
                // agrProd.Itm_desc;
                agrProd.Itm_qty = ITM.Sad_qty; //Convert.ToDecimal(txtQtyAdd.Text.Trim());
                agrProd.Itm_unitprice = ITM.Sad_unit_rt;//Convert.ToDecimal(txtPriceAdd.Text.Trim());
                Int32 eff = CHNLSVC.General.Save_AccountAgreementProduct(agrProd);
            }
        }

        private void btnSaveMM_Click(object sender, EventArgs e)
        {
            //validation------------------------
            //Convert.ToInt32(txtSchmePeriod_MM_new.Text.Trim());
            try
            {
                if (txtSchmePeriod_MM_new.Text.Trim() != "")
                {
                    Convert.ToInt32(txtSchmePeriod_MM_new.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter valid scheme period", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AccountAgreementContact AgreeContact = new AccountAgreementContact();
            AgreeContact.Cnt_account = txtAcc_no2.Text.Trim();
            AgreeContact.Cnt_add1 = txtAddr_MM_new.Text;
            AgreeContact.Cnt_city = txtCity_MM_new.Text.Trim();
            AgreeContact.Cnt_code = txtCustID.Text.Trim();//txtCustID_MM_new.Text.Trim() ;
            AgreeContact.Cnt_district = txtDistrict_MM_new.Text.Trim();
            AgreeContact.Cnt_dl = txtDL_MM_new.Text.Trim();
            AgreeContact.Cnt_dob = dtpDOB_MM_new.Value.Date;
            AgreeContact.Cnt_fname = txtFirstName_MM_new.Text.Trim();
            AgreeContact.Cnt_location = txtPC_2.Text.Trim();
            AgreeContact.Cnt_mobile = txtMobile_MM_new.Text.Trim();
            AgreeContact.Cnt_nic = txtNIC_MM_new.Text.Trim();
            AgreeContact.Cnt_passport = txtPP_MM_new.Text.Trim();
            AgreeContact.Cnt_postalcode = txt_Postal_MM_new.Text.Trim();
            AgreeContact.Cnt_province = txtProvince_MM_new.Text.Trim();

            //AgreeContact.Cnt_sex ;

            AgreeContact.Cnt_telhome = txtTelHome_MM_new.Text.Trim();
            AgreeContact.Cnt_telresident = txtTelResident_MM_new.Text.Trim();
            AgreeContact.Cnt_title = txtTitle_MM_new.Text.Trim();
            AgreeContact.Cnt_type = txtPP_MM_new.Text.Trim();

            AgreeContact.Cnt_guarantor1_addr1 = txtG1_Adr1_MM_new.Text;
            AgreeContact.Cnt_guarantor1_addr2 = txtG1_Adr2_MM_new.Text;
            AgreeContact.Cnt_guarantor1_code = txtG1_id_MM_new.Text;
            AgreeContact.Cnt_guarantor1_name = txtG1_name_MM_new.Text;
            AgreeContact.Cnt_guarantor1_nic = txtG1_NIC_MM_new.Text;
            AgreeContact.Cnt_guarantor1_mobile = txtG1_Mob_MM_new.Text.Trim();
            AgreeContact.Cnt_guarantor1_tel = txtG1_Tel_MM_new.Text;
            AgreeContact.Cnt_guarantor1_PP = txtG1_PP_MM_new.Text;
            AgreeContact.Cnt_guarantor1_DL = txtG1_DL_MM_new.Text;

            AgreeContact.Cnt_guarantor2_addr1 = txtG2_Adr1_MM_new.Text;
            AgreeContact.Cnt_guarantor2_addr2 = txtG2_Adr2_MM_new.Text;
            AgreeContact.Cnt_guarantor2_code = txtG2_id_MM_new.Text;
            AgreeContact.Cnt_guarantor2_name = txtG2_name_MM_new.Text;
            AgreeContact.Cnt_guarantor2_nic = txtG2_NIC_MM_new.Text;
            AgreeContact.Cnt_guarantor2_mobile = txtG2_Mob_MM_new.Text.Trim();
            AgreeContact.Cnt_guarantor2_tel = txtG2_Tel_MM_new.Text;
            AgreeContact.Cnt_guarantor2_PP = txtG2_PP_MM_new.Text;
            AgreeContact.Cnt_guarantor2_DL = txtG2_DL_MM_new.Text;

            AgreeContact.Cnt_guarantor3_addr1 = txtG3_Adr1_MM_new.Text;
            AgreeContact.Cnt_guarantor3_addr2 = txtG3_Adr2_MM_new.Text;
            AgreeContact.Cnt_guarantor3_code = txtG3_id_MM_new.Text;
            AgreeContact.Cnt_guarantor3_name = txtG3_name_MM_new.Text;
            AgreeContact.Cnt_guarantor3_nic = txtG3_NIC_MM_new.Text;
            AgreeContact.Cnt_guarantor3_mobile = txtG3_Mob_MM_new.Text.Trim();
            AgreeContact.Cnt_guarantor3_tel = txtG3_Tel_MM_new.Text;
            AgreeContact.Cnt_guarantor3_PP = txtG3_PP_MM_new.Text;
            AgreeContact.Cnt_guarantor3_DL = txtG3_DL_MM_new.Text;
            //AgreeContact.

            AccountAgreementScheme AgreeScheme = new AccountAgreementScheme();
            AgreeScheme.Sch_account = txtAcc_no2.Text.Trim();
            AgreeScheme.Sch_code = txtSchemeCD_MM_new.Text.Trim();
            AgreeScheme.Sch_desc = txtSchmeDesc_MM_new.Text;
            if (txtSchmePeriod_MM_new.Text.Trim() != "")
            {
                AgreeScheme.Sch_term = Convert.ToInt32(txtSchmePeriod_MM_new.Text.Trim());
            }


            Int32 eff = CHNLSVC.General.Save_AccountAgreement_MisMatch_Details(txtAcc_no2.Text.Trim(), AgreeContact, AgreeScheme);
            if (eff > 0)
            {
                MessageBox.Show("Successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);
            }
            else
            {
                MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnSearchPC3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC3;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
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

        private void txtPC3_TextChanged(object sender, EventArgs e)
        {
            grvStartDt.DataSource = null;
            grvStartDt.AutoGenerateColumns = false;
            grvStartDt.DataSource = CHNLSVC.General.Get_Account_Agreement_StartDt(txtPC3.Text.Trim());
        }

        private void btnViewAllStartDt_Click(object sender, EventArgs e)
        {

            grvStartDt.DataSource = null;
            grvStartDt.AutoGenerateColumns = false;
            grvStartDt.DataSource = CHNLSVC.General.Get_Account_Agreement_StartDt(string.Empty);
        }

        private void btnSaveStartDt_Click(object sender, EventArgs e)
        {
            AccountAgreementStartDate agrStartDt = new AccountAgreementStartDate();
            agrStartDt.Agrs_createddate = DateTime.Now;
            agrStartDt.Agrs_createuser = BaseCls.GlbUserID;
            agrStartDt.Agrs_date = dt_AgrStartDtNew.Value.Date;
            agrStartDt.Agrs_location = txtPC3.Text.Trim();
            agrStartDt.Agrs_modifieddate = DateTime.Now;
            agrStartDt.Agrs_sessionid = BaseCls.GlbUserSessionID;
            agrStartDt.Agrs_timestamp = DateTime.Now; 
            agrStartDt.Args_modifieduser = BaseCls.GlbUserID;
            agrStartDt.Args_com = BaseCls.GlbUserComCode;       //kapila 13/1/2014

            Int32 eff = CHNLSVC.General.Save_accountAgreement_startdate(agrStartDt);
            if (eff > 0)
            {
                MessageBox.Show("Successfully saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtPC3_TextChanged(null, null);
            }
            else
            {
                MessageBox.Show("Not saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            //try
            //   {

            //   }
            //   catch (Exception ex)
            //   {
            //       MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            //   }
            //   finally
            //   {
            //       CHNLSVC.CloseAllChannels();
            //   }
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItmCdAdd;
            _CommonSearch.ShowDialog();
            txtItmCdAdd.Select();
        }

        private void btnVeiwAddedList_Click(object sender, EventArgs e)
        {
            //if (_FinalItemList==null)
            //{
            //    _FinalItemList = new List<InvoiceItem>();
            //}
            _FinalItemList = new List<InvoiceItem>();

            List<AccountAgreementProduct> saved_prodList = CHNLSVC.General.Get_Acc_Agreement_products(txtAcc_no2.Text.Trim());
            if (saved_prodList != null)
            {
                foreach (AccountAgreementProduct prod in saved_prodList)
                {
                    InvoiceItem invItm = new InvoiceItem();
                    invItm.Sad_itm_cd = prod.Itm_code;
                    invItm.Mi_longdesc = prod.Itm_desc;
                    //invItm.Mi_model =prod.
                    invItm.Sad_qty = prod.Itm_qty;
                    invItm.Sad_unit_rt = prod.Itm_unitprice;

                    _FinalItemList.Add(invItm);
                }
            }
            grvAgrItems.DataSource = null;
            grvAgrItems.AutoGenerateColumns = false;
            grvAgrItems.DataSource = _FinalItemList;
            //invItm.Sad_itm_cd =
            //_FinalItemList =
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPC.Text)) return;
            MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, txtPC.Text.Trim());
            if (_pc == null || string.IsNullOrEmpty(_pc.Mpc_com)) { MessageBox.Show("Selected profit center is invalid. Please check the profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); Clear_Agreenement(true, true); return; }
        }

        private void txtAcc_no_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAcc_no.Text)) return;
            Clear_Agreenement(false, false);
            HpAccount _ac = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAcc_no.Text);
            if (_ac == null || string.IsNullOrEmpty(_ac.Hpa_com)) { MessageBox.Show("Please select the valid account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); Clear_Agreenement(false, true); return; }
            txtUnitPrice.Text = string.Format("{0:n2}", _ac.Hpa_cash_val);
            Account_Load();


        }

        private void Clear_Agreenement(bool _isPc, bool _isAcc)
        {
            if (_isPc) txtPC.Clear();
            if (_isAcc) txtAcc_no.Clear();

            txtAccStatus.Clear();
            txtSlipNo.Clear();

            txtCustName.Clear();
            txtCustID.Clear();
            txtCustAddr.Clear();
            txtHireVal.Clear();
            txtRental.Clear();
            txtAccBalance.Clear();
            txtUnitPrice.Clear();

            ddlHO_to.SelectedValue = string.Empty;
            txtHandOvName.Clear();
            txtHandOvID.Clear();
            ddlClosureTp.SelectedValue = string.Empty;

            grvDocs.DataSource = new DataTable();
            grvGuarantors.DataSource = new DataTable();
            gvATradeItem.DataSource = new DataTable();
            DataTable dt = CHNLSVC.General.Get_RequiredDocuments("HPAGREE");
            grvDocs.AutoGenerateColumns = false;
            grvDocs.DataSource = dt;

        }

        private void txtNIC_MM_new_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC_MM_new.Text))
            {
                if (!IsValidNIC(txtNIC_MM_new.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNIC_MM_new.Text = ""; return;
                }
            }
        }

        private void btnPerTown_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCity_MM_new;
                _CommonSearch.ShowDialog();
                txtCity_MM_new.Select();
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

        private void txtCity_MM_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPerTown_Click(null, null);
        }

        private void txtCity_MM_new_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPerTown_Click(null, null);
        }

        private void LoadPremenentTownDetails()
        {
            txtDistrict_MM_new.Text = "";
            txtProvince_MM_new.Text = "";
            txt_Postal_MM_new.Text = "";

            if (!string.IsNullOrEmpty(txtCity_MM_new.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtCity_MM_new.Text.Trim().ToUpper());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                        txtDistrict_MM_new.Text = district;
                        txtProvince_MM_new.Text = province;
                        txt_Postal_MM_new.Text = postalCD;

                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCity_MM_new.Text = "";
                        txtCity_MM_new.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCity_MM_new.Text = "";
                    txtCity_MM_new.Focus();
                }
            }
        }
        private void txtCity_MM_new_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCity_MM_new.Text != "")
                {
                    LoadPremenentTownDetails();
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

        private MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, BaseCls.GlbUserComCode);
        }

        private void load_Cust(string _code)
        {
            if (!string.IsNullOrEmpty(_code))
            {
                if (!IsValidNIC(_code))
                {
                    MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtG1_NIC_MM_new.Text = "";
                    return;
                }

               // LoadCustomerDetail(txtG1_id_MM_new.Text.Trim(), sender);
                //MasterBusinessEntity custProf = GetbyNIC(_code);
                //if (custProf.Mbe_cd != null)
                //{
                //    txtG1_id_MM_new.Text = custProf.Mbe_cd;
                //    txtG1_name_MM_new.Text = custProf.Mbe_name;
                //    txtG1_Adr1_MM_new.Text = custProf.Mbe_add1;
                //    txtG1_Adr2_MM_new.Text = custProf.Mbe_add2;
                //    txtG1_Mob_MM_new.Text = custProf.Mbe_mob;
                //    txtG1_Tel_MM_new.Text = custProf.Mbe_tel;
                //    if (custProf.Mbe_dob != Convert.ToDateTime("01/Jan/0001")) { dtG1_DOB_MM_new.Value = custProf.Mbe_dob; }
                //    txtG1_DL_MM_new.Text = custProf.Mbe_dl_no;
                //    txtG1_PP_MM_new.Text = custProf.Mbe_pp_no;
                //}

            }
        }
        private void btnsearch_gur1_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();

                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtG1_NIC_MM_new;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtG1_NIC_MM_new.Select();
                MasterBusinessEntity custProf = GetbyNIC(txtG1_NIC_MM_new.Text);
                if (custProf.Mbe_cd != null)
                {
                    txtG1_id_MM_new.Text = custProf.Mbe_cd;
                    LoadCustomerDetail(custProf.Mbe_cd, "G1");
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

        private void txtG1_id_MM_new_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnsearch_gur1_Click(null, null);
        }

        private void txtG1_id_MM_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnsearch_gur1_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtG1_name_MM_new.Focus();
        }

        private void ClearGuruantor1()
        {
            txtG1_name_MM_new.Clear();
            txtG1_NIC_MM_new.Clear();
            txtG1_Adr1_MM_new.Clear();
            txtG1_Adr2_MM_new.Clear();
            txtG1_Tel_MM_new.Clear();
            txtG1_DL_MM_new.Clear();
            txtG1_PP_MM_new.Clear();
        }
        private void ClearGuruantor2()
        {
            txtG2_name_MM_new.Clear();
            txtG2_NIC_MM_new.Clear();
            txtG2_Adr1_MM_new.Clear();
            txtG2_Adr2_MM_new.Clear();
            txtG2_Tel_MM_new.Clear();
            txtG2_DL_MM_new.Clear();
            txtG2_PP_MM_new.Clear();

        }
        private void ClearGuruantor3()
        {
            txtG3_name_MM_new.Clear();
            txtG3_NIC_MM_new.Clear();
            txtG3_Adr1_MM_new.Clear();
            txtG3_Adr2_MM_new.Clear();
            txtG3_Tel_MM_new.Clear();
            txtG3_DL_MM_new.Clear();
            txtG3_PP_MM_new.Clear();
        }

        private void LoadCustomerDetail(string _code, string _tp)
        {
            try
            {
                //string c_name = ((TextBox)sender).Name;
                //if (c_name.Contains("txtG1_id_MM_new"))
                //{
                //    ClearGuruantor1();
                //}
                //if (c_name.Contains("txtG2_id_MM_new"))
                //{
                //    ClearGuruantor2();
                //}
                //if (c_name.Contains("txtG3_id_MM_new"))
                //{
                //    ClearGuruantor3();
                //}

                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _code, string.Empty, string.Empty, "C");
                if (_masterBusinessCompany != null && !string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_tp=="G1")
                    {
                        txtG1_name_MM_new.Text = _masterBusinessCompany.Mbe_name;
                        txtG1_NIC_MM_new.Text = _masterBusinessCompany.Mbe_nic;
                        txtG1_Adr1_MM_new.Text = _masterBusinessCompany.Mbe_add1;
                        txtG1_Adr2_MM_new.Text = _masterBusinessCompany.Mbe_add2;
                        txtG1_Tel_MM_new.Text = _masterBusinessCompany.Mbe_mob;
                        if (_masterBusinessCompany.Mbe_dob != Convert.ToDateTime("01/Jan/0001")) { dtG1_DOB_MM_new.Value = _masterBusinessCompany.Mbe_dob; }
                        txtG1_DL_MM_new.Text = _masterBusinessCompany.Mbe_dl_no;
                        txtG1_PP_MM_new.Text = _masterBusinessCompany.Mbe_pp_no;
                    }
                    if (_tp == "G2")
                    {
                        txtG2_name_MM_new.Text = _masterBusinessCompany.Mbe_name;
                        txtG2_NIC_MM_new.Text = _masterBusinessCompany.Mbe_nic;
                        txtG2_Adr1_MM_new.Text = _masterBusinessCompany.Mbe_add1;
                        txtG2_Adr2_MM_new.Text = _masterBusinessCompany.Mbe_add2;
                        txtG2_Tel_MM_new.Text = _masterBusinessCompany.Mbe_mob;
                        if (_masterBusinessCompany.Mbe_dob != Convert.ToDateTime("01/Jan/0001")) { dtG2_DOB_MM_new.Value = _masterBusinessCompany.Mbe_dob; }
                        txtG2_DL_MM_new.Text = _masterBusinessCompany.Mbe_dl_no;
                        txtG2_PP_MM_new.Text = _masterBusinessCompany.Mbe_pp_no;
                    }
                    if (_tp == "G3")
                    {
                        txtG3_name_MM_new.Text = _masterBusinessCompany.Mbe_name;
                        txtG3_NIC_MM_new.Text = _masterBusinessCompany.Mbe_nic;
                        txtG3_Adr1_MM_new.Text = _masterBusinessCompany.Mbe_add1;
                        txtG3_Adr2_MM_new.Text = _masterBusinessCompany.Mbe_add2;
                        txtG3_Tel_MM_new.Text = _masterBusinessCompany.Mbe_mob;
                       if (_masterBusinessCompany.Mbe_dob != Convert.ToDateTime("01/Jan/0001")) { dtG3_DOB_MM_new.Value = _masterBusinessCompany.Mbe_dob;}
                        txtG3_DL_MM_new.Text = _masterBusinessCompany.Mbe_dl_no;
                        txtG3_PP_MM_new.Text = _masterBusinessCompany.Mbe_pp_no;
                    }
                    return;
                }
                MessageBox.Show("There is no such customer available. Please check the customer code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_tp == "G1")
                {
                    txtG1_id_MM_new.Clear();
                    txtG1_id_MM_new.Focus();
                }
                if (_tp == "G2")
                {
                    txtG2_id_MM_new.Clear();
                    txtG2_id_MM_new.Focus();
                }
                if (_tp == "G3")
                {
                    txtG3_id_MM_new.Clear();
                    txtG3_id_MM_new.Focus();
                }

                return;
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
        private void txtG1_id_MM_new_Leave(object sender, EventArgs e)
        {
            //Load Details
            if (string.IsNullOrEmpty(txtG1_id_MM_new.Text)) return;
            //LoadCustomerDetail(txtG1_id_MM_new.Text.Trim(), sender);
        }

        private void btnsearch_gur2_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();

                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtG2_NIC_MM_new;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtG2_NIC_MM_new.Select();
                MasterBusinessEntity custProf = GetbyNIC(txtG2_NIC_MM_new.Text);
                if (custProf.Mbe_cd != null)
                {
                    txtG2_id_MM_new.Text = custProf.Mbe_cd;
                    LoadCustomerDetail(custProf.Mbe_cd, "G2");
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

        private void txtG2_id_MM_new_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnsearch_gur2_Click(null, null);
        }

        private void txtG2_id_MM_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnsearch_gur2_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtG2_name_MM_new.Focus();

        }

        private void txtG2_id_MM_new_Leave(object sender, EventArgs e)
        {
            //Load Details
            if (string.IsNullOrEmpty(txtG2_id_MM_new.Text)) return;
            //LoadCustomerDetail(txtG2_id_MM_new.Text.Trim(), sender);
        }

        private void btnsearch_gur3_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();

                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtG3_NIC_MM_new;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtG3_NIC_MM_new.Select();
                MasterBusinessEntity custProf = GetbyNIC(txtG3_NIC_MM_new.Text);
                if (custProf.Mbe_cd != null)
                {
                    txtG3_id_MM_new.Text = custProf.Mbe_cd;
                    LoadCustomerDetail(custProf.Mbe_cd, "G3");
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

        private void txtG3_id_MM_new_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnsearch_gur3_Click(null, null);
        }

        private void txtG3_id_MM_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnsearch_gur3_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtG3_name_MM_new.Focus();
        }

        private void txtG3_id_MM_new_Leave(object sender, EventArgs e)
        {
            //Load Details
            if (string.IsNullOrEmpty(txtG3_id_MM_new.Text)) return;
            //LoadCustomerDetail(txtG3_id_MM_new.Text.Trim(), sender);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPC.Text) || string.IsNullOrEmpty(txtAcc_no.Text)) return;
            if ((string.IsNullOrEmpty(txtPC_2.Text) && string.IsNullOrEmpty(txtAcc_no2.Text)) || string.IsNullOrEmpty(txtAcc_no2.Text))
            {
                //assign new pc and account
                txtPC_2.Text = txtPC.Text.Trim();
                txtAcc_no2.Text = txtAcc_no.Text.Trim();
                txtAcc_no2_Leave(null, null);
            }

            if (!string.IsNullOrEmpty(txtPC_2.Text) && !string.IsNullOrEmpty(txtAcc_no2.Text))
            {
                if ((!txtPC_2.Text.Contains(txtPC.Text) && !txtAcc_no2.Text.Contains(txtAcc_no.Text)) || !txtAcc_no2.Text.Contains(txtAcc_no.Text))
                {
                    txtPC_2.Text = txtPC.Text.Trim();
                    txtAcc_no2.Text = txtAcc_no.Text.Trim();
                    txtAcc_no2_Leave(null, null);
                }
            }

        }

        private void txtAcc_no2_Leave(object sender, EventArgs e)
        {
            try
            {

                _FinalItemList = BindAccountItemForMisMatch(txtAcc_no2.Text.Trim());
                grvAgrItems.DataSource = null;
                grvAgrItems.AutoGenerateColumns = false;

                txtG1_id_MM.Text = "";
                txtG1_name_MM.Text = "";
                txtG1_NIC_MM.Text = "";
                txtG1_Adr1_MM.Text = "";
                txtG1_Adr2_MM.Text = "";
                txtG1_Tel_MM.Text = "";
                dtG1_DOB_MM.Text = "";
                txtG1_DL_MM.Text = "";
                txtG1_PP_MM.Text = "";

                txtG2_id_MM.Text = "";
                txtG2_name_MM.Text = "";
                txtG2_NIC_MM.Text = "";
                txtG2_Adr1_MM.Text = "";
                txtG2_Adr2_MM.Text = "";
                txtG2_Tel_MM.Text = "";
                dtG2_DOB_MM.Text = "";
                txtG2_DL_MM.Text = "";
                txtG2_PP_MM.Text = "";

                txtG3_id_MM.Text = "";
                txtG3_name_MM.Text = "";
                txtG3_NIC_MM.Text = "";
                txtG3_Adr1_MM.Text = "";
                txtG3_Adr2_MM.Text = "";
                txtG3_Tel_MM.Text = "";
                dtG3_DOB_MM.Text = "";
                txtG3_DL_MM.Text = "";
                txtG3_PP_MM.Text = "";

                txtGurName_MM.Text = "";
                txtGurNIC_MM.Text = "";
                txtGurDL_MM.Text = "";
                txtGurPP_MM.Text = "";
                //dt_GurDOB_MM.Value = DateTime.Now.Date;



                #region Account - Gurantors
                DataTable dt = CHNLSVC.Sales.Get_gurantors(txtAcc_no2.Text.Trim());
                if (dt != null)
                {
                    Int32 count = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime _bday = DateTime.Now.Date;
                        string G_code = dr["htc_cust_cd"].ToString();
                        string G_NIC = dr["mbe_nic"].ToString();
                        string G_name = dr["mbe_name"].ToString();
                        string Address1 = dr["mbe_add1"].ToString();
                        string Address2 = dr["mbe_add2"].ToString();
                        string _mobile = dr["mbe_mob"].ToString();
                        string _tel = dr["MBE_TEL"].ToString();
                        _bday = dr["MBE_DOB"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(dr["MBE_DOB"]);
                        string _DL = dr["MBE_DL_NO"].ToString();
                        string _PP = dr["MBE_PP_NO"].ToString();


                        if (count == 1)
                        {
                            //Gurantor one.
                            txtG1_id_MM.Text = G_code;
                            txtG1_name_MM.Text = G_name;
                            txtG1_NIC_MM.Text = G_NIC;
                            txtG1_Adr1_MM.Text = Address1;
                            txtG1_Adr2_MM.Text = Address2;
                            txtG1_Tel_MM.Text = _tel;
                            dtG1_DOB_MM.Value = _bday;
                            txtG1_DL_MM.Text = _DL;
                            txtG1_PP_MM.Text = _PP;


                            MasterBusinessEntity be = CHNLSVC.Sales.GetCustomerProfile(G_code, string.Empty, string.Empty, string.Empty, string.Empty);
                            if (be != null)
                            {
                                txtGurName_MM.Text = be.Mbe_name;
                                txtGurNIC_MM.Text = be.Mbe_nic;
                                txtGurDL_MM.Text = be.Mbe_dl_no;
                                txtGurPP_MM.Text = be.Mbe_pp_no;
                                txtG1_Mob_MM.Text = _mobile;
                                try
                                {
                                    dt_GurDOB_MM.Value = be.Mbe_dob;
                                    dt_GurDOB_MM_new.Value = be.Mbe_dob;
                                }
                                catch (Exception ex)
                                {

                                }


                            }
                        }

                        if (count == 2)
                        {
                            //Gurantor two.
                            txtG2_id_MM.Text = G_code;
                            txtG2_name_MM.Text = G_name;
                            txtG2_NIC_MM.Text = G_NIC;
                            txtG2_Adr1_MM.Text = Address1;
                            txtG2_Adr2_MM.Text = Address2;
                            txtG2_Mob_MM.Text = _mobile;
                            txtG2_Tel_MM.Text = _tel;
                            dtG2_DOB_MM.Value = _bday;
                            txtG2_DL_MM.Text = _DL;
                            txtG2_PP_MM.Text = _PP;
                        }

                        if (count == 3)
                        {
                            //Gurantor three.
                            txtG3_id_MM.Text = G_code;
                            txtG3_name_MM.Text = G_name;
                            txtG3_NIC_MM.Text = G_NIC;
                            txtG3_Adr1_MM.Text = Address1;
                            txtG3_Adr2_MM.Text = Address2;
                            txtG3_Mob_MM.Text = _mobile;
                            txtG3_Tel_MM.Text = _tel;
                            dtG3_DOB_MM.Value = _bday;
                            txtG3_DL_MM.Text = _DL;
                            txtG3_PP_MM.Text = _PP;
                        }
                        count = count + 1;
                    }


                }
                #endregion

                #region Account - Item Detail
                BindAccountItem(txtAcc_no2.Text.Trim());
                #endregion

                #region Account - Customer Detail
                MasterBusinessEntity cust = CHNLSVC.Sales.GetCustomerProfile(txtCustID.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                txtNIC_MM.Text = cust.Mbe_nic;
                txtGurNIC_MM.Text = cust.Mbe_nic;

                txtDL_MM.Text = cust.Mbe_dl_no;
                txtPP_MM.Text = cust.Mbe_pp_no;

                txtFirstName_MM.Text = cust.Mbe_name;
                txtGurName_MM.Text = cust.Mbe_name;

                if (cust.Mbe_sex == "FEMALE")
                {
                    txtTitle_MM.Text = "Ms";
                    txtGurTitle_MM.Text = "Ms";
                    txtGurTitle_MM_new.Text = "Ms";
                }
                else
                {
                    txtTitle_MM.Text = "Mr";
                    txtGurTitle_MM.Text = "Mr";
                    txtGurTitle_MM_new.Text = "Mr";
                }
                txtProvince_MM.Text = cust.Mbe_cr_province_cd;
                txtDistrict_MM.Text = cust.Mbe_distric_cd;
                txtTelHome_MM.Text = cust.Mbe_contact;
                txtTelResident_MM.Text = cust.Mbe_cr_tel;
                txtMobile_MM.Text = cust.Mbe_mob;

                txtAddr_MM.Text = cust.Mbe_add1 + cust.Mbe_add2;
                txtCity_MM.Text = cust.Mbe_town_cd;
                txtDistrict_MM.Text = cust.Mbe_distric_cd;
                txtProvince_MM.Text = cust.Mbe_province_cd;
                txt_Postal_MM.Text = cust.Mbe_postal_cd;
                txtTelHome_MM.Text = cust.Mbe_tel;
                txtTelResident_MM.Text = cust.Mbe_cr_tel;
                txtMobile_MM.Text = cust.Mbe_mob;

                try
                {
                    dtpDOB_MM.Value = cust.Mbe_dob;
                    dt_GurDOB_MM.Value = cust.Mbe_dob;
                }
                catch (Exception ex)
                {

                }
                #endregion

                #region Agreement - Detail
                AccountAgreementContact acAgree_Contact = CHNLSVC.General.Get_Accounts_Agreement_Contact(txtAcc_no2.Text.Trim());
                //Agreement No Data
                if (acAgree_Contact == null)
                {
                    txtNIC_MM_new.Text = cust.Mbe_nic;
                    txtGurNIC_MM_new.Text = cust.Mbe_nic;


                    txtDL_MM_new.Text = cust.Mbe_dl_no;
                    txtPP_MM_new.Text = cust.Mbe_pp_no;
                    txtGurPP_MM_new.Text = cust.Mbe_pp_no;      //kapila 17/12/13

                    if (cust.Mbe_sex == "FEMALE")
                    {
                        txtTitle_MM_new.Text = "Ms";
                    }
                    else
                    {
                        txtTitle_MM_new.Text = "Mr";
                    }

                    txtProvince_MM_new.Text = cust.Mbe_cr_province_cd;
                    txtDistrict_MM_new.Text = cust.Mbe_distric_cd;
                    txtTelHome_MM_new.Text = cust.Mbe_contact;
                    txtTelResident_MM_new.Text = cust.Mbe_cr_tel;
                    txtMobile_MM_new.Text = cust.Mbe_mob;

                    try
                    {
                        dtpDOB_MM.Value = cust.Mbe_dob;
                    }
                    catch (Exception ex)
                    {

                    }

                    //--------------------------------
                    txtG1_id_MM_new.Text = txtG1_id_MM.Text;
                    txtG1_name_MM_new.Text = txtG1_name_MM.Text;
                    txtG1_NIC_MM_new.Text = txtG1_NIC_MM.Text;
                    txtG1_Adr1_MM_new.Text = txtG1_Adr1_MM.Text;
                    txtG1_Adr2_MM_new.Text = txtG1_Adr2_MM.Text;
                    txtG1_Mob_MM_new.Text = txtG1_Mob_MM.Text;
                    txtG1_Tel_MM_new.Text = txtG1_Tel_MM.Text;
                    txtG1_DL_MM_new.Text = txtG1_DL_MM.Text;
                    txtG1_PP_MM_new.Text = txtG1_PP_MM.Text;

                    txtG2_id_MM_new.Text = txtG2_id_MM.Text;
                    txtG2_name_MM_new.Text = txtG2_name_MM.Text;
                    txtG2_NIC_MM_new.Text = txtG2_NIC_MM.Text;
                    txtG2_Adr1_MM_new.Text = txtG2_Adr1_MM.Text;
                    txtG2_Adr2_MM_new.Text = txtG2_Adr2_MM.Text;
                    txtG2_Mob_MM_new.Text = txtG2_Mob_MM.Text;
                    txtG2_Tel_MM_new.Text = txtG2_Tel_MM.Text;
                    txtG2_DL_MM_new.Text = txtG2_DL_MM.Text;
                    txtG2_PP_MM_new.Text = txtG2_PP_MM.Text;

                    txtG3_id_MM_new.Text = txtG3_id_MM.Text;
                    txtG3_name_MM_new.Text = txtG3_name_MM.Text;
                    txtG3_NIC_MM_new.Text = txtG3_NIC_MM.Text;
                    txtG3_Adr1_MM.Text = txtG3_Adr1_MM.Text;
                    txtG3_Adr2_MM_new.Text = txtG3_Adr2_MM.Text;
                    txtG3_Mob_MM_new.Text = txtG3_Mob_MM.Text;
                    txtG3_Tel_MM_new.Text = txtG3_Tel_MM.Text;
                    txtG3_DL_MM_new.Text = txtG3_DL_MM.Text;
                    txtG3_PP_MM_new.Text = txtG3_PP_MM.Text;
                }
                else
                //Agreement Data
                {
                    txtNIC_MM_new.Text = acAgree_Contact.Cnt_nic;//cust.Mbe_nic;
                    txtDL_MM_new.Text = acAgree_Contact.Cnt_dl;// cust.Mbe_dl_no;
                    txtGurDL_MM_new.Text = acAgree_Contact.Cnt_dl;// cust.Mbe_dl_no;    kapila 17/12/13

                    txtPP_MM_new.Text = acAgree_Contact.Cnt_passport;// cust.Mbe_pp_no;
                    txtGurPP_MM_new.Text = acAgree_Contact.Cnt_passport;// cust.Mbe_pp_no;  kapila
                    txtTitle_MM_new.Text = acAgree_Contact.Cnt_title;
                    txtGurTitle_MM_new.Text = acAgree_Contact.Cnt_title;     //kapila 17/12/13

                    txtFirstName_MM_new.Text = acAgree_Contact.Cnt_fname;
                    txtGurName_MM_new.Text = acAgree_Contact.Cnt_fname;     //kapila 15/12/13

                    dtpDOB_MM_new.Value = acAgree_Contact.Cnt_dob;          //kapila 15/12/13
                    dt_GurDOB_MM_new.Value = acAgree_Contact.Cnt_dob;

                    txtAddr_MM_new.Text = acAgree_Contact.Cnt_add1 + acAgree_Contact.Cnt_add2 + acAgree_Contact.Cnt_add3;
                    if (acAgree_Contact.Cnt_sex == "FEMALE")
                    {
                        txtTitle_MM_new.Text = "Ms";
                    }
                    else
                    {
                        txtTitle_MM_new.Text = "Mr";
                    }

                    txtProvince_MM_new.Text = acAgree_Contact.Cnt_province;//cust.Mbe_cr_province_cd;
                    txtDistrict_MM_new.Text = acAgree_Contact.Cnt_district;// cust.Mbe_distric_cd;
                    txtCity_MM_new.Text = acAgree_Contact.Cnt_city;
                    txtTelHome_MM_new.Text = acAgree_Contact.Cnt_telhome;// cust.Mbe_contact;
                    txtTelResident_MM_new.Text = acAgree_Contact.Cnt_telresident;// cust.Mbe_cr_tel;
                    txtMobile_MM_new.Text = acAgree_Contact.Cnt_mobile;// cust.Mbe_mob;

                    //--------------------------------
                    txtG1_id_MM_new.Text = acAgree_Contact.Cnt_guarantor1_code;
                    txtG1_name_MM_new.Text = acAgree_Contact.Cnt_guarantor1_name;
                    txtG1_NIC_MM_new.Text = acAgree_Contact.Cnt_guarantor1_nic;
                    txtG1_Adr1_MM_new.Text = acAgree_Contact.Cnt_guarantor1_addr1;
                    txtG1_Adr2_MM_new.Text = acAgree_Contact.Cnt_guarantor1_addr2;
                    txtG1_Mob_MM_new.Text = acAgree_Contact.Cnt_guarantor1_mobile;
                    txtG1_Tel_MM_new.Text = acAgree_Contact.Cnt_guarantor1_tel;
                    txtG1_DL_MM_new.Text = acAgree_Contact.Cnt_guarantor1_DL;
                    txtG1_PP_MM_new.Text = acAgree_Contact.Cnt_guarantor1_PP;

                    txtG2_id_MM_new.Text = acAgree_Contact.Cnt_guarantor2_code;
                    txtG2_name_MM_new.Text = acAgree_Contact.Cnt_guarantor2_name;
                    txtG2_NIC_MM_new.Text = acAgree_Contact.Cnt_guarantor2_nic;
                    txtG2_Adr1_MM_new.Text = acAgree_Contact.Cnt_guarantor2_addr1;
                    txtG2_Adr2_MM_new.Text = acAgree_Contact.Cnt_guarantor2_addr2;
                    txtG2_Mob_MM_new.Text = acAgree_Contact.Cnt_guarantor2_mobile;
                    txtG2_Tel_MM_new.Text = acAgree_Contact.Cnt_guarantor2_tel;
                    txtG2_DL_MM_new.Text = acAgree_Contact.Cnt_guarantor2_DL;
                    txtG2_PP_MM_new.Text = acAgree_Contact.Cnt_guarantor2_PP;

                    txtG3_id_MM_new.Text = acAgree_Contact.Cnt_guarantor3_code;
                    txtG3_name_MM_new.Text = acAgree_Contact.Cnt_guarantor3_name;
                    txtG3_NIC_MM_new.Text = acAgree_Contact.Cnt_guarantor3_nic;
                    txtG3_Adr1_MM.Text = acAgree_Contact.Cnt_guarantor3_addr1;
                    txtG3_Adr2_MM_new.Text = acAgree_Contact.Cnt_guarantor3_addr2;
                    txtG3_Mob_MM_new.Text = acAgree_Contact.Cnt_guarantor3_mobile;
                    txtG3_Tel_MM_new.Text = acAgree_Contact.Cnt_guarantor3_tel;
                    txtG3_DL_MM_new.Text = acAgree_Contact.Cnt_guarantor3_DL;
                    txtG3_PP_MM_new.Text = acAgree_Contact.Cnt_guarantor3_PP;

                    this.btnVeiwAddedList_Click(null, null);

                }
                #endregion

                #region Agreement Schemes
                AccountAgreementScheme acAgree_Sch = CHNLSVC.General.Get_Accounts_Agreement_Scheme(txtAcc_no2.Text.Trim());

                if (acAgree_Sch != null)
                {
                    txtSchemeCD_MM_new.Text = acAgree_Sch.Sch_code;
                    txtSchmeDesc_MM_new.Text = acAgree_Sch.Sch_desc;
                    txtSchmePeriod_MM_new.Text = acAgree_Sch.Sch_term.ToString();

                }
                #endregion
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

        private void btnsearch_scheme_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
            DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSchemeCD_MM_new;
            _CommonSearch.ShowDialog();
            txtSchemeCD_MM_new.Select();

            HpSchemeDetails _SchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(txtSchemeCD_MM_new.Text.ToString());
            txtSchmeDesc_MM_new.Text = _SchemeDetails.Hsd_desc;
            txtSchmePeriod_MM_new.Text = _SchemeDetails.Hsd_term.ToString();
        }

        private void txtItmCdAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItemSearch_Click(null, null);

        }

        private void txtItmCdAdd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItemSearch_Click(null, null);
        }

        private void txtItmCdAdd_Leave(object sender, EventArgs e)
        {

        }

        private void txtSchemeCD_MM_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnsearch_scheme_Click(null, null);
            }
        }

        private void txtSchemeCD_MM_new_DoubleClick(object sender, EventArgs e)
        {
            this.btnsearch_scheme_Click(null, null);
        }

        private void txtAcc_no_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
