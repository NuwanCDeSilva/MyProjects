using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class CustomerSatisfaction : Base
    {
        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


        List<SCV_JOB_SATIS> _lstJobSatis;
        private String _serChannel = string.Empty;
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;

        private void clearSatis()
        {
            optQ1A1.Checked = false;
            optQ1A2.Checked = false;
            optQ1A3.Checked = false;
            optQ1A4.Checked = false;
            optQ1A5.Checked = false;
            optQ1A6.Checked = false;
            optQ2A1.Checked = false;
            optQ2A2.Checked = false;
            optQ2A3.Checked = false;
            optQ2A4.Checked = false;
            optQ2A5.Checked = false;
            optQ2A6.Checked = false;
            optQ3A1.Checked = false;
            optQ3A2.Checked = false;
            optQ3A3.Checked = false;
            optQ3A4.Checked = false;
            optQ3A5.Checked = false;
            optQ3A6.Checked = false;
            optQ4A1.Checked = false;
            optQ4A2.Checked = false;
            optQ4A3.Checked = false;
            optQ4A4.Checked = false;
            optQ4A5.Checked = false;
            optQ4A6.Checked = false;
            optQ5A1.Checked = false;
            optQ5A2.Checked = false;
            optQ5A3.Checked = false;
            optQ5A4.Checked = false;
            optQ5A5.Checked = false;
            optQ5A6.Checked = false;

            txtJobNo.Text = "";
            lblName.Text = "";
            lblAddrss.Text = "";
            lblTele.Text = "";
            dgvJobItemsD2.AutoGenerateColumns = false;
            dgvJobItemsD2.DataSource = null;

        }
        public CustomerSatisfaction()
        {
            InitializeComponent();
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            cmbComType.SelectedIndex = 0;
        }

        public CustomerSatisfaction(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();

            txtFBJobNo.Text = GblJobNum;
            GetJob();
            load_cust_feedbacks();
            cmbComType.SelectedIndex = 0;
            
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        if (pnlFeedback.Visible == true)
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "2,3,4,5,6" + seperator);
                        else
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "7,8,11" + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GetJobDetails(0);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
               // clearScreen();
                clearSatis();
            
            }
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            //if (dtpFrom.Value.Date > dtpTo.Value.Date)
            //{ MessageBox.Show("Invalid date range !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            DateTime dtTemp = DateTime.Today.AddMonths(-1);  // DateTime.Today.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
            // _CommonSearch.dtpFrom.Value = dtTemp;
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtJobNo.Focus();

            load_job_values();
            GetSatisJob();


        }

        private void load_job_values()
        {
            List<Service_job_Det> _JobDet = new List<Service_job_Det>();
            _JobDet = CHNLSVC.CustService.GetPcJobDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtJobNo.Text.Trim());
            foreach (Service_job_Det _jDet in _JobDet)
            {
                if (_jDet.Jbd_stage == 7 || _jDet.Jbd_stage == 8 || _jDet.Jbd_stage == 11)
                {
                    pic1.Visible = false;
                    pic2.Visible = false;
                    pic3.Visible = false;

                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                    lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;
                    lblAddrss.Text = oJOB_HDR.SJB_B_ADD1 + "  " + oJOB_HDR.SJB_B_ADD2 + "  " + oJOB_HDR.SJB_B_ADD3;
                    lblTele.Text = oJOB_HDR.SJB_B_MOBINO;



                    //sms reply value
                    DataTable _dt = CHNLSVC.CustService.GetCustSatisReplyVal(BaseCls.GlbUserComCode, _serChannel, 1, null);
                    if (_dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(_dt.Rows[0]["ssj_quest_val"]) == 1)
                        {
                            optR1.Checked = true;
                            lblReply.Text = optR1.Text;
                        }
                        if (Convert.ToInt32(_dt.Rows[0]["ssj_quest_val"]) == 2)
                        {
                            optR2.Checked = true;
                            lblReply.Text = optR2.Text;
                        }
                        if (Convert.ToInt32(_dt.Rows[0]["ssj_quest_val"]) == 3)
                        {
                            optR3.Checked = true;
                            lblReply.Text = optR3.Text;
                        }
                        if (Convert.ToInt32(_dt.Rows[0]["ssj_quest_val"]) == 4)
                        {
                            optR4.Checked = true;
                            lblReply.Text = optR4.Text;
                        }
                        if (Convert.ToInt32(_dt.Rows[0]["ssj_quest_val"]) == 5)
                        {
                            optR5.Checked = true;
                            lblReply.Text = optR5.Text;
                        }

                        if (_dt.Rows[0]["ssv_grade"].ToString() == "1")
                            pic1.Visible = true;
                        if (_dt.Rows[0]["ssv_grade"].ToString() == "2")
                            pic2.Visible = true;
                        if (_dt.Rows[0]["ssv_grade"].ToString() == "3")
                            pic3.Visible = true;
                    }

                    Int32 _satisLvlLine = 1;
                    DataTable _dt1 = CHNLSVC.CustService.GetCustSatisReplyVal(BaseCls.GlbUserComCode, _serChannel, 0, txtJobNo.Text);
                    if (_dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in _dt1.Rows)
                        {
                            if (_satisLvlLine == 1)
                            {
                                if (Convert.ToInt32(_dt1.Rows[0]["ssj_quest_val"]) == 1)
                                    optQ1A1.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[0]["ssj_quest_val"]) == 2)
                                    optQ1A2.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[0]["ssj_quest_val"]) == 3)
                                    optQ1A3.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[0]["ssj_quest_val"]) == 4)
                                    optQ1A4.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[0]["ssj_quest_val"]) == 5)
                                    optQ1A5.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[0]["ssj_quest_val"]) == 6)
                                    optQ1A6.Checked = true;
                            }
                            if (_satisLvlLine == 2)
                            {
                                if (Convert.ToInt32(_dt1.Rows[1]["ssj_quest_val"]) == 1)
                                    optQ2A1.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[1]["ssj_quest_val"]) == 2)
                                    optQ2A2.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[1]["ssj_quest_val"]) == 3)
                                    optQ2A3.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[1]["ssj_quest_val"]) == 4)
                                    optQ2A4.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[1]["ssj_quest_val"]) == 5)
                                    optQ2A5.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[1]["ssj_quest_val"]) == 6)
                                    optQ2A6.Checked = true;
                            }
                            if (_satisLvlLine == 3)
                            {
                                if (Convert.ToInt32(_dt1.Rows[2]["ssj_quest_val"]) == 1)
                                    optQ3A1.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[2]["ssj_quest_val"]) == 2)
                                    optQ3A2.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[2]["ssj_quest_val"]) == 3)
                                    optQ3A3.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[2]["ssj_quest_val"]) == 4)
                                    optQ3A4.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[2]["ssj_quest_val"]) == 5)
                                    optQ3A5.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[2]["ssj_quest_val"]) == 6)
                                    optQ3A6.Checked = true;
                            }
                            if (_satisLvlLine == 4)
                            {
                                if (Convert.ToInt32(_dt1.Rows[3]["ssj_quest_val"]) == 1)
                                    optQ4A1.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[3]["ssj_quest_val"]) == 2)
                                    optQ4A2.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[3]["ssj_quest_val"]) == 3)
                                    optQ4A3.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[3]["ssj_quest_val"]) == 4)
                                    optQ4A4.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[3]["ssj_quest_val"]) == 5)
                                    optQ4A5.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[3]["ssj_quest_val"]) == 6)
                                    optQ4A6.Checked = true;
                            }
                            if (_satisLvlLine == 5)
                            {
                                if (Convert.ToInt32(_dt1.Rows[4]["ssj_quest_val"]) == 1)
                                    optQ5A1.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[4]["ssj_quest_val"]) == 2)
                                    optQ5A2.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[4]["ssj_quest_val"]) == 3)
                                    optQ5A3.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[4]["ssj_quest_val"]) == 4)
                                    optQ5A4.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[4]["ssj_quest_val"]) == 5)
                                    optQ5A5.Checked = true;
                                if (Convert.ToInt32(_dt1.Rows[4]["ssj_quest_val"]) == 6)
                                    optQ5A6.Checked = true;
                            }
                            _satisLvlLine = _satisLvlLine + 1;
                        }
                    }
                    else
                    {
                       optQ1A1.Checked = false;
                       optQ1A2.Checked = false;
                       optQ1A3.Checked = false;
                       optQ1A4.Checked = false;
                       optQ1A5.Checked = false;
                       optQ1A6.Checked = false;
                       optQ2A1.Checked = false;
                       optQ2A2.Checked = false;
                       optQ2A3.Checked = false;
                       optQ2A4.Checked = false;
                       optQ2A5.Checked = false;
                       optQ2A6.Checked = false;
                       optQ3A1.Checked = false;
                       optQ3A2.Checked = false;
                       optQ3A3.Checked = false;
                       optQ3A4.Checked = false;
                       optQ3A5.Checked = false;
                       optQ3A6.Checked = false;
                       optQ4A1.Checked = false;
                       optQ4A2.Checked = false;
                       optQ4A3.Checked = false;
                       optQ4A4.Checked = false;
                       optQ4A5.Checked = false;
                       optQ4A6.Checked = false;
                       optQ5A1.Checked = false;
                       optQ5A2.Checked = false;
                       optQ5A3.Checked = false;
                       optQ5A4.Checked = false;
                       optQ5A5.Checked = false;
                       optQ5A6.Checked = false;
                        
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Job number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        private void load_quest()
        {
            Int32 _rowline = 1;
            Int32 _satisLvlLine = 1;
            DataTable _dtLine = null;
            DataTable _dt = CHNLSVC.CustService.GetCustSatisByChnl(BaseCls.GlbUserComCode, _serChannel, 0);
            foreach (DataRow dr in _dt.Rows)
            {
                #region 01
                if (_rowline == 1)
                {

                    pnlQ1.Visible = true;
                    lblQ1Code.Text = dr["ssq_seq"].ToString();
                    lblQ1.Text = "1.) " + dr["ssq_quest"].ToString();

                    _dtLine = CHNLSVC.CustService.GetCustSatisfData(Convert.ToInt32(dr["ssq_seq"]), 0);
                    foreach (DataRow dr1 in _dtLine.Rows)
                    {

                        if (_satisLvlLine == 1)
                        {
                            optQ1A1.Visible = true;
                            optQ1A1.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 2)
                        {
                            optQ1A2.Visible = true;
                            optQ1A2.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 3)
                        {
                            optQ1A3.Visible = true;
                            optQ1A3.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 4)
                        {
                            optQ1A4.Visible = true;
                            optQ1A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 5)
                        {
                            optQ1A5.Visible = true;
                            optQ1A5.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 6)
                        {
                            optQ1A6.Visible = true;
                            optQ1A6.Text = dr1["ssv_desc"].ToString();
                        }

                        _satisLvlLine = _satisLvlLine + 1;
                    }

                }
                #endregion
                #region 02
                if (_rowline == 2)
                {

                    pnlQ2.Visible = true;
                    lblQ2Code.Text = dr["ssq_seq"].ToString();
                    lblQ2.Text = "2.) " + dr["ssq_quest"].ToString();

                    _dtLine = CHNLSVC.CustService.GetCustSatisfData(Convert.ToInt32(dr["ssq_seq"]), 0);
                    foreach (DataRow dr1 in _dtLine.Rows)
                    {

                        if (_satisLvlLine == 1)
                        {
                            optQ2A1.Visible = true;
                            optQ2A1.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 2)
                        {
                            optQ2A2.Visible = true;
                            optQ2A2.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 3)
                        {
                            optQ2A3.Visible = true;
                            optQ2A3.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 4)
                        {
                            optQ2A4.Visible = true;
                            optQ2A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 5)
                        {
                            optQ2A4.Visible = true;
                            optQ2A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 6)
                        {
                            optQ2A6.Visible = true;
                            optQ2A6.Text = dr1["ssv_desc"].ToString();
                        }

                        _satisLvlLine = _satisLvlLine + 1;
                    }

                }
                #endregion
                #region 03
                if (_rowline == 3)
                {

                    pnlQ3.Visible = true;
                    lblQ3Code.Text = dr["ssq_seq"].ToString();
                    lblQ3.Text = "3.) " + dr["ssq_quest"].ToString();

                    _dtLine = CHNLSVC.CustService.GetCustSatisfData(Convert.ToInt32(dr["ssq_seq"]), 0);
                    foreach (DataRow dr1 in _dtLine.Rows)
                    {

                        if (_satisLvlLine == 1)
                        {
                            optQ3A1.Visible = true;
                            optQ3A1.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 2)
                        {
                            optQ3A2.Visible = true;
                            optQ3A2.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 3)
                        {
                            optQ3A3.Visible = true;
                            optQ3A3.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 4)
                        {
                            optQ3A4.Visible = true;
                            optQ3A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 5)
                        {
                            optQ3A4.Visible = true;
                            optQ3A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 6)
                        {
                            optQ3A6.Visible = true;
                            optQ3A6.Text = dr1["ssv_desc"].ToString();
                        }

                        _satisLvlLine = _satisLvlLine + 1;
                    }

                }
                #endregion
                #region 04
                if (_rowline == 4)
                {

                    pnlQ4.Visible = true;
                    lblQ4Code.Text = dr["ssq_seq"].ToString();
                    lblQ4.Text = "4.) " + dr["ssq_quest"].ToString();

                    _dtLine = CHNLSVC.CustService.GetCustSatisfData(Convert.ToInt32(dr["ssq_seq"]), 0);
                    foreach (DataRow dr1 in _dtLine.Rows)
                    {

                        if (_satisLvlLine == 1)
                        {
                            optQ4A1.Visible = true;
                            optQ4A1.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 2)
                        {
                            optQ4A2.Visible = true;
                            optQ4A2.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 3)
                        {
                            optQ4A3.Visible = true;
                            optQ4A3.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 4)
                        {
                            optQ4A4.Visible = true;
                            optQ4A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 5)
                        {
                            optQ4A4.Visible = true;
                            optQ4A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 6)
                        {
                            optQ4A6.Visible = true;
                            optQ4A6.Text = dr1["ssv_desc"].ToString();
                        }

                        _satisLvlLine = _satisLvlLine + 1;
                    }

                }
                #endregion
                #region 05
                if (_rowline == 5)
                {

                    pnlQ5.Visible = true;
                    lblQ5Code.Text = dr["ssq_seq"].ToString();
                    lblQ5.Text = "5.) " + dr["ssq_quest"].ToString();

                    _dtLine = CHNLSVC.CustService.GetCustSatisfData(Convert.ToInt32(dr["ssq_seq"]), 0);
                    foreach (DataRow dr1 in _dtLine.Rows)
                    {

                        if (_satisLvlLine == 1)
                        {
                            optQ5A1.Visible = true;
                            optQ5A1.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 2)
                        {
                            optQ5A2.Visible = true;
                            optQ5A2.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 3)
                        {
                            optQ5A3.Visible = true;
                            optQ5A3.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 4)
                        {
                            optQ5A4.Visible = true;
                            optQ5A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 5)
                        {
                            optQ5A4.Visible = true;
                            optQ5A4.Text = dr1["ssv_desc"].ToString();
                        }
                        if (_satisLvlLine == 6)
                        {
                            optQ5A6.Visible = true;
                            optQ5A6.Text = dr1["ssv_desc"].ToString();
                        }

                        _satisLvlLine = _satisLvlLine + 1;
                    }

                }
                #endregion

                _rowline = _rowline + 1;
                _satisLvlLine = 1;
            }

            //reply sms
            DataTable _dt1 = CHNLSVC.CustService.GetCustSatisByChnl(BaseCls.GlbUserComCode, _serChannel, 1);
            foreach (DataRow dr in _dt1.Rows)
            {
                lblSMSQ.Text = dr["ssq_quest"].ToString();

                _dtLine = CHNLSVC.CustService.GetCustSatisfData(Convert.ToInt32(dr["ssq_seq"]), 0);
                foreach (DataRow dr1 in _dtLine.Rows)
                {

                    if (_satisLvlLine == 1)
                    {
                        //optR1.Visible = true;
                        optR1.Text = dr1["ssv_desc"].ToString();
                    }
                    if (_satisLvlLine == 2)
                    {
                        //optR2.Visible = true;
                        optR2.Text = dr1["ssv_desc"].ToString();
                    }
                    if (_satisLvlLine == 3)
                    {
                        //optR3.Visible = true;
                        optR3.Text = dr1["ssv_desc"].ToString();
                    }
                    if (_satisLvlLine == 4)
                    {
                        //optR4.Visible = true;
                        optR4.Text = dr1["ssv_desc"].ToString();
                    }
                    if (_satisLvlLine == 5)
                    {
                        //optR5.Visible = true;
                        optR5.Text = dr1["ssv_desc"].ToString();
                    }
                    _satisLvlLine = _satisLvlLine + 1;
                }
            }
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                load_job_values();
                GetSatisJob();
            }
            else
                load_quest();
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnView.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick(null, null);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
                load_job_values();
        }

        private void dtpFromDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpTo.Focus();
            }
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnApprove.Select();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Service Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void clearScreen()
        {
            _lstJobSatis = new List<SCV_JOB_SATIS>();
            CustomerSatisfaction formnew = new CustomerSatisfaction();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        //private void dtpFrom_ValueChanged(object sender, EventArgs e)
        //{
        //    if (dtpFrom.Value > dtpTo.Value)
        //    {
        //        // MessageBox.Show("Please enter a valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        dtpFrom.Value = dtpTo.Value;
        //    }
        //}

        //private void dtpTo_ValueChanged(object sender, EventArgs e)
        //{
        //    if (dtpFrom.Value > dtpTo.Value)
        //    {
        //        //  MessageBox.Show("Please enter a valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        dtpTo.Value = dtpFrom.Value;
        //    }
        //}

        private void CustomerSatisfaction_Load(object sender, EventArgs e)
        {
            _lstJobSatis = new List<SCV_JOB_SATIS>();
            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                if (_Parameters.SP_ISNEEDWIP != 1)
                {
                    MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Enabled = false;
                }
                _serChannel = _Parameters.SP_SERCHNL;

                load_quest();
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
            }




        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Select the Job number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Int32 _jobSeq = 0;
            Int32 _jobLine = 0;
            if (MessageBox.Show("Are you sure ?", "Customer Satisfaction", MessageBoxButtons.YesNo) == DialogResult.No) return;

            _lstJobSatis = new List<SCV_JOB_SATIS>();

            List<Service_job_Det> _jobDetRecall = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, -777, BaseCls.GlbUserComCode);
            if (_jobDetRecall != null && _jobDetRecall.Count > 0)
            {
                foreach (Service_job_Det _dtt in _jobDetRecall)
                {
                    _jobSeq = _dtt.Jbd_seq_no;
                    _jobLine = _dtt.Jbd_jobline;
                }
            }

            //   DataTable _dtJob = CHNLSVC.CustService.sp_get_job_details(txtJobNo.Text);
            if (_jobSeq != 0)
            {

                if (pnlQ1.Visible == true)
                {
                    SCV_JOB_SATIS _jb = new SCV_JOB_SATIS();
                    _jb.Ssj_cre_by = BaseCls.GlbUserID;
                    _jb.Ssj_jobline = _jobLine;
                    _jb.Ssj_jobno = txtJobNo.Text;
                    _jb.Ssj_quest_seq = Convert.ToInt32(lblQ1Code.Text);
                    _jb.Ssj_quest_val = 0;
                    if (optQ1A1.Checked == true) _jb.Ssj_quest_val = 1;
                    if (optQ1A2.Checked == true) _jb.Ssj_quest_val = 2;
                    if (optQ1A3.Checked == true) _jb.Ssj_quest_val = 3;
                    if (optQ1A4.Checked == true) _jb.Ssj_quest_val = 4;
                    if (optQ1A5.Checked == true) _jb.Ssj_quest_val = 5;
                    if (optQ1A6.Checked == true) _jb.Ssj_quest_val = 6;
                    _jb.Ssj_seq_no = _jobSeq;
                    _lstJobSatis.Add(_jb);
                }
                if (pnlQ2.Visible == true)
                {
                    SCV_JOB_SATIS _jb = new SCV_JOB_SATIS();
                    _jb.Ssj_cre_by = BaseCls.GlbUserID;
                    _jb.Ssj_jobline = _jobLine;
                    _jb.Ssj_jobno = txtJobNo.Text;
                    _jb.Ssj_quest_seq = Convert.ToInt32(lblQ2Code.Text);
                    _jb.Ssj_quest_val = 0;
                    if (optQ2A1.Checked == true) _jb.Ssj_quest_val = 1;
                    if (optQ2A2.Checked == true) _jb.Ssj_quest_val = 2;
                    if (optQ2A3.Checked == true) _jb.Ssj_quest_val = 3;
                    if (optQ2A4.Checked == true) _jb.Ssj_quest_val = 4;
                    if (optQ2A5.Checked == true) _jb.Ssj_quest_val = 5;
                    if (optQ2A6.Checked == true) _jb.Ssj_quest_val = 6;
                    _jb.Ssj_seq_no = _jobSeq;
                    _lstJobSatis.Add(_jb);
                }
                if (pnlQ3.Visible == true)
                {
                    SCV_JOB_SATIS _jb = new SCV_JOB_SATIS();
                    _jb.Ssj_cre_by = BaseCls.GlbUserID;
                    _jb.Ssj_jobline = _jobLine;
                    _jb.Ssj_jobno = txtJobNo.Text;
                    _jb.Ssj_quest_seq = Convert.ToInt32(lblQ3Code.Text);
                    _jb.Ssj_quest_val = 0;
                    if (optQ3A1.Checked == true) _jb.Ssj_quest_val = 1;
                    if (optQ3A2.Checked == true) _jb.Ssj_quest_val = 2;
                    if (optQ3A3.Checked == true) _jb.Ssj_quest_val = 3;
                    if (optQ3A4.Checked == true) _jb.Ssj_quest_val = 4;
                    if (optQ3A5.Checked == true) _jb.Ssj_quest_val = 5;
                    if (optQ3A6.Checked == true) _jb.Ssj_quest_val = 6;
                    _jb.Ssj_seq_no = _jobSeq;
                    _lstJobSatis.Add(_jb);
                }

                if (pnlQ4.Visible == true)
                {
                    SCV_JOB_SATIS _jb = new SCV_JOB_SATIS();
                    _jb.Ssj_cre_by = BaseCls.GlbUserID;
                    _jb.Ssj_jobline = _jobLine;
                    _jb.Ssj_jobno = txtJobNo.Text;
                    _jb.Ssj_quest_seq = Convert.ToInt32(lblQ4Code.Text);
                    _jb.Ssj_quest_val = 0;
                    if (optQ4A1.Checked == true) _jb.Ssj_quest_val = 1;
                    if (optQ4A2.Checked == true) _jb.Ssj_quest_val = 2;
                    if (optQ4A3.Checked == true) _jb.Ssj_quest_val = 3;
                    if (optQ4A4.Checked == true) _jb.Ssj_quest_val = 4;
                    if (optQ4A5.Checked == true) _jb.Ssj_quest_val = 5;
                    if (optQ4A6.Checked == true) _jb.Ssj_quest_val = 6;
                    _jb.Ssj_seq_no = _jobSeq;
                    _lstJobSatis.Add(_jb);
                }
                if (pnlQ5.Visible == true)
                {
                    SCV_JOB_SATIS _jb = new SCV_JOB_SATIS();
                    _jb.Ssj_cre_by = BaseCls.GlbUserID;
                    _jb.Ssj_jobline = _jobLine;
                    _jb.Ssj_jobno = txtJobNo.Text;
                    _jb.Ssj_quest_seq = Convert.ToInt32(lblQ5Code.Text);
                    _jb.Ssj_quest_val = 0;
                    if (optQ5A1.Checked == true) _jb.Ssj_quest_val = 1;
                    if (optQ5A2.Checked == true) _jb.Ssj_quest_val = 2;
                    if (optQ5A3.Checked == true) _jb.Ssj_quest_val = 3;
                    if (optQ5A4.Checked == true) _jb.Ssj_quest_val = 4;
                    if (optQ5A5.Checked == true) _jb.Ssj_quest_val = 5;
                    if (optQ5A6.Checked == true) _jb.Ssj_quest_val = 6;
                    _jb.Ssj_seq_no = _jobSeq;
                    _lstJobSatis.Add(_jb);
                }
                string msg = "";
                Int32 _eff = CHNLSVC.CustService.Save_Cust_Satis(_lstJobSatis, out msg);
                if (_eff == 1)
                {
                    MessageBox.Show("Successfully Saved", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearSatis();

                }
            }
            else
            {
                MessageBox.Show("Invalid Job number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(txtJobNo.Text, -777);
                oJobDetailsViwer.ShowDialog();
            }
            else
                MessageBox.Show("Select Job Number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select the Job number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            bool isValid = ValidateMobileNo(lblTele.Text);
            if (isValid == false)
            {
                MessageBox.Show("Invalid mobile number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //send message
            OutSMS _out = new OutSMS();

            _out.Msg = lblSMSQ.Text;
            _out.Msgtype = "SATIS";
            //  _out.Receiver = Convert.ToString(gvCust.Rows[i].Cells["MBE_CD"].Value);
            string _mob = lblTele.Text.Trim();
            if (_mob.Length == 10)
            {
                _out.Receiverphno = "+94" + _mob.Substring(1, 9);
                _out.Senderphno = "+94" + _mob.Substring(1, 9);
            }
            if (_mob.Length == 9)
            {
                _out.Receiverphno = "+94" + _mob;
                _out.Senderphno = "+94" + _mob;
            }

            _out.Sender = BaseCls.GlbUserID;
            _out.Createtime = DateTime.Now;
            _out.Refdocno = txtJobNo.Text;
            _out.Msgstatus = 0;
            _out.Receivedtime = DateTime.Now;

            Int32 _eff = CHNLSVC.General.SaveSMSOut(_out);

            MessageBox.Show("Successfully Send the Message", "Customer Satisfaction", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private bool ValidateMobileNo(string num)
        {
            int intNum = 0;
            //check only contain degits
            if (!int.TryParse(num, out intNum))
                return false;
            //check for length
            else
            {
                if (num.Length < 10)
                {
                    return false;
                }
                //check for first three chars
                else
                {
                    string firstChar = num.Substring(0, 3);
                    if (firstChar != "071" && firstChar != "077" && firstChar != "078" && firstChar != "072" && firstChar != "075" && firstChar != "076" && firstChar != "074")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            pnlFeedback.Show();
            pnlMain.Hide();         
        }

        private void btnSatis_Click(object sender, EventArgs e)
        {
            pnlFeedback.Hide();
            pnlMain.Show();          
        }

        private void btn_srch_job_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);  // DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                // _CommonSearch.dtpFrom.Value = dtTemp;
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFBJobNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtFBJobNo.Focus();

                GetJob();
                load_cust_feedbacks();
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void GetJob()
        {
            Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtFBJobNo.Text, BaseCls.GlbUserComCode);

            lblCustCode.Text = oJOB_HDR.SJB_B_CUST_CD;
            lblCustomer.Text = oJOB_HDR.SJB_B_CUST_NAME;
            lblAddress.Text = oJOB_HDR.SJB_B_ADD1;
            lblAddress2.Text = oJOB_HDR.SJB_B_ADD2;
            lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
            lblTele.Text = oJOB_HDR.SJB_MOBINO;
            lblContactNum.Text = oJOB_HDR.SJB_CNT_PHNO;
            lblMob.Text = oJOB_HDR.SJB_B_MOBINO;

            lblCustomerCode.Text = oJOB_HDR.SJB_B_CUST_CD;
            lblComJobstge.Text = oJOB_HDR.SJB_JOBSTAGE_TEXT;
            lblJobStgeNo.Text = Convert.ToDecimal(oJOB_HDR.SJB_JOBSTAGE).ToString();

            List<Service_job_Det> _jobDetRecall = CHNLSVC.CustService.GetJobDetails(txtFBJobNo.Text, -777, BaseCls.GlbUserComCode);
            dgvJobItemsD1.AutoGenerateColumns = false;
            dgvJobItemsD1.DataSource = new List<Service_job_Det>();
            dgvJobItemsD1.DataSource = _jobDetRecall;

            txtComment.Text = "";
        }

        private void GetSatisJob()
        {
            List<Service_job_Det> _jobDetRecall = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, -777, BaseCls.GlbUserComCode);
            dgvJobItemsD2.DataSource = new List<Service_job_Det>();
            dgvJobItemsD2.DataSource = _jobDetRecall;
        }
        private void txtFBJobNo_Leave(object sender, EventArgs e)
        {
            GetJob();
            load_cust_feedbacks();
        }

        private void txtFBJobNo_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_job_Click(null, null);
        }

        private void txtFBJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_job_Click(null, null);
        }

        private void btnAddComent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFBJobNo.Text))
            {
                MessageBox.Show("Please select the job number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtComment.Text))
            {
                MessageBox.Show("Please enter the customer feedback", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Customer Feedback", MessageBoxButtons.YesNo) == DialogResult.No) return;

            int commntlngth = txtComment.Text.Length;
            int issms = 1;

            if(commntlngth > 160)
            {
                if (MessageBox.Show("Character length More than 1 sms charges apply are you sure to send SMS", "Customer Feedback", MessageBoxButtons.YesNo) == DialogResult.No) issms = 0;
            }

            if(commntlngth >= 500)
            {
                MessageBox.Show("comment Maximum Length is 500 characters", "Customer Feedback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int infmall = 0;
            int inftech = 0;

            if (chkInformAll.Checked)
            {
                infmall = 1;
            }

            if(chkinfmtech.Checked)
            {
                inftech = 1;
            }

            Int32 _eff = CHNLSVC.CustService.SAVE_SCV_JOBCUS_FEED(txtFBJobNo.Text, 1, DateTime.Now.Date, lblCustCode.Text, txtComment.Text, BaseCls.GlbUserID, int.Parse(cmbComType.SelectedIndex.ToString()), int.Parse(lblJobStgeNo.Text), infmall, inftech, issms, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            load_cust_feedbacks();
            txtComment.Text = "";
            MessageBox.Show("Successfully Added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void load_cust_feedbacks()
        {
            DataTable _dt = CHNLSVC.CustService.GetCustJobFeedback(txtFBJobNo.Text);
            drvRecords.AutoGenerateColumns = false;
            drvRecords.DataSource = _dt;
        }

        private void drvRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 _eff = CHNLSVC.CustService.DeleteCustFeedback(txtFBJobNo.Text, Convert.ToInt32(drvRecords.Rows[e.RowIndex].Cells["sjf_seqline"].Value));
                    load_cust_feedbacks();
                }
            }   
        }

        private void drvRecords_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtComment.Text = drvRecords.Rows[e.RowIndex].Cells["sjf_feedback"].Value.ToString();
        }
    }
}