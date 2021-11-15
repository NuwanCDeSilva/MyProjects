using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.UserControls
{
    public partial class ucDefectHistory : UserControl
    {
        Base _basePage;

        private Service_job_Det oJobDetaill_DH;
        private List<Service_Job_Defects> oJobDefects_DH;
        private List<Service_Enquiry_TechAllo_Hdr> oJobAllocations_DH;
        private List<Service_Enquiry_Tech_Cmnt> oTechComments_DH;
        private List<Service_Enquiry_StandByItems> oStandByItems_DH;
        List<Tuple<string, string, string>> ConRemark_Type_User_DH = new List<Tuple<string, string, string>>();

        private string _Serial = string.Empty;
        private string _Item = string.Empty;

        public ucDefectHistory()
        {
            InitializeComponent();
        }

        public string Serial
        {
            get { return _Serial; }
            set { _Serial = value; }
        }

        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private void getSerialDetails(String Serial, String Item)
        {
            dgvADActualDefecsD10.DataSource = new List<Service_Job_Defects>();
            _basePage = new Base();
            dgvSerialDetailsD9.DataSource = new List<Service_job_Det>();
            List<Service_job_Det> oJobItems = _basePage.CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(Serial, Item, BaseCls.GlbUserComCode);

            oJobItems.Where(x => x.Jbd_warr_stus == 1).ToList().ForEach(x => x.Jbd_warr_stus_text = "Active");
            oJobItems.Where(x => x.Jbd_warr_stus == 0).ToList().ForEach(x => x.Jbd_warr_stus_text = "Inactive");

            dgvSerialDetailsD9.DataSource = oJobItems;

            //kapila 5/3/2016
            if (dgvSerialDetailsD9.Rows.Count > 0)
            {
                dgvSerialDetailsD9.Rows[0].Cells["dataGridViewCheckBoxColumn1"].Value = true;
                string msg = "";
                decimal totalAmount = 0;

                int result = _basePage.CHNLSVC.CustService.GetAllJobDetailsEnquiry(dgvSerialDetailsD9.Rows[0].Cells["Jbd_jobnoD9"].Value.ToString(),Convert.ToInt32( dgvSerialDetailsD9.Rows[0].Cells["Jbd_joblineD9"].Value), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out oJobDetaill_DH, out oJobDefects_DH, out oJobAllocations_DH, out oTechComments_DH, out ConRemark_Type_User_DH, out oStandByItems_DH, out msg, out totalAmount);
                linkLabel1_LinkClicked(null, null);
            }
        }

        private void dgvSerialDetailsD9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvADActualDefecsD10.DataSource = new List<Service_Job_Defects>();

            if (e.RowIndex > -1)
            {
                for (int i = 0; i < dgvSerialDetailsD9.Rows.Count; i++)
                {
                    dgvSerialDetailsD9.Rows[i].Cells[0].Value = false;
                }
                dgvSerialDetailsD9.Rows[e.RowIndex].Cells[0].Value = true;

                GetDefectHistry();
            }
        }

        private void GetDefectHistry()
        {
            String Jobnumber = string.Empty;
            Int32 jobLineNum = 0;

            Jobnumber = dgvSerialDetailsD9.SelectedRows[0].Cells["Jbd_jobnoD9"].Value.ToString();
            jobLineNum = Convert.ToInt32(dgvSerialDetailsD9.SelectedRows[0].Cells["Jbd_joblineD9"].Value.ToString());

            string msg;
            Decimal totalAmount = 0;
            Cursor = Cursors.WaitCursor;
            _basePage = new Base();
            int result = _basePage.CHNLSVC.CustService.GetAllJobDetailsEnquiry(Jobnumber, jobLineNum, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out oJobDetaill_DH, out oJobDefects_DH, out oJobAllocations_DH, out oTechComments_DH, out ConRemark_Type_User_DH, out oStandByItems_DH, out msg, out totalAmount);
            Cursor = Cursors.Default;

            if (result > 0)
            {
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isAnySelected())
            {
                MessageBox.Show("Please select item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvADActualDefecsD10.Columns.Clear();
            dgvADActualDefecsD10.Columns.Add("SDT_DESCD10", "Defect");
            dgvADActualDefecsD10.Columns.Add("SRD_DEF_RMKD10", "Remark");
            dgvADActualDefecsD10.Columns["SDT_DESCD10"].DataPropertyName = "SDT_DESC";
            dgvADActualDefecsD10.Columns["SRD_DEF_RMKD10"].DataPropertyName = "SRD_DEF_RMK";

            dgvADActualDefecsD10.DataSource = new List<Service_Job_Defects>();

            dgvADActualDefecsD10.DataSource = oJobDefects_DH;
            // Commented by Nadeeka 20-02-2015
            //if (oJobDefects_DH.Count > 0)
            //{
            //    if (oJobDefects_DH.FindAll(x => x.SRD_STAGE == "W").Count > 0)
            //    {
            //        dgvADActualDefecsD10.DataSource = oJobDefects_DH.FindAll(x => x.SRD_STAGE == "W");
            //    }
            //}

            dgvADActualDefecsD10.Columns["SDT_DESCD10"].Width = 150;
            dgvADActualDefecsD10.Columns["SRD_DEF_RMKD10"].Width = 350;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isAnySelected())
            {
                MessageBox.Show("Please select item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvADActualDefecsD10.Columns.Clear();
            dgvADActualDefecsD10.Columns.Add("ITS_ITM_CDD10", "Item Code");
            dgvADActualDefecsD10.Columns.Add("ITS_ITM_STUSD10", "Status");
            dgvADActualDefecsD10.Columns.Add("ITS_SER_1D10", "Serial");
            dgvADActualDefecsD10.Columns.Add("MI_LONGDESCD10", "Description");

            dgvADActualDefecsD10.Columns["ITS_ITM_CDD10"].DataPropertyName = "ITS_ITM_CD";
            dgvADActualDefecsD10.Columns["ITS_ITM_STUSD10"].DataPropertyName = "ITS_ITM_STUS";
            dgvADActualDefecsD10.Columns["ITS_SER_1D10"].DataPropertyName = "ITS_SER_1";
            dgvADActualDefecsD10.Columns["MI_LONGDESCD10"].DataPropertyName = "MI_LONGDESC";

            String Jobnumber = string.Empty;
            Int32 jobLineNum = 0;
            Jobnumber = dgvSerialDetailsD9.SelectedRows[0].Cells["Jbd_jobnoD9"].Value.ToString();
            jobLineNum = Convert.ToInt32(dgvSerialDetailsD9.SelectedRows[0].Cells["Jbd_joblineD9"].Value.ToString());

            Cursor = Cursors.WaitCursor;
            List<Service_Enquiry_InventryItems> oItems = _basePage.CHNLSVC.CustService.GET_INVITMS_BYJOBLINE_ENQRY(Jobnumber, jobLineNum, BaseCls.GlbUserComCode);
            Cursor = Cursors.Default;
            dgvADActualDefecsD10.DataSource = new List<Service_Enquiry_InventryItems>();
            dgvADActualDefecsD10.DataSource = oItems;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isAnySelected())
            {
                MessageBox.Show("Please select item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvADActualDefecsD10.Columns.Clear();
            dgvADActualDefecsD10.Columns.Add("STH_EMP_CDD10", "EPF");
            dgvADActualDefecsD10.Columns.Add("ESEP_FIRST_NAMED10", "Name");
            dgvADActualDefecsD10.Columns.Add("ESEP_MOBI_NOD10", "Mobile Num");
            dgvADActualDefecsD10.Columns.Add("STH_CURR_STUSD10", "Is Active");
            dgvADActualDefecsD10.Columns.Add("STH_STUSD10", "Status");
            dgvADActualDefecsD10.Columns["STH_EMP_CDD10"].DataPropertyName = "STH_EMP_CD";
            dgvADActualDefecsD10.Columns["ESEP_FIRST_NAMED10"].DataPropertyName = "ESEP_FIRST_NAME";
            dgvADActualDefecsD10.Columns["ESEP_MOBI_NOD10"].DataPropertyName = "ESEP_MOBI_NO";
            dgvADActualDefecsD10.Columns["STH_CURR_STUSD10"].DataPropertyName = "STH_CURR_STUS";
            dgvADActualDefecsD10.Columns["STH_STUSD10"].DataPropertyName = "STH_STUS";

            dgvADActualDefecsD10.DataSource = new List<Service_Enquiry_TechAllo_Hdr>();

            if (oJobAllocations_DH != null && oJobAllocations_DH.Count > 0)
            {
                dgvADActualDefecsD10.DataSource = oJobAllocations_DH;
            }

            dgvADActualDefecsD10.Columns["ESEP_FIRST_NAMED10"].Width = 150;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isAnySelected())
            {
                MessageBox.Show("Please select item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string jobNumber = string.Empty;

            for (int i = 0; i < dgvSerialDetailsD9.Rows.Count; i++)
            {
                if (dgvSerialDetailsD9.Rows[i].Cells["dataGridViewCheckBoxColumn1"].Value != null && Convert.ToBoolean(dgvSerialDetailsD9.Rows[i].Cells["dataGridViewCheckBoxColumn1"].Value) == true)
                {
                    jobNumber = dgvSerialDetailsD9.Rows[i].Cells["Jbd_jobnoD9"].Value.ToString();
                    break;
                }
            }

            DataTable dtTemmp = _basePage.CHNLSVC.CustService.GET_CON_HDRS_JOB_COM(BaseCls.GlbUserComCode, jobNumber);
            dgvADActualDefecsD10.Columns.Clear();
            dgvADActualDefecsD10.AutoGenerateColumns = false;
            dgvADActualDefecsD10.Columns.Add("CONFIRMATIONNO", "CONFIRMATION NO");
            dgvADActualDefecsD10.Columns.Add("REMARK", "REMARK");
            dgvADActualDefecsD10.Columns["CONFIRMATIONNO"].DataPropertyName = "CONFIRMATION NO";
            dgvADActualDefecsD10.Columns["REMARK"].DataPropertyName = "REMARK";
            dgvADActualDefecsD10.DataSource = dtTemmp;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isAnySelected())
            {
                MessageBox.Show("Please select item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvADActualDefecsD10.Columns.Clear();
            dgvADActualDefecsD10.Columns.Add("stc_descD10", "Defect");
            dgvADActualDefecsD10.Columns.Add("jtc_cmt_rmkD10", "Remark");

            dgvADActualDefecsD10.Columns["stc_descD10"].DataPropertyName = "stc_desc";
            dgvADActualDefecsD10.Columns["jtc_cmt_rmkD10"].DataPropertyName = "jtc_cmt_rmk";

            if (oTechComments_DH != null && oTechComments_DH.Count > 0)
            {
                dgvADActualDefecsD10.DataSource = oTechComments_DH;
            }

            dgvADActualDefecsD10.Columns["stc_descD10"].Width = 150;
            dgvADActualDefecsD10.Columns["jtc_cmt_rmkD10"].Width = 350;
        }

        public void loadData()
        {
            dgvADActualDefecsD10.AutoGenerateColumns = false;
            dgvSerialDetailsD9.AutoGenerateColumns = false;

            dgvSerialDetailsD9.RowTemplate.Height = 18;
            dgvADActualDefecsD10.RowTemplate.Height = 18;

            getSerialDetails(_Serial, _Item);
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvSerialDetailsD9.Rows.Count > 0)
            {
                for (int i = 0; i < dgvSerialDetailsD9.Rows.Count; i++)
                {
                    if (dgvSerialDetailsD9.Rows[i].Cells["dataGridViewCheckBoxColumn1"].Value != null && Convert.ToBoolean(dgvSerialDetailsD9.Rows[i].Cells["dataGridViewCheckBoxColumn1"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        private void dgvSerialDetailsD9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSerialDetailsD9_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSerialDetailsD9.IsCurrentCellDirty)
            {
                dgvSerialDetailsD9.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

    }
}
