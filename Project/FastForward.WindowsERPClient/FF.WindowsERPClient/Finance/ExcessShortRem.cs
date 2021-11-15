using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FF.WindowsERPClient.Reports.Sales;


using FF.BusinessObjects;
using System.Globalization;

namespace FF.WindowsERPClient.Finance
{
    public partial class ExcessShortRem : Base
    {
        #region properties

        string ExcessShortId;

        #endregion

        public ExcessShortRem()
        {
            InitializeComponent();
            ExcessShortId = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes) {
                this.Close();
            }
        }

        private void ExcessShortRem_Load(object sender, EventArgs e)
        {
            try
            {
                gvBal.AutoGenerateColumns = false;
                GvExcRemSum.AutoGenerateColumns = false;
                GvOthRem.AutoGenerateColumns = false;
                gvSettle.AutoGenerateColumns = false;
                BindSettlementData(BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1));
                BindBalanceData(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                BindRemitTypes();
                process_month_change();
                BindExcessShortOthRemData();
                dateTimePickerMonthYear.Select();
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

        private void dateTimePickerMonthYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                //if (dateTimePickerMonthYear.Value > _date) {
                //    MessageBox.Show("Your selected date is higher than today", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                BindSettlementData(BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1));
                calc_Balance();
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

        private void dateTimePickerMonthYearProcess_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                process_month_change();
                BindExcessShortOthRemData();
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayButtons();
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

        #region methods

        protected void process_month_change()
        {
            /*
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            if (_date.Day == 31)
            {
                if (dateTimePickerMonthYearProcess.Value.Month != 2)
                {
                    dateTimePickerDate.Value = new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 30);
                }
                else {
                    dateTimePickerDate.Value = new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthYearProcess.Value.Year,dateTimePickerMonthYearProcess.Value.Month));
                }
            }
            else
            {
                dateTimePickerDate.Value = new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, _date.Day);
            }
             */
            DateTime _procesMonth = new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 1);
            lblNote.Text = "This will be cancell all finalization from " + (_procesMonth).ToShortDateString();

            Decimal _weekNo = 0;
            int K = CHNLSVC.General.GetWeek(dateTimePickerDate.Value.Date, out _weekNo, BaseCls.GlbUserComCode);
            txtWeek.Text = _weekNo.ToString();

            string _excsStatus = "";
            string _ID = "";
            DataTable DT = CHNLSVC.Financial.GetExcsStatus(BaseCls.GlbUserDefProf, _procesMonth, out _excsStatus, out _ID);
            switch (_excsStatus)
            {
                case "F":
                    {
                        btnProcess.Enabled = false;
                        btnConfirm.Enabled = false;
                        btnAdd.Enabled = false;
                        btnDelete1.Enabled = false;
                        lblStatus.Text = "CONFIRMED";
                        ExcessShortId = _ID;
                        break;
                    }
                case "P":
                    {
                        btnProcess.Enabled = true;
                        btnConfirm.Enabled = true;
                        btnAdd.Enabled = true;
                        btnDelete1.Enabled = true;
                        lblStatus.Text = "PENDING";
                        ExcessShortId = _ID;
                        break;
                    }
                default:
                    {
                        lblStatus.Text = "";
                        btnAdd.Enabled = true;
                        btnDelete1.Enabled = true;
                        btnProcess.Enabled = true;
                        ExcessShortId = BaseCls.GlbUserName + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                        btnConfirm.Enabled = false;
                        break;
                    }
            }
        }

        protected void calc_Balance()
        {
            Decimal _excsBal = 0;
            DataTable DT = CHNLSVC.Financial.GetExcessBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1), out _excsBal);
            txtBalance.Text = _excsBal.ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        private void Add()
        {
            if (string.IsNullOrEmpty(txtSetAmt.Text))
            {
                MessageBox.Show("Enter the Amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSetAmt.Focus();
                return;
            }

            //check whether acc period is finalized.
            ExcessShortTrna _excsShortRem = new ExcessShortTrna();

            _excsShortRem.Exss_com = BaseCls.GlbUserComCode;
            _excsShortRem.Exss_pc = BaseCls.GlbUserDefProf;
            _excsShortRem.Exss_mnth = new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1);
            _excsShortRem.Exss_tp = "S";
            _excsShortRem.Exss_txn_dt = dateTimePickerSettleDate.Value.Date;
            _excsShortRem.Exss_desc = "Month of " + dateTimePickerMonthYear.Value.ToString("MMMM") + " " + dateTimePickerMonthYear.Value.Year;
            if (Convert.ToDecimal(txtBalance.Text) > 0)
            {
                _excsShortRem.Exss_amt = Convert.ToDecimal(txtSetAmt.Text) * -1;
            }
            else
            {
                _excsShortRem.Exss_amt = Convert.ToDecimal(txtSetAmt.Text);
            }
            _excsShortRem.Exss_user = BaseCls.GlbUserName;
            _excsShortRem.Exss_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _excsShortRem.Exss_rem = txtSetAmt.Text;
            _excsShortRem.EXSS_IS_MEMO = Convert.ToInt32(chkMemoSet.Checked);   //17/9/2014
            _excsShortRem.EXSS_IS_CLAIM = Convert.ToInt32(optClaim.Checked);   //17/9/2014
            _excsShortRem.Exss_remarks = txtRemark.Text;

            Int32 row_aff = CHNLSVC.Financial.SaveExcessTrans(_excsShortRem);

            BindSettlementData(BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1));
            BindBalanceData(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

            clearSettlement();
            MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void DisplayButtons()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                btnPrint.Visible = false;
                btnProcess.Visible = false;
                btnConfirm.Visible = false;

                toolStripSeparator1.Visible = false;
                toolStripSeparator3.Visible = false;
                toolStripSeparator4.Visible = false;

                dateTimePickerMonthYear.Select();
            }
            else
            {
                btnProcess.Visible = true;
                btnPrint.Visible = true;
                btnConfirm.Visible = true;

                toolStripSeparator1.Visible = true;
                toolStripSeparator3.Visible = true;
                toolStripSeparator4.Visible = true;
                dateTimePickerMonthYearProcess.Select();
            }

        }

        #endregion

        #region clear events

        protected void clearSettlement()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtRemark.Text = "";
            txtSetAmt.Text = "";
            dateTimePickerMonthYear.Value = _date;
            dateTimePickerSettleDate.Value = DateTime.Now;
            chkMemoSet.Checked = false;
        }

        protected void clearProcess()
        {
            txtAmt.Text = "";
            txtRem.Text = "";
            chkMemo.Checked = false;
        }

        private void ClearAll()
        {

            if (tabControl1.SelectedIndex == 0)
            {
                txtBalance.Text = "";
                txtRemark.Text = "";
                txtSetAmt.Text = "";

                gvBal.AutoGenerateColumns = false;
                gvSettle.AutoGenerateColumns = false;
                BindSettlementData(BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1));
                BindBalanceData(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                process_month_change();
            }
            else
            {
                GvExcRemSum.AutoGenerateColumns = false;
                GvOthRem.AutoGenerateColumns = false;
                BindExcessShortOthRemData();
                BindRemitTypes();
            }
        }

        #endregion

        #region databind events

        protected void BindExcesShortData(string _com, string _pc, DateTime _mnth)
        {
            GvExcRemSum.DataSource = CHNLSVC.Financial.GetExcsShortGridData(_com, _pc, _mnth);
        }

        protected void BindExcessShortOthRemData()
        {
            GvOthRem.DataSource = CHNLSVC.Financial.GetExcsShortOthRemGridData(ExcessShortId);
        }

        protected void BindBalanceData(string _com, string _pc)
        {
            gvBal.DataSource = CHNLSVC.Financial.GetExcsBalanceDridData(_com, _pc);

            Decimal _excsBal = 0;
            DataTable DT = CHNLSVC.Financial.GetExcessBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1), out _excsBal);
            txtBalance.Text = _excsBal.ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        protected void BindSettlementData(string _pc, DateTime _mnth)
        {
            gvSettle.DataSource = CHNLSVC.Financial.GetExcsSettlementDridData(_pc, _mnth);
        }

        protected void BindRemitTypes()
        {
            Int16 _isExcs = 0;
            if (rdoExcess.Checked == true)
            {
                _isExcs = 1;
            }
            cmbRemitanceType.DataSource = null;
            //select type
            ExcessRemitTypes ex = new ExcessRemitTypes();
            ex.Esr_desc = "--Select Remit Type--";
            ex.Esr_cd = "-1";
            List<ExcessRemitTypes> temp = new List<ExcessRemitTypes>();
            temp.Add(ex);
            temp.AddRange(CHNLSVC.Financial.GetExcsShortRemitType(_isExcs));
            cmbRemitanceType.DataSource = temp;
            cmbRemitanceType.DisplayMember = "esr_desc";
            cmbRemitanceType.ValueMember = "esr_cd";
        }

        #endregion

        #region button click events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Add();
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

        private void btnDelate_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = CHNLSVC.Financial.DeleteExcsSettlement(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1), dateTimePickerSettleDate.Value.Date);

                BindSettlementData(BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1));
                BindBalanceData(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                clearSettlement();
                MessageBox.Show("Successfully Deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void btnAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal _excsAmt = 0;
                Decimal _shortAmt = 0;
                ExcessShortDet _excsShortRemDet = new ExcessShortDet();

                if (lblStatus.Text == "CONFIRMED") {
                    MessageBox.Show("Already Confirmed,can not add");
                    return;
                }

                if (cmbRemitanceType.SelectedValue.ToString().Equals("-1"))
                {
                    MessageBox.Show("Select remittance type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRemitanceType.Focus();
                    return;
                }
                decimal val;
                if (!decimal.TryParse(txtAmt.Text, out val))
                {
                    MessageBox.Show("Please enter Amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmt.Focus();
                    return;
                }

                if (rdoExcess.Checked == true)
                {
                    _excsAmt = Convert.ToDecimal(txtAmt.Text);
                    _shortAmt = 0;
                }
                else
                {
                    _excsAmt = 0;
                    _shortAmt =(-1)* Convert.ToDecimal(txtAmt.Text); ;
                }
                _excsShortRemDet.Esrd_id = ExcessShortId;
                _excsShortRemDet.Esrd_week = Convert.ToInt32(txtWeek.Text);
                _excsShortRemDet.Esrd_line = 1;
                _excsShortRemDet.Esrd_sec = "10";
                _excsShortRemDet.Esrd_cd = cmbRemitanceType.SelectedValue.ToString();
                _excsShortRemDet.Esrd_desc = cmbRemitanceType.Text.ToString();
                _excsShortRemDet.Esrd_invs = "";
                _excsShortRemDet.Esrd_exces = _excsAmt;
                _excsShortRemDet.Esrd_short = _shortAmt;
                _excsShortRemDet.Esrd_fixed = false;
                _excsShortRemDet.Esrd_is_memo = Convert.ToInt32(chkMemo.Checked);   //12/9/2014
                _excsShortRemDet.Esrd_cre_by = BaseCls.GlbUserID;   //24/12/2014
                _excsShortRemDet.Esrd_rem = txtRem.Text;        //22/12/2015

                Int32 row_aff = CHNLSVC.Financial.SaveExcessDetails(_excsShortRemDet);

                BindExcessShortOthRemData();

                clearProcess();
                MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnDelete1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRemitanceType.SelectedValue.ToString().Equals("-1"))
                {
                    MessageBox.Show("Select remittance type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Int32 Z = CHNLSVC.Financial.DeleteExcsShortOthRem(ExcessShortId, Convert.ToInt32(txtWeek.Text), "10", cmbRemitanceType.SelectedValue.ToString());
                BindExcessShortOthRemData();

                clearProcess();
                MessageBox.Show("Successfully Deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isNew = false;
                if (lblStatus.Text == "CONFIRMED")
                {
                    MessageBox.Show("Already confirmed this month.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (lblStatus.Text == "")
                {
                    _isNew = true;
                }

                if (_isNew == true)
                {
                    ExcessShortHeader _excsShortHdr = new ExcessShortHeader();
                    _excsShortHdr.Esrh_id = ExcessShortId;
                    _excsShortHdr.Esrh_pc = BaseCls.GlbUserDefProf;
                    _excsShortHdr.Esrh_mnth = new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 1);
                    _excsShortHdr.Esrh_user = BaseCls.GlbUserID;
                    _excsShortHdr.Esrh_stus = "P";
                    _excsShortHdr.Esrh_cre_by = BaseCls.GlbUserID;
                    _excsShortHdr.Esrh_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                    _excsShortHdr.Esrh_mod_by = BaseCls.GlbUserID;
                    _excsShortHdr.Esrh_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                    _excsShortHdr.Esrh_session_id = BaseCls.GlbUserSessionID;

                    int Y = CHNLSVC.Financial.SaveExcessHeader(_excsShortHdr);
                }
                else
                {
                    Int32 Z = CHNLSVC.Financial.DeleteExcsShortDetail(ExcessShortId);
                }

                int X = CHNLSVC.Financial.ProcessExcessShort(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, ExcessShortId, BaseCls.GlbUserID);

                BindExcesShortData(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 1));

                MessageBox.Show("Successfully Processed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerMonthYearProcess_ValueChanged(null, null);

                string _desc = "Month of " + dateTimePickerMonthYearProcess.Value.ToString("MMMM") + " " + dateTimePickerMonthYearProcess.Value.Year;

                int X = CHNLSVC.Financial.ConfirmExcessShort(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 1), dateTimePickerDate.Value, ExcessShortId, _desc, BaseCls.GlbUserID);

                lblStatus.Text = "CONFIRMED";
                btnProcess.Enabled = false;
                btnAdd1.Enabled = false;
                btnDelete1.Enabled = false;

                clearProcess();
                MessageBox.Show("Successfully Confirmed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string _compDesc = "";
                string _compAddr = "";
                Decimal _curBal = 0;
                Decimal _prvBal = 0;

                //delete from sat_excs_bal table and insert
                string _stus = "";
                if (lblStatus.Text == "PENDING")
                {
                    _stus = "NOT CONFIRMED";
                }

                int X = CHNLSVC.Financial.PrintExcessShort(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ExcessShortId, new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 1), _stus, BaseCls.GlbUserID, out _prvBal, out _curBal);


                //GlbReportName = "ExcessShort";
                //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ExcessShort.rpt";
                //GlbReportMapPath = "~/Reports_Module/Sales_Rep/ExcessShort.rpt";

                GlbReportName = "Excess Short";
                BaseCls.GlbReportPrvBal = _prvBal;
                BaseCls.GlbReportCurBal = _curBal;
                BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                BaseCls.GlbReportDoc = ExcessShortId;
                BaseCls.GlbStatus = _stus;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(dateTimePickerMonthYearProcess.Text);

                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                if (_masterComp != null)
                {
                    BaseCls.GlbReportComp = _masterComp.Mc_desc;
                    BaseCls.GlbReportCompAddr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
                }

                ReportViewer _view = new ReportViewer();
                _view.GlbReportName = "ExcessShort.rpt";
                BaseCls.GlbReportName = "ExcessShort.rpt";
                _view.Show();
                _view = null;

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        #endregion

        #region key down events

        #region tab page 1

        private void dateTimePickerMonthYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {
                dateTimePickerMonthYear.Value = new DateTime(dateTimePickerMonthYear.Value.Year, 2, 1);
            }
            if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {
                dateTimePickerMonthYear.Value = new DateTime(dateTimePickerMonthYear.Value.Year, 4, 1);
            }
            if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
            {
                dateTimePickerMonthYear.Value = new DateTime(dateTimePickerMonthYear.Value.Year, 6, 1);
            }
            if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
            {
                dateTimePickerMonthYear.Value = new DateTime(dateTimePickerMonthYear.Value.Year, 9, 1);
            }
           
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerSettleDate.Focus();
            }
        }

        private void dateTimePickerSettleDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSetAmt.Focus();
            }
        }

        private void txtSetAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark.Focus();
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }

        #endregion

        #region tab page 2

        private void dateTimePickerMonthYearProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }

        }

        private void rdoExcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbRemitanceType.Focus();
            }
        }

        private void rdoShort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbRemitanceType.Focus();
            }
        }

        private void cmbRemitanceType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerDate.Focus();
            }
        }

        private void dateTimePickerDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmt.Focus();
            }
        }

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd1.Focus();
            }
        }

        #endregion

        #endregion

        private void rdoExcess_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BindRemitTypes();
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

        private void rdoShort_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BindRemitTypes();
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

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                process_month_change();
                BindExcessShortOthRemData();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "RMVEXCS"))
            {
                if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                DateTime _procesMonth = new DateTime(dateTimePickerMonthYearProcess.Value.Year, dateTimePickerMonthYearProcess.Value.Month, 1);
                Int32 _eff = CHNLSVC.Financial.CancelExcessShortFinal(BaseCls.GlbUserDefProf, _procesMonth, BaseCls.GlbUserID);
                MessageBox.Show("Successfully Cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Access Denied !. \n Permission code : RMVEXCS", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
