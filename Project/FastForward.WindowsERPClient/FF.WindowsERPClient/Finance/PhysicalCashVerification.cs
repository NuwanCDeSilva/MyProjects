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
    public partial class PhysicalCashVerification : Base
    {
        public PhysicalCashVerification()
        {
            InitializeComponent();
        }

        public enum CashTypes
        {

            CREDIT_CARD = 1,
            CHEQUE = 2,
            BANK_SLIP = 3,
            AD_REFUND = 4,
            GIFT_VOUCHER = 5,
            PETTY_CASH = 6,
            PEN_REM = 7,
            CASH_HAND = 8,
            EX_REM = 9,
            CR_NOTE=10
        }

        List<AuditAccountableCash> _creditCard = new List<AuditAccountableCash>();
        List<AuditAccountableCash> _cheque = new List<AuditAccountableCash>();
        List<AuditAccountableCash> _bankSlip = new List<AuditAccountableCash>();
        List<AuditAccountableCash> _advancedRec = new List<AuditAccountableCash>();
        List<AuditAccountableCash> _giftVoucegr = new List<AuditAccountableCash>();
        List<AuditAccountableCash> _pettyCash = new List<AuditAccountableCash>();
        List<AuditAccountableCash> _crnote = new List<AuditAccountableCash>();

        List<AuditCashVeriDetail> _auditDetails = new List<AuditCashVeriDetail>();
        AuditCashVeriMain _auditMain =null;

        bool isCCAdd = false;
        bool  isBSAdd=false;
        bool isAFAdd = false;
        bool isGVAdd = false;
        bool isPettAdd = false;
        bool isChqAdd = false;
        bool isCRNAdd = false;

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        #region key press events
        private void txt5000_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt2000_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt1000_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt500_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt100_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt50_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txt10_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion


        #region denomination calculation
        private void txt5000_Leave(object sender, EventArgs e)
        {
            if (txt5000.Text != "")
            {
                txt5000Tot.Text =FormatToCurrency((Convert.ToInt32(txt5000.Text) * 5000).ToString()); 
                CalculateDenominationTotal();
            }
        }

        private void txt2000_Leave(object sender, EventArgs e)
        {
            if (txt2000.Text != "")
            {
                txt2000Tot.Text = FormatToCurrency((Convert.ToInt32(txt2000.Text) * 2000).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt1000_Leave(object sender, EventArgs e)
        {
            if (txt1000.Text != "")
            {
                txt1000Tot.Text =FormatToCurrency( (Convert.ToInt32(txt1000.Text) * 1000).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt500_Leave(object sender, EventArgs e)
        {
            if (txt500.Text != "")
            {
                txt500Tot.Text =FormatToCurrency( (Convert.ToInt32(txt500.Text) * 500).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt100_Leave(object sender, EventArgs e)
        {
            if (txt100.Text != "")
            {
                txt100Tot.Text =FormatToCurrency( (Convert.ToInt32(txt100.Text) * 100).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt50_Leave(object sender, EventArgs e)
        {
            if (txt50.Text != "")
            {
                txt50Tot.Text =FormatToCurrency( (Convert.ToInt32(txt50.Text) * 50).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt20_Leave(object sender, EventArgs e)
        {
            if (txt20.Text != "")
            {
                txt20Tot.Text = FormatToCurrency((Convert.ToInt32(txt20.Text) * 20).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt10_Leave(object sender, EventArgs e)
        {
            if (txt10.Text != "")
            {
                txt10Tot.Text =FormatToCurrency( (Convert.ToInt32(txt10.Text) * 10).ToString()); ;
                CalculateDenominationTotal();
            }
        }

        private void txt1_Leave(object sender, EventArgs e)
        {
            if (txt1.Text != "")
            {
                txt1Tot.Text = FormatToCurrency((Convert.ToInt32(txt1.Text) * 1).ToString()); ;
                CalculateDenominationTotal();
            }
        }
        #endregion


        private void CalculateDenominationTotal() {
            try
            {
                decimal v5000;
                decimal v2000;
                decimal v1000;
                decimal v500;
                decimal v100;
                decimal v50;
                decimal v20;
                decimal v10;
                decimal v1;

                if (txt5000Tot.Text == "")
                {
                    v5000 = 0;
                }
                else
                {
                    v5000 = Convert.ToDecimal(txt5000Tot.Text);
                }
                if (txt2000Tot.Text == "")
                {
                    v2000 = 0;
                }
                else
                {
                    v2000 = Convert.ToDecimal(txt2000Tot.Text);
                }
                if (txt1000Tot.Text == "")
                {
                    v1000 = 0;
                }
                else
                {
                    v1000 = Convert.ToDecimal(txt1000Tot.Text);
                }
                if (txt500Tot.Text == "")
                {
                    v500 = 0;
                }
                else
                {
                    v500 = Convert.ToDecimal(txt500Tot.Text);
                }
                if (txt100Tot.Text == "")
                {
                    v100 = 0;
                }
                else
                {
                    v100 = Convert.ToDecimal(txt100Tot.Text);
                }
                if (txt50Tot.Text == "")
                {
                    v50 = 0;
                }
                else
                {
                    v50 = Convert.ToDecimal(txt50Tot.Text);
                }
                if (txt20Tot.Text == "")
                {
                    v20 = 0;
                }
                else
                {
                    v20 = Convert.ToDecimal(txt20Tot.Text);
                }
                if (txt10Tot.Text == "")
                {
                    v10 = 0;
                }
                else
                {
                    v10 = Convert.ToDecimal(txt10Tot.Text);
                }
                if (txt1Tot.Text == "")
                {
                    v1 = 0;
                }
                else
                {
                    v1 = Convert.ToDecimal(txt1Tot.Text);
                }

                txtDenominationTotal.Text = FormatToCurrency((v5000 + v2000 + v1000 + v500 + v100 + v50 + v20 + v10 + v1).ToString());
            }
            catch (Exception) { }
        
        }

        #region pop up show

        private void btnBS_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = true;
            pnlCheque.Visible = false;
            pnlCreditCard.Visible = false;
            pnlGiftVoucher.Visible = false;
            pnlPettyCash.Visible = false;
            pnlAdvRef.Visible = false;
            pnlCreditNote.Visible = false;
            labelPopUbHeader.Text = "Bank Slips";
            cmbBSType.SelectedIndex = 0;
            cmbBSType_SelectedIndexChanged(null, null);
        }

        private void btnCCSlips_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = false;
            pnlCheque.Visible = false;
            pnlCreditCard.Visible = true;
            pnlGiftVoucher.Visible = false;
            pnlPettyCash.Visible = false;
            pnlAdvRef.Visible = false;
            pnlCreditNote.Visible = false;

            labelPopUbHeader.Text = "Credit Card Slips";
        }

        private void btnChqs_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = false;
            pnlCheque.Visible = true;
            pnlCreditCard.Visible = false;
            pnlGiftVoucher.Visible = false;
            pnlPettyCash.Visible = false;
            pnlAdvRef.Visible = false;
            pnlCreditNote.Visible = false;

            labelPopUbHeader.Text = "Cheques";
        }

        private void btnAdvancedRefund_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = false;
            pnlCheque.Visible = false;
            pnlCreditCard.Visible = false;
            pnlGiftVoucher.Visible = false;
            pnlPettyCash.Visible = false;
            pnlAdvRef.Visible = true;
            pnlCreditNote.Visible = false;

            labelPopUbHeader.Text = "Advanced Receipt Refund";
        }

        private void btnGV_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = false;
            pnlCheque.Visible = false;
            pnlCreditCard.Visible = false;
            pnlGiftVoucher.Visible = true;
            pnlPettyCash.Visible = false;
            pnlAdvRef.Visible = false;
            pnlCreditNote.Visible = false;

            labelPopUbHeader.Text = "Gift Voucher";
        }

        private void btnPtyExp_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = false;
            pnlCheque.Visible = false;
            pnlCreditCard.Visible = false;
            pnlGiftVoucher.Visible = false;
            pnlPettyCash.Visible = true;
            pnlAdvRef.Visible = false;
            pnlCreditNote.Visible = false;

            labelPopUbHeader.Text = "Petty Cash";
        }

        private void btnCreditNote_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
            pnlMain.Enabled = false;
            pnlBankSlip.Visible = false;
            pnlCheque.Visible = false;
            pnlCreditCard.Visible = false;
            pnlGiftVoucher.Visible = false;
            pnlPettyCash.Visible = false;
            pnlAdvRef.Visible = false;
            pnlCreditNote.Visible = true;

            labelPopUbHeader.Text = "Credit Note";
        }

        #endregion

        private void btnClsPopUp_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = false;
            pnlMain.Enabled = true;


            //update details
            if (isPettAdd) {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.PETTY_CASH.ToString());

                foreach (AuditAccountableCash _cash in _pettyCash) {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }
            }

            if (isChqAdd)
            {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.CHEQUE.ToString());

                foreach (AuditAccountableCash _cash in _cheque)
                {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }

            }

            if (isCCAdd)
            {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.CREDIT_CARD.ToString());

                foreach (AuditAccountableCash _cash in _creditCard)
                {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }

            }

            if (isAFAdd)
            {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.AD_REFUND.ToString());

                foreach (AuditAccountableCash _cash in _advancedRec)
                {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }

            }

            if (isBSAdd)
            {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.BANK_SLIP.ToString());

                foreach (AuditAccountableCash _cash in _bankSlip)
                {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }

            }

            if (isGVAdd)
            {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.GIFT_VOUCHER.ToString());

                foreach (AuditAccountableCash _cash in _giftVoucegr)
                {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }

            }

            if (isCRNAdd)
            {
                CHNLSVC.Sales.UpdateAuditAccountableCash(txtJobNo.Text, CashTypes.CR_NOTE.ToString());

                foreach (AuditAccountableCash _cash in _crnote)
                {
                    _cash.Aucc_job = _auditMain.Aucm_job; _cash.Aucc_seq = _auditMain.Aucm_seq; _cash.Aucc_is_act = true;
                    CHNLSVC.Sales.SaveAuditCashVerificationAccountableCash(_cash);
                }

            }

            isAFAdd = false;
            isBSAdd = false;
            isGVAdd = false;
            isCCAdd = false;
            isPettAdd = false;
            isChqAdd = false;
            isCRNAdd = false;

        }

        private void btnAddCC_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCCBank.Text == "") {
                    MessageBox.Show("Please select bank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCCRef.Text == "")
                {
                    MessageBox.Show("Please select CC Reference ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<AuditAccountableCash> _duplicate = (from _res in _creditCard
                                                         where _res.Aucc_ref == txtCCRef.Text && _res.Aucc_bank == txtCCBank.Text
                                                         select _res).ToList<AuditAccountableCash>();
                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Credit Card no already in  the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtCCDepDate.Value.Date;
                _cas.Aucc_type = CashTypes.CREDIT_CARD.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtCCAmo.Text);
                _cas.Aucc_bank = txtCCBank.Text;
                _cas.Aucc_ref = txtCCRef.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_cre_dt = DateTime.Now;
                _creditCard.Add(_cas);
                isCCAdd = true;
                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _creditCard;

                grvCreditCard.AutoGenerateColumns = false;
                grvCreditCard.DataSource = _bnding;

                txtCCSlips.Text =FormatToCurrency( (Convert.ToDecimal(txtCCSlips.Text) + Convert.ToDecimal(txtCCAmo.Text)).ToString());
                CalculateAccountableCashTotal();
                dtCCDepDate.Value = DateTime.Now;
                txtCCAmo.Text = "";
                txtCCBank.Text = "";
                txtCCRef.Text = "";

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

        private void CalculateAccountableCashTotal()
        {
            try
            {
                decimal cashHand = 0;
                decimal bankslip = 0;
                decimal ccslip = 0;
                decimal cheques = 0;
                decimal advancereciept = 0;
                decimal exrem = 0;
                decimal giftvoucher = 0;
                decimal pettycash = 0;
                decimal crnote = 0;
                decimal penrem = 0;
                /*txtCashInHand
                    txtBankSlips
                    txtBankSlips
                    txtCheques
                    txtAdvancedRefund
                    txtExRem
                    txtGiftVoucher
                    txtPettyCash
                    txtPenRem
                 */
                if (txtCashInHand.Text != "")
                {
                    cashHand = Convert.ToDecimal(txtCashInHand.Text);
                }
                else
                {
                    cashHand = 0;
                }
                if (txtBankSlips.Text != "")
                {
                    bankslip = Convert.ToDecimal(txtBankSlips.Text);
                }
                if (txtCCSlips.Text != "")
                {
                    ccslip = Convert.ToDecimal(txtCCSlips.Text);
                }
                else
                {
                    bankslip = 0;
                }
                if (txtCheques.Text != "")
                {
                    cheques = Convert.ToDecimal(txtCheques.Text);
                }
                else
                {
                    cheques = 0;
                }
                if (txtAdvancedRefund.Text != "")
                {
                    advancereciept = Convert.ToDecimal(txtAdvancedRefund.Text);
                }
                else
                {
                    advancereciept = 0;
                }
                if (txtExRem.Text != "")
                {
                    exrem = Convert.ToDecimal(txtExRem.Text);
                }
                else
                {
                    exrem = 0;
                }
                if (txtPettyCash.Text != "")
                {
                    pettycash = Convert.ToDecimal(txtPettyCash.Text);
                }
                else
                {
                    pettycash = 0;
                }
                if (txtGiftVoucher.Text != "")
                {
                    giftvoucher = Convert.ToDecimal(txtGiftVoucher.Text);
                }
                else
                {
                    giftvoucher = 0;
                }
                if (txtCreditNote.Text != "")
                {
                    crnote = Convert.ToDecimal(txtCreditNote.Text);
                }
                else
                {
                    crnote = 0;
                }
                if (txtPenRem.Text != "")
                {
                    penrem = Convert.ToDecimal(txtPenRem.Text);
                }
                else
                {
                    penrem = 0;
                }

                textBox10.Text = FormatToCurrency((cashHand + bankslip + ccslip + cheques + advancereciept + exrem + penrem + pettycash + giftvoucher + crnote).ToString());
            }
            catch (Exception) { }
        }

        private void btnAddBS_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBSBank.Text == "") {
                    MessageBox.Show("Please select Bank Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbBSType.Text == "Cheque")
                {
                    List<AuditAccountableCash> _duplicate = (from _res in _bankSlip
                                                             where _res.Aucc_ref == txtBSCheque.Text && _res.Aucc_bank == txtBSBank.Text
                                                             select _res).ToList<AuditAccountableCash>();
                    if (_duplicate != null && _duplicate.Count > 0)
                    {
                        MessageBox.Show("Bank slip no already in  the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtBankSlipDepDate.Value.Date;
                _cas.Aucc_type = CashTypes.BANK_SLIP.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtBSAmount.Text);
                _cas.Aucc_bank = txtBSBank.Text;
                _cas.Aucc_ref = txtBSCheque.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_rmk = cmbBSType.Text == "Cash" ? "Cash" : "Cheque";
                _cas.Aucc_cre_dt = DateTime.Now;
                _bankSlip.Add(_cas);
                isBSAdd = true;

                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _bankSlip;

                grvBabkSlip.AutoGenerateColumns = false;
                grvBabkSlip.DataSource = _bnding;

                txtBankSlips.Text =FormatToCurrency( (Convert.ToDecimal(txtBankSlips.Text) + Convert.ToDecimal(txtBSAmount.Text)).ToString());
                CalculateAccountableCashTotal();

                txtBSAmount.Text = "0";
                txtBSBank.Text = "";
                txtBSCheque.Text = "";
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

        private void btnAddChq_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCheBank.Text == "")
                {
                    MessageBox.Show("Please select bank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCheNo.Text == "")
                {
                    MessageBox.Show("Please select CC Reference ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<AuditAccountableCash> _duplicate = (from _res in _cheque
                                                         where _res.Aucc_ref == txtCheNo.Text && _res.Aucc_bank==txtCheBank.Text
                                                         select _res).ToList<AuditAccountableCash>();
                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Cheque no already in  the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtCheDepDate.Value.Date;
                _cas.Aucc_type = CashTypes.CHEQUE.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtCheAmo.Text);
                _cas.Aucc_bank = txtCheBank.Text;
                _cas.Aucc_ref = txtCheNo.Text;
                _cas.Aucc_customer = txtCheCustomer.Text;
                _cas.Aucc_rmk = txtCheAc.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_cre_dt = DateTime.Now;
                _cheque.Add(_cas);
                isChqAdd = true;
                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _cheque;

                grvCheque.AutoGenerateColumns = false;
                grvCheque.DataSource = _bnding;

                txtCheques.Text =FormatToCurrency( (Convert.ToDecimal(txtCheques.Text) + Convert.ToDecimal(txtCheAmo.Text)).ToString());
                CalculateAccountableCashTotal();

                txtCheAmo.Text = "0";
                txtCheBank.Text = "";
                txtCheNo.Text = "";
                txtCheAc.Text = "";
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

        private void btnAddAdvanRef_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtARNo.Text == "")
                {
                    MessageBox.Show("Please select advanced receipt no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                

                List<AuditAccountableCash> _duplicate = (from _res in _advancedRec
                                                         where _res.Aucc_ref == txtARNo.Text
                                                         select _res).ToList<AuditAccountableCash>();
                if (_duplicate != null && _duplicate.Count > 0) {
                    MessageBox.Show("Advanced Receipt no already in  the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtARDate.Value.Date;
                _cas.Aucc_type = CashTypes.AD_REFUND.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtARAmount.Text);
                _cas.Aucc_ref = txtARNo.Text;
                _cas.Aucc_rmk = txtARRemark.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_cre_dt = DateTime.Now;
                _advancedRec.Add(_cas);
                isAFAdd = true;
                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _advancedRec;

                grvAdvanRec.AutoGenerateColumns = false;
                grvAdvanRec.DataSource = _bnding;
                

                txtAdvancedRefund.Text =FormatToCurrency( (Convert.ToDecimal(txtAdvancedRefund.Text) + Convert.ToDecimal(txtARAmount.Text)).ToString());
                CalculateAccountableCashTotal();

                txtARAmount.Text = "0";
                txtARNo.Text = "";
                txtARRemark.Text = "";
                dtARDate.Value = DateTime.Now;
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

        private void btnAddGV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGVNo.Text == "")
                {
                    MessageBox.Show("Please select gift voucher no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<AuditAccountableCash> _duplicate = (from _res in _giftVoucegr
                                                         where _res.Aucc_ref == txtGVNo.Text
                                                         select _res).ToList<AuditAccountableCash>();
                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Gift Voucher no already in  the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtGvDate.Value.Date;
                _cas.Aucc_type = CashTypes.GIFT_VOUCHER.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtGVAmount.Text);
                _cas.Aucc_ref = txtGVNo.Text;
                _cas.Aucc_rmk = txtGVRmk.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_cre_dt = DateTime.Now;
                _giftVoucegr.Add(_cas);
                isGVAdd = true;
                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _advancedRec;

                grvGiftVoucher.AutoGenerateColumns = false;
                grvGiftVoucher.DataSource = _bnding;

                txtGiftVoucher.Text =FormatToCurrency( (Convert.ToDecimal(txtGiftVoucher.Text) + Convert.ToDecimal(txtGVAmount.Text)).ToString());
                CalculateAccountableCashTotal();

                txtGVAmount.Text = "0";
                txtGVNo.Text = "";
                txtGVRmk.Text = "";
                dtGvDate.Value = DateTime.Now;
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

        private void btnAddPetty_Click(object sender, EventArgs e)
        {
            try
            {

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtPettyDate.Value.Date;
                _cas.Aucc_type = CashTypes.PETTY_CASH.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtPettyAmount.Text);
                _cas.Aucc_ref = txtPettyDesc.Text;
                _cas.Aucc_rmk = txtPettyRmk.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_cre_dt = DateTime.Now;
                _pettyCash.Add(_cas);
                isPettAdd = true;
                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _pettyCash;

                grvPetty.AutoGenerateColumns = false;
                grvPetty.DataSource = _bnding;

                txtPettyCash.Text =FormatToCurrency( (Convert.ToDecimal(txtPettyCash.Text) + Convert.ToDecimal(txtPettyAmount.Text)).ToString());
                CalculateAccountableCashTotal();

                txtPettyAmount.Text = "0";
                txtPettyDesc.Text = "";
                txtPettyRmk.Text = "";
                dtPettyDate.Value = DateTime.Now;
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

        private void btnCCBank_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCCBank;
                _CommonSearch.ShowDialog();
                txtCCBank.Select();
              
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


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AuditCashVerify:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator+BaseCls.GlbUserDefProf+seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnCheBank_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCheBank;
                _CommonSearch.ShowDialog();
                txtCheBank.Select();

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

        private void btnBSBank_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBSBank;
                _CommonSearch.ShowDialog();
                txtBSBank.Select();

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

        private void txtBSAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvBabkSlip_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0) {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    txtBankSlips.Text =FormatToCurrency( (Convert.ToDecimal(txtBankSlips.Text) - _bankSlip[e.RowIndex].Aucc_amount).ToString());
                    _bankSlip.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _bankSlip;
                    isBSAdd = true;
                    grvBabkSlip.DataSource = _bnding;
                    CalculateAccountableCashTotal();
                }
            }
        }

        private void txtCheAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvCheque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtCheques.Text =FormatToCurrency( (Convert.ToDecimal(txtCheques.Text) - _cheque[e.RowIndex].Aucc_amount).ToString());
                    _cheque.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _cheque;
                    isChqAdd = true;
                    grvCheque.DataSource = _bnding;
                    CalculateAccountableCashTotal();
                }
            }
        }

        private void txtCCAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvCreditCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtCCSlips.Text =FormatToCurrency( (Convert.ToDecimal(txtCCSlips.Text) - _creditCard[e.RowIndex].Aucc_amount).ToString());
                    _creditCard.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _creditCard;
                    isCCAdd = true;
                    grvCreditCard.DataSource = _bnding;
                    CalculateAccountableCashTotal();
                }
            }
        }

        private void txtPettyAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvPetty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtPettyCash.Text =FormatToCurrency( (Convert.ToDecimal(txtPettyCash.Text) - _pettyCash[e.RowIndex].Aucc_amount).ToString());
                    _pettyCash.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _pettyCash;
                    isPettAdd = true;
                    grvPetty.DataSource = _bnding;
                    CalculateAccountableCashTotal();
                }
            }
        }

        private void txtGVAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvGiftVoucher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtGiftVoucher.Text =FormatToCurrency( (Convert.ToDecimal(txtGiftVoucher.Text) - _giftVoucegr[e.RowIndex].Aucc_amount).ToString());
                    _giftVoucegr.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _giftVoucegr;
                    isGVAdd = true;
                    grvGiftVoucher.DataSource = _bnding;
                    CalculateAccountableCashTotal();
                }
            }
        }

        private void txtARAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvAdvanRec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtAdvancedRefund.Text =FormatToCurrency( (Convert.ToDecimal(txtAdvancedRefund.Text) -_advancedRec[e.RowIndex].Aucc_amount).ToString());
                    _advancedRec.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _advancedRec;

                    grvAdvanRec.DataSource = _bnding;
                    isAFAdd = true;
                    CalculateAccountableCashTotal();
                }
            }
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PhysicalCashVerification formnew = new PhysicalCashVerification();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                

                #region validation

                if (_auditMain == null)
                {
                    MessageBox.Show("Job not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

               

                //fill accountable cash
                List<AuditAccountableCash> _tem = new List<AuditAccountableCash>();
                if (_creditCard != null && _creditCard.Count > 0)
                    _tem.AddRange(_creditCard);
                if (_cheque != null && _cheque.Count > 0)
                    _tem.AddRange(_cheque);
                if (_bankSlip != null && _bankSlip.Count > 0)
                    _tem.AddRange(_bankSlip);
                if (_advancedRec != null && _advancedRec.Count > 0)
                    _tem.AddRange(_advancedRec);
                if (_giftVoucegr != null && _giftVoucegr.Count > 0)
                    _tem.AddRange(_giftVoucegr);
                if (_pettyCash != null && _pettyCash.Count > 0)
                    _tem.AddRange(_pettyCash);
                if (_crnote != null && _crnote.Count > 0)
                    _tem.AddRange(_crnote);

                if (_tem.Count <= 0  && Convert.ToDecimal(txtCashInHand.Text)<0) {
                    MessageBox.Show("Accountable cash details not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _tem = new List<AuditAccountableCash>();
                btnSave.Enabled = false;
                //fill denomination table
                AuditCashVeriDenomination _deno = new AuditCashVeriDenomination();
                try
                {

                    _deno.Aucd_5000 = Convert.ToInt32(txt5000.Text);
                    _deno.Aucd_2000 = Convert.ToInt32(txt2000.Text);
                    _deno.Aucd_1000 = Convert.ToInt32(txt1000.Text);
                    _deno.Aucd_500 = Convert.ToInt32(txt500.Text);
                    _deno.Aucd_100 = Convert.ToInt32(txt100.Text);
                    _deno.Aucd_50 = Convert.ToInt32(txt50.Text);
                    _deno.Aucd_20 = Convert.ToInt32(txt20.Text);
                    _deno.Aucd_10 = Convert.ToInt32(txt10.Text);
                    _deno.Aucd_1 = Convert.ToInt32(txt1.Text);
                    _deno.Aucd_total = Convert.ToDecimal(txtDenominationTotal.Text);
                }
                catch (Exception ex)
                {
                    btnSave.Enabled = true;
                    return;
                }
                //add accountable cash(petty,ex rem,pen rem)
                try
                {
                    if (txtCashInHand.Text != "")
                    {
                        AuditAccountableCash _cash = new AuditAccountableCash();
                        _cash.Aucc_type = CashTypes.CASH_HAND.ToString();
                        _cash.Aucc_amount = Convert.ToDecimal(txtCashInHand.Text);
                        _cash.Aucc_cre_by = BaseCls.GlbUserID;
                        _cash.Aucc_cre_dt = DateTime.Now;
                        _tem.Add(_cash);
                    }

                    if (txtPenRem.Text != "" && txtPenRem.Text != "0")
                    {
                        AuditAccountableCash _cash = new AuditAccountableCash();
                        _cash.Aucc_type = CashTypes.PEN_REM.ToString();
                        _cash.Aucc_amount = Convert.ToDecimal(txtPenRem.Text);
                        _cash.Aucc_cre_by = BaseCls.GlbUserID;
                        _cash.Aucc_cre_dt = DateTime.Now;
                        _tem.Add(_cash);
                    }
                    if (txtExRem.Text != "" && txtExRem.Text != "0")
                    {
                        AuditAccountableCash _cash = new AuditAccountableCash();
                        _cash.Aucc_type = CashTypes.EX_REM.ToString();
                        _cash.Aucc_amount = Convert.ToDecimal(txtExRem.Text);
                        _cash.Aucc_cre_by = BaseCls.GlbUserID;
                        _cash.Aucc_cre_dt = DateTime.Now;
                        _tem.Add(_cash);
                    }
                    if (txtPettyCash.Text != "" && txtPettyCash.Text != "0")
                    {
                        AuditAccountableCash _cash = new AuditAccountableCash();
                        _cash.Aucc_type = CashTypes.PETTY_CASH.ToString();
                        _cash.Aucc_amount = Convert.ToDecimal(txtPettyAmount.Text);
                        _cash.Aucc_cre_by = BaseCls.GlbUserID;
                        _cash.Aucc_cre_dt = DateTime.Now;
                        _tem.Add(_cash);
                    }
                    if (txtCreditNote.Text != "" && txtCreditNote.Text != "0")
                    {
                        AuditAccountableCash _cash = new AuditAccountableCash();
                        _cash.Aucc_type = CashTypes.CR_NOTE.ToString();
                        _cash.Aucc_amount = Convert.ToDecimal(txtCreditNote.Text);
                        _cash.Aucc_cre_by = BaseCls.GlbUserID;
                        _cash.Aucc_cre_dt = DateTime.Now;
                        _tem.Add(_cash);
                    }
                    
                }
                catch (Exception ex) { }
                try
                {
                    _auditMain.Aucm_excess = Convert.ToDecimal(txtExcess.Text);
                    _auditMain.Ausm_rmk = txtRmk.Text;
                }
                catch (Exception) { }

                string _error = "";
                int _result = CHNLSVC.Sales.SavePhysicalCashVerification(_tem, _deno, _auditDetails, null, _auditMain, txtJobNo.Text, out _error);
                if (_result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = true;
                    return;
                }
                else {
                    MessageBox.Show("Successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    _view.GlbReportName = "Physical_Cash_Verify_Report.rpt";
                    BaseCls.GlbReportName = "Physical_Cash_Verify_Report.rpt";
                    BaseCls.GlbReportDoc = _auditMain.Aucm_job;
                    _view.Show();
                    _view = null;
                    btnSave.Enabled = true;
                    btnClear_Click(null, null);
                   
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnJobNo_Click(object sender, EventArgs e)
        {
            //get job no
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AuditCashVerify);
                DataTable _result = CHNLSVC.CommonSearch.GetAuditCashVerification(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJobNo ;
                _CommonSearch.ShowDialog();
                txtJobNo.Select();

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

        private void txtPenRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCashInHand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            //AuditCashVeriMain _main = CHNLSVC.Sales.GetJobFromDate(dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            //if (_main != null)
            //{
            //    txtJobNo.Text = _main.Aucm_job;
            //    _auditMain = _main;
            //}
            //else {
            //    txtJobNo.Text = "";
            //}
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            //AuditCashVeriMain _main = CHNLSVC.Sales.GetJobFromDate(dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            //if (_main != null)
            //{
            //    txtJobNo.Text = _main.Aucm_job;
            //    _auditMain = _main;
            //}
            //else
            //{
            //    txtJobNo.Text = "";
            //}
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtJobNo.Text == "")
                {
                    string _job = "";
                    string _error = "";
                    List<AuditCashVeriDetail> _detailsList;
                    MasterAutoNumber _auto = new MasterAutoNumber();
                    _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _auto.Aut_cate_tp = "PRO";
                    _auto.Aut_start_char = "JO";
                    _auto.Aut_moduleid = "JOB";
                    _auto.Aut_direction = 1;
                    _auto.Aut_year = null;
                    int result = CHNLSVC.Sales.ProcessPhysicalCashVerification(dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _auto, out _detailsList, BaseCls.GlbUserID, out _job, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        txtJobNo.Text = _job;
                        grvDetails.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _detailsList;

                        grvDetails.DataSource = _source;
                        _auditDetails = _detailsList;
                        if (_detailsList != null && _detailsList.Count > 0)
                        {
                            txtDocTotal.Text = FormatToCurrency((_detailsList.Sum(x => x.Aucd_total)).ToString());
                        }
                        txtJobNo_TextChanged(null, null);
                    }
                }
                //reprocess
                else
                {
                    if (MessageBox.Show("Do you want to Re process\nAdvanced Receipts and Collection receipts will re process", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    //reprocess
                    if (_auditMain != null)
                    {
                        List<AuditCashVeriDetail> _processDetails = CHNLSVC.Sales.GetAuditDetailsByJob(_auditMain.Aucm_job, _auditMain.Aucm_seq);
                        List<AuditCashVeriDetail> _detailsList;
                        string _error;
                        int result = CHNLSVC.Sales.ReProcessPhysicalCashVerification(dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out _detailsList,BaseCls.GlbUserID,_auditMain, out _error);
                        if (result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (_processDetails != null && _processDetails.Count > 0) {
                                _detailsList.AddRange(_processDetails);
                            }
                          
                            grvDetails.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = _detailsList;

                            grvDetails.DataSource = _source;
                            _auditDetails = _detailsList;

                            txtDocTotal.Text = (_detailsList.Sum(x => x.Aucd_total)).ToString();
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

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            //validate job no
            //kapila 23/2/2017
            decimal _val1 = 0;
            decimal _val2 = 0;

            CalculateAccountableCashTotal();

            if (!string.IsNullOrEmpty(textBox10.Text)) _val1 =Convert.ToDecimal(textBox10.Text);
            if(!string.IsNullOrEmpty(txtDocTotal.Text)) _val2=Convert.ToDecimal(txtDocTotal.Text);
            txtExcess.Text = FormatToCurrency((_val1 - _val2).ToString());
        }

        private void txtDenominationTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCashInHand.Text = txtDenominationTotal.Text;
                CalculateAccountableCashTotal();
                //textBox10.Text = txtDenominationTotal.Text;
                txtExcess.Text = FormatToCurrency((Convert.ToDecimal(textBox10.Text) - Convert.ToDecimal(txtDocTotal.Text)).ToString());
            }
            catch (Exception) { }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtExcess.Text = FormatToCurrency((Convert.ToDecimal(textBox10.Text) - (Convert.ToDecimal(txtDocTotal.Text))).ToString());
            }
            catch (Exception) { 
            
            }
        }

        private void txtCashInHand_TextChanged(object sender, EventArgs e)
        {
            if(txtCashInHand.Text!="")
            CalculateAccountableCashTotal();
        }

        private void txtExRem_TextChanged(object sender, EventArgs e)
        {
            if (txtExRem.Text != "")
            CalculateAccountableCashTotal();

            txtDenominationTotal.Text = txtCashInHand.Text;
        }

        private void txtPenRem_TextChanged(object sender, EventArgs e)
        {
            if (txtPenRem.Text != "")
            CalculateAccountableCashTotal();
        }

        private void txtJobNo_TextChanged(object sender, EventArgs e)
        {
            //AuditCashVeriMain _main = CHNLSVC.Sales.GetJobFromDate(dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            //if (_main != null)
            //{
            //    txtJobNo.Text = _main.Aucm_job;
            //    _auditMain = _main;
            //}
            //else
            //{
            //    txtJobNo.Text = "";
            //}

            AuditCashVeriMain _main = CHNLSVC.Sales.GetCashVerificationMain(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtJobNo.Text);
            if (_main != null) {
                txtJobNo.Text = _main.Aucm_job;
                _auditMain = _main;
                List<AuditCashVeriDetail> _details = CHNLSVC.Sales.GetCashVerificationDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtJobNo.Text);
                if (_details != null)
                {
                    LoadDetails(_details);
                    _auditDetails = _details;
                    txtDocTotal.Text = FormatToCurrency((_details.Sum(x => x.Aucd_total)).ToString());

                    //load accountable cash
                    List<AuditAccountableCash> _cashList = CHNLSVC.Sales.GetAccounableCash(_main.Aucm_seq, _main.Aucm_job);
                    if (_cashList != null)
                    {
                        List<AuditAccountableCash> _bsList = (from _res in _cashList
                                                              where _res.Aucc_type == CashTypes.BANK_SLIP.ToString()
                                                              select _res).ToList<AuditAccountableCash>();

                        _bankSlip = _bsList;
                        if (_bankSlip != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _bankSlip;

                            grvBabkSlip.AutoGenerateColumns = false;
                            grvBabkSlip.DataSource = _bnding;

                            txtBankSlips.Text = FormatToCurrency(_bankSlip.Select(x => x.Aucc_amount).Sum().ToString());
                        }

                        List<AuditAccountableCash> _cclist = (from _res in _cashList
                                                              where _res.Aucc_type == CashTypes.CREDIT_CARD.ToString()
                                                              select _res).ToList<AuditAccountableCash>();

                        _creditCard = _cclist;
                        if (_creditCard != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _creditCard;

                            grvCreditCard.AutoGenerateColumns = false;
                            grvCreditCard.DataSource = _bnding;

                            txtCCSlips.Text = FormatToCurrency(_creditCard.Select(x => x.Aucc_amount).Sum().ToString());
                        }


                        List<AuditAccountableCash> _gvList = (from _res in _cashList
                                                              where _res.Aucc_type == CashTypes.GIFT_VOUCHER.ToString()
                                                              select _res).ToList<AuditAccountableCash>();

                        _giftVoucegr = _gvList;
                        if (_giftVoucegr != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _giftVoucegr;

                            grvGiftVoucher.AutoGenerateColumns = false;
                            grvGiftVoucher.DataSource = _bnding;

                            txtGiftVoucher.Text = FormatToCurrency(_giftVoucegr.Select(x => x.Aucc_amount).Sum().ToString());
                        }

                        List<AuditAccountableCash> _chqList = (from _res in _cashList
                                                               where _res.Aucc_type == CashTypes.CHEQUE.ToString()
                                                               select _res).ToList<AuditAccountableCash>();

                        _cheque = _chqList;
                        if (_cheque != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _cheque;

                            grvCheque.AutoGenerateColumns = false;
                            grvCheque.DataSource = _bnding;

                            txtCheques.Text = FormatToCurrency(_cheque.Select(x => x.Aucc_amount).Sum().ToString());
                        }

                        List<AuditAccountableCash> _pettList = (from _res in _cashList
                                                                where _res.Aucc_type == CashTypes.PETTY_CASH.ToString()
                                                                select _res).ToList<AuditAccountableCash>();

                        _pettyCash = _pettList;
                        if (_pettyCash != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _pettyCash;

                            grvPetty.AutoGenerateColumns = false;
                            grvPetty.DataSource = _bnding;

                            txtPettyCash.Text = FormatToCurrency(_pettyCash.Select(x => x.Aucc_amount).Sum().ToString());
                        }


                        List<AuditAccountableCash> _crList = (from _res in _cashList
                                                              where _res.Aucc_type == CashTypes.CR_NOTE.ToString()
                                                              select _res).ToList<AuditAccountableCash>();

                        _crnote = _crList;
                        if (_crnote != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _crnote;

                            grvCrNote.AutoGenerateColumns = false;
                            grvCrNote.DataSource = _bnding;

                            txtCreditNote.Text = FormatToCurrency(_crnote.Select(x => x.Aucc_amount).Sum().ToString());
                        }


                        List<AuditAccountableCash> _afList = (from _res in _cashList
                                                              where _res.Aucc_type == CashTypes.AD_REFUND.ToString()
                                                              select _res).ToList<AuditAccountableCash>();

                        _advancedRec = _afList;
                        if (_pettyCash != null)
                        {
                            BindingSource _bnding = new BindingSource();
                            _bnding.DataSource = _advancedRec;

                            grvAdvanRec.AutoGenerateColumns = false;
                            grvAdvanRec.DataSource = _bnding;

                            txtAdvancedRefund.Text = FormatToCurrency(_advancedRec.Select(x => x.Aucc_amount).Sum().ToString());
                        }

                        CalculateAccountableCashTotal();
                    }
                }
                else {
                    txtDocTotal.Text = "0";
                    CalculateAccountableCashTotal();
                }

            }
            
        }

        private void LoadDetails(List<AuditCashVeriDetail> _details)
        {
            grvDetails.AutoGenerateColumns=false;
            BindingSource _sou = new BindingSource();
            _sou.DataSource = _details;
            grvDetails.DataSource = _sou;
        }

        private void cmbBSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBSType.Text == "Cash")
                txtBSCheque.Enabled = false;
            else
                txtBSCheque.Enabled = true;
        }

        private void btnCrNNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNoteAud);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNoteAll(_CommonSearch.SearchParams, null, null, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCrNNo;
                _CommonSearch.ShowDialog();
                txtCrNAmount.Text = _CommonSearch.GetResult(_CommonSearch.GlbSelectData, 3);
                txtCrNAmount.Text = FormatToCurrency(txtCrNAmount.Text);
                txtCrNNo.Select();                

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

        private void btn_CrNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCrNNo.Text == "")
                {
                    MessageBox.Show("Please select Credit Note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<AuditAccountableCash> _duplicate = (from _res in _crnote
                                                            where _res.Aucc_ref == txtCrNNo.Text
                                                            select _res).ToList<AuditAccountableCash>();
                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Credit Note no already in  the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               

                AuditAccountableCash _cas = new AuditAccountableCash();
                _cas.Aucc_date = dtpCrNDate.Value.Date;
                _cas.Aucc_type = CashTypes.CR_NOTE.ToString();
                _cas.Aucc_amount = Convert.ToDecimal(txtCrNAmount.Text);
                _cas.Aucc_ref = txtCrNNo.Text;
                _cas.Aucc_cre_by = BaseCls.GlbUserID;
                _cas.Aucc_rmk = txtCrNRmk.Text;
                _cas.Aucc_cre_dt = DateTime.Now;
                _crnote.Add(_cas);
                isCRNAdd = true;

                BindingSource _bnding = new BindingSource();
                _bnding.DataSource = _crnote;

                grvCrNote.AutoGenerateColumns = false;
                grvCrNote.DataSource = _bnding;

                txtCreditNote.Text = FormatToCurrency((Convert.ToDecimal(txtCreditNote.Text) + Convert.ToDecimal(txtCrNAmount.Text)).ToString());
                CalculateAccountableCashTotal();

                txtCrNAmount.Text = "0";
                txtCrNNo.Text = "";
                txtCrNRmk.Text = "";
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

        private void txtCrNNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void grvCrNote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtCreditNote.Text = FormatToCurrency((Convert.ToDecimal(txtCreditNote.Text) - _crnote[e.RowIndex].Aucc_amount).ToString());
                    _crnote.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _crnote;

                    grvCrNote.DataSource = _bnding;
                    isCRNAdd = true;
                    CalculateAccountableCashTotal();
                }
            }
        }
        

    }
}
