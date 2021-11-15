using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class AdjustmentReceipt : Base
    {
        List<ReturnChequeCalInterest> _newAddList = new List<ReturnChequeCalInterest>();
        int _efect_rem = 0;
        DataTable dt_Acc = null;
        Deposit_Bank_Pc_wise objAdjReceipt;
        int sequence_no;
        string recept_no;


        public AdjustmentReceipt()
        {
            InitializeComponent();
            BindBanks();
            //Load_Adj_Types();




        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutstandingInv:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Division:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.satReceiptByAnal3:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator + "ADVAN");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReturnCheque:
                    {
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text.Trim().ToUpper() + seperator);                        
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text.Trim().ToUpper() + seperator + DropDownListBank.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CreditNoteWithoutSRN: //Add by Akila 2017/01/24
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text+seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void Load_Adj_Types()
        {
            DataTable dt = CHNLSVC.Sales.Load_Adj_TypesDets();
            
            //Add by akila 2017/01/24
            if (dt.Rows.Count > 0)
            {
                BindingSource _bindSource = new BindingSource();
                _bindSource.DataSource = dt;
                cmbRecType.DataSource = _bindSource.DataSource;
                cmbRecType.DisplayMember = "SAJD_TP_DESC";
                cmbRecType.ValueMember = "SAJD_TP";
                cmbRecType.SelectedIndex = -1;
            }
            else { cmbRecType.SelectedIndex = -1; }
            //cmbRecType.Items.Clear();

            //cmbRecType.DataSource = dt;
            //cmbRecType.DisplayMember = "SAJD_TP_DESC";
            //cmbRecType.ValueMember = "SAJD_TP";
            //cmbRecType.SelectedIndex = -1;
            //cmbRecType.Text = "--Select Adjustment Type--";
        }
        private void Load_Sub_Types(string _adj_type)
        {
            DataTable dt = CHNLSVC.Sales.Load_Sub_Adj_Types(_adj_type);

            //cmbSubType.DataSource = dt;
            //cmbSubType.DisplayMember = "SUB_TYPE_DESC";
            //cmbSubType.ValueMember = "SAJD_SUB_TP";

            if (dt.Rows.Count > 0) 
            {
                BindingSource _bindSource = new BindingSource();
                _bindSource.DataSource = dt;
                cmbSubType.DataSource = _bindSource;
                cmbSubType.DisplayMember = "SUB_TYPE_DESC";
                cmbSubType.ValueMember = "SAJD_SUB_TP";
                cmbSubType.SelectedIndex = 0; 
            }
            else { cmbSubType.SelectedIndex = -1; }
            

            //cmbSubType.Text = "--Select Sub Type--";
            //cmbSubType.SelectedIndex = -1;
        }

        private void Load_AccountDetails(string _adjtype, string _subtype)
        {
            if (cmbSubType.SelectedIndex != -1)
            {
                dt_Acc = CHNLSVC.Sales.Load_Adj_Acc_Details(_adjtype, _subtype);
                if (dt_Acc != null && dt_Acc.Rows.Count > 0)
                {
                    txtDebitAcc.Text = dt_Acc.Rows[0]["SAJD_ACC_DBT"].ToString();
                    txtCreditAcc.Text = dt_Acc.Rows[0]["SAJD_ACC_CRD"].ToString();
                    _efect_rem = Convert.ToInt32(dt_Acc.Rows[0]["SAJD_EFCT_REM"]);
                }
            }
        }


        private void BindBanks()
        {
            try
            {
                DataTable datasource2 = CHNLSVC.Financial.GetBanks();
                DropDownListBank.DataSource = datasource2;
                DropDownListBank.DisplayMember = "mbi_desc";
                DropDownListBank.ValueMember = "mbi_cd";
                DropDownListBank.SelectedIndex = -1;

                //cmbRecType.SelectedIndex = 0;
                TextBoxLocation.Text = BaseCls.GlbUserDefProf;

                MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                _RecDiv = CHNLSVC.Sales.GetDefRecDivision(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                if (_RecDiv.Msrd_cd != null)
                {
                    txtDivision.Text = _RecDiv.Msrd_cd;
                }
                else
                {
                    txtDivision.Text = "";
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


        private void btnClear_Click(object sender, EventArgs e)
        {
            TextBoxLocation.Text = "";
            DropDownListBank.SelectedIndex = -1;
            txtAmt.Text = "0.00";
            txtRem.Text = "";
            clear();
            clear_srch_chq();
            cmbSubType.DataSource = null;
        }

        private void clear()
        {
            //clear_srch_chq();
            //cmbSubType.DataSource = null;
            DropDownListBank.SelectedIndex = -1;
            TextBoxChequeNumber.Text = "";
            txtRef.Text = "";
            lblRetDt.Text = "";
            lblset_int.Text = "";
            lblset_capital.Text = "";
            lblSysVal.Text = "0.00";
            lblActVal.Text = "0.00";
            lblSet.Text = "0.00";
            lblInt.Text = "0.00";
            lblBal.Text = "0.00";

            // txtDivision.Text = "";
            txtInvoice.Text = "";
            txtCusCode.Text = "";
            txtBalance.Text = "0.00";
            lblAName.Text = "";
            lblAAddress1.Text = "";
            lblMobile.Text = "";
            lblNIC.Text = "";

            txtAdvRecNo.Text = "";
            lblAdvAddr.Text = "";
            lblAdvBal.Text = "0.00";
            lblAdvName.Text = "";
            lblAdvRecAmt.Text = "0.00";
            lblUsedAmt.Text = "0.00";
            lblItem.Text = "";
            lblSerial.Text = "";
            lblDesc.Text = "";
            lblManRef.Text = "";
            lblModel.Text = "";
            txtRecSbTp.Text = "";

            //Add by Akila - Re-Active Credit Note
            txtCreditNote.Text = "";
            lblCrnAmt.Text = "0.00";
            lblCrnBalanceAmt.Text = "0.00";
            lblCrnReduceAmt.Text = "0.00";
            lblCrnUsedAmt.Text = "0.00";
            pnlCreditNoteControls.Visible = false;
            pnlFooterControls.Visible = true;
            pnlAdvRec.Visible = true;
            btnActive.Visible = false;
            btnSave.Visible = true;

            txtDebitAcc.Text = "";
            txtCreditAcc.Text = "";
            _efect_rem = 0;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region validations
            if (string.IsNullOrEmpty(txtAmt.Text))
            {
                MessageBox.Show("Amount cannot be Empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                MessageBox.Show("Please select Profit Center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Focus();
                return;
            }
            if (cmbRecType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Adjustment Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Focus();
                return;
            }
            if (cmbSubType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Sub Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Focus();
                return;
            }
            if (Convert.ToDecimal(txtAmt.Text) <= 0)
            {
                MessageBox.Show("Invalid Amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRem.Text))
            {
                MessageBox.Show("Please enter remarks !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRem.Focus();
                return;
            }

            if (cmbRecType.Text == "ADVANCE RECEIPT")   //advance refund
            {
                if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(lblAdvBal.Text))
                {
                    MessageBox.Show("Invalid Amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmt.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAdvRecNo.Text))
                {
                    MessageBox.Show("Please select receipt number !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdvRecNo.Focus();
                    return;
                }
            }
            if (cmbRecType.Text == "RETURN CHEQUE")   //return chq
            {
                if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(lblBal.Text))
                {
                    MessageBox.Show("Invalid Amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmt.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(TextBoxChequeNumber.Text))
                {
                    MessageBox.Show("Please enter cheque number !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxChequeNumber.Focus();
                    return;
                }
                if (DropDownListBank.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select bank !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxChequeNumber.Focus();
                    return;
                }
            }
            if (cmbRecType.Text == "CREDIT SALES")   //credit sale settlement
            {
                if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(txtBalance.Text))
                {
                    MessageBox.Show("Invalid Amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmt.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    MessageBox.Show("Please select invoice number !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInvoice.Focus();
                    return;
                }
                if(cmbSubType.SelectedValue.ToString() == "AFSL")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10143))
                    {
                        MessageBox.Show("Sorry, You have no permission!\n( Advice: Required permission code :10143 )");
                        return;
                    }
                }
            }
            #endregion

            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            #region advance refund
            if (cmbRecType.Text == "ADVANCE RECEIPT" && _efect_rem == 1) //advance refund
            {

                RecieptHeader _ReceiptHeader = new RecieptHeader();
                _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdrByAnal3(txtAdvRecNo.Text, TextBoxLocation.Text.Trim(), BaseCls.GlbUserComCode)[0];

                //_ReceiptHeader.Sar_receipt_no = txtAdvRecNo.Text;
                _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _ReceiptHeader.Sar_receipt_type = "ADREF";
                _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(txtAmt.Text);
                _ReceiptHeader.Sar_receipt_date = dtDate.Value.Date;
                _ReceiptHeader.Sar_direct = false;
                _ReceiptHeader.Sar_acc_no = "";
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
                _ReceiptHeader.Sar_profit_center_cd = TextBoxLocation.Text;
                _ReceiptHeader.Sar_is_mgr_iss = false;
                _ReceiptHeader.Sar_currency_cd = "LKR";
                _ReceiptHeader.Sar_uploaded_to_finance = false;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                _ReceiptHeader.Sar_direct_deposit_branch = "";
                _ReceiptHeader.Sar_is_used = false;
                _ReceiptHeader.Sar_ser_job_no = "";
                _ReceiptHeader.Sar_used_amt = 0;
                _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _ReceiptHeader.Sar_anal_5 = 0;
                _ReceiptHeader.Sar_anal_6 = 0;
                _ReceiptHeader.Sar_anal_7 = 0;
                _ReceiptHeader.Sar_anal_8 = 0;
                _ReceiptHeader.Sar_anal_9 = 0;
                _ReceiptHeader.Sar_remarks = txtRem.Text;


                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = TextBoxLocation.Text;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = txtDivision.Text;
                masterAuto.Aut_year = null;



                RecieptItem ri = new RecieptItem();
                //ri = _i;
                ri.Sard_settle_amt = Convert.ToDecimal(txtAmt.Text);
                ri.Sard_pay_tp = "CASH";
                ri.Sard_line_no = 1;


                List<RecieptItem> _RecItemList = new List<RecieptItem>();
                _RecItemList.Add(ri);

                #region remitance summary
                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, TextBoxLocation.Text, Convert.ToDateTime(dtDate.Text).Date);

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = TextBoxLocation.Text;
                _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Value).Date;
                _remSumDet.Rem_sec = "03";
                _remSumDet.Rem_cd = "018";
                _remSumDet.Rem_sh_desc = "Advance Receipt Adjustment (HO)";
                _remSumDet.Rem_lg_desc = "Advance Receipt Adjustment (HO)";
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_week = (_wkNo + "S").ToString();
                _remSumDet.Rem_ref_no = "";
                _remSumDet.Rem_rmk = "";
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet.Rem_is_sos = false;
                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = false;
                _remSumDet.Rem_del_alw = false;
                _remSumDet.Rem_is_rem_sum = false;
                _remSumDet.Rem_cat = 12;
                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";
                _remSumDet.Rem_bnk_cd = "";
                _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
                //8/8/2013
                _remSumDet.REM_CHQNO = "";
                _remSumDet.REM_CHQ_BANK_CD = "";
                _remSumDet.REM_CHQ_BRANCH = "";
                _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                _remSumDet.REM_DEPOSIT_BANK_CD = "";
                _remSumDet.REM_DEPOSIT_BRANCH = "";

                #endregion


                #region Save_Adjustment_Recept

                objAdjReceipt = new Deposit_Bank_Pc_wise();

                //objAdjReceipt.Seq_no = Convert.ToDouble(seqNo);
                objAdjReceipt.Company = BaseCls.GlbUserComCode;
                objAdjReceipt.Profit_center = TextBoxLocation.Text;
                objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
                objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
                objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
                objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
                objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
                if (rdoAdjPlus.Checked)
                {
                    objAdjReceipt.Adj_direct = true;
                }
                else
                {
                    objAdjReceipt.Adj_direct = false;
                }
                objAdjReceipt.Remark = txtRem.Text.Trim();
                objAdjReceipt.Createby = BaseCls.GlbUserID;
                objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                //if (rdoAdjPlus.Checked)
                //{
                //    objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                //}
                //else
                //{
                //    objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text) * -1;
                //}

                //objAdjReceipt.Ref_lnk = _cusNo;
                //objAdjReceipt.Ref_seq = Convert.ToDecimal(seqNo);

                #endregion


                string _RefundNo = "";
                int effect = CHNLSVC.Sales.CreateRefundByAdj(_ReceiptHeader, objAdjReceipt, _RecItemList, masterAuto, null, _remSumDet, out _RefundNo);
                if (effect > 0)
                {
                    MessageBox.Show("Updated Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (cmbRecType.Text == "ADVANCE RECEIPT" && _efect_rem == 0)
            {

                Int32 eff = 0;


                //update reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                recieptHeadder.Sar_mod_by = BaseCls.GlbUserID;
                //if (rdoAdjPlus.Checked)
                //{
                //      recieptHeadder.Sar_used_amt = Convert.ToDecimal(txtAmt.Text.Trim());
                //}
                //else
                //{
                //      recieptHeadder.Sar_used_amt = Convert.ToDecimal(txtAmt.Text.Trim()) * -1;
                //}
                recieptHeadder.Sar_used_amt = Convert.ToDecimal(txtAmt.Text.Trim());
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_receipt_no = recept_no;



                //Update Master Auto reciept no
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = TextBoxLocation.Text;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = txtDivision.Text;
                masterAuto.Aut_year = null;


                //RemitanceSummaryDetail update
                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = TextBoxLocation.Text;
                _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Value).Date;
                _remSumDet.Rem_sec = "03";
                _remSumDet.Rem_cd = "018";
                _remSumDet.Rem_lg_desc = "Advance Receipt Adjustment (HO)";
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);

                _remSumDet.Rem_ref_no = "";
                _remSumDet.Rem_rmk = "";

                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;

                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";

                #region Save_Adjustment_Recept

                objAdjReceipt = new Deposit_Bank_Pc_wise();
                double _adj_seqNo = CHNLSVC.Inventory.GetSerialID();
                objAdjReceipt.Seq_no = Convert.ToDouble(_adj_seqNo);
                objAdjReceipt.Company = BaseCls.GlbUserComCode;
                objAdjReceipt.Profit_center = TextBoxLocation.Text;
                objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
                objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
                objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
                objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
                objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
                if (rdoAdjPlus.Checked)
                {
                    objAdjReceipt.Adj_direct = true;
                }
                else
                {
                    objAdjReceipt.Adj_direct = false;
                }
                objAdjReceipt.Remark = txtRem.Text.Trim();
                objAdjReceipt.Createby = BaseCls.GlbUserID;
                objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                objAdjReceipt.Ref_lnk = recept_no;
                objAdjReceipt.Ref_seq = Convert.ToDecimal(_adj_seqNo);

                #endregion

                string RtnReceipt = "";

                eff = CHNLSVC.Sales.Insert_CheqRetn_with_Adj(recieptHeadder, objAdjReceipt, null, _remSumDet, out RtnReceipt);

                if (eff > 0)
                {
                    MessageBox.Show("Updated Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


            #endregion


            #region return cheque
            if (cmbRecType.Text == "RETURN CHEQUE" && _efect_rem == 1) //return cheq
            {
                List<RecieptItem> _recieptItemList = new List<RecieptItem>();
                RecieptItem _recieptItem = new RecieptItem();
                _recieptItem.Sard_pay_tp = "CASH";
                _recieptItem.Sard_cc_expiry_dt = DateTime.Now.Date;
                _recieptItem.Sard_settle_amt = Convert.ToDecimal(txtAmt.Text);
                _recieptItem.Sard_cc_is_promo = false;
                _recieptItem.Sard_cc_period = 0;
                _recieptItem.Sard_anal_3 = 0;
                _recieptItem.Sard_anal_4 = 0;
                _recieptItem.Sard_line_no = 1;
                _recieptItemList.Add(_recieptItem);



                //get reciept no
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = TextBoxLocation.Text;//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_cate_tp = TextBoxLocation.Text;//"PC";//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_start_char = "RCHQS";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RCHQS";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;//2012;                      

                string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);


                //insert reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                int seqNo = CHNLSVC.Inventory.GetSerialID();
                recieptHeadder.Sar_seq_no = seqNo;
                recieptHeadder.Sar_receipt_no = _cusNo;
                recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                recieptHeadder.Sar_manual_ref_no = txtRef.Text;
                recieptHeadder.Sar_ref_doc = txtRef.Text;
                recieptHeadder.Sar_receipt_type = "RCHQS";
                recieptHeadder.Sar_receipt_date = dtDate.Value.Date;//DateTime.Now;
                recieptHeadder.Sar_profit_center_cd = TextBoxLocation.Text; //GlbUserDefLoca.ToUpper();
                recieptHeadder.Sar_debtor_name = "CHEQUE";
                recieptHeadder.Sar_tot_settle_amt = Convert.ToDecimal(txtAmt.Text);//Convert.ToDecimal(TextBoxSAmo.Text);
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_create_when = Convert.ToDateTime(dtDate.Value.ToString());// DateTime.Now; 
                recieptHeadder.Sar_create_by = BaseCls.GlbUserID;//GlbUserName;
                recieptHeadder.Sar_comm_amt = 0;
                recieptHeadder.Sar_remarks = txtRem.Text;

                Int32 eff = 0;

                //return cheque update
                ChequeReturn chequeReturn = new ChequeReturn();
                chequeReturn.Seq = seqNo;
                chequeReturn.Pc = TextBoxLocation.Text;
                chequeReturn.Cheque_no = TextBoxChequeNumber.Text;
                chequeReturn.Company = BaseCls.GlbUserComCode;
                chequeReturn.Create_by = BaseCls.GlbUserID;
                chequeReturn.Bank = DropDownListBank.SelectedValue.ToString();

                //-----------------------------------------------------------
                Decimal bal = (Convert.ToDecimal(lblBal.Text) - Convert.ToDecimal(txtAmt.Text));
                if (bal <= 0)
                    chequeReturn.Is_set = true;
                //-----------------------------------------------------------

                Decimal _setCapital = 0;
                Decimal _intBal = 0;
                Decimal _setInt = 0;

                _intBal = Convert.ToDecimal(lblInt.Text) - Convert.ToDecimal(lblset_int.Text);
                if (_intBal > Convert.ToDecimal(txtAmt.Text))
                {
                    _setInt = Convert.ToDecimal(txtAmt.Text);
                    _setCapital = 0;
                }
                else
                {
                    _setInt = _intBal;
                    _setCapital = (Convert.ToDecimal(txtAmt.Text) - _intBal);
                }

                //chequeReturn.Intrest = _newCalInt;//Convert.ToDecimal(lblTotNewInt.Text);
                chequeReturn.Srcq_cap_set = _setCapital;
                chequeReturn.Srcq_intr_set = _setInt;

                chequeReturn.Create_Date = Convert.ToDateTime(dtDate.Value.ToString());
                chequeReturn.Settle_val = Convert.ToDecimal(txtAmt.Text);//Convert.ToDecimal(lblPayPaid.Text);

                #region remitance summary
                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, TextBoxLocation.Text, Convert.ToDateTime(dtDate.Text).Date);

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = TextBoxLocation.Text;
                _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Value).Date;
                _remSumDet.Rem_sec = "04";
                _remSumDet.Rem_cd = "009";
                _remSumDet.Rem_sh_desc = "Return Cheque Adjustment (HO)";
                _remSumDet.Rem_lg_desc = "Return Cheque Adjustment (HO)";
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_week = (_wkNo + "S").ToString();
                _remSumDet.Rem_ref_no = TextBoxChequeNumber.Text;
                _remSumDet.Rem_rmk = "";
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet.Rem_is_sos = false;
                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = false;
                _remSumDet.Rem_del_alw = false;
                _remSumDet.Rem_is_rem_sum = false;
                _remSumDet.Rem_cat = 12;
                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";
                _remSumDet.Rem_bnk_cd = "";
                _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
                //8/8/2013
                _remSumDet.REM_CHQNO = "";
                _remSumDet.REM_CHQ_BANK_CD = "";
                _remSumDet.REM_CHQ_BRANCH = "";
                _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                _remSumDet.REM_DEPOSIT_BANK_CD = "";
                _remSumDet.REM_DEPOSIT_BRANCH = "";
                #endregion

                #region Save_Adjustment_Recept

                objAdjReceipt = new Deposit_Bank_Pc_wise();
                //double _adj_seqNo = CHNLSVC.Inventory.GetSerialID();
                objAdjReceipt.Seq_no = Convert.ToDouble(seqNo);
                objAdjReceipt.Company = BaseCls.GlbUserComCode;
                objAdjReceipt.Profit_center = TextBoxLocation.Text;
                objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
                objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
                objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
                objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
                objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
                if (rdoAdjPlus.Checked)
                {
                    objAdjReceipt.Adj_direct = true;
                }
                else
                {
                    objAdjReceipt.Adj_direct = false;
                }
                objAdjReceipt.Remark = txtRem.Text.Trim();
                objAdjReceipt.Createby = BaseCls.GlbUserID;
                objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                objAdjReceipt.Ref_lnk = _cusNo;
                objAdjReceipt.Ref_seq = Convert.ToDecimal(seqNo);

                #endregion

                string RtnReceipt = "";
                eff = CHNLSVC.Financial.SaveReturnCheque_NEW(recieptHeadder, objAdjReceipt, _recieptItemList, chequeReturn, null, _remSumDet, out RtnReceipt);

                if (eff > 0)
                {
                    MessageBox.Show("Updated Successfully!\n\nReceipt No. =" + _cusNo, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (cmbRecType.Text == "RETURN CHEQUE" && _efect_rem == 0)
            {
                Int32 eff = 0;


                //update reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                recieptHeadder.Sar_mod_by = BaseCls.GlbUserID;
                recieptHeadder.Sar_used_amt = 0;
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;



                //Update Master Auto reciept no
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = TextBoxLocation.Text;//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_cate_tp = TextBoxLocation.Text;//"PC";//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_start_char = "RCHQS";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RCHQS";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;//2012;      


                //return cheque update
                ChequeReturn chequeReturn = new ChequeReturn();

                chequeReturn.Cheque_no = TextBoxChequeNumber.Text;
                chequeReturn.Company = BaseCls.GlbUserComCode;
                chequeReturn.Bank = DropDownListBank.SelectedValue.ToString();

                //-----------------------------------------------------------
                Decimal bal = (Convert.ToDecimal(lblBal.Text) - Convert.ToDecimal(txtAmt.Text));
                if (bal <= 0)
                    chequeReturn.Is_set = true;
                //-----------------------------------------------------------

                Decimal _setCapital = 0;
                Decimal _intBal = 0;
                Decimal _setInt = 0;

                _intBal = Convert.ToDecimal(lblInt.Text) - Convert.ToDecimal(lblset_int.Text);
                if (_intBal > Convert.ToDecimal(txtAmt.Text))
                {
                    _setInt = Convert.ToDecimal(txtAmt.Text);
                    _setCapital = 0;
                }
                else
                {
                    _setInt = _intBal;
                    _setCapital = (Convert.ToDecimal(txtAmt.Text) - _intBal);
                }

                //chequeReturn.Intrest = _newCalInt;//Convert.ToDecimal(lblTotNewInt.Text);
                chequeReturn.Srcq_cap_set = _setCapital;
                chequeReturn.Srcq_intr_set = _setInt;
                chequeReturn.Settle_val = Convert.ToDecimal(txtAmt.Text);//Convert.ToDecimal(lblPayPaid.Text);



                //RemitanceSummaryDetail update
                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = TextBoxLocation.Text;
                _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Value).Date;
                _remSumDet.Rem_sec = "04";
                _remSumDet.Rem_cd = "009";
                _remSumDet.Rem_lg_desc = "Return Cheque Adjustment (HO)";
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);

                _remSumDet.Rem_ref_no = TextBoxChequeNumber.Text;
                _remSumDet.Rem_rmk = "";

                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;

                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";



                #region Save_Adjustment_Recept

                objAdjReceipt = new Deposit_Bank_Pc_wise();
                double _adj_seqNo = CHNLSVC.Inventory.GetSerialID();
                objAdjReceipt.Seq_no = Convert.ToDouble(_adj_seqNo);
                objAdjReceipt.Company = BaseCls.GlbUserComCode;
                objAdjReceipt.Profit_center = TextBoxLocation.Text;
                objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
                objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
                objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
                objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
                objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
                if (rdoAdjPlus.Checked)
                {
                    objAdjReceipt.Adj_direct = true;
                }
                else
                {
                    objAdjReceipt.Adj_direct = false;
                }
                objAdjReceipt.Remark = txtRem.Text.Trim();
                objAdjReceipt.Createby = BaseCls.GlbUserID;
                objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                objAdjReceipt.Ref_lnk = txtRef.Text.Trim();
                objAdjReceipt.Ref_seq = Convert.ToDecimal(_adj_seqNo);

                #endregion

                string RtnReceipt = "";

                eff = CHNLSVC.Sales.Insert_CheqRetn_with_Adj(recieptHeadder, objAdjReceipt, chequeReturn, _remSumDet, out RtnReceipt);

                if (eff > 0)
                {
                    MessageBox.Show("Updated Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }


            #endregion

            #region credit sale settlement
            if (cmbRecType.Text == "CREDIT SALES" && cmbSubType.SelectedValue.ToString() == "AFSL")
            {
                saveAdjustment();
            }
            else if (cmbRecType.Text == "CREDIT SALES" && _efect_rem == 1)
            {
                SaveReceiptHeader();
            }
            else if (cmbRecType.Text == "CREDIT SALES" && _efect_rem == 0)
            {
                Int32 eff = 0;


                //update reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                recieptHeadder.Sar_mod_by = BaseCls.GlbUserID;
                recieptHeadder.Sar_used_amt = 0;
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;




                //Update Master Auto reciept no
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = TextBoxLocation.Text;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = txtDivision.Text.Trim();
                masterAuto.Aut_year = null;

                //RemitanceSummaryDetail update
                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = TextBoxLocation.Text;
                _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Value).Date;
                _remSumDet.Rem_sec = "04";
                _remSumDet.Rem_cd = "010";
                _remSumDet.Rem_lg_desc = "Credit Sale Adjustment (HO)";
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);

                _remSumDet.Rem_ref_no = "";
                _remSumDet.Rem_rmk = txtInvoice.Text;

                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;

                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";

                #region Save_Adjustment_Recept

                objAdjReceipt = new Deposit_Bank_Pc_wise();
                double _adj_seqNo = CHNLSVC.Inventory.GetSerialID();
                objAdjReceipt.Seq_no = Convert.ToDouble(_adj_seqNo);
                objAdjReceipt.Company = BaseCls.GlbUserComCode;
                objAdjReceipt.Profit_center = TextBoxLocation.Text;
                objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
                objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
                objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
                objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
                objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
                if (rdoAdjPlus.Checked)
                {
                    objAdjReceipt.Adj_direct = true;
                }
                else
                {
                    objAdjReceipt.Adj_direct = false;
                }
                objAdjReceipt.Remark = txtRem.Text.Trim();
                objAdjReceipt.Createby = BaseCls.GlbUserID;
                objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                objAdjReceipt.Ref_lnk = txtInvoice.Text.Trim();

                objAdjReceipt.Ref_seq = Convert.ToDecimal(_adj_seqNo);

                #endregion


                string _error = "";
                CHNLSVC.Sales.UpdateToSat_hdr(Convert.ToDecimal(txtAmt.Text.Trim()), txtInvoice.Text.Trim(), out _error);

                string RtnReceipt = "";


                eff = CHNLSVC.Sales.Insert_CheqRetn_with_Adj(recieptHeadder, objAdjReceipt, null, _remSumDet, out RtnReceipt);

                if (eff > 0)
                {
                    MessageBox.Show("Updated Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }






            }
            #endregion

            clear();
            cmbRecType.SelectedIndex = -1;
            cmbSubType.DataSource = null;
            txtCreditAcc.Text = "";
            txtDebitAcc.Text = "";
            _efect_rem = 0;
            DropDownListBank.SelectedIndex = -1;
            txtAmt.Text = "0.00";
            txtRem.Text = "";
        }

        private void saveAdjustment()
        {
            objAdjReceipt = new Deposit_Bank_Pc_wise();

            objAdjReceipt.Seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);

            objAdjReceipt.Company = BaseCls.GlbUserComCode;
            objAdjReceipt.Profit_center = TextBoxLocation.Text;
            objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
            objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
            objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
            objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
            objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
            if (rdoAdjPlus.Checked)
            {
                objAdjReceipt.Adj_direct = true;
            }
            else
            {
                objAdjReceipt.Adj_direct = false;
            }
            objAdjReceipt.Remark = txtRem.Text.Trim();
            objAdjReceipt.Createby = BaseCls.GlbUserID;
            objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
            objAdjReceipt.Ref_lnk = txtInvoice.Text.Trim();
            objAdjReceipt.Ref_seq = Convert.ToDecimal(objAdjReceipt.Seq_no);


            string hh = "";
            CHNLSVC.Sales.InsertTo_sat_Adj(objAdjReceipt, out hh);

            //}
            MessageBox.Show("Updated Successfully!","Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SaveReceiptHeader()
        {
            if (string.IsNullOrEmpty(txtDivision.Text))
            {
                MessageBox.Show("Please enter devision !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDivision.Focus();
                return;
            }
            Int32 row_aff = 0;
            string _msg = string.Empty;

            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
            int _seq = _ReceiptHeader.Sar_seq_no;
            _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
            _ReceiptHeader.Sar_receipt_type = "DEBT";
            _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();// txtRecNo.Text.Trim();
            _ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
            _ReceiptHeader.Sar_manual_ref_no = "";
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(dtDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = TextBoxLocation.Text;
            _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = lblAName.Text;
            _ReceiptHeader.Sar_debtor_add_1 = lblAAddress1.Text;
            _ReceiptHeader.Sar_debtor_add_2 = "";
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = lblMobile.Text;
            _ReceiptHeader.Sar_nic_no = lblNIC.Text;
            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(txtAmt.Text);
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = txtRem.Text;
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
            _ReceiptHeader.Sar_anal_8 = 0;
            _ReceiptHeader.Sar_anal_4 = "";
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;


            List<RecieptItem> _recieptItemList = new List<RecieptItem>();
            RecieptItem _recieptItem = new RecieptItem();
            _recieptItem.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
            _recieptItem.Sard_pay_tp = "CASH";
            _recieptItem.Sard_cc_expiry_dt = DateTime.Now.Date;
            _recieptItem.Sard_settle_amt = Convert.ToDecimal(txtAmt.Text);
            _recieptItem.Sard_cc_is_promo = false;
            _recieptItem.Sard_cc_period = 0;
            _recieptItem.Sard_anal_3 = 0;
            _recieptItem.Sard_anal_4 = 0;
            _recieptItem.Sard_line_no = 1;
            _recieptItem.Sard_inv_no = txtInvoice.Text;
            _recieptItemList.Add(_recieptItem);

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = TextBoxLocation.Text;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = txtDivision.Text.Trim();
            masterAuto.Aut_year = null;

            DataTable _pcInfo = new DataTable();
            _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, TextBoxLocation.Text);


            MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
            masterAutoRecTp.Aut_cate_cd = TextBoxLocation.Text;
            masterAutoRecTp.Aut_cate_tp = "PC";
            masterAutoRecTp.Aut_direction = null;
            masterAutoRecTp.Aut_modify_dt = null;

            if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && BaseCls.GlbUserComCode == "LRP")
            {
                masterAutoRecTp.Aut_moduleid = "REC_LRP";
            }
            else
            {
                masterAutoRecTp.Aut_moduleid = "RECEIPT";
            }
            masterAutoRecTp.Aut_number = 5;//what is Aut_number
            masterAutoRecTp.Aut_start_char = "DEBT";
            masterAutoRecTp.Aut_year = null;

            string QTNum;

            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _recieptItemList, masterAuto, null, null, null, null, null, null, masterAutoRecTp, null, out QTNum);

            if (row_aff == 1)
            {
                //if (Convert.ToDecimal(txtBalance.Text) == Convert.ToDecimal(txtAmt.Text))
                //{
                #region remitance summary
                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, TextBoxLocation.Text, Convert.ToDateTime(dtDate.Text).Date);

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = TextBoxLocation.Text;
                _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Value).Date;
                _remSumDet.Rem_sec = "04";
                _remSumDet.Rem_cd = "010";
                _remSumDet.Rem_sh_desc = "Credit Sale Adjustment (HO)";
                _remSumDet.Rem_lg_desc = "Credit Sale Adjustment (HO)";
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_week = (_wkNo + "S").ToString();
                _remSumDet.Rem_ref_no = QTNum;
                _remSumDet.Rem_rmk = txtInvoice.Text;
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet.Rem_is_sos = false;
                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = false;
                _remSumDet.Rem_del_alw = false;
                _remSumDet.Rem_is_rem_sum = false;
                _remSumDet.Rem_cat = 12;
                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = txtInvoice.Text;
                _remSumDet.Rem_bnk_cd = "";
                _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
                //8/8/2013
                _remSumDet.REM_CHQNO = "";
                _remSumDet.REM_CHQ_BANK_CD = "";
                _remSumDet.REM_CHQ_BRANCH = "";
                _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                _remSumDet.REM_DEPOSIT_BANK_CD = "";
                _remSumDet.REM_DEPOSIT_BRANCH = "";



                objAdjReceipt = new Deposit_Bank_Pc_wise();

                objAdjReceipt.Seq_no = Convert.ToDouble(_seq);
                objAdjReceipt.Company = BaseCls.GlbUserComCode;
                objAdjReceipt.Profit_center = TextBoxLocation.Text;
                objAdjReceipt.Adj_Date = Convert.ToDateTime(dtDate.Value).Date;
                objAdjReceipt.Adj_Type = cmbRecType.SelectedValue.ToString();
                objAdjReceipt.Adj_Sub_Type = cmbSubType.SelectedValue.ToString();
                objAdjReceipt.Adj_crd = txtCreditAcc.Text.Trim();
                objAdjReceipt.Adj_dbt = txtDebitAcc.Text.Trim();
                if (rdoAdjPlus.Checked)
                {
                    objAdjReceipt.Adj_direct = true;
                }
                else
                {
                    objAdjReceipt.Adj_direct = false;
                }
                objAdjReceipt.Remark = txtRem.Text.Trim();
                objAdjReceipt.Createby = BaseCls.GlbUserID;
                objAdjReceipt.Adj_amount = Convert.ToDecimal(txtAmt.Text);
                objAdjReceipt.Ref_lnk = txtInvoice.Text.Trim();
                objAdjReceipt.Ref_seq = Convert.ToDecimal(_seq);

                int _aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);

                string hh = "";
                CHNLSVC.Sales.InsertTo_sat_Adj(objAdjReceipt, out hh);


                #endregion
                //}
                MessageBox.Show("Updated Successfully!\n\nReceipt No. =" + QTNum, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear_Data();          
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(QTNum))
                {
                    MessageBox.Show(QTNum, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListBank.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Bank!");
                    return;
                }
                if (TextBoxLocation.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Profit center!");
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReturnCheque);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_return_cheque(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxChequeNumber;
                _CommonSearch.ShowDialog();
                txtAmt.Select();

                load_chq_details();
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

        private void load_chq_details()
        {
            try
            {
                DataTable dt = CHNLSVC.Financial.get_rtn_chq_byBankChqNo(DropDownListBank.SelectedValue.ToString(), TextBoxChequeNumber.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Decimal _val = Convert.ToDecimal(dr["srcq_act_val"]);
                            lblActVal.Text = _val.ToString("n");
                            _val = Convert.ToDecimal(dr["srcq_sys_val"]);
                            lblSysVal.Text = _val.ToString("n");
                            _val = Convert.ToDecimal(dr["srcq_intr"]);
                            lblInt.Text = _val.ToString("n");
                            _val = Convert.ToDecimal(dr["srcq_set_val"]);
                            lblSet.Text = _val.ToString("n");
                            Decimal _bal = Convert.ToDecimal(lblActVal.Text) + Convert.ToDecimal(lblInt.Text) - Convert.ToDecimal(lblSet.Text);
                            lblBal.Text = _bal.ToString("n");
                            txtRef.Text = dr["srcq_ref"].ToString();
                            lblRetDt.Text = dr["srcq_rtn_dt"].ToString();
                            lblset_int.Text = dr["srcq_intr_set"].ToString();
                            lblset_capital.Text = dr["srcq_cap_set"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Cheque Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clear_chq_det();
                        TextBoxChequeNumber.Focus();
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

        private void clear_srch_chq()
        {
            cmbRecType.SelectedIndex = -1;
            cmbSubType.SelectedIndex = -1;
            txtDebitAcc.Text = "";
            txtCreditAcc.Text = "";
            _efect_rem = 0;
            //cmbRecType.Text = "--Select Adjustment Type--";
            TextBoxChequeNumber.Text = "";
            txtRef.Text = "";
        }

        private void clear_chq_det()
        {
            lblSysVal.Text = "0.00";
            lblBal.Text = "0.00";
            lblInt.Text = "0.00";
            lblSet.Text = "0.00";
            lblActVal.Text = "0.00";
            lblRetDt.Text = "";
            lblset_int.Text = "0.00";
            lblset_capital.Text = "";
        }


        private void cmbRecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRecType.SelectedIndex != -1)
            {

                clear();

                Load_Sub_Types(cmbRecType.SelectedValue.ToString());
                txtCreditAcc.Text = "";
                txtDebitAcc.Text = "";
                _efect_rem = 0;
                dt_Acc = null;

                if (cmbRecType.Text == "ADVANCE RECEIPT")
                {
                    pnlAdvRec.Visible = true;
                    pnlCrdSale.Visible = false;
                    pnlRtnChq.Visible = false;
                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        txtDivision.Text = _RecDiv.Msrd_cd;
                    }
                    else
                    {
                        txtDivision.Text = "";
                    }
                }
                if (cmbRecType.Text == "RETURN CHEQUE")
                {
                    pnlRtnChq.Visible = true;
                    pnlAdvRec.Visible = false;
                    pnlCrdSale.Visible = false;
                }
                if (cmbRecType.Text == "CREDIT SALES")
                {
                    pnlCrdSale.Visible = true;
                    pnlRtnChq.Visible = false;
                    pnlAdvRec.Visible = false;

                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        txtDivision.Text = _RecDiv.Msrd_cd;
                    }
                    else
                    {
                        txtDivision.Text = "";
                    }
                }

                //Add by Akila 2017/01/24
                if (cmbRecType.Text == "CREDIT NOTE")
                {
                    pnlRtnChq.Visible = false;
                    pnlAdvRec.Visible = false;
                    pnlCrdSale.Visible = false;
                    pnlFooterControls.Visible = false;
                    pnlCreditNoteControls.Visible = true;
                    btnActive.Visible = true;
                    btnSave.Visible = false;
                }
            }

        }

        private void TextBoxChequeNumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxChequeNumber.Text))
                load_chq_details();
        }

        private void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxLocation;
                _CommonSearch.ShowDialog();
                TextBoxLocation.Select();

                clear();
                DropDownListBank.SelectedIndex = -1;

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

        private void TextBoxLocation_Leave(object sender, EventArgs e)
        {
            clear();
            DropDownListBank.SelectedIndex = -1;
            Load_Adj_Types();

            if (!string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TextBoxLocation.Focus();
                    return;
                }
            }
        }

        private void TextBoxChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAmt.Focus();
        }

        private void btn_srch_inv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                MessageBox.Show("Please select customer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCusCode.Focus();
                return;
            }

            DataTable _result = null;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInv);
            _result = CHNLSVC.CommonSearch.GetOutstandingInvoice(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInvoice;
            _CommonSearch.ShowDialog();
            txtAmt.Focus();

            txtBalance.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmt(BaseCls.GlbUserComCode, TextBoxLocation.Text, txtCusCode.Text, txtInvoice.Text)).ToString("n");
            

        }

        private void LoadCustomerDetails()
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                    lblAName.Text = _masterBusinessCompany.Mbe_name;
                    lblAAddress1.Text = _masterBusinessCompany.Mbe_add1 + " " + _masterBusinessCompany.Mbe_add2;
                    lblNIC.Text = _masterBusinessCompany.Mbe_nic;
                    lblMobile.Text = _masterBusinessCompany.Mbe_mob;

                }
                else
                {
                    MessageBox.Show("Invalid customer.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCusCode.Text = "";
                    lblAName.Text = "";
                    lblAAddress1.Text = "";
                    lblNIC.Text = "";
                    lblMobile.Text = "";
                    txtCusCode.Focus();
                    return;
                }
            }
            else
            {
                lblAName.Text = "";
                lblAAddress1.Text = "";
                lblNIC.Text = "";
                lblMobile.Text = "";
            }
        }

        private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbRecType.Focus();
        }

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_inv_Click(null, null);
        }

        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            txtInvVal.Text = "0.00";
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                txtBalance.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmt(BaseCls.GlbUserComCode, TextBoxLocation.Text, txtCusCode.Text, txtInvoice.Text)).ToString("n");
                
                DataTable _dtInv = CHNLSVC.Sales.GetSalesHdr(txtInvoice.Text);
                if (_dtInv.Rows.Count > 0)
                    txtInvVal.Text = (Convert.ToDecimal(_dtInv.Rows[0]["Sah_anal_7"]) - Convert.ToDecimal(_dtInv.Rows[0]["sah_pdi_req"])).ToString("n");
            }
        }

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusCode.Text))
                LoadCustomerDetails();
        }

        private void btn_srch_cust_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCusCode;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtInvoice.Focus();

            LoadCustomerDetails();
        }

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_cust_Click(null, null);
        }

        private void btn_srch_div_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Division);
            DataTable _result = CHNLSVC.CommonSearch.GetDivision(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDivision;
            _CommonSearch.ShowDialog();
            txtDivision.Select();
        }

        private void txtDivision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_div_Click(null, null);
        }

        private void btn_srch_adv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.satReceiptByAnal3);
                DataTable _result = CHNLSVC.CommonSearch.SearchReceiptByAnal3(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtAdvRecNo;
                _CommonSearch.ShowDialog();
                txtAmt.Focus();

                load_adv_rec_det();

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

        private void txtAdvRecNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_adv_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtAmt.Focus();
        }

        private void txtAdvRecNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAdvRecNo.Text))
                load_adv_rec_det();
        }

        private void load_adv_rec_det()
        {
            try
            {
                //RecieptHeader _ReceiptHeader = new RecieptHeader();
                DataTable _ReceiptHeader = CHNLSVC.Sales.GetReceiptByAnal3(txtAdvRecNo.Text, TextBoxLocation.Text, BaseCls.GlbUserComCode);
                if (_ReceiptHeader != null)
                {
                    Decimal _val = 0;
                    recept_no = _ReceiptHeader.Rows[0]["SAR_RECEIPT_NO"].ToString();

                    _val = Convert.ToDecimal(_ReceiptHeader.Rows[0]["Sar_tot_settle_amt"]);
                    lblAdvRecAmt.Text = _val.ToString("n");

                    _val = Convert.ToDecimal(_ReceiptHeader.Rows[0]["Sar_used_amt"]);
                    lblUsedAmt.Text = _val.ToString("n");

                    _val = (Convert.ToDecimal(lblAdvRecAmt.Text) - Convert.ToDecimal(lblUsedAmt.Text));
                    lblAdvBal.Text = _val.ToString("n");

                    lblAdvName.Text = _ReceiptHeader.Rows[0]["Sar_debtor_name"].ToString();
                    lblManRef.Text = _ReceiptHeader.Rows[0]["Sar_manual_ref_no"].ToString();
                    lblAdvAddr.Text = _ReceiptHeader.Rows[0]["Sar_debtor_add_1"].ToString() + " " + _ReceiptHeader.Rows[0]["Sar_debtor_add_2"].ToString();
                    txtRecSbTp.Text = _ReceiptHeader.Rows[0]["SAR_SUBREC_TP"].ToString();

                    List<ReceiptItemDetails> _tmpRecItem = new List<ReceiptItemDetails>();
                    _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(txtAdvRecNo.Text);

                    if (_tmpRecItem != null)
                    {
                        foreach (ReceiptItemDetails ser in _tmpRecItem)
                        {
                            lblItem.Text = ser.Sari_item;
                            lblDesc.Text = ser.Sari_item_desc;
                            lblModel.Text = ser.Sari_model;
                            lblSerial.Text = ser.Sari_serial;
                        }
                    }
                }
                else
                {
                    lblItem.Text = "";
                    lblDesc.Text = "";
                    lblModel.Text = "";
                    lblSerial.Text = "";
                    lblAdvRecAmt.Text = "0.00";
                    lblUsedAmt.Text = "0.00";
                    lblAdvBal.Text = "0.00";
                    lblAdvName.Text = "";
                    lblManRef.Text = "";
                    lblAdvAddr.Text = "";
                    txtRecSbTp.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Receipt Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                lblItem.Text = "";
                lblDesc.Text = "";
                lblModel.Text = "";
                lblSerial.Text = "";
                lblAdvRecAmt.Text = "0.00";
                lblUsedAmt.Text = "0.00";
                lblAdvBal.Text = "0.00";
                lblAdvName.Text = "";
                lblManRef.Text = "";
                lblAdvAddr.Text = "";
                txtRecSbTp.Text = "";

            }
            finally
            {
            }
        }

        private void DropDownListBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxChequeNumber.Text = "";
        }

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRem.Focus();
        }

        private void txtRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        private void lblDebit_Click(object sender, EventArgs e)
        {

        }

        private void AdjustmentReceipt_Load(object sender, EventArgs e)
        {
            Load_Adj_Types();
            txtCreditAcc.Text = "";
            txtDebitAcc.Text = "";
            _efect_rem = 0;
        }

        private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubType.SelectedIndex != -1)
                {
                    Load_AccountDetails(cmbRecType.SelectedValue.ToString(), cmbSubType.SelectedValue.ToString());
                }
                rdoAdjPlus.Checked = true;
                if (cmbSubType.SelectedValue.ToString() == "AFSL")
                {
                    pnlAFSL.Visible = true;
                    rdoAdjMinus.Enabled = true;
                    rdoAdjPlus.Enabled = true;
                }
                else
                {
                    rdoAdjMinus.Checked = true;
                    pnlAFSL.Visible = false;
                    rdoAdjMinus.Enabled = false;
                    rdoAdjPlus.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbRecType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void cmbSubType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPer_TextChanged_1(object sender, EventArgs e)
        {
            decimal _amt = 0;
            decimal _DP = 0;
            decimal _adj = 0;

            txtDP.Text = "0.00";
            txtAdj.Text = "0.00";
            txtNewInvVal.Text = "0.00";
            txtLease.Text = "0.00";


            if (!string.IsNullOrEmpty(txtPer.Text))
            {
                if (!IsNumeric(txtPer.Text))
                {
                    MessageBox.Show("Invalid Percentage value", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPer.Text = "";
                    txtPer.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtPer.Text) > 100)
                {
                    MessageBox.Show("Invalid Percentage value", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPer.Text = "";
                    txtPer.Focus();
                    return;
                }
                _amt = Convert.ToDecimal(txtBalance.Text) * 100 / (100 - Convert.ToDecimal(txtPer.Text));
                _DP = _amt / 100 * Convert.ToDecimal(txtPer.Text);
                _adj = _DP - Convert.ToDecimal(txtInvVal.Text) + Convert.ToDecimal(txtBalance.Text);

                txtDP.Text = _DP.ToString("n");
                txtAdj.Text = _adj.ToString("n");
                txtNewInvVal.Text = _amt.ToString("n");
                txtLease.Text = (_amt - _DP).ToString("n");

            }

        }

        private void tabLayoutMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadCreditNotes()
        {
            try
            {
                if (string.IsNullOrEmpty(cmbRecType.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select the adjustment type", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbRecType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbSubType.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select the sub type", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSubType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxLocation.Text))
                {
                    MessageBox.Show("Please select the profit center", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxLocation.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNoteWithoutSRN);
                DataTable _result = CHNLSVC.CommonSearch.SearchCreditNotes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCreditNote;
                _CommonSearch.ShowDialog();
                txtCreditNote.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading credit notes" + Environment.NewLine + ex.Message, "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbRecType.Focus();
            }
            
        }

        private void btnSearchCRN_Click(object sender, EventArgs e)
        {
            LoadCreditNotes();
        }

        private void txtCreditNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchCRN_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                LoadCreditNoteInfo();
                btnActive.Focus();
            }
        }

        private void LoadCreditNoteInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(cmbRecType.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select the adjustment type", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbRecType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbSubType.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select the sub type", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSubType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxLocation.Text))
                {
                    MessageBox.Show("Please select the profit center", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxLocation.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCreditNote.Text))
                {
                    MessageBox.Show("Please enter credit note number", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCreditNote.Focus();
                    return;
                }

                DataTable _crnInfo = new DataTable();
                _crnInfo = CHNLSVC.Financial.GetCreditNoteInfo(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim().ToUpper(), txtCreditNote.Text.Trim().ToUpper());
                if (_crnInfo.Rows.Count > 0)
                {
                    foreach (DataRow _creditNote in _crnInfo.Rows)
                    {
                        lblCrnAmt.Text = FormatToCurrency( _creditNote["CREDIT AMOUNT"].ToString());
                        lblCrnUsedAmt.Text = FormatToCurrency( _creditNote["USED AMOUNT"].ToString());
                        lblCrnReduceAmt.Text = FormatToCurrency( _creditNote["REDUCE AMOUNT"].ToString());
                        lblCrnBalanceAmt.Text = FormatToCurrency( _creditNote["BALANCE"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Credit note information not found. Please enter a valid credit note number", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCreditNote.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading credit note details" + Environment.NewLine + ex.Message, "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCreditNote.Focus();
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void txtCreditNote_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCRN_Click(null, null);
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            //Activate credit note - Add By akila 2017/01/25

            try
            {
                //Validation
                if (string.IsNullOrEmpty(cmbRecType.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select the adjustment type", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbRecType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbSubType.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select the sub type", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSubType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxLocation.Text))
                {
                    MessageBox.Show("Please select the profit center", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxLocation.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCreditNote.Text))
                {
                    MessageBox.Show("Please enter credit note number", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCreditNote.Focus();
                    return;
                }

                //Activate credit note and get the result set
                CHNLSVC.Financial.ActivateCreditNote(txtCreditNote.Text.Trim().ToUpper(), "REACT", BaseCls.GlbUserID); // to activate REACT, to cancel CANCEL
                LoadCreditNoteInfo();

                if (double.Parse(lblCrnBalanceAmt.Text) > 0) 
                { 
                    MessageBox.Show("Credit Note # " + txtCreditNote.Text.ToUpper() + " has been activated successfully", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblCrnAmt.Text = "0.00";
                    lblCrnUsedAmt.Text = "0.00";
                    lblCrnReduceAmt.Text = "0.00";
                    lblCrnBalanceAmt.Text = "0.00";
                    txtCreditNote.Text = "";
                }
                else { MessageBox.Show("Credit Note # " + txtCreditNote.Text.ToUpper() + " couldn't activate", "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while activating credit note" + Environment.NewLine + ex.Message, "Receipt Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnActive.Focus();
                CHNLSVC.CloseChannel();
                return;
            }

            
        }

    }
}
