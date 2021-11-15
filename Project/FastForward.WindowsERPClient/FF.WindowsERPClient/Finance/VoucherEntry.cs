using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class VoucherEntry : Base
    {
        private List<VoucherDetails> VoucherDetails = new List<VoucherDetails>();
        private string EmpCode = "";
        private string Prepare = "";
        private string Accept = "";
        private string Autho = "";

        private List<InterVoucherDetails> oMainDetailList = new List<InterVoucherDetails>();
        private List<InterVoucherDetails> oFundTransferDetailList = new List<InterVoucherDetails>();
        private List<VoucherHeader> _voucherHeaderList = new List<VoucherHeader>();
        private bool isExcelUpload = false;

        private int LastDetailLine = 0;

        private bool mouseIsDown = false;
        private Point firstPoint;

        private string PC = "";
        private string PCD = "";
        private string ACC = "";
        private string ACCD = "";
        private string INVO = "";
        private string DESC = "";
        private string DEP = "";
        private string DEPD = "";
        private string AMNT = "";

        public VoucherEntry()
        {
            InitializeComponent();
        }

        private void VoucherEntry_Load(object sender, EventArgs e)
        {
            try
            {
                dtValid.Value = DateTime.Now.Date.AddDays(30).Date;

                UtilityClasses.NumberToWord _num = new UtilityClasses.NumberToWord();
                //MessageBox.Show(_num.GetWords("110101.20"));

                LoadExpense(cmbExpense1);
                LoadMinusExpense(cmbDeduct);
                LoadMinusExpense(cmbDeduct2);
                LoadMinusExpense(cmbDeduc3);
                LoadOthExpense(cmbRecoveries);
                LoadOthExpense(cmbRecoveries2);
                LoadOthExpense(cmbRecoveries3);
                cmbExpense1.SelectedIndex = -1;

                cmbDeduc3.SelectedIndex = -1;
                cmbDeduct2.SelectedIndex = -1;
                cmbDeduct.SelectedIndex = -1;

                cmbRecoveries3.SelectedIndex = -1;
                cmbRecoveries2.SelectedIndex = -1;
                cmbRecoveries.SelectedIndex = -1;

                cmbExpense1_SelectedIndexChanged(null, null);
                cmbDeduct_SelectedIndexChanged(null, null);
                cmbDeduct2_SelectedIndexChanged(null, null);
                cmbDeduc3_SelectedIndexChanged(null, null);

                cmbRecoveries_SelectedIndexChanged(null, null);
                cmbRecoveries2_SelectedIndexChanged(null, null);
                cmbRecoveries3_SelectedIndexChanged(null, null);

                dataGridView1.AutoGenerateColumns = false;
                dgvFTDetails.AutoGenerateColumns = false;

                pnlSummery.Visible = false;
                pnlFTSummery.Visible = false;

                ChangeExpire();
                calculateTotals();
                calculateFundTTotals();

                btnCEClear_Click(null, null);
                btnFTClear_Click(null, null);
                btnClearCp_Click(null, null);

                List<Hpr_SysParameter> param = CHNLSVC.Sales.GetAll_hpr_Para("INTVOU", "COM", BaseCls.GlbUserComCode);
                if (param != null && param.Count > 0)
                {
                    dateTimePickerEnd.Value = dateTimePickerBegin.Value.AddDays((int)param[0].Hsy_val);
                }
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerTransaction, lblError, string.Empty, out _allowCurrentTrans);
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
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(txtBankCode.Tag.ToString() + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 2)
                        {
                            paramsText.Append(txtFTBankCode.Tag.ToString() + seperator);
                            break;
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.JournalEntryAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + Convert.ToDateTime(dtpDate.Value.Date).Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ChequeVouchers:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "CHQ" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.FundTransferVouchers:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "FUND" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10091))
                {
                    MessageBox.Show("Sorry, You have no permission to asscess cheque voucher.\n( Advice: Required permission code :1009)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    return;
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                dateTimePickerFrom.Value = Convert.ToDateTime("01-01-2014");
            }
            else if (tabControl1.SelectedIndex == 2)
            {
            }
        }

        #region Voucher Entry

        private void LoadMinusExpense(ComboBox cmbExpense1)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            List<VoucherPrintExpenseDefinition> _vou = CHNLSVC.Financial.GetAllVoucherExpense(BaseCls.GlbUserComCode, _date.Date);

            List<VoucherPrintExpenseDefinition> _vou1 = (from _res in _vou
                                                         where _res.Gved_expe_from_dt <= dateTimePickerTransaction.Value.Date &&
                                                         _res.Gved_expe_to_dt >= dateTimePickerTransaction.Value.Date /* && _res.Gved_expe_cat == "MINUS"*/
                                                         select _res).ToList<VoucherPrintExpenseDefinition>();

            if (_vou1 != null && _vou1.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _vou1;
                cmbExpense1.DataSource = _source;
                cmbExpense1.DisplayMember = "Gved_expe_cd";
                cmbExpense1.ValueMember = "Gved_expe_cd";
            }
        }

        private void NumberToWord()
        {
            decimal val = 0;
            //foreach (VoucherDetails _exp in VoucherDetails)
            //{
            //    val = val + _exp.Givd_expe_val;
            //}
            if (!decimal.TryParse(txtTotal.Text, out val))
            {
                return;
            }
            UtilityClasses.NumberToWord _number = new UtilityClasses.NumberToWord();
            txtAmtInWord.Text = _number.GetWords(txtTotal.Text);
        }

        private void LoadExpense(ComboBox cmb)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            List<VoucherPrintExpenseDefinition> _vou = CHNLSVC.Financial.GetAllVoucherExpense(BaseCls.GlbUserComCode, _date.Date);

            List<VoucherPrintExpenseDefinition> _vou1 = (from _res in _vou
                                                         where _res.Gved_expe_from_dt <= dateTimePickerTransaction.Value.Date &&
                                                         _res.Gved_expe_to_dt >= dateTimePickerTransaction.Value.Date /*&& _res.Gved_stus == false*/
                                                         select _res).ToList<VoucherPrintExpenseDefinition>();

            if (_vou1 != null && _vou1.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _vou1;
                cmb.DataSource = _source;
                cmb.DisplayMember = "Gved_expe_cd";
                cmb.ValueMember = "Gved_expe_cd";
            }
        }

        private void LoadOthExpense(ComboBox cmb)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            List<VoucherPrintExpenseDefinition> _vou = CHNLSVC.Financial.GetAllVoucherExpense(BaseCls.GlbUserComCode, _date.Date);

            List<VoucherPrintExpenseDefinition> _vou1 = (from _res in _vou
                                                         where _res.Gved_expe_from_dt <= dateTimePickerTransaction.Value.Date &&
                                                         _res.Gved_expe_to_dt >= dateTimePickerTransaction.Value.Date /*&& _res.Gved_expe_cat == "MINUS"*/
                                                         select _res).ToList<VoucherPrintExpenseDefinition>();

            if (_vou1 != null && _vou1.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _vou1;
                cmb.DataSource = _source;
                cmb.DisplayMember = "Gved_expe_cd";
                cmb.ValueMember = "Gved_expe_cd";
            }
        }

        private void cmbExpense1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbExpense1.SelectedValue != null && cmbExpense1.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbExpense1.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        if (_def.Gved_expe_cat == "PLUS")
                        {
                            lblExpense1.Text = _def.Gved_expe_desc;
                            txtExpense1.Text = _def.Gved_expe_val.ToString();
                            NumberToWord();
                        }
                        else
                        {
                            lblExpense1.Text = _def.Gved_expe_desc;
                            txtExpense1.Text = (_def.Gved_expe_val).ToString();
                            NumberToWord();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10090))    //Suneth 17/09/2014
                {
                    MessageBox.Show("Sorry, You have no permission for saving the cash voucher!\n( Advice: Required permission code :10090)", "Cash Voucher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CalcTotal();
                NumberToWord();

                #region validation

                decimal val;
                decimal adjusmentPlus = 0;
                decimal adjusmentMinus = 0;
                decimal deduction = 0;
                decimal deduction2 = 0;
                decimal deduction3 = 0;
                decimal recoveries = 0;
                decimal recoveries2 = 0;
                decimal recoveries3 = 0;
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, string.Empty, dateTimePickerTransaction, lblError, dateTimePickerTransaction.Value.ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dateTimePickerTransaction.Value.Date != _date.Date)
                        {
                            MessageBox.Show("Back date not allow for selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        dateTimePickerTransaction.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dateTimePickerTransaction.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtChannel.Text))
                {
                    MessageBox.Show("Please select relevant channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtChannel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDept.Text))
                {
                    MessageBox.Show("Please select relevant department.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDept.Focus();
                    return;
                }

                if (!decimal.TryParse(txtDeduction2.Text, out val))
                {
                    MessageBox.Show("Adjustment + has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtDeduction3.Text, out val))
                {
                    MessageBox.Show("Adjustment - has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtRecoveries.Text, out val))
                {
                    MessageBox.Show("Recoveries has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtDeduction.Text, out val))
                {
                    MessageBox.Show("Deduction has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtTotal.Text, out val))
                {
                    MessageBox.Show("Total value has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (EmpCode == "" && txtPay.Text == "")
                {
                    MessageBox.Show("Please select Employee", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDecimal(txtTotal.Text) <= 0)
                {
                    MessageBox.Show("Total value has to be greater than 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateTimePickerBegin.Value > dateTimePickerEnd.Value)
                {
                    MessageBox.Show("Begin date has to be less than End date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //check prepare,accept,autho
                if (txtPrepare.Text == "" || txtAccept.Text == "" || txtAutho.Text == "")
                {
                    if (MessageBox.Show("Do you want to save without Prepare by and/or Accept by and/or Authorized by", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                #endregion validation

                try
                {
                    adjusmentPlus = Convert.ToDecimal(txtDeduction2.Text);
                    adjusmentMinus = Convert.ToDecimal(txtDeduction3.Text);
                    recoveries = Convert.ToDecimal(txtRecoveries.Text);
                    deduction = Convert.ToDecimal(txtDeduction.Text);
                    deduction2 = Convert.ToDecimal(txtDeduction2.Text);
                    deduction3 = Convert.ToDecimal(txtDeduction3.Text);
                    recoveries2 = Convert.ToDecimal(txtRecoveries2.Text);
                    recoveries3 = Convert.ToDecimal(txtRecoveries3.Text);

                    VoucherHeader _vouHdr = new VoucherHeader();
                    _vouHdr.Givh_claim_dt = _date.Date;
                    _vouHdr.Givh_claim_pc = txtProfitCenter.Text;
                    _vouHdr.Givh_claim_stus = 0;
                    _vouHdr.Givh_com = BaseCls.GlbUserComCode;
                    _vouHdr.Givh_cre_by = BaseCls.GlbUserDeptID;
                    _vouHdr.Givh_cre_dt = _date.Date;
                    _vouHdr.Givh_dt = _date.Date;
                    _vouHdr.Givh_print_stus = 0;
                    _vouHdr.Givh_valid_from = dateTimePickerBegin.Value.Date;
                    _vouHdr.Givh_valid_to = dateTimePickerEnd.Value.Date;
                    if (EmpCode != "")
                        _vouHdr.Givh_emp_cd = EmpCode;
                    _vouHdr.Givd_emp_name = txtPay.Text;
                    _vouHdr.Givd_emp_cat = txtEmpCate.Text;
                    _vouHdr.Givh_val = Convert.ToDecimal(txtTotal.Text);
                    _vouHdr.Givh_act_stus = 1;
                    _vouHdr.Givh_prep_by = txtPrepare.Text;
                    _vouHdr.Givd_acpt_by = txtAccept.Text;
                    _vouHdr.Givd_auth_by = txtAutho.Text;
                    _vouHdr.Givh_val_txt = txtAmtInWord.Text;
                    _vouHdr.Givd_chnl = txtChannel.Text.Trim();
                    _vouHdr.Givd_dept = txtDept.Text.Trim();

                    //load voucher details
                    int line = 1;
                    List<VoucherDetails> _voucherDetails = new List<VoucherDetails>();
                    foreach (VoucherDetails _vou in VoucherDetails)
                    {
                        _vou.Givd_line = line++;

                        _voucherDetails.Add(_vou);
                    }
                    /*
                    if (adjusmentPlus > 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbDeduct2.SelectedValue.ToString(); //"RECOVERY";
                        _vouDetail.Givd_expe_desc = "DEDUCT  "+lblDeduct2.Text; //"RECOVERIES";
                        _vouDetail.Givd_expe_val = adjusmentPlus;
                        _vouDetail.Givd_line = line++;
                        _vouDetail.Givd_expe_direct = true;
                        ///\_vouDetail.Gvid_dt = dateTimePickerAdjPlus.Value.Date;
                        _voucherDetails.Add(_vouDetail);
                    }
                    if (adjusmentMinus > 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbDeduc3.SelectedValue.ToString(); //"RECOVERY";
                        _vouDetail.Givd_expe_desc ="DEDUCT  " +lblDeduct3.Text; //"RECOVERIES";
                        _vouDetail.Givd_expe_val = adjusmentMinus;
                        _vouDetail.Givd_expe_direct = true;
                        _vouDetail.Givd_line = line++;
                        _vouDetail.Gvid_dt = dateTimePickerAdjMinus.Value.Date;
                        _voucherDetails.Add(_vouDetail);
                    }
                     */
                    if (recoveries != 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbRecoveries.SelectedValue.ToString(); //"RECOVERY";
                        _vouDetail.Givd_expe_desc = "REFUND  " + lblRecoveries.Text; //"RECOVERIES";
                        _vouDetail.Givd_expe_direct = false;
                        _vouDetail.Givd_expe_val = recoveries;
                        _vouDetail.Givd_line = line++;
                        _voucherDetails.Add(_vouDetail);
                    }
                    if (recoveries2 != 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbRecoveries2.SelectedValue.ToString(); //"RECOVERY";
                        _vouDetail.Givd_expe_desc = "REFUND  " + lblRecoveries2.Text; //"RECOVERIES";
                        _vouDetail.Givd_expe_val = recoveries2;
                        _vouDetail.Givd_expe_direct = false;
                        _vouDetail.Givd_line = line++;
                        _voucherDetails.Add(_vouDetail);
                    }
                    if (recoveries3 != 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbRecoveries3.SelectedValue.ToString(); //"RECOVERY";
                        _vouDetail.Givd_expe_desc = "REFUND  " + lblRecoveries3.Text; //"RECOVERIES";
                        _vouDetail.Givd_expe_val = recoveries3;
                        _vouDetail.Givd_expe_direct = false;
                        _vouDetail.Givd_line = line++;
                        _voucherDetails.Add(_vouDetail);
                    }
                    if (deduction != 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbDeduct.SelectedValue.ToString(); //"DEDUCTION";
                        _vouDetail.Givd_expe_desc = "DEDUCT  " + lblDeduct.Text; //"DEDUCTION";
                        _vouDetail.Givd_expe_direct = true;
                        _vouDetail.Givd_expe_val = deduction;
                        _vouDetail.Givd_line = line++;
                        _voucherDetails.Add(_vouDetail);
                    }
                    if (deduction2 != 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbDeduct2.SelectedValue.ToString(); //"DEDUCTION";
                        _vouDetail.Givd_expe_desc = "DEDUCT  " + lblDeduct2.Text; //"DEDUCTION";
                        _vouDetail.Givd_expe_val = deduction2;
                        _vouDetail.Givd_expe_direct = true;
                        _vouDetail.Givd_line = line++;
                        _voucherDetails.Add(_vouDetail);
                    }
                    if (deduction3 != 0)
                    {
                        VoucherDetails _vouDetail = new VoucherDetails();
                        _vouDetail.Givd_expe_cd = cmbDeduc3.SelectedValue.ToString(); //"DEDUCTION";
                        _vouDetail.Givd_expe_desc = "DEDUCT  " + lblDeduct3.Text; //"DEDUCTION";
                        _vouDetail.Givd_expe_val = deduction3;
                        _vouDetail.Givd_expe_direct = true;
                        _vouDetail.Givd_line = line++;
                        _voucherDetails.Add(_vouDetail);
                    }

                    MasterAutoNumber _vouAuto = new MasterAutoNumber();
                    _vouAuto.Aut_cate_tp = "PC";
                    _vouAuto.Aut_direction = 1;
                    _vouAuto.Aut_modify_dt = null;
                    _vouAuto.Aut_number = 0;

                    //if (BaseCls.GlbUserComCode == "AAL")        //kapila 7/6/2014
                    //{
                    //    _vouAuto.Aut_moduleid = "AAVOU";
                    //    _vouAuto.Aut_cate_cd = "AAVOU";
                    //    _vouAuto.Aut_start_char = "AAVOU";
                    //}
                    //else
                    //{
                    //    _vouAuto.Aut_moduleid = "VOU";
                    //    _vouAuto.Aut_cate_cd = "VOU";
                    //    _vouAuto.Aut_start_char = "VOU";
                    //}

                    _vouAuto.Aut_moduleid = BaseCls.GlbUserComCode + "VOU";
                    _vouAuto.Aut_cate_cd = BaseCls.GlbUserComCode + "VOU";
                    _vouAuto.Aut_start_char = BaseCls.GlbUserComCode + "VOU";
                    _vouAuto.Aut_year = null;

                    string doc_no = "";
                    int result = CHNLSVC.Financial.SaveVoucher(_vouHdr, _voucherDetails, _vouAuto, out doc_no);
                    if (result > 0)
                    {
                        MessageBox.Show("Record insert successfully\nDocument No - " + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (MessageBox.Show("Are you want to print", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                            BaseCls.GlbReportName = "VoucherPrints.rpt";
                            BaseCls.GlbReportDoc = doc_no;
                            _view.Show();
                            _view = null;

                            VoucherHeader _voucher = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, doc_no);
                            if (_voucher != null)
                            {
                                result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, doc_no, 1, 1, BaseCls.GlbUserID, _date);
                            }
                        }
                        Clear();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Nothing Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CHNLSVC.CloseChannel();
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

        private void txtExpense1_Leave(object sender, EventArgs e)
        {
            try
            {
                NumberToWord();
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

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {
                decimal val;
                if (cmbExpense1.SelectedValue == null)
                    return;
                if (!decimal.TryParse(txtExpense1.Text, out val))
                    return;

                var _duplicate = (from _res in VoucherDetails
                                  where _res.Givd_expe_cd == cmbExpense1.SelectedValue.ToString()
                                  select _res).ToList<VoucherDetails>();

                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Can not insert duplicate values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                VoucherDetails _det = new VoucherDetails();
                _det.Givd_expe_cd = cmbExpense1.SelectedValue.ToString();
                _det.Givd_expe_desc = lblExpense1.Text;
                _det.Givd_expe_val = Convert.ToDecimal(txtExpense1.Text);
                VoucherDetails.Add(_det);

                grvDetails.AutoGenerateColumns = false;
                BindingSource _sourec = new BindingSource();
                _sourec.DataSource = VoucherDetails;
                grvDetails.DataSource = _sourec;

                CalcTotal();
                NumberToWord();
                txtDeduction2.Focus();
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

        private void grvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        VoucherDetails.RemoveAt(e.RowIndex);

                        BindingSource _source = new BindingSource();
                        _source.DataSource = VoucherDetails;
                        grvDetails.DataSource = _source;
                        NumberToWord();
                        CalcTotal();
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

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPay;
                _CommonSearch.ShowDialog();
                if (txtPay.Text != "")
                {
                    EmpCode = txtPay.Text;
                    DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtPay.Text);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        txtPay.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString();
                        MasterUserCategory cat = CHNLSVC.General.GetUserCatByCode(_dt.Rows[0]["ESEP_CAT_CD"].ToString());
                        if (cat != null)
                            txtEmpCate.Text = cat.Mec_desc;
                    }
                    else
                    {
                        txtPay.Text = "";
                    }
                }
                txtPay.Select();
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

        private void CalcTotal()
        {
            decimal val;
            //if (!decimal.TryParse(txtDeduction2.Text,out val)) {
            //    return;
            //}
            //if (!decimal.TryParse(txtDeduction3.Text, out val)) {
            //    return;
            //}
            //if (!decimal.TryParse(txtRecoveries.Text, out val)) {
            //    return;
            //}
            decimal total = 0;

            List<VoucherDetails> _expense = (from _exp in VoucherDetails
                                             where _exp.Givd_expe_cd != "ADJ -" && _exp.Givd_expe_cd != "ADJ +" && _exp.Givd_expe_cd != "DEDUCTION" && _exp.Givd_expe_cd != "RECOVERY"
                                             select _exp).ToList<VoucherDetails>();
            if (_expense != null && _expense.Count > 0)
            {
                foreach (VoucherDetails _det in _expense)
                {
                    total = total + _det.Givd_expe_val;
                }
            }

            total = total - (Convert.ToDecimal(txtDeduction2.Text) + Convert.ToDecimal(txtDeduction3.Text) + Convert.ToDecimal(txtDeduction.Text)) + (Convert.ToDecimal(txtRecoveries.Text) + Convert.ToDecimal(txtRecoveries2.Text) + Convert.ToDecimal(txtRecoveries3.Text));
            txtTotal.Text = total.ToString();
        }

        private void txtAdjusmentPlus_Leave(object sender, EventArgs e)
        {
            try
            {
                CalcTotal();
                NumberToWord();
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

        private void txtAdjusmentMinus_Leave(object sender, EventArgs e)
        {
            try
            {
                CalcTotal();
                NumberToWord();
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

        private void txtDeduction_Leave(object sender, EventArgs e)
        {
            try
            {
                //txtDeduction.Text = (Convert.ToDecimal(txtDeduction.Text) * -1).ToString();
                CalcTotal();
                NumberToWord();
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

        private void txtRecoveries_Leave(object sender, EventArgs e)
        {
            try
            {
                //txtRecoveries.Text = (Convert.ToDecimal(txtRecoveries.Text) * -1).ToString();
                CalcTotal();
                NumberToWord();
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
            try
            {
                Clear();
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

        private void Clear()
        {
            txtDeduction3.Text = "0";
            txtDeduction2.Text = "0";
            txtAmtInWord.Text = "";
            txtDeduction.Text = "0";
            //txtExpense1.Text = "0";
            txtPay.Text = "";
            txtRecoveries.Text = "0";
            txtRef.Text = "";
            txtTotal.Text = "0";
            //lblExpense1.Text = "";
            txtPrepare.Text = "";
            txtAccept.Text = "";
            txtAutho.Text = "";
            txtEmpCate.Text = "";
            grvDetails.DataSource = null;
            VoucherDetails = new List<VoucherDetails>();
            EmpCode = "";
            btnSave.Enabled = true;
            btnCancel.Enabled = false;
            btnPrint.Enabled = false;
            Prepare = "";
            Accept = "";
            Autho = "";
            txtChannel.Text = "";
            txtDept.Text = "";
            List<Hpr_SysParameter> param = CHNLSVC.Sales.GetAll_hpr_Para("INTVOU", "COM", BaseCls.GlbUserComCode);
            if (param != null && param.Count > 0)
            {
                dateTimePickerEnd.Value = dateTimePickerBegin.Value.AddDays((int)param[0].Hsy_val);
            }
            ChangeExpire();
            VoucherDetails = new List<VoucherDetails>();
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VoucherNo);
                DataTable _result = CHNLSVC.CommonSearch.GetVoucheNos(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRef;
                _CommonSearch.ShowDialog();
                txtRef.Select();
                if (txtRef.Text != "")
                    LoadVoucher();
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

        private void LoadVoucher()
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = true;
            btnPrint.Enabled = true;
            VoucherDetails = new List<BusinessObjects.VoucherDetails>();
            VoucherHeader _vou = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, txtRef.Text);
            if (_vou != null)
            {
                txtTotal.Text = _vou.Givh_val.ToString();
                txtAmtInWord.Text = _vou.Givh_val_txt;
                dateTimePickerBegin.Value = _vou.Givh_valid_from;
                dateTimePickerEnd.Value = _vou.Givh_valid_to;
                txtPrepare.Text = _vou.Givh_prep_by;
                txtAutho.Text = _vou.Givd_auth_by;
                txtAccept.Text = _vou.Givd_acpt_by;
                txtAmtInWord.Text = _vou.Givh_val_txt;
                txtPay.Text = _vou.Givd_emp_name;
                txtEmpCate.Text = _vou.Givd_emp_cat;
                dtValid.Value = _vou.Givh_end_dt;
                if (string.IsNullOrEmpty(_vou.Givd_chnl))
                {
                    txtChannel.Text = "";
                }
                else
                {
                    txtChannel.Text = _vou.Givd_chnl;
                }

                if (string.IsNullOrEmpty(_vou.Givd_dept))
                {
                    txtDept.Text = "";
                }
                else
                {
                    txtDept.Text = _vou.Givd_dept;
                }
                //txtProfitCenter.Text = _vou.Givh_claim_pc;

                //get details
                List<VoucherDetails> _details = CHNLSVC.Financial.GetVoucherDetail(txtRef.Text);
                VoucherDetails.AddRange(_details);

                List<VoucherDetails> _expense = (from _exp in _details
                                                 where _exp.Givd_expe_cd != "ADJ -" && _exp.Givd_expe_cd != "ADJ +" && _exp.Givd_expe_cd != "DEDUCTION" && _exp.Givd_expe_cd != "RECOVERY"
                                                 select _exp).ToList<VoucherDetails>();
                if (_expense != null && _expense.Count > 0)
                {
                    grvDetails.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _expense;
                    grvDetails.DataSource = _source;
                }

                //load adjustment +
                List<VoucherDetails> _adjPlus = (from _exp in _details
                                                 where _exp.Givd_expe_cd == "ADJ +"
                                                 select _exp).ToList<VoucherDetails>();

                if (_adjPlus != null && _adjPlus.Count > 0)
                {
                    txtDeduction2.Text = _adjPlus[0].Givd_expe_val.ToString();
                    dateTimePickerAdjPlus.Value = _adjPlus[0].Gvid_dt;
                }

                //load adjustment -
                List<VoucherDetails> _adjMinus = (from _exp in _details
                                                  where _exp.Givd_expe_cd == "ADJ -"
                                                  select _exp).ToList<VoucherDetails>();

                if (_adjMinus != null && _adjMinus.Count > 0)
                {
                    txtDeduction3.Text = _adjMinus[0].Givd_expe_val.ToString();
                    dateTimePickerAdjMinus.Value = _adjMinus[0].Gvid_dt;
                }

                //load recoveries
                List<VoucherDetails> _recovery = (from _exp in _details
                                                  where _exp.Givd_expe_cd == "RECOVERY"
                                                  select _exp).ToList<VoucherDetails>();

                if (_recovery != null && _recovery.Count > 0)
                {
                    txtRecoveries.Text = _recovery[0].Givd_expe_val.ToString();
                }

                //load deduction
                List<VoucherDetails> _deduction = (from _exp in _details
                                                   where _exp.Givd_expe_cd == "DEDUCTION"
                                                   select _exp).ToList<VoucherDetails>();

                if (_deduction != null && _deduction.Count > 0)
                {
                    txtDeduction.Text = _deduction[0].Givd_expe_val.ToString();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10090))    //Suneth 17/09/2014
                {
                    MessageBox.Show("Sorry, You have no permission for cancel the cash voucher!\n( Advice: Required permission code :10090)", "Cash Voucher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtRef.Text != "")
                {
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    VoucherHeader _voucher = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, txtRef.Text);
                    if (_voucher != null && _voucher.Givh_act_stus == 0)
                    {
                        MessageBox.Show("Can not cancel canceled Vouchers", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, txtRef.Text, 0, 0, BaseCls.GlbUserID, _date);
                        if (result > 0)
                        {
                            MessageBox.Show("Cancelled Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                        }
                        else
                            MessageBox.Show("Cancel unsuccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnPrepare_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPrepare;
                _CommonSearch.ShowDialog();
                if (txtPrepare.Text != "")
                {
                    Prepare = txtPrepare.Text;
                    DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtPrepare.Text);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        txtPrepare.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString();
                    }
                    else
                        txtPrepare.Text = "";
                }
                txtPrepare.Select();
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

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccept;
                _CommonSearch.ShowDialog();
                if (txtAccept.Text != "")
                {
                    Accept = txtAccept.Text;
                    DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtAccept.Text);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        txtAccept.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString();
                    }
                    else
                    {
                        txtAccept.Text = "";
                    }
                }
                txtAccept.Select();
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

        private void btnAutho_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAutho;
                _CommonSearch.ShowDialog();
                if (txtAutho.Text != "")
                {
                    Autho = txtAutho.Text;
                    DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtAutho.Text);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        txtAutho.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString();
                    }
                    else
                        txtAutho.Text = "";
                }
                txtAutho.Select();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRef_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRef.Text != "")
                {
                    LoadVoucher();
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

        private void txtProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtExpense1.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnPC_Click(null, null);
        }

        private void txtExpense1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddExpense.Focus();
            }
        }

        private void txtAdjusmentPlus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDeduction3.Focus();
            }
        }

        private void txtAdjusmentMinus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDeduction.Focus();
            }
        }

        private void txtDeduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRecoveries.Focus();
            }
        }

        private void txtRecoveries_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtRecoveries2.Focus();
        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnVouNo_Click(null, null);
        }

        private void txtRef_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnVouNo_Click(null, null);
        }

        private void txtProfitCenter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPC_Click(null, null);
        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtProfitCenter.Focus();
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

        private void txtPay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnEmployee_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtEmpCate.Focus();
            }
        }

        private void txtAdjusmentPlus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdjusmentMinus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDeduction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRecoveries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void ChangeExpire()
        {
            if ((dateTimePickerEnd.Value - dateTimePickerBegin.Value).Days > 0)
            {
                lblExpire.Text = "This voucher will expire within " + (dateTimePickerEnd.Value - dateTimePickerBegin.Value).Days + " days from the date of issue";
            }
            else
            {
                lblExpire.Text = "This voucher will expire within 0 days from the date of issue";
            }
        }

        private void dateTimePickerBegin_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeExpire();
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

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeExpire();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You want to Quit", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtPrepare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtAccept.Focus();
        }

        private void txtAccept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtAutho.Focus();
        }

        private void txtAutho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        private void txtExpense1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRef.Text != "")
                {
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    VoucherHeader _voucher = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, txtRef.Text);
                    if (_voucher != null && _voucher.Givh_print_stus == 1)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10082))    //kapila 7/6/2014
                        {
                            //MessageBox.Show("Can not print previously printed Vouchers", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Sorry, You have no permission for re-print the cash voucher!\n( Advice: Required permission code :10082)", "Cash Voucher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportName = "VoucherPrints.rpt";
                        BaseCls.GlbReportDoc = _voucher.Givh_vou_no;
                        _view.Show();
                        _view = null;

                        if (_voucher != null)
                        {
                            int result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, _voucher.Givh_vou_no, 1, 1, BaseCls.GlbUserID, _date);
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

        private void txtRef_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbDeduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeduct.SelectedValue != null && cmbDeduct.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbDeduct.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        lblDeduct.Text = _def.Gved_expe_desc;
                        txtDeduction.Text = (_def.Gved_expe_val).ToString();
                        CalcTotal();
                        //NumberToWord();
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

        private void cmbRecoveries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRecoveries.SelectedValue != null && cmbRecoveries.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbRecoveries.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        lblRecoveries.Text = _def.Gved_expe_desc;
                        txtRecoveries.Text = (_def.Gved_expe_val).ToString();
                        CalcTotal();
                        //NumberToWord();
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

        private void cmbDeduct2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeduct2.SelectedValue != null && cmbDeduct2.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbDeduct2.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        lblDeduct2.Text = _def.Gved_expe_desc;
                        txtDeduction2.Text = (_def.Gved_expe_val).ToString();
                        CalcTotal();
                        //NumberToWord();
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

        private void cmbDeduc3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeduc3.SelectedValue != null && cmbDeduc3.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbDeduc3.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        lblDeduct3.Text = _def.Gved_expe_desc;
                        txtDeduction3.Text = (_def.Gved_expe_val).ToString();
                        CalcTotal();
                        //NumberToWord();
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

        private void cmbRecoveries2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRecoveries2.SelectedValue != null && cmbRecoveries2.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbRecoveries2.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        lblRecoveries2.Text = _def.Gved_expe_desc;
                        txtRecoveries2.Text = (_def.Gved_expe_val).ToString();
                        CalcTotal();
                        //NumberToWord();
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

        private void cmbRecoveries3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRecoveries3.SelectedValue != null && cmbRecoveries3.SelectedValue.ToString() != "")
                {
                    //get definition
                    VoucherPrintExpenseDefinition _def = CHNLSVC.Financial.GetVoucherExpense(BaseCls.GlbUserComCode, cmbRecoveries3.SelectedValue.ToString());
                    //load values
                    if (_def != null)
                    {
                        lblRecoveries3.Text = _def.Gved_expe_desc;
                        txtRecoveries3.Text = (_def.Gved_expe_val).ToString();
                        CalcTotal();
                        //NumberToWord();
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

        private void txtRecoveries2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtRecoveries3.Focus();
        }

        private void txtRecoveries2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRecoveries2_Leave(object sender, EventArgs e)
        {
            try
            {
                //txtDeduction.Text = (Convert.ToDecimal(txtDeduction.Text) * -1).ToString();
                CalcTotal();
                NumberToWord();
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

        private void txtRecoveries3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtPrepare.Focus();
        }

        private void txtRecoveries3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRecoveries3_Leave(object sender, EventArgs e)
        {
            try
            {
                //txtDeduction.Text = (Convert.ToDecimal(txtDeduction.Text) * -1).ToString();
                CalcTotal();
                NumberToWord();
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

        private void btnSearchChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChannel;
                _CommonSearch.ShowDialog();
                txtChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChannel;
                _CommonSearch.ShowDialog();
                txtChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtChannel_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtChannel;
                    _CommonSearch.ShowDialog();
                    txtChannel.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchDept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDept;
                _CommonSearch.ShowDialog();
                txtDept.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDept_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                    DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtDept;
                    _CommonSearch.ShowDialog();
                    txtDept.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    cmbExpense1.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDept_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDept;
                _CommonSearch.ShowDialog();
                txtDept.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validatePayUser()
        {
            if (!string.IsNullOrEmpty(txtPay.Text))
            {
                if (CHNLSVC.Financial.ValidateVoucherUser(txtPay.Text, dateTimePickerBegin.Value))
                {
                    MessageBox.Show("This user has a record to this date range.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion Voucher Entry

        #region Journal Entry

        #region Events

        private void btnPrintJE_Click(object sender, EventArgs e)
        {
        }

        private void btnSaveJE_Click(object sender, EventArgs e)
        {
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtPC.Select();
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

        private void txtAccountCode_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
            _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);
            _result.Columns.Remove("type");
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountCode;
            _CommonSearch.ShowDialog();
            txtAccountCode.Select();
            Cursor = Cursors.Default;
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtAccountCode.Focus();
            }
            else if (e.KeyCode == Keys.F3)
            {
                txtPC.Text = PC;
                txtPCDesc.Text = PCD;
                txtAccountCode.Text = ACC;
                txtAccountDesc.Text = ACCD;
                txtInvoiceNo.Text = INVO;
                txtDescription.Text = DESC;
                txtDeptCode.Text = DEP;
                txtDeptName.Text = DEPD;
                txtAmount.Text = AMNT;
            }
        }

        private void txtAccountCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAccountCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtInvoiceNo.Focus();
            }
        }

        private void txtInvoiceNo_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPC.Text))
            {
                MessageBox.Show("Please select a profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
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

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtInvoiceNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDescription.Focus();
            }
        }

        private void txtDeptCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDeptCode;
                _CommonSearch.ShowDialog();
                txtDeptCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDeptCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtAmount.Focus();
            }
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPC.Text))
            {
                if (!CHNLSVC.General.CheckProfitCenter(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper()))
                {
                    MessageBox.Show("Please check the profit center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPC.Clear();
                    txtPCDesc.Clear();
                    txtPC.Focus();
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                txtPCDesc.Text = FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(75, txtPC.Text.ToUpper()));
            }
        }

        private void txtAccountNo_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();
        }

        private void txtAccountCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountCode.Text.Trim()))
            {
                DataTable _result = new DataTable();
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
                _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);
                Cursor = Cursors.Default;
                if (_result.Rows.Count > 0)
                {
                    DataTable Filterd = new DataTable();
                    if (_result.Select("CODE='" + txtAccountCode.Text.Trim() + "'").Length > 0)
                    {
                        Filterd = _result.Select("CODE='" + txtAccountCode.Text.Trim() + "'").CopyToDataTable();
                        if (Filterd.Rows.Count > 0)
                        {
                            txtAccountDesc.Text = Filterd.Rows[0]["NAME"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Account no.", "Cheque Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtAccountCode.Clear();
                            txtAccountDesc.Clear();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Account no.", "Cheque Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAccountCode.Clear();
                        txtAccountDesc.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Account no.", "Cheque Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccountCode.Clear();
                    txtAccountDesc.Clear();
                }
            }
        }

        private void txtAccountCode_TextChanged(object sender, EventArgs e)
        {
            txtAccountDesc.Clear();
        }

        private void txtBankCode_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBankCode;
            _CommonSearch.ShowDialog();
            txtBankCode.Select();
        }

        private void txtBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtBankCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //txtBranckCode.Focus();
                txtPC.Focus();
            }
        }

        private void txtBankCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBankCode.Text))
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                if (_result.Rows.Count > 0)
                {
                    if (_result.Select("Code='" + txtBankCode.Text + "'").Length > 0)
                    {
                        DataTable dtTemp = _result.Select("Code='" + txtBankCode.Text + "'").CopyToDataTable();
                        lblBank.Text = dtTemp.Rows[0]["Description"].ToString();
                        txtBankCode.Tag = dtTemp.Rows[0]["ID"].ToString();
                        txtBranckCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalied Bank code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBankCode.Clear();
                        txtBankCode.Focus();
                        lblBank.Text = "";
                        return;
                    }
                }
            }
        }

        private void txtBranckCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBranckCode.Text))
            {
                DataTable _result = new DataTable();

                _result = CHNLSVC.Sales.Get_buscom_branch_det(txtBankCode.Tag.ToString());

                if (_result.Select("mbb_bus_cd='" + txtBankCode.Tag.ToString() + "' AND mbb_cd ='" + txtBranckCode.Text + "'").Length > 0)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = _result.Select("mbb_bus_cd='" + txtBankCode.Tag.ToString() + "' AND mbb_cd ='" + txtBranckCode.Text + "'").CopyToDataTable();
                    lblBranck.Text = dtTemp.Rows[0]["mbb_desc"].ToString();
                }
                else
                {
                    txtBranckCode.Clear();
                    lblBranck.Text = "";
                    txtBranckCode.Focus();
                    MessageBox.Show("Please select a correct branch code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtBranckCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtBranckCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtPC.Focus();
            }
        }

        private void txtBranckCode_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBankCode.Text))
            {
                MessageBox.Show("Please select a bank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankCode.Focus();
                return;
            }
            else
            {
                try
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                    DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtBranckCode;
                    _CommonSearch.ShowDialog();
                    txtBranckCode.Select();
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
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, txtAccountNo.Text);
                if (_result == null || _result.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a correct account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtAccNoDesc.Text = _result.Rows[0]["Description"].ToString();
                    txtBankCode.Text = _result.Rows[0]["MSBA_CD"].ToString();
                    txtBankCode_Leave(null, null);
                }
            }
        }

        private void txtAccountNo_TextChanged(object sender, EventArgs e)
        {
            txtAccNoDesc.Clear();
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAccountNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtBankCode.Focus();
            }
        }

        private void txtDeptCode_Leave(object sender, EventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(txtDeptCode.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                    if (_result.Select("CODE='" + txtDeptCode.Text.Trim() + "'").Length > 0)
                    {
                        DataTable dtFiltered = _result.Select("CODE='" + txtDeptCode.Text.Trim() + "'").CopyToDataTable();
                        txtDeptName.Text = dtFiltered.Rows[0]["DESCRIPTION"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please select a correct department code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDeptCode.Clear();
                        txtDeptCode.Focus();
                        return;
                    }
                }
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCdtDbt.Focus();
            }
        }

        private void txtCrdDebt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEPFEFT.Focus();
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
      && !char.IsDigit(e.KeyChar)
      && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (valdateAdd() && validateAddItems())
            {
                PC = txtPC.Text;
                PCD = txtPCDesc.Text;
                ACC = txtAccountCode.Text;
                ACCD = txtAccountDesc.Text;
                INVO = txtInvoiceNo.Text;
                DESC = txtDescription.Text;
                DEP = txtDeptCode.Text;
                DEPD = txtDeptName.Text;
                AMNT = txtAmount.Text;

                InterVoucherDetails Item = new InterVoucherDetails();
                Item.GIVD_EXPE_CD = txtAccountCode.Text;
                Item.GIVD_EXPE_DESC = txtDescription.Text;
                Item.GIVD_EXPE_VAL = Convert.ToDecimal(txtAmount.Text);
                Item.GIVD_ANAL3 = txtBankCode.Tag.ToString();
                Item.GIVD_ANAL4 = txtBranckCode.Text;
                Item.GIVD_ANAL5 = txtPC.Text;
                Item.GIVD_ANAL6 = string.Empty;
                Item.GIVD_ANAL7 = txtDeptCode.Text;
                Item.GIVD_ANAL8 = txtInvoiceNo.Text;
                Item.GIVD_ANAL9 = txtEPFEFT.Text;
                Item.GIVD_ANAL10 = txtTax.Text;
                Item.GVID_DT = Convert.ToDateTime(dtpDate.Text).Date;
                Item.AccountCodeDescription = txtAccountDesc.Text;

                DataRow[] drTemp = CHNLSVC.Financial.GetChqVoucherAccount(BaseCls.GlbUserComCode, DateTime.Today).Select("CHQ_ISS = 1 AND TYPE = 'S' AND CODE = '" + txtAccountCode.Text + "'");
                if (drTemp.Length > 0)
                {
                    Item.isSupplierPrint = 1;
                    txtPrintName.Text = drTemp[0][1].ToString();
                }
                else
                {
                    Item.isSupplierPrint = 0;
                }

                if (cmbCdtDbt.Text.ToUpper() == "Credit".ToUpper())
                {
                    Item.Credt = Convert.ToDecimal(txtAmount.Text);
                    Item.GIVD_EXPE_DIRECT = 1;
                }
                else if (cmbCdtDbt.Text.ToUpper() == "debit".ToUpper())
                {
                    Item.Debit = Convert.ToDecimal(txtAmount.Text);
                    Item.GIVD_EXPE_DIRECT = 0;
                }

                if (txtSearch.Text.Length > 0)
                {
                    Item.GIVD_VOU_NO = txtSearch.Text;
                    Item.GIVD_SEQ = Convert.ToInt32(txtSearch.Tag);
                    Item.GIVD_LINE = LastDetailLine + 1;
                }

                oMainDetailList.Add(Item);

                UpdateMailList();

                if (oMainDetailList.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = oMainDetailList;
                }
                ClearGrpbox1();
                txtAccountCode.Focus();
                dtpDate.Enabled = false;
                txtAccountNo.Enabled = false;
                txtBankCode.Enabled = false;
                txtBranckCode.Enabled = false;

                if (MessageBox.Show("Do you want to add new item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    txtPC.Focus();
                }
                else
                {
                    txtChqBook.Focus();
                }
            }
            calculateTotals();
        }

        private void label44_DoubleClick(object sender, EventArgs e)
        {
            if (pnlSummery.Visible == false)
            {
                pnlSummery.Visible = true;
            }
            else
            {
                pnlSummery.Visible = false;
            }
        }

        private void btnSummery_Click(object sender, EventArgs e)
        {
            CalculateSummery();
            if (pnlSummery.Visible == false)
            {
                pnlSummery.Visible = true;
            }
            else
            {
                pnlSummery.Visible = false;
            }
        }

        private void btnCESave_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10092))
            {
                MessageBox.Show("Sorry, You have no permission to save the cheque voucher.\n( Advice: Required permission code :10092)");
                return;
            }
            if (rbnMultipleCheques.Checked)
            {
                if (IsCreditorHasDebit())
                {
                    return;
                }
            }

            if (txtSearch.Text.Length > 0)
            {
                if (MessageBox.Show("Are you want to Update", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                upDetails();
            }
            else
            {
                if (Convert.ToDecimal(txtDiff.Text) != 0)
                {
                    MessageBox.Show("Please check amounts. Deffecence should be zero.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Are you want to Save", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                bool isSingleCheque = false;
                if (valdateHeader())
                {
                    Cursor = Cursors.WaitCursor;

                    if (!string.IsNullOrEmpty(txtChqNo.Text))
                    {
                        if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, txtAccountNo.Text, "PRN-CHQ", Convert.ToInt32(txtChqNo.Text)))
                        {
                            MessageBox.Show("Please enter a valied cheque number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtChqNo.Clear();
                            txtChqNo.Focus();
                            return;
                        }
                    }

                    if (oMainDetailList.Count > 0)
                    {
                        if (rbnSingleCheck.Checked)
                        {
                            isSingleCheque = true;
                        }
                        else if (rbnMultipleCheques.Checked)
                        {
                            isSingleCheque = false;
                        }

                        VoucherHeader oHeader = new VoucherHeader();

                        oHeader.Givh_com = BaseCls.GlbUserComCode;
                        oHeader.Givh_dt = Convert.ToDateTime(dtpDate.Text).Date;
                        oHeader.Givh_valid_from = Convert.ToDateTime(dtpDate.Text).Date;
                        oHeader.Givh_valid_to = Convert.ToDateTime(dtpDate.Text).Date;
                        oHeader.Givh_emp_cd = string.Empty;
                        oHeader.Givh_val = Convert.ToDecimal(txtDiff.Text);
                        oHeader.Givh_print_stus = 0;
                        oHeader.Givh_act_stus = 2;
                        //oHeader.Givh_claim_stus = "";
                        //oHeader.Givh_claim_com = "";
                        //oHeader.Givh_claim_pc = "";
                        //oHeader.Givh_claim_dt = "";
                        oHeader.Givh_cre_by = BaseCls.GlbUserID;
                        oHeader.Givh_cre_dt = DateTime.Now;
                        oHeader.Givh_mod_by = BaseCls.GlbUserID;
                        oHeader.Givh_mod_dt = DateTime.Now;
                        oHeader.Givh_val_txt = "";
                        oHeader.Givh_prep_by = BaseCls.GlbUserID;
                        oHeader.Givd_acpt_by = "";
                        oHeader.Givd_auth_by = "";
                        oHeader.Givd_emp_name = "";
                        oHeader.Givd_emp_cat = "";
                        oHeader.Givd_chnl = "";
                        oHeader.Givd_dept = "";
                        oHeader.Givh_vou_tp = "CHQ";
                        oHeader.Givh_bnk_acc = txtAccountNo.Text;
                        oHeader.Givh_bnk_cd = txtBankCode.Tag.ToString();
                        oHeader.Givh_brnch_cd = txtBranckCode.Text;

                        oHeader.Givd_chnl = BaseCls.GlbDefChannel;
                        oHeader.Givh_claim_pc = BaseCls.GlbUserDefProf;
                        oHeader.Givd_dept = BaseCls.GlbUserDeptID;

                        if (!string.IsNullOrEmpty(txtChqNo.Text))
                        {
                            oHeader.Givh_cheque_no = Convert.ToInt32(txtChqNo.Text);
                        }
                        else
                        {
                            oHeader.Givh_cheque_no = Convert.ToInt32("-1");
                        }
                        oHeader.Givh_period = dtpPeriod.Text;
                        oHeader.Givh_issunupload = (chkNotSunUpload.Checked) ? 1 : 0;
                        oHeader.Givh_printname = txtPrintName.Text;
                        oHeader.Givh_Note = txtNarration.Text;
                        MasterAutoNumber _vouAuto = new MasterAutoNumber();
                        _vouAuto.Aut_cate_tp = "PC";
                        _vouAuto.Aut_direction = 1;
                        _vouAuto.Aut_modify_dt = null;
                        _vouAuto.Aut_number = 0;

                        //if (BaseCls.GlbUserComCode == "AAL")
                        //{
                        //    _vouAuto.Aut_moduleid = "AAVOU";
                        //    _vouAuto.Aut_cate_cd = "AAVOU";
                        //    _vouAuto.Aut_start_char = "AAVOU";
                        //}
                        //else
                        //{
                        //    _vouAuto.Aut_moduleid = "VOU";
                        //    _vouAuto.Aut_cate_cd = "VOU";
                        //    _vouAuto.Aut_start_char = "VOU";
                        //}

                        _vouAuto.Aut_moduleid = BaseCls.GlbUserComCode + "CHQVOU";
                        _vouAuto.Aut_cate_cd = BaseCls.GlbUserComCode + "CHQVOU";
                        _vouAuto.Aut_start_char = BaseCls.GlbUserComCode + "CHQVOU";
                        _vouAuto.Aut_year = null;

                        _vouAuto.Aut_year = null;

                        string doc_no = "";

                        foreach (InterVoucherDetails item in oMainDetailList)
                        {
                            item.GIVD_LINE = oMainDetailList.IndexOf(item) + 1;
                        }
                        int result = CHNLSVC.Financial.SaveJournalVouvher(oHeader, oMainDetailList, _vouAuto, out doc_no, isSingleCheque);

                        if (!string.IsNullOrEmpty(txtChqNo.Text))
                        {
                            int result2 = CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, txtAccountNo.Text, "PRN-CHQ", Convert.ToInt32(txtChqNo.Text), doc_no);
                        }
                        if (result > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("Record insert successfully\nDocument No - " + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCEClear_Click(null, null);
                            return;
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("Process Terminated. \n" + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Please add details to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnCEPrint_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10095))
            {
                MessageBox.Show("Sorry, You have no permission to print cheque voucher.\n( Advice: Required permission code :10095)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Please select a voucher to print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSearch.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtChqNo.Text))
            {
                MessageBox.Show("Please enter cheque number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtChqNo.Focus();
                return;
            }
         
            if (MessageBox.Show("Is this cheque payee only?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {// Nadeeka 13-01-2014
                BaseCls.GlbRecType = "Y";
            }
            else
            {
                BaseCls.GlbRecType = "N";
            }

            printVoucher(txtSearch.Text);
            int result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, txtSearch.Text, 1, 1, BaseCls.GlbUserID, DateTime.Now);
            if (result > 0)
            {
                MessageBox.Show("Process successfully completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCEClear_Click(null, null);
                return;
            }
            else
            {
                MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnCEClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (((TextBox)item).Name == "txtAccountNo" || ((TextBox)item).Name == "txtBankCode" || ((TextBox)item).Name == "txtAccNoDesc")
                    {
                        continue;
                    }
                    ((TextBox)item).Clear();
                }
            }

            txtChqNo.Clear();
            dtpDate.Value = DateTime.Now;
            cmbCdtDbt.SelectedIndex = 0;
            chkNotSunUpload.Checked = false;
            dataGridView1.DataSource = null;
            oMainDetailList.Clear();
            dtpPeriod.Value = DateTime.Now;
            pnlSummery.Visible = false;
            calculateTotals();
            SetSummeryGridEmpty();
            //lblBank.Text = "";
            lblBranck.Text = "";
            isExcelUpload = false;
            rbnSingleCheck.Checked = true;
            txtFileName_pv.Clear();
            isExcelUpload = false;
            dtpDate.Enabled = true;
            txtAccountNo.Enabled = true;
            txtBankCode.Enabled = true;
            txtBranckCode.Enabled = true;
            txtSearch.Clear();
            txtPrintName.Clear();
            txtNarration.Clear();
            LastDetailLine = 0;
            //txtBankCode.Tag = "";
            txtSearch.Tag = "";
            btnCESave.Enabled = true;
            lblStatus.Text = "";
            btnCEPrint.Enabled = true;
            btnApprove.Enabled = true;
            btnCVCancel.Enabled = true;
            txtAccountNo.Focus();
        }

        private void btnCEClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDeptCode.Focus();
            }
        }

        private void cmbCdtDbt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEPFEFT.Focus();
            }
        }

        private void txtEPFEFT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTax.Focus();
            }
        }

        private void txtTax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }

        private void txtChqNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (e.ColumnIndex == dgvFTDetails.Columns.IndexOf(dgvFTDetails.Columns["Delete"]))
                {
                    DialogResult dgr = MessageBox.Show("Do you want to delete this record?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dgr == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    oMainDetailList.Remove(oMainDetailList.Find(x => x.GIVD_ANAL5 == dataGridView1.Rows[e.RowIndex].Cells["ProfitCenter"].Value.ToString() &&
                                                                x.GIVD_ANAL8 == dataGridView1.Rows[e.RowIndex].Cells["InvoiceNo"].Value.ToString() &&
                                                                x.GIVD_EXPE_CD == dataGridView1.Rows[e.RowIndex].Cells["AccountCode"].Value.ToString() &&
                                                                x.GIVD_EXPE_VAL == Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value.ToString())));

                    calculateTotals();

                    dataGridView1.DataSource = null;

                    if (oMainDetailList.Count > 0)
                    {
                        dataGridView1.DataSource = oMainDetailList;
                        dataGridView1.Refresh();
                    }
                }
            }
        }

        private void txtDeptCode_TextChanged(object sender, EventArgs e)
        {
            txtDeptName.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(txtBankCode.Tag.ToString());
        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtFileName_pv.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName_pv.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFileName_pv.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFileName_pv.Clear();
                    txtFileName_pv.Focus();
                    return;
                }

                System.IO.FileInfo _fileObj = new System.IO.FileInfo(txtFileName_pv.Text);

                if (_fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFileName_pv.Focus();
                    return;
                }

                string _extension = _fileObj.Extension;
                string _conStr = string.Empty;

                if (_extension.ToUpper() == ".XLS")
                {
                    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName_pv.Text + "; Extended Properties='Excel 8.0;HDR=YES;'";
                }
                else if (_extension.ToUpper() == ".XLSX")
                {
                    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName_pv.Text + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
                }
                else
                {
                    MessageBox.Show("Please Select valid Ms Excel File.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                _conStr = String.Format(_conStr, txtFileName_pv.Text, "NO");
                OleDbConnection _connExcel = new OleDbConnection(_conStr);
                OleDbCommand _cmdExcel = new OleDbCommand();
                OleDbDataAdapter _oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                _cmdExcel.Connection = _connExcel;
                _connExcel.Open();
                DataTable _dtExcelSchema;
                _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                _connExcel.Close();

                _connExcel.Open();
                _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
                _oda.SelectCommand = _cmdExcel;
                _oda.Fill(_dt);
                _connExcel.Close();
                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                oMainDetailList.Clear();
                string _msg = string.Empty;

                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0]["Period"].ToString().Length > 0)
                    {
                        string[] arrayDate = _dt.Rows[0]["Period"].ToString().Split('/');

                        if (arrayDate.Length > 1)
                        {
                            string mm = arrayDate[0];
                            string yyyy = arrayDate[1];
                            dtpPeriod.Value = Convert.ToDateTime("01/" + mm + '/' + yyyy);
                        }
                    }

                    if (validateExcelFile(_dt))
                    {
                        // MessageBox.Show("Invalied Excel file. Please check the excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        oMainDetailList.Clear();
                        dataGridView1.DataSource = null;
                        return;
                    }

                    dtpDate.Value = Convert.ToDateTime(_dt.Rows[0]["Date"].ToString());

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(_dt.Rows[i]["AccountCode"].ToString()))
                        {
                            //Validate EPF/ETF and Tax Code

                            #region Excel Validation

                            double num;
                            if (double.TryParse(_dt.Rows[i]["T7 - EPF/ETF"].ToString(), out num) || _dt.Rows[i]["T7 - EPF/ETF"].ToString().Length == 0)
                            {
                                MessageBox.Show("Invalied Excel file. \nPlease check EPF/ETF Code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                btnCEClear_Click(null, null);
                                return;
                            }

                            double num2;
                            if (double.TryParse(_dt.Rows[i]["T3 - Tax Code"].ToString(), out num2) || _dt.Rows[i]["T3 - Tax Code"].ToString().Length == 0)
                            {
                                MessageBox.Show("Invalied Excel file. \nPlease check the Tax Code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                btnCEClear_Click(null, null);
                                return;
                            }

                            #endregion Excel Validation

                            InterVoucherDetails oItem = new InterVoucherDetails();
                            oItem.GIVD_EXPE_CD = txtAccountNo.Text;
                            oItem.GIVD_EXPE_DESC = _dt.Rows[i]["Description"].ToString();
                            oItem.GIVD_EXPE_VAL = Convert.ToDecimal(_dt.Rows[i]["Amount"].ToString());
                            if (oItem.GIVD_EXPE_VAL < 0)
                            {
                                oItem.GIVD_EXPE_VAL = oItem.GIVD_EXPE_VAL * -1;
                            }
                            oItem.GIVD_ANAL3 = _dt.Rows[i]["T5 - Bank"].ToString();
                            oItem.GIVD_ANAL4 = _dt.Rows[i]["T6 - Bank Branch"].ToString();
                            oItem.GIVD_ANAL5 = _dt.Rows[i]["T0 - BRANCH CODE "].ToString();
                            oItem.GIVD_EXPE_CD = _dt.Rows[i]["AccountCode"].ToString();
                            oItem.GIVD_ANAL7 = _dt.Rows[i]["DepartmentCode"].ToString();
                            oItem.GIVD_ANAL8 = _dt.Rows[i]["Trn Ref"].ToString();

                            oItem.GIVD_ANAL9 = _dt.Rows[i]["T7 - EPF/ETF"].ToString().ToUpper();
                            oItem.GIVD_ANAL10 = _dt.Rows[i]["T3 - Tax Code"].ToString().ToUpper();

                            oItem.AccountCodeDescription = txtAccountDesc.Text;
                            if (_dt.Rows[i]["D_C"].ToString().ToUpper() == "C".ToUpper())
                            {
                                oItem.Credt = oItem.GIVD_EXPE_VAL;
                                oItem.GIVD_EXPE_DIRECT = 1;
                            }
                            else if (_dt.Rows[i]["D_C"].ToString().ToUpper() == "D".ToUpper())
                            {
                                oItem.Debit = oItem.GIVD_EXPE_VAL;
                                oItem.GIVD_EXPE_DIRECT = 0;
                            }
                            oMainDetailList.Add(oItem);
                            isExcelUpload = true;
                            if (oMainDetailList.Count > 0)
                            {
                                dataGridView1.DataSource = null;
                                dataGridView1.DataSource = oMainDetailList;
                            }
                            ClearGrpbox1();
                            txtAccountCode.Focus();
                        }
                    }
                    calculateTotals();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFileName_pv_DoubleClick(object sender, EventArgs e)
        {
            btnSearchFile_spv_Click(null, null);
        }

        private void txtFileName_pv_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName_pv.Text))
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        private void txtChqNo_TextChanged(object sender, EventArgs e)
        {
            //Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_INV");
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (txtAmount.Text.Length > 0)
            {
                txtAmount.Text = (Convert.ToDecimal(txtAmount.Text) > 0) ? txtAmount.Text : (Convert.ToDecimal(txtAmount.Text) * -1).ToString();
            }
            if (txtAmount.Text.Length > 0)
            {
                if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(99999999.9999))
                {
                    MessageBox.Show("Please enter valied amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                    txtAmount.Clear();
                    txtAmount.Focus();
                    return;
                }
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDate.Value < DateTime.Now)
            {
                dtpDate.Value = DateTime.Now;
            }
        }

        private void txtEPFEFT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10093))
            {
                MessageBox.Show("Sorry, You have no permission to approve the cheque voucher.\n( Advice: Required permission code :10093)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtSearch.Text.Length > 0)
            {
                if (MessageBox.Show("Do you want to approve this voucher?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = CHNLSVC.Financial.UPDATE_VOUCHER_STATUS(BaseCls.GlbUserComCode, txtSearch.Text, 1, 0, BaseCls.GlbUserID, DateTime.Now);
                    if (result > 0)
                    {
                        MessageBox.Show("Voucher approved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCEClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //btnCEClear_Click(null, null);
        }

        private void txtSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChequeVouchers);
                DataTable _result = CHNLSVC.CommonSearch.GetChequeVouchers(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSearch;
                _CommonSearch.ShowDialog();
                txtSearch.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSearch_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSummery.Focus();
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                VoucherHeader oHeader = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, txtSearch.Text);

                if (oHeader != null && oHeader.Givh_vou_no != null)
                {
                    txtSearch.Tag = oHeader.Givh_seq;
                    dtpDate.Value = Convert.ToDateTime(oHeader.Givh_valid_from);
                    txtEPFEFT.Text = oHeader.Givh_emp_cd;

                    txtAccountNo.Text = oHeader.Givh_bnk_acc;

                    DataTable bankName = CHNLSVC.Sales.getReturnChequeBank(oHeader.Givh_bnk_cd);
                    txtBankCode.Text = bankName.Rows[0]["mbi_id"].ToString();
                    lblBank.Text = bankName.Rows[0]["mbi_desc"].ToString();
                    txtBankCode.Tag = oHeader.Givh_bnk_cd;
                    txtBranckCode.Text = oHeader.Givh_brnch_cd;
                    txtBranckCode_Leave(null, null);
                    txtChqNo.Text = oHeader.Givh_cheque_no.ToString();
                    if (oHeader.Givh_cheque_no.ToString() == "-1")
                    {
                        txtChqNo.Clear();
                    }

                    if (oHeader.Givh_period.Length > 0)
                    {
                        string[] period = oHeader.Givh_period.Split('/');
                        if (period.Length >= 2)
                        {
                            dtpPeriod.Value = Convert.ToDateTime("01/" + period[0] + "/" + period[1]);
                        }
                    }

                    if (oHeader.Givh_issunupload == 1)
                    {
                        chkNotSunUpload.Checked = true;
                    }
                    else
                    {
                        chkNotSunUpload.Checked = false;
                    }

                    txtPrintName.Text = oHeader.Givh_printname;
                    txtNarration.Text = oHeader.Givh_Note;

                    GetDetails(txtSearch.Text);

                    btnCESave.Enabled = true;
                    lblStatus.Text = "";
                    btnCEPrint.Enabled = true;
                    btnApprove.Enabled = true;
                    btnCVCancel.Enabled = true;

                    if (oHeader.Givh_act_stus == 0 && oHeader.Givh_print_stus == 0)
                    {
                        lblStatus.Text = "Canceled";
                        btnCEPrint.Enabled = false;
                    }
                    if (oHeader.Givh_act_stus == 2 && oHeader.Givh_print_stus == 0)
                    {
                        //btnCESave.Enabled = false;
                        lblStatus.Text = "Approval pending.";
                        btnCEPrint.Enabled = false;
                    }
                    if (oHeader.Givh_act_stus == 1 && oHeader.Givh_print_stus == 0)
                    {
                        btnCESave.Enabled = false;
                        btnApprove.Enabled = false;
                        lblStatus.Text = "Approved.";
                    }
                    if (oHeader.Givh_act_stus == 1 && oHeader.Givh_print_stus == 1)
                    {
                        btnCESave.Enabled = false;
                        lblStatus.Text = "Printed.";
                        //btnCEPrint.Enabled = false;
                        btnApprove.Enabled = false;
                        btnCVCancel.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please select correct voucher number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearch.Clear();
                    txtSearch.Focus();
                }
            }
        }

        private void label44_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label44_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void label44_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = pnlSummery.Location.X - xDiff;
                int y = pnlSummery.Location.Y - yDiff;
                pnlSummery.Location = new Point(x, y);
            }
        }

        private void btnHideSummery_Click(object sender, EventArgs e)
        {
            if (pnlSummery.Visible == false)
            {
                pnlSummery.Visible = true;
            }
            else
            {
                pnlSummery.Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtSearch_DoubleClick(null, null);
        }

        private void btnCVCancel_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10094))
            {
                MessageBox.Show("Sorry, You have no permission to cancel cheque voucher.\n( Advice: Required permission code :10094)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSearch.Text.Length > 0)
            {
                if (MessageBox.Show("Do you want to cancel this voucher?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = CHNLSVC.Financial.UPDATE_VOUCHER_STATUS(BaseCls.GlbUserComCode, txtSearch.Text, 0, 0, BaseCls.GlbUserID, DateTime.Now);
                    if (result > 0)
                    {
                        MessageBox.Show("Voucher successfully canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCEClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void txtChqNo_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtAccountNo.Text))
            //{
            //    MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtAccountNo.Focus();
            //    return;
            //}

            if (!string.IsNullOrEmpty(txtChqNo.Text))
            {
                txtChqNo.Text = txtChqNo.Text.PadLeft(6, '0');
                if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, txtAccountNo.Text, "PRN-CHQ", Convert.ToInt32(txtChqNo.Text)))
                {
                    MessageBox.Show("Please enter a valied cheque number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChqNo.Clear();
                    txtChqNo.Focus();
                    return;
                }
                else
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        VoucherHeader oHeader = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, txtSearch.Text);
                        if (oHeader != null && oHeader.Givh_vou_no != null)
                        {
                            if (oHeader.Givh_cheque_no < 0)
                            {
                                if (MessageBox.Show("Do you want to update new cheque number?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    int result2 = CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, txtAccountNo.Text, "PRN-CHQ", Convert.ToInt32(txtChqNo.Text), txtSearch.Text);
                                    //should update voucher
                                    int result3 = CHNLSVC.Financial.UpdateVoucherChequeNumber(BaseCls.GlbUserComCode, txtChqNo.Text, BaseCls.GlbUserID, txtSearch.Text);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtChqNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkNotSunUpload.Focus();
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkNotSunUpload.Focus();
            }
        }

        private void chkNotSunUpload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpPeriod.Focus();
            }
        }

        private void dtpPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPrintName.Focus();
            }
        }

        private void txtPrintName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNarration.Focus();
            }
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip2.Focus();
                btnCESave.Select();
            }
        }

        private void txtPay_Leave(object sender, EventArgs e)
        {
            validatePayUser();
        }

        private void txtChqBook_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChqBook.Text))
            {
                DataTable dtItem = CHNLSVC.Sales.Get_Currect_By_Book("PRN-CHQ", txtChqBook.Text, txtAccountNo.Text);
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    txtChqNo.Text = dtItem.Rows[0]["mdd_current"].ToString();
                }
                else
                {
                    MessageBox.Show("Please enter correct cheque book number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChqBook.Clear();
                    txtChqBook.Focus();
                    return;
                }
            }
        }

        private void txtChqBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChqNo.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtChqBook_DoubleClick(null, null);
            }
        }

        private void txtChqBook_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChequeBook);
                DataTable _result = CHNLSVC.CommonSearch.GET_CHEQUE_BOOKs(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChqBook;
                _CommonSearch.ShowDialog();
                txtChqBook.Select();
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

        #endregion Events

        #region Methods

        private bool valdateHeader()
        {
            bool status = false;
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtBankCode.Text))
            {
                MessageBox.Show("Please enter bank code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankCode.Focus();
                return status;
            }
            //if (string.IsNullOrEmpty(txtBranckCode.Text))
            //{
            //    MessageBox.Show("Please enter branch code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtBranckCode.Focus();
            //    return status;
            //}
            if (string.IsNullOrEmpty(txtPrintName.Text))
            {
                MessageBox.Show("Please enter print Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrintName.Focus();
                return status;
            }
            //if (string.IsNullOrEmpty(txtChqNo.Text))
            //{
            //    MessageBox.Show("Please enter cheque number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtChqNo.Focus();
            //    return status;
            //}
            if (!isExcelUpload)
            {
                //if (string.IsNullOrEmpty(txtPC.Text))
                //{
                //    MessageBox.Show("Please enter Profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPC.Focus();
                //    return status;
                //}
            }

            status = true;
            return status;
        }

        private void CalculateSummery()
        {
            try
            {
                if (oMainDetailList.Count > 0)
                {
                    #region Calculate Invoice Summery

                    string[] invoiceNumbers = oMainDetailList.Select(x => x.GIVD_ANAL8).Distinct().ToArray();

                    if (invoiceNumbers.Length > 0)
                    {
                        DataTable dtInvoiceSummery = new DataTable();
                        dtInvoiceSummery.Columns.Add("ISProfitCenter");
                        dtInvoiceSummery.Columns.Add("ISInvoice");
                        dtInvoiceSummery.Columns.Add("ISAmount", typeof(decimal));
                        dtInvoiceSummery.Columns.Add("ISType");

                        for (int i = 0; i < invoiceNumbers.Length; i++)
                        {
                            if (rbnMultipleCheques.Checked)
                            {
                                DataRow dr = dtInvoiceSummery.NewRow();
                                dr["ISProfitCenter"] = oMainDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Select(x => x.GIVD_ANAL5).ToArray()[0];
                                dr["ISInvoice"] = invoiceNumbers[i].ToString();

                                //decimal totD = 0;
                                decimal totC = 0;

                                //totD = oMainDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                                totC = oMainDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);

                                dr["ISAmount"] = oMainDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString() && z.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
                                //dr["ISAmount"] = (totD - totC).ToString("N");
                                dr["ISAmount"] = (totC).ToString("N");

                                dr["ISType"] = "C";

                                //if ((totD - totC) > 0)
                                //{
                                //    dr["ISType"] = "D";
                                //}
                                //else
                                //{
                                //    dr["ISType"] = "C";
                                //    dr["ISAmount"] = ((totD - totC) * -1).ToString("N");
                                //}

                                if (oMainDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString() && z.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL) > 0)
                                {
                                    dtInvoiceSummery.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = dtInvoiceSummery.NewRow();
                                dr["ISProfitCenter"] = oMainDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Select(x => x.GIVD_ANAL5).ToArray()[0];
                                dr["ISInvoice"] = invoiceNumbers[i].ToString();
                                dr["ISAmount"] = oMainDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Sum(x => x.GIVD_EXPE_VAL);
                                dr["ISType"] = "";

                                //decimal totD = 0;
                                decimal totC = 0;

                                //totD = oMainDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                                totC = oMainDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
                                //dr["ISAmount"] = (totD - totC).ToString("N");
                                dr["ISAmount"] = (totC).ToString("N");

                                dr["ISType"] = "C";

                                //if ((totD - totC) > 0)
                                //{
                                //    dr["ISType"] = "D";
                                //}
                                //else
                                //{
                                //    dr["ISType"] = "C";
                                //    dr["ISAmount"] = ((totD - totC) * -1).ToString("N");
                                //}

                                if (oMainDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Sum(x => x.GIVD_EXPE_VAL) > 0)
                                {
                                    dtInvoiceSummery.Rows.Add(dr);
                                }
                            }
                        }

                        dgvInvoiceSummery.DataSource = dtInvoiceSummery;
                    }

                    #endregion Calculate Invoice Summery

                    #region Calculate journal Summery

                    string[] AccountCodeList = oMainDetailList.Select(x => x.GIVD_EXPE_CD).Distinct().ToArray();

                    if (AccountCodeList.Length > 0)
                    {
                        DataTable dtJournalSummery = new DataTable();
                        dtJournalSummery.Columns.Add("JSAccountCode");
                        dtJournalSummery.Columns.Add("JSAccDescription");
                        dtJournalSummery.Columns.Add("JSAmount", typeof(decimal));
                        dtJournalSummery.Columns.Add("JSType");

                        for (int i = 0; i < AccountCodeList.Length; i++)
                        {
                            DataRow dr = dtJournalSummery.NewRow();
                            dr["JSAccountCode"] = AccountCodeList[i].ToString();
                            dr["JSAccDescription"] = oMainDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Select(x => x.AccountCodeDescription).ToArray()[0].ToString();
                            dr["JSAmount"] = oMainDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Sum(x => x.GIVD_EXPE_VAL);

                            decimal totD = 0;
                            decimal totC = 0;

                            totD = oMainDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString() && x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                            totC = oMainDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString() && x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);

                            dr["JSAmount"] = (totD - totC).ToString("N");

                            dr["JSType"] = oMainDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Select(x => x.GIVD_EXPE_DIRECT).ToArray()[0].ToString();

                            //if (dr["JSType"].ToString() == "1")
                            //{
                            //    dr["JSType"] = "C";
                            //}
                            //else
                            //{
                            //    dr["JSType"] = "D";
                            //}
                            //dr["JSType"] = "C";

                            if ((totD - totC) > 0)
                            {
                                dr["JSType"] = "D";
                            }
                            else
                            {
                                dr["JSType"] = "C";
                                dr["JSAmount"] = ((totD - totC) * -1).ToString("N");
                            }
                            dtJournalSummery.Rows.Add(dr);
                        }
                        dgvJournalSummery.DataSource = dtJournalSummery;
                    }

                    #endregion Calculate journal Summery
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void calculateTotals()
        {
            decimal totDebit = 0;
            decimal totCrdt = 0;
            decimal diff = 0;
            if (oMainDetailList.Count > 0)
            {
                if (oMainDetailList.FindAll(x => x.GIVD_EXPE_DIRECT == 0).Count > 0)
                {
                    totDebit = oMainDetailList.Where(x => x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                }
                if (oMainDetailList.FindAll(x => x.GIVD_EXPE_DIRECT == 1).Count > 0)
                {
                    totCrdt = oMainDetailList.Where(x => x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
                }
            }
            diff = totDebit - totCrdt;
            txtDiff.Text = diff.ToString("N");
            txtTotCrdt.Text = totCrdt.ToString("N");
            txtTotDebit.Text = totDebit.ToString("N");
        }

        private void ClearGrpbox1()
        {
            txtAccountCode.Clear();
            txtAccountDesc.Clear();
            txtInvoiceNo.Clear();
            txtDescription.Clear();
            txtDeptCode.Clear();
            txtDeptName.Clear();
            txtAmount.Clear();
            cmbCdtDbt.SelectedIndex = 0;
            txtEPFEFT.Clear();
            txtTax.Clear();
        }

        private bool valdateAdd()
        {
            bool status = false;
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtBankCode.Text))
            {
                MessageBox.Show("Please enter bank code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankCode.Focus();
                return status;
            }
            //if (string.IsNullOrEmpty(txtBranckCode.Text))
            //{
            //    MessageBox.Show("Please enter branch code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtBranckCode.Focus();
            //    return status;
            //}
            if (string.IsNullOrEmpty(txtPC.Text))
            {
                MessageBox.Show("Please enter Profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPC.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(txtAccountCode.Text))
            {
                MessageBox.Show("Please enter account code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountCode.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MessageBox.Show("Please enter invoice number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoiceNo.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please enter description.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return status;
            }

            //if (string.IsNullOrEmpty(txtDeptCode.Text))
            //{
            //    MessageBox.Show("Please enter department code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtDeptCode.Focus();
            //    return status;
            //}

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                MessageBox.Show("Please enter amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(cmbCdtDbt.Text))
            {
                MessageBox.Show("Please select pay type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbCdtDbt.Focus();
                return status;
            }
            //if (string.IsNullOrEmpty(txtEPFEFT.Text))
            //{
            //    MessageBox.Show("Please enter EPF/ETF.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtEPFEFT.Focus();
            //    return status;
            //}
            //if (string.IsNullOrEmpty(txtTax.Text))
            //{
            //    MessageBox.Show("Please enter TAX.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtTax.Focus();
            //    return status;
            //}
            status = true;
            return status;
        }

        private void SetSummeryGridEmpty()
        {
            DataTable dtInvoiceSummery = new DataTable();
            dtInvoiceSummery.Columns.Add("ISProfitCenter");
            dtInvoiceSummery.Columns.Add("ISInvoice");
            dtInvoiceSummery.Columns.Add("ISAmount");
            dtInvoiceSummery.Columns.Add("ISType");
            dgvInvoiceSummery.DataSource = dtInvoiceSummery;

            DataTable dtJournalSummery = new DataTable();
            dtJournalSummery.Columns.Add("JSAccountCode");
            dtJournalSummery.Columns.Add("JSAccDescription");
            dtJournalSummery.Columns.Add("JSAmount");
            dtJournalSummery.Columns.Add("JSType");
            dgvJournalSummery.DataSource = dtJournalSummery;
        }

        private bool checkAccountCodes(string[] accountCodes)
        {
            Cursor = Cursors.WaitCursor;
            bool status = false;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
            _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);

            Cursor = Cursors.Default;
            return status;
        }

        private void GetDetails(string voucherNum)
        {
            oMainDetailList.Clear();
            oMainDetailList = CHNLSVC.Financial.GetChequeVoucherDetail(voucherNum);
            if (oMainDetailList.Count > 0)
            {
                foreach (InterVoucherDetails item in oMainDetailList)
                {
                    item.AccountCodeDescription = txtAccountDesc.Text;

                    if (item.GIVD_EXPE_DIRECT == 1)
                    {
                        item.Credt = Convert.ToDecimal(item.GIVD_EXPE_VAL);
                    }
                    else if (item.GIVD_EXPE_DIRECT == 0)
                    {
                        item.Debit = Convert.ToDecimal(item.GIVD_EXPE_VAL);
                    }
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = oMainDetailList;

                LastDetailLine = oMainDetailList.Max(x => x.GIVD_LINE);

                ClearGrpbox1();
                //txtAccountCode.Focus();
                dtpDate.Enabled = false;
                //txtAccountNo.Enabled = false;
                //txtBankCode.Enabled = false;
                //txtBranckCode.Enabled = false;
                calculateTotals();
            }
        }

        private void upDetails()
        {
            if (valdateHeader())
            {
                if (oMainDetailList.Count > 0)
                {
                    VoucherHeader oHeader = new VoucherHeader();
                    oHeader.Givh_vou_no = txtSearch.Text;
                    oHeader.Givh_com = BaseCls.GlbUserComCode;
                    oHeader.Givh_dt = Convert.ToDateTime(dtpDate.Text).Date;
                    oHeader.Givh_valid_from = Convert.ToDateTime(dtpDate.Text).Date;
                    oHeader.Givh_valid_to = Convert.ToDateTime(dtpDate.Text).Date;
                    oHeader.Givh_emp_cd = txtEPFEFT.Text;
                    oHeader.Givh_val = Convert.ToDecimal(txtDiff.Text);
                    oHeader.Givh_print_stus = 0;
                    oHeader.Givh_act_stus = 2;
                    oHeader.Givh_cre_by = BaseCls.GlbUserID;
                    oHeader.Givh_cre_dt = DateTime.Now;
                    oHeader.Givh_mod_by = BaseCls.GlbUserID;
                    oHeader.Givh_mod_dt = DateTime.Now;
                    oHeader.Givh_val_txt = "";
                    oHeader.Givh_prep_by = BaseCls.GlbUserID;
                    oHeader.Givd_acpt_by = "";
                    oHeader.Givd_auth_by = "";
                    oHeader.Givd_emp_name = "";
                    oHeader.Givd_emp_cat = "";
                    oHeader.Givd_chnl = "";
                    oHeader.Givd_dept = "";
                    oHeader.Givh_vou_tp = "CHQ";
                    oHeader.Givh_bnk_acc = txtAccountNo.Text;
                    oHeader.Givh_bnk_cd = txtBankCode.Tag.ToString();
                    oHeader.Givh_brnch_cd = txtBranckCode.Text;
                    oHeader.Givh_cheque_no = Convert.ToInt32(txtChqNo.Text);
                    oHeader.Givh_period = dtpPeriod.Text;
                    oHeader.Givh_issunupload = (chkNotSunUpload.Checked) ? 1 : 0;
                    oHeader.Givh_printname = txtPrintName.Text;
                    oHeader.Givh_Note = txtNarration.Text;

                    string doc_no = "";

                    int result = CHNLSVC.Financial.UpdateChequeVoucher(oHeader, oMainDetailList);
                    if (result > 0)
                    {
                        MessageBox.Show("Record Updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCEClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated. \n" + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please add details to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool IsCreditorHasDebit()
        {
            bool status = false;
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
            _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);

            foreach (InterVoucherDetails item in oMainDetailList.FindAll(x => x.GIVD_EXPE_DIRECT == 0))
            {
                DataRow[] dr = _result.Select("CODE='" + item.GIVD_ANAL6 + "'" + "AND TYPE = 'S'");
                if (dr.Length > 0)
                {
                    MessageBox.Show(item.GIVD_ANAL6 + " Creditor has a debit entry.\n You cant continue the process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    status = true;
                    break;
                }
            }

            Cursor = Cursors.Default;
            return status;
        }

        private bool validateExcelFile(DataTable dt)
        {
            bool status = false;

            #region Profic Centers

            DataTable _result = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, "");
            string[] pcArray = _result.AsEnumerable().Select(r => r.Field<string>("mpc_cd")).Distinct().ToArray();
            string[] pcExcel = dt.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Field<string>("AccountCode"))).Select(r => r.Field<string>("T0 - BRANCH CODE ")).Distinct().ToArray();

            string[] Others = pcArray.Intersect(pcExcel).ToArray();
            if (Others.Length != pcExcel.Length)
            {
                MessageBox.Show("Invalied Excel file.\nPlease check the Profit Centers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = true;
            }

            #endregion Profic Centers

            #region Department

            List<MasterDepartment> deps = CHNLSVC.General.GetDepartment();
            string[] depArray = deps.Select(x => x.Msdt_cd).ToArray();
            string[] depExcel = dt.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Field<string>("DepartmentCode"))).Select(r => r.Field<string>("DepartmentCode")).Distinct().ToArray();
            string[] otherDeps = depExcel.Intersect(depArray).ToArray();
            if (otherDeps.Length != depExcel.Length)
            {
                MessageBox.Show("Invalied Excel file.\nPlease check the Departments.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = true;
            }

            #endregion Department

            #region AccountCode

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result2 = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
            _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);

            string[] AccountsExcel = dt.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Field<string>("AccountCode"))).Select(r => r.Field<string>("AccountCode")).Distinct().ToArray();
            string[] AccArray = _result.AsEnumerable().Select(r => r.Field<string>("CODE")).Distinct().ToArray();

            string[] commArray = AccArray.Intersect(AccountsExcel).ToArray();
            if (commArray.Length != AccountsExcel.Length)
            {
                MessageBox.Show("Invalied Excel file.\nPlease check the Account Codes", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = true;
            }

            #endregion AccountCode

            return status;
        }

        private bool validateAddItems()
        {
            bool status = false;

            //if (rbnSingleCheck.Checked)
            //{
            //    if (oMainDetailList.Count > 0)
            //    {
            //        string[] AccountCodeList = oMainDetailList.Select(x => x.GIVD_EXPE_CD).ToArray();
            //        DataTable dtAccountCodes = CHNLSVC.Financial.GetChqVoucherAccount(BaseCls.GlbUserComCode, DateTime.Today).Select("CHQ_ISS = 1 AND TYPE = 'S'").CopyToDataTable();
            //        string[] AccountCodeList2 = dtAccountCodes.AsEnumerable().Select(r => r.Field<string>("CODE")).ToArray();
            //        string[] arr = AccountCodeList.Intersect(AccountCodeList2).ToArray();

            //        if (arr.Length > 0)
            //        {
            //            DataRow[] drTemp = dtAccountCodes.Select("CODE = '" + txtAccountCode.Text.Trim() + "' AND CHQ_ISS = 1 AND TYPE = 'S'");
            //            if (drTemp.Length > 0)
            //            {
            //                MessageBox.Show("You cant add more than one cheque print allowed suppliers ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                return status;
            //            }
            //            else
            //            {
            //                status = true;
            //            }
            //        }
            //        return status;
            //    }
            //    else
            //    {
            //        status = true;
            //    }
            //    return status;
            //}
            //else if (rbnMultipleCheques.Checked)
            {
                if (oMainDetailList.Count > 0)
                {
                    if (oMainDetailList.FindAll(x => x.GIVD_ANAL8 == txtInvoiceNo.Text.Trim() && x.isSupplierPrint == 1).Count > 0)
                    {
                        if (oMainDetailList.FindAll(x => x.GIVD_ANAL8 == txtInvoiceNo.Text.Trim() && x.isSupplierPrint == 1)[0].GIVD_EXPE_CD == txtAccountCode.Text)
                        {
                            status = true;
                            return status;
                        }
                    }

                    string[] AccountCodeList = oMainDetailList.Where(x => x.GIVD_ANAL8 == txtInvoiceNo.Text.Trim()).Select(x => x.GIVD_EXPE_CD).ToArray();
                    DataTable dtAccountCodes = CHNLSVC.Financial.GetChqVoucherAccount(BaseCls.GlbUserComCode, DateTime.Today).Select("CHQ_ISS = 1 AND TYPE = 'S'").CopyToDataTable();
                    string[] AccountCodeList2 = dtAccountCodes.AsEnumerable().Select(r => r.Field<string>("CODE")).ToArray();
                    string[] arr = AccountCodeList.Intersect(AccountCodeList2).ToArray();
                    if (arr.Length > 0)
                    {
                        DataRow[] drTemp = dtAccountCodes.Select("CODE = '" + txtAccountCode.Text.Trim() + "' AND CHQ_ISS = 1 AND TYPE = 'S'");
                        if (drTemp.Length > 0)
                        {
                            MessageBox.Show("You cant add more than one cheque print allowed suppliers ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return status;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                    else
                    {
                        status = true;
                    }
                    return status;
                }
                else
                {
                    status = true;
                }
            }
            return status;
        }

        private void UpdateMailList()
        {
            if (oMainDetailList.Count > 0)
            {
                foreach (InterVoucherDetails Item in oMainDetailList)
                {
                    if (Item.isSupplierPrint == 1)
                    {
                        Item.DefaultSupplier = Item.GIVD_EXPE_CD;
                    }
                    else
                    {
                        if (oMainDetailList.FindAll(x => x.GIVD_ANAL8 == Item.GIVD_ANAL8 && x.isSupplierPrint == 1).Count > 0)
                        {
                            Item.DefaultSupplier = oMainDetailList.FindAll(x => x.GIVD_ANAL8 == Item.GIVD_ANAL8 && x.isSupplierPrint == 1)[0].GIVD_EXPE_CD;
                        }
                        else
                        {
                            Item.DefaultSupplier = "";
                        }
                    }
                }
            }
        }

        #endregion Methods

        #endregion Journal Entry

        #region Fund Transfer

        #region Events

        private void btnFTSave_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10092))
            {
                MessageBox.Show("Sorry, You have no permission to save the cheque voucher.\n( Advice: Required permission code :10092)");
                return;
            }

            if (Convert.ToDecimal(txtFTDefference.Text) != 0)
            {
                MessageBox.Show("Voucher is not balanced. Please balance the voucher before save.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }

            if (txtFTVoucherNo.Text.Length > 0)
            {
                if (MessageBox.Show("Are you want to Update", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                updateFundTransferDetails();
            }
            else
            {
                if (MessageBox.Show("Are you want to Save", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                bool isSingleCheque = true;
                if (valdateFundTransferHeader())
                {
                    Cursor = Cursors.WaitCursor;

                    if (oFundTransferDetailList.Count > 0)
                    {
                        VoucherHeader oHeader = new VoucherHeader();
                        oHeader.Givh_com = BaseCls.GlbUserComCode;
                        oHeader.Givh_dt = DateTime.Today.Date;

                        //oHeader.Givh_emp_cd = oFundTransferDetailList.FindAll(x => x.GIVD_EXPE_DIRECT == 0)[0].GIVD_EXPE_CD;
                        oHeader.Givh_emp_cd = txtFTAccNo.Text;
                        oHeader.Givh_val = Convert.ToDecimal("0");
                        oHeader.Givh_print_stus = 0;
                        oHeader.Givh_act_stus = 2;
                        oHeader.Givh_cre_by = BaseCls.GlbUserID;
                        oHeader.Givh_cre_dt = DateTime.Now;
                        oHeader.Givh_mod_by = BaseCls.GlbUserID;
                        oHeader.Givh_mod_dt = DateTime.Now;
                        oHeader.Givh_val_txt = "";
                        oHeader.Givh_prep_by = BaseCls.GlbUserID;
                        oHeader.Givd_acpt_by = "";
                        oHeader.Givd_auth_by = "";
                        oHeader.Givd_emp_name = "";
                        oHeader.Givd_emp_cat = "";
                        oHeader.Givd_chnl = "";
                        oHeader.Givd_dept = "";
                        oHeader.Givh_vou_tp = "FUND";
                        oHeader.Givh_bnk_acc = txtFTAccNo.Text;
                        oHeader.Givh_bnk_cd = txtFTBankCode.Tag.ToString();
                        oHeader.Givh_brnch_cd = txtFTBrachCode.Text;
                        oHeader.Givh_printname = "  "; // set 2 spaces to captuer in common search :(

                        MasterAutoNumber _vouAuto = new MasterAutoNumber();
                        _vouAuto.Aut_cate_tp = "PC";
                        _vouAuto.Aut_direction = 1;
                        _vouAuto.Aut_modify_dt = null;
                        _vouAuto.Aut_number = 0;

                        //if (BaseCls.GlbUserComCode == "AAL")
                        //{
                        //    _vouAuto.Aut_moduleid = "AAVOU";
                        //    _vouAuto.Aut_cate_cd = "AAVOU";
                        //    _vouAuto.Aut_start_char = "AAVOU";
                        //}
                        //else
                        //{
                        //    _vouAuto.Aut_moduleid = "VOU";
                        //    _vouAuto.Aut_cate_cd = "VOU";
                        //    _vouAuto.Aut_start_char = "VOU";
                        //}

                        _vouAuto.Aut_moduleid = BaseCls.GlbUserComCode + "FUNDTR";
                        _vouAuto.Aut_cate_cd = BaseCls.GlbUserComCode + "FUNDTR";
                        _vouAuto.Aut_start_char = BaseCls.GlbUserComCode + "FUNDTR";
                        _vouAuto.Aut_year = null;

                        _vouAuto.Aut_year = null;

                        string doc_no = "";

                        foreach (InterVoucherDetails item in oFundTransferDetailList)
                        {
                            item.GIVD_LINE = oFundTransferDetailList.IndexOf(item) + 1;
                        }
                        int result = CHNLSVC.Financial.SaveJournalVouvher(oHeader, oFundTransferDetailList, _vouAuto, out doc_no, isSingleCheque);

                        if (result > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("Record insert successfully\nDocument No - " + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnFTClear_Click(null, null);
                            return;
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("Process Terminated. \n" + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Please add details to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnFTClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtFTVoucherNo.Clear();
                txtFTAccNo.Clear();
                txtFTAccNoDec.Clear();
                txtFTBankCode.Clear();
                txtFTBrachCode.Clear();
                txtFTProfitCnter.Clear();
                txtFTProfictCenDesc.Clear();
                txtFTInvoice.Clear();
                txtFTDesciption.Clear();
                txtFTDepCode.Clear();
                txtFTDepDesc.Clear();
                txtFTAmount.Clear();
                txtFTEpfEtf.Clear();
                txtFTTax.Clear();
                txtFTAccCode.Clear();
                txtFTAccCodeDesc.Clear();
                lblFTBankDesc.Text = "";
                lblFTBranchDecs.Text = "";
                txtFTTotalDebit.Clear();
                txtFTTotalCredit.Clear();
                txtFTDefference.Clear();
                txtFTExcelPath.Clear();
                txtFTBankCode.Tag = "";
                oFundTransferDetailList.Clear();
                dgvFTDetails.DataSource = null;
                dgvFTInvoiceSummery.DataSource = null;
                dgvFTJournalSummery.DataSource = null;
                //txtFTAccNo.Focus();
                cmbFTCdtDbt.SelectedIndex = 0;
                pnlFTSummery.Visible = false;
                lblFTStatus.Text = "";

                btnFTSave.Enabled = true;
                btnFTApprove.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnFTClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFTVouSch_Click(object sender, EventArgs e)
        {
            txtFTVoucherNo_DoubleClick(null, null);
        }

        private void btnFTAdd_Click(object sender, EventArgs e)
        {
            if (valdateFundTAdd())
            {
                PC = txtFTProfitCnter.Text;
                PCD = txtFTProfictCenDesc.Text;
                ACC = txtFTAccCode.Text;
                ACCD = txtFTAccCodeDesc.Text;
                INVO = txtFTInvoice.Text;
                DESC = txtFTDesciption.Text;
                DEP = txtFTDepCode.Text;
                DEPD = txtFTDepDesc.Text;

                InterVoucherDetails Item = new InterVoucherDetails();
                Item.GIVD_EXPE_CD = txtFTAccCode.Text;
                Item.GIVD_EXPE_DESC = txtFTDesciption.Text;
                Item.GIVD_EXPE_VAL = Convert.ToDecimal(txtFTAmount.Text);
                Item.GIVD_ANAL3 = txtFTBankCode.Tag.ToString();
                Item.GIVD_ANAL4 = txtFTBrachCode.Text;
                Item.GIVD_ANAL5 = txtFTProfitCnter.Text;
                Item.GIVD_ANAL6 = string.Empty;
                Item.GIVD_ANAL7 = txtFTDepCode.Text;
                Item.GIVD_ANAL8 = txtFTInvoice.Text;
                Item.GIVD_ANAL9 = txtFTEpfEtf.Text;
                Item.GIVD_ANAL10 = txtFTTax.Text;
                Item.GVID_DT = DateTime.Today.Date;
                Item.AccountCodeDescription = txtFTAccCodeDesc.Text;
                if (cmbFTCdtDbt.Text.ToUpper() == "Credit".ToUpper())
                {
                    Item.Credt = Convert.ToDecimal(txtFTAmount.Text);
                    Item.GIVD_EXPE_DIRECT = 1;
                }
                else if (cmbFTCdtDbt.Text.ToUpper() == "debit".ToUpper())
                {
                    Item.Debit = Convert.ToDecimal(txtFTAmount.Text);
                    Item.GIVD_EXPE_DIRECT = 0;
                }

                if (txtFTVoucherNo.Text.Length > 0)
                {
                    Item.GIVD_VOU_NO = txtFTVoucherNo.Text;
                    Item.GIVD_SEQ = Convert.ToInt32(txtFTVoucherNo.Tag);
                    Item.GIVD_LINE = LastDetailLine + 1;
                }

                oFundTransferDetailList.Add(Item);

                if (oFundTransferDetailList.Count > 0)
                {
                    dgvFTDetails.DataSource = null;
                    dgvFTDetails.DataSource = oFundTransferDetailList;
                }
                ClearFundDetailGroupbox();

                txtFTAccNo.Enabled = false;
                txtFTAccNoDec.Enabled = false;
                txtFTBankCode.Enabled = false;
                txtFTBrachCode.Enabled = false;

                if (MessageBox.Show("Do you want to add new item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    txtFTProfitCnter.Focus();
                }
                else
                {
                    toolStrip3.Focus();
                    btnFTSave.Select();
                }
            }
            calculateFundTTotals();
        }

        private void txtFTUPload_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFTExcelPath.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFTExcelPath.Clear();
                    txtFTExcelPath.Focus();
                    return;
                }

                System.IO.FileInfo _fileObj = new System.IO.FileInfo(txtFTExcelPath.Text);

                if (_fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFTExcelPath.Focus();
                    return;
                }

                oFundTransferDetailList.Clear();

                string _extension = _fileObj.Extension;
                string _conStr = string.Empty;

                if (_extension.ToUpper() == ".XLS")
                {
                    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFTExcelPath.Text + "; Extended Properties='Excel 8.0;HDR=YES;'";
                }
                else if (_extension.ToUpper() == ".XLSX")
                {
                    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFTExcelPath.Text + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
                }
                else
                {
                    MessageBox.Show("Please Select valid Ms Excel File.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                _conStr = String.Format(_conStr, txtFTExcelPath.Text, "NO");
                OleDbConnection _connExcel = new OleDbConnection(_conStr);
                OleDbCommand _cmdExcel = new OleDbCommand();
                OleDbDataAdapter _oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                _cmdExcel.Connection = _connExcel;
                _connExcel.Open();
                DataTable _dtExcelSchema;
                _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                _connExcel.Close();

                _connExcel.Open();
                _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
                _oda.SelectCommand = _cmdExcel;
                _oda.Fill(_dt);
                _connExcel.Close();
                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                oMainDetailList.Clear();
                string _msg = string.Empty;

                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0]["Period"].ToString().Length > 0)
                    {
                        string[] arrayDate = _dt.Rows[0]["Period"].ToString().Split('/');

                        if (arrayDate.Length > 1)
                        {
                            string mm = arrayDate[0];
                            string yyyy = arrayDate[1];
                            dtpPeriod.Value = Convert.ToDateTime("01/" + mm + '/' + yyyy);
                        }
                    }

                    if (validateExcelFile(_dt))
                    {
                        // MessageBox.Show("Invalied Excel file. Please check the excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        oFundTransferDetailList.Clear();
                        dgvFTDetails.DataSource = null;
                        return;
                    }

                    dtpDate.Value = Convert.ToDateTime(_dt.Rows[0]["Date"].ToString());

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        //Validate EPF/ETF and Tax Code

                        #region Excel Validation

                        double num;
                        if (double.TryParse(_dt.Rows[i]["T7 - EPF/ETF"].ToString(), out num))
                        {
                            MessageBox.Show("Invalied Excel file. Please check the EPF/ETF Code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnFTClear_Click(null, null);
                            return;
                        }

                        double num2;
                        if (double.TryParse(_dt.Rows[i]["T3 - Tax Code"].ToString(), out num2))
                        {
                            MessageBox.Show("Invalied Excel file. Please check the Tax Code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnFTClear_Click(null, null);
                            return;
                        }

                        #endregion Excel Validation

                        if (!string.IsNullOrEmpty(_dt.Rows[i]["AccountCode"].ToString()))
                        {
                            InterVoucherDetails oItem = new InterVoucherDetails();
                            oItem.GIVD_EXPE_CD = txtAccountNo.Text;
                            oItem.GIVD_EXPE_DESC = _dt.Rows[i]["Description"].ToString();
                            oItem.GIVD_EXPE_VAL = Convert.ToDecimal(_dt.Rows[i]["Amount"].ToString());
                            oItem.GIVD_ANAL3 = _dt.Rows[i]["T5 - Bank"].ToString();
                            oItem.GIVD_ANAL4 = _dt.Rows[i]["T6 - Bank Branch"].ToString();
                            oItem.GIVD_ANAL5 = _dt.Rows[i]["T0 - BRANCH CODE "].ToString();
                            oItem.GIVD_EXPE_CD = _dt.Rows[i]["AccountCode"].ToString();
                            oItem.GIVD_ANAL7 = _dt.Rows[i]["DepartmentCode"].ToString();
                            oItem.GIVD_ANAL8 = _dt.Rows[i]["Trn Ref"].ToString();

                            oItem.GIVD_ANAL9 = _dt.Rows[i]["T7 - EPF/ETF"].ToString();
                            oItem.GIVD_ANAL10 = _dt.Rows[i]["T3 - Tax Code"].ToString();

                            oItem.AccountCodeDescription = txtAccountDesc.Text;
                            if (_dt.Rows[i]["D_C"].ToString().ToUpper() == "C".ToUpper())
                            {
                                oItem.Credt = Convert.ToDecimal(_dt.Rows[i]["Amount"].ToString());
                                oItem.GIVD_EXPE_DIRECT = 1;
                            }
                            else if (_dt.Rows[i]["D_C"].ToString().ToUpper() == "D".ToUpper())
                            {
                                oItem.Debit = Convert.ToDecimal(_dt.Rows[i]["Amount"].ToString());
                                oItem.GIVD_EXPE_DIRECT = 0;
                            }
                            oFundTransferDetailList.Add(oItem);
                            isExcelUpload = true;
                            if (oFundTransferDetailList.Count > 0)
                            {
                                dgvFTDetails.DataSource = null;
                                dgvFTDetails.DataSource = oFundTransferDetailList;
                            }
                        }
                    }
                    calculateFundTTotals();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Please check the excel file. \n" + ex.Message, "Warining", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFTBrowse_Click(object sender, EventArgs e)
        {
            txtFTExcelPath.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFTExcelPath.Text = openFileDialog1.FileName;
        }

        private void btnFTSummery_Click(object sender, EventArgs e)
        {
            CalculateFundTransferSummery();
            if (pnlFTSummery.Visible == false)
            {
                pnlFTSummery.Visible = true;
            }
            else
            {
                pnlFTSummery.Visible = false;
            }
        }

        private void btnFTSummeryClose_Click(object sender, EventArgs e)
        {
            if (pnlFTSummery.Visible == false)
            {
                pnlFTSummery.Visible = true;
            }
            else
            {
                pnlFTSummery.Visible = false;
            }
        }

        private void txtFTVoucherNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FundTransferVouchers);
                DataTable _result = CHNLSVC.CommonSearch.GetChequeVouchers(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFTVoucherNo;
                _CommonSearch.ShowDialog();
                txtFTVoucherNo.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFTVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTVoucherNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTAccNo.Focus();
                // txtFTProfitCnter.Focus();
            }
        }

        private void txtFTVoucherNo_Leave(object sender, EventArgs e)
        {
            if (txtFTVoucherNo.Text.Length > 0)
            {
                VoucherHeader oHeader = CHNLSVC.Financial.GetVoucher(BaseCls.GlbUserComCode, txtFTVoucherNo.Text);

                if (oHeader != null && oHeader.Givh_vou_no != null)
                {
                    txtFTVoucherNo.Tag = oHeader.Givh_seq;
                    txtFTAccNo.Text = oHeader.Givh_emp_cd;
                    DataTable bankName = CHNLSVC.Sales.getReturnChequeBank(oHeader.Givh_bnk_cd);
                    txtFTBankCode.Text = bankName.Rows[0]["mbi_id"].ToString();
                    lblFTBankDesc.Text = bankName.Rows[0]["mbi_desc"].ToString();
                    txtFTBankCode.Tag = oHeader.Givh_bnk_cd;
                    txtFTBrachCode.Text = oHeader.Givh_brnch_cd;
                    txtFTBrachCode_Leave(null, null);

                    GetFundTransferDetails(txtFTVoucherNo.Text);

                    btnFTSave.Enabled = true;
                    lblFTStatus.Text = "";
                    btnFTApprove.Enabled = true;
                    btnFTCancel.Enabled = true;

                    if (oHeader.Givh_act_stus == 0 && oHeader.Givh_print_stus == 0)
                    {
                        lblFTStatus.Text = "Canceled";
                    }
                    if (oHeader.Givh_act_stus == 2 && oHeader.Givh_print_stus == 0)
                    {
                        lblFTStatus.Text = "Approval pending.";
                    }
                    if (oHeader.Givh_act_stus == 1 && oHeader.Givh_print_stus == 0)
                    {
                        btnFTSave.Enabled = false;
                        btnFTApprove.Enabled = false;
                        lblFTStatus.Text = "Approved.";
                    }
                    if (oHeader.Givh_act_stus == 1 && oHeader.Givh_print_stus == 1)
                    {
                        btnFTSave.Enabled = false;
                        lblFTStatus.Text = "Printed.";
                        btnFTApprove.Enabled = false;
                        btnFTCancel.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please select correct voucher number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFTVoucherNo.Select();
                }
            }
        }

        private void txtFTAccNo_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtFTAccNo;
            _CommonSearch.ShowDialog();
            txtFTAccNo.Select();
        }

        private void txtFTAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTAccNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTBankCode.Focus();
            }
        }

        private void txtFTAccNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFTAccNo.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, txtFTAccNo.Text);
                if (_result == null || _result.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a correct account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtFTAccNoDec.Text = _result.Rows[0]["Description"].ToString();
                    txtFTBankCode.Text = _result.Rows[0]["MSBA_CD"].ToString();
                    txtFTBankCode_Leave(null, null);

                    txtFTAccCode.Text = txtFTAccNo.Text;
                }
            }
        }

        private void txtFTBankCode_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtFTBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTBankCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTBrachCode.Focus();
            }
        }

        private void txtFTBankCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFTBankCode.Text))
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                if (_result.Rows.Count > 0)
                {
                    if (_result.Select("Code='" + txtFTBankCode.Text + "'").Length > 0)
                    {
                        DataTable dtTemp = _result.Select("Code='" + txtFTBankCode.Text + "'").CopyToDataTable();
                        lblFTBankDesc.Text = dtTemp.Rows[0]["Description"].ToString();
                        txtFTBankCode.Tag = dtTemp.Rows[0]["ID"].ToString();
                        txtFTBrachCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalied Bank code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFTBankCode.Clear();
                        txtFTBankCode.Focus();
                        txtFTBrachCode.Text = "";
                        return;
                    }
                }
            }
        }

        private void txtFTBrachCode_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFTBankCode.Text))
            {
                MessageBox.Show("Please select a bank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankCode.Focus();
                return;
            }
            else
            {
                try
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                    DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtFTBrachCode;
                    _CommonSearch.ShowDialog();
                    txtFTBrachCode.Select();
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
        }

        private void txtFTBrachCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTBrachCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTProfitCnter.Focus();
            }
        }

        private void txtFTBrachCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFTBankCode.Text) && !string.IsNullOrEmpty(txtFTBrachCode.Text))
            {
                DataTable _result = new DataTable();

                _result = CHNLSVC.Sales.Get_buscom_branch_det(txtFTBankCode.Tag.ToString());

                if (_result.Select("mbb_bus_cd='" + txtFTBankCode.Tag.ToString() + "' AND mbb_cd ='" + txtFTBrachCode.Text + "'").Length > 0)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = _result.Select("mbb_bus_cd='" + txtFTBankCode.Tag.ToString() + "' AND mbb_cd ='" + txtFTBrachCode.Text + "'").CopyToDataTable();
                    lblFTBranchDecs.Text = dtTemp.Rows[0]["mbb_desc"].ToString();
                }
                else
                {
                    txtFTBrachCode.Clear();
                    lblFTBranchDecs.Text = "";
                    txtFTBrachCode.Focus();
                    MessageBox.Show("Please select a correct branch code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtFTProfitCnter_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFTProfitCnter;
                _CommonSearch.ShowDialog();
                txtFTProfitCnter.Select();
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

        private void txtFTProfitCnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTProfitCnter_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTAccCode.Focus();
            }
            else if (e.KeyCode == Keys.F3)
            {
                txtFTProfitCnter.Text = PC;
                txtFTProfictCenDesc.Text = PCD;
                txtFTAccCode.Text = ACC;
                txtFTAccCodeDesc.Text = ACCD;
                txtFTInvoice.Text = INVO;
                txtFTDesciption.Text = DESC;
                txtFTDepCode.Text = DEP;
                txtFTDepDesc.Text = DEPD;
                txtFTAmount.Focus();
            }
        }

        private void txtFTProfitCnter_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFTProfitCnter.Text))
            {
                if (!CHNLSVC.General.CheckProfitCenter(BaseCls.GlbUserComCode, txtFTProfitCnter.Text.Trim().ToUpper()))
                {
                    MessageBox.Show("Please check the profit center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFTProfitCnter.Clear();
                    txtFTProfictCenDesc.Clear();
                    txtFTProfitCnter.Focus();
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                txtFTProfictCenDesc.Text = FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(75, txtFTProfitCnter.Text.ToUpper()));
            }
        }

        private void txtFTAccCode_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
            _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);
            _result.Columns.Remove("type");
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtFTAccCode;
            _CommonSearch.ShowDialog();
            txtFTAccCode.Select();
            Cursor = Cursors.Default;
        }

        private void txtFTAccCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFTAccCode.Text.Trim()))
            {
                DataTable _result = new DataTable();
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JournalEntryAccount);
                _result = CHNLSVC.Financial.GetChequeVoucherAccount(_CommonSearch.SearchParams, null, null);
                Cursor = Cursors.Default;
                if (_result.Rows.Count > 0)
                {
                    DataTable Filterd = new DataTable();
                    if (_result.Select("CODE='" + txtFTAccCode.Text.Trim() + "'").Length > 0)
                    {
                        Filterd = _result.Select("CODE='" + txtFTAccCode.Text.Trim() + "'").CopyToDataTable();
                        if (Filterd.Rows.Count > 0)
                        {
                            txtFTAccCodeDesc.Text = Filterd.Rows[0]["NAME"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Account no.", "Cheque Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtFTAccCode.Select();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Account no.", "Cheque Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFTAccCode.Clear();
                        txtFTAccCodeDesc.Clear();
                        txtFTAccCode.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Account no.", "Cheque Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFTAccCode.Clear();
                    txtFTAccCodeDesc.Clear();
                    txtFTAccCode.Focus();
                }
            }
        }

        private void txtFTAccCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTAccCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTInvoice.Focus();
            }
        }

        private void txtFTInvoice_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFTProfitCnter.Text))
            {
                MessageBox.Show("Please select a profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTProfitCnter.Focus();
                return;
            }
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFTInvoice;
                _CommonSearch.ShowDialog();
                txtFTInvoice.Select();
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

        private void txtFTInvoice_Leave(object sender, EventArgs e)
        {
        }

        private void txtFTInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTInvoice_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTDesciption.Focus();
            }
        }

        private void txtFTDesciption_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtFTDesciption_Leave(object sender, EventArgs e)
        {
        }

        private void txtFTDesciption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFTDepCode.Focus();
            }
        }

        private void txtFTDepCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFTDepCode;
                _CommonSearch.ShowDialog();
                txtFTDepCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFTDepCode_Leave(object sender, EventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(txtFTDepCode.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
                    if (_result.Select("CODE='" + txtFTDepCode.Text.Trim() + "'").Length > 0)
                    {
                        DataTable dtFiltered = _result.Select("CODE='" + txtFTDepCode.Text.Trim() + "'").CopyToDataTable();
                        txtFTDepDesc.Text = dtFiltered.Rows[0]["DESCRIPTION"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please select a correct department code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFTDepCode.Clear();
                        txtFTDepCode.Focus();
                        txtFTDepDesc.Clear();
                        return;
                    }
                }
            }
        }

        private void txtFTDepCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtFTDepCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtFTAmount.Focus();
            }
        }

        private void txtFTAmount_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtFTAmount_Leave(object sender, EventArgs e)
        {
            if (txtFTAmount.Text.Length > 0)
            {
                txtFTAmount.Text = (Convert.ToDecimal(txtFTAmount.Text) > 0) ? txtFTAmount.Text : (Convert.ToDecimal(txtFTAmount.Text) * -1).ToString();
            }
            if (txtFTAmount.Text.Length > 0)
            {
                if (Convert.ToDecimal(txtFTAmount.Text) > Convert.ToDecimal(99999999.9999))
                {
                    MessageBox.Show("Please enter valied amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                    txtFTAmount.Select();
                    return;
                }
            }
        }

        private void txtFTAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbFTCdtDbt.Focus();
            }
        }

        private void txtFTAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
      && !char.IsDigit(e.KeyChar)
      && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void cmbFTCdtDbt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFTEpfEtf.Focus();
            }
        }

        private void txtFTEpfEtf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFTTax.Focus();
            }
        }

        private void txtFTEpfEtf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtFTTax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFTAdd.Focus();
            }
        }

        private void txtFTTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void label72_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label72_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = pnlFTSummery.Location.X - xDiff;
                int y = pnlFTSummery.Location.Y - yDiff;
                pnlFTSummery.Location = new Point(x, y);
            }
        }

        private void label72_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void dgvFTDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFTDetails.Rows.Count > 0)
            {
                if (e.ColumnIndex == 11)
                {
                    DialogResult dgr = MessageBox.Show("Do you want to delete this record?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dgr == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    oFundTransferDetailList.Remove(oFundTransferDetailList.Find(x => x.GIVD_ANAL5 == dgvFTDetails.Rows[e.RowIndex].Cells["ProfitCenterFT"].Value.ToString() &&
                                                                x.GIVD_ANAL8 == dgvFTDetails.Rows[e.RowIndex].Cells["InvoiceNoFT"].Value.ToString() &&
                                                                x.GIVD_EXPE_CD == dgvFTDetails.Rows[e.RowIndex].Cells["AccountCodeFT"].Value.ToString() &&
                                                                x.GIVD_EXPE_VAL == Convert.ToDecimal(dgvFTDetails.Rows[e.RowIndex].Cells["AmountFT"].Value.ToString())));

                    calculateFundTTotals();

                    dgvFTDetails.DataSource = null;

                    if (oFundTransferDetailList.Count > 0)
                    {
                        dgvFTDetails.DataSource = oFundTransferDetailList;
                        dgvFTDetails.Refresh();
                    }
                }
            }
        }

        private void btnFTCancel_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10094))
            {
                MessageBox.Show("Sorry, You have no permission to cancel cheque voucher.\n( Advice: Required permission code :10094)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtFTVoucherNo.Text.Length > 0)
            {
                if (MessageBox.Show("Do you want to cancel this voucher?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, txtFTVoucherNo.Text, 0, 0, BaseCls.GlbUserID, DateTime.Now);
                    if (result > 0)
                    {
                        MessageBox.Show("Voucher successfully Canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnFTClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void btnFTApprove_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10093))
            {
                MessageBox.Show("Sorry, You have no permission to approve the cheque voucher.\n( Advice: Required permission code :10093)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtFTVoucherNo.Text.Length > 0)
            {
                if (MessageBox.Show("Do you want to approve this voucher?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, txtFTVoucherNo.Text, 1, 0, BaseCls.GlbUserID, DateTime.Now);
                    if (result > 0)
                    {
                        MessageBox.Show("Voucher approved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnFTClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        #endregion Events

        private bool valdateFundTAdd()
        {
            bool status = false;
            if (string.IsNullOrEmpty(txtFTAccNo.Text))
            {
                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTAccNo.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtFTBankCode.Text))
            {
                MessageBox.Show("Please enter bank code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTBankCode.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtFTBrachCode.Text))
            {
                MessageBox.Show("Please enter branch code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTBrachCode.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtFTProfitCnter.Text))
            {
                MessageBox.Show("Please enter Profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTProfitCnter.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(txtFTAccCode.Text))
            {
                MessageBox.Show("Please enter account code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTAccCode.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtFTInvoice.Text))
            {
                MessageBox.Show("Please enter invoice number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTInvoice.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(txtFTDesciption.Text))
            {
                MessageBox.Show("Please enter description.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTDesciption.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(txtFTDepCode.Text))
            {
                MessageBox.Show("Please enter department code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTDepCode.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(txtFTAmount.Text))
            {
                MessageBox.Show("Please enter amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTAmount.Focus();
                return status;
            }

            if (string.IsNullOrEmpty(cmbFTCdtDbt.Text))
            {
                MessageBox.Show("Please select pay type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbFTCdtDbt.Focus();
                return status;
            }
            //if (string.IsNullOrEmpty(txtFTEpfEtf.Text))
            //{
            //    MessageBox.Show("Please enter EPF/ETF.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtFTEpfEtf.Focus();
            //    return status;
            //}
            //if (string.IsNullOrEmpty(txtFTTax.Text))
            //{
            //    MessageBox.Show("Please enter TAX.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtFTTax.Focus();
            //    return status;
            //}
            status = true;
            return status;
        }

        private void ClearFundDetailGroupbox()
        {
            txtFTProfitCnter.Clear();
            txtFTProfictCenDesc.Clear();
            txtFTInvoice.Clear();
            txtFTDesciption.Clear();
            txtFTDepCode.Clear();
            txtFTDepDesc.Clear();
            txtFTAmount.Clear();
            txtFTEpfEtf.Clear();
            txtFTTax.Clear();
            txtFTAccCode.Clear();
            txtFTAccCodeDesc.Clear();
            lblFTBankDesc.Text = "";
            lblFTBranchDecs.Text = "";
            cmbFTCdtDbt.SelectedIndex = 0;
        }

        private void calculateFundTTotals()
        {
            decimal totDebit = 0;
            decimal totCrdt = 0;
            decimal diff = 0;
            if (oFundTransferDetailList.Count > 0)
            {
                if (oFundTransferDetailList.FindAll(x => x.GIVD_EXPE_DIRECT == 0).Count > 0)
                {
                    totDebit = oFundTransferDetailList.Where(x => x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                }
                if (oFundTransferDetailList.FindAll(x => x.GIVD_EXPE_DIRECT == 1).Count > 0)
                {
                    totCrdt = oFundTransferDetailList.Where(x => x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
                }
            }
            diff = totDebit - totCrdt;
            txtFTDefference.Text = diff.ToString("N");
            txtFTTotalCredit.Text = totCrdt.ToString("N");
            txtFTTotalDebit.Text = totDebit.ToString("N");
        }

        private void CalculateFundTransferSummery()
        {
            #region MyRegion

            //try
            //{
            //    if (oFundTransferDetailList.Count > 0)
            //    {
            //        #region Calculate Invoice Summery

            //        string[] invoiceNumbers = oFundTransferDetailList.Select(x => x.GIVD_ANAL8).Distinct().ToArray();

            //        if (invoiceNumbers.Length > 0)
            //        {
            //            DataTable dtInvoiceSummery = new DataTable();
            //            dtInvoiceSummery.Columns.Add("ISProfitCenter");
            //            dtInvoiceSummery.Columns.Add("ISInvoice");
            //            dtInvoiceSummery.Columns.Add("ISAmount", typeof(decimal));
            //            dtInvoiceSummery.Columns.Add("ISType");

            //            for (int i = 0; i < invoiceNumbers.Length; i++)
            //            {
            //                if (rbnMultipleCheques.Checked)
            //                {
            //                    DataRow dr = dtInvoiceSummery.NewRow();
            //                    dr["ISProfitCenter"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Select(x => x.GIVD_ANAL5).ToArray()[0];
            //                    dr["ISInvoice"] = invoiceNumbers[i].ToString();
            //                    dr["ISAmount"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString() && z.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
            //                    dr["ISType"] = "C";
            //                    if (oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString() && z.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL) > 0)
            //                    {
            //                        dtInvoiceSummery.Rows.Add(dr);
            //                    }
            //                }
            //                else
            //                {
            //                    DataRow dr = dtInvoiceSummery.NewRow();
            //                    dr["ISProfitCenter"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Select(x => x.GIVD_ANAL5).ToArray()[0];
            //                    dr["ISInvoice"] = invoiceNumbers[i].ToString();
            //                    dr["ISAmount"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Sum(x => x.GIVD_EXPE_VAL);
            //                    dr["ISType"] = "";
            //                    if (oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Sum(x => x.GIVD_EXPE_VAL) > 0)
            //                    {
            //                        dtInvoiceSummery.Rows.Add(dr);
            //                    }
            //                }
            //            }

            //            dgvFTInvoiceSummery.DataSource = dtInvoiceSummery;
            //        }

            //        #endregion Calculate Invoice Summery

            //        #region Calculate journal Summery

            //        string[] AccountCodeList = oFundTransferDetailList.Select(x => x.GIVD_EXPE_CD).Distinct().ToArray();

            //        if (AccountCodeList.Length > 0)
            //        {
            //            DataTable dtJournalSummery = new DataTable();
            //            dtJournalSummery.Columns.Add("JSAccountCode");
            //            dtJournalSummery.Columns.Add("JSAccDescription");
            //            dtJournalSummery.Columns.Add("JSAmount", typeof(decimal));
            //            dtJournalSummery.Columns.Add("JSType");

            //            for (int i = 0; i < AccountCodeList.Length; i++)
            //            {
            //                DataRow dr = dtJournalSummery.NewRow();
            //                dr["JSAccountCode"] = AccountCodeList[i].ToString();
            //                dr["JSAccDescription"] = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Select(x => x.AccountCodeDescription).ToArray()[0].ToString();
            //                dr["JSAmount"] = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Sum(x => x.GIVD_EXPE_VAL);
            //                dr["JSType"] = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Select(x => x.GIVD_EXPE_DIRECT).ToArray()[0].ToString();

            //                if (dr["JSType"].ToString() == "1")
            //                {
            //                    dr["JSType"] = "C";
            //                }
            //                else
            //                {
            //                    dr["JSType"] = "D";
            //                }
            //                dtJournalSummery.Rows.Add(dr);
            //            }
            //            dgvFTJournalSummery.DataSource = dtJournalSummery;
            //        }

            //        #endregion Calculate journal Summery
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //============================================

            #endregion MyRegion

            try
            {
                if (oFundTransferDetailList.Count > 0)
                {
                    #region Calculate Invoice Summery

                    string[] invoiceNumbers = oFundTransferDetailList.Select(x => x.GIVD_ANAL8).Distinct().ToArray();

                    if (invoiceNumbers.Length > 0)
                    {
                        DataTable dtInvoiceSummery = new DataTable();
                        dtInvoiceSummery.Columns.Add("ISProfitCenter");
                        dtInvoiceSummery.Columns.Add("ISInvoice");
                        dtInvoiceSummery.Columns.Add("ISAmount", typeof(decimal));
                        dtInvoiceSummery.Columns.Add("ISType");

                        for (int i = 0; i < invoiceNumbers.Length; i++)
                        {
                            //if (rbnMultipleCheques.Checked)
                            //{
                            //    DataRow dr = dtInvoiceSummery.NewRow();
                            //    dr["ISProfitCenter"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Select(x => x.GIVD_ANAL5).ToArray()[0];
                            //    dr["ISInvoice"] = invoiceNumbers[i].ToString();

                            //    decimal totD = 0;
                            //    decimal totC = 0;

                            //    totD = oFundTransferDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                            //    totC = oFundTransferDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);

                            //    dr["ISAmount"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString() && z.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
                            //    dr["ISAmount"] = (totD - totC).ToString("N");

                            //    dr["ISType"] = "C";

                            //    if ((totD - totC) > 0)
                            //    {
                            //        dr["ISType"] = "D";
                            //    }
                            //    else
                            //    {
                            //        dr["ISType"] = "C";
                            //        dr["ISAmount"] = ((totD - totC) * -1).ToString("N");
                            //    }

                            //    if (oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString() && z.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL) > 0)
                            //    {
                            //        dtInvoiceSummery.Rows.Add(dr);
                            //    }
                            //}
                            //else
                            {
                                DataRow dr = dtInvoiceSummery.NewRow();
                                dr["ISProfitCenter"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Select(x => x.GIVD_ANAL5).ToArray()[0];
                                dr["ISInvoice"] = invoiceNumbers[i].ToString();
                                dr["ISAmount"] = oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Sum(x => x.GIVD_EXPE_VAL);
                                dr["ISType"] = "";

                                decimal totD = 0;
                                decimal totC = 0;

                                totD = oFundTransferDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                                totC = oFundTransferDetailList.Where(x => x.GIVD_ANAL8 == invoiceNumbers[i].ToString() && x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);
                                dr["ISAmount"] = (totD - totC).ToString("N");

                                dr["ISType"] = "C";

                                if ((totD - totC) > 0)
                                {
                                    dr["ISType"] = "D";
                                }
                                else
                                {
                                    dr["ISType"] = "C";
                                    dr["ISAmount"] = ((totD - totC) * -1).ToString("N");
                                }

                                if (oFundTransferDetailList.Where(z => z.GIVD_ANAL8 == invoiceNumbers[i].ToString()).Sum(x => x.GIVD_EXPE_VAL) > 0)
                                {
                                    dtInvoiceSummery.Rows.Add(dr);
                                }
                            }
                        }

                        dgvFTInvoiceSummery.DataSource = dtInvoiceSummery;
                    }

                    #endregion Calculate Invoice Summery

                    #region Calculate journal Summery

                    string[] AccountCodeList = oFundTransferDetailList.Select(x => x.GIVD_EXPE_CD).Distinct().ToArray();

                    if (AccountCodeList.Length > 0)
                    {
                        DataTable dtJournalSummery = new DataTable();
                        dtJournalSummery.Columns.Add("JSAccountCode");
                        dtJournalSummery.Columns.Add("JSAccDescription");
                        dtJournalSummery.Columns.Add("JSAmount", typeof(decimal));
                        dtJournalSummery.Columns.Add("JSType");

                        for (int i = 0; i < AccountCodeList.Length; i++)
                        {
                            DataRow dr = dtJournalSummery.NewRow();
                            dr["JSAccountCode"] = AccountCodeList[i].ToString();
                            dr["JSAccDescription"] = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Select(x => x.AccountCodeDescription).ToArray()[0].ToString();
                            dr["JSAmount"] = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Sum(x => x.GIVD_EXPE_VAL);

                            decimal totD = 0;
                            decimal totC = 0;

                            totD = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString() && x.GIVD_EXPE_DIRECT == 0).Sum(x => x.GIVD_EXPE_VAL);
                            totC = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString() && x.GIVD_EXPE_DIRECT == 1).Sum(x => x.GIVD_EXPE_VAL);

                            dr["JSAmount"] = (totD - totC).ToString("N");

                            dr["JSType"] = oFundTransferDetailList.Where(x => x.GIVD_EXPE_CD == AccountCodeList[i].ToString()).Select(x => x.GIVD_EXPE_DIRECT).ToArray()[0].ToString();

                            if ((totD - totC) > 0)
                            {
                                dr["JSType"] = "D";
                            }
                            else
                            {
                                dr["JSType"] = "C";
                                dr["JSAmount"] = ((totD - totC) * -1).ToString("N");
                            }
                            dtJournalSummery.Rows.Add(dr);
                        }
                        dgvFTJournalSummery.DataSource = dtJournalSummery;
                    }

                    #endregion Calculate journal Summery
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool valdateFundTransferHeader()
        {
            bool status = false;
            if (string.IsNullOrEmpty(txtFTAccNo.Text))
            {
                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTAccNo.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtFTBankCode.Text))
            {
                MessageBox.Show("Please enter bank code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTBankCode.Focus();
                return status;
            }
            if (string.IsNullOrEmpty(txtFTBrachCode.Text))
            {
                MessageBox.Show("Please enter branch code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTBrachCode.Focus();
                return status;
            }
            status = true;
            return status;
        }

        private void updateFundTransferDetails()
        {
            if (valdateFundTransferHeader())
            {
                if (oFundTransferDetailList.Count > 0)
                {
                    VoucherHeader oHeader = new VoucherHeader();
                    oHeader.Givh_vou_no = txtFTVoucherNo.Text;
                    oHeader.Givh_com = BaseCls.GlbUserComCode;
                    oHeader.Givh_dt = DateTime.Today.Date;
                    oHeader.Givh_emp_cd = txtFTAccNo.Text;
                    oHeader.Givh_val = Convert.ToDecimal(txtDiff.Text);
                    oHeader.Givh_print_stus = 0;
                    oHeader.Givh_act_stus = 2;
                    oHeader.Givh_cre_by = BaseCls.GlbUserID;
                    oHeader.Givh_cre_dt = DateTime.Now;
                    oHeader.Givh_mod_by = BaseCls.GlbUserID;
                    oHeader.Givh_mod_dt = DateTime.Now;
                    oHeader.Givh_val_txt = "";
                    oHeader.Givh_prep_by = BaseCls.GlbUserID;
                    oHeader.Givd_acpt_by = "";
                    oHeader.Givd_auth_by = "";
                    oHeader.Givd_emp_name = "";
                    oHeader.Givd_emp_cat = "";
                    oHeader.Givd_chnl = "";
                    oHeader.Givd_dept = "";
                    oHeader.Givh_vou_tp = "FUND";
                    oHeader.Givh_bnk_acc = txtFTAccNo.Text;
                    oHeader.Givh_bnk_cd = txtFTBankCode.Tag.ToString();
                    oHeader.Givh_brnch_cd = txtFTBrachCode.Text;
                    oHeader.Givh_printname = "  ";

                    string doc_no = "";

                    int result = CHNLSVC.Financial.UpdateChequeVoucher(oHeader, oFundTransferDetailList);
                    if (result > 0)
                    {
                        MessageBox.Show("Record Updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnFTClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated. \n" + doc_no, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please add details to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GetFundTransferDetails(string voucherNum)
        {
            oFundTransferDetailList.Clear();
            oFundTransferDetailList = CHNLSVC.Financial.GetChequeVoucherDetail(voucherNum);
            if (oFundTransferDetailList.Count > 0)
            {
                foreach (InterVoucherDetails item in oFundTransferDetailList)
                {
                    item.AccountCodeDescription = txtAccountDesc.Text;

                    if (item.GIVD_EXPE_DIRECT == 1)
                    {
                        item.Credt = Convert.ToDecimal(item.GIVD_EXPE_VAL);
                    }
                    else if (item.GIVD_EXPE_DIRECT == 0)
                    {
                        item.Debit = Convert.ToDecimal(item.GIVD_EXPE_VAL);
                    }
                }

                dgvFTDetails.DataSource = null;
                dgvFTDetails.DataSource = oFundTransferDetailList;

                LastDetailLine = oFundTransferDetailList.Max(x => x.GIVD_LINE);
                calculateFundTTotals();
            }
        }

        #endregion Fund Transfer

        #region Cheque Print

        private void btnCPView_Click(object sender, EventArgs e)
        {
            LoadPrintVouchers();
            chkAllocate.Checked = false;
            chkPrint.Checked = false;
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatus.Checked)
            {
                rbnActive.Enabled = true;
                rbnCancel.Enabled = true;
            }
            else
            {
                rbnActive.Enabled = false;
                rbnCancel.Enabled = false;
            }
        }

        private void btnCPAllocate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartingNo.Text))
            {
                MessageBox.Show("Please enter starting cheque number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartingNo.Focus();
                return;
            }
            if (Convert.ToInt32(lblVouCountTOAllocate.Text) == 0)
            {
                MessageBox.Show("Please selete cheques to allocate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to allocate this vouchers", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            DataTable dt = CHNLSVC.Inventory.Get_all_manual_docs_by_type(BaseCls.GlbUserComCode, txtAccountNoCP.Text, "PRN-CHQ");

            if (dt.Rows.Count > 0)
            {
                DataRow[] drTemp = dt.Select("Current_Number = '" + txtStartingNo.Text + "'");

                if (drTemp.Length > 0)
                {
                    DataTable dtTemp = drTemp.CopyToDataTable();

                    string currentNo = dtTemp.Rows[0]["Current_Number"].ToString();
                    string lastNo = dtTemp.Rows[0]["Last_Number"].ToString();

                    int AllocateLength = Convert.ToInt32(lastNo) - Convert.ToInt32(currentNo);

                    if (Convert.ToInt32(lblVouCountTOAllocate.Text) > AllocateLength + 1)
                    {
                        MessageBox.Show("Cheque book is completed. Please select " + AllocateLength.ToString() + " Cheuqs only", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < grvHeader.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(grvHeader.Rows[i].Cells["chkChq"].Value) == true)
                            {
                                grvHeader.Rows[i].Cells["ChequeNoCP"].Value = txtStartingNo.Text;

                                string voucherNum = grvHeader.Rows[i].Cells["Column1"].Value.ToString();

                                int result2 = CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, txtAccountNoCP.Text, "PRN-CHQ", Convert.ToInt32(txtStartingNo.Text), grvHeader.Rows[i].Cells["Column1"].Value.ToString());
                                //should update voucher
                                int result3 = CHNLSVC.Financial.UpdateVoucherChequeNumber(BaseCls.GlbUserComCode, txtStartingNo.Text, BaseCls.GlbUserID, voucherNum);
                                if (result3 > 0 && result2 > 0)
                                {
                                    txtStartingNo.Text = (Convert.ToInt32(txtStartingNo.Text) + 1).ToString();
                                }
                                else
                                {
                                    MessageBox.Show("process terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valied starting cheque number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStartingNo.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter valied starting cheque number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartingNo.Focus();
                return;
            }
        }

        private void grvHeader_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvHeader.IsCurrentCellDirty)
            {
                grvHeader.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void grvHeader_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int count = 0;
            int AllocateCount = 0;
            try
            {
                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    if (grvHeader.Rows[i].Cells["Select"].Value != null && Convert.ToBoolean(grvHeader.Rows[i].Cells["Select"].Value) == true)
                    {
                        count = count + 1;
                    }
                    if (grvHeader.Rows[i].Cells["chkChq"].Value != null && Convert.ToBoolean(grvHeader.Rows[i].Cells["chkChq"].Value) == true)
                    {
                        AllocateCount = AllocateCount + 1;
                    }

                    if (grvHeader.Rows[i].Cells[9].Value.ToString() == "App Pending" || grvHeader.Rows[i].Cells[9].Value.ToString() == "Canceled")
                    {
                        btnPrintCP.Enabled = false;
                    }
                    else
                    {
                        btnPrintCP.Enabled = true;
                    }
                }
                lblSelectedVoucherCount.Text = count.ToString();
            }
            catch (Exception ex)
            {
                lblSelectedVoucherCount.Text = ex.Message;
            }

            lblSelectedVoucherCount.Text = count.ToString();
            lblVouCountTOAllocate.Text = AllocateCount.ToString();
        }

        private void txtStartingNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtStartingNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNoCP.Text))
            {
                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtChqNo.Text))
            {
                if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, txtAccountNoCP.Text, "PRN-CHQ", Convert.ToInt32(txtStartingNo.Text)))
                {
                    MessageBox.Show("Please enter a valied cheque number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStartingNo.Clear();
                    txtStartingNo.Focus();
                    return;
                }
            }
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNoCP;
            _CommonSearch.ShowDialog();
            txtAccountNoCP.Select();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                textBox2_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtCPVoucherNum.Focus();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNoCP.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, txtAccountNoCP.Text);
                if (_result == null || _result.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a correct account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccountNoCP.Focus();
                }
                else
                {
                    txtAccountNameCP.Text = _result.Rows[0]["Description"].ToString();
                }
            }
        }

        private void btnClearCp_Click(object sender, EventArgs e)
        {
            txtAccountNoCP.Clear();
            txtAccountNameCP.Clear();
            chkStatus.Checked = false;
            dateTimePickerFrom.Value = DateTime.Now;
            dateTimePickerTo.Value = DateTime.Now;
            txtStartingNo.Clear();
            grvHeader.Rows.Clear();
            txtCPVoucherNum.Clear();

            dateTimePickerFrom.Value = Convert.ToDateTime("01-01-2014");

            chkAllocate.Checked = false;
            chkPrint.Checked = false;

            lblSelectedVoucherCount.Text = "00";
            lblVouCountTOAllocate.Text = "00";

            btnPrintCP.Enabled = true;
        }

        private void chkAllocate_CheckedChanged(object sender, EventArgs e)
        {
            grvHeader_CurrentCellDirtyStateChanged(null, null);

            if (chkAllocate.Checked)
            {
                chkPrint.Checked = false;
                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    if (grvHeader.Rows[i].Cells["ChequeNoCP"].Value != null)
                    {
                        if (grvHeader.Rows[i].Cells["ChequeNoCP"].Value.ToString() == "-1")
                        {
                            grvHeader.Rows[i].Cells["chkChq"].Value = true;
                        }
                    }
                    else
                    {
                        grvHeader.Rows[i].Cells["chkChq"].Value = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    if (grvHeader.Rows[i].Cells["ChequeNoCP"].Value != null)
                    {
                        if (grvHeader.Rows[i].Cells["ChequeNoCP"].Value.ToString() == "-1")
                        {
                            grvHeader.Rows[i].Cells["chkChq"].Value = false;
                        }
                    }
                    else
                    {
                        grvHeader.Rows[i].Cells["chkChq"].Value = false;
                    }
                }
            }
            grvHeader_CellMouseUp(null, null);
        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrint.Checked)
            {
                chkAllocate.Checked = false;

                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    grvHeader.Rows[i].Cells["Select"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    grvHeader.Rows[i].Cells["Select"].Value = false;
                }
            }
            grvHeader_CellMouseUp(null, null);
        }

        private void btnPrintCP_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10095))
            {
                MessageBox.Show("Sorry, You have no permission to print cheque voucher.\n( Advice: Required permission code :10095)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int result = 0;
            string printedReports = string.Empty;
            if (Convert.ToInt32(lblSelectedVoucherCount.Text) > 0)
            {
                List<string> _vouList = new List<string>();

                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    if (grvHeader.Rows[i].Cells["ChequeNoCP"].Value != null)
                    {
                        if (Convert.ToBoolean(grvHeader.Rows[i].Cells["Select"].Value))
                        {
                            if (grvHeader.Rows[i].Cells["actStatus"].Value.ToString() == "1")
                            {
                                string voucherNum = grvHeader.Rows[i].Cells["Column1"].Value.ToString();
                                printedReports += voucherNum + "|";
                                _vouList.Add(voucherNum);
                                result = CHNLSVC.Financial.CancelVoucher(BaseCls.GlbUserComCode, voucherNum, 1, 1, BaseCls.GlbUserID, DateTime.Now);
                            }
                        }
                    }
                }

                if (MessageBox.Show("Is this cheque payee only?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {// Nadeeka 13-01-2014
                    BaseCls.GlbRecType = "Y";
                }
                else
                {
                    BaseCls.GlbRecType = "N";
                }


                if (_vouList.Count > 0)
                {
                    printVoucherList(_vouList);

                    if (result > 0)
                    {
                        MessageBox.Show("Process successfully completed. \n Printed vouchers :- " + printedReports, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClearCp_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select vouchers to print.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtCPVoucherNum_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChequeVouchers);
                DataTable _result = CHNLSVC.CommonSearch.GetChequeVouchers(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCPVoucherNum;
                _CommonSearch.ShowDialog();
                txtCPVoucherNum.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFTSearchVoucher_Click(object sender, EventArgs e)
        {
            txtCPVoucherNum_DoubleClick(null, null);
        }

        private void txtCPVoucherNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip4.Focus();
                btnCPView.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtCPVoucherNum_DoubleClick(null, null);
            }
        }

        private void grvHeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grvHeader.Rows[e.RowIndex].Cells["ChequeNoCP"].Value == null)
                {
                    grvHeader.Rows[e.RowIndex].Cells["Select"].Value = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadPrintVouchers()
        {
            if (String.IsNullOrEmpty(txtAccountNoCP.Text))
            {
                MessageBox.Show("Please enter account number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNoCP.Focus();
                return;
            }

            int Status = 3;
            if (chkStatus.Checked)
            {
                if (rbnActive.Checked)
                {
                    Status = 1;
                }
                else
                {
                    Status = 0;
                }
            }

            string vou = "";
            if (txtCPVoucherNum.Text == "")
                vou = "N/A";
            else
                vou = txtCPVoucherNum.Text;

            List<VoucherHeader> _list = CHNLSVC.Financial.GetVoucherSearchToPrint(Status, 0, BaseCls.GlbUserComCode, dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.Date, vou);//.FindAll(X => X.Givh_bnk_acc == txtAccountNoCP.Text);
            List<VoucherHeader> _listNew = new List<VoucherHeader>();

            if (_list != null && _list.Count > 0)
            {
                if (_list.FindAll(X => X.Givh_bnk_acc == txtAccountNoCP.Text && X.Givh_print_stus == 0 && X.Givh_emp_cd != "").Count > 0)
                {
                    _listNew = _list.FindAll(X => X.Givh_bnk_acc == txtAccountNoCP.Text && X.Givh_print_stus == 0 && X.Givh_emp_cd != "");
                }
            }

            if (_listNew.Count > 0)
            {
                grvHeader.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _listNew;
                grvHeader.DataSource = _source;
                ModifyGrid();
            }
            else
            {
                MessageBox.Show("No Vouchers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNoCP.Clear();
                txtAccountNoCP.Focus();
                btnClearCp_Click(null, null);
            }
        }

        private void ModifyGrid()
        {
            if (grvHeader.Rows.Count > 0)
            {
                for (int i = 0; i < grvHeader.Rows.Count; i++)
                {
                    if (grvHeader.Rows[i].Cells["ChequeNoCP"].Value.ToString() != "-1")
                    {
                        DataGridViewCheckBoxCell chkchecking = grvHeader.Rows[i].Cells["chkChq"] as DataGridViewCheckBoxCell;
                        chkchecking.ReadOnly = true;
                    }
                    else
                    {
                        DataGridViewTextBoxCell chqNo = grvHeader.Rows[i].Cells["ChequeNoCP"] as DataGridViewTextBoxCell;
                        chqNo.Value = "";
                    }

                    if (grvHeader.Rows[i].Cells["actStatus"].Value.ToString() == "0")
                    {
                        grvHeader.Rows[i].Cells["Status"].Value = "Canceled";
                    }
                    else if (grvHeader.Rows[i].Cells["actStatus"].Value.ToString() == "1")
                    {
                        grvHeader.Rows[i].Cells["Status"].Value = "Approved";
                    }
                    else if (grvHeader.Rows[i].Cells["actStatus"].Value.ToString() == "2")
                    {
                        grvHeader.Rows[i].Cells["Status"].Value = "App Pending";
                    }
                }
            }
            grvHeader_CurrentCellDirtyStateChanged(null, null);
        }

        private void printVoucher(string voucherNumber)
        {
            //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            //BaseCls.GlbReportName = "CheckPrinting.rpt";
            //BaseCls.GlbReportHeading = "Check Printing Voucher";
            //BaseCls.GlbReportDoc = voucherNumber;
            //_view.Show();
            //_view = null;


            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            clsSalesRep objsales = new clsSalesRep();
            BaseCls.GlbReportName = "ChequePrinting1.rpt";
            BaseCls.GlbReportHeading = "Cheque Printing Voucher";
            //BaseCls.GlbReportDoc = "VOU-10002";
            List<string> _vouList = new List<string>();
            _vouList.Add(voucherNumber);
            objsales.getVoudets(_vouList);
            _view.Show();
            _view = null;

            //=========== Voucher Print
            // Nadeeka 13-01-2015
            Reports.Sales.ReportViewer _view_vou = new Reports.Sales.ReportViewer();
      
            BaseCls.GlbReportName = "ChequePrinting.rpt";
            BaseCls.GlbReportHeading = "Cheque Printing Voucher";
            
            _view_vou.Show();
            _view_vou = null;

        }

        private void printVoucherList(List<string> _vouList)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            clsSalesRep objsales = new clsSalesRep();
            BaseCls.GlbReportName = "ChequePrinting1.rpt";
            BaseCls.GlbReportHeading = "Check Printing Voucher";
            objsales.getVoudets(_vouList);
            _view.Show();
            _view = null;

            //=========== Voucher Print
            // Nadeeka 13-01-2015
            Reports.Sales.ReportViewer _view_vou = new Reports.Sales.ReportViewer();

            BaseCls.GlbReportName = "ChequePrinting.rpt";
            BaseCls.GlbReportHeading = "Cheque Printing Voucher";

            _view_vou.Show();
            _view_vou = null;
        }

        #endregion Cheque Print

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10109))    //kapila 31/8/2015
            {
                MessageBox.Show("Sorry, You have no permission for update the end date!\n( Advice: Required permission code :10109)", "Cash Voucher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(string.IsNullOrEmpty(txtRef.Text))
            {
                MessageBox.Show("Please select the voucher number", "Cash Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.Financial.UPDATE_VOUCHER_END(BaseCls.GlbUserComCode,txtRef.Text,dtValid.Value.Date,BaseCls.GlbUserID);

            if(_eff==1)
            {
                MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            else
            {
                MessageBox.Show("Not updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkValid_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10109))    //kapila 31/8/2015
            {
                MessageBox.Show("Sorry, You have no permission for update the end date!\n( Advice: Required permission code :10109)", "Cash Voucher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (chkValid.Checked == true)
                dtValid.Enabled = true;
            else
                dtValid.Enabled = false;
        }
    }
}