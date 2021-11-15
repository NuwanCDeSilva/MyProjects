using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Drawing;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceJobDetailsViwer : Base
    {
        private string JobNumber = string.Empty;
        private Int32 JobLine;
        private string serial1 = string.Empty;
        private string serial2 = string.Empty;
        private string RegistrionNo = string.Empty;

        public ServiceJobDetailsViwer(string jobNumber, Int32 jobLinePARA)
        {
            InitializeComponent();
            //dgvItemDetails.DataSource = new List<Service_job_Det>();
            JobNumber = jobNumber;
            JobLine = jobLinePARA;

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                serial1 = _Parameters.SP_DB_SERIAL;
                serial2 = _Parameters.SP_DB_CHASSIS;
                RegistrionNo = _Parameters.SP_DB_VEHI_REG;

                dgvItemDetails.Columns[5].HeaderText = serial1;
                dgvItemDetails.Columns[6].HeaderText = serial2;
                dgvItemDetails.Columns[8].HeaderText = RegistrionNo;
            }

            foreach (Control oControl in groupBox1.Controls)
            {
                if (oControl is Label)
                {
                    if (oControl.Name.Length > 3 && oControl.Name.Substring(0, 3).ToUpper() == "LBL")
                    {
                        oControl.DoubleClick += copyText;
                    }
                }

            }
        }

        private void copyText(object sender, EventArgs e)
        {
            try
            {
                Label lbl = (Label)sender;
                Clipboard.SetText(lbl.Text.ToString());
                MessageBox.Show(lbl.Text, "Copy to Clipboard");
            }
            catch (Exception ex)
            {

            }
        }

        private void ServiceJobDetailsViwer_Load(object sender, EventArgs e)
        {
            dgvItemDetails.AutoGenerateColumns = false;
            dgvEmpDetails.AutoGenerateColumns = false;
            dgvItemDefects.AutoGenerateColumns = false;
            getServiceHeader();
            GetServiceItemDetails(JobLine);

            GetItemDefects(JobNumber, Convert.ToInt32(dgvItemDetails.Rows[0].Cells["LineNum"].Value.ToString()));
            GetJobEMPS(JobNumber, Convert.ToInt32(dgvItemDetails.Rows[0].Cells["LineNum"].Value.ToString()));
        }

        public void getServiceHeader()
        {
            Service_JOB_HDR oHDR = CHNLSVC.CustService.GetServiceJobHeader(JobNumber, BaseCls.GlbUserComCode);
            lblJobno.Text = oHDR.SJB_JOBNO;
            lblOtherDocNo.Text = oHDR.SJB_OTHERREF;
            lblJobDate.Text = oHDR.SJB_DT.ToString("dd/MMM/yyyy");
            lblJobStatus.Text = oHDR.SJB_STUS;
            lblCusName.Text = oHDR.SJB_B_CUST_TIT + " " + oHDR.SJB_B_CUST_NAME;
            lblAddress.Text = oHDR.SJB_B_ADD1 + " " + oHDR.SJB_B_ADD2 + " " + oHDR.SJB_B_ADD3;
            lblTelephone.Text = oHDR.SJB_B_MOBINO;
            lblCategori.Text = oHDR.SJB_JOBCAT;
            lblFromDate.Text = oHDR.SJB_ST_DT.ToString("dd/MMM/yyyy");
            lblTodate.Text = oHDR.SJB_ED_DT.ToString("dd/MMM/yyyy");
            lblCusExpeDate.Text = oHDR.SJB_CUSTEXPTDT.ToString("dd/MMM/yyyy");
            lblCreateDate.Text = oHDR.SJB_CRE_DT.ToString("dd/MMM/yyyy");
            lblTown.Text = oHDR.SJB_TOWN;
            lblPriority.Text = oHDR.SJB_PRORITY;
            lblJobStage.Text = "";

            MasterBusinessEntity Customer = CHNLSVC.Sales.GetCustomerProfile(oHDR.SJB_CUST_CD, null, null, null, null);
            if (Customer != null && Customer.Mbe_cd != null)
            {
                lblVatNum.Text = Customer.Mbe_tax_no.ToString();
                lblSvatnum.Text = Customer.Mbe_svat_no.ToString();
            }
            else
            {
                lblVatNum.Text = "";
                lblSvatnum.Text = "";
            }

            Service_Category oItem = CHNLSVC.CustService.GET_SCV_CATE_BY_JOB(JobNumber, BaseCls.GlbUserComCode);
            if (oItem != null && oItem.Sc_direct != null)
            {
                lblJobType.Text = (oItem.Sc_direct == "W") ? "Workshop" : "Field";
            }
        }

        public void GetServiceItemDetails(int lineNum)
        {
            List<Service_job_Det> oItems = CHNLSVC.CustService.GetJobDetails(JobNumber, lineNum, BaseCls.GlbUserComCode);
            dgvItemDetails.DataSource = oItems;
            modifyJobDetailGrid();

            if (oItems.Count == 1)
            {
                dgvItemDetails_CellClick(dgvItemDetails, new DataGridViewCellEventArgs(0, 0));
            }
        }

        public void GetItemDefects(string jobNo, Int32 lineNo)
        {
            DataTable Dt = CHNLSVC.CustService.getServiceJobDefects(jobNo, lineNo);
            dgvItemDefects.DataSource = Dt;
        }

        public void GetJobEMPS(string jobNo, Int32 lineNo)
        {
            DataTable Dt = CHNLSVC.CustService.getServiceJobEmployees(jobNo, lineNo);
            dgvEmpDetails.DataSource = Dt;
        }

        private void dgvItemDetails_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                GetItemDefects(JobNumber, Convert.ToInt32(dgvItemDetails.Rows[e.RowIndex].Cells["LineNum"].Value.ToString()));
                GetJobEMPS(JobNumber, Convert.ToInt32(dgvItemDetails.Rows[e.RowIndex].Cells["LineNum"].Value.ToString()));

                List<Service_job_Det> oitems = CHNLSVC.CustService.GetJobDetails(JobNumber, Convert.ToInt32(dgvItemDetails.Rows[e.RowIndex].Cells["LineNum"].Value.ToString()), BaseCls.GlbUserComCode);
                if (oitems != null && oitems.Count > 0)
                {
                    lblJobStage.Text = oitems[0].StageText;

                    List<Service_job_Det> _preSerJob = new List<Service_job_Det>();
                    _preSerJob = CHNLSVC.CustService.getPrejobDetails(BaseCls.GlbUserComCode, oitems[0].Jbd_ser1, oitems[0].Jbd_itm_cd);
                    if (_preSerJob != null && _preSerJob.Count > 0)
                    {
                        lblAttems.Text = (_preSerJob.Count - 1).ToString();
                    }
                    else
                    {
                        lblAttems.Text = "0";
                    }
                }
            }
        }

        private void dgvItemDetails_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void dgvItemDetails_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Down)
                {
                    GetItemDefects(JobNumber, Convert.ToInt32(dgvItemDetails.SelectedRows[0].Cells["LineNum"].Value.ToString()));
                    GetJobEMPS(JobNumber, Convert.ToInt32(dgvItemDetails.SelectedRows[0].Cells["LineNum"].Value.ToString()));
                }
                else if (e.KeyCode == System.Windows.Forms.Keys.Up)
                {
                    GetItemDefects(JobNumber, Convert.ToInt32(dgvItemDetails.SelectedRows[0].Cells["LineNum"].Value.ToString()));
                    GetJobEMPS(JobNumber, Convert.ToInt32(dgvItemDetails.SelectedRows[0].Cells["LineNum"].Value.ToString()));
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void lblJobno_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblJobno, lblJobno.Text);
        }

        private void lblCusName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblCusName, lblCusName.Text);
        }

        private void lblAddress_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblAddress, lblAddress.Text);
        }

        private void modifyJobDetailGrid()
        {
            if (dgvItemDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItemDetails.Rows.Count; i++)
                {
                    if (dgvItemDetails.Rows[i].Cells["jbd_act"].Value.ToString() == "0")
                    {
                        dgvItemDetails.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                }
            }
        }

        private void ServiceJobDetailsViwer_DoubleClick(object sender, EventArgs e)
        {
            foreach (Control _ctrl in this.Controls)
            {
                if (_ctrl is Label)
                {
                    if (sender == _ctrl)
                    {
                        MessageBox.Show(_ctrl.Text);
                    }
                }
            }
        }

        private void dgvItemDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvItemDetails.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = dgvItemDetails.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
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



    }
}