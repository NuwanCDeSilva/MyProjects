using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceJobAccept : Base
    {
        private static decimal _jobStage = 0;
        private List<Service_Job_StageLog> _mainList = new List<Service_Job_StageLog>();
        public ServiceJobAccept(decimal _JbStage)
        {

            InitializeComponent();
            _jobStage = _JbStage;
            if (_jobStage == Convert.ToDecimal(2.2))
                this.Text = "Allocated Job Acceptance";
            else
                this.Text = "Completed Job Acceptance";
            grvPending.AutoGenerateColumns = false;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ServicePayments_Load(object sender, EventArgs e)
        {
            load_pending_jobs();
        }

        private void load_pending_jobs()
        {
            SystemUser _sysUser = new SystemUser();
            _sysUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
            int _isTech = BaseCls.GlbUserCategory == "TECH" ? 1 : 0;
            DataTable _dt = CHNLSVC.CustService.GetAcceptPendingJobs(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerial.Text, txtJobNo.Text, _sysUser.Se_emp_cd, BaseCls.GlbDefSubChannel, _isTech, _jobStage);

            grvPending.AutoGenerateColumns = false;
            grvPending.DataSource = _dt;
        }

        private void grvPending_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grvPending_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvPending.IsCurrentCellDirty)
            {
                if (grvPending.IsCurrentCellDirty)
                {
                    grvPending.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            decimal _newJobStage = 0;
            if (_jobStage == Convert.ToDecimal(2.2))
                _newJobStage = Convert.ToDecimal(3);
            else
                _newJobStage = Convert.ToDecimal(8);
            foreach (DataGridViewRow row in grvPending.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = row.Cells["JBD_JOBNO"].Value.ToString();
                    oLog.SJL_JOBLINE = Convert.ToInt32(row.Cells["JBD_JOBLINE"].Value);
                    oLog.SJL_COM = BaseCls.GlbUserComCode;
                    oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                    oLog.SJL_JOBSTAGE = _newJobStage;
                    oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                    oLog.SJL_INFSUP = 0;
                    _mainList.Add(oLog);
                }
            }

            Int32 _eff = CHNLSVC.CustService.UpdateAcceptedPendJobs(_mainList);
            if (_eff > 0)
            {
                MessageBox.Show("Successfully Updated. ", "Pending Jobs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            else
            {
                MessageBox.Show("Not updated ", "Pending Jobs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clear()
        {
            grvPending.AutoGenerateColumns = false;
            grvPending.DataSource = null;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            load_pending_jobs();
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void btn_srch_job_Click(object sender, EventArgs e)
        {
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
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        paramsText.Append("Serial #" + seperator + "SER" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,2,6,4,5.1,5" + seperator + "GET_ALL_JOBS" + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }
        private void chkAllSer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSer.Checked == true)
            {
                txtSerial.Enabled = false;
                btn_srch_ser.Enabled = false;
            }
            else
            {
                txtSerial.Enabled = true;
                btn_srch_ser.Enabled = true;
            }
        }

        private void chkAllJob_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllJob.Checked == true)
            {
                txtJobNo.Enabled = false;
                btn_srch_job.Enabled = false;
            }
            else
            {
                txtJobNo.Enabled = true;
                btn_srch_job.Enabled = true;
            }
        }

        private void btn_srch_ser_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.ShowDialog();
                txtSerial.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {  }
        }
    }
}