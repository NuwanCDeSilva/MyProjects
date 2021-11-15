using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceTasks : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        
        public ServiceTasks(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();

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


        private void getPayments()
        {
            List<JobTasks> _lstJobTasks = new List<JobTasks>();

            //Request
            //MRN
            DataTable dtReq = CHNLSVC.CustService.GetReqByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            if (dtReq != null && dtReq.Rows.Count > 0)
            {
                JobTasks _jobTaks = new JobTasks();
                _jobTaks.Task_Desc = "REQUEST";
                _jobTaks.Task_Date = Convert.ToDateTime(dtReq.Rows[0]["task_dt"]);
                _jobTaks.Task_Ref = dtReq.Rows[0]["task_ref"].ToString();
                _jobTaks.Task_UpdBy = dtReq.Rows[0]["task_by"].ToString();
                _jobTaks.Task_UpdTime = Convert.ToDateTime(dtReq.Rows[0]["task_when"]);
                _jobTaks.Task_userName = dtReq.Rows[0]["UserName"].ToString();
                _lstJobTasks.Add(_jobTaks);
            }

            //Job Stage Log
            DataTable dtLog = CHNLSVC.CustService.GetJobTaskByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            if (dtLog != null && dtLog.Rows.Count > 0)
            {                           
                foreach (DataRow dr in dtLog.Rows)
                {
                    JobTasks _jobTaks = new JobTasks();
                    _jobTaks.Task_Desc = dr["task_desc"].ToString().Trim();
                    _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
                    _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                    _jobTaks.Task_UpdBy = dr["task_by"].ToString().Trim();
                    _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
                    _jobTaks.Task_userName = dr["UserName"].ToString().Trim();
                   
                    _lstJobTasks.Add(_jobTaks);                    
                }
            }

            //MRN
            //DataTable dtTemp = CHNLSVC.CustService.GetJobMRNByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dtTemp.Rows)
            //    {
            //        JobTasks _jobTaks = new JobTasks();
            //        _jobTaks.Task_Desc = "MRN";
            //        _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
            //        _jobTaks.Task_Ref = dr["task_ref"].ToString();
            //        _jobTaks.Task_UpdBy = dr["task_by"].ToString();
            //        _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
            //        _lstJobTasks.Add(_jobTaks);
            //    }
            //}

            //Part remove
            DataTable dtPart = CHNLSVC.CustService.GetPartRemoveByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            if (dtPart != null && dtPart.Rows.Count > 0)
            {
                JobTasks _jobTaks = new JobTasks();
                _jobTaks.Task_Desc = "PART REMOVE";
                _jobTaks.Task_Date = Convert.ToDateTime(dtPart.Rows[0]["task_dt"]);
                _jobTaks.Task_Ref = dtPart.Rows[0]["task_ref"].ToString();
                _jobTaks.Task_UpdBy = dtPart.Rows[0]["task_by"].ToString();
                _jobTaks.Task_UpdTime = Convert.ToDateTime(dtPart.Rows[0]["task_when"]);
                _jobTaks.Task_userName = dtPart.Rows[0]["UserName"].ToString();
                _lstJobTasks.Add(_jobTaks);
            }

            //part transfered
            List<Service_Enquiry_PartTrasferd> oItem = CHNLSVC.CustService.GET_SCV_PART_TRSFER_ENQRY(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            for (int i = 0; i < oItem.Count; i++)
            {

            }

            //temporary issue details

            List<Service_TempIssue> stockReturnItems = new List<Service_TempIssue>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, "", BaseCls.GlbUserDefProf, "TMPI");
            for (int i = 0; i < stockReturnItems.Count; i++)
            {
                JobTasks _jobTaks = new JobTasks();
                _jobTaks.Task_Desc = "TEMP ISSUE";
                _jobTaks.Task_Date = Convert.ToDateTime(stockReturnItems[i].STI_DT);
                _jobTaks.Task_Ref = stockReturnItems[i].STI_DOC.ToString();
                _jobTaks.Task_UpdBy = stockReturnItems[i].STI_CRE_BY.ToString();
                _jobTaks.Task_UpdTime = Convert.ToDateTime(stockReturnItems[i].STI_CRE_DT);
                _jobTaks.Task_userName = stockReturnItems[i].USERNAME.ToString();
                _lstJobTasks.Add(_jobTaks);
            }

            //receipts
            DataTable dtRec = null;
            if (GblJobNum != "N/A")
            {
                dtRec = CHNLSVC.CustService.GetRecByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
                if (dtRec != null && dtRec.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRec.Rows)
                    {
                        JobTasks _jobTaks = new JobTasks();
                        _jobTaks.Task_Desc = "RECEIPT";
                        _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
                        _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                        _jobTaks.Task_UpdBy = dr["task_by"].ToString().Trim();
                        _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
                        _jobTaks.Task_userName = dr["UserName"].ToString().Trim();
                        
                        _lstJobTasks.Add(_jobTaks);
                    }
                }
            }
            //MRN / Quotation / estimate / sup war clain req / job transfer / war claim req send / receive / hold
            DataTable dtQ = null;
            if (GblJobNum != "N/A")
            {
                dtQ = CHNLSVC.CustService.GetJobTaskByJob(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
                if (dtQ != null && dtQ.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtQ.Rows)
                    {
                        JobTasks _jobTaks = new JobTasks();
                        _jobTaks.Task_Desc = dr["task_desc"].ToString().Trim();
                        _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
                        _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                        _jobTaks.Task_UpdBy = dr["task_by"].ToString().Trim();
                        _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
                        _jobTaks.Task_userName = dr["UserName"].ToString().Trim();
                        _jobTaks.Taskstutes = dr["STUS"].ToString().Trim();
                        _lstJobTasks.Add(_jobTaks);
                    }
                }
            }

            // Tharindu 2018-06-15

            if(chkExpandAll.Checked)
            {

                // Customer Comment 
                DataTable dtC = null;
                if (GblJobNum != "N/A")
                {
                    dtC = CHNLSVC.CustService.GetCustJobFeedback(GblJobNum);
                    if (dtC != null && dtC.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtC.Rows)
                        {
                            JobTasks _jobTaks = new JobTasks();
                            _jobTaks.Task_Desc = dr["SJF_FEEDBACK"].ToString().Trim();
                            _jobTaks.Task_Date = Convert.ToDateTime(dr["SJF_CRE_DT"]);
                            //  _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                            _jobTaks.Task_UpdBy = dr["sjf_cre_by"].ToString().Trim();
                            _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["sjf_cre_dt"]);
                            _jobTaks.Task_userName = dr["SJF_CRE_BY"].ToString().Trim();
                         //   _jobTaks.Taskstutes = dr["SE_USR_NAME"].ToString().Trim();
                            _lstJobTasks.Add(_jobTaks);
                        }
                    }
                }
            }
           

            _lstJobTasks = _lstJobTasks.OrderBy(x => x.Task_UpdTime).ToList();

            BindingSource _source = new BindingSource();
            _source.DataSource = _lstJobTasks;
            grvPending.DataSource = _source;

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
            getPayments();
            txtJobNo.Text = GblJobNum;
        }

        private void grvPending_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (grvPending.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = grvPending.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
                        Clipboard.SetText(_copyText.ToString());
                        MessageBox.Show(_copyText, "Copy to Clipboard");

                        #endregion Copy text
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void chkExpandAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkCollapse.Checked = false;
                getPayments();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
        }

        private void chkCollapse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkExpandAll.Checked = false;
                getPayments();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
        }
    }
}