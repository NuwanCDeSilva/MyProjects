using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Data;
using System.ComponentModel;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_VisitComment : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private Int32 SeqNumber = 0;
        private List<Tuple<Int32, String>> empList = new List<Tuple<Int32, String>>();
        private Int32 selectedLine = 0;

        private List<Service_VisitComments> oMainList = new List<Service_VisitComments>();
        private List<Service_job_visit_Technician> oMainEMPList = new List<Service_job_visit_Technician>();

        private bool mouseIsDown = false;
        private Point firstPoint;

        public ServiceWIP_VisitComment(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;

            InitializeComponent();

            //dgvActualDefects.AutoGenerateColumns = false;
            dgvRecords.AutoGenerateColumns = false;
            dgvTechnicians.AutoGenerateColumns = false;

            pnlAddNewRecord.Size = new Size(517, 296);
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

        private void ServiceWIP_VisitComment_Load(object sender, EventArgs e)
        {
            List<Service_job_Det> jobDetails = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            SeqNumber = jobDetails[0].Jbd_seq_no;
            getItems();
            getAllocationHeader(GblJobNum, GbljobLineNum.ToString());
            pnlAddNewRecord.Visible = false;
            oMainEMPList = CHNLSVC.CustService.GET_SCV_JOB_VISIT_TECH(GblJobNum, GbljobLineNum, SeqNumber);

        }

        #region Events

        private void btnAddDefect_Click(object sender, EventArgs e)
        {
            Service_VisitComments Item = new Service_VisitComments();
            Item.JTV_SEQ_NO = SeqNumber;
            Item.JTV_JOB_NO = GblJobNum;
            Item.JTV_JOB_LINE = GbljobLineNum;
            Item.JTV_VISIT_LINE = (oMainList.Count > 0) ? oMainList.Max(x => x.JTV_VISIT_LINE) + 1 : 1;
            Item.JTV_VISIT_FROM = dtpFrom.Value;
            Item.JTV_VISIT_TO = dtpTo.Value;
            Item.JTV_VISIT_RMK = txtComment.Text;
            Item.JTV_ACT = 1;
            Item.JTV_CRE_BY = BaseCls.GlbUserID;
            Item.JTV_CRE_DT = DateTime.Now;
            Item.JTV_MOD_BY = BaseCls.GlbUserID;
            Item.JTV_MOD_DT = DateTime.Now;
            oMainList.Add(Item);

            bindData();
        }

        private void btnSaveActualDefects_Click(object sender, EventArgs e)
        {
            if (oMainList.Count == 0)
            {
                MessageBox.Show("Please enter records.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (empList.Count == 0)
            {
                MessageBox.Show("Please add employees", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Int32 result1 = -1;

            result1 = CHNLSVC.CustService.SaveServiceVisitiComment(oMainList, empList);

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

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlAddNewRecord.Visible = false;
            dgvRecords.Enabled = true; ;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (dgvTechnicians.Rows.Count > 0)
            {
                if (isAnySelected())
                {
                    for (int i = 0; i < dgvTechnicians.Rows.Count; i++)
                    {
                        if (dgvTechnicians.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvTechnicians.Rows[i].Cells["select"].Value.ToString()) == true)
                        {
                            String empCode = dgvTechnicians.Rows[i].Cells["STH_EMP_CD"].Value.ToString();
                            empList.Add(new Tuple<Int32, String>(selectedLine, empCode));
                        }
                    }

                    pnlAddNewRecord.Visible = false;
                    dgvRecords.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAddNewRecord.Visible = false;
            dgvRecords.Enabled = true; ;
        }

        private void label10_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label10_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = pnlAddNewRecord.Location.X - xDiff;
                int y = pnlAddNewRecord.Location.Y - yDiff;
                pnlAddNewRecord.Location = new Point(x, y);
            }
        }

        private void label10_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void dgvRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == dgvRecords.Columns.IndexOf(dgvRecords.Columns["delete2"]))
                {
                    if (MessageBox.Show("Do you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string defectType = string.Empty;
                        Int32 lineNum = 0;

                        lineNum = Convert.ToInt32(dgvRecords.Rows[e.RowIndex].Cells["JTV_VISIT_LINE2"].Value.ToString());

                        oMainList.Remove(oMainList.Find(x => x.JTV_VISIT_LINE == lineNum));
                        bindData();
                    }
                }
                if (e.ColumnIndex == dgvRecords.Columns.IndexOf(dgvRecords.Columns["addemp2"]))
                {
                    oMainEMPList = CHNLSVC.CustService.GET_SCV_JOB_VISIT_TECH(GblJobNum, GbljobLineNum, SeqNumber);
                    List<Service_job_visit_Technician> selectedVisitLine = oMainEMPList.FindAll(x => x.JVT_VISIT_LINE == selectedLine);
                    for (int i = 0; i < dgvTechnicians.Rows.Count; i++)
                    {
                        string emp = dgvTechnicians.Rows[i].Cells["STH_EMP_CD"].Value.ToString();
                        if (selectedVisitLine.FindAll(x => x.JVT_EMP_CD == emp).Count > 0)
                        {
                            dgvTechnicians.Rows[i].Cells["select"].Value = true;
                        }
                        else
                        {
                            dgvTechnicians.Rows[i].Cells["select"].Value = false;
                        }
                    }

                    selectedLine = Convert.ToInt32(dgvRecords.Rows[e.RowIndex].Cells["JTV_VISIT_LINE2"].Value.ToString());
                    dgvRecords.Enabled = false;
                    pnlAddNewRecord.Show();
                }
                if (e.ColumnIndex == dgvRecords.Columns.IndexOf(dgvRecords.Columns["selectPrint"]))
                {
                    for (int i = 0; i < dgvRecords.Rows.Count; i++)
                    {
                        dgvRecords.Rows[i].Cells["selectPrint"].Value = false;
                    }
                    dgvRecords.Rows[e.RowIndex].Cells["selectPrint"].Value = true;
                }
            }
        }

        private void dgvRecords_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvRecords.IsCurrentCellDirty)
            {
                dgvRecords.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvRecords_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string defectType = string.Empty;
            Int32 lineNum = 0;
            lineNum = Convert.ToInt32(dgvRecords.Rows[e.RowIndex].Cells["JTV_VISIT_LINE2"].Value.ToString());
            Service_VisitComments item = oMainList.Find(x => x.JTV_VISIT_LINE == lineNum);
            item.JTV_VISIT_RMK = dgvRecords.Rows[e.RowIndex].Cells["JTV_VISIT_RMK2"].Value.ToString();
        }

        #endregion Events

        #region Methods

        private void getItems()
        {
            oMainList = new List<Service_VisitComments>();
            oMainList = CHNLSVC.CustService.GET_SCV_JOB_VISIT_COMNT(GblJobNum, GbljobLineNum);
            bindData();
        }

        private void bindData()
        {

            DataTable dtTemp = new DataTable();
            dtTemp = ToDataTable(oMainList);
            dgvRecords.DataSource = new List<Service_VisitComments>();
            dgvRecords.DataSource = dtTemp;

            txtComment.Clear();
            txtComment.Focus();
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void getAllocationHeader(string jobNo, string lineNo)
        {
            List<Service_Tech_Aloc_Hdr> oheaders = CHNLSVC.CustService.GetJobAllocations(jobNo, Convert.ToInt32(lineNo), BaseCls.GlbUserComCode);
            if (oheaders != null && oheaders.Count > 0)
            {
                dgvTechnicians.DataSource = new List<Service_Tech_Aloc_Hdr>();
                dgvTechnicians.DataSource = oheaders;
            }
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvTechnicians.Rows.Count > 0)
            {
                for (int i = 0; i < dgvTechnicians.Rows.Count; i++)
                {
                    if (dgvTechnicians.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvTechnicians.Rows[i].Cells["select"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        #endregion Methods

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDefect_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtComment.Text.Trim()))
            {
                MessageBox.Show("Please enter comment.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComment.Focus();
                return;
            }
            if (dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("Please enter a valid date range", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Service_VisitComments Item = new Service_VisitComments();
            Item.JTV_SEQ_NO = SeqNumber;
            Item.JTV_JOB_NO = GblJobNum;
            Item.JTV_JOB_LINE = GbljobLineNum;
            Item.JTV_VISIT_LINE = (oMainList.Count > 0) ? oMainList.Max(x => x.JTV_VISIT_LINE) + 1 : 1;
            Item.JTV_VISIT_FROM = dtpFrom.Value;
            Item.JTV_VISIT_TO = dtpTo.Value;
            Item.JTV_VISIT_RMK = txtComment.Text;
            Item.JTV_ACT = 1;
            Item.JTV_CRE_BY = BaseCls.GlbUserID;
            Item.JTV_CRE_DT = DateTime.Now;
            Item.JTV_MOD_BY = BaseCls.GlbUserID;
            Item.JTV_MOD_DT = DateTime.Now;
            oMainList.Add(Item);

            bindData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
            }
        }

        private void clearAll()
        {
            txtComment.Clear();
            dgvRecords.DataSource = new List<Service_VisitComments>();
            pnlAddNewRecord.Visible = false;
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to print the report?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int lineNumber = 0;
                for (int i = 0; i < dgvRecords.Rows.Count; i++)
                {
                    if (dgvRecords.Rows[i].Cells["selectPrint"].Value != null && Convert.ToBoolean(dgvRecords.Rows[i].Cells["selectPrint"].Value) == true)
                    {
                        lineNumber = Convert.ToInt32(dgvRecords.Rows[i].Cells["JTV_VISIT_LINE2"].Value.ToString());
                        break;
                    }
                }
                BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                BaseCls.GlbReportDoc = GblJobNum;
                BaseCls.GlbReportHeading = "Field visit job sheet";
                BaseCls.GlbReportParaLine1 = GbljobLineNum;
                BaseCls.GlbReportParaLine2 = lineNumber;

                Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                //BaseCls.GlbReportName = "JobSheet_FieldVisit.rpt";
                BaseCls.GlbReportName = "JobCard_ITS_Field.rpt";
                _view.Show();
                _view = null;
            }
        }
    }
}