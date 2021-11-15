using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServicePayments : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private Int32 SeqNumber = 0;
        private string GblCustCode = string.Empty;

        private List<Service_Job_Defects> oDefectMainList = new List<Service_Job_Defects>();
        private List<Service_Job_Tech_Comments> oMainListCommnets = new List<Service_Job_Tech_Comments>();

        public ServicePayments(string job, Int32 jobLine,string CustCd)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            GblCustCode = CustCd;
            InitializeComponent();

            grvPending.AutoGenerateColumns = false;
            grvPayment.AutoGenerateColumns = false;
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
            grvPending.DataSource = oDefectMainList;

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
            grvPayment.DataSource = new List<Service_Job_Tech_Comments>();
            grvPayment.DataSource = oMainListCommnets;

        }



        private void dgvActualDefects_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void dgvActualDefects_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string defectType = string.Empty;
            Int32 lineNum = 0;
            defectType = grvPending.Rows[e.RowIndex].Cells["SRD_DEF_TP"].Value.ToString();
            lineNum = Convert.ToInt32(grvPending.Rows[e.RowIndex].Cells["SRD_DEF_LINE"].Value.ToString());
            Service_Job_Defects item = oDefectMainList.Find(x => x.SRD_DEF_LINE == lineNum && x.SRD_DEF_TP == defectType);
            item.SRD_DEF_RMK = grvPending.Rows[e.RowIndex].Cells["SRD_DEF_RMK"].Value.ToString();
        }


        private void getPayments()
        {
            DataTable _dt = CHNLSVC.CustService.GetJobServiceCharge(GblJobNum, GbljobLineNum,GblCustCode);
            grvPending.AutoGenerateColumns = false;
            grvPending.DataSource = _dt;

            DataTable _dt1 = CHNLSVC.CustService.GetReceiptByJobNo(GblJobNum, GbljobLineNum);
            grvPayment.AutoGenerateColumns = false;
            grvPayment.DataSource = _dt1;
        }


        private void dgvTechnicianComment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string defectType = string.Empty;
            Int32 lineNum = 0;

            defectType = grvPayment.Rows[e.RowIndex].Cells["JTC_CMT_TP"].Value.ToString();
            lineNum = Convert.ToInt32(grvPayment.Rows[e.RowIndex].Cells["JTC_CMT_LINE"].Value.ToString());

            Service_Job_Tech_Comments item = oMainListCommnets.Find(x => x.JTC_CMT_TP == defectType && x.JTC_CMT_LINE == lineNum);
            item.JTC_CMT_RMK = grvPayment.Rows[e.RowIndex].Cells["JTC_CMT_RMK"].Value.ToString();
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
    }
}