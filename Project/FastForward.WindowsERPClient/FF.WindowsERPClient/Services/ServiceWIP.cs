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
    public partial class ServiceWIP : Base
    {
        public int SelectedJobLine = -1;
        public string Serial_No = "";   //kapila 16/2/2016
        public decimal gblJobStage;
        private Boolean isLoad = false;
        private static string glbJobNo = "";   //kapila 15/2/2016

        private InventorySerialMaster _warrItemTemp = null;
        private List<Service_Job_Defects> oDefetsbulk = new List<Service_Job_Defects>();
        private DataTable _tblSerEmp = new DataTable();
        public bool IsHavingGitItems = false; //By akila 2017/05/08 - if git items available, service not allow to close
        public bool IsOldItemAdded = false;
        public ServiceWIP()
        {
            InitializeComponent();
            dgvJobDetails.AutoGenerateColumns = false;
            dgvDefects.AutoGenerateColumns = false;
            dgvEmpDetails.AutoGenerateColumns = false;
            btnClear_Click(null, null);

            pnlAdditionalItems.Size = new Size(702, 163);
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

                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchF3:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP:
                    {

                        if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10816))
                        {
                            //paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator + "GET_ALL_JOBS" + seperator);
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + ddlStages.SelectedValue + seperator + "GET_ALL_JOBS" + seperator);
                            break;
                        }
                        else
                        {
                            //paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator + BaseCls.GlbUserID + seperator);
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + ddlStages.SelectedValue + seperator + BaseCls.GlbUserID + seperator);
                            break;
                        }

                    }

                    break;
            }

            return paramsText.ToString();
        }

        #region Events

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick(null, null);
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsWIP(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
            _CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            txtJobNo.Focus();

            _CommonSearch.ShowDialog();

            //for (int i = 0; i < _CommonSearch.dvResult.Rows.Count; i++)
            //{
            //    DataTable dt = CHNLSVC.CustService.getPriorityDataByCode(_CommonSearch.dvResult.Rows[i].Cells["PRORITY"].Value.ToString());
            //    Color newColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[0]["scp_color"].ToString());
            //    _CommonSearch.dvResult.Rows[i].DefaultCellStyle.BackColor = newColor;
            //}

            _CommonSearch.dvResult.Refresh();

            txtJobNo_Leave(null, null);     //kapila 3/9/2015
            //txtJobNo.Focus();
            //dgvJobDetails.Focus();
            //txtJobNo_Leave(null, null);
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int i = 0;
            if (i == 0)
            {
                testMethod();
                i = i + 1;
            }
            Cursor = Cursors.Default;       
        }

        private void testMethod()
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                //dgvJobDetails.Focus();
                string job = txtJobNo.Text;
                clrScreen();
                txtJobNo.Text = job;                
                getJobJetails();

                Service_JOB_HDR oHedaer = CHNLSVC.CustService.GetServiceJobHeader(job, BaseCls.GlbUserComCode);
                List<MST_BUSPRIT_LVL> oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(oHedaer.SJB_B_CUST_CD, BaseCls.GlbUserComCode);
                if (oItems != null && oItems.Count > 0)
                {
                    ucServicePriority1.GblJobNumber = job;
                    ucServicePriority1.LoadData();
                }
                else
                {
                    ucServicePriority1.GblCustCode = "CASH";
                    ucServicePriority1.LoadData();
                }
            }
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //testMethod();
                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    if (txtJobNo.Focused == true)
                    {
                        dgvJobDetails.Focus();
                    }
                    else
                    {
                        txtJobNo_Leave(null, null);
                    }                   
                }
                
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearchF3);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsF3(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
                _CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJobNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtJobNo.Focus();

                _CommonSearch.ShowDialog();

                _CommonSearch.dvResult.Refresh();

                txtJobNo_Leave(null, null);
            }
        }

        private void ServiceWIP_Load(object sender, EventArgs e)
        {
            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                if (_Parameters.SP_ISNEEDWIP != 1)
                {
                    MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }

                SerialNo.HeaderText = _Parameters.SP_DB_SERIAL;
                dgvJobDetails.Columns["SerialNo"].HeaderText = _Parameters.SP_DB_SERIAL;
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
                return;
            }

            //kapila 26/5/2017
            if (_Parameters.SP_IS_CHNG_JBOPN_DT == 1)
                dtpSelectedDateTime.Enabled = true;
            else
                dtpSelectedDateTime.Enabled = false;

            btnClear_Click(null, null);
            //kapila 15/2/2016
            //if (_Parameters.SP_IS_LOAD_PEND_JOBS == 1)
            //{
            if (BaseCls.GlbUserCategory == "TECH" || BaseCls.GlbUserCategory == "ASST")
            {
                Enquiries.Service.AllocPendingJobs frm = new Enquiries.Service.AllocPendingJobs();
                frm.setEmpCategory(BaseCls.GlbUserCategory);
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(0, 55);
                frm.ShowDialog();

            }
            if (!string.IsNullOrEmpty(glbJobNo))
            {
                txtJobNo.Text = glbJobNo;
                glbJobNo = "";
                txtJobNo_Leave(null, null);
            }
            //}

        }

        public ServiceWIP(string job)
        {
            glbJobNo = job;
        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(txtJobNo.Text, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtJobNo.Clear();
            lblName.Text = "";
            lblTele.Text = "";
            lblContEmail.Text = "";
            lblContNo.Text = "";
            lblContName.Text = "";
            lblCustomerCode.Text = "";
            lblJobStage.Text = "";
            lblJobCategori.Text = "";
            lblLevel.Text = "";
            dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);
            dgvDefects.DataSource = new List<Service_Job_Defects>();
            lblScheduleEnd.Text = "";
            lblScheduleStart.Text = "";
            lblStartOpenDate.Text = "";
            lblCompleteDate.Text = "";
            lblJobStageNew.Text = "";
            SelectedJobLine = -1;
            Serial_No = "";
            pnlSelect.Visible = false;

            lblStageText.Text = "";

            enableDisableBtns(false);

            lblItem.Text = "";
            lblItemDesc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblSerNo.Text = "";
            lblWarStus.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblAttempt.Text = "";
            lblWarPrd.Text = "";
            lblWarRemain.Text = "";
            lblWarRem.Text = "";
            lblWarNo.Text = "";
            lblAddrss.Text = "";

            fillStages();

            isLoad = false;
            lblJobCount.Text = "";
            pnlHist.Hide();
            pnlHist.Size = new Size(775, 504);
            getPendingJObCount();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to start this job?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                pnlSelect.Tag = "S";
                             pnlSelect.Visible = true;
                
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (chkbulk.Checked == false)
            {
                if (SelectedJobLine == -1)
                {
                    MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvJobDetails.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Focus();
                    return;
                }
                if (SelectedJobLine == -1)
                {
                    MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvJobDetails.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(lblStartOpenDate.Text))
                {
                    MessageBox.Show("Please start the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Service_Chanal_parameter _Parameters = null;
                _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                if (_Parameters != null)
                {
                    if (_Parameters.SP_AUTO_REMOVE_OLD_PART == 1)
                    {
                        if (!IsOldItemAdded)
                        {
                            ServiceWIP_oldPartRemove frm = new ServiceWIP_oldPartRemove(txtJobNo.Text, SelectedJobLine);
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
                            frm.AddOldItemAuto = true;
                            frm.ShowDialog();
                        }

                    }
                }

                decimal jobstage = Convert.ToDecimal(lblJobStageNew.Text);
                int[] jobLine = new int[] { };

                ServiceWIP_JobClose frmClose = new ServiceWIP_JobClose(txtJobNo.Text, SelectedJobLine, jobstage, lblStartOpenDate.Text, lblWarStus.Text, chkbulk.Checked, jobLine, IsHavingGitItems);

                Int32 isActionTaken = 0;
                frmClose.StartPosition = FormStartPosition.Manual;
                frmClose.Location = new Point(this.Location.X + this.Width - 120 - frmClose.Width, this.Location.Y + 80);
                frmClose.ShowDialog();
                isActionTaken = frmClose.isActionTaken;
                selectSameJobItem();
                //lblCompleteDate.Text = "";
                if (isActionTaken == 1)
                {
                    clrScreen();
                }
            }

            else
            {


                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Focus();
                    return;
                }


                if (String.IsNullOrEmpty(lblStartOpenDate.Text))
                {
                    MessageBox.Show("Please start the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                int[] jobLine = new int[dgvJobDetails.Rows.Count];
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells["select"].Value) == true)
                    {
                        Int32 _line = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value);
                        jobLine[i] = _line;
                    }
                }


                decimal jobstage = Convert.ToDecimal(lblJobStageNew.Text);
                ServiceWIP_JobClose frm = new ServiceWIP_JobClose(txtJobNo.Text, SelectedJobLine, jobstage, lblStartOpenDate.Text, lblWarStus.Text, chkbulk.Checked, jobLine, IsHavingGitItems);

                Int32 isActionTaken = 0;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
                frm.ShowDialog();
                isActionTaken = frm.isActionTaken;
                selectSameJobItem();
                //lblCompleteDate.Text = "";
                if (isActionTaken == 1)
                {
                    clrScreen();
                }
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
        }

        private void label32_Click(object sender, EventArgs e)
        {
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlSelect.Visible = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            pnlSelect.Visible = false;
            if (pnlSelect.Tag.ToString() == "S")
            {
                lblStartOpenDate.Text = dtpSelectedDateTime.Value.ToString("dd/MMM/yyyy  hh:mm tt");

                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);
                if (dtpSelectedDateTime.Value < oJOB_HDR.SJB_DT)
                {
                    MessageBox.Show("Please select a valid date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStartOpenDate.Text = "";
                    return;
                }

                int jobstage = Convert.ToInt32(lblJobStageNew.Text);
                if (jobstage <= 3)
                {
                    int result1 = CHNLSVC.CustService.Update_Job_dates(txtJobNo.Text, SelectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), DateTime.MinValue);
                    int result = CHNLSVC.CustService.Update_JobDetailStage(txtJobNo.Text, SelectedJobLine, 4);

                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = txtJobNo.Text;
                    oLog.SJL_JOBLINE = SelectedJobLine;
                    oLog.SJL_COM = BaseCls.GlbUserComCode;
                    oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                    oLog.SJL_JOBSTAGE = 4;
                    oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                    oLog.SJL_INFSUP = 0;
                    result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);

                    if (result1 > 0)
                    {
                        MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //btnRequisition.Enabled = true;
                        enableDisableBtns(false);
                        //dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0,0));
                        btnStartOpenJob.Enabled = false;
                        selectSameJobItem();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else if (pnlSelect.Tag.ToString() == "E")
            {
                lblCompleteDate.Text = dtpSelectedDateTime.Value.ToString("dd/MMM/yyyy  hh:mm tt");
                int jobstage = Convert.ToInt32(lblJobStageNew.Text);
                if (jobstage == 6)
                {
                    int result1 = CHNLSVC.CustService.Update_Job_dates(txtJobNo.Text, SelectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), Convert.ToDateTime(lblStartOpenDate.Text));
                    int result = CHNLSVC.CustService.Update_JobDetailStage(txtJobNo.Text, SelectedJobLine, 5);
                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = txtJobNo.Text;
                    oLog.SJL_JOBLINE = SelectedJobLine;
                    oLog.SJL_COM = BaseCls.GlbUserComCode;
                    oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                    oLog.SJL_JOBSTAGE = jobstage;
                    oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                    oLog.SJL_INFSUP = 0;
                    result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                    if (result1 > 0)
                    {
                        MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    int result1 = CHNLSVC.CustService.Update_Job_dates(txtJobNo.Text, SelectedJobLine, DateTime.Now, DateTime.Now, Convert.ToDateTime(lblStartOpenDate.Text), Convert.ToDateTime(lblStartOpenDate.Text));
                    int result = CHNLSVC.CustService.Update_JobDetailStage(txtJobNo.Text, SelectedJobLine, 6);
                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = txtJobNo.Text;
                    oLog.SJL_JOBLINE = SelectedJobLine;
                    oLog.SJL_COM = BaseCls.GlbUserComCode;
                    oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                    oLog.SJL_JOBSTAGE = jobstage;
                    oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                    oLog.SJL_INFSUP = 0;
                    result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                    if (result1 > 0)
                    {
                        MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void dgvJobDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int z = 0;
                if (chkbulk.Checked == false)
                {
                    if (dgvJobDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "0")
                    {
                        MessageBox.Show("This item has replaced by the supplier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        z = z + 1;
                        if (z == 1)
                        {
                            for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                            {
                                dgvJobDetails.Rows[i].Cells["select"].Value = false;
                            }
                            dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value = true;

                            LoadDefects(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                            GetJobEMPS(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString()));
                            SelectedJobLine = Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                            Serial_No = dgvJobDetails.Rows[e.RowIndex].Cells["SerialNo"].Value.ToString();
                            Int32 linenum = getAllocationHeader(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                            getJobDetails(linenum);
                        }
                    }
                }
                else
                {
                    if (dgvJobDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "0")
                    {
                        MessageBox.Show("This item has replaced by the supplier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value = true;
                        LoadDefects(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                        GetJobEMPS(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString()));
                        SelectedJobLine = Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                        Serial_No = dgvJobDetails.Rows[e.RowIndex].Cells["SerialNo"].Value.ToString();
                        Int32 linenum = getAllocationHeader(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                        getJobDetails(linenum);
                    }
                }

            }
        }

        private void btnRequisition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }

            //List<Service_job_Det> _jobItems = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, SelectedJobLine, BaseCls.GlbUserComCode);
            //decimal _jobStage = _jobItems[0].Jbd_stage;



            ServicesWIP_MRN frm = new ServicesWIP_MRN(txtJobNo.Text, SelectedJobLine, String.Empty);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnReciepts_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_AODReceive frm = new ServiceWIP_AODReceive(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.GlbModuleName = "m_Trans_Inventory_StockTransferInward";
            frm.ShowDialog();
            IsHavingGitItems = frm.IsGitAvailable;
        }

        private void btnStockReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_StockReturn frm = new ServiceWIP_StockReturn(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnOldPart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_oldPartRemove frm = new ServiceWIP_oldPartRemove(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
            IsOldItemAdded = frm.IsOldItemsAdded;
        }

        private void btnTempIssue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_TempIssue frm = new ServiceWIP_TempIssue(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void dgvJobDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                panel2.Focus();
            }
        }

        private void btnSuppWrntyClmReq_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_warrClmReq frm = new ServiceWIP_warrClmReq(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnSuppWarrtClmReceive_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_warrClmReceive frm = new ServiceWIP_warrClmReceive(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnConsumableItems_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_ConsumableItems frm = new ServiceWIP_ConsumableItems(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            int[] jobLine = new int[] { };
            Int32 jobstage = Convert.ToInt32(lblJobStageNew.Text);
            ServiceWIP_JobClose frm = new ServiceWIP_JobClose(txtJobNo.Text, SelectedJobLine, jobstage, lblStartOpenDate.Text, lblWarStus.Text, chkbulk.Checked, jobLine, IsHavingGitItems);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_Comments frm = new ServiceWIP_Comments(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnVisitComment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_VisitComment frm = new ServiceWIP_VisitComment(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnStartOpenJob_Click(object sender, EventArgs e)
        {
           
          

            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(lblStartOpenDate.Text))
            {
                MessageBox.Show("This job is already started", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to start this job?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                pnlSelect.Tag = "S";
                //Tharanga 2017?may/31
                Service_Chanal_parameter _Parameters = null;
                _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                if (_Parameters.SP_IS_SPCPER_JBOPN == 1)
                {
                    btnConfirm.Enabled = false;
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10825))
                    {
                        btnConfirm.Enabled = false;
                        MessageBox.Show("You don't have permission. Permission code : " + 10825, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        btnConfirm.Enabled = true;
                    }
                }
                else
                {
                    btnConfirm.Enabled = true;
                }

                //Check Job Estimate or Quotation
                if (_Parameters.SP_IS_NEED_ESTIMATE == 1)
                {
                    btnConfirm.Enabled = false;
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10826))
                    {
                        if (!CHNLSVC.CustService.Is_EstimateAvailable(txtJobNo.Text))
                        {
                            btnConfirm.Enabled = false;
                            MessageBox.Show("You are not allow to start the job with out Job Estimate or Quotation. Permission code : " + 10826, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            btnConfirm.Enabled = true;
                        }
                    }
                    else
                    {
                        btnConfirm.Enabled = true;
                    }
                }
                else
                {
                    btnConfirm.Enabled = true;
                }

                //
                pnlSelect.Visible = true;
            }
        }

        private void btnMore1_Click(object sender, EventArgs e)
        {
            //if (btnMore1.Text == "More >>")
            //{
            //    pnlWarr.Height = 293;
            //    btnMore1.Text = "<< More";
            //    btnMore1.BackColor = Color.Orange;
            //    btnMore1.ForeColor = Color.Maroon;
            //}
            //else
            //{
            //    pnlWarr.Height = 109;
            //    btnMore1.Text = "More >>";
            //    btnMore1.BackColor = Color.Maroon;
            //    btnMore1.ForeColor = Color.White;
            //}
            if (btnMore1.Text == "More >>")
            {
                pnlWarr.Height = 293;
                //if (optReq.Checked == true) pnlWarr.Height = 392;

                btnMore1.Text = "<< More";
                btnMore1.BackColor = Color.Orange;
                btnMore1.ForeColor = Color.Maroon;

                if (pnlSuppWarr.Visible == true)
                {
                    pnlWarr.Height = 488;
                }
                else
                {
                    pnlWarr.Height = 392;
                }
            }
            else
            {
                pnlWarr.Height = 109;
                btnMore1.Text = "More >>";
                btnMore1.BackColor = Color.Maroon;
                btnMore1.ForeColor = Color.White;
            }
        }

        #endregion Events

        #region Methods

        private void getJobJetails()
        {
            DataTable DtDetails = new DataTable();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            //stage = "3,2,6,4,5.1,5";
            stage = ddlStages.SelectedValue.ToString();
            //DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10816))
            {
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, "GET_ALL_JOBS");
            }
            else
            {
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, BaseCls.GlbUserID);
            }

            if (DtDetails.Rows.Count > 0)
            {
                DataView dv = DtDetails.DefaultView;
                dv.Sort = "jbd_jobline";
                DataTable sortedDT = dv.ToTable();

                dgvJobDetails.DataSource = sortedDT;
                modifyJobDetailGrid();


                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;
                lblAddrss.Text = oJOB_HDR.SJB_B_ADD1 + "  " + oJOB_HDR.SJB_B_ADD2 + "  " + oJOB_HDR.SJB_B_ADD3;
                lblTele.Text = oJOB_HDR.SJB_B_MOBINO;
                lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
                lblConPhone.Text = oJOB_HDR.SJB_CNT_PHNO;
                lblCustomerCode.Text = oJOB_HDR.SJB_CUST_CD;
                //kapila 23/2/2016
                lblContEmail.Text = oJOB_HDR.SJB_EMAIL;
                lblContNo.Text = oJOB_HDR.SJB_CNT_PHNO;
                lblContName.Text = oJOB_HDR.SJB_CNT_PERSON;

                //lblJobStage.Text = oJOB_HDR.SJB_JOBSTAGE_TEXT;
                lblJobCategori.Text = oJOB_HDR.SJB_JOBCAT;
                lblLevel.Text = oJOB_HDR.SJB_PRORITY;
                txtInstruction.Text = oJOB_HDR.SJB_TECH_RMK;
                txtJobRemarks.Text = oJOB_HDR.SJB_JOB_RMK;
                lblJobStageNew.Text = oJOB_HDR.SJB_JOBSTAGE.ToString();
                //if (oJOB_HDR.SJB_JOBSTAGE > 3)
                //{
                //    button1.Enabled = false;
                //}
                enableDisableBtns(false);
                //dgvJobDetails.Focus();

                if (sortedDT.Rows.Count == 1)
                {
                    dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0, 0));
                }
            }
            else
            {
                MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
                return;
            }
        }

        public void GetJobEMPS(string jobNo, Int32 lineNo)
        {
            //if (_isbulk == false)
            //{
            DataTable Dt = CHNLSVC.CustService.getServiceJobEmployees(jobNo, lineNo);
            dgvEmpDetails.DataSource = Dt;
            //}
            //else
            //{
            //    DataTable Dt = CHNLSVC.CustService.getServiceJobEmployees(jobNo, lineNo);
            //    _tblSerEmp.Merge(Dt);
            //    dgvEmpDetails.DataSource = _tblSerEmp;
            //}
        }

        private void LoadDefects(string jobNo, string lineNo)
        {
            //if (_isbulk == false)
            //{
            dgvDefects.DataSource = null;
            List<Service_Job_Defects> oDefets = new List<Service_Job_Defects>();
            oDefets = CHNLSVC.CustService.GetJobDefects(jobNo, Convert.ToInt32(lineNo), "J");
            dgvDefects.DataSource = oDefets;
            //}
            //else
            //{
            //   // dgvDefects.DataSource = null;
            //    List<Service_Job_Defects> oDefets = new List<Service_Job_Defects>();
            //    oDefets = CHNLSVC.CustService.GetJobDefects(jobNo, Convert.ToInt32(lineNo), "J");
            //    oDefetsbulk.AddRange(oDefets);
            //    dgvDefects.DataSource = oDefetsbulk;
            //}


        }

        private void getJobDetails(Int32 lineNo)
        {
            List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, lineNo, BaseCls.GlbUserComCode);
            lblStageText.Text = ojob_Det[0].StageText;

            if (ojob_Det[0].Jbd_techst_dt_man != DateTime.MinValue)
            {
                lblStartOpenDate.Text = ojob_Det[0].Jbd_techst_dt_man.ToString("dd/MMM/yyyy  hh:mm tt");
                enableDisableBtns(true);
                btnStartOpenJob.Enabled = false;
                FillItemDetails(ojob_Det[0]);
            }
            else
            {
                lblStartOpenDate.Text = "";
                enableDisableBtns(false);
                FillItemDetails(ojob_Det[0]);
            }
            if (ojob_Det[0].Jbd_techfin_dt_man != DateTime.MinValue)
            {
                lblCompleteDate.Text = ojob_Det[0].Jbd_techfin_dt_man.ToString("dd/MMM/yyyy  hh:mm tt");
            }
            else
            {
                lblCompleteDate.Text = "";
            }

            lblJobStage.Text = ojob_Det[0].StageText;
            lblJobStageNew.Text = ojob_Det[0].Jbd_stage.ToString();

            if (ojob_Det[0].Jbd_stage == 6)
            {
                btnStartOpenJob.Enabled = true;
            }
            if (ojob_Det[0].Jbd_stage == 5)
            {
                lblCompleteDate.Text = "";
            }
            gblJobStage = ojob_Det[0].Jbd_stage;

            if (ojob_Det[0].Jbd_stage >= 3 && ojob_Det[0].Jbd_stage < 6 && lblStartOpenDate.Text.Length > 0)
            {
                btnRequisition.Enabled = true;
                btnReciepts.Enabled = true;
                btnStockReturn.Enabled = true;
                btnOldPart.Enabled = true;
                btnConsumableItems.Enabled = true;
                btnComments.Enabled = true;
                btnVisitComment.Enabled = true;
                btnSuppWrntyClmReq.Enabled = true;
                btnSuppWarrtClmReceive.Enabled = true;
                btnAttachDoc.Enabled = true;
                btnTechnicians.Enabled = true;
                btnJobTask.Enabled = true;
                btnViewPay.Enabled = true;

                if (ojob_Det[0].Jbd_reqwcn == 1)
                {
                    btnOldPart.Enabled = false;
                }
                else
                {
                    btnOldPart.Enabled = true;
                }
            }
            else
            {
                btnRequisition.Enabled = false;
                btnReciepts.Enabled = false;
                btnStockReturn.Enabled = false;
                btnOldPart.Enabled = false;
                btnConsumableItems.Enabled = false;
                btnComments.Enabled = false;
                btnVisitComment.Enabled = false;
                btnSuppWrntyClmReq.Enabled = false;
                btnSuppWarrtClmReceive.Enabled = false;
                btnTempIssue.Enabled = false;
                btnAttachDoc.Enabled = false;
                btnTechnicians.Enabled = false;
                btnJobTask.Enabled = false;
                btnViewPay.Enabled = false;
            }

            if (gblJobStage < 3)
            {
                btnStartOpenJob.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                if (gblJobStage > 3)
                {
                    btnStartOpenJob.Enabled = false;
                }
                button2.Enabled = true; ;
            }
            if (gblJobStage == 3)
            {
                btnStartOpenJob.Enabled = true;
            }

            BindOutwardListGridData(txtJobNo.Text, lineNo);



            List<Service_job_Det> _preSerJob = new List<Service_job_Det>();
            _preSerJob = CHNLSVC.CustService.getPrejobDetails(BaseCls.GlbUserComCode, ojob_Det[0].Jbd_ser1, ojob_Det[0].Jbd_itm_cd);
            if (_preSerJob != null && _preSerJob.Count > 0)
            {
                lblAttempt.Text = (_preSerJob.Count - 1).ToString();
            }
            else
            {
                lblAttempt.Text = "0";
            }
        }

        private InventorySerialMaster FillReqItemDetails(Service_Req_Hdr _reqHdr, Service_Req_Det _reqItm)
        {
            InventorySerialMaster _warrItem = new InventorySerialMaster();

            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reqItm.Jrd_itm_cd);

            _warrItem.Irsm_itm_cd = _reqItm.Jrd_itm_cd;
            _warrItem.Irsm_itm_desc = _reqItm.Jrd_itm_desc;
            _warrItem.Irsm_itm_model = _reqItm.Jrd_model;
            _warrItem.Irsm_itm_brand = _reqItm.Jrd_brand;
            _warrItem.Irsm_ser_1 = _reqItm.Jrd_ser1;
            _warrItem.Irsm_warr_no = _reqItm.Jrd_warr;
            _warrItem.Irsm_doc_dt = _reqItm.Jrd_warrstartdt;
            _warrItem.Irsm_warr_period = _reqItm.Jrd_warrperiod;
            _warrItem.Irsm_warr_rem = _reqItm.Jrd_warrrmk;

            _warrItem.Irsm_invoice_no = _reqItm.Jrd_invc_no;
            _warrItem.Irsm_invoice_dt = _reqItm.Jrd_date_pur;
            _warrItem.Irsm_acc_no = _reqItm.Jrd_msnno;
            //_warrItem.Irsm_loc = "";
            //_warrItem.Irsm_loc_desc = "";

            _warrItem.Irsm_cust_mobile = _reqHdr.Srb_mobino;
            _warrItem.Irsm_cust_cd = _reqHdr.Srb_cust_cd;
            _warrItem.Irsm_cust_name = _reqHdr.Srb_cust_name;
            _warrItem.Irsm_cust_addr = _reqHdr.Srb_add1;

            //_warrItem.Irsm_orig_supp;
            //_warrItem.Irsm_exist_supp;
            _warrItem.Irsm_anal_3 = _masterItem.Mi_itm_tp;
            _warrItem.Irsm_anal_4 = _masterItem.Mi_cate_1;

            return _warrItem;
        }

        private Int32 getAllocationHeader(string jobNo, string lineNo)
        {
            List<Service_Tech_Aloc_Hdr> oheaders = CHNLSVC.CustService.GetJobAllocations(jobNo, Convert.ToInt32(lineNo), BaseCls.GlbUserComCode);
            if (oheaders != null && oheaders.Count > 0)
            {
                lblScheduleStart.Text = oheaders.Min(x => x.STH_FROM_DT).ToString("dd/MM/yyyy   hh:mm tt");
                lblScheduleEnd.Text = oheaders.Max(x => x.STH_TO_DT).ToString("dd/MM/yyyy   hh:mm tt");
                return oheaders[0].STH_JOBLINE;
            }
            else
            {
                return Convert.ToInt32(lineNo);
            }
        }

        private void modifyJobDetailGrid()
        {
            if (dgvJobDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (dgvJobDetails.Rows[i].Cells["Status"].Value.ToString() == "0")
                    {
                        dgvJobDetails.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                        dgvJobDetails.Rows[i].ReadOnly = true;
                        dgvJobDetails.Rows[i].Cells["select"].ReadOnly = true;
                    }
                }
            }
        }

        private void FillItemDetails(Service_job_Det _warrItem)
        {
            lblItem.Text = _warrItem.Jbd_itm_cd;
            lblItemDesc.Text = _warrItem.Jbd_itm_desc;
            lblModel.Text = _warrItem.Jbd_model;
            lblBrand.Text = _warrItem.Jbd_brand;
            lblSerNo.Text = _warrItem.Jbd_ser1;
            lblWarNo.Text = _warrItem.Jbd_warr;
            string _warrStatus = string.Empty;

            if (_warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).Date >= CHNLSVC.Security.GetServerDateTime().Date)
            {
                _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green;
            }
            else
            {
                _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red;
            }

            lblWarStus.Text = _warrStatus;
            lblWarStart.Text = _warrItem.Jbd_warrstartdt.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = _warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).ToString("dd-MMM-yyyy");
            lblWarPrd.Text = _warrItem.Jbd_warrperiod.ToString();
            lblWarRemain.Text = (_warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).Date - CHNLSVC.Security.GetServerDateTime().Date).TotalDays.ToString();
            lblWarRem.Text = _warrItem.Jbd_warrrmk;

            if (lblWarRemain.Text.Contains("-"))
            {
                lblWarRemain.Text = "N/A";
            }

            lblInv.Text = _warrItem.Jbd_invc_no;

            if (_warrItem.Jbd_date_pur < Convert.ToDateTime("01-01-1800"))
            {
                lblInvDt.Text = "";
            }
            else
            {
                lblInvDt.Text = _warrItem.Jbd_date_pur.ToString("dd-MMM-yyyy");
            }
            //lblAccNo.Text = _warrItem.Irsm_acc_no;
            //lblDelLoc.Text = _warrItem.Irsm_loc;
            //lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;

            MasterItem _itemdetail = new MasterItem();
            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _warrItem.Jbd_itm_cd);

            //txtMobile.Text = _warrItem.Irsm_cust_mobile;
            //txtCustCode.Text = _warrItem.Irsm_cust_cd;
            //txtCusName.Text = _warrItem.Irsm_cust_name;
            //txtAddress1.Text = _warrItem.Irsm_cust_addr;

            lblSuppCode.Text = _warrItem.Jbd_supp_cd;
            //lblItmTp.Text = _warrItem.Jbd_itmtp;
            lblItemCat.Text = (_itemdetail == null) ? string.Empty : _itemdetail.Mi_cate_1;

            //lblDelLoc.Text = _warrItem.Irsm_loc;
            //lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;
            //kapila 23/6/2017 - due to slowness
            if (_warrItem.Jbd_warr != "N/A")
            Load_Serial_Infor(_warrItem.Jbd_ser1, DateTime.Now.Date, _warrItem.Jbd_warr);

            Service_JOB_HDR _scvjobHdr = CHNLSVC.CustService.GetServiceJobHeader(_warrItem.Jbd_jobno, BaseCls.GlbUserComCode);

            lblBuyerCustCode.Text = _scvjobHdr.SJB_CUST_CD;
            lblBuyerCustName.Text = _scvjobHdr.SJB_CUST_NAME;
            lblBuyerCustAdd1.Text = _scvjobHdr.SJB_ADD1;
            lblBuyerCustAdd2.Text = _scvjobHdr.SJB_ADD2;
            lblBuyerCustMobi.Text = _scvjobHdr.SJB_MOBINO;
        }

        private void enableDisableBtns(bool doEnable)
        {


            if (doEnable == false)
            {
                btnRequisition.Enabled = true;
                btnReciepts.Enabled = true;
                btnStockReturn.Enabled = true;
                btnOldPart.Enabled = true;
                btnConsumableItems.Enabled = true;
                btnComments.Enabled = true;
                btnVisitComment.Enabled = true;
                btnSuppWrntyClmReq.Enabled = true;
                btnSuppWarrtClmReceive.Enabled = true;
                btnAttachDoc.Enabled = true;
                btnTechnicians.Enabled = true;
                btnJobTask.Enabled = true;    //kapila 24/4/2015
                btnViewPay.Enabled = true;
            }
            else
            {
                btnRequisition.Enabled = false;
                btnReciepts.Enabled = false;
                btnStockReturn.Enabled = false;
                btnOldPart.Enabled = false;
                btnConsumableItems.Enabled = false;
                btnComments.Enabled = false;
                btnVisitComment.Enabled = false;
                btnSuppWrntyClmReq.Enabled = false;
                btnSuppWarrtClmReceive.Enabled = false;
                btnAttachDoc.Enabled = false;
                btnTechnicians.Enabled = false;
                btnJobTask.Enabled = false;
                btnViewPay.Enabled = false;
            }
        }

        private void clrScreen()
        {
            txtJobNo.Clear();
            lblName.Text = "";
            lblTele.Text = "";
            lblContEmail.Text = "";
            lblContNo.Text = "";
            lblContName.Text = "";
            lblCustomerCode.Text = "";
            lblJobStage.Text = "";
            lblJobCategori.Text = "";
            lblLevel.Text = "";
            dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);
            dgvDefects.DataSource = new List<Service_Job_Defects>();
            lblScheduleEnd.Text = "";
            lblScheduleStart.Text = "";
            lblStartOpenDate.Text = "";
            lblCompleteDate.Text = "";
            lblJobStageNew.Text = "";
            SelectedJobLine = -1;
            Serial_No = "";
            pnlSelect.Visible = false;
            enableDisableBtns(false);
            lblItem.Text = "";
            lblItemDesc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblSerNo.Text = "";
            lblWarStus.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblAttempt.Text = "";
            lblWarPrd.Text = "";
            lblWarRemain.Text = "";
            lblWarRem.Text = "";
            lblWarNo.Text = "";
            lblAddrss.Text = "";
            lblStageText.Text = "";
            btnStartOpenJob.Enabled = true;
            button2.Enabled = true;
            DataTable Dt = CHNLSVC.CustService.getServiceJobEmployees("XXXX", -1);
            dgvEmpDetails.DataSource = Dt;
            txtInstruction.Clear();
            txtJobRemarks.Clear();
            ucServicePriority1.clearAll();
            lblJobCount.Text = "";
            getPendingJObCount();
        }

        private Boolean FormOpenAlready(string formName)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == formName)
                {
                    f.WindowState = FormWindowState.Normal;
                    f.Activate();
                    return true;
                }
            }

            return false;
        }

        private void Load_Serial_Infor(String _ser, DateTime _jobDt, String _warrnoum)
        {
            if (isLoad == true)
            {
                //return;
            }
            isLoad = true;

            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
            List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
            Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;

            string _warr = _warrnoum;
            string _regno = null;
            string _invcno = null;

            //if (string.IsNullOrEmpty(_warrNo))
            //{
            //    if (_txt == txtSer) _ser = txtSer.Text.ToString();
            //    if (_txt == txtWar) _warr = txtWar.Text.ToString();
            //    if (_txt == txtRegNo) _regno = txtWar.Text.ToString();
            //}
            //else
            //{
            //    _warr = _warrNo;
            //}
            if (!string.IsNullOrEmpty(_warr))
            {
                _ser = string.Empty;
            }

            _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, "", _regno, _warr, "", "", 0, out _returnStatus, out _returnMsg);
            if (_warrMstDic == null)
            {
                //SystemInformationMessage("There is no warranty details available.", "No warranty");
                //_txt.Clear();
                //_txt.Focus();
                return;
            }

            foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
            {
                _warrMst = pair.Key;
                _warrMstSub = pair.Value;
            }
            if (_warrMst == null)
            {
                //SystemInformationMessage("There is no warranty details available.", "No warranty");
                //_txt.Clear();
                //_txt.Focus();
                return;
            }
            if (_warrMst.Count <= 0)
            {
                //SystemInformationMessage("There is no warranty details available.", "No warranty");
                //_txt.Clear();
                //_txt.Focus();
                return;
            }


            if (_warrMst.Count > 1)
            {
                //gvMultipleItem.AutoGenerateColumns = false;
                //gvMultipleItem.DataSource = new List<InventorySerialMaster>();
                //gvMultipleItem.DataSource = _warrMst;
                //pnlMultiItems.Visible = true;
                return;
            }
            else
            {
                FillItemDetails(_warrMst[0], _jobDt);
            }
        }

        private void FillItemDetails(InventorySerialMaster _warrItem, DateTime _jobDt)
        {
            _warrItemTemp = new InventorySerialMaster();
            _warrItemTemp = _warrItem;

            lblItem.Text = _warrItem.Irsm_itm_cd;
            lblItemDesc.Text = _warrItem.Irsm_itm_desc;
            lblModel.Text = _warrItem.Irsm_itm_model;
            lblBrand.Text = _warrItem.Irsm_itm_brand;
            lblSerNo.Text = _warrItem.Irsm_ser_1;
            lblWarNo.Text = _warrItem.Irsm_warr_no;
            string _warrStatus = string.Empty;
            if (_warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).Date >= _jobDt.Date)
            { _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green; }
            else
            { _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red; }

            lblWarStus.Text = _warrStatus;
            lblWarStart.Text = _warrItem.Irsm_doc_dt.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = _warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).ToString("dd-MMM-yyyy");
            lblWarPrd.Text = _warrItem.Irsm_warr_period.ToString();
            //lblWarRemain.Text = (_warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).Date - dtDate.Value.Date).TotalDays.ToString();
            lblWarRem.Text = _warrItem.Irsm_warr_rem;
            //lblSwrrRemain.Text = (_warrItem.Irsm_sup_warr_stdt.AddMonths(_warrItem.Irsm_sup_warr_pd).Date - dtDate.Value.Date).TotalDays.ToString();
            lblSwrrPeriod.Text = _warrItem.Irsm_sup_warr_pd.ToString();
            lblSwrrRemark.Text = _warrItem.Irsm_sup_warr_rem;

            lblInv.Text = _warrItem.Irsm_invoice_no;
            lblInvDt.Text = _warrItem.Irsm_invoice_dt.ToString("dd-MMM-yyyy");
            lblAccNo.Text = _warrItem.Irsm_acc_no;
            lblDelLoc.Text = _warrItem.Irsm_loc;
            lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;

            //if (_jobRecall == 0)
            //{
            //    txtMobile.Text = _warrItem.Irsm_cust_mobile;
            //    txtCustCode.Text = _warrItem.Irsm_cust_cd;
            //    txtCusName.Text = _warrItem.Irsm_cust_name;
            //    txtAddress1.Text = _warrItem.Irsm_cust_addr;
            //}

            //List<Service_job_Det> _preSerJob = new List<Service_job_Det>();
            //_preSerJob = CHNLSVC.CustService.getPrejobDetails(BaseCls.GlbUserComCode, txtSer.Text.Trim(), lblItem.Text.Trim());

            //lblAttempt.Text = _preSerJob.Count.ToString();

            lblSuppCode.Text = _warrItem.Irsm_orig_supp;
            lblSuppName.Text = _warrItem.Irsm_exist_supp;
            //txtItmTp.Text = _warrItem.Irsm_anal_3;
            lblItemCat.Text = _warrItem.Irsm_anal_4;

            //_scvjobHdr.SJB_CUST_CD = _warrItem.Irsm_cust_cd;
            //_scvjobHdr.SJB_CUST_NAME = _warrItem.Irsm_cust_name;
            //_scvjobHdr.SJB_ADD1 = _warrItem.Irsm_cust_addr;
            //_scvjobHdr.SJB_MOBINO = _warrItem.Irsm_cust_mobile;

            //lblBuyerCustCode.Text = _scvjobHdr.SJB_CUST_CD;
            //lblBuyerCustName.Text = _scvjobHdr.SJB_CUST_NAME;
            //lblBuyerCustAdd1.Text = _scvjobHdr.SJB_ADD1;
            //lblBuyerCustAdd2.Text = _scvjobHdr.SJB_ADD2;
            //lblBuyerCustMobi.Text = _scvjobHdr.SJB_MOBINO;



            //FillPriority();

            if (_warrItem.Irsm_sup_warr_stdt == DateTime.MinValue)
            {
                pnlSuppWarr.Visible = false;
                pnlBottom.Location = new Point(0, 125);
            }
            else
            {
                pnlSuppWarr.Visible = true;
                pnlBottom.Location = new Point(0, 216);
            }

            //if (_warrItem.PartNumber != null && _warrItem.PartNumber != "N/A")
            //{
            //    lblPartNo.Text = _warrItem.PartNumber;
            //    lblPartNo.Visible = true;
            //    label111.Visible = true;
            //    label113.Visible = true;
            //    lblModel.Size = new System.Drawing.Size(67, 14);
            //}
            //else
            //{
            //    lblModel.Size = new System.Drawing.Size(203, 14);
            //    label111.Visible = false;
            //    label113.Visible = false;
            //    lblPartNo.Visible = false;
            //}

            //checkCustomer(null, _scvjobHdr.SJB_CUST_CD);
            //if (_isBlack == true)
            //{
            //    MessageBox.Show("Customer is black listed.", "Customer Infor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //if (txtCustCode.Text == "CASH")
            //{
            //    MessageBox.Show("Please select valid customer.Cannot use customer code as [CASH].", "Customer Infor", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //}
        }

        private void fillStages()
        {
            List<ComboBoxObject> oItems = new List<ComboBoxObject>();
            ComboBoxObject Item1 = new ComboBoxObject();
            ComboBoxObject Item2 = new ComboBoxObject();
            ComboBoxObject Item3 = new ComboBoxObject();
            ComboBoxObject Item4 = new ComboBoxObject();
            ComboBoxObject Item5 = new ComboBoxObject();
            ComboBoxObject Item6 = new ComboBoxObject();

            Item1.Text = "NEW JOB OPEN               ";
            Item2.Text = "TECHNICIAN ALLOCATED       ";
            Item3.Text = "JOB STARTED - TECHNICIAN   ";
            Item4.Text = "JOB REOPENED - TECHNICIAN  ";
            Item5.Text = "JOB COMMENTED - TECHNICIAN ";
            Item6.Text = "All";

            Item1.Value = "2";
            Item2.Value = "3";
            Item3.Value = "4";
            Item4.Value = "5";
            Item5.Value = "6";
            Item6.Value = "3,2,6,4,5.1,5";

            oItems.Add(Item6);
            oItems.Add(Item1);
            oItems.Add(Item2);
            oItems.Add(Item3);
            oItems.Add(Item4);
            oItems.Add(Item5);

            ddlStages.DataSource = oItems;
            ddlStages.DisplayMember = "Text";
            ddlStages.ValueMember = "Value";

            ddlStages.SelectedIndex = 0;
        }

        #endregion Methods

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            txtJobNo.Clear();
            lblName.Text = "";
            lblTele.Text = "";
            lblContEmail.Text = "";
            lblContNo.Text = "";
            lblContName.Text = "";
            lblCustomerCode.Text = "";
            lblJobStage.Text = "";
            lblJobCategori.Text = "";
            lblLevel.Text = "";
            dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);
            dgvDefects.DataSource = new List<Service_Job_Defects>();
            lblScheduleEnd.Text = "";
            lblScheduleStart.Text = "";
            lblStartOpenDate.Text = "";
            lblCompleteDate.Text = "";
            lblJobStageNew.Text = "";
            SelectedJobLine = -1;
            Serial_No = "";
            pnlSelect.Visible = false;

            enableDisableBtns(false);

            lblItem.Text = "";
            lblItemDesc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblSerNo.Text = "";
            lblWarStus.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblAttempt.Text = "";
            lblWarPrd.Text = "";
            lblWarRemain.Text = "";
            lblWarRem.Text = "";
            lblWarNo.Text = "";
            lblAddrss.Text = "";
            lblStageText.Text = "";
            lblInv.Text = "";
            lblInvDt.Text = "";
            lblAccNo.Text = "";
            lblDelLoc.Text = "";
            lblDelLocDesc.Text = "";

            btnReciepts.Text = "Receipts";

            DataTable Dt = CHNLSVC.CustService.getServiceJobEmployees("XXXX", -1);
            dgvEmpDetails.DataSource = Dt;

            txtInstruction.Clear();
            txtJobRemarks.Clear();

            ucServicePriority1.clearAll();
            dtpSelectedDateTime.Value = DateTime.Now;

            ddlStages.SelectedIndex = 0;

            lblJobCount.Text = "";
            getPendingJObCount();

            IsHavingGitItems = false;
            IsOldItemAdded = false;
        }

        private void lblAddrss_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblAddrss, lblAddrss.Text);
        }

        private void lblName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblName, lblName.Text);
        }

        private void btnAttachDoc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ImageUpload frm = new ImageUpload(txtJobNo.Text, SelectedJobLine, Serial_No, 0);
            frm.ShowDialog();
        }

        private void btnTechnicians_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10818))
            {
                MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10818", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            ServiceWIP_TechAllocation frm = new ServiceWIP_TechAllocation(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();

        }

        private void btnAddParts_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }


            ServiceWIP_AddParts frm = new ServiceWIP_AddParts(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnCloseWIP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void selectSameJobItem()
        {
            if (dgvJobDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (dgvJobDetails.Rows[i].Cells[0].Value != null && Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                    {
                        dgvJobDetails_CellClick(null, new DataGridViewCellEventArgs(0, i));
                    }
                }
            }
        }

        private void BindOutwardListGridData(String jobNum, Int32 jobLine)
        {
            btnReciepts.Text = "Receipts";
            int RecCount = 0;
            IsHavingGitItems = false; //Add by akila 2017/06/17

            try
            {
                InventoryHeader _inventoryRequest = new InventoryHeader();
                _inventoryRequest.Ith_oth_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Ith_doc_tp = "AOD";
                _inventoryRequest.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                _inventoryRequest.FromDate = "01-01-1900";
                _inventoryRequest.ToDate = "31-12-2999";

                DataTable _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                if (_table.Rows.Count <= 0)
                {
                    var _tblItems = from dr in _table.AsEnumerable()
                                    group dr by new
                                    {
                                        ith_doc_no = dr["ith_doc_no"],
                                        ith_doc_date = dr["ith_doc_date"],
                                        ith_doc_tp = dr["ith_doc_tp"],
                                        ith_manual_ref = dr["ith_manual_ref"],
                                        ith_com = dr["ith_com"],
                                        ith_loc = dr["ith_loc"],
                                        ith_bus_entity = dr["ith_bus_entity"],
                                        ith_sub_docno = dr["ith_sub_docno"]
                                    }
                                        into item
                                        select new
                                        {
                                            ith_doc_no = item.Key.ith_doc_no,
                                            ith_doc_date = item.Key.ith_doc_date,
                                            ith_doc_tp = item.Key.ith_doc_tp,
                                            ith_manual_ref = item.Key.ith_manual_ref,
                                            ith_com = item.Key.ith_com,
                                            ith_loc = item.Key.ith_loc,
                                            ith_bus_entity = item.Key.ith_bus_entity,
                                            ith_sub_docno = item.Key.ith_sub_docno
                                        };
                    RecCount = _tblItems.ToList().Count;
                }
                else
                {
                    DataTable dtTemp = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                    if (dtTemp.Select("ith_direct = 0 AND ith_job_no='" + jobNum + "'").Length > 0)
                    {
                        DataTable dtNew = dtTemp.Select("ith_direct = 0 AND ith_job_no='" + jobNum + "'").CopyToDataTable();
                        if (dtNew.Rows.Count > 0)
                        {
                            RecCount = dtNew.Rows.Count;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //List<ReptPickSerials> _list = new List<ReptPickSerials>();
                //_table = new DataTable();
                //_table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, OutwardType);
                //if (_table.Rows.Count <= 0)
                //{
                //    gvSerial.DataSource = _table;
                //    var _tblItems = from dr in _table.AsEnumerable() group dr by new { Tus_itm_cd = dr["Tus_itm_cd"], Tus_itm_desc = dr["Tus_itm_desc"], Tus_itm_model = dr["Tus_itm_model"], Tus_itm_stus = dr["Tus_itm_stus"] } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => 0) };
                //    gvItem.DataSource = _tblItems;
                //}

                if (RecCount > 0)
                {
                    btnReciepts.Text = "Receipts - " + RecCount.ToString();
                    btnReciepts.BackColor = Color.Thistle;
                    IsHavingGitItems = true;
                }
                else
                {
                    btnReciepts.Text = "Receipts";
                    btnReciepts.BackColor = btnRequisition.BackColor;
                    IsHavingGitItems = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            ServicePayments frm = new ServicePayments(txtJobNo.Text, 0, lblCustomerCode.Text);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnJobTask_Click(object sender, EventArgs e)
        {
            ServiceTasks frm = new ServiceTasks(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 100 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void txtJobNo_Click(object sender, EventArgs e)
        {
            txtJobNo.SelectAll();
        }

        private void btnPopupVehClose_Click(object sender, EventArgs e)
        {
            pnlAdditionalItems.Visible = false;
        }

        private void btnSubItems_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text) && dgvJobDetails.SelectedRows.Count > 0)
            {
                pnlAdditionalItems.Visible = true;
                Int32 jobLine = Convert.ToInt32(dgvJobDetails.SelectedRows[0].Cells["JobLine"].Value.ToString());
                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(txtJobNo.Text.Trim(), jobLine);
                grvAddiItems.AutoGenerateColumns = false;
                grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
                grvAddiItems.DataSource = oSubItems;
            }
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            CustomerSatisfaction frm = new CustomerSatisfaction(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X, this.Location.Y + 60);
            frm.ShowDialog();
        }

        private void getPendingJObCount()
        {
            DataTable dtTemp = CHNLSVC.CustService.GET_ALLALOCATEDJOBS_BY_USER(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                lblJobCount.Text = dtTemp.Rows.Count.ToString();
            }
        }

        private void dgvJobDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkbulk_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbulk.Checked)
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    dgvJobDetails.Rows[i].Cells["select"].Value = true;
                    dgvJobDetails.Rows[i].Cells["select"].Value = true;
                    LoadDefects(dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString());
                    GetJobEMPS(dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString()));
                    SelectedJobLine = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString());
                    Serial_No = dgvJobDetails.Rows[i].Cells["SerialNo"].Value.ToString();
                    Int32 linenum = getAllocationHeader(dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString());
                    getJobDetails(linenum);
                }



            }
            else
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    dgvJobDetails.Rows[i].Cells["select"].Value = false;
                }
            }


        }

        private void txtJobNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSupClaims_Click(object sender, EventArgs e)
        {
            pnlHistory.Width = 839;
            pnlHistory.Height = 517;
            pnlHistory.Visible = true;
            ucSupplierWarranty1.Visible = true;
            ucSupplierWarranty1.clerSWCLables();
            ucSupplierWarranty1.GblJobNumber = txtJobNo.Text;
            ucSupplierWarranty1.GblJobLine = SelectedJobLine;
            ucSupplierWarranty1.LoadData();
        }

        private void btnCloseSerialPnl_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = false;
        }

        private void btnAlocJobs_Click(object sender, EventArgs e)
        {
            Enquiries.Service.AllocPendingJobs frm = new Enquiries.Service.AllocPendingJobs();
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(0, 55);
            frm.ShowDialog();
        }

        private void btnHis_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Serial_No))
            {
                if (Serial_No.ToUpper().ToString() != "N/A")
                {
                    //  panel1.Enabled = false;
                    lblDefectHistyHeader.Text = ":: Job History :: [ Item :- " + lblItem.Text + " Serial :-" + lblSerNo.Text + " ]";
                    ucDefectHistory1.Serial = lblSerNo.Text;
                    ucDefectHistory1.Item = lblItem.Text;
                    ucDefectHistory1.loadData();
                    pnlHist.Show();

                }
            }
        }

        private void btnCloseHistory_Click(object sender, EventArgs e)
        {
            pnlHist.Hide();
            //   panel1.Enabled = true;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            ServiceJobAccept frm = new ServiceJobAccept(Convert.ToDecimal(2.2));
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void lblContName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblContName, lblContName.Text);
        }

        private void lblContNo_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblContNo, lblContNo.Text);
        }

        private void lblContEmail_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblContEmail, lblContEmail.Text);
        }

        private void pnlSelect_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPOItems_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (SelectedJobLine == -1)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            ServiceWIP_POItems frm = new ServiceWIP_POItems(txtJobNo.Text, SelectedJobLine);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }
    }
}