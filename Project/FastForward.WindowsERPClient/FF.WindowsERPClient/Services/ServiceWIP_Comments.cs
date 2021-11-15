using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_Comments : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private Int32 SeqNumber = 0;

        private List<Service_Job_Defects> oDefectMainList = new List<Service_Job_Defects>();

        private List<Service_Job_Tech_Comments> oMainListCommnets = new List<Service_Job_Tech_Comments>();

        public ServiceWIP_Comments(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();

            dgvActualDefects.AutoGenerateColumns = false;
            dgvTechnicianComment.AutoGenerateColumns = false;
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
                        string ItmCode = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode)[0].Jbd_itm_cd;
                        string cateCode = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, ItmCode).Mi_cate_1;
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefSubChannel + seperator + cateCode + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "2" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TechComments:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefSubChannel + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void ServiceWIP_Comments_Load(object sender, EventArgs e)
        {
            getDefects();
            GetComments();
        }

        private void btnSaveActualDefects_Click(object sender, EventArgs e)
        {
            if (oDefectMainList.Count == 0)
            {
                MessageBox.Show("Please enter defects", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Int32 result1 = -1;

            result1 = CHNLSVC.CustService.SAVE_SCV_JOB_DEFECTS(oDefectMainList);

            if (result1 > 0)
            {
                MessageBox.Show("Record successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            oDefectMainList = new List<Service_Job_Defects>();
            dgvActualDefects.DataSource = oDefectMainList;
            txtDefect.Clear();
            txtRemarkActcual.Clear();
        }

        private void btnAddDefect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDefect.Text))
            {
                MessageBox.Show("please enter defect type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDefect.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRemarkActcual.Text))
            {
                MessageBox.Show("please enter a remark.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarkActcual.Focus();
                return;
            }

            Service_Job_Defects item = new Service_Job_Defects();
            item.SRD_SEQ_NO = SeqNumber;
            item.SRD_JOB_NO = GblJobNum;
            item.SRD_JOB_LINE = GbljobLineNum;
            item.SRD_DEF_TP = txtDefect.Text;
            //item.SRD_DEF_LINE = (oDefectMainList.Count > 0) ? oDefectMainList.Max(x => x.SRD_DEF_LINE) + 1 : 1;
            item.SRD_DEF_LINE = 0;
            item.SRD_DEF_RMK = txtRemarkActcual.Text;
            item.SRD_ACT = 1;
            item.SRD_CRE_DT = DateTime.Now;
            item.SRD_CRE_BY = BaseCls.GlbUserID;
            item.SRD_MOD_DT = DateTime.Now;
            item.SRD_MOD_BY = BaseCls.GlbUserID;
            item.SRD_STAGE = "W";
            oDefectMainList.Add(item);

            bindDefects();
            txtDefect.Clear();
            txtRemarkActcual.Clear();
        }

        private void btnSaveTechComments_Click(object sender, EventArgs e)
        {
            if (oMainListCommnets == null || oMainListCommnets.Count == 0)
            {
                MessageBox.Show("please enter records.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Int32 result1 = -1;


            result1 = CHNLSVC.CustService.SaveTechnicainComments(oMainListCommnets);

            if (result1 > 0)
            {
                MessageBox.Show("Record successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            oMainListCommnets = new List<Service_Job_Tech_Comments>();
            dgvTechnicianComment.DataSource = new List<Service_Job_Tech_Comments>();
            dgvTechnicianComment.DataSource = oMainListCommnets;
            txtComment.Clear();
            txtCommentRemark.Clear();
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtComment.Text))
            {
                MessageBox.Show("please enter comment type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComment.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCommentRemark.Text))
            {
                MessageBox.Show("please enter a remark.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCommentRemark.Focus();
                return;
            }

            Service_Job_Tech_Comments item = new Service_Job_Tech_Comments();

            item.JTC_SEQ_NO = SeqNumber;
            item.JTC_JOB_NO = GblJobNum;
            item.JTC_JOB_LINE = GbljobLineNum;
            item.JTC_CMT_LINE = (oMainListCommnets.Count > 0) ? oMainListCommnets.Max(x => x.JTC_CMT_LINE) + 1 : 1;
            item.JTC_CMT_LINE = 0;
            item.JTC_CMT_TP = txtComment.Text;
            item.JTC_CMT_RMK = txtCommentRemark.Text;
            item.JTC_ACT = 1;
            item.JTC_CRE_BY = BaseCls.GlbUserID;
            item.JTC_CRE_DT = DateTime.Now;
            item.JTC_MOD_BY = BaseCls.GlbUserID;
            item.JTC_MOD_DT = DateTime.Now;

            oMainListCommnets.Add(item);

            bindComment();
            txtCommentRemark.Clear();
            txtComment.Clear();
            txtComment.Focus();
        }

        private void txtDefect_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                _result = CHNLSVC.CommonSearch.GetDefectTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDefect;
                _CommonSearch.ShowDialog();
                txtDefect.Select();
                Cursor = Cursors.Default;
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

        private void txtDefect_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDefect.Text))
            {
                List<Service_Defect_Types> items = CHNLSVC.CustService.GetDefectTypes(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
                if (items.Count > 0 && items.FindAll(x => x.SDT_TP == txtDefect.Text).Count > 0)
                {
                }
                else
                {
                    MessageBox.Show("Please enter valied Defect Type for the channel", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDefect.Clear();
                    txtDefect.Focus();
                    return;
                }
            }
        }

        private void txtDefect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDefect_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtRemarkActcual.Focus();
            }
        }

        private void txtRemarkActcual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddDefect.Focus();
            }
        }

        private void dgvActualDefects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == dgvActualDefects.Columns.IndexOf(dgvActualDefects.Columns["delete"]))
                {
                    if (MessageBox.Show("Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string defectType = string.Empty;
                        Int32 lineNum = 0;

                        defectType = dgvActualDefects.Rows[e.RowIndex].Cells["SRD_DEF_TP"].Value.ToString();
                        lineNum = Convert.ToInt32(dgvActualDefects.Rows[e.RowIndex].Cells["SRD_DEF_LINE"].Value.ToString());

                        oDefectMainList.Remove(oDefectMainList.Find(x => x.SRD_DEF_LINE == lineNum && x.SRD_DEF_TP == defectType));
                        bindDefects();
                    }
                }
            }
        }

        private void dgvActualDefects_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void dgvActualDefects_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string defectType = string.Empty;
            Int32 lineNum = 0;
            defectType = dgvActualDefects.Rows[e.RowIndex].Cells["SRD_DEF_TP"].Value.ToString();
            lineNum = Convert.ToInt32(dgvActualDefects.Rows[e.RowIndex].Cells["SRD_DEF_LINE"].Value.ToString());
            Service_Job_Defects item = oDefectMainList.Find(x => x.SRD_DEF_LINE == lineNum && x.SRD_DEF_TP == defectType);
            item.SRD_DEF_RMK = dgvActualDefects.Rows[e.RowIndex].Cells["SRD_DEF_RMK"].Value.ToString();
        }

        private void txtComment_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TechComments);
                _result = CHNLSVC.CommonSearch.SERCH_TECHCOMMTBYCHNNL(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComment;
                _CommonSearch.ShowDialog();
                txtComment.Select();
                Cursor = Cursors.Default;
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

        private void txtComment_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComment.Text))
            {
                List<Service_Tech_Comment> oTech_Comment = CHNLSVC.CustService.GetTechCommtByChnnl(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
                if (oTech_Comment.FindAll(x => x.STC_CD == txtComment.Text).Count == 0)
                {
                    MessageBox.Show("please enter correct comment code.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComment.Clear();
                    txtComment.Focus();
                    return;
                }
            }
        }

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCommentRemark.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtComment_DoubleClick(null, null);
            }
        }

        private void txtCommentRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddComment.Focus();
            }
        }

        private void bindDefects()
        {
            dgvActualDefects.DataSource = new List<Service_Job_Defects>();
            dgvActualDefects.DataSource = oDefectMainList;
        }

        private void bindComment()
        {
            dgvTechnicianComment.DataSource = new List<Service_Job_Tech_Comments>();
            dgvTechnicianComment.DataSource = oMainListCommnets;
        }

        private void getDefects()
        {
            oDefectMainList = CHNLSVC.CustService.GetJobDefects(GblJobNum, GbljobLineNum, "W");
            bindDefects();
            //List<Service_Job_Defects> TempList = CHNLSVC.CustService.GetJobDefects(GblJobNum, GbljobLineNum, "J");
            //SeqNumber = TempList[0].SRD_SEQ_NO;
            List<Service_job_Det> jobDetails = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            SeqNumber = jobDetails[0].Jbd_seq_no;
        }

        private void GetComments()
        {
            oMainListCommnets = CHNLSVC.CustService.GetServiceJobTechComments(SeqNumber, GblJobNum, GbljobLineNum);
            bindComment();
        }

        private void dgvTechnicianComment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == dgvTechnicianComment.Columns.IndexOf(dgvTechnicianComment.Columns["delectC"]))
                {
                    if (MessageBox.Show("Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string defectType = string.Empty;
                        Int32 lineNum = 0;

                        defectType = dgvTechnicianComment.Rows[e.RowIndex].Cells["JTC_CMT_TP"].Value.ToString();
                        lineNum = Convert.ToInt32(dgvTechnicianComment.Rows[e.RowIndex].Cells["JTC_CMT_LINE"].Value.ToString());

                        oMainListCommnets.Remove(oMainListCommnets.Find(x => x.JTC_CMT_LINE == lineNum && x.JTC_CMT_TP == defectType));
                        bindComment();
                    }
                }
            }
        }

        private void dgvTechnicianComment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string defectType = string.Empty;
            Int32 lineNum = 0;

            defectType = dgvTechnicianComment.Rows[e.RowIndex].Cells["JTC_CMT_TP"].Value.ToString();
            lineNum = Convert.ToInt32(dgvTechnicianComment.Rows[e.RowIndex].Cells["JTC_CMT_LINE"].Value.ToString());

            Service_Job_Tech_Comments item = oMainListCommnets.Find(x => x.JTC_CMT_TP == defectType && x.JTC_CMT_LINE == lineNum);
            item.JTC_CMT_RMK = dgvTechnicianComment.Rows[e.RowIndex].Cells["JTC_CMT_RMK"].Value.ToString();
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            txtDefect_DoubleClick(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtComment_DoubleClick(null, null);
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
    }
}