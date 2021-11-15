using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Services;
using System.Drawing;

namespace FF.WindowsERPClient.Enquiries.Service
{
    public partial class ServiceJobEnquiry : Base
    {
        private Int32 selectedTxt = 0;
        private String SearchText = string.Empty;

        private Service_job_Det oJobDetaill_DH;
        private List<Service_Job_Defects> oJobDefects_DH;
        private List<Service_Enquiry_TechAllo_Hdr> oJobAllocations_DH;
        private List<Service_Enquiry_Tech_Cmnt> oTechmoments_DH;
        private List<Service_Enquiry_StandByItems> oStandByItems_DH;
        private string ConfrimationRemark_DH;

        private Service_Enquiry_Job_Hdr oheader;

        private Int32 UserSeqNo = 0;
        private string OutwardType = string.Empty;
        private string _supplier = string.Empty;
        private string _subdoc = string.Empty;
        private string OutwardNo = string.Empty;
        private DateTime? hdnOutwarddate = null;
        private string _dono = string.Empty;
        private List<ReptPickSerials> PickSerialsList = null;
        private string hdnAllowBin = "0";

        private String Serial1 = string.Empty;
        private String SerialOth1 = string.Empty;
        private String SerialOth2 = string.Empty;
        private Service_JOB_HDR oMainJobHeader = null;

        private List<Service_Enquiry_Invoice_Items> oItems_Invo;
        private List<Service_Enquiry_Invoice_Header> oHeaders_Invo;

        private string _warrSearchtp = string.Empty;
        private Service_Chanal_parameter _Parameters = null;
        private string _warrSearchorder = string.Empty;

        private bool IsFromSerial = false;
        //private bool IsgetJobDetails = false;

        public ServiceJobEnquiry()
        {
            InitializeComponent();

            dgvJobItemsD1.AutoGenerateColumns = false;
            dgvOpenDefetsD2.AutoGenerateColumns = false;
            dgvActualDefectsD3.AutoGenerateColumns = false;
            dgvStandByItemsD5.AutoGenerateColumns = false;
            dgvTechAllocationD6.AutoGenerateColumns = false;
            dgvTechnicianFinalCommentsD7.AutoGenerateColumns = false;
            dgvJobsD8.AutoGenerateColumns = false;
            dgvMRNDetailsD11.AutoGenerateColumns = false;
            gvPendingD12.AutoGenerateColumns = false;
            dgvItemsD13.AutoGenerateColumns = false;
            dgvItemsD14.AutoGenerateColumns = false;
            dgvTempitemsD15.AutoGenerateColumns = false;
            dgvRequestedItemsD16.AutoGenerateColumns = false;
            dgvEstimateHeaderD17.AutoGenerateColumns = false;
            dgvEstimateItemsD18.AutoGenerateColumns = false;
            dgvEstimateTAXDetailsD19.AutoGenerateColumns = false;
            dgvInvoiceHeaderD20.AutoGenerateColumns = false;
            dgvInvoiceItemsD21.AutoGenerateColumns = false;
            dgvInvoiceTAXD22.AutoGenerateColumns = false;
            dgvPartTransferdD23.AutoGenerateColumns = false;
            dgvCostDetailsD24.AutoGenerateColumns = false;
            dgvWrrtReplemntD26.AutoGenerateColumns = false;
            dgvCustCollecDataD27.AutoGenerateColumns = false;
            dgvStageLogD28.AutoGenerateColumns = false;
            dgvIssuaraceDetailsD29.AutoGenerateColumns = false;

            grvAddiItems.AutoGenerateColumns = false;

            gvSerial.AutoGenerateColumns = false;
            gvItem.AutoGenerateColumns = false;

            dgvActualDefectsD3.RowTemplate.Height = 18;
            dgvJobItemsD1.RowTemplate.Height = 18;
            dgvJobsD8.RowTemplate.Height = 18;
            dgvOpenDefetsD2.RowTemplate.Height = 18;
            dgvStandByItemsD5.RowTemplate.Height = 18;
            dgvTechAllocationD6.RowTemplate.Height = 18;
            dgvTechnicianFinalCommentsD7.RowTemplate.Height = 18;
            dgvMRNDetailsD11.RowTemplate.Height = 18;
            dgvItemsD13.RowTemplate.Height = 18;
            dgvItemsD14.RowTemplate.Height = 18;
            dgvTempitemsD15.RowTemplate.Height = 18;
            dgvRequestedItemsD16.RowTemplate.Height = 18;
            dgvEstimateHeaderD17.RowTemplate.Height = 18;
            dgvEstimateItemsD18.RowTemplate.Height = 18;
            dgvEstimateTAXDetailsD19.RowTemplate.Height = 18;
            gvPendingD12.RowTemplate.Height = 18;
            dgvInvoiceHeaderD20.RowTemplate.Height = 18;
            dgvInvoiceItemsD21.RowTemplate.Height = 18;
            dgvInvoiceTAXD22.RowTemplate.Height = 18;
            dgvPartTransferdD23.RowTemplate.Height = 18;
            dgvCostDetailsD24.RowTemplate.Height = 18;
            dgvWrrtReplemntD26.RowTemplate.Height = 18;
            dgvCustCollecDataD27.RowTemplate.Height = 18;
            dgvStageLogD28.RowTemplate.Height = 18;
            dgvIssuaraceDetailsD29.RowTemplate.Height = 18;

            gvSerial.RowTemplate.Height = 18;
            gvItem.RowTemplate.Height = 18;

            _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                if (_Parameters.SP_ISNEEDWIP != 1)
                {
                    MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Enabled = false;
                }

                lblSerialNumer.Text = _Parameters.SP_DB_SERIAL;
                lblOtherSerial2.Text = _Parameters.SP_DB_CHASSIS;
                lblOtherSerial1.Text = _Parameters.SP_DB_VEHI_REG;
                if (string.IsNullOrEmpty(lblOtherSerial1.Text))
                {
                    txtOtherSearal1.Visible = false;
                    btnOtherSearial1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
            }

            foreach (Control oControl in groupBox2.Controls)
            {
                if (oControl is Label)
                {
                    if (oControl.Name.Length > 3 && oControl.Name.Substring(0, 3).ToUpper() == "LBL")
                    {
                        oControl.DoubleClick += copyText;
                    }
                }
            }

            foreach (Control oControl in this.Controls)
            {
                if (oControl is Panel)
                {
                    foreach (Control oSubControls in oControl.Controls)
                    {
                        if (oSubControls is DataGridView)
                        {
                            DataGridView dgv = (DataGridView)oSubControls;
                            dgv.CellDoubleClick += copyGridCell;
                        }
                    }
                }

            }

            pnlSerialDetails.Size = new Size(379, 237);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchF3:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
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

                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + string.Empty + seperator + string.Empty + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJObSerarhEnquiry:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials:
                    {
                        if (selectedTxt == 0)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtJobNumber.Text + seperator + "SERIAL1" + seperator + SearchText + seperator + dtpDate.Value.ToString("dd-MM-yyyy") + seperator + dtpTo.Value.ToString("dd-MM-yyyy") + seperator);
                            break;
                        }
                        else if (selectedTxt == 1)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtJobNumber.Text + seperator + "SERIAL2" + seperator + SearchText + seperator + dtpDate.Value.ToString("dd-MM-yyyy") + seperator + dtpTo.Value.ToString("dd-MM-yyyy") + seperator);
                            break;
                        }
                        else if (selectedTxt == 2)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtJobNumber.Text + seperator + "REGISTRATION" + seperator + SearchText + seperator + dtpDate.Value.ToString("dd-MM-yyyy") + seperator + dtpTo.Value.ToString("dd-MM-yyyy") + seperator);
                            break;
                        }

                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        paramsText.Append(_warrSearchtp + seperator + _warrSearchorder + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SubJob:
                    {
                        paramsText.Append(string.Empty + seperator + string.Empty + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ServiceJobEnquiry_Load(object sender, EventArgs e)
        {
            _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                if (_Parameters.SP_ISNEEDWIP != 1)
                {
                    MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Enabled = false;
                }

                Serial1 = _Parameters.SP_DB_SERIAL;
                SerialOth1 = _Parameters.SP_DB_CHASSIS;
                SerialOth2 = _Parameters.SP_DB_VEHI_REG;
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
            }

            clearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
            }
        }

        private void txtLoc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoc;
                _CommonSearch.txtSearchbyword.Text = txtLoc.Text;
                _CommonSearch.ShowDialog();
                CHNLSVC.CloseAllChannels();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLoc_Leave(object sender, EventArgs e)
        {
            List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(BaseCls.GlbUserComCode, txtLoc.Text.ToUpper());
            if (loc_list == null || loc_list.Count == 0)
            {
                MessageBox.Show("Please enter correct location code.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoc.Clear();
                txtLoc.Focus();
                return;
            }
        }

        private void txtLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtLoc_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtJobNumber.Focus();
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            txtLoc_DoubleClick(null, null);
        }

        private void txtJobNumber_DoubleClick(object sender, EventArgs e)
        {
            IsFromSerial = false;
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJObSerarhEnquiry);
            //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsEnqeuiy(_CommonSearch.SearchParams, null, null, DateTime.Now.AddMonths(-1), DateTime.Now);
            _CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNumber;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtJobNumber_Leave(null, null);
        }

        private void txtJobNumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNumber.Text))
            {
                Service_JOB_HDR OJobheader = CHNLSVC.CustService.GET_SCV_JOB_HDR(txtJobNumber.Text, BaseCls.GlbUserComCode);

                if (OJobheader == null || OJobheader.SJB_JOBNO == null)
                {
                    MessageBox.Show("Please enter correct job number", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNumber.Clear();
                    txtJobNumber.Focus();
                    return;
                }
                else
                {
                    txtSearial.Clear();
                    txtOtherSeari2.Clear();
                    oMainJobHeader = OJobheader;
                    //SystemUser oSecUser = CHNLSVC.Security.GetUserByUserID(OJobheader.SJB_CRE_BY);
                    //string name = (oSecUser != null) ? oSecUser.Se_usr_desc : string.Empty;
                    //lblCreateBy.Text = OJobheader.SJB_CRE_BY + " - " + name;
                    Service_Category oItem = CHNLSVC.CustService.GET_SCV_CATE_BY_JOB(txtJobNumber.Text, BaseCls.GlbUserComCode);
                    if (oItem != null && oItem.Sc_direct != null)
                    {
                        lblContactPerson2.Text = (oItem.Sc_direct == "W") ? "Workshop" : "Field";
                    }

                    if (sender == null || sender.ToString() != "test")
                    {
                        btnSearch_Click(null, null);
                    }

                    // Modified by Nadeeka (Ovecome to slowness, twice call to job header)
                    SystemUser oSecUser = CHNLSVC.Security.GetUserByUserID(OJobheader.SJB_CRE_BY);
                    string name = (oSecUser != null) ? oSecUser.Se_usr_desc : string.Empty;
                    lblCreateBy.Text = OJobheader.SJB_CRE_BY + " - " + name;
                }
            }
        }

        private void txtJobNumber_KeyDown(object sender, KeyEventArgs e)
        {
            IsFromSerial = false;
            if (e.KeyCode == Keys.F2)
            {
                txtJobNumber_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtJobNumber.Text))
                {
                    // IsgetJobDetails = false;
                    txtJobNumber_Leave("test", null);
                    btnSearch_Click(null, null);
                }
                //txtSearial.Focus();
            }
            else if (e.KeyCode == Keys.F3)  //kapila 7/4/2016
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
                _CommonSearch.obj_TragetTextBox = txtJobNumber;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtJobNumber.Focus();

                _CommonSearch.ShowDialog();

                _CommonSearch.dvResult.Refresh();

                txtJobNumber_Leave(null, null);
            }
        }

        private void btnjob_Click(object sender, EventArgs e)
        {
            txtJobNumber_DoubleClick(null, null);
        }

        private void txtSearial_DoubleClick(object sender, EventArgs e)
        {
            selectedTxt = 0;
            SearchText = lblSerialNumer.Text.Trim();
            CommonSearch.CommonSearch _commonSearch = null;
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceDetailSearials(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtSearial;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtSearial.Select();
            }
            catch (Exception ex)
            { txtSearial.Clear(); this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtSearial_Leave(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtSearial.Text))
            //{
            //    List<Service_job_Det> oItem = CHNLSVC.CustService.SCV_JOB_GET_SER_OSR_REG(BaseCls.GlbUserComCode, txtLoc.Text, txtSearial.Text, "", "");
            //    if (oItem == null || oItem.Count == 0)
            //    {
            //        MessageBox.Show("Please enter correct " + Serial1, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtSearial.Clear();
            //        txtSearial.Focus();
            //        return;
            //    }
            //}
        }

        private void txtSearial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSearial_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearial.Text))
                {
                    btnSearch_Click(null, null);

                }
                else
                {
                    txtOtherSeari2.Focus();
                }
            }
        }

        private void btnSerial_Click(object sender, EventArgs e)
        {
            txtSearial_DoubleClick(null, null);
        }

        private void btnOtherSearial2_Click(object sender, EventArgs e)
        {
            txtOtherSeari2_DoubleClick(null, null);
        }

        private void txtOtherSeari2_DoubleClick(object sender, EventArgs e)
        {
            selectedTxt = 1;
            SearchText = lblOtherSerial2.Text.Trim();
            CommonSearch.CommonSearch _commonSearch = null;
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceDetailSearials(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtOtherSeari2;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtOtherSeari2.Select();
            }
            catch (Exception ex)
            {
                txtOtherSeari2.Clear();
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnOtherSearial1_Click(object sender, EventArgs e)
        {
            txtOtherSearal1_DoubleClick(null, null);
        }

        private void txtOtherSearal1_DoubleClick(object sender, EventArgs e)
        {
            selectedTxt = 2;
            SearchText = lblOtherSerial1.Text.Trim();
            CommonSearch.CommonSearch _commonSearch = null;
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceDetailSearials(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtOtherSearal1;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtOtherSearal1.Select();
            }
            catch (Exception ex)
            {
                txtOtherSearal1.Clear();
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtOtherSearal1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOtherSearal1.Text))
            {
                List<Service_job_Det> oItem = CHNLSVC.CustService.SCV_JOB_GET_SER_OSR_REG(BaseCls.GlbUserComCode, txtLoc.Text, "", "", txtOtherSearal1.Text);
                if (oItem == null || oItem.Count == 0)
                {
                    MessageBox.Show("Please enter correct " + SerialOth2, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOtherSearal1.Clear();
                    txtOtherSearal1.Focus();
                    return;
                }
            }
        }

        private void txtOtherSearal1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtOtherSearal1_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // IsgetJobDetails = false;
            if (IsFromSerial == false)
            {
                dgvJobsD8.DataSource = new List<Service_Enquiry_Job_Det>();
            }
            if (string.IsNullOrEmpty(txtJobNumber.Text))
            {
                if (string.IsNullOrEmpty(txtJobNumber.Text) && txtSearial.Text.Length > 4)
                {
                    GetRelaventJobs();
                }
                
            }
            else
            {
                GetJob();
                txtWorkInstrucktions.Text = oheader.SJB_TECH_RMK;
                txtInicialRemarks.Text = oheader.SJB_JOB_RMK;
            }
        }

        private void dgvJobsD8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IsFromSerial = true;

                dgvOpenDefetsD2.DataSource = new List<Service_Job_Defects>();
                dgvActualDefectsD3.DataSource = new List<Service_Job_Defects>();
                dgvTechAllocationD6.DataSource = new List<Service_Enquiry_TechAllo_Hdr>();

                lblStatus.Text = "";
                lblStatus.Text = "";
                lblStarton.Text = "";
                lblEndon.Text = "";
                btnDefectHistory.Enabled = false;

                txtJobNumber.Text = dgvJobsD8.Rows[e.RowIndex].Cells["JBD_JOBNOD8"].Value.ToString();
                txtJobNumber_Leave(null, null);
                GetJob();

                txtWorkInstrucktions.Text = oheader.SJB_TECH_RMK;
                txtInicialRemarks.Text = oheader.SJB_JOB_RMK;
            }
        }

        private void dgvJobItemsD1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgvOpenDefetsD2.DataSource = new List<Service_Job_Defects>();
                dgvActualDefectsD3.DataSource = new List<Service_Job_Defects>();
                dgvTechAllocationD6.DataSource = new List<Service_Enquiry_TechAllo_Hdr>();
                dgvTechnicianFinalCommentsD7.DataSource = new List<Service_Enquiry_Tech_Cmnt>();

                for (int i = 0; i < dgvJobItemsD1.Rows.Count; i++)
                {
                    dgvJobItemsD1.Rows[i].Cells[0].Value = false;
                }
                dgvJobItemsD1.Rows[e.RowIndex].Cells[0].Value = true;
                Int32 JObLine = Convert.ToInt32(dgvJobItemsD1.Rows[e.RowIndex].Cells["Jbd_joblineD1"].Value.ToString());
                getJobDetails(JObLine);

                if (dgvJobItemsD1.Rows[e.RowIndex].Cells["Jbd_ser1D1"].Value.ToString() != "N/A")
                {
                    btnDefectHistory.Enabled = true;
                }

                foreach (Control item in groupBox4.Controls)
                {
                    if (item is Button)
                    {
                        item.Enabled = true;
                    }
                }

                List<Service_Enquiry_Inssuarance> oInssuaranceD = CHNLSVC.CustService.GET_INSSURANCE_ENQRY(BaseCls.GlbUserComCode, dgvJobItemsD1.Rows[e.RowIndex].Cells["Jbd_ser1D1"].Value.ToString(), dgvJobItemsD1.Rows[e.RowIndex].Cells["JBD_ITM_CDD1"].Value.ToString(), dgvJobItemsD1.Rows[e.RowIndex].Cells["JBD_INVC_NOD1"].Value.ToString());
                dgvIssuaraceDetailsD29.DataSource = new List<Service_Enquiry_Inssuarance>();
                dgvIssuaraceDetailsD29.DataSource = oInssuaranceD;
            }
        }

        private void btnShowDelCustomer_Click(object sender, EventArgs e)
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (btnShowDelCustomer.Text == "Show Billing Customer")
            {
                btnShowDelCustomer.Text = "Show Delivery Customer";
                lblCustomer.Text = oheader.SJB_B_CUST_NAME;
                lblAddress.Text = oheader.SJB_B_ADD1;
                lblAddress2.Text = oheader.SJB_B_ADD2;
                lblContactPerson.Text = oheader.SJB_CNT_PERSON;
                lblTele.Text = oheader.SJB_B_MOBINO;
                lblContactNum.Text = oheader.SJB_CNT_PHNO;
                lblCustomerCode.Text = oheader.SJB_B_CUST_CD;

                //kapila 12/8/2015
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(oheader.SJB_B_CUST_CD, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                    lblPhone.Text = oheader.SJB_B_PHNO + "/" + _masterBusinessCompany.Mbe_email;
                else
                    lblPhone.Text = oheader.SJB_B_PHNO;

                lblCustomerType.Text = "Billing Customer";
            }
            else
            {
                btnShowDelCustomer.Text = "Show Billing Customer";
                lblCustomer.Text = oheader.SJB_CUST_NAME;
                lblAddress.Text = oheader.SJB_ADD1;
                lblAddress2.Text = oheader.SJB_ADD2;
                lblContactPerson.Text = oheader.SJB_CNT_PERSON;
                lblTele.Text = oheader.SJB_MOBINO;
                lblContactNum.Text = oheader.SJB_CNT_PHNO;
                lblCustomerCode.Text = oheader.SJB_CUST_CD;

                //kapila 12/8/2015
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(oheader.SJB_B_CUST_CD, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                    lblPhone.Text = oheader.SJB_PHNO + "/" + _masterBusinessCompany.Mbe_email;
                else
                    lblPhone.Text = oheader.SJB_PHNO;

                lblCustomerType.Text = "Delivery Customer";

            }
        }

        private void dgvStandByItemsD5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnDefectHistory_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlDefectHistory.Show();
                string selectedSerial = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_ser1D1"].Value.ToString();
                String Item = dgvJobItemsD1.SelectedRows[0].Cells["JBD_ITM_CDD1"].Value.ToString();
                lblDefectHistyHeader.Text = "Defect History - [ Item :- " + Item + "   Serial :-" + selectedSerial + " ]";
                getSerialDetails(selectedSerial, Item);
            }
        }

        private void btnMRNDetails_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlMRNDetails.Show();

                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                getSavedItems(Jobnumber, jobLineNum);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlNewPartstoBeConfirmed.Visible = false;
        }

        private void btnNewPartsToBeConfirmed_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlNewPartstoBeConfirmed.Show();
                BindOutwardListGridData();
            }
        }

        private void gvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvPendingD12.RowCount > 0)
                {
                    int _rowIndex = e.RowIndex;
                    if (_rowIndex != -1)
                        BindSelectedOutwardNo(_rowIndex);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void gvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnNewPartsStockInHand_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlNewPartsInHand.Show();

                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                getSavedItems(Jobnumber, jobLineNum);

                List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
                stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, Jobnumber, jobLineNum, "", BaseCls.GlbUserDefProf);

                if (stockReturnItems != null && stockReturnItems.Count > 0)
                {
                    dgvItemsD13.DataSource = stockReturnItems;
                    modifyGrid();
                }
                else
                {
                    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlMain.Enabled = true;
                    pnlNewPartsInHand.Visible = false;
                    return;
                }
            }
        }

        private void btnOldpartDetails_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlOldPartDetails.Show();
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                List<Service_OldPartRemove> MailList = new List<Service_OldPartRemove>();
                MailList.AddRange(CHNLSVC.CustService.Get_SCV_Oldparts(Jobnumber, jobLineNum, "", ""));

                if (MailList.Count > 0)
                {
                    foreach (Service_OldPartRemove item in MailList)
                    {
                        item.dgvLine = MailList.Max(x => x.dgvLine) + 1;
                    }
                }
                dgvItemsD14.DataSource = new List<Service_OldPartRemove>();
                if (MailList.Count > 0)
                {
                    dgvItemsD14.DataSource = MailList;
                    for (int i = 0; i < dgvItemsD14.Rows.Count; i++)
                    {
                        if (dgvItemsD14.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
                        {
                            dgvItemsD14.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlMain.Enabled = true;
                    pnlOldPartDetails.Visible = false;
                    return;
                }
            }
        }

        private void btnTemporaryIssueDetails_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlTemporyIssueDetails.Show();
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                List<Service_TempIssue> stockReturnItems = new List<Service_TempIssue>();
                stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, Jobnumber, jobLineNum, "", BaseCls.GlbUserDefProf, "TMPI");

                if (stockReturnItems != null && stockReturnItems.Count > 0)
                {
                    dgvTempitemsD15.DataSource = stockReturnItems;
                }
                else
                {
                    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlMain.Enabled = true;
                    pnlTemporyIssueDetails.Visible = false;
                    return;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlSupplieWarrantyClaim.Visible = false;
        }

        private void btnSupplierWarrantyClaims_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                //  clerSWCLables();
                pnlMain.Enabled = false;
                pnlSupplieWarrantyClaim.Show();
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                // dgvWarrantyDetailsD23.DataSource = new List<Service_Enquiry_SupplierWrntyDetails>();
                // dgvReceivedDetails.DataSource = new List<Service_Enquiry_SupplierWrntyDetails>();
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                pnlSupplieWarrantyClaim.Width = 839;
                pnlSupplieWarrantyClaim.Height = 550;

                ucSupplierWarranty1.Visible = true;
                ucSupplierWarranty1.clerSWCLables();
                ucSupplierWarranty1.GblJobNumber = Jobnumber;
                ucSupplierWarranty1.GblJobLine = jobLineNum;
                ucSupplierWarranty1.LoadData();


                //DataTable DtTemp = CHNLSVC.CustService.GetSupplierWarrantyClaimRequestedItems(BaseCls.GlbUserComCode, Jobnumber, jobLineNum);
                //if (DtTemp != null && DtTemp.Rows.Count > 0)
                //{
                //    dgvRequestedItemsD16.DataSource = DtTemp;

                //    if (DtTemp.Rows.Count == 1)
                //    {
                //        dgvRequestedItemsD16_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    pnlMain.Enabled = true;
                //    pnlSupplieWarrantyClaim.Visible = false;
                //    return;
                //}
            }
        }

        private void btnJobEstimate_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlEstimate.Show();
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                List<Service_Enquiry_Estimate_Hdr> oDisplyItems = new List<Service_Enquiry_Estimate_Hdr>();
                dgvEstimateHeaderD17.DataSource = new List<Service_Enquiry_Estimate_Hdr>();
                oDisplyItems = CHNLSVC.CustService.GetEstimateHeaderEnquiry(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, Jobnumber);

                if (oDisplyItems != null && oDisplyItems.Count > 0)
                {
                    dgvEstimateHeaderD17.DataSource = oDisplyItems;
                }
                else
                {
                    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlMain.Enabled = true;
                    pnlEstimate.Visible = false;
                    return;
                }
            }
        }

        private void dgvEstimateHeaderD17_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Int32 SeqNum = Convert.ToInt32(dgvEstimateHeaderD17.Rows[e.RowIndex].Cells["ESH_SEQ_NOD17"].Value.ToString());
                List<Service_Enquiry_Estimate_Items> oItems = new List<Service_Enquiry_Estimate_Items>();
                int result = CHNLSVC.CustService.GetEstimateDetailsEnquiry(SeqNum, out oItems);
                dgvEstimateItemsD18.DataSource = new List<Service_Enquiry_Estimate_Items>();
                dgvEstimateItemsD18.DataSource = oItems;
            }
        }

        private void dgvEstimateItemsD18_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Int32 SeqNum = Convert.ToInt32(dgvEstimateItemsD18.Rows[e.RowIndex].Cells["ESI_SEQ_NOD18"].Value.ToString());
                Int32 JobLine = Convert.ToInt32(dgvEstimateItemsD18.Rows[e.RowIndex].Cells["ESI_LINED18"].Value.ToString());

                List<Service_Enquiry_Estimate_TAX> oTAXItems = CHNLSVC.CustService.GET_SCV_EST_TAX_ENQRY(SeqNum, JobLine);
                dgvEstimateTAXDetailsD19.DataSource = new List<Service_Enquiry_Estimate_TAX>();
                dgvEstimateTAXDetailsD19.DataSource = oTAXItems;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            PnlInvoiceDetails.Visible = false;
        }

        private void btnInvoiceDetails_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                PnlInvoiceDetails.Show();
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                oItems_Invo = new List<Service_Enquiry_Invoice_Items>();
                oHeaders_Invo = new List<Service_Enquiry_Invoice_Header>();

                int result = CHNLSVC.CustService.GetInvoiceDetailsEnquiry(Jobnumber, BaseCls.GlbUserComCode, out oItems_Invo, out oHeaders_Invo);

                dgvInvoiceHeaderD20.DataSource = new List<Service_Enquiry_Invoice_Header>();
                if (oHeaders_Invo != null && oHeaders_Invo.Count > 0)
                {
                    dgvInvoiceHeaderD20.DataSource = oHeaders_Invo;
                }
                else
                {
                    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlMain.Enabled = true;
                    PnlInvoiceDetails.Visible = false;
                    return;
                }
            }
        }

        private void dgvInvoiceHeaderD20_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String invoiceNum = dgvInvoiceHeaderD20.Rows[e.RowIndex].Cells["InvoiceNumD20"].Value.ToString();
                List<Service_Enquiry_Invoice_Items> oSelectedIvoItems = oItems_Invo.FindAll(x => x.SAH_INV_NO == invoiceNum);
                dgvInvoiceItemsD21.DataSource = new List<Service_Enquiry_Invoice_Items>();
                dgvInvoiceItemsD21.DataSource = oSelectedIvoItems;
            }
        }

        private void dgvInvoiceItemsD21_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String InvoiceNum = dgvInvoiceItemsD21.Rows[e.RowIndex].Cells["SAD_INV_NOD21"].Value.ToString();
                Int32 lineNum = Convert.ToInt32(dgvInvoiceItemsD21.Rows[e.RowIndex].Cells["SAD_ITM_LINED21"].Value.ToString());
                List<Service_Enquiry_Invoice_TAX> oInvoiceTAX = CHNLSVC.CustService.GET_SCV_INVO_ITM_TAX_ENQRY(InvoiceNum, lineNum);
                dgvInvoiceTAXD22.DataSource = new List<Service_Enquiry_Invoice_TAX>();
                dgvInvoiceTAXD22.DataSource = oInvoiceTAX;
            }
        }

        private void btnPartTransferd_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlMain.Enabled = false;
                pnlPartTransfered.Show();
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                List<Service_Enquiry_PartTrasferd> oItem = CHNLSVC.CustService.GET_SCV_PART_TRSFER_ENQRY(Jobnumber, jobLineNum, BaseCls.GlbUserComCode);
                dgvPartTransferdD23.DataSource = new List<Service_Enquiry_PartTrasferd>();
                if (oItem != null && oItem.Count > 0)
                {
                    dgvPartTransferdD23.DataSource = oItem;

                    for (int i = 0; i < dgvPartTransferdD23.Rows.Count; i++)
                    {
                        String Direction = dgvPartTransferdD23.Rows[i].Cells["ITH_DIRECTD23"].Value.ToString();

                        if (Direction.ToUpper() == "Inward".ToUpper())
                        {
                            dgvPartTransferdD23.Rows[i].Cells["INQtyD23"].Value = dgvPartTransferdD23.Rows[i].Cells["ITB_QTYD23"].Value;
                        }
                        else
                        {
                            dgvPartTransferdD23.Rows[i].Cells["OutQtyD23"].Value = dgvPartTransferdD23.Rows[i].Cells["ITB_QTYD23"].Value;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlMain.Enabled = true;
                    pnlPartTransfered.Visible = false;
                    return;
                }
            }
        }

        private void dgvRequestedItemsD16_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String job = dgvRequestedItemsD16.Rows[e.RowIndex].Cells["JOBD16"].Value.ToString();
                Int32 LineNum = Convert.ToInt32(dgvRequestedItemsD16.Rows[e.RowIndex].Cells["JOBLINED16"].Value.ToString());
                Int32 SeqNUm = Convert.ToInt32(dgvRequestedItemsD16.Rows[e.RowIndex].Cells["SEQD16"].Value.ToString());
                String Type = dgvRequestedItemsD16.Rows[e.RowIndex].Cells["ItemStatusTextD16"].Value.ToString();

                lblStatusSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["sentwcnD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["sentwcnD16"].Value.ToString();
                lblReceiveStatusSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["recwncD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["recwncD16"].Value.ToString();
                lblSerialSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["SerialD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["SerialD16"].Value.ToString();
                lblQtySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["QTYD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["QTYD16"].Value.ToString();
                lblCategorySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["FROMTABLED16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["FROMTABLED16"].Value.ToString();
                lblPartIDSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["PartIDD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["PartIDD16"].Value.ToString();
                lblOEMSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["OEMD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["OEMD16"].Value.ToString();
                lblCaseIdSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["CaseIDD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["CaseIDD16"].Value.ToString();
                lblRequestBySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCN_BY"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCN_BY"].Value.ToString();
                lblReqeustDateSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCNDT"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCNDT"].Value.ToString();
                lblRejectBySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_BY"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_BY"].Value.ToString();
                lblRejectedDateSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_DT"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_DT"].Value.ToString();

                List<Service_Enquiry_SupplierWrntyDetails> oitems = CHNLSVC.CustService.GET_SCV_SUPP_WRNTREQHDR_ENQ(job, LineNum, SeqNUm, Type);
                dgvWarrantyDetailsD23.DataSource = new List<Service_Enquiry_SupplierWrntyDetails>();
                if (oitems != null && oitems.Count > 0)
                {
                    if (oitems.FindAll(x => x.SWC_TP == "0").Count > 0)
                    {
                        dgvWarrantyDetailsD23.AutoGenerateColumns = false;
                        dgvWarrantyDetailsD23.DataSource = oitems.FindAll(x => x.SWC_TP == "0");

                        if (dgvWarrantyDetailsD23.Rows.Count == 1)
                        {
                            dgvWarrantyDetailsD23_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                        }
                    }
                    if (oitems.FindAll(x => x.SWC_TP == "1").Count > 0)
                    {
                        dgvReceivedDetails.AutoGenerateColumns = false;
                        dgvReceivedDetails.DataSource = oitems.FindAll(x => x.SWC_TP == "1");

                        if (dgvReceivedDetails.Rows.Count == 1)
                        {
                            dgvReceivedDetails_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                        }
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlCostSheet.Visible = false;
        }

        private void btnJobCostSheet_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10814))
            {
                if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
                {
                    pnlMain.Enabled = false;
                    pnlCostSheet.Show();
                    String Jobnumber = string.Empty;
                    Int32 jobLineNum = 0;
                    Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                    jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                    List<Service_Enquiry_ConfDetails> oItmes = CHNLSVC.CustService.GET_SCV_CONFDET_ENQRY(Jobnumber, jobLineNum, BaseCls.GlbUserComCode);
                    if (oItmes != null && oItmes.Count > 0)
                    {
                        dgvCostDetailsD24.DataSource = new List<Service_Enquiry_ConfDetails>();
                        dgvCostDetailsD24.DataSource = oItmes;

                        List<Service_Enquiry_CostSheet> oCostItems = CHNLSVC.CustService.GET_SCV_COST_SHEET_ENQRY(Jobnumber, jobLineNum, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        dgvCostItemsD25.DataSource = new List<Service_Enquiry_CostSheet>();
                        dgvCostItemsD25.DataSource = oCostItems;
                    }
                    else
                    {
                        MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlMain.Enabled = true;
                        pnlCostSheet.Visible = false;
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10814", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnWarrantyReplacemensts_Click(object sender, EventArgs e)
        {

            lblRequestNo.Text = "";
            lblReqFrom.Text = "";
            lblOldPartReceivedLoc.Text = "";
            lblCustomerPrefferdLoc.Text = "";
            lblReqUser.Text = "";
            lblReqDate.Text = "";
            lblCurrentStatus.Text = "";
            lblApprovedBy.Text = "";
            lblApprovedDate.Text = "";
            lblDocNo.Text = "";
            lblLocation.Text = "";
            lblDateW.Text = "";
            lblReceivedUser.Text = "";
            lblReceivedDate.Text = "";
            lblSmartWarrPerc.Text = "";
            lblCreditVal.Text = "";
            label90.Text = "";
            label89.Text = "";
            label88.Text = "";
            label86.Text = "";
            label85.Text = "";

            dgvReqDetails.DataSource = null;
            dgvIssiedSers.DataSource = null;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10823))
            {
                if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
                {
                    try
                    {
                        String Jobnumber = string.Empty;
                        Int32 jobLineNum = 0;
                        Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                        jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                        DataTable dtDocs;
                        DataTable dtRecSerial;
                        DataTable dtIssuedSer;
                        DataTable dtRequest = CHNLSVC.CustService.GET_WRR_RPLC_DETAILS(BaseCls.GlbUserComCode, Jobnumber, out dtDocs, out dtRecSerial, out dtIssuedSer);

                        dgvReqDetails.DataSource = dtRecSerial;
                        if (dtRequest != null && dtRequest.Rows.Count > 0)
                        {
                            lblRequestNo.Text = dtRequest.Rows[0]["grah_ref"].ToString();
                            lblReqFrom.Text = dtRequest.Rows[0]["REQLOCDESC"].ToString();
                            lblOldPartReceivedLoc.Text = dtRequest.Rows[0]["ReceivedLoc"].ToString();
                            lblCustomerPrefferdLoc.Text = dtRequest.Rows[0]["CusPreffLoc"].ToString();
                            lblReqUser.Text = dtRequest.Rows[0]["CREATEUSER"].ToString();
                            lblReqDate.Text = dtRequest.Rows[0]["grah_cre_dt"].ToString();
                            lblCurrentStatus.Text = dtRequest.Rows[0]["text"].ToString();
                            lblApprovedBy.Text = dtRequest.Rows[0]["ApprovedUser"].ToString();
                            lblApprovedDate.Text = dtRequest.Rows[0]["grah_app_dt"].ToString();
                            lblSmartWarrPerc.Text = dtRequest.Rows[0]["SMART_PERC"].ToString();
                            lblCreditVal.Text = dtRequest.Rows[0]["CREDIT_VAL"].ToString();

                            pnlWarrantyRelpcement.Show();

                            if (dtDocs.Rows.Count > 0)
                            {
                                DataTable dtReceived = dtDocs.Select("ith_direct = 1").CopyToDataTable();
                                if (dtReceived != null && dtReceived.Rows.Count > 0)
                                {
                                    lblDocNo.Text = dtReceived.Rows[0]["ith_doc_no"].ToString();
                                    lblLocation.Text = dtReceived.Rows[0]["ith_loc"].ToString();
                                    lblDateW.Text = Convert.ToDateTime(dtReceived.Rows[0]["ith_doc_date"].ToString()).ToString("dd/MMM/yyyy");
                                    lblReceivedUser.Text = dtReceived.Rows[0]["ith_cre_by"].ToString();
                                    lblReceivedDate.Text = dtReceived.Rows[0]["ith_cre_when"].ToString();
                                }
                                DataTable dtIssed = dtDocs.Select("ith_direct = 0").CopyToDataTable();
                                if (dtIssed != null && dtIssed.Rows.Count > 0)
                                {
                                    label90.Text = dtIssed.Rows[0]["ith_doc_no"].ToString();
                                    label89.Text = dtIssed.Rows[0]["ith_loc"].ToString();
                                    label88.Text = Convert.ToDateTime(dtIssed.Rows[0]["ith_doc_date"].ToString()).ToString("dd/MMM/yyyy");
                                    label86.Text = dtIssed.Rows[0]["ith_cre_by"].ToString();
                                    label85.Text = dtIssed.Rows[0]["ith_cre_when"].ToString();
                                    dgvIssiedSers.DataSource = dtIssuedSer;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10823", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlCustCollecData.Visible = false;
        }

        private void btnCustomerColletionData_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10814))
            {
                if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
                {
                    String Jobnumber = string.Empty;
                    Int32 jobLineNum = 0;
                    pnlMain.Enabled = false;
                    pnlCustCollecData.Show();
                    Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                    jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                    dgvCustCollecDataD27.DataSource = new List<Service_Enquiry_CustCollectionData>();

                    List<Service_Enquiry_CustCollectionData> oItems = new List<Service_Enquiry_CustCollectionData>();
                    oItems = CHNLSVC.CustService.GET_CUST_COLLDATE_ENQRY(Jobnumber, jobLineNum, BaseCls.GlbUserComCode);

                    if (oItems.Count > 0)
                    {

                        dgvCustCollecDataD27.DataSource = oItems;
                    }
                    else
                    {
                        MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlMain.Enabled = true;
                        pnlCustCollecData.Visible = false;
                        return;
                    }
                }
            }
        }

        private void btnStageLog_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10814))
            {
                if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
                {
                    DataTable odata = new DataTable();
                        String Jobnumber = string.Empty;
                    Int32 jobLineNum = 0;
                    Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                    jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

                    dgvStageLogD28.DataSource = new List<_Service_Enquiry_StageLog>();

                    //List<_Service_Enquiry_StageLog> oItems = new List<_Service_Enquiry_StageLog>();
                    //oItems = CHNLSVC.CustService.GET_STAGELOG_ENQRY(Jobnumber, jobLineNum);
                    //Tharanga 2016/06/06
                    List<_Service_Enquiry_StageLog_stage> oItems = new List<_Service_Enquiry_StageLog_stage>();

                    List<_Service_Enquiry_StageLog_stage_list> newlist = new List<_Service_Enquiry_StageLog_stage_list>();  
                    _Service_Enquiry_StageLog_stage_list list = new _Service_Enquiry_StageLog_stage_list();

                    DateTime _tmpDate = DateTime.Now;
                    int allowduration = 0;
                    //IList<string> intList1 = new List<string>();
                    string duration = "";
                    string estimate_duration = "";
                    oItems = CHNLSVC.CustService.GET_STAGELOG_ENQRY_Stage(Jobnumber, jobLineNum);
                    foreach (_Service_Enquiry_StageLog_stage _pik in oItems)
                    {
                        list = new _Service_Enquiry_StageLog_stage_list();
                        odata = CHNLSVC.CustService.GetJobStage_des(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, _pik.SJL_JOBSTAGE);
                        if (odata.Rows.Count>1)
                        {
                            foreach (DataRow dr in odata.Rows)
                            {
                                if (_pik.SJL_CRE_DT > _tmpDate)
                                {
                                    allowduration=int.Parse( dr["SCP_DUR"].ToString());
                                    TimeSpan _dateDiff = (_pik.SJL_CRE_DT - _tmpDate);
                                    TimeSpan dateDifference = _pik.SJL_CRE_DT.Subtract(_tmpDate);
                                    int _timeDifferance = 0;
                                    int _dateDifferance = 0;
                                    string _periodType = dr["scp_dur_tp"] == DBNull.Value ? string.Empty : dr["scp_dur_tp"].ToString();
                                    if (!string.IsNullOrEmpty(_periodType))
                                    {
                                        if (_periodType == "HH")
                                        {
                                            
                                            _timeDifferance = _dateDiff.Hours;
                                            //if (_timeDifferance >= allowduration)
                                            //{
                                                estimate_duration = Convert.ToString(allowduration) + " " + "HH";
                                                duration = _dateDiff.ToString() + " "+"HH";
                                           // }
                                        }
                                        else if (_periodType == "DD")
                                        {
                                            _dateDifferance = _dateDiff.Days;
                                           // if (_dateDifferance>allowduration)
                                          //  {
                                                estimate_duration = Convert.ToString(allowduration) + " " + "DD";
                                                duration = _dateDiff.Days.ToString()+ " " + "DD";
                                          //  }
                                        }
                                    }

                                }
                                _tmpDate = _pik.SJL_CRE_DT;
                               }
 
                        }


                        list.JBS_DESC = (_pik.JBS_DESC.ToString());
                        list.SJL_CRE_DT = _pik.SJL_CRE_DT;
                        list.duration = duration;
                        list.estimate_duration = estimate_duration;
                        newlist.Add(list);
                    }


                    if (oItems.Count > 0)
                    {
                        pnlMain.Enabled = false;
                        pnlStageLog.Show();
                       // dgvStageLogD28.DataSource = oItems;
                        dgvStageLogD28.DataSource = newlist; //Tharanga
                    }
                    else
                    {
                        MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("You need permision to view this funtion. \nPermission code : " + "10814", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void GetRelaventJobs()
        {
            string err;
            if (!string.IsNullOrEmpty(txtSearial.Text.Trim()) || !string.IsNullOrEmpty(txtOtherSeari2.Text.Trim()) || !string.IsNullOrEmpty(txtOtherSearal1.Text.Trim()))
            {
                List<Service_Enquiry_Job_Det> oJobDetails = CHNLSVC.CustService.GET_JOB_DET_ENQRY_New(txtSearial.Text.Trim(), txtOtherSeari2.Text, txtOtherSearal1.Text, BaseCls.GlbUserComCode, txtLoc.Text, DateTime.MinValue, DateTime.MaxValue, out err);
                if (oJobDetails != null && oJobDetails.Count > 0)
                {
                    dgvJobsD8.DataSource = new List<Service_Enquiry_Job_Det>();
                    dgvJobsD8.DataSource = oJobDetails;
                }
                else
                {
                    MessageBox.Show("No jobs found", "Service Inquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GetJob()
        {
            List<Service_job_Det> oJobDetails;
            string msg;

            Int32 result = CHNLSVC.CustService.GetJobDetailsEnquiry(txtJobNumber.Text, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out oJobDetails, out oheader, out msg);
            if (result >= 1)
            {
                //Tharaka 2015-08-09 Check PC

                DataTable dtPcs = CHNLSVC.CustService.GET_EQULPC_TO_PC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                if (dtPcs != null && dtPcs.Rows.Count > 0)
                {
                    if (dtPcs.Select("PC = '" + oJobDetails[0].Jbd_pc + "'").Length == Convert.ToInt32(0))
                    {
                        lblContactPerson2.Text = "";
                        MessageBox.Show("Please enter a correct job number", "Service Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                oJobDetails.Where(x => x.Jbd_warr_stus == 1).ToList().ForEach(x => x.Jbd_warr_stus_text = "Active");
                oJobDetails.Where(x => x.Jbd_warr_stus == 0).ToList().ForEach(x => x.Jbd_warr_stus_text = "Inactive");

                dgvJobItemsD1.DataSource = new List<Service_job_Det>();
                dgvJobItemsD1.DataSource = oJobDetails;

                if (oJobDetails.Count > 0)
                {
                    dgvJobItemsD1_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }

                lblRequestNum.Text = oheader.SJB_REQNO;
                lblSite.Text = oheader.SC_DESC;
                lblJobType.Text = oheader.SJB_JOBTP;
                lblRequestedVia.Text = oheader.SERS_DESC;

                lblCustomer.Text = oheader.SJB_B_CUST_NAME;
                lblAddress.Text = oheader.SJB_B_ADD1;
                lblAddress2.Text = oheader.SJB_B_ADD2;
                lblContactPerson.Text = oheader.SJB_CNT_PERSON;
                lblTele.Text = oheader.SJB_MOBINO;
                lblContactNum.Text = oheader.SJB_CNT_PHNO;

                btnShowDelCustomer.Text = "Show Delivery Customer";
                lblCustomer.Text = oheader.SJB_B_CUST_NAME;
                lblAddress.Text = oheader.SJB_B_ADD1;
                lblAddress2.Text = oheader.SJB_B_ADD2;
                lblContactPerson.Text = oheader.SJB_CNT_PERSON;
                lblTele.Text = oheader.SJB_B_MOBINO;
                lblContactNum.Text = oheader.SJB_CNT_PHNO;
                lblOrderNum.Text = oheader.SJB_ORDERNO;
                lblRequestNum.Text = oheader.SJB_REQNO;
                lblCustomerCode.Text = oheader.SJB_B_CUST_CD;

                //kapila 12/8/2015
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(oheader.SJB_B_CUST_CD, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                    lblPhone.Text = oheader.SJB_B_PHNO + "/" + _masterBusinessCompany.Mbe_email;
                else
                    lblPhone.Text = oheader.SJB_B_PHNO;

                //Service_JOB_HDR OJobheader = CHNLSVC.CustService.GET_SCV_JOB_HDR(txtJobNumber.Text, BaseCls.GlbUserComCode);
                //SystemUser oSecUser = CHNLSVC.Security.GetUserByUserID(OJobheader.SJB_CRE_BY);
                //string name = (oSecUser != null) ? oSecUser.Se_usr_desc : string.Empty;
                //lblCreateBy.Text = OJobheader.SJB_CRE_BY + " - " + name;
            }
        }

        private void getJobDetails(Int32 JobLineNum)
        {
            //if (IsgetJobDetails == true)
            //{
            //    return;
            //}
            //IsgetJobDetails = true;

            lblStatus.Text = "";
            lblStarton.Text = "";
            lblEndon.Text = "";
            lblTransferdTo.Text = "";
            lblTransferToLoc.Text = "";
            lblMainJobNum.Text = "";
            Service_job_Det oJobDetaill;
            List<Service_Job_Defects> oJobDefects;
            List<Service_Enquiry_TechAllo_Hdr> oJobAllocations;
            List<Service_Enquiry_Tech_Cmnt> oTechComments;
            List<Service_Enquiry_StandByItems> oStandByItems;

            string msg;
            Decimal totalAmount = 0;
            List<Tuple<string, string, string>> ConRemark_Type_User = new List<Tuple<string, string, string>>();
            Cursor = Cursors.WaitCursor;
            int result = CHNLSVC.CustService.GetAllJobDetailsEnquiry(txtJobNumber.Text.Trim(), JobLineNum, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out oJobDetaill, out oJobDefects, out oJobAllocations, out oTechComments, out ConRemark_Type_User, out oStandByItems, out msg, out totalAmount);
            Cursor = Cursors.Default;



            if (result > 0)
            {
                dgvOpenDefetsD2.DataSource = new List<Service_Job_Defects>();
                dgvActualDefectsD3.DataSource = new List<Service_Job_Defects>();
                dgvStandByItemsD5.DataSource = new List<Service_Enquiry_StandByItems>();

                if (oJobDefects.FindAll(x => x.SRD_STAGE == "J").Count > 0)
                {
                    dgvOpenDefetsD2.DataSource = oJobDefects.FindAll(x => x.SRD_STAGE == "J");
                }
                if (oJobDefects.FindAll(x => x.SRD_STAGE == "W").Count > 0)
                {
                    dgvActualDefectsD3.DataSource = oJobDefects.FindAll(x => x.SRD_STAGE == "W");
                }
                if (oJobAllocations != null && oJobAllocations.Count > 0)
                {
                    dgvTechAllocationD6.DataSource = oJobAllocations;
                }
                if (oTechComments != null && oTechComments.Count > 0)
                {
                    dgvTechnicianFinalCommentsD7.DataSource = oTechComments;
                }
                if (oStandByItems != null && oStandByItems.Count > 0)
                {
                    dgvStandByItemsD5.DataSource = oStandByItems;
                }

                txtJobFinalComments.Text = ConRemark_Type_User[0].Item1.ToString();
                SystemUser oSecUser = CHNLSVC.Security.GetUserByUserID(ConRemark_Type_User[0].Item3.ToString());
                string nameIn = (oSecUser != null) ? oSecUser.Se_usr_desc : string.Empty;
                lblInvoicedBy.Text = ConRemark_Type_User[0].Item3.ToString() + " - " + nameIn;
                if (ConRemark_Type_User[0].Item2.ToString() == "C")
                {
                    label40.Text = "Conf. Remark";
                    label32.Text = "Confirm by";
                }
                else
                {
                    label40.Text = "Invoice Remark";
                    label32.Text = "Invoice by";
                }

                lblStatus.Text = oJobDetaill.StageText;
                if (oMainJobHeader != null)
                {
                    lblStarton.Text = oMainJobHeader.SJB_CRE_DT.ToString("dd/MMM/yyyy  hh:mm tt");
                }

                //List<Service_confirm_Header> oConHdrs = CHNLSVC.CustService.GetServiceConfirmHeader(BaseCls.GlbUserComCode, "", oJobDetaill.Jbd_loc, oJobDetaill.Jbd_pc, oJobDetaill.Jbd_jobno, "", "", "", DateTime.MinValue, DateTime.MaxValue);
                //if (oConHdrs != null && oConHdrs.Count > 0)
                //{
                //    lblEndon.Text = oConHdrs[0].Jch_cre_dt.ToString("dd/MMM/yyyy  hh:mm tt");

                //}
                //if (oJobDetaill.Jbd_techfin_dt_man == DateTime.MinValue)
                //{
                //    lblEndon.Text = "";
                //}
                //else
                //{
                //    lblEndon.Text = oJobDetaill.Jbd_techfin_dt_man.ToString("dd/MMM/yyyy  hh:mm tt");
                //}

                List<_Service_Enquiry_StageLog> oItems = new List<_Service_Enquiry_StageLog>();
                oItems = CHNLSVC.CustService.GET_STAGELOG_ENQRY(txtJobNumber.Text.Trim(), JobLineNum);
                if (oItems.Count > 0)
                {
                    if (oItems[oItems.Count - 1] != null)
                    {
                        lblEndon.Text = oItems[oItems.Count - 1].SJL_CRE_DT.ToString("dd/MMM/yyyy  hh:mm tt");
                    }
                }

                lblInvoiceTotal.Text = totalAmount.ToString("N2");

                SCV_TRANS_LOG oSCV_TRANS_LOG = CHNLSVC.CustService.GetTrasferDetailsEnquiry(txtJobNumber.Text.Trim(), JobLineNum, BaseCls.GlbUserDefLoca);
                if (oSCV_TRANS_LOG != null && oSCV_TRANS_LOG.Stl_jobno != null)
                {
                    lblTransferdTo.Text = oSCV_TRANS_LOG.Stl_from_loc;
                    lblTransferToLoc.Text = oSCV_TRANS_LOG.Stl_cur_loc;
                    lblMainJobNum.Text = oSCV_TRANS_LOG.Stl_sjobno;
                }

                lblAgreementNum.Text = oJobDetaill.Jbd_supp_cd;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, "SCM II", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void getSerialDetails(String Serial, String Item)
        {
            ucDefectHistory1.Serial = Serial;
            ucDefectHistory1.Item = Item;
            ucDefectHistory1.loadData();
        }

        private void getSavedItems(String JobNumber, Int32 jobLineNum)
        {
            dgvMRNDetailsD11.DataSource = null;
            DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(BaseCls.GlbUserComCode, JobNumber, jobLineNum);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                dgvMRNDetailsD11.DataSource = dtTemp;
            }
            else
            {
                MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlMain.Enabled = true;
                pnlMRNDetails.Visible = false;
                return;
            }
        }

        private void BindOutwardListGridData()
        {
            try
            {
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());

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
                    gvPendingD12.DataSource = _tblItems;
                }
                else
                {
                    DataTable dtTemp = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                    if (dtTemp.Select("ith_direct = 0 AND ith_job_no='" + Jobnumber + "'").Length > 0)
                    {
                        DataTable dtNew = dtTemp.Select("ith_direct = 0 AND ith_job_no='" + Jobnumber + "'").CopyToDataTable();
                        if (dtNew.Rows.Count > 0)
                        {
                            gvPendingD12.DataSource = dtNew;
                        }
                        else
                        {
                            MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pnlMain.Enabled = true;
                            pnlNewPartstoBeConfirmed.Visible = false;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlMain.Enabled = true;
                        pnlNewPartstoBeConfirmed.Visible = false;
                        return;
                    }
                }
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _table = new DataTable();
                _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, OutwardType);
                if (_table.Rows.Count <= 0)
                {
                    gvSerial.DataSource = _table;
                    var _tblItems = from dr in _table.AsEnumerable() group dr by new { Tus_itm_cd = dr["Tus_itm_cd"], Tus_itm_desc = dr["Tus_itm_desc"], Tus_itm_model = dr["Tus_itm_model"], Tus_itm_stus = dr["Tus_itm_stus"] } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => 0) };
                    gvItem.DataSource = _tblItems;
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

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        protected void BindSelectedOutwardNo(int _rowIndex)
        {
            try
            {
                _supplier = string.Empty; _subdoc = string.Empty;

                this.Cursor = Cursors.WaitCursor;

                OutwardNo = Convert.ToString(gvPendingD12.Rows[_rowIndex].Cells["pen_docno"].Value);

                DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(BaseCls.GlbUserComCode, OutwardNo);

                if (_headerchk != null && _headerchk.Rows.Count > 0)
                {
                    string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                    string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));
                    if (!string.IsNullOrEmpty(_headerUser))
                        if (BaseCls.GlbUserID.Trim() != _headerUser.Trim())
                        {
                            MessageBox.Show("Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate, "Scanned Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                }

                hdnOutwarddate = Convert.ToDateTime(gvPendingD12.Rows[_rowIndex].Cells["pen_Date"].Value);

                OutwardType = Convert.ToString(gvPendingD12.Rows[_rowIndex].Cells["pen_Type"].Value);

                //lblIssuedDocNo.Text = OutwardNo;

                //lblIssedCompany.Text = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_issueCompany"].Value);

                //lblIssuedLocation.Text = Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_issueLocation"].Value);

                _supplier = Convert.ToString(gvPendingD12.Rows[_rowIndex].Cells["pen_supcode"].Value);

                _subdoc = Convert.ToString(gvPendingD12.Rows[_rowIndex].Cells["pen_subdoc"].Value);

                //DataTable _tbl = null;

                //if (!string.IsNullOrEmpty(lblIssuedLocation.Text) && !string.IsNullOrEmpty(lblIssedCompany.Text))
                //    _tbl = CHNLSVC.Inventory.Get_location_by_code(lblIssedCompany.Text.Trim(), lblIssuedLocation.Text.Trim());

                //if (_tbl != null && _tbl.Rows.Count > 0)
                //    lblIssueLocDesc.Text = _tbl.Rows[0].Field<string>("ml_loc_desc");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                BindOutwardItems();
            }
        }

        protected void BindOutwardItems()
        {
            try
            {
                _dono = string.Empty; PickSerialsList = null;
                ReptPickHeader _reptPickHdr = new ReptPickHeader();
                Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, OutwardNo);
                UserSeqNo = _seq;
                _reptPickHdr.Tuh_direct = true;
                _reptPickHdr.Tuh_doc_no = OutwardNo;
                _reptPickHdr.Tuh_doc_tp = OutwardType;
                _reptPickHdr.Tuh_ischek_itmstus = false;
                _reptPickHdr.Tuh_ischek_reqqty = true;
                _reptPickHdr.Tuh_ischek_simitm = false;
                _reptPickHdr.Tuh_session_id = BaseCls.GlbUserSessionID;
                _reptPickHdr.Tuh_usr_com = BaseCls.GlbUserComCode;
                _reptPickHdr.Tuh_usr_id = BaseCls.GlbUserID;
                _reptPickHdr.Tuh_usrseq_no = _seq;
                string _unavailableitemlist = string.Empty;
                List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(BaseCls.GlbUserDefLoca, hdnAllowBin, _reptPickHdr, out _unavailableitemlist);
                if (PickSerials != null)
                {
                    if (Convert.ToString(gvPendingD12.SelectedRows[0].Cells["pen_Type"].Value) == "PRN")
                    {
                        DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                        int _adhocline = 1;
                        foreach (ReptPickSerials _pik in PickSerials)
                        {
                            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_pik.Tus_ser_id);
                            if (_master != null && !string.IsNullOrEmpty(_master.Irsm_com))
                            {
                                _pik.Tus_new_remarks = _master.Irsm_anal_2;
                                _dono = _master.Irsm_anal_2;
                                DataTable _tbl = CHNLSVC.Inventory.GetPOLine(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dono, _pik.Tus_ser_id);
                                if (_tbl != null && _tbl.Rows.Count > 0)
                                {
                                    _pik.Tus_itm_stus = _tbl.Rows[0].Field<string>("itb_itm_stus");
                                    _pik.Tus_new_status = Convert.ToString(_tbl.Rows[0].Field<Int32>("itb_base_refline"));
                                    _pik.Tus_base_itm_line = _tbl.Rows[0].Field<Int32>("itb_base_refline");
                                }
                                else
                                {
                                    var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_scm2_imp")).ToList();
                                    _pik.Tus_itm_stus = Convert.ToString(_lp[0]);
                                    _pik.Tus_new_status = Convert.ToString(_adhocline);
                                    _pik.Tus_base_itm_line = _adhocline; _adhocline += 1;
                                }
                            }
                        }
                    }
                    else if (Convert.ToString(gvPendingD12.SelectedRows[0].Cells["pen_Type"].Value) == "DO")
                    {
                        DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                        //int _adhocline = 1;
                        foreach (ReptPickSerials _pik in PickSerials)
                        {
                            var _lp = _status.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_lp_cd")).ToList();

                            _pik.Tus_itm_stus = Convert.ToString(_lp[0]);
                            //_pik.Tus_new_status = Convert.ToString(_adhocline);
                            //_pik.Tus_base_itm_line = _adhocline; _adhocline += 1;
                        }
                    }

                    var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                    BindingSource _sourceItem = new BindingSource();
                    BindingSource _sourceSerial = new BindingSource();
                    _sourceItem.DataSource = _tblItems;
                    gvItem.DataSource = _sourceItem;
                    _sourceSerial.DataSource = PickSerials; gvSerial.DataSource = _sourceSerial;
                    PickSerialsList = PickSerials;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void modifyGrid()
        {
            if (dgvItemsD13.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItemsD13.Rows.Count; i++)
                {
                    if (dgvItemsD13.Rows[i].Cells["serial_no"].Value.ToString() == "N/A")
                    {
                        dgvItemsD13.Rows[i].Cells["Qty"].ReadOnly = false;
                    }
                    else
                    {
                        dgvItemsD13.Rows[i].Cells["Qty"].ReadOnly = true;
                    }
                }
            }
        }

        private void SetParameterNames()
        {
            lblSerialNumer.Text = Serial1;
            lblOtherSerial1.Text = SerialOth1;
            lblOtherSerial2.Text = SerialOth2;
        }

        #region Panal Hiding

        private void button12_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlStageLog.Visible = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            lblDefectHistyHeader.Text = "Defect History";
            pnlMain.Enabled = true;
            pnlDefectHistory.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlMRNDetails.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlNewPartsInHand.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlOldPartDetails.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlTemporyIssueDetails.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlEstimate.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlPartTransfered.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlWarrantyRelpcement.Visible = false;
        }

        #endregion Panal Hiding

        private void txtOtherSeari2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOtherSeari2.Text))
            {
                List<Service_job_Det> oItem = CHNLSVC.CustService.SCV_JOB_GET_SER_OSR_REG(BaseCls.GlbUserComCode, txtLoc.Text, "", txtOtherSeari2.Text, "");
                if (oItem == null || oItem.Count == 0)
                {
                    MessageBox.Show("Please enter correct " + SerialOth1, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOtherSeari2.Clear();
                    txtOtherSeari2.Focus();
                    return;
                }
            }
        }

        private void txtJobNumber_Click(object sender, EventArgs e)
                {
            txtJobNumber.SelectAll();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                String Jobnumber = string.Empty;
                Int32 jobLineNum = 0;
                string SerialNo = "";
                Jobnumber = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_jobnoD1"].Value.ToString();
                jobLineNum = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                SerialNo = dgvJobItemsD1.SelectedRows[0].Cells["Jbd_ser1D1"].ToString();

                ImageUpload frm = new ImageUpload(Jobnumber, jobLineNum, SerialNo, 0);
                frm.ShowDialog();
            }
        }

        private void txtOtherSeari2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtOtherSeari2_DoubleClick(null, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void clearAll()
        {
            txtLoc.Text = BaseCls.GlbUserDefLoca;
            txtJobNumber.Clear();
            txtSearial.Clear();
            txtOtherSearal1.Clear();
            txtOtherSeari2.Clear();
            dtpDate.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;

            lblRequestNum.Text = "";
            lblSite.Text = "";
            lblJobType.Text = "";
            lblRequestedVia.Text = "";
            lblAgreementNum.Text = "";
            lblOrderNum.Text = "";
            lblCustomer.Text = "";
            lblCustomerCode.Text = "";
            lblPhone.Text = "";

            lblAddress.Text = "";
            lblAddress2.Text = "";
            lblContactPerson.Text = "";
            lblContactPerson2.Text = "";
            lblStatus.Text = "";
            lblTransferdTo.Text = "";

            lblTransferToLoc.Text = "";

            lblMainJobNum.Text = "";
            lblStarton.Text = "";
            lblEndon.Text = "";
            lblContactNum.Text = "";
            lblTele.Text = "";

            lblCreateBy.Text = "";
            lblInvoicedBy.Text = "";

            lblInvoiceTotal.Text = "0.00";

            txtInicialRemarks.Clear();
            txtWorkInstrucktions.Clear();
            txtJobFinalComments.Clear();

            txtSer.Text = "";

            pnlSerialDetails.Visible = false;

            oheader = new Service_Enquiry_Job_Hdr();

            dgvJobsD8.DataSource = new List<Service_Enquiry_Job_Det>();
            dgvJobItemsD1.DataSource = new List<Service_job_Det>();
            dgvOpenDefetsD2.DataSource = new List<Service_Job_Defects>();
            dgvActualDefectsD3.DataSource = new List<Service_Job_Defects>();
            dgvTechAllocationD6.DataSource = new List<Service_Enquiry_TechAllo_Hdr>();
            dgvStandByItemsD5.DataSource = new List<Service_Enquiry_StandByItems>();
            dgvEstimateHeaderD17.DataSource = new List<Service_Enquiry_Estimate_Hdr>();
            dgvInvoiceItemsD21.DataSource = new List<Service_Enquiry_Invoice_Items>();
            dgvTechnicianFinalCommentsD7.DataSource = new List<Service_Enquiry_Tech_Cmnt>();

            btnShowDelCustomer.Text = "Show Delivery Customer";

            pnlDefectHistory.Hide();
            pnlDefectHistory.Size = new System.Drawing.Size(768, 507);
            pnlDefectHistory.Location = new System.Drawing.Point(60, 108);

            pnlNewPartstoBeConfirmed.Size = new System.Drawing.Size(768, 507);
            pnlNewPartstoBeConfirmed.Location = new System.Drawing.Point(60, 108);

            pnlMRNDetails.Hide();
            pnlMRNDetails.Size = new System.Drawing.Size(770, 308);
            pnlMRNDetails.Location = new System.Drawing.Point(60, 108);

            pnlNewPartstoBeConfirmed.Hide();
            pnlNewPartstoBeConfirmed.Size = new System.Drawing.Size(768, 507);
            pnlNewPartstoBeConfirmed.Location = new System.Drawing.Point(60, 108);

            pnlNewPartsInHand.Hide();
            pnlNewPartsInHand.Size = new System.Drawing.Size(768, 507);
            pnlNewPartsInHand.Location = new System.Drawing.Point(60, 108);

            pnlOldPartDetails.Hide();
            pnlOldPartDetails.Size = new System.Drawing.Size(768, 507);
            pnlOldPartDetails.Location = new System.Drawing.Point(60, 108);

            pnlTemporyIssueDetails.Hide();
            pnlTemporyIssueDetails.Size = new System.Drawing.Size(768, 507);
            pnlTemporyIssueDetails.Location = new System.Drawing.Point(60, 108);

            pnlSupplieWarrantyClaim.Hide();
            pnlSupplieWarrantyClaim.Size = new System.Drawing.Size(841, 518);
            pnlSupplieWarrantyClaim.Location = new System.Drawing.Point(60, 108);

            pnlEstimate.Hide();
            pnlEstimate.Size = new System.Drawing.Size(768, 507);
            pnlEstimate.Location = new System.Drawing.Point(60, 108);

            PnlInvoiceDetails.Hide();
            PnlInvoiceDetails.Size = new System.Drawing.Size(768, 507);
            PnlInvoiceDetails.Location = new System.Drawing.Point(60, 108);

            pnlPartTransfered.Hide();
            pnlPartTransfered.Size = new System.Drawing.Size(768, 507);
            pnlPartTransfered.Location = new System.Drawing.Point(60, 108);

            pnlCostSheet.Hide();
            pnlCostSheet.Size = new System.Drawing.Size(768, 507);
            pnlCostSheet.Location = new System.Drawing.Point(60, 108);

            pnlWarrantyRelpcement.Hide();
            //pnlWarrantyRelpcement.Size = new System.Drawing.Size(768, 507);
            //pnlWarrantyRelpcement.Location = new System.Drawing.Point(60, 108);
            pnlWarrantyRelpcement.Size = new System.Drawing.Size(609, 599);
            pnlWarrantyRelpcement.Location = new System.Drawing.Point(150, 40);


            dgvReqDetails.AutoGenerateColumns = false;
            dgvIssiedSers.AutoGenerateColumns = false;


            pnlCustCollecData.Hide();
            pnlCustCollecData.Size = new System.Drawing.Size(768, 507);
            pnlCustCollecData.Location = new System.Drawing.Point(60, 108);

            pnlStageLog.Hide();
            pnlStageLog.Size = new System.Drawing.Size(768, 507);
            pnlStageLog.Location = new System.Drawing.Point(60, 108);

            pnlAdditionalItems.Size = new Size(702, 163);

            foreach (Control item in groupBox4.Controls)
            {
                if (item is Button)
                {
                    item.Enabled = false;
                }
            }

            IsFromSerial = false;
            //IsgetJobDetails = false;
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

        private void dgvJobItemsD1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvJobItemsD1.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = dgvJobItemsD1.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
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

        private void copyGridCell(object sender, DataGridViewCellEventArgs e)
        {
            {
                try
                {
                    DataGridView dgv = (DataGridView)sender;

                    this.Cursor = Cursors.WaitCursor;
                    if (dgv.ColumnCount > 0)
                    {
                        Int32 _rowIndex = e.RowIndex;
                        Int32 _colIndex = e.ColumnIndex;

                        if (_rowIndex != -1)
                        {
                            #region Copy text

                            string _copyText = dgv.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
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

        private void btnJobTasks_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNumber.Text))
            {
                MessageBox.Show("Please select the job number.", "Job Tasks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ServiceTasks frm = new ServiceTasks(txtJobNumber.Text, 0);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            ServicePayments frm = new ServicePayments(txtJobNumber.Text, 0, lblCustomerCode.Text);
            frm.StartPosition = FormStartPosition.CenterParent;

            frm.Show();
        }

        private void btnSubItems_Click(object sender, EventArgs e)
        {
            if (dgvJobItemsD1.Rows.Count > 0 && dgvJobItemsD1.SelectedRows.Count > 0)
            {
                pnlAdditionalItems.Visible = true;
                Int32 jobLine = Convert.ToInt32(dgvJobItemsD1.SelectedRows[0].Cells["Jbd_joblineD1"].Value.ToString());
                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(txtJobNumber.Text.Trim(), jobLine);
                grvAddiItems.AutoGenerateColumns = false;
                grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
                grvAddiItems.DataSource = oSubItems;
            }
        }

        private void btnPopupVehClose_Click(object sender, EventArgs e)
        {
            pnlAdditionalItems.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _warrSearchtp = _Parameters.SP_DB_SERIAL;
                _warrSearchorder = "SER";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSer;
                _CommonSearch.ShowDialog();
                txtSer.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }

        }

        private void btnCloseSerialPnl_Click(object sender, EventArgs e)
        {
            pnlSerialDetails.Visible = false;
        }

        private void Load_Serial_Infor(TextBox _txt, string _warrNo, DateTime _jobDt)
        {
            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
            List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
            Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;

            string _ser = null;
            string _warr = null;
            string _regno = null;
            string _invcno = null;

            if (string.IsNullOrEmpty(_warrNo))
            {
                if (_txt == txtSer) _ser = txtSer.Text.ToString();
                //if (_tmxt == txtWar) _warr = txtWar.Text.ToString();
                //if (_txt == txtRegNo) _regno = txtWar.Text.ToString();
            }
            else
            {
                _warr = _warrNo;
            }

            _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, "", _regno, _warr, "", "", 0, out _returnStatus, out _returnMsg);
            //if (_warrMstDic == null)
            //{
            //    SystemInformationMessage("There is no warranty details available.", "No warranty");
            //    _txt.Clear();
            //    _txt.Focus();
            //    return;
            //}

            //foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
            //{
            //    _warrMst = pair.Key;
            //    _warrMstSub = pair.Value;
            //}
            //if (_warrMst == null)
            //{
            //    SystemInformationMessage("There is no warranty details available.", "No warranty");
            //    _txt.Clear();
            //    _txt.Focus();
            //    return;
            //}
            //if (_warrMst.Count <= 0)
            //{
            //    SystemInformationMessage("There is no warranty details available.", "No warranty");
            //    _txt.Clear();
            //    _txt.Focus();
            //    return;
            //}


            //if (_warrMst.Count > 1)
            //{
            //    gvMultipleItem.AutoGenerateColumns = false;
            //    gvMultipleItem.DataSource = new List<InventorySerialMaster>();
            //    gvMultipleItem.DataSource = _warrMst;
            //    pnlMultiItems.Visible = true;
            //    return;
            //}
            //else
            //{
            //    FillItemDetails(_warrMst[0], _jobDt);
            //    if (_warrMstSub != null)
            //    {
            //        if (_warrMstSub.Count > 0)
            //        {
            //            Cursor = Cursors.WaitCursor;
            //            _tempItemSubList = new List<Service_Job_Det_Sub>();
            //            _tempItemSubList = FillItemSubDetails(_warrMstSub);
            //            pnlAdditionalItems.Visible = true;

            //            if (!string.IsNullOrEmpty(txtReqNo.Text) && optJob.Checked)
            //            {
            //                Int32 selectedItmLine = 0;
            //                for (int i = 0; i < grvJobItms.Rows.Count; i++)
            //                {
            //                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
            //                    {
            //                        //selectedItmLine = i;
            //                        selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["jbd_jobline"].Value);
            //                    }
            //                }

            //                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(txtReqNo.Text, selectedItmLine);

            //                grvAddiItems.AutoGenerateColumns = false;
            //                grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
            //                grvAddiItems.DataSource = _tempItemSubList;// oSubItems;
            //                Cursor = Cursors.Default;

            //                _scvItemSubList = new List<Service_Job_Det_Sub>();

            //                for (int i = 0; i < grvAddiItems.Rows.Count; i++)
            //                {
            //                    if (oSubItems.FindAll(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()).Count > 0)
            //                    {
            //                        grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
            //                        _scvItemSubList.Add(oSubItems.Find(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()));
            //                    }

            //                    if (Convert.ToInt32(grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value.ToString()) < 0)
            //                    {
            //                        grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value = "N/A";
            //                    }
            //                }

            //                //btnAddAdditional.Visible = true;
            //            }
            //            else
            //            {
            //                btnAddAdditional.Visible = true;

            //                grvAddiItems.AutoGenerateColumns = false;

            //                if (grvAddiItems.Rows.Count == 0)
            //                {
            //                    grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
            //                    grvAddiItems.DataSource = _tempItemSubList;

            //                    for (int i = 0; i < grvAddiItems.Rows.Count; i++)
            //                    {
            //                        if (grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value.ToString() != "N/A" && Convert.ToInt32(grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value.ToString()) < 0)
            //                        {
            //                            grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value = "N/A";
            //                        }

            //                        grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = false;

            //                        string itemCodess = grvAddiItems.Rows[i].Cells["jbds_itm_cd"].Value.ToString();
            //                        if (_scvItemSubList.FindAll(x => x.JBDS_ITM_CD == itemCodess).Count > 0)
            //                        {
            //                            grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
            //                        }
            //                    }
            //                }
            //                Cursor = Cursors.Default;
            //            }
            //            SetReadOnlyClmn();
            //        }
            //    }
            //}
        }

        private void txtSer_Leave(object sender, EventArgs e)
        {

        }

        private void txtSer_DoubleClick(object sender, EventArgs e)
        {
            button14_Click(null, null);
        }

        private void btnSearchSearialMain_Click(object sender, EventArgs e)
        {
            label61.Text = "Profit Center";
            label67.Text = "Invoice Date";
            label99.Text = "Invoice No";

            lblPCSerial.Text = "";
            lblItemCodeSerial.Text = "";
            lblUnitRateSerial.Text = "";
            lblCustomerCodeSerail.Text = "";
            lblCompantNameSerial.Text = "";
            lblInvoiceDateSerial.Text = "";
            lbladd1Serial.Text = "";
            lbladd2Serial.Text = "";
            lblInvoiceSerial.Text = "";

            lblAddWarrPeriod.Text = "";
            lblAddWarrRemark.Text = "";
            lblAddWarrStartDate.Text = "";

            lblUnitRateSerial.ForeColor = Color.Blue;

            if (!string.IsNullOrEmpty(txtSer.Text))
            {
                pnlSerialDetails.Visible = true;

                string err = string.Empty;
                DataTable dtTempWarra;
                DataTable dtInvoiceDetails;
                DataTable dtWarrReplace;
                DataTable dtIntHdr;
                DataTable dt_inr_sermst;

                try
                {
                    int result = CHNLSVC.CustService.getSerialDetails(txtSer.Text.Trim(), out   err, out   dtTempWarra, out   dtInvoiceDetails, out dtWarrReplace, out dtIntHdr);

                    if (dtTempWarra != null && dtTempWarra.Rows.Count > 0)
                    {
                        if (dtTempWarra.Rows[0]["WARRANTYSTATUS"].ToString() == "N" && dtWarrReplace != null && dtWarrReplace.Rows.Count > 0)
                        {
                            if (MessageBox.Show("Item is warranty replaced. Do you want to view", "Enquiry", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                    if (dtIntHdr == null || dtIntHdr.Rows.Count == 0)
                    {
                        lblPCSerial.Text = dtInvoiceDetails.Rows[0]["profit_center_code"].ToString() + "  " + dtInvoiceDetails.Rows[0]["cost_center_description"].ToString();
                        lblItemCodeSerial.Text = dtInvoiceDetails.Rows[0]["item_code"].ToString();
                        lblUnitRateSerial.Text = dtInvoiceDetails.Rows[0]["unit_rate"].ToString();

                        if (lblUnitRateSerial.Text != "")
                        {
                            decimal uniteRate = Convert.ToDecimal(dtInvoiceDetails.Rows[0]["TOT_AMOUNT"].ToString()) / Convert.ToDecimal(dtInvoiceDetails.Rows[0]["QTY"].ToString());
                            //lblUnitRateSerial.Text = Convert.ToDecimal(lblUnitRateSerial.Text).ToString("N2");
                            lblUnitRateSerial.Text = uniteRate.ToString("N2");

                            if (uniteRate == 0)
                            {
                                lblUnitRateSerial.Text = "FOC Issue";
                                lblUnitRateSerial.ForeColor = Color.Red;
                            }
                        }

                        lblCustomerCodeSerail.Text = dtInvoiceDetails.Rows[0]["customer_code"].ToString();
                        lblCompantNameSerial.Text = dtInvoiceDetails.Rows[0]["company_name"].ToString();
                        lblInvoiceDateSerial.Text = Convert.ToDateTime(dtInvoiceDetails.Rows[0]["invoice_date"].ToString()).ToString("dd/MMM/yyyy");

                        lbladd1Serial.Text = dtInvoiceDetails.Rows[0]["d_add1"].ToString();
                        lbladd2Serial.Text = dtInvoiceDetails.Rows[0]["d_add2"].ToString();

                        lblAddWarrPeriod.Text = dtTempWarra.Rows[0]["add_warr_pb"].ToString();
                        lblAddWarrRemark.Text = dtTempWarra.Rows[0]["add_warr_rem"].ToString();
                        lblAddWarrStartDate.Text = dtTempWarra.Rows[0]["add_warr_stdt"].ToString();

                        lblInvoiceSerial.Text = dtTempWarra.Rows[0]["INVOICENO"].ToString();
                        //kapila 10/11/2016
                        if (dtInvoiceDetails.Rows[0]["SAH_INV_TP"].ToString() == "HS")
                        {
                            decimal _accBal = 0;
                            HpAccountSummary SUMMARY = new HpAccountSummary();
                            HpAccount Acc = new HpAccount();
                            Acc.Hpa_acc_no = dtInvoiceDetails.Rows[0]["SAH_ACC_NO"].ToString();
                            _accBal = SUMMARY.getAccountBal(dtInvoiceDetails.Rows[0]["profit_center_code"].ToString(),Acc, DateTime.Now.Date);
                            if (_accBal > 0)
                                lblInvoiceSerial.Text = dtTempWarra.Rows[0]["INVOICENO"].ToString() + "  (A/C Balance : " + FormatToCurrency(_accBal.ToString()) + ")";
                        }
                        if (dtInvoiceDetails.Rows[0]["SAH_INV_TP"].ToString() == "CRED")
                        {
                            DataTable _dtOuts = CHNLSVC.Financial.Get_credit_Outs_ByInv(BaseCls.GlbUserComCode, dtInvoiceDetails.Rows[0]["profit_center_code"].ToString(), dtTempWarra.Rows[0]["INVOICENO"].ToString(), BaseCls.GlbUserID, 1);
                                if(_dtOuts.Rows.Count>0)
                                {
                                    decimal _credBal =Convert.ToDecimal(_dtOuts.Rows[0]["ds_tot_bal"]);
                                    lblInvoiceSerial.Text = dtTempWarra.Rows[0]["INVOICENO"].ToString() + "  (Outstanding : " + FormatToCurrency(_credBal.ToString()) + ")";
                                }
                        }
                    }
                    else
                    {
                        if (dtIntHdr.Rows[0]["ith_cate_tp"].ToString() == "FGAP")
                        {
                            label61.Text = "Location";
                            label67.Text = "Document Date";
                            label99.Text = "Document No";
                            lblPCSerial.Text = dtIntHdr.Rows[0]["ith_loc"].ToString() + "  " + dtIntHdr.Rows[0]["ml_loc_desc"].ToString();
                            lblItemCodeSerial.Text = dtTempWarra.Rows[0]["itemcode"].ToString();
                            lblUnitRateSerial.Text = "0.00";
                            lblCustomerCodeSerail.Text = "-";
                            lblCompantNameSerial.Text = "-";
                            lblInvoiceDateSerial.Text = Convert.ToDateTime(dtIntHdr.Rows[0]["ith_doc_date"].ToString()).ToString("dd/MMM/yyyy");

                            lbladd1Serial.Text = "-";
                            lbladd2Serial.Text = "-";
                            lblAddWarrPeriod.Text = "-";
                            lblAddWarrRemark.Text = "-";
                            lblAddWarrStartDate.Text = "-";
                            lblInvoiceSerial.Text = dtIntHdr.Rows[0]["ith_doc_no"].ToString() + " ( FGAP )";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void txtSer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchSearialMain_Click(null, null);
            }
        }

        private void lblAddress_MouseHover(object sender, EventArgs e)
        {
            ToolTip otoolTip1 = new ToolTip();
            otoolTip1.IsBalloon = true;
            otoolTip1.ShowAlways = true;
            otoolTip1.SetToolTip(lblAddress, lblAddress.Text);
        }

        private void lblAddress2_MouseHover(object sender, EventArgs e)
        {
            ToolTip otoolTip1 = new ToolTip();
            otoolTip1.IsBalloon = true;
            otoolTip1.ShowAlways = true;
            otoolTip1.SetToolTip(lblAddress2, lblAddress2.Text);
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            CustomerSatisfaction frm = new CustomerSatisfaction(txtJobNumber.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X, this.Location.Y + 60);
            frm.ShowDialog();
        }

        private void btnVisitCom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNumber.Text))
            {
                MessageBox.Show("Please select the job number.", "Job Tasks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ServiceWIP_VisitComent frm = new ServiceWIP_VisitComent(txtJobNumber.Text, 0);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void clerSWCLables()
        {
            lblStatusSWC.Text = "";
            lblReceiveStatusSWC.Text = "";
            lblSerialSWC.Text = "";
            lblQtySWC.Text = "";
            lblCategorySWC.Text = "";
            lblRejectBySWC.Text = "";
            lblPartIDSWC.Text = "";
            lblOEMSWC.Text = "";
            lblCaseIdSWC.Text = "";
            lblRequestBySWC.Text = "";
            lblReqeustDateSWC.Text = "";
            lblRejectedDateSWC.Text = "";

            lblSD2DocNumber.Text = "";
            lblRequestNumberasdasd.Text = "";
            lblItemCodeqwe.Text = "";
            lblSuppIteme3fg.Text = "";
            lblSerialrf43.Text = "";
            lblStatussad.Text = "";
            lblHoldReasonasd.Text = "";
            lblWarrantyNumasd.Text = "";
            lblSuppWarrntyNumaSd.Text = "";
            lblOEMSerialNumasd.Text = "";
            lblCaseID.Text = "";
            lblOtherDocasasd.Text = "";
            lblItemStatusasd.Text = "";
            lblDocNumasd.Text = "";
            lblDateasd.Text = "";
            lblTypeasd.Text = "";
            lblSuppilerasd.Text = "";
            lblClaimSupplierasd.Text = "";
            lblOtherDoc.Text = "";
            lblRemarkasd.Text = "";
            lblBillNumasd.Text = "";
            lblBillDate.Text = "";

            label136.Text = "";
            label162.Text = "";
            label138.Text = "";
            label165.Text = "";
            label147.Text = "";
            label169.Text = "";
            label140.Text = "";
            label167.Text = "";
            label149.Text = "";
            label171.Text = "";
            label137.Text = "";
            label164.Text = "";
            label139.Text = "";
            label166.Text = "";
            label148.Text = "";
            label170.Text = "";
            label146.Text = "";
            label168.Text = "";
            label150.Text = "";
            label172.Text = "";
            label173.Text = "";
            label174.Text = "";
        }

        private void dgvWarrantyDetailsD23_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                lblSD2DocNumber.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_DOC_NO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_DOC_NO"].Value.ToString();
                lblRequestNumberasdasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_othdocnoD16"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_othdocnoD16"].Value.ToString();
                lblItemCodeqwe.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITMCD"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITMCD"].Value.ToString();
                lblSuppIteme3fg.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPITMCD"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPITMCD"].Value.ToString();
                lblSerialrf43.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SER1"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SER1"].Value.ToString();
                lblStatussad.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_STUS"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_STUS"].Value.ToString();
                lblHoldReasonasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_hold_reason"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_hold_reason"].Value.ToString();
                lblWarrantyNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_WARRNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_WARRNO"].Value.ToString();
                lblSuppWarrntyNumaSd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPWARRNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPWARRNO"].Value.ToString();
                lblOEMSerialNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OEMSERNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OEMSERNO"].Value.ToString();
                lblCaseID.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_CASEID"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_CASEID"].Value.ToString();
                lblOtherDocasasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OTHDOCNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OTHDOCNO"].Value.ToString();
                lblItemStatusasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITM_STUS"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITM_STUS"].Value.ToString();
                lblDocNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DOC_NO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DOC_NO"].Value.ToString();
                lblDateasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DT"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DT"].Value.ToString();
                lblTypeasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_TP"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_TP"].Value.ToString();
                lblSuppilerasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_SUPP"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_SUPP"].Value.ToString();
                lblClaimSupplierasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_CLM_SUPP"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_CLM_SUPP"].Value.ToString();
                lblOtherDoc.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_OTHDOCNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_OTHDOCNO"].Value.ToString();
                lblRemarkasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_RMKS"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_RMKS"].Value.ToString();
                lblBillNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_AIR_BILL_NO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_AIR_BILL_NO"].Value.ToString();
                lblBillDate.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_BILL_DT"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_BILL_DT"].Value.ToString();
            }
        }

        private void dgvReceivedDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                label136.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn1"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn1"].Value.ToString();
                label162.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                label138.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn3"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                label165.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn4"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn4"].Value.ToString();
                label147.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn5"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn5"].Value.ToString();
                label169.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn6"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn6"].Value.ToString();
                label140.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn7"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn7"].Value.ToString();
                label167.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn8"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn8"].Value.ToString();
                label149.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn9"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn9"].Value.ToString();
                label171.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn10"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn10"].Value.ToString();
                label137.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn11"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn11"].Value.ToString();
                label164.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn12"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn12"].Value.ToString();
                label139.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn13"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn13"].Value.ToString();
                label166.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn14"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn14"].Value.ToString();
                label148.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn15"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn15"].Value.ToString();
                label170.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn16"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn16"].Value.ToString();
                label146.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn17"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn17"].Value.ToString();
                label168.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn18"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn18"].Value.ToString();
                label150.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn19"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn19"].Value.ToString();
                label172.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn20"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn20"].Value.ToString();
                label173.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn21"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn21"].Value.ToString();
                label174.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn22"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn22"].Value.ToString();
            }
        }

        private void dgvRequestedItemsD16_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnsupcls_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlSupplieWarrantyClaim.Visible = false;
        }

        private void btnAlocPend_Click(object sender, EventArgs e)
        {
            AllocPendingJobs frm = new AllocPendingJobs();
            frm.setEmpCategory(BaseCls.GlbUserCategory);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(0, 55);
            frm.Show();
        }

        private void btnAlocAcept_Click(object sender, EventArgs e)
        {
            ServiceJobAccept frm = new ServiceJobAccept(Convert.ToDecimal(2.2));
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnCompAcept_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10133))
            {
                MessageBox.Show("Sorry, You have no permission !\n( Advice: Required permission code :10133)", "Job", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ServiceJobAccept frm = new ServiceJobAccept(Convert.ToDecimal(7.9));
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btncustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtcustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtcustomer.Select();

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

        private void btnBlkLst_Click(object sender, EventArgs e)
        {
            try
            {
                BlackListCustomers _blackListCustomers = new BlackListCustomers();
                _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(txtcustomer.Text.ToString(), 1);
                if (_blackListCustomers !=null )
                {
                    MessageBox.Show("This customer is already black listed.", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtcustomer.Focus();
                    return;
                }
               

                if (MessageBox.Show("Do you need to black list this customer?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BlackListCustomers _obj = new BlackListCustomers();

                    if (string.IsNullOrEmpty(txtBValue.Text))
                    {
                        MessageBox.Show("Please select the value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBValue.Focus();
                        return;
                    }

                    if (IsNumeric(txtBValue.Text) == false)
                    {
                        MessageBox.Show("Please select the valid value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBValue.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtBValue.Text) <= 0)
                    {
                        MessageBox.Show("Please select the valid value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBValue.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtBRemark.Text))
                    {
                        MessageBox.Show("Please select the remarks", "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBRemark.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtcustomer.Text))
                    {
                        MessageBox.Show("Please select the customer", "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBRemark.Focus();
                        return;
                    }
                    
                    _obj.Hbl_act = true;
                    _obj.Hbl_cls_bal = 0;
                    _obj.Hbl_cre_by = BaseCls.GlbUserID;
                    _obj.Hbl_cre_dt = DateTime.Now.Date;
                    _obj.Hbl_cust_cd = txtcustomer.Text;
                    _obj.Hbl_def_val = Convert.ToDecimal(txtBValue.Text.Trim());
                    _obj.Hbl_dt = DateTime.Now.Date;
                    _obj.Hbl_rmk = txtBRemark.Text.Trim();
                    _obj.Hbl_rmv_dt = Convert.ToDateTime("12/12/9999");
                    _obj.Hbl_com = BaseCls.GlbUserComCode;
                    _obj.Hbl_pc = BaseCls.GlbUserDefProf;
                    if (radSR.Checked == false)
                    {
                        _obj.Hbl_com = string.Empty;
                        _obj.Hbl_pc = string.Empty;
                    }

                    int effect = CHNLSVC.Sales.SaveBlackListCustomer(_obj);
                    if (effect == -1)
                        MessageBox.Show("Process terminated. Please try again", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    else
                        MessageBox.Show("Save successfully", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void btnRemoveBlkLst_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcustomer.Text))
                {
                    MessageBox.Show("Please select the customer", "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBRemark.Focus();
                    return;
                }

                if (MessageBox.Show("Do you need to release black list status?", "Remove Black List...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int effect = CHNLSVC.Sales.ReleaseBlackListCustomer(txtcustomer.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, radHd.Checked ? "1" : "0");
                    if (effect == -1)
                        MessageBox.Show("Process terminated. Please try again", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    else
                        MessageBox.Show("Update successfully", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void btnblacklist_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10149))
            {
                MessageBox.Show("Sorry, You have no permission to view black list customers!\n( Advice: Required permission code :10149)");
                return;
            }

            if (pnlBlkLst.Visible)
                pnlBlkLst.Visible = false;
            else
            {
                pnlBlkLst.Visible = true;
                pnlBlkLst.Size = new Size(592, 279);
                pnlBlkLst.Location = new Point(403, 26);
            }
        }

        private void txtsubJon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                IsFromSerial = false;
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SubJob);
                DataTable _result = CHNLSVC.CommonSearch.GetsubJobNo(_CommonSearch.SearchParams, null, null, DateTime.Now.AddMonths(-1), DateTime.Now);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtsubJon;
                string subjob = txtsubJon.Text.Trim();
                this.Cursor = Cursors.Default;
                
                _CommonSearch.ShowDialog();

                if (!string.IsNullOrEmpty(txtsubJon.Text))
                {
                    DataTable odt = new DataTable();
                    odt = CHNLSVC.CommonSearch.GetJobNo(txtsubJon.Text.Trim(), BaseCls.GlbUserDefLoca);
                    if (odt.Rows.Count > 0)
                    {
                        foreach (DataRow row in odt.Rows)
                        {
                            txtJobNumber.Text = odt.Rows[0]["Job Number"] == DBNull.Value ? string.Empty : odt.Rows[0]["Job Number"].ToString();
                            txtJobNumber.Focus();
                            txtJobNumber_Leave(null, null);
                        }
                    }
                }
            }
            catch (Exception )
            {
                
                throw;
            }
           
        }

        private void btnSubjob_Click(object sender, EventArgs e)
        {
            txtsubJon_DoubleClick(null, null);
        }
    }
}